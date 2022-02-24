using System;
using BL.Data;
    public partial class Admin_NameExclusion : System.Web.UI.Page
    {
        BAL bal = new BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            rptNameExclusion.DataSource = bal.GetNameExclusion();
            rptNameExclusion.DataBind();
        }
    }
