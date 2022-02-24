using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Controls_DigitalSignature : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string GetStatus()
    {
        string value = Eval("Position").ToString();
        switch (value)
        {
            case "10":
                return "Chairman";
            default:
                return "Promotion Committee Member";

        }
    }
    protected void CheckBoxDigitalSignature_CheckedChanged(object sender, EventArgs e)
    {
        
        int appID = (int) Session["ApplicationIDGlobal"];

        
        if (GetDSStatusByApplication(appID))
        {
            try
            {
                EmailTableAdapters.Task_ExtMessagesTableAdapter adapterTskExtMsg = new EmailTableAdapters.Task_ExtMessagesTableAdapter();
                ExternalRemindersTableAdapters.Task_ExtTableAdapter adapterTskExt = new ExternalRemindersTableAdapters.Task_ExtTableAdapter();
                string emailTemplate = adapterTskExtMsg.GetDataByTaskID(adapterTskExt.GetDataByTitle("All Members Have Signed")[0].TaskID)[0].Message;
                string emailSubject = adapterTskExtMsg.GetDataByTaskID(adapterTskExt.GetDataByTitle("All Members Have Signed")[0].TaskID)[0].Subject;
                emailTemplate = emailTemplate.Replace("@@PCChair@@", GetPC(appID)[0].Name);
                emailTemplate = emailTemplate.Replace("@@Applicant@@", GetApplicant(appID).Name);
                Emailer.Send(GetPC(appID)[0].Email + ", promotion@kfupm.edu.sa"
                            , emailSubject
                            , emailTemplate
                            , "AutoEmailer"
                            ,appID);
            }
            catch (Exception)
            {
                
               
            }
                    
        }
  
    }
    public bool GetDSStatusByApplication(int appID)
    {

        for (int i = 0; i < 5; i++)
        {
            if (GetPC(appID)[i].DigitalSignature == false)
                return false;
        }
        return true;

    }
    public Promotion.PromotionCommitteeTempDataTable GetPC(int appID)
    {
        PromotionTableAdapters.PromotionCommitteeTempTableAdapter adapter = new PromotionTableAdapters.PromotionCommitteeTempTableAdapter();
        return adapter.GetData(appID);
    }
    public HR.EmployeeRow GetApplicant(int appID)
    {
        HRTableAdapters.EmployeeTableAdapter adapterEmployee = new HRTableAdapters.EmployeeTableAdapter();
        return  adapterEmployee.GetApplicant(appID)[0];
    }
    public HR.EmployeeRow GetEmployee(string employeeID)
    {
        HRTableAdapters.EmployeeTableAdapter adapterEmployee = new HRTableAdapters.EmployeeTableAdapter();
        return adapterEmployee.GetDataByEmpID(employeeID)[0];
    }

}
