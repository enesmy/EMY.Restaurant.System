using EMY.Restaurant.Core.Application.Abstract;
using EMY.Restaurant.Core.Domain;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EMY.Restaurant.Infrastructure.Persistence.Concrete
{
    public class EmailManager : IEmailService
    {

        public async Task<(bool IsSuccess, string Message)> SendEmail(string email, string subject, string message, MailPriority mailPriority)
        {
            if (string.IsNullOrWhiteSpace(email)) return (false, "Email is empty!");
            EmailSendConfigure emailConfig = new EmailSendConfigure()
            {
                From = CredentialInformationConfiguration.MailAdress,
                ClientCredentialPassword = CredentialInformationConfiguration.CredentialPassword,
                ClientCredentialUserName = CredentialInformationConfiguration.MailAdress,
                EnableSSL = CredentialInformationConfiguration.RequireSSL,
                FromDisplayName = CredentialInformationConfiguration.DisplayName,
                Port = CredentialInformationConfiguration.SmtpPort,
                Priority = mailPriority,
                Subject = subject,
                SMTPServer = CredentialInformationConfiguration.SmtpServer,
                TOs = new string[] { email }
            };
            EmailContent content = new EmailContent()
            {
                Content = message,
                IsHtml = true
            };
            MailMessage mailMessage = ConstructEmailMessage(emailConfig, content);
            try
            {
                var result = await Send(mailMessage, emailConfig);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private MailMessage ConstructEmailMessage(EmailSendConfigure emailConfig, EmailContent content)
        {
            MailMessage msg = new System.Net.Mail.MailMessage();
            if (emailConfig.TOs != null)
                foreach (string to in emailConfig.TOs)
                {
                    if (!string.IsNullOrEmpty(to))
                    {
                        msg.To.Add(to);
                    }
                }
            if (emailConfig.CCs != null)
                foreach (string cc in emailConfig.CCs)
                {
                    if (!string.IsNullOrEmpty(cc))
                    {
                        msg.CC.Add(cc);
                    }
                }

            msg.From = new MailAddress(emailConfig.From,
                                       emailConfig.FromDisplayName,
                                       System.Text.Encoding.UTF8);
            msg.IsBodyHtml = content.IsHtml;
            msg.Body = content.Content;
            msg.Priority = emailConfig.Priority;
            msg.Subject = emailConfig.Subject;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;

            if (!string.IsNullOrEmpty(content.AttachFileName))
            {
                Attachment data = new Attachment(content.AttachFileName);
                msg.Attachments.Add(data);
            }

            return msg;
        }

        private async Task<(bool IsSuccess, string Message)> Send(MailMessage message, EmailSendConfigure emailConfig)
        {
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(
                                  emailConfig.ClientCredentialUserName,
                                  emailConfig.ClientCredentialPassword);
            client.Host = emailConfig.SMTPServer;
            client.Port = emailConfig.Port;
            client.EnableSsl = emailConfig.EnableSSL;

            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
                return (false, $"Error in Send email: {e.Message}");

            }
            message.Dispose();
            return (true, "Success!");
        }
    }
}
