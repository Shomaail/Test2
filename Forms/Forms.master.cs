using System;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Linq;
using BL.Data;
using System.Web;
using Newtonsoft.Json;

public partial class Forms : MasterPage
    {
        public BAL bal = new BAL();
        public string currentItemURL = "";
        protected override void AddedControl(Control control, int index)
        {
            if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                Page.ClientTarget = "uplevel";
            }
            base.AddedControl(control, index);
        }
        public void Page_Load(object sender, EventArgs e)
        {
            //if (IsPostBack) { return; }
            try
            {
                if (Session["user"] == null || Request.QueryString["applicationID"] == null || Request.QueryString["roleID"] == null)
                {
                    Session.Clear();
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                    return;
                }

                // List<Application_TaskLog> latl = bal.GetAppTaskLog(ApplicationID).Where(a => !a.Completed).ToList();
                if(CurRoleID == (int)RoleID.Promotion_Committee_Chairman 
                || CurRoleID == (int)RoleID.Promotion_Committee_Member_1
                || CurRoleID == (int)RoleID.Promotion_Committee_Member_2
                || CurRoleID == (int)RoleID.Promotion_Committee_Member_3
                || CurRoleID == (int)RoleID.Promotion_Committee_Member_4)
                {
                    //List<Employee> le = new List<Employee>();
                    List<Form_FinalPC> ffpc = bal.GetForm_FinalPC(ApplicationID);
                    //le.Add(new Employee { EmployeeID = ffpc[0].EmployeeID });
                    //le.Add(new Employee { EmployeeID = ffpc[1].EmployeeID });
                    //le.Add(new Employee { EmployeeID = ffpc[2].EmployeeID });
                    //le.Add(new Employee { EmployeeID = ffpc[3].EmployeeID });
                    //le.Add(new Employee { EmployeeID = ffpc[4].EmployeeID });
                    //PCMembers= JsonConvert.SerializeObject(le);
                    PCMembers = ffpc[0].EmployeeID + "_" + ffpc[1].EmployeeID + "_" + ffpc[2].EmployeeID + "_" + ffpc[3].EmployeeID + "_" + ffpc[4].EmployeeID;
                }
                DatabindControls();
                // Application_TaskLog atl = new Application_TaskLog();
                //if (!(bool)Session["ExtEmp"])
                //{
                //    atl = latl.Where(at => at.Task.Role.Application_Role.Where(ar => ar.EmployeeID == Employee.EmployeeID && ar.RoleID == CurRoleID).Count() > 0).First();
                //    employee = atl.Task.Role.Application_Role.Where(ar => ar.ApplicationID == ApplicationID).Select(ar => ar.Employee).First();
                //    //lblEmployee.Text = Employee.NameString + " (" + Employee.Rank + ")";
                //    lblRole.Text = atl.Task.Role.Title;
                //}
                //else
                //{
                //    atl = latl.Where(at => at.Task.Role.Application_Role.Where(ar => ar.ExternalEmployeeID == EEmployee.ExternalEmployeeID && ar.RoleID == CurRoleID).Count() > 0).First();
                //    eEmployee = atl.Task.Role.Application_Role.Where(ar => ar.ApplicationID == ApplicationID).Select(ar => ar.ExternalEmployee).First();
                //    //lblEmployee.Text = EEmployee.NameString + " (" + EEmployee.Rank + " in " + EEmployee.Organization + ")";
                //    lblRole.Text = atl.Task.Role.Title;
                //}
                lblRole.Text = bal.GetRoleByPK(CurRoleID)[0].Title;

                lblTask.Text = bal.GetTask(TaskID)[0].Title.Replace("@@TopAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopAuthority_TitleShort"])
                    .Replace("@@TopLowAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"])
                    .Replace("@@TopMostAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"]);
                /* Forms Levels */
                formLevels = new Hashtable();
                formInstructions = new Hashtable();
                formIDs = new Hashtable();
                formCheckable = new Hashtable();
                /* get menu items */
                List<TaskForm> taskForms;
                foreach (FormCategory fc in bal.GetFormCategory().Where(a => a.FormCategoryID != (int)FormCategoryID.Application_Links))
                {
                    taskForms = bal.GetTaskFormByTask(TaskID, false)
                        .Where(a => a.Form.FormCategoryID == fc.FormCategoryID).ToList();



                    HtmlGenericControl li;
                    HtmlGenericControl anchor;
                    HtmlGenericControl circle;
                    HtmlGenericControl span;
                    li = new HtmlGenericControl("li");
                    li.Attributes.Add("class", "title");
                    li.InnerText = fc.Title.Replace("_", " ");
                    HtmlGenericControl Menu = FindControl("Menu" + fc.FormCategoryID) as HtmlGenericControl;
                    Menu.Controls.Add(li);
                    foreach (TaskForm row in taskForms)
                    {
                        li = new HtmlGenericControl("li");
                        //li.Attributes["class"] = "text-muted";

                        circle = new HtmlGenericControl("i");
                        span = new HtmlGenericControl("span");
                        anchor = new HtmlGenericControl("a");

                        string itemURL = row.Form.Page + "?applicationID=" + ApplicationID + "&roleID=" + CurRoleID;
                        // MenuItem item;

                        if (row.Checkable == true)
                        {
                            if (bal.GetApplicationTaskForm(ApplicationID, row.TaskID, row.FormID, false, 0).Count > 0
                                && bal.GetApplicationTaskForm(ApplicationID, row.TaskID, row.FormID, false, 0)[0].Completed)
                            {
                                circle.InnerHtml = "<i class=\"fa fa-check-circle text-success \" aria-hidden=\"true\"></i> ";
                            }
                            else
                            {
                                circle.InnerHtml = "<i class=\"fa fa-check-circle \" aria-hidden=\"true\"></i> ";
                            }
                            anchor.Controls.Add(circle);
                            span.InnerHtml = "<span>" + row.Form.Title + "</span> ";
                            anchor.Controls.Add(span);
                            anchor.Attributes.Add("href", itemURL);

                            //string reqMenuItem = "<span style=\"color: #669900; font-weight: bold\">" + row.Form.Title + "</span>";
                            //anchor.InnerHtml = reqMenuItem;                    
                            //anchor.Attributes.Add("href", itemURL);                    
                            //    item = new MenuItem(reqMenuItem, row.FormID.ToString(), "~/images/Pointer.png", itemURL);
                            //item.ToolTip = "To complete your task, fill out this form and go to Action Menu item for required action";

                        }
                        else
                        {
                            circle.InnerHtml = "<i class=\"fa fa-circle text-muted \" aria-hidden=\"true\"></i> ";
                            anchor.Controls.Add(circle);
                            span.InnerHtml = "<span>" + row.Form.Title + "</span> ";
                            anchor.Controls.Add(span);
                            anchor.Attributes.Add("href", itemURL);
                        }

                        if (row.Form.Page == CurrentPageName)
                        {
                            if (row.Checkable == true)
                            {
                                if (bal.GetApplicationTaskForm(ApplicationID, row.TaskID, row.FormID, false, 0).Count > 0
                               && bal.GetApplicationTaskForm(ApplicationID, row.TaskID, row.FormID, false, 0)[0].Completed)
                                {
                                    circle.InnerHtml = "<i class=\"fa fa-check-circle text-success \" aria-hidden=\"true\"></i> ";
                                    anchor.Attributes.Add("class", "active");
                                    anchor.Controls.Add(circle);
                                    span.InnerHtml = "<span>" + row.Form.Title + "</span> ";
                                }
                                else
                                {
                                    circle.InnerHtml = "<i class=\"fa fa-check-circle \" aria-hidden=\"true\"></i> ";
                                    anchor.Attributes.Add("class", "active");
                                    anchor.Controls.Add(circle);
                                    span.InnerHtml = "<span>" + row.Form.Title + "</span> ";
                                }
                            }
                            else
                            {
                                circle.InnerHtml = "<i class=\"fa fa-circle text-muted \" aria-hidden=\"true\"></i> ";
                                anchor.Attributes.Add("class", "active");
                                anchor.Controls.Add(circle);
                                span.InnerHtml = "<span>" + row.Form.Title + "</span> ";
                                anchor.Controls.Add(span);
                                anchor.Attributes.Add("href", itemURL);
                            }


                            anchor.Controls.Add(span);
                            anchor.Attributes.Add("href", itemURL);
                            //  anchor.InnerText = "<span style=\"color: #FFFFFF\">" + row.Title + "</span>";
                            // anchor.InnerText =  row.Form.Title ;
                            //  item.Text = "<span style=\"color: #FFFFFF\">" + row.Title + "</span>";
                            //item.Selected = true;
                            li.Attributes.Add("class", "active");
                        }


                        li.Controls.Add(anchor);

                        Menu.Controls.Add(li);


                        /* header page-title */
                        if (row.Form.Page == CurrentPageName)
                        {
                            lblPageTitle.Text = row.Form.Title.Replace("<br>","").Replace("&nbsp;", "");
                            currentItemURL = itemURL;
                        }
                    }
                }
                taskForms = bal.GetTaskFormByTask(TaskID, false);
                foreach (TaskForm row in taskForms)
                {
                    string itemURL = row.Form.Page + "?applicationID=" + ApplicationID + "&roleID=" + CurRoleID;
                    if (row.Form.Page == CurrentPageName)
                    {
                        lblPageTitle.Text = row.Form.Title.Replace("<br>", "").Replace("&nbsp;", "");
                        currentItemURL = itemURL;
                    }
                    formLevels[row.Form.Page] = (int)row.Level;
                    formInstructions[row.Form.Page] = row.Instruction;
                    formIDs[row.Form.Page] = row.Form.FormID;
                    formCheckable[row.Form.Page] = row.Checkable;
                }
                
                Session["forms"] = taskForms;
                Session["applicationFormLevel" + ApplicationID] = formLevels;
                Session["applicationFormInstruction" + ApplicationID] = formInstructions;
                Session["applicationFormIDs" + ApplicationID] = formIDs;
                Session["applicationFormCheckable" + ApplicationID] = formCheckable;

                /* header role */

                /* printer friendly */
                if (Utils.IsPrintMode())
                {
                    //HyperLinkLogout.Visible = false;
                    //PanelQuickJump.Visible = false;
                    //   tdLeftMenu.Visible = false;
                    //lnkPrint.Visible = false;
                    //divFooter.Visible = false;
                    //      divTop.Visible = false;
                    bodyMaster.Style.Clear();
                    // ImageButtonSearch.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                if (ex.Message == "There is no row at position 0.")
                {
                    Response.Redirect("~/Logout.aspx");
                }
                else
                {
                    FormsAuthentication.RedirectToLoginPage();
                }

                //Response.Write(ex.ToString());
            }
        }
        public int GetNoOfActiveTask(int appID)
        {
            return bal.GetAppTaskLog(appID)
                        .Where(atl => !atl.Completed)
                        .GroupBy(atl => atl.TaskID)
                        .Select(group => group.First()).Count();
        }
        public bool IsActing()
        {
            return bal.GetApplicationRole(ApplicationID, curRoleID).Where(a =>
            a.IsActing == "Acting").ToList().Count > 0;
        }

        private void DatabindControls()
        {
            
            lblStatus.DataBind();
            lbtnAction.DataBind();
            lbtnMessage.DataBind();
            lbtnPromotionTracking.DataBind();
            lbtnApplicationRoles.DataBind();
            lbtnCorrespondence.DataBind();
            lbtnActiveTask.DataBind();
            lbtnMeeting.DataBind();
            lbtnAdmin.DataBind();
            lbtnSearchEmp.Attributes.Add("onclick", "window.open ('SearchPop.aspx',null,'scrollbars=yes, status= no, resizable = yes, toolbar=no,location=no,height = 700, width = 900, left = 200, top= 200, screenx=10,screeny=600,menubar=no');");
            lbtnSearchEmp.DataBind();
            if(!app.ApplicationClosed)
            {
                promodetails.Style["background-color"] = "#fffbe9";
                promodetails.Style["padding"] = "10px";
            }
            else
            {
                
                promodetails.Style["background-color"] = "#e6e6e6";
                promodetails.Style["padding"] = "10px";
            }
            
            if (Request.QueryString["applicationID"] != null)
            {
                //int applicationID = int.Parse(Request.QueryString["applicationID"]);
                //Application app = bal.GetApplication(applicationID)[0];
                lblApplicantName.Text = app.Employee.NameString;
                lblApplicantDept.Text = app.Employee.Department;
                lblApplicantEmail.Text = app.Employee.Email;
                lblApplicantRank.Text = app.Employee.Rank;
                lblAppStartDate.Text = app.StartDate != null ? app.StartDate.Value.ToLongDateString() : "";
                lblForRank.Text = app.ForRank;
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
                        if (bal.GetApplication(emp.EmployeeID)
                            .Where(a=>!a.ApplicationClosed && a.ForRank == App.ForRank).Count() > 1)
                        {
                            divWarningMultiplsApp.Visible = true;
                        }
                        else
                        {
                            divWarningMultiplsApp.Visible = false;
                        }
                        try
                        {
                            if (bal.GetWorkflowAttribute((int)GlobalAttribute.StopEmail)[0].Value == "true")
                            {
                                divWarningEmailStopped.Visible = true;
                            }
                            else
                            {
                                divWarningEmailStopped.Visible = false;
                            }
                        }
                        catch (Exception)
                        {

                            
                        }
                    }
                    else
                    {
                        eEmp = Session["user"] as ExternalEmployee;
                        lblUserName.Text = ((bool)Session["IsDeputy"]) ? "Deputy of " + eEmp.NameString : eEmp.NameString;
                    }
                    lblDeputy.DataBind();
                    lblRole.DataBind();
                    lblActingRole.DataBind();
                }
                catch (Exception exp)
                {

                    Session.Clear();
                    throw (exp);
                }
            }
        }
        /* check if a task is completed */
        //refactor: return list of tasks
        public FormStatusStruct CheckFormTask(TaskForm form)
        {
            foreach (TaskFormDTO row in FormsStatus)
            {
                if (row.FormID == form.FormID)
                {
                    return new FormStatusStruct(form.Form.Title.Replace("<br>", ""), row.Completed, row.Message.Length > 0 ? row.Message : "Incomplete!");
                }
            }
            return new FormStatusStruct();
        }
        private void ReportFormStatus(bool isComplete, string message)
        {
            bal.DeletetAppTskFrm(ApplicationID, TaskID, CurrentFormID, false, 0);
            bal.InsertAppTskFrm(ApplicationID, TaskID, CurrentFormID, isComplete, message, false, 0);
            FormsStatus = null;
        }
        public void ReportSuccess()
        {
            ReportFormStatus(true, "");
        }
        public void ReportSuccess(string OKMessage)
        {
            ReportFormStatus(true, OKMessage);
        }

        public void ReportFailure(string errorMessage)
        {
            ReportFormStatus(false, errorMessage);
        }

        #region propertires
        // public vw_ActiveTask1 activeTask = null;
        //public vw_ActiveTask1 ActiveTask
        //{
        //    get
        //    {
        //        if (activeTask == null)
        //        {
        //            if (Session["applicationTask" + ApplicationID] != null)
        //            {
        //                activeTask = (vw_ActiveTask1)Session["applicationTask" + ApplicationID];
        //            }
        //            else
        //            {
        //                if(Employee != null)
        //                {
        //                    activeTask = bal.GetActiveTask(ApplicationID, Employee.EmployeeID)[0];
        //                }
        //                else
        //                {
        //                    Response.Redirect("~/Main.aspx");
        //                }

        //            }
        //        }

        //        return activeTask;
        //    }
        //}

        private int taskID = -1;
        public int TaskID
        {
            get
            {
                if (taskID == -1)
                {
                    try
                    {
                        //List<Application_TaskLog> latl = bal.GetAppTaskLog(ApplicationID)
                        //    .Where(a => !a.Completed).ToList();
                        ////&& a.Task.Role.Application_Role.Where(ar => ar.EmployeeID == Employee.EmployeeID && ar.RoleID == CurRoleID).Count() > 0).ToList();
                        //foreach (Application_TaskLog atl in latl)
                        //{
                        //    if (atl.Task.RoleID == CurRoleID && bal.GetApplicationRole(ApplicationID, atl.Task.RoleID).Where(ar => ar.EmployeeID == Employee.EmployeeID).Count() > 0)
                        //    {
                        //        taskID = atl.TaskID.Value;
                        //        break;
                        //    }
                        //    else
                        //    {
                        //        taskID = -1;
                        //    }
                        //}
                        List<Application_TaskLog> latl = bal.GetAppTaskLog(ApplicationID).Where(a => !a.Completed).ToList();
                        Application_TaskLog atl = latl.Where(at => at.Task.Role.Application_Role.Where(ar => ar.EmployeeID == Employee.EmployeeID && ar.RoleID == CurRoleID).Count() > 0).First();
                        taskID = atl.Task.TaskID;

                    }
                    catch (Exception exc)
                    {
                        Response.Redirect("~/Main.aspx");
                        return -1;
                    }
                }
                return taskID;
            }
        }
        //public byte RoleID
        //{
        //    get
        //    {
        //        List<Application_TaskLog> latl = bal.GetAppTaskLog(ApplicationID).Where(a => !a.Application.ApplicationClosed && !a.Completed).ToList();
        //        if(Employee != null)
        //        {
        //            Application_TaskLog atl = latl.Where(at => at.Task.Role.Application_Role.Where(ar => ar.EmployeeID == Employee.EmployeeID).Count() > 0).First();
        //            return atl.Task.Role.RoleID;
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }
        //}
        private byte curRoleID = 0;
        public byte CurRoleID
        {
            get
            {
                if (curRoleID == 0)
                {
                    try
                    {
                        curRoleID = byte.Parse(Cryptography.Decrypt(Request.QueryString["roleID"].Replace(" ", "+")));
                    }
                    catch (Exception)
                    {
                        Response.Redirect("~/Main.aspx");

                    }
                }
                return curRoleID;
            }
        }
        private Hashtable formCheckable = null;
        public Hashtable FormCheckable
        {
            get
            {
                if (formCheckable == null)
                {
                    if (Session["applicationFormCheckable" + ApplicationID] != null)
                    {
                        formCheckable = (Hashtable)Session["applicationFormCheckable" + ApplicationID];
                    }
                    else
                    {
                        formCheckable = new Hashtable();
                    }
                }
                return formCheckable;
            }
        }
        public Hashtable formLevels = null;
        public Hashtable FormLevels
        {
            get
            {
                if (formLevels == null)
                {
                    if (Session["applicationFormLevel" + ApplicationID] != null)
                    {
                        formLevels = (Hashtable)Session["applicationFormLevel" + ApplicationID];
                    }
                    else
                    {
                        formLevels = new Hashtable();
                    }
                }
                return formLevels;
            }
        }
        private int applicationID = -1;
        public int ApplicationID
        {
            get
            {
                if (applicationID == -1)
                {
                    try
                    {
                        applicationID = int.Parse(Request.QueryString["applicationID"]);
                    }
                    catch (Exception)
                    {
                        Response.Redirect("~/Main.aspx");

                    }
                }
                return applicationID;
            }
        }
        private Application app = null;
        public Application App
        {
            get
            {
                if (app == null)
                {
                    try
                    {
                        app = bal.GetApplication(ApplicationID)[0];
                    }
                    catch (Exception)
                    {
                        return null;

                    }
                }
                return app;
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
        private Employee applicant = null;
        public Employee Applicant
        {

            get
            {
                if (applicant == null)
                {
                    if (bal.GetApplicant(ApplicationID).Count() > 0)
                    {
                        applicant = bal.GetApplicant(ApplicationID)[0];
                    }
                }
                return applicant;

            }
        }
        private List<TaskForm> taskForms = null;
        public List<TaskForm> TaskForms
        {
            get
            {
                if (taskForms == null)
                {
                    taskForms = (List<TaskForm>)Session["forms"];
                }

                return taskForms;
            }
        }

        string currPageName = null;
        public string CurrentPageName
        {
            get
            {
                if (currPageName == null)
                {
                    currPageName = Page.Request.ServerVariables["SCRIPT_NAME"];
                    currPageName = currPageName.Substring(currPageName.LastIndexOf("/") + 1);
                }
                return currPageName;
            }
        }
        public int CurrentFormLevel
        {
            get
            {
                if (FormLevels[CurrentPageName] == null)
                {
                    return -1;
                }
                else
                {
                    return (int)FormLevels[CurrentPageName];
                }
            }

        }


        private Hashtable formInstructions = null;
        public Hashtable FormInstructions
        {
            get
            {
                if (formInstructions == null)
                {
                    if (Session["applicationFormInstruction" + ApplicationID] != null)
                    {
                        formInstructions = (Hashtable)Session["applicationFormInstruction" + ApplicationID];
                    }
                    else
                    {
                        formInstructions = new Hashtable();
                    }
                }
                return formInstructions;
            }
        }

        private Hashtable formIDs = null;
        public Hashtable FormIDs
        {
            get
            {
                if (formIDs == null)
                {
                    if (Session["applicationFormIDs" + ApplicationID] != null)
                    {
                        formIDs = (Hashtable)Session["applicationFormIDs" + ApplicationID];
                    }
                    else
                    {
                        formIDs = new Hashtable();
                    }
                }
                return formIDs;
            }
        }
        public string CurrentFormInstruction
        {
            get
            {
                if (FormInstructions[CurrentPageName] == null)
                {
                    return null;
                }
                else
                {
                    return FormInstructions[CurrentPageName].ToString()== ""? "" : FormInstructions[CurrentPageName].ToString()
                        .Replace("@@OrganizationEmail1@@", ConfigurationManager.AppSettings["OrganizationEmail1"].ToString())
                        .Replace("@@OrganizationEmail2@@", ConfigurationManager.AppSettings["OrganizationEmail2"].ToString())
                        .Replace("@@OrganizationShortName@@", ConfigurationManager.AppSettings["OrganizationShortName"].ToString())
                        .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"].ToString());
                }
            }
        }
        public byte CurrentFormID
        {
            get
            {
                if (FormIDs[CurrentPageName] == null)
                {
                    return byte.Parse("0");
                }
                else
                {
                    return (byte)FormIDs[CurrentPageName];
                }
            }
        }
        private List<TaskFormDTO> formsStatus = null;
        //public Promotion.ApplicationTaskFormDataTable FormsStatus
        //{
        //    get
        //    {
        //        if (formsStatus == null)
        //        {
        //            if (Session["applicationFormsStatus" + ApplicationID] != null)
        //                formsStatus = (Promotion.ApplicationTaskFormDataTable)Session["applicationFormsStatus" + ApplicationID];
        //            else
        //            {
        //                formsStatus = new PromotionTableAdapters.ApplicationTaskFormTableAdapter().GetDataByApplication(ApplicationID, TaskID);
        //                Session["applicationFormsStatus" + ApplicationID] = formsStatus;
        //            }

        //        }
        //        return formsStatus;
        //    }
        //    set
        //    {
        //        formsStatus = value;
        //        Session["applicationFormsStatus" + ApplicationID] = value;
        //    }
        //}
        public List<TaskFormDTO> FormsStatus
        {
            get
            {
                if (formsStatus == null)
                {

                    formsStatus = bal.GetTaskFormByAppIDTask(ApplicationID, TaskID, false, 0);
                }
                return formsStatus;
            }
            set
            {
                formsStatus = value;
            }
        }


    public string PCMembers
    {
        set
        {
            ViewState["PCMembers"] = value;
        }
        get
        {
            if (ViewState["PCMembers"] != null)
            {
                return (string)ViewState["PCMembers"];
            }
            else
            {
                return "";
            }
        }
    }
    #endregion
    //public void ddlQuickJump_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Response.Redirect(Request.RawUrl.Replace(ApplicationID.ToString(), ddlQuickJump.SelectedValue),false);
    //    }
    //    catch (Exception exp)
    //    {
    //        throw (exp);

    //    }
    //}

    //protected void lbtnActiveTask_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("../Tasks.aspx?applicationID=" + ApplicationID + "&roleID=ae54sdafaas5313qw" + CurRoleID + "f98poj64lsks98336hujm9811336fsdf",true);
    //}
} 
