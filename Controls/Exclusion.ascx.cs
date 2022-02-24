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

public partial class Controls_Exclusion : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void LoadExclusion(int applicationID)
    {
        PromotionTableAdapters.ApplicationTableAdapter adapter = new PromotionTableAdapters.ApplicationTableAdapter();
        Promotion.ApplicationDataTable table = adapter.GetApplication(applicationID);
        if (table.Count == 0) return;

        Promotion.ApplicationRow row = table[0];
        rdoYes.Checked = row.IsExclusionListNull() ? false : row.ExclusionList;
        rdoNo .Checked = row.IsExclusionListNull() ? false : !row.ExclusionList;
    }

    public void SetReadonly(bool status)
    {
        rdoYes.Enabled = !status;
        rdoNo .Enabled = !status;
    }

    public bool SaveExclusion(int applicationID)
    {
        PromotionTableAdapters.ApplicationTableAdapter adapter = new PromotionTableAdapters.ApplicationTableAdapter();
        Promotion.ApplicationDataTable table = adapter.GetApplication(applicationID);
        if (table.Count == 0) return false;
        
        Promotion.ApplicationRow row = table[0];
        
        try
        {
            if (!rdoYes.Checked && !rdoNo.Checked)
                row.SetExclusionListNull();
            else
                row.ExclusionList = rdoYes.Checked;

            adapter.Update(table);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
