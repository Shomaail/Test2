using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ExternalMessages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (GridView1.SelectedIndex == -1) return;
        e.InputParameters[0] = GridView1.SelectedDataKey.Values[0];        

    }
    protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (GridView1.SelectedIndex == -1) return;
        e.InputParameters[2] = GridView1.SelectedDataKey.Values[0];
        GridView1.DataBind();
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
        GridView1.DataBind();
    }
}