using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using BL.Data;
using System.Linq;

/// <summary>
/// A class to control the workflow of the application
/// </summary>
public class WorkFlow
{
 
    static ExtRevBAL erBAL = new ExtRevBAL();
    static BAL bal = new BAL();
    //public static Promotion.NextTasksDataTable GetNextTasks(int applicationID, int taskID, int actionID)
    public static List<vw_NextTaskDTO> GetNextTasks(int applicationID, int taskID, int actionID)
    {       
        Application application = bal.GetApplication(applicationID)[0];
        List<vw_NextTaskDTO> ListofNextTasks = bal.GetNextTask(taskID, actionID, applicationID);        
        ///* sender */
        Employee applicant, sender, receiver;
        bool isReceiverActing = false;
        bool isSenderActing = false;
        /* applicant */
        try
        {
            applicant = bal.GetApplicant(applicationID)[0];
        }
        catch
        {
            applicant = null;
        }

        /* sender */
        try
        {
            sender = bal.GetEmployeeByAppTsk(applicationID, taskID)[0];
            isSenderActing = bal.GetApplicationRole(applicationID, bal.GetTask(taskID)[0].RoleID, sender.EmployeeID, 0)[0].IsActing == "Acting";
        }
        catch (Exception e)
        {
            string s = e.Message;
            sender = null;
        }


        foreach (vw_NextTaskDTO nextTask in ListofNextTasks)
        {
            /* reciever */
            try
            {
                receiver = bal.GetEmployeeByAppRole(applicationID, nextTask.NextRoleID.Value)[0];
                isReceiverActing = bal.GetApplicationRole(applicationID, nextTask.NextRoleID.Value, receiver.EmployeeID, 0)[0].IsActing == "Acting";
            }
            catch {
                receiver = null;
            }

            if (string.IsNullOrEmpty(nextTask.Message))
            {
                nextTask.Message = "";
                ListofNextTasks.Remove(nextTask);
                continue;
            }

            /* replace message tags */
            //application
            //string message = Cryptography.Decrypt(row.Message);
            // string message = row.Message;
            //message = message.Replace("@@ToRank@@", bal.GetApplication(applicationID)[0].ForRank);
            nextTask.Message = nextTask.Message.Replace("@@ToRank@@", application.ForRank);
            nextTask.Message = nextTask.Message.Replace("@@ForRank@@", application.ForRank);
            nextTask.Message = nextTask.Message.Replace("@@Specialty@@", application.Employee.Specialty);
            nextTask.Message = nextTask.Message.Replace("@@WebsiteURL@@", ConfigurationManager.AppSettings["WebsiteURL"]);
            nextTask.Message = nextTask.Message.Replace("@@TopAuthority@@", bal.GetApplicationRole(applicationID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].Employee.NameString);
            nextTask.Message = nextTask.Message.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);
            nextTask.Message = nextTask.Message.Replace("@@TopAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopAuthority_TitleShort"]);
            nextTask.Message = nextTask.Message.Replace("@@TopLowAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"]);
            nextTask.Message = nextTask.Message.Replace("@@TopLowAuthority_Title@@", ConfigurationManager.AppSettings["TopLowAuthority_Title"]);
            nextTask.Message = nextTask.Message.Replace("@@TopMostAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"]);
            nextTask.Message = nextTask.Message.Replace("@@TopMostAuthority_Title@@", ConfigurationManager.AppSettings["TopMostAuthority_Title"]);

            List<Application_Role> lar = new List<Application_Role>();           
            //applicant
            if (applicant != null)
            {
                nextTask.Message = nextTask.Message.Replace("@@Applicant@@", applicant.NameString );
                nextTask.Message = nextTask.Message.Replace("@@ApplicantDepartment@@", applicant.Department1.NameString);
                nextTask.Message = nextTask.Message.Replace("@@ApplicantCollege@@", applicant.Department1.ParentDeptString);
            }

            //sender
            if (sender != null)
            {
                nextTask.Message = nextTask.Message.Replace("@@Sender@@", sender.NameString + (isSenderActing ? " (Acting)" : ""));
                nextTask.Message = nextTask.Message.Replace("@@SenderDepartment@@", sender.Department1.NameString);
                nextTask.Message = nextTask.Message.Replace("@@SenderCollege@@", sender.Department1.ParentDeptString);
            //    nextTask.Message = nextTask.Message.Replace("@@SenderRole@@", bal.GetTask(nextTask.TaskID.Value)[0].Role.Title);
            }

            //receiver
            if (receiver != null)
            {
                nextTask.Message = nextTask.Message.Replace("@@Receiver@@", receiver.NameString + (isReceiverActing ? " (Acting)": ""));
                nextTask.Message = nextTask.Message.Replace("@@ReceiverDepartment@@", receiver.Department1.NameString);
                nextTask.Message = nextTask.Message.Replace("@@ReceiverCollege@@", receiver.Department1.ParentDeptString);
            }
            
            if (nextTask.HasCommitteeInfo.HasValue && nextTask.HasCommitteeInfo.Value && nextTask.PhaseID != null)
            {
                Phase phase = new Phase();
                if (bal.GetPhase(nextTask.PhaseID.Value).Count != 0 )
                {
                    phase = bal.GetPhase(nextTask.PhaseID.Value).First();
                }
                else
                {
                    continue;
                }
                
                //foreach (var phase in bal.GetPhase(true))
                //{
                lar = bal.GetApplicationRole(applicationID).Where(ar => ar.Role.Title.Contains(phase.Title)).ToList();
                    if (lar.Count < (phase.Min.HasValue ? phase.Min : 0))
                    {
                        ListofNextTasks.RemoveAll(a => a.ActionID != 0);
                        return ListofNextTasks;
                    }
                    int counter = 0;
                    for (counter = 0; counter < lar.Count; counter++)
                    {
                        if (lar[counter].ExternalEmployee.ExternalEmployeeID == 0)
                        {
                            if (phase.HasChair.HasValue && phase.HasChair.Value && counter == 0)
                            {
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Chair" + "@@", lar[counter].Employee.NameString);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Chair" + "Dept@@", lar[counter].Employee.Department1.ShortName);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Chair" + "Phone@@", lar[counter].Employee.Phone);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Chair" + "Email@@", lar[counter].Employee.Email);
                            }
                            else
                            {
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "@@", lar[counter].Employee.NameString);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "Dept@@", lar[counter].Employee.Department1.ShortName);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "Phone@@", lar[counter].Employee.Phone);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "Email@@", lar[counter].Employee.Email);
                            }
                        }
                        else
                        {
                            if (phase.HasChair.HasValue && phase.HasChair.Value && counter == 0)
                            {
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Chair" + "@@", lar[counter].ExternalEmployee.NameString);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Chair" + "Dept@@", lar[counter].ExternalEmployee.Organization);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Chair" + "Phone@@", lar[counter].ExternalEmployee.Phone);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Chair" + "Email@@", lar[counter].ExternalEmployee.Email);
                            }
                            else
                            {
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "@@", lar[counter].ExternalEmployee.NameString);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "Dept@@", lar[counter].ExternalEmployee.Organization);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "Phone@@", lar[counter].ExternalEmployee.Phone);
                                nextTask.Message = nextTask.Message.Replace("@@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "Email@@", lar[counter].ExternalEmployee.Email);
                            }

                        }
                    }
                    if (phase.Max.HasValue)
                    {
                        for (; counter < phase.Max.Value; counter++)
                        {
                            nextTask.Message = nextTask.Message                                
                                .Replace("(Ext. @@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "Phone@@,", "")
                                .Replace(" Email: @@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "Email@@)", "");
                            nextTask.Message = nextTask.Message
                               .Replace("@@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "@@, (Member)", "");
                            nextTask.Message = nextTask.Message
                               .Replace("@@" + phase.Title.Replace(" ", "") + "Member" + (counter) + "@@", "");

                    }
                    }
                //}
            }
           
        



            //if (nextTask.NextRole.ToLower().Contains("Promotion Committee".ToLower()) || nextTask.NextTask.ToLower().Contains("Promotion Committee".ToLower()))
            //{
            //    lar = bal.GetApplicationRole(applicationID).Where(ar => ar.Role.Title.Contains("Promotion Committee")).ToList();
            //    if (lar.Count != 5)
            //    {
            //        // ListofNextTasks.RemoveAll(a => a.ActionID != 0);
            //        //return ListofNextTasks;
            //        return null;
            //    }
            //    for (int counter = 0; counter < lar.Count; counter++)
            //    {
            //        if (lar[counter].Employee.EmployeeID != "0")
            //        {
            //            nextTask.Message = nextTask.Message.Replace("@@PromotionCommittee" + (counter + 1) + "@@", lar[counter].Employee.NameString);
            //            nextTask.Message = nextTask.Message.Replace("@@PromotionCommittee" + (counter + 1) + "Dept@@", lar[counter].Employee.Department1.ShortName);
            //        }
            //    }
            //}
         foreach (var item in bal.GetFormVariable())
            {
                if(item.FormTable == "Form_ChairmanInput")
                {
                    if (item.ValueString == "MeetingNo" && bal.GetForm_ChairmanInput(applicationID).Count != 0)
                    {
                        nextTask.Message = nextTask.Message.Replace(item.Variable, bal.GetForm_ChairmanInput(applicationID)[0].MeetingNo);
                    }
                    if (item.ValueString == "MeetingDate" && bal.GetForm_ChairmanInput(applicationID).Count != 0)
                    {
                        nextTask.Message = nextTask.Message.Replace(item.Variable, bal.GetForm_ChairmanInput(applicationID)[0].MeetingDate.HasValue ? bal.GetForm_ChairmanInput(applicationID)[0].MeetingDate.Value.ToShortDateString(): "");
                    }
                }
                if (item.FormTable == "Form_CollegeInput")
                {
                    if (item.ValueString == "MeetingNo" && bal.GetForm_CollegeInput(applicationID).Count != 0 )
                    {
                        nextTask.Message = nextTask.Message.Replace(item.Variable, bal.GetForm_CollegeInput(applicationID)[0].MeetingNo);
                    }
                    if (item.ValueString == "MeetingDate" && bal.GetForm_CollegeInput(applicationID).Count != 0)
                    {
                        nextTask.Message = nextTask.Message.Replace(item.Variable, bal.GetForm_CollegeInput(applicationID)[0].MeetingDate.HasValue ? bal.GetForm_CollegeInput(applicationID)[0].MeetingDate.Value.ToShortDateString(): "");
                    }
                }
                //if(item.FormTable == "")
                //{
                //    if(typeof(BAL).GetMethods().ToList().Where(a=>a.Name ==item.FormTable).Count() != 0)
                //    {
                //        List<Form_ChairmanInput> lfci =  typeof(BAL).GetMethods().ToList().Where(a => a.Name == item.FormTable).First().Invoke(List<Form_ChairmanInput>, new object[] { applicationID });
                //    }
                //}
            }
            

            //if (nextTask.Message.Contains("department council in its meeting #[xxxx] "))
            //{
            //    if (bal.GetChairmanInput(applicationID).Count() > 0 && bal.GetChairmanInput(applicationID)[0].MeetingDate.HasValue)
            //    {
            //        nextTask.Message = nextTask.Message.Replace("department council in its meeting #[xxxx] on [Date]", "department council in its meeting # " + bal.GetChairmanInput(applicationID)[0].MeetingNo + " on " + bal.GetChairmanInput(applicationID)[0].MeetingDate.Value.ToShortDateString());
            //    }
            //}
            //if (nextTask.NextRole.ToLower().Contains("Departmental Committee".ToLower()) || nextTask.Task.ToLower().Contains("Departmental Committee".ToLower()))
            //{
            //    lar = bal.GetApplicationRole(applicationID).Where(ar => ar.Role.Title.Contains("Departmental Committee")).ToList();
            //    if (lar.Count > 0)
            //    {
            //        int i;
            //        for (i = 0; i < lar.Count; i++)
            //        {
            //            //if (lar[i].ExternalEmployeeID == 0)
            //            //{
            //            nextTask.Message = nextTask.Message.Replace("@@DeptCommittee" + (i + 1) + "@@", lar[i].Employee.NameString);
            //            //}
            //            //else
            //            //{
            //            //    message = message.Replace("@@DeptCommittee" + (i + 1) + "@@", lar[i].ExternalEmployee.NameString);
            //            //}

            //        }
            //        for (; i < 5; i++)
            //        {
            //            nextTask.Message = nextTask.Message.Replace("@@DeptCommittee" + (i + 1) + "@@", "");
            //        }
            //    }
            //}
            //if (committee.Count > 0)
            //{
            //    int i;
            //    for (i = 0; i < committee.Count; i++)
            //    {
            //        message = message.Replace("@@DeptCommittee" + (i + 1) + "@@", bal.GetEmployeeByPK(committee[i].EmployeeID)[0].Name);
            //    }
            //    for (; i < 5; i++)
            //    {
            //        message = message.Replace("@@DeptCommittee" + (i + 1) + "@@", "");
            //    }
            //}

            //row.Message = message;

        }

        return ListofNextTasks;
    }

    public static bool ApplyAction(int applicationID, int taskID, int actionID, List<Application_TaskLog1> nextTasks
            , string senderEmail, out string errorMessage)
    {
        WFAction action = bal.GetActionByPK(actionID)[0];
        if (action.Type == "CloseTopAuthority")
        {
            if (bal.GetNoOfActiveTask(applicationID) > 1)
            {
                errorMessage = Resources.Resource.ActionM10
                    .Replace("@@NoOfActiveTasks@@", bal.GetNoOfActiveTask(applicationID).ToString());
                return false;
            }
        }
        if (action.Type != ActionType.Forward.ToString() && action.Type != ActionType.ForwardAliveLock.ToString() 
            && action.Type != ActionType.ForwardAlive.ToString()  && action.Type != ActionType.Share.ToString())
        {
            foreach (Task_Dependencies rowTskDep in bal.GetTaskDependency().Where(td => td.ParentTaskID == taskID))
            {
                bal.UpdateAppTaskLogStatusByTaskID(true, applicationID, rowTskDep.ChildTaskID);
            }
        }
        
        if (action.Type == "Initiate")
        {
            Application a = bal.GetApplication(applicationID)[0];
            bal.UpdateApplication(a.ApplicationID, a.ApplicationClosed, a.EmployeeID, a.ForRank, DateTime.Now
                , a.ExclusionList, a.Comment, a.HardCopy, a.SCComments, a.RectorDecision, a.FinalDeicision
                , a.SCDecision, a.RectorComments);
        }
        else if (action.Type == "Unarchive")
        {

            Application a = bal.GetApplication(applicationID)[0];
            if (bal.GetApplication(a.EmployeeID).Where(app => app.ForRank == a.ForRank
            && !a.ApplicationClosed).Count() > 0)
            {
                errorMessage = Resources.Resource.ActionM8;
                return false;
            }
            bal.UpdateApplication(a.ApplicationID, false, a.EmployeeID, a.ForRank, a.StartDate
            , a.ExclusionList, a.Comment, a.HardCopy, a.SCComments, a.RectorDecision, a.FinalDeicision
            , a.SCDecision, a.RectorComments);
        }

        /* Mark the current task as Completed (where TaskID=Task) */
        if (nextTasks.Count > 0)
        {

            if (action.Type != "ForwardAliveLock" && action.Type != "ForwardAlive" && action.Type != "Share" && action.Type != "Unshare")
            {
                bal.UpdateAppTaskLogStatusByTaskID(true, applicationID, taskID);
            }
            //if(taskID != (int)TaskID.TopAuthority_for_External_and_Internal_Evaluation  
            //    || (taskID == (int) TaskID.TopAuthority_for_External_and_Internal_Evaluation 
            //    && actionID == (int) ActionID.Return_to_TopLowAuthority))
            //{
            //    bal.UpdateAppTaskLogStatusByTaskID(true, applicationID, taskID);
            //}
            if (action.Type == "Share" || action.Type == "Unshare")
            {
                //              action.Status = "Inactive";
                //                bal.UpdateAction(actionID, action.Type, action.Title, action.Status);

                bal.UpdateApplicationActionStatus(applicationID, actionID, "Inactive");
                if (action.AttActionID == null)
                {
                    errorMessage = Resources.Resource.ActionM9;
                    return false;
                }
                WFAction attAction = bal.GetActionByPK(action.AttActionID.Value)[0];
                //attAction.Status = "Active";
                //bal.UpdateAction(action.AttActionID.Value, attAction.Type, attAction.Title, attAction.Status);

                bal.UpdateApplicationActionStatus(applicationID, attAction.ActionID, "Active");

            }
            if (action.Type == "ForwardAliveLock")
            {
                //bal.GetApplicationActionStatus(applicationID, actionID)
                //foreach (Action act in bal.GetActionByTask(taskID, "Active"))
                //{
                //    //act.Status = "Inactive";
                //    //bal.UpdateAction(act.ActionID, act.Type, act.Title, act.Status);
                //    bal.UpdateApplicationActionStatus(applicationID, act.ActionID, "Inactive");
                //}
                foreach (Application_ActionStatus aas in bal.GetApplicationActionStatus(applicationID)
                    .Where(a => a.WFAction.TaskID == taskID &&
                    a.WFAction.AttActionID == null &&
                    //a.WFAction.Type != "Share" &&
                    //a.WFAction.Type != "Unshare" &&
                    a.Status == RecordStatus.Active.ToString()))
                {
                    //act.Status = "Inactive";
                    //bal.UpdateAction(act.ActionID, act.Type, act.Title, act.Status);
                    bal.UpdateApplicationActionStatus(applicationID, aas.ActionID, RecordStatus.Inactive.ToString());
                }
            }
            if (action.Type == "ReturnUnlock")/*The nexttask of the action with type "ReturnUnlock" is to be one single task with NextPhase ID = NULL*/
            {
                //foreach (Action act in bal.GetActionByTask(nextTasks[0].TaskID.Value, "Inactive"))
                //{
                //    //act.Status = "Active";
                //    //bal.UpdateAction(act.ActionID, act.Type, act.Title, act.Status);
                //    bal.UpdateApplicationActionStatus(applicationID, act.ActionID, "Active");
                //}
                foreach (Application_ActionStatus aas in bal.GetApplicationActionStatus(applicationID)
                    .Where(a => a.WFAction.TaskID == nextTasks[0].TaskID &&
                    a.WFAction.AttActionID == null &&
                    a.Status == RecordStatus.Inactive.ToString()))
                {
                    //act.Status = "Active";
                    //bal.UpdateAction(act.ActionID, act.Type, act.Title, act.Status);
                    bal.UpdateApplicationActionStatus(applicationID, aas.ActionID, RecordStatus.Active.ToString());
                }
            }
            if (action.Type == "ReturnUnlockNotRec")/*The nexttask of the action with type "ReturnUnlockNotRec" is to be one single task with NextPhase ID = NULL*/
            {
                foreach (Application_TaskForm atf in bal.GetApplicationTaskForm(applicationID, taskID, false, 0))
                {
                    bal.UpdateApplicationTaskForm(atf.ApplicationID, atf.TaskID, atf.FormID, false, "", atf.TaskExternal, atf.ExternalReviewerID);
                }
                foreach (Application_TaskForm atf in bal.GetApplicationTaskForm(applicationID, nextTasks[0].TaskID.Value, false, 0))
                {
                    bal.UpdateApplicationTaskForm(atf.ApplicationID, atf.TaskID, atf.FormID, false, "", atf.TaskExternal, atf.ExternalReviewerID);
                }
                foreach (Application_ActionStatus aas in bal.GetApplicationActionStatus(applicationID)
                    .Where(a => a.WFAction.TaskID == nextTasks[0].TaskID &&
                    a.WFAction.AttActionID == null &&
                    a.Status == "Inactive"))
                {
                    //act.Status = "Active";
                    //bal.UpdateAction(act.ActionID, act.Type, act.Title, act.Status);
                    bal.UpdateApplicationActionStatus(applicationID, aas.ActionID, "Active");
                }
            }
            if (action.Type == "ReturnNotRec")/*The nexttask of the action with type "ReturnUnlockNotRec" is to be one single task with NextPhase ID = NULL*/
            {
                foreach (Application_TaskForm atf in bal.GetApplicationTaskForm(applicationID, taskID, false, 0))
                {
                    bal.UpdateApplicationTaskForm(atf.ApplicationID, atf.TaskID, atf.FormID, false, "", atf.TaskExternal, atf.ExternalReviewerID);
                }
                foreach (Application_TaskForm atf in bal.GetApplicationTaskForm(applicationID, nextTasks[0].TaskID.Value, false, 0))
                {
                    bal.UpdateApplicationTaskForm(atf.ApplicationID, atf.TaskID, atf.FormID, false, "", atf.TaskExternal, atf.ExternalReviewerID);
                }              
            }
        }
        /* Insert next tasks as active tasks with their messages */
        int? nextTaskID = 0;

        if (action.Type != "Unshare")
        {
            foreach (Application_TaskLog1 row in nextTasks)
            {

                nextTaskID = row.TaskID;

                int count = bal.GetAppTaskLogByAppIDTskIDActID(applicationID, row.TaskID, actionID).Where(atl => !atl.Completed).Count();
                if (count == 0)
                {
                    DateTime dt = DateTime.Now;
                    bal.InsertAppTskLg(row.ApplicationID, row.TaskID, actionID, dt, row.Completed, 0, row.Message);
                    try
                    {
                        Employee employee;
                        if (bal.GetApplicationRole(applicationID, bal.GetTask(nextTaskID.Value)[0].RoleID).Count > 0)
                        {
                            employee = bal.GetApplicationRole(applicationID, bal.GetTask(nextTaskID.Value)[0].RoleID)[0].Employee;
                            //employee = bal.GetEmployeeByAppTsk(applicationID, nextTaskID.Value)[0];
                            List<Department> ld;
                            ld = bal.GetDepartmentByEmp(employee.EmployeeID);
                            string email = "";
                            if (employee.EmployeeID != "0")
                            {
                                email = employee.Email;
                            }
                            else
                            {
                                ExternalEmployee eEmployee;
                                eEmployee = bal.GetApplicationRole(applicationID, bal.GetTask(nextTaskID.Value)[0].RoleID)[0].ExternalEmployee;
                                //eEmployee = bal.GetEEmployeeByAppTsk(applicationID, nextTaskID.Value)[0];
                                email = eEmployee.Email;
                            }



                            //mngrTable = bal.GetDepartmentByAppTsk(applicationID, nextTaskID.Value);



                            if (ld.Count > 0)
                            {
                                Department dept = ld[0];
                                if (!string.IsNullOrEmpty(dept.DeputyEmail))
                                {
                                    Emailer.Send(dept.DeputyEmail
                                        , ConfigurationManager.AppSettings["System_Title"] + "- Message  CC"
                                        , row.Message, email, row.ApplicationID);
                                }
                                if (!string.IsNullOrEmpty(dept.Deputy2Email))
                                {
                                    Emailer.Send(dept.Deputy2Email
                                    , ConfigurationManager.AppSettings["System_Title"] + "- Message  CC"
                                    , row.Message, email, row.ApplicationID);
                                }
                            }
                            Emailer.Send(email, ConfigurationManager.AppSettings["System_Title"] + "- Message"
                                , row.Message, senderEmail, row.ApplicationID);
                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
            }

        }
        string actor = "";
        try
        {
            actor = bal.GetEmployeeByEmail(senderEmail)[0].NameString;
        }
        catch (Exception)
        {

        }

        bal.InsertApplication_Log(applicationID, actionID, DateTime.Now, null, GetIsActingFromApplicationRole(applicationID, bal.GetActionByPK(actionID)[0].Task.RoleID), null, actor);

        if (action.Type == "ClosePromotable" || action.Type == "CloseUnpromotable")
        {
            Application a = bal.GetApplication(applicationID)[0];
            bal.UpdateApplication(a.ApplicationID, true, a.EmployeeID, a.ForRank, a.StartDate, a.ExclusionList, a.Comment, a.HardCopy, a.SCComments, a.RectorDecision, a.FinalDeicision, a.SCDecision, a.RectorComments);
            List<Application_TaskLogExt> latle = bal.GetAppTaskLogExt().Where(atle => atle.ApplicationID == applicationID).ToList();
            foreach (Application_TaskLogExt atle in latle)
            {
                bal.UpdateAppTskLgExt(atle.ApplicationID, atle.TaskID, atle.ActionID, atle.SentDate, true, atle.Reminders, atle.EmailAddress, atle.ExternalReviewerID, atle.Message, atle.EmployeeID, atle.ExternalEmployeeID);
            }
            List<Application_TaskLog> latl = bal.GetAppTaskLog().Where(atl => atl.ApplicationID == applicationID).ToList();
            foreach (Application_TaskLog atl in latl)
            {
                if (action.NextPhaseID != atl.Task.PhaseID)
                {
                    bal.UpdateAppTaskLogStatusByTaskID(true, atl.ApplicationID, atl.TaskID);
                }
            }
            if (action.Type == "ClosePromotable")
            {
                bal.UpdateEmployeeRank(PromotionApplication.GetNextRank(bal.GetApplicant(applicationID)[0]), bal.GetApplicant(applicationID)[0].EmployeeID);
            }
        }
        errorMessage = "";
        return true;
    }
    public static string GetIsActingFromApplicationRole(int ApplicationID, byte RoleID)
    {
        return bal.GetApplicationRole(ApplicationID, RoleID)[0].IsActing == "Acting" ? " - Acting" : "";
    }
    public static void SendReminders()
    {
        var context = new FPSDBEntities();
        List<vw_Reminders> lvr = (from vr in context.vw_Reminders
                                  select vr).ToList();
        int MaxNoOfReminders = int.Parse(bal.GetWorkflowAttribute()
            .Where(a => a.Attribute == GlobalAttribute.MaxNoOfReminders.ToString()).ToList()[0].Value);
        foreach (vw_Reminders row in lvr)
        {
            if (row.RemindAfter > 0)
            {
                string email;
                if (row.ActionID == -1)
                {
                    Application_TaskLogExt aptle = bal.GetAppTaskLogExt(row.TaskLogID).ToList()[0];
                    email = aptle.EmailAddress;
                    if (row.ReminderNumber > MaxNoOfReminders)
                    {
                        // Stop sending reminder automatic Reminders
                        
                 
                        //aptle.Completed = true;
                        //if (aptle.TaskID != (int)TaskExtID.External_Evaluation)
                        //{
                        //    //bal.UpdateAppTskLgExt(aptle.ApplicationID, aptle.TaskID, aptle.ActionID, aptle.SentDate, aptle.Completed,
                        //    //    aptle.Reminders, aptle.EmailAddress, aptle.ExternalReviewerID.Value, aptle.Message, aptle.EmployeeID
                        //    //    , aptle.ExternalEmployeeID.Value);                           
                        //}
                        //else
                        //{
                            if (aptle.EmployeeID != "0")
                            {
                                if (bal.GetTaskExtByTitle("Notification_Excessive_Reminders_Employee").Count != 0)
                                {
                                    if (bal.GetExtTaskMessage(bal.GetTaskExtByTitle("Notification_Excessive_Reminders_Employee")[0].TaskID).Count != 0)
                                    {
                                        Task_ExtMessages tem = bal.GetExtTaskMessage(bal.GetTaskExtByTitle("Notification_Excessive_Reminders_Employee")[0].TaskID)[0];
                                        Emailer.Send(ConfigurationManager.AppSettings["TechnicalAdminEmail"]
                                             , tem.Subject
                                             , tem.Message
                                             .Replace("@@MaxNoOfReminders@@", MaxNoOfReminders.ToString())
                                             .Replace("@@EmployeeName@@", aptle.Employee.NameString)
                                             .Replace("@@EmployeeDepartment@@", aptle.Employee.Department1.Name)
                                             .Replace("@@EmployeeEmail@@", aptle.Employee.Email)
                                             .Replace("@@EmployeePhone@@", aptle.Employee.Phone)
                                             .Replace("@@ExternalReviewer@@", aptle.ExternalReviewer.NameString)
                                             .Replace("@@ReminderEmail@@", Cryptography.Decrypt(row.Message))
                                             , "AutoEmailer", row.ApplicationID);
                                    aptle.Reminders ++;
                                    bal.UpdateAppTskLgExt(aptle.ApplicationID, aptle.TaskID, aptle.ActionID, aptle.SentDate, aptle.Completed,
                                        aptle.Reminders, aptle.EmailAddress, aptle.ExternalReviewerID, aptle.Message, aptle.EmployeeID
                                        , aptle.ExternalEmployeeID);
                                    }
                                 }
                            }
                            else if (aptle.ExternalReviewerID != 0)
                            {
                                if (bal.GetTaskExtByTitle("Notification_of_Excessive_Reminders_External_Reviewer").Count != 0)
                                {
                                    if (bal.GetExtTaskMessage(bal.GetTaskExtByTitle("Notification_of_Excessive_Reminders_External_Reviewer")[0].TaskID).Count != 0)
                                    {
                                        Task_ExtMessages tem = bal.GetExtTaskMessage(bal.GetTaskExtByTitle("Notification_of_Excessive_Reminders_External_Reviewer")[0].TaskID)[0];
                                        Emailer.Send(ConfigurationManager.AppSettings["Mail.From"]
                                             , tem.Subject
                                             , tem.Message
                                             .Replace("@@MaxNoOfReminders@@", MaxNoOfReminders.ToString())
                                             .Replace("@@ExternalReviewerName@@", aptle.ExternalReviewer.NameString)
                                             .Replace("@@ExternalReviewerMailingAddress@@", aptle.ExternalReviewer.MailingAddress)
                                             .Replace("@@ExternalReviewerEmail@@", aptle.ExternalReviewer.Email)
                                             .Replace("@@ExternalReviewerPhone@@", aptle.ExternalReviewer.PhoneAndFax)
                                             .Replace("@@ReminderEmail@@", Cryptography.Decrypt(row.Message))
                                             , "AutoEmailer", row.ApplicationID);
                                    }
                                aptle.Reminders++;
                                bal.UpdateAppTskLgExt(aptle.ApplicationID, aptle.TaskID, aptle.ActionID, aptle.SentDate, aptle.Completed,
                                    aptle.Reminders, aptle.EmailAddress, aptle.ExternalReviewerID, aptle.Message, aptle.EmployeeID
                                    , aptle.ExternalEmployeeID);

                            }
                        }
                            else if (aptle.ExternalEmployeeID != 0)
                            {
                                Emailer.Send(ConfigurationManager.AppSettings["Mail.From"]
                                , "Notification of Excessive Reminders for " + aptle.ExternalEmployee.NameString + " (External Employee) , Reminder # " + row.ReminderNumber, "The following reminder has been sent 7 times to "
                                + aptle.ExternalEmployee.NameString
                                + ", Email: " + aptle.ExternalEmployee.Email
                                + ", Phone: " + aptle.ExternalEmployee.Phone
                                + ", Mailing Address: " + aptle.ExternalEmployee.Address
                                + ". The Administration of Faculty Promotion System need to follow up manually.     "
                                + Cryptography.Decrypt(row.Message), "AutoEmailer", row.ApplicationID);
                            aptle.Reminders++;
                            bal.UpdateAppTskLgExt(aptle.ApplicationID, aptle.TaskID, aptle.ActionID, aptle.SentDate, aptle.Completed,
                                aptle.Reminders, aptle.EmailAddress, aptle.ExternalReviewerID, aptle.Message, aptle.EmployeeID
                                , aptle.ExternalEmployeeID);

                        }
                        // }
                        continue;
                    }
                }
                else
                {
                    if(bal.GetEmployeeByAppTsk(row.ApplicationID, row.TaskID).Count == 0)
                    {
                        return;
                    }
                    Employee emp = bal.GetEmployeeByAppTsk(row.ApplicationID, row.TaskID)[0];
                    email = emp.Email;
                   
                    if (row.ReminderNumber > MaxNoOfReminders)
                    {
                        // Stop sending reminder automatic Reminders
                        //Application_TaskLog aptl = bal.GetAppTaskLog().Where(a => a.TaskLogID == row.TaskLogID).ToList()[0];
                        //aptl.Completed = true;
                        //bal.UpdateAppTaskLogStatusByTaskID(aptl.Completed, aptl.ApplicationID, aptl.TaskID);
                        if (bal.GetTaskExtByTitle("Notification_Excessive_Reminders_Employee").Count != 0)
                        {
                            if (bal.GetExtTaskMessage(bal.GetTaskExtByTitle("Notification_Excessive_Reminders_Employee")[0].TaskID).Count != 0)
                            {
                                Task_ExtMessages tem = bal.GetExtTaskMessage(bal.GetTaskExtByTitle("Notification_Excessive_Reminders_Employee")[0].TaskID)[0];
                                Emailer.Send(ConfigurationManager.AppSettings["TechnicalAdminEmail"]
                                     , tem.Subject
                                     , tem.Message
                                     .Replace("@@MaxNoOfReminders@@", MaxNoOfReminders.ToString())
                                     .Replace("@@Employee@@", emp.NameString)
                                     .Replace("@@EmployeeDepartment@@", emp.Department1.Name)
                                     .Replace("@@EmployeeEmail@@", emp.Email)
                                     .Replace("@@EmployeePhone@@", emp.Phone)
                                     .Replace("@@ReminderEmail@@", Cryptography.Decrypt(row.Message))
                                     , "AutoEmailer", row.ApplicationID);
                                bal.UpdateAppTaskLogReminders(row.TaskLogID);

                            }
                        }
                        continue;
                    }
                }


                try
                {
                    Emailer.Send(email, ConfigurationManager.AppSettings["System_Title"] + "- Reminder (" + row.ReminderNumber + ")", Cryptography.Decrypt(row.Message), "AutoEmailer", row.ApplicationID);


                    if (row.ActionID == -1)
                    {
                        Application_TaskLogExt aptle = bal.GetAppTaskLogExt().Where(a => a.TaskLogID == row.TaskLogID).ToList()[0];
                        bal.UpdateAppTskLgExt(
                            aptle.ApplicationID,
                            aptle.TaskID,
                            aptle.ActionID,
                            aptle.SentDate,
                            aptle.Completed,
                            aptle.Reminders + 1,
                            aptle.EmailAddress,
                            aptle.ExternalReviewerID,
                            aptle.Message,
                            aptle.EmployeeID, aptle.ExternalEmployeeID);
                    }
                    else
                    {
                        bal.UpdateAppTaskLogReminders(row.TaskLogID);
                        List<Department> mngrTable = bal.GetDepartmentByAppTsk(row.ApplicationID, row.TaskID);
                        if (mngrTable.Count > 0)
                        {

                            Department managerInfo = mngrTable[0];
                            if (!string.IsNullOrEmpty(managerInfo.DeputyEmail))
                            {
                                try
                                {
                                    Emailer.Send(managerInfo.DeputyEmail, ConfigurationManager.AppSettings["System_Title"] + "- Reminder CC (" + row.ReminderNumber + ")", Cryptography.Decrypt(row.Message), "AutoEmailer", row.ApplicationID);
                                }
                                catch (Exception e)
                                {
                                    throw (e);
                                }
                            }
                        }

                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
    //public static void SendReminders()
    //{        
    //    PromAdminTableAdapters.ApplicationTaskLogTableAdapter tasksAdapter = new PromAdminTableAdapters.ApplicationTaskLogTableAdapter();
    //    PromAdminTableAdapters.vw_RemindersTableAdapter adapter = new PromAdminTableAdapters.vw_RemindersTableAdapter();
    //    PromAdmin.vw_RemindersDataTable table = adapter.GetData();
    //    HRTableAdapters.EmployeeTableAdapter empAdapter = new HRTableAdapters.EmployeeTableAdapter();
    //    PromAdminTableAdapters.DepartmentManagersTableAdapter mngrAdapter = new PromAdminTableAdapters.DepartmentManagersTableAdapter();
    //    ExternalRemindersTableAdapters.Task_ExtTableAdapter tskExtAdapter = new ExternalRemindersTableAdapters.Task_ExtTableAdapter();
    //    ExternalRemindersTableAdapters.Application_TaskLogExtTableAdapter appTskLgExtAdapter = new ExternalRemindersTableAdapters.Application_TaskLogExtTableAdapter();


    //    foreach (PromAdmin.vw_RemindersRow row in table)
    //    {
    //        //while ((row.RemindersExpected - row.Reminders) > 1)
    //        //{
    //        //    if (row.ActionID == -1)
    //        //    {
    //        //        appTskLgExtAdapter.UpdateReminders(row.TaskLogID);
    //        //    }
    //        //    else
    //        //    {
    //        //        tasksAdapter.UpdateReminders(row.TaskLogID);
    //        //    }
    //        //    table = adapter.GetData();
    //        //}
    //        if (row.RemindAfter > 0)
    //        {



    //            string email;
    //            if (row.ActionID == -1)
    //            {
    //                email = appTskLgExtAdapter.GetDataByTaskLogID(row.TaskLogID)[0].EmailAddress;
    //            }
    //            else
    //            {
    //                email = empAdapter.GetDataByAppTsk(row.ApplicationID, row.TaskID)[0].Email + "@kfupm.edu.sa";
    //            }


    //            try
    //            {
    //                Emailer.Send(email, "Faculty Promotion System - Reminder (" + row.ReminderNumber + ")", Cryptography.Decrypt(row.Message), "AutoEmailer", row.ApplicationID);
    //            }
    //            catch (Exception)
    //            {

    //                throw;
    //            }

    //            if (row.ActionID == -1)
    //            {
    //                appTskLgExtAdapter.UpdateReminders(row.TaskLogID);
    //            }
    //            else
    //            {
    //                tasksAdapter.UpdateReminders(row.TaskLogID);
    //                PromAdmin.DepartmentManagersDataTable mngrTable = mngrAdapter.GetDataByApplicationTask(row.ApplicationID, row.TaskID);
    //                if (mngrTable.Rows.Count > 0)
    //                {
    //                    PromAdmin.DepartmentManagersRow managerInfo = mngrTable[0];
    //                    if (!string.IsNullOrEmpty(managerInfo.DeputyEmail) && managerInfo.DeputyEmail != "abc@kfupm.edu.sa")
    //                    {
    //                        try
    //                        {
    //                            Emailer.Send(managerInfo.DeputyEmail, "Faculty Promotion System - Reminder CC (" + row.ReminderNumber + ")", Cryptography.Decrypt(row.Message), "AutoEmailer", row.ApplicationID);
    //                        }
    //                        catch (Exception e)
    //                        {
    //                            throw(e);
    //                        }
    //                    }
    //                }


    //            }
    //        }
    //    }
    //    if (adapter.GetData().Count != 0)
    //    {
    //        Emailer.Send("facpromote@kfupm.edu.sa", "Number of Reminders not updated", @"see the Reminders View", "Auto Mailer", -1);
    //    }

    //}
}
