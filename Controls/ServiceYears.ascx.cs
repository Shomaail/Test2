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

public partial class Controls_ServiceYears : System.Web.UI.UserControl
{
    public enum Mode { View, InputByApplicant, InputByFacultyAffairs }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SwitchMode(Mode mode)
    {
        trKFUPMYears.Visible = false;
        trSaudiYears.Visible = false;
        trVerfKFUPMYears.Visible = false;
        trVerfSaudiYears.Visible = false;

        switch (mode)
        {
            case Mode.View:
                txtYears            .ReadOnly = true;
                txtMonths           .ReadOnly = true;
                txtSaudiYears       .ReadOnly = true;
                txtSaudiMonths      .ReadOnly = true;
                txtVerfYears        .ReadOnly = true;
                txtVerfMonths       .ReadOnly = true;
                txtVerfSaudiYears   .ReadOnly = true;
                txtVerfSaudiMonths  .ReadOnly = true;

                trKFUPMYears    .Visible = true;
                trSaudiYears    .Visible = true;
                trVerfKFUPMYears.Visible = true;
                trVerfSaudiYears.Visible = true;

                if (txtYears      .Text  != txtVerfYears.Text ||
                    txtMonths     .Text  != txtVerfMonths.Text ||
                    txtSaudiYears .Text  != txtVerfSaudiYears.Text ||
                    txtSaudiMonths.Text  != txtVerfSaudiMonths.Text)
                {
                    if (trKFUPMYears.Visible == trVerfKFUPMYears.Visible)
                    {
                        lblDifferent.Visible = true;
                    }

                }

                break;

            case Mode.InputByApplicant:
                txtYears            .ReadOnly = false;
                txtMonths           .ReadOnly = false;
                txtSaudiYears       .ReadOnly = false;
                txtSaudiMonths      .ReadOnly = false;
                txtVerfYears        .ReadOnly = true;
                txtVerfMonths       .ReadOnly = true;
                txtVerfSaudiYears   .ReadOnly = true;
                txtVerfSaudiMonths  .ReadOnly = true;

                trKFUPMYears    .Visible = true;
                trSaudiYears    .Visible = true;
                break;

            case Mode.InputByFacultyAffairs:
                txtYears            .ReadOnly = true;
                txtMonths           .ReadOnly = true;
                txtSaudiYears       .ReadOnly = true;
                txtSaudiMonths      .ReadOnly = true;
                txtVerfYears        .ReadOnly = false;
                txtVerfMonths       .ReadOnly = false;
                txtVerfSaudiYears   .ReadOnly = false;
                txtVerfSaudiMonths  .ReadOnly = false;

                trVerfKFUPMYears.Visible = true;
                trVerfSaudiYears.Visible = true;
                break;
        }
    }

    public bool LoadServiceYears(int applicationID, Mode mode)
    {

        PromotionTableAdapters.ServiceYearsTableAdapter adapter = new PromotionTableAdapters.ServiceYearsTableAdapter();

        Promotion.ServiceYearsDataTable table = adapter.GetServiceYearsByApplication(applicationID);
        if (table.Count == 0)
        {
            SwitchMode(mode);
            return false;
        }

        Promotion.ServiceYearsRow row = table[0];

        txtYears            .Text = row.IsApp_KFUPM_NumYearsNull()?   "0":row.App_KFUPM_NumYears.ToString();
        txtMonths           .Text = row.IsApp_KFUPM_NumMonthsNull()?  "0":row.App_KFUPM_NumMonths.ToString();
        txtSaudiYears       .Text = row.IsApp_Saudi_NumYearsNull()?   "0":row.App_Saudi_NumYears.ToString();
        txtSaudiMonths      .Text = row.IsApp_Saudi_NumMonthsNull()?  "0":row.App_Saudi_NumMonths.ToString();
        txtVerfYears        .Text = row.IsKFUPM_NumYearsNull()?       "0":row.KFUPM_NumYears.ToString();    
        txtVerfMonths       .Text = row.IsKFUPM_NumMonthsNull()?      "0":row.KFUPM_NumMonths.ToString(); 
        txtVerfSaudiYears   .Text = row.IsSaudi_NumYearsNull()?       "0":row.Saudi_NumYears.ToString();
        txtVerfSaudiMonths  .Text = row.IsSaudi_NumMonthsNull()?      "0":row.Saudi_NumMonths.ToString();

        if (row.IsKFUPM_NumYearsNull() &&
            mode != Mode.InputByFacultyAffairs)
        {
            trVerfKFUPMYears.Visible = false;
            trVerfSaudiYears.Visible = false;
        }
        
        SwitchMode(mode);
        
        return true;
    }

    public bool SaveServiceYears(int applicationID, Mode mode)
    {
        PromotionTableAdapters.ServiceYearsTableAdapter adapter = new PromotionTableAdapters.ServiceYearsTableAdapter();

        Promotion.ServiceYearsDataTable table = adapter.GetServiceYearsByApplication(applicationID);

        try
        {
            if (table.Count == 0)
            {
                if (mode == Mode.InputByApplicant)
                {
                    adapter.InsertQuery(
                        applicationID,
                        byte.Parse(txtYears.Text),
                        byte.Parse(txtMonths.Text),
                        byte.Parse(txtSaudiYears.Text),
                        byte.Parse(txtSaudiMonths.Text),
                        null,
                        null,
                        null,
                        null,
                        null);
                }
                else if (mode == Mode.InputByFacultyAffairs)
                {

                    adapter.InsertQuery(
                        applicationID,
                        null,
                        null,
                        null,
                        null,
                        byte.Parse(txtVerfYears.Text),
                        byte.Parse(txtVerfMonths.Text),
                        byte.Parse(txtVerfSaudiYears.Text),
                        byte.Parse(txtVerfSaudiMonths.Text),
                        null);
                }
            }
            else
            {
                Promotion.ServiceYearsRow row = table[0];
                if (mode == Mode.InputByApplicant)
                {
                    row.App_KFUPM_NumYears  = byte.Parse(txtYears.Text);
                    row.App_KFUPM_NumMonths = byte.Parse(txtMonths.Text);
                    row.App_Saudi_NumYears  = byte.Parse(txtSaudiYears.Text);
                    row.App_Saudi_NumMonths = byte.Parse(txtSaudiMonths.Text);
                }
                else
                {
                    if (byte.Parse(txtVerfYears.Text) == 0
                        && byte.Parse(txtVerfMonths.Text) == 0
                        && byte.Parse(txtVerfSaudiYears.Text) == 0
                        && byte.Parse(txtVerfSaudiMonths.Text) == 0)
                    {
                        return false;
                    }
                    else 
                    {
                        row.KFUPM_NumYears = byte.Parse(txtVerfYears.Text);
                        row.KFUPM_NumMonths = byte.Parse(txtVerfMonths.Text);
                        row.Saudi_NumYears = byte.Parse(txtVerfSaudiYears.Text);
                        row.Saudi_NumMonths = byte.Parse(txtVerfSaudiMonths.Text);
                    }
                }

                adapter.Update(table);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
