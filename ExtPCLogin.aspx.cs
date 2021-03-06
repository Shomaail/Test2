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

public partial class _ExtPCLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session != null)
            if (Session["login"] != null && Session["login"].ToString().Equals("true"))
                Response.Redirect("ExtPCForms/ExtPCMain.aspx");
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        int ackSend = 0;
        PromotionTableAdapters.PromotionCommitteeTempTableAdapter adapterPC = new PromotionTableAdapters.PromotionCommitteeTempTableAdapter();
        Promotion.PromotionCommitteeTempDataTable tablePC = adapterPC.GetByUsernamePassword(txtUserName.Text.ToString(), txtPassword.Text.ToString());
        PromotionTableAdapters.OldPromotionCommitteeTableAdapter adapterOldPC = new PromotionTableAdapters.OldPromotionCommitteeTableAdapter();
        if (tablePC.Count > 0)
        {
            ackSend = Int32.Parse(adapterOldPC.ScalarQuery(tablePC[0].ApplicationID).ToString());
            if (ackSend == 0)
            {
                lblEx.Text = "Your account is not activated yet. Please wait for the acknowledment email for the activation of your account.";
                lblEx.Visible = true;
                return;

            }
            Session["pcID"] = tablePC[0].AutoNumberPK;
            Session["applicationID"] = tablePC[0].ApplicationID;
            Session["ApplicationIDGlobal"] = tablePC[0].ApplicationID;
            Session["login"] = "true";
            Session["pcMem"] = tablePC[0];
            Session["IsDeputy"] = false;
            Session["IsAdmin"] = false;
            Response.Redirect("ExtPCForms/ExtPCMain.aspx");
        }
        else
        {
            lblEx.Text = "Invalid Username or Password. Please try again";
            lblEx.Visible = true;
            txtPassword.Text = "";
            txtUserName.Text = "";
        }

    }
}

