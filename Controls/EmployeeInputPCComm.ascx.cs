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
                lblEmployeeID.Text= "";
                txtPhone.Text = "";

                return false;
            }
            else
            {
                pnlError.Visible = false;

                HR.EmployeeRow row = table[0];

                /* populate record */
                lblName.Text = row.IsNameNull() ? "" : row.Name.ToString();
                lblRank.Text = row.IsRankNull() ? "" : row.Rank.ToString();
                txtEmail.Text = row.IsKFUPMUserIDNull() ? "" : row.KFUPMUserID + "@kfupm.edu.sa";
                txtDepartment.Text = row.IsDepartmentNull() ? "" : row.Department;
                lblEmployeeID.Text= row.EmployeeID;
                txtPhone.Text = row.IsPhoneNull() ? "" : row.Phone;

                return true;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            return false;
        }
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
        { return lblEmployeeID.Text; }
        set
        { lblEmployeeID.Text = value; }
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
