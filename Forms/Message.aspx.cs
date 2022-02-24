using System;
using System.Data;
using System.Web.UI.WebControls;
using BL.Data;
using System.Linq;

public partial class Forms_Message : System.Web.UI.Page
{
    BAL bal = new BAL();
    //public override string StyleSheetTheme
    //{
    //    get
    //    {
    //        return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        bal.CheckAndCompletePermanentCommitteeInAppRole(Master.ApplicationID);
        if (bal.GetApplicationActionStatus(Master.ApplicationID).Count == 0)
        {
            foreach (WFAction a in bal.GetActions(RecordStatus.Active.ToString()))
            {
                bal.InsertApplicationActionStatus(Master.ApplicationID, a.ActionID, a.Status);
                if (a.AttActionID.HasValue)
                {
                    bal.InsertApplicationActionStatus(Master.ApplicationID, a.AttActionID.Value, RecordStatus.Inactive.ToString());
                }
            }
        }
        if(bal.GetApplicationActionStatus(Master.ApplicationID).Count != (bal.GetActions(RecordStatus.Active.ToString()).Count + bal.GetActions(RecordStatus.Active.ToString(),true).Count))
        {
            bal.DeleteApplicationActionStatus(Master.ApplicationID);
            foreach (WFAction a in bal.GetActions(RecordStatus.Active.ToString()))
            {
                bal.InsertApplicationActionStatus(Master.ApplicationID, a.ActionID, a.Status);
                if (a.AttActionID.HasValue)
                {
                    bal.InsertApplicationActionStatus(Master.ApplicationID, a.AttActionID.Value, RecordStatus.Inactive.ToString());
                }
            }
        }
        if (bal.GetForm_AppProperties(Master.ApplicationID).Count == 0)
        {
            if (bal.GetForm_AreaOfSp(Master.ApplicationID).Count == 0)
            {
                bal.InsertForm_AppProperties(Master.ApplicationID, "", "", "", null, null);
            }
            else
            {
                Form_AreaOfSp fas = bal.GetForm_AreaOfSp(Master.ApplicationID)[0];
                bal.InsertForm_AppProperties(Master.ApplicationID, fas.AreaOfSpecialization, fas.PhdFrom, "", null, null);
            }
        }
        try
        {
            foreach (TaskForm tf in bal.GetTaskFormByTask(Master.TaskID, false).Where(tf => tf.Checkable))
            {
                if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, tf.FormID, false, 0).Count == 0)
                {
                    bal.InsertAppTskFrm(Master.ApplicationID, Master.TaskID, tf.FormID, false, "", false, 0);
                }
            }
            //PromotionTableAdapters.TaskMessageTableAdapter adapter = new PromotionTableAdapters.TaskMessageTableAdapter();
            //rptMessages.DataSource = adapter.GetDataByTask(Master.ApplicationID, Master.TaskID);
            //rptMessages.DataBind();
            rptMessages.DataSource = bal.GetAppTaskLogByAppIDTaskID(Master.ApplicationID, Master.TaskID).Where(atl => !atl.Completed);
            rptMessages.DataBind();
        }
        catch (Exception exp)
        {
            Response.Redirect("~/Main.aspx");
            //FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectToLoginPage();
        }
    }
    protected void rptMessages_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
}
