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

public partial class Forms_History : System.Web.UI.Page
{
    //public override string StyleSheetTheme
    //{
    //    get
    //    {
    //        return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack) return;

        if (Master.CurrentFormLevel == -1)
        {
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            return;
        }

        Instruction1.Text = Master.CurrentFormInstruction;

        History1.LoadHistory(Master.ApplicationID, Master.CurRoleID);
    }
}
