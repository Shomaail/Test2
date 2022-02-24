using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL.Data;
    public partial class Admin_Role : System.Web.UI.Page
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

        protected void lbtnAddRole_Click(object sender, EventArgs e)
        {
            string Title = tbTitleAdd.Value;
            if (Title == "")
            {
                return;
            }
            bal.InsertRole(tbTitleAdd.Value, byte.Parse(tbTypeAdd.Value));
            DatabindControls();
        }
        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }
        private void DatabindControls()
        {
            rptRole.DataSource = bal.GetRole();
            rptRole.DataBind();
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
            //item.FindControl("lblType").Visible = !isEdit;        
            //Toggle DDL

            //Toggle TextBoxes.
            item.FindControl("tbTitleEdit").Visible = isEdit;
            item.FindControl("tbTypeEdit").Visible = isEdit;


        }
        protected void rptRole_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            int roleID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Delete")
            {

                if (bal.GetRole().Count == 1)
                {
                    labelProgrammaticPopup0.Text = "There has to be atleast one Role. You cannot delete this Role";
                    programmaticModalPopup0.Show();
                    return;
                }
                bal.DeleteRole(roleID);
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
                string type = (e.Item.FindControl("tbTypeEdit") as HtmlInputControl).Value;
                bal.UpdateRole(roleID, title, byte.Parse(type));
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
            }
        }


    }
