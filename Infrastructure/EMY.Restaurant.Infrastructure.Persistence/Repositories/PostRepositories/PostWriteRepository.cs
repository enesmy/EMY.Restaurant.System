using EMY.Restaurant.Core.Application.Repositories.PostRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.PostRepositories
{
    internal class PostWriteRepository : WriteRepository<Post>, IPostWriteRepository
    {
        public PostWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
