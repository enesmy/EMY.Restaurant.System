using EMY.Restaurant.Core.Application.Repositories.UserGroupRepositories;
using EMY.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Restaurant.Core.Application.Repositories.UserRepositories;
using EMY.Restaurant.Core.Domain.Common;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.UserRepositories
{
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        IUserGroupReadRepository _userGroupReadRepository;
        IUserGroupRoleReadRepository _userGroupRoleReadRepository;
        public UserReadRepository(DbContext context, IUserGroupReadRepository userGroupReadRepository, IUserGroupRoleReadRepository userGroupRoleReadRepository) : base(context)
        {
            _userGroupReadRepository = userGroupReadRepository;
            _userGroupRoleReadRepository = userGroupRoleReadRepository;
        }

        public async Task<ResultModel<User>> CanIChangePassword(Guid userID, string newPassword, string hiddenQuestionAnswer)
        {
            ResultModel<User> result = new ResultModel<User>();
            result.IsSuccess = false;

            //Password is empty?
            if (string.IsNullOrEmpty(newPassword))
            {
                result.Message = "Please write a password!";
                return result;
            }

            User prf = await GetByIdAsync(userID);

            //hidden answer is correct?

            if (prf.HiddenQuestionAnswer != hiddenQuestionAnswer)
            {
                result.Message = "Hidden answer is not correct!";
                return result;
            }
            result.Value = prf;
            result.Message = "You can use this password!";
            result.IsSuccess = true;
            return result;
        }

        public async Task<ResultModel<User>> CheckLoginProfile(string userName, string password)
        {
            ResultModel<User> result = new ResultModel<User>() { IsSuccess = false };

            var usrs = Table.Where(o => o.IsActive && !o.IsDeleted && o.UserName.ToLower() == userName.ToLower());
            if (usrs.Count() == 0)
            {
                result.Message = "User name is not found!";
                return result;
            }

            foreach (var user in usrs)
            {
                if (user.PasswordControl(password))
                {
                    if (user.IsLocked && user.LockedTime < DateTime.Now.AddMinutes(-Configuration.LockedMinutes))
                    {
                        var diff = DateTime.Now.AddMinutes(-Configuration.LockedMinutes) - user.LockedTime.Value;
                        result.Message = $"Please {diff.Minutes} minutes after re try! Because this user is forced!";
                        return result;
                    }
                    user.Password = "";
                    result.Value = user;
                    result.IsSuccess = true;
                    result.Message = $"Welcome {user.Name} {user.LastName}";
                    return result;
                }
            }

            result.Message = "Password is not correct!";
            usrs.ToList().ForEach((usr) =>
            {
                if (!usr.IsLocked && usr.WrongForceCount + 1 >= Configuration.MaxWrongTryCount && usr.LastWrongTryingTime < DateTime.Now.AddMinutes(-Configuration.LockedMinutes))
                {
                    usr.WrongForceCount = 0;
                    usr.LastWrongTryingTime = DateTime.Now;
                    usr.LockedTime = DateTime.Now;
                    usr.IsLocked = true;
                    Table.Update(usr);
                    result.Message = $"Please {Configuration.LockedMinutes} minutes after re try! Because this user is forced and now `Locked!´" + Environment.NewLine + $"You tried {Configuration.MaxWrongTryCount} times wrong!";
                }
                else
                {
                    usr.WrongForceCount++;
                    usr.LastWrongTryingTime = DateTime.Now;
                    Table.Update(usr);
                }
            });

            return result;
        }

        public async Task<List<UserGroupRole>> GetAllRoles(Guid userID)
        {
            var user = await GetByIdAsync(userID);
            if (user == null) return new List<UserGroupRole>();

            var group = await _userGroupReadRepository.GetByIdAsync(user.UserGroupID);
            if (group == null) return new List<UserGroupRole>();

            var roles = _userGroupRoleReadRepository.GetWhere(o => o.UserGroupID == group.UserGroupID).ToList();
            return roles;
        }
    }
}
