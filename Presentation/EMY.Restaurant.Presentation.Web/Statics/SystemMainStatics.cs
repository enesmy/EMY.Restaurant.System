using EMY.Restaurant.Core.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EMY.Restaurant.Presentation.Web.Statics
{
    public static class SystemMainStatics
    {
        public const string DefaultScheme = "EmyRestaurantScheme";

        public const string ClaimIdentity = "EmyIdentityRestaurantService";
        public static string AuthorizeErrorMessage = "You do not have enaugh authorize!";
        public static bool IsBetween(this DateTime selectedDate, DateTime dtBegin, DateTime dtEnd) =>
            (selectedDate <= dtEnd && selectedDate >= dtBegin) || (selectedDate <= dtBegin && selectedDate >= dtEnd);
        public static Guid ActiveUserID(this Controller controller) => controller.User.Identity.Name.ToGuid();

        public static string CreateOrderNumber(this long id) => DecimalToArbitrarySystem(id + (34 * 34 * 34 * 34), 34);
        static string DecimalToArbitrarySystem(long decimalNumber, int radix)
        {
            const int BitsInLong = 64;
            const string Digits = "T5AWIQ3Z912BF0EGYX674VKL8DORSUHJPN";

            if (radix < 2 || radix > Digits.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

            if (decimalNumber == 0)
                return "0";

            int index = BitsInLong - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[BitsInLong];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % radix);
                charArray[index--] = Digits[remainder];
                currentNumber = currentNumber / radix;
            }

            string result = new String(charArray, index + 1, BitsInLong - index - 1);
            if (decimalNumber < 0)
            {
                result = "-" + result;
            }

            return result;
        }
    }



}
