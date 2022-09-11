using EMY.Restaurant.Core.Domain.Common;
using EMY.Restaurant.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMY.Restaurant.Core.Application.Repositories.UserRepositories
{
    public interface IUserReadRepository : IReadRepository<User>
    {
        Task<ResultModel<User>> CheckLoginProfile(string userName, string password);
        Task<List<UserGroupRole>> GetAllRoles(Guid userID);
        Task<ResultModel<User>> CanIChangePassword(Guid userID, string newPassword, string hiddenQuestionAnswer);
    }
}
