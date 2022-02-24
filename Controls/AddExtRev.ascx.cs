using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Controls_AddExtRev : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Form.Form_RefreeDataTable rfrDT = Adapters.FrmRefAdapter.GetByApplication(ApplicationID,_Type);
        if (rfrDT.Count < 10)
        {
            GridViewRow row = GridView1.SelectedRow;
            Adapters.FrmRefAdapter.Insert(ApplicationID, _Type, Convert.ToByte(rfrDT.Count)
                , row.Cells[2].Text
                , row.Cells[3].Text
                , row.Cells[4].Text
                , row.Cells[5].Text
                , row.Cells[6].Text
                , row.Cells[7].Text
                , row.Cells[8].Text
                , row.Cells[9].Text
                , row.Cells[10].Text
                , row.Cells[11].Text
                , row.Cells[12].Text
                , Int32.Parse(row.Cells[13].Text)
                , Int32.Parse(row.Cells[14].Text)
                , Int32.Parse(row.Cells[15].Text)
                , Int32.Parse(row.Cells[16].Text));
            Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);

        }
        else
            Alert1.Message("You cannot select more than 10 External Reviewers");
    }
    public void LoadControl(int applicationID, byte type, string dept)
    {
        ApplicationID = applicationID;
        _Type = type;
        AppDepartment = dept;
        
    }
    public int ApplicationID
    {
        get
        {
            if (hdnApplicationID.Value.Length == 0)
                return -1;
            return int.Parse(hdnApplicationID.Value);
        }
        set
        {
            hdnApplicationID.Value = value.ToString();
        }
    }


    public byte _Type
    {
        get
        {
            if (hdnType.Value.Length == 0)
                return 0;
            return byte.Parse(hdnType.Value);
        }
        set
        {
            hdnType.Value = value.ToString();
        }
    }
    public string AppDepartment
    {
        get
        {
            if (hdnAppDepartment.Value.Length == 0)
                return "";
            return (hdnAppDepartment.Value);
        }
        set
        {
            hdnAppDepartment.Value = value.ToString();
        }
    }
    
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlDataSource1.SelectParameters["Department"].DefaultValue = AppDepartment;
        GridView1.DataBind();
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
