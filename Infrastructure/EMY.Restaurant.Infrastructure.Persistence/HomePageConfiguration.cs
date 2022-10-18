using EMY.Restaurant.Core.Domain.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace EMY.Restaurant.Infrastructure.Persistence
{
    public static class HomePageConfiguration
    {
        static ConfigurationManager _configurationManager;

        static ConfigurationManager configuration
        {
            get
            {
                if (_configurationManager == null)
                {
                    LoadConfiguration();
                }
                return _configurationManager;
            }
        }

        private static void LoadConfiguration()
        {
            _configurationManager = new();
            _configurationManager.SetBasePath(Directory.GetCurrentDirectory());
            _configurationManager.AddJsonFile(ConfigFileLocation);

        }

        #region 3lü slider
        public static string FirstImage
        {
            get
            {
                string result = configuration["FirstImage"];
                return result;
            }
        }

        public static string FirstHeader
        {
            get
            {
                string result = configuration["FirstHeader"];
                return result;
            }
        }

        public static string FirstText
        {
            get
            {
                string result = configuration["FirstText"];
                return result;
            }
        }
        public static string FirstUrl
        {
            get
            {
                string result = configuration["FirstUrl"];
                return result;
            }
        }

        public static string SecondImage
        {
            get
            {
                string result = configuration["SecondImage"];

                return result;
            }
        }

        public static string SecondHeader
        {
            get
            {
                string result = configuration["SecondHeader"];
                return result;
            }
        }

        public static string SecondText
        {
            get
            {
                string result = configuration["SecondText"];
                return result;
            }
        }

        public static string SecondUrl
        {
            get
            {
                string result = configuration["SecondUrl"];
                return result;
            }
        }
        public static string ThirdImage
        {
            get
            {
                string result = configuration["ThirdImage"];
                return result;
            }
        }

        public static string ThirdHeader
        {
            get
            {
                string result = configuration["ThirdHeader"];
                return result;
            }
        }

        public static string ThirdText
        {
            get
            {
                string result = configuration["ThirdText"];
                return result;
            }
        }
        public static string ThirdUrl
        {
            get
            {
                string result = configuration["ThirdUrl"];
                return result;
            }
        }
        #endregion

        #region Video
        public static string YoutubeVideoPhoto
        {
            get
            {
                string result = configuration["YoutubeVideoPhoto"];
                return result;
            }
        }

        public static string YoutubeVideoUrl
        {
            get
            {
                string result = configuration["YoutubeVideoUrl"];
                return result;
            }
        }

        public static string VideoRightsideHeader
        {
            get
            {
                string result = configuration["VideoRightsideHeader"];
                return result;
            }
        }

        public static string VideoRightsideContent
        {
            get
            {
                string result = configuration["VideoRightsideContent"];
                return result;
            }
        }
        #endregion

        #region Celebration
        public static string CelebrationImageUrl
        {
            get
            {
                string result = configuration["CelebrationImageUrl"];
                return result;
            }
        }

        public static string CelebrationHeader1st
        {
            get
            {
                string result = configuration["CelebrationHeader1st"];
                return result;
            }
        }

        public static string CelebrationHeader2nd
        {
            get            {

                string result = configuration["CelebrationHeader2nd"];
                return result;
            }
        }

        public static string CelebrationContent
        {
            get
            {
                string result = configuration["CelebrationContent"];
                return result;
            }
        }

        public static string CelebrationUrl
        {
            get
            {
                string result = configuration["CelebrationUrl"];
                return result;
            }
        }
        #endregion

        #region Contact Informations
        public static string WhatsappNumber
        {
            get
            {
                string result = configuration["WhatsappNumber"];
                return result;
            }
        }

        public static string PhoneNumber
        {
            get
            {
                string result = configuration["PhoneNumber"];
                return result;
            }
        }

        public static string Adress
        {
            get
            {
                string result = configuration["Adress"];
                return result;
            }
        }

        public static string Email
        {
            get
            {
                string result = configuration["Email"];
                return result;
            }
        }

        public static string OpenHours
        {
            get
            {
                string result = configuration["OpenHours"];
                return result;
            }
        }

        public static string FaceBook
        {
            get
            {
                string result = configuration["FaceBook"];
                return result;
            }
        }
        public static string Instagram
        {
            get
            {
                string result = configuration["Instagram"];
                return result;
            }
        }
        public static string Youtube
        {
            get
            {
                string result = configuration["Youtube"];
                return result;
            }
        }


        #endregion

        #region Footer
        public static string Datenschutz
        {
            get
            {
                string result = configuration["Datenschutz"];
                return result;
            }
        }

        public static string Impressum
        {
            get
            {
                string result = configuration["Impressum"];
                return result;
            }
        }
        #endregion

        public static string CreateOrderMail
        {
            get
            {
                string result = configuration["CreateOrderMail"];
                return result;
            }
        }

        public static string CreateReservationMail
        {
            get
            {
                string result = configuration["CreateReservationMail"];
                return result;
            }
        }

        public static string CreateEmailRegistrationMail
        {
            get
            {
                string result = configuration["CreateEmailRegistrationMail"];
                return result;
            }
        }

                public static string KategorienBG
        {
            get
            {
                string result = configuration["KategorienBG"];
                return result;
            }
        }

        public static string WhatsappBG
        {
            get
            {
                string result = configuration["WhatsappBG"];
                return result;
            }
        }

        public static string OthersBG
        {
            get
            {
                string result = configuration["OthersBG"];
                return result;
            }
        }

        public static (bool isSucces, string Message) SetValue(string key, string value)
        {
            var json = "{}";
            if (File.Exists(ConfigFileLocation)) json = System.IO.File.ReadAllText(ConfigFileLocation);

            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);
            jsonObj[key] = value;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            try
            {
                System.IO.File.WriteAllText(ConfigFileLocation, output);
                LoadConfiguration();
                return (true, "Succeed");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        static string ConfigFileLocation
        {
            get
            {
                return Directory.GetCurrentDirectory() + "/HomePageContent.json";
            }
        }

      

        static void Crypt(CryptType type)
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (!isWindows) return;

            if (System.IO.File.Exists(ConfigFileLocation))
            {
                FileInfo fi = new FileInfo(ConfigFileLocation);

                if (type == CryptType.Encrypt) fi.Encrypt();
                else fi.Decrypt();
            }

        }
    }
}
