using EMY.Restaurant.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EMY.Restaurant.Core.Application.Repositories.MenuRepositories
{
    public interface IMenuReadRepository : IReadRepository<Menu>
    {
        IList<Menu> GetMenuFromCategory(Guid guid);
        IList<Menu> GetAllMenu();
    }
}
