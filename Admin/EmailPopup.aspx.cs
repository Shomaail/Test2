using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EmailPopup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dept = "";
        if (Request.QueryString["dept"] != null)
        {
            dept = Request.QueryString["dept"].ToString();
        }
        int roleID = 0;
        if (Request.QueryString["dept"] != null)
        {
            roleID = int.Parse(Request.QueryString["roleID"].ToString());
        }
        BAL bal = new BAL();
        List<BL.Data.Department> ld = bal.GetDepartmentBySN(dept).Where(a => a.HeadRoleID == roleID).ToList();
        if(ld.Count == 0 )
        {
            Response.Redirect("~/Login.aspx",false);
            return;
        }

        string emailAddresses = ld[0].HeadEmail;
        string MessageBody;
        string emailSubject;
        //EmailTableAdapters.Task_ExtMessagesTableAdapter adapterTskExtMsg = new EmailTableAdapters.Task_ExtMessagesTableAdapter();
        //ExternalRemindersTableAdapters.Task_ExtTableAdapter adapterTskExt = new ExternalRemindersTableAdapters.Task_ExtTableAdapter();
        int taskExtID = bal.GetTaskExtByTitle("Welcome_note_to_new_department_managers").Count > 0 ? bal.GetTaskExtByTitle("Welcome_note_to_new_department_managers")[0].TaskID : 0;
        if(taskExtID != 0)
        {
            MessageBody = bal.GetTaskExtMessage(taskExtID).Count > 0 ? bal.GetTaskExtMessage(taskExtID)[0].Message : "";
            MessageBody = MessageBody.Replace("@@Reciever@@", ld[0].Name);
            MessageBody = MessageBody.Replace("@@Role@@", ld[0].Role.Title);
            emailSubject = bal.GetTaskExtMessage(taskExtID).Count > 0 ? bal.GetTaskExtMessage(taskExtID)[0].Subject: "";
            Email.ComposeEmailDataTable emailDataTable = new Email.ComposeEmailDataTable();
            emailDataTable.NewComposeEmailRow();
            emailDataTable.AddComposeEmailRow(emailAddresses, emailSubject, MessageBody);
            EmailContent email = new EmailContent { To = emailAddresses, Body = MessageBody, Subject = emailSubject };
            List<EmailContent> le = new List<EmailContent>();
            le.Add(email);
            ComposeEmail1.LoadData(le);
            ComposeEmail1.Visible = true;
        }
        
       // string emailTemplate = adapterTskExtMsg.GetDataByTaskID(adapterTskExt.GetDataByTitle("Welcome note to new department managers")[0].TaskID)[0].Message;
       //string emailSubject = adapterTskExtMsg.GetDataByTaskID(adapterTskExt.GetDataByTitle("Welcome note to new department managers")[0].TaskID)[0].Subject;
        


           
    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }
    protected void ComposeEmail_EmailSent(object sender, EventArgs e)
    {
//        AlertMessage("Email is successfully sent");
        labelProgrammaticPopup0.Text = "Email is successfully sent";
        programmaticModalPopup0.Show();
    }   
    protected void ComposeEmail_EmailNotSent(object sender, EventArgs e)
    {
        //AlertMessage("No email is sent");
        labelProgrammaticPopup0.Text = "No email is sent";
        programmaticModalPopup0.Show();
    } 
//    private void CloseWindow()
//    {
//        Response.Write(@"<script language=javascript>
//                    window.close();
//                    </script>");
//    }
/*    public void AlertMessage(string strMessage)
    {
        string strScript = "<script language=JavaScript>";
        strScript += "alert('" + strMessage + "');window.close();";
        strScript += "</script>";
        if (!Page.IsStartupScriptRegistered("clientScript"))
            Page.RegisterStartupScript("clientScript", strScript);
    }*/
}