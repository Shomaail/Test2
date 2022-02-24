using System;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.IO;
using BL.Data;
public partial class Forms_ReviewersSummary : System.Web.UI.Page
{
    BAL bal = new BAL();
    ExtRevBAL erBAL = new ExtRevBAL();
    ExtRevFormBAL erfBAL = new ExtRevFormBAL();
    public FileInfo[] files;
    public string Language
    {
        set
        {
            ViewState["Language"] = value;
        }
        get
        {
            if (ViewState["Language"] != null)
            {
                return ViewState["Language"].ToString();
            }
            else
            {
                return "en-US";
            }
        }
    }
    private string GetRBValueFromScore(string aRBValue)
    {
        if (aRBValue == null || aRBValue == "")
        {
            return "";
        }
        int value; 
        if (!int.TryParse(aRBValue,out value))
        {
            return aRBValue + " ";            
        }
        if (int.Parse(aRBValue) > 8)
        {
            return "<b><span class=\"rating excellent\">Excellent (" + aRBValue + ")</span></b></br><b> Justification: </b> ";
        }
        else if (int.Parse(aRBValue) == 8)
        {
            return "<b><span class=\"rating very-good\">Very Good (" + aRBValue + ")</span></b></br><b> Justification: </b> ";
        }
        else if (int.Parse(aRBValue) == 7)
        {
            return "<b><span class=\"rating good\">Good (" + aRBValue + ")</span></b></br><b> Justification: </b> ";
        }
        else if (int.Parse(aRBValue) == 6)
        {
            return "<b><span class=\"rating fair\">Fair (" + aRBValue + ")</span></b></br><b> Justification: </b> ";
        }
        else if (int.Parse(aRBValue) < 6)
        {
            return "<b><span class=\"rating poor\">Poor (" + aRBValue + ")</span></b></br><b> Justification: </b> ";
        }
        else
        {
            return "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Instruction1.Text = Master.CurrentFormInstruction;
            TableHeaderRow thr = new TableHeaderRow();
            List<Form_FinalExtRev> lffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID)
                .Where(a => a.EvaluationStatus == EvaluationStatus.Submitted.ToString()).ToList();
            if (lffer.Count == 0)
            {
                TableSummary.Visible = false;
                lblMessage.Text = "There is no Evaluation submitted yet";
                return;
            }
            else
            {
                lblMessage.Text = "";
                btnExportToWord.Attributes.Add("onclick", "window.open ('SummaryExport.aspx?applicationID=" + Master.ApplicationID + "',null,'scrollbars=yes, status= no, resizable = yes, toolbar=no,location=no,height = 700, width = 900, left = 200, top= 200, screenx=10,screeny=600,menubar=no');");
            }
            thr = new TableHeaderRow();
            thr.TableSection = TableRowSection.TableHeader;
            thr.Cells.Add(new TableHeaderCell());
            thr.Cells[thr.Cells.Count - 1].Text = "Names of External Reviewers";
            thr.Cells[thr.Cells.Count - 1].Width = new Unit("10%");
            thr.Cells[thr.Cells.Count - 1].HorizontalAlign = HorizontalAlign.Left;
            thr.Cells[thr.Cells.Count - 1].VerticalAlign = VerticalAlign.Top;           
            foreach (Form_FinalExtRev er in lffer)
            {
                thr.Cells.Add(new TableHeaderCell());
                thr.Cells[thr.Cells.Count - 1].Text = "<b>" + er.ExternalReviewer.NameString + "</b></br>(" + er.ExternalReviewer.MailingAddress + ")";
                thr.Cells[thr.Cells.Count - 1].Width = new Unit("18%");
                thr.Cells[thr.Cells.Count - 1].HorizontalAlign = HorizontalAlign.Left;
                thr.Cells[thr.Cells.Count - 1].VerticalAlign = VerticalAlign.Top;
                //tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.Color.LightSteelBlue;
                //tr.Cells[tr.Cells.Count - 1].BorderColor = System.Drawing.ColorTranslator.FromHtml("#0088ce");
                //tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;

            }
            TableSummary.Rows.Add(thr);
            TableRow tr = new TableRow();
            foreach (Evaluation q in erfBAL.GetEvaluation(Master.ApplicationID, lffer[0].ExternalReviewerID, Language))
            {
                tr = new TableRow();              
                tr.Cells.Add(new TableCell());
                tr.Cells[tr.Cells.Count - 1].Text = q.ARBTitle;
                string newTitle = Regex.Replace(q.ATATitle, "\\(.*?\\)", "");
                tr.Cells[tr.Cells.Count - 1].HorizontalAlign = HorizontalAlign.Left;
                tr.Cells[tr.Cells.Count - 1].VerticalAlign = VerticalAlign.Top;
                tr.Cells[tr.Cells.Count - 1].CssClass = "eval-q";            
                foreach (Form_FinalExtRev row in lffer)
                {
                    tr.Cells.Add(new TableCell());                    
                }                
                TableSummary.Rows.Add(tr);
            }
            int evalCounter = 0;
            for (int row = 1; row < TableSummary.Rows.Count; row +=1)
            {
                for (int cell = 1; cell < TableSummary.Rows[row].Cells.Count; cell++)
                {
                    Evaluation ev = erfBAL.GetEvaluation(Master.ApplicationID, lffer[cell - 1].ExternalReviewerID, Language)[evalCounter];                   
                    if (ev.ARBValue == null || ev.ARBValue == "")
                    {
                        TableSummary.Rows[row].Cells[cell].Text = "<b>Remarks: </b>" + ev.ATAValue;
                     }
                    else
                    {
                        TableSummary.Rows[row].Cells[cell].Text = GetRBValueFromScore(ev.ARBValue) + ev.ATAValue;
                    }
                    TableSummary.Rows[row].Cells[cell].HorizontalAlign = HorizontalAlign.Left;
                    TableSummary.Rows[row].Cells[cell].VerticalAlign = VerticalAlign.Top;
                }
                evalCounter++;
            }
            var span = new HtmlGenericControl("span");           
            span.InnerHtml = "Attachments";
            span.Attributes["class"] = "glyphicon glyphicon-paperclip";
            tr = new TableFooterRow();
            tr.TableSection = TableRowSection.TableFooter;
            tr.Cells.Add(new TableCell());
            tr.Cells[tr.Cells.Count - 1].Controls.Add(span);
            tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#007D40");
            tr.Cells[tr.Cells.Count - 1].BorderColor = System.Drawing.ColorTranslator.FromHtml("#007D40");
            tr.Cells[tr.Cells.Count - 1].Font.Italic = true;
            tr.Cells[tr.Cells.Count - 1].ForeColor = System.Drawing.Color.White;

            foreach (Form_FinalExtRev er in lffer)
            {
                tr.Cells.Add(new TableCell());              
                tr.Cells[tr.Cells.Count - 1].BackColor = System.Drawing.ColorTranslator.FromHtml("#007D40");
                tr.Cells[tr.Cells.Count - 1].BorderColor = System.Drawing.ColorTranslator.FromHtml("#007D40");
                tr.Cells[tr.Cells.Count - 1].BorderWidth = 1;
                tr.Cells[tr.Cells.Count - 1].ForeColor = System.Drawing.Color.White;
            }
            TableSummary.Rows.Add(tr);
            bool isPrintMode = Utils.IsPrintMode();
            if (isPrintMode)
            {
                Instruction1.Visible = false;
            }
        }
        catch (Exception exp)
        {
            
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID+"&roleID="+Master.CurRoleID);
            return;
        }
            

    }
}
