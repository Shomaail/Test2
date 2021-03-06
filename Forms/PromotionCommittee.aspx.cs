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
using System.Collections.Generic;
using System.Linq;
using BL.Data;

public partial class Forms_PromotionCommittee : System.Web.UI.Page
{
    BAL bal = new BAL();
    private enum Mode { InputByCollege = 1, VRGSSRInitialApproval = 9}
    private enum MethodCallMode { PageLoad = 1, EventDriven = 2 }
    public override string StyleSheetTheme
    {
        get
        {
            return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack) return;
            LoadCommittee(Master.ApplicationID, 'c');
            if (Master.CurrentFormLevel == -1)
            {
                Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
                return;
            }

            Instruction1.Text = Master.CurrentFormInstruction + " <span style=\"color: #FF5050\"><b>Please dont select any co-author</b></span>";
            SwitchMode((Mode)Master.CurrentFormLevel);

            //hide buttons when printing
            if (Utils.IsPrintMode())
            {
                Instruction1.Visible = false;
                CollegeFaculty1.Visible = false;
            }
        }
        catch (Exception)
        {
            
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            return;
        }

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
                divSuggestedPC.Disabled = false;
                break;
            case Mode.VRGSSRInitialApproval:
                divSuggestedPC.Disabled = true;
                break;
        }
    }

    public void LoadCommittee(int applicationID, char type)
    {
        List<Form_PromotionCommittee> lfpc = bal.GetForm_PromotionCommittee(applicationID, type.ToString());
        if (lfpc.Count == 0)
            return ;
        for (int i = 0; i < lfpc.Count; i++)
        {
            (divSuggestedPC.FindControl("tbSugPC" + (lfpc[i].Position + 1) + "EmailID") as TextBox).Text = lfpc[i].Email.Remove(lfpc[i].Email.IndexOf('@'));
            LoadEmployee(lfpc[i].Position + 1, (int) MethodCallMode.PageLoad );
        }
        if (lfpc.Count == 10)
            ShowMessage("Ten names for Suggested Promotion Committee is Completed");
        else
            ShowError("Ten entries of suggested promotion committee Members is not complete"); 
    }

    protected void tbSugPC1EmailID_TextChanged(object sender, EventArgs e)
    {
        LoadEmployee(1, (int)MethodCallMode.EventDriven);
    }
    protected void tbSugPC2EmailID_TextChanged(object sender, EventArgs e)
    {
        LoadEmployee(2, (int)MethodCallMode.EventDriven);
    }
    protected void tbSugPC3EmailID_TextChanged(object sender, EventArgs e)
    {
        LoadEmployee(3, (int)MethodCallMode.EventDriven);
    }
    protected void tbSugPC4EmailID_TextChanged(object sender, EventArgs e)
    {
        LoadEmployee(4, (int)MethodCallMode.EventDriven);
    }
    protected void tbSugPC5EmailID_TextChanged(object sender, EventArgs e)
    {
        LoadEmployee(5, (int)MethodCallMode.EventDriven);
    }
    protected void tbSugPC6EmailID_TextChanged(object sender, EventArgs e)
    {
        LoadEmployee(6, (int)MethodCallMode.EventDriven);
    }
    protected void tbSugPC7EmailID_TextChanged(object sender, EventArgs e)
    {
        LoadEmployee(7, (int)MethodCallMode.EventDriven);
    }
    protected void tbSugPC8EmailID_TextChanged(object sender, EventArgs e)
    {
        LoadEmployee(8, (int)MethodCallMode.EventDriven);
    }
    protected void tbSugPC9EmailID_TextChanged(object sender, EventArgs e)
    {
        LoadEmployee(9, (int)MethodCallMode.EventDriven);
    }
    protected void tbSugPC10EmailID_TextChanged(object sender, EventArgs e)
    {
        LoadEmployee(10, (int)MethodCallMode.EventDriven);
    }

    private void LoadEmployee(int rowNumber, int methodCallMode)
    {
        string EmailID = (divSuggestedPC.FindControl("tbSugPC" + rowNumber + "EmailID") as TextBox).Text;
        EmailID = EmailID.Trim();
        string Email;
        if (EmailID.Contains(ConfigurationManager.AppSettings["OrganizationEmail1"]))
        {
            EmailID = EmailID.Replace(ConfigurationManager.AppSettings["OrganizationEmail1"], "");
        }
        Email = EmailID + ConfigurationManager.AppSettings["OrganizationEmail1"];

        
        if (Email.Trim().Length == 0)
        {
            (divSuggestedPC.FindControl("lblError" + rowNumber) as Label).Text = "";
            bal.DeleteFormPromotionCommittee(Master.ApplicationID, "c", (byte)(rowNumber - 1));
            PopulateLabels(rowNumber);
        }
        else
        {
            
            if (bal.GetEmployeeByEmail(Email).Count == 0)
            {
                (divSuggestedPC.FindControl("lblError" + rowNumber) as Label).Text = "Employee does not exist";
                bal.DeleteFormPromotionCommittee(Master.ApplicationID, "c", (byte)(rowNumber - 1));
                PopulateLabels(rowNumber);
            }
            else
            {
                Employee emp = bal.GetEmployeeByEmail(Email)[0];
                if (Email.ToLower() == emp.Email.ToLower() && methodCallMode == (int)MethodCallMode.EventDriven 
                    && (divSuggestedPC.FindControl("lblName" + rowNumber) as Label).Text != "")
                {
                    return;
                }
                else
                {
                    PopulateLabels(rowNumber, emp, methodCallMode);
                }
                
            }
        }
    }
    private void PopulateLabels(int rowNo, Employee emp, int methodCallMode)
    {
        int appID = int.Parse(Request.Params.Get("applicationID").ToString());
        string toRank = bal.GetApplication(appID)[0].ForRank;

        if (SuggestedPCChairmanFromDepartment(emp.EmployeeID, appID) && rowNo < 4/* Three suggested rows for Chairman*/)
        {
            (divSuggestedPC.FindControl("lblError" + rowNo) as Label).Text = "The Suggested Promotion Committee Chairman cannot be from the Candidates Department";
            PopulateLabels(rowNo);

            return;
        }
        if (!IsRankEligible(toRank, emp.Rank))
        {
            (divSuggestedPC.FindControl("lblError" + rowNo) as Label).Text = "Suggested Member Rank is less than the Candidate's Rank";
            return;
        }
        if (!IsInvolvedinApp(emp.EmployeeID, appID))
        {
            (divSuggestedPC.FindControl("lblError" + rowNo) as Label).Text = "The Promotion Committee Member cannot be the Candidate, Dept Chairman, Dean or Vice Rector";
            return;
        }
        if (methodCallMode == (int)MethodCallMode.PageLoad)
        {
            if (bal.GetForm_PromotionCommittee(appID, "c").Where(pc => pc.Name == emp.Name).Count() > 1)
            {
                (divSuggestedPC.FindControl("lblError" + rowNo) as Label).Text = "The name is already selected";
                bal.DeleteFormPromotionCommittee(Master.ApplicationID, "c", (byte)(rowNo - 1));
                return;
            }
        }
        else 
        {
            if (bal.GetForm_PromotionCommittee(appID, "c").Where(pc => pc.Name == emp.Name).Count() > 0)
            {
                (divSuggestedPC.FindControl("lblError" + rowNo) as Label).Text = "The name is already selected";
                bal.DeleteFormPromotionCommittee(Master.ApplicationID, "c", (byte)(rowNo - 1));
                return;
            }        
        }
        (divSuggestedPC.FindControl("lblError" + rowNo) as Label).Text = "";
   //     (divSuggestedPC.FindControl("tbSugPC"+ rowNo + "EmailID" + rowNo) as TextBox).Text = emp.Email.Replace(ConfigurationManager.AppSettings["OrganizationEmail1"], "");
        (divSuggestedPC.FindControl("lblName" + rowNo) as Label).Text = emp.Name;
        (divSuggestedPC.FindControl("lblRank" + rowNo) as Label).Text = emp.Rank;
        (divSuggestedPC.FindControl("lblDepartment" + rowNo) as Label).Text = emp.Department1.Name;
        (divSuggestedPC.FindControl("lblEmail" + rowNo) as Label).Text = emp.Email;
        if (bal.GetForm_PromotionCommitteeByPK(appID, "c", (byte)(rowNo - 1)).Count == 1)
        {
            bal.UpdateFormPromotionCommittee(appID, "c", (byte)(rowNo - 1), true, emp.EmployeeID, emp.Name, emp.Department1.Name, emp.Rank, emp.Email , "", "", emp.Phone,0);
        }
        else
            bal.InsertFormPromotionCommittee(appID, "c", (byte)(rowNo - 1), true, emp.EmployeeID, emp.Name, emp.Department1.Name, emp.Rank, emp.Email, "", "", emp.Phone,0);

        if (bal.GetForm_PromotionCommittee(appID, "c").Count == 10 && methodCallMode == (int) MethodCallMode.EventDriven)
            ShowMessage("Ten names for Suggested Promotion Committee is Completed");
        else
            ShowError("Ten entries of suggested promotion committee Members is not complete");
    }
    private void PopulateLabels(int i)
    {
        (divSuggestedPC.FindControl("lblName" + i) as Label).Text = "";
        (divSuggestedPC.FindControl("lblRank" + i) as Label).Text = "";
        (divSuggestedPC.FindControl("lblDepartment" + i) as Label).Text = "";
        (divSuggestedPC.FindControl("lblEmail" + i) as Label).Text = "";
    }
    private bool SuggestedPCChairmanFromDepartment(string PCChairEmpID, int appID)
    {

        if (bal.GetEmployeeByPK(PCChairEmpID)[0].Department1.Name == bal.GetEmployeeByPK(bal.GetApplication(appID)[0].EmployeeID)[0].Department1.Name)
            return true;
        else
            return false;
    }
    private bool IsRankEligible(string toRank, string p)
    {
        if (toRank == p)
            return true;
        else if (toRank == "Associate Professor" && p == "Professor")
            return true;
        else return false;
    }
    private bool IsInvolvedinApp(string EmployeeID, int appID)
    {

        foreach (Application_Role ar in bal.GetApplicationRole(appID))
        {
            if ((ar.RoleID == 1 || ar.RoleID == 2 || ar.RoleID == 4 || ar.RoleID == 5 || ar.RoleID == 6 || ar.RoleID == 8) && ar.EmployeeID == EmployeeID)
                return false;
        }

        return true;
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int rowNumber = Int32.Parse(((LinkButton)sender).ID.Replace("lbtnDelete", ""));
        bal.DeleteFormPromotionCommittee(Master.ApplicationID, "c", (byte)(rowNumber - 1));
        (divSuggestedPC.FindControl("tbSugPC" + rowNumber + "EmailID") as TextBox).Text = "";
        (divSuggestedPC.FindControl("lblError" + rowNumber) as Label).Text = "";
        PopulateLabels(rowNumber);
    }
}
