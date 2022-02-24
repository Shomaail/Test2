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
using System.Data.SqlClient;
using System.Threading;
using BL.Data;
public partial class Controls_EmployeeInput : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void button_Click(object sender, EventArgs e)
    {
        if (chkKFUPM.Checked)
            LoadEmployee();
    }

    public bool LoadEmployee()
    {
        if (txtKFUPMUserID.Text.Trim().Length == 0)
            return false;
        try
        {
            HRTableAdapters.EmployeeTableAdapter adapter = new HRTableAdapters.EmployeeTableAdapter();

            HR.EmployeeDataTable table = adapter.GetEmployeeByEmail(txtKFUPMUserID.Text);

            if (table.Count == 0)
            {
                lblError.Text = "Employee Does Not Exist!";
                pnlError.Visible = true;

                //reset employee data
                lblName.Text = "";
                lblRank.Text = "";
                txtEmail.Text = "";
                txtMajor.Text = "";
                txtDepartment.Text = "";
                txtMailingAddress.Text = "";
                txtPhone.Text = "";
                hdnEmployeeID.Value = "";

                return false;
            }
            else
            {
                HR.EmployeeRow row = table[0];
                BAL bal = new BAL();
              //  System.Web.UI.Page page = (System.Web.UI.Page)this.Page;
               // MasterPage1 mp1 = (MasterPage1)page.Master;

                int appID = ((Forms)Page.Master).ApplicationID;
                //int appID = ((Page.Master)this.Page.Master).ApplicationID;
                string toRank = bal.GetApplication(appID)[0].ForRank;
                if (!IsRankEligible(toRank, row.IsRankNull() ? "" : row.Rank.ToString()))
                {
                    lblError.Text = "Suggested Member Rank is less than the Candidate's Rank";
                    pnlError.Visible = true;
                    return false;
                }
                if (!IsInvolvedinApp(row.EmployeeID,appID))
                {
                    lblError.Text = "The Promotion Committee Member cannot be the the Candidate, Dept Chairman, Dean or Vice Rector";
                    pnlError.Visible = true;
                    return false;
                }
                pnlError.Visible = false;
                /* populate record */
                lblName.Text = row.IsNameNull() ? "" : row.Name.ToString();
                lblRank.Text = row.IsRankNull() ? "" : row.Rank.ToString();
                txtEmail.Text = row.IsKFUPMUserIDNull() ? "" : row.KFUPMUserID + "@kfupm.edu.sa";
                txtDepartment.Text = row.IsDepartmentNull() ? "" : row.Department;
                hdnEmployeeID.Value = row.EmployeeID;
                txtPhone.Text = row.IsPhoneNull() ? "" : row.Phone.ToString();
                return true;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            return false;
        }
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
        BAL bal = new BAL();
        foreach (Application_Role ar in bal.GetApplicationRole(appID))
            {
                if ((ar.RoleID == 1 || ar.RoleID == 2 || ar.RoleID == 4 || ar.RoleID == 5 || ar.RoleID == 6 || ar.RoleID == 8) && ar.EmployeeID == EmployeeID)
                    return false;
            }
        return true;
    }

    public void SetReadOnly(bool status)
    {
        txtKFUPMUserID.ReadOnly = status;
        lblName.ReadOnly = status;
        lblRank.ReadOnly = status;

        button.Visible = !status;
    }

    protected void chkKFUPM_CheckedChanged(object sender, EventArgs e)
    {
        button.Visible = chkKFUPM.Checked;
        txtKFUPMUserID.Visible = chkKFUPM.Checked;

        lblName.ReadOnly = chkKFUPM.Checked;
        lblRank.ReadOnly = chkKFUPM.Checked;
        txtDepartment.ReadOnly = chkKFUPM.Checked;
        txtEmail.ReadOnly = chkKFUPM.Checked;
        txtMajor.ReadOnly = chkKFUPM.Checked;
        txtMailingAddress.Visible = !chkKFUPM.Checked;
        txtPhone.ReadOnly = chkKFUPM.Checked;

        lblName.BorderWidth = chkKFUPM.Checked ? new Unit(0, UnitType.Pixel) : new Unit(1, UnitType.Pixel);
        lblRank.BorderWidth = lblName.BorderWidth;
        txtDepartment.BorderWidth = lblName.BorderWidth;
        txtEmail.BorderWidth = lblName.BorderWidth;
        txtMajor.BorderWidth = lblName.BorderWidth;
        txtMailingAddress.BorderWidth = lblName.BorderWidth;
        txtPhone.BorderWidth = lblName.BorderWidth;

        pnlError.Visible = false;

        //reset employee data
        lblName.Text = "";
        lblRank.Text = "";
        txtEmail.Text = "";
        txtMajor.Text = "";
        txtDepartment.Text = "";
        txtMailingAddress.Text = "";
        txtPhone.Text = "";
    }

    public bool EnableNonKFUPM
    {
        get
        { return chkKFUPM.Visible; }
        set
        { chkKFUPM.Visible = value; }
    }

    public bool EnableFullDetails
    {
        get
        { return pnlFullDetails.Visible; }
        set
        { pnlFullDetails.Visible = value; }
    }

    public bool IsNonKFUPM
    {
        get
        { return !chkKFUPM.Checked; }
        set
        {
            chkKFUPM.Checked = !value;
            chkKFUPM_CheckedChanged(this, null);
        }
    }

    public bool IsValidEmployee
    {
        get
        { return LoadEmployee(); }
    }

    public string KFUPMUserID
    {
        get
        { return txtKFUPMUserID.Text; }
        set
        { txtKFUPMUserID.Text = value; }
    }
    public string EmployeeID
    {
        get
        { return hdnEmployeeID.Value; }
        set
        { hdnEmployeeID.Value = value; }
    }

    public string EmployeeName
    {
        get
        { return lblName.Text; }
        set
        { lblName.Text = value; }
    }


    public string EmployeeRank
    {
        get
        { return lblRank.Text; }
        set
        { lblRank.Text = value; }
    }


    /* Extra Fields */
    public string EmployeeDepartment
    {
        get
        { return txtDepartment.Text; }
        set
        { txtDepartment.Text = value; }
    }
    public string EmployeeEmail
    {
        get
        { return txtEmail.Text; }
        set
        { txtEmail.Text = value; }
    }

    public string EmployeeMailingAddress
    {
        get
        { return txtMailingAddress.Text; }
        set
        { txtMailingAddress.Text = value; }
    }

    public string EmployeeMajor
    {
        get
        { return txtMajor.Text; }
        set
        { txtMajor.Text = value; }
    }
    public string EmployeePhone
    {
        get
        { return txtPhone.Text; }
        set
        { txtPhone.Text = value; }
    }

}
