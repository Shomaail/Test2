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
using System.Data.SqlClient;

public partial class Controls_Refrees : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
       (FindControl("taExtRevData") as HtmlTextArea).Focus();

    }

    public void ListServiceYears(int applicationID, int type)
    {
        _Type = Convert.ToByte(type);


        grdRefrees.DataSource = Adapters.FrmRefAdapter.GetByApplication(applicationID, _Type);
        grdRefrees.DataBind();
        
    }

    public void ListRefrees(int applicationID, int type)
    {
        
        _Type = Convert.ToByte(type);
        grdRefrees.DataSource = Adapters.FrmRefAdapter.GetByApplication(applicationID, _Type);
        grdRefrees.DataBind();
        dlRefrees.DataSource = Adapters.FrmRefAdapter.GetByApplication(applicationID, _Type);
        dlRefrees.DataBind();
        pnlRefreesList.Visible = true;
    }

    public bool LoadRefrees(int applicationID, int type)
    {
        ApplicationID = applicationID;
        _Type = Convert.ToByte(type);

        Form.Form_RefreeDataTable table = Adapters.FrmRefAdapter.GetByApplication(ApplicationID, _Type);

        pnlRefreesInput.Visible = true;
        if (type != 0)
        {
            AddExtRev1.Visible = true;
            HRTableAdapters.EmployeeTableAdapter empAdapter = new HRTableAdapters.EmployeeTableAdapter();        



            AddExtRev1.LoadControl(applicationID, Convert.ToByte(type), empAdapter.GetApplicant(applicationID)[0].Department);

        }
        if (table.Count == 0)
            return false;

        ASP.controls_refreeinput_ascx[] inputs = new ASP.controls_refreeinput_ascx[10];
        inputs[0] = RefreeInput1;
        inputs[1] = RefreeInput2;
        inputs[2] = RefreeInput3;
        inputs[3] = RefreeInput4;
        inputs[4] = RefreeInput5;
        inputs[5] = RefreeInput6;
        inputs[6] = RefreeInput7;
        inputs[7] = RefreeInput8;
        inputs[8] = RefreeInput9;
        inputs[9] = RefreeInput10;

        for (int i = 0; i < table.Count; i++)
        {
            LoadRefreeInput(table[i], inputs[i]);
        }


        return true;
    }

    private static void LoadRefreeInput(Form.Form_RefreeRow row, ASP.controls_refreeinput_ascx input)
    {
        input.Ref_Name                  = row.IsNameNull() ?                 "" : row.Name.ToString();
        input.Ref_Rank                  = row.IsRankNull() ?                 "" : row.Rank.ToString();
        input.Ref_MailingAddress        = row.IsMailingAddressNull() ?       "" : row.MailingAddress.ToString();
        input.Ref_PhoneAndFax           = row.IsPhoneAndFaxNull() ?          "" : row.PhoneAndFax.ToString();
        input.Ref_EMail                 = row.IsEmailNull() ?                "" : row.Email.ToString().Trim();
        input.Ref_Major                 = row.IsMajorNull() ?                "" : row.Major.ToString();
        input.Ref_Speciality            = row.IsSpecialityNull() ?           "" : row.Speciality.ToString();
        input.Ref_ActiveAreaOfResearch  = row.IsActiveAreaOfResearchNull() ? "" : row.ActiveAreaOfResearch.ToString();        
        input.Ref_Webpage               = row.IsWebpageNull() ?              "" : row.Webpage.ToString();
        input.Ref_Comments              = row.IsCommentsNull() ?             "" : row.Comments.ToString();
        input.TotalPublications         = row.IsTotalPublicationsNull() ?    0  : row.TotalPublications;
        input.NoOfJournals              = row.IsNoOfJournalsNull() ?         0 : row.NoOfJournals;
        input.HIndex                    = row.IsHIndexNull() ?               0 : row.HIndex;
        input.Citations                 = row.IsCitationsNull() ?            0 : row.Citations;                
    }
    private static void LoadRefreeInput(string[] erData, ASP.controls_refreeinput_ascx input)
    {
        int dummy = 0;
        input.Ref_Name                  = erData.Length > 0 ? erData[0]         :   input.Ref_Name;
        input.Ref_Rank                  = erData.Length > 1 ? erData[1]         :   input.Ref_Rank                   ;
        input.Ref_MailingAddress        = erData.Length > 2 ? erData[2]         :   input.Ref_MailingAddress         ;
        input.Ref_PhoneAndFax           = erData.Length > 3 ? erData[3]         :   input.Ref_PhoneAndFax            ;
        input.Ref_EMail                 = erData.Length > 4 ? erData[4]         :   input.Ref_EMail.Trim()           ;
        input.Ref_Major                 = erData.Length > 5 ? erData[5]         :   input.Ref_Major                  ;
        input.Ref_Speciality            = erData.Length > 6 ? erData[6]         :   input.Ref_Speciality             ;
        input.Ref_ActiveAreaOfResearch  = erData.Length > 7 ? erData[7]         :   input.Ref_ActiveAreaOfResearch   ;      
        input.Ref_Webpage               = erData.Length > 8 ? erData[8]         :   input.Ref_Webpage                ;
        input.Ref_Comments              = erData.Length > 9 ? erData[9]         :   input.Ref_Comments               ;
        input.TotalPublications         = erData.Length > 10 ? Int32.TryParse(erData[10], out  dummy) ? dummy : input.TotalPublications : input.TotalPublications;
        input.NoOfJournals              = erData.Length > 11 ? Int32.TryParse(erData[11], out  dummy) ? dummy : input.NoOfJournals : input.NoOfJournals;
        input.HIndex                    = erData.Length > 12 ? Int32.TryParse(erData[12], out  dummy) ? dummy : input.HIndex : input.HIndex;
        input.Citations                 = erData.Length > 13 ? Int32.TryParse(erData[13], out  dummy) ? dummy : input.Citations:input.Citations                                    ;
    }

    public int SaveRefrees(int applicationID, byte type, out int numProfessors)
    {
     
        int refreesCount = 0;
        numProfessors = 0;

        Adapters.FrmRefAdapter.DeleteByApplication(applicationID, type);

        ASP.controls_refreeinput_ascx[] inputs = new ASP.controls_refreeinput_ascx[10];
        inputs[0] = RefreeInput1;
        inputs[1] = RefreeInput2;
        inputs[2] = RefreeInput3;
        inputs[3] = RefreeInput4;
        inputs[4] = RefreeInput5;
        inputs[5] = RefreeInput6;
        inputs[6] = RefreeInput7;
        inputs[7] = RefreeInput8;
        inputs[8] = RefreeInput9;
        inputs[9] = RefreeInput10;

        for (byte i = 0; i < inputs.Length; i++)
        {
            if (inputs[i].Ref_Name.Trim().Length > 0)
            {
                try
                {
                    Adapters.FrmRefAdapter.Insert(
                        applicationID,
                        type,
                        i,
                        inputs[i].Ref_Name.Trim().Replace("Dr. ",""),
                        inputs[i].Ref_Rank,
                        inputs[i].Ref_MailingAddress,
                        inputs[i].Ref_EMail.Trim(),
                        inputs[i].Ref_Major,
                        inputs[i].Ref_Speciality,
                        inputs[i].Ref_PhoneAndFax,
                        inputs[i].Ref_ActiveAreaOfResearch,
                        "", //prev. area of research omitted
                        inputs[i].Ref_Webpage,
                        inputs[i].Ref_Comments,
                        inputs[i].TotalPublications,
                        inputs[i].NoOfJournals,                        
                        inputs[i].HIndex,
                        inputs[i].Citations
                        );
                    if (inputs[i].Ref_Name != ""
                        && inputs[i].Ref_EMail != ""
                        && inputs[i].Ref_Major != ""
                        && inputs[i].Ref_Speciality != ""
                        && inputs[i].Ref_ActiveAreaOfResearch != "")
                    {
                        refreesCount++;
                    }
                    
                    if (inputs[i].Ref_Rank.ToLower() == "professor" ||
                        inputs[i].Ref_Rank.ToLower() == "reader")
                        numProfessors++;
                }
                catch
                {
                }
            }
        }

        return refreesCount;
    }

    public void SetPrinterFriendly(bool status)
    {
        dlRefrees.Visible  = status;
        grdRefrees.Visible = !status;

    }

    public int ApplicationID
    {
        get
        {
            if (hdnApplicationID.Value.Length == 0)
                return -1;
            return int.Parse(hdnApplicationID.Value);
        }
        set
        {
            hdnApplicationID.Value = value.ToString();
        }
    }


    public byte _Type
    {
        get
        {
            if (hdnType.Value.Length == 0)
            {
                return 0;
            }
            return byte.Parse(hdnType.Value);
        }
        set
        {
            hdnType.Value = value.ToString();
        }
    }

   
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

        Adapters.FrmRefAdapter.Delete(ApplicationID, _Type, 0);
        SerializeRecords();
        Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        
        Adapters.FrmRefAdapter.Delete(ApplicationID, _Type, 1);
        SerializeRecords();
        Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        
        Adapters.FrmRefAdapter.Delete(ApplicationID, _Type, 2);
        SerializeRecords();
        Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        
        Adapters.FrmRefAdapter.Delete(ApplicationID, _Type, 3);
        SerializeRecords();
        Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);
    }
    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        
        Adapters.FrmRefAdapter.Delete(ApplicationID, _Type, 4);
        SerializeRecords();
        Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        
        Adapters.FrmRefAdapter.Delete(ApplicationID, _Type, 5);
        SerializeRecords();
        Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);
    }
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        
        Adapters.FrmRefAdapter.Delete(ApplicationID, _Type, 6);
        SerializeRecords();
        Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);
    }
    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        
        Adapters.FrmRefAdapter.Delete(ApplicationID, _Type, 7);
        SerializeRecords();
        Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);
    }
    protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    {
        
        Adapters.FrmRefAdapter.Delete(ApplicationID, _Type, 8);
        SerializeRecords();
        Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);
    }
    protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
    {
        
        Adapters.FrmRefAdapter.Delete(ApplicationID, _Type, 9);
        SerializeRecords();
        Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);
    }
    public void SerializeRecords()
    {
        
        Form.Form_RefreeDataTable frmRfrDT = Adapters.FrmRefAdapter.GetByApplication(ApplicationID,_Type);
        byte count = 0;
        foreach (Form.Form_RefreeRow row in frmRfrDT)
        {
            Adapters.FrmRefAdapter.Delete(row.ApplicationID, row.Type, row.Serial);
            row.Serial = count++;
            Adapters.FrmRefAdapter.Insert(row.ApplicationID, row.Type, row.Serial,row.Name ,row.Rank ,row.MailingAddress,row.Email,
                row .Major, row.Speciality,row.PhoneAndFax, row.ActiveAreaOfResearch ,row.PrevAreaOfResearch,
                row.Webpage , row.Comments ,row.TotalPublications,row.NoOfJournals,row.HIndex, row.Citations);
        }
    }
    protected void btnFillTB_ServerClick(object sender, EventArgs e)
    {
        ASP.controls_refreeinput_ascx[] inputs = new ASP.controls_refreeinput_ascx[10];
        inputs[0] = RefreeInput1;
        inputs[1] = RefreeInput2;
        inputs[2] = RefreeInput3;
        inputs[3] = RefreeInput4;
        inputs[4] = RefreeInput5;
        inputs[5] = RefreeInput6;
        inputs[6] = RefreeInput7;
        inputs[7] = RefreeInput8;
        inputs[8] = RefreeInput9;
        inputs[9] = RefreeInput10;
        if(ddlExtRevRank.Value == "-1" )
        {return;}
        LoadRefreeInput(taExtRevData.InnerText.Split('\t','\r','\n'),inputs[Int32.Parse(ddlExtRevRank.Value)-1]);
        int numProfessor;
        SaveRefrees(ApplicationID, _Type, out numProfessor);
        Response.Redirect(Page.Request.Url.ToString() + "#" + _Type);
    }
}
