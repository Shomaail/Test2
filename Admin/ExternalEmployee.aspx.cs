using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Linq;
using System.Configuration;
using System.Web.UI.HtmlControls;
using BL.Data;

    public partial class Admin_ExternalEmployee : System.Web.UI.Page
    {
        BAL bal = new BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            DatabindControls();

        }

        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            RepeaterItem rItem = (sender as LinkButton).Parent as RepeaterItem;
            int eEmpID = int.Parse((rItem.FindControl("lblExternalEmployeeID") as Label).Text);
            if (bal.GetApplicationRole("0", eEmpID).Count() > 0)
            {
                labelProgrammaticPopup0.Text = "You cannot delete this external employee since he/she has involvement in one or more promotion applications.";
                programmaticModalPopup0.Show();
                return;
            }
            bal.DeleteExternalEmployee(eEmpID);
            DatabindControls();
        }
        private void DatabindControls()
        {
            rptExternalEmployee.DataSource = bal.GetExternalEmployee().Where(a => a.ExternalEmployeeID != 0);
            rptExternalEmployee.DataBind();
        }
        protected void rptExternalEmployee_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int extEmpID = int.Parse(e.CommandArgument.ToString());
                if (bal.GetExternalEmployee().Count() == 1)
                {
                    labelProgrammaticPopup0.Text = "There has to be atleast one external employee. You cannot delete this External Employee";
                    programmaticModalPopup0.Show();
                    return;
                }
                bal.DeleteExternalEmployee(extEmpID);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                int extEmpID = int.Parse(e.CommandArgument.ToString());
                //  string Title        = (e.Item.FindControl("tbTitleEdit") as HtmlInputControl).Value;
                //string Name         = (e.Item.FindControl("tbNameEdit") as HtmlInputControl).Value;
                string Rank = (e.Item.FindControl("tbRankEdit") as HtmlInputControl).Value;
                string Department = (e.Item.FindControl("tbDepartmentEdit") as HtmlInputControl).Value;
                string Organization = (e.Item.FindControl("tbOrganizationEdit") as HtmlInputControl).Value;
                string Address = (e.Item.FindControl("tbAddressEdit") as HtmlInputControl).Value;
                string Email = (e.Item.FindControl("tbEmailEdit") as HtmlInputControl).Value;
                string Email2 = (e.Item.FindControl("tbEmail2Edit") as HtmlInputControl).Value;
                //string Phone        = (e.Item.FindControl("tbPhoneEdit") as HtmlInputControl).Value;
                string Mobile = (e.Item.FindControl("tbMobileEdit") as HtmlInputControl).Value;
                string Country = (e.Item.FindControl("tbCountryEdit") as HtmlInputControl).Value;
                string NameString = (e.Item.FindControl("tbNameStringEdit") as HtmlInputControl).Value;
                //string Status       = (e.Item.FindControl("tbStatusEdit") as HtmlInputControl).Value;
                string Password = (e.Item.FindControl("tbPasswordEdit") as HtmlInputControl).Value;
                string Major = (e.Item.FindControl("tbMajorEdit") as HtmlInputControl).Value;
                string Website = (e.Item.FindControl("tbWebsiteEdit") as HtmlInputControl).Value;
                ExternalEmployee extE = bal.GetExternalEmployeeByPK(extEmpID)[0];
                bal.UpdateExternalEmployee(extEmpID, Title, extE.Name, Rank, Department, Organization,
                 Address, Email, Email2, extE.Phone, Mobile, Country, NameString,
                 extE.Status, Cryptography.HashPassword(Password), Major, Website);
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
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

            //item.FindControl("lblTitle").Visible = !isEdit;
            //item.FindControl("lblName").Visible = !isEdit;
            item.FindControl("lblRank").Visible = !isEdit;
            item.FindControl("lblDepartment").Visible = !isEdit;
            item.FindControl("lblOrganization").Visible = !isEdit;
            item.FindControl("lblAddress").Visible = !isEdit;
            item.FindControl("lblEmail").Visible = !isEdit;
            item.FindControl("lblEmail2").Visible = !isEdit;
            // item.FindControl("lblPhone").Visible = !isEdit;
            item.FindControl("lblMobile").Visible = !isEdit;
            // item.FindControl("lblCountry").Visible = !isEdit;
            item.FindControl("lblNameString").Visible = !isEdit;
            //       item.FindControl("lblStatus").Visible = !isEdit;
            item.FindControl("lblPassword").Visible = !isEdit;
            item.FindControl("lblMajor").Visible = !isEdit;
            item.FindControl("lblWebsite").Visible = !isEdit;
            //Toggle TextBoxes.
            //item.FindControl("tbTitleEdit").Visible = isEdit;
            //item.FindControl("tbNameEdit").Visible = isEdit;
            item.FindControl("tbRankEdit").Visible = isEdit;
            item.FindControl("tbDepartmentEdit").Visible = isEdit;
            item.FindControl("tbOrganizationEdit").Visible = isEdit;
            item.FindControl("tbAddressEdit").Visible = isEdit;
            item.FindControl("tbEmailEdit").Visible = isEdit;
            item.FindControl("tbEmail2Edit").Visible = isEdit;
            // item.FindControl("tbPhoneEdit").Visible = isEdit;
            item.FindControl("tbMobileEdit").Visible = isEdit;
            item.FindControl("tbCountryEdit").Visible = isEdit;
            item.FindControl("tbNameStringEdit").Visible = isEdit;
            //item.FindControl("tbStatusEdit").Visible = isEdit;
            item.FindControl("tbPasswordEdit").Visible = isEdit;
            item.FindControl("tbMajorEdit").Visible = isEdit;
            item.FindControl("tbWebsiteEdit").Visible = isEdit;

        }

        protected void lbtnAddExtEmployee_Click(object sender, EventArgs e)
        {

            string email = tbEmailAdd.Value;
            if (email == "")
            {
                labelProgrammaticPopup0.Text = "Email cannot be left empty!.";
                programmaticModalPopup0.Show();
                return;
            }
            //if (bal.GetExternalEmployeeByEmail(email).Count == 0)
            //{
            //    labelProgrammaticPopup0.Text = "No Employee with this EmployeeID (" + employeeID + " ) exists .";
            //    programmaticModalPopup0.Show();
            //    return;
            //}
            if (bal.GetExternalEmployeeByEmail(email).Count != 0)
            {
                labelProgrammaticPopup0.Text = "The External Employee with the Email (" + email + " ) is already present.";
                programmaticModalPopup0.Show();
                return;
            }

            bal.InsertExternalEmployee(tbTitleAdd.Value, tbNameAdd.Value, tbRankAdd.Value, tbDepartmentAdd.Value, tbOrganizationAdd.Value, tbAddressAdd.Value,
                tbEmailAdd.Value, tbEmail2Add.Value, tbPhoneAdd.Value, tbMobileAdd.Value, tbCountryAdd.Value, tbTitleAdd.Value + " " + tbNameAdd.Value, "Active",
                Cryptography.HashPassword(tbPasswordAdd.Value), tbMajorAdd.Value, tbWebsiteAdd.Value);
            DatabindControls();
        }

        protected void lbtnAddClient_Click(object sender, EventArgs e)
        {

        }


    }
