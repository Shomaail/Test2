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
using System.Text.RegularExpressions;
using System.Drawing;

public partial class Controls_RefreeInput : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string Ref_Name
    {
        get { return txtName.Text; }
        set { txtName.Text = value; }
    }
    public string Ref_Rank
    {
        get {
            if (ddlRank.SelectedIndex < ddlRank.Items.Count-1)
                return ddlRank.SelectedValue;
            else
                return txtRank.Text;
        }
        set {
            if (ddlRank.Items.FindByValue(value) != null)
                ddlRank.SelectedValue = value;
            else
            {
                ddlRank.SelectedIndex = ddlRank.Items.Count - 1;
                txtRank.Text = value;
                pnlRank.Visible = true;
            }
        }
    }
    public string Ref_MailingAddress
    {
        get { return txtMailAddress.Text; }
        set { txtMailAddress.Text = value; }
    }
    public string Ref_PhoneAndFax
    {
        get { return txtPhoneAndFax.Text; }
        set { txtPhoneAndFax.Text = value; }
    }
    public string Ref_EMail
    {
        get {

            txtEmail.Text = Regex.Replace(txtEmail.Text, "\\s", "");
            return txtEmail.Text; }
        set 
        {            
            txtEmail.Text = value;
            txtEmail.Text = Regex.Replace(txtEmail.Text, "\\s", "");
        }
    }
    public string Ref_Major
    {
        get { return txtMajor.Text; }
        set { txtMajor.Text = value; }
    }
    public string Ref_Speciality
    {
        get { return txtSpeciality.Text; }
        set { txtSpeciality.Text = value; }
    }
    public string Ref_ActiveAreaOfResearch
    {
        get { return txtActiveAreaOfResearch.Text; }
        set { txtActiveAreaOfResearch.Text = value; }
    }
    public string Ref_Webpage
    {
        get { return txtWebpage.Text; }
        set { txtWebpage.Text = value; }
    }
    public string Ref_Comments
    {
        get { return txtComments.Text; }
        set { txtComments.Text = value; }
    }
    public int TotalPublications
    {
        get {return Int32.Parse(TextBoxTotPublications.Text == "" ? "0" : TextBoxTotPublications.Text);}
        set { TextBoxTotPublications.Text = value.ToString(); }
    }
    public int NoOfJournals
    {
        get { return Int32.Parse(TextBoxNoOfJournals.Text == "" ? "0" : TextBoxNoOfJournals.Text); }
        set { TextBoxNoOfJournals.Text = value.ToString(); }
    }
    public int HIndex
    {
        get { return Int32.Parse(TextBoxHIndex.Text == "" ? "0" : TextBoxHIndex.Text); }
        set { TextBoxHIndex.Text = value.ToString(); ; }
    }
    public int Citations
    {
        get { return Int32.Parse(TextBoxCitations.Text == "" ? "0" : TextBoxCitations.Text); }
        set { TextBoxCitations.Text = value.ToString(); ; }
    }



    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRank.SelectedIndex == ddlRank.Items.Count - 1)
        {
            pnlRank.Visible = true;
        }
        else
        {
            pnlRank.Visible = false;
        }
    }
    protected void txtMailAddress_TextChanged(object sender, EventArgs e)
    {
        string ma = txtMailAddress.Text.ToLower();
        if (             !ma.Contains("univ") && 
                         !ma.Contains("üniversite") &&  !ma.Contains("yliopisto") &&  
                         !ma.Contains("uniwersytet") && !ma.Contains("tech") &&  !ma.Contains("institute") &&
                          !ma.Contains("iit") && !ma.Contains("mit") && !ma.Contains("uoit") && !ma.Contains("school") &&  
                         !ma.Contains("polytechnique") && !ma.Contains("ecole") &&  !ma.Contains("جامعة") &&  
                         !ma.Contains("الأكاديمية") &&  !ma.Contains("imperial college"))
        { txtMailAddress.BackColor = Color.Red; }
        else
        { txtMailAddress.BackColor = Color.White; }
    }
}
