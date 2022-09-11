using EMY.Restaurant.Core.Application.Repositories.OrderItemRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.BasketItemRepositories
{
    public class OrderItemReadRepository : ReadRepository<OrderItem>, IOrderItemReadRepository
    {
        public OrderItemReadRepository(DbContext context) : base(context)
        {
        }
    }
}
