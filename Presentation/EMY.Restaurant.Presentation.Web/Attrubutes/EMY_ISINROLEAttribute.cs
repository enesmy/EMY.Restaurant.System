
using EMY.Restaurant.Core.Domain.Common;
using EMY.Restaurant.Presentation.Web.Statics;
using Microsoft.AspNetCore.Authorization;
/// <summary>
/// Specifies that the class or method that this attribute is applied to requires role-based authorization. <br />
/// To authorize users with either role A or role B, use:
/// <code>
/// [EMY_ISINROLE("A", "B")]
/// </code>
/// To only authorize users with both role A and role B, use:
/// <code>
/// [EMY_ISINROLE("A")] <br />
/// [EMY_ISINROLE("B")]
/// </code>
/// </summary>
public class EMY_ISINROLEAttribute : AuthorizeAttribute
{
    public EMY_ISINROLEAttribute(string FormName, AuthType auth)
    {
        string NormalAuth = AuthTypeHelper.GetAuthCode(FormName, auth);
        string FullAuth = AuthTypeHelper.GetAuthCode(FormName, AuthType.Full);
        string AdminAuth = AuthTypeHelper.GetAuthCode("Admin", AuthType.Full);
        Roles = NormalAuth + "," + FullAuth + "," + AdminAuth;
        AuthenticationSchemes = SystemMainStatics.DefaultScheme;
    }


    public EMY_ISINROLEAttribute(Forms Form, AuthType auth)
    {
        string NormalAuth = AuthTypeHelper.GetAuthCode(Form.ToString(), auth);
        string FullAuth = AuthTypeHelper.GetAuthCode(Form.ToString(), AuthType.Full);
        string AdminAuth = AuthTypeHelper.GetAuthCode(Forms.Admin.ToString(), AuthType.Full);
        Roles = NormalAuth + "," + FullAuth + "," + AdminAuth;
        AuthenticationSchemes = SystemMainStatics.DefaultScheme;
    }
    public EMY_ISINROLEAttribute()
    {
        AuthenticationSchemes = SystemMainStatics.DefaultScheme;
    }


}