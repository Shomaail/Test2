using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BL.Data;
    public partial class Controls_History : System.Web.UI.UserControl
    {
        BAL bal = new BAL();
        public List<byte> leri = new List<byte>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string GetRoleFromApplicationRole(byte RoleID)
        {
            int applicationID = int.Parse(Request.QueryString["applicationID"]);
            return bal.GetApplicationRole(applicationID, RoleID)[0].Role.Title;

        }
         public string GetIsActingFromApplicationRole(byte RoleID)
         {
             int applicationID = int.Parse(Request.QueryString["applicationID"]);
             return bal.GetApplicationRole(applicationID, RoleID)[0].IsActing == "Acting" ? " - Acting" : "";
         }
         public void LoadHistory(int applicationID, int roleID)
         {
            leri = bal.GetNameExclusion(CurRoleID).Select(ne => ne.ExcludedRoleID).ToList();
            gvApplicationTracking.DataSource = bal.GetApplicationLog()
                .Where(a => a.ApplicationID == applicationID)
                .OrderByDescending(a=>a.ActionTime);

            gvApplicationTracking.DataBind();
      
         }

        public string GetPeriod(DateTime dt1, DateTime dt2)
        {
            if (dt2 == DateTime.MinValue) return " ";

            try
            {
                TimeSpan ts = dt1.Subtract(dt2);
                string result = "";
                if (ts.Days == 1)
                    result += ts.Days + " day ";
                if (ts.Days > 1)
                    result += ts.Days + " days ";
                if (ts.Hours == 1)
                    result += ts.Hours + " hour";
                if (ts.Hours > 1)
                    result += ts.Hours + " hours";
                if (ts.Days == 0 && ts.Hours == 0)
                    return "(Less than an hour)";
                else
                    return "(" + result + ")";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #region Properties
        private byte curRoleID = 0;
        public byte CurRoleID
        {
            get
            {
                if (curRoleID == 0)
                {
                    try
                    {
                        curRoleID = byte.Parse(Cryptography.Decrypt(Request.QueryString["RoleID"].Replace(" ", "+")));
                        //curRoleID = byte.Parse(Request.QueryString["RoleID"].Replace("ae54sdafaas5313qw", "").Replace("f98poj64lsks98336hujm9811336fsdf", ""));
                    }
                    catch (Exception)
                    {
                        Response.Redirect("~/Main.aspx");

                    }
                }
                return curRoleID;
            }
        }
        #endregion
        protected void grdHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {      

            //if (e.CommandName == "ShowEmail")
            //{
            //    int logID = int.Parse(e.CommandArgument.ToString());
            //    WorkflowDynamicTableAdapters.Application_LogTableAdapter appLgAdapter = new WorkflowDynamicTableAdapters.Application_LogTableAdapter();
            //    WorkflowDynamicTableAdapters.AppTskLg4AppTrackingTableAdapter appTskLgAdapter = new WorkflowDynamicTableAdapters.AppTskLg4AppTrackingTableAdapter();
            //    WorkflowDynamicTableAdapters.Application_LogExtTableAdapter appLgExtAdapter = new WorkflowDynamicTableAdapters.Application_LogExtTableAdapter();


            //    //WorkflowDynamic.Application_LogExtRow appLgExtRow = 
            //    WorkflowDynamic.Application_LogRow appLgRow;
            //    WorkflowDynamic.AppTskLg4AppTrackingDataTable dt = new WorkflowDynamic.AppTskLg4AppTrackingDataTable();
            //    try
            //    {
            //        if (appLgExtAdapter.GetDataByPK(logID)[0].ExtActionTitle.Contains("External"))
            //        {
            //            WorkflowDynamic.AppTskLg4AppTrackingRow dr = dt.NewAppTskLg4AppTrackingRow();                    
            //            dr.Message = appLgExtAdapter.GetDataByPK(logID)[0].IsMessageNull() ? "" : appLgExtAdapter.GetDataByPK(logID)[0].Message;
            //            dt.ImportRow(dr);
            //            gvEmail.DataSource = dt;
            //            gvEmail.DataBind();

            //        }
            //    }
            //    catch (Exception)
            //    {
            //        appLgRow = appLgAdapter.GetDataByPK(logID)[0];
            //        dt = appTskLgAdapter.GetDataByAppAcDt(appLgRow.ApplicationID, appLgRow.ActionID, appLgRow.ActionTime);
            //        if (dt.Count == 0)
            //         return;
            //        gvEmail.DataSource = dt;
            //        gvEmail.DataBind();
            //    }

            //    //foreach (WorkflowDynamic.AppTskLg4AppTrackingRow r in dt)


            //    //if (dt.Rows.Count < 6)
            //    //   
            //    //else
            //    //{
            //    //    for (int i = dt.Rows.Count - 1; i >= 1; i--)
            //    //    {
            //    //        dt.Rows.RemoveAt(i);
            //    //    }
            //    //    gvEmail.DataSource = dt;
            //    //}
            //    //dt.ImportRow(dt.Rows[0]);

            //    programmaticModalPopup1.Show();
            //}
        }
        protected void hideModalPopupViaServer1_Click(object sender, EventArgs e)
        {
            this.programmaticModalPopup1.Hide();
        }
    }
