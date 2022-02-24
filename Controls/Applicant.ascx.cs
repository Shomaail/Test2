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
public partial class Controls_Applicant : System.Web.UI.UserControl
{
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public bool LoadApplicant(int applicationID)
    {
        // HRTableAdapters.EmployeeTableAdapter adapter = new HRTableAdapters.EmployeeTableAdapter();        

        //HR.EmployeeDataTable table = adapter.GetApplicant(applicationID);

        if (bal.GetApplicant(applicationID).Count == 0)
        {
            return false;
        }
        Employee applicant = bal.GetApplicant(applicationID)[0];
        lblID.Text = applicant.EmployeeID;
        lblName.Text = applicant.NameString;
        lblDepartment.Text = applicant.Department;
        lblCollege.Text = applicant.ParentDept;
        lblPhone.Text = applicant.Phone;
        lblEmail.Text = applicant.Email;
        lblPOBox.Text = applicant.POBox;
        lblRank.Text = applicant.Rank;
        //lblJoinDate.Text =      DateTime.TryParse(applicant.JoinDate

        //lblID           .Text = row.EmployeeID;
        //lblName         .Text = row.IsNameNull()?           "-":row.Name;
        //lblDepartment   .Text = row.IsDepartmentNull()?     "-":row.Department;
        //lblCollege      .Text = row.IsCollegeNull()?        "-":row.College;
        //lblPhone        .Text = row.IsPhoneNull()?          "-":row.Phone;
        //lblEmail        .Text = row.IsKFUPMUserIDNull()?    "-":row.KFUPMUserID;     
        //lblPOBox        .Text = row.IsPOBoxNull()?          "-":row.POBox;
        //lblRank         .Text = row.IsRankNull()?           "-":row.Rank;
        //lblJoinDate     .Text = row.IsJoinDateNull()?       "-":DateTime.Parse(row.JoinDate).ToShortDateString();

        //PromotionTableAdapters.ApplicationTableAdapter appAdapter = new PromotionTableAdapters.ApplicationTableAdapter();
        //Promotion.ApplicationRow application = appAdapter.GetApplication(applicationID)[0];
        
        lblToRank.Text = bal.GetApplication(applicationID)[0].ForRank; ;
        if(bal.GetApplication(applicationID)[0].StartDate != null)
        {
            lblStartDate.Text = bal.GetApplication(applicationID)[0].StartDate.ToString();
        }
        return true;
    }
}