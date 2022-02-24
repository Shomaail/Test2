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

public partial class Forms_Exclusion : System.Web.UI.Page
{
    private enum Mode { InputByApplicant = 1, View = 2 }

    public override string StyleSheetTheme
    {
        get
        {
            return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack) return;

        if (Master.CurrentFormLevel == -1)
        {
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            return;
        }


        Instruction1.Text = Master.CurrentFormInstruction;

        SwitchMode((Mode)Master.CurrentFormLevel);


        //hide buttons when printing
        if (Utils.IsPrintMode())
        {
            btnSave.Visible = false;
            Instruction1.Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool success = Exclusion1.SaveExclusion(Master.ApplicationID);
        if (success)
            ShowMessage("Saved Successfully");
        else
            ShowError("Please make sure you make a choice.");

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


    private void SwitchMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.InputByApplicant:
                Exclusion1.LoadExclusion(Master.ApplicationID);
                break;
            case Mode.View:
                Exclusion1.LoadExclusion(Master.ApplicationID);
                Exclusion1.SetReadonly(true);
                btnSave.Visible = false;
                break;
        }
    }
}
