using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using BL.Data;
using System.Collections;

public partial class Admin_SentEmails : System.Web.UI.Page
    {
        ExtRevBAL erbal = new ExtRevBAL();
        BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        PageSize = 100;
        DatabindControls();

    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }
    private void DatabindControls()
    {
        PagedDataSource pgitems = new PagedDataSource();
        pgitems.DataSource = bal.GetSentEmail();
        pgitems.AllowPaging = true;
        pgitems.PageSize = PageSize;
        pgitems.CurrentPageIndex = PageNumber;
        if (pgitems.PageCount > 1)
        {
            rptPaging.Visible = true;
            ArrayList pages = new ArrayList();
            for (int i = 0; i <= pgitems.PageCount - 1; i++)
            {
                pages.Add((i + 1).ToString());
            }
            rptPaging.DataSource = pages;
            rptPaging.DataBind();
        }
        else
        {
            rptPaging.Visible = false;
        }
        rpSentEmail.DataSource = pgitems;
        rpSentEmail.DataBind();
        //            rpSentEmail.DataSource = bal.GetSentEmail();
        //rpSentEmail.DataBind();
    }
    protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
        DatabindControls();
    }
    protected void rpSentEmail_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        int ID = int.Parse(e.CommandArgument.ToString());

        if (e.CommandName == "Delete")
        {
            bal.DeleteSentEmail(ID);
            DatabindControls();
        }
        else if (e.CommandName == "Page")
        {
            PageNumber = ID - 1;
            DatabindControls();
        }

    }
    #region Properties
    public int PageNumber
    {
        get
        {
            if (ViewState["PageNumber"] != null)
            {
                return Convert.ToInt32(ViewState["PageNumber"]);
            }
            else
            {
                return 0;
            }
        }
        set { ViewState["PageNumber"] = value; }
    }
    public int PageSize
    {
        get
        {
            if (ViewState["PageSize"] != null)
            {
                return Convert.ToInt32(ViewState["PageSize"]);
            }
            else
            {
                return 0;
            }
        }
        set { ViewState["PageSize"] = value; }
    }
    #endregion
}
