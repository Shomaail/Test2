using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using BL.Data;
    public partial class Admin_Action : System.Web.UI.Page
    {
        
        BAL bal = new BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            DatabindControls();

        }

        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }
        private void DatabindControls()
        {
            rpAction.DataSource = bal.GetActions(RecordStatus.Active.ToString()).Where(a => a.ActionID != 0);
            rpAction.DataBind();
        }


        protected void rpAction_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string[] args = e.CommandArgument.ToString().Split(';');
            int actionID = int.Parse(args[0]);
            if (e.CommandName == "Delete")
            {

                if (bal.GetActionMessage().Where(a => a.ActionID == actionID).Count() > 0
                    || bal.GetApplicationActionStatus().Where(a => a.ActionID == actionID).Count() > 0
                    || bal.GetApplicationLog().Where(a => a.ActionID == actionID).Count() > 0
                    || bal.GetAppTaskLog().Where(a => a.ActionID == actionID).Count() > 0
                    )
                {
                    labelProgrammaticPopup0.Text = "You cannot delete this action since this action is involved in other tables ActionMessage, Application_ActionStatus,  Application_Log, Application_TaskLog.";
                    programmaticModalPopup0.Show();
                    return;
                }
                bal.DeleteAction(actionID);
                DatabindControls();
            }

        }
    } 

