using EMY.Restaurant.Core.Domain.Common;
using EMY.Restaurant.Core.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace EMY.Restaurant.Core.Application.Repositories.UserRepositories
{
    public interface IUserWriteRepository : IWriteRepository<User>
    {

        Task<ResultModel<User>> ChangePassword(Guid userID, string newPassword, string hiddenQuestionAnswer);
        Task<bool> CheckRoleIsExist(string userID, string formName, AuthType type);
        Task DeActivate(string userID, string deactivaterRef);

    }
}
