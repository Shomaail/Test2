using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using BL.Data;

public partial class Forms_ShowWillingnessPC : System.Web.UI.Page
{
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        //lblOrganizationName.Text = ConfigurationManager.AppSettings["OrganizationName"];
        int appID = int.Parse(Request.Params["appID"].ToString());
        Application app = bal.GetApplication(appID)[0];
        string empID = Request.Params["empID"].ToString();
        int eeID = int.Parse(Request.Params["eeID"].ToString());
        if (bal.GetForm_FinalPCByPK(appID, empID, eeID).Count == 0)
        {
            Response.Redirect("~/Forms/PageNotAvailable.aspx");
            return;
        }
        Form_FinalPC ffpc = bal.GetForm_FinalPCByPK(appID, empID, eeID)[0];
        string message = "";
        if (ffpc.Position == (int)PCPosition.Chairman)
        {
            message = bal.GetTaskExtMessage((int)TaskExtID.Willingness_Letter_Content_Promotion_Committee_Chairman)[0].Message;
            lblWillingnessLetterContent.Text = message
                       .Replace("@@RecipientName@@", ffpc.Employee.NameString)
                       .Replace("@@ApplicantCollege@@", app.Employee.Department1.ParentDeptString)
                       .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
                       .Replace("@@OrganizationAddress@@", ConfigurationManager.AppSettings["OrganizationAddress"])
                       .Replace("@@Applicant@@", app.Employee.NameString)
                       .Replace("@@ApplicantRank@@", app.Employee.Rank)
                       .Replace("@@ApplicantDept@@", app.Employee.Department)
                       .Replace("@@ApplicantAreaOfSp@@", app.Form_AppProperties.AreaOfSpecialization)
                       .Replace("@@ForRank@@", app.ForRank)                       
                       .Replace("\r\n", "<br>")
                       .Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
            ;
        }
        else if ((ffpc.ExternalEmployeeID == 0 ? "Internal" : "External") == EmployeeType.Internal.ToString())
        {
            message = bal.GetTaskExtMessage((int)TaskExtID.Willingness_Letter_Content_Promotion_Committee_Member)[0].Message;
            lblWillingnessLetterContent.Text = message
                       .Replace("@@RecipientName@@", ffpc.Employee.NameString)
                       .Replace("@@ApplicantCollege@@", app.Employee.Department1.ParentDeptString)
                       .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
                       .Replace("@@OrganizationAddress@@", ConfigurationManager.AppSettings["OrganizationAddress"])
                       .Replace("@@Applicant@@", app.Employee.NameString)
                       .Replace("@@ApplicantRank@@", app.Employee.Rank)
                       .Replace("@@ApplicantDept@@", app.Employee.Department)
                       .Replace("@@ApplicantAreaOfSp@@", app.Form_AppProperties.AreaOfSpecialization)
                       .Replace("@@ForRank@@", app.ForRank)
                       .Replace("\r\n", "<br>")
                       .Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
        }
        else if ((ffpc.ExternalEmployeeID == 0 ? "Internal" : "External") == EmployeeType.External.ToString())
        {
            message = bal.GetTaskExtMessage((int)TaskExtID.Willingness_Letter_Content_Promotion_Committee_Member_External)[0].Message;
            lblWillingnessLetterContent.Text = message
                       .Replace("@@RecipientName@@", ffpc.ExternalEmployee.NameString)
                       .Replace("@@ApplicantCollege@@", app.Employee.Department1.ParentDeptString)
                       .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
                       .Replace("@@OrganizationAddress@@", ConfigurationManager.AppSettings["OrganizationAddress"])
                       .Replace("@@Applicant@@", app.Employee.NameString)
                       .Replace("@@ApplicantRank@@", app.Employee.Rank)
                       .Replace("@@ApplicantDept@@", app.Employee.Department)
                       .Replace("@@ApplicantAreaOfSp@@", app.Form_AppProperties.AreaOfSpecialization)
                       .Replace("@@ForRank@@", app.ForRank)
                       .Replace("\r\n", "<br>")
                       .Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
            ;
        }      
        
        if (ffpc.WLStatus == WillingessStatus.Waiting.ToString())
        {
            RadioButtonAccept.Enabled = true;
            RadioButtonReject.Enabled = true;            
            btnSubmit.Enabled = true;
        }
        else
        {
            if (ffpc.WLStatus == WillingessStatus.Accepted.ToString())
            {
                RadioButtonAccept.Checked = true;
                RadioButtonReject.Checked = false;
            }
            else
            {
                RadioButtonAccept.Checked = false;
                RadioButtonReject.Checked = true;
            }
            RadioButtonAccept.Enabled = false;
            RadioButtonReject.Enabled = false;
            
            btnSubmit.Enabled = false;
            lblThanks.Text = "You have already submitted your willingness.";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int appID = int.Parse(Request.Params["appID"].ToString());
        string empID = Request.Params["empID"].ToString();
        int eeID = int.Parse(Request.Params["eeID"].ToString());
        Form_FinalPC ffpc = bal.GetForm_FinalPCByPK(appID, empID, eeID)[0];   
        if (RadioButtonAccept.Checked == true)
        {
            bal.UpdateForm_FinalPC(appID, empID, eeID, ffpc.Position, WillingessStatus.Accepted.ToString(), ffpc.DigitalSignature, ffpc.DSDate, ffpc.Comments, ffpc.Source
                , DateTime.Now, tbComments.Value, ffpc.Status);
            string messageBody = bal.GetTaskExtMessage((int)TaskExtID.Acceptance_Notification_Promotion_Committee)[0].Message;
            string email = "";
            if(ffpc.ExternalEmployeeID == 0 )
            {
                messageBody = messageBody.Replace("@@RecipientName@@", ffpc.Employee.NameString);
                email = ffpc.Employee.Email;
            }
            else
            {
                messageBody = messageBody.Replace("@@RecipientName@@", ffpc.ExternalEmployee.NameString);
                email = ffpc.ExternalEmployee.Email;
            }
            messageBody = messageBody.Replace("@@Applicant@@", ffpc.Application.Employee.NameString);
            messageBody = messageBody.Replace("@@ApplicantDept@@", ffpc.Application.Employee.Department);
            messageBody = messageBody.Replace("@@ForRank@@", ffpc.Application.ForRank);
            messageBody = messageBody.Replace("@@WebsiteURL@@", ConfigurationManager.AppSettings["WebsiteURL"]);
            messageBody = messageBody.Replace("@@TopAuthority@@", bal.GetEmployeeByPK((bal.GetApplicationRole(appID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameString);
            messageBody = messageBody.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);
            messageBody = messageBody.Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"]);

            Emailer.Send(email, bal.GetTaskExtMessage((int)TaskExtID.Acceptance_Notification)[0].Subject, messageBody, "AutoEmailer", appID);

            RadioButtonAccept.Enabled = false;
            RadioButtonReject.Enabled = false;
            btnSubmit.Enabled = false;
            lblThanks.Text = @"Thanks for accepting the request. An email has been sent to your email address containing the login details. 
                                Thanks for cooperation.";
            List<Form_FinalPC> lffpc = bal.GetForm_FinalPC(appID).Where(p => p.WLStatus == WillingessStatus.Accepted.ToString()).ToList();
            if (lffpc.Count() == 5 )
            {
                byte i = 1;
                foreach (Form_FinalPC pc in lffpc)
                {
                    if (pc.Position == (int)PCPosition.Chairman)
                    {
                        if (bal.GetApplicationRole(appID, (byte)RoleID.Promotion_Committee_Chairman).Count == 0)
                        {
                            bal.InsertApplicationRoles(appID,pc.EmployeeID, (byte)RoleID.Promotion_Committee_Chairman);
                        }
                        else
                        {
                            bal.UpdateApplicationRoles(appID, (byte)RoleID.Promotion_Committee_Chairman, pc.EmployeeID, pc.ExternalEmployeeID,"");
                        }
                    }

                    else
                    {
                        if (bal.GetApplicationRole(appID, (byte)((byte)RoleID.Promotion_Committee_Chairman + i)).Count == 0)
                        {
                            bal.InsertApplicationRoles(appID, pc.EmployeeID, (byte)((byte)RoleID.Promotion_Committee_Chairman + i));
                        }
                        else
                        {
                            bal.UpdateApplicationRoles(appID, (byte)((byte)RoleID.Promotion_Committee_Chairman + i), pc.EmployeeID, pc.ExternalEmployeeID,"");
                        }
                        i++;
                    }

                }
                messageBody = bal.GetExtTaskMessage((int)TaskExtID.All_PCMembers_Have_Accepted)[0].Message;
                email = "";
                messageBody = messageBody.Replace("@@TopAuthority@@", bal.GetEmployeeByPK((bal.GetApplicationRole(appID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameString);
                messageBody = messageBody.Replace("@@Applicant@@", ffpc.Application.Employee.NameString);
                messageBody = messageBody.Replace("@@ApplicantDept@@", ffpc.Application.Employee.Department);
                messageBody = messageBody.Replace("@@ForRank@@", ffpc.Application.ForRank);
                email = bal.GetEmployeeByPK((bal.GetApplicationRole(appID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].Email;
                email += "," + bal.GetDepartmentBySN(ConfigurationManager.AppSettings["TopAuthority_TitleShort"])[0].DeputyEmail;
                messageBody = messageBody.Replace("@@TopAuthority@@", bal.GetEmployeeByPK((bal.GetApplicationRole(appID).Where(a => a.RoleID == (byte)RoleID.TopAuthority).ToList()[0].EmployeeID))[0].NameString);
                messageBody = messageBody.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);
                Emailer.Send(email, bal.GetExtTaskMessage((int)TaskExtID.All_PCMembers_Have_Accepted)[0].Subject, messageBody, "AutoEmailer", appID);
            }         
        }
        else
        {
            RadioButtonAccept.Enabled = false;
            RadioButtonReject.Enabled = false;
            btnSubmit.Enabled = false;
            bal.UpdateForm_FinalPC(appID, empID, eeID, ffpc.Position, WillingessStatus.Declined.ToString(), ffpc.DigitalSignature, ffpc.DSDate, ffpc.Comments, ffpc.Source
                , DateTime.Now, tbComments.Value, ffpc.Status);
            lblThanks.Text = "Your willingness has been submitted. Thanks for cooperation.";
        }
        Application_TaskLogExt  atle = bal.GetAppTaskLogExt().Where(a => a.ApplicationID == appID && a.EmployeeID == empID && a.ExternalEmployeeID == eeID).ToList().First();
        bal.UpdateAppTskLgExt(appID, atle.TaskID, atle.ActionID, atle.SentDate, true, atle.Reminders, atle.EmailAddress
            , atle.ExternalReviewerID, atle.Message
            , empID, eeID);
    }
}

