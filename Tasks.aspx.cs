using System;
using System.Configuration;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using BL.Data;

    public partial class Tasks : System.Web.UI.Page
    {
        BAL bal = new BAL();
        public List<byte> leri = new List<byte>();
        protected void Page_Load(object sender, EventArgs e)
        {
        
            if (IsPostBack)
            {
                return;
            }
            try
            {
            
            DatabindControls();

                if (Session["user"] != null && Request.QueryString["applicationID"] != null)
                {
                    int applicationID = int.Parse(Request.QueryString["applicationID"]);
                    bal.CheckAndCompletePermanentCommitteeInAppRole(applicationID);
                    leri = bal.GetNameExclusion(CurRoleID).Select(ne => ne.ExcludedRoleID).ToList();
                    Employee emp = new Employee();
                    ExternalEmployee eEmp = new ExternalEmployee();
                    if (!(bool)Session["ExtEmp"])
                    {
                        emp = Session["user"] as Employee;
                        if (!Request.Url.ToString().Contains("localhost") && Request.QueryString["spaccess"] == null)
                        {
                            bal.InsertActionLog(emp.NameString + " (user id: " + emp.Email + ")" + ((bool)Session["IsDeputy"] ? "(Deputy)" : ""), DateTime.Now);
                        }
                    }
                    else
                    {
                        eEmp = Session["user"] as ExternalEmployee;
                        if (!Request.Url.ToString().Contains("localhost") && Request.QueryString["spaccess"] == null)
                        {
                            bal.InsertActionLog(eEmp.NameString + " (email: " + eEmp.Email + ")" + ((bool)Session["IsDeputy"] ? "(Deputy)" : ""), DateTime.Now);
                        }
                    }
                    Session["ApplicationID"] = applicationID;
                    //List<Application_TaskLog> latl = bal.GetAppTaskLog(applicationID).Where(a => !a.Completed).ToList();
                    //Application_TaskLog atl = new Application_TaskLog();
                    gvActiveTasks.DataSource = bal.GetAppTaskLog(applicationID)
                        .Where(a => !a.Completed)                        
                        .GroupBy(a => a.TaskID)
                        .Select(group => group.First());

                    //if (!(bool)Session["ExtEmp"])
                    //{
                    //    atl = latl.Where(at => at.Task.Role.Application_Role.Where(ar => ar.EmployeeID == Employee.EmployeeID && ar.RoleID == CurRoleID).Count() > 0).First();
                    //    employee = atl.Task.Role.Application_Role.Where(ar => ar.ApplicationID == applicationID).Select(ar => ar.Employee).First();
                    //    //lblEmployee.Text = Employee.NameString + " (" + Employee.Rank + ")";
                    //    lblRole.Text = atl.Task.Role.Title;
                    //    lblStatus.Text = employee.EmployeeID == Employee.EmployeeID && atl.Task.RoleID == CurRoleID
                    //        ? RecordStatus.Active.ToString() : RecordStatus.Inactive.ToString();

                    //}
                    //else
                    //{
                    //    atl = latl.Where(at => at.Task.Role.Application_Role.Where(ar => ar.ExternalEmployeeID == EEmployee.ExternalEmployeeID && ar.RoleID == CurRoleID).Count() > 0).First();
                    //    eEmployee = atl.Task.Role.Application_Role.Where(ar => ar.ApplicationID == applicationID).Select(ar => ar.ExternalEmployee).First();
                    //    //lblEmployee.Text = EEmployee.NameString + " (" + EEmployee.Rank + " in " + EEmployee.Organization + ")";
                    //    lblRole.Text = atl.Task.Role.Title;
                    //    lblStatus.Text = eEmployee.ExternalEmployeeID == EEmployee.ExternalEmployeeID && atl.Task.RoleID == CurRoleID
                    //            ? RecordStatus.Active.ToString() : RecordStatus.Inactive.ToString();
                    //}
                    //lblTask.Text = atl.Task.Title.Replace("@@TopAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopAuthority_TitleShort"])
                    //    .Replace("@@TopLowAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"])
                    //    .Replace("@@TopMostAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"]);

                    gvActiveTasks.DataBind();
                    gvApplicationTracking.DataSource = bal.GetApplicationLog()
                        .Where(a => a.ApplicationID == applicationID)
                         .OrderByDescending(a => a.ActionTime); 
                    gvApplicationTracking.DataBind();
                    //ObjectDataSourceHistory.SelectParameters["ApplicationID"].DefaultValue = applicationID.ToString(); ;
                    //ObjectDataSourceHistory.SelectParameters["RoleID"].DefaultValue = CurRoleID.ToString();
                    //ObjectDataSourceActiveTasks.SelectParameters["ApplicationID"].DefaultValue = applicationID.ToString(); ;
                    //ObjectDataSourceActiveTasks.SelectParameters["RoleID"].DefaultValue = CurRoleID.ToString();
                    //                bool deptFlag = false, collegeFlag = false;                
                    bool collegeFlag = false;
                    foreach (Application_Role ar in bal.GetApplicationRole(applicationID))
                    {
                        if (ar.RoleID == (byte)RoleID.College_Dean)
                        {
                            collegeFlag = true;
                            continue;
                        }

                    }

                    //if (deptFlag == false || collegeFlag == false)
                    if (collegeFlag == false)
                    {
                        if (!(bool)Session["ExtEmp"])
                        {
                            Emailer.Send(ConfigurationManager.AppSettings["Mail.From"], "Incomplete Promotion Application", @"The following user's promotion application has missing Chairman or Dean roles:

                        Dr. " + emp.Name + " (" + emp.EmployeeID + "), email: " + emp.Email + @"
                        Dept:" + emp.Department + @"
                        
                        ApplicationID: " + applicationID, "Auto Mailer", applicationID);
                            gvActiveTasks.Enabled = false;
                            labelProgrammaticPopup0.Text = @"The promotion application for the user " + emp.Name + ", is incomplete with incorrect assignment of Dept Chairman or Dean . Please contact the System Administrator. An Email is also sent to the Administrator.";
                            programmaticModalPopup0.Show();
                        }
                        else
                        {
                            Emailer.Send(ConfigurationManager.AppSettings["Mail.From"], "Incomplete Promotion Application", @"The following user's promotion application has missing Chairman or Dean roles:

                        Dr. " + eEmp.Name + " (" + eEmp.ExternalEmployeeID + "), email: " + eEmp.Email + @"
                        Dept:" + eEmp.Department + @"
                        
                        ApplicationID: " + applicationID, "Auto Mailer", applicationID);
                            gvActiveTasks.Enabled = false;
                            labelProgrammaticPopup0.Text = @"The promotion application for the user " + eEmp.Name + ", is incomplete with incorrect assignment of Dept Chairman or Dean . Please contact the System Administrator. An Email is also sent to the Administrator.";
                            programmaticModalPopup0.Show();
                        }
                    }

                }
                else
                {
                    Response.Redirect("Login.aspx", false);
                    return;
                }
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        private ExternalEmployee eEmployee = null;
        public ExternalEmployee EEmployee
        {
            get
            {
                if (eEmployee == null)
                {
                    if (Session["user"] != null && (bool)Session["ExtEmp"])
                    {
                        eEmployee = (ExternalEmployee)Session["user"];
                    }
                    else
                    {
                        if (Session["user"] != null)
                        {
                            return new ExternalEmployee() { ExternalEmployeeID = 0 };
                        }
                        else
                        {
                            return null;

                        }

                    }
                }
                return eEmployee;
            }
        }
        private Employee employee = null;
        public Employee Employee
        {
            get
            {
                if (employee == null)
                {
                    if (Session["user"] != null && !(bool)Session["ExtEmp"])
                    {
                        employee = (Employee)Session["user"];
                    }
                    else
                    {
                        if (Session["user"] != null)
                        {
                            return new Employee() { EmployeeID = "0" };
                        }
                        else
                        {
                            return null;

                        }
                    }
                }
                return employee;
            }
        }
        public bool CheckTask(string employeeID, int eEmployeeID, string roleID)
        {
            //   int curUserRole = int.Parse(Request.QueryString["RoleID"].Replace("ae54sdafaas5313qw", "").Replace("f98poj64lsks98336hujm9811336fsdf", ""));
            if (Session["user"] != null)
            {
                Employee emp = new Employee();
                ExternalEmployee eEmp = new ExternalEmployee();
                if (!(bool)Session["ExtEmp"])
                {
                    emp = Session["user"] as Employee;

                    if (employeeID == emp.EmployeeID && roleID == CurRoleID.ToString())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    eEmp = Session["user"] as ExternalEmployee;
                    if (eEmployeeID == eEmp.ExternalEmployeeID && roleID == CurRoleID.ToString())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public bool CheckTask(string appID, string employeeID, int eEmployeeID, string roleID)
        {
            //var scMemRoleID = new List<int> { 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38};
            // int curUserRole = int.Parse(Request.QueryString["RoleID"].Replace("ae54sdafaas5313qw", "").Replace("f98poj64lsks98336hujm9811336fsdf", ""));
            if (Session["user"] != null)
            {
                Employee emp = new Employee();
                ExternalEmployee eEmp = new ExternalEmployee();
                if (!(bool)Session["ExtEmp"])
                {
                    emp = Session["user"] as Employee;
                    if (employeeID == emp.EmployeeID && roleID == CurRoleID.ToString())
                    {
                        if (emp.EmployeeID == appID && roleID != "1")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    eEmp = Session["user"] as ExternalEmployee;
                    if (eEmployeeID == eEmp.ExternalEmployeeID && roleID == CurRoleID.ToString())
                    {
                        if (emp.EmployeeID == appID && roleID != "1")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }



            }
            return false;
        }
        public string GetStatus(string appID)
        {
            string status;

            Application appRow = bal.GetApplication(int.Parse(appID))[0];
            if (appRow.ApplicationClosed == true)
            {
                status = "(Closed)";
            }
            else
            {
                status = "(Pending)";
            }
            return status;
        }
        public string GetRole(byte RoleID)
        {
            return bal.GetRoleByPK(RoleID)[0].Title;
        }
        public string GetRoleFromApplicationRole(byte RoleID)
        {
            int applicationID = int.Parse(Request.QueryString["applicationID"]);
            return bal.GetApplicationRole(applicationID, RoleID)[0].Role.Title;            
        }
         public string GetIsActingFromApplicationRole(byte RoleID)
        {
           int applicationID = int.Parse(Request.QueryString["applicationID"]);
            return bal.GetApplicationRole(applicationID, RoleID)[0].IsActing == "Acting"? " - Acting": "";
        }   
    public string GetAction(string action)
        {
            return action.Replace("@@TopMostAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"])
                .Replace("@@TopAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopAuthority_TitleShort"])
                .Replace("@@TopLowAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"]);
        }
        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }

        private void DatabindControls()
        {
            
            divAppTracking.DataBind();
            lbtnAdmin.DataBind();
            lbtnSearchEmp.Attributes.Add("onclick", "window.open ('Forms/SearchPop.aspx',null,'scrollbars=yes, status= no, resizable = yes, toolbar=no,location=no,height = 700, width = 900, left = 200, top= 200, screenx=10,screeny=600,menubar=no');");
            lbtnSearchEmp.DataBind();
            if (Request.QueryString["applicationID"] != null)
            {
                int applicationID = int.Parse(Request.QueryString["applicationID"]);
                Application app = bal.GetApplication(applicationID)[0];
                lblApplicantName.Text = app.Employee.NameString;
                lblApplicantDept.Text = app.Employee.Department;
                lblApplicantRank.Text = app.Employee.Rank;
                lblAppStartDate.Text = app.StartDate != null ? app.StartDate.Value.ToLongDateString() : "";
                lblForRank.Text = app.ForRank;
                 if (!app.ApplicationClosed)
                 {
                     ctl00_promodetails.Style["background-color"] = "#fffbe9";
                     ctl00_promodetails.Style["padding"] = "10px";
                 }
                 else
                 {
                     ctl00_promodetails.Style["background-color"] = "#e6e6e6";
                     ctl00_promodetails.Style["padding"] = "10px";
                 }
            }
            if (Session["user"] != null)
            {
                Employee emp = new Employee();
                ExternalEmployee eEmp = new ExternalEmployee();
                try
                {
                    if ((bool)Session["ExtEmp"] == false)
                    {
                        emp = Session["user"] as Employee;
                        lblUserName.Text = ((bool)Session["IsDeputy"]) ? "Deputy of " + emp.NameString : emp.NameString;

                    }
                    else
                    {
                        eEmp = Session["user"] as ExternalEmployee;
                        lblUserName.Text = ((bool)Session["IsDeputy"]) ? "Deputy of " + eEmp.NameString : eEmp.NameString;
                    }
                    lblDeputy.DataBind();
                    lblRole.DataBind();
                }
                catch (Exception exp)
                {

                    Session.Clear();
                    throw (exp);
                }
            }
        }
        #region Properties
        private byte curRoleID = 0;
        public byte CurRoleID
        {
            get
            {
                if (curRoleID == 0)
                {
                    try
                    {
                        curRoleID = byte.Parse(Cryptography.Decrypt(Request.QueryString["RoleID"].Replace(" ", "+")));
                        //curRoleID = byte.Parse(Request.QueryString["RoleID"].Replace("ae54sdafaas5313qw", "").Replace("f98poj64lsks98336hujm9811336fsdf", ""));
                    }
                    catch (Exception)
                    {
                        Response.Redirect("~/Main.aspx");

                    }
                }
                return curRoleID;
            }
        }
        #endregion
    }

