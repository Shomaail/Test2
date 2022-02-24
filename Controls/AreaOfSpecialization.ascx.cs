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
using BL.Data;

public partial class Controls_AreaOfSpecialization : System.Web.UI.UserControl
{
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {       

    }
    public void LoadAreaOfSp(int applicationID)
    {
        try
        {
            if(bal.GetForm_AppProperties(applicationID).Count == 0)
            {
                if(bal.GetForm_AreaOfSp(applicationID).Count == 0 )
                {
                    bal.InsertForm_AppProperties(applicationID, "", "", "", null, null);
                }
                else
                {
                    Form_AreaOfSp fas = bal.GetForm_AreaOfSp(applicationID)[0];
                    bal.InsertForm_AppProperties(applicationID, fas.AreaOfSpecialization, fas.PhdFrom, "", null, null);
                }
                

            }
            else
            {
                tbArOfSp.Text = bal.GetForm_AppProperties(applicationID)[0].AreaOfSpecialization;
                tbPhdFrom.Text = bal.GetForm_AppProperties(applicationID)[0].PlaceOfGraduation;
            }
            
            //Form.Form_AreaOfSpRow arOfSpRow;
            //if (Adapters.FrmArSpAdapter.GetDataByAppId(applicationID).Count == 0)
            //    return;
            //else
            //    arOfSpRow = Adapters.FrmArSpAdapter.GetDataByAppId(applicationID)[0];

            //tbArOfSp.Text = arOfSpRow.IsAreaOfSpecializationNull() ? "" : arOfSpRow.AreaOfSpecialization;
            //tbPhdFrom.Text = arOfSpRow.IsPhdFromNull() ? "" : arOfSpRow.PhdFrom;
        }
        catch
        {
          //  Response.Redirect("~/ErrorPage.aspx");
            //FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectToLoginPage();
        }
    }
    public bool SaveAreaOfSp(int applicationID)
    {
        if (tbArOfSp.Text == "" || tbPhdFrom.Text == "")
        {
            return false;
        }
        else
        {
            try
            {
                if (bal.GetForm_AppProperties(applicationID).Count == 0)
                {
                    bal.InsertForm_AppProperties(applicationID, tbArOfSp.Text,tbPhdFrom.Text, "", null, null);
                }
                else
                {
                    bal.UpdateForm_AppProperties(applicationID, tbArOfSp.Text, tbPhdFrom.Text, "", null, null);
                }

                //if (Adapters.FrmArSpAdapter.GetDataByAppId(applicationID).Count != 0)
                //{
                //    Adapters.FrmArSpAdapter.Delete(applicationID);
                //    Adapters.FrmArSpAdapter.Insert(applicationID, tbArOfSp.Text, tbPhdFrom.Text);
                //}
                //else
                //{
                //    Adapters.FrmArSpAdapter.Insert(applicationID, tbArOfSp.Text, tbPhdFrom.Text);
                //}
            }
            catch
            {
                return false;
            }
            return true;
        }
        
    }
}
