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
/// Summary description for FormSuccessFailure
/// </summary>
public abstract class FormSuccessFailure : System.Web.UI.Page
{
	public FormSuccessFailure()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public abstract void ShowSuccess();

    public abstract void ShowFailure(string meassage);


}
