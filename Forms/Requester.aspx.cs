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

public partial class Forms_Requester : System.Web.UI.Page
{
    public override string StyleSheetTheme
    {
        get
        {
            return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { return; }

        if (Master.CurrentFormLevel == -1)
        {        
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            return;
        }

        Instruction1.Text = Master.CurrentFormInstruction;
        divApplicant.DataBind();
        Applicant1.LoadApplicant(Master.ApplicationID);
        AreaOfSpecialization1.LoadAreaOfSp(Master.ApplicationID);
        if (Utils.IsPrintMode())
        {
            Instruction1.Visible = false;
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool success = AreaOfSpecialization1.SaveAreaOfSp(Master.ApplicationID);
        if (success)
            ShowMessage("Saved Successfully");
        else
            ShowError("Please make sure you input all the fields.");
    }

    private void ShowError(string message)
    {
        lblMessage.ForeColor = System.Drawing.Color.Red;
        lblMessage.Text = message;
        Master.ReportFailure(message);
    }

    private void ShowMessage(string message)
    {
        lblMessage.ForeColor = System.Drawing.Color.Blue;
        lblMessage.Text = message;
        Master.ReportSuccess();
    }
}
