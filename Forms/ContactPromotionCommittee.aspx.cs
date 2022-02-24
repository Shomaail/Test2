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
using BL.Data;
using System.Collections.Generic;
using System.Linq;

public partial class Forms_ContactPromotionCommittee : System.Web.UI.Page
{
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        int appID = int.Parse(Request.Params.Get("applicationID").ToString());

        if (Master.CurrentFormLevel == -1)
        {
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            return;
        }
        gvForm_FinalPC.DataSource = bal.GetForm_FinalPC(appID);
        gvForm_FinalPC.DataBind();
        string emailOfPC = "";
        List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(Master.ApplicationID);

        foreach (Form_FinalPC pc in lffpc)
        {
            if (pc.ExternalEmployee.ExternalEmployeeID == 0)
            {
                emailOfPC += pc.Employee.Email;

            }
            else
            {
                emailOfPC += pc.ExternalEmployee.Email;
            }
            emailOfPC += ";";
        }
        aLink.HRef = "mailto:" + emailOfPC + "?subject=A message to Promotion Committee of the Promotion Application of " + Master.Applicant.NameString + " &body= This is a message to the Promotion Committee of " + Master.Applicant.NameString + " in the " + Master.Applicant.Department + ", applying for the promotion to the rank of " + lffpc[0].Application.ForRank;

        Instruction1.Text = Master.CurrentFormInstruction;
    //    ComposeEmail1.Visible = false;

    }
    protected void ComposeEmail_OnEmailSent(object sender, EventArgs e)
    {
        Alert1.Message("Email has been successfully send.");
       // ComposeEmail1.Visible = false;
    }
    protected void ComposeEmail_OnEmailNotSent(object sender, EventArgs e)
    {        
       // ComposeEmail1.Visible = false;
    }
    //public void AlertMessage(string strMessage)
    //{
    //    string strScript = "<script language=JavaScript>";
    //    strScript += "alert('" + strMessage + "');";
    //    strScript += "</script>";
    //    if (!Page.IsStartupScriptRegistered("clientScript"))
    //        Page.RegisterStartupScript("clientScript", strScript);
    //}

    protected void ButtonSend_Click(object sender, EventArgs e)
    {
        int appID = int.Parse(Request.Params.Get("applicationID").ToString());
        List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(appID);
        if (lffpc.Count != 5)
        {
            Alert1.Message("The Promotion Committee is not yet finalized or the VRGSSR has not saved the Promotion Committee after changes. Email cannot be sent.");
            return;
        }
            
        string emailAddresses =     lffpc[0].Employee.Email
                            + "," + lffpc[1].Employee.Email
                            + "," + lffpc[2].Employee.Email
                            + "," + lffpc[3].Employee.Email
                            + "," + lffpc[4].Employee.Email
                            + "," + "facpromote@kfupm.edu.sa";
        Email.ComposeEmailDataTable email = new Email.ComposeEmailDataTable();
        email.NewComposeEmailRow();
        email.AddComposeEmailRow(emailAddresses, "",  @"Please Reply to: " + bal.GetForm_FinalPC(appID)[0].Employee.Email);
    //    ComposeEmail1.LoadData(email);
    //    ComposeEmail1.Visible = true;

    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }
   
    public string GetStatus(int Position)
    {
        
        switch (Position)
        {
            case 10:
                return "Chairman";
            default:
                return "Promotion Committee Member";

        }
    }
}
