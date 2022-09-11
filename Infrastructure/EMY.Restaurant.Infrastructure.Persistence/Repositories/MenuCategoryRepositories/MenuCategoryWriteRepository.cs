using EMY.Restaurant.Core.Application.Repositories.MenuCategoryRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.MenuCategoryRepositories
{
    public class MenuCategoryWriteRepository : WriteRepository<MenuCategory>, IMenuCategoryWriteRepository
    {
        public MenuCategoryWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
