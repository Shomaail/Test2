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

public partial class Admin_DepartmentManagers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
    }
    string temp = null;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string temp2 = e.Row.Cells[1].Text;
        if (e.Row.Cells[1].Text == temp)
            e.Row.Cells[1].Text = "";
        temp = temp2;
    }
    protected void SelectedManagerDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (GridView1.SelectedIndex != -1)
        {
            e.InputParameters["Department"] = GridView1.SelectedDataKey.Values[0];
            e.InputParameters["RoleID"] = GridView1.SelectedDataKey.Values[1];
            GridView1.Visible = false;
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DetailsView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.SelectedIndex = -1;
        GridView1.Visible = true;
        DataBind();
    }
    protected void SelectedManagerDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string kfupmID = ((TextBox)DetailsView1.Rows[5].FindControl("TextBox2")).Text;
        PromAdminTableAdapters.DepartmentManagersTableAdapter adapter = new PromAdminTableAdapters.DepartmentManagersTableAdapter();
        if (adapter.GetEmployeeID(kfupmID) == null)
        {
            Alert1.Message("This KFUPM email ID does not exist.");
        }
        e.InputParameters["EmployeeID"] = adapter.GetEmployeeID(kfupmID) == null ? GridView1.SelectedDataKey.Values[2]: adapter.GetEmployeeID(kfupmID) ;
        e.InputParameters["DeputyEmployeeID"] = ((TextBox)DetailsView1.Rows[6].FindControl("TextBox1")).Text;
        e.InputParameters["original_Department"] = GridView1.SelectedDataKey.Values[0];
        e.InputParameters["original_RoleID"] = GridView1.SelectedDataKey.Values[1];
        e.InputParameters["original_EmployeeID"] = GridView1.SelectedDataKey.Values[2];
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ShowEmailPopup")
        {
            string department = GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Values[0].ToString();
            string roleID =  GridView1.DataKeys[int.Parse(e.CommandArgument.ToString())].Values[1].ToString();
            Response.Write(@"<script language=javascript>
                    window.open ('EmailPopup.aspx?dept="+ department+"&roleID="+roleID + @"',null,'scrollbars=yes, status= no, resizable = yes, toolbar=no,location=no,height = 700, width = 900, left = 200, top= 200, screenx=10,screeny=600,menubar=no');                   
                    </script>");
        }
    }
}
