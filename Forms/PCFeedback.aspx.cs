using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL.Data;
public partial class Forms_PCFeedback : System.Web.UI.Page
{
    BAL bal = new BAL();
    private enum Mode { ViewByApplicant = 1, InputByPCChairman = 2, ViewByVRGSSR = 3 }

    //public override string StyleSheetTheme
    //{
    //    get
    //    {
    //        return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
    //    }
    //}
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.Validate();
        //if (!Page.IsValid)
        //{
        //    LabelValicationSummary.Visible = true;
        //}
        //else
        //    LabelValicationSummary.Visible = false;
        if (IsPostBack)
        {
            return;
        }

        if (Master.CurrentFormLevel == -1)
        {
            Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
            return;
        }
        divFeeback.DataBind();
        Instruction1.Text = Master.CurrentFormInstruction;
        DatabindControls();
        SwitchMode((Mode)Master.CurrentFormLevel);
        

        //hide buttons when printing
        if (Utils.IsPrintMode())
        {
            btnSave.Visible = false;
            Instruction1.Visible = false;

            LabelPrCR.Visible = true;
            LabelPrCU_PS.Visible = true;
            LabelPrGenRmrk.Visible = true;
            LabelPrIR.Visible = true;
            LabelPrOrig.Visible = true;
            LabelPrProd.Visible = true;
            LabelPrQualC.Visible = true;
            LabelPrQualJ.Visible = true;
            LabelPrSigni.Visible = true;
            LabelPrSS.Visible = true;
            LabelPrTP.Visible = true;
            TextBoxCR.Visible = false;
            TextBoxCU_PS.Visible = false;
            TextBoxGenRmrk.Visible = false;
            TextBoxIR.Visible = false;
            TextBoxOrig.Visible = false;
            TextBoxProd.Visible = false;
            TextBoxQualC.Visible = false;
            TextBoxQualJ.Visible = false;
            TextBoxSigni.Visible = false;
            TextBoxSS.Visible = false;
            TextBoxTP.Visible = false;
         //   RequiredFieldValidator1.Visible = false;
            //RequiredFieldValidator2.Visible = false;
            //RequiredFieldValidator3.Visible = false;
            //RequiredFieldValidator4.Visible = false;
            //RequiredFieldValidator5.Visible = false;
            //RequiredFieldValidator6.Visible = false;
            //RequiredFieldValidator7.Visible = false;
            //RequiredFieldValidator8.Visible = false;
            //RequiredFieldValidator9.Visible = false;
            //RequiredFieldValidator10.Visible = false;
            //RequiredFieldValidator11.Visible = false;
            LabelValicationSummary.Visible = false;
        //    CustomValidator1.Visible = false;
            //CustomValidator2.Visible = false;
            //CustomValidator3.Visible = false;
            //CustomValidator4.Visible = false;
            //CustomValidator5.Visible = false;
            //CustomValidator6.Visible = false;
            //CustomValidator7.Visible = false;
            //CustomValidator8.Visible = false;
            //CustomValidator9.Visible = false;
            //CustomValidator10.Visible = false;
            //CustomValidator11.Visible = false;

            LabelCR.Visible = TextBoxCR.Text != "";
            LabelCU_PS.Visible = TextBoxCU_PS.Text != "";
            LabelGenRmrk.Visible = TextBoxGenRmrk.Text != "";
            LabelIR.Visible = TextBoxIR.Text != "";
            LabelOrig.Visible = TextBoxOrig.Text != "";
            LabelProd.Visible = TextBoxProd.Text != "";
            LabelQualC.Visible = TextBoxQualC.Text != "";
            LabelQualJ.Visible = TextBoxQualJ.Text != "";
            LabelSigni.Visible = TextBoxSigni.Text != "";
            LabelSS.Visible = TextBoxSS.Text != "";
            LabelTP.Visible = TextBoxTP.Text != "";

                    
        }
    }


    private void DatabindControls()
    {
        if (bal.GetForm_PCFeedback(Master.ApplicationID).Count == 0)
        {
            divTopOfFormMsgFailure.DataBind();
            lblMessageFailure.DataBind();
            divTopOfFormMsgSuccess.DataBind();
            lblMessageSuccess.DataBind();
            return;
        }
        Form_PCFeedback fpf = bal.GetForm_PCFeedback(Master.ApplicationID).First();

        LabelPrCR.Text = fpf.CR;
        LabelPrCU_PS.Text = fpf.CU_PS;
        LabelPrGenRmrk.Text = fpf.GenRmrk;
        LabelPrIR.Text = fpf.IR;
        LabelPrOrig.Text = fpf.Orig;
        LabelPrProd.Text = fpf.Prod;
        LabelPrQualC.Text = fpf.QualC;
        LabelPrQualJ.Text = fpf.QualJ;
        LabelPrSigni.Text = fpf.Signi;
        LabelPrSS.Text = fpf.SS;
        LabelPrTP.Text = fpf.TP;

        
        if(fpf.TP.Length < 10)
        {
            TextBoxTP.BorderColor = Color.Brown;            
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxTP.BorderColor = Color.Gray;
            LabelPrTP.Text = "";
        }
        if (fpf.CU_PS.Length < 10)
        {
            TextBoxCU_PS.BorderColor = Color.Brown;
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxCU_PS.BorderColor = Color.Gray;           
        }
        if (fpf.CR.Length < 10)
        {
            TextBoxCR.BorderColor = Color.Brown;
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxCR.BorderColor = Color.Gray;
        }
        if (fpf.GenRmrk.Length < 10)
        {
            TextBoxGenRmrk.BorderColor = Color.Brown;
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxGenRmrk.BorderColor = Color.Gray;
        }
        if (fpf.IR.Length < 10)
        {
            TextBoxIR.BorderColor = Color.Brown;
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxIR.BorderColor = Color.Gray;
        }
        if (fpf.Orig.Length < 10)
        {
            TextBoxOrig.BorderColor = Color.Brown;
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxOrig.BorderColor = Color.Gray;
        }
        if (fpf.Prod.Length < 10)
        {
            TextBoxProd.BorderColor = Color.Brown;
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxProd.BorderColor = Color.Gray;
        }
        if (fpf.QualC.Length < 10)
        {
            TextBoxQualC.BorderColor = Color.Brown;
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxQualC.BorderColor = Color.Gray;
        }
        if (fpf.QualJ.Length < 10)
        {
            TextBoxQualJ.BorderColor = Color.Brown;
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxQualJ.BorderColor = Color.Gray;
        }
        if (fpf.Signi.Length < 10)
        {
            TextBoxSigni.BorderColor = Color.Brown;
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxSigni.BorderColor = Color.Gray;
        }
        if (fpf.SS.Length < 10)
        {
            TextBoxSS.BorderColor = Color.Brown;
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxSS.BorderColor = Color.Gray;
        }
        if (fpf.TP.Length < 10)
        {
            TextBoxTP.BorderColor = Color.Brown;
            ShowError("Write atleast two sentences");
        }
        else
        {
            TextBoxTP.BorderColor = Color.Gray;            
        }
        if (fpf.TP.Length >= 10 && fpf.SS.Length >= 10 && fpf.Signi.Length >= 10 && fpf.QualC.Length >= 10 && fpf.QualJ.Length >= 10 && fpf.Prod.Length >= 10 && fpf.Orig.Length >= 10 && fpf.IR.Length >= 10
            && fpf.GenRmrk.Length >= 10 && fpf.CR.Length >= 10 && fpf.CU_PS.Length >= 10 && fpf.TP.Length >= 10)
        {
            ShowMessage("Complete!");
        }
        TextBoxCU_PS.Text = fpf.CU_PS;
        TextBoxCR.Text = fpf.CR;
        TextBoxGenRmrk.Text = fpf.GenRmrk;
        TextBoxIR.Text = fpf.IR;
        TextBoxOrig.Text = fpf.Orig;
        TextBoxProd.Text = fpf.Prod;
        TextBoxQualC.Text = fpf.QualC;
        TextBoxQualJ.Text = fpf.QualJ;
        TextBoxSigni.Text = fpf.Signi;
        TextBoxSS.Text = fpf.SS;
        TextBoxTP.Text = fpf.TP;

        divTopOfFormMsgFailure.DataBind();
        lblMessageFailure.DataBind();
        divTopOfFormMsgSuccess.DataBind();
        lblMessageSuccess.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        bool success = SaveFeedback();
        if (success)
        {
            //ShowMessage("Saved Successfully");
            //this.programmaticModalPopup1.Show();
            labelProgrammaticPopup0.Text = "Saved Successfully";
            programmaticModalPopup0.Show();
        }
        else
        {
            ShowError("Please make sure you input all the fields.");
        }

    }

    private void ShowError(string message)
    {
        Master.ReportFailure(message);
    }

    private void ShowMessage(string message)
    {
        Master.ReportSuccess(message);
    }


    private void SwitchMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.InputByPCChairman:
                LoadFeedback();
                break;
            case Mode.ViewByVRGSSR:
                LoadFeedback();
                break;
            case Mode.ViewByApplicant:
                LoadFeedback();
                btnSave.Visible = false;
                break;
        }
    }
    public bool SaveFeedback()
    {
        if (Master.App.ApplicationClosed)
        {
            return false;
        }
        if(bal.GetForm_PCFeedback(Master.ApplicationID).Count == 0 )
        {
            Form_PCFeedback fpf = new Form_PCFeedback
            {
                CR = TextBoxCR.Text,
                CU_PS = TextBoxCU_PS.Text,
                GenRmrk = TextBoxGenRmrk.Text,
                IR = TextBoxIR.Text,
                Orig = TextBoxOrig.Text,
                Prod = TextBoxProd.Text,
                QualC = TextBoxQualC.Text,
                QualJ = TextBoxQualJ.Text,
                Signi = TextBoxSigni.Text,
                SS = TextBoxSS.Text,
                TP = TextBoxTP.Text
            };
            bal.InsertForm_PCFeedback(Master.ApplicationID, fpf.CR, fpf.CU_PS, fpf.GenRmrk, fpf.IR, fpf.Orig, fpf.Prod, fpf.QualC, fpf.QualJ, fpf.Signi, fpf.SS, fpf.TP);
            return true;
        }
        else
        {
            Form_PCFeedback fpf = bal.GetForm_PCFeedback(Master.ApplicationID).First();
            fpf.CR = TextBoxCR.Text;
            fpf.CU_PS = TextBoxCU_PS.Text;
            fpf.GenRmrk = TextBoxGenRmrk.Text;
            fpf.IR = TextBoxIR.Text;
            fpf.Orig = TextBoxOrig.Text;
            fpf.Prod = TextBoxProd.Text;
            fpf.QualC = TextBoxQualC.Text;
            fpf.QualJ = TextBoxQualJ.Text;
            fpf.Signi = TextBoxSigni.Text;
            fpf.SS = TextBoxSS.Text;
            fpf.TP = TextBoxTP.Text;

            bal.UpdateForm_PCFeedback(Master.ApplicationID, fpf.CR, fpf.CU_PS, fpf.GenRmrk, fpf.IR, fpf.Orig, fpf.Prod, fpf.QualC, fpf.QualJ, fpf.Signi, fpf.SS, fpf.TP);
            return true;
        }
    
        
    }
    public void LoadFeedback()
    {
        if (bal.GetForm_PCFeedback(Master.ApplicationID).Count == 0)
        {
            return ;
        }
        Form_PCFeedback fpf = bal.GetForm_PCFeedback(Master.ApplicationID).First();

        LabelPrCR.Text = fpf.CR;
        LabelPrCU_PS.Text = fpf.CU_PS;
        LabelPrGenRmrk.Text = fpf.GenRmrk;
        LabelPrIR.Text = fpf.IR;
        LabelPrOrig.Text = fpf.Orig;
        LabelPrProd.Text = fpf.Prod;
        LabelPrQualC.Text = fpf.QualC;
        LabelPrQualJ.Text = fpf.QualJ;
        LabelPrSigni.Text = fpf.Signi;
        LabelPrSS.Text = fpf.SS;
        LabelPrTP.Text = fpf.TP;

        TextBoxCR.Text      = fpf.CR;
        TextBoxCU_PS.Text   = fpf.CU_PS;
        TextBoxGenRmrk.Text = fpf.GenRmrk;
        TextBoxIR.Text      = fpf.IR;
        TextBoxOrig.Text    = fpf.Orig;
        TextBoxProd.Text    = fpf.Prod;
        TextBoxQualC.Text   = fpf.QualC;
        TextBoxQualJ.Text   = fpf.QualJ;
        TextBoxSigni.Text   = fpf.Signi;
        TextBoxSS.Text      = fpf.SS;
        TextBoxTP.Text      = fpf.TP;
        LabelPrCR.Visible = false;
        LabelPrCU_PS.Visible = false;
        LabelPrGenRmrk.Visible = false;
        LabelPrIR.Visible = false;
        LabelPrOrig.Visible = false;
        LabelPrProd.Visible = false;
        LabelPrQualC.Visible = false;
        LabelPrQualJ.Visible = false;
        LabelPrSigni.Visible = false;
        LabelPrSS.Visible = false;
        LabelPrTP.Visible = false;
    }
   public void ValidateTextLength(object source, ServerValidateEventArgs args)
    {
        args.IsValid = args.Value.Length > 10;
    }
   
   protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
   {
       this.programmaticModalPopup0.Hide();
   }
   protected void TextBoxTP_TextChanged(object sender, EventArgs e)
    {
     
       SaveFeedback();
        DatabindControls();
    }
   protected void TextBoxProd_TextChanged(object sender, EventArgs e)
   {
       SaveFeedback();
        DatabindControls();
    }
   protected void TextBoxQualJ_TextChanged(object sender, EventArgs e)
   {
       SaveFeedback();
        DatabindControls();
    }
   protected void TextBoxQualC_TextChanged(object sender, EventArgs e)
   {
       SaveFeedback();
        DatabindControls();
    }
   protected void TextBoxOrig_TextChanged(object sender, EventArgs e)
   {
       SaveFeedback(); DatabindControls();
    }
   protected void TextBoxSigni_TextChanged(object sender, EventArgs e)
   {
       SaveFeedback(); DatabindControls();
    }
   protected void TextBoxIR_TextChanged(object sender, EventArgs e)
   {
       SaveFeedback();
   }
   protected void TextBoxCR_TextChanged(object sender, EventArgs e)
   {
       SaveFeedback(); DatabindControls();
    }

    protected void TextBoxSS_TextChanged(object sender, EventArgs e)
   {
       SaveFeedback(); DatabindControls();
    }
   protected void TextBoxCU_PS_TextChanged(object sender, EventArgs e)
   {
       SaveFeedback(); DatabindControls();
    }
   protected void TextBoxGenRmrk_TextChanged(object sender, EventArgs e)
   {
       SaveFeedback(); DatabindControls();
    }
    #region Get Methods 
    public bool GetAppTaskFormCompleted()
    {
        if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0).Count > 0)
        {
            return bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0)[0].Completed;
        }
        else
        {
            return false;
        }
    }
    public string GetAppTaskFormMessage()
    {
        if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0).Count > 0)
        {
            return bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0)[0].Message;
        }
        else
        {
            return "";
        }

    }
    #endregion
}

