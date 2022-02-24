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

public partial class Controls_ChecklistForm : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
       
    }

    public int ApplicationID
    {
        set
        {
            ViewState["ApplicationID"] = value;
            ObjectDataSource_CheckList.DataBind();
        }
        get
        {
            if (ViewState["ApplicationID"] != null)
                return (int)ViewState["ApplicationID"];
            else
                return -1;
        }
    }
    protected void ObjectDataSource_CheckList_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        int appId = int.Parse(Request.Params.Get("applicationID").ToString());
        e.InputParameters[0] = appId;

    }
    protected void ObjectDataSource_CheckList_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        int appId = int.Parse(Request.Params.Get("applicationID").ToString());
        e.InputParameters[0] = appId;
    }

    public void ActivateEditMode()
    {
        FormView1.DefaultMode = FormViewMode.Edit;
        FormView1.Visible = true;
    }
    public bool update()
    {
        int appId = int.Parse(Request.Params.Get("applicationID").ToString());

        ApplicationCheckListTableAdapters.ApplicationChecklistTableAdapter appChkLstAdapter = new ApplicationCheckListTableAdapters.ApplicationChecklistTableAdapter();
        if (appChkLstAdapter.GetDataByApplicationID(appId).Count == 0) 
        { 
            return false; 
        }
        string text1 = "";
        string text2 = "";
        string text3 = "";
        string text4 = "";
        string text5 = "";
        string text6 = "";
        string text7 = "";
        string text8 = "";
        string overAll = "";

        bool el1;
        bool el2;
        bool el3;
        bool el4;
        bool el5;
        bool el6;
        bool el7;
        bool el8;
        bool oAel;

        TextBox txt1 = (TextBox)FormView1.FindControl("Field1CommentsTextBox");
        TextBox txt2 = (TextBox)FormView1.FindControl("Field2CommentsTextBox");
        TextBox txt3 = (TextBox)FormView1.FindControl("Field3CommentsTextBox");
        TextBox txt4 = (TextBox)FormView1.FindControl("Field4CommentsTextBox");
        TextBox txt5 = (TextBox)FormView1.FindControl("Field5CommentsTextBox");
        TextBox txt6 = (TextBox)FormView1.FindControl("Field6CommentsTextBox");
        TextBox txt7 = (TextBox)FormView1.FindControl("Field7CommentsTextBox");
        TextBox txt8 = (TextBox)FormView1.FindControl("Field8CommentsTextBox");
        TextBox oAtext = (TextBox)FormView1.FindControl("RemarksTextBox");

        CheckBox eligibility1 = (CheckBox)FormView1.FindControl("Field1StatusCheckBox");
        CheckBox eligibility2 = (CheckBox)FormView1.FindControl("Field2StatusCheckBox");
        CheckBox eligibility3 = (CheckBox)FormView1.FindControl("Field3StatusCheckBox");
        CheckBox eligibility4 = (CheckBox)FormView1.FindControl("Field4StatusCheckBox");
        CheckBox eligibility5 = (CheckBox)FormView1.FindControl("Field5StatusCheckBox");
        CheckBox eligibility6 = (CheckBox)FormView1.FindControl("Field6StatusCheckBox");
        CheckBox eligibility7 = (CheckBox)FormView1.FindControl("Field7StatusCheckBox");
        CheckBox eligibility8 = (CheckBox)FormView1.FindControl("Field8StatusCheckBox");
        CheckBox overAllEl = (CheckBox)FormView1.FindControl("OverallStatusCheckBox");
        
        el1 = eligibility1.Checked;
        el2 = eligibility2.Checked;
        el3 = eligibility3.Checked;
        el4 = eligibility4.Checked;
        el5 = eligibility5.Checked;
        el6 = eligibility6.Checked;
        el7 = eligibility7.Checked;
        el8 = eligibility8.Checked;
        oAel = overAllEl.Checked;

        if (txt1.Text != null && txt1.Text != "")
            text1 = txt1.Text;
        if (txt2.Text != null && txt2.Text != "")
            text2 = txt2.Text;
        if (txt3.Text != null && txt3.Text != "")
            text3 = txt3.Text;
        if (txt4.Text != null && txt4.Text != "")
            text4 = txt4.Text;
        if (txt5.Text != null && txt5.Text != "")
            text5 = txt5.Text;
        if (txt6.Text != null && txt6.Text != "")
            text6 = txt6.Text;
        if (txt7.Text != null && txt7.Text != "")
            text7 = txt7.Text;
        if (txt8.Text != null && txt8.Text != "")
            text8 = txt8.Text;
        if (oAtext.Text != null && oAtext.Text != "")
            overAll = oAtext.Text;

        appChkLstAdapter.UpdateQuery(el1, text1, el2, text2, el3, text3, el4, text4, el5, text5, el6, text6, el7, text7, el8, text8, oAel, overAll, appId, appId);
//        appChkLstAdapter.UpdateQuery(appId,FormView1.FindControl(
        return true;
    }

}
