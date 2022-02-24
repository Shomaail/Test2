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

public partial class Controls_ComposeEmail : System.Web.UI.UserControl
{
    public event EventHandler EmailSent;
    public event EventHandler EmailNotSent;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //    return;
    }
    public void LoadData(Email.ComposeEmailDataTable Emails)
    {
        GridViewEmail.DataSource = Emails;
        GridViewEmail.DataBind();


    }
    protected void ButtonSendEmail_Click(object sender, EventArgs e)
    {
        int appID = -1;
        string KFUPMUserID = "";
        try
        {
            appID = int.Parse(Request.Params.Get("applicationID").ToString());
        }
        catch
        {
            try
            {
                appID = int.Parse(Session["applicationID"].ToString());
            }
            catch
            {              

            }
        }
        try
        {
            HR.EmployeeRow employee = (HR.EmployeeRow)Session["user"];
            KFUPMUserID = employee.KFUPMUserID;
        }
        catch 
        {

        }
        
        foreach (GridViewRow row in GridViewEmail.Rows)
        {
            if (GridViewEmail.Rows.Count == 1)
                EmailBody = (row.FindControl("TextBoxBody") as TextBox).Text;
            Emailer.Send((row.FindControl("TextBoxTo") as TextBox).Text,
                (row.FindControl("TextBoxSubject") as TextBox).Text,
                (row.FindControl("TextBoxBody") as TextBox).Text, KFUPMUserID, appID);

        }
        if (EmailSent != null)
            EmailSent(this, e);
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {     
        if (EmailNotSent != null)
            EmailNotSent(this,e);           
    }
    public void SetReadOnly(bool status)
    {
        //txtKFUPMUserID.ReadOnly = status;
        //lblName.ReadOnly = status;
        //lblRank.ReadOnly = status;

        //button.Visible = !status;
    }
    private string emailBody;
    public string EmailBody
    {
        get
        {

            return emailBody;
        }
        set
        {
            emailBody = value;
        }
    }
}
