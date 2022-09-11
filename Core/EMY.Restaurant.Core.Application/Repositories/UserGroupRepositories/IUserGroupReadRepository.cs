using EMY.Restaurant.Core.Domain.Entities;
using System.Collections.Generic;

namespace EMY.Restaurant.Core.Application.Repositories.UserGroupRepositories
{
    public interface IUserGroupReadRepository : IReadRepository<UserGroup>
    {
        List<UserGroupRole> GetUserGroupRolesFromUserGroup(string userGroupID);
    }
}
