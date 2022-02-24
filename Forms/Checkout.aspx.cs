using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using BL.Data;

    public partial class Forms_Checkout : System.Web.UI.Page
    {
        BAL bal = new BAL();
        public override string StyleSheetTheme
        {
            get
            {
                return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Master.GetNoOfActiveTask(Master.ApplicationID) > 1)
            {
                lblMessage.Text = Resources.Resource.ActionM3.Replace("@@NoOfActiveTask@@", Master.GetNoOfActiveTask(Master.ApplicationID).ToString());
                lblMessage.ToolTip = Resources.Resource.ActionM3.Replace("@@NoOfActiveTask@@", Master.GetNoOfActiveTask(Master.ApplicationID).ToString());                
            }
            else
            {
                lblMessage.Text = "";
                divMsg.Visible = false;
            }
            if (IsPostBack)
                {
                    return;
                }
            DatabindControls();
            if (Master.CurrentFormLevel == -1 || Master.TaskID == -1)
            {
                
                lstActions.Visible = false;
                divMessages.Visible = false;
                //grdMessages.Visible = false;
                btnSubmit.Visible = false;
                lblMessage.Text = Resources.Resource.ActionM2;
                // Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID+"&roleID="+Master.CurRoleID);
                return;
            }

           
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("S.No.", typeof(string)));
            dt.Columns.Add(new DataColumn("Task", typeof(string)));
            dt.Columns.Add(new DataColumn("Role", typeof(string)));
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            if (Master.TaskID != 0)
            {
                List<Application_ActionStatus> futureActions = bal.GetApplicationActionStatus(Master.ApplicationID)
                    .Where(a => a.WFAction.TaskID == Master.TaskID && a.Status == "Active").ToList();
                List<Application_ActionStatus> futureActionsDisabled = bal.GetApplicationActionStatus(Master.ApplicationID)
                    .Where(a => a.WFAction.TaskID == Master.TaskID && a.Status == "Inactive").ToList();
                List<Application_ActionStatus> futureActionsCopy = new List<Application_ActionStatus>(futureActions);
                //foreach (Application_ActionStatus action in futureActionsCopy)
                //{
                //    if (bal.GetAppTaskLogByAppIDCompleted(Master.ApplicationID, false)
                //        .Where(atl => atl.ActionID == action.ActionID).Count() >= 1)
                //    {
                //        futureActions.Remove(action);
                //    }
                //}
                foreach (Application_ActionStatus action in futureActions)
                {
                    action.WFAction.Title = action.WFAction.Title.Replace("@@TopMostAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"]);
                    action.WFAction.Title = action.WFAction.Title.Replace("@@TopAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopAuthority_TitleShort"]);
                    action.WFAction.Title = action.WFAction.Title.Replace("@@TopLowAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"]);
                }
                if (futureActions.Count == 0 && futureActionsDisabled.Count > 0
                || (futureActions.Count > 0 && futureActions.Where(a=>a.WFAction.Type == "Share").Count() == futureActions.Count && futureActionsDisabled.Count > 0))
                {
                    //lblMsgForLockedActions.Text = "All the actions below are disabled until the promotion application is returned by the committee or council chairman.";
                    lblMsgForLockedActions.Text = Resources.Resource.ActionM1;
                    divMsgForLockedActions.Visible = true;
                    lstActions.DataSource = futureActionsDisabled.Select(a => new { Text = a.WFAction.Title, Value = a.ActionID });
                    lstActions.Visible = true;
                    lstActions.DataBind();
                    lstActions.Enabled = false;
                }
                else
                {
                    lblMsgForLockedActions.Text = "";
                    divMsgForLockedActions.Visible = false;
                    lstActions.DataSource = futureActions.Select(a => new { Text = a.WFAction.Title, Value = a.ActionID });
                    lstActions.Visible = true;
                    lstActions.DataBind();

                }
                foreach (ListItem li in lstActions.Items)
                {
                    //add tooltip for current item
                    li.Attributes.Add("title", bal.GetActionByPK(int.Parse(li.Value))[0].WFActionType.Description);
                }

        }
            //if(Master.TaskID != 0 )
            //{
            //    List<Action> futureActions = bal.GetActionByTask(Master.TaskID, "Active");
            //    List<Action> futureActionsDisabled = bal.GetActionByTask(Master.TaskID, "Inactive");
            //    List<Action> futureActionsCopy = new List<Action>(futureActions);
            //    foreach (Action action in futureActionsCopy)
            //    {
            //        if (bal.GetAppTaskLogByAppIDCompleted(Master.ApplicationID, false).Where(atl => atl.ActionID == action.ActionID).Count() >= 1)
            //        {
            //            futureActions.Remove(action);
            //        }   
            //    }
            //    foreach (Action action in futureActions)
            //    {
            //       action.Title = action.Title.Replace("@@TopMostAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"]);
            //       action.Title = action.Title.Replace("@@TopAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopAuthority_TitleShort"]);
            //       action.Title = action.Title.Replace("@@TopLowAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"]);
            //    }
            //    if(futureActions.Count == 0 && futureActionsDisabled.Count > 0)
            //    {
            //        lblMsgForLockedActions.Text = "All the actions below are disabled until the promotion application is returned by the committee or council chairman.";
            //        lstActions.DataSource = futureActionsDisabled ;
            //        lstActions.Visible = true;
            //        lstActions.DataBind();
            //        lstActions.Enabled = false;
            //    }
            //    else
            //    {
            //        lblMsgForLockedActions.Text = "";
            //        lstActions.DataSource = futureActions;
            //        lstActions.Visible = true;
            //        lstActions.DataBind();

            //    }


            //}
            BindCheckList();


            //hide buttons when printing
            if (Utils.IsPrintMode())
            {
                btnSubmit.Visible = false;
            }
        }
        private void BindCheckList()
        {
            ArrayList list = new ArrayList();
            IsTaskComplete = true;
            IsTaskCompleteForForwardAlive = true;

            foreach (TaskForm form in Master.TaskForms)
            {
                if (form.Checkable)
                {
                    FormStatusStruct status = Master.CheckFormTask(form);
                    list.Add(status);
                    if (status.Status == false)
                    {
                        IsTaskComplete = false;
                    }
                    if (form.Form.AllowFAAction.Value)
                    {
                        IsTaskCompleteForForwardAlive = status.Status;
                    }

                }

            }

            lstFormsStatus.DataSource = list;
            divChecklist.Visible = list.Count > 0;
            lstFormsStatus.DataBind();
        }
        #region Properties


        //private bool isTaskComplete;
        public bool IsTaskComplete
        {
            get { return (bool)ViewState["isTaskComplete"]; }
            set { ViewState["isTaskComplete"] = value; }
        }
       // private bool isTaskCompleteForForwardAlive;
        public bool IsTaskCompleteForForwardAlive
        {
            get { return (bool)ViewState["isTaskCompleteForForwardAlive"]; }
            set { ViewState["isTaskCompleteForForwardAlive"] = value; }
        }
       // private TasksStatus[] branchedTasks;
        public TasksStatus[] BranchedTasks
        {
            get { return (TasksStatus[])ViewState["branchedTasks"]; }
            set { ViewState["branchedTasks"] = value; }
        }

        #endregion
        #region Struct
        [Serializable]
        public struct TasksStatus
        {
            public Boolean Status;
            public int TaskID;
            public TasksStatus(int TaskID, Boolean Status)
            {
                this.Status = Status;
                this.TaskID = TaskID;
            }
        }
        #endregion
        protected void lstActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSubmit.Visible = true;
            //lblTaskMessage.Text = "";
            int actionID = int.Parse(lstActions.SelectedValue);
            if (bal.GetActionByPK(actionID)[0].Type == "ForwardAliveLock")
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ActionM12;
                programmaticModalPopup0.Show();
            }

        //List<vw_NextTaskDTO> lnt = WorkFlow.GetNextTasks1(Master.ApplicationID, Master.TaskID, actionID);
        List<vw_NextTaskDTO> lnt = WorkFlow.GetNextTasks(Master.ApplicationID, Master.TaskID, actionID);
            if (lnt.Count == 0 && bal.GetActionByPK(actionID)[0].Type != ActionType.Save.ToString())
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ActionM11;
                programmaticModalPopup0.Show();
                btnSubmit.Visible = false;
                return;
            }
            if (lnt.Count == 1 && lnt[0].NextTaskID == null && lnt[0].NextRoleID == null)
            {
                labelProgrammaticPopup0.Text = lnt[0].Message;
                programmaticModalPopup0.Show();
                btnSubmit.Visible = false;
                return;
            }
            divMessages.Visible = true;
            rpMessages.DataSource = lnt;
            rpMessages.DataBind();
            //grdMessages.DataSource = lnt;
            //grdMessages.DataBind();
            if (bal.GetActionByPK(actionID)[0].Type == ActionType.Save.ToString()
                || !bal.GetActionByPK(actionID)[0].RequireCompleteTask.Value
                || (bal.GetActionByPK(actionID)[0].Type == ActionType.ForwardAliveLock.ToString() && IsTaskCompleteForForwardAlive))
            {
                btnSubmit.Enabled = true;
                return;
            }
            else
            {
                if (!IsTaskComplete)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ActionM5;
                    programmaticModalPopup0.Show();
                }
                btnSubmit.Enabled = IsTaskComplete;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int applicationID = Master.ApplicationID;
                int taskID = Master.TaskID;
                byte roleID = Master.CurRoleID;
                string Email = "";
                try
                {
                    Employee employee = (Employee)Session["user"];
                    Email = employee.Email;
                }
                catch
                {

                }
                List<Application_TaskLog1> latl = new List<Application_TaskLog1>();

                //foreach (GridViewRow row in grdMessages.Rows)
                foreach (RepeaterItem row in rpMessages.Items)
                {
                    Application_TaskLog1 atl = new Application_TaskLog1();
                    atl.ApplicationID = applicationID;

                    atl.TaskID = (row.FindControl("lblTaskID") as Label).Text == "0" ? (int?)null : int.Parse((row.FindControl("lblTaskID") as Label).Text);
                    TextBox txtMessage = (TextBox) row.FindControl("tbMessage");
                    if (PromotionApplication.IsHTML(txtMessage.Text))
                    {
                        labelProgrammaticPopup0.Text = Resources.Resource.ActionM6;
                        programmaticModalPopup0.Show();
                        return;
                    }
                    //txtMessage.Text = Regex.Replace(txtMessage.Text, PromotionApplication.specialCharacters, string.Empty);
                    if (txtMessage.Visible)
                    {
                        atl.Message = txtMessage.Text;
                    }
                    else
                    {
                        //   atl.Message = ((Controls_DeptCommitteeMessage)row.FindControl("DeptCommitteeMessage")).GetMessage();
                    }

                    atl.Completed = false;
                    atl.Reminders = 1;
                    latl.Add(atl);
                }
                int actionID = int.Parse(lstActions.SelectedValue);
                string message = "";
                if (!WorkFlow.ApplyAction(applicationID, taskID, actionID, latl, Email, out message))
                {
                    //labelProgrammaticPopup0.Text = Resources.Resource.ActionM7;
                    labelProgrammaticPopup0.Text = message;
                    programmaticModalPopup0.Show();
                    return;
                }
                if (bal.GetActionByPK(actionID)[0].Type == ActionType.Save.ToString())
                {
                    Response.Redirect("~/Tasks.aspx?applicationID=" + applicationID + "&roleID=" + Cryptography.Encrypt(roleID.ToString()));
                }
                else
                {
                    Response.Redirect("Popup.aspx?applicationID=" + applicationID + "&roleID=" + Cryptography.Encrypt(roleID.ToString()));
                }

                //  Response.Redirect("~/Tasks.aspx?applicationID=" + applicationID+"&roleID="+Master.CurRoleID,false);
            }
            catch (Exception )
            {
                //FormsAuthentication.RedirectToLoginPage();

            }
        }
     
        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }
    protected void rbltMaterialCheck_SelectedIndexChanged(object sender, EventArgs e)
    {
        divMessages.Visible = false; 
        int appID = Master.ApplicationID;
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;
        Employee Applicant = bal.GetApplicant(appID)[0];
        Employee PCChair = bal.GetForm_FinalPC(Master.ApplicationID).Where(a => a.Position == (int)PCPosition.Chairman).ToList()[0].Employee;
        if (rbltMaterialCheck.SelectedValue == "MaterialOK")
        {
            Task_ExtMessages tem = bal.GetExtTaskMessage().Where(a => a.Task_Ext.Title == TaskExtID.Material_Ready_to_be_Sent.ToString()).ToList()[0];
            string MessageBody = tem.Message;
            MessageBody = MessageBody
                .Replace("@@Applicant@@", Applicant.NameString)
                .Replace("@@ForRank@@", bal.GetApplication(appID)[0].ForRank)
                .Replace("@@ApplicantDept@@", Applicant.Department)
                .Replace("@@RecipientName@@", TopAuthority.NameString)
                .Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"])
                .Replace("@@Sender@@", PCChair.NameString)
                .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
            ;
            EmailContent email = new EmailContent
            {
                To = bal.GetDepartmentBySN(ConfigurationManager.AppSettings["TopAuthority_TitleShort"])[0].DeputyEmail,
                Body = MessageBody,
                Subject = tem.Subject
            };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadData(le);
            ComposeEmail1.Visible = true;

        }
        else
        {

            Task_ExtMessages tem = bal.GetExtTaskMessage().Where(a => a.Task_Ext.Title == TaskExtID.Material_Not_Ready_to_be_Sent.ToString()).ToList()[0];
            string MessageBody = tem.Message;
            MessageBody = MessageBody
                .Replace("@@Applicant@@", Applicant.NameString)
                .Replace("@@ForRank@@", bal.GetApplication(appID)[0].ForRank)
                .Replace("@@ApplicantDept@@", Applicant.Department)
                .Replace("@@RecipientName@@", TopAuthority.NameString)
                .Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"])
                .Replace("@@Sender@@", PCChair.NameString)
                .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
            ;
            EmailContent email = new EmailContent
            {
                To = bal.GetDepartmentBySN(ConfigurationManager.AppSettings["TopAuthority_TitleShort"])[0].DeputyEmail,
                Body = MessageBody,
                Subject = tem.Subject
            };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadData(le);
            ComposeEmail1.Visible = true;

        }

    }
    protected void lbtnViewEmailFromPCMaterialOK_Click(object sender, EventArgs e)
        {
        int appID = Master.ApplicationID;
        if (string.IsNullOrEmpty(bal.GetFormMaterialFlag(appID)[0].MaterialOKEmail))
        {
            labelProgrammaticPopup0.Text = "No Email found.";
            programmaticModalPopup0.Show();
            return;
        }
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;

        EmailContent email = new EmailContent
        {
            To = TopAuthority.Email + ","
                + bal.GetDepartmentBySN(ConfigurationManager.AppSettings["TopAuthority_TitleShort"])[0].DeputyEmail,
            Body = bal.GetFormMaterialFlag(appID)[0].MaterialOKEmail,
            Subject = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Material_Ready_to_be_Sent).ToList()[0].Subject
        };
        List<EmailContent> le = new List<EmailContent>();
        le.Add(email);
        ComposeEmail1.LoadDataForPreview(le);
        ComposeEmail1.Visible = true;

        DatabindControls();
    }
    protected void lbtnViewEmailFromPCMaterialNotOK_Click(object sender, EventArgs e)
    {
        int appID = Master.ApplicationID;
        if (string.IsNullOrEmpty(bal.GetFormMaterialFlag(appID)[0].MaterialNotOKEmail))
        {
            labelProgrammaticPopup0.Text = "No Email found.";
            programmaticModalPopup0.Show();
            return;
        }
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;

        EmailContent email = new EmailContent
        {
            To = TopAuthority.Email + ","
                + bal.GetDepartmentBySN(ConfigurationManager.AppSettings["TopAuthority_TitleShort"])[0].DeputyEmail,
            Body = bal.GetFormMaterialFlag(appID)[0].MaterialNotOKEmail,
            Subject = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Material_Not_Ready_to_be_Sent).ToList()[0].Subject

        };
        List<EmailContent> le = new List<EmailContent>();
        le.Add(email);
        ComposeEmail1.LoadDataForPreview(le);
        ComposeEmail1.Visible = true;

        DatabindControls();
    }

    protected void lbtnViewEmailFromTopAuthority_Click(object sender, EventArgs e)
    {
        List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(Master.ApplicationID);
        string allPCEmail = "";
        foreach (Form_FinalPC pcMem in lffpc)
        {
            if (pcMem.ExternalEmployeeID == 0)
            {
                allPCEmail = allPCEmail + pcMem.Employee.Email + ",";
            }
            else
            {
                allPCEmail = allPCEmail + pcMem.ExternalEmployee.Email + ",";
            }
        }
        allPCEmail = allPCEmail.Trim(',');
        int appID = Master.ApplicationID;
        Employee TopAuthority = bal.GetApplicationRole(appID, (byte)RoleID.TopAuthority)[0].Employee;
        if (string.IsNullOrEmpty(bal.GetFormMaterialFlag(appID)[0].EmailFromTopAuthority))
        {
            labelProgrammaticPopup0.Text = "No Email found.";
            programmaticModalPopup0.Show();
            return;
        }
        EmailContent email = new EmailContent
        {
            To = allPCEmail,
            Body = bal.GetFormMaterialFlag(appID)[0].EmailFromTopAuthority,
            Subject = bal.GetExtTaskMessage().Where(a => a.Task_Ext.TaskID == (int)TaskExtID.Material_Updated).ToList()[0].Subject

        };
        List<EmailContent> le = new List<EmailContent>();
        le.Add(email);
        ComposeEmail1.LoadDataForPreview(le);
        ComposeEmail1.Visible = true;

        DatabindControls();
    }

    private void DatabindControls()
    {
        divMaterialCheck.DataBind();
        divMaterialOKStatus.DataBind();
    }
    #region Compose Email
    protected void ComposeEmail_OnEmailSent(object sender, EventArgs e)
    {
        if (bal.GetFormMaterialFlag(Master.ApplicationID).Count == 0)
        {
            bal.InsertFormMaterialFlag(Master.ApplicationID, null, "", "", "");
        }
        bool materialOK = rbltMaterialCheck.SelectedValue == "MaterialOK";
        Form_MaterialFlag fmf = bal.GetFormMaterialFlag(Master.ApplicationID)[0];
        if (materialOK)
        {

            bal.UpdateFormMaterialFlag(Master.ApplicationID, materialOK, ComposeEmail1.EmailBody, "", fmf.EmailFromTopAuthority);
        }
        else
        {
            bal.UpdateFormMaterialFlag(Master.ApplicationID, materialOK, "", ComposeEmail1.EmailBody, fmf.EmailFromTopAuthority);
        }
        ComposeEmail1.Visible = false;
        DatabindControls();        
    }
    protected void ComposeEmail_OnEmailNotSent(object sender, EventArgs e)
    {
        ComposeEmail1.Visible = false;
    }
    #endregion
    public bool GetMaterialOKStatus()
    {
        if (bal.GetFormMaterialFlag(Master.ApplicationID).Count == 0)
        {
            bal.InsertFormMaterialFlag(Master.ApplicationID, null, "", "", "");
        }
        if (bal.GetFormMaterialFlag(Master.ApplicationID)[0].PCMaterialReady4ExtRevFlag.HasValue)
        {
            lblPCMessage.Text = "";
            return bal.GetFormMaterialFlag(Master.ApplicationID)[0].PCMaterialReady4ExtRevFlag.Value;
        }
        else
        {
            lblPCMessage.Text = "The Promotion Committee has not yet viewed the material";
            return false;
        }
    }
}
