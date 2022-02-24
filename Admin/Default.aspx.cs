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

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if ((Session["IsAdmin"] == null) || ((bool)Session["IsAdmin"] == false))
			Response.Redirect("..");

        if (this.User.Identity.Name.ToLower().EndsWith("saiban"))
        {
            //HyperLink4.Visible = false;
            //HyperLink5.Visible = false;
        }
    }
}
