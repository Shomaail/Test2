using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Collections.Generic;
using BL.Data;
using System.Linq;

public partial class _ExtLogin : System.Web.UI.Page
{
    BAL bal = new BAL();
    ExtRevBAL erBAL = new ExtRevBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
             //   Response.Write(Cryptography.Encrypt("edc"));
        Response.Cookies["langCookie"].Value = "en";
        Response.Cookies["langCookie"].Expires = DateTime.Now.AddDays(1);
        if (Session != null)
        {
            if (Session["login"] != null && Session["login"].ToString().Equals("true"))
            {
                Response.Redirect("ExtForms/ExtMain.aspx");
            }
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

    protected void Button1_Click(object sender, EventArgs e)
    {        
        List<Employee> le = bal.GetEmployeeByEmail(txtUserName.Text + ConfigurationManager.AppSettings["OrganizationEmail1"]);
        List<Employee> leDeputy = bal.GetEmployeeByDeputyEmail(txtUserName.Text + ConfigurationManager.AppSettings["OrganizationEmail1"]);
        if (bal.GetWorkflowAttribute().Where(wa => wa.Attribute == GlobalAttribute.Password.ToString()).Count() == 0)
        {
            Task_ExtMessages tem = bal.GetExtTaskMessage().Where(a => a.Task_Ext.Title == TaskExtID.Check_System_Notification.ToString()).ToList()[0];
            Emailer.Send(ConfigurationManager.AppSettings["TechnicalAdminEmail"], tem.Subject, tem.Message, "AutoEmailer", -1);
            return;
        }
        string pwd = bal.GetWorkflowAttribute().Where(wa => wa.Attribute == GlobalAttribute.Password.ToString()).ToList()[0].Value;

        if (leDeputy.Count > 0 && txtPassword.Text == pwd)
        {
            Session["user"] = leDeputy[0];
            Session["IsDeputy"] = true;
            Session["IsAdmin"] = IsAdmin(txtUserName.Text + ConfigurationManager.AppSettings["OrganizationEmail1"]);
            Session["ExtEmp"] = false;
            Response.Redirect("Main.aspx?spaccess=1");
            return;
        }
        else if (le.Count > 0 )
        {
            if(txtPassword.Text == pwd)
            {
                Session["user"] = le[0];

            }
            else
            {
                Session["user"] = "NonFacultyEmployee";
            }
            Session["ExtEmp"] = false;
            Session["IsDeputy"] = false;
            Session["IsAdmin"] = IsAdmin(txtUserName.Text + ConfigurationManager.AppSettings["OrganizationEmail1"]);
            Response.Redirect("Main.aspx?spaccess=1");
            return;
        }

        //HRTableAdapters.EmployeeTableAdapter adapter = new HRTableAdapters.EmployeeTableAdapter();
        //    HR.EmployeeDataTable allEmptable = adapter.GetDataByUserID(txtUserName.Text.ToString());
        //    HR.EmployeeDataTable deputyTable = adapter.GetDataByDeputyUserID(txtUserName.Text.ToString());
        //    if (deputyTable.Count > 0 && txtPassword.Text.ToString() == Cryptography.Decrypt("ptD7JUyHvvN1bj5H+G7EuQ=="))
        //    {
        //        Session["user"] = deputyTable[0];
        //        Session["IsDeputy"] = true;
        //        Session["IsAdmin"] = IsAdmin(txtUserName.Text.ToString());
        //        Response.Redirect("Main.aspx?spaccess=1");
        //        return;
        //    }
        //    else if (allEmptable.Count > 0 && txtPassword.Text.ToString() == Cryptography.Decrypt("ptD7JUyHvvN1bj5H+G7EuQ=="))
        //    {
        //        Session["user"] = allEmptable[0];
        //        Session["IsDeputy"] = false;
        //        Session["IsAdmin"] = IsAdmin(txtUserName.Text.ToString());                
        //        Response.Redirect("Main.aspx?spaccess=1");
        //        return;
        //    }
        if (erBAL.GetExtRevByEmailPwd(txtUserName.Text, txtPassword.Text).Count > 0)
        {
            ExternalReviewer er = erBAL.GetExtRevByEmailPwd(txtUserName.Text, txtPassword.Text)[0];
            Session["ExtRevID"] = er.ExternalReviewerID;
            Session["login"] = "true";
            Session["IsDeputy"] = false;
            Session["IsAdmin"] = false;
            Session["ExtEmp"] = false;
            bal.InsertActionLog(er.NameString + " (email: " + er.Email + ")" + Session["userInfo"], DateTime.Now);
            Response.Redirect("ExtForms/ExtMain.aspx");
        }
        //Form.Form_FinalRefreeDataTable table = Adapters.FrmFnlRefAdapter.GetUser(txtUserName.Text.ToString(), Cryptography.Encrypt(txtPassword.Text));
        //if (table.Count > 0)
        //{
        //    Session["refree"] = table[0];
        //    Session["refreeID"] = table[0].RefreeID;
        //    Session["applicationID"] = table[0].ApplicationID;
        //    Session["login"] = "true";
        //    Session["user"] = table[0];
        //    Session["IsDeputy"] = false;
        //    Session["IsAdmin"] = false;
        //    /*
        //    Session["refreeID"] = table[0].RefreeID;
        //    Session["applicationID"] = table[0].ApplicationID;
        //    Session["login"] = "true";
        //    */
        //    Response.Redirect("ExtForms/ExtMain.aspx");
        //}
        else
        {
            lblEx.Text = "Invalid Username or Password. Please try again.";
            lblEx.Visible = true;
            txtPassword.Text = "";
            txtUserName.Text = "";
        }

    }
}
