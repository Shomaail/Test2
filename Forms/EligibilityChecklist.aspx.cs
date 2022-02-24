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

public partial class Forms_EligibilityChecklist : System.Web.UI.Page
{
    private enum Mode { InputByCollege = 4, InputBySCSubcomm = 9, InputByVRGSSR = 6 }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Instruction1.Text = Master.CurrentFormInstruction;
        CheckList1.Visible = true;
        CheckList1.ActivateEditMode();
        divEligibilityChecklist.DataBind();
        SwitchMode((Mode)Master.CurrentFormLevel);
        bool isPrintMode = Utils.IsPrintMode();

        //hide buttons when printing
        if (isPrintMode)
        {
            ButtonSave.Visible = false;
            Instruction1.Visible = false;
        }


    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        //bool success = CheckList1.update(Master.ApplicationID);
        bool success = CheckList1.update();
       //success = true;
        if (success)
            ShowMessage("Saved Successfully");
        else
            ShowError("Please Make sure you input all the fields.");
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
            case Mode.InputByCollege:
                //ApplicationCheckListTableAdapters.ApplicationChecklistTableAdapter appChkLstAdapter = new ApplicationCheckListTableAdapters.ApplicationChecklistTableAdapter();
                //if (appChkLstAdapter.GetDataByApplicationID(int.Parse(Request.Params.Get("applicationID").ToString())).Count == 0)
                //{
                //    lblMessage.Text = "The Eligibility checklist form cannot be shown. Points information from Departmental committee is missing.";
                //    ButtonSave.Visible = false;
                //}
                //else
                //{
                //    ButtonSave.Visible = true;
                //}
                ButtonSave.Visible = true;
                break;
            case Mode.InputBySCSubcomm:
                ButtonSave.Visible = true;
                break;
            case Mode.InputByVRGSSR:
                ButtonSave.Visible = true;
                break;
            default:
                ButtonSave.Visible = false;
                break;


        }
    }

}
