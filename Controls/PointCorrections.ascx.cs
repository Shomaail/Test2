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

public partial class Controls_PointCorrections : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void LoadPointCorrections(int applicationID)
    {
        PromotionTableAdapters.PointChangesTableAdapter adapter = new PromotionTableAdapters.PointChangesTableAdapter();
        Promotion.PointChangesDataTable table = adapter.GetDataByApplication(applicationID);
        if (table.Count == 0) return;

        if (table.Count < 1) return;
        txtR1Address.Text = table[0].CellCode;
        txtR1From.Text = table[0].From;
        txtR1To.Text = table[0].To;
        txtR1Justification.Text = table[0].Justification;

        if (table.Count < 2) return;
        txtR2Address.Text = table[1].CellCode;
        txtR2From.Text = table[1].From;
        txtR2To.Text = table[1].To;
        txtR2Justification.Text = table[1].Justification;

        if (table.Count < 3) return;
        txtR3Address.Text = table[2].CellCode;
        txtR3From.Text = table[2].From;
        txtR3To.Text = table[2].To;
        txtR3Justification.Text = table[2].Justification;

        if (table.Count < 4) return;
        txtR4Address.Text = table[3].CellCode;
        txtR4From.Text = table[3].From;
        txtR4To.Text = table[3].To;
        txtR4Justification.Text = table[3].Justification;

        if (table.Count < 5) return;
        txtR5Address.Text = table[4].CellCode;
        txtR5From.Text = table[4].From;
        txtR5To.Text = table[4].To;
        txtR5Justification.Text = table[4].Justification;
    }

    public void SetReadOnly(bool status)
    {
        txtR1Address.ReadOnly = status;
        txtR1From.ReadOnly = status;
        txtR1To.ReadOnly = status;
        txtR1Justification.ReadOnly = status;


        txtR2Address.ReadOnly = status;
        txtR2From.ReadOnly = status;
        txtR2To.ReadOnly = status;
        txtR2Justification.ReadOnly = status;


        txtR3Address.ReadOnly = status;
        txtR3From.ReadOnly = status;
        txtR3To.ReadOnly = status;
        txtR3Justification.ReadOnly = status;


        txtR4Address.ReadOnly = status;
        txtR4From.ReadOnly = status;
        txtR4To.ReadOnly = status;
        txtR4Justification.ReadOnly = status;


        txtR5Address.ReadOnly = status;
        txtR5From.ReadOnly = status;
        txtR5To.ReadOnly = status;
        txtR5Justification.ReadOnly = status;
    }


    public bool SavePointCorrections(int applicationID)
    {
        PromotionTableAdapters.PointChangesTableAdapter adapter = new PromotionTableAdapters.PointChangesTableAdapter();
        adapter.DeleteByApplication(applicationID);
        try
        {
            if (txtR1Address.Text.Trim().Length > 0) adapter.Insert(applicationID, txtR1Address.Text, txtR1From.Text, txtR1To.Text, txtR1Justification.Text);
            if (txtR2Address.Text.Trim().Length > 0) adapter.Insert(applicationID, txtR2Address.Text, txtR2From.Text, txtR2To.Text, txtR2Justification.Text);
            if (txtR3Address.Text.Trim().Length > 0) adapter.Insert(applicationID, txtR3Address.Text, txtR3From.Text, txtR3To.Text, txtR3Justification.Text);
            if (txtR4Address.Text.Trim().Length > 0) adapter.Insert(applicationID, txtR4Address.Text, txtR4From.Text, txtR4To.Text, txtR4Justification.Text);
            if (txtR5Address.Text.Trim().Length > 0) adapter.Insert(applicationID, txtR5Address.Text, txtR5From.Text, txtR5To.Text, txtR5Justification.Text);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
