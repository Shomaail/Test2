using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Drawing;
using BL.Data;

public partial class Forms_FormExtRev : System.Web.UI.Page
{
    ExtRevBAL erBAL = new ExtRevBAL();
    BAL bal = new BAL();
    private enum Mode { InputByCandidate = 1, ViewApp = 2, ViewAppInputByDept = 3, ViewAppDept = 4, ViewAppDeptInputCollege = 5, ViewAppDeptCollege = 6 }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (IsPostBack)
        {
            if (!(Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("ddlQuickJump")))
            {
                return;
            }
        }
        
        SwitchMode((Mode)Master.CurrentFormLevel);

        try
        {
            DatabindControls();
        }
        catch (Exception exp)
        {
            Emailer.Send("facpromote@kfupm.edu.sa", "Special Error", exp.Message, "", Master.ApplicationID);
            labelProgrammaticPopup0.Text = "An error has occurred. An email has been sent to the Administrator. Sorry for the inconvenience";
            programmaticModalPopup0.Show();
            //Response.Redirect("~/Login.aspx");
        }
    }
    #region Properties
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
    //public int ExternalReviewerID
    //{
    //    get
    //    {
    //        if (hfExtRevID.Value.Length == 0)
    //        {
    //            return -1;
    //        }
    //        return int.Parse(hfExtRevID.Value);
    //    }
    //    set
    //    {
    //        hfExtRevID.Value = value.ToString();
    //    }
    //}
    public int Serial
    {
        get
        {
            if (hfSerial.Value.Length == 0)
                return 0;
            return int.Parse(hfSerial.Value);
        }
        set
        {
            hfSerial.Value = value.ToString();
        }
    }
    public int Type
    {
        get
        {
            if (hfType.Value.Length == 0)
            {
                return 0;
            }
            return int.Parse(hfType.Value);
        }
        set
        {
            hfType.Value = value.ToString();
        }
    }

    #endregion
    private void DatabindControls()
    {
        //if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, (byte) FormID.FormExtRev_aspx, Master.SubTaskID, false).Count > 0)
        //{
        //    if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, (byte)FormID.FormExtRev_aspx, Master.SubTaskID, false)[0].Completed)
        //    {
        //        lblMessage.ForeColor = Color.Green;
        //    }
        //    else
        //    {
        //        lblMessage.ForeColor = (Color)ColorTranslator.FromHtml("#CC3300");
        //    }
        //    lblMessage.Text = bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, (byte)FormID.FormExtRev_aspx, Master.SubTaskID, false)[0].Message;
        //}
        //lblMessage.DataBind();
        List<Form_ExtRev> lFER = erBAL.GetForm_ExtRev(Master.ApplicationID, Type);
        if (Type == 0)
        {
            if (lFER.Count == 5)
            {
                ShowMessage("List of External Reviewers is complete with 5 entries");
            }
            else
            {
                ShowError("List of External Reviewers is Incomplete. Must have 5 entries.  As per the directions of the Vice President of Research and Innovation as indicated in his earlier circular, please verify from Scopus source only, the Total Publications, Number of journal publications, H-index and Citations of the reviewers. No personal association or co-authorship with the applicant should be ascertained from CV of the Applicant");
            }

        }
        if (Master.CurRoleID == (int)RoleID.Departmental_Committee_Chairman ||
            Master.CurRoleID == (int)RoleID.Departmental_Committee_Member_1 ||
            Master.CurRoleID == (int)RoleID.Departmental_Committee_Member_2 ||
            Master.CurRoleID == (int)RoleID.Departmental_Committee_Member_3 ||
            Master.CurRoleID == (int)RoleID.Departmental_Committee_Member_4
            )
        {
            if (lFER.Count == 15)
            {
                ShowMessage("List of External Reviewers is complete with 15 entries");
            }
            else
            {
                ShowError("List of External Reviewers is Incomplete. Must have 15 entries.  As per the directions of the Vice President of Research and Innovation as indicated in his earlier circular, please verify from Scopus source only, the Total Publications, Number of journal publications, H-index and Citations of the reviewers. No personal association or co-authorship with the applicant should be ascertained from CV of the Applicant");
            }

        }
        if (Master.CurRoleID == (int)RoleID.College_Dean)
        {
            if (lFER.Count == 10)
            {
                ShowMessage("List of External Reviewers is complete with 10 entries");
            }
            else
            {
                ShowError("List of External Reviewers is Incomplete. Must have 10 entries.  As per the directions of the Vice President of Research and Innovation as indicated in his earlier circular, please verify from Scopus source only, the Total Publications, Number of journal publications, H-index and Citations of the reviewers. No personal association or co-authorship with the applicant should be ascertained from CV of the Applicant");
            }
        }
        if (Master.CurRoleID == (int)RoleID.DepartmentChairman)
        {
            List<Application_TaskLog> latl = bal.GetAppTaskLog(Master.ApplicationID)
                    .Where(a => a.TaskID == (int)TaskIDs.Department_Approval &&
                    (a.ActionID == (int)ActionID.Verify_and_Return_to_the_Department_Chairman
                    || a.ActionID == (int)ActionID.Not_Recommended_and_Return_to_Department)).ToList();
            if (latl.Count > 0
               && latl.Last().ActionID == (int)ActionID.Verify_and_Return_to_the_Department_Chairman)
            {
                ShowMessage("The List of External Reviewer is complete");
            }
            else
            {
                ShowError("The Departmental Committee did not approve.");
            }
        }
        divFormExtRevCandidate.DataBind();
        btnAddFormExtRevCand.DataBind();
        divFormExtRevDepartment.DataBind();
        btnAddFormExtRevDept.DataBind();
        divFormExtRevCollege.DataBind();
        btnAddFormExtRevColl.DataBind();
        divInsertFormExtRev.DataBind();

        divTopOfFormMsgFailure.DataBind();
        lblMessageFailure.DataBind();
        divTopOfFormMsgSuccess.DataBind();
        lblMessageSuccess.DataBind();
    }
    private void ShowError(string message)
    {
        //lblMessage.ForeColor = ColorTranslator.FromHtml("#CC3300");
        //lblMessage.Text = message;
        Master.ReportFailure(message);
    }

    private void ShowMessage(string message)
    {
        //lblMessage.ForeColor = Color.Green;
        //lblMessage.Text = message;
        Master.ReportSuccess();
    }
    private void SwitchMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.InputByCandidate:
               
                LoadExternalReviewers();
                FormMode = "ViewApplicant";
                Type = (byte)ViewType.Candidate;
                //divFormExtRevCandidate.Visible = true;
                //divFormExtRevDepartment.Visible = false;
                //divFormExtRevCollege.Visible = false;
                break;
            case Mode.ViewAppInputByDept:
                Type = (byte)ViewType.Department;
                LoadExternalReviewers();
                FormMode = "ViewDepartmentCommittee";
                gvExternalReviewersCand.Columns[1].Visible = false;
                gvExternalReviewersCand.Columns[2].Visible = false;
                gvExternalReviewersCand.Columns[3].Visible = false;
                gvExternalReviewersDept.Columns[0].Visible = false;
                break;
            case Mode.ViewApp:
                Type = (byte)ViewType.Candidate;
                gvExternalReviewersCand.Columns[1].Visible = false;
                gvExternalReviewersCand.Columns[2].Visible = false;
                gvExternalReviewersCand.Columns[3].Visible = false;
               
                break;
            case Mode.ViewAppDept:
                Type = (byte)ViewType.Candidate;
                LoadExternalReviewers();
                FormMode = "ViewDepartment";
                gvExternalReviewersCand.Columns[1].Visible = false;
                gvExternalReviewersCand.Columns[2].Visible = false;
                gvExternalReviewersCand.Columns[3].Visible = false;
                gvExternalReviewersDept.Columns[1].Visible = false;
                gvExternalReviewersDept.Columns[2].Visible = false;
                gvExternalReviewersDept.Columns[3].Visible = false;
                break;
            case Mode.ViewAppDeptInputCollege:
                Type = (byte)ViewType.College;
                LoadExternalReviewers();
                FormMode = "ViewCollege";
                gvExternalReviewersCand.Columns[1].Visible = false;
                gvExternalReviewersCand.Columns[2].Visible = false;
                gvExternalReviewersCand.Columns[3].Visible = false;
                gvExternalReviewersDept.Columns[1].Visible = false;
                gvExternalReviewersDept.Columns[2].Visible = false;
                gvExternalReviewersDept.Columns[3].Visible = false;
                gvExternalReviewersColl.Columns[0].Visible = false;
               
                break;

            case Mode.ViewAppDeptCollege:
                Type = (byte)ViewType.College;
                LoadExternalReviewers();
                FormMode = "View";
                gvExternalReviewersCand.Columns[1].Visible = false;
                gvExternalReviewersCand.Columns[2].Visible = false;
                gvExternalReviewersCand.Columns[3].Visible = false;
                gvExternalReviewersDept.Columns[1].Visible = false;
                gvExternalReviewersDept.Columns[2].Visible = false;
                gvExternalReviewersDept.Columns[3].Visible = false;
                gvExternalReviewersColl.Columns[1].Visible = false;
                gvExternalReviewersColl.Columns[2].Visible = false;
                gvExternalReviewersColl.Columns[3].Visible = false;
                break;
        }
    }
    private void PopulateAddEditForm(int erID)
    {
        taExtRev.InnerText = "";
        List<ExternalReviewer> lER = erBAL.GetExtRevByID(erID);
        ExternalReviewer er = lER[0];
        ExternalReviewerID = er.ExternalReviewerID;
        tbName.Text = er.Name;
        //ddlRank.SelectedItem.Selected = false;
        //if (er.Rank != null && er.Rank != "")
        //{
        //    if (ddlRank.Items.FindByText(er.Rank) != null)
        //    {
        //        ddlRank.Items.FindByText(er.Rank).Selected = true;
        //    }

        //}
        if (er.Rank != null && er.Rank != "")
        {
            if (ddlRank.Items.FindByText(er.Rank) != null)
            {
                ddlRank.SelectedItem.Selected = false;
                ddlRank.Items.FindByText(er.Rank).Selected = true;
            }
            else
            {

                tbOthers.Text = er.Rank;
            }
        }
        if (ddlRank.SelectedValue == "Others")
        {
            tbOthers.Visible = true;
            tbOthers.Text = er.Rank;
        }
        else
        {
            tbOthers.Visible = false;
            tbOthers.Text = "";
        }
        tbPhoneAndFax.Text = er.PhoneAndFax;
        tbMailingAddress.Text = er.MailingAddress;
        tbMajor.Text = er.Major;
        tbSpecialty.Text = er.Specialty;
        tbActiveAreaOfResearch.Text = er.ActiveAreaOfResearch;
        tbWebpage.Text = er.Webpage;
        tbEmail.Text = er.Email;
        //tbTotPublications.Text = er.TotalPublications.HasValue ? er.TotalPublications.Value.ToString() : "0";
        tbTotPublications.Value= er.TotalPublications.HasValue ? er.TotalPublications.Value.ToString() : "0";
        tbNoOfJournals.Value = er.NoOfJournals.HasValue ? er.NoOfJournals.Value.ToString() : "0";
        tbHIndex.Value = er.HIndex.HasValue ? er.HIndex.Value.ToString() : "0";
        tbCitations.Value = er.Citations.HasValue ? er.Citations.Value.ToString() : "0";

        tbEmail.BorderColor = Color.Gray;
        tbPhoneAndFax.BorderColor = Color.Gray;
        tbMailingAddress.BorderColor = Color.Gray;
        tbMajor.BorderColor = Color.Gray;
        tbSpecialty.BorderColor = Color.Gray;
        tbActiveAreaOfResearch.BorderColor = Color.Gray;
        tbWebpage.BorderColor = Color.Gray;
        tbTotPublications.Style["border-color"] = Color.Gray.Name;
        tbNoOfJournals.Style["border-color"] = Color.Gray.Name;
        tbHIndex.Style["border-color"] = Color.Gray.Name;
        tbCitations.Style["border-color"] = Color.Gray.Name;
    }
    private void PopulateAddEditForm(int appID, byte type, byte serial)
    {
        tbEmail.BackColor = Color.White;
        List<Form_ExtRev> lER = erBAL.GetFormExtRevByPK(appID, type, serial);
        Form_ExtRev fer = lER[0];        
        tbName.Text = fer.ExternalReviewer.Name;
        ddlRank.SelectedItem.Selected = false;
        ddlRank.Items.FindByText(fer.ExternalReviewer.Rank).Selected = true;
        tbPhoneAndFax.Text = fer.ExternalReviewer.PhoneAndFax;
        tbMailingAddress.Text = fer.ExternalReviewer.MailingAddress;
        tbMajor.Text = fer.ExternalReviewer.Major;
        tbSpecialty.Text = fer.ExternalReviewer.Specialty;
        tbActiveAreaOfResearch.Text = fer.ExternalReviewer.ActiveAreaOfResearch;
        tbWebpage.Text = fer.ExternalReviewer.Webpage;
        tbEmail.Text = fer.ExternalReviewer.Email;
        //tbTotPublications.Text = fer.ExternalReviewer.TotalPublications.HasValue ? fer.ExternalReviewer.TotalPublications.Value.ToString() : "0";
        tbTotPublications.Value= fer.ExternalReviewer.TotalPublications.HasValue ? fer.ExternalReviewer.TotalPublications.Value.ToString() : "0";
        tbNoOfJournals.Value = fer.ExternalReviewer.NoOfJournals.HasValue ? fer.ExternalReviewer.NoOfJournals.Value.ToString() : "0";
        tbHIndex.Value = fer.ExternalReviewer.HIndex.HasValue ? fer.ExternalReviewer.HIndex.Value.ToString() : "0";
        tbCitations.Value = fer.ExternalReviewer.Citations.HasValue ? fer.ExternalReviewer.Citations.Value.ToString() : "0";
       
    }
    private void PopulateAddEditForm()
    {
        taExtRev.InnerText = "";
        tbName.Text = "";
        ddlRank.SelectedItem.Selected = false;
        ddlRank.Items.FindByText("Professor").Selected = true;
        tbPhoneAndFax.Text = "";
        tbMailingAddress.Text = "";
        tbMajor.Text = "";
        tbSpecialty.Text = "";
        tbActiveAreaOfResearch.Text = "";
        tbWebpage.Text = "";
        tbEmail.Text = "";
        //tbTotPublications.Text = "0";
        tbTotPublications.Value = "0";
        tbNoOfJournals.Value = "0";
        tbHIndex.Value = "0";
        tbCitations.Value = "0";


        tbName.BorderColor = Color.Gray;  
        tbPhoneAndFax.BorderColor  = Color.Gray;
        tbMailingAddress.BorderColor  = Color.Gray;
        tbMajor.BorderColor  = Color.Gray;
        tbSpecialty.BorderColor  = Color.Gray;
        tbActiveAreaOfResearch.BorderColor  = Color.Gray;
        tbWebpage.BorderColor  = Color.Gray;
        tbEmail.BorderColor  = Color.Gray;
        //tbTotPublications.BorderColor  = Color.Gray;
        tbTotPublications.Style["border-color"] = Color.Gray.Name;
        tbNoOfJournals.Style["border-color"] = Color.Gray.Name;
        tbHIndex.Style["border-color"] = Color.Gray.Name;
        tbCitations.Style["border-color"] = Color.Gray.Name;

    }
    public void LoadExternalReviewers()
    {
        odsFormExtRevCand.SelectParameters["Type"].DefaultValue = ((byte)ViewType.Candidate).ToString();
        gvExternalReviewersCand.DataBind();
        divTableOutputApplicantList.DataBind();
        divTableOutputApplicantList4Other.DataBind();
        rptExternalReviewersApplicant.DataSource = erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, (int)ViewType.Candidate);
        rptExternalReviewersApplicant.DataBind();
        odsFormExtRevDept.SelectParameters["Type"].DefaultValue = ((byte)ViewType.Department).ToString();
        gvExternalReviewersDept.DataBind();
        divTableOutputDeptList.DataBind();
        divTableOutputDeptList4Other.DataBind();
        rptExternalReviewersDept.DataSource = erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, (int)ViewType.Department);
        rptExternalReviewersDept.DataBind();
        odsFormExtRevColl.SelectParameters["Type"].DefaultValue = ((byte)ViewType.College).ToString();
        gvExternalReviewersColl.DataBind();
        divTableOutputCollegeList.DataBind();
        divTableOutputCollegeList4Other.DataBind();
        rptExternalReviewersCollege.DataSource = erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, (int)ViewType.College);
        rptExternalReviewersCollege.DataBind();
    }
    private void ShowHideArrowButton(GridViewRowEventArgs e, byte viewType)
    {
        int totalRows = erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, viewType).Count;
        if (totalRows == 1 && e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("iBtnArrowUp") as ImageButton).Visible = false;
            (e.Row.FindControl("iBtnArrowDn") as ImageButton).Visible = false;
        }
        if (e.Row.RowIndex == 0 && e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("iBtnArrowUp") as ImageButton).Visible = false;
        }
        if (e.Row.RowIndex == totalRows - 1 && e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("iBtnArrowDn") as ImageButton).Visible = false;
        }
    }
    #region Event Handlers
    #region Gridview
    #region GridviewCandidate
    protected void gvExternalReviewersCand_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Type = (int)ViewType.Candidate;
        Serial = int.Parse(gvExternalReviewersCand.DataKeys[e.NewEditIndex].Values[2].ToString());
        int erID = erBAL.GetFormExtRevByPK(Master.ApplicationID, Type, Serial)[0].ExternalReviewerID;
        //   PopulateAddEditForm(Master.ApplicationID,Type,Serial);
        PopulateAddEditForm(erID);
        FormMode = "Edit";
        DatabindControls();
        e.Cancel = true;
    }
    protected void gvExternalReviewersCand_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReorderRows(e, (byte)ViewType.Candidate);
        gvExternalReviewersCand.DataBind();
    }
    protected void gvExternalReviewersCand_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ShowHideArrowButton(e, (byte)ViewType.Candidate);
    }
    #endregion
    #region GridviewDept
    protected void gvExternalReviewersDept_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Type = (int)ViewType.Department;
        Serial = int.Parse(gvExternalReviewersDept.DataKeys[e.NewEditIndex].Values[2].ToString());
        int erID = erBAL.GetFormExtRevByPK(Master.ApplicationID, Type, Serial)[0].ExternalReviewerID;
        PopulateAddEditForm(erID);
        FormMode = "Edit";
        DatabindControls();
        e.Cancel = true;
    }
    protected void gvExternalReviewersDept_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReorderRows(e, (byte)ViewType.Department);
        gvExternalReviewersDept.DataBind();
    }
    protected void gvExternalReviewersDept_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ShowHideArrowButton(e, (byte)ViewType.Department);
    }

    #endregion
    #region GridviewCollege
    protected void gvExternalReviewersColl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Type = (int)ViewType.College;
        Serial = int.Parse(gvExternalReviewersColl.DataKeys[e.NewEditIndex].Values[2].ToString());
        int erID = erBAL.GetFormExtRevByPK(Master.ApplicationID, Type, Serial)[0].ExternalReviewerID;
        PopulateAddEditForm(erID);
        FormMode = "Edit";
        DatabindControls();
        e.Cancel = true;
    }
    protected void gvExternalReviewersColl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ReorderRows(e, (byte)ViewType.College);
        gvExternalReviewersColl.DataBind();
    }
    protected void gvExternalReviewersColl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ShowHideArrowButton(e, (byte)ViewType.College);
    }
    #endregion
    #region  gvExtRevSearchResult
    //protected void gvExtRevSearchResult_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Serial = int.Parse(gvExtRevSearchResult.DataKeys[gvExtRevSearchResult.SelectedIndex].Values[2].ToString());
    //    Type = (int)ViewType.Candidate;
    //    PopulateAddEditForm(erID);
    //}
    protected void gvExtRevSearchResult_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        ExternalReviewerID = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "Select")
        {
            PopulateAddEditForm(ExternalReviewerID);
        }
    }
    #endregion
    #region  gvOldExtRevList
    //protected void gvOldExtRevList_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Select")
    //    {
    //        ExternalReviewerID = Convert.ToInt32(e.CommandArgument.ToString());
    //        List<Form_ExtRev1> lFER = erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, Type);

    //        if (lFER.Where(er => er.Email.Equals(tbEmail.Text)).Count() > 0)
    //        {
    //            //Already present in your List 
    //            labelProgrammaticPopup0.Text = "This External Reviewers with the Email Address: (" + tbEmail.Text + ") is already present in the External Reviewers list.";
    //            this.programmaticModalPopup0.Show();
    //            return;
    //        }
    //            erBAL.InsertFormExtRev(
    //                    Master.ApplicationID,
    //                    Type,
    //                    erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, Type).Count,
    //                    ExternalReviewerID
    //                    );

    //        if (lFER.Count == 9)
    //        {
    //            Master.ReportSuccess("List of External Reviewers is complete");
    //        }
    //        else
    //        {
    //            lblMessage.Text = "List of 10 External Reviewers is incomplete";
    //            Master.ReportFailure("List of 10 External Reviewers is incomplete");
    //        }
    //        ExternalReviewerID = -1;
    //        FormMode = "View";
    //        SwitchMode((Mode)Master.CurrentFormLevel);
    //        DatabindControls();
    //    }

    //}

    #endregion
    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRank.SelectedValue == "Others")
        {
            tbOthers.Visible = true;
        }
        else
        {
            tbOthers.Visible = false;
        }
      //  UpdatePanel1.Update();
    }
    #endregion
    #region Button Event Handlers
    protected void btnFillTB_Click(object sender, EventArgs e)
    {
        //string[] erData = taExtRev.InnerText.Split('\t', '\r', '\n');
        string[] erData = taExtRev.InnerText.Split('\t');
        if (erData.Length >= 1)
        {
            tbName.Text = erData[0];
        }
        if (erData.Length >= 2)
        {            
            if(ddlRank.Items.FindByText(erData[1]) != null)
            {
                ddlRank.SelectedItem.Selected = false;
                ddlRank.Items.FindByText(erData[1]).Selected = true;
            }
            

        }
        if (erData.Length >= 3)
        {
            tbMailingAddress.Text = erData[2];
        }
        if (erData.Length >= 4)
        {
            tbPhoneAndFax.Text = erData[3];

        }
        if (erData.Length >= 5)
        {
            tbEmail.Text = erData[4].Trim();

        }
        if (erData.Length >= 6)
        {
            tbMajor.Text = erData[5];

        }
        if (erData.Length >= 7)
        {
            tbSpecialty.Text = erData[6];
            //tbActiveAreaOfResearch.Text = erData[7];
            //tbWebpage.Text = erData[8];

            //tbTotPublications.Text = erData[9];
            //tbTotPublications.Value= erData[9];
            //tbNoOfJournals.Value = erData[10];
            //tbHIndex.Value = erData[11];
            //tbCitations.Value = erData[12];
        }
        if (erData.Length >= 8)
        {
            tbActiveAreaOfResearch.Text = erData[7];
        }
        if (erData.Length >= 9)
        {
            tbWebpage.Text = erData[8];

        }
        if (erData.Length >= 10)
        {
            // tbTotPublications.Text = erData[9];
            tbTotPublications.Value= erData[9];

        }
        if (erData.Length >= 11)
        {
            tbNoOfJournals.Value = erData[10];
        }
        if (erData.Length >= 12)
        {
            tbHIndex.Value = erData[11];
        }
        if (erData.Length >= 13)
        {
            tbCitations.Value = erData[12];
        }
        if (erData.Length >= 14)
        {
        }
        if (erData.Length >= 15)
        {
        }
        if (erData.Length >= 16)
        {
        }
        if (erData.Length >= 17)
        {
        }
    }
    protected void btnShowDivExcel_Click(object sender, EventArgs e)
    {
        divExcel.Visible = true;
        btnShowDivExcel.Visible = false;

    }

    protected void btnHideDivExcel_Click(object sender, EventArgs e)
    {
        divExcel.Visible = false;
        btnShowDivExcel.Visible = true;
    }
    protected void btnAddFormExtRevCand_Click(object sender, EventArgs e)
    {
        ExternalReviewerID = -1;
        FormMode = "Add";
        Type = (byte)ViewType.Candidate;
        PopulateAddEditForm();
        DatabindControls();
    }
    protected void btnAddFormExtRevDept_Click(object sender, EventArgs e)
    {
        
        ExternalReviewerID = -1;
        FormMode = "Add";
        Type = (byte)ViewType.Department;
        PopulateAddEditForm();
     
        DatabindControls();
    }
    protected void btnAddFormExtRevColl_Click(object sender, EventArgs e)
    {
        ExternalReviewerID = -1;
        FormMode = "Add";
        Type = (byte)ViewType.College;
        PopulateAddEditForm();
        
        DatabindControls();
    }
    protected void btnAddExtRev_Click(object sender, EventArgs e)
    {
        string[] deptLowInResearch = { "PE", "IAS", "GS" };
        //if ((tbTotPublications.Text == "0" || tbNoOfJournals.Text == "0" || tbHIndex.Text == "0" || tbCitations.Text == "0") && !deptLowInResearch.Contains(Master.ActiveTask.AppDeptSN))
        //{
        //    labelProgrammaticPopup0.Text = "Total Publications, No of Journals, H-Index, and Citations has to be greater than 0.";
        //    this.programmaticModalPopup0.Show();
        //    return;
        //}
        if(!ValidationChecks())
        {
            return;
        }

        List<Form_ExtRev> lFER = erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, Type);
        if (FormMode.Contains("Add"))
        {
            if (lFER.Where(er => er.ExternalReviewer.Email.Equals(tbEmail.Text)).Count() > 0)
            { 
                //Already present in your List 
                labelProgrammaticPopup0.Text = "This External Reviewers with the Email Address: (" + tbEmail.Text + ") is already present in the External Reviewers list.";
                programmaticModalPopup0.Show();
                return;
            }

        }
        else
        {
            if (lFER.Where(er => er.ExternalReviewer.Email.Equals(tbEmail.Text)).Count() > 1)
            {
                //Already present in your List 
                labelProgrammaticPopup0.Text = "This External Reviewers with the Email Address: (" + tbEmail.Text + ") is already present in the External Reviewers list.";
                programmaticModalPopup0.Show();
                return;
            }
        }
        //        if (ExternalReviewerID != -1)
        //        {
        //            ExternalReviewer er = erBAL.GetExtRevByID(ExternalReviewerID)[0];
        //            erBAL.UpdateExtRev(ExternalReviewerID,
        //                                      tbName.Text,
        //                                      ddlRank.SelectedValue,
        //                                      tbMailingAddress.Text,
        //                                      tbEmail.Text,
        //                                      tbMajor.Text,
        //                                      tbSpeciality.Text,
        //                                      tbPhoneAndFax.Text,
        //                                      tbActiveAreaOfResearch.Text,
        //                                      "",
        //                                      tbWebpage.Text,
        //                                      "",
        //                                      int.Parse(tbTotPublications.Text == "" ? "0" : tbTotPublications.Text),
        //                                      int.Parse(tbNoOfJournals.Text == "" ? "0" : tbNoOfJournals.Text),
        //                                      int.Parse(tbHIndex.Text == "" ? "0" : tbHIndex.Text),
        //                                      int.Parse(tbCitations.Text == "" ? "0" : tbCitations.Text),
        //                                     er.PassportNo, er.IBAN, RecordStatus.Active.ToString(), er.Password,
        //er.BankName, er.BranchAndAddress, er.AccHolderName, er.AccNumber, er.ABARoutingNo
        //, er.SWIFT_BIC, er.OtherBankCodes, er.PreferredCurrency
        //                                    );
        //        }
        //        else
        //        {
        //            ExternalReviewerID = erBAL.InsertExtRev(
        //                                    tbName.Text,
        //                                    ddlRank.SelectedValue,
        //                                    tbMailingAddress.Text,
        //                                    tbEmail.Text,
        //                                    tbMajor.Text,
        //                                    tbSpeciality.Text,
        //                                    tbPhoneAndFax.Text,
        //                                    tbActiveAreaOfResearch.Text,
        //                                    "",
        //                                    tbWebpage.Text,
        //                                    "",
        //                                    int.Parse(tbTotPublications.Text == "" ? "0" : tbTotPublications.Text),
        //                                    int.Parse(tbNoOfJournals.Text == "" ? "0" : tbNoOfJournals.Text),
        //                                    int.Parse(tbHIndex.Text == "" ? "0" : tbHIndex.Text),
        //                                    int.Parse(tbCitations.Text == "" ? "0" : tbCitations.Text),
        //                                   "", "", "", "", "", "", "", "", "", "");
        //        }
        List<ExternalReviewer> lER = erBAL.GetAllExtRev();

        if (ExternalReviewerID != -1)
        {
            ExternalReviewer er = erBAL.GetExtRevByID(ExternalReviewerID)[0];
            if(er.Email != tbEmail.Text && erBAL.GetAllExtRev().Where(extRev=>extRev.Email == tbEmail.Text).Count() == 1)
            {
                labelProgrammaticPopup0.Text = "This External Reviewer with the Email Address: (" + tbEmail.Text + ") is already present in the External Reviewers list. Update cannot be done.";
                programmaticModalPopup0.Show();
                return;
            }
            if (er.Name != tbName.Text && erBAL.GetAllExtRev().Where(extRev => extRev.Name == tbName.Text).Count() == 1)
            {
                labelProgrammaticPopup0.Text = "This External Reviewer with the Name: (" + tbName.Text + ") is already present in the External Reviewers list. Update cannot be done.";
                programmaticModalPopup0.Show();
                return;
            }
            erBAL.UpdateExtRev(ExternalReviewerID,
                            tbName.Text,
                            ddlRank.SelectedValue == "Others" ? tbOthers.Text : ddlRank.SelectedValue,
                            tbMailingAddress.Text,
                            tbEmail.Text,
                            tbMajor.Text,
                            tbSpecialty.Text,
                            tbPhoneAndFax.Text,
                            tbActiveAreaOfResearch.Text,
                            er.PrevAreaOfResearch,
                            tbWebpage.Text,
                            er.Comments,
                            int.Parse(tbTotPublications.Value == "" ? "0" : tbTotPublications.Value),
                            int.Parse(tbNoOfJournals.Value == "" ? "0" : tbNoOfJournals.Value),
                            int.Parse(tbHIndex.Value == "" ? "0" : tbHIndex.Value),
                            int.Parse(tbCitations.Value == "" ? "0" : tbCitations.Value),
                            RecordStatus.Active.ToString(),
                            er.Password, er.IBAN, er.PassportNo, tbName.Text,"" /*tbDescription.Text*/
                            , er.Name_1, er.Name_2, er.Name_3, er.Name_4, er.Salt);
            //erBAL.UpdateExtRev(
            //            ExternalReviewerID,                        
            //            tbName.Text,
            //            ddlRank.SelectedValue == "Others" ? tbOthers.Text : ddlRank.SelectedValue,
            //            tbMailingAddress.Text,
            //            tbEmail.Text.Trim(),
            //            tbMajor.Text,
            //            tbSpecialty.Text,
            //            tbPhoneAndFax.Text,
            //            tbActiveAreaOfResearch.Text,
            //            "",
            //            tbWebpage.Text,
            //            "",
            //            //int.Parse(tbTotPublications.Text == "" ? "0" : tbTotPublications.Text),
            //            int.Parse(tbTotPublications.Value == "" ? "0" : tbTotPublications.Value),
            //            int.Parse(tbNoOfJournals.Value == "" ? "0" : tbNoOfJournals.Value),
            //            int.Parse(tbHIndex.Value == "" ? "0" : tbHIndex.Value),
            //            int.Parse(tbCitations.Value == "" ? "0" : tbCitations.Value),"","","","",
            //            tbName.Text.Contains("Dr. ") ? tbName.Text : "Dr. " + tbName.Text
            //            , "");

        }
        else
        {
            //ExternalReviewerID = erBAL.InsertExtRev(tbName.Text,
            //    ddlRank.SelectedValue == "Others" ? tbOthers.Text : ddlRank.SelectedValue,
            //    tbMailingAddress.Text,
            //    tbEmail.Text.Trim(),
            //    tbMajor.Text,
            //    tbSpecialty.Text,
            //            tbPhoneAndFax.Text,
            //            tbActiveAreaOfResearch.Text,
            //            "",
            //            tbWebpage.Text,
            //            "",
            //            //int.Parse(tbTotPublications.Text == "" ? "0" : tbTotPublications.Text),
            //            int.Parse(tbTotPublications.Value == "" ? "0" : tbTotPublications.Value),
            //            int.Parse(tbNoOfJournals.Value == "" ? "0" : tbNoOfJournals.Value),
            //            int.Parse(tbHIndex.Value == "" ? "0" : tbHIndex.Value),
            //            int.Parse(tbCitations.Value == "" ? "0" : tbCitations.Value),
            //            RecordStatus.Active.ToString(), "", "", "", tbName.Text.Contains("Dr. ") ? tbName.Text : "Dr. " + tbName.Text, "");

            if (erBAL.GetAllExtRev().Where(extRev => extRev.Email == tbEmail.Text).Count() == 1)
            {
                labelProgrammaticPopup0.Text = "This External Reviewer with the Email Address: (" + tbEmail.Text + ") is already present in the External Reviewers list. Addition cannot be done.";
                programmaticModalPopup0.Show();
                return;
            }
            if (erBAL.GetAllExtRev().Where(extRev => extRev.Name == tbName.Text).Count() == 1)
            {
                labelProgrammaticPopup0.Text = "This External Reviewer with the Name: (" + tbName.Text + ") is already present in the External Reviewers list. Addition cannot be done.";
                programmaticModalPopup0.Show();
                return;
            }
            ExternalReviewerID = erBAL.InsertExtRev(
                                 tbName.Text,
                                 ddlRank.SelectedValue == "Others" ? tbOthers.Text : ddlRank.SelectedValue,
                                 tbMailingAddress.Text,
                                 tbEmail.Text,
                                 tbMajor.Text,
                                 tbSpecialty.Text,
                                 tbPhoneAndFax.Text,
                                 tbActiveAreaOfResearch.Text,
                                 "",
                                 tbWebpage.Text,
                                 "",
                                 int.Parse(tbTotPublications.Value == "" ? "0" : tbTotPublications.Value),
                                 int.Parse(tbNoOfJournals.Value == "" ? "0" : tbNoOfJournals.Value),
                                 int.Parse(tbHIndex.Value == "" ? "0" : tbHIndex.Value),
                                 int.Parse(tbCitations.Value == "" ? "0" : tbCitations.Value),
                                 RecordStatus.Active.ToString(), "", "", "", tbName.Text,
                                 ""/*tbDescription.Text*/, tbName.Text, "", "", "", "");


        }
        if (FormMode.Contains("Add"))
        {
            erBAL.InsertFormExtRev(
                       Master.ApplicationID,
                       Type,
                       erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, Type).Count,
                       ExternalReviewerID);
        }
        else
        {
            erBAL.DeleteFormExtRev(
                        Master.ApplicationID,
                        Type,
                        Serial);
            erBAL.InsertFormExtRev(
                        Master.ApplicationID,
                        Type,
                        Serial,
                        ExternalReviewerID
                        );

        }
        if (lFER.Count == 9)
        {
            Master.ReportSuccess("List of External Reviewers is complete");
            
        }
        else
        {
        //    lblMessage.Text = "List of 10 External Reviewers is incomplete";
            
            Master.ReportFailure("List of 10 External Reviewers is incomplete");
        }
        FormMode = "View";
        SwitchMode((Mode)Master.CurrentFormLevel);
        DatabindControls();
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        FormMode = "View";
        SwitchMode((Mode)Master.CurrentFormLevel);
        DatabindControls();
    }

    //protected void btnSearchName_Click(object sender, EventArgs e)
    //{
    //    tbSearch.Text = tbName.Text;
    //    ddlListType.SelectedIndex = -1;
    //    odsExtRevSearch.SelectParameters["MainSearchString"].DefaultValue = tbName.Text;
    //    gvExtRevSearchResult.DataBind();
    //    lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";
    //}
    protected void btnSearchExtRev_Click(object sender, EventArgs e)
    {
        gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbSearch.Text.ToLower());
        gvExtRevSearchResult.DataBind();
        lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";
    }
    protected void btnClearForm_Click(object sender, EventArgs e)
    {
        PopulateAddEditForm();
    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }
    #endregion
    #region  ddlListType
    protected void ddlListType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlListType.SelectedValue == "-1")
        {
            tbSearch.Text = "";
            gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbSearch.Text.ToLower());
            gvExtRevSearchResult.DataBind();
            lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";
        }
        else
        {
            Application app = bal.GetApplication(Master.ApplicationID)[0];            
            tbSearch.Text = "::" + ddlListType.SelectedItem.Text + "@" + app.Employee.Department1.ShortName;            
            gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbSearch.Text.ToLower());
            gvExtRevSearchResult.DataBind();
            lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";
        }
    }
    #endregion
    #region TextBox Event Handlers
    protected void tbSearch_TextChanged(object sender, EventArgs e)
    {        
        gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbSearch.Text.ToLower());
        gvExtRevSearchResult.DataBind();
        lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";
    }
    //protected void tbName_TextChanged(object sender, EventArgs e)
    //{
    //    string name = tbName.Text.Trim();
    //    tbName.Text = name.Replace("Dr. ", "");
    //    tbName.Text = name.Replace("Prof. ", "");
    //    tbName.Text = name.Replace("Professor", "");
    //    tbSearch.Text = tbName.Text.ToLower();
    //    odsExtRevSearch.SelectParameters["MainSearchString"].DefaultValue = tbName.Text.ToLower();
    //    gvExtRevSearchResult.DataBind();
    //    lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";


    //}
    //protected void tbMailingAddress_TextChanged(object sender, EventArgs e)
    //{
    //    string ma = tbMailingAddress.Text.Trim().ToLower();
    //    if (!ma.Contains("univ") &&
    //                     !ma.Contains("üniversite") && !ma.Contains("yliopisto") &&
    //                     !ma.Contains("uniwersytet") && !ma.Contains("tech") && !ma.Contains("institute") &&
    //                     !ma.Contains("iit") && !ma.Contains("mit") && !ma.Contains("uoit") && !ma.Contains("school") &&
    //                     !ma.Contains("Polytechnique") && !ma.Contains("ecole") && !ma.Contains("جامعه") &&
    //                     !ma.Contains("imperial college"))
    //    { tbMailingAddress.BackColor = Color.Red; }
    //    else
    //    { tbMailingAddress.BackColor = Color.White; }
    //}
    //protected void tbEmail_TextChanged(object sender, EventArgs e)
    //{
    //    tbEmail.Text = tbEmail.Text.Trim().ToLower();
    //    List<ExternalReviewer> lER = erBAL.GetExtRevByEmail(tbEmail.Text);
    //    if (lER.Count != 0)
    //    {
    //        tbEmail.BackColor = Color.Red;
    //        odsExtRevSearch.SelectParameters["MainSearchString"].DefaultValue = tbEmail.Text;
    //        gvExtRevSearchResult.DataBind();
    //        divExtRevSearch.Visible = true;
    //       // btnAddExtRev.Enabled = false;
    //        tbEmail.ToolTip = "The Email address is already in use with other external reviewer";
    //    }
    //    else
    //    {
    //        tbEmail.BackColor = Color.White;
    //        btnAddExtRev.Enabled = true;
    //        tbEmail.ToolTip = "Enter only one Email Address";
    //    }

    //}
    #endregion
    #region odsOExtRev
    protected void odsFormExtRevCand_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        DeleteAndReOrder((byte)ViewType.Candidate);
        Master.ReportFailure("List of 10 External Reviewers is incomplete");

        DatabindControls();
    }
    protected void odsFormExtRevColl_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        DeleteAndReOrder((byte)ViewType.College);
        Master.ReportFailure("List of 10 External Reviewers is incomplete");
        DatabindControls();
    }
    protected void odsFormExtRevDept_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        DeleteAndReOrder((byte)ViewType.Department);
        Master.ReportFailure("List of 10 External Reviewers is incomplete");
        DatabindControls();
    }
    private void DeleteAndReOrder(byte viewType)
    {
        foreach (Form_ExtRev fer in erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, viewType))
        {
            erBAL.DeleteFormExtRev(Master.ApplicationID, fer.Type, fer.Serial);
            erBAL.InsertFormExtRev(Master.ApplicationID, fer.Type, fer.Serial + 10, fer.ExternalReviewerID);
       //     erBAL.InsertFormExtRev(Master.ApplicationID, fer.Type, (byte) (fer.Serial + 10), fer.Name, fer.Rank, fer.MailingAddress, fer.Email,
       //fer.Major, fer.Speciality, fer.PhoneAndFax, fer.ActiveAreaOfResearch, fer.PrevAreaOfResearch,
       //fer.Webpage, fer.Comments, fer.TotalPublications, fer.NoOfJournals, fer.HIndex, fer.Citations);
        }
        int i = 0;
        foreach (Form_ExtRev fer in erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, viewType))
        {
            erBAL.DeleteFormExtRev(Master.ApplicationID, fer.Type, fer.Serial);
            erBAL.InsertFormExtRev(Master.ApplicationID, fer.Type, i++, fer.ExternalReviewerID);
            //     erBAL.InsertFormExtRev(Master.ApplicationID, fer.Type, (byte) i++,fer.Name, fer.Rank, fer.MailingAddress, fer.Email,
            //fer.Major, fer.Speciality, fer.PhoneAndFax, fer.ActiveAreaOfResearch, fer.PrevAreaOfResearch,
            //fer.Webpage, fer.Comments, fer.TotalPublications, fer.NoOfJournals, fer.HIndex, fer.Citations);
        }
    }
    #endregion
    #endregion
    private bool ValidationChecks()
    {
        bool isOK = true;
        if (tbCitations.Value == "0" || tbCitations.Value == "")
        {
            tbCitations.Style["border-color"] = Color.OrangeRed.Name;
            labelProgrammaticPopup0.Text = "Citations of external reviewer is must and must be greater than 0";
            programmaticModalPopup0.Show();
            isOK = false;
        }
        else
        {
            tbCitations.Style["border-color"] = Color.Gray.Name;
        }
        if (tbHIndex.Value == "0" || tbHIndex.Value == "")
        {
            tbHIndex.Style["border-color"] = Color.OrangeRed.Name;
            labelProgrammaticPopup0.Text = "H-Index of external reviewer is must and must be greater than 0";
            programmaticModalPopup0.Show();
            isOK = false;
        }
        else
        {
            tbHIndex.Style["border-color"] = Color.Gray.Name;
        }
        if (tbNoOfJournals.Value == "0" || tbNoOfJournals.Value == "")
        {
            tbNoOfJournals.Style["border-color"] = Color.OrangeRed.Name;
            labelProgrammaticPopup0.Text = "No Of Journals of external reviewer is must and must be greater than 0";
            programmaticModalPopup0.Show();
            isOK = false;
        }
        else
        {
            tbNoOfJournals.Style["border-color"] = Color.Gray.Name;
        }
        //        if (tbTotPublications.Text == "0" || tbTotPublications.Text == "")
        if (tbTotPublications.Value == "0" || tbTotPublications.Value == "")
        {
            //tbTotPublications.BorderColor = Color.OrangeRed;
            tbTotPublications.Style["border-color"] = Color.OrangeRed.Name;
            labelProgrammaticPopup0.Text = "Total Publications of external reviewer is must and must be greater than 0";
            programmaticModalPopup0.Show();
            isOK = false;
        }
        else
        {
            //tbTotPublications.BorderColor = Color.Gray;
            tbTotPublications.Style["border-color"] = Color.Gray.Name;            
        }
        if (tbActiveAreaOfResearch.Text == "")
        {
            tbActiveAreaOfResearch.BorderColor = Color.OrangeRed;
            labelProgrammaticPopup0.Text = "Active Area Of Research of external reviewer is must";
            programmaticModalPopup0.Show();
            isOK = false;
        }
        else
        {
            tbActiveAreaOfResearch.BorderColor = Color.Gray;
        }

        if (tbSpecialty.Text == "")
        {
            tbSpecialty.BorderColor = Color.OrangeRed;
            labelProgrammaticPopup0.Text = "Specialty of external reviewer is must";
            programmaticModalPopup0.Show();
            isOK = false;
        }
        else
        {
            tbSpecialty.BorderColor = Color.Gray;
        }

        if (tbMajor.Text == "")
        {
            tbMajor.BorderColor = Color.OrangeRed;
            labelProgrammaticPopup0.Text = "Major of external reviewer is must";
            programmaticModalPopup0.Show();
            isOK = false;
        }
        else
        {
            tbMajor.BorderColor = Color.Gray;
        }
        if (!PromotionApplication.IsValidEmail(tbEmail.Text.Trim()))
        {
            tbEmail.BorderColor = Color.OrangeRed;
            labelProgrammaticPopup0.Text = "A valid Email of external reviewer is must";
            programmaticModalPopup0.Show();
            isOK = false;
        }
        else
        {
            tbEmail.BorderColor = Color.Gray;
        }        
        if (!PromotionApplication.ValidateMailingAddress(tbMailingAddress.Text.Trim().ToLower()))
        {
            tbMailingAddress.BorderColor = Color.OrangeRed;
            labelProgrammaticPopup0.Text = "The Mailing Address must contain the name of the University/Institute/School";
            programmaticModalPopup0.Show();
            isOK = false;
        }
        else
        {
            tbMailingAddress.BorderColor = Color.Gray;
        }

        if (tbName.Text == "")
        {
            tbName.BorderColor = Color.OrangeRed;
            labelProgrammaticPopup0.Text = "The Name of external reviewer is must";
            programmaticModalPopup0.Show();
            isOK = false;
        }
        else
        {
            tbName.BorderColor = Color.Gray;

        }


        if (isOK)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected bool ListIncomplete(string list)
    {
        if (list == "CandidateList")
        {
            if (erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, (byte)ExternalReviewerSelectionType.Applicant).Count < 5)
            {
                ShowError("List incomplete. Must have 5 entries. As per the directions of the Vice President of Research and Innovation as indicated in his earlier circular, please verify from Scopus source only, the Total Publications, Number of journal publications, H-index and Citations of the reviewers. No personal association or co-authorship with the applicant should be ascertained from CV of the Applicant");
                return true;
            }
            else
            {
                ShowMessage("List complete");
                return false;
            }
            
                
        }
        else if (list == "DepartmentList" && divFormExtRevDepartment.Visible)
        {
            if( erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, (byte)ExternalReviewerSelectionType.Department).Count < 15)
            {
                ShowError("List incomplete. Must have 15 entries. As per the directions of the Vice President of Research and Innovation as indicated in his earlier circular, please verify from Scopus source only, the Total Publications, Number of journal publications, H-index and Citations of the reviewers. No personal association or co-authorship with the applicant should be ascertained from CV of the Applicant");
                return true;
            }
            else
            {
                ShowMessage("List complete");
                return false;
            }
        }
        else if (list == "CollegeList" && divFormExtRevCollege.Visible)
        {
            if( erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, (byte)ExternalReviewerSelectionType.College).Count < 10)
            {
                ShowError("List incomplete. Must have 10 entries. As per the directions of the Vice President of Research and Innovation as indicated in his earlier circular, please verify from Scopus source only, the Total Publications, Number of journal publications, H-index and Citations of the reviewers. No personal association or co-authorship with the applicant should be ascertained from CV of the Applicant");
                return true;
            }
            else
            {
                ShowMessage("List complete");
                return false;
            }
        }
        return true;
    }
    private void ReorderRows(GridViewCommandEventArgs e, byte viewType)
    {
        if (e.CommandName == "MoveRowUp")
        {
            byte serial = Convert.ToByte(e.CommandArgument.ToString());
            Form_ExtRev fer = erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, viewType)
                .Where(er => er.Serial == serial).ToList()[0];

            erBAL.DeleteFormExtRev(fer.ApplicationID, fer.Type, fer.Serial);
            //Form_Refree1 ferUR = erBAL.GetFormExtRevByPK(fer.ApplicationID, fer.Type,(byte) (fer.Serial - 1))[0];
            Form_ExtRev ferUR = erBAL.GetFormExtRevByPK(fer.ApplicationID, fer.Type, (byte)(fer.Serial - 1))[0];
            erBAL.DeleteFormExtRev(fer.ApplicationID, fer.Type, fer.Serial - 1);
            erBAL.InsertFormExtRev(fer.ApplicationID, fer.Type, fer.Serial, ferUR.ExternalReviewerID);
            //     erBAL.InsertFormExtRev(fer.ApplicationID, fer.Type, fer.Serial, ferUR.Name, ferUR.Rank, ferUR.MailingAddress, ferUR.Email,
            //ferUR.Major, ferUR.Speciality, ferUR.PhoneAndFax, ferUR.ActiveAreaOfResearch, ferUR.PrevAreaOfResearch,
            //ferUR.Webpage, ferUR.Comments, ferUR.TotalPublications, ferUR.NoOfJournals, ferUR.HIndex, ferUR.Citations);
            erBAL.InsertFormExtRev(fer.ApplicationID, fer.Type, fer.Serial - 1, fer.ExternalReviewerID);
       //     erBAL.InsertFormExtRev(fer.ApplicationID, fer.Type,(byte) (fer.Serial - 1), fer.Name, fer.Rank, fer.MailingAddress, fer.Email,
       //fer.Major, fer.Speciality, fer.PhoneAndFax, fer.ActiveAreaOfResearch, fer.PrevAreaOfResearch,
       //fer.Webpage, fer.Comments, fer.TotalPublications, fer.NoOfJournals, fer.HIndex, fer.Citations);
        }
        else if (e.CommandName == "MoveRowDn")
        {
            byte serial = Convert.ToByte(e.CommandArgument.ToString());
            Form_ExtRev fer = erBAL.GetFormExtRevByAppIDType(Master.ApplicationID, viewType).Where(er => er.Serial == serial).ToList()[0];

            erBAL.DeleteFormExtRev(fer.ApplicationID, fer.Type, fer.Serial);
            Form_ExtRev ferLR = erBAL.GetFormExtRevByPK(fer.ApplicationID, fer.Type,fer.Serial + 1)[0];
            erBAL.DeleteFormExtRev(fer.ApplicationID, fer.Type, fer.Serial + 1);
            erBAL.InsertFormExtRev(fer.ApplicationID, fer.Type, fer.Serial, ferLR.ExternalReviewerID);
            //     erBAL.InsertFormExtRev(fer.ApplicationID, fer.Type, fer.Serial, ferLR.Name, ferLR.Rank, ferLR.MailingAddress, ferLR.Email,
            //ferLR.Major, ferLR.Speciality, ferLR.PhoneAndFax, ferLR.ActiveAreaOfResearch, ferLR.PrevAreaOfResearch,
            //ferLR.Webpage, ferLR.Comments, ferLR.TotalPublications, ferLR.NoOfJournals, ferLR.HIndex, ferLR.Citations);
            erBAL.InsertFormExtRev(fer.ApplicationID, fer.Type, fer.Serial + 1, fer.ExternalReviewerID);
       //     erBAL.InsertFormExtRev(fer.ApplicationID, fer.Type,(byte) (fer.Serial + 1), fer.Name, fer.Rank, fer.MailingAddress, fer.Email,
       //fer.Major, fer.Speciality, fer.PhoneAndFax, fer.ActiveAreaOfResearch, fer.PrevAreaOfResearch,
       //fer.Webpage, fer.Comments, fer.TotalPublications, fer.NoOfJournals, fer.HIndex, fer.Citations);
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