using System;
using System.Web.Security;

public partial class ExtLogout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        FormsAuthentication.SignOut();
        Response.Redirect("ExtLogin.aspx");
    }
}
