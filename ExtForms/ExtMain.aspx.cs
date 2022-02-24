
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using BL.Data;
    public partial class ExtForms_ExtMain : System.Web.UI.Page
    {
        BAL bal = new BAL();
        ExtRevBAL erBAL = new ExtRevBAL();
        ExtRevFormBAL erfBAL = new ExtRevFormBAL();
        public string dataPath;
        public FileInfo[] uploadedFiles;
    public enum FormModeMain { Applications, MyDetails };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            //if (Request.QueryString["MyDetails"] == null)
            //{
            //    FormMode = FormModeMain.Applications.ToString();
            //}
            //else
            //{
            //    FormMode = FormModeMain.MyDetails.ToString();
            //}

            Session["applicationID"] = null;

            if (Session["ExtRevID"] != null)
            {
                DatabindControls();
            }
            else
            {
                FormsAuthentication.SignOut();
                Response.Redirect("../Login.aspx");
            }


        }
        public string GetURL(int appID)
        {
            int extReviewerID = int.Parse(Session["ExtRevID"].ToString());
            //Session["applicationID"] = appID;
            bal.InsertActionLog(erBAL.GetExtRevByID(extReviewerID)[0].NameString + " Case of Dr. " + bal.GetApplicant(appID)[0].NameString, DateTime.Now);
            return "ExtMessage.aspx?applicationID="+ appID;

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
                {
                    return (string)ViewState["FormMode"];
                }
                else
                {
                    return "";
                }

            }
        }
        #endregion
        public void DatabindControls()
        {
            if (Request.QueryString["MyDetails"] == null && !IsPostBack)
            {
                FormMode = FormModeMain.Applications.ToString();
            }
            else if (Request.QueryString["MyDetails"] != null && !IsPostBack)
            {
                FormMode = FormModeMain.MyDetails.ToString();
            }
            if(Session["ExtRevID"] == null)
        {
            FormsAuthentication.SignOut();
            return;
        }
            int extReviewerID = int.Parse(Session["ExtRevID"].ToString());
            ExternalReviewer er = erBAL.GetExtRevByID(extReviewerID)[0];
            lblUserName.Text = er.NameString;
            divMyDetails.DataBind();
            divApplications.DataBind();
            if (FormMode == FormModeMain.Applications.ToString())
            {
                liApp.Attributes["class"] = "active";
                liMyDetails.Attributes["class"] = "";
            }
            else if (FormMode == FormModeMain.MyDetails.ToString())
            {
                liApp.Attributes["class"] = "";
                liMyDetails.Attributes["class"] = "active";
            }


            rpCurrentEvaluation.DataSource = erBAL.GetForm_FinalExtRev()
                                                    .Where(a => a.ExternalReviewerID == extReviewerID
                                                    && a.MaterialSentStatus == SendSelPubStatus.Material_Sent.ToString().Replace("_", " ")
                                                    && a.EvaluationStatus != EvaluationStatus.Submitted.ToString()
                                                    && a.EvaluationStatus != EvaluationStatus.Withdrawn.ToString());
            rpCurrentEvaluation.DataBind();
            divCurrentEvalMsg.Visible = rpCurrentEvaluation.Items.Count != 0;
            divCurrentEvalAbsentMsg.Visible = rpCurrentEvaluation.Items.Count == 0;
            rpSubmittedEvaluation.DataSource = erBAL.GetForm_FinalExtRev().Where(a => a.ExternalReviewerID == extReviewerID &&
                                                a.EvaluationStatus == EvaluationStatus.Submitted.ToString());
            rpSubmittedEvaluation.DataBind();
            divSubmittedEvalMsg.Visible = rpSubmittedEvaluation.Items.Count != 0;
            divSubmittedEvalAbsentMsg.Visible = rpSubmittedEvaluation.Items.Count == 0;
            rpWithdrawnEvaluations.DataSource = erBAL.GetForm_FinalExtRev().Where(a => a.ExternalReviewerID == extReviewerID
                                           && a.EvaluationStatus == EvaluationStatus.Withdrawn.ToString());
            rpWithdrawnEvaluations.DataBind();
            divWithdrawnEvalMsg.Visible = rpWithdrawnEvaluations.Items.Count != 0;
            divWithdrawnEvalAbsentMsg.Visible = rpWithdrawnEvaluations.Items.Count == 0;
            taName.Value = er.NameString;
            taRank.Value = er.Rank;
            taAddress.Value = er.MailingAddress;
            taEmail.Value = er.Email;
            taMajor.Value = er.Major;
            taSpecialty.Value = er.Specialty;
            taPhoneNFax.Value = er.PhoneAndFax;
            taActiveAreaOfResearch.Value = er.ActiveAreaOfResearch;
            taPreviousAreaOfResearch.Value = er.PrevAreaOfResearch;
            taWebsite.Value = er.Webpage;
            taTotalPublications.Value = er.TotalPublications.ToString();
            taNoOfJournals.Value = er.NoOfJournals.ToString();
            taHIndex.Value = er.HIndex.ToString();
            taCitations.Value = er.Citations.ToString();
            taPassportNo.Value = er.PassportNo;
            if(string.IsNullOrEmpty(er.IBAN))
            {
            er.IBAN = @"ACCOUNT NO:
IBAN / SWIFT CODE:
ROUTING NO:
BANK NAME:
BRANCH:
CITY:
COUNTRY:";
            }
            taBank.Value = er.IBAN;
            LoadPassportAttachment();

    }

        protected void lbtnApplications_Click(object sender, EventArgs e)
        {
            FormMode = FormModeMain.Applications.ToString();
            DatabindControls();
        }

        protected void lbtnMyDetails_Click(object sender, EventArgs e)
        {
            FormMode = FormModeMain.MyDetails.ToString();
            DatabindControls();
        }



        //protected void RouteToExtMessage(int appID)
        //{
        //    int extReviewerID = int.Parse(Session["ExtRevID"].ToString());
        //    Session["applicationID"] = appID;
        //    bal.InsertActionLog(erBAL.GetExtRevByID(extReviewerID)[0].NameString + " Case of Dr. " + bal.GetApplicant(appID)[0].NameString, DateTime.Now);
        //    Response.Redirect("ExtMessage.aspx");
        //}
        //protected void RouteToExtMessage(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        //{
        //    int appID = int.Parse(e.CommandArgument.ToString());
        //    int extReviewerID = int.Parse(Session["ExtRevID"].ToString());
        //    if (e.CommandName == "Open")
        //    {
        //        Session["applicationID"] = appID;
        //        bal.InsertActionLog(erBAL.GetExtRevByID(extReviewerID)[0].NameString + " Case of Dr. " + bal.GetApplicant(appID)[0].NameString, DateTime.Now);
        //        Response.Redirect("ExtMessage.aspx");
        //    }
        //}



       

    //protected void lbtn_Click(object sender, EventArgs e)
    //{
    //    RepeaterItem rItem = (sender as LinkButton).Parent as RepeaterItem;
    //    int appID = int.Parse((rItem.FindControl("lblApplicationID") as Label).Text);
    //    RouteToExtMessage(appID);

    //}
    private void LoadPassportAttachment()
    {
        int extReviewerID = -1;
        if (Session["ExtRevID"] != null)
        {
            extReviewerID = int.Parse(Session["ExtRevID"].ToString());
        }
        else
        {
            return;
            //FormsAuthentication.SignOut();
            //Response.Redirect("../Login.aspx");
        }
        dataPath = Server.MapPath("~/App_Data/PassportAttachments/");
        erfBAL.GetPassportAttachments(extReviewerID, dataPath, out uploadedFiles);
        if (uploadedFiles.Count() != 0)
        {
            lbtnPassportAttachment.Text = uploadedFiles[0].Name.Replace(extReviewerID + "_", "");
            btnDelPassportAttachment.Visible = true;
            lbtnPassportAttachment.Visible = true;
        }
        else
        {
            btnDelPassportAttachment.Visible = false;
            lbtnPassportAttachment.Visible = false;
        }
    }
    
    #region Event Handler
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int extReviewerID = -1;
        if (Session["ExtRevID"] != null)
        {
            extReviewerID = int.Parse(Session["ExtRevID"].ToString());
        }
        else
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("../Login.aspx");
            return;            
        }
        ExternalReviewer er = erBAL.GetExtRevByID(extReviewerID)[0];
        if(taBank.Value == @"ACCOUNT NO:
IBAN / SWIFT CODE:
ROUTING NO:
BANK NAME:
BRANCH:
CITY:
COUNTRY:")
        {
            labelProgrammaticPopup0.Text = "Bank details is necessary for processing Honrarium. Form is not complete";
            programmaticModalPopup0.Show();
            return;
        }


        erBAL.UpdateExtRev(er.ExternalReviewerID, taName.Value
            , taRank.Value, taAddress.Value, taEmail.Value, taMajor.Value, taSpecialty.Value, taPhoneNFax.Value, taActiveAreaOfResearch.Value
            , taPreviousAreaOfResearch.Value, taWebsite.Value, er.Comments, int.Parse(taTotalPublications.Value), int.Parse(taNoOfJournals.Value)
            , int.Parse(taHIndex.Value), int.Parse(taCitations.Value), er.Status, er.Password,
             taBank.Value, taPassportNo.Value, taName.Value, taDescription.Value, er.Name_1, er.Name_2, er.Name_3, er.Name_4, er.Salt);
        dataPath = Server.MapPath("~/App_Data/PassportAttachments/");
        erfBAL.GetPassportAttachments(er.ExternalReviewerID, dataPath, out uploadedFiles);
        HttpPostedFile file = Request.Files[0];

        if (file.FileName != "")
        {
            var regexItem = new Regex("^[\\w,\\s-]+\\.[A-Za-z]{3}$");
            if (!regexItem.IsMatch(file.FileName))
            {
                labelProgrammaticPopup0.Text = "The file name of the uploaded file is unacceptable. No file is uploaded. File name can only have characters like alphabets from A-Z, digits from 0-9 and _ .";
                programmaticModalPopup0.Show();
                return;
            }
            //if (file.ContentType != "application/pdf")
            //{
            //    labelProgrammaticPopup0.Text = "You can only upload pdf files.";
            //    programmaticModalPopup0.Show();
            //    return ;

            //}
            if (file.ContentLength > 4000000)
            {
                labelProgrammaticPopup0.Text = "You can only upload files of size less than 4 MB.";
                programmaticModalPopup0.Show();
                return;
            }
            foreach (var item in uploadedFiles)
            {
                System.IO.File.Delete(dataPath + item.Name);
            }
            file.SaveAs(dataPath + er.ExternalReviewerID + "_" + file.FileName);
        }
        else
        {
            if(uploadedFiles.Length == 0)
            {
                labelProgrammaticPopup0.Text = "Scanned copy of Passport is also necessary for processing honorarium.";
                programmaticModalPopup0.Show();
                return;
            }
        }

        LoadPassportAttachment();
        labelProgrammaticPopup0.Text = "All information for Honorarium is saved successfully and administration is notified to perform further actions.";
        programmaticModalPopup0.Show();
        Emailer.Send(ConfigurationManager.AppSettings["Mail.From"], er.NameString +" - Details for Honorarium Complete", @"This is to notify the administration of Faculty Promotion System, that the details for processing Honorarium for the External Reviewer "+ er.NameString+" " +
            "(LoginInfo: "+ er.Email+ " / "+Cryptography.Decrypt(er.Password)+") is complete. ", "AutoEmailer", null);
    }
    protected void lbtnPassportAttachment_Click(object sender, EventArgs e)
    {
        int extReviewerID = -1;
        if (Session["ExtRevID"] != null)
        {
            extReviewerID = int.Parse(Session["ExtRevID"].ToString());
        }
        else
        {
            return;
            //FormsAuthentication.SignOut();
            //Response.Redirect("../Login.aspx");
        }
        dataPath = Server.MapPath("~/App_Data/PassportAttachments/");
        erfBAL.GetPassportAttachments(extReviewerID, dataPath, out uploadedFiles);
        FileStream stream = System.IO.File.OpenRead(uploadedFiles[0].FullName);
        long length = stream.Length;
        stream.Close();
        Response.Clear();
        try
        {
            Response.Charset = "UTF-8";
            Response.AddHeader("Content-Disposition", "attachment; filename= " + uploadedFiles[0].Name);
            Response.AddHeader("Content-Length", "" + length);
            Response.WriteFile(uploadedFiles[0].FullName);
            Response.Flush();
            Response.Close();
        }
        catch
        {
        }
    }
    protected void btnDelPassportAttachment_Click(object sender, EventArgs e)
    {
        int extReviewerID = -1;
        if (Session["ExtRevID"] != null)
        {
            extReviewerID = int.Parse(Session["ExtRevID"].ToString());
        }
        else
        {
            FormsAuthentication.SignOut();
            return;            
            //Response.Redirect("../Login.aspx");
        }
        dataPath = Server.MapPath("~/App_Data/PassportAttachments/");
        erfBAL.GetPassportAttachments(extReviewerID, dataPath, out uploadedFiles);
        System.IO.File.Delete(uploadedFiles[0].FullName);
        LoadPassportAttachment();
    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }
    #endregion

}
