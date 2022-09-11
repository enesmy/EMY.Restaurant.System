using EMY.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRoleRepositories
{
    public class UserGroupRoleReadRepository : ReadRepository<UserGroupRole>, IUserGroupRoleReadRepository
    {
        public UserGroupRoleReadRepository(DbContext context) : base(context)
        {
        }
    }
}
