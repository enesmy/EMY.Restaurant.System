using EMY.Restaurant.Core.Application.Repositories.OrderRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.BasketRepositories
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
