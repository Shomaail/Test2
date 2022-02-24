using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Data;
public partial class ExtForms_Popup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Message"] != null)
        {
            labelProgrammaticPopup0.Text = Session["Message"].ToString();
        }
        else
        {
            labelProgrammaticPopup0.Text = "Email Sent";
        }
        
        programmaticModalPopup0.Show();
       
    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        Session["Message"] = null;
        programmaticModalPopup0.Hide();
        Response.Redirect("ExtMain.aspx");
    }
}