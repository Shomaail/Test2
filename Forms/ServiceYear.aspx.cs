using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BL.Data;
public partial class Forms_ServiceYear : System.Web.UI.Page
    {
        BAL bal = new BAL();
        public enum Mode { InputByApplicant = 1, InputByDeptComm =2, View = 3}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) { return; }

            try
            {
                DatabindControls();
            }
            catch (Exception )
            {
                Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            }
            if (Master.CurrentFormLevel == -1)
            {
                Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
                return;
            }
        }
        private void DatabindControls()
        {
            SwitchMode((Mode)Master.CurrentFormLevel);

            divTopOfFormMsgFailure.DataBind();
            lblMessageFailure.DataBind();
            divTopOfFormMsgSuccess.DataBind();
            lblMessageSuccess.DataBind();
            divServiceYears.DataBind();
        }
        private void ShowError(string message)
        {
            Master.ReportFailure(message);
        }

        private void ShowMessage(string message)
        {
            Master.ReportSuccess(message);
        }
        public void SwitchMode(Mode mode)
        {
            trServiceYears.Visible = false;
            trSaudiYears.Visible = false;
            trVerfServiceYears.Visible = false;
            trVerfSaudiYears.Visible = false;

            switch (mode)
            {
                case Mode.InputByApplicant:
                    trServiceYears.Visible = true;
                    trSaudiYears.Visible = true;
                    trVerfServiceYears.Visible = false;
                    trVerfSaudiYears.Visible = false;

                    if (bal.GetForm_ServiceYears(Master.ApplicationID).Count > 0)
                    {
                        
                        Form_ServiceYears fsy = bal.GetForm_ServiceYears(Master.ApplicationID)[0];
                  
                        tbYears.Text = fsy.App_Service_NumYears.ToString();
                        tbMonths.Text = fsy.App_Service_NumMonths.ToString();
                        tbYearsInSaudi.Text = fsy.App_Saudi_NumYears.ToString();
                        tbMonthsInSaudi.Text = fsy.App_Saudi_NumMonths.ToString();
                        if (fsy.App_Service_NumMonths > 11 || fsy.App_Saudi_NumMonths > 11)
                        {
                            ShowError("Number of months should be less than 12");
                            break;
                        }
                        if ((fsy.App_Service_NumYears + (fsy.App_Service_NumMonths / 12.0)) < 3.5 && fsy.Application.Employee.Rank != RankProfessorial.Visiting_Associate_Professor.ToString().Replace("_", " "))
                        {
                            ShowError("Number of years in the current rank must be atleast 3 years and 6 months");
                        }
                        if ((fsy.App_Service_NumYears + (fsy.App_Service_NumMonths / 12.0)) < 1.0 && fsy.Application.Employee.Rank == RankProfessorial.Visiting_Associate_Professor.ToString().Replace("_", " "))
                        {
                            ShowError("Number of years in the current rank (Visiting Associate Professor) must be atleast 1 year");
                        }
                        else if ((fsy.App_Saudi_NumYears + (fsy.App_Saudi_NumMonths / 12.0)) < 1)
                        {
                            ShowError("Number of years in Saudi must be atleast 1 year");
                        }
                        else
                        {
                            ShowMessage("Number of years of service meets the Eligibility");
                        }
                    }
                    else 
                    {
                        ShowError("Number of years in the current rank must be atleast 3 years and 6 months for Assitant and Associate Professor and Number of years in Saudi must be atleast 1 year");
                    }
                    break;

                case Mode.InputByDeptComm:
                    trVerfServiceYears.Visible = true;
                    trVerfSaudiYears.Visible = true;
                    if (bal.GetForm_ServiceYears(Master.ApplicationID).Count > 0)
                    {
                        Form_ServiceYears fsy = bal.GetForm_ServiceYears(Master.ApplicationID)[0];
                       
                        tbYears.Text = fsy.App_Service_NumYears.ToString();
                        tbMonths.Text = fsy.App_Service_NumMonths.ToString();
                        tbYearsInSaudi.Text = fsy.App_Saudi_NumYears.ToString();
                        tbMonthsInSaudi.Text = fsy.App_Saudi_NumMonths.ToString();
                        tbVerifiedYears.Text = fsy.Service_NumYears.ToString();
                        tbVerifiedMonths.Text = fsy.Service_NumMonths.ToString();
                        tbVerifiedYearsInSaudi.Text = fsy.Saudi_NumYears.ToString();
                        tbVerifiedMonthsInSaudi.Text = fsy.Saudi_NumMonths.ToString();
                        if (fsy.App_Service_NumMonths > 11 || fsy.App_Saudi_NumMonths > 11
                           || fsy.Service_NumMonths > 11 || fsy.Saudi_NumMonths > 11)
                        {
                            ShowError("Number of months should be less than 12");
                            break;
                        }
                        if ((fsy.App_Service_NumYears + (fsy.App_Service_NumMonths / 12.0)) < 3.5 && fsy.Application.Employee.Rank != RankProfessorial.Visiting_Associate_Professor.ToString().Replace("_", " "))
                        {
                            ShowError("Number of years in the current rank must be atleast 3 years and 6 months");
                        }
                        if ((fsy.App_Service_NumYears + (fsy.App_Service_NumMonths / 12.0)) < 1.0 && fsy.Application.Employee.Rank ==   RankProfessorial.Visiting_Associate_Professor.ToString().Replace("_", " "))
                        {
                            ShowError("Number of years in the current rank (Visiting Associate Professor) must be atleast 1 year");
                        }
                        else if ((fsy.App_Saudi_NumYears + (fsy.App_Saudi_NumMonths / 12.0)) < 1 && fsy.Application.Employee.Rank != RankProfessorial.Visiting_Associate_Professor.ToString().Replace("_", " "))
                        {
                            ShowError("Number of years in Saudi must be atleast 1 year");
                        }
                        else if ((fsy.Service_NumYears + (fsy.Service_NumMonths / 12.0)) < 3.5 && fsy.Application.Employee.Rank != RankProfessorial.Visiting_Associate_Professor.ToString().Replace("_", " "))
                        {
                            ShowError("Number of years in the current rank must be atleast 3 years and 6 months");
                        }
                        else if ((fsy.Saudi_NumYears + (fsy.Saudi_NumMonths / 12.0)) < 1 && fsy.Application.Employee.Rank != RankProfessorial.Visiting_Associate_Professor.ToString().Replace("_", " "))
                        {
                            ShowError("Number of years in Saudi must be atleast 1 year");
                        }
                        else
                        {
                            ShowMessage("Number of years of service meets the Eligibility");
                        }
                    }
                    else
                    {
                        ShowError("Number of years in the current rank must be atleast 3 years and 6 months for Assitant and Associate Professor and Number of years in Saudi must be atleast 1 year");
                    }
                    break;
                case Mode.View:
                    trServiceYears.Visible = true;
                    trSaudiYears.Visible = true;
                    trVerfServiceYears.Visible = true;
                    trVerfSaudiYears.Visible = true;
                    if (bal.GetForm_ServiceYears(Master.ApplicationID).Count > 0)
                    {
                        Form_ServiceYears fsy = bal.GetForm_ServiceYears(Master.ApplicationID)[0];
               
                        tbYears.Text = fsy.App_Service_NumYears.ToString();
                        tbMonths.Text = fsy.App_Service_NumMonths.ToString();
                        tbYearsInSaudi.Text = fsy.App_Saudi_NumYears.ToString();
                        tbMonthsInSaudi.Text = fsy.App_Saudi_NumMonths.ToString();
                        tbVerifiedYears.Text = fsy.Service_NumYears.ToString();
                        tbVerifiedMonths.Text = fsy.Service_NumMonths.ToString();
                        tbVerifiedYearsInSaudi.Text = fsy.Saudi_NumYears.ToString();
                        tbVerifiedMonthsInSaudi.Text = fsy.Saudi_NumMonths.ToString();
                        if (fsy.App_Service_NumMonths > 11 || fsy.App_Saudi_NumMonths > 11
                             || fsy.Service_NumMonths > 11 || fsy.Saudi_NumMonths > 11)
                        {
                            ShowError("Number of months should be less than 12");
                            break;
                        }
                        if ((fsy.App_Service_NumYears + (fsy.App_Service_NumMonths / 12.0)) < 3.5 && fsy.Application.Employee.Rank !=   RankProfessorial.Visiting_Associate_Professor.ToString().Replace("_", " "))
                        {
                            ShowError("Number of years in the current rank must be atleast 3 years and 6 months");
                        }
                        if ((fsy.App_Service_NumYears + (fsy.App_Service_NumMonths / 12.0)) < 1.0 && fsy.Application.Employee.Rank ==   RankProfessorial.Visiting_Associate_Professor.ToString().Replace("_", " "))
                        {
                            ShowError("Number of years in the current rank (Visiting Associate Professor) must be atleast 1 year");
                        }           
                        else if ((fsy.App_Saudi_NumYears + (fsy.App_Saudi_NumMonths / 12.0)) < 1)
                        {
                            ShowError("Number of years in Saudi must be atleast 1 year");
                        }
                        else if ((fsy.Service_NumYears + (fsy.Service_NumMonths / 12.0)) < 3.5 && fsy.Application.Employee.Rank != RankProfessorial.Visiting_Associate_Professor.ToString().Replace("_", " "))
                        {
                            ShowError("Number of years in the current rank must be atleast 3 years and 6 months");
                        }
                        else if ((fsy.Service_NumYears + (fsy.Service_NumMonths / 12.0)) < 1.0 && fsy.Application.Employee.Rank == RankProfessorial.Visiting_Associate_Professor.ToString().Replace("_", " "))
                        {
                            ShowError("Number of years in the current rank (Visiting Associate Professor) must be atleast 1 year");
                        }
                        else if ((fsy.Saudi_NumYears + (fsy.Saudi_NumMonths / 12.0)) < 1)
                        {
                            ShowError("Number of years in Saudi must be atleast 1 year");
                        }
                        else
                        {
                            ShowMessage("Number of years of service meets the Eligibility");
                        }
                    }
                    else
                    {
                        ShowError("Number of years in the current rank must be atleast 3 years and 6 months for Assitant and Associate Professor and Number of years in Saudi   must be atleast 1 year");
                    }
                    if(Master.CurRoleID ==(byte) RoleID.College_Dean )
                    {
                      List<Application_TaskLog> latl = bal.GetAppTaskLog(Master.ApplicationID)
                        .Where(a => a.TaskID == (int)TaskIDs.College_Approval &&
                        a.ActionID== (int)ActionID.Return_to_the_Department_Chairman_From_FacultyAffairs
                        ).ToList() ;
                        if (latl.Count > 0
                           && latl.Last().ActionID == (int)ActionID.Return_to_the_Department_Chairman_From_FacultyAffairs)

                        {
                            ShowMessage("OK");
                        }
                        else
                        {
                            ShowError("The Service Years condition is not approved by the Dean of Faculty Affairs");
                        }
                    }
  
                    break;
            }
        }
        protected void UpdateServiceYears(object sender, EventArgs e)
        {
            byte duration = 0;
            if (bal.GetForm_ServiceYears(Master.ApplicationID).Count > 0)
            {
                Form_ServiceYears fsy = bal.GetForm_ServiceYears(Master.ApplicationID)[0];

                fsy.App_Service_NumYears = byte.TryParse(tbYears.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.App_Service_NumMonths = byte.TryParse(tbMonths.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.Service_NumYears = byte.TryParse(tbVerifiedYears.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.Service_NumMonths= byte.TryParse(tbVerifiedMonths.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.App_Saudi_NumYears = byte.TryParse(tbYearsInSaudi.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.App_Saudi_NumMonths = byte.TryParse(tbMonthsInSaudi.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.Saudi_NumYears = byte.TryParse(tbVerifiedYearsInSaudi.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.Saudi_NumMonths = byte.TryParse(tbVerifiedMonthsInSaudi.Text, out duration) ? duration : (byte?)null;                
                bal.UpdateForm_ServiceYears(fsy.ApplicationID
                , fsy.App_Service_NumYears
                , fsy.App_Service_NumMonths
                , fsy.App_Saudi_NumYears
                , fsy.App_Saudi_NumMonths
                , fsy.Service_NumYears
                , fsy.Service_NumMonths
                , fsy.Saudi_NumYears
                , fsy.Saudi_NumMonths
                , ""
                , null, null, null, null, null, null, null, null);
            }
            else
            {
                Form_ServiceYears fsy = new Form_ServiceYears();
                fsy.App_Service_NumYears = byte.TryParse(tbYears.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.App_Service_NumMonths = byte.TryParse(tbMonths.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.Service_NumYears = byte.TryParse(tbVerifiedYears.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.Service_NumMonths = byte.TryParse(tbVerifiedMonths.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.App_Saudi_NumYears = byte.TryParse(tbYearsInSaudi.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.App_Saudi_NumMonths = byte.TryParse(tbMonthsInSaudi.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.Saudi_NumYears = byte.TryParse(tbVerifiedYearsInSaudi.Text, out duration) ? duration : (byte?)null;
                duration = 0;
                fsy.Saudi_NumMonths = byte.TryParse(tbVerifiedMonthsInSaudi.Text, out duration) ? duration : (byte?)null;
                bal.InsertForm_ServiceYears(
                    Master.ApplicationID
                    , fsy.App_Service_NumYears
                    , fsy.App_Service_NumMonths
                    , fsy.App_Saudi_NumYears
                    , fsy.App_Saudi_NumMonths
                    , fsy.Service_NumYears
                    , fsy.Service_NumMonths
                    , fsy.Saudi_NumYears
                    , fsy.Saudi_NumMonths, "", null, null, null, null, null, null, null, null);
            }
            DatabindControls();

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
        #endregion
    }
