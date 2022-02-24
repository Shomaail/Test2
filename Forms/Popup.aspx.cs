using System;
using BL.Data;
    public partial class Forms_Popup : System.Web.UI.Page
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
            Response.Redirect("~/Tasks.aspx?applicationID=" + Request.QueryString["applicationID"] + "&roleID=" + Request.QueryString["roleID"], false);

        }
    }
