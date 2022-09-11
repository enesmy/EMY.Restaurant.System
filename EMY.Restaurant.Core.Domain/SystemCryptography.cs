using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EMY.Restaurant.Core.Domain
{
    public class SystemCryptography
    {
        public static string Encrypt(string toEncrypt)
        {
            using (var sha1 = new SHA512Managed())
            {
                var hash = Encoding.UTF8.GetBytes(toEncrypt);
                var generatedHash = sha1.ComputeHash(hash);

                //begin salt
                generatedHash = generatedHash.Select(x => (byte)(x > 127 ? ++x : --x)).ToArray();
                //end salt


                var generatedHashString = Convert.ToBase64String(generatedHash);
                return generatedHashString;
            }
        }


    }
}
