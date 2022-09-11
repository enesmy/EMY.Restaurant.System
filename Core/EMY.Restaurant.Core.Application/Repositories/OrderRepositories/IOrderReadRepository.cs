using EMY.Restaurant.Core.Domain.Entities;
using EMY.Restaurant.Core.Domain.ViewModels;

namespace EMY.Restaurant.Core.Application.Repositories.OrderRepositories
{
    public interface IOrderReadRepository : IReadRepository<Order>
    {
        BasketStatsViewModel BasketStats();
    }
}
