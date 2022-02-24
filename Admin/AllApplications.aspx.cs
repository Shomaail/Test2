using System;
using System.IO;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL.Data;
public partial class Admin_AllApplications : System.Web.UI.Page
{
    BAL bal = new BAL();
    ExtRevBAL erBAL = new ExtRevBAL();
    ExtRevFormBAL erfBAL = new ExtRevFormBAL();
    public FileInfo[] files;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        DataBindControls();
    }
    private void DataBindControls()
    {
        rptAllApplication.DataSource = bal.GetApplication();
        rptAllApplication.DataBind();
    }
   
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }
    protected void rptAllApplication_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string dataPath = Server.MapPath("~\\App_Data\\ApplicationAttachments") + "\\";
        if (e.CommandName == "Delete")
          {
            int appID = int.Parse(e.CommandArgument.ToString());
            if (bal.GetApplication().Count() == 1)
            {
                labelProgrammaticPopup0.Text = "There has to be atleast one application. You cannot delete this application.";
                programmaticModalPopup0.Show();
                return;
            }
            
            bal.DeleteForm_AttachmentAdByAppID(appID);
            bal.DeleteForm_ChairmanInput(appID);            
            bal.DeleteForm_FinalPC(appID);
            bal.DeleteFormMaterialFlag(appID);            
            bal.DeleteForm_PCFeedback(appID);
            bal.DeleteForm_PointChanges(appID);
            bal.DeleteForm_Points(appID);
            bal.DeleteForm_AreaOfSp(appID);
            bal.DeleteForm_DepartmentCommittee(appID);

            bal.DeleteForm_ServiceYears(appID);
            bal.DeleteForm_AttachmentAdByAppID(appID);            
            bal.DeleteForm_PromotionCommittee(appID);
          
            //bal.DeleteForm_ServiceYears(appID); 
            bal.DeletetAppTskFrm(appID);
            bal.DeleteForm_PCFeedback(appID);

            erBAL.DeleteForm_ExtRev(appID);
            erBAL.DeleteForm_FinalExtRev(appID);

            //foreach(Form_Attachment attachment in bal.GetForm_AttachmentByAppID(appID))
            //{
            //    File.Delete(dataPath + attachment.ApplicationID + "_" + attachment.EmployeeID + "_" + attachment.DocumentName);
            //}            dataPath = Server.MapPath("~/App_Data/EvaluationAttachments/");
            //foreach (Evaluation evaluation in erfBAL.GetEvaluation(appID))
            //{                if(evaluation.ExternalReviewerID.HasValue)
            //    {
            //        erfBAL.GetEvaluationAttachments(appID, evaluation.ExternalReviewerID.Value, dataPath, out files);
            //        if(files.Count() > 0)
            //        {
            //            File.Delete(files[0].FullName);
            //        }                    
            //    }
            //}           
            bal.DeleteForm_AppProperties(appID);
            bal.DeleteApplicationActionStatus(appID);
            //bal.DeleteSentEmailByAppID(appID);
            bal.DeleteAppTskLg(appID);
            
            //bal.DeleteApplication_TaskLogExtByAppID(appID);
            //bal.DeleteApplication_LogExtByAppID(appID);
            bal.DeleteApplication_Log(appID);
            bal.DeleteSentEmailByAppID(appID);
            bal.DeleteApplicationRoles(appID);
            erfBAL.DeleteEvaluation(appID);
            bal.DeleteApplication(appID);
            DataBindControls();
                }
    }
}