using EMY.Restaurant.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMY.Restaurant.Core.Application.Repositories.MenuCategoryRepositories
{
    public interface IMenuCategoryReadRepository : IReadRepository<MenuCategory>
    {
        Task<List<MenuCategory>> GetAllMenuCategoryWithMenus();
    }
}
