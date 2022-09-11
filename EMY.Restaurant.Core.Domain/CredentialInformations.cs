namespace EMY.Restaurant.Core.Domain
{
    public class CredentialInformations
    {
        public string CredentialPassword { get; set; }
        public string DisplayName { get; set; }
        public string MailAdress { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool RequireSSL { get; set; }
    }
}
