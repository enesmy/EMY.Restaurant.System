using System;

namespace EMY.Restaurant.Core.Domain.Common
{
    public static class AuthTypeHelper
    {
        public static string GetAuthCode(string FormName, AuthType AuthorizeType)
        {
            string result = AuthorizeType switch
            {
                AuthType.Read => FormName + "Show",
                AuthType.Write => FormName + "Add",
                AuthType.Delete => FormName + "Del",
                AuthType.Update => FormName + "Up",
                AuthType.Full => FormName + "Full",
                _ => throw new NotSupportedException()
            };
            return result;
        }
    }
}
