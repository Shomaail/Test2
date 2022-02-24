using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Configuration;
using BL.Data;
public partial class Forms_FilesUpload : System.Web.UI.Page
{
    ExtRevFormBAL erfBAL = new ExtRevFormBAL();
    ExtRevBAL erBAL = new ExtRevBAL();
    BAL bal = new BAL();
    private string dataPath = null;
    public override string StyleSheetTheme
    {
        get
        {
            return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
        }
    }

    public enum Mode { InputByRole = 1, ViewRole = 2, ViewAll = 3, ViewAppInputRole = 4, ViewAllInputRole = 5,PCChair = 6}

    protected void Page_Load(object sender, EventArgs e)
    {       
        dataPath = Server.MapPath("~\\App_Data\\ApplicationAttachments") + "\\";
        if (IsPostBack) { return; }

        try
        {
            DatabindControls();
        }
        catch 
        {
            Response.Redirect("~/Login.aspx",false);
        }
        if (Master.CurrentFormLevel == -1)
        {
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID+"&roleID="+Master.CurRoleID);
            return;
        }

    }    
    #region Event Handlers 
   
    #region gvAttachments

    //protected void gvAttachments_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    List <Form_Attachment> ld = bal.GetForm_Attachment(int.Parse(gvAttachments.SelectedDataKey.Value.ToString()));
    //    if (ld.Count == 0 || ld[0].DocumentName=="") { return; }
    //    Form_Attachment attachment = ld[0];
    //    //  Response.Redirect("App_Data\\ApplicationAttachments\\"+ Master.ApplicationID + "_" + Master.Employee.EmployeeID + "_" + attachment.DocumentName);
    //    Response.Redirect("NoMasterPage\\Handler.ashx?appID=" + Master.ApplicationID + "&empID=" + Master.Employee.EmployeeID + "&fileName=" + attachment.DocumentName);
    //}

    protected void gvAttachments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int form_AttachmentAdID = int.Parse(gvAttachments.DataKeys[e.RowIndex].Value.ToString());
        Form_AttachmentAd attachment = bal.GetForm_AttachmentAd(form_AttachmentAdID)[0];
        //if (attachment.SelectionForExtRev.Value &&
        //    erfBAL.GetPublicationsEvaluation().Where(pe=>pe.Form_AttachmentID == form_AttachmentID).Count() != 0)
        //{
        //    labelProgrammaticPopup0.Text = "You cannot delete this publication since it has been sent for evaluation.";
        //    programmaticModalPopup0.Show();
        //    return;
        //}
        //foreach(PublicationsEvaluation pe in erfBAL.GetPublicationsEvaluation().Where(pe=>pe.ApplicationID == Master.ApplicationID && pe.Form_AttachmentID == form_AttachmentID))
        //{
        //    erfBAL.DeletePublicationsEvaluation(Master.ApplicationID, pe.ExternalReviewerID, form_AttachmentID);
        //}
        System.IO.File.Delete(dataPath + attachment.ApplicationID + "_" + attachment.EmployeeID + "_" + attachment.DocumentName);
        bal.DeleteForm_AttachmentAd(form_AttachmentAdID);
        DatabindControls();
    }

    protected void gvAttachments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvAttachments_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridViewRow row = gvAttachments.Rows[e.NewEditIndex];
        //string value = (row.FindControl("lblAttachmentCategory") as Label).Text;
        gvAttachments.EditIndex = e.NewEditIndex;
        DatabindControls();
        //(gvAttachments.Rows[e.NewEditIndex].FindControl("ddlAttCat") as DropDownList).SelectedValue = value;
    }

    protected void gvAttachments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvAttachments.EditIndex = -1;
        DatabindControls();
    }
    protected void gvAttachments_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        List<Form_AttachmentAd> la = bal.GetForm_AttachmentAdByAppID(Master.ApplicationID);
        GridViewRow row = gvAttachments.Rows[e.RowIndex];
        Form_AttachmentAd fa = la[row.DataItemIndex];
        fa.Description = ((TextBox)row.FindControl("tbDescriptionEdit")).Text;
        fa.Description = Regex.Replace(fa.Description, PromotionApplication.specialCharacters, string.Empty);
        fa.Journal = ((TextBox)row.FindControl("tbJournalEdit")).Text;        
        if (((HtmlInputText)row.FindControl("tbDateOfPublicationEdit")).Value != "")
        {
            DateTime dateOfPublication;
            if(DateTime.TryParse(((HtmlInputText)row.FindControl("tbDateOfPublicationEdit")).Value, out dateOfPublication))
            {
                fa.DateOfPublication = dateOfPublication;
            }
        }
        
        fa.AttachmentCategoryID = int.Parse(((DropDownList)row.FindControl("ddlAttCat")).SelectedValue);
        fa.Points = (decimal)GetPoints(fa.AttachmentCategoryID, fa.NoOfAuthors, fa.FirstAuthor, fa.Accept);
        bal.UpdateForm_AttachmentAd(fa.Form_AttachmentAdID, fa.AttachmentCategoryID, fa.ApplicationID, fa.EmployeeID
            , fa.DocumentName, fa.DocumentSize, fa.RoleID, fa.Description, fa.SelectionForExtRev
            , fa.DateOfPublication,fa.Journal , fa.NoOfAuthors,fa.FirstAuthor,fa.Points,fa.Remarks,fa.Accept);
        gvAttachments.EditIndex = -1;
        DatabindControls();
    }
    protected void gvAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            (e.Row.FindControl("lbtnDownload") as LinkButton).Attributes.Add("target", "_blank");
        }
        catch (Exception)
        {

           
        }
       
    }
    protected void gvAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
    #endregion
    #region CheckBox Event Handler
    protected void cbSel4ExtRev_CheckedChanged(object sender, EventArgs e)
    {
        int form_AttachmentAdID = -1;
        foreach (GridViewRow row in gvAttachments.Rows)
        {
            form_AttachmentAdID = int.Parse(gvAttachments.DataKeys[row.RowIndex].Values[0].ToString());
            if(bal.GetForm_AttachmentAd(form_AttachmentAdID).Count > 0 )
            {
                Form_AttachmentAd fa = bal.GetForm_AttachmentAd(form_AttachmentAdID)[0];
                CheckBox cb = row.FindControl("cbSel4ExtRev") as CheckBox;
                fa.SelectionForExtRev = cb.Checked;
                bal.UpdateForm_AttachmentAd(fa.Form_AttachmentAdID, fa.AttachmentCategoryID,
                    fa.ApplicationID, fa.EmployeeID, fa.DocumentName, fa.DocumentSize, fa.RoleID, fa.Description, fa.SelectionForExtRev
                    , fa.DateOfPublication, fa.Journal, fa.NoOfAuthors, fa.FirstAuthor, fa.Points, fa.Remarks, fa.Accept);
            }


        }
        DatabindControls();
    }



    //protected void cbFirstAuthor_CheckedChanged(object sender, EventArgs e)
    //{
    //    int form_AttachmentID = -1;
    //    foreach (GridViewRow row in gvAttachments.Rows)
    //    {
    //        form_AttachmentID = int.Parse(gvAttachments.DataKeys[row.RowIndex].Values[0].ToString());
    //        Form_Attachment fa = bal.GetForm_Attachment(form_AttachmentID)[0];
    //        CheckBox cb = row.FindControl("cbFirstAuthor") as CheckBox;
    //        fa.FirstAuthor = cb.Checked;
    //        fa.Points = (decimal)GetPoints(fa.AttachmentCategoryID, fa.NoOfAuthors, fa.FirstAuthor, fa.Accept);
    //        bal.UpdateForm_Attachment(fa.Form_AttachmentID, fa.AttachmentCategoryID,
    //            fa.ApplicationID, fa.EmployeeID, fa.DocumentName, fa.DocumentSize, fa.RoleID, fa.Description, fa.FirstAuthor
    //            , fa.DateOfPublication, fa.Journal, fa.NoOfAuthors, fa.FirstAuthor, fa.Points, fa.Remarks, fa.Accept);

    //    }
    //    DatabindControls();
    //}
    //protected void cbAcceptable_CheckedChanged(object sender, EventArgs e)
    //{
    //    int form_AttachmentID = -1;
    //    foreach (GridViewRow row in gvAttachments.Rows)
    //    {
    //        form_AttachmentID = int.Parse(gvAttachments.DataKeys[row.RowIndex].Values[0].ToString());
    //        Form_Attachment fa = bal.GetForm_Attachment(form_AttachmentID)[0];
    //        CheckBox cb = row.FindControl("cbAccept") as CheckBox;
    //        fa.Accept = cb.Checked;
    //        fa.Points = (decimal)GetPoints(fa.AttachmentCategoryID, fa.NoOfAuthors, fa.FirstAuthor, fa.Accept);
    //        bal.UpdateForm_Attachment(fa.Form_AttachmentID, fa.AttachmentCategoryID,
    //            fa.ApplicationID, fa.EmployeeID, fa.DocumentName, fa.DocumentSize, fa.RoleID, fa.Description, fa.FirstAuthor
    //            , fa.DateOfPublication, fa.Journal, fa.NoOfAuthors, fa.FirstAuthor, fa.Points, fa.Remarks, fa.Accept);

    //    }
    //    DatabindControls();
    //}
    #endregion
    #region Link Button Event Handler 
    protected void lbtnUpload_Click(object sender, EventArgs e)
    {
        HttpFileCollection uploadedFiles = Request.Files;

        Span.Text = string.Empty;
        if(uploadedFiles.Count > 0 && uploadedFiles[0].FileName == "" || uploadedFiles.Count == 0)
        {
            labelProgrammaticPopup0.Text = "No file(s) chosen. Click on the Choose Files button to select file(s) to upload.";
            programmaticModalPopup0.Show();
            return;
        }
        if (Master.CurrentFormLevel == -1)
        {
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            return;
        }
        for (int i = 0; i < uploadedFiles.Count; i++)
        {
            UploadFile(uploadedFiles, i, int.Parse(ddlAttCatAddMulFiles.SelectedValue), uploadedFiles[i].FileName, null, "");
        }

        DatabindControls();
    }
    protected void lbtnDownload_PreRender(object sender, EventArgs e)
    {
        int rowID = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
        int faID = int.Parse(gvAttachments.DataKeys[rowID].Value.ToString());
        List<Form_AttachmentAd> ld = bal.GetForm_AttachmentAd(faID);
        if (ld.Count == 0 || ld[0].DocumentName == "")
        {
            return;
        }
        Form_AttachmentAd attachment = ld[0];
        (sender as LinkButton).OnClientClick = "window.open('"
            + "../Forms/NoMasterPage/Handler.ashx?appID=" + Master.ApplicationID
            + "&empID="
            + attachment.EmployeeID
            + "&fileName=" + attachment.DocumentName + "'); return false ;";
    }
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        if ((gvAttachments.FooterRow.FindControl("tbDescriptionAdd") as HtmlTextArea).Value == "")
        {
            labelProgrammaticPopup0.Text = "Title cannot be left empty!.";
            programmaticModalPopup0.Show();
            return;
        }
        HttpFileCollection uploadedFiles = Request.Files;
        DateTime? dateOfPublication = null;
        if (gvAttachments.FooterRow.FindControl("tbDateOfPublicationAdd") as HtmlInputGenericControl != null
            && (gvAttachments.FooterRow.FindControl("tbDateOfPublicationAdd") as HtmlInputGenericControl).Value != "")
        {
            dateOfPublication = DateTime.Parse((gvAttachments.FooterRow.FindControl("tbDateOfPublicationAdd") as HtmlInputGenericControl).Value);
        }

        Span.Text = string.Empty;
        UploadFile(uploadedFiles, 1, int.Parse((gvAttachments.FooterRow.FindControl("ddlAttCatAdd") as DropDownList).SelectedValue)
            , (gvAttachments.FooterRow.FindControl("tbDescriptionAdd") as HtmlTextArea).Value
            , dateOfPublication
            , (gvAttachments.FooterRow.FindControl("tbJournalAdd") as HtmlTextArea).Value);
        //HttpPostedFile userPostedFile = uploadedFiles[1];
        //    bal.InsertForm_Attachment((int)AttachmentCategoryID.Publications, Master.ApplicationID, Master.Employee.EmployeeID, userPostedFile.FileName
        //               , userPostedFile.ContentLength / 1000000 + " MB"
        //               , (byte?)Master.CurRoleID, (gvAttachments.FooterRow.FindControl("tbDescriptionAdd") as  HtmlTextArea).Value, false);
        DatabindControls();

    }

    protected void lbtnViewEmailFromPCMaterialOK_Click(object sender, EventArgs e)
    {
        int appID = Master.ApplicationID;
        if(string.IsNullOrEmpty(bal.GetFormMaterialFlag(appID)[0].MaterialOKEmail))
        {
            labelProgrammaticPopup0.Text = "No Email found.";
            programmaticModalPopup0.Show();
            return;
        }
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;

        EmailContent email = new EmailContent
        {
            To = TopAuthority.Email + ","
                + bal.GetDepartmentBySN(ConfigurationManager.AppSettings["TopAuthority_TitleShort"])[0].DeputyEmail,
            Body = bal.GetFormMaterialFlag(appID)[0].MaterialOKEmail,
            Subject = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Material_Ready_to_be_Sent).ToList()[0].Subject
        };
        List<EmailContent> le = new List<EmailContent>();
        le.Add(email);
        ComposeEmail1.LoadDataForPreview(le);
        ComposeEmail1.Visible = true;
      
        DatabindControls();
    }

    protected void lbtnViewEmailFromPCMaterialNotOK_Click(object sender, EventArgs e)
    {
        int appID = Master.ApplicationID;
        if (string.IsNullOrEmpty(bal.GetFormMaterialFlag(appID)[0].MaterialNotOKEmail))
        {
            labelProgrammaticPopup0.Text = "No Email found.";
            programmaticModalPopup0.Show();
            return;
        }
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
                allPCEmail = allPCEmail + pcMem.Employee.Email + ",";
            }
            else
            {
                allPCEmail = allPCEmail + pcMem.ExternalEmployee.Email + ",";
            }
        }
        allPCEmail = allPCEmail.Trim(',');
        int appID = Master.ApplicationID;
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;
        if (string.IsNullOrEmpty(bal.GetFormMaterialFlag(appID)[0].EmailFromTopAuthority))
        {
            labelProgrammaticPopup0.Text = "No Email found.";
            programmaticModalPopup0.Show();
            return;
        }
        EmailContent email = new EmailContent
        {
            To = allPCEmail,
            Body = bal.GetFormMaterialFlag(appID)[0].EmailFromTopAuthority,
            Subject = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Material_Updated).ToList()[0].Subject

        };
        List<EmailContent> le = new List<EmailContent>();
        le.Add(email);
        ComposeEmail1.LoadDataForPreview(le);
        ComposeEmail1.Visible = true;
       
        DatabindControls();
    }
    #endregion
    protected void rbltMaterialCheck_SelectedIndexChanged(object sender, EventArgs e)
    {
        int appID = Master.ApplicationID;
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;
        Employee Applicant = bal.GetApplicant(appID)[0];
        Employee PCChair = bal.GetForm_FinalPC(Master.ApplicationID).Where(a => a.Position == (int)PCPosition.Chairman).ToList()[0].Employee;
        if (rbltMaterialCheck.SelectedValue == "MaterialOK")
        {
            Task_ExtMessages tem = bal.GetExtTaskMessage().Where(a => a.Task_Ext.Title == TaskExtID.Material_Ready_to_be_Sent.ToString()).ToList()[0];
            string MessageBody = tem.Message;
            MessageBody = MessageBody
                .Replace("@@Applicant@@", Applicant.NameString)
                .Replace("@@ForRank@@", bal.GetApplication(appID)[0].ForRank)
                .Replace("@@ApplicantDept@@", Applicant.Department)
                .Replace("@@RecipientName@@", TopAuthority.NameString)
                .Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"])
                .Replace("@@Sender@@", PCChair.NameString)
                .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
            ;
            EmailContent email = new EmailContent
            {
                To = bal.GetDepartmentBySN(ConfigurationManager.AppSettings["TopAuthority_TitleShort"])[0].DeputyEmail,
                Body = MessageBody,
                Subject = tem.Subject
            };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadData(le);
            ComposeEmail1.Visible = true;
        }
        else
        {

            Task_ExtMessages tem = bal.GetExtTaskMessage().Where(a => a.Task_Ext.Title == TaskExtID.Material_Not_Ready_to_be_Sent.ToString()).ToList()[0];
            string MessageBody = tem.Message;
            MessageBody = MessageBody
                .Replace("@@Applicant@@", Applicant.NameString)
                .Replace("@@ForRank@@", bal.GetApplication(appID)[0].ForRank)
                .Replace("@@ApplicantDept@@", Applicant.Department)
                .Replace("@@RecipientName@@", TopAuthority.NameString)
                .Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"])
                .Replace("@@Sender@@", PCChair.NameString)
                .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
            ;
            EmailContent email = new EmailContent
            {
                To = bal.GetDepartmentBySN(ConfigurationManager.AppSettings["TopAuthority_TitleShort"])[0].DeputyEmail,
                Body = MessageBody,
                Subject = tem.Subject
            };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadData(le);
            ComposeEmail1.Visible = true;
        }

    }
    protected void tbNoOfAuthors_TextChanged(object sender, EventArgs e)
    {
        
        int form_AttachmentAdID = -1;
        foreach (GridViewRow row in gvAttachments.Rows)
        {
            form_AttachmentAdID = int.Parse(gvAttachments.DataKeys[row.RowIndex].Values[0].ToString());
            Form_AttachmentAd fa = bal.GetForm_AttachmentAd(form_AttachmentAdID)[0];
            TextBox tb = row.FindControl("tbNoOfAuthors") as TextBox;
            fa.NoOfAuthors = int.Parse(tb.Text) > 0 ? int.Parse(tb.Text) : fa.NoOfAuthors;
            fa.Points = (decimal) GetPoints(fa.AttachmentCategoryID, fa.NoOfAuthors, fa.FirstAuthor, fa.Accept);
            bal.UpdateForm_AttachmentAd(fa.Form_AttachmentAdID, fa.AttachmentCategoryID,
                fa.ApplicationID, fa.EmployeeID, fa.DocumentName, fa.DocumentSize, fa.RoleID, fa.Description, fa.SelectionForExtRev
                , fa.DateOfPublication, fa.Journal , fa.NoOfAuthors, fa.FirstAuthor, fa.Points, fa.Remarks, fa.Accept);

        }
        DatabindControls();
    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }   
    #endregion
    #region Get Methods
    private double GetPoints(int? attachmentCategoryID, int noOfAuthors, bool firstAuthor, bool Accept)
    {
        if(!Accept)
        {
            return 0;
        }
        //if (attachmentCategoryID == (int)AttachmentCategoryID.Original_Article && Accept)
        //{
            if (noOfAuthors == 1)
            {
                return 1.0;
            }
            else if (noOfAuthors == 2)
            {
                return 0.5;
            }
            else if (noOfAuthors > 2)
            {        
                if (firstAuthor)
                {
                    return 0.5;
                }
                else
                {
                    return 0.25;
                }
            }
            else
            {
                return 0;
            }
        //}
        //else if (attachmentCategoryID == (int)AttachmentCategoryID.Conference_Paper && Accept)
        //{
        //    return 1;
        //}
        //else if (attachmentCategoryID == (int)AttachmentCategoryID.Case_Report && Accept)
        //{
        //    return 1;
        //}
        //else
        //{
        //    return 0;
        //}
    }
    public bool GetVisibleStatus(string AttachmentID)
    {

        return bal.GetForm_AttachmentAd(int.Parse(AttachmentID))[0].SelectionForExtRev.Value;
    }
    public bool GetCheckStatus(string AttachmentID)
    {
        return bal.GetForm_AttachmentAd(int.Parse(AttachmentID))[0].SelectionForExtRev.Value;
    }

    protected string GetEmployeeName()
    {
        return bal.GetEmployeeByPK(Eval("EmployeeID").ToString())[0].NameString;
    }
    protected string GetTotalPoints()
    {
        return bal.GetForm_AttachmentAdByAppID(Master.ApplicationID).Select(p => p.Points).Sum().ToString();
    }
    public bool GetMaterialOKStatus()
    {
        if (bal.GetFormMaterialFlag(Master.ApplicationID).Count == 0)
        {
            bal.InsertFormMaterialFlag(Master.ApplicationID, null, "", "", "");
        }
        if (bal.GetFormMaterialFlag(Master.ApplicationID)[0].PCMaterialReady4ExtRevFlag.HasValue)
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

    #endregion
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
    public string DeleteDecision
    {
        set
        {
            ViewState["DeleteDecision"] = value;            
        }
        get
        {
            if (ViewState["DeleteDecision"] != null)
            {
                return ViewState["DeleteDecision"].ToString();
            }
            else
            {
                return "";
            }
        }
    }
    #endregion
    private void UploadFile(HttpFileCollection uploadedFiles, int i, int attCat, string description, DateTime? DateOfPublication, string Journal)
    {
        HttpPostedFile userPostedFile = uploadedFiles[i];
        try
        {
            if (userPostedFile.ContentLength > 0)
            {
                var regexItem = new Regex("^[\\w,\\s-]+\\.[A-Za-z]{3}$");
                if (!regexItem.IsMatch(userPostedFile.FileName))
                {
                    labelProgrammaticPopup0.Text = "The file name of the uploaded file is unacceptable. No file is uploaded. File name can only have characters like alphabets from A-Z, digits from 0-9 and _ .";
                    programmaticModalPopup0.Show();
                    return;
                }
                if (userPostedFile.ContentType != "application/pdf")
                {
                    labelProgrammaticPopup0.Text = "You can only upload pdf files.";
                    programmaticModalPopup0.Show();

                    return;
                }
                if(userPostedFile.ContentLength > 4000000)
                {
                    labelProgrammaticPopup0.Text = "You can only upload files of size less than 4 MB.";
                    programmaticModalPopup0.Show();

                    return;
                }                
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
                   , userPostedFile.ContentLength > 1000000.0 ? userPostedFile.ContentLength / 1000000.0 + " MB" : userPostedFile.ContentLength / 1000.0 + " KB"
                   , Master.CurRoleID, description, false
                   , DateOfPublication, Journal, 1, true,0, "", true);
                }
                userPostedFile.SaveAs(dataPath + Master.ApplicationID + "_" + Master.Employee.EmployeeID + "_" + userPostedFile.FileName);
            }
            //else if (description != "")
            //{
            //    bal.InsertForm_Attachment(attCat, Master.ApplicationID, Master.Employee.EmployeeID, ""
            //   , userPostedFile.ContentLength > 1000000.0 ? userPostedFile.ContentLength / 1000000.0 + " MB" : userPostedFile.ContentLength / 1000.0 + " KB"
            //   ,(byte?)Master.CurRoleID, description, false
            //   , DateOfPublication, Journal, 1, true, (decimal)GetPoints(attCat, 1, true, true), "", true);

            //}
        }
        catch (Exception Ex)
        {
            Span.Text += "Error: <br>" + Ex.Message;
        }
    }
    private void SwitchMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.InputByRole:
                //UserMode = "";
                
                break;
            case Mode.ViewRole:
                //UserMode = "";
                
                gvAttachments.Columns[1].Visible = false;                
                divUploadMultipleFiles.Style["Display"] = "none";
                gvAttachments.ShowFooter = false;

                break;
            case Mode.PCChair:
                gvAttachments.Columns[1].Visible = false;
                divUploadMultipleFiles.Style["Display"] = "none";
                gvAttachments.ShowFooter = false;
                if (bal.GetFormMaterialFlag(Master.ApplicationID)[0].PCMaterialReady4ExtRevFlag.HasValue)
                {
                    if(bal.GetFormMaterialFlag(Master.ApplicationID)[0].PCMaterialReady4ExtRevFlag.Value)
                    {
                        ShowMessage("Material is ready to be sent to External Reviewers");
                    }
                    else
                    {
                        ShowError("Material is not ready to be sent to External Reviewers");
                    }
                }
                break;

        }
        gvAttachments.DataBind();
    }
    private void DatabindControls()
    {
        divFileUpload.DataBind();
        if (rbltMaterialCheck.SelectedValue != "")
        {
            rbltMaterialCheck.SelectedItem.Selected = false;
        } 
        divMaterialCheck.DataBind();
        divMaterialOKStatus.DataBind();
        ddlAttCatAddMulFiles.DataBind();
        if (bal.GetForm_AttachmentAdByAppID(Master.ApplicationID).Count == 0 )
        {
            List<Form_AttachmentAd> lfa = new List<Form_AttachmentAd>();
            lfa.Add(new Form_AttachmentAd
            {
                EmployeeID = "-1",
                RoleID = (int) RoleID.Applicant,
                AttachmentCategoryID =  (int)AttachmentCategoryID.Publication,
                SelectionForExtRev = false,
                DocumentName = ""
            });
            gvAttachments.DataSource = lfa;
            gvAttachments.DataBind();
            gvAttachments.Rows[0].FindControl("divEditDelDownload").Visible = false;
            gvAttachments.Rows[0].FindControl("lblSerialNo").Visible = false;
            gvAttachments.Rows[0].FindControl("lblAttachmentCategory").Visible = false;
            (gvAttachments.Rows[0].FindControl("cbSel4ExtRev") as CheckBox).Enabled = false;
            gvAttachments.Rows[0].Enabled = false;
        }
        else
        {
            if(bal.GetForm_AttachmentAdByAppID(Master.ApplicationID)
                .Where(fa=>fa.AttachmentCategoryID == (int) AttachmentCategoryID.Publication
                 && fa.SelectionForExtRev.Value).ToList().Count < 2)
            {
                ShowError("Atleast two publications has to be uploaded and selected for external reviewers.");
            }
            else if (bal.GetForm_AttachmentAdByAppID(Master.ApplicationID)
              .Where(fa => fa.AttachmentCategoryID == (int)AttachmentCategoryID.CV
               && fa.SelectionForExtRev.Value).ToList().Count == 0)
            {
                ShowError("CV need to be uploaded and selected for external reviewers.");
            }else if(Master.CurRoleID == (byte) RoleID.TopAuthority &&!bal.GetFormMaterialFlag(Master.ApplicationID)[0].PCMaterialReady4ExtRevFlag.HasValue 
                || (Master.CurRoleID == (byte)RoleID.TopAuthority && bal.GetFormMaterialFlag(Master.ApplicationID)[0].PCMaterialReady4ExtRevFlag.HasValue
                &&!bal.GetFormMaterialFlag(Master.ApplicationID)[0].PCMaterialReady4ExtRevFlag.Value))
            {
                ShowError("Promotion Committee has not yet reviewed the material. The Promotion Committee consent of Material OK is necessary");
            }
            else
            {
                if (Master.CurRoleID == (byte)RoleID.Promotion_Committee_Chairman && 
                    (bal.GetFormMaterialFlag(Master.ApplicationID).Count == 0 || bal.GetFormMaterialFlag(Master.ApplicationID)[0].PCMaterialReady4ExtRevFlag == null))
                {
                    ShowError("Review of the CV and publications by Promotion Committee not complete");
                }
                else
                {
                    ShowMessage("OK!");
                }
                
            }
            List<byte> leri = bal.GetNameExclusion(Master.CurRoleID).Select(ne => ne.ExcludedRoleID).ToList();
            gvAttachments.DataSource = bal.GetForm_AttachmentAdByAppID(Master.ApplicationID)
                .Where(at => !leri.Contains(at.RoleID.Value) && at.AttachmentCategoryID != (int) AttachmentCategoryID.PCReport)
                .OrderByDescending(fa=>fa.DateOfPublication)
                .ToList();
            gvAttachments.DataBind();
        }
        SwitchMode((Mode) Master.CurrentFormLevel);

        divTopOfFormMsgFailure.DataBind();
        lblMessageFailure.DataBind();
        divTopOfFormMsgSuccess.DataBind();
        lblMessageSuccess.DataBind();        
    }

    private void ShowError(string message)
    {
        
        Master.ReportFailure(message);
    }

    private void ShowMessage(string message)
    {
       
        Master.ReportSuccess();
    }
    
    #region Compose Email
    protected void ComposeEmail_OnEmailSent(object sender, EventArgs e)
    {
        if (bal.GetFormMaterialFlag(Master.ApplicationID).Count == 0)
        {
            bal.InsertFormMaterialFlag(Master.ApplicationID, null, "", "","");
        }
        bool materialOK = rbltMaterialCheck.SelectedValue == "MaterialOK";
        Form_MaterialFlag fmf = bal.GetFormMaterialFlag(Master.ApplicationID)[0];
        if (materialOK)
        {
            
            bal.UpdateFormMaterialFlag(Master.ApplicationID, materialOK, ComposeEmail1.EmailBody, "", fmf.EmailFromTopAuthority);           
        }
        else
        {
            bal.UpdateFormMaterialFlag(Master.ApplicationID, materialOK, "", ComposeEmail1.EmailBody, fmf.EmailFromTopAuthority);            
        }
        ComposeEmail1.Visible = false;
        DatabindControls();
    }
    protected void ComposeEmail_OnEmailNotSent(object sender, EventArgs e)
    {
        ComposeEmail1.Visible = false;        
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
