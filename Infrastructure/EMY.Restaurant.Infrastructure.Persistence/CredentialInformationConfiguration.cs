using EMY.Restaurant.Core.Domain.Common;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EMY.Restaurant.Infrastructure.Persistence
{
    public static partial class CredentialInformationConfiguration
    {

        static ConfigurationManager _configurationManager;
        static ConfigurationManager configuration
        {
            get
            {
                if (_configurationManager == null)
                {
                    _configurationManager = new();
                    _configurationManager.SetBasePath(Directory.GetCurrentDirectory());
                    _configurationManager.AddJsonFile(ConfigFileLocation);
                }

                return _configurationManager;
            }
        }

        public static string DisplayName
        {
            get
            {
                string result = configuration["DisplayName"];
                return result;
            }
        }

        public static string CredentialPassword
        {
            get
            {
                string result = configuration["CredentialPassword"];
                return result;
            }
        }

        public static string MailAdress
        {
            get
            {
                string result = configuration["MailAdress"];
                return result;
            }
        }

        public static string SmtpServer
        {
            get
            {
                string result = configuration["SmtpServer"];
                return result;
            }
        }

        public static int SmtpPort
        {
            get
            {
                int result = int.Parse(configuration["SmtpPort"]);
                return result;
            }
        }
        public static bool RequireSSL
        {
            get
            {
                bool result = bool.Parse(configuration["RequireSSL"]);             
                return result;
            }
        }

        static string ConfigFileLocation
        {
            get
            {
                return Directory.GetCurrentDirectory() + "/MailConfig.json";
            }
        }
      
    }

}
