using EMY.Restaurant.Core.Application.Repositories.MailListRepositories;
using EMY.Restaurant.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMY.Restaurant.Infrastructure.Persistence.Repositories.MailListRepositories
{
    public class MailListWriteRepository : WriteRepository<MailList>, IMailListWriteRepository
    {
        public MailListWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
