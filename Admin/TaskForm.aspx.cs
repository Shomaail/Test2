using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL.Data;

    public partial class Admin_TaskForm : System.Web.UI.Page
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

        protected void rptTaskForm_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(';');
            int taskID = int.Parse(args[0]);
            byte formID = byte.Parse(args[1]);
            bool taskExternal = bool.Parse(args[2]);

            if (e.CommandName == "Delete")
            {
                bal.DeleteTaskForm(taskID, formID, taskExternal);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                string instruction = (e.Item.FindControl("tbInstructionEdit") as HtmlTextArea).Value;
                TaskForm tf = bal.GetTaskForm(taskID, formID, taskExternal)[0];
                bal.UpdateTaskForm(tf.TaskID, tf.FormID, tf.TaskExternal, tf.Rank, tf.Level, instruction, tf.Checkable);
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
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

            //item.FindControl("lblCountry").Visible = !isEdit;
            //Toggle DDL

            //item.FindControl("ddlActionRequiredEdit").Visible = isEdit;

            //Toggle TextBoxes.
            (item.FindControl("tbInstructionEdit") as HtmlTextArea).Disabled = !isEdit;
            // item.FindControl("tbRemindAgainAfterEdit").Visible = isEdit;

        }
        private void DatabindControls()
        {
            rptTaskForm.DataSource = bal.GetTaskForm().OrderBy(p => p.TaskID);
            rptTaskForm.DataBind();
        }
    }
