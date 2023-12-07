using EMY.Restaurant.Core.Application.Repositories.PostRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.PostRepositories
{
    public class PostReadRepository : ReadRepository<Post>, IPostReadRepository
    {
        public PostReadRepository(DbContext context) : base(context)
        {
        }
    }
}
