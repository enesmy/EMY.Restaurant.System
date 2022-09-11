using EMY.Restaurant.Core.Application.Repositories.PasswordHistoryRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.PasswordHistoryRepositories
{
    public class PasswordHistoryWriteRepository : WriteRepository<PasswordHistory>, IPasswordHistoryWriteRepository
    {
        public PasswordHistoryWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
