using EMY.Restaurant.Core.Application.Repositories.PasswordHistoryRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.PasswordHistoryRepositories
{
    public class PasswordHistoryReadRepository : ReadRepository<PasswordHistory>, IPasswordHistoryReadRepository
    {
        public PasswordHistoryReadRepository(DbContext context) : base(context)
        {
        }
    }
}
