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

public partial class Admin_Messages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (GridView1.SelectedIndex == -1) return;
        e.InputParameters[0] = GridView1.SelectedDataKey.Values[0];
        e.InputParameters[1] = GridView1.SelectedDataKey.Values[1];

    }
    protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (GridView1.SelectedIndex == -1) return;
        e.InputParameters[1] = GridView1.SelectedDataKey.Values[0];
        e.InputParameters[2] = GridView1.SelectedDataKey.Values[1];
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
            row.Visible = false;
        GridView1.SelectedRow.Visible = true;
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
            row.Visible = true;
        GridView1.SelectedIndex = -1;
    }
}
