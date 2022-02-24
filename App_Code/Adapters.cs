using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Adapters
/// </summary>
public class Adapters
{
    private static HRTableAdapters.EmployeeTableAdapter employeeAdpater;
    public static HRTableAdapters.EmployeeTableAdapter EmployeeAdpater
    {
        get
        {
            if (Adapters.employeeAdpater == null)
                Adapters.employeeAdpater = new HRTableAdapters.EmployeeTableAdapter();
            return Adapters.employeeAdpater; }        
    }
    private static WorkflowStaticTableAdapters.ActionTableAdapter actionAdapter;
    public static WorkflowStaticTableAdapters.ActionTableAdapter ActionAdapter
    {
        get
        {
            if (Adapters.actionAdapter == null)
                Adapters.actionAdapter = new WorkflowStaticTableAdapters.ActionTableAdapter();
            return Adapters.actionAdapter; }
  
    }   
    private static ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter frmRevFrmAdapter = null;
    public static ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter FrmRevFrmAdapter
    {
        get
        {
            if (frmRevFrmAdapter == null)
                frmRevFrmAdapter = new ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter();
            return frmRevFrmAdapter;
        }
    }
    private static WorkflowDynamicTableAdapters.Application_TaskFormTableAdapter appTskFrmAdapter = null;
    public static WorkflowDynamicTableAdapters.Application_TaskFormTableAdapter AppTskFrmAdapter
    {
        get
        {    if (appTskFrmAdapter == null)
                appTskFrmAdapter = new WorkflowDynamicTableAdapters.Application_TaskFormTableAdapter();
            return Adapters.appTskFrmAdapter; }        
    }
    private static WorkflowDynamicTableAdapters.ApplicationTableAdapter appAdapter;
    public static WorkflowDynamicTableAdapters.ApplicationTableAdapter AppAdapter
    {
        get
        {
            if (Adapters.appAdapter == null)
                Adapters.appAdapter = new WorkflowDynamicTableAdapters.ApplicationTableAdapter();
            return Adapters.appAdapter; }        
    }
    private static FeedBackTableAdapters.Form_FeedbackTableAdapter frmFBAdapter;
    public static FeedBackTableAdapters.Form_FeedbackTableAdapter FrmFBAdapter
    {
        get
        {
            if (Adapters.frmFBAdapter == null)
                Adapters.frmFBAdapter = new FeedBackTableAdapters.Form_FeedbackTableAdapter();
            return Adapters.frmFBAdapter;
        }
    }
    private static FormTableAdapters.Form_AreaOfSpTableAdapter frmArSpAdapter;
    public static FormTableAdapters.Form_AreaOfSpTableAdapter FrmArSpAdapter
    {
        get
        {
            if (Adapters.frmArSpAdapter == null)
                Adapters.frmArSpAdapter = new FormTableAdapters.Form_AreaOfSpTableAdapter();
            return Adapters.frmArSpAdapter;
        }
    } 
    private static FormTableAdapters.Form_RefreeTableAdapter frmRefAdapter;        
    public static FormTableAdapters.Form_RefreeTableAdapter FrmRefAdapter
    {
        get
        {
            if (Adapters.frmRefAdapter == null)
                Adapters.frmRefAdapter = new FormTableAdapters.Form_RefreeTableAdapter();
            return Adapters.frmRefAdapter;
        }
    }    
    private static FormTableAdapters.Form_FinalRefreeTableAdapter frmFnlRefAdapter;
    public static FormTableAdapters.Form_FinalRefreeTableAdapter FrmFnlRefAdapter
    {
        get
        {
            if (Adapters.frmFnlRefAdapter == null)
                Adapters.frmFnlRefAdapter = new FormTableAdapters.Form_FinalRefreeTableAdapter();
            return Adapters.frmFnlRefAdapter;
        }
    }

    public static SqlConnection connectionSP;
    public static SqlCommand pr_UpdateRanksSqlCmd;
    public static void InitializeSP()
    {
        connectionSP = new SqlConnection(ConfigurationManager.ConnectionStrings["PromotionConnectionString"].ConnectionString);
        pr_UpdateRanksSqlCmd = new SqlCommand("pr_UpdateRanks", connectionSP);
        pr_UpdateRanksSqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
    }
       
}
