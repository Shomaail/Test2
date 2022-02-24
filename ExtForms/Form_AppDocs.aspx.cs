using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using BL.Data;
public partial class ExtForms_Form_AppDocs : System.Web.UI.Page
{
    BAL bal = new BAL();
    private string dataPath = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        gvForm_Attachment.DataSource = bal.GetForm_AttachmentAdByAppID(Master.ApplicationID).Where(fa => fa.SelectionForExtRev.Value);
        gvForm_Attachment.DataBind();

        //dataPath = Server.MapPath("~\\App_LocalResources") + "\\";
        //gvApplication_Attachments.DataSource = bal.GetApplication_Attachments(Master.ApplicationID, (byte)RoleID.External_Reviewer);
        //gvApplication_Attachments.DataBind();
    }
    //protected string GetEmployeeName()
    //{
    //    return bal.GetEmployeeByPK(Eval("EmployeeID").ToString())[0].NameString;
    //}
    //protected void gvApplication_Attachments_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    List<Form_Attachment> ld = bal.GetForm_Attachment(int.Parse(gvApplication_Attachments.SelectedDataKey.Value.ToString()));
    //    if (ld.Count == 0) { return; }
    //    Form_Attachment attachment = ld[0];
    //    Response.Redirect("NoMasterPage\\Handler.ashx?appID=" + Master.ApplicationID + "&empID=" + bal.GetApplicant(Master.ApplicationID)[0].EmployeeID + "&fileName=" + attachment.DocumentName);
    //}
    #region Event Handler
    protected void lbtnDownload_PreRender(object sender, EventArgs e)
    {
        int rowID = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;
        int faID = int.Parse(gvForm_Attachment.DataKeys[rowID].Value.ToString());
        List<Form_AttachmentAd> ld = bal.GetForm_AttachmentAd(faID);
        if (ld.Count == 0 || ld[0].DocumentName == "") { return; }
        Form_AttachmentAd attachment = ld[0];
        (sender as LinkButton).OnClientClick = "window.open('"
            + "../Forms/NoMasterPage/Handler.ashx?appID=" + Master.ApplicationID
            + "&empID="
            + attachment.EmployeeID
            + "&fileName=" + attachment.DocumentName + "'); return false ;";
    }
    #endregion

}