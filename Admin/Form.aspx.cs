using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL.Data;
    public partial class Admin_Form : System.Web.UI.Page
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

            rptForm.DataSource = bal.GetForm();
            rptForm.DataBind();
        }
        protected void rptForm_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            byte formID = byte.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Delete")
            {

                if (bal.GetForm().Count == 1)
                {
                    labelProgrammaticPopup0.Text = "There has to be atleast one Form. You cannot delete this form";
                    programmaticModalPopup0.Show();
                    return;
                }
                bal.DeleteTask(formID);
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
                string page = (e.Item.FindControl("tbPageEdit") as HtmlInputControl).Value;
                DropDownList ddlAllowFAActionEdit = e.Item.FindControl("ddlAllowFAActionEdit") as DropDownList;
                bal.UpdateForm(formID, title, page, bool.Parse(ddlAllowFAActionEdit.SelectedValue));
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
                //ToggleElements(e.Item, false);
            }
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
            //item.FindControl("lblTitle").Visible = !isEdit;
            //item.FindControl("lblRemindAfter").Visible = !isEdit;
            //item.FindControl("lblRemindAgainAfter").Visible = !isEdit;
            //item.FindControl("lblCountry").Visible = !isEdit;
            //Toggle DDL

            item.FindControl("ddlAllowFAActionEdit").Visible = isEdit;

            //Toggle TextBoxes.
            item.FindControl("tbTitleEdit").Visible = isEdit;
            item.FindControl("tbPageEdit").Visible = isEdit;


        }

        protected void lbtnAddForm_Click(object sender, EventArgs e)
        {
            string Title = tbTitleAdd.Value;
            if (Title == "")
            {
                return;
            }
            bal.InsertForm(tbTitleAdd.Value, tbPageAdd.Value, bool.Parse(ddlAllowFAActionAdd.SelectedValue));
            DatabindControls();
        }
    }
