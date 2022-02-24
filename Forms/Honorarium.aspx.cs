using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using BL.Data;
public partial class Forms_Honorarium : System.Web.UI.Page
{
    BAL bal = new BAL();
    ExtRevBAL extRevBAL = new ExtRevBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Master.CurrentFormLevel == -1)
        {
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID+"&roleID="+Master.CurRoleID);
            return;
        }

        Instruction1.Text = Master.CurrentFormInstruction;

    }
 
    protected void ComposeEmail_OnEmailSent(object sender, EventArgs e)
    {
        ComposeEmail1.Visible = false;

    }

    protected void ComposeEmail_OnEmailNotSent(object sender, EventArgs e)
    {
        ComposeEmail1.Visible = false;
    }



    protected void rbltHonorarium_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbltHonorarium.SelectedValue == "Promotion_Committee")
        {
            Task_ExtMessages tem = bal.GetExtTaskMessage().Where(a => a.Task_Ext.Title == "Honorarium_Promotion_Committee").ToList()[0];
            string MessageBody = tem.Message;
            //List<Application_TaskLog> latl = bal.GetAppTaskLog(Master.ApplicationID).Where(a => !a.Completed && a.Task.RoleID == (byte)RoleID.Promotion_Committee_Chair).ToList();            
            
            string Names = "\n\n\t\t\t\t\t";
            int serialNo = 1;
            foreach (Form_FinalPC ffpc in bal.GetForm_FinalPC(Master.ApplicationID).Where(f => f.ExternalEmployeeID == 0 && f.DSDate != null))
            {
                    Names += serialNo++ + ". "+ ffpc.Employee.NameString+ "\t(" + ffpc.Employee.Department1.ShortName  + ")\t\t" + ffpc.Employee.EmployeeID + (ffpc.Position == 10 ? " \tChairman" : " \tMember") + "\n\n\t\t\t\t\t";
            }
            //foreach (Form_FinalPC ffpc in bal.GetForm_FinalPC(Master.ApplicationID).Where(f=>f.ExternalEmployeeID == 0 && f.DSDate != null))
            //{
            //    Names += @"
            //            Name: "+ ffpc.Employee.NameString + @""+ (ffpc.Position == 10 ? " (Chairman)": "")+ @" 
            //            Department: " + ffpc.Employee.Department + @"                           
            //            ID: " + ffpc.Employee.EmployeeID + @"                           
                        
            //             ";
            //}

            MessageBody = 
                bal.GetForm_AttachmentAdByAppID(Master.ApplicationID).Where(fa => fa.AttachmentCategoryID == (int)AttachmentCategoryID.PCReport).Count() != 1
                ? "The promotion Committee is not yet finished with its work." 
                : MessageBody
                .Replace("@@Names@@", Names)
                .Replace("@@Sender@@", bal.GetApplicationRole(Master.ApplicationID, (byte)RoleID.TopAuthority)[0].Employee.NameString)
                .Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"])
                .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"])
                ;
            EmailContent email = new EmailContent
            {
                To = ""
        ,
                Body = MessageBody,
                Subject = tem.Subject
            };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadDataForPreview(le);            
            ComposeEmail1.Visible = true;            
        }
        else
        {

            Task_ExtMessages tem = bal.GetExtTaskMessage().Where(a => a.Task_Ext.Title == "Honorarium_External_Reviewer").ToList()[0];
            string MessageBody = tem.Message;
            string Names = "";
            foreach (Form_FinalExtRev ffer in extRevBAL.GetForm_FinalExtRev(Master.ApplicationID).Where(f => f.EvaluationStatus  == EvaluationStatus.Submitted.ToString()))
            {
                Names += @"
                        Name: " + ffer.ExternalReviewer.NameString + @"
                        Address: " + ffer.ExternalReviewer.MailingAddress + "  \n\n\t\t";
            }

            MessageBody = MessageBody
                .Replace("@@Names@@", Names)
                .Replace("@@Sender@@", bal.GetApplicationRole(Master.ApplicationID, (byte)RoleID.TopAuthority)[0].Employee.NameString)
                .Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"])
                .Replace("@@OrganizationName@@", ConfigurationManager.AppSettings["OrganizationName"]);
            EmailContent email = new EmailContent
            {
                To = ""
        ,
                Body = MessageBody,
                Subject = tem.Subject
            };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadDataForPreview(le);
            ComposeEmail1.Visible = true;
        }
    }
}
