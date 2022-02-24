using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL.Data;

    public partial class Admin_ManageAdmins : System.Web.UI.Page
    {
        BAL bal = new BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            DataBindControls();
        }
        private void DataBindControls()
        {
            rptAdmin.DataSource = bal.GetAdmin();
            rptAdmin.DataBind();
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            string employeeID = tbEmployeeIDAdd.Value;
            if (!IsEmployeeIDOK(employeeID))
            {
                return;
            }
            tbEmployeeIDAdd.Value = "";
            lblNameAdd.Text = "";
            bal.InsertAdmin(employeeID);
            DataBindControls();

        }
        private bool IsEmployeeIDOK(string employeeID)
        {
            if (employeeID == "")
            {
                labelProgrammaticPopup0.Text = "EmployeeID cannot be left empty!.";
                programmaticModalPopup0.Show();
                return false;
            }
            if (bal.GetEmployeeByPK(employeeID).Count == 0)
            {
                labelProgrammaticPopup0.Text = "No Employee with this EmployeeID (" + employeeID + " ) exists .";
                programmaticModalPopup0.Show();
                return false;
            }
            if (bal.GetAdmin().Where(ad => ad.EmployeeID == employeeID).Count() != 0)
            {
                labelProgrammaticPopup0.Text = "The Employee with the EmployeeID (" + employeeID + " ) is already an admin.";
                programmaticModalPopup0.Show();
                return false;
            }
            return true;

        }
        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {

            string employeeID = tbEmployeeIDAdd.Value;
            if (employeeID == "")
            {
                lblNameAdd.Text = "No Employee found";
                return;
            }
            if (!employeeID.Contains('@'))
            {
                employeeID = employeeID.PadLeft(8, '0');
                employeeID = "@" + employeeID;
                tbEmployeeIDAdd.Value = employeeID;
            }
            if (bal.GetEmployeeByPK(employeeID).Count == 0)
            {
                lblNameAdd.Text = "No Employee found";
                return;
            }
            lblNameAdd.Text = bal.GetEmployeeByPK(employeeID)[0].NameString;
        }

        protected void rptAdmin_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int adminID = int.Parse(e.CommandArgument.ToString());
                if (bal.GetAdmin().Count() == 1)
                {
                    labelProgrammaticPopup0.Text = "There has to be atleast one admin. You cannot delete this admin";
                    programmaticModalPopup0.Show();
                    return;
                }
                bal.DeleteAdmin(adminID);
                DataBindControls();
            }
            else if (e.CommandName == "Edit")
            {
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                int adminID = int.Parse(e.CommandArgument.ToString());
                string empID = (e.Item.FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value;
                if (!IsEmployeeIDOK(empID))
                {
                    return;
                }
                bal.UpdateAdmin(adminID, empID);
                DataBindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DataBindControls();
                //ToggleElements(e.Item, false);
            }

        }
        private void ToggleElements(RepeaterItem item, bool isEdit)
        {
            //Toggle Buttons.
            item.FindControl("lbtnEdit").Visible = !isEdit;
            item.FindControl("lbtnUpdate").Visible = isEdit;
            item.FindControl("lbtnCancel").Visible = isEdit;
            item.FindControl("lbtnDelete").Visible = !isEdit;

            //Toggle Labels.
            //item.FindControl("lblContactName").Visible = !isEdit;
            //item.FindControl("lblCountry").Visible = !isEdit;

            //Toggle TextBoxes.
            item.FindControl("tbEmployeeIDEdit").Visible = isEdit;

        }
    }
