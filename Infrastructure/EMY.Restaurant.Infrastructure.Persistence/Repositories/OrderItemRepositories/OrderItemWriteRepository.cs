using EMY.Restaurant.Core.Application.Repositories.OrderItemRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.BasketItemRepositories
{
    public class OrderItemWriteRepository : WriteRepository<OrderItem>, IOrderItemWriteRepository
    {
        public OrderItemWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
