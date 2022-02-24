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

public partial class SummaryExport : System.Web.UI.Page
{
    //public Adapters adapters;
    //public SummaryExport()
    //{
    //    adapters = new Adapters();
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        int applicationID;
        applicationID = int.Parse(Request.QueryString["applicationID"]); ;
        bool lastQuestion = false;
//        LinkButtonClose.Attributes.Add("onclick", "self.close()");


        string[] reviewersFormItem = new string[10];
        reviewersFormItem[0] = "Productivity";
        reviewersFormItem[1] = "Quality of the journals";
        reviewersFormItem[2] = "Quality of conferences";
        reviewersFormItem[3] = "Originality";
        reviewersFormItem[4] = "Significance";
        reviewersFormItem[5] = "Independence in research";
        reviewersFormItem[6] = "Citations";
        reviewersFormItem[7] = "Scholarly stature ";
        reviewersFormItem[8] = "Promotability";
        reviewersFormItem[9] = "Comments";

        ReviewerFormTableAdapters.ReviewersSummaryTableAdapter adapter = new ReviewerFormTableAdapters.ReviewersSummaryTableAdapter();
        ReviewerForm.ReviewersSummaryDataTable datatable = adapter.GetDataByApplication(applicationID);

        TableRow tr = new TableRow();
        TableRow tr2;

        tr.Cells.Add(new TableCell());
        tr.Cells.Add(new TableCell());
        tr.Cells[tr.Cells.Count - 1].Text = "Names of Referees";
//        tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.SteelBlue;
        tr.Cells[tr.Cells.Count - 1].Font.Italic = true;
        
        foreach (ReviewerForm.ReviewersSummaryRow row in datatable)
        {
            tr.Cells.Add(new TableCell());
            tr.Cells[tr.Cells.Count - 1].Text = "<b>" + row.Name + "</b></br>(" + Adapters.FrmFnlRefAdapter.GetDataByByRefreeID(row.RefreeID)[0].MailingAddress + ")";
            //tr.Cells[tr.Cells.Count - 1].BorderStyle = BorderStyle.Groove;
            //tr.Cells[tr.Cells.Count - 1].BorderColor = System.Drawing.Color.SteelBlue;
//            tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
        }
        if (datatable.Count == 0)
        {
            for (int dummyCounter = 0; dummyCounter < 3; dummyCounter++)
            {
                tr.Cells.Add(new TableCell());
                tr.Cells[tr.Cells.Count - 1].Text = "[     ]";
//                tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
            }
        }
        TableSummary.Rows.Add(tr);
        tr = new TableRow();
        tr.Cells.Add(new TableCell());
        tr.Cells.Add(new TableCell());
        tr.Cells[tr.Cells.Count - 1].Text = "Familiarity";
//        tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.SteelBlue;
        tr.Cells[tr.Cells.Count - 1].Font.Italic = true;
        foreach (ReviewerForm.ReviewersSummaryRow row in datatable)
        {
            tr.Cells.Add(new TableCell());
            tr.Cells[tr.Cells.Count - 1].Text = row.familiarity;
            //tr.Cells[tr.Cells.Count - 1].BorderStyle = BorderStyle.Groove;
            //tr.Cells[tr.Cells.Count - 1].BorderColor = System.Drawing.Color.SteelBlue;
//            tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
        }
        if (datatable.Count == 0)
        {
            for (int dummyCounter = 0; dummyCounter < 3; dummyCounter++)
            {
                tr.Cells.Add(new TableCell());
                tr.Cells[tr.Cells.Count - 1].Text = "[    ]";
//                tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
            }
        }

        TableSummary.Rows.Add(tr);
        for (int i = 0; i < 9; i++)
        {
            tr = new TableRow();
            tr2 = new TableRow();
            tr.Cells.Add(new TableCell());
            tr.Cells[tr.Cells.Count - 1].Text = reviewersFormItem[i];
//            tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.SteelBlue;
//            tr.Cells[tr.Cells.Count - 1].BorderStyle = BorderStyle.None;
            tr.Cells[tr.Cells.Count - 1].Font.Bold = true;
  //          tr.Cells[tr.Cells.Count - 1].ForeColor = System.Drawing.Color.White;
            tr.Cells[tr.Cells.Count - 1].RowSpan = 2;

            tr.Cells.Add(new TableCell());
            tr.Cells[tr.Cells.Count - 1].Text = "Rating";
    //        tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.SteelBlue;
            tr.Cells[tr.Cells.Count - 1].Font.Italic = true;
//            tr2.Cells.Add(new TableCell());            
            tr2.Cells.Add(new TableCell());
            tr2.Cells[tr2.Cells.Count - 1].Text = "Justification";
      //      tr2.Cells[tr2.Cells.Count - 1].BackColor = System.Drawing.Color.SteelBlue;
            tr2.Cells[tr2.Cells.Count - 1].Font.Italic = true;



            foreach (ReviewerForm.ReviewersSummaryRow row in datatable)
            {

                tr.Cells.Add(new TableCell());
                if ((i + 1) == 9)
                {
                    lastQuestion = true;
                    tr.Cells[tr.Cells.Count - 1].Text = RatingConversion(int.Parse(row["Q" + (i + 1)].ToString()), lastQuestion);
                }
                else
                {
                    tr.Cells[tr.Cells.Count - 1].Text = RatingConversion(int.Parse(row["Q" + (i + 1)].ToString()));
                }
                //tr.Cells[tr.Cells.Count - 1].BorderStyle = BorderStyle.Groove;
                //tr.Cells[tr.Cells.Count - 1].BorderColor = System.Drawing.Color.LightSteelBlue;
//                tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
                tr2.Cells.Add(new TableCell());            
                tr2.Cells[tr2.Cells.Count - 1].Text = row["Q" + (i + 1) + "Justification"].ToString();
  //              tr2.Cells[tr2.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
                //tr2.Cells[tr2.Cells.Count - 1].BorderStyle = BorderStyle.Groove;
                //tr2.Cells[tr2.Cells.Count - 1].BorderColor = System.Drawing.Color.LightSteelBlue;
            }
            if (datatable.Count == 0)
            {
                for (int dummyCounter = 0; dummyCounter < 3; dummyCounter++)
                {
                    tr.Cells.Add(new TableCell());
                    tr.Cells[tr.Cells.Count - 1].Text = "[    ]";                    
//                    tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
                    tr2.Cells.Add(new TableCell());
                    tr2.Cells[tr2.Cells.Count - 1].Text = "[    ]";
  //                  tr2.Cells[tr2.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;                    
                }
            }
            TableSummary.Rows.Add(tr);
            TableSummary.Rows.Add(tr2);

        }
            tr = new TableRow();
            tr.Cells.Add(new TableCell());
            tr.Cells.Add(new TableCell());
            tr.Cells[tr.Cells.Count - 1].Text = "Comments";
//            tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.SteelBlue;
            tr.Cells[tr.Cells.Count - 1].Font.Italic = true;
            TableSummary.Rows.Add(tr);
            foreach (ReviewerForm.ReviewersSummaryRow row in datatable)
            {
                tr.Cells.Add(new TableCell());
                tr.Cells[tr.Cells.Count - 1].Text = row["Comments"].ToString();
//                tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
            }

    }
    public string RatingConversion(int rating)
    {
        switch (rating)
        {
            case 1: 
            return "Excellent";
            case 2:
            return "Very Good";
            case 3:
            return "Good";
            case 4:
            return "Fair";
            case 5:
            return "Poor";

        }
        return "";
    }
    public string RatingConversion(int rating, bool lastQuestion)
    {
        switch (rating)
        {
            case 1:
                return "Promotable";
            case 2:
                return "Marginally Promotable";
            case 3:
                return "Unpromotable";
        }
        return "";
    }

    protected void LinkButtonExport_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd-ms.word";

        ///uncomment the 1st  line if you want to open directly in IE and comment the 2nd  line
        //HttpContext.Current.Response.AddHeader("content-disposition", "inline; filename=ExportWord.doc");
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=ExportWord.doc");


    }
    protected void LinkButtonClose_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'> { self.close() }</script>");
    }
}
