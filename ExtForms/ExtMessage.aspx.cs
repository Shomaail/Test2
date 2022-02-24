using System;
using System.Data;
using System.Configuration;
using System.Linq;
using BL.Data;
using System.Threading;

public partial class ExtForms_ExtMessage : System.Web.UI.Page
{
    BAL bal = new BAL();
    ExtRevBAL erBAL = new ExtRevBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = ConfigurationManager.AppSettings["OrganizationShortName"] +" "+ ConfigurationManager.AppSettings["System_Title"];
        foreach (TaskForm tf in bal.GetTaskFormByTask((int) TaskExtID.External_Evaluation,true).OrderBy(a=>a.Rank))
        {
            
            if (bal.GetApplicationTaskForm(Master.ApplicationID, (int)TaskExtID.External_Evaluation,tf.FormID,true,Master.ExtReviewerID).Count == 0)
            {
                bal.InsertAppTskFrm(Master.ApplicationID, (int)TaskExtID.External_Evaluation, tf.FormID, false, "",  true,Master.ExtReviewerID);
                Thread.Sleep(1);
            }
        }
        
    }
    public string SetVariables(string str)
    {
        
        Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID, Master.ExtReviewerID)[0];
        Application app = bal.GetApplication(Master.ApplicationID)[0];
        Employee applicant = bal.GetApplicant(Master.ApplicationID)[0];
        string topAuthorityID = bal.GetApplicationRole(ffer.ApplicationID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID;
        str = str
            .Replace("@@ReviewersName@@", ffer.ExternalReviewer.NameString)
            .Replace("@@OrganizationShortName@@", ConfigurationManager.AppSettings["OrganizationShortName"])
            .Replace("@@ApplicantName@@", applicant.NameString)
            .Replace("@@ForRank@@", app.ForRank)
            .Replace("@@ApplicantDept@@", applicant.Department)
            .Replace("@@TopAuthority@@", bal.GetEmployees().Where(a => a.EmployeeID == topAuthorityID).First().NameString)
            .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
            .Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"])

            .Replace("@@ReviewersNameAr@@", ffer.ExternalReviewer.NameAr)
     //       .Replace("@@ForRankAr@@", app.ForRankAr)
            .Replace("@@OrganizationNameAr@@", ConfigurationManager.AppSettings["OrganizationNameAr"])
            ;

        return str;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Master.Language == "ar-sa")
        {
            TextBoxMessage.Text = SetVariables(bal.GetExtTaskMessage((int)TaskExtID.Welcome_Message_External_Evaluation)[1].Message);
        }
        else
        {
            TextBoxMessage.Text = SetVariables(bal.GetExtTaskMessage((int)TaskExtID.Welcome_Message_External_Evaluation)[0].Message);
          }
    }
}
