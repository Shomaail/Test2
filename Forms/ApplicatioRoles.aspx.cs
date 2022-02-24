using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Data;
public partial class Forms_ApplicatioRoles : System.Web.UI.Page
{
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(IsPostBack)
            {
                return;
            }
            else
            {
                DatabindControls();
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    private void DatabindControls()
    {
        divAddApplicationRole.DataBind();
        Instruction1.Text = Master.CurrentFormInstruction;
        ddlRole.DataSource = bal.GetRole().Where(a=>a.Title.Contains("Member")).Select(a => new { Text = a.Title + " (" + a.RoleID + ")", Value = a.RoleID });
        ddlRole.DataBind();
        List<byte> leri = bal.GetNameExclusion(Master.CurRoleID).Select(ne => ne.ExcludedRoleID).ToList();
        gvAppLicationRole.DataSource = bal.GetApplicationRole(Master.ApplicationID)
            .Where(ar => !leri.Contains(ar.RoleID)).OrderBy(ar => ar.RoleID);
        gvAppLicationRole.DataBind();
    }
    protected void gvAppLicationRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        byte roleID = byte.Parse(gvAppLicationRole.DataKeys[e.RowIndex].Values[1].ToString());
        bal.DeleteApplicationRoles(Master.ApplicationID,roleID);
        DatabindControls();
    }
    protected void gvAppLicationRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvAppLicationRole_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvAppLicationRole.EditIndex = e.NewEditIndex;
        DatabindControls();      
    }
    protected void gvAppLicationRole_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvAppLicationRole.EditIndex = -1;
        DatabindControls();
    }
    protected void gvAppLicationRole_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        List<Application_Role> la = bal.GetApplicationRole(Master.ApplicationID);
        GridViewRow row = gvAppLicationRole.Rows[e.RowIndex];
        Application_Role ar = la[row.DataItemIndex];
        ar.RoleID = byte.Parse(((Label)(row.FindControl("lblRoleID"))).Text.Trim());
        ar.EmployeeID = ((TextBox)row.FindControl("tbEmployeeIDEdit")).Text.Trim();
        if(ar.EmployeeID == "0")
        {
            string eEmpID = ((TextBox)row.FindControl("tbExternalEmployeeIDEdit")).Text.Trim();
            int eEmpIDInt = 0;
            if(int.TryParse(eEmpID,out  eEmpIDInt))
                {
              //  ar.ExternalEmployeeID = eEmpIDInt;
            }
            else
            {
                //ar.ExternalEmployeeID = 0;
            }            
        }       
        bal.UpdateApplicationRoles(Master.ApplicationID, ar.RoleID,ar.EmployeeID,0,ar.IsActing);
        gvAppLicationRole.EditIndex = -1;
        DatabindControls();
    }
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (ddlEmployeeType.SelectedValue == EmployeeType.Internal.ToString())
        {
            string employeeID = tbEmployeeIDAdd.Value;
            if (employeeID == "")
            {
                lblNameAdd.Text = "No Employee found";
                return;
            }
            if (bal.GetEmployeeByPK(employeeID).Count == 0)
            {
                lblNameAdd.Text = "No Employee found";
                return;
            }
            lblNameAdd.Text = bal.GetEmployeeByPK(employeeID)[0].NameString;
        }
        else
        {
            int eEmployeeID;
            if (!int.TryParse(tbEmployeeIDAdd.Value, out eEmployeeID) || eEmployeeID == 0)
            {
                lblNameAdd.Text = "No External Employee found";
                return;
            }
            if (bal.GetExternalEmployeeByPK(eEmployeeID).Count == 0)
            {
                lblNameAdd.Text = "No Exernal Employee found";
                return;
            }
            lblNameAdd.Text = bal.GetExternalEmployeeByPK(eEmployeeID)[0].NameString;
        }
    
    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        string employeeID = "0";
        int eEmployeeID = 0;
        if (ddlEmployeeType.SelectedValue == EmployeeType.Internal.ToString())
        {
            employeeID = tbEmployeeIDAdd.Value.Trim();
            if (employeeID == "")
            {
                labelProgrammaticPopup0.Text = "EmployeeID cannot be left empty!.";
                programmaticModalPopup0.Show();
                return;
            }
            if (bal.GetEmployeeByPK(employeeID).Count == 0)
            {
                labelProgrammaticPopup0.Text = "No Employee with this EmployeeID (" + employeeID + " ) exists .";
                programmaticModalPopup0.Show();
                return;
            }
          
        }
        else
        {
            if (!int.TryParse(tbEmployeeIDAdd.Value,out eEmployeeID) || eEmployeeID == 0)
            {
                lblNameAdd.Text = "No External Employee found";
                return;
            }
            if (bal.GetExternalEmployeeByPK(eEmployeeID).Count == 0)
            {
                lblNameAdd.Text = "No Exernal Employee found";
                return;
            }
        }
        tbEmployeeIDAdd.Value = "";
        lblNameAdd.Text = "";
        List<Application_Role> lar = bal.GetApplicationRole(Master.ApplicationID, byte.Parse(ddlRole.SelectedValue));
        if (lar.Count != 0)
        {
            //labelProgrammaticPopup0.Text = "An "
            //    + (lar[0].EmployeeID != "0" ? "Employee " + lar[0].Employee.NameString : "External Employee" + lar[0].ExternalEmployee.NameString)
            //    + " is already added in the promotion application for the role " + ddlRole.SelectedItem + ". Delete this entry in the Application Role to proceed with addition";
            labelProgrammaticPopup0.Text = "An "
    + (lar[0].EmployeeID != "0" ? "Employee " + lar[0].Employee.NameString : "External Employee" + "")
    + " is already added in the promotion application for the role " + ddlRole.SelectedItem + ". Delete this entry in the Application Role to proceed with addition.";

            programmaticModalPopup0.Show();

            return;
        }
        //bal.InsertApplicationRoles(Master.ApplicationID, byte.Parse(ddlRole.SelectedValue), employeeID, eEmployeeID);
        bal.InsertApplicationRoles(Master.ApplicationID, employeeID, byte.Parse(ddlRole.SelectedValue) );
        labelProgrammaticPopup0.Text = "Application Role Is added successfully";
        programmaticModalPopup0.Show();
        DatabindControls();

    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }

    protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblID.Text = "";
        lblNameAdd.Text = "";
        tbEmployeeIDAdd.Value = "";
        if (ddlEmployeeType.SelectedValue == "Internal")
        {
            lblID.Text = "Employee ID: ";
        }
        else
        {
            lblID.Text = "External Employee ID: ";
        }
        
    }
}