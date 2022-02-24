using System;
using System.Configuration;
using System.Web.Security;
using BL.Data;

    public partial class Admin_Admin : System.Web.UI.MasterPage
    {
        BAL bal = new BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            lbtnSearchEmp.Attributes.Add("onclick", "window.open ('../Forms/SearchPop.aspx',null,'scrollbars=yes, status= no, resizable = yes, toolbar=no,location=no,height = 700, width = 900, left = 200, top= 200, screenx=10,screeny=600,menubar=no');");
            divAdmin.DataBind();
            divCollegAdmin.DataBind();
            Employee emp = new Employee();
            if (Session["user"] != null)
            {
                if ((bool)Session["ExtEmp"] == false)
                {
                    emp = Session["user"] as Employee;
                    lblUserName.Text = ((bool)Session["IsDeputy"]) ? "Deputy of " + emp.NameString : emp.NameString;
                }
                if (!((Session["IsAdmin"] != null) && ((bool)Session["IsAdmin"]) ||
                    (Session["IsCollegeAdmin"] != null) && ((bool)Session["IsCollegeAdmin"])))
                {
                    Response.Redirect("..");
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
                FormsAuthentication.SignOut();
                Response.Cookies.Remove("langCookie");
                Session["IsAdmin"] = false;

                Session.Clear();

                Response.Redirect(ConfigurationManager.AppSettings["LogoutURL"]);
            }


        }
    }
