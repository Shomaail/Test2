using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;

public partial class ExternalReviewers : System.Web.UI.Page
{
    public OleDbConnection connExcel;
    public OleDbCommand cmdExcel; 
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridViewExtReviewers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowAllRecords")
        {
            GridViewExtReviewers.Visible = false;
            ObjectDataSourceSelExtRev.SelectParameters["Name"].DefaultValue = e.CommandArgument.ToString();
            GridViewSelectedExtRev.DataBind();
            GridViewSelectedExtRev.Visible = true;
        }
    }
    protected void GridViewSelectedExtRev_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ButtonReturn_Click(object sender, EventArgs e)
    {
        GridViewSelectedExtRev.Visible = false;
        GridViewExtReviewers.Visible = true;
    }
    protected void linkButtonExportExcel_Click(object sender, EventArgs e)
    {
        string dataPath = Server.MapPath("~\\Reports") + "\\";
        if (OpenConnection(dataPath + "ExternalReviewers.xls"))
        {
            try
            {
                WriteToExcel(); 
            }
            catch (Exception exp)
            {
                Response.Write(exp.Message);

            }
            finally
            {
                cmdExcel.Dispose();
                connExcel.Dispose();
            }
            Response.Redirect("~/Reports/ExternalReviewers.xls");
        }  
    }

    private void WriteToExcel()
    {
        ViewsTableAdapters.vw_ExtReviewersTableAdapter adapterExtRev = new ViewsTableAdapters.vw_ExtReviewersTableAdapter();
        int rowCounter = 0;
        Views.vw_ExtReviewersDataTable dataTableExtRev = adapterExtRev.GetData();        
        
        rowCounter = 2;
        foreach (Views.vw_ExtReviewersRow row in dataTableExtRev)
        {
            try
            {
                cmdExcel.CommandText = "UPDATE [AllExtRev$B" + rowCounter + ":O" + rowCounter + "] SET F1 = '" + row.Name + "', F2 = '" + row.Rank + "',F3 = '" + row.MailingAddress + "', F4 = '" + row.Email + @"', F5 = '"
                        + row.Major + "', F6 = '" + row.Speciality + "',F7 = '" + row.PhoneAndFax + "', F8 = '" + row.ActiveAreaOfResearch + "', F9 = '" + row.PrevAreaOfResearch + @"', F10 = '"
                        + row.Webpage + "',F11 = '" + row.Comments + "', F12 = '" + row.TotalPublications + "', F13 = '" + row.NoOfJournals + "', F14 = '" + row.HIndex + "'";
                cmdExcel.ExecuteNonQuery();rowCounter++;
            }
            catch (Exception)
            {
                
                
            }
            
        }
        
    }
    private bool OpenConnection(String strFilePath)
    {
        if (!File.Exists(strFilePath)) return false;
        string strExcelConn;
        strExcelConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + strFilePath + ";" +
        "Extended Properties='Excel 8.0;HDR=NO';";

        //strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;" +
        //"Data Source=" + strFilePath + ";Excel 12.0;HDR=NO;";
        connExcel = new OleDbConnection(strExcelConn);
        cmdExcel = new OleDbCommand();
        cmdExcel.Connection = connExcel;
        connExcel.Open();
        return true;
    } 
}