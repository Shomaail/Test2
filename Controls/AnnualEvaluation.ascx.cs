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

public partial class Controls_AnnualEvaluation : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadEvaluation(int applicationID)
    {
        PromotionTableAdapters.ServiceYearsTableAdapter adapter = new PromotionTableAdapters.ServiceYearsTableAdapter();
        Promotion.ServiceYearsDataTable table = adapter.GetServiceYearsByApplication(applicationID);
        if (table.Count == 0) return;

        Promotion.ServiceYearsRow row = table[0];
        txtEvaluations.Text = row.IsEvaluationsNull() ? "" : row.Evaluations;
    }

    public void SetReadonly(bool status)
    {
        txtEvaluations.ReadOnly = status;
    }


    public bool SaveEvaluation(int applicationID)
    {
        PromotionTableAdapters.ServiceYearsTableAdapter adapter = new PromotionTableAdapters.ServiceYearsTableAdapter();
        Promotion.ServiceYearsDataTable table = adapter.GetServiceYearsByApplication(applicationID);
        try
        {
            if (table.Count == 0)
            {
                if (txtEvaluations.Text != "")
                {
                    adapter.InsertQuery(
                            applicationID,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            txtEvaluations.Text);

                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (txtEvaluations.Text != "")
                {
                    table[0].Evaluations = txtEvaluations.Text;
                    adapter.Update(table);
                }
                else
                {
                    return false;
                }                
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
