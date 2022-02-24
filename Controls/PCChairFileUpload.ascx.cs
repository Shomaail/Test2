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

public partial class Controls_PCChairFileUpload : System.Web.UI.UserControl
{
    private string dataPath = null;
    private int ApplicationID = -1;
    private bool newInsert = true;

    public void Page_Load(object sender, EventArgs e)
    {

        dataPath = Server.MapPath("~\\App_LocalResources") + "\\";
        ApplicationID = int.Parse(Request.Params.Get("applicationID").ToString());
        if (ApplicationID != -1)
        {
            FormTableAdapters.Form_PromotionReportTableAdapter fadapter = new FormTableAdapters.Form_PromotionReportTableAdapter();
            Form.Form_PromotionReportDataTable table = fadapter.GetDataByPK(ApplicationID);
            if (table.Count > 0)
            {
                btnUpload.Text = "Update Report";
                lblInfo.Text = "You have already uploaded the Report.<br/>If you upload any other file, the current file will be replaced with the new one";
                lblInfo.ForeColor = System.Drawing.Color.Blue;
                newInsert = false;
            }
            else
            {
                btnUpload.Text = "Upload Report";
            }
        }
        else
        {
            lblInfo.Text = "Invalid Application ID";
            lblInfo.ForeColor = System.Drawing.Color.Red;
            btnUpload.Enabled = false;
        }
        
    }

    public void btnUpload_Click(object sender, EventArgs e)
    {
        
//        btnUpload.PostBackUrl = "~/Forms/PromotionReportPCChair.aspx?applicationID=" + ApplicationID;
        if (ApplicationID != -1)
        {
//            Page.LoadControl("~/Controls/ViewPCChairReport.ascx");

            if (FileUploader.HasFile)
            {
                if (FileUploader.FileName.Length < 50)
                {
                    if (FileUploader.PostedFile.ContentType == "application/pdf"
                        || FileUploader.PostedFile.ContentType == "application/msword"
                        || FileUploader.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    {
                        try
                        {
                            FormTableAdapters.Form_PromotionReportTableAdapter fadapter = new FormTableAdapters.Form_PromotionReportTableAdapter();

                            //String FileName = "PCChairReport" + ApplicationID + ".pdf";

                            FileUploader.PostedFile.SaveAs(dataPath + ApplicationID.ToString() + FileUploader.FileName);
                            if (newInsert)
                            {
                                //fadapter.InsertPCChairReport(ApplicationID, FileName, txtDescription.Text);
                                fadapter.Insert(ApplicationID, ApplicationID.ToString() + FileUploader.FileName, txtDescription.Text);

                            }
                            else
                            {
                                Form.Form_PromotionReportRow row = fadapter.GetDataByPK(ApplicationID)[0];
                                fadapter.Update(row.FileName,txtDescription.Text, ApplicationID);
                            }

                            //fileID = int.Parse(adapter.InsertFile(ApplicationID, FileUploader.FileName, txtDescription.Text, RoleID).ToString());




                            txtDescription.Text = "";
                            Label1.Text = "File is Uploaded Successfully";
                            Label1.ForeColor = System.Drawing.Color.Blue;
                            //                        GridView grdFiles = ViewPCChairReport1.FindControl("grdFiles") as GridView;
                            //                        grdFiles.DataBind();                        
                            //                        btnUpload.Parent.Page.LoadControl("~/Controls/PCChairFileUpload.ascx");
                            //                        Page.LoadControl("~/Controls/ViewPCChairReport.ascx");
                            ViewPCChairReport2.Page_Load(null, null);

                            PromotionTableAdapters.PromotionCommitteeTempTableAdapter adapter = new PromotionTableAdapters.PromotionCommitteeTempTableAdapter();
                            Promotion.PromotionCommitteeTempDataTable pcTable = adapter.GetData(ApplicationID);
                            adapter.UpdateDS(false, null, pcTable[0].AutoNumberPK);
                            adapter.UpdateDS(false, null, pcTable[1].AutoNumberPK);
                            adapter.UpdateDS(false, null, pcTable[2].AutoNumberPK);
                            adapter.UpdateDS(false, null, pcTable[3].AutoNumberPK);
                            adapter.UpdateDS(false, null, pcTable[4].AutoNumberPK);



                        }
                        catch (Exception ex)
                        {
                            lblInfo.Text = "Error Occured! " + ex.ToString();
                            lblInfo.ForeColor = System.Drawing.Color.Red;
                            //adapter.Delete(fileID);
                        }
                    }
                }
                else
                {
                    //Alert1.Message("Filename cannot be larger than 50 characters.");
                    Label1.Text = "Filename cannot be larger than 50 characters.";
                    Label1.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                Label1.Text = "No file is specified. Please browse a file and then press upload.";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    public bool GetStatusByApplication(int appID)
    {
        return ViewPCChairReport2.GetStatusByApplication(appID);
    }
    //public void AlertMessage(string strMessage)
    //{
    //    string strScript = "<script language=JavaScript>";
    //    strScript += "alert('" + strMessage + "');";
    //    strScript += "</script>";
    //    if (!Page.IsStartupScriptRegistered("clientScript"))
    //        Page.RegisterStartupScript("clientScript", strScript);
    //}
}
