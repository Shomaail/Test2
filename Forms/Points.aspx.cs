using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using BL.Data;
using System.Linq;

public partial class Forms_Points : System.Web.UI.Page
{
    //public override string StyleSheetTheme
    //{
    //    get
    //    {
    //        return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
    //    }
    //}
    private enum Mode { InputByApp = 1, ViewApp = 2, InputByDept = 3, ViewAppDept = 4, ViewDeptWC = 5, ViewDept = 6 }
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) { return; }

        if (Master.CurrentFormLevel == -1)
        {
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            return;
        }

        try
        {
            Instruction1.Text = Master.CurrentFormInstruction;
            DatabindControls();
            //hide buttons when printing
            if (Utils.IsPrintMode())
            {
                btnSave.Visible = false;
                Instruction1.Visible = false;
            }
        }
        catch (Exception ex1)
        {
            
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            return;
        }
    }
    private void DatabindControls()
    {
        SwitchMode((Mode)Master.CurrentFormLevel);
        if (Master.CurRoleID == (int)RoleID.DepartmentChairman)
        {
            List<Application_TaskLog> latl = bal.GetAppTaskLog(Master.ApplicationID)
                    .Where(a => a.TaskID == (int)TaskIDs.Department_Approval &&
                    (a.ActionID == (int)ActionID.Verify_and_Return_to_the_Department_Chairman
                    || a.ActionID == (int)ActionID.Not_Recommended_and_Return_to_Department)).ToList();
            if (latl.Count > 0
               && latl.Last().ActionID == (int)ActionID.Verify_and_Return_to_the_Department_Chairman)
            {
                ShowMessage("The Points are approved by Departmental Committee");
            }
            else
            {
                ShowError("The Departmental Committee did not approve.");
            }
        }

        divTopOfFormMsgFailure.DataBind();
        lblMessageFailure.DataBind();
        divTopOfFormMsgSuccess.DataBind();
        lblMessageSuccess.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool success = Points1.SavePoints(Master.ApplicationID, "A");
        if (success)
        {
            ShowMessage("Saved Successfully");
        }
        else
        {
            ShowError("Please Make sure you input all the fields.");
        }
            
    }

    protected void btnSaveDept_Click(object sender, EventArgs e)
    {
        bool success = Points2.SavePoints(Master.ApplicationID, "D");
        if (success)
        {
            ShowMessage("Saved Successfully");
        } 
            
        else
        {
            ShowError("Please make sure you input all the fields.");
        }
        PointCorrections1.SavePointCorrections(Master.ApplicationID);

    }

    private void ShowError(string message)
    {
        
        Master.ReportFailure(message);
    }

    private void ShowMessage(string message)
    {
        
        Master.ReportSuccess();
    }

    private void SwitchMode(Mode mode)
    {
        //Points1.SetTabIndex(100);
        //Points2.SetTabIndex(200);

        switch (mode)
        {
            case Mode.InputByApp:
                Points1.LoadPoints(Master.ApplicationID, "A");
                pnlDepartment.Visible = false;
                break;
            case Mode.InputByDept:
                Points1.LoadPoints(Master.ApplicationID, "A");
                Points1.SetReadOnly(true);
                Points2.LoadPoints(Master.ApplicationID, "D");
                PointCorrections1.LoadPointCorrections(Master.ApplicationID);
                btnSave.Visible = false;
                break;
            case Mode.ViewApp:
                Points1.LoadPoints(Master.ApplicationID, "A");
                Points1.SetReadOnly(true);
                btnSave.Visible = false;
                pnlDepartment.Visible = false;
                break;
            case Mode.ViewAppDept:
                Points1.LoadPoints(Master.ApplicationID, "A");
                Points1.SetReadOnly(true);
                Points2.LoadPoints(Master.ApplicationID, "D");
                Points2.SetReadOnly(true);
                PointCorrections1.LoadPointCorrections(Master.ApplicationID);
                PointCorrections1.SetReadOnly(true);
                btnSave.Visible = false;
                btnSaveDept.Visible = false;
                break;
            case Mode.ViewDeptWC:
                Points2.LoadPoints(Master.ApplicationID, "D");
                Points2.SetReadOnly(true);
                PointCorrections1.LoadPointCorrections(Master.ApplicationID);
                PointCorrections1.SetReadOnly(true);
                btnSaveDept.Visible = false;
                pnlApplicant.Visible = false;
                break;
            case Mode.ViewDept:
                Points2.LoadPoints(Master.ApplicationID, "D");
                Points2.SetReadOnly(true);
                PointCorrections1.Visible = false;
                btnSaveDept.Visible = false;
                pnlApplicant.Visible = false;
                break;
        }
    }
    #region Get Methods 
    public bool GetAppTaskFormCompleted()
    {
        if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0).Count > 0)
        {
            return bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0)[0].Completed;
        }
        else
        {
            return false;
        }
    }
    public string GetAppTaskFormMessage()
    {
        if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0).Count > 0)
        {
            return bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0)[0].Message;
        }
        else
        {
            return "";
        }

    }
    #endregion
}
