using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Data;
public partial class Controls_ExternalReviewer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void LoadExternalReviewers(int Type)
    {
        odsFormExtRev.SelectParameters["Type"].DefaultValue = Type.ToString();
        gvExternalReviewers.DataBind();
    }
    protected void odsFormExtRev_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        int appID = int.Parse(Request.QueryString["applicationID"]);
        ExtRevBAL extRevBAL = new ExtRevBAL();
        foreach (Form_ExtRev fer in extRevBAL.GetFormExtRevByAppID(appID))
        {
            extRevBAL.DeleteFormExtRev(appID, fer.Type, fer.Serial);
            extRevBAL.InsertFormExtRev(appID, fer.Type, fer.Serial + 10 , fer.ExternalReviewerID);
        }
        int i = 1; 
        foreach (Form_ExtRev fer in extRevBAL.GetFormExtRevByAppID(appID))
        {
            extRevBAL.DeleteFormExtRev(appID, fer.Type, fer.Serial);
            extRevBAL.InsertFormExtRev(appID, fer.Type, i++, fer.ExternalReviewerID);
        }
        gvExternalReviewers.DataBind();
    }
    protected void gvExternalReviewers_SelectedIndexChanged(object sender, EventArgs e)
    {
        

    }
}