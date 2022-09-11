
using EMY.Restaurant.Core.Domain.Common;
using System;

public static class IIR
{
    public static bool IsInRole(this System.Security.Claims.ClaimsPrincipal User, string Form, AuthType auth)
    {
        return User.IsInRole(GetAuthCode(Form, auth)) || User.IsInRole(GetAuthCode(Form, AuthType.Full)) || User.IsInRole(GetAuthCode("Admin", AuthType.Full));
    }

    public static bool IsInRole(this System.Security.Claims.ClaimsPrincipal User, Forms Form, AuthType auth)
    {
        return User.IsInRole(GetAuthCode(Form.ToString(), auth)) || User.IsInRole(GetAuthCode(Form.ToString(), AuthType.Full)) || User.IsInRole(GetAuthCode(Forms.Admin.ToString(), AuthType.Full));
    }
    static string GetAuthCode(string FormName, AuthType AuthorizeType)
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