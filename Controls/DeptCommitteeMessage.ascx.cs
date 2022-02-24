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

public partial class Controls_DeptCommitteeMessage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // UpdateOptions();
    }
    protected void rdo1_CheckedChanged(object sender, EventArgs e)
    {
        chkR1C1.Enabled = rdo1.Checked;
        chkR1C2.Enabled = rdo1.Checked;
        chkR1C3.Enabled = rdo1.Checked;

        chkR2C1.Enabled = rdo2.Checked;
        chkR2C2.Enabled = rdo2.Checked;
        chkR2C3.Enabled = rdo2.Checked;

        chkR1C1.Checked = rdo1.Checked;
        chkR1C2.Checked = rdo1.Checked;
        chkR1C3.Checked = rdo1.Checked;

        chkR2C1.Checked = rdo2.Checked;
        chkR2C2.Checked = rdo2.Checked;
        chkR2C3.Checked = rdo2.Checked;

        //ViewState["IsTaskComplete"] = rdo2.Checked;
        if (!(Parent.Parent.Parent.Parent.Parent.FindControl("btnSubmit") as Button).Enabled)
            (Parent.Parent.Parent.Parent.Parent.FindControl("btnSubmit") as Button).Enabled = rdo2.Checked || rdo3.Checked || rdo4.Checked;
        //(Parent.Parent.Parent.Parent.Parent.FindControl("btnSubmit") as Button).Enabled = rdo1.Checked || rdo2.Checked || rdo3.Checked || rdo4.Checked;

    }

    private void UpdateOptions()
    {
        chkR1C1.Enabled = rdo1.Checked;
        chkR1C2.Enabled = rdo1.Checked;
        chkR1C3.Enabled = rdo1.Checked;

        chkR2C1.Enabled = rdo2.Checked;
        chkR2C2.Enabled = rdo2.Checked;
        chkR2C3.Enabled = rdo2.Checked;

        chkR1C1.Checked = rdo1.Checked;
        chkR1C2.Checked = rdo1.Checked;
        chkR1C3.Checked = rdo1.Checked;

        chkR2C1.Checked = rdo2.Checked;
        chkR2C2.Checked = rdo2.Checked;
        chkR2C3.Checked = rdo2.Checked;
        //ViewState["IsTaskComplete"] = rdo2.Checked;
        (Parent.Parent.Parent.Parent.Parent.FindControl("btnSubmit") as Button).Enabled = rdo2.Checked || rdo3.Checked || rdo4.Checked;
    }

    public string Text
    {
        set
        {
            const string del = "@@body@@";
            int index = value.IndexOf(del);
            if (index < 0) return;
            lblHeader.Text = value.Substring(0, index);
            lblFooter.Text = value.Substring(index + del.Length);
        }
    }


    public string GetMessage()
    {
        const string CRLF = "\r\n";
        const string TAB = "    ";

        string message = "";

        message = lblHeader.Text + CRLF;

        if (rdo1.Checked)
        {
            message += rdo1.Text + "\r\n";
            if (chkR1C1.Checked) message += TAB + chkR1C1.Text + CRLF;
            if (chkR1C2.Checked) message += TAB + chkR1C2.Text + CRLF;
            if (chkR1C3.Checked) message += TAB + chkR1C3.Text + CRLF;

        }
        else if (rdo2.Checked)
        {
            message += rdo2.Text + "\r\n";
            if (chkR2C1.Checked) message += TAB + chkR2C1.Text + CRLF;
            if (chkR2C2.Checked) message += TAB + chkR2C2.Text + CRLF;
            if (chkR2C3.Checked) message += TAB + chkR2C3.Text + CRLF;
        }
        else if (rdo3.Checked)
        {
            message += rdo3.Text + CRLF;
        }
        else if (rdo4.Checked)
        {
            message += rdo4.Text + CRLF;
        }
        if (txtAdditionalComments.Text != "")
        {
            message += CRLF + CRLF + "-Additional Comments:" + CRLF;
            message += txtAdditionalComments.Text + CRLF;
        }

        message += CRLF + lblFooter.Text + CRLF;

        return message;
    }
   
}
