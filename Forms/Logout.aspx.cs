using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Forms_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cookies.Remove("langCookie"); 
        FormsAuthentication.SignOut();
        Session.Clear();
        Response.Redirect("https://login.kfupm.edu.sa:8447/cas-web/logout?url=http://facultypromotion.kfupm.edu.sa/Login.aspx");
        //Response.Redirect("~/Main.aspx");
    }
}
