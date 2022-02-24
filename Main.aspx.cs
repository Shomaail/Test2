using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Linq;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using BL.Data;
using KFUPM.CAS;

public partial class Main : System.Web.UI.Page
    {
        BAL bal = new BAL();
        ExtRevBAL erBAL = new ExtRevBAL();
        ExtRevFormBAL erfBAL = new ExtRevFormBAL();
        private enum Mode { Applicant = 1, DeptChair = 2, CollegeDean = 4, TopAuthority = 5, DirectorGeneralHR = 8 }
        public enum FormModeMain { Applications, MyDetails, DeputyDetails };
        public bool Status { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
        if (Request.QueryString["spaccess"] == null && Session["user"] == null)
        {
            KFUPMCASAuthentication();
        }

        if (Session["user"] != null && Session["user"].ToString() == "NonFacultyEmployee")
        {
            Response.Redirect("Login.aspx");
            return;
        }
        if (Request.QueryString["spaccess"] == null && Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (Session["user"] != null && Session["user"].ToString() == "NonFacultyEmployee")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["IsDeputy"] != null)
            {
                lblDeputy.Visible = (bool)Session["IsDeputy"];
            }
            if (Session["ExtRevID"] != null)
            {
                Response.Redirect("ExtForms/ExtMessage.aspx");
                return;
            }

            //if (Session["ExtPCMember"] != null)
            //{
            //    Response.Redirect("ExtPCForms/ExtPCMain.aspx");
            //    return;
            //}
            if (Session["user"] != null)
            {
                Employee emp = new Employee();
                ExternalEmployee eEmp = new ExternalEmployee();
                try
                {
                    if ((bool)Session["ExtEmp"] == false)
                    {
                        emp = Session["user"] as Employee;
                        eEmp.ExternalEmployeeID = 0;
                        lblEmpID.Text = emp.EmployeeID;
                        lblName.Text = emp.NameString;
                        lblEmail.Text = emp.Email;
                        lblRank.Text = emp.Rank;
                        lblDept.Text = emp.Department1.NameString;

                        //ObjectDataSourceClosedApps.SelectParameters["EmployeeID"].DefaultValue = user.EmployeeID;

                        //odsActiveAppList.SelectParameters["EmployeeID"].DefaultValue = user.EmployeeID;
                        //odsDeputy.SelectParameters["HeadEmpID"].DefaultValue = emp.EmployeeID;
                        //PanelCosedApplications.Visible = bal.GetApplicationRole(emp.EmployeeID,0).Count > 0;
                        if (emp.Status != null && emp.Status == "Inactive")
                        {
                            Emailer.Send(ConfigurationManager.AppSettings["Mail.From"], "Inactive User Login", @"The following user tried to login into the system while his record is INACTIVE
User: " + emp.Name + " (" + emp.EmployeeID + ") email: " + emp.Email, "Auto Mailer", -1);
                            //gvApplicatonsGrid.Enabled = false;

                            // gvClosedApps.Enabled = false;
                            lblProgrammaticPopup0.Text = "The userdata for the user " + emp.Name + " is not found in the Database. Please contact the System Administrator. An Email is also sent to the Administrator.";
                            programmaticModalPopup0.Show();
                        }
                        if (bal.GetDepartmentByEmp(emp.EmployeeID).Count > 0 && (bool)Session["IsDeputy"] == false)
                        {
                            divDeputyDetails.Visible = true;
                        }
                        else
                        {
                            divDeputyDetails.Visible = false;
                        }
                        DatabindControls();
                    }
                    else
                    {
                        divApply.Visible = false;
                        divDeputyDetails.Visible = false;
                        emp.EmployeeID = "0";
                        eEmp = Session["user"] as ExternalEmployee;

                        lblEmpID.Text = eEmp.ExternalEmployeeID.ToString();
                        lblName.Text = eEmp.NameString;
                        lblEmail.Text = eEmp.Email;
                        lblRank.Text = eEmp.Rank;
                        lblDept.Text = emp.Department;
                        DatabindControls();
                    }
                    //  lbtnAdmin.Visible = (Session["IsAdmin"] != null) && ((bool)Session["IsAdmin"]);
                }
                catch (Exception exp)
                {

                    Session.Clear();
                    throw (exp);
                }

            }
            else
            {
                FormsAuthentication.SignOut();
                Response.Cookies.Remove("langCookie");
                Session["IsAdmin"] = false;

                Session.Clear();

                Response.Redirect(ConfigurationManager.AppSettings["LogoutURL"]);
            }
        }

        #region Event Handlers


        protected void lbtnApplications_Click(object sender, EventArgs e)
        {
            FormMode = FormModeMain.Applications.ToString();
            DatabindControls();
        }
        //protected void lbtnDeputyDetails_Click(object sender, EventArgs e)
        //{
        //    FormMode = FormModeMain.DeputyDetails.ToString();
        //    DatabindControls();
        //}
        protected void lbtnMyDetails_Click(object sender, EventArgs e)
        {
            FormMode = FormModeMain.MyDetails.ToString();
            DatabindControls();
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                Employee user = Session["user"] as Employee;
                string welcomeMessage = bal.GetActionMessageByPK((int)ActionID.Start_Application, (int)TaskIDs.Apply_for_Promotion)[0].Message;
                if(user.DepartmentID == null ||user.DepartmentID == 0 || user.Department1.HeadEmpID == null)
                {
                    Emailer.Send(ConfigurationManager.AppSettings["Mail.From"], "Incomplete User detail", @"The following user tried to start a new promotion appication in the system while his record is incomplete.
                    Department information is not found or is incomplete. 
                    User: " + user.NameString + " (" + user.EmployeeID + ") email: " + user.Email, "Auto Mailer", null);
                    lblProgrammaticPopup0.Text = "The userdata for the user " + user.NameString + " is not complete in the Database (Department information is not found or is incomplete). Please contact the System Administrator. An Email is also sent to the Administrator.";
                    programmaticModalPopup0.Show();
                    return;
                }
                if(user.Department1.ParentDeptID == null ||user.Department1.ParentDeptID == 0 )
                {
                    Emailer.Send(ConfigurationManager.AppSettings["Mail.From"], "Incomplete User detail", @"The following user tried to start a new promotion appication in the system while his record is incomplete.
                    College information is not found. 
                    User: " + user.NameString + " (" + user.EmployeeID + ") email: " + user.Email, "Auto Mailer", null);
                    lblProgrammaticPopup0.Text = "The userdata for the user " + user.NameString + " is not complete in the Database (College information is not found). Please contact the System Administrator. An Email is also sent to the Administrator.";
                    programmaticModalPopup0.Show();
                    return;
                }
                /* create new application */
                int applicationID = bal.InsertApplication(user.EmployeeID, PromotionApplication.GetNextRank(user));
                Session["ApplicationID"] = applicationID;
                bal.InsertApplicationRoles(applicationID);
                welcomeMessage = welcomeMessage.Replace("@@Applicant@@", user.NameString)
                    .Replace("@@TopAuthority@@", bal.GetEmployeeByPK((bal.GetApplicationRole(applicationID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameString)
                    .Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);
                bal.InsertAppTskLg(applicationID, (int)TaskIDs.Apply_for_Promotion, null, DateTime.Now, false, 0, welcomeMessage);
                foreach (WFAction a in bal.GetActions(RecordStatus.Active.ToString()))
                {
                    bal.InsertApplicationActionStatus(applicationID, a.ActionID, a.Status);
                    if (a.AttActionID.HasValue)
                    {
                        bal.InsertApplicationActionStatus(applicationID, a.AttActionID.Value, RecordStatus.Inactive.ToString());
                    }
                }

                Response.Redirect("Forms\\Message.aspx?applicationID=" + applicationID + "&roleID=" + (byte)RoleID.Applicant);

            }


        }
        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }
        protected void rpApplications_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int appID = int.Parse((e.Item.FindControl("lblApplicationID") as Label).Text);
                bal.DeleteForm_FinalPC(appID);
                bal.DeletetAppTskFrm(appID);
                //      bal.DeleteFormDepartmentCommittee(appID);        
                erBAL.DeleteFormExtRev(appID);
                erBAL.DeleteFormFinalExtRev(appID);
                erfBAL.DeletePublicationsEvaluation(appID);
                bal.DeleteForm_AttachmentAdByAppID(appID);
                bal.DeleteForm_AppProperties(appID);
                bal.DeleteApplicationActionStatus(appID);
                bal.DeleteSentEmailByAppID(appID);
                bal.DeleteApplication_TaskLog(appID);
                bal.DeleteApplication_TaskLogExt(appID);
                bal.DeleteApplication_TaskLogExtByAppID(appID);
                bal.DeleteApplication_LogExtByAppID(appID);
                bal.DeleteApplication_Log(appID);
                bal.DeleteApplicationRoles(appID);
                bal.DeleteApplication(appID);
                if (Session["user"] != null)
                {
                    if ((bool)Session["ExtEmp"] == false)
                    {
                        Employee emp = new Employee();
                        emp = Session["user"] as Employee;
                        DatabindControls();
                        //e.Cancel = true;
                        //if (bal.GetAppTaskLog(appID).Count() == 1)
                        //{
                        //    divApply.Visible = true;
                        //}
                    }
                }
            }

        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            RepeaterItem rItem = (sender as LinkButton).Parent as RepeaterItem;
            int appID = int.Parse((rItem.FindControl("lblApplicationID") as Label).Text);
            bal.DeleteForm_FinalPC(appID);
            bal.DeletetAppTskFrm(appID);
            //      bal.DeleteFormDepartmentCommittee(appID);        
            erBAL.DeleteFormExtRev(appID);
            erBAL.DeleteFormFinalExtRev(appID);
            erfBAL.DeletePublicationsEvaluation(appID);
            bal.DeleteForm_AttachmentAdByAppID(appID);
            bal.DeleteForm_AppProperties(appID);
            bal.DeleteApplication_TaskLog(appID);
            bal.DeleteApplicationRoles(appID);
            bal.DeleteApplication(appID);
          //  bal.DeleteApplication(appID, appID);
            if (Session["user"] != null)
            {
                if ((bool)Session["ExtEmp"] == false)
                {
                    Employee emp = new Employee();
                    emp = Session["user"] as Employee;
                    DatabindControls();
                    //e.Cancel = true;
                    if (bal.GetAppTaskLog(appID).Count() == 1)
                    {
                        divApply.Visible = true;
                    }
                }
            }
        }
        #region gvDeputy
        protected void gvDeputy_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (Session["user"] != null)
            {
                if ((bool)Session["ExtEmp"] == false)
                {
                    Employee emp = new Employee();
                    emp = Session["user"] as Employee;
                    gvDeputy.EditIndex = e.NewEditIndex;
                    DatabindControls();
                }
            }

        }
        protected void gvDeputy_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            if (Session["user"] != null)
            {
                if ((bool)Session["ExtEmp"] == false)
                {
                    Employee emp = new Employee();
                    emp = Session["user"] as Employee;
                    gvDeputy.EditIndex = -1;
                    DatabindControls();
                }
            }
        }
        protected void gvDeputy_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            if (Session["user"] != null)
            {
                if ((bool)Session["ExtEmp"] == false)
                {
                    Employee emp = new Employee();
                    emp = Session["user"] as Employee;
                    List<Department> ld = bal.GetDepartments("Active").Where(d => d.HeadEmail != null && d.HeadEmpID == emp.EmployeeID).ToList();
                    GridViewRow row = gvDeputy.Rows[e.RowIndex];
                    Department dept = ld[row.DataItemIndex];
                    bal.UpdateDepartmentDeputyEmail(
                        ((HtmlInputControl)row.FindControl("tbDeputyEdit")).Value,
                        ((HtmlInputControl)row.FindControl("tbDeputy2Edit")).Value,
                        dept.DepartmentID);
                    gvDeputy.EditIndex = -1;
                    DatabindControls();
                }
            }
        }
        #endregion
        #region gvApplicationsGrid
        protected void gvApplicatonsGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["RoleID"] = "";
        }

        //protected void gvApplicatonsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    int appID = int.Parse(gvApplications.DataKeys[e.RowIndex].Value.ToString());
        //    bal.DeleteForm_FinalPC(appID);
        //    bal.DeletetAppTskFrm(appID);
        //    //      bal.DeleteFormDepartmentCommittee(appID);        
        //    erBAL.DeleteFormExtRev(appID);
        //    erBAL.DeleteFormFinalExtRev(appID);
        //    erfBAL.DeletePublicationsEvaluation(appID);
        //    bal.DeleteForm_AttachmentByAppID(appID);
        //    bal.DeleteForm_AppProperties(appID);
        //    bal.DeleteAppTskLg(appID);
        //    bal.DeleteApplicationRoles(appID);
        //    bal.DeleteApplication(appID, appID);
        //    if (Session["user"] != null)
        //    {
        //        if ((bool)Session["ExtEmp"] == false)
        //        {
        //            Employee emp = new Employee();
        //            emp = Session["user"] as Employee;
        //            DatabindControls();
        //            e.Cancel = true;
        //            if (bal.GetAppTaskLog(appID).Count() == 1)
        //            {
        //                divApply.Visible = true;
        //            }
        //        }
        //    }

        //}

        #endregion
        #region gvClosedApps
        protected void gvClosedApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["RoleID"] = "";
        }
        #endregion
        #endregion
        public bool CheckTask(string employeeID, int roleID, int appID)
        {
            if (Session["user"] != null)
            {
                Employee user = Session["user"] as Employee;
                ApplicationList application = bal.GetApplicationList(appID, user.EmployeeID)[0];
                if (employeeID == user.EmployeeID && roleID == application.RoleID)
                {
                    return true;
                }

            }
            return false;
        }
        public int GetNoOfActiveTask(int appID)
        {
            return bal.GetAppTaskLog(appID)
                        .Where(atl => !atl.Completed)
                        .GroupBy(atl => atl.TaskID)
                        .Select(group => group.First()).Count();
        }
        public bool GetStatus(int appID, byte roleID)
        {
            Status = false;
            Employee emp = new Employee();
            ExternalEmployee eEmp = new ExternalEmployee();
            if (!(bool)Session["ExtEmp"])
            {
                emp = Session["user"] as Employee;
                //bal.GetApplicationRole(appID, roleID, emp.EmployeeID, eEmp.ExternalEmployeeID)[0];
                Status = bal.GetAppTaskLog(appID).Where(atl => !atl.Completed && atl.Task.RoleID == roleID).Count() > 0;

                //foreach (Application_TaskLog aptl in bal.GetAppTaskLog(appID).Where(aptl => !aptl.Completed && !aptl.Application.ApplicationClosed).GroupBy(atl => atl.TaskID)
                //    .Select(group => group.First()))
                //{
                //    if (aptl.Task.Role.Application_Role
                //        .Where(ar => ar.ApplicationID == appID)
                //        .Select(ar => ar.Employee).First().EmployeeID == emp.EmployeeID && aptl.Task.RoleID == roleID)
                //    {
                //        Status = true;
                //        break;

                //    }
                //}
                //List<vw_ActiveTask1> lat = bal.GetActiveTask(roleID, appID);
                //foreach (vw_ActiveTask1 activeTask in lat)
                //{
                //    if (activeTask.EmployeeID == emp.EmployeeID && roleID == activeTask.RoleID)
                //    {
                //        Status = true;
                //        break;
                //    }

                //}
            }
            else
            {
                eEmp = Session["user"] as ExternalEmployee;
                foreach (Application_TaskLog aptl in bal.GetAppTaskLog(appID).Where(aptl => !aptl.Completed && !aptl.Application.ApplicationClosed).GroupBy(atl => atl.TaskID)
                        .Select(group => group.First()))
                {
                    if (aptl.Task.Role.Application_Role
                        .Where(ar => ar.ApplicationID == appID)
                        .Select(ar => ar.ExternalEmployee).First().ExternalEmployeeID == eEmp.ExternalEmployeeID && aptl.Task.RoleID == roleID)
                    {
                        Status = true;
                        break;

                    }
                }
            }

            return Status;
        }
        public bool GetStatus()
        {
            Status = false;
            int appID = int.Parse(Eval("ApplicationID").ToString());
            byte roleID = byte.Parse(Eval("RoleID").ToString());
            Employee emp = new Employee();
            ExternalEmployee eEmp = new ExternalEmployee();
            if (!(bool)Session["ExtEmp"])
            {
                emp = Session["user"] as Employee;
                foreach (Application_TaskLog aptl in bal.GetAppTaskLog(appID).Where(aptl => !aptl.Completed && !aptl.Application.ApplicationClosed).GroupBy(atl => atl.TaskID)
                    .Select(group => group.First()))
                {
                    if (aptl.Task.Role.Application_Role
                        .Where(ar => ar.ApplicationID == appID)
                        .Select(ar => ar.Employee).First().EmployeeID == emp.EmployeeID && aptl.Task.RoleID == roleID)
                    {
                        Status = true;
                        break;

                    }
                }
                //List<vw_ActiveTask1> lat = bal.GetActiveTask(roleID, appID);
                //foreach (vw_ActiveTask1 activeTask in lat)
                //{
                //    if (activeTask.EmployeeID == emp.EmployeeID && roleID == activeTask.RoleID)
                //    {
                //        Status = true;
                //        break;
                //    }

                //}
            }
            else
            {
                eEmp = Session["user"] as ExternalEmployee;
                foreach (Application_TaskLog aptl in bal.GetAppTaskLog(appID).Where(aptl => !aptl.Completed && !aptl.Application.ApplicationClosed).GroupBy(atl => atl.TaskID)
                        .Select(group => group.First()))
                {
                    if (aptl.Task.Role.Application_Role
                        .Where(ar => ar.ApplicationID == appID)
                        .Select(ar => ar.ExternalEmployee).First().ExternalEmployeeID == eEmp.ExternalEmployeeID && aptl.Task.RoleID == roleID)
                    {
                        Status = true;
                        break;

                    }
                }
            }

            return Status;
        }
        public int GetAppTaskLogCount(int appID)
        {
            return bal.GetAppTaskLog(appID).Count();
        }
        public int GetAppTaskLogCount()
        {
            int appID = int.Parse(Eval("ApplicationID").ToString());
            return bal.GetAppTaskLog(appID).Count();
        }
        public string GetDeputy(string Email)
        {
            if (Email == "" || Email == null)
            {
                return "";

            }
            else if (bal.GetEmployeeByEmail(Email).Count != 0)
            {
                return bal.GetEmployeeByEmail(Email)[0].NameString;
            }
            else
            {
                return "No such Email exist";
            }

        }
        public string GetURL(int appID, byte roleID)
        {
            if (GetStatus(appID, roleID))
            {
                return "Forms/Message.aspx?applicationID=" + appID + "&roleID=" + Cryptography.Encrypt(roleID.ToString());
            }
            else
            {
                if (Request.QueryString["spaccess"] == null)
                {
                    return "Tasks.aspx?applicationID=" + appID + "&roleID=" + Cryptography.Encrypt(roleID.ToString());
                }
                else
                {
                    return "Tasks.aspx?applicationID=" + appID + "&roleID=" + Cryptography.Encrypt(roleID.ToString()) + "&spaccess=1";
                }
            }

        }
        public string GetURL()
        {
            int appID = int.Parse(Eval("ApplicationID").ToString());
            byte roleID = byte.Parse(Eval("RoleID").ToString());
            if (Request.QueryString["spaccess"] == null)
            {
                return "Tasks.aspx?applicationID=" + appID + "&roleID=" + Cryptography.Encrypt(roleID.ToString());
            }
            else
            {
                return "Tasks.aspx?applicationID=" + appID + "&roleID=" + Cryptography.Encrypt(roleID.ToString()) + "&spaccess=1";
            }

        }

        private void DatabindControls()
        {

            lbtnAdmin.DataBind();
            lbtnSearchEmp.Attributes.Add("onclick", "window.open ('Forms/SearchPop.aspx',null,'scrollbars=yes, status= no, resizable = yes, toolbar=no,location=no,height = 700, width = 900, left = 200, top= 200, screenx=10,screeny=600,menubar=no');");
            lbtnSearchEmp.DataBind();

            if (Request.QueryString["MyDetails"] == null && !IsPostBack)
            {
                FormMode = FormModeMain.Applications.ToString();
            }
            else if (Request.QueryString["MyDetails"] != null && !IsPostBack)
            {
                FormMode = FormModeMain.MyDetails.ToString();
            }
            divApplications.DataBind();
            divMyDetails.DataBind();
            if (FormMode == FormModeMain.Applications.ToString())
            {
                liApp.Attributes["class"] = "active";
                liMyDetails.Attributes["class"] = "";

            }
            else if (FormMode == FormModeMain.MyDetails.ToString())
            {
                liApp.Attributes["class"] = "";
                liMyDetails.Attributes["class"] = "active";
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
                        gvDeputy.DataSource = bal.GetDepartments("Active")
                            .Where(d => d.HeadEmail != null && d.HeadEmpID == emp.EmployeeID);
                        gvDeputy.DataBind();
                        //if(isVR(emp))
                        //{
                        //  rpClosedApplications.DataSource = bal.GetApplicationRole((byte)RoleID.TopAuthority)
                        //    .Where(ar => ar.Application.ApplicationClosed)
                        //    .OrderBy(ar => ar.Application.StartDate);

                        //}
                        //else
                        //{
                            rpClosedApplications.DataSource = bal.GetApplicationRole()
                            .Where(ar => (ar.EmployeeID == emp.EmployeeID && ar.Application.EmployeeID == emp.EmployeeID && ar.Application.ApplicationClosed)
                            || (ar.EmployeeID == emp.EmployeeID && ar.Application.ApplicationClosed && ar.Application.Application_TaskLog
                            .Where(atl => atl.TaskID != (int)TaskIDs.Apply_for_Promotion).Count() > 0)).OrderBy(ar => ar.Application.StartDate);
                        //}
                        rpClosedApplications.DataBind();
                        //rpApplications.DataSource = bal.GetApplicationRole()
                        //    .Where(ar => (ar.EmployeeID == emp.EmployeeID && ar.Application.EmployeeID == emp.EmployeeID && !ar.Application.ApplicationClosed)
                        //    || (ar.EmployeeID == emp.EmployeeID && !ar.Application.ApplicationClosed && ar.Application.Application_TaskLog
                        //    .Where(atl => atl.TaskID != (int)TaskIDs.Apply_for_Promotion).Count() > 0)).OrderBy(ar=>ar.Application.StartDate);
                        if(bal.GetApplicationRole().Where(a=>a.EmployeeID == emp.EmployeeID && a.RoleID == (byte) RoleID.Promotion_Committee_Chairman
                        && !a.Application.ApplicationClosed).Count() != 0)
                        {

                        }   

                        List<Application_Role> lar = bal.GetApplicationRole()
                            .Where(ar => (ar.EmployeeID == emp.EmployeeID && ar.Application.EmployeeID == emp.EmployeeID && !ar.Application.ApplicationClosed)
                            || (ar.EmployeeID == emp.EmployeeID && !ar.Application.ApplicationClosed)).OrderBy(ar => ar.Application.StartDate).ToList();
                        lar.RemoveAll(a=> a.Application.EmployeeID != emp.EmployeeID && a.Application.Application_TaskLog
                        .Where(atl => atl.TaskID != (int)TaskIDs.Apply_for_Promotion).Count() == 0);
                        //The Applications which has not reached the current role 
                        //lar.RemoveAll(a => a.Application.Application_TaskLog.Where(atl => atl.Task.RoleID == a.RoleID).Count() == 0);
                        rpApplications.DataSource = lar;

                        //&& ar.Application.Application_TaskLog
                        //.Where(atl => atl.TaskID != (int)TaskIDs.Apply_for_Promotion).Count() > 0)).OrderBy(ar => ar.Application.StartDate);
                        rpApplications.DataBind();

                        divCurrentAppMsg.Visible = rpApplications.Items.Count != 0;
                        divCurrentAppAbsentMsg.Visible = rpApplications.Items.Count == 0;
                        divClosedAppMsg.Visible = rpClosedApplications.Items.Count != 0;
                        divClosedAppAbsentMsg.Visible = rpClosedApplications.Items.Count == 0;

                        //gvApplicatonsGrid.DataSource = bal.GetActiveApplicationList(emp.EmployeeID);
                        //                gvApplications.DataSource = bal.GetApplicationRole()
                        //.Where(ar => (ar.EmployeeID == emp.EmployeeID && ar.Application.EmployeeID == emp.EmployeeID && !ar.Application.ApplicationClosed)
                        //|| (ar.EmployeeID == emp.EmployeeID && !ar.Application.ApplicationClosed && ar.Application.Application_TaskLog.Where(atl => atl.TaskID != 1).Count() > 0));

                        //                gvApplications.DataBind();
                        string nextRank = PromotionApplication.GetNextRank(emp);
                    // Conditions to apply for promotion 
                    // 1- Possibility to have next rank
                    // 2- Check if the applicant has additional active applications. 
                    //    He can have archived Promotion Application for the same rank                 
                    // 3- is not deputy

                    //if (nextRank != "" 
                    //    && bal.GetActiveApplicationList(emp.EmployeeID)
                    //            .Where(aa => aa.RoleID == (int)RoleID.Applicant).ToList().Count == 0
                    //    && !(bool)Session["IsDeputy"])

                    //if (nextRank != ""
                    //    && bal.GetApplicationRole(emp.EmployeeID,0)
                    //    .Where(a=>!a.Application.ApplicationClosed && a.EmployeeID == emp.EmployeeID
                    //    || (!a.Application.ApplicationClosed && a.Application.Application_TaskLog
                    //        .Where(atl => atl.Application.EmployeeID == emp.EmployeeID && atl.TaskID == (int)TaskIDs.Apply_for_Promotion).Count() > 0 )).Count() == 0                            
                    //    && !(bool)Session["IsDeputy"])
                    if (nextRank != ""
                        && bal.GetApplicationRole(emp.EmployeeID, 0)
                        .Where(a => !a.Application.ApplicationClosed && a.EmployeeID == emp.EmployeeID && a.RoleID == (byte)RoleID.Applicant).Count() == 0
                        && !(bool)Session["IsDeputy"])

                    {
                        lblNextRank.Text = nextRank;
                            divApply.Visible = true;
                            //if (bal.GetScientificCouncil().Count != 11)
                            //{
                            //    Emailer.Send(ConfigurationManager.AppSettings["Mail.From"], "Incomplete Scientific Council", @"The scientific council members entries is not complete. It has to be 10 members and one Chairman. No new application can start", "Auto Mailer", -1);
                            //    divApply.Enabled = false;
                            //    //ApplicatonsGrid.Enabled = false;
                            //    //GridViewClosedApps.Enabled = false;
                            //    labelProgrammaticPopup0.Text = "Ooops! Some thing went wrong! Contact Adminstrator. An email has already being sent to the Adminstrator. You cannot start a new application";
                            //    programmaticModalPopup0.Show();
                            //}
                        }
                        else
                        {

                            divApply.Visible = false;
                        }
                        lblUserName.Text = ((bool)Session["IsDeputy"]) ? "Deputy of " + emp.NameString : emp.NameString;

                    }
                    else
                    {
                        eEmp = Session["user"] as ExternalEmployee;
                        lblUserName.Text = ((bool)Session["IsDeputy"]) ? "Deputy of " + eEmp.NameString : eEmp.NameString;
                        rpApplications.DataSource = bal.GetApplicationRole()
                            .Where(ar => (ar.ExternalEmployeeID == eEmp.ExternalEmployeeID 
                            && !ar.Application.ApplicationClosed && ar.Application.Application_TaskLog
                            .Where(atl => atl.TaskID != (int)TaskIDs.Apply_for_Promotion).Count() > 0));
                        rpApplications.DataBind();
                        divCurrentAppMsg.Visible = rpApplications.Items.Count != 0;
                        divCurrentAppAbsentMsg.Visible = rpApplications.Items.Count == 0;
                        //rpApplications.DataSource = bal.GetActiveApplicationList(emp.EmployeeID);
                        //rpApplications.DataBind();
                        //gvApplicatonsGrid.DataSource = bal.GetActiveApplicationList4EE(eEmp.ExternalEmployeeID);
                        //gvApplicatonsGrid.DataBind();
                    }
                }
                catch (Exception exp)
                {

                    Session.Clear();
                    Response.Redirect(ConfigurationManager.AppSettings["LogoutURL"]);
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                Response.Cookies.Remove("langCookie");
                Session["IsAdmin"] = false;

                Session.Clear();

                Response.Redirect(ConfigurationManager.AppSettings["LogoutURL"]);
            }
        }

        private bool isVR(Employee emp)
        {
            if (bal.GetDepartmentByEmp(emp.EmployeeID).Count > 0)
            {
                foreach (Department dept in bal.GetDepartmentByEmp(emp.EmployeeID))
                {
                    if (dept.Type == DepartmentType.Vice_Rector.ToString().Replace("_", " "))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #region Properties
        public string FormMode
        {
            set
            {
                ViewState["FormMode"] = value;
            }
            get
            {
                if (ViewState["FormMode"] != null)
                {
                    return (string)ViewState["FormMode"];
                }
                else
                {
                    return "";
                }

            }
        }
        #endregion
        protected void rpClosedApplications_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    public void KFUPMCASAuthentication()
    {
        CASAuthentication clsCAS = new CASAuthentication(Request);
        KFUPMuser myUser = new KFUPMuser();
        //if (clsCAS.LogoutHappened)
        //{
        //    Session["user"] = null;
        //    Response.Redirect(clsCAS.getCasURL());
        //    return;
        //}

        try
        {
            if (clsCAS.doAuthenticate())
            {
                if (Session["user"] != null) // Page refresh issue
                {
                    return;
                }
                myUser = clsCAS.getKFUPMUser();
                //HRTableAdapters.EmployeeTableAdapter adapter = new HRTableAdapters.EmployeeTableAdapter();
                //HR.EmployeeDataTable allEmptable = adapter.GetDataByUserID(myUser.getUID());
                //HR.EmployeeDataTable deputyTable = adapter.GetDataByDeputyUserID(myUser.getUID());
                List<Employee> le = bal.GetEmployeeByEmail(myUser.getUID() + ConfigurationManager.AppSettings["OrganizationEmail1"]);
                List<Employee> leDeputy = bal.GetEmployeeByDeputyEmail(myUser.getUID() + ConfigurationManager.AppSettings["OrganizationEmail1"]);
                if (bal.GetWorkflowAttribute().Where(wa => wa.Attribute == GlobalAttribute.Password.ToString()).Count() == 0)
                {
                    Task_ExtMessages tem = bal.GetExtTaskMessage().Where(a => a.Task_Ext.Title == TaskExtID.Check_System_Notification.ToString()).ToList()[0];
                    Emailer.Send(ConfigurationManager.AppSettings["TechnicalAdminEmail"], tem.Subject, tem.Message, "AutoEmailer", null);
                    return;
                }
                string pwd = bal.GetWorkflowAttribute().Where(wa => wa.Attribute == GlobalAttribute.Password.ToString()).ToList()[0].Value;

                if (leDeputy.Count > 0)
                {
                    Session["user"] = leDeputy[0];
                    Session["IsDeputy"] = true;
                    Session["IsAdmin"] = IsAdmin(myUser.getUID() + ConfigurationManager.AppSettings["OrganizationEmail1"]);
                    Session["ExtEmp"] = false;
                }
                else
                {
                    if (le.Count > 0)
                    {
                        Session["user"] = le[0];

                    }
                    else
                    {
                        Session["user"] = "NonFacultyEmployee";
                    }
                    Session["ExtEmp"] = false;
                    Session["IsDeputy"] = false;
                    Session["IsAdmin"] = IsAdmin(myUser.getUID() + ConfigurationManager.AppSettings["OrganizationEmail1"]);
                }

            }

            else
            {
                Session["user"] = null;
                Response.Redirect(clsCAS.getCasURL());
            }
        }
        catch (Exception)
        {

            return;
        }
    }
    private bool IsAdmin(string Email)
    {
        foreach (Admin a in bal.GetAdmin())
        {
            if (a.Employee.Email.ToLower() == Email.ToLower()
                //|| a.Employee.Department1.DeputyEmail == user.ToLower() 
                //|| a.Employee.Department1.Deputy2Email == user.ToLower()
                )
            {
                return true;
            }
            foreach (Department d in bal.GetDepartmentByEmp(a.EmployeeID))
            {
                if (d.DeputyEmail != null && d.DeputyEmail.ToLower() == Email.ToLower()
                    || d.Deputy2Email != null && d.Deputy2Email.ToLower() == Email.ToLower())
                {
                    return true;
                }
            }
        }
        return false;
    }
}

 
