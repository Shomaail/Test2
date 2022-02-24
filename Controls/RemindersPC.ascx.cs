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

public partial class Controls_RemindersPC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    public void LoadControl(int appID)
    {
        ApplicationID = appID;
        SqlDataSourceRemindersPC.SelectParameters["ApplicationID"].DefaultValue = ApplicationID.ToString();
        GridView1.DataBind();
    }
    public int ApplicationID
    {
        get
        {
            if (hdnApplicationID.Value.Length == 0)
                return -1;
            return int.Parse(hdnApplicationID.Value);
        }
        set
        {
            hdnApplicationID.Value = value.ToString();
        }
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DetailsView1.PageIndex = GridView1.SelectedIndex;
        DetailsView1.DataBind();
    }
    public bool CheckStatus(string ReminderStatus)
    {
        return bool.Parse(ReminderStatus);
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        ExternalRemindersTableAdapters.Application_TaskLogExtTableAdapter adapterAppTskLogExt = new ExternalRemindersTableAdapters.Application_TaskLogExtTableAdapter();
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox CheckBox1= row.FindControl("CheckBox1") as CheckBox;
            adapterAppTskLogExt.UpdateExtTskComplete(!CheckBox1.Checked, ApplicationID, 2, Int32.Parse(GridView1.DataKeys[row.RowIndex].Values[0].ToString()));

        }
    }
}
