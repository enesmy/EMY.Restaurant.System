using EMY.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRoleRepositories
{
    public class UserGroupRoleWriteRepository : WriteRepository<UserGroupRole>, IUserGroupRoleWriteRepository
    {
        public UserGroupRoleWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
