using System;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;
using BL.Data;
public partial class ExtForms : System.Web.UI.MasterPage
{
    public string Dir { get; set; }
    public string currentItemURL = "";
    BAL bal = new BAL();
    ExtRevBAL erBAL = new ExtRevBAL();
    //protected override void AddedControl(Control control, int index)
    //{
    //    if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
    //    {
    //        Page.ClientTarget = "uplevel";
    //    }
    //    base.AddedControl(control, index);
    //}   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ddlLang.Items.FindByValue(Language) != null)
            {
                ddlLang.Items.FindByValue(Language).Selected = true;
                
            }
        }
        
        //else
        //{
        //    return;
        //}
        if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("ddlLang"))
        {
            //catching the postback from ddlLang Control 
            //Set the Language.
            Language = Request.Form[Request.Form["__EVENTTARGET"]];
        }

        HtmlGenericControl li;
        HtmlGenericControl anchor;
        HtmlGenericControl circle;
        HtmlGenericControl span;
        List<Application_TaskForm> appTaskForms = bal.GetAppTaskFormByAppID(ApplicationID)
           .Where(a => a.TaskID == (int)TaskExtID.External_Evaluation 
           && a.ExternalReviewerID == ExtReviewerID
           && a.TaskExternal).ToList();
        Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(ApplicationID, ExtReviewerID)[0];
        if (ffer.EvaluationStatus == EvaluationStatus.Submitted + ""
            || ffer.EvaluationStatus == EvaluationStatus.Withdrawn + "")
        {
            appTaskForms.Remove(appTaskForms.Where(tf => tf.FormID == (int)FormID.ExtAction_aspx).First());
        }
        Application app = bal.GetApplication(ApplicationID)[0];
        if (!app.ApplicationClosed)
        {
            promodetails.Style["background-color"] = "#fffbe9";
            promodetails.Style["padding"] = "10px";
        }
        else
        {
            promodetails.Style["background-color"] = "#e6e6e6";
            promodetails.Style["padding"] = "10px";
        }
        lblApplicantName.Text = app.Employee.NameString;
        lblApplicantRank.Text = app.Employee.Rank;
        lblApplicantDept.Text = app.Employee.Department;
        lblForRank.Text = app.ForRank;
        lblAppStartDate.Text = app.StartDate.HasValue ? app.StartDate.Value.ToLongDateString() : "";       
        lblUserName.Text = erBAL.GetExtRevByID(ExtReviewerID)[0].NameString;

        formLevels = new Hashtable();
        formInstructions = new Hashtable();
        formIDs = new Hashtable();
        formCheckable = new Hashtable();
        //.OrderBy(t => t.Task.TaskForms.Select(tf => tf.Rank)
        foreach (Application_TaskForm atfRow in appTaskForms.OrderBy(a=>a.InsertDate))
        {
            TaskForm row = bal.GetTaskForm(atfRow.TaskID, atfRow.FormID, atfRow.TaskExternal)[0];
            circle = new HtmlGenericControl("i");
            span = new HtmlGenericControl("span");
            anchor = new HtmlGenericControl("a");
            li = new HtmlGenericControl("li");

            string itemURL = row.Form.Page + "?applicationID=" + ApplicationID;            
            if (row.Checkable == true)
            {
                //string reqMenuItem = "<span style=\"color: #669900;  font-weight: bold\">" + row.Form.Title + "</span>";
                //anchor.InnerHtml = reqMenuItem;                
                //anchor.Attributes.Add("href", itemURL);
                if (bal.GetApplicationTaskForm(ApplicationID, row.TaskID, row.FormID, true, ExtReviewerID).Count > 0
                            && bal.GetApplicationTaskForm(ApplicationID, row.TaskID, row.FormID, true, ExtReviewerID)[0].Completed)
                {
                    circle.InnerHtml = "<i class=\"fa fa-check-circle text-success \" aria-hidden=\"true\"></i> ";
                }
                else
                {
                    circle.InnerHtml = "<i class=\"fa fa-check-circle \" aria-hidden=\"true\"></i> ";
                }
               // circle.InnerHtml = "<i class=\"fa fa-check-circle \" aria-hidden=\"true\"></i> ";
                anchor.Controls.Add(circle);
                span.InnerHtml = "<span>" + row.Form.Title + "</span> ";
                anchor.Controls.Add(span);
                anchor.Attributes.Add("href", itemURL);
            }
            else
            {
                circle.InnerHtml = "<i class=\"fa fa-circle text-muted \" aria-hidden=\"true\"></i> ";
                anchor.Controls.Add(circle);
                span.InnerHtml = "<span>" + row.Form.Title + "</span> ";
                anchor.Controls.Add(span);
                anchor.Attributes.Add("href", itemURL);
                //anchor.Attributes.Add("href", itemURL);
                //anchor.InnerText = row.Form.Title;                
            }

            if (row.Form.Page == CurrentPageName)
            {
                if (row.Checkable == true)
                {
                    if (bal.GetApplicationTaskForm(ApplicationID, row.TaskID, row.FormID, true, ExtReviewerID).Count > 0
                           && bal.GetApplicationTaskForm(ApplicationID, row.TaskID, row.FormID, true, ExtReviewerID)[0].Completed)
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
                    //circle.InnerHtml = "<i class=\"fa fa-check-circle \" aria-hidden=\"true\"></i> ";
                    //anchor.Attributes.Add("class", "active");
                    //anchor.Controls.Add(circle);
                    //span.InnerHtml = "<span>" + row.Form.Title + "</span> ";
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
                //string reqMenuItem = "<span style=\"font-weight: bold\">" + row.Form.Title + "</span>";
                //anchor.InnerHtml = reqMenuItem;
            }
 

            li.Controls.Add(anchor);
            Menu.Controls.Add(li);
           

            formLevels[row.Form.Page] = (int)row.Level;
            formInstructions[row.Form.Page] = row.Instruction;
            formIDs[row.Form.Page] = row.FormID;
            formCheckable[row.Form.Page] = row.Checkable;

            /* header page-title */
            if (row.Form.Page == CurrentPageName)
            {
                lblPageTitle.Text = row.Form.Title;
                currentItemURL = itemURL;
            }
        }

        li = new HtmlGenericControl("li");
        anchor = new HtmlGenericControl("a");
        anchor.InnerText = "  ";
        li.Controls.Add(anchor);
        Menu.Controls.Add(li);


        //li = new HtmlGenericControl("li");
        //anchor = new HtmlGenericControl("a");
        //anchor.InnerText = "Other Evaluations";
        //anchor.Attributes.Add("href", "ExtMain.aspx");
        //li.Controls.Add(anchor);
        //Menu.Controls.Add(li);

        //li = new HtmlGenericControl("li");
        //anchor = new HtmlGenericControl("a");
        //anchor.InnerText = "Logout";
        //anchor.Attributes.Add("href", "../ExtLogout.aspx");
        li.Controls.Add(anchor);
        Menu.Controls.Add(li);

        
        //lblRole.Text = "المحكم";
        //lblAppOf.Text = "طلب";
        Session["forms"] = taskForms;
        Session["applicationFormLevel" + ApplicationID] = formLevels;
        Session["applicationFormInstruction" + ApplicationID] = formInstructions;
        Session["applicationFormIDs" + ApplicationID] = formIDs;
        Session["applicationFormCheckable" + ApplicationID] = formCheckable;
    }
    //protected void Page_PreRender(object sender, EventArgs e)
    //{
    // //   //if (ApplicationID == -1)
    // //   //{
    // //   //    Response.Redirect("ExtMain.aspx");
    // //   //}
    // //   //if (ExtReviewerID == -1)
    // //   //{
    // //   //    FormsAuthentication.SignOut();
    // //   //    Session.Clear();
    // //   //    Response.Redirect("../Login.aspx");
    // //   //}
    // //   if (Language == "en-US")
    // //   {
    // //       Body.Attributes["dir"] = "ltr";
    // //      // tdAppParticulars.Attributes["align"] = "right";
    // //     //  lblFooter.Text = ConfigurationManager.AppSettings["OrganizationName"] + " " + ConfigurationManager.AppSettings["OrganizationAddress"];
    // //   }
    // //   else
    // //   {
    // //       Body.Attributes["dir"] = "rtl";
    // //       //tdAppParticulars.Attributes["align"] = "left";
    // //    //   lblFooter.Text = ConfigurationManager.AppSettings["OrganizationNameAr"] + " " + ConfigurationManager.AppSettings["OrganizationAddressAr"];
    // //   }
    // //   Thread.CurrentThread.CurrentCulture = new CultureInfo(Language);
    // //   Thread.CurrentThread.CurrentUICulture = new CultureInfo(Language);
    // //   List<ExtFormInstruction> lefi = bal.ExtFormInstructionByType("External Reviewers");

    // //   formInstructions = new Hashtable();
    // //   formIDs = new Hashtable();
    // //   string currentItemURL = "";
    // //   foreach (ExtFormInstruction row in lefi)
    // //   {
    // //       string itemURL = row.Form.Page;
    // //       formInstructions[row.Form.Page] = Language == "ar-sa" ? "" : row.Instruction;
    // //       formIDs[row.Form.Page] = row.FormID;
    // //       if (row.Form.Page == CurrentPageName)
    // //       {
    // //           if (Language == "en-US")
    // //           {
    // //               Application app = bal.GetApplication(ApplicationID)[0];
    // //               lblApplicantName.Text = app.Employee.NameString;
    // //               lblApplicantRank.Text = app.Employee.Rank;
    // //               lblApplicantDept.Text = app.Employee.Department;
    // //               lblForRank.Text = app.ForRank;
    // //               lblAppStartDate.Text = app.StartDate.HasValue ? app.StartDate.Value.ToLongDateString() : "";
    // //               lblPageTitle.Text = row.Form.Title;
    // //               lblUserName.Text= erBAL.GetExtRevByID(ExtReviewerID)[0].NameString;
    // //               //lblApplicantEmail.Text = bal.GetApplicant(ApplicationID)[0].NameString;
    // //               //lblEmployee.Text = erBAL.GetExtRevByID(ExtReviewerID)[0].Name;
    // //           }
    // //           else if (Language == "ar-sa")
    // //           {
    // //               //lblApplicantEmail.Text = bal.GetApplicant(ApplicationID)[0].NameStringAr;
    // //               //lblEmployee.Text = erBAL.GetExtRevByID(ExtReviewerID)[0].NameAr;
    // //               lblPageTitle.Text = bal.GetFormByPage(row.Form.Page.Replace(".aspx", "Ar.aspx"))[0].Title;
    // //               /*A workaround: The arabic pages does not exist. The arabic version is controlled through cookies checking in each page
    // //               The names of arabic Pages are stored in the Form Table just to get the Arabic Title   
    // //               */
    // //           }
    // //       }
            
    // //   }
       
    // ////   MakeMenu(Language);
    // //   if (IsPostBack)
    // //   {
    // //       return;
    // //   }

    // //   /* printer friendly */
    // //   if (Utils.IsPrintMode())
    // //   {
    // //       //tdLeftMenu.Visible = false;
    // //       //lnkPrint.Visible = false;
    // //   }
    // //   //lnkPrint.NavigateUrl = currentItemURL + "?print=1";
        
    //}
    //private void MakeMenu(string Language)
    //{
    //    HtmlGenericControl li;
    //    HtmlGenericControl anchor;

    //    if (Language == "en-US")
    //    {
    //       // Body.Attributes["dir"] = "ltr";
    //       //tdAppParticulars.Attributes["align"] = "right";
    //        //RadioButtonListLang.SelectedIndex = 1;

    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "Home";
    //        anchor.Attributes.Add("href", "ExtMessage.aspx");
    //        li.Controls.Add(anchor);
    //        li.Attributes.Add("style","display: block;color: #000;padding: 8px 16px;text - decoration: none; ");
    //        Menu.Controls.Add(li);

    //        //li = new HtmlGenericControl("li");
    //        //anchor = new HtmlGenericControl("a");
    //        //anchor.InnerText = "My Profile";
    //        //anchor.Attributes.Add("href", "ExtRevProfile.aspx");
    //        //li.Controls.Add(anchor);
    //        //Menu.Controls.Add(li);

    //        //li = new HtmlGenericControl("li");
    //        //anchor = new HtmlGenericControl("a");
    //        //anchor.InnerText = "Application Documents";
    //        //anchor.Attributes.Add("href", "Form_AppDocs.aspx");
    //        //li.Controls.Add(anchor);
    //        //Menu.Controls.Add(li);

    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "Evaluation Form";
    //        anchor.Attributes.Add("href", "PublicationsEvaluation.aspx");
    //        li.Controls.Add(anchor);
    //        Menu.Controls.Add(li);

    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "Action";
    //        anchor.Attributes.Add("href", "ExtAction.aspx");
    //        li.Controls.Add(anchor);
    //        Menu.Controls.Add(li);

    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "  ";
    //        li.Controls.Add(anchor);
    //        Menu.Controls.Add(li);


    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "Other Evaluations";
    //        anchor.Attributes.Add("href", "ExtMain.aspx");
    //        li.Controls.Add(anchor);
    //        Menu.Controls.Add(li);

    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "Logout";
    //        anchor.Attributes.Add("href", "../ExtLogout.aspx");
    //        li.Controls.Add(anchor);
    //        Menu.Controls.Add(li);
            
           
    //    }
    //    else if (Language == "ar-sa")
    //    {
    //       // Body.Attributes["dir"] = "rtl";
    //        //tdAppParticulars.Attributes["align"] = "left";
    //        //RadioButtonListLang.SelectedIndex = 0;

    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "الصفحة الرئيسية";
    //        anchor.Attributes.Add("href", "ExtMessage.aspx");
    //        li.Controls.Add(anchor);
    //        Menu.Controls.Add(li);

    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "ملفي الشخصي";
    //        anchor.Attributes.Add("href", "ExtRevProfile.aspx");
    //        li.Controls.Add(anchor);
    //        Menu.Controls.Add(li);

    //        //li = new HtmlGenericControl("li");
    //        //anchor = new HtmlGenericControl("a");
    //        //anchor.InnerText = "وثاثق الطلب";
    //        //anchor.Attributes.Add("href", "Form_AppDocs.aspx");
    //        //li.Controls.Add(anchor);
    //        //Menu.Controls.Add(li);

    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "نموذج المحكِّم";
    //        anchor.Attributes.Add("href", "Form_Evaluation.aspx");
    //        li.Controls.Add(anchor);
    //        Menu.Controls.Add(li);

    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "  ";
    //        li.Controls.Add(anchor);
    //        Menu.Controls.Add(li);

    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "التقييمات الأخرى";
    //        anchor.Attributes.Add("href", "ExtMain.aspx");
    //        li.Controls.Add(anchor);
    //        Menu.Controls.Add(li);

    //        li = new HtmlGenericControl("li");
    //        anchor = new HtmlGenericControl("a");
    //        anchor.InnerText = "الخروج";
    //        anchor.Attributes.Add("href", "../ExtLogout.aspx");
    //        li.Controls.Add(anchor);
    //        Menu.Controls.Add(li);

    //        //lblRole.Text = "المحكم";
    //        //lblAppOf.Text = "طلب";
    //    }
    //}
    protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
    {
        Language = ddlLang.SelectedValue;
       // Response.Redirect(Request.RawUrl);
    }
    public FormStatusStruct CheckFormTask(TaskForm form)
    {
        foreach (Application_TaskForm row in FormsStatus)
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
    //    bal.DeletetAppTskFrm(ApplicationID, (int)TaskExtID.External_Evaluation, CurrentFormID, true, ExtReviewerID);
    //    bal.InsertAppTskFrm(ApplicationID, (int)TaskExtID.External_Evaluation, CurrentFormID, isComplete, message, true, ExtReviewerID);
        bal.UpdateAppTskFrm(ApplicationID, (int)TaskExtID.External_Evaluation, CurrentFormID, isComplete, message, true, ExtReviewerID);
        // FormsStatus = null;
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
    #region Properties
    public string Language
    {
        set
        {
            Session["Language"] = value;
        }
        get
        {
            if (Session["Language"] != null)
            {
                return Session["Language"].ToString();
            }
            else
            {
                return "en-US";
            }
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
                    Response.Redirect("~/ExtMain.aspx");

                }
            }
            return applicationID;
        }
    }
    //public int ApplicationID
    //{
    //    set
    //    {
    //        ViewState["ApplicationID"] = value;
    //    }
    //    get
    //    {
    //        if (ViewState["ApplicationID"] != null)
    //        {
    //            return (int)ViewState["ApplicationID"];
    //        }
    //        else if (Session["ApplicationID"] != null)
    //        {
    //            return int.Parse(Request.QueryString["applicationID"]);
    //        }
    //        else
    //        {
    //            Response.Redirect("ExtMain.aspx");
    //            return -1;
    //        }
    //    }
    //}
    public int ExtReviewerID
    {
        set
        {
            ViewState["ExtReviewerID"] = value;
        }
        get
        {
            if (ViewState["ExtReviewerID"] != null)
            {
                return (int)ViewState["ExtReviewerID"];
            }
            else if (Session["ExtRevID"] != null)
            {
                return int.Parse(Session["ExtRevID"].ToString());
            }
            else
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Response.Redirect("../Login.aspx");
                return -1;
            }
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
    private Hashtable formInstructions = null;
    public Hashtable FormInstructions
    {
        get
        {
            if (formInstructions == null)
            {
                if (Session["applicationFormInstruction" + ApplicationID] != null)
                    formInstructions = (Hashtable)Session["applicationFormInstruction" + ApplicationID];
                else
                    formInstructions = new Hashtable();
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
                return null;
            else
                return FormInstructions[CurrentPageName].ToString();
        }
    }
    public byte CurrentFormID
    {
        get
        {
            if (FormIDs[CurrentPageName] == null)
            {
                return byte.Parse("-1");
            }
            else
            {
                return (byte)FormIDs[CurrentPageName];
            }
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
    private List<Application_TaskForm> formsStatus = null;

    public List<Application_TaskForm> FormsStatus
    {
        get
        {
            if (formsStatus == null)
            {                
                formsStatus = bal.GetAppTaskFormByAppID(ApplicationID, (int) TaskExtID.External_Evaluation,  true,ExtReviewerID);
            }
            return formsStatus;
        }
        set
        {
            formsStatus = value;
        }
    }
    #endregion
}
//public struct FormStatusStruct
//{

//    public string task;
//    public string Task
//    {
//        get { return task; }
//    }
//    private bool status;
//    public bool Status
//    {
//        get { return status; }
//    }
//    private string message;

//    public string Message
//    {
//        get { return message; }
//        set { message = value; }
//    }


//    public FormStatusStruct(string t, bool s, string m)
//    {
//        task = t;
//        status = s;
//        message = m;
//    }
//}