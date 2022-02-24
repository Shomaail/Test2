using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using BL.Data;

    public partial class Admin_Department : System.Web.UI.Page
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
            ddlParentDeptIDAdd.DataSource = bal.GetDepartments(RecordStatus.Active.ToString()).Select(a => new { Text = a.NameString + " (" + a.DepartmentID + ")", Value = a.DepartmentID });
            ddlParentDeptIDAdd.DataBind();
            ddlRoleIDAdd.DataSource = bal.GetRole().Select(a => new { Text = a.Title + " (" + a.RoleID + ")", Value = a.RoleID });
            ddlRoleIDAdd.DataBind();
            ddlTypeAdd.DataSource = bal.GetDepartments(RecordStatus.Active.ToString()).Select(a => new { Text = a.Type, Value = a.Type }).Distinct();
            ddlTypeAdd.DataBind();
            rpDepartment.DataSource = bal.GetDepartments("Active");
            rpDepartment.DataBind();
        }
        public string GetDeputy(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            if (bal.GetEmployeeByEmail(email).Count > 0)
            {
                return bal.GetEmployeeByEmail(email)[0].NameString;
            }
            else
            {
                return "";
            }

        }

        protected void rpDepartment_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(';');
            int DepartmentID = int.Parse(args[0]);
            //    int nextTaskID = int.Parse(args[1]);
            if (e.CommandName == "Delete")
            {

                if (bal.GetEmployees().Where(a => a.DepartmentID == DepartmentID).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = "You cannot delete this Department since there are employees associated with this Department.";
                    programmaticModalPopup0.Show();
                    return;
                }
                bal.DeleteDepartment(DepartmentID);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                foreach(RepeaterItem ri in rpDepartment.Items)
                {
                    ToggleElements(ri, false);
                }
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                Department dept = bal.GetDepartmentByPK(DepartmentID);

                string Name = (e.Item.FindControl("tbNameEdit") as HtmlInputControl).Value;
                string ShortName = (e.Item.FindControl("tbShortNameEdit") as HtmlInputControl).Value;
                string ParentDeptID = (e.Item.FindControl("ddlParentDeptIDEdit") as DropDownList).SelectedValue;
                string Phone = (e.Item.FindControl("tbPhoneEdit") as HtmlInputControl).Value;
                string Website = (e.Item.FindControl("tbWebsiteEdit") as HtmlInputControl).Value;
                string Fax = (e.Item.FindControl("tbFaxEdit") as HtmlInputControl).Value;
                string Type = (e.Item.FindControl("ddlTypeEdit") as DropDownList).SelectedValue;
                string HeadEmpID = (e.Item.FindControl("tbHeadEmpIDEdit") as HtmlInputControl).Value;
                string HeadRoleID = (e.Item.FindControl("ddlHeadRoleIDEdit") as DropDownList).SelectedValue;
                string Status = (e.Item.FindControl("ddlStatusEdit") as DropDownList).SelectedValue;
                string DeputyEmail = (e.Item.FindControl("tbDeputyEmailEdit") as HtmlInputControl).Value;
                string Deputy2Email = (e.Item.FindControl("tbDeputy2EmailEdit") as HtmlInputControl).Value;

                string HeadNewName = bal.GetEmployeeByPK(HeadEmpID)[0].NameString;
                string HeadOldName = bal.GetEmployeeByPK(dept.HeadEmpID)[0].NameString; 
                if (dept.HeadRoleID == (byte)RoleID.DepartmentChairman)
                {
                    foreach (Application_Role ar in bal.GetApplicationRole().Where(ar => ar.Application.Employee.DepartmentID == DepartmentID && !  ar.Application.ApplicationClosed))
                    {
                        if (ar.RoleID == dept.HeadRoleID)
                        {
                            bal.UpdateApplicationRoles(ar.ApplicationID, ar.RoleID, HeadEmpID, ar.ExternalEmployeeID, ar.IsActing);
                        }
                    }
                    foreach (Application_TaskLog atl in bal.GetAppTaskLog().Where(a => a.Application.Employee.DepartmentID == DepartmentID && ! a.Application.ApplicationClosed && a.Task.RoleID == (byte)RoleID.DepartmentChairman && !a.Completed))
                    {
                        bal.UpdateApplication_TaskLog("Dear " + HeadOldName, "Dear " + HeadNewName, atl.TaskLogID);
                    }
                }
                else if (dept.HeadRoleID == (byte)RoleID.College_Dean)
                {
                    foreach (Application_Role ar in bal.GetApplicationRole().Where(ar =>  ar.Application.Employee.Department1.ParentDeptID == DepartmentID && ! ar.Application.ApplicationClosed))
                    {
                        if (ar.RoleID == dept.HeadRoleID)
                        {
                            bal.UpdateApplicationRoles(ar.ApplicationID, ar.RoleID, HeadEmpID, ar.ExternalEmployeeID, ar.IsActing);
                        }
                    }
                    foreach (Application_TaskLog atl in bal.GetAppTaskLog().Where(a => a.Application.Employee.Department1.ParentDeptID == DepartmentID && ! a.Application.ApplicationClosed && a.Task.RoleID == (byte)RoleID.College_Dean && !a.Completed))
                    {
                        bal.UpdateApplication_TaskLog("Dear "+ HeadOldName, "Dear " + HeadNewName, atl.TaskLogID);
                    }                
                }
                if (dept.HeadRoleID == (byte)RoleID.TopLowAuthority)
                {
                    foreach (Application_Role ar in bal.GetApplicationRole().Where(ar => !ar.Application.ApplicationClosed))
                    {
                        bal.UpdateApplicationRoles(ar.ApplicationID, ar.RoleID, HeadEmpID, ar.ExternalEmployeeID, ar.IsActing);
                    }
                    foreach (Application_TaskLog atl in bal.GetAppTaskLog().Where(a => !a.Application.ApplicationClosed && a.Task.RoleID == (byte)RoleID.TopLowAuthority    && !a.Completed))
                    {
                        bal.UpdateApplication_TaskLog("Dear " + HeadOldName, "Dear " + HeadNewName, atl.TaskLogID);
                    }
                }
                if (dept.HeadRoleID == (byte) RoleID.TopAuthority)
                        {
                            foreach (Application_Role ar in bal.GetApplicationRole().Where(ar => !ar.Application.ApplicationClosed))
                            {
                                bal.UpdateApplicationRoles(ar.ApplicationID, ar.RoleID, HeadEmpID, ar.ExternalEmployeeID, ar.IsActing);
                            }
                foreach (Application_TaskLog atl in bal.GetAppTaskLog().Where(a => !a.Application.ApplicationClosed && a.Task.RoleID == (byte)RoleID.TopAuthority && !a.Completed))
                {
                    bal.UpdateApplication_TaskLog("Dear " + HeadOldName, "Dear " + HeadNewName, atl.TaskLogID);
                }
            }
            
            bal.UpdateDepartment(Name, ShortName, int.Parse(ParentDeptID), HeadEmpID, Phone, Website, Fax, byte.Parse(HeadRoleID), Type, Status, DeputyEmail, Deputy2Email, DepartmentID);
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

            item.FindControl("lblName").Visible = !isEdit;
            item.FindControl("lblShortName").Visible = !isEdit;
            item.FindControl("lblParentDept").Visible = !isEdit;
            item.FindControl("lblPhone").Visible = !isEdit;
            item.FindControl("lblWebsite").Visible = !isEdit;

            item.FindControl("lblFax").Visible = !isEdit;
            item.FindControl("lblType").Visible = !isEdit;
            item.FindControl("lblHeadRole").Visible = !isEdit;
            item.FindControl("lblDeputy").Visible = !isEdit;
            item.FindControl("lblDeputy2").Visible = !isEdit;


            //Toggle TextBoxes.
            item.FindControl("tbNameEdit").Visible = isEdit;
            item.FindControl("ddlStatusEdit").Visible = isEdit;
            item.FindControl("tbShortNameEdit").Visible = isEdit;
            item.FindControl("ddlParentDeptIDEdit").Visible = isEdit;


            item.FindControl("tbPhoneEdit").Visible = isEdit;
            item.FindControl("tbWebsiteEdit").Visible = isEdit;
            item.FindControl("tbFaxEdit").Visible = isEdit;
            item.FindControl("ddlTypeEdit").Visible = isEdit;
            item.FindControl("tbHeadEmpIDEdit").Visible = isEdit;
            item.FindControl("lbtnSearch").Visible = isEdit;
            item.FindControl("ddlHeadRoleIDEdit").Visible = isEdit;
            item.FindControl("tbDeputyEmailEdit").Visible = isEdit;
            item.FindControl("tbDeputy2EmailEdit").Visible = isEdit;

        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as RepeaterItem).ItemIndex;
            string employeeID = (rpDepartment.Items[rowIndex].FindControl("tbHeadEmpIDEdit") as HtmlInputControl).Value;
            if (employeeID == "")
            {
                (rpDepartment.Items[rowIndex].FindControl("lblNameEdit") as Label).Text = " No Employee found";                
                return;
            }
            //if (!employeeID.Contains('@'))
            //{
            //    employeeID = employeeID.PadLeft(8, '0');
            //    employeeID = "@" + employeeID;
            //    (rpDepartment.Items[rowIndex].FindControl("tbHeadEmpIDEdit") as HtmlInputControl).Value = employeeID;
            //}
            if (bal.GetEmployeeByPK(employeeID).Count == 0)
            {
                (rpDepartment.Items[rowIndex].FindControl("lblNameEdit") as Label).Text = " No Employee found";
                return;
            }
            (rpDepartment.Items[rowIndex].FindControl("lblNameEdit") as Label).Text = 
                " " + bal.GetEmployeeByPK(employeeID)[0].NameString 
                + " (" + bal.GetEmployeeByPK(employeeID)[0].Department1.ShortName + ")";
        }
        protected void lbtnAddDept_Click(object sender, EventArgs e)
        {
            string Name = tbNameAdd.Value;
            if (Name == "" || tbEmployeeIDAdd.Value == "")
            {
                return;
            }
            tbNameAdd.Value = "";
            lblNameAdd.Text = "";
            bal.InsertDepartment(tbNameAdd.Value, tbShortNameAdd.Value, int.Parse(ddlParentDeptIDAdd.SelectedValue),
                tbEmployeeIDAdd.Value, tbPhoneAdd.Value, tbWebsiteAdd.Value, tbFaxAdd.Value, byte.Parse(ddlRoleIDAdd.SelectedValue),
                ddlTypeAdd.SelectedValue, ddlStatusAdd.SelectedValue, tbDeputyEmailADD.Value, tbDeputy2EmailADD.Value);
            DatabindControls();
        }

        protected void ddlParentDeptIDEdit_DataBinding(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)(sender);

            ddl.DataSource = bal.GetDepartments(RecordStatus.Active.ToString())
                .Select(a => new { Text = a.NameString + " (" + a.DepartmentID + ")", Value = a.DepartmentID });
            ddl.Items.Add(new ListItem("No Dept", "0"));
        }

        protected void ddlTypeEdit_DataBinding(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)(sender);
            ddl.DataSource = bal.GetDepartments(RecordStatus.Active.ToString())
                .Select(a => new { Text = a.Type, Value = a.Type }).Distinct();
        }

        protected void tbEmployeeIDAdd_TextChanged(object sender, EventArgs e)
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

        protected void ddlHeadRoleIDEdit_DataBinding(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)(sender);
            ddl.DataSource = bal.GetRole().Select(a => new { Text = a.Title + " (" + a.RoleID + ")", Value = a.RoleID });

        }

    }
