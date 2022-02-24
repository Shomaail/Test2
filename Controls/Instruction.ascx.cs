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

public partial class Controls_Instruction : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["langCookie"].Value == "ar")
                lblInstructionHead.Text = "  تليمات  ";
            else
                lblInstructionHead.Text = "Instructions";
        }
        catch (Exception)
        {
            lblInstructionHead.Text = "Instructions";
        }
            

        //if (Profile.Language == "")
        //   Profile.Language = "en";       
        //if(Profile.Language.ToString() == "en")
        //    lblInstructionHead.Text = "Instructions";
        //else
        //    lblInstructionHead.Text = "  تليمات  ";
    }
    public string Text
    {
        set
        {
            if (string.IsNullOrEmpty(value))
                this.Visible = false;
            else
                lblInstructions.Text = value;
        }
    }
}
