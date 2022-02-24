using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Utils
{
	public Utils()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool IsPrintMode()
    {
        string printFlag = HttpContext.Current.Request.QueryString["print"];
        return printFlag == "1";
    }

    public const string PrinterStyleSheet = "Print";
}
