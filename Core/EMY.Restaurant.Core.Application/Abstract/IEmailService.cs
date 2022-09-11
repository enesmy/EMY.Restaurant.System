using System.Net.Mail;
using System.Threading.Tasks;

namespace EMY.Restaurant.Core.Application.Abstract
{
    public interface IEmailService
    {
        Task<(bool IsSuccess, string Message)> SendEmail(string email, string subject, string message, MailPriority mailPriority);
    }
}
