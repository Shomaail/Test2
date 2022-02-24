using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using BL.Data;
using Newtonsoft.Json;

public partial class Forms_FormFinalPC : System.Web.UI.Page
{
    ExtRevFormBAL erfBAL = new ExtRevFormBAL();
    ExtRevBAL erBAL = new ExtRevBAL();
    private string dataPath = null;
    BAL bal = new BAL();
    private enum Mode { InputTopAuthority = 1, ViewByPC = 2,
        ViewBySC = 3
    }
    private enum Status { Not_saved_New_= 0, Saved_Unsuccessfully = 1, Saved_Successfully = 2, Updated = 3, Submitted = 4}  
    protected void Page_Load(object sender, EventArgs e)
    {
        dataPath = Server.MapPath("~\\App_Data\\ApplicationAttachments") + "\\";
        gvPCSuggestedByCollege.DataSource = bal.GetForm_PromotionCommittee(Master.ApplicationID,"C");
        if (IsPostBack )
        {
            if (!(Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("ddlQuickJump")))
            {
                return;
            }
                
        }
        if (bal.GetForm_AppProperties(Master.ApplicationID).Count == 0)
        {
            if (bal.GetForm_AreaOfSp(Master.ApplicationID).Count == 0)
            {
                bal.InsertForm_AppProperties(Master.ApplicationID, "", "", "", null, null);
            }
            else
            {
                Form_AreaOfSp fas = bal.GetForm_AreaOfSp(Master.ApplicationID)[0];
                bal.InsertForm_AppProperties(Master.ApplicationID, fas.AreaOfSpecialization, fas.PhdFrom, "", null, null);
            }
        }
        FormMode = "View";
        try
        {
            DatabindControls();
        }
        catch (Exception)
        {

            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID+"&roleID="+Master.CurRoleID);
            return;
        }
    }
    #region Properties
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
    public string PCCompleteStatus
    {
        set
        {
            ViewState["PCCompleteStatus"] = value;
        }
        get
        {
            if (ViewState["PCCompleteStatus"] != null)
                return (string)ViewState["PCCompleteStatus"];
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
    public string Subject
    {
        set
        {
            ViewState["Subject"] = value;
        }
        get
        {
            if (ViewState["Subject"] != null)
            {
                return (string)ViewState["Subject"];
            }
                
            else
            {
                return "";
            }
                
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
    public string EmployeeID
    {
        get
        {
            if (hfEmployeeID.Value.Length == 0)
            {
                return "0";
            }
                
            return hfEmployeeID.Value;
        }
        set
        {
            hfEmployeeID.Value = value.ToString();
        }
    }
    public int ExternalEmployeeID
    {
        get
        {
            if (hfExternalEmployeeID.Value.Length == 0)
            {
                return -1;
            }
            return int.Parse(hfExternalEmployeeID.Value);
        }
        set
        {
            hfExternalEmployeeID.Value = value.ToString();
        }
    }
    #endregion    
    private void SwitchMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.InputTopAuthority:
                UserMode = "InputTopAuthority";
                gvFinalPC.DataSource = bal.GetForm_FinalPC(Master.ApplicationID);
                gvAttachments.DataSource = bal.GetForm_AttachmentAdByAppID(Master.ApplicationID).Where(fa => fa.AttachmentCategoryID == (int)AttachmentCategoryID.PCReport);
                gvAttachments.Columns[0].Visible = false;
                divPCSuggestedByCollege.Visible = true;
                divNewSelection.Visible = true;
                break;
            case Mode.ViewByPC:
                UserMode = "ViewByPC";
                gvFinalPC.DataSource = bal.GetForm_FinalPC(Master.ApplicationID).Where(pc => pc.WLStatus == WillingessStatus.Accepted.ToString());
                gvFinalPC.Columns[2].Visible = false;
                gvFinalPC.Columns[6].Visible = false;
                gvFinalPC.Columns[7].Visible = false;
                gvFinalPC.Columns[8].Visible = false;
                gvFinalPC.Columns[9].Visible = false;
                gvFinalPC.Columns[11].Visible = false;
                gvAttachments.DataSource = bal.GetForm_AttachmentAdByAppID(Master.ApplicationID).Where(fa => fa.AttachmentCategoryID == (int)AttachmentCategoryID.PCReport);
                gvAttachments.Columns[0].Visible = false;
                divPCSuggestedByCollege.Visible = false;
                divNewSelection.Visible = false;
                break;
            case Mode.ViewBySC:
                gvFinalPC.DataSource = bal.GetForm_FinalPC(Master.ApplicationID).Where(pc => pc.WLStatus == WillingessStatus.Accepted.ToString());
                gvFinalPC.Columns[2].Visible = false;
                gvFinalPC.Columns[6].Visible = false;
                gvFinalPC.Columns[7].Visible = false;
                gvFinalPC.Columns[8].Visible = false;
                gvFinalPC.Columns[9].Visible = false;
                gvAttachments.DataSource = bal.GetForm_AttachmentAdByAppID(Master.ApplicationID).Where(fa => fa.AttachmentCategoryID == (int)AttachmentCategoryID.PCReport);
                gvAttachments.Columns[0].Visible = false;
                divPCSuggestedByCollege.Visible = false;
                divNewSelection.Visible = false;
                break;
        }
        gvAttachments.DataBind();
        gvFinalPC.DataBind();
    }
    private void DatabindControls()
    {
       // Instruction1.Text = Master.CurrentFormInstruction;
        SwitchMode((Mode)Master.CurrentFormLevel);
        divPCPromotionReportUpload.DataBind();
        lbtnEraseDS.DataBind();
        if (bal.GetForm_FinalPCByPK(Master.ApplicationID, Master.Employee.EmployeeID, Master.EEmployee.ExternalEmployeeID).Count() != 0)
        {
            Form_FinalPC ffpc = bal.GetForm_FinalPCByPK(Master.ApplicationID, Master.Employee.EmployeeID, Master.EEmployee.ExternalEmployeeID)[0];
            lbtnDigitalSign.Enabled = !ffpc.DigitalSignature.Value;
            lbtnDigitalSign.Visible = bal.GetForm_AttachmentAdByAppID(Master.ApplicationID).Where(a => a.AttachmentCategoryID == (int)AttachmentCategoryID.PCReport).Count() == 1;
            lbtnEraseDS.Visible = Master.CurRoleID == (byte)RoleID.Promotion_Committee_Chairman 
                && bal.GetForm_AttachmentAdByAppID(Master.ApplicationID).Where(a => a.AttachmentCategoryID == (int)AttachmentCategoryID.PCReport).Count() == 1;
            List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(Master.ApplicationID);
            string emailOfPC = "";
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
          //  aLink.HRef = "mailto:" + emailOfPC + "?subject=A message to Promotion Committee of the Promotion Application of "+ Master.Applicant.NameString  + " &body= This is a message to the Promotion Committee of " + Master.Applicant.NameString + " in the "+ Master.Applicant.Department + ", applying for the promotion to the rank of "+ lffpc[0].Application.ForRank;

        }
        else
        {
            lbtnDigitalSign.Visible = false;
            lbtnEraseDS.Visible = false;
        }

        List<Application_TaskLog> latl = bal.GetAppTaskLog(Master.ApplicationID).Where(a => !a.Completed && a.Task.RoleID == (byte)RoleID.Promotion_Committee_Chairman).ToList();        
        if(latl.Count > 0 && Master.CurRoleID == (byte)RoleID.TopAuthority)
        {
            divPCPromotionReport.Visible = false;
        }
        if (bal.GetForm_FinalPC(Master.ApplicationID).Where(p => p.DigitalSignature.Value).Count() == 5 )
        {
            if(bal.GetRole().Where(r => r.Title.Contains("Promotion Committee") && r.RoleID == Master.CurRoleID).Count() > 0)
            {
                PCCompleteStatus = "All Promotion Committee Members have digitally signed the promotion committee report. The promotion application can be returned to the "
                    + ConfigurationManager.AppSettings["TopAuthority_TitleShort"];
                lblMessage.Text = PCCompleteStatus;
                ShowMessage(PCCompleteStatus);
            }
            else if(Master.CurRoleID == (byte) RoleID.TopAuthority && latl.Count > 0)
            {
                PCCompleteStatus = "All Promotion Committee Members have digitally signed the promotion committee report but the application is not yet returned by the Promotion Committee Chairman. Once Returned then you can share the promotion application with the Scientific Council";
                lblMessage.Text = PCCompleteStatus;
                ShowError(PCCompleteStatus);
            }
            else if(Master.CurRoleID == (byte)RoleID.TopAuthority && latl.Count == 0)
            {
                PCCompleteStatus = "All Promotion Committee Members have digitally signed the promotion committee report. The promotion application can now be shared with the Scientific Council for final decision";
                lblMessage.Text = PCCompleteStatus;
                ShowMessage(PCCompleteStatus);
            }
        }
        else if (bal.GetForm_FinalPC(Master.ApplicationID)
            .Where(p => p.WLStatus == WillingessStatus.Accepted.ToString()).Count() == 5 && latl.Count > 0 && Master.CurRoleID == (byte)RoleID.TopAuthority)
        {
            divPCPromotionReport.Visible = false;
            PCCompleteStatus = "Promotion committee has been given access. Promotion Committee report is not yet uploaded and digitally signed by all members.";            
            ShowError(PCCompleteStatus);
        }
        else if (bal.GetForm_FinalPC(Master.ApplicationID)
     .Where(p => p.WLStatus == WillingessStatus.Accepted.ToString()).Count() == 5 && latl.Count > 0 
     && bal.GetRole().Where(r=>r.Title.Contains("Promotion Committee") && r.RoleID == Master.CurRoleID).Count() > 0)
        {
            if(Master.CurRoleID == (byte)RoleID.TopAuthority)
            {
                divPCPromotionReport.Visible = false;
            }            
            PCCompleteStatus = "Promotion Committee report is not yet uploaded and digitally signed by all members.";
            ShowError(PCCompleteStatus);
        }
        else if (latl.Count == 0 && bal.GetForm_FinalPC(Master.ApplicationID).Where(p => p.WLStatus == WillingessStatus.Accepted.ToString()).Count() == 5)
        {
            divPCPromotionReport.Visible = false;
            PCCompleteStatus = "Complete and ready to be given access. Go to Action Menu item and forward the Application to Promotion Committee.";
            //HttpCookie cookie = Request.Cookies["cookie"];
            //if (string.IsNullOrEmpty(cookie.Value))
            //{
            //    cookie = new HttpCookie("cookie");
            //    List<Employee> le = new List<Employee>();
            //    List<Form_FinalPC> ffpc = bal.GetForm_FinalPC(Master.ApplicationID);
            //    le.Add(new Employee { EmployeeID = ffpc[0].EmployeeID });
            //    le.Add(new Employee { EmployeeID = ffpc[1].EmployeeID });
            //    le.Add(new Employee { EmployeeID = ffpc[2].EmployeeID });
            //    le.Add(new Employee { EmployeeID = ffpc[3].EmployeeID });
            //    le.Add(new Employee { EmployeeID = ffpc[4].EmployeeID });
            //    cookie.Value = JsonConvert.SerializeObject(le);
            //    cookie.Expires = DateTime.Now.AddDays(120);
            //    Response.Cookies.Add(cookie);
            //}
            ShowMessage(PCCompleteStatus);
        }
        else if (bal.GetForm_FinalPC(Master.ApplicationID).Count() == 5)
        {
            divPCPromotionReport.Visible = false;
            PCCompleteStatus = "Complete but willingess to work is not received";
            ShowError(PCCompleteStatus);
        }
        else if (bal.GetForm_FinalPC(Master.ApplicationID).Count() == 5 && Master.App.ApplicationClosed)
        {
            List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(Master.ApplicationID);
            string emailOfPC = "";
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
        }
        else
        {
            divPCPromotionReport.Visible = false;
            PCCompleteStatus = "Incomplete";
            ShowError(PCCompleteStatus);
        }

        //lblPCCompleteStatus.DataBind();
        dvPCMemberInternal.DataBind();
        dvPCMemberExternal.DataBind();
        divFormFinalPC.DataBind();
        divViewDetail.DataBind();
        divSendWL.DataBind();
        divPCSuggestedByCollege.DataBind();
        divNewSelection.DataBind();
        lbtnSelect.Enabled = !bal.GetApplication(Master.ApplicationID)[0].ApplicationClosed;
        tbNewSelectionPCEmail.Text = "";
        lblName1.Text = "";
        lblRank1.Text = "";
        lblOrganization1.Text = "";
        lblDepartment1.Text = "";

        divTopOfFormMsgFailure.DataBind();
        lblMessageFailure.DataBind();
        divTopOfFormMsgSuccess.DataBind();
        lblMessageSuccess.DataBind();

    }
    #region Event Handlers
    
    #region Gridview
    #region GridviewFormFinalPC

    protected void gvFinalPC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int appID = Master.ApplicationID;
        if (e.CommandName == "Refresh")
        {
            DatabindControls();            
            return;
        }
        else
        {
            string[] args = e.CommandArgument.ToString().Split(';');
            ExternalEmployeeID = Convert.ToInt32(args[0]);
            EmployeeID = args[1];
        }
        if (e.CommandName == "DeletePC")
        {
            Form_FinalPC ffpc = bal.GetForm_FinalPC(appID).Where(pm => pm.EmployeeID == EmployeeID && pm.ExternalEmployeeID == ExternalEmployeeID).ToList()[0];
            if(bal.GetAppTaskLog(Master.ApplicationID).Where(a => a.TaskID == (int)TaskIDs.Promotion_Committee_Chairmanship && !a.Completed).Count() == 1)
            {
                labelProgrammaticPopup0.Text = "The promtion committee chairman/member cannot be deleted since the promotion committee has been given access. Deletion will be possible once the Promotion Committee Chariman returns the promotion application to "
                    + ConfigurationManager.AppSettings["TopAuthority_TitleShort"];
                programmaticModalPopup0.Show();
                return;
            }
            if (ffpc.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_"," ") && ffpc.WLStatus != WillingessStatus.Declined.ToString().Replace("_", " "))
            {
                labelProgrammaticPopup0.Text = "The promtion committee chairman/member cannot be deleted since the willingness has been sent. Delete Williness Status to proceed deletion.";
                programmaticModalPopup0.Show();
                return;
            }
            bal.DeleteForm_FinalPC(Master.ApplicationID, ffpc.EmployeeID, ffpc.ExternalEmployeeID);
            if (bal.GetApplicationRole(appID).Where(a => a.EmployeeID == ffpc.EmployeeID && a.Role.Title.Contains("Promotion Committee")).Count() != 0)
            {
                Application_Role ar = bal.GetApplicationRole(appID).Where(a => a.EmployeeID == ffpc.EmployeeID && a.Role.Title.Contains("Promotion Committee")).ToList()[0];
                bal.DeleteApplicationRoles(appID, ar.RoleID);

            }
        }
        else if (e.CommandName == "SelectDetail")
        {
            if(ExternalEmployeeID == 0)
            {
                dvPCMemberInternal.DataSource = bal.GetEmployeeByPK(EmployeeID);
                dvPCMemberInternal.DataBind();                
            }
            else
            {
                dvPCMemberExternal.DataSource = bal.GetExternalEmployeeByPK(ExternalEmployeeID);
                dvPCMemberExternal.DataBind();                
            }
            
            
            FormMode = "PCMemberDetail";
           

        }
        else if (e.CommandName == "SendWL")
        {
            List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(Master.ApplicationID);
            if (lffpc.Count != 5)
            {
                labelProgrammaticPopup0.Text = "Ad hoc Promotion Committee is not complete. It completes with 1 Chairman and 4 members. Once completed willingness letters can be sent.";
                programmaticModalPopup0.Show();
                return;
            }
            Form_FinalPC ffpc = bal.GetForm_FinalPC(appID).Where(pm => pm.EmployeeID == EmployeeID && pm.ExternalEmployeeID == ExternalEmployeeID).ToList()[0];

            //string name = "";
            //if (ExternalEmployeeID == 0)
            //{
            //    name = bal.GetEmployeeByPK(EmployeeID)[0].NameString;

            //}
            //else
            //{
            //    name= bal.GetExternalEmployeeByPK(ExternalEmployeeID).NameString;

            //}
            if (ffpc.Position == (int)PCPosition.Chairman)
            {
                MessageBody = bal.GetExtTaskMessage((int)TaskExtID.Willingness_for_being_Promotion_Committee_Chairman)[0].Message;
                Subject = bal.GetExtTaskMessage((int)TaskExtID.Willingness_for_being_Promotion_Committee_Chairman)[0].Subject.Replace("_", " ");
            }
            else if((ffpc.ExternalEmployeeID == 0 ? "Internal" : "External") == EmployeeType.Internal.ToString())
            {
                MessageBody = bal.GetExtTaskMessage((int)TaskExtID.Willingness_for_being_Promotion_Committee_Member)[0].Message;
                Subject = bal.GetExtTaskMessage((int)TaskExtID.Willingness_for_being_Promotion_Committee_Member)[0].Subject.Replace("_", " ");
            }
            else 
            {
                MessageBody = bal.GetExtTaskMessage((int)TaskExtID.Willingness_for_being_Promotion_Committee_Member_External)[0].Message;
                Subject = bal.GetExtTaskMessage((int)TaskExtID.Willingness_for_being_Promotion_Committee_Member_External)[0].Subject.Replace("_", " ");
            }
            MessageBody = MessageBody.Replace("@@RecipientName@@", ffpc.ExternalEmployeeID == 0 ? ffpc.Employee.NameString : ffpc.ExternalEmployee.NameString);            
            MessageBody = MessageBody.Replace("@@ApplicantCollege@@", Master.Applicant.ParentDept);
            MessageBody = MessageBody.Replace("@@Applicant@@", Master.Applicant.NameString);
            MessageBody = MessageBody.Replace("@@ApplicantDept@@", Master.Applicant.Department);
            MessageBody = MessageBody.Replace("@@ApplicantRank@@", Master.Applicant.Rank);
            MessageBody = MessageBody.Replace("@@ForRank@@", bal.GetApplication(Master.ApplicationID)[0].ForRank);
            MessageBody = MessageBody.Replace("@@WebsiteURL@@", ConfigurationManager.AppSettings["WebsiteURL"]);
            MessageBody = MessageBody.Replace("@@appID@@", Master.ApplicationID.ToString());
            MessageBody = MessageBody.Replace("@@empID@@", EmployeeID);
            MessageBody = MessageBody.Replace("@@eeID@@", ExternalEmployeeID.ToString());        
            MessageBody = MessageBody.Replace("@@ApplicantAreaOfSp@@", bal.GetForm_AppProperties(appID)[0].AreaOfSpecialization);
            MessageBody = MessageBody.Replace("@@TopAuthority@@", bal.GetEmployeeByPK((bal.GetApplicationRole(appID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameString);
            MessageBody = MessageBody.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);

            
            EmailContent email = new EmailContent { To = ffpc.ExternalEmployeeID == 0 ? ffpc.Employee.Email : ffpc.ExternalEmployee.Email
                , Body = MessageBody, Subject = Subject };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadData(le);
            ComposeEmail1.Visible = true;
            FormMode = "SendWL";
           
        }       
        else if (e.CommandName == "ViewWLEmail")
        {
            
            Form_FinalPC ffpc = bal.GetForm_FinalPC(appID).Where(pm => pm.EmployeeID == EmployeeID && pm.ExternalEmployeeID == ExternalEmployeeID).ToList()[0];
            Application_TaskLogExt aptle;
            int taskExtID = -1;
            if(ffpc.Position == (int) PCPosition.Chairman)
            {
                taskExtID = (int)TaskExtID.Willingness_for_being_Promotion_Committee_Chairman;
            }else
            {
                taskExtID = (int)TaskExtID.Willingness_for_being_Promotion_Committee_Member;
            }
            aptle = bal.GetAppTaskLogExt(ffpc.ApplicationID, taskExtID, ffpc.EmployeeID, ffpc.ExternalEmployeeID)[0];
            EmailContent email = new EmailContent
            {
                To = aptle.EmailAddress,
                Body = aptle.Message
                ,
                Subject = bal.GetExtTaskMessage(taskExtID)[0].Subject.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
            };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadDataForPreview(le);
            ComposeEmail1.Visible = true;
            FormMode = "SendWL";
            
        }
        else if (e.CommandName == "DeleteWLStatus")
        {
            Form_FinalPC ffpc = bal.GetForm_FinalPC(appID).Where(pm => pm.EmployeeID == EmployeeID && pm.ExternalEmployeeID == ExternalEmployeeID).ToList()[0];
            //if (ffpc.MaterialSentStatus == SendSelPubStatus.Material_Sent.ToString())
            //{
            //    labelProgrammaticPopup0.Text = "The willingness of the promotion committee chairman/member cannot be deleted since promotion Committee is finalized.";
            //    programmaticModalPopup0.Show();
            //    return;
            //}
            foreach (Application_TaskLogExt row in bal.GetAppTaskLogExt().Where(atle => atle.ApplicationID == appID
            && atle.EmployeeID == ffpc.EmployeeID && atle.ExternalEmployeeID == ffpc.ExternalEmployeeID))
            {
                bal.DeleteApplication_TaskLogExt(row.TaskLogID);
            }
            ffpc.WLStatus = WillingessStatus.Not_Sent.ToString().Replace("_", " ");
            ffpc.WLDate = null;
            ffpc.CommentsWithWL = "";
            bal.UpdateForm_FinalPC(appID,ffpc.EmployeeID, ffpc.ExternalEmployeeID, ffpc.Position,ffpc.WLStatus,ffpc.DigitalSignature, ffpc.DSDate, ffpc.Comments,
                ffpc.Source, ffpc.WLDate,ffpc.CommentsWithWL, ffpc.Status);
            if (bal.GetApplicationRole(appID).Where(a => a.EmployeeID == ffpc.EmployeeID && a.Role.Title.Contains("Promotion Committee")).Count() != 0 )
            {
                Application_Role ar = bal.GetApplicationRole(appID).Where(a => a.EmployeeID == ffpc.EmployeeID && a.Role.Title.Contains("Promotion Committee")).ToList()[0];
                bal.DeleteApplicationRoles(appID, ar.RoleID);     
            }
          
        }
        DatabindControls();
    }
    protected void gvFinalPC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void gvFinalPC_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        DatabindControls();
    }
    #endregion
    #region gvPCSuggestedByCollege
    protected void gvPCSuggestedByCollege_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvPCSuggestedByCollege.Rows)
        {
            DropDownList DropDownListAsWhat = row.FindControl("ddlAsWhat") as DropDownList;
            if (row.RowIndex >= 0 && row.RowIndex <= 2)
            {
                DropDownListAsWhat.Items.FindByValue("Chairman").Selected = true;

            }
            else if (row.RowIndex >= 3 && row.RowIndex <= 6)
            {
                DropDownListAsWhat.Items.FindByValue("Member_Specialty_Area").Selected = true;
            }
            else
            {
                DropDownListAsWhat.Items.FindByValue("Member_Related_Area").Selected = true;
            }

        }
    }
    protected void gvPCSuggestedByCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow row = gvPCSuggestedByCollege.SelectedRow;
        //string nameString = gvPCSuggestedByCollege.SelectedRow.Cells[3].Text;
        DropDownList ddlAsWhat = row.FindControl("ddlAsWhat") as DropDownList;
        Form_PromotionCommittee  fpc = bal.GetForm_PromotionCommitteeByPK(Master.ApplicationID,"c", (byte)gvPCSuggestedByCollege.SelectedRow.RowIndex)[0];
        EmployeeID = fpc.EmployeeID;
        ExternalEmployeeID = fpc.ExternalEmployeeID;
        CheckAndInsert(ddlAsWhat, PCMemberSelType.College);
        DatabindControls();

    }
    #endregion
    #region gvAttachment
    protected void gvAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if ((e.Row.FindControl("lbtnDownload") as LinkButton) != null)
            {
                (e.Row.FindControl("lbtnDownload") as LinkButton).Attributes.Add("target", "_blank");
            }
            
        }
        catch (Exception)
        {


        }
        //if(e.Row.RowType != DataControlRowType.Header 
        //    && e.Row.RowType != DataControlRowType.Footer 
        //    && e.Row.RowType != DataControlRowType.Pager 
        //    && e.Row.RowType != DataControlRowType.EmptyDataRow              
        //    )
        //{
        //    string attCat = "";
        //    if (e.Row.RowState == (DataControlRowState.Alternate ^ DataControlRowState.Edit))
        //    {
        //        attCat = (e.Row.FindControl("ddlAttCat") as DropDownList).SelectedValue.Replace(" ", "_");
        //    }
        //    else
        //    {
        //        attCat = ((Label)e.Row.FindControl("lblAttachmentCategory")).Text.Replace(" ", "_");
        //    }
        //    AttachmentCategoryID acID = (AttachmentCategoryID)Enum.Parse(typeof(AttachmentCategoryID), attCat);
        //    if (acID == AttachmentCategoryID.Others)
        //    {
        //        (e.Row.FindControl("cbFirstAuthor") as CheckBox).Visible = false;
        //        (e.Row.FindControl("tbNoOfAuthors") as TextBox).Visible = false;
        //    }
        //    else
        //    {
        //        (e.Row.FindControl("cbFirstAuthor") as CheckBox).Visible = true;
        //        (e.Row.FindControl("tbNoOfAuthors") as TextBox).Visible = true;
        //    }
        //}
    }
    protected void gvAttachments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int form_AttachmentID = int.Parse(gvAttachments.DataKeys[e.RowIndex].Value.ToString());
        Form_AttachmentAd attachment = bal.GetForm_AttachmentAd(form_AttachmentID)[0];
        //if (attachment.SelectionForExtRev.Value &&
        //    erfBAL.GetPublicationsEvaluation().Where(pe => pe.Form_AttachmentID == form_AttachmentID).Count() != 0)
        //{
        //    labelProgrammaticPopup0.Text = "You cannot delete this publication since it has been sent for evaluation.";
        //    programmaticModalPopup0.Show();
        //    return;
        //}
        //foreach (PublicationsEvaluation pe in erfBAL.GetPublicationsEvaluation().Where(pe => pe.ApplicationID == Master.ApplicationID && pe.Form_AttachmentID == form_AttachmentID))
        //{
        //    erfBAL.DeletePublicationsEvaluation(Master.ApplicationID, pe.ExternalReviewerID, form_AttachmentID);
        //}
        EraseDigitalSignature();
        System.IO.File.Delete(dataPath + attachment.ApplicationID + "_" + attachment.EmployeeID + "_" + attachment.DocumentName);
        bal.DeleteForm_AttachmentAd(form_AttachmentID);
        DatabindControls();
    }
    #endregion
    #endregion
    #region Button Event Handlers
    protected void lbtnSendEmailToPC_Click(object sender, EventArgs e)
    {
        int appID = Master.ApplicationID;
        //if (bal.GetAppTaskLog(appID).Where(a => a.TaskID == (int)TaskIDs.Promotion_Committee_Chairmanship && !a.Completed).Count() != 1)
        //{
        //    labelProgrammaticPopup0.Text = "The promtion committee has not been given access to the Promotion Application.If you have formed a Promotion Committee then go to Action Menu item and Forward the Application to Internal Promotion Committee.";
        //    programmaticModalPopup0.Show();
        //    return;
        //}
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;
        Employee Applicant = bal.GetApplicant(appID)[0];
        Employee PCChair = bal.GetForm_FinalPC(Master.ApplicationID).Where(a => a.Position == (int)PCPosition.Chairman).ToList()[0].Employee;


        string Message = "", Subject = "";
        if (bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Material_Updated).Count() == 1)
        {
            Message = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Material_Updated).ToList()[0].Message;
            Subject = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Material_Updated).ToList()[0].Subject;
            Message = Message
                .Replace("@@Applicant@@", Applicant.NameString)
                .Replace("@@ForRank@@", bal.GetApplication(appID)[0].ForRank)
                .Replace("@@ApplicantDept@@", Applicant.Department)
                .Replace("@@RecipientName@@", PCChair.NameString)
                .Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"])
                .Replace("@@Sender@@", TopAuthority.NameString)
                .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"]);
        }
        List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(Master.ApplicationID);
        string allPCEmail = "";
        foreach (Form_FinalPC pcMem in lffpc)
        {
            if (pcMem.ExternalEmployeeID == 0)
            {
                allPCEmail = allPCEmail + pcMem.Employee.Email + ",";
            }
            else
            {
                allPCEmail = allPCEmail + pcMem.ExternalEmployee.Email + ",";
            }
        }
        allPCEmail = allPCEmail.Trim(',');
        EmailContent email = new EmailContent
        {
            To = allPCEmail,
            Body = Message,
            Subject = Subject
        };
        List<EmailContent> le = new List<EmailContent>();
        le.Add(email);
        ComposeEmail1.LoadData(le);
        ComposeEmail1.Visible = true;
        FormMode = "SendEmailMaterialUpdated";
        DatabindControls();
    }
    protected void lbtnContactPCByPCChair_Click(object sender, EventArgs e)
    {
        int appID = Master.ApplicationID;
        //if (bal.GetAppTaskLog(appID).Where(a => a.TaskID == (int)TaskIDs.Promotion_Committee_Chairmanship && !a.Completed).Count() != 1)
        //{
        //    labelProgrammaticPopup0.Text = "The promtion committee has not been given access to the Promotion Application.If you have formed a Promotion Committee then go to Action Menu item and Forward the Application to Internal Promotion Committee.";
        //    programmaticModalPopup0.Show();
        //    return;
        //}
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;
        Employee Applicant = bal.GetApplicant(appID)[0];
        Employee PCChair = bal.GetForm_FinalPC(Master.ApplicationID).Where(a => a.Position == (int)PCPosition.Chairman).ToList()[0].Employee;


        string Message = "", Subject = "";
        if (bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Request_Members_to_Digitally_Sign).Count() == 1)
        {
            Message = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Request_Members_to_Digitally_Sign).ToList()[0].Message;
            Subject = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Request_Members_to_Digitally_Sign).ToList()[0].Subject;
            Message = Message
                .Replace("@@Applicant@@", Applicant.NameString)
                .Replace("@@ForRank@@", bal.GetApplication(appID)[0].ForRank)
                .Replace("@@ApplicantDept@@", Applicant.Department)                
                .Replace("@@Sender@@", PCChair.NameString)
                .Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"])                
                .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"]);
        }
        List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(Master.ApplicationID);
        string allPCEmail = "";
        foreach (Form_FinalPC pcMem in lffpc)
        {
            if(pcMem.Position == (int) PCPosition.Chairman)
            {
                continue;
            }
            if (pcMem.ExternalEmployeeID == 0)
            {
                allPCEmail = allPCEmail + pcMem.Employee.Email + ",";
            }
            else
            {
                allPCEmail = allPCEmail + pcMem.ExternalEmployee.Email + ",";
            }
        }
        allPCEmail = allPCEmail.Trim(',');
        EmailContent email = new EmailContent
        {
            To = allPCEmail,
            Body = Message,
            Subject = Subject
        };
        List<EmailContent> le = new List<EmailContent>();
        le.Add(email);
        ComposeEmail1.LoadData(le);
        ComposeEmail1.Visible = true;
        FormMode = "SendEmailRequestToDigitalSign";
        DatabindControls();
    }
    protected void lbtnUpload_Click(object sender, EventArgs e)
    {
        HttpFileCollection uploadedFile = Request.Files;        
        if (uploadedFile[0].FileName == "")
        {
            labelProgrammaticPopup0.Text = "Choose a file then click on Upload button.";
            programmaticModalPopup0.Show();
            return;
        }
        var regexItem = new Regex("^[\\w,\\s-]+\\.[A-Za-z]{3}$");
        if (!regexItem.IsMatch(uploadedFile[0].FileName)) 
        {
            labelProgrammaticPopup0.Text = "The file name of the uploaded file is unacceptable. No file is uploaded. File name can only have characters like alphabets from A-Z, digits from 0-9 and _ .";
            programmaticModalPopup0.Show();
            return;
        }
        if (uploadedFile[0].ContentType != "application/pdf")
        {
            labelProgrammaticPopup0.Text = "You can only upload pdf files. No file is uploaded. ";
            programmaticModalPopup0.Show();
            return;
        }
        if (uploadedFile[0].ContentLength > 4000000)
        {
            labelProgrammaticPopup0.Text = "You can only upload files of size less than 4 MB. No file is uploaded. ";
            programmaticModalPopup0.Show();

            return;
        }
        if (gvAttachments.DataKeys.Count != 0 )
        {
            int form_AttachmentID = int.Parse(gvAttachments.DataKeys[0].Value.ToString());
            Form_AttachmentAd attachment = bal.GetForm_AttachmentAd(form_AttachmentID)[0];
            System.IO.File.Delete(dataPath + attachment.ApplicationID + "_" + attachment.EmployeeID + "_" + attachment.DocumentName);
            bal.DeleteForm_AttachmentAd(form_AttachmentID);
        }
        
        Span.Text = string.Empty;
        tbPCReportDesc.Value = Regex.Replace(tbPCReportDesc.Value, PromotionApplication.specialCharacters, string.Empty);
        UploadFile(uploadedFile, 0,(int) AttachmentCategoryID.PCReport, tbPCReportDesc.Value=="" ? uploadedFile[0].FileName: tbPCReportDesc.Value, DateTime.Now,"");
        EraseDigitalSignature();
        DatabindControls();
    }

    protected void lbtnDownload_PreRender(object sender, EventArgs e)
    {
        int rowID = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
        int faID = int.Parse(gvAttachments.DataKeys[rowID].Value.ToString());
        List<Form_AttachmentAd> ld = bal.GetForm_AttachmentAd(faID);
        if (ld.Count == 0 || ld[0].DocumentName == "") { return; }
        Form_AttachmentAd attachment = ld[0];
        (sender as LinkButton).OnClientClick = "window.open('"
            + "NoMasterPage/Handler.ashx?appID=" + Master.ApplicationID
            + "&empID="
            + bal.GetForm_FinalPC(Master.ApplicationID)[0].EmployeeID
            + "&fileName=" + attachment.DocumentName + "'); return false ;";
    }
    protected void lbtnDigitalSign_Click(object sender, EventArgs e)
    {
        Form_FinalPC ffpc = bal.GetForm_FinalPCByPK(Master.ApplicationID, Master.Employee.EmployeeID, Master.EEmployee.ExternalEmployeeID)[0];
        ffpc.DigitalSignature = true;
        ffpc.DSDate = DateTime.Now;
        bal.UpdateForm_FinalPC(ffpc.ApplicationID, ffpc.EmployeeID, ffpc.ExternalEmployeeID, ffpc.Position, ffpc.WLStatus, ffpc.DigitalSignature, ffpc.DSDate, ffpc.Comments
           , ffpc.Source, ffpc.WLDate, ffpc.CommentsWithWL, ffpc.Status);
        DatabindControls();

    }
    protected void lbtnEraseDS_Click(object sender, EventArgs e)
    {
        EraseDigitalSignature();
    }
    protected void lbtnSelect_Click(object sender, EventArgs e)
    {
        //        bal.InsertForm_FinalPC(Master.ApplicationID, EmployeeID, ExternalEmployeeID, (int) Enum.Parse(typeof(PCPosition), ddlNewSelection.SelectedValue), WillingessStatus.Waiting.ToString()
        //, false, null, "", RecordStatus.Active.ToString(), PCMemberSelType.TopAuthority.ToString());
        //        gvFinalPC.DataBind();
        Regex regex = new Regex(PromotionApplication.specialCharacters);
        if (regex.IsMatch(tbNewSelectionPCEmail.Text))
        {
            return;
        }

        if (tbNewSelectionPCEmail.Text == "")
        {
            return;
        }
        CheckAndInsert(ddlNewSelection, PCMemberSelType.TopAuthority);
        DatabindControls();
    }
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
    protected void tbNewSelectionPCEmail_TextChanged(object sender, EventArgs e)
    {
        EmployeeID = "0";
        ExternalEmployeeID = 0;
        if (!tbNewSelectionPCEmail.Text.Contains('@'))
        {
            tbNewSelectionPCEmail.Text = tbNewSelectionPCEmail.Text + ConfigurationManager.AppSettings["OrganizationEmail1"];
         //   (pnlDeptComm.FindControl("tbDC" + rowNumber + "Email") as TextBox).Text = Email;
        }
        if (bal.GetEmployeeByEmail(tbNewSelectionPCEmail.Text).Count > 0)
        {
            Employee emp = bal.GetEmployeeByEmail(tbNewSelectionPCEmail.Text)[0];
            lblName1.Text = emp.NameString;
            lblRank1.Text = emp.Rank;
            lblDepartment1.Text = emp.Department1.Name;
            lblOrganization1.Text = "Internal";
            EmployeeID = emp.EmployeeID;
        }
        else if(bal.GetExternalEmployeeByEmail(tbNewSelectionPCEmail.Text).Count > 0)
        {
            ExternalEmployee ee = bal.GetExternalEmployeeByEmail(tbNewSelectionPCEmail.Text)[0];
            lblName1.Text = ee.NameString;
            lblRank1.Text = ee.Rank;
            lblDepartment1.Text = ee.Department;
            lblOrganization1.Text = ee.Organization;
            ExternalEmployeeID = ee.ExternalEmployeeID;
        }
        else
        {
            lblError1.Text = "User not found";
            lblError1.Visible = true;
            lblName1.Text = "";
            lblRank1.Text = "";
            lblDepartment1.Text = "";
            lblOrganization1.Text = "";
        }
    }
    #region ComposeEmail
    protected void ComposeEmail_OnEmailSent(object sender, EventArgs e)
    {
        if(Master.CurRoleID == (byte)RoleID.Promotion_Committee_Chairman)
        {
            labelProgrammaticPopup0.Text = "Email has been successfully sent.";
            programmaticModalPopup0.Show();
            FormMode = "View";
            DatabindControls();
            return;
        }
        List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(Master.ApplicationID);
        if(lffpc.Where(a => a.WLStatus == WillingessStatus.Accepted.ToString()).Count() == 5)
        {
            if (bal.GetFormMaterialFlag(Master.ApplicationID).Count == 0)
            {
                bal.InsertFormMaterialFlag(Master.ApplicationID, null, "", "", "");
            }
            Form_MaterialFlag fmf = bal.GetFormMaterialFlag(Master.ApplicationID)[0];
            bal.UpdateFormMaterialFlag(fmf.ApplicationID, fmf.PCMaterialReady4ExtRevFlag, fmf.MaterialOKEmail, fmf.MaterialNotOKEmail, ComposeEmail1.EmailBody);
        }
        else
        {
            Form_FinalPC ffpc = bal.GetForm_FinalPC(Master.ApplicationID).Where(pm => pm.EmployeeID == EmployeeID && pm.ExternalEmployeeID == ExternalEmployeeID).ToList()[0];
            bal.UpdateForm_FinalPC(ffpc.ApplicationID, EmployeeID, ExternalEmployeeID, ffpc.Position, WillingessStatus.Waiting.ToString(), ffpc.DigitalSignature, ffpc.DSDate, ffpc.Comments
                ,ffpc.Source,ffpc.WLDate,ffpc.CommentsWithWL,ffpc.Status);        
            if(ffpc.Position == (int) PCPosition.Chairman)
            {
                bal.InsertAppTskLgExt(ffpc.ApplicationID
                                        , (int)TaskExtID.Willingness_for_being_Promotion_Committee_Chairman
                                        , null, DateTime.Now, false, 0, ffpc.ExternalEmployeeID == 0 ? ffpc.Employee.Email : ffpc.ExternalEmployee.Email, 0, ComposeEmail1.EmailBody, EmployeeID,ExternalEmployeeID);

            }
            else
            {
                bal.InsertAppTskLgExt(ffpc.ApplicationID
                                        , (int)TaskExtID.Willingness_for_being_Promotion_Committee_Member
                                        , null, DateTime.Now, false, 0, ffpc.ExternalEmployeeID == 0 ? ffpc.Employee.Email : ffpc.ExternalEmployee.Email, 0, ComposeEmail1.EmailBody, EmployeeID, ExternalEmployeeID);
            }
        }
        labelProgrammaticPopup0.Text = "Email has been successfully sent.";
        programmaticModalPopup0.Show();
        FormMode = "View";
        DatabindControls();
       

    }
    protected void ComposeEmail_OnEmailNotSent(object sender, EventArgs e)
    {
        if (Master.CurRoleID == (byte)RoleID.Promotion_Committee_Chairman)
        {            
            FormMode = "View";
            DatabindControls();
            return;
        }
        FormMode = "View";
        DatabindControls();

    }
    #endregion
    #endregion
    #region Related Methods
    private void EraseDigitalSignature()
    {
        foreach (Form_FinalPC ffpc in bal.GetForm_FinalPC(Master.ApplicationID))
        {
            ffpc.DigitalSignature = false;
            ffpc.DSDate = null;
            bal.UpdateForm_FinalPC(ffpc.ApplicationID, ffpc.EmployeeID, ffpc.ExternalEmployeeID, ffpc.Position, ffpc.WLStatus, ffpc.DigitalSignature, ffpc.DSDate, ffpc.Comments
           , ffpc.Source, ffpc.WLDate, ffpc.CommentsWithWL, ffpc.Status);
        }
        DatabindControls();
    }
    private void UploadFile(HttpFileCollection uploadedFiles, int i, int attCat, string description, DateTime? DateOfPublication, string Journal)
    {
        HttpPostedFile userPostedFile = uploadedFiles[i];
        try
        {
            if (userPostedFile.ContentLength > 0)
            {
                DirectoryInfo folder = new DirectoryInfo(dataPath);
                FileInfo[] files;
                files = folder.GetFiles(Master.ApplicationID + "_" + Master.Employee.EmployeeID + "_" + userPostedFile.FileName, SearchOption.AllDirectories);

                if (files.Count() == 0 || files[0].Length != userPostedFile.ContentLength)
                {
                    if (bal.GetForm_AttachmentAdByAppID(Master.ApplicationID).Where(f => f.DocumentName == userPostedFile.FileName).Count() != 0)
                    {
                        bal.DeleteForm_AttachmentAd(bal.GetForm_AttachmentAdByAppID(Master.ApplicationID).Where(f => f.DocumentName == userPostedFile.FileName).ToList()[0].Form_AttachmentAdID);
                    }
                    bal.InsertForm_AttachmentAd(attCat, Master.ApplicationID, Master.Employee.EmployeeID, userPostedFile.FileName
                   , userPostedFile.ContentLength / 1000000 + " MB", Master.CurRoleID, description, false
                   , DateOfPublication, Journal, 1, true, 0, "", true);
                }
                userPostedFile.SaveAs(dataPath + Master.ApplicationID + "_" + Master.Employee.EmployeeID + "_" + userPostedFile.FileName);
            }
           
        }
        catch (Exception Ex)
        {
            Span.Text += "Error: <br>" + Ex.Message;
        }
    }
    public string GetReminders(int position, string empID, int extEmpID)
    {
        int taskExtID = -1;
        if(position == (int) PCPosition.Chairman)
        {
            taskExtID = (int)TaskExtID.Willingness_for_being_Promotion_Committee_Chairman;
        }
        else
        {
            taskExtID = (int)TaskExtID.Willingness_for_being_Promotion_Committee_Member ;
        }
        if (bal.GetAppTaskLogExt(Master.ApplicationID, taskExtID,empID,extEmpID).Count > 0)
        {
            return bal.GetAppTaskLogExt(Master.ApplicationID, taskExtID, empID, extEmpID)[0].Reminders.ToString();
        }
        else
        {
            return "";
        }

    }
    public Color GetColor(string status)
    {
        if (status == EvaluationStatus.Submitted.ToString())
            return Color.Green;
        else if (status == EvaluationStatus.Not_Submitted.ToString())
            return Color.Red;
        else if (status == WillingessStatus.Accepted.ToString())
            return Color.Green;
        else if (status == WillingessStatus.Declined.ToString())
            return Color.Red;
        if (status == SendSelPubStatus.Material_Sent.ToString().Replace("_", " "))
            return Color.Green;
        else if (status == SendSelPubStatus.Material_Not_Sent.ToString().Replace("_", " "))
            return Color.Red;
        else if (status == WillingessStatus.Withdrawn.ToString())
            return Color.Red;
        return Color.Black;

    }
    private void CheckAndInsert(DropDownList ddlAsWhat, PCMemberSelType selectionType)
    {
        string selectionTypeString = "";
        if (selectionType == PCMemberSelType.TopAuthority)
        {
            selectionTypeString = ConfigurationManager.AppSettings["TopAuthority_TitleShort"];

        }
        else
        {
            selectionTypeString = selectionType.ToString();
        }
        List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(Master.ApplicationID);
        if (lffpc.Where(pc => (pc.EmployeeID == EmployeeID && EmployeeID != "0") || (ExternalEmployeeID > 0  && pc.ExternalEmployeeID == ExternalEmployeeID)).Count() > 0)
        {
            labelProgrammaticPopup0.Text = "This selection is already present in the Ad hoc Promotion Committee.";
            programmaticModalPopup0.Show();
            return;
        }
        if (lffpc.Count == 5 )
        {
            labelProgrammaticPopup0.Text = "Ad hoc Promotion Committee is complete you cannot have more than 1 Chairman and 4 members in a promotion committee.";
            programmaticModalPopup0.Show();
            return;
        }
        if (ddlAsWhat.SelectedValue == PCPosition.Chairman.ToString())
        {
            if (lffpc.Where(pc => pc.Position == (int)PCPosition.Chairman).Count() == 1)
            {
                labelProgrammaticPopup0.Text = "The Nominated Ad hoc Promotion Committee can have only one chairman. Please delete the existing entry of chairman to include the new selection.";
                programmaticModalPopup0.Show();
            }
            else
            {
                if(ExternalEmployeeID != 0)
                {
                    labelProgrammaticPopup0.Text = "The Nominated Ad hoc Promotion Committee cannot be external.";
                    programmaticModalPopup0.Show();
                    return;
                }
                bal.InsertForm_FinalPC(Master.ApplicationID, EmployeeID, ExternalEmployeeID, (int)PCPosition.Chairman, WillingessStatus.Not_Sent.ToString().Replace("_"," ")
                    , false, null, "", RecordStatus.Active.ToString(), selectionTypeString);               
            }
        }
        else if (ddlAsWhat.SelectedValue == PCPosition.Member_Specialty_Area.ToString())
        {
            if (lffpc.Where(pc => pc.Position == (int)PCPosition.Member_Specialty_Area).Count() == 2)
            {
                labelProgrammaticPopup0.Text = "The Nominated Ad hoc Promotion Committee can have only two members from Specialty Area. Please delete any of the existing entries of members in Specialty Area to include the new selection.";
                programmaticModalPopup0.Show();
            }
            else
            {
                bal.InsertForm_FinalPC(Master.ApplicationID, EmployeeID, ExternalEmployeeID, (int)PCPosition.Member_Specialty_Area, WillingessStatus.Not_Sent.ToString().Replace("_", " ")
                    , false, null, "", RecordStatus.Active.ToString(), selectionTypeString);              
            }
        }
        else
        {
            if (lffpc.Where(pc => pc.Position == (int)PCPosition.Member_Related_Area).Count() == 2)
            {
                labelProgrammaticPopup0.Text = "The Nominated Ad hoc Promotion Committee can have only two members from Related Area. Please delete any of the existing entries of members in Related Area to include the new selection.";
                programmaticModalPopup0.Show();
            }
            else
            {
                bal.InsertForm_FinalPC(Master.ApplicationID, EmployeeID, ExternalEmployeeID, (int)PCPosition.Member_Related_Area, WillingessStatus.Not_Sent.ToString().Replace("_", " ")
                , false, null, "", RecordStatus.Active.ToString(), selectionTypeString);
              

            }

        }
    }
    private void ShowError(string message)
    { 
        Master.ReportFailure(message);
    }

    private void ShowMessage(string message)
    {
    
        Master.ReportSuccess(message);
    }
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
}