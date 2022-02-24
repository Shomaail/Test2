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

public partial class Controls_ChairmanInput : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void LoadComments(int applicationID)
    {
        FormTableAdapters.Form_ChairmanInputTableAdapter adapter = new FormTableAdapters.Form_ChairmanInputTableAdapter();
        Form.Form_ChairmanInputDataTable table = adapter.GetDataByAppID(applicationID);
        if (table.Count == 0) return;

        Form.Form_ChairmanInputRow row = table[0];
        txtTeaching.Text = row.IsTeachingNull()? "": row.Teaching;
        txtResearch.Text = row.IsResearchNull()? "": row.Research;
        txtCommunityService.Text = row.IsCommunityServiceNull()? "": row.CommunityService;
        txtComments.Text = row.IsCommentsAndRecommendationNull()? "": row.CommentsAndRecommendation;
        tbDeptCouncilMeetingNo.Text = row.IsMeetingNoNull() ? "" : row.MeetingNo;
        tbDeptCouncilMeetingDate.Text = row.IsMeetingDateNull() ? "" : row.MeetingDate.ToString();

        LabelTeaching.Text = row.IsTeachingNull() ? "" : row.Teaching.Replace("\n", "<BR>").Replace("\t",
"&nbsp;&nbsp;&nbsp;&nbsp;");
        LabelResearch.Text = row.IsResearchNull() ? "" : row.Research.Replace("\n", "<BR>").Replace("\t",
"&nbsp;&nbsp;&nbsp;&nbsp;");
        LabelCommunityService.Text = row.IsCommunityServiceNull() ? "" : row.CommunityService.Replace("\n", "<BR>").Replace("\t",
"&nbsp;&nbsp;&nbsp;&nbsp;");
        LabelComments.Text = row.IsCommentsAndRecommendationNull() ? "" : row.CommentsAndRecommendation.Replace("\n", "<BR>").Replace("\t",
"&nbsp;&nbsp;&nbsp;&nbsp;");


    }
    public void SetPrinterFriendly(bool status)
    {

        txtTeaching.Visible = !status;
        txtResearch.Visible = !status;
        txtCommunityService.Visible = !status;
        txtComments.Visible = !status;

        LabelTeaching.Visible = status;
        LabelResearch.Visible = status;
        LabelCommunityService.Visible = status;
        LabelComments.Visible = status;
    }
    public void SetReadonly(bool status)
    {
        txtTeaching.ReadOnly = status;
        txtResearch.ReadOnly = status;
        txtCommunityService.ReadOnly = status;
        txtComments.ReadOnly = status;
    }


    public bool SaveComments(int applicationID)
    {
        FormTableAdapters.Form_ChairmanInputTableAdapter adapter = new FormTableAdapters.Form_ChairmanInputTableAdapter();
        

        try
        {
            if (txtTeaching.Text == ""
                || txtResearch.Text == ""
                || txtCommunityService.Text == ""
                || txtComments.Text == ""
                || tbDeptCouncilMeetingNo.Text == ""
                || tbDeptCouncilMeetingDate.Text == "")
            {
                adapter.DeleteByAppID(applicationID);
                adapter.Insert(applicationID, txtTeaching.Text, txtResearch.Text, txtCommunityService.Text, txtComments.Text
                    ,tbDeptCouncilMeetingNo.Text, tbDeptCouncilMeetingDate.Text == "" ? (DateTime?) null : DateTime.Parse(tbDeptCouncilMeetingDate.Text));

                return false;
            }
            else
            {
                adapter.DeleteByAppID(applicationID);
                adapter.Insert(applicationID, txtTeaching.Text, txtResearch.Text, txtCommunityService.Text, txtComments.Text
                    , tbDeptCouncilMeetingNo.Text, tbDeptCouncilMeetingDate.Text == "" ? (DateTime?) null : DateTime.Parse(tbDeptCouncilMeetingDate.Text));
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
    protected void txtTeaching_TextChanged(object sender, EventArgs e)
    {
        
        SaveComments(int.Parse(Request.Params.Get("applicationID").ToString()));
    }
    protected void txtResearch_TextChanged(object sender, EventArgs e)
    {
        SaveComments(int.Parse(Request.Params.Get("applicationID").ToString()));
    }
    protected void txtCommunityService_TextChanged(object sender, EventArgs e)
    {
        SaveComments(int.Parse(Request.Params.Get("applicationID").ToString()));
    }
    protected void txtComments_TextChanged(object sender, EventArgs e)
    {
        SaveComments(int.Parse(Request.Params.Get("applicationID").ToString()));
    }
    protected void tbDeptCouncilMeetingDate_TextChanged(object sender, EventArgs e)
    {
        SaveComments(int.Parse(Request.Params.Get("applicationID").ToString()));
    }
    protected void tbDeptCouncilMeetingNo_TextChanged(object sender, EventArgs e)
    {
        SaveComments(int.Parse(Request.Params.Get("applicationID").ToString()));
    }
}