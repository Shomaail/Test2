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

public partial class Controls_ReviewersSummary : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool isPrintMode = Utils.IsPrintMode();
        if (isPrintMode)
        {

            ButtonExport.Visible = false;
        }

        int applicationID;
        bool lastQuestion = false;
        try
        {
            applicationID  = int.Parse(Request.QueryString["applicationID"]); ;
        }
        catch
        {
            applicationID = int.Parse(Session["applicationID"].ToString());
        }
        

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
        
        
        if (datatable.Count == 0)
        {
            LabelNoForm.Text = "No Evaluation Form has been submitted by any External Reviewer.";
            LabelNoForm.ForeColor = System.Drawing.Color.Red;
            ButtonExport.Enabled = false;
        }
        else
        {
            ButtonExport.Enabled = true;
            ButtonExport.Attributes.Add("onclick", "window.open ('SummaryExport.aspx?applicationID=" + applicationID + "',null,'scrollbars=yes, status= no, resizable = yes, toolbar=no,location=no,height = 700, width = 900, left = 200, top= 200, screenx=10,screeny=600,menubar=no');");   
//'Exportable Summary',
            //'height=250, width=250,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no'
        }

        TableRow tr = new TableRow();
        TableRow tr2;

        tr.Cells.Add(new TableCell());
        tr.Cells.Add(new TableCell());
        tr.Cells[tr.Cells.Count - 1].Text = "Names of Referees";
        tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#007d40");
        tr.Cells[tr.Cells.Count - 1].Font.Italic = true;
        
        foreach (ReviewerForm.ReviewersSummaryRow row in datatable)
        {
            tr.Cells.Add(new TableCell());
            tr.Cells[tr.Cells.Count-1].Text = "<b>"+row.Name+"</b></br>("+ Adapters.FrmFnlRefAdapter.GetDataByByRefreeID(row.RefreeID)[0].MailingAddress+")";
            //tr.Cells[tr.Cells.Count - 1].BorderStyle = BorderStyle.Groove;
            //tr.Cells[tr.Cells.Count - 1].BorderColor = System.Drawing.Color.SteelBlue;
            tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
            tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;
        }
        if (datatable.Count == 0)
        {
            for (int dummyCounter = 0; dummyCounter < 3; dummyCounter++)
            {
                tr.Cells.Add(new TableCell());
                tr.Cells[tr.Cells.Count - 1].Text = "[     ]";
                tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
                tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;
            }
        }
        TableSummary.Rows.Add(tr);
        tr = new TableRow();
        tr.Cells.Add(new TableCell());
        tr.Cells.Add(new TableCell());
        tr.Cells[tr.Cells.Count - 1].Text = "Familiarity";
        tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#007d40");
        tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;
        tr.Cells[tr.Cells.Count - 1].Font.Italic = true;
        foreach (ReviewerForm.ReviewersSummaryRow row in datatable)
        {
            tr.Cells.Add(new TableCell());
            tr.Cells[tr.Cells.Count - 1].Text = row.familiarity;
            tr.Cells[tr.Cells.Count - 1].BorderStyle = BorderStyle.Ridge;
            tr.Cells[tr.Cells.Count - 1].BorderColor = System.Drawing.ColorTranslator.FromHtml("#007d40");
            tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;

            tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
        }
        if (datatable.Count == 0)
        {
            for (int dummyCounter = 0; dummyCounter < 3; dummyCounter++)
            {
                tr.Cells.Add(new TableCell());
                tr.Cells[tr.Cells.Count - 1].Text = "[    ]";
                tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
                tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;
                tr.Cells[tr.Cells.Count - 1].BorderStyle = BorderStyle.Ridge;
            }
        }

        TableSummary.Rows.Add(tr);
        for (int i = 0; i < 9; i++)
        {
            tr = new TableRow();
            tr2 = new TableRow();
            tr.Cells.Add(new TableCell());
            tr.Cells[tr.Cells.Count - 1].Text = reviewersFormItem[i];
            tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#007d40");
            tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;
            tr.Cells[tr.Cells.Count - 1].BorderStyle = BorderStyle.Ridge;
            tr.Cells[tr.Cells.Count - 1].Font.Bold = true;
            tr.Cells[tr.Cells.Count - 1].ForeColor = System.Drawing.Color.White;
            tr.Cells[tr.Cells.Count - 1].RowSpan = 2;

            tr.Cells.Add(new TableCell());
            tr.Cells[tr.Cells.Count - 1].Text = "Rating";
            tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#007d40");
            tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;
            tr.Cells[tr.Cells.Count - 1].BorderStyle = BorderStyle.Ridge;
            tr.Cells[tr.Cells.Count - 1].Font.Italic = true;
//            tr2.Cells.Add(new TableCell());            
            tr2.Cells.Add(new TableCell());
            tr2.Cells[tr2.Cells.Count - 1].Text = "Justification";
            tr2.Cells[tr2.Cells.Count - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#007d40");
            tr2.Cells[tr2.Cells.Count - 1].BorderWidth = 1;
            tr2.Cells[tr2.Cells.Count - 1].BorderStyle = BorderStyle.Ridge;
            tr2.Cells[tr2.Cells.Count - 1].Font.Italic = true;



            foreach (ReviewerForm.ReviewersSummaryRow row in datatable)
            {

                tr.Cells.Add(new TableCell());
                if ((i+1) == 9)
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
                tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
                tr2.Cells.Add(new TableCell());            
                tr2.Cells[tr2.Cells.Count - 1].Text = row["Q" + (i + 1) + "Justification"].ToString();
                tr2.Cells[tr2.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
                tr2.Cells[tr2.Cells.Count - 1].BorderWidth = 1;

            }
            if (datatable.Count == 0)
            {
                for (int dummyCounter = 0; dummyCounter < 3; dummyCounter++)
                {
                    tr.Cells.Add(new TableCell());
                    tr.Cells[tr.Cells.Count - 1].Text = "[    ]";
                    tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
                    tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;
                    tr2.Cells.Add(new TableCell());
                    tr2.Cells[tr2.Cells.Count - 1].Text = "[    ]";
                    tr2.Cells[tr2.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue; 
                    tr2.Cells[tr2.Cells.Count - 1].BorderWidth = 1;
                }
            }
            TableSummary.Rows.Add(tr);
            TableSummary.Rows.Add(tr2);

        }
            tr = new TableRow();
            tr.Cells.Add(new TableCell());
            tr.Cells.Add(new TableCell());
            tr.Cells[tr.Cells.Count - 1].Text = "Comments";
            tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#007d40");
            tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;
            tr.Cells[tr.Cells.Count - 1].Font.Italic = true;
            TableSummary.Rows.Add(tr);
            foreach (ReviewerForm.ReviewersSummaryRow row in datatable)
            {
                tr.Cells.Add(new TableCell());
                tr.Cells[tr.Cells.Count - 1].Text = row["Comments"].ToString() + GetAttachmentLink(row);
                tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
                tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;
               
            }

    }

    private string GetAttachmentLink(ReviewerForm.ReviewersSummaryRow row)
    {
        if (row["SupDocument"].ToString() == "")
            return "";
        else
            // return "<a href=\"http:\\www.google.com\"> Attachment</a> ";
            //return "<a href=\"\\ApplicationFiles\\" + row["SupDocument"].ToString() + "\"> Attachment</a> ";
            return "<a href=NoMasterPage\\Handler.ashx?appID="+ row.ApplicationID + "refreeID="+ row.RefreeID+">Attachment</a> ";
    }
    //public Adapters adapters;
    //public Controls_ReviewersSummary()
    //{
    //    adapters = new Adapters();
    //}

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

}

