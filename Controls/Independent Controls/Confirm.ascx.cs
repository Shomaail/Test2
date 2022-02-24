using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Independent_Controls_Confirm : System.Web.UI.UserControl
{
    bool decision;

    public bool Decision
    {
        get { return decision; }
        set { decision = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       

    }
    public void ConfirmMessage(string Message)
    {
        LabelMessageConfirm.Text = Message;
        
        ModalPopupExtenderConfirm.Show();        
    }
    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        Decision = true;
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Decision = false;
    }
}