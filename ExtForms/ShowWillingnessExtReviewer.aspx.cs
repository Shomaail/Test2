using System;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using BL.Data;


public partial class Forms_ShowWillingessExtReviewer : System.Web.UI.Page
{
    ExtRevFormBAL erfBAL = new ExtRevFormBAL();
    ExtRevBAL erBAL = new ExtRevBAL();
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }

        try
        {

            //    lblOrganizationName.Text = ConfigurationManager.AppSettings["OrganizationName"];
            int appID = int.Parse(Request.Params.Get("appID"));
            Application app = bal.GetApplication(appID)[0];
            int extRevID = int.Parse(Request.Params.Get("erID"));
            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID).Where(er => er.ExternalReviewerID == extRevID).ToList()[0];
            lblWillingnessLetterContent.Text = bal.GetExtTaskMessage((int)TaskExtID.Willingness_Letter_Content_External_Reviewer)[0].Message
                .Replace("@@ExternalReviewer@@", ffer.ExternalReviewer.NameString)
                .Replace("@@ApplicantCollege@@", app.Employee.Department1.ParentDeptString)
                .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
                .Replace("@@OrganizationAddress@@", ConfigurationManager.AppSettings["OrganizationAddress"])
                .Replace("@@Applicant@@", app.Employee.NameString)
                .Replace("@@Specialty@@", app.Employee.Specialty)
                .Replace("@@ApplicantRank@@", app.Employee.Rank)
                .Replace("@@ApplicantDept@@", app.Employee.Department)
                .Replace("@@ApplicantAreaOfSp@@", app.Form_AppProperties.AreaOfSpecialization)
                .Replace("@@ForRank@@", app.ForRank)
                .Replace("\r\n", "<br>")
                .Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
            ;
            //lblApplicantName.Text = bal.GetApplicant(ffer.ApplicationID)[0].Name;
            tbName.Value = ffer.ExternalReviewer.NameString;
            tbMailingAddress.Value = ffer.ExternalReviewer.MailingAddress;
            //  tbPassportNo.Value = ffer.PassportNo;
            // tbIBAN.Value = ffer.IBAN;
            if (ffer.WLStatus != "Accepted"
                && ffer.WLStatus != "Declined")
            {
                RadioButtonAccept.Enabled = true;
                RadioButtonReject.Enabled = true;
                btnSubmit.Enabled = chbxCinfrm.Checked;
            }
            else
            {
                if (ffer.WLStatus == "Accepted")
                {
                    RadioButtonAccept.Checked = true;
                    RadioButtonReject.Checked = false;
                    chbxCinfrm.Checked = true;
                }
                else
                {
                    RadioButtonAccept.Checked = false;
                    RadioButtonReject.Checked = true;
                }
                chbxCinfrm.Enabled = false;
                RadioButtonAccept.Enabled = false;
                RadioButtonReject.Enabled = false;
                btnSubmit.Enabled = false;
                lblThanks.Text = "You have already submitted your feedback.";
            }
        }
        catch (Exception ex)
        {
            LabelWebageUnavailable.Text = "The webpage requested is unavailable. Please check the URL";

        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int appID = int.Parse(Request.Params.Get("appID"));
        int extRevID = int.Parse(Request.Params.Get("erID"));
        Application app = bal.GetApplication(appID)[0];
        string Username, Password;
        Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID, extRevID)[0];
        if (RadioButtonAccept.Checked == true)
        {
            ExternalReviewer er = erBAL.GetExtRevByID(ffer.ExternalReviewerID)[0];

            Username = er.Email;
            if (er.Password == "" || er.Password == null)
            {
                er.Password = Cryptography.Encrypt(GetPassword());
               // er.Password = GetPassword();
                Password = er.Password;
               //Password = er.Email;
            }
            else
            {
                Password = er.Password;
            }
            //byte[] salt = PromotionApplication.GenerateSalt();
            //byte[] bPwd = PromotionApplication.ComputeHash(Password.Trim(), salt);
            //Password = Convert.ToBase64String(bPwd);
            erBAL.UpdateExtRev(ffer.ExternalReviewerID, tbName.Value, er.Rank, tbMailingAddress.Value, er.Email, er.Major
                    , er.Specialty, er.PhoneAndFax, er.ActiveAreaOfResearch
                    , er.PrevAreaOfResearch, er.Webpage, er.Comments, er.TotalPublications, er.NoOfJournals, er.HIndex, er.Citations
                    , er.Status, er.Password, er.IBAN, er.PassportNo, tbName.Value, er.Description
                    , er.Name_1, er.Name_2, er.Name_3, er.Name_4, er.Salt);

                string messageBody = bal.GetExtTaskMessage((int)TaskExtID.Acceptance_Notification)[0].Message;
            messageBody = messageBody.Replace("@@ReviewersName@@", tbName.Value);
            messageBody = messageBody.Replace("@@ReviewersNameAr@@", er.NameAr);
            messageBody = messageBody.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"]);
            messageBody = messageBody.Replace("@@OrganizationNameAr@@", ConfigurationManager.AppSettings["OrganizationName"]);
            messageBody = messageBody.Replace("@@Applicant@@", app.Employee.NameString);
                messageBody = messageBody.Replace("@@Specialty@@", app.Employee.Specialty);
                messageBody = messageBody.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);
            messageBody = messageBody.Replace("@@TopAuthority@@", bal.GetEmployeeByPK((bal.GetApplicationRole(ffer.ApplicationID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameString);            
            Emailer.Send(er.Email, bal.GetExtTaskMessage((int)TaskExtID.Acceptance_Notification)[0].Subject, messageBody, "AutoEmailer", ffer.ApplicationID);
            ffer.WLStatus = WillingessStatus.Accepted.ToString();
            ffer.WLDate = DateTime.Now;
            ffer.CommentsWithWL = tbComments.Value;
            ffer.UserName = Username;
            ffer.Password = Password;
            erBAL.UpdateFormFinalExtRev(ffer.ApplicationID, ffer.ExternalReviewerID, ffer.Serial, ffer.WLStatus
                , ffer.WLDate, ffer.CommentsWithWL, ffer.MaterialSentStatus, ffer.MaterialSentDate,
              ffer.EvaluationStatus, ffer.EvaluationDate, ffer.CommentsWithEval, ffer.ShowExtRev2PC, ffer.ShowEval2PC
              , ffer.UserName, ffer.Password, ffer.Source, ffer.PromotionRecom, ffer.Reasons, ffer.Score
              );
            RadioButtonAccept.Enabled = false;
            RadioButtonReject.Enabled = false;
            btnSubmit.Enabled = false;
            lblThanks.Text = @"
            <center>Thanks for accepting the request. 
An email has been sent to your email address. 
Thanks for cooperation.</center>";
            bal.InsertAppTskLgExt(ffer.ApplicationID, (int)TaskExtID.External_Evaluation, null, null, true, 0, er.Email, extRevID, messageBody, "0", 0);
            labelProgrammaticPopup0.Text = "Thanks for accepting the request. An email has been sent to your email address.Thanks for cooperation.";
            programmaticModalPopup0.Show();
        }
        else
        {
            RadioButtonAccept.Enabled = false;
            RadioButtonReject.Enabled = false;
            btnSubmit.Enabled = false;

            ffer.WLStatus = WillingessStatus.Declined.ToString();
            ffer.WLDate = DateTime.Now;
            ffer.CommentsWithWL = tbComments.Value;

            erBAL.UpdateFormFinalExtRev(ffer.ApplicationID, ffer.ExternalReviewerID, ffer.Serial, ffer.WLStatus
                , ffer.WLDate, ffer.CommentsWithWL, ffer.MaterialSentStatus, ffer.MaterialSentDate,
              ffer.EvaluationStatus, ffer.EvaluationDate, ffer.CommentsWithEval, ffer.ShowExtRev2PC, ffer.ShowEval2PC
              , ffer.UserName, ffer.Password, ffer.Source, ffer.PromotionRecom, ffer.Reasons, ffer.Score);

            lblThanks.Text = "Your feedback has been submitted. <br>Thanks for cooperation.";
        }
        Application_TaskLogExt aptle = bal.GetAppTaskLogExt(ffer.ApplicationID, (int)TaskExtID.Willingness_Letter_for_External_Reviewers, extRevID)[0];
        bal.UpdateAppTskLgExt(ffer.ApplicationID, (int)TaskExtID.Willingness_Letter_for_External_Reviewers, -1, DateTime.Now, true, aptle.Reminders, aptle.EmailAddress, extRevID, aptle.Message, aptle.EmployeeID, aptle.ExternalEmployeeID);
    }
    private string RandomString(int size, bool lowerCase)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
    }

    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public string GetPassword()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append(RandomString(1, true));
        builder.Append(RandomNumber(100, 999));
        builder.Append(RandomString(1, false));
        return builder.ToString();
    }
    //public string GetUsername()
    //{
    //    System.Text.StringBuilder builder = new System.Text.StringBuilder();
    //    builder.Append(RandomString(3, true));
    //    return builder.ToString();
    //}
    protected void RadioButtonAccept_CheckedChanged(object sender, EventArgs e)
    {
        divUserInfo.Visible = true;
        btnSubmit.Enabled = false;
    }
    protected void RadioButtonReject_CheckedChanged(object sender, EventArgs e)
    {
        divUserInfo.Visible = false;
        btnSubmit.Enabled = true;
    }
    protected void chbxCinfrm_CheckedChanged(object sender, EventArgs e)
    {
        if (tbName.Value == "")
        {
            tbName.Style["background"] = System.Drawing.Color.Red.ToString();
            tbName.Attributes["placeholder"] = "Enter a Name";
            btnSubmit.Enabled = false;
            return;
        }
        else
        {
            tbName.Style["background"] = System.Drawing.Color.White.ToString();
        }

        btnSubmit.Enabled = chbxCinfrm.Checked;
    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }
}
