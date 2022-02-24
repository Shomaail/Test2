using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL.Data;



public partial class Admin_Task_Ext : System.Web.UI.Page
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
            ddlRoleAdd.DataSource = bal.GetRole().Select(a => new { Text = a.Title + " (" + a.RoleID + ")", Value = a.RoleID });
            ddlRoleAdd.DataBind();
            rptTask_Ext.DataSource = bal.GetTaskExt();
            rptTask_Ext.DataBind();
        }
        //public List<> GetDataSource()
        //{
        //    return (List<a>) bal.GetRole().Select(a => new { Text = a.Title + " (" + a.RoleID + ")", Value = a.RoleID });        
        //}
        protected void lbtnAddTask_Ext_Click(object sender, EventArgs e)
        {

            string subject = tbTitleAdd.Value;
            if (subject == "")
            {
                lblProgrammaticPopup0.Text = "Title cannot be left empty!.";
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

            bal.InsertTask_Ext(tbTitleAdd.Value, byte.Parse(tbRemindAfterAdd.Value), byte.Parse(tbRemindAgainAfterAdd.Value), byte.Parse(ddlRoleAdd.SelectedValue));
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
            item.FindControl("lblTitle").Visible = !isEdit;
            item.FindControl("lblRemindAfter").Visible = !isEdit;

            item.FindControl("lblRemindAgainAfter").Visible = !isEdit;
            item.FindControl("lblRoleID").Visible = !isEdit;
            //item.FindControl("lblSubject").Visible = !isEdit;
            //item.FindControl("lblMessage").Visible = !isEdit;

            item.FindControl("tbTitleEdit").Visible = isEdit;
            item.FindControl("tbRemindAfterEdit").Visible = isEdit;
            item.FindControl("tbRemindAgainAfterEdit").Visible = isEdit;
            item.FindControl("ddlRoleIDEdit").Visible = isEdit;
            // (item.FindControl("tbMessageEdit") as HtmlTextArea).Disabled = !isEdit;


        }
        protected void rptTask_Ext_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(';');
            int taskID = int.Parse(args[0]);
            //  string subject = args[1];
            if (e.CommandName == "Delete")
            {


                bal.DeleteTask_Ext(taskID);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                string title = (e.Item.FindControl("tbTitleEdit") as HtmlInputControl).Value;
                string remindAfter = (e.Item.FindControl("tbRemindAfterEdit") as HtmlInputControl).Value;
                string remindAgainAfter = (e.Item.FindControl("tbRemindAgainAfterEdit") as HtmlInputControl).Value;
                string roleID = (e.Item.FindControl("ddlRoleIDEdit") as DropDownList).SelectedValue;
                bal.UpdateTask_Ext(taskID, title, byte.Parse(remindAfter), byte.Parse(remindAgainAfter), byte.Parse(roleID));
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
            }
        }

        protected void ddlRoleIDEdit_DataBinding(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)(sender);
            ddl.DataSource = bal.GetRole().Select(a => new { Text = a.Title + " (" + a.RoleID + ")", Value = a.RoleID });
        }
    }
