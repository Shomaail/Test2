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
using BL.Data;
using KFUPM.CAS;
using System.Linq;
using System.Collections.Generic;
public partial class Forms_ContactVRGSSR : System.Web.UI.Page
{
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        if (Master.CurrentFormLevel == -1)
        {
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            return;
        }

        Instruction1.Text = Master.CurrentFormInstruction;
       // ComposeEmail1.Visible = false;

    }
    protected void ComposeEmail_OnEmailSent(object sender, EventArgs e)
    {
        if (RadioButtonListPCChairtoVR.SelectedValue == "1")
        {
            ApplicationCheckListTableAdapters.Form_MaterialFlagTableAdapter frmMatFlg = new ApplicationCheckListTableAdapters.Form_MaterialFlagTableAdapter();
            if (frmMatFlg.Update(true, Master.ApplicationID) == 0)
                frmMatFlg.Insert(Master.ApplicationID, true);
        }
        Alert1.Message("Email has been successfully send.");
      //  ComposeEmail1.Visible = false;
    }
    protected void ComposeEmail_OnEmailNotSent(object sender, EventArgs e)
    {        
       // ComposeEmail1.Visible = false;
    }
    //public void AlertMessage(string strMessage)
    //{
    //    string strScript = "<script language=JavaScript>";
    //    strScript += "alert('" + strMessage + "');";
    //    strScript += "</script>";
    //    if (!Page.IsStartupScriptRegistered("clientScript"))
    //        Page.RegisterStartupScript("clientScript", strScript);
    //}


    protected void RadioButtonListPCChairtoVR_SelectedIndexChanged(object sender, EventArgs e)
    {
        //PromotionTableAdapters.RoleTableAdapter roleAdapter = new PromotionTableAdapters.RoleTableAdapter();
        //HRTableAdapters.EmployeeTableAdapter empAdapter = new HRTableAdapters.EmployeeTableAdapter();
        string vrr = bal.GetEmployeeByPK((bal.GetApplicationRole(Master.ApplicationID)
            .Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameString;
        Employee applicant = bal.GetApplicant(Master.ApplicationID)[0];
        //HR.EmployeeRow VRGSSR = empAdapter.GetDataByRole(Master.ApplicationID, roleAdapter.GetDataByRoleName("VRGSSR")[0].RoleID)[0];
        //HR.EmployeeRow applicant = empAdapter.GetApplicant(Master.ApplicationID)[0];
        //PromotionTableAdapters.PromotionCommitteeTempTableAdapter pcAdapter = new PromotionTableAdapters.PromotionCommitteeTempTableAdapter();
        //Promotion.PromotionCommitteeTempRow PCChairman = pcAdapter.GetMembersAgainstAppID(Master.ApplicationID)[0];                 

        string emailAddress = "facpromote@kfupm.edu.sa";
       
        Email.ComposeEmailDataTable email = new Email.ComposeEmailDataTable();
        
        email.NewComposeEmailRow();
        if (RadioButtonListPCChairtoVR.SelectedValue == "1")
        {
            email.AddComposeEmailRow(emailAddress, "Send Material", "Dear " + vrr+ @",
Considering the Promotion Case of " + applicant.NameString 
+ @" of the Department of " +applicant.Department
+ @", the promotion committee in its meeting decided that the selected CV and Publications are now ready to be sent to the External Reviewers for Evaluation

Thanks 
" + bal.GetForm_FinalPC(Master.ApplicationID)[0].Employee.NameString + @",
Chairman Promotion Committee of " + bal.GetApplicant(Master.ApplicationID)[0].NameString);
         //   ComposeEmail1.LoadData(email);            
          //  ComposeEmail1.Visible = true;

        }
        else if (RadioButtonListPCChairtoVR.SelectedValue == "2")
        {
            email.AddComposeEmailRow(emailAddress, "Update CV and Publications", "Dear " + vrr + @",
Considering the Promotion Case of " + applicant.NameString + @" of the Department of " + applicant.Department 
+ @", the promotion committee in its meeting found the following points to be updated in the promotion application:

Thanks 
" + bal.GetForm_FinalPC(Master.ApplicationID)[0].Employee.NameString + @",
Chairman Promotion Committee of " + applicant.NameString);
          //  ComposeEmail1.LoadData(email);
           // ComposeEmail1.Visible = true;
        
        }
    }
}
