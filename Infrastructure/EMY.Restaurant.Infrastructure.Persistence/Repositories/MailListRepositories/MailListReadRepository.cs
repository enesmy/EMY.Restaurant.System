using EMY.Restaurant.Core.Application.Repositories.MailListRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.MailListRepositories
{
    public class MailListReadRepository : ReadRepository<MailList>, IMailListReadRepository
    {
        public MailListReadRepository(DbContext context) : base(context)
        {
        }
    }
}
