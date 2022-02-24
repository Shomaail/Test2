using System;
using System.Data;
using System.DirectoryServices;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using KFUPM.CAS;

public partial class Login : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Keys.Count > 0)
        {
            if (Session["user"] == "NonFacultyEmployee")
            {                    
                    divLogin.Visible = false;
                    divAfterLogin.Visible = true;
                    lblLoginMessage.Text = "The faculty promotion system is designed for KFUPM Teaching Faculty use only.";
                    return;
            }
            else if (Session["user"] == "NonKFUPM")
            {
                divLogin.Visible = false;
                divAfterLogin.Visible = true;
                lblLoginMessage.Text = "You are logged in successfully, however there seems to be some problem with your profile infomation. Kindly contact ITC.";
                return;            
            }
        }

        //if (Request.Browser.Browser != "InternetExplorer" && Request.Browser.Browser != "IE")
        //{
        //    labelWarning.Visible = true;
        //    labelWarning.Text = "You are using " + Request.Browser.Browser + ". The Website is best run on the web browser: Internet Explorer";
        //}
        
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Main.aspx");
    }
    protected void btnExtLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExtLogin.aspx");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("https://login.kfupm.edu.sa:8447/cas-web/logout?url=" + Request.UrlReferrer.ToString());       
    }
}

