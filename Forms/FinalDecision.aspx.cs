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

public partial class Forms_FinalDecision : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        Instruction1.Text = Master.CurrentFormInstruction;

        FinalDecision1.LoadComments(Master.ApplicationID);
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
    protected void ButtonSaveSC_Click(object sender, EventArgs e)
    {
        bool success = FinalDecision1.SaveComments(Master.ApplicationID);
        if (success)
            ShowMessage("Saved Successfully");
        else
            ShowError("Please make sure you input all the fields.");
  
    }
}
