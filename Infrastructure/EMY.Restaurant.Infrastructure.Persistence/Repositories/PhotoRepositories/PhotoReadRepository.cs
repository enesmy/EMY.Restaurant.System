using EMY.Restaurant.Core.Application.Repositories.PhotoRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.PhotoRepositories
{
    public class PhotoReadRepository : ReadRepository<Photo>, IPhotoReadRepository
    {
        public PhotoReadRepository(DbContext context) : base(context)
        {
        }
    }
}
