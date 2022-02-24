using System;
using BL.Data;

    public partial class Admin_TaskDependency : System.Web.UI.Page
    {
        BAL bal = new BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            rptTaskDependency.DataSource = bal.GetTaskDependency();
            rptTaskDependency.DataBind();
        }
    }
