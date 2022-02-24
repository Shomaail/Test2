using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text.RegularExpressions;
using BL.Data;


    public partial class Forms_DeptCommittee : System.Web.UI.Page
    {
        BAL bal = new BAL();
        private enum MethodCallMode { PageLoad = 1, EventDriven = 2 }
        private enum Mode { InputByDeptChair = 2, View = 3 }
        public override string StyleSheetTheme
        {
            get
            {
                return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                if (!(Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("ddlQuickJump")))
                {
                    return;
                }
            }
            DatabindControls();
            
            if (Master.CurrentFormLevel == -1)
            {
                Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
                return;
            }
            SwitchMode((Mode)Master.CurrentFormLevel);
            Instruction1.Text = Master.CurrentFormInstruction;
            //hide buttons when printing
            if (Utils.IsPrintMode())
            {
                Instruction1.Visible = false;
                //DepartmentFaculty.Visible = false;


            }
        }

        private void DatabindControls()
        {
            LoadCommittee(Master.ApplicationID);
            divTopOfFormMsgFailure.DataBind();
            lblMessageFailure.DataBind();
            divTopOfFormMsgSuccess.DataBind();
            lblMessageSuccess.DataBind();            
        }

        private void SwitchMode(Mode mode)
        {
            switch (mode)
            {
                case Mode.InputByDeptChair:
                    pnlDeptComm.Enabled = true;

                    break;
                case Mode.View:
                    pnlDeptComm.Enabled = false;
                    Instruction1.Visible = false;
                    lbtnDelete1.Visible = false;
                    lbtnDelete2.Visible = false;
                    lbtnDelete3.Visible = false;
                    lbtnDelete4.Visible = false;
                    lbtnDelete5.Visible = false;

                    break;
            }
        }

        private void ShowError(string message)
        {
            Master.ReportFailure(message);
        }

        private void ShowMessage(string message)
        {
            Master.ReportSuccess(message);
        }
        public void LoadCommittee(int applicationID)
        {
            List<Application_Role> lar = bal.GetApplicationRole(applicationID).Where(ar => ar.Role.Title.Contains("Departmental Committee")).ToList();

            if (lar.Count == 0)
            {
                List<Form_DepartmentCommittee> ldc = bal.GetForm_DepartmentCommittee(applicationID).OrderBy(a=>a.Position).ToList();
                if(ldc.Count == 0)
                {
                    return;
                }
                else
                {
                    int i = 0;
                    foreach (var item in ldc)
                    {
                        if(i == 0 )
                        {
                            bal.InsertApplicationRoles(applicationID, (byte)RoleID.Departmental_Committee_Chairman, item.EmployeeID, 0);
                            i++;
                            continue;
                        }
                        if(bal.GetRole("Departmental Committee Member " + i).Count == 1 )
                        {
                            bal.InsertApplicationRoles(applicationID, bal.GetRole("Departmental Committee Member " + i)[0].RoleID, item.EmployeeID, 0);                            
                        }
                        i++;
                    }
                    lar = bal.GetApplicationRole(applicationID).Where(ar => ar.Role.Title.Contains("Departmental Committee")).ToList();
                    if(lar.Count == 0)
                    {
                        return;
                    }
            }
        }

            for (int i = 0; i < 5; i++)
            {
                if (i < lar.Count)
                {
                    if (!string.IsNullOrEmpty(lar[i].Employee.Email))
                    {
                        (pnlDeptComm.FindControl("tbDC" + (i + 1) + "Email") as TextBox).Text = lar[i].Employee.Email;
                    }
                    if (!string.IsNullOrEmpty(lar[i].ExternalEmployee.Email))
                    {
                        (pnlDeptComm.FindControl("tbDC" + (i + 1) + "Email") as TextBox).Text = lar[i].ExternalEmployee.Email;
                    }
                }
                else
                {
                    (pnlDeptComm.FindControl("tbDC" + (i + 1) + "Email") as TextBox).Text = "";
                }
                LoadEmployee(i + 1, (int)MethodCallMode.PageLoad);
            }
            CheckFormCompletion(applicationID);
        }
        #region  Event Handlers
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            int rowNumber = int.Parse(((LinkButton)sender).ID.Replace("lbtnDelete", ""));
            bal.DeleteApplicationRoles(Master.ApplicationID, GetDeptCommRoleID(rowNumber));
            (pnlDeptComm.FindControl("tbDC" + rowNumber + "Email") as TextBox).Text = "";
            (pnlDeptComm.FindControl("lblError" + rowNumber) as Label).Text = "";
            PopulateLabels(rowNumber);
            CheckFormCompletion(Master.ApplicationID);

        }
        protected void tbDC1Email_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(PromotionApplication.specialCharacters);
            if (regex.IsMatch(tbDC1Email.Text))
            {
                return;
            }
            LoadEmployee(1, (int)MethodCallMode.EventDriven);
        }
        protected void tbDC2Email_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(PromotionApplication.specialCharacters);
            if (regex.IsMatch(tbDC2Email.Text))
            {
                return;
            }
            LoadEmployee(2, (int)MethodCallMode.EventDriven);
        }
        protected void tbDC3Email_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(PromotionApplication.specialCharacters);
            if (regex.IsMatch(tbDC3Email.Text))
            {
                return;
            }
            LoadEmployee(3, (int)MethodCallMode.EventDriven);
        }
        protected void tbDC4Email_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(PromotionApplication.specialCharacters);
            if (regex.IsMatch(tbDC4Email.Text))
            {
                return;
            }
            LoadEmployee(4, (int)MethodCallMode.EventDriven);
        }
        protected void tbDC5Email_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(PromotionApplication.specialCharacters);
            if (regex.IsMatch(tbDC5Email.Text))
            {
                return;
            }
            LoadEmployee(5, (int)MethodCallMode.EventDriven);
        }
        #endregion
        private void LoadEmployee(int rowNumber, int methodCallMode)
        {
            string Email = (pnlDeptComm.FindControl("tbDC" + rowNumber + "Email") as TextBox).Text.Trim();

            if (Email.Trim().Length == 0)
            {
                (pnlDeptComm.FindControl("lblError" + rowNumber) as Label).Text = "";
                bal.DeleteApplicationRoles(Master.ApplicationID, GetDeptCommRoleID(rowNumber));
                PopulateLabels(rowNumber);
            }
            else
            {
            if (!Email.Contains('@'))
            {
                Email = Email + ConfigurationManager.AppSettings["OrganizationEmail1"];
                (pnlDeptComm.FindControl("tbDC" + rowNumber + "Email") as TextBox).Text = Email;
            }

            if (bal.GetEmployeeByEmail(Email).Count == 0 && bal.GetExternalEmployeeByEmail(Email).Count == 0)
                {
                    (pnlDeptComm.FindControl("lblError" + rowNumber) as Label).Text = "Employee does not exist";
                    bal.DeleteApplicationRoles(Master.ApplicationID, GetDeptCommRoleID(rowNumber));
                    PopulateLabels(rowNumber);
                }
                else
                {
                    Employee emp = new Employee();
                    ExternalEmployee eEmp = new ExternalEmployee();
                    if (bal.GetEmployeeByEmail(Email).Count != 0)
                    {
                        emp = bal.GetEmployeeByEmail(Email)[0];
                        eEmp.ExternalEmployeeID = 0;
                    }
                    else if (bal.GetExternalEmployeeByEmail(Email).Count != 0)
                    {
                        emp.EmployeeID = "0";
                        eEmp = bal.GetExternalEmployeeByEmail(Email)[0];
                    }
                    string email = emp.Email + eEmp.Email;
                    if ((Email.ToLower() == email.ToLower())
                        && methodCallMode == (int)MethodCallMode.EventDriven
                        && (pnlDeptComm.FindControl("lblName" + rowNumber) as Label).Text != "")
                    {
                        return;
                    }
                    else
                    {
                        PopulateLabels(rowNumber, emp, eEmp, methodCallMode);
                    }
                }
            }
        }
        private void PopulateLabels(int rowNo, Employee emp, ExternalEmployee eEmp, int methodCallMode)
        {
            int appID = int.Parse(Request.Params.Get("applicationID").ToString());
            string toRank = bal.GetApplication(appID)[0].ForRank;

            if (!IsRankEligible(toRank, emp.Rank) && !Master.App.ApplicationClosed)
            {
                (pnlDeptComm.FindControl("lblError" + rowNo) as Label).Text = "Suggested Member Rank is less than the Applicant's Rank";
                return;
            }
            if (!IsInvolvedinApp(emp.EmployeeID, eEmp.ExternalEmployeeID, appID))
            {
                (pnlDeptComm.FindControl("lblError" + rowNo) as Label).Text = "The Departmental Committee Member cannot be the Applicant, College Dean, "
                    + ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"] + " or " + ConfigurationManager.AppSettings["TopAuthority_TitleShort"] + "";
                return;
            }
            if (methodCallMode == (int)MethodCallMode.PageLoad)
            {
                if (bal.GetApplicationRole(appID).Where(ar => ar.EmployeeID == emp.EmployeeID && ar.ExternalEmployeeID == eEmp.ExternalEmployeeID && ar.Role.Title.Contains("Academic Promotion Committee")).Count() > 1)
                {
                    (pnlDeptComm.FindControl("lblError" + rowNo) as Label).Text = "The name is already selected";
                    return;
                }
            }
            else
            {
                if (bal.GetApplicationRole(appID).Where(ar => ar.EmployeeID == emp.EmployeeID && ar.ExternalEmployeeID == eEmp.ExternalEmployeeID && ar.Role.Title.Contains("Academic Promotion Committee")).Count() > 0)
                {
                    (pnlDeptComm.FindControl("lblError" + rowNo) as Label).Text = "The name is already selected";
                    return;
                }

            }
            if (eEmp.ExternalEmployeeID == 0)
            {
                (pnlDeptComm.FindControl("lblError" + rowNo) as Label).Text = "";
                (pnlDeptComm.FindControl("lblName" + rowNo) as Label).Text = emp.Name;
                (pnlDeptComm.FindControl("lblRank" + rowNo) as Label).Text = emp.Rank;
                (pnlDeptComm.FindControl("lblDepartment" + rowNo) as Label).Text = emp.Department1.NameString;
                (pnlDeptComm.FindControl("lblOrganization" + rowNo) as Label).Text = "Internal";
            }
            else
            {
                (pnlDeptComm.FindControl("lblError" + rowNo) as Label).Text = "";
                (pnlDeptComm.FindControl("lblName" + rowNo) as Label).Text = eEmp.Name;
                (pnlDeptComm.FindControl("lblRank" + rowNo) as Label).Text = eEmp.Rank;
                (pnlDeptComm.FindControl("lblDepartment" + rowNo) as Label).Text = eEmp.Department;
                (pnlDeptComm.FindControl("lblOrganization" + rowNo) as Label).Text = eEmp.Organization;
            }

            if (bal.GetApplicationRole(appID).Where(ar => ar.EmployeeID == emp.EmployeeID || ar.ExternalEmployeeID == eEmp.ExternalEmployeeID).Count() == 1)
            {
                // bal.UpdateFormDepartmentCommittee(appID, (byte)(rowNo - 1), emp.EmployeeID);
                bal.DeleteApplicationRoles(appID, GetDeptCommRoleID(rowNo));
                bal.InsertApplicationRoles(appID, GetDeptCommRoleID(rowNo), emp.EmployeeID, eEmp.ExternalEmployeeID);
            }
            else
            {
                //    bal.DeleteFormDepartmentCommittee(Master.ApplicationID, (byte)(rowNo - 1));
                bal.DeleteApplicationRoles(appID, GetDeptCommRoleID(rowNo));
                //   bal.InsertFormDepartmentCommittee(appID, (byte)(rowNo - 1), emp.EmployeeID);
                bal.InsertApplicationRoles(appID, GetDeptCommRoleID(rowNo), emp.EmployeeID, eEmp.ExternalEmployeeID);
            }

            CheckFormCompletion(appID);
        }
        private void PopulateLabels(int i)
        {
            (pnlDeptComm.FindControl("lblName" + i) as Label).Text = "";
            (pnlDeptComm.FindControl("lblRank" + i) as Label).Text = "";
            (pnlDeptComm.FindControl("lblDepartment" + i) as Label).Text = "";
            (pnlDeptComm.FindControl("lblOrganization" + i) as Label).Text = "";
        }
        private void CheckFormCompletion(int appID)
        {
            if (bal.GetApplicationRole(appID).Where(ar => ar.RoleID == (int)RoleID.Departmental_Committee_Chairman).Count() == 0)
            {
                ShowError("Departmental Committee chairman is not selected.");
                return;
            }
            if (bal.GetApplicationRole(appID).Where(ar => ar.Role.Title.Contains("Departmental Committee")).Count() >= 3)
            {
                ShowMessage("Departmental Committee is now complete");
            }
            else
            {
                ShowError("Incomplete. There should be at least three entries of Departmental Committee members");
            }
        }
        #region Get Methods 
        public bool GetAppTaskFormCompleted()
        {
            if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0).Count > 0)
            {
                return bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0)[0].Completed;
            }
            else
            {
                return false;
            }
        }
        public string GetAppTaskFormMessage()
        {
            if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0).Count > 0)
            {
                return bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0)[0].Message;
            }
            else
            {
                return "";
            }

        }
        
        private byte GetDeptCommRoleID(int position)
        {
            if (position == 1)
            {
                return (byte)RoleID.Departmental_Committee_Chairman;
            }
            else
            {
                return bal.GetRole().Where(a => a.Title == RoleID.Departmental_Committee_Member_1
                .ToString()
                .Replace("_", " ")
                .Replace("1", "") + (position - 1)).First().RoleID;
                
            }
        }
        #endregion
        private bool IsRankEligible(string toRank, string p)
        {
            if (toRank == p)
            {
                return true;
            }
            else if (toRank == RankProfessorial.Associate_Professor.ToString().Replace("_", " ") && p == RankProfessorial.Professor.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsInvolvedinApp(string EmployeeID, int ExternalEmployeeID, int appID)
        {

            foreach (Application_Role ar in bal.GetApplicationRole(appID))
            {
                if (((RoleID)ar.RoleID == RoleID.Applicant
                    || (RoleID)ar.RoleID == RoleID.College_Dean
                    || (RoleID)ar.RoleID == RoleID.TopLowAuthority
                    || (RoleID)ar.RoleID == RoleID.TopAuthority
                    ) && ar.EmployeeID == EmployeeID)
                    return false;
            }
            if (ExternalEmployeeID == 0)
            {
                return true;
            }

            foreach (Application_Role ar in bal.GetApplicationRole(appID))
            {
                if (((RoleID)ar.RoleID == RoleID.Applicant
                    || (RoleID)ar.RoleID == RoleID.College_Dean
                    || (RoleID)ar.RoleID == RoleID.TopLowAuthority
                    || (RoleID)ar.RoleID == RoleID.TopAuthority
                    ) && ar.ExternalEmployeeID == ExternalEmployeeID)
                    return false;
            }

            return true;
        }

    }
