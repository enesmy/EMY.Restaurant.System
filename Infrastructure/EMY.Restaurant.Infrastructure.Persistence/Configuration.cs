using Microsoft.Extensions.Configuration;
using System.IO;

namespace EMY.Restaurant.Infrastructure.Persistence
{
    public static class Configuration
    {
   
        
        internal static string ConnectionString
        {
            get
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return connectionString;
            }
        }
        public static string SystemName
        {
            get
            {
                var systemname = configuration["SystemName"];
                return systemname;
            }
        }
        static ConfigurationManager configuration
        {
            get
            {
                ConfigurationManager conf = new();
                conf.SetBasePath(Directory.GetCurrentDirectory());
                conf.AddJsonFile(ConfigFileLocation);
                return conf;
            }
        }
        static string ConfigFileLocation
        {
            get
            {
                return Directory.GetCurrentDirectory() + "/DBConfig.json";
            }
        }

        public static int LockedMinutes { get; internal set; }
        public static int MaxWrongTryCount { get; internal set; }
    }

}
