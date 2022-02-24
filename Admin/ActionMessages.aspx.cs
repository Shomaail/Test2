using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI.HtmlControls;
using BL.Data;

    public partial class Admin_ActionMessage : System.Web.UI.Page
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
        private void DatabindControls()
        {
            ddlNextTaskIDAdd.DataSource = bal.GetTask().Select(a => new { Text = a.Title + " (" + a.TaskID + ")", Value = a.TaskID });
            ddlNextTaskIDAdd.DataBind();
            ddlActionIDAdd.DataSource = bal.GetActions(RecordStatus.Active.ToString()).Select(a => new { Text = a.Title + " (" + a.ActionID + ")", Value = a.ActionID });
            ddlActionIDAdd.DataBind();
            rpActionMessage.DataSource = bal.GetActionMessage();
            rpActionMessage.DataBind();
        }


        protected void rpActionMessage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(';');
            int actionID = int.Parse(args[0]);
            int nextTaskID = int.Parse(args[1]);
            if (e.CommandName == "Delete")
            {
                bal.DeleteActionMessage(actionID, nextTaskID);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                string Message = (e.Item.FindControl("tbMessageEdit") as HtmlTextArea).Value;
                if (Message == "")
                {
                    lblProgrammaticPopup0.Text = "Message cannot be left empty!.";
                    programmaticModalPopup0.Show();
                    return;
                }
                bal.UpdateActionMessage(actionID, nextTaskID, Message);
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
            }

        }

        protected void lbtnAddActionMessage_Click(object sender, EventArgs e)
        {
            string message = tbMessageAdd.Value;
            if (message == "")
            {
                lblProgrammaticPopup0.Text = "Message cannot be left empty!.";
                programmaticModalPopup0.Show();
                return;
            }
            //if (bal.GetExternalEmployeeByEmail(email).Count == 0)
            //{
            //    labelProgrammaticPopup0.Text = "No Employee with this EmployeeID (" + employeeID + " ) exists .";
            //    programmaticModalPopup0.Show();
            //    return;
            //}
            //if (bal.gettask(email).Count != 0)
            //{
            //    labelProgrammaticPopup0.Text = "The External Employee with the Email (" + email + " ) is already present.";
            //    programmaticModalPopup0.Show();
            //    return;
            //}

            bal.InsertActionMessage(int.Parse(ddlActionIDAdd.SelectedValue), int.Parse(ddlNextTaskIDAdd.SelectedValue), tbMessageAdd.Value);
            DatabindControls();
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
            //item.FindControl("lblSubject").Visible = !isEdit;
            ////item.FindControl("lblMessage").Visible = !isEdit;

            (item.FindControl("tbMessageEdit") as HtmlTextArea).Disabled = !isEdit;
            item.FindControl("divVariables").Visible = isEdit;
            //item.FindControl("tbMessageEdit").Visible = isEdit;


        }
    }
