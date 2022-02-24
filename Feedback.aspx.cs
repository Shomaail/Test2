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
using BL.Data;
public partial class Feedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        //ButtonSendFB.Attributes.Add("OnClick", "return Confirm_Post();");
        //LabelMessageConfirm.Text = "Are you sure you want to post this message?";
        if (Session["IsDeputy"] != null)
            lblDeputy.Visible = (bool)Session["IsDeputy"];
        PanelFB.Visible = IsAdmin();
        PanelFeedbackUser.Visible = !IsAdmin();
        Employee emp = new Employee();
        if (Session["user"] != null)
        {
            emp = Session["user"] as Employee;
            lblEmpID.Text = emp.EmployeeID;
            lblName.Text = emp.Name;
            lblEmail.Text = emp.Email;
            lblRank.Text = emp.Rank;          
        }        
    }
    protected void ButtonSendFB_Click(object sender, EventArgs e)
    {
        Confirm1.ConfirmMessage("Do you ?");
        Employee emp = new Employee();
        if (Confirm1.Decision)
        {
            emp = Session["user"] as Employee;
            Adapters.FrmFBAdapter.Insert(emp.Title + emp.Name, emp.Department1.Name , emp.Email, emp.Phone, TextBoxFeedback.Text, DateTime.Now);
            GridViewAdminFB.DataBind();
            Alert1.Message("Your Feedback is submitted successfully. Thank you");
            TextBoxFeedback.Text = "";
        }
        

    }

    public void ButtonOK_Click(object sender, EventArgs e)
    {
        GridViewAdminFB.Visible = true;
        DetailsView1.Visible = false;
    }

    protected void GridViewAdminFB_SelectedIndexChanged(object sender, EventArgs e)
    {
        DetailsView1.Visible = true;
        GridViewAdminFB.Visible = false;
        ObjectDataSourceFBDT.SelectParameters["ID"].DefaultValue = GridViewAdminFB.SelectedValue.ToString();    
        //DetailsView1.PageIndex = GridViewAdminFB.SelectedIndex;
        DetailsView1.DataBind();
    }
    public bool CheckVisibility(string Name)
    {
        Employee emp = new Employee();
        if (Session["user"] != null)
        {
            emp = Session["user"] as Employee;
            if (("Dr. " + emp.Name) == Name)
                return true;
            else
                return false;
        }
        else { return false; }
    }
    public bool IsAdmin()
    {
        return (bool)(Session["IsAdmin"] != null) && ((bool)Session["IsAdmin"] == true);
    }
    
}
