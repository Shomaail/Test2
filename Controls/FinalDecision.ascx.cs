using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using BL.Data;
public partial class Controls_FinalDecision : System.Web.UI.UserControl
{
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
    }
    #region Properties
    public string VRGSSR
    {
        set
        {
            ViewState["VRGSSR"] = value;
        }
        get
        {
            if (ViewState["VRGSSR"] != null)
                return (string)ViewState["VRGSSR"];
            else
                return "";
        }
    }
    public string MessageBody
    {
        set
        {
            ViewState["MessageBody"] = value;
        }
        get
        {
            if (ViewState["MessageBody"] != null)
                return (string)ViewState["MessageBody"];
            else
                return "";
        }
    }
    public string FinalDecision
    {
        set
        {
            ViewState["FinalDecision"] = value;
        }
        get
        {
            if (ViewState["FinalDecision"] != null)
                return (string)ViewState["FinalDecision"];
            else
                return "";
        }
    }
    public int ApplicationID
    {
        set
        {
            ViewState["ApplicationID"] = value;
        }
        get
        {
            if (ViewState["ApplicationID"] != null)
                return (int)ViewState["ApplicationID"];
            else
                return -1;
        }
    }
    #endregion

    public void LoadComments(int applicationID)
    {
        ApplicationID = applicationID;
        PromotionTableAdapters.ApplicationTableAdapter adapter = new PromotionTableAdapters.ApplicationTableAdapter();

        Promotion.ApplicationDataTable table = adapter.GetApplication(applicationID);

        if (table.Count == 0) return;

        Promotion.ApplicationRow row = table[0];

        TextBoxRectorCom.Text = row.IsRectorCommentsNull() ? "" : row.RectorComments.ToString();
        TextBoxSCComments.Text = row.IsSCCommentsNull() ? "" : row.SCComments.ToString();
        RadioButtonListSCDecision.SelectedIndex = Convert.ToInt32(!row.SCDecision);
        RadioButtonListRectorDecision.SelectedIndex = Convert.ToInt32(!row.RectorDecision);
       // if (row.FinalDeicision != null)
            RadioButtonListFinalDec.SelectedIndex = Convert.ToInt32(!row.FinalDeicision);
    }

    public bool SaveComments(int applicationID)
    {

        PromotionTableAdapters.ApplicationTableAdapter adapter = new PromotionTableAdapters.ApplicationTableAdapter();

        try
        {
            adapter.UpdateFinalDecision(TextBoxSCComments.Text
                , !Convert.ToBoolean(RadioButtonListRectorDecision.SelectedIndex)
                , !Convert.ToBoolean(RadioButtonListFinalDec.SelectedIndex)
                , !Convert.ToBoolean(RadioButtonListSCDecision.SelectedIndex)
                , TextBoxRectorCom.Text
                , applicationID);
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void RadioButtonListFinalDec_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (bal.GetApplication(ApplicationID)[0].ApplicationClosed)
            return;

      //  ComposeEmail1.Visible = true;
        Email.ComposeEmailDataTable email = new Email.ComposeEmailDataTable();
        email.NewComposeEmailRow();
        VRGSSR = bal.GetEmployeeByPK((bal.GetApplicationRole(ApplicationID).Where<Application_Role>(a => a.RoleID == (byte)RoleID.TopAuthority).ToList<Application_Role>()[0].EmployeeID))[0].Name;

        if (RadioButtonListFinalDec.SelectedValue == "True")
        {
            MessageBody = bal.GetTaskExtMessage((int)TaskExtID.Promotion_Decision_Notification_Positive_Eng)[0].Message;
            MessageBody = MessageBody.Replace("@@Applicant@@", bal.GetApplicant(ApplicationID)[0].Name);
            MessageBody = MessageBody.Replace("@@ToRank@@", bal.GetApplication(ApplicationID)[0].ForRank);
            MessageBody = MessageBody.Replace("@@VRGSSR@@", VRGSSR);

            string subject = bal.GetTaskExtMessage((int)TaskExtID.Promotion_Decision_Notification_Positive_Eng)[0].Subject;
            email.AddComposeEmailRow(bal.GetApplicant(ApplicationID)[0].Email, subject, MessageBody);
       //     ComposeEmail1.LoadData(email);
            FinalDecision = "Promotable";
        }
        else
        {
            MessageBody = bal.GetTaskExtMessage((int)TaskExtID.Promotion_Decision_Notification_Negative_Eng)[0].Message;
            MessageBody = MessageBody.Replace("@@Applicant@@", bal.GetApplicant(ApplicationID)[0].Name);
            MessageBody = MessageBody.Replace("@@ToRank@@", bal.GetApplication(ApplicationID)[0].ForRank);
            MessageBody = MessageBody.Replace("@@VRGSSR@@", VRGSSR);

            string subject = bal.GetTaskExtMessage((int)TaskExtID.Promotion_Decision_Notification_Negative_Eng)[0].Subject;
            email.AddComposeEmailRow(bal.GetApplicant(ApplicationID)[0].Email, subject, MessageBody);
        //    ComposeEmail1.LoadData(email);
            FinalDecision = "Un Promotable";
        }
    }
    #region ComposeEmail
    protected void ComposeEmail_OnEmailSent(object sender, EventArgs e)
    {
        if (FinalDecision == "Promotable")
        {
            bal.InsertAppLgExt(ApplicationID, "Promotion Decision", (int)TaskExtID.Promotion_Decision_Notification_Positive_Eng, VRGSSR, MessageBody, DateTime.Now);
        }
        else
        {
            bal.InsertAppLgExt(ApplicationID, "Promotion Decision", (int)TaskExtID.Promotion_Decision_Notification_Negative_Eng, VRGSSR, MessageBody, DateTime.Now);
        }

        labelProgrammaticPopup0.Text = "Email has been successfully sent.";
        this.programmaticModalPopup0.Show();
      //  ComposeEmail1.Visible = false;

    }
    protected void ComposeEmail_OnEmailNotSent(object sender, EventArgs e)
    {
       // ComposeEmail1.Visible = false;
    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        this.programmaticModalPopup0.Hide();
    }
    #endregion


}
