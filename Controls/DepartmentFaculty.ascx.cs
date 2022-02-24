using System;
using System.Data;
using System.Linq;

public partial class Controls_DepartmentFaculty : System.Web.UI.UserControl
{
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        int appID = int.Parse(Request.QueryString["applicationID"].ToString());
        rptDeptFaculty.DataSource = bal.GetEmployees().Where(emp => emp.Department1.ParentDeptID == bal.GetApplicant(appID)[0].Department1.ParentDeptID);
        rptDeptFaculty.DataBind();
    }   
}
