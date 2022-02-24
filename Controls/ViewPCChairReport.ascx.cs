using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Controls_ViewPCChairReport : System.Web.UI.UserControl
{
    //public int ApplicationID
    //{
    //    get 
    //    {
    //        if (HiddenFieldApplicationID.Value.Length == 0)
    //            return -1;
    //        else
    //           //return Int32.Parse(HiddenFieldApplicationID.Value);
    //            return int.Parse(Request.Params.Get("applicationID").ToString());
    //    }
    //    set 
    //    {
    //        HiddenFieldApplicationID.Value = value.ToString();
    //    }
    //} 
    private string dataPath = null;

    public void Page_Load(object sender, EventArgs e)
    {
        int appID = int.Parse(Request.Params.Get("applicationID").ToString());
        //ApplicationID = int.Parse(Request.Params.Get("applicationID").ToString());
        //try
        //{
        //    Promotion.PromotionCommitteeTempRow user = Session["pcMem"] as Promotion.PromotionCommitteeTempRow;
        //    ApplicationID = user.ApplicationID;

        //}
        //catch (Exception)
        //{
        //    ApplicationID = int.Parse(Request.Params.Get("applicationID").ToString());            
        //}

        dataPath = Server.MapPath("~\\App_LocalResources") + "\\";
        //ApplicationID = int.Parse(Request.Params.Get("applicationID").ToString());
        //if (ApplicationID > 0)
        if (appID > 0)
        {
            BindFiles(appID);
        }
        
    }

    private void BindFiles(int appID)
    {
        //PromotionTableAdapters.vw_PCChairReportTableAdapter adapter = new PromotionTableAdapters.vw_PCChairReportTableAdapter();
        ViewsTableAdapters.vw_PCChairReportTableAdapter adapter = new ViewsTableAdapters.vw_PCChairReportTableAdapter();
        Views.vw_PCChairReportDataTable  table = adapter.GetDataByAppID(appID);

        grdFiles.DataSource = table;
        grdFiles.DataBind();
    }

    protected void grdFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        int appID = int.Parse(Request.Params.Get("applicationID").ToString());
        if (appID == -1)
            return;
        FormTableAdapters.Form_PromotionReportTableAdapter adapterPR = new FormTableAdapters.Form_PromotionReportTableAdapter();
        string fileName = adapterPR.GetDataByPK(appID)[0].FileName;



        
        string filePath = dataPath + fileName;

        if (!File.Exists(filePath))
            return;

        FileStream stream = File.OpenRead(filePath);
        long length = stream.Length;
        stream.Close();

        Response.Clear();
        try
        {
            Response.Charset = "UTF-8";
            Response.AddHeader("Content-Disposition", "attachment; filename= " + fileName);
            Response.AddHeader("Content-Length", "" + length);
            Response.WriteFile(dataPath + fileName);
            Response.Flush();
            Response.Close();
        }
        catch
        {
        }
    }
    public bool GetStatusByPCID(int pcID)
    {

        PromotionTableAdapters.PromotionCommitteeTempTableAdapter adapter = new PromotionTableAdapters.PromotionCommitteeTempTableAdapter();
        Promotion.PromotionCommitteeTempRow pcRow = adapter.GetDataByAutoNumberPK(pcID)[0];
        if (pcRow.DigitalSignature == true)
            return false;
        else
            return true;

    }
    public bool GetStatusByApplication(int appID)
    {

        PromotionTableAdapters.PromotionCommitteeTempTableAdapter adapter = new PromotionTableAdapters.PromotionCommitteeTempTableAdapter();
        Promotion.PromotionCommitteeTempDataTable tablePC = adapter.GetData(appID);       
        for(int i = 0; i < 5 ; i++)

        {
            if (tablePC[i].DigitalSignature == false)
                return false;
        }
        return true;

    }
    protected void grdFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        FormTableAdapters.Form_PromotionReportTableAdapter adapterPR = new FormTableAdapters.Form_PromotionReportTableAdapter();
        PromotionTableAdapters.PromotionCommitteeTempTableAdapter adapterPC = new PromotionTableAdapters.PromotionCommitteeTempTableAdapter();
        int appID = int.Parse(Request.Params.Get("applicationID").ToString());
        Promotion.PromotionCommitteeTempDataTable pcTable = adapterPC.GetData(appID);
        adapterPC.UpdateDS(false, null, pcTable[0].AutoNumberPK);
        adapterPC.UpdateDS(false, null, pcTable[1].AutoNumberPK);
        adapterPC.UpdateDS(false, null, pcTable[2].AutoNumberPK);
        adapterPC.UpdateDS(false, null, pcTable[3].AutoNumberPK);
        adapterPC.UpdateDS(false, null, pcTable[4].AutoNumberPK);        

        string fileName = adapterPR.GetDataByPK(appID)[0].FileName;
        try
        {

            adapterPR.DeleteByPK(appID);
            File.Delete(dataPath + fileName);

        }
        catch
        {

        }
        BindFiles(appID);
    }
    protected void grdFiles_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdFiles.Rows)
        {
            (row.FindControl("LinkButtonDelete") as LinkButton).Attributes.Add("OnClick", "return confirm_delete();");
        }
    }
}


