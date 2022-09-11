using EMY.Restaurant.Core.Application.Repositories.UserGroupRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRepositories
{
    public class UserGroupWriteRepository : WriteRepository<UserGroup>, IUserGroupWriteRepository
    {
        public UserGroupWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
