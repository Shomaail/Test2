using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.IO;

public partial class Forms_ReviewersForm : System.Web.UI.Page
{
    /*
* Form_ReviewersForm
* 
* Status = 3 -- Submitted
* Status = 2 -- Saved Succesfully
* Status = 1 -- Saved Unsuccesfully
* Status = 0 -- Not saved (New)
* 
*/
    public string dataPath;
    public FileInfo[] files;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LoadInformation();
            LoadReviewerFormToView();
            SetPrinterFriendly(true);
        }
        catch (Exception)
        {
            
            
        }

//        HyperLink1.NavigateUrl = filePath;
  //      HyperLink1.Text = filePath;


    }

    private void LoadInformation()
    {
        int rowNo = 0;
        int refreeID;
        try
        {
            refreeID = int.Parse(Request.QueryString["refreeID"]); 
        }
        catch
        {
            refreeID = int.Parse(Session["refreeID"].ToString());
        }
        
        
        Form.Form_FinalRefreeDataTable refreeTable = Adapters.FrmFnlRefAdapter.GetDataByByRefreeID(refreeID);
        lblName.Text = refreeTable[rowNo].Name.ToString() + "(" + refreeTable[rowNo].Rank.ToString() + ")";
        lblAddress.Text = refreeTable[rowNo].MailingAddress.ToString();

        int appId = refreeTable[rowNo].ApplicationID;
        PromotionTableAdapters.ApplicationTableAdapter appAdapter = new PromotionTableAdapters.ApplicationTableAdapter();
        Promotion.ApplicationDataTable appTable = appAdapter.GetApplication(appId);
        HRTableAdapters.EmployeeTableAdapter employeeAdapter = new HRTableAdapters.EmployeeTableAdapter();
        LabelApplicant.Text = employeeAdapter.GetApplicant(appId)[0].Title + " " + employeeAdapter.GetApplicant(appId)[0].Name + " (" +employeeAdapter.GetApplicant(appId)[0].Department + " )";
        lblRank.Text = appTable[rowNo].ForRank.ToString();
        
    }

    public int GetFormStatus()
    {

        int rowNo = 0;
        int refreeID = int.Parse(Session["refreeID"].ToString());
        ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter adapter = new ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter();
        ReviewerForm.Form_ReviewersFormDataTable table = adapter.GetDataByPK(refreeID);
        int status = 0;
        if (table.Count > 0)
        {
            status = table[rowNo].Status;
        }
        return status;
    }

    private void LoadReviewerFormToView()
    {
        int refreeID = int.Parse(Session["refreeID"].ToString());

        int rowNo = 0;
        ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter adapter = new ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter();
        ReviewerForm.Form_ReviewersFormDataTable table = adapter.GetDataByPK(refreeID);
        if (table.Count > 0)
        {
            rblFamiliarity.Enabled = true;
            TextBoxOthers.ReadOnly = false;
            string familiarity = table[rowNo].familiarity;
            if (familiarity.ToLower().StartsWith("i am"))
            {
                rblFamiliarity.SelectedIndex = 0;
            }
            else if (familiarity.ToLower().StartsWith("i have"))
            {
                rblFamiliarity.SelectedIndex = 1;
            }
            else if (familiarity.ToLower().StartsWith("my experience"))
            {
                rblFamiliarity.SelectedIndex = 2;
            }
            else //if (familiarity.ToLower().StartsWith("Others"))
            {
                rblFamiliarity.SelectedIndex = 3;
                TextBoxOthers.Text = familiarity;
                LabelOthers.Text = familiarity;
            }

            rblQ1.Enabled = true;
            rblQ1.SelectedIndex = table[rowNo].Q1 - 1;

            rblQ2.Enabled = true;
            rblQ2.SelectedIndex = table[rowNo].Q2 - 1;

            rblQ3.Enabled = true;
            rblQ3.SelectedIndex = table[rowNo].Q3 - 1;

            rblQ4.Enabled = true;
            rblQ4.SelectedIndex = table[rowNo].Q4 - 1;

            rblQ5.Enabled = true;
            //rblQ5.SelectedIndex = table[rowNo].Q6 - 1;
            // Updated By Salman 30 Sep 2012
            rblQ5.SelectedIndex = table[rowNo].Q5 - 1;

            rblQ6.Enabled = true;
            rblQ6.SelectedIndex = table[rowNo].Q6 - 1;

            rblQ7.Enabled = true;
            rblQ7.SelectedIndex = table[rowNo].Q7 - 1;

            rblQ8.Enabled = true;
            rblQ8.SelectedIndex = table[rowNo].Q8 - 1;

            rblQ9.Enabled = true;
            rblQ9.SelectedIndex = table[rowNo].Q9 - 1;

            TextBoxJust1.Enabled = true;
            TextBoxJust1.Text = table[rowNo].Q1Justification;
            LabelJust1.Text = table[rowNo].Q1Justification;

            TextBoxJustification2.Enabled = true;
            TextBoxJustification2.Text = table[rowNo].Q2Justification;
            LabelJustification2.Text = table[rowNo].Q2Justification;

            TextBoxJustification3.Enabled = true;
            TextBoxJustification3.Text = table[rowNo].Q3Justification;
            LabelJustification3.Text = table[rowNo].Q3Justification;

            TextBoxJustification4.Enabled = true;
            TextBoxJustification4.Text = table[rowNo].Q4Justification;
            LabelJustification4.Text = table[rowNo].Q4Justification;

            TextBoxJustification5.Enabled = true;
            TextBoxJustification5.Text = table[rowNo].Q5Justification;
            LabelJustification5.Text = table[rowNo].Q5Justification;


            TextBoxJustification6.Enabled = true;
            TextBoxJustification6.Text = table[rowNo].Q6Justification;
            LabelJustification6.Text = table[rowNo].Q6Justification;


            TextBoxJustification7.Enabled = true;
            TextBoxJustification7.Text = table[rowNo].Q7Justification;
            LabelJustification7.Text = table[rowNo].Q7Justification;

            TextBoxJustification8.Enabled = true;
            TextBoxJustification8.Text = table[rowNo].Q8Justification;
            LabelJustification8.Text = table[rowNo].Q8Justification;

            TextBoxJustification9.Enabled = true;
            TextBoxJustification9.Text = table[rowNo].Q9Justification;
            LabelJustification9.Text = table[rowNo].Q9Justification;

            TextBoxComments.Enabled = true;
            TextBoxComments.Text = table[rowNo].Comments;
            LabelComments.Text = table[rowNo].Comments;

            if (table[rowNo].IsSupDocumentNull())
            {
                PanelUploadSupDoc.Visible = false;
            }
            else
            {
                LinkButtonSuppDoc.Text = table[rowNo].SupDocument;
                LabelSize.Text = table[rowNo].Size;
                
            }



        }
    }


    string SupDoc
    {
        get
        {
            if (HiddenFieldSupDoc.Value.Length == 0)
                return "";
            else
                return HiddenFieldSupDoc.Value.ToString();
        }
        set
        {
            HiddenFieldSupDoc.Value = value.ToString();
        }
    }
    string Size
    {
        get
        {
            if (HiddenFieldSize.Value.Length == 0)
                return "";
            else
                return HiddenFieldSize.Value.ToString();
        }
        set
        {
            HiddenFieldSize.Value = value.ToString();
        }
    }
    public void GetEvaluationAttachments(int appID, int refreeID, string dataPath, out FileInfo[] files)
    {
        DirectoryInfo folder = new DirectoryInfo(dataPath);
        files = folder.GetFiles(appID + "_" + refreeID + "_*", SearchOption.AllDirectories);
    }
    protected void lbtnEvaluationAttachment_PreRender(object sender, EventArgs e)
    {
        dataPath = Server.MapPath("~/App_Data/EvaluationAttachments/");

        int refreeID = int.Parse(Session["refreeID"].ToString());
        //FormTableAdapters.Form_FinalRefreeAccInfoTableAdapter adapterFrmFnRfrAccInf = new FormTableAdapters.Form_FinalRefreeAccInfoTableAdapter();
        //Form.Form_FinalRefreeAccInfoRow r = adapterFrmFnRfrAccInf.GetDataByPK(refreeID)[0];
        //Form.Form_FinalRefreeDataTable refreeTable = Adapters.FrmFnlRefAdapter.GetDataByByRefreeID(refreeID);

        //int appId = refreeTable[0].ApplicationID;
        int appID = int.Parse(Session["ApplicationIDGlobal"].ToString());

        GetEvaluationAttachments(appID, refreeID, dataPath, out files);
        if (files.Count() > 0)
        {
            (sender as LinkButton).OnClientClick = "window.open('"
            + "../Forms/NoMasterPage/Handler.ashx?appID=" + appID
            + "&refreeID="
            + refreeID
            + "&fileName=" + files[0].Name + "'); return false ;";
        }
    }
    protected void LinkButtonSuppDoc_Click(object sender, EventArgs e)
    {
        string dataPath = Server.MapPath("~\\App_LocalResources") + "\\";
        int refreeID = int.Parse(Session["refreeID"].ToString());
        ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter adapterRevForm = new ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter();
        string fileName = adapterRevForm.GetDataByPK(refreeID)[0].SupDocument;
        string filePath = dataPath + fileName;

        if (!File.Exists(filePath))
            return;

        FileStream stream = File.OpenRead(filePath);
        long length = stream.Length;
        stream.Close();

        Response.Clear();
        try
        {
            Response.Charset = "UTF-8";
            Response.AddHeader("Content-Disposition", "attachment; filename= " + fileName);
            Response.AddHeader("Content-Length", "" + length);
            Response.WriteFile(dataPath + fileName);
            Response.Flush();
            Response.Close();
        }
        catch
        {
        }
    }
    public void SetPrinterFriendly(bool status)
    {
        TextBoxOthers.Visible = !status;
        TextBoxJust1.Visible = !status;
        TextBoxJustification2.Visible = !status;
        TextBoxJustification3.Visible = !status;
        TextBoxJustification4.Visible = !status;
        TextBoxJustification5.Visible = !status;
        TextBoxJustification6.Visible = !status;
        TextBoxJustification7.Visible = !status;
        TextBoxJustification8.Visible = !status;
        TextBoxJustification9.Visible = !status;
        TextBoxComments.Visible = !status;

        LabelOthers.Visible = status;
        LabelJust1.Visible = status;
        LabelJustification2.Visible = status;
        LabelJustification3.Visible = status;
        LabelJustification4.Visible = status;
        LabelJustification5.Visible = status;
        LabelJustification6.Visible = status;
        LabelJustification7.Visible = status;
        LabelJustification8.Visible = status;
        LabelJustification9.Visible = status;
        LabelComments.Visible = status;
    }

}
