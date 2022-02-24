using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Data;

public partial class Forms_NoMasterPage_ExtRevList4TopAuthority : System.Web.UI.Page
{
    BAL bal = new BAL();
    ExtRevBAL erBAL = new ExtRevBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        int appId = int.Parse(Request.Params.Get("applicationID").ToString());
        lblApplicantEmail.Text = bal.GetApplicant(appId)[0].NameString + "(" + bal.GetApplicant(appId)[0].Department1.ShortName + ")";
        // lblSpeciality.Text = Adapters.FrmArSpAdapter.GetDataByAppId(appId)[0].AreaOfSpecialization;
        lblSpeciality.Text = bal.GetForm_AppProperties(appId).Count > 0 ? bal.GetForm_AppProperties(appId)[0].AreaOfSpecialization : "";
        //lblPhdFrom.Text = Adapters.FrmArSpAdapter.GetDataByAppId(appId)[0].IsPhdFromNull() ? "" : Adapters.FrmArSpAdapter.GetDataByAppId(appId)[0].PhdFrom;
        lblPhdFrom.Text = bal.GetForm_AppProperties(appId).Count > 0 ? bal.GetForm_AppProperties(appId)[0].PlaceOfGraduation: "";
        lblToRank.Text = bal.GetApplication(appId)[0].ForRank;
        gvSugExtRevList.DataSource = erBAL.GetFormExtRevByAppID(appId);
        gvSugExtRevList.DataBind();
        //gvExtRevList4VR.DataSource = erBAL.GetForm_FinalExtRev(appId);
        gvExtRevList4VR.DataSource = erBAL.GetForm_FinalExtRev(appId)
            .OrderByDescending(a => a.ExternalReviewer.HIndex.HasValue && a.ExternalReviewer.NoOfJournals.HasValue ?
            a.ExternalReviewer.HIndex.Value + a.ExternalReviewer.NoOfJournals.Value : a.Serial);
        gvExtRevList4VR.DataBind();
    }
    
    public string GetSuggestedBy(int Type)
    {
        string value = Type.ToString();
        switch (value)
        {
            case "0":
                return "Applicant";
            case "1":
                return "Department";
            case "2":
                return "College";
            case "3":
                return "VPRI";
            default:
                return "SCSubCom";
        }
    }
}