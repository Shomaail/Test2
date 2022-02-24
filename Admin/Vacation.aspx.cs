using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Admin_Vacation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddRecord")
        {
            SqlDataSourceReminderFreeze.InsertParameters["Description"].DefaultValue = ((TextBox)GridView1.FooterRow.FindControl("TextBoxDescription")).Text;
            SqlDataSourceReminderFreeze.InsertParameters["StartDate"].DefaultValue = ((Calendar)GridView1.FooterRow.FindControl("CalendarStartDate")).SelectedDate.ToString();
            SqlDataSourceReminderFreeze.InsertParameters["EndDate"].DefaultValue = ((Calendar)GridView1.FooterRow.FindControl("CalendarEndDate")).SelectedDate.ToString();
            SqlDataSourceReminderFreeze.Insert();
        }
        GridView1.DataBind();
    }


}
