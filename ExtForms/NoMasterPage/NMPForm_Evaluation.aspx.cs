using System;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using BL.Data;
using System.Web.UI.WebControls;

public partial class ExtForms_NoMasterPage_NMPForm_Evaluation : Page
{
    ExtRevBAL erBAL = new ExtRevBAL();
    BAL bal = new BAL();
    public string dataPath;
    public FileInfo[] files;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;           
        }
        LoadControls();
    }

    private void LoadControls()
    {
        lblExtRev.InnerText = erBAL.GetExtRevByID(ExtReviewerID)[0].NameString;
        lblApplicant.InnerText = bal.GetApplicant(ApplicationID)[0].NameString + " ("+ bal.GetApplicant(ApplicationID)[0].Department1.ShortName + ")";
        var context = new FPSDBEntities();
        var questionsRecord = from rtfA in context.Evaluations
                              where
                                rtfA.ApplicationID == ApplicationID &&
                                rtfA.ExternalReviewerID == ExtReviewerID &&
                                rtfA.EmployeeID == "0" &&
                                rtfA.ExternalEmployeeID == 0 &&
                                rtfA.Lang == Language
                              select rtfA;

        LoadEvaluationAttachment();
        //if (questionsRecord.Count() == 0)
        //{


        //    var path = Server.MapPath(@"~/App_Code/json.json");
        //    using (StreamReader r = new StreamReader(path, false))
        //    {
        //        string json = r.ReadToEnd();
        //        var jsonData = JsonConvert.DeserializeObject<Rootobject>(json);
        //        var questions = jsonData.Question.Where(q => q.Lang == Language).ToList();
        //        if (questions.Count == 0)
        //        {
        //            divSubmit.Style["display"] = "none";
        //            return;
        //        }
        //        lblFormTitle1.InnerText = jsonData.FormTitle;
        //        for (int QNo = 1; QNo < questions.Count + 1; QNo++)
        //        {
        //            (FindControl("lblQ" + QNo + "Title") as HtmlGenericControl).InnerText = questions[QNo - 1].Title;
        //            (FindControl("lblQ" + QNo + "Text") as HtmlGenericControl).InnerText = questions[QNo - 1].Text;
        //            (FindControl("lblQ" + QNo + "AnswerInstruction") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerInstruction;
        //            if (questions[QNo - 1].AnswerOnSameLine == "yes")
        //            {
        //                (FindControl("divQ" + QNo + "Text") as HtmlGenericControl).Style.Add("float", "left");
        //                (FindControl("divQ" + QNo + "Text") as HtmlGenericControl).Style.Add("width", questions[QNo - 1].questionWidth);
        //            }
        //            else
        //            {
        //                (FindControl("divQ" + QNo + "Text") as HtmlGenericControl).Style.Remove("float");
        //                (FindControl("divQ" + QNo + "Text") as HtmlGenericControl).Style.Remove("width");
        //            }

        //            (FindControl("divQ" + QNo) as HtmlGenericControl).Visible = bool.Parse(questions[QNo - 1].visible);
        //            (FindControl("divQ" + QNo) as HtmlGenericControl).Style.Remove("dir");
        //            (FindControl("divQ" + QNo) as HtmlGenericControl).Style.Add("dir", questions[QNo - 1].Lang);
        //            if (bool.Parse(questions[QNo - 1].AnswerRadioButton.visibleRB))
        //            {
        //                var stringBuilder = new System.Text.StringBuilder();
        //                if (questions[QNo - 1].AnswerRadioButton.titleRB == "")
        //                {

        //                    (FindControl("lblQ" + QNo + "AnswerRB") as HtmlGenericControl).Visible = false;
        //                }
        //                else
        //                {
        //                    (FindControl("lblQ" + QNo + "AnswerRB") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerRadioButton.titleRB;

        //                }

        //                var licRB = questions[QNo - 1].AnswerRadioButton.optionRB;
        //                for (int i = 0; i < licRB.Length; i++)
        //                {
        //                    if (licRB[i]._checked == "true")
        //                    {
        //                        stringBuilder.Append(String.Format("<input type=\"radio\" name=\"Q" + (QNo) + "AnswerRBOption\" value='{0}' title='{1}'  checked=\"checked\" class=\"form - check - input\"/>{1}  <br />", licRB[i].value, licRB[i].text));
        //                    }
        //                    else
        //                    {
        //                        stringBuilder.Append(String.Format("<input type=\"radio\" name=\"Q" + (QNo) + "AnswerRBOption\" value='{0}' title='{1}'  />{1} <br />", licRB[i].value, licRB[i].text));
        //                    }
        //                }
        //                (FindControl("divQ" + QNo + "AnswerRBL") as HtmlGenericControl).InnerHtml = stringBuilder.ToString();
        //            }
        //            else
        //            {
        //                (FindControl("divQ" + QNo + "AnswerRBL") as HtmlGenericControl).Visible = false;
        //            }
        //            if (bool.Parse(questions[QNo - 1].AnswerCheckBox.visibleCB))
        //            {
        //                (FindControl("lblQ" + QNo + "AnswerCB") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerCheckBox.titleCB;

        //                var licCB = questions[QNo - 1].AnswerCheckBox.optionCB;
        //                var stringBuilder = new System.Text.StringBuilder();
        //                for (int i = 0; i < licCB.Length; i++)
        //                {
        //                    if (licCB[i]._checked == "true")
        //                    {
        //                        stringBuilder.Append(String.Format("<input type=\"checkbox\" name=\"Q" + (QNo) + "AnswerCBOption\" value='{0}' title='{1}'  checked=\"checked\" />{1} <br />", licCB[i].value, licCB[i].text));
        //                    }
        //                    else
        //                    {
        //                        stringBuilder.Append(String.Format("<input type=\"checkbox\" name=\"Q" + (QNo) + "AnswerCBOption\" value='{0}' title='{1}'  />{1} <br />", licCB[i].value, licCB[i].text));
        //                    }
        //                }
        //                (FindControl("divQ" + QNo + "AnswerCBL") as HtmlGenericControl).InnerHtml = stringBuilder.ToString();
        //            }
        //            else
        //            {
        //                (FindControl("divQ" + QNo + "AnswerCBL") as HtmlGenericControl).Visible = false;
        //            }
        //            if (bool.Parse(questions[QNo - 1].AnswerDropDownList.visibleDDl))
        //            {
        //                if (questions[QNo - 1].AnswerDropDownList.titleDDL == "")
        //                {

        //                    (FindControl("lblQ" + QNo + "AnswerDDL") as HtmlGenericControl).Visible = false;
        //                }
        //                else
        //                {
        //                    (FindControl("lblQ" + QNo + "AnswerDDL") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerDropDownList.titleDDL;

        //                }


        //                //(FindControl("lblQ" + QNo + "AnswerDDL") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerDropDownList.titleDDL;
        //                var licDDL = questions[QNo - 1].AnswerDropDownList.optionDDL;
        //                var builder = new System.Text.StringBuilder();
        //                for (int i = 0; i < licDDL.Length; i++)
        //                {
        //                    (FindControl("SelectQ" + QNo + "Answer") as HtmlSelect).Items.Add(new System.Web.UI.WebControls.ListItem(licDDL[i].text, licDDL[i].value));
        //                }

        //                if (questions[QNo - 1].AnswerDropDownList.required != "yes")
        //                {
        //                    (FindControl("SelectQ" + QNo + "Answer") as HtmlSelect).Attributes.Remove("required");
        //                }
        //                else
        //                {
        //                    (FindControl("SelectQ" + QNo + "Answer") as HtmlSelect).Attributes.Add("required", "required");
        //                }

        //            }
        //            else
        //            {
        //                (FindControl("divQ" + QNo + "AnswerDDL") as HtmlGenericControl).Visible = false;
        //            }
        //            if (bool.Parse(questions[QNo - 1].AnswerTA.visibleTA))
        //            {
        //                HtmlTextArea controlsTA = (FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea);
        //                if (questions[QNo - 1].AnswerTA.titleTA == "")
        //                {

        //                    (FindControl("lblQ" + QNo + "AnswerTA") as HtmlGenericControl).Visible = false;
        //                }
        //                else
        //                {
        //                    (FindControl("lblQ" + QNo + "AnswerTA") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerTA.titleTA;

        //                }
        //                //(FindControl("lblQ" + QNo + "AnswerTA") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerTA.titleTA;
        //                controlsTA.Attributes.Remove("placeholder");
        //                controlsTA.Attributes.Add("placeholder", questions[QNo - 1].AnswerTA.watermark);
        //                controlsTA.Attributes.Remove("required");
        //                if (questions[QNo - 1].AnswerTA.required == "yes")
        //                {
        //                    controlsTA.Attributes.Add("required","required");
        //                } 


        //                controlsTA.Attributes.Remove("title");
        //                controlsTA.Attributes.Add("title", questions[QNo - 1].AnswerTA.tooltip);
        //                controlsTA.Attributes.Remove("maxlength");
        //                controlsTA.Attributes.Add("maxlength", questions[QNo - 1].AnswerTA.maxlength);
        //                controlsTA.Attributes.Remove("minlength");
        //                controlsTA.Attributes.Add("minlength", questions[QNo - 1].AnswerTA.minlength);
        //                controlsTA.Style.Remove("height");
        //                controlsTA.Style.Add("height", questions[QNo - 1].AnswerTA.height);
        //                controlsTA.Style.Remove("width");
        //                controlsTA.Style.Add("width", questions[QNo - 1].AnswerTA.width);


        //                //var attr = questions[QNo - 1].AnswerTA.Attribute;
        //                //for (int i = 0; i < attr.Length; i++)
        //                //{
        //                //    (FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Attributes[attr[i].text] = attr[i].value;
        //                //}
        //                //var style = questions[QNo - 1].AnswerTA.Style;
        //                //for (int i = 0; i < style.Length; i++)
        //                //{
        //                //    (FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Style[style[i].text] = style[i].value;
        //                //}


        //                //    if (questions[QNo - 1].AnswerTA.required != "yes")
        //                //{
        //                //    (FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Attributes.Remove("required");
        //                //}
        //                //else
        //                //{
        //                //    (FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Attributes.Add("required", "required");
        //                //}
        //            }
        //            else
        //            {
        //                (FindControl("divQ" + QNo + "AnswerTA") as HtmlGenericControl).Visible = false;
        //            }
        //        }
        //    }
        //}
        //else

        if (questionsRecord.Count() != 0)
        {
            foreach (var q in questionsRecord)
            {
                (FindControl("lblQ" + q.QNo + "Title") as HtmlGenericControl).InnerText = q.Title;
                (FindControl("lblQ" + q.QNo + "Text") as HtmlGenericControl).InnerText = q.QText;
                (FindControl("divQ" + q.QNo) as HtmlGenericControl).Visible = q.Visible.Value;
                (FindControl("divQ" + q.QNo) as HtmlGenericControl).Style.Remove("dir");
                (FindControl("divQ" + q.QNo) as HtmlGenericControl).Style.Add("dir", q.Lang);

                (FindControl("lblQ" + q.QNo + "AnswerInstruction") as HtmlGenericControl).InnerText = q.AnswerInstruction;
                if (q.AnswerOnSameLine.Value)
                {
                    (FindControl("divQ" + q.QNo + "Text") as HtmlGenericControl).Style.Add("float", "left");
                    (FindControl("divQ" + q.QNo + "Text") as HtmlGenericControl).Style.Add("width", q.QuestionWidth);
                }
                else
                {
                    (FindControl("divQ" + q.QNo + "Text") as HtmlGenericControl).Style.Remove("float");
                    (FindControl("divQ" + q.QNo + "Text") as HtmlGenericControl).Style.Remove("width");
                }
                if (q.ARBVisible.Value)
                {
                    (FindControl("lblQ" + q.QNo + "AnswerRB") as HtmlGenericControl).InnerText = q.ARBTitle;

                    (FindControl("divQ" + q.QNo + "AnswerRBL") as HtmlGenericControl).InnerHtml = q.ARBOption
                        .Replace("checked=\"checked\"", "")
                        .Replace("value='" + q.ARBValue + "'", "value='" + q.ARBValue + "' checked=\"checked\"");
                }
                else
                {
                    (FindControl("divQ" + q.QNo + "ARB") as HtmlGenericControl).Visible = false;
                }
                if (q.ACBVisible.Value)
                {
                    q.ACBOption = q.ACBOption.Replace("checked=\"checked\"", "");
                    /*First Option*/
                    //string[] list = q.ACBValue.Split(',');
                    //for (int i = 0; i < list.Count(); i++)
                    //{
                    //    q.ACBOption = q.ACBOption.Replace("value='"+ list[i] + "' ", "value='" + list[i] + "' checked=\"checked\""); 
                    //}
                    /*Second Option*/
                    //q.ACBValue.Split(',').Select(s => new    {
                    //        Search = "value='" + s + "' ",
                    //        Replace = "value='" + s + "' checked=\"checked\""
                    //        }).ToList().ForEach(tuple =>q.ACBOption = q.ACBOption.Replace(tuple.Search, tuple.Replace));
                    /*Third Option*/
                    q.ACBOption = q.ACBValue.Split(',').Select(s => new
                    {
                        Search = "value='" + s + "' ",
                        Replace = "value='" + s + "' checked=\"checked\""
                    }).Aggregate(q.ACBOption, (res, tuple) => res.Replace(tuple.Search, tuple.Replace));

                    (FindControl("lblQ" + q.QNo + "AnswerCB") as HtmlGenericControl).InnerText = q.ACBTitle;

                    (FindControl("divQ" + q.QNo + "AnswerCBL") as HtmlGenericControl).InnerHtml = q.ACBOption;
                }
                else
                {
                    (FindControl("divQ" + q.QNo + "AnswerCB") as HtmlGenericControl).Visible = false;
                }
                if (q.ADDLVisible.Value)
                {
                    (FindControl("lblQ" + q.QNo + "AnswerDDL") as HtmlGenericControl).InnerText = q.ADDLTitle;

                    HtmlSelect selectControl = (FindControl("SelectQ" + q.QNo + "Answer") as HtmlSelect);

                    List<Optionddl> oddl = q.ADDLOption.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)
                       .Select(x => new Optionddl
                       {
                           text = x.Split(',')[0],
                           value = x.Split(',')[1]
                       })
                       .ToList<Optionddl>();
                    for (int i = 0; i < oddl.Count; i++)
                    {
                        selectControl.Items.Add(new System.Web.UI.WebControls.ListItem(oddl[i].text, oddl[i].value));
                    }
                    //  (FindControl("SelectQ" + q.QNo + "Answer") as HtmlSelect).Items.AddRange(oddl.Cast<Optionddl>().ToArray());
                    //  (FindControl("SelectQ" + q.QNo + "Answer") as HtmlSelect).Datasource
                    //(FindControl("SelectQ" + q.QNo + "Answer") as HtmlSelect).DataBind();



                    if (!q.ADDLRequired.Value)
                    {
                        selectControl.Attributes.Remove("required");
                    }
                    else
                    {
                        selectControl.Attributes.Add("required", "required");
                    }
                    selectControl.SelectedIndex = selectControl.Items.IndexOf(selectControl.Items.FindByValue(q.ADDLValue));

                }
                else
                {

                    (FindControl("divQ" + q.QNo + "AnswerDDL") as HtmlGenericControl).Visible = false;
                }
                if (q.ATAVisible.Value)
                {
                    (FindControl("lblQ" + q.QNo + "AnswerTA") as HtmlGenericControl).InnerText = q.ATATitle;

                    (FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).InnerText = q.ATAValue;

                    List<Attribute> attl = q.ATAAttribute.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(x => new Attribute
                      {
                          text = x.Split(',')[0],
                          value = x.Split(',')[1]
                      })
                      .ToList<Attribute>();
                    for (int i = 0; i < attl.Count; i++)
                    {
                        (FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).Attributes[attl[i].text] = attl[i].value;
                    }

                    List<Style> styl = q.ATAAttribute.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(x => new Style
                      {
                          text = x.Split(',')[0],
                          value = x.Split(',')[1]
                      })
                      .ToList<Style>();
                    for (int i = 0; i < styl.Count; i++)
                    {
                        (FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).Style[styl[i].text] = styl[i].value;
                    }


                    //var attr = questions[QNo - 1].AnswerTA.Attribute;
                    //for (int i = 0; i < attr.Length; i++)
                    //{
                    //    (FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Attributes[attr[i].text] = attr[i].value;
                    //}
                    //var style = questions[QNo - 1].AnswerTA.Style;
                    //for (int i = 0; i < style.Length; i++)
                    //{
                    //    (FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).Style[style[i].text] = style[i].value;
                    //}
                    if (!q.ATARequired.Value)
                    {
                        (FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).Attributes.Remove("required");
                    }
                    else
                    {
                        (FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).Attributes.Add("required", "required");
                    }
                }
                else
                {
                    (FindControl("div" + q.QNo + "AnswerTA") as HtmlGenericControl).Visible = false;
                }
            }
        }
    }

    private void LoadEvaluationAttachment()
    {
        GetEvaluationAttachments(out dataPath, out files);
        if (files.Count() != 0)
        {
            lbtnEvaluationAttachment.Text = files[0].Name.Replace(ApplicationID + "_" + ExtReviewerID + "_", "");
         //   btnDelEvaluationAttachment.Visible = true;
            lbtnEvaluationAttachment.Visible = true;
        }
        else
        {
        //    btnDelEvaluationAttachment.Visible = false;
            lbtnEvaluationAttachment.Visible = false;
        }
    }

    private void GetEvaluationAttachments(out string dataPath, out FileInfo[] files)
    {
       
        dataPath = Server.MapPath("~/App_Data/EvaluationAttachments/");
        DirectoryInfo folder = new DirectoryInfo(dataPath);
        files = folder.GetFiles(ApplicationID + "_" + ExtReviewerID + "_*", SearchOption.AllDirectories);
    }

  
    protected void lbtnEvaluationAttachment_Click(object sender, EventArgs e)
    {
        GetEvaluationAttachments(out dataPath, out files);
        FileStream stream = System.IO.File.OpenRead(files[0].FullName);
        long length = stream.Length;
        stream.Close();
        Response.Clear();
        try
        {
            Response.Charset = "UTF-8";
            Response.AddHeader("Content-Disposition", "attachment; filename= " + files[0].Name);
            Response.AddHeader("Content-Length", "" + length);
            Response.WriteFile(files[0].FullName);
            Response.Flush();
            Response.Close();
        }
        catch
        {
        }
    }
    //protected void btnDelEvaluationAttachment_Click(object sender, EventArgs e)
    //{
    //    GetEvaluationAttachments(out dataPath, out files); 
    //    System.IO.File.Delete(files[0].FullName);
    //    LoadEvaluationAttachment();
    //}
    #region Properties
    public int ApplicationID
    {
        set
        {
            ViewState["ApplicationID"] = value;
        }
        get
        {
            if (ViewState["ApplicationID"] != null)
            {
                return (int)ViewState["ApplicationID"];
            }
            else 
            {
                return int.Parse(Request.QueryString["appID"]);
            }
           
        }
    }
    public int ExtReviewerID
    {
        set
        {
            ViewState["ExtReviewerID"] = value;
        }
        get
        {
            if (ViewState["ExtReviewerID"] != null)
            {
                return (int)ViewState["ExtReviewerID"];
            }
            else
            {
                return int.Parse(Request.QueryString["extRevID"]);
            }
        }
    }
    public string Language
    {
        set
        {
            Session["Language"] = value;
        }
        get
        {
            if (Session["Language"] != null)
            {
                return Session["Language"].ToString();
            }
            else
            {
                return "en-US";
            }
        }
    }
    #endregion


}