using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Alert : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    public void Message(string strMessage)
    {
        LabelMessage.Text = strMessage;
        string strScript = "<script language='javascript' src='../Controls/Independent Controls/Alert.js'></script>";
        //strScript += "function pageLoad() {        $find(\"mpe\").show();    }";
        //strScript += "</script>";
        if (!Page.ClientScript.IsStartupScriptRegistered("clientScript"))
            Page.ClientScript.RegisterStartupScript(this.GetType(), "clientScript", strScript);
     //   ScriptManager.RegisterStartupScript(HiddenButton, this.GetType(), "JS", strScript, false);
    }

}