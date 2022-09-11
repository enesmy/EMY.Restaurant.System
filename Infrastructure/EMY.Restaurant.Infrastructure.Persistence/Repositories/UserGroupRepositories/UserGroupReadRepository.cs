using EMY.Restaurant.Core.Application.Repositories.UserGroupRepositories;
using EMY.Restaurant.Core.Application.Repositories.UserGroupRoleRepositories;
using EMY.Restaurant.Core.Domain.Common;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.UserGroupRepositories
{
    public class UserGroupReadRepository : ReadRepository<UserGroup>, IUserGroupReadRepository
    {
        IUserGroupRoleReadRepository _userGroupRoleReadRepository;
        public UserGroupReadRepository(DbContext context, IUserGroupRoleReadRepository userGroupRoleReadRepository) : base(context)
        {
            _userGroupRoleReadRepository = userGroupRoleReadRepository;
        }

        public List<UserGroupRole> GetUserGroupRolesFromUserGroup(string userGroupID)
        {
            return _userGroupRoleReadRepository.GetWhere(o => o.UserGroupID == userGroupID.ToGuid() && !o.IsDeleted, false).ToList();
        }


    }
}
