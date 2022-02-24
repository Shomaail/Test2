using System;
using BL.Data;

    public partial class Admin_Phase : System.Web.UI.Page
    {
        BAL bal = new BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            rptPhase.DataSource = bal.GetRole();
            rptPhase.DataBind();
        }
    }
