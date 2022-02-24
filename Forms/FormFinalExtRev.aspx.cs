using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using BL.Data;
public partial class Forms_FormFinalExtRev : System.Web.UI.Page
{
    ExtRevFormBAL erfBAL = new ExtRevFormBAL();
    ExtRevBAL erBAL = new ExtRevBAL();

    BAL bal = new BAL();         
    private enum Mode { InputTopAuthority = 1, ViewByPC = 2, ViewBySC = 3}


    /* Form_Evaluation
 *
 * Status = 4-- Submitted
 * Status = 3-- Updated
 * Status = 2-- Saved Succesfully
 * Status = 1 -- Saved Unsuccesfully
 * Status = 0 -- Not saved(New)
 * 
 */
    
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (IsPostBack)
        {
            if (!(Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("ddlQuickJump")))
            {
                return;
            }
        }

        FormMode = "View";
        try
        {
            DatabindControls();
        }
        catch (Exception exc)
        {

            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            return;
        }

    }
    public bool GetMaterialOKStatus()
    {
        if(bal.GetFormMaterialFlag(Master.ApplicationID).Count == 0 )
        {
            bal.InsertFormMaterialFlag(Master.ApplicationID, null, "", "","");
        }
        if(bal.GetFormMaterialFlag(Master.ApplicationID)[0].PCMaterialReady4ExtRevFlag.HasValue)
        {
            lblPCMessage.Text = "";
            return bal.GetFormMaterialFlag(Master.ApplicationID)[0].PCMaterialReady4ExtRevFlag.Value;
        }
        else
        {
            lblPCMessage.Text = "The Promotion Committee has not yet viewed the material";
            return false;
        }
    }
    #region Properties
    public string MessageBody
    {
        set
        {
            ViewState["MessageBody"] = value;
        }
        get
        {
            if (ViewState["MessageBody"] != null)
            {
                return (string)ViewState["MessageBody"];
            }
            else
            {
                return "";
            }
                
        }
    }
    public string Subject
    {
        set
        {
            ViewState["Subject"] = value;
        }
        get
        {
            if (ViewState["Subject"] != null)
                return (string)ViewState["Subject"];
            else
                return "";
        }
    }
    public string FormMode
    {
        set
        {
            ViewState["FormMode"] = value;
        }
        get
        {
            if (ViewState["FormMode"] != null)
                return (string)ViewState["FormMode"];
            else
                return "";
        }
    }
    public string UserMode
    {
        set
        {
            ViewState["UserMode"] = value;
        }
        get
        {
            if (ViewState["UserMode"] != null)
                return (string)ViewState["UserMode"];
            else
                return "";
        }
    }
    public int ExternalReviewerID
    {
        get
        {
            if (hfExtRevID.Value.Length == 0)
                return -1;
            return int.Parse(hfExtRevID.Value);
        }
        set
        {
            hfExtRevID.Value = value.ToString();
        }
    }
    #endregion
    private void SwitchMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.InputTopAuthority:
                 UserMode = "InputTopAuthority";
                gvFinalExtRev.DataSource = erBAL.GetForm_FinalExtRev(Master.ApplicationID);
                //gvFinalExtRev.DataSource = erBAL.GetForm_FinalExtRev(Master.ApplicationID)
                //    .OrderByDescending(a => a.ExternalReviewer.HIndex.HasValue && a.ExternalReviewer.NoOfJournals.HasValue ?
                //    a.ExternalReviewer.HIndex.Value + a.ExternalReviewer.NoOfJournals.Value : a.Serial);
                break;
            case Mode.ViewByPC:
                 UserMode = "ViewByPC";

                gvFinalExtRev.DataSource = erBAL.GetForm_FinalExtRev(Master.ApplicationID)
                    .Where(a => a.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_", " "))
                    ;
                //gvFinalExtRev.DataSource = erBAL.GetForm_FinalExtRev(Master.ApplicationID)
                //    .Where(a => a.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_", " "))
                //    .OrderByDescending(a => a.ExternalReviewer.HIndex.HasValue && a.ExternalReviewer.NoOfJournals.HasValue ?
                //    a.ExternalReviewer.HIndex.Value + a.ExternalReviewer.NoOfJournals.Value : a.Serial);
                //if(erBAL.GetForm_FinalExtRev(Master.ApplicationID).Where(er => er.WLStatus == WillingessStatus.Accepted.ToString()).Count() == 0)
                //{
                //    lblPCMessage.Text = "The external reviewers will be visible here once the evaluation is started.";
                //}

                 gvFinalExtRev.Columns[4].Visible = false;
                 gvFinalExtRev.Columns[5].Visible = true;
                 gvFinalExtRev.Columns[6].Visible = false;
                 gvFinalExtRev.Columns[7].Visible = false;
                 gvFinalExtRev.Columns[8].Visible = false;
                 gvFinalExtRev.Columns[11].Visible = false;
                
                break;
            case Mode.ViewBySC:
                UserMode = "ViewBySC";

                gvFinalExtRev.DataSource = erBAL.GetForm_FinalExtRev(Master.ApplicationID)
                    ;
                //gvFinalExtRev.DataSource = erBAL.GetForm_FinalExtRev(Master.ApplicationID)
                //   .Where(a => a.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_", " "))
                //   .OrderByDescending(a => a.ExternalReviewer.HIndex.HasValue && a.ExternalReviewer.NoOfJournals.HasValue ?
                //   a.ExternalReviewer.HIndex.Value + a.ExternalReviewer.NoOfJournals.Value : a.Serial);

                //if(erBAL.GetForm_FinalExtRev(Master.ApplicationID).Where(er => er.WLStatus == WillingessStatus.Accepted.ToString()).Count() == 0)
                //{
                //    lblPCMessage.Text = "The external reviewers will be visible here once the evaluation is started.";
                //}

                gvFinalExtRev.Columns[4].Visible = false;
                gvFinalExtRev.Columns[5].Visible = true;
                gvFinalExtRev.Columns[6].Visible = false;
                gvFinalExtRev.Columns[7].Visible = false;
                gvFinalExtRev.Columns[8].Visible = false;
                gvFinalExtRev.Columns[11].Visible = false;

                break;
        }
       // gvFinalExtRev.DataBind();
    }
    private void DatabindControls()
    {
        SwitchMode((Mode)Master.CurrentFormLevel);
        if(!Master.App.ApplicationClosed)
        {
            if (erBAL.GetForm_FinalExtRev(Master.ApplicationID).Where(er => er.EvaluationStatus == EvaluationStatus.Submitted.ToString()).Count() > 2)
            {
                Master.ReportSuccess("");
            }
            else
            {
                Master.ReportFailure("Three Evaluation Reports from External Reviewers not received yet");
            }
        }
        if (bal.GetFormMaterialFlag(Master.ApplicationID).Count == 0)
        {
            bal.InsertFormMaterialFlag(Master.ApplicationID, null, "", "", "");
        }
        divMaterialOKStatus.DataBind();
        lbtnViewEmailFromTopAuthority.ToolTip = "View email sent from " + ConfigurationManager.AppSettings["TopAuthority_TitleShort"];
        lbtnViewEmailFromPCMaterialOK.DataBind();
        lbtnViewEmailFromPCMaterialNotOK.DataBind();
        divFormFinalExtRev.DataBind();
        divViewDetail.DataBind();
        divSendWL.DataBind();
    }
    #region Event Handlers
    #region Gridview
    #region GridviewFormFinalExtRev
    protected void cbShowExtRev2PC_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvFinalExtRev.Rows)
        {
            CheckBox cbShowExtRev2PC = row.FindControl("cbShowExtRev2PC") as CheckBox;
            erBAL.UpdateFormFinalExtRevShowExtRev2PC(cbShowExtRev2PC.Checked, int.Parse(gvFinalExtRev.DataKeys[row.RowIndex].Values[0].ToString()), int.Parse(gvFinalExtRev.DataKeys[row.RowIndex].Values[1].ToString()));
            //frmAttAdapter.UpdateSelForExtRev(cbShowExtRev2PC.Checked, Int32.Parse(gvFinalExtRev.DataKeys[row.RowIndex].Values[0].ToString()), Int32.Parse(gvFinalExtRev.DataKeys[row.RowIndex].Values[1].ToString()));

        }
    }
    public bool GetcbShowExtRev2PCCheckStatus(int appID, int extRevID)
    {
        if (erBAL.GetForm_FinalExtRev(appID, extRevID)[0].ShowExtRev2PC.HasValue)
        {
            return erBAL.GetForm_FinalExtRev(appID, extRevID)[0].ShowExtRev2PC.Value;
        }
        else
        {
            return false;
        }
    }
    protected void cbShowEval2PC_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvFinalExtRev.Rows)
        {
            CheckBox cbShowEval2PC = row.FindControl("cbShowEval2PC") as CheckBox;
            erBAL.UpdateFormFinalExtRevShowEval2PC(cbShowEval2PC.Checked, int.Parse(gvFinalExtRev.DataKeys[row.RowIndex].Values[0].ToString()), int.Parse(gvFinalExtRev.DataKeys[row.RowIndex].Values[1].ToString()));
            //frmAttAdapter.UpdateSelForExtRev(cbShowExtRev2PC.Checked, Int32.Parse(gvFinalExtRev.DataKeys[row.RowIndex].Values[0].ToString()), Int32.Parse(gvFinalExtRev.DataKeys[row.RowIndex].Values[1].ToString()));

        }
    }
    public bool GetcbShowEval2PCCheckStatus(int appID, int extRevID)
    {
        if (erBAL.GetForm_FinalExtRev(appID, extRevID)[0].ShowEval2PC.HasValue)
        {
            return erBAL.GetForm_FinalExtRev(appID, extRevID)[0].ShowEval2PC.Value;
        }
        else
        {
            return false;
        }
    }
     protected void gvFinalExtRev_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(Session["user"] == null)
        {
            Response.Redirect("Main.aspx");
            return;
        }
        int appID = Master.ApplicationID;
        ExternalReviewerID = e.CommandArgument.ToString() == "" || e.CommandArgument == null ? 0 : Convert.ToInt32(e.CommandArgument.ToString());

        if (e.CommandName == "SelectDetail")
        {
            odsFormExtRevDet.SelectParameters["ExternalReviewerID"].DefaultValue = ExternalReviewerID.ToString();
            dvExtReviewers.DataBind();
            FormMode = "ExtRevDetail";
            DatabindControls();

        }
        else if (e.CommandName == "SendWL")
        {
            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID).Where(er => er.ExternalReviewerID == ExternalReviewerID).ToList()[0];
            MessageBody = bal.GetExtTaskMessage((int)TaskExtID.Willingness_Letter_for_External_Reviewers)[0].Message;
            Subject = bal.GetExtTaskMessage((int)TaskExtID.Willingness_Letter_for_External_Reviewers)[0].Subject;
            if (ffer.EvaluationStatus == "")
            {
                erBAL.UpdateFormFinalExtRev(ffer.ApplicationID, ffer.ExternalReviewerID, ffer.Serial, ffer.WLStatus, ffer.WLDate, ffer.CommentsWithWL, ffer.MaterialSentStatus, ffer.MaterialSentDate,
                            ffer.EvaluationStatus, ffer.EvaluationDate, ffer.CommentsWithEval, ffer.ShowExtRev2PC, ffer.ShowEval2PC, ffer.UserName, ffer.Password
                            , ffer.Source, ffer.EvaluationID);
            }
            ffer = erBAL.GetForm_FinalExtRev(appID).Where(er => er.ExternalReviewerID == ExternalReviewerID).ToList()[0];
            Subject = Subject.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"]);
            MessageBody = MessageBody.Replace("@@RecipientName@@", ffer.ExternalReviewer.NameString);
            MessageBody = MessageBody.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"]);
            MessageBody = MessageBody.Replace("@@OrganizationAddress@@", ConfigurationManager.AppSettings["OrganizationAddress"]);
            MessageBody = MessageBody.Replace("@@WebsiteURL@@", ConfigurationManager.AppSettings["WebsiteURL"]);
            MessageBody = MessageBody.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);

            MessageBody = MessageBody.Replace("@@appID@@", Master.ApplicationID.ToString());
            MessageBody = MessageBody.Replace("@@erID@@", ExternalReviewerID.ToString());
            MessageBody = MessageBody.Replace("@@ApplicantCollege@@", Master.Applicant.ParentDept);
            MessageBody = MessageBody.Replace("@@Applicant@@", Master.Applicant.Name);
            MessageBody = MessageBody.Replace("@@ApplicantDept@@", Master.Applicant.Department);
            MessageBody = MessageBody.Replace("@@ForRank@@", ffer.Application.ForRank);
            MessageBody = MessageBody.Replace("@@ApplicantRank@@", Master.Applicant.Rank);
            if (bal.GetForm_AppProperties(appID).Count > 0 )
            {
                MessageBody = MessageBody.Replace("@@ApplicantAreaOfSp@@", bal.GetForm_AppProperties(appID)[0].AreaOfSpecialization);
            }
            
            MessageBody = MessageBody.Replace("@@TopAuthority@@", bal.GetEmployeeByPK((bal.GetApplicationRole(Master.ApplicationID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameString);


            EmailContent email = new EmailContent { To = ffer.ExternalReviewer.Email, Body = MessageBody, Subject = "Request for willingness" };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadData(le);
            ComposeEmail1.Visible = true;
            FormMode = "SendWL";
            DatabindControls();
        }
        else if (e.CommandName == "ViewWLEmail")
        {
            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID).Where(er => er.ExternalReviewerID == ExternalReviewerID).ToList()[0];
            Application_TaskLogExt aptle = bal.GetAppTaskLogExt(ffer.ApplicationID, (int)TaskExtID.Willingness_Letter_for_External_Reviewers, ffer.ExternalReviewerID)[0];
            EmailContent email = new EmailContent { To = aptle.EmailAddress, Body = aptle.Message
                , Subject = bal.GetExtTaskMessage((int)TaskExtID.Willingness_Letter_for_External_Reviewers)[0].Subject.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
            };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadDataForPreview(le);
            ComposeEmail1.Visible = true;
            FormMode = "SendWL";
            DatabindControls();
        }
        else if (e.CommandName == "DeleteWLStatus")
        {
            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID).Where(er => er.ExternalReviewerID == ExternalReviewerID).ToList()[0];
            if (ffer.MaterialSentStatus == SendSelPubStatus.Material_Sent.ToString())
            {
                labelProgrammaticPopup0.Text = "The willingness of the external reviewer cannot be deleted since publication has been sent to the External Reviewer.";
                programmaticModalPopup0.Show();
                return;
            }
            foreach (Application_TaskLogExt row in bal.GetAppTaskLogExt().Where(atle => atle.ApplicationID == appID
            && atle.ExternalReviewerID == ffer.ExternalReviewerID))
            {
                bal.DeleteApplication_TaskLogExt(row.TaskLogID);
            }
            ffer.WLStatus = WillingessStatus.Not_Sent.ToString().Replace("_", " ");
            ffer.WLDate = null;
            ffer.CommentsWithWL = "";
            erBAL.UpdateFormFinalExtRev(appID, ExternalReviewerID, ffer.Serial, ffer.WLStatus, ffer.WLDate, ffer.CommentsWithWL
                , ffer.MaterialSentStatus, ffer.MaterialSentDate, ffer.EvaluationStatus, ffer.EvaluationDate
                , ffer.CommentsWithEval, ffer.ShowExtRev2PC, ffer.ShowEval2PC
                , ffer.UserName, ffer.Password, ffer.Source, ffer.EvaluationID);
            DatabindControls();
        }
        else if (e.CommandName == "SendSelDoc")
        {

            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID,ExternalReviewerID)[0];
            if (ffer.WLStatus != WillingessStatus.Accepted.ToString())
            {
                labelProgrammaticPopup0.Text = @"The willingness of the external reviewer must be taken first before sending the documents 
            for evaluation. Click on the Send Email button in the Willingess Letter column of the Final External Reviewers List.";
                programmaticModalPopup0.Show();
                return;
            }
            MessageBody = bal.GetExtTaskMessage((int)TaskExtID.External_Evaluation)[1].Message;
            Subject = bal.GetExtTaskMessage((int)TaskExtID.External_Evaluation)[1].Subject;
            Subject = Subject.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"]);
            MessageBody = MessageBody.Replace("@@ReviewersNameAr@@", ffer.ExternalReviewer.NameAr);
            MessageBody = MessageBody.Replace("@@ReviewersName@@", ffer.ExternalReviewer.NameString);
            MessageBody = MessageBody.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"]);
            MessageBody = MessageBody.Replace("@@OrganizationNameAr@@", ConfigurationManager.AppSettings["OrganizationNameAr"]);
            MessageBody = MessageBody.Replace("@@OrganizationAddress@@", ConfigurationManager.AppSettings["OrganizationAddress"]);
            MessageBody = MessageBody.Replace("@@WebsiteURL@@", ConfigurationManager.AppSettings["WebsiteURL"]);
            MessageBody = MessageBody.Replace("@@ExtRevEmail@@", ffer.ExternalReviewer.Email);
            MessageBody = MessageBody.Replace("@@Password@@", Cryptography.Decrypt(ffer.ExternalReviewer.Password));
            MessageBody = MessageBody.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);


            MessageBody = MessageBody.Replace("@@appID@@", Master.ApplicationID.ToString());
            MessageBody = MessageBody.Replace("@@erID@@", ExternalReviewerID.ToString());
            MessageBody = MessageBody.Replace("@@ApplicantCollege@@", Master.Applicant.ParentDept);
            MessageBody = MessageBody.Replace("@@Applicant@@", Master.Applicant.NameString);
           // MessageBody = MessageBody.Replace("@@ApplicantAr@@", Master.Applicant.NameStringAr);
            MessageBody = MessageBody.Replace("@@ApplicantDept@@", Master.Applicant.Department);
            MessageBody = MessageBody.Replace("@@ApplicantRank@@", Master.Applicant.Rank);
            MessageBody = MessageBody.Replace("@@ApplicantAreaOfSp@@", bal.GetForm_AppProperties(appID)[0].AreaOfSpecialization);
            MessageBody = MessageBody.Replace("@@TopAuthority@@", bal.GetEmployeeByPK((bal.GetApplicationRole(Master.ApplicationID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameString);
          //  MessageBody = MessageBody.Replace("@@TopAuthorityAr@@", bal.GetEmployeeByPK((bal.GetApplicationRole(Master.ApplicationID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameStringAr);

            EmailContent email = new EmailContent { To = ffer.ExternalReviewer.Email, Body = MessageBody
                , Subject=Subject
            };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadData(le);
            ComposeEmail1.Visible = true;
            FormMode = "SendSelDoc";
            DatabindControls();
        }

        else if (e.CommandName == "DeleteMSStatus")
        {

            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID).Where(er => er.ExternalReviewerID == ExternalReviewerID).ToList()[0];
            if (ffer.EvaluationStatus == EvaluationStatus.Saved_But_Not_Submitted.ToString()
                || ffer.EvaluationStatus == EvaluationStatus.Submitted.ToString())
            {
                labelProgrammaticPopup0.Text = "The material Sent status of the external reviewer cannot be deleted since Evaluation has been started or submitted by the External Reviewer.";
                programmaticModalPopup0.Show();
                return;
            }
            foreach (Application_TaskLogExt row in bal.GetAppTaskLogExt().Where(atle => atle.ApplicationID == appID
            && atle.ExternalReviewerID == ffer.ExternalReviewerID
            && atle.TaskID == (int)TaskExtID.External_Evaluation))
            {
                Application_TaskLogExt atle = bal.GetAppTaskLogExt(row.TaskLogID)[0];
                bal.UpdateAppTskLgExt(atle.ApplicationID, atle.TaskID, atle.ActionID, atle.SentDate, true, atle.Reminders, atle.EmailAddress,
               atle.ExternalReviewerID, atle.Message, atle.EmployeeID, atle.ExternalEmployeeID);
            }
            ffer.MaterialSentStatus = SendSelPubStatus.Material_Not_Sent.ToString().Replace("_", " ");
            ffer.MaterialSentDate = null;
            erBAL.UpdateFormFinalExtRev(appID, ExternalReviewerID, ffer.Serial, ffer.WLStatus, ffer.WLDate, ffer.CommentsWithWL
                , ffer.MaterialSentStatus, ffer.MaterialSentDate, ffer.EvaluationStatus, ffer.EvaluationDate
                , ffer.CommentsWithEval, ffer.ShowExtRev2PC, ffer.ShowEval2PC
                , ffer.UserName, ffer.Password, ffer.Source, ffer.EvaluationID);
            DatabindControls();
        }
        else if (e.CommandName == "ViewMSEmail")
        {
            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID).Where(er => er.ExternalReviewerID == ExternalReviewerID).ToList()[0];
            Application_TaskLogExt aptle = bal.GetAppTaskLogExt(ffer.ApplicationID, (int)TaskExtID.External_Evaluation, ffer.ExternalReviewerID)[0];

            EmailContent email = new EmailContent { To = aptle.EmailAddress, Body = aptle.Message
                , Subject = bal.GetExtTaskMessage((int)TaskExtID.External_Evaluation)[1].Subject.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
            };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadDataForPreview(le);
            ComposeEmail1.Visible = true;
            FormMode = "SendSelDoc";
            DatabindControls();
        }
        else if (e.CommandName == "ShowEvaluation")
        {
            int extReviewerID = int.Parse(e.CommandArgument.ToString());
            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID, extReviewerID)[0];
            //Session["ExtRevID"] = extReviewerID;
            //Session["applicationID"] = Master.ApplicationID;


            if (ffer.EvaluationStatus == EvaluationStatus.Submitted.ToString())
            {/*Response.Write was making the whole page bold */
                //Response.Write(@"<script language=javascript>shomil</script>");
//                Response.Write(@"<script language=javascript>
//                                    window.open ('../ExtForms/NoMasterPage/NMPForm_Evaluation.aspx?appID="
//+ Master.ApplicationID + @"&extRevID=" + ExternalReviewerID + @"',null,'scrollbars=yes, status= no, resizable = yes, toolbar=no,location=no,height = 700, width = 900, left = 200, top= 200, screenx=10,screeny=600,menubar=no');                   
//                                    </script>");
            }
            DatabindControls();
        }
        else if (e.CommandName == "DeleteEvaluation")
        {
            //foreach (PublicationsEvaluation row in erfBAL.GetPublicationsEvaluation(Master.ApplicationID, ExternalReviewerID)
            //    .Where(pe => pe.Form_Attachment.SelectionForExtRev.Value))
            //{
            //    row.EvaluationMark = 1;
            //    row.Remarks = "";
            //    erfBAL.UpdatePublicationsEvaluations(row.ApplicationID, row.ExternalReviewerID, row.Form_AttachmentID
            //        , row.EvaluationMark, row.Remarks);
            //}


            erfBAL.DeleteEvaluation(Master.ApplicationID, ExternalReviewerID, "en-US");
            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID, ExternalReviewerID)[0];
            ffer.EvaluationStatus = EvaluationStatus.Not_Submitted.ToString().Replace("_", " ");
            ffer.EvaluationDate = null;
            ffer.CommentsWithEval = "";
            erBAL.UpdateFormFinalExtRev(ffer.ApplicationID, ffer.ExternalReviewerID, ffer.Serial, ffer.WLStatus, ffer.WLDate, ffer.CommentsWithWL
           , ffer.MaterialSentStatus, ffer.MaterialSentDate, ffer.EvaluationStatus, ffer.EvaluationDate, ffer.CommentsWithEval
           , ffer.ShowExtRev2PC, ffer.ShowEval2PC, ffer.UserName, ffer.Password
           , ffer.Source, ffer.EvaluationID);
//            bal.DeletetAppTskFrm(Master.ApplicationID, (int)TaskExtID.External_Evaluation, (int)FormID.PublicationsEvaluation_aspx, true, 0);
            DatabindControls();
        }
        else if (e.CommandName == "ReturnEvaluation")
        {
            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID, ExternalReviewerID)[0];
            MessageBody = bal.GetExtTaskMessage((int)TaskExtID.External_Evaluation)[0].Message;
            Subject = bal.GetExtTaskMessage((int)TaskExtID.External_Evaluation)[0].Subject;

            Subject = Subject.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"]);
            MessageBody = MessageBody.Replace("@@ReviewersNameAr@@", ffer.ExternalReviewer.NameAr);
            MessageBody = MessageBody.Replace("@@ReviewersName@@", ffer.ExternalReviewer.NameString);
            MessageBody = MessageBody.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"]);
            MessageBody = MessageBody.Replace("@@OrganizationNameAr@@", ConfigurationManager.AppSettings["OrganizationNameAr"]);
            MessageBody = MessageBody.Replace("@@OrganizationAddress@@", ConfigurationManager.AppSettings["OrganizationAddress"]);
            MessageBody = MessageBody.Replace("@@WebsiteURL@@", ConfigurationManager.AppSettings["WebsiteURL"]);
            MessageBody = MessageBody.Replace("@@ExtRevEmail@@", ffer.ExternalReviewer.Email);
            MessageBody = MessageBody.Replace("@@Password@@",Cryptography.Decrypt(ffer.ExternalReviewer.Password));
            MessageBody = MessageBody.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);


            MessageBody = MessageBody.Replace("@@appID@@", Master.ApplicationID.ToString());
            MessageBody = MessageBody.Replace("@@erID@@", ExternalReviewerID.ToString());
            MessageBody = MessageBody.Replace("@@ApplicantCollege@@", Master.Applicant.ParentDept);
            MessageBody = MessageBody.Replace("@@Applicant@@", Master.Applicant.NameString);
           // MessageBody = MessageBody.Replace("@@ApplicantAr@@", Master.Applicant.NameStringAr);
            MessageBody = MessageBody.Replace("@@ApplicantDept@@", Master.Applicant.Department);
            MessageBody = MessageBody.Replace("@@ApplicantRank@@", Master.Applicant.Rank);
            MessageBody = MessageBody.Replace("@@ApplicantAreaOfSp@@", bal.GetForm_AppProperties(appID)[0].AreaOfSpecialization);
            MessageBody = MessageBody.Replace("@@TopAuthority@@", bal.GetEmployeeByPK((bal.GetApplicationRole(Master.ApplicationID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameString);
            //MessageBody = MessageBody.Replace("@@TopAuthorityAr@@", bal.GetEmployeeByPK((bal.GetApplicationRole(Master.ApplicationID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameStringAr);

            EmailContent email = new EmailContent { To = ffer.ExternalReviewer.Email, Body = MessageBody
                , Subject = bal.GetExtTaskMessage((int)TaskExtID.External_Evaluation)[0].Subject.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
            };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadData(le);
            ComposeEmail1.Visible = true;
            FormMode = "SendSelDoc";
            DatabindControls();
        }


        /*
* Form_Evaluation
* 
* Status = 4 -- Submitted 
* Status = 3 -- Updated
* Status = 2 -- Saved Succesfully
* Status = 1 -- Saved Unsuccesfully
* Status = 0 -- Not saved (New)
* 
*/
        else if (e.CommandName == "ShowForm")
        {
            int extReviewerID = int.Parse(e.CommandArgument.ToString());
            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID, extReviewerID)[0];
            //Session["ExtRevID"] = extReviewerID;
            //Session["applicationID"] = Master.ApplicationID;


            if (ffer.EvaluationStatus == EvaluationStatus.Submitted.ToString())
            {
                Response.Write(@"<script language=javascript>
                    window.open ('../ExtForms/NoMasterPage/NMForm_Evaluation.aspx?appID="
+ Master.ApplicationID + @"& extRevID="+ExternalReviewerID + @"',null,'scrollbars=yes, status= no, resizable = yes, toolbar=no,location=no,height = 700, width = 900, left = 200, top= 200, screenx=10,screeny=600,menubar=no');                   
                    </script>");
            }
            DatabindControls();
        }
        else if (e.CommandName == "Refresh")
        {
            DatabindControls();
        }           
       
    }

    protected void gvFinalExtRev_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }    
    #endregion 
    #endregion
    #region Button Event Handlers


    protected void btnHideDetail_Click(object sender, EventArgs e)
    {
        FormMode = "View";
        DatabindControls();
    }
  
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }
    #endregion
    #region Link Button Event Handler 
    protected void lbtnViewEmailFromPCMaterialOK_Click(object sender, EventArgs e)
    {
        int appID = Master.ApplicationID;
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;

        EmailContent email = new EmailContent
        {
            To = TopAuthority.Email+ ","
                + bal.GetDepartmentBySN(ConfigurationManager.AppSettings["TopAuthority_TitleShort"])[0].DeputyEmail,
            Body = bal.GetFormMaterialFlag(appID)[0].MaterialOKEmail,
            Subject = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Material_Ready_to_be_Sent).ToList()[0].Subject
        };
        List<EmailContent> le = new List<EmailContent>();
        le.Add(email);
        ComposeEmail1.LoadDataForPreview(le);
        ComposeEmail1.Visible = true;
        FormMode = "ViewMaterialStatusEmail";
        DatabindControls();
    }

    protected void lbtnViewEmailFromPCMaterialNotOK_Click(object sender, EventArgs e)
    {
        int appID = Master.ApplicationID;
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;

        EmailContent email = new EmailContent
        {
            To = TopAuthority.Email + ","
                + bal.GetDepartmentBySN(ConfigurationManager.AppSettings["TopAuthority_TitleShort"])[0].DeputyEmail,
            Body = bal.GetFormMaterialFlag(appID)[0].MaterialNotOKEmail,
            Subject = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Material_Not_Ready_to_be_Sent).ToList()[0].Subject

        };
        List<EmailContent> le = new List<EmailContent>();
        le.Add(email);
        ComposeEmail1.LoadDataForPreview(le);
        ComposeEmail1.Visible = true;
        FormMode = "ViewMaterialStatusEmail";
        DatabindControls();
    }

    protected void lbtnViewEmailFromTopAuthority_Click(object sender, EventArgs e)
    {
        List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(Master.ApplicationID);
        string allPCEmail = "";
        foreach (Form_FinalPC pcMem in lffpc)
        {
            if (pcMem.ExternalEmployeeID == 0)
            {
                allPCEmail = allPCEmail + pcMem.Employee.Email+ ",";
            }
            else
            {
                allPCEmail = allPCEmail + pcMem.ExternalEmployee.Email + ",";
            }
        }
        allPCEmail = allPCEmail.Trim(',');
        int appID = Master.ApplicationID;
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;

        EmailContent email = new EmailContent
        {
            To = allPCEmail,
            Body = bal.GetFormMaterialFlag(appID)[0].EmailFromTopAuthority,
            Subject = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Material_Updated).ToList()[0].Subject

        };
        if(bal.GetFormMaterialFlag(appID)[0].EmailFromTopAuthority == "" 
            || bal.GetFormMaterialFlag(appID)[0].EmailFromTopAuthority == null)
        {
            labelProgrammaticPopup0.Text = "There is no email.";
            programmaticModalPopup0.Show();
            return;
        }
        List<EmailContent> le = new List<EmailContent>();
        le.Add(email);
        ComposeEmail1.LoadDataForPreview(le);
        ComposeEmail1.Visible = true;
        FormMode = "ViewMaterialStatusEmail";
        DatabindControls();
    }
    #endregion
    #region ComposeEmail
    protected void ComposeEmail_OnEmailSent(object sender, EventArgs e)
    {
        Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID).Where(er => er.ExternalReviewerID == ExternalReviewerID).ToList()[0];
        if (ffer.WLStatus == WillingessStatus.Not_Sent.ToString().Replace("_", " "))
        {
            ffer.WLStatus = WillingessStatus.Waiting.ToString();
            ffer.WLDate = DateTime.Now;                       
            erBAL.UpdateFormFinalExtRev(ffer.ApplicationID, ffer.ExternalReviewerID, ffer.Serial, ffer.WLStatus, ffer.WLDate, ffer.CommentsWithWL
           , ffer.MaterialSentStatus, ffer.MaterialSentDate, ffer.EvaluationStatus, ffer.EvaluationDate, ffer.CommentsWithEval
           , ffer.ShowExtRev2PC, ffer.ShowEval2PC, ffer.UserName, ffer.Password
           , ffer.Source, ffer.EvaluationID);
           // MessageBody = Regex.Replace(MessageBody, PromotionApplication.specialCharacters, string.Empty);
            
            bal.InsertAppTskLgExt(ffer.ApplicationID
                                    , (int)TaskExtID.Willingness_Letter_for_External_Reviewers
                                    , null, DateTime.Now, false, 0, ffer.ExternalReviewer.Email, ffer.ExternalReviewerID
                                    , ComposeEmail1.EmailBody, "0", 0);           
            labelProgrammaticPopup0.Text = "Email has been successfully sent. The external reveiewer can show his willingness by clicking on the URL given";

        }
        else if (ffer.WLStatus == WillingessStatus.Accepted + ""
            && ffer.MaterialSentStatus == SendSelPubStatus.Material_Not_Sent.ToString().Replace("_", " "))
        {
            ffer.MaterialSentStatus = SendSelPubStatus.Material_Sent.ToString().Replace("_", " ");
            ffer.MaterialSentDate = DateTime.Now;
            ffer.EvaluationStatus = EvaluationStatus.Not_Submitted.ToString().Replace("_", " ");
            erBAL.UpdateFormFinalExtRev(ffer.ApplicationID, ffer.ExternalReviewerID, ffer.Serial, ffer.WLStatus, ffer.WLDate
                  , ffer.CommentsWithWL, ffer.MaterialSentStatus, ffer.MaterialSentDate, ffer.EvaluationStatus, ffer.EvaluationDate, ffer.CommentsWithEval
                  , ffer.ShowExtRev2PC, ffer.ShowEval2PC, ffer.UserName, ffer.Password
                  , ffer.Source, ffer.EvaluationID);

            Application_TaskLogExt aptle = bal.GetAppTaskLogExt(ffer.ApplicationID, (int)TaskExtID.External_Evaluation, ffer.ExternalReviewerID)[0];
            bal.UpdateAppTskLgExt(ffer.ApplicationID, aptle.TaskID, null, DateTime.Now, false, aptle.Reminders
                , aptle.EmailAddress, aptle.ExternalReviewerID, ComposeEmail1.EmailBody
                , aptle.EmployeeID, aptle.ExternalEmployeeID);
            bal.InsertAppLgExt(ffer.ApplicationID, "External Evaluation Start (" + ffer.ExternalReviewerID + ")", (int) TaskExtID.External_Evaluation
               , ffer.ExternalReviewer.Name, ComposeEmail1.EmailBody, DateTime.Now);
            bal.InsertApplication_Log(ffer.ApplicationID, null, DateTime.Now, ComposeEmail1.EmailBody, "", "External Evaluation Start (" + ffer.ExternalReviewerID + ")", ffer.ExternalReviewer.NameString);
            labelProgrammaticPopup0.Text = "Email has been successfully sent. The external reviewer can now access the selected documents and evaluation form using the given login information";

        }
        else if (ffer.WLStatus == WillingessStatus.Accepted + ""
    && ffer.MaterialSentStatus == SendSelPubStatus.Material_Sent.ToString().Replace("_", " ")
    && ffer.EvaluationStatus == EvaluationStatus.Submitted+"")
        {
            ffer.MaterialSentDate = DateTime.Now;
            //            ffer.EvaluationStatus = EvaluationStatus.Not_Submitted.ToString().Replace("_", " ");
            ffer.EvaluationStatus = EvaluationStatus.Returned.ToString();
            ffer.EvaluationDate = null;

            erBAL.UpdateFormFinalExtRev(ffer.ApplicationID, ffer.ExternalReviewerID, ffer.Serial, ffer.WLStatus, ffer.WLDate, ffer.CommentsWithWL
          , ffer.MaterialSentStatus, ffer.MaterialSentDate, ffer.EvaluationStatus, ffer.EvaluationDate, ffer.CommentsWithEval
          , ffer.ShowExtRev2PC, ffer.ShowEval2PC, ffer.UserName, ffer.Password
          , ffer.Source, ffer.EvaluationID);

            Application_TaskLogExt aptle = bal.GetAppTaskLogExt(ffer.ApplicationID, (int)TaskExtID.External_Evaluation, ffer.ExternalReviewerID)[0];
            bal.UpdateAppTskLgExt(ffer.ApplicationID, aptle.TaskID, null, DateTime.Now, false, aptle.Reminders, aptle.EmailAddress
                , aptle.ExternalReviewerID, ComposeEmail1.EmailBody, aptle.EmployeeID, aptle.ExternalEmployeeID);
            bal.InsertAppLgExt(ffer.ApplicationID, "External Evaluation Start (" + ffer.ExternalReviewerID + ")", (int)TaskExtID.External_Evaluation
               , ffer.ExternalReviewer.Name, ComposeEmail1.EmailBody, DateTime.Now);
            //foreach (Form_Attachment row in bal.GetForm_AttachmentByAppID(ffer.ApplicationID))
            //{
            //    if (erfBAL.GetPublicationsEvaluation(ffer.ApplicationID, ExternalReviewerID, row.Form_AttachmentID).Count == 0)
            //    {
            //        erfBAL.InsertPublicationsEvaluation(ffer.ApplicationID, ExternalReviewerID, row.Form_AttachmentID, 1, "");
            //    }

            //}
            labelProgrammaticPopup0.Text = @"Email has been successfully sent. The external reviewer can 
            now access the selected documents and evaluation form using the given login information";
            bal.DeletetAppTskFrm(Master.ApplicationID, (int)TaskExtID.External_Evaluation, (int)FormID.PublicationsEvaluation_aspx,  true,0);
        }
        programmaticModalPopup0.Show();
        FormMode = "View";
        DatabindControls();

    }
    protected void ComposeEmail_OnEmailNotSent(object sender, EventArgs e)
    {
        FormMode = "View";
        DatabindControls();

    }
    #endregion
    #endregion
    #region Get Methods 
    public bool GetAppTaskFormCompleted()
    {
        if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0).Count > 0)
        {
            return bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0)[0].Completed;
        }
        else
        {
            return false;
        }
    }
    public string GetAppTaskFormMessage()
    {
        if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0).Count > 0)
        {
            return bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0)[0].Message;
        }
        else
        {
            return "";
        }

    }
    #endregion
    public string GetReminders(TaskExtID teID)
    {
        if(bal.GetAppTaskLogExt(Master.ApplicationID, (int)teID, int.Parse(Eval("ExternalReviewerID").ToString())).Count > 0)
        {
            return bal.GetAppTaskLogExt(Master.ApplicationID, (int)teID, int.Parse(Eval("ExternalReviewerID").ToString()))[0].Reminders.ToString();
        }
        else
        {
            return "";
        }
        
    }

    public System.Drawing.Color GetColor(string status)
    {
        if (status == EvaluationStatus.Submitted.ToString()) 
            return System.Drawing.Color.Green;
        else if (status == EvaluationStatus.Not_Submitted.ToString()) 
            return System.Drawing.Color.Red;
        else if (status == WillingessStatus.Accepted.ToString())
            return System.Drawing.Color.Green;
        else if (status == WillingessStatus.Declined.ToString())
            return System.Drawing.Color.Red;
        if (status == SendSelPubStatus.Material_Sent.ToString().Replace("_", " "))
            return System.Drawing.Color.Green;
        //else if (status == SendSelPubStatus.Material_Not_Sent.ToString().Replace("_", " "))
        //    return System.Drawing.Color.Red;
        else if (status == WillingessStatus.Withdrawn.ToString())
            return System.Drawing.Color.Red;
        else if (status == "True")
            return System.Drawing.Color.Green;
        else if (status == "False")
            return System.Drawing.Color.Red;

        return System.Drawing.Color.Black;

    }
}