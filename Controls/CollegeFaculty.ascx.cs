using System;
using System.Linq;
using System.Web.UI.WebControls;
using BL.Data;
public partial class Controls_DepartmentFaculty : System.Web.UI.UserControl
{
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        ddlDept.DataSource = bal.GetDepartments(RecordStatus.Active.ToString()).Where(d => d.Type == DepartmentType.College.ToString()).Select(a => new { Text = a.NameString, Value = a.DepartmentID });
        ddlDept.DataBind();
        rptEmployee.DataSource = bal.GetEmployees().Where(emp => emp.DepartmentID != null && emp.Department1.ParentDeptID != null && emp.Department1.ParentDeptID.Value.ToString() == ddlDept.SelectedValue);
        rptEmployee.DataBind();
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        rptEmployee.DataSource = bal.GetEmployees().Where(emp => emp.DepartmentID != null && emp.Department1.ParentDeptID != null && emp.Department1.ParentDeptID.Value.ToString() == ddlDept.SelectedValue);
        rptEmployee.DataBind();
        //gvEmployee.DataSource = bal.GetEmployees().Where(emp => emp.DepartmentID != null && emp.Department1.ParentDeptID != null && emp.Department1.ParentDeptID.Value.ToString() == ddlDept.SelectedValue);
        //gvEmployee.DataBind();
    }
}
