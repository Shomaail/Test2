using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using BL.Data;
public partial class ExtForms_ExtAction : System.Web.UI.Page
{
    BAL bal = new BAL();
    ExtRevFormBAL erfBAL = new ExtRevFormBAL();
    ExtRevBAL erBAL = new ExtRevBAL();

    protected void Page_Load(object sender, EventArgs e)
    {


        if (IsPostBack) { return; }
        //lblTaskMessage.Text = "";
        Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID, Master.ExtReviewerID)[0];

        //lstActions.Items.Add(new ListItem("Submit Evaluation", TaskExtID.Submit_External_Evaluation.ToString()));
        //lstActions.Items.Add(new ListItem("Withdraw", TaskExtID.Withdraw_External_Evaluation.ToString()));

        if (Master.CurrentFormLevel == -1)
        {
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID);
            return;
        }

        Instruction1.Text = Master.CurrentFormInstruction;          
        BindCheckList();
        if (ffer.EvaluationStatus == EvaluationStatus.Submitted.ToString())
        {
            lblTaskMessage.Text = "You have already submitted the evaluation form.";
            lstActions.Enabled = false;
            btnSubmit.Enabled = false;
        }

        //hide buttons when printing
        if (Utils.IsPrintMode())
        {
            btnSubmit.Visible = false;
        }
    }
    private void BindCheckList()
    {
        ArrayList list = new ArrayList();
        IsTaskComplete = true;

        //foreach (TaskForm form in Master.TaskForms)
        //{
        //    if (form.Checkable)
        //    {
        //        FormStatusStruct status = Master.CheckFormTask(form);
        //        list.Add(status);
        //        if (status.Status == false)
        //        {
        //            IsTaskComplete = false;
        //        }
        //    }

        //}

        foreach (Application_TaskForm atfRow in Master.FormsStatus)
        {
            TaskForm row = bal.GetTaskForm(atfRow.TaskID, atfRow.FormID, atfRow.TaskExternal)[0];
            if (row.Checkable)
            {
                FormStatusStruct status = Master.CheckFormTask(row);
                list.Add(status);
                if (status.Status == false)
                {
                    IsTaskComplete = false;
                }
            }

        }
        lstFormsStatus.DataSource = list;
        divChecklist.Visible = list.Count > 0;
        lstFormsStatus.DataBind();
    }
    #region Event Handlers
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }
    protected void lstActions_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        btnSubmit.Visible = true;
        btnSubmit.Enabled = IsTaskComplete;
        if (lstActions.SelectedValue == TaskExtID.Withdraw_External_Evaluation.ToString())
        {
            grdMessages.Visible = true;
            string msg = bal.GetExtTaskMessage("Withdraw")[0].Message;
            msg = msg.Replace("@@TopAuthority@@", bal.GetEmployeeByAppRole(Master.ApplicationID, (byte)RoleID.TopAuthority)[0].NameString);
            msg = msg.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);
            msg = msg.Replace("@@Applicant@@", bal.GetApplicant(Master.ApplicationID)[0].NameString);
            msg = msg.Replace("@@SendersName@@", erBAL.GetExtRevByID(Master.ExtReviewerID)[0].NameString);
            vw_NextTaskDTO m = new vw_NextTaskDTO()
            {
                NextRole = ConfigurationManager.AppSettings["TopAuthority_Title"]
                ,
                TaskID = (int)TaskExtID.External_Evaluation,
                NextTask = "External Evaluation Completed"
                ,
                Message = msg
            };
            List<vw_NextTaskDTO> lnt = new List<vw_NextTaskDTO>();
            lnt.Add(m);
            grdMessages.DataSource = lnt;
            grdMessages.DataBind();
            btnSubmit.Enabled = true;
            //lblTaskMessage.Text = "Withdraw";
        }
        else
        {
            //lblTaskMessage.Text = "Submit";
            grdMessages.Visible = false;
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID, Master.ExtReviewerID)[0];
        string emailAddress = "";
        string body = "";
        if (lstActions.SelectedValue == TaskExtID.Submit_External_Evaluation.ToString())
        {
            foreach(Task_ExtMessages row in bal.GetExtTaskMessage((int) TaskExtID.Submit_External_Evaluation))
            {
                row.Message = row.Message.Replace("@@RecipientName@@", ffer.ExternalReviewer.Email);
                row.Message = row.Message.Replace("@@TopAuthority@@", bal.GetEmployeeByAppRole(Master.ApplicationID, (byte)RoleID.TopAuthority)[0].NameString);
                row.Message = row.Message.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);
                row.Message = row.Message.Replace("@@@TopAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopAuthority_TitleShort"]);
                row.Message = row.Message.Replace("@@Applicant@@", bal.GetApplicant(Master.ApplicationID)[0].NameString);
                row.Message = row.Message.Replace("@@SendersName@@", ffer.ExternalReviewer.NameString);
                if (row.Subject == "External Evaluation Completed")
                {                    
                    emailAddress = bal.GetEmployeeByAppRole(Master.ApplicationID, (byte)RoleID.TopAuthority)[0].Email;
                    bal.InsertApplication_Log(Master.ApplicationID, null, DateTime.Now
                       , row.Message, ""
                       , "External Evaluation End (" + Master.ExtReviewerID + ")"
                       , erBAL.GetExtRevByID(Master.ExtReviewerID)[0].NameString);
                }
                else
                {
                   
                    body = row.Message;
                    emailAddress = ffer.ExternalReviewer.Email;
                }
                Emailer.Send(emailAddress
                   , row.Subject
                   , row.Message, "AutoEmailer", Master.ApplicationID);
            }
            ffer.EvaluationStatus = EvaluationStatus.Submitted.ToString();
            ffer.EvaluationDate = DateTime.Now;
            ffer.CommentsWithEval = "";
            bal.InsertAppLgExt(ffer.ApplicationID, "External Evaluation End (" + ffer.ExternalReviewerID + ")", (int)TaskExtID.External_Evaluation
           , ffer.ExternalReviewer.Name, body, DateTime.Now);
         //   Session["Message"] = "Your Evaluation is successfully submitted.";
            labelProgrammaticPopup0.Text = "Your Evaluation is successfully submitted.";
            programmaticModalPopup0.Show();

        }
        else
        {

            foreach (Task_ExtMessages row in bal.GetExtTaskMessage((int)TaskExtID.Withdraw_External_Evaluation))
            {

                row.Message = row.Message.Replace("@@RecipientName@@", ffer.ExternalReviewer.NameString);
                row.Message = row.Message.Replace("@@TopAuthority@@", bal.GetEmployeeByAppRole(Master.ApplicationID, (byte)RoleID.TopAuthority)[0].NameString);
                row.Message = row.Message.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);
                row.Message = row.Message.Replace("@@Applicant@@", bal.GetApplicant(Master.ApplicationID)[0].NameString);
                row.Message = row.Message.Replace("@@SendersName@@", ffer.ExternalReviewer.NameString);
                if (row.Subject == "Withdraw")
                {
                    row.Message = (grdMessages.Rows[0].FindControl("txtMessage") as TextBox).Text;
                    emailAddress = bal.GetEmployeeByAppRole(Master.ApplicationID, (byte)RoleID.TopAuthority)[0].Email;
                }
                else
                {
                    emailAddress = ffer.ExternalReviewer.Email;
                }
                Emailer.Send(emailAddress
                   , row.Subject
                  , row.Message, "AutoEmailer", Master.ApplicationID);
            }

            ffer.EvaluationStatus = EvaluationStatus.Withdrawn.ToString();
            ffer.EvaluationDate = DateTime.Now;
            ffer.CommentsWithEval = (grdMessages.Rows[0].FindControl("txtMessage") as TextBox).Text;
            bal.InsertAppLgExt(ffer.ApplicationID, "External Evaluation Withdrawn (" + ffer.ExternalReviewerID + ")", (int)TaskExtID.External_Evaluation
           , ffer.ExternalReviewer.Name, body, DateTime.Now);
            // Session["Message"] = "You are successfully withdrawn";
            labelProgrammaticPopup0.Text = "You are successfully withdrawn.";
            programmaticModalPopup0.Show();
        }
        try
        {
            Application_TaskLogExt aptle = bal.GetAppTaskLogExt(ffer.ApplicationID, (int)TaskExtID.External_Evaluation, ffer.ExternalReviewerID)[0];
            bal.UpdateAppTskLgExt(ffer.ApplicationID, aptle.TaskID, null, DateTime.Now, true, aptle.Reminders, aptle.EmailAddress
                , aptle.ExternalReviewerID, aptle.Message, aptle.EmployeeID, aptle.ExternalEmployeeID);
            erBAL.UpdateFormFinalExtRev(ffer.ApplicationID, ffer.ExternalReviewerID, ffer.Serial, ffer.WLStatus, ffer.WLDate, ffer.CommentsWithWL
                , ffer.MaterialSentStatus, ffer.MaterialSentDate, ffer.EvaluationStatus, ffer.EvaluationDate, ffer.CommentsWithEval
                , ffer.ShowExtRev2PC, ffer.ShowEval2PC, ffer.UserName, ffer.Password
                , ffer.Source, ffer.EvaluationID);
        
        }
        catch (Exception)
        {
            labelProgrammaticPopup0.Text = "Something has gone wrong. contact the Administrator ";
            programmaticModalPopup0.Show();
            return;
        }
       // Response.Redirect("Popup.aspx");
    }
    #endregion
    #region Propoerties
    private bool isTaskComplete;
    public bool IsTaskComplete
    {
        get { return (bool)ViewState["isTaskComplete"]; }
        set { ViewState["isTaskComplete"] = value; }
    }
    #endregion

}