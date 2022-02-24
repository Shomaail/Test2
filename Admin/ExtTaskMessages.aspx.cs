using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI.HtmlControls;
using BL.Data;


public partial class Admin_ExtTaskMessage : System.Web.UI.Page
    {
        ExtRevBAL erbal = new ExtRevBAL();
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
            ddlTaskExternalAdd.DataSource = bal.GetTaskExt().Select(a => new { Text = a.Title + " (" + a.TaskID + ")", Value = a.TaskID });
            ddlTaskExternalAdd.DataBind();
            rpExtTaskMessage.DataSource = bal.GetExtTaskMessage();
            rpExtTaskMessage.DataBind();
        }


        protected void rpExtTaskMessage_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(';');
            int taskID = int.Parse(args[0]);
            string subject = args[1];
            if (e.CommandName == "Delete")
            {


                bal.DeleteExtTaskMessage(taskID, subject);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                //string NewSubject = (e.Item.FindControl("tbSubjectEdit") as HtmlInputControl).Value;
                string Message = (e.Item.FindControl("tbMessageEdit") as HtmlTextArea).Value;
                bal.UpdateTask_ExtMessages(taskID, subject, Message.Trim());
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
            //item.FindControl("lblSubject").Visible = !isEdit;
            ////item.FindControl("lblMessage").Visible = !isEdit;

            //item.FindControl("tbSubjectEdit").Visible = isEdit;
            (item.FindControl("tbMessageEdit") as HtmlTextArea).Disabled = !isEdit;


        }
        protected void lbtnAddExtTaskMessage_Click(object sender, EventArgs e)
        {
            string subject = tbSubjectAdd.Value;
            if (subject == "")
            {
                labelProgrammaticPopup0.Text = "Subject cannot be left empty!.";
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

            bal.InsertTask_ExtMessages(int.Parse(ddlTaskExternalAdd.SelectedValue), tbSubjectAdd.Value, tbMessageAdd.Value);
            DatabindControls();
        }
    }
