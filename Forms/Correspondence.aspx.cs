using System;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using BL.Data;


    public partial class Forms_Correspondence : System.Web.UI.Page
    {
        BAL bal = new BAL();
        ExtRevBAL erBAL = new ExtRevBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = Resources.Resource.CorrespondenceM1;
            divMsg.Visible = true;
            rptCorrespondence.DataSource = bal.GetSentEmail(Master.ApplicationID).OrderByDescending(a => a.SentDate);
            rptCorrespondence.DataBind();
        }
        protected string GetNameThruEmail(string Email)
        {
            if (Email.ToLower().Contains(ConfigurationManager.AppSettings["OrganizationEmail1"].ToLower()) ||
                Email.ToLower().Contains(ConfigurationManager.AppSettings["OrganizationEmail2"].ToLower()) ||
                !Email.ToLower().Contains("@"))

            {
                if (bal.GetEmployeeByEmail(Email).Count > 0)
                {
                    return bal.GetEmployeeByEmail(Email)[0].NameString;
                }
                else
                {
                    return "Auto Emailer";
                }
            }
            else
            {
                if (bal.GetExternalEmployeeByEmail(Email).Count > 0)
                {
                    return bal.GetExternalEmployeeByEmail(Email)[0].NameString;
                }
                else if (erBAL.GetExtRevByEmail(Email).Count > 0)
                {
                    return erBAL.GetExtRevByEmail(Email)[0].NameString;
                }
                else
                {
                    return "";
                }
            }

        }
    }
