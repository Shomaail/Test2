using System;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Configuration;
using BL.Data;
using System.Web.Services;

public partial class ExtForms_Form_Evaluation : Page
{
    ExtRevBAL erBAL = new ExtRevBAL();
    ExtRevFormBAL erfBAL = new ExtRevFormBAL();
    int score = 0;
    BAL bal = new BAL();
    public string dataPath;
    public FileInfo[] files;
    //protected override void InitializeCulture()
    //{
    //    if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("ddlLang"))
    //    {
    //        UICulture = Request.Form[Request.Form["__EVENTTARGET"]];
    //        Culture = Request.Form[Request.Form["__EVENTTARGET"]];

    //        Thread.CurrentThread.CurrentCulture =
    //            CultureInfo.CreateSpecificCulture(Request.Form[Request.Form["__EVENTTARGET"]]);
    //        Thread.CurrentThread.CurrentUICulture = new
    //            CultureInfo(Request.Form[Request.Form["__EVENTTARGET"]]);
    //        LoadControls();
    //    }
    //    base.InitializeCulture();
        //else
        //{
        //    UICulture = Master.Language;
        //    Culture = Master.Language;
        //    Thread.CurrentThread.CurrentCulture =  CultureInfo.CreateSpecificCulture(Master.Language);
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(Master.Language);

        //}
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("ddlLang"))
        //{
        //    //UICulture = Request.Form[Request.Form["__EVENTTARGET"]];
        //    //Culture = Request.Form[Request.Form["__EVENTTARGET"]];

        //    //Thread.CurrentThread.CurrentCulture =
        //    //    CultureInfo.CreateSpecificCulture(Request.Form[Request.Form["__EVENTTARGET"]]);
        //    //Thread.CurrentThread.CurrentUICulture = new
        //    //    CultureInfo(Request.Form[Request.Form["__EVENTTARGET"]]);
        //    LoadControls();
        //    return;
        //}

        if (IsPostBack)
        {
            return;           
        }

        //if (Session["ApplicationID"] == null)
        //{
        //    Response.Redirect("ExtMain.aspx");
        //}
        //if (Session["ExtRevID"] == null)
        //{
        //    FormsAuthentication.SignOut();
        //    Session.Clear();
        //    Response.Redirect("../Login.aspx");
        //}        
        LoadControls();
    }

    private void LoadControls()
    {
        
           Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(ApplicationID, ExtReviewerID)[0];
        if (ffer.EvaluationStatus == EvaluationStatus.Submitted.ToString())
        {
            divSubmit.Style["display"] = "none";
            EvaluationAttachment.Visible = false;
        }
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

        if (questionsRecord.Count() == 0)
        {
            //(Master.FindControl("MainContent").FindControl("Q1AnswerTA") as HtmlTextArea).InnerText = erBAL.GetExtRevByID(ExtReviewerID)[0].Name;
            //(Master.FindControl("MainContent").FindControl("Q2AnswerTA") as HtmlTextArea).InnerText = erBAL.GetExtRevByID(ExtReviewerID)[0].MailingAddress;
            //(Master.FindControl("MainContent").FindControl("Q3AnswerTA") as HtmlTextArea).InnerText = erBAL.GetExtRevByID(ExtReviewerID)[0].PassportNo;
            //(Master.FindControl("MainContent").FindControl("Q4AnswerTA") as HtmlTextArea).InnerText = erBAL.GetExtRevByID(ExtReviewerID)[0].IBAN;
            var path = "";
            if (bal.GetApplication(Master.ApplicationID)[0].ForRank == RankProfessorial.Associate_Professor.ToString().Replace("_"," "))
            {
                path = Server.MapPath(@"~/App_Code/BL/AssociateProfessor.json");
            }
            else
            {
                path = Server.MapPath(@"~/App_Code/BL/Professor.json");
            }
            
            using (StreamReader r = new StreamReader(path, false))
            {
                string json = r.ReadToEnd();
                var jsonData = JsonConvert.DeserializeObject<Rootobject>(json);
                var questions = jsonData.Question.Where(q => q.Lang == Language).ToList();
                if (questions.Count == 0)
                {
                    divSubmit.Style["display"] = "none";
                    divEvaluationAttachment.Style["display"] = "none";
                    lblFormTitle1.InnerText = "No Arabic form is found";
                    return;
                }
                else
                {
                    lblFormTitle1.InnerText = jsonData.FormTitle;
                }
                
                for (int QNo = 1; QNo < questions.Count + 1; QNo++)
                {
                    (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "Title") as HtmlGenericControl).InnerText = questions[QNo - 1].Title;
                    (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "Text") as HtmlGenericControl).InnerText = questions[QNo - 1].Text;
                    (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerInstruction") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerInstruction;
                    if (questions[QNo - 1].AnswerOnSameLine == "yes")
                    {
                        (Master.FindControl("MainContent").FindControl("divQ" + QNo + "Text") as HtmlGenericControl).Style.Add("float", "left");
                        (Master.FindControl("MainContent").FindControl("divQ" + QNo + "Text") as HtmlGenericControl).Style.Add("width", questions[QNo - 1].questionWidth);
                    }
                    else
                    {
                        (Master.FindControl("MainContent").FindControl("divQ" + QNo + "Text") as HtmlGenericControl).Style.Remove("float");
                        (Master.FindControl("MainContent").FindControl("divQ" + QNo + "Text") as HtmlGenericControl).Style.Remove("width");
                    }

                    (Master.FindControl("MainContent").FindControl("divQ" + QNo) as HtmlGenericControl).Visible = bool.Parse(questions[QNo - 1].visible);
                    (Master.FindControl("MainContent").FindControl("divQ" + QNo) as HtmlGenericControl).Style.Remove("dir");
                    (Master.FindControl("MainContent").FindControl("divQ" + QNo) as HtmlGenericControl).Style.Add("dir", questions[QNo - 1].Lang);
                    if (bool.Parse(questions[QNo - 1].AnswerRadioButton.visibleRB))
                    {
                        var stringBuilder = new System.Text.StringBuilder();
                        if (questions[QNo - 1].AnswerRadioButton.titleRB == "")
                        {
                            (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerRB") as HtmlGenericControl).Visible = false;
                        }
                        else
                        {
                            (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerRB") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerRadioButton.titleRB;
                        }

                        var licRB = questions[QNo - 1].AnswerRadioButton.optionRB;
                        for (int i = 0; i < licRB.Length; i++)
                        {
                            if (licRB[i]._checked == "true")
                            {
                                stringBuilder.Append(string.Format("<input type=\"radio\" name=\"Q" + (QNo) + "AnswerRBOption\" value='{0}' title='{1}'  checked=\"checked\" class=\"form - check - input\"/>{1}  <br />", licRB[i].value, licRB[i].text));
                            }
                            else
                            {
                                stringBuilder.Append(string.Format("<input type=\"radio\" name=\"Q" + (QNo) + "AnswerRBOption\" value='{0}' title='{1}'  />{1} <br />", licRB[i].value, licRB[i].text));
                            }
                        }
                        (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerRBL") as HtmlGenericControl).InnerHtml = stringBuilder.ToString();
                    }
                    else
                    {
                        (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerRBL") as HtmlGenericControl).Visible = false;
                    }
                    if (bool.Parse(questions[QNo - 1].AnswerCheckBox.visibleCB))
                    {
                        (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerCB") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerCheckBox.titleCB;

                        var licCB = questions[QNo - 1].AnswerCheckBox.optionCB;
                        var stringBuilder = new System.Text.StringBuilder();
                        for (int i = 0; i < licCB.Length; i++)
                        {
                            if (licCB[i]._checked == "true")
                            {
                                stringBuilder.Append(string.Format("<input type=\"checkbox\" name=\"Q" + (QNo) + "AnswerCBOption\" value='{0}' title='{1}'  checked=\"checked\" />{1} <br />", licCB[i].value, licCB[i].text));
                            }
                            else
                            {
                                stringBuilder.Append(string.Format("<input type=\"checkbox\" name=\"Q" + (QNo) + "AnswerCBOption\" value='{0}' title='{1}'  />{1} <br />", licCB[i].value, licCB[i].text));
                            }
                        }
                        (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerCBL") as HtmlGenericControl).InnerHtml = stringBuilder.ToString();
                    }
                    else
                    {
                        (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerCBL") as HtmlGenericControl).Visible = false;
                    }
                    if (bool.Parse(questions[QNo - 1].AnswerDropDownList.visibleDDl))
                    {
                        if (questions[QNo - 1].AnswerDropDownList.titleDDL == "")
                        {

                            (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerDDL") as HtmlGenericControl).Visible = false;
                        }
                        else
                        {
                            (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerDDL") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerDropDownList.titleDDL;

                        }


                        //(Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerDDL") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerDropDownList.titleDDL;
                        var licDDL = questions[QNo - 1].AnswerDropDownList.optionDDL;
                        var builder = new System.Text.StringBuilder();
                        for (int i = 0; i < licDDL.Length; i++)
                        {
                            (Master.FindControl("MainContent").FindControl("SelectQ" + QNo + "Answer") as HtmlSelect).Items.Add(new System.Web.UI.WebControls.ListItem(licDDL[i].text, licDDL[i].value));
                        }

                        if (questions[QNo - 1].AnswerDropDownList.required != "yes")
                        {
                            (Master.FindControl("MainContent").FindControl("SelectQ" + QNo + "Answer") as HtmlSelect).Attributes.Remove("required");
                        }
                        else
                        {
                            (Master.FindControl("MainContent").FindControl("SelectQ" + QNo + "Answer") as HtmlSelect).Attributes.Add("required", "required");
                        }

                    }
                    else
                    {
                        (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerDDL") as HtmlGenericControl).Visible = false;
                    }
                    if (bool.Parse(questions[QNo - 1].AnswerTA.visibleTA))
                    {
                        HtmlTextArea controlsTA = (Master.FindControl("MainContent").FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea);
                        if (questions[QNo - 1].AnswerTA.titleTA == "")
                        {

                            (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerTA") as HtmlGenericControl).Visible = false;
                        }
                        else
                        {
                            (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerTA") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerTA.titleTA;

                        }
                        //(Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerTA") as HtmlGenericControl).InnerText = questions[QNo - 1].AnswerTA.titleTA;
                        controlsTA.Attributes.Remove("placeholder");
                        controlsTA.Attributes.Add("placeholder", questions[QNo - 1].AnswerTA.watermark);
                        controlsTA.Attributes.Remove("required");
                        if (questions[QNo - 1].AnswerTA.required == "yes")
                        {
                            controlsTA.Attributes.Add("required","required");
                        } 


                        controlsTA.Attributes.Remove("title");
                        controlsTA.Attributes.Add("title", questions[QNo - 1].AnswerTA.tooltip);
                        controlsTA.Attributes.Remove("maxlength");
                        controlsTA.Attributes.Add("maxlength", questions[QNo - 1].AnswerTA.maxlength);
                        controlsTA.Attributes.Remove("minlength");
                        controlsTA.Attributes.Add("minlength", questions[QNo - 1].AnswerTA.minlength);
                        controlsTA.Style.Remove("height");
                        controlsTA.Style.Add("height", questions[QNo - 1].AnswerTA.height);
                        controlsTA.Style.Remove("width");
                        controlsTA.Style.Add("width", questions[QNo - 1].AnswerTA.width);


                        //var attr = questions[QNo - 1].AnswerTA.Attribute;
                        //for (int i = 0; i < attr.Length; i++)
                        //{
                        //    (Master.FindControl("MainContent").FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Attributes[attr[i].text] = attr[i].value;
                        //}
                        //var style = questions[QNo - 1].AnswerTA.Style;
                        //for (int i = 0; i < style.Length; i++)
                        //{
                        //    (Master.FindControl("MainContent").FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Style[style[i].text] = style[i].value;
                        //}


                        //    if (questions[QNo - 1].AnswerTA.required != "yes")
                        //{
                        //    (Master.FindControl("MainContent").FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Attributes.Remove("required");
                        //}
                        //else
                        //{
                        //    (Master.FindControl("MainContent").FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Attributes.Add("required", "required");
                        //}
                    }
                    else
                    {
                        (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerTA") as HtmlGenericControl).Visible = false;
                    }
                }
            }
        }
        else
        {
         
            
            foreach (var q in questionsRecord)
            {
                (Master.FindControl("MainContent").FindControl("lblQ" + q.QNo + "Title") as HtmlGenericControl).InnerText = q.Title;
                (Master.FindControl("MainContent").FindControl("lblQ" + q.QNo + "Text") as HtmlGenericControl).InnerText = q.QText;
                (Master.FindControl("MainContent").FindControl("divQ" + q.QNo) as HtmlGenericControl).Visible = q.Visible.Value;
                (Master.FindControl("MainContent").FindControl("divQ" + q.QNo) as HtmlGenericControl).Style.Remove("dir");
                (Master.FindControl("MainContent").FindControl("divQ" + q.QNo) as HtmlGenericControl).Style.Add("dir", q.Lang);

                (Master.FindControl("MainContent").FindControl("lblQ" + q.QNo + "AnswerInstruction") as HtmlGenericControl).InnerText = q.AnswerInstruction;
                if (q.AnswerOnSameLine.Value)
                {
                    (Master.FindControl("MainContent").FindControl("divQ" + q.QNo + "Text") as HtmlGenericControl).Style.Add("float", "left");
                    (Master.FindControl("MainContent").FindControl("divQ" + q.QNo + "Text") as HtmlGenericControl).Style.Add("width", q.QuestionWidth);
                }
                else
                {
                    (Master.FindControl("MainContent").FindControl("divQ" + q.QNo + "Text") as HtmlGenericControl).Style.Remove("float");
                    (Master.FindControl("MainContent").FindControl("divQ" + q.QNo + "Text") as HtmlGenericControl).Style.Remove("width");
                }
                if (q.ARBVisible.Value)
                {
                    (Master.FindControl("MainContent").FindControl("lblQ" + q.QNo + "AnswerRB") as HtmlGenericControl).InnerText = q.ARBTitle;

                    (Master.FindControl("MainContent").FindControl("divQ" + q.QNo + "AnswerRBL") as HtmlGenericControl).InnerHtml = q.ARBOption
                        .Replace("checked=\"checked\"", "")
                        .Replace("value='" + q.ARBValue + "'", "value='" + q.ARBValue + "' checked=\"checked\"");
                    int qScore = 0;
                    if(int.TryParse(q.ARBValue,out qScore))
                    {
                        score = score + qScore;
                    }
                    

                }
                else
                {
                    (Master.FindControl("MainContent").FindControl("divQ" + q.QNo + "ARB") as HtmlGenericControl).Visible = false;
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

                    (Master.FindControl("MainContent").FindControl("lblQ" + q.QNo + "AnswerCB") as HtmlGenericControl).InnerText = q.ACBTitle;

                    (Master.FindControl("MainContent").FindControl("divQ" + q.QNo + "AnswerCBL") as HtmlGenericControl).InnerHtml = q.ACBOption;
                }
                else
                {
                    (Master.FindControl("MainContent").FindControl("divQ" + q.QNo + "AnswerCB") as HtmlGenericControl).Visible = false;
                }
                if (q.ADDLVisible.Value)
                {
                    (Master.FindControl("MainContent").FindControl("lblQ" + q.QNo + "AnswerDDL") as HtmlGenericControl).InnerText = q.ADDLTitle;

                    HtmlSelect selectControl = (Master.FindControl("MainContent").FindControl("SelectQ" + q.QNo + "Answer") as HtmlSelect);

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
                    //  (Master.FindControl("MainContent").FindControl("SelectQ" + q.QNo + "Answer") as HtmlSelect).Items.AddRange(oddl.Cast<Optionddl>().ToArray());
                    //  (Master.FindControl("MainContent").FindControl("SelectQ" + q.QNo + "Answer") as HtmlSelect).Datasource
                    //(Master.FindControl("MainContent").FindControl("SelectQ" + q.QNo + "Answer") as HtmlSelect).DataBind();



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

                    (Master.FindControl("MainContent").FindControl("divQ" + q.QNo + "AnswerDDL") as HtmlGenericControl).Visible = false;
                }
                if (q.ATAVisible.Value)
                {
                    (Master.FindControl("MainContent").FindControl("lblQ" + q.QNo + "AnswerTA") as HtmlGenericControl).InnerText = q.ATATitle;

                    (Master.FindControl("MainContent").FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).InnerText = q.ATAValue;

                    List<Attribute> attl = q.ATAAttribute.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(x => new Attribute
                      {
                          text = x.Split(',')[0],
                          value = x.Split(',')[1]
                      })
                      .ToList();
                    for (int i = 0; i < attl.Count; i++)
                    {
                        (Master.FindControl("MainContent").FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).Attributes[attl[i].text] = attl[i].value;
                    }

                    List<Style> styl = q.ATAAttribute.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(x => new Style
                      {
                          text = x.Split(',')[0],
                          value = x.Split(',')[1]
                      })
                      .ToList();
                    for (int i = 0; i < styl.Count; i++)
                    {
                        (Master.FindControl("MainContent").FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).Style[styl[i].text] = styl[i].value;
                    }


                    //var attr = questions[QNo - 1].AnswerTA.Attribute;
                    //for (int i = 0; i < attr.Length; i++)
                    //{
                    //    (Master.FindControl("MainContent").FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Attributes[attr[i].text] = attr[i].value;
                    //}
                    //var style = questions[QNo - 1].AnswerTA.Style;
                    //for (int i = 0; i < style.Length; i++)
                    //{
                    //    (Master.FindControl("MainContent").FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).Style[style[i].text] = style[i].value;
                    //}
                    if (!q.ATARequired.Value)
                    {
                        (Master.FindControl("MainContent").FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).Attributes.Remove("required");
                    }
                    else
                    {
                        (Master.FindControl("MainContent").FindControl("Q" + q.QNo + "AnswerTA") as HtmlTextArea).Attributes.Add("required", "required");
                    }
                   
                }
                else
                {
                    (Master.FindControl("MainContent").FindControl("div" + q.QNo + "AnswerTA") as HtmlGenericControl).Visible = false;
                }
            }
            
        }
      //  tbTotalScore.Text = score.ToString();
       // ffer.Score = score;
        erBAL.UpdateFormFinalExtRev(ffer.ApplicationID, ffer.ExternalReviewerID, ffer.Serial, ffer.WLStatus, ffer.WLDate, ffer.CommentsWithWL, ffer.MaterialSentStatus, ffer.MaterialSentDate
          , ffer.EvaluationStatus,ffer.EvaluationDate, ffer.CommentsWithEval, ffer.ShowExtRev2PC, ffer.ShowEval2PC, ffer.UserName, ffer.Password, ffer.Source
          , ffer.EvaluationID);
    }

    private void LoadEvaluationAttachment()
    {
        dataPath = Server.MapPath("~/App_Data/EvaluationAttachments/");
        erfBAL.GetEvaluationAttachments(ApplicationID,ExtReviewerID, dataPath, out files);
        if (files.Count() != 0)
        {
            lbtnEvaluationAttachment.Text = files[0].Name.Replace(ApplicationID + "_" + ExtReviewerID + "_", "");
            btnDelEvaluationAttachment.Visible = true;
            lbtnEvaluationAttachment.Visible = true;
        }
        else
        {
            btnDelEvaluationAttachment.Visible = false;
            lbtnEvaluationAttachment.Visible = false;
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Save();
        Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID, Master.ExtReviewerID)[0];
        string emailAddress = "";
        string body = "";
        foreach (Task_ExtMessages row in bal.GetExtTaskMessage((int)TaskExtID.Submit_External_Evaluation))
        {
            row.Message = row.Message.Replace("@@RecipientName@@", ffer.ExternalReviewer.NameString);
            row.Message = row.Message.Replace("@@TopAuthority@@", bal.GetEmployeeByAppRole(Master.ApplicationID, (byte)RoleID.TopAuthority)[0].NameString);
            row.Message = row.Message.Replace("@@TopAuthority_Title@@", ConfigurationManager.AppSettings["TopAuthority_Title"]);
            row.Message = row.Message.Replace("@@TopAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopAuthority_TitleShort"]);         
            row.Message = row.Message.Replace("@@Applicant@@", bal.GetApplicant(Master.ApplicationID)[0].NameString);
            row.Message = row.Message.Replace("@@SendersName@@", ffer.ExternalReviewer.NameString);
            if (row.Subject == "External Evaluation Completed")
            {
                emailAddress = bal.GetEmployeeByAppRole(Master.ApplicationID, (byte)RoleID.TopAuthority)[0].Email
                    + ";"+ bal.GetDepartmentBySN(DepartmentID.VPRI.ToString())[0].DeputyEmail;
            }
            else
            {

                body = row.Message;
                emailAddress = ffer.ExternalReviewer.Email;
            }
            Emailer.Send(emailAddress
               , row.Subject
               , row.Message, "AutoEmailer", Master.ApplicationID);
        }
        ffer.EvaluationStatus = EvaluationStatus.Submitted.ToString();
        ffer.EvaluationDate = DateTime.Now;
        ffer.CommentsWithEval = "";

        bal.InsertAppLgExt(ffer.ApplicationID, "External Evaluation End (" + ffer.ExternalReviewerID + ")", (int)TaskExtID.External_Evaluation
       , ffer.ExternalReviewer.Name, body, DateTime.Now);
        bal.InsertApplication_Log(Master.ApplicationID, null, DateTime.Now
                        , body, ""
                        , "External Evaluation End (" + Master.ExtReviewerID + ")"
                        , erBAL.GetExtRevByID(Master.ExtReviewerID)[0].NameString);

        erBAL.UpdateFormFinalExtRev(ffer.ApplicationID, ffer.ExternalReviewerID, ffer.Serial, ffer.WLStatus, ffer.WLDate, ffer.CommentsWithWL, ffer.MaterialSentStatus, ffer.MaterialSentDate
            , EvaluationStatus.Submitted.ToString(), DateTime.Now, ffer.CommentsWithEval, ffer.ShowExtRev2PC, ffer.ShowEval2PC, ffer.UserName, ffer.Password, ffer.Source
            ,ffer.EvaluationID);
        Application_TaskLogExt aptle = bal.GetAppTaskLogExt(ffer.ApplicationID, (int)TaskExtID.External_Evaluation, ffer.ExternalReviewerID)[0];
        bal.UpdateAppTskLgExt(ffer.ApplicationID, aptle.TaskID, null, DateTime.Now, true, aptle.Reminders, aptle.EmailAddress, aptle.ExternalReviewerID, aptle.Message,aptle.EmployeeID,aptle.ExternalEmployeeID);
        //Session["Message"] = "Your Evaluation is successfully submitted.";
        labelProgrammaticPopup0.Text = "Your Evaluation is successfully submitted.";
        programmaticModalPopup0.Show();
        LoadControls();
        ShowMessage("Your Evaluation is successfully submitted.");

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      //  Validate();
        if (Save())
        {
            labelProgrammaticPopup0.Text = "Your Evaluation is saved successfully but not submitted.";
            programmaticModalPopup0.Show();
            ShowMessage("Your Evaluation is saved successfully but not submitted.");
        }
        else
        {
            labelProgrammaticPopup0.Text = "Evaluation Form is incomplete, Please complete the form by providing proper justification for each answer";
            programmaticModalPopup0.Show();
            ShowError("Evaluation Form is incomplete, Please complete the form by providing proper justification for each answer");
        }
        Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(ApplicationID, ExtReviewerID)[0];
       var context = new FPSDBEntities();
       var query= (from rtfA in context.Evaluations
                               where
                                 rtfA.ApplicationID == ApplicationID &&
                                 rtfA.ExternalReviewerID == ExtReviewerID &&
                                 rtfA.EmployeeID == "0" &&
                                 rtfA.ExternalEmployeeID == 0 &&
                                 rtfA.Lang == Language
                               select rtfA).ToList(); 
        
        foreach(var q in query)
        {
            if (q.ARBTitle == "Promotability")
            {
            //    ffer.PromotionRecom = q.ARBValue == "Promotable";
            }
        }     
        erBAL.UpdateFormFinalExtRev(ffer.ApplicationID, ffer.ExternalReviewerID, ffer.Serial, ffer.WLStatus, ffer.WLDate, ffer.CommentsWithWL, ffer.MaterialSentStatus, ffer.MaterialSentDate
            , EvaluationStatus.Saved_But_Not_Submitted.ToString().Replace("_"," "), DateTime.Now, ffer.CommentsWithEval, ffer.ShowExtRev2PC, ffer.ShowEval2PC, ffer.UserName, ffer.Password, ffer.Source
            , ffer.EvaluationID);

        LoadControls();

    }
    private bool Save()
    {
        var path = "";
        if (bal.GetApplication(Master.ApplicationID)[0].ForRank == RankProfessorial.Associate_Professor.ToString().Replace("_", " "))
        {
            path = Server.MapPath(@"~/App_Code/BL/AssociateProfessor.json");
        }
        else
        {
            path = Server.MapPath(@"~/App_Code/BL/Professor.json");
        }

        using (StreamReader r = new StreamReader(path, false))
        {
            bool isFormComplete = true;
            var context = new FPSDBEntities();
            string json = r.ReadToEnd();
            var jsonData = JsonConvert.DeserializeObject<Rootobject>(json);
            var questions = jsonData.Question;
            var query = from rtfA in context.Evaluations
                        where
                          rtfA.ApplicationID == ApplicationID
                          && rtfA.ExternalReviewerID == ExtReviewerID
                          && rtfA.Lang == Language
                        select rtfA;
            foreach (var row in query)
            {
                context.Evaluations.Remove(row);
            }
            context.SaveChanges();
            //ExternalReviewer1 er = erBAL.GetExtRevByID(ExtReviewerID)[0];
            //erBAL.UpdateExtRev(er.ExternalReviewerID, ((Master.FindControl("MainContent").FindControl("Q1AnswerTA")) as HtmlTextArea).Value
            //    , er.Rank, ((Master.FindControl("MainContent").FindControl("Q2AnswerTA")) as HtmlTextArea).Value, er.Email, er.Major, er.Specialty
            //    , er.PhoneAndFax, er.ActiveAreaOfResearch, er.PrevAreaOfResearch, er.Webpage, er.Comments, er.TotalPublications, er.NoOfJournals, er.HIndex, er.Citations
            //    , ((Master.FindControl("MainContent").FindControl("Q3AnswerTA")) as HtmlTextArea).Value
            //    , ((Master.FindControl("MainContent").FindControl("Q2AnswerTA")) as HtmlTextArea).Value);

            string strDDLItems, strAttribute, strStyle;
            for (int QNo = 1; QNo < questions.Length + 1; QNo++)
            {
                strDDLItems = ""; strAttribute = ""; strStyle = "";
                System.Web.UI.WebControls.ListItemCollection lic = (Master.FindControl("MainContent").FindControl("SelectQ" + QNo + "Answer") as HtmlSelect).Items;
                for (int i = 0; i < lic.Count; i++)
                {
                    strDDLItems += lic[i].Text + "," + lic[i].Value + "|";
                }
                var textAreaControl = (Master.FindControl("MainContent").FindControl("Q" + QNo + "AnswerTA")) as HtmlTextArea;
                //string str = control.Attributes.Keys.Select(key => key + "," + control.Attributes[key] + "|")
                //                    .Aggregate(string.Empty, (c, n) => c + n);
                string mL = (Master.FindControl("MainContent").FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Attributes["minlength"];
                if (textAreaControl.Attributes["required"] == "required"
                    && (Request.Form["ctl00$MainContent$Q" + QNo + "AnswerTA"].Length < int.Parse(mL)))
                {
                    textAreaControl.Style["border"] = "solid #d9534f";
                    isFormComplete = false;
                }
                else
                {
                    textAreaControl.Style.Remove("border");
                }
                foreach (string key in textAreaControl.Attributes.Keys)
                {
                    strAttribute += key + "," + textAreaControl.Attributes[key] + "|";
                }
                foreach (string key in textAreaControl.Style.Keys)
                {
                    strStyle += key + "," + textAreaControl.Style[key] + "|";
                }

                try
                {
                    context.Evaluations.Add(new Evaluation
                    {
                        QNo = QNo,
                        Title = (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "Title") as HtmlGenericControl).InnerText,
                        QText = (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "Text") as HtmlGenericControl).InnerText,
                        Visible = (Master.FindControl("MainContent").FindControl("divQ" + QNo) as HtmlGenericControl).Visible,
                        Lang = (Master.FindControl("MainContent").FindControl("divQ" + QNo) as HtmlGenericControl).Style["dir"],

                        ARBVisible = (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerRBL") as HtmlGenericControl).Visible,
                        ARBTitle = (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerRB") as HtmlGenericControl).InnerText,
                        ARBOption = (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerRBL") as HtmlGenericControl).InnerHtml,
                        ARBValue = Request.Form["Q" + QNo + "AnswerRBOption"],

                        ACBVisible = (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerCBL") as HtmlGenericControl).Visible,
                        ACBTitle = (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerCB") as HtmlGenericControl).InnerText,
                        ACBOption = (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerCBL") as HtmlGenericControl).InnerHtml,
                        ACBValue = Request.Form["Q" + QNo + "AnswerCBOption"],

                        ADDLVisible = (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerDDL") as HtmlGenericControl).Visible,
                        ADDLTitle = (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerDDL") as HtmlGenericControl).InnerText,
                        ADDLOption = strDDLItems,
                        ADDLRequired = (Master.FindControl("MainContent").FindControl("SelectQ" + QNo + "Answer") as HtmlSelect).Attributes["required"] == "required" ? true : false,
                        ADDLValue = Request.Form["SelectQ" + QNo + "Answer"],

                        ATAVisible = (Master.FindControl("MainContent").FindControl("divQ" + QNo + "AnswerTA") as HtmlGenericControl).Visible,
                        ATATitle = (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerTA") as HtmlGenericControl).InnerText,
                        ATAAttribute = strAttribute,
                        ATAStyle = strStyle,
                        ATARequired = (Master.FindControl("MainContent").FindControl("Q" + QNo + "AnswerTA") as HtmlTextArea).Attributes["required"] == "required" ? true : false,
                        ATAValue = Request.Form["ctl00$MainContent$Q" + QNo + "AnswerTA"],

                        ApplicationID = ApplicationID,
                        EmployeeID = "0",
                        ExternalReviewerID = ExtReviewerID,
                        AnswerInstruction = (Master.FindControl("MainContent").FindControl("lblQ" + QNo + "AnswerInstruction") as HtmlGenericControl).InnerText,
                        AnswerOnSameLine = (Master.FindControl("MainContent").FindControl("divQ" + QNo + "Text") as HtmlGenericControl).Style["float"] == "left" ? true : false,
                        QuestionWidth = (Master.FindControl("MainContent").FindControl("divQ" + QNo + "Text") as HtmlGenericControl).Style["width"],
                        ExternalEmployeeID = 0
                    });
                    context.SaveChanges();
                }
                catch (Exception exp)
                {
                    
                }

            }
            dataPath = Server.MapPath("~/App_Data/EvaluationAttachments/");
            erfBAL.GetEvaluationAttachments(ApplicationID,ExtReviewerID,dataPath, out files);
            HttpPostedFile file = Request.Files[0];

            if (file.FileName != "")
            {
                var regexItem = new Regex("^[\\w,\\s-]+\\.[A-Za-z]{3}$");
                if (!regexItem.IsMatch(file.FileName))
                {
                    labelProgrammaticPopup0.Text = "The file name of the uploaded file is unacceptable. No file is uploaded. File name can only have characters like alphabets from A-Z, digits from 0-9 and _ .";
                    programmaticModalPopup0.Show();
                    return isFormComplete; 
                }
                if (file.ContentType != "application/pdf")
                {
                    labelProgrammaticPopup0.Text = "You can only upload pdf files.";
                    programmaticModalPopup0.Show();
                    return isFormComplete;

                }
                if (file.ContentLength > 4000000)
                {
                    labelProgrammaticPopup0.Text = "You can only upload files of size less than 4 MB.";
                    programmaticModalPopup0.Show();
                    return isFormComplete;
                }
                foreach (var item in files)
                {
                    System.IO.File.Delete(dataPath + item.Name);
                }
                file.SaveAs(dataPath + ApplicationID + "_" + ExtReviewerID + "_" + file.FileName);
            }
            
            LoadEvaluationAttachment();
            return isFormComplete;
        }
        
    }
    //[Ajax.AjaxMethod()]
    //private static bool TestWebMethod()
    //{
    //        return true;
    //}
    protected void lbtnEvaluationAttachment_Click(object sender, EventArgs e)
    {
        dataPath = Server.MapPath("~/App_Data/EvaluationAttachments/");
        erfBAL.GetEvaluationAttachments(ApplicationID, ExtReviewerID, dataPath, out files);
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
    protected void btnDelEvaluationAttachment_Click(object sender, EventArgs e)
    {
        dataPath = Server.MapPath("~/App_Data/EvaluationAttachments/");
        erfBAL.GetEvaluationAttachments(ApplicationID, ExtReviewerID, dataPath, out files);
        System.IO.File.Delete(files[0].FullName);
        LoadEvaluationAttachment();
    }
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
                return Master.ApplicationID;
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
                return Master.ExtReviewerID;
            }
        }
    }
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
                return Master.Language;
            }
        }
    }
    #endregion    
    private void ShowError(string message)
    {
        lblMessage.ForeColor = ColorTranslator.FromHtml("#CC3300");
        lblMessage.Text = message;
        Master.ReportFailure(message);
    }

    private void ShowMessage(string message)
    {
        lblMessage.ForeColor = Color.Green;
        lblMessage.Text = message;
        Master.ReportSuccess();
    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }

    protected void btnSaveHidden_Click(object sender, EventArgs e)
    {
        Save();
    }
}