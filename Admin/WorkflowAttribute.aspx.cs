using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL.Data;

public partial class Admin_WorkflowAttribute : System.Web.UI.Page
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
        rptWorkflowAttribute.DataSource = bal.GetWorkflowAttribute();
        rptWorkflowAttribute.DataBind();
    }
   
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        string attribute = tbAttributeAdd.Value;
        string value = tbValueAdd.Value;

        tbAttributeAdd.Value = "";
        tbValueAdd.Value = "";
        bal.InsertWorkflowAttribute(attribute, value);
        DataBindControls();

    }
   
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }

   

    protected void rptWorkflowAttribute_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int attributeID = int.Parse(e.CommandArgument.ToString());
            if (bal.GetWorkflowAttribute().Count() == 1)
            {
                labelProgrammaticPopup0.Text = "There has to be atleast one attribute. You cannot delete this attribute";
                programmaticModalPopup0.Show();
                return;
            }
            //labelProgrammaticPopup0.Text = "Go to the GlobalAttribute enum specifier in the App_Code\\AllClasses.cs and delete this attribute as well in order to avoid error.";
            //programmaticModalPopup0.Show();
            bal.DeleteWorkflowAttribute(attributeID);
            DataBindControls();
        }
        else if (e.CommandName == "Edit")
        {
            //labelProgrammaticPopup0.Text = "If you Edit the Attribute then do the same modification in GlobalAttribute enum specifier in the App_Code\\AllClasses.cs in order to avoid error. While Editing the value will not cause any error.";
            //programmaticModalPopup0.Show();
            ToggleElements(e.Item, true);
        }
        else if (e.CommandName == "Update")
        {
            labelProgrammaticPopup0.Text = "Any Change in the Min or Max of a committee/council should be reflected in the Min and Max of the Corresponding Phase. Currently, Modification in the phase can be done by only Database Administrator";
            programmaticModalPopup0.Show();
            int attributeID = int.Parse(e.CommandArgument.ToString());
            string attribute = (e.Item.FindControl("tbAttributeEdit") as HtmlInputControl).Value;
            string value = (e.Item.FindControl("tbValueEdit") as HtmlInputControl).Value;            
            bal.UpdateWorkflowAttribute(attributeID,attribute,value);
            DataBindControls();
        }
        else if (e.CommandName == "Cancel")
        {
            DataBindControls();             
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
        item.FindControl("tbAttributeEdit").Visible = isEdit;
        item.FindControl("tbValueEdit").Visible = isEdit;

    }


}