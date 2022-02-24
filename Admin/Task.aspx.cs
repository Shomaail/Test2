using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL.Data;

    public partial class Admin_Task : System.Web.UI.Page
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
        private void DatabindControls()
        {

            ddlPhaseIDAdd.DataSource = bal.GetPhase().Select(a => new { Text = a.Title + " (" + a.PhaseID + ")", Value = a.PhaseID });
            ddlPhaseIDAdd.DataBind();
            ddlRoleIDAdd.DataSource = bal.GetRole().Select(a => new { Text = a.Title + " (" + a.RoleID + ")", Value = a.RoleID });
            ddlRoleIDAdd.DataBind();
            rptTask.DataSource = bal.GetTask();
            rptTask.DataBind();
        }

        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }


        private void ToggleElements(RepeaterItem item, bool isEdit)
        {
            //Toggle Buttons.
            item.FindControl("lbtnEdit").Visible = !isEdit;
            item.FindControl("lbtnUpdate").Visible = isEdit;
            item.FindControl("lbtnCancel").Visible = isEdit;
            item.FindControl("lbtnDelete").Visible = !isEdit;

            //Toggle Labels.
            item.FindControl("lblTitle").Visible = !isEdit;
            item.FindControl("lblRemindAfter").Visible = !isEdit;
            item.FindControl("lblRemindAgainAfter").Visible = !isEdit;
            //item.FindControl("lblCountry").Visible = !isEdit;
            //Toggle DDL
            item.FindControl("ddlRoleIDEdit").Visible = isEdit;
            item.FindControl("ddlPhaseIDEdit").Visible = isEdit;
            item.FindControl("ddlActionRequiredEdit").Visible = isEdit;

            //Toggle TextBoxes.
            item.FindControl("tbTitleEdit").Visible = isEdit;
            item.FindControl("tbRemindAfterEdit").Visible = isEdit;
            item.FindControl("tbRemindAgainAfterEdit").Visible = isEdit;

        }
        private bool IsEmployeeIDOK(string employeeID, int PermanentCommitteeTypeID)
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
            if (bal.GetPermanentCommittee().Where(ad => ad.EmployeeID == employeeID && ad.PermanentCommitteeTypeID == PermanentCommitteeTypeID).Count() != 0)
            {
                labelProgrammaticPopup0.Text = "The Employee with the EmployeeID (" + employeeID + " ) is already present in the committee.";
                programmaticModalPopup0.Show();
                return false;
            }
            return true;

        }
        protected void rptTask_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int taskID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Delete")
            {

                if (bal.GetTask().Count() == 1)
                {
                    labelProgrammaticPopup0.Text = "There has to be atleast one Task. You cannot delete this task";
                    programmaticModalPopup0.Show();
                    return;
                }
                bal.DeleteTask(taskID);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                //  (e.Item.FindControl("ddlCommitteeTypeEdit") as DropDownList).DataSource = bal.GetPermanentCommitteeType().Select(a => new { Text = a.Title + " (" + a.PermanentCommitteeTypeID + ")", Value = a.PermanentCommitteeTypeID });
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                string title = (e.Item.FindControl("tbTitleEdit") as HtmlInputControl).Value;
                string remindAfter = (e.Item.FindControl("tbRemindAfterEdit") as HtmlInputControl).Value;
                string remindAgainAfter = (e.Item.FindControl("tbRemindAgainAfterEdit") as HtmlInputControl).Value;
                DropDownList ddlRoleIDEdit = e.Item.FindControl("ddlRoleIDEdit") as DropDownList;
                DropDownList ddlPhaseIDEdit = e.Item.FindControl("ddlPhaseIDEdit") as DropDownList;
                DropDownList ddlActionRequiredEdit = e.Item.FindControl("ddlActionRequiredEdit") as DropDownList;
                bal.UpdateTask(taskID, title, byte.Parse(ddlPhaseIDEdit.SelectedValue), byte.Parse(ddlRoleIDEdit.SelectedValue), byte.Parse(remindAfter), byte.Parse(remindAgainAfter), bool.Parse(ddlActionRequired.SelectedValue));
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
                //ToggleElements(e.Item, false);
            }
        }




        protected void lbtnAddTask_Click(object sender, EventArgs e)
        {
            string Title = tbTitleAdd.Value;
            if (Title == "")
            {
                return;
            }
            bal.InsertTask(tbTitleAdd.Value, byte.Parse(ddlPhaseIDAdd.SelectedValue), byte.Parse(ddlRoleIDAdd.SelectedValue), byte.Parse(tbRemindAfterAdd.Value)
                , byte.Parse(tbRemindAgainAfterAdd.Value), bool.Parse(ddlActionRequired.SelectedValue));
            DatabindControls();
        }

        protected void ddlRoleIDEdit_DataBinding(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)(sender);
            ddl.DataSource = bal.GetRole().Select(a => new { Text = a.Title + " (" + a.RoleID + ")", Value = a.RoleID });

        }

        protected void ddlPhaseIDEdit_DataBinding(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)(sender);
            ddl.DataSource = bal.GetPhase().Select(a => new { Text = a.Title + " (" + a.PhaseID + ")", Value = a.PhaseID });
        }


    }
