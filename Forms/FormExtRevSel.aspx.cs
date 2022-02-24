using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Configuration;
using BL.Data;
    public partial class Forms_FormExtRevSel : System.Web.UI.Page
    {
        ExtRevBAL erBAL = new ExtRevBAL();
        BAL bal = new BAL();
        public int SizeOfListFinalER;
        protected void Page_Load(object sender, EventArgs e)
        {
            SizeOfListFinalER = int.Parse(bal.GetWorkflowAttribute()
                .Where(a => a.Attribute == GlobalAttribute.SizeOfListFinalER.ToString()).ToList()[0].Value);
            if (IsPostBack)
            {
                return;
            }
            lbtnExtRevList4VR.Attributes.Add("onclick", "window.open ('NoMasterPage/ExtRevList4TopAuthority.aspx?applicationid=" + Master.ApplicationID.ToString() + "',null,'scrollbars=yes, status= no, resizable = yes, toolbar=no,location=no,height = 800, width = 1200, left = 100, top= 100, screenx=10,screeny=600,menubar=no');");
            odsFormExtRevSugg.SelectParameters["ApplicationID"].DefaultValue = Master.ApplicationID.ToString();
            FormMode = "ExtRev";
            DatabindControls();
        }
       
       


        private void DatabindControls()
        {
            divMsg.DataBind();
            btnShowInsertExtRev.DataBind();
            List<Form_FinalExtRev> lffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID);
            if (lffer.Count >= SizeOfListFinalER)
            {
                ShowMessage(Resources.Resource.ExternalReviewerSelectionM1);

            }
            else
            {
                ShowError(Resources.Resource.ExternalReviewerSelectionM2.Replace("@@SizeOfListFinalER@@", SizeOfListFinalER.ToString()));                                
            }
            if(FormMode == "ExtRev")
            {
                gvFinalExtRev.DataSource = erBAL.GetForm_FinalExtRev(Master.ApplicationID);
                gvFinalExtRev.DataBind();
            }
            divFinalExtRev.DataBind();
            divFormExtRevSuggested.DataBind();
            divViewDetail.DataBind();
            divInsertFormExtRev.DataBind();

            divTopOfFormMsgFailure.DataBind();
            lblMessageFailure.DataBind();
            divTopOfFormMsgSuccess.DataBind();
            lblMessageSuccess.DataBind();
        }
        #region Event Handlers
       
        #region Gridview
        #region GridviewSuggestedExtRev
        protected void gvExternalReviewersSugg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int extRevID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "SelectDetail")
            {
                odsFormExtRevSuggDet.SelectParameters["ExternalReviewerID"].DefaultValue = extRevID.ToString();
                dvExtReviewers.DataBind();
                FormMode = "ExtRevDetail";
                DatabindControls();
            }
            else if (e.CommandName == "Select")
            {
                //List<Form_FinalExtRev> lffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID);
                //for (int rowNo = 0; rowNo < lffer.Count; rowNo++)
                //{
                //    if (lffer[rowNo].ExternalReviewerID == extRevID)
                //    {
                //        labelProgrammaticPopup0.Text = "This selection is already present in the Final External Reviewers.";
                //        programmaticModalPopup0.Show();
                //        return;
                //    }
                //}
                erBAL.InsertFormFinalExtRev(Master.ApplicationID, extRevID, erBAL.GetForm_FinalExtRev(Master.ApplicationID).Count + 1
                    , WillingessStatus.Not_Sent.ToString().Replace("_", " "), null, "", SendSelPubStatus.Material_Not_Sent.ToString().Replace("_", " ")
                    , null, EvaluationStatus.Not_Submitted.ToString().Replace("_", " "), null, "", null, null, "", ""
                    , erBAL.GetSourceForFinalExtRev(extRevID, Master.ApplicationID), null, "", null);

                DatabindControls();
            }
        }
        #endregion
        #region GridviewFinalExtRev
        protected void gvFinalExtRev_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int appID = Master.ApplicationID;
            int extRevID = Convert.ToInt32(e.CommandArgument.ToString());
            Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID).Where(er => er.ExternalReviewerID == extRevID).ToList()[0];
            if (e.CommandName == "MoveRowUp")
            {
                erBAL.DeleteFormFinalExtRev(appID, extRevID);
                Form_FinalExtRev fferUR = erBAL.GetForm_FinalExtRevByAppIDSer(appID, ffer.Serial.Value - 1)[0];
                erBAL.DeleteFormFinalExtRev(appID, fferUR.ExternalReviewerID);
                erBAL.InsertFormFinalExtRev(appID, fferUR.ExternalReviewerID, ffer.Serial,
                    fferUR.WLStatus,
                    fferUR.WLDate,
                    fferUR.CommentsWithWL,
                    fferUR.MaterialSentStatus,
                    fferUR.MaterialSentDate,
                    fferUR.EvaluationStatus,
                    fferUR.EvaluationDate,
                    fferUR.CommentsWithEval,
                    fferUR.ShowExtRev2PC,
                    fferUR.ShowEval2PC,
                    fferUR.UserName,
                    fferUR.Password,
                    fferUR.Source,
                    fferUR.PromotionRecom,
                    fferUR.Reasons,
                    fferUR.Score
                    );
                erBAL.InsertFormFinalExtRev(appID, ffer.ExternalReviewerID, ffer.Serial - 1,
                    ffer.WLStatus,
                    ffer.WLDate,
                    ffer.CommentsWithWL,
                    ffer.MaterialSentStatus,
                    ffer.MaterialSentDate,
                    ffer.EvaluationStatus,
                    ffer.EvaluationDate,
                    ffer.CommentsWithEval,
                    ffer.ShowExtRev2PC,
                    ffer.ShowEval2PC,
                    ffer.UserName,
                    ffer.Password,
                    ffer.Source,
                    ffer.PromotionRecom,
                    ffer.Reasons,
                    ffer.Score
                    );
                DatabindControls();
            }
            else if (e.CommandName == "MoveRowDn")
            {
                erBAL.DeleteFormFinalExtRev(appID, extRevID);
                Form_FinalExtRev fferLR = erBAL.GetForm_FinalExtRevByAppIDSer(appID, ffer.Serial.Value + 1)[0];
                erBAL.DeleteFormFinalExtRev(appID, fferLR.ExternalReviewerID);
                erBAL.InsertFormFinalExtRev(appID, fferLR.ExternalReviewerID, ffer.Serial,
                    fferLR.WLStatus,
                    fferLR.WLDate,
                    fferLR.CommentsWithWL,
                    fferLR.MaterialSentStatus,
                    fferLR.MaterialSentDate,
                    fferLR.EvaluationStatus,
                    fferLR.EvaluationDate,
                    fferLR.CommentsWithEval,
                    fferLR.ShowExtRev2PC,
                    fferLR.ShowEval2PC,
                    fferLR.UserName,
                    fferLR.Password,
                    fferLR.Source,
                    fferLR.PromotionRecom,
                    fferLR.Reasons,
                    fferLR.Score);
                erBAL.InsertFormFinalExtRev(appID, ffer.ExternalReviewerID, ffer.Serial + 1,
                    ffer.WLStatus,
                    ffer.WLDate,
                    ffer.CommentsWithWL,
                    ffer.MaterialSentStatus,
                    ffer.MaterialSentDate,
                    ffer.EvaluationStatus,
                    ffer.EvaluationDate,
                    ffer.CommentsWithEval,
                    ffer.ShowExtRev2PC,
                    ffer.ShowEval2PC,
                    ffer.UserName,
                    ffer.Password,
                    ffer.Source,
                    ffer.PromotionRecom,
                    ffer.Reasons,
                    ffer.Score);
                DatabindControls();
            }
            if (e.CommandName == "SelectDetail")
            {
                odsFormExtRevSuggDet.SelectParameters["ExternalReviewerID"].DefaultValue = extRevID.ToString();
                dvExtReviewers.DataBind();
                FormMode = "ExtRevDetail";
                DatabindControls();
            }

        }
        protected void gvFinalExtRev_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int totalRows = erBAL.GetForm_FinalExtRev(Master.ApplicationID).Count;
            if (totalRows == 1 && e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("iBtnArrowUp") as ImageButton).Visible = false;
                (e.Row.FindControl("iBtnArrowDn") as ImageButton).Visible = false;
            }
            if (e.Row.RowIndex == 0 && e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("iBtnArrowUp") as ImageButton).Visible = false;
            }
            if (e.Row.RowIndex == totalRows - 1 && e.Row.RowType == DataControlRowType.DataRow)
            {
                (e.Row.FindControl("iBtnArrowDn") as ImageButton).Visible = false;
            }
        }
        protected void gvFinalExtRev_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int appID = Master.ApplicationID;
            //int extRevID = int.Parse(gvFinalExtRev.DataKeys[e.RowIndex].Values[1].ToString());
            //Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID).Where(er => er.ExternalReviewerID == extRevID).ToList()[0];
            //if (ffer.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_", " "))
            //{
            //    labelProgrammaticPopup0.Text = "There are some communication associated with this reviewer. Delete the communication in External Reviewer Communication page inorder to completely delete the external reviewer from final list.";
            //    programmaticModalPopup0.Show();
            //    return;
            //}

            //foreach (Application_TaskLogExt row in bal.GetAppTaskLogExt().Where(atle => atle.ApplicationID == appID && atle.ExternalReviewerID == extRevID))
            //{
            //    bal.DeleteApplication_TaskLogExt(row.TaskLogID);
            //}
            //erBAL.DeleteFormFinalExtRev(appID, extRevID);
            //int i = 1;
            //foreach (Form_FinalExtRev row in erBAL.GetForm_FinalExtRev(appID))
            //{
            //    erBAL.DeleteFormFinalExtRev(Master.ApplicationID, row.ExternalReviewerID);
            //    erBAL.InsertFormFinalExtRev(Master.ApplicationID, row.ExternalReviewerID, i++,
            //           row.WLStatus,
            //           row.WLDate,
            //           row.CommentsWithWL,
            //           row.MaterialSentStatus,
            //           row.MaterialSentDate,
            //           row.EvaluationStatus,
            //           row.EvaluationDate,
            //           row.CommentsWithEval,
            //           row.ShowExtRev2PC,
            //           row.ShowEval2PC,
            //           row.UserName,
            //           row.Password,
            //           row.Source,
            //                row.PromotionRecom,
            //        row.Reasons,
            //        row.Score
            //           );
            //}
            //if (erBAL.GetForm_FinalExtRev(appID).Count < 10)
            //{
            //    Master.ReportFailure("List of 10 Final External Reviewers is incomplete");
            //}

            //DatabindControls();            
        }
        protected void gvFinalExtRev_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //int extRevID = int.Parse(gvFinalExtRev.DataKeys[e.NewEditIndex].Values[1].ToString());
            //PopulateAddEditForm(extRevID);
            //FormMode = "EditExtRev";
            //DatabindControls();
            //e.Cancel = true;

        }
        protected void gvFinalExtRev_DataBound(object sender, EventArgs e)
        {
            //foreach (GridViewRow row in gvFinalExtRev.Rows)
            //{
            //    LinkButton lbtnDelete = row.FindControl("lbtnDelete") as LinkButton;
            //    lbtnDelete.Attributes.Add("OnClick", "return confirm_delete_ExtReviewer();");
            //}
        }
        #endregion
        
        #region  gvExtRevSearchResult

        protected void gvExtRevSearchResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ExternalReviewerID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Select")
            {
                PopulateAddEditForm(ExternalReviewerID);
            }
        }
        #endregion
        #endregion
        #region Button Event Handlers
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            FormMode = "ExtRev";
            DatabindControls();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            taExtRev.InnerText = "";
        }
        protected void btnFillTB_Click(object sender, EventArgs e)
        {
            //string[] erData = taExtRev.InnerText.Split('\t', '\r', '\n');
            taExtRev.InnerText = Regex.Replace(taExtRev.InnerText, PromotionApplication.specialCharacters, string.Empty);
            string[] erData = taExtRev.InnerText.Split('\t');
            if (erData.Length >= 1)
            {
                tbName.Text = erData[0];
            }
            if (erData.Length >= 2)
            {
                //ddlRank.SelectedItem.Selected = false;
                ddlRank.SelectedIndex = -1;
                if (ddlRank.Items.FindByText(erData[1]) != null)
                {
                    ddlRank.Items.FindByText(erData[1]).Selected = true;
                }


            }
            //if (erData.Length >= 3)
            //{
            //    tbDescription.Text = erData[2];
            //}
            if (erData.Length >= 3)
            {
                tbMailingAddress.Text = erData[2];

            }
            if (erData.Length >= 4)
            {
                tbPhoneAndFax.Text = erData[3];

            }
            if (erData.Length >= 5)
            {
                tbEmail.Text = erData[5].Trim();

            }
            if (erData.Length >= 6)
            {
                tbMajor.Text = erData[5];

            }
            if (erData.Length >= 7)
            {
                tbSpecialty.Text = erData[6];
            }
            if (erData.Length >= 8)
            {
                tbActiveAreaOfResearch.Text = erData[7];
            }
            if (erData.Length >= 9)
            {
                tbWebpage.Text = erData[8];
            }
            int result = 0;
            if (erData.Length >= 10)
            {
                if (int.TryParse(erData[9], out result))
                {
                    tbTotPublications.Value = result.ToString();
                }
                else
                {
                    tbTotPublications.Value = "0";
                }
            }
            if (erData.Length >= 11)
            {
                if (int.TryParse(erData[10], out result))
                {
                    tbNoOfJournals.Value = result.ToString();
                }
                else
                {
                    tbNoOfJournals.Value = "0";
                }
            }
            if (erData.Length >= 12)
            {
                if (int.TryParse(erData[11], out result))
                {
                    tbHIndex.Value = result.ToString();
                }
                else
                {
                    tbHIndex.Value = "0";
                }
            }
            if (erData.Length >= 13)
            {
                if (int.TryParse(erData[12], out result))
                {
                    tbCitations.Value = result.ToString();
                }
                else
                {
                    tbCitations.Value = "0";
                }
            }
            if (erData.Length >= 14)
            {
            }
            if (erData.Length >= 15)
            {
            }
            if (erData.Length >= 16)
            {
            }
            if (erData.Length >= 17)
            {
            }
        }
        protected void btnShowDivExcel_Click(object sender, EventArgs e)
        {
            divExcel.Visible = true;
            taExtRev.DataBind();
            btnShowDivExcel.Visible = false;

        }

        protected void btnHideDivExcel_Click(object sender, EventArgs e)
        {
            divExcel.Visible = false;
            btnShowDivExcel.Visible = true;
        }
        protected void btnHideDetail_Click(object sender, EventArgs e)
        {
            ExternalReviewerID = -1;
            FormMode = "ExtRev";
            DatabindControls();
        }
        protected void btnHideInsertExtRev_Click(object sender, EventArgs e)
        {
            ExternalReviewerID = -1;
            FormMode = "ExtRev";
            DatabindControls();
        }
        protected void btnShowInsertExtRev_Click(object sender, EventArgs e)
        {
            ExternalReviewerID = -1;
            FormMode = "AddExtRev";
            PopulateAddEditForm();
            DatabindControls();
        }
        protected void btnAddExtRev_Click(object sender, EventArgs e)
        {
            //string[] deptLowInResearch = { "PE", "IAS", "GS" };
            //if ((tbTotPublications.Value == "0" || tbNoOfJournals.Value == "0" || tbHIndex.Value == "0" || tbCitations.Value == "0") && !deptLowInResearch.Contains(Master.Applicant.DeptShort))
            //{
            //    labelProgrammaticPopup0.Text = "Total Publications, No of Journals, H-Index, and Citations has to be greater than 0.";
            //    programmaticModalPopup0.Show();
            //    return;
            //}
            if (!ValidationChecks())
            {
                return;
            }
            List<Form_FinalExtRev> lffer = erBAL.GetForm_FinalExtRev(Master.ApplicationID);

            if (FormMode.Contains("Add"))
            {
                if (lffer.Where(er => er.ExternalReviewer.Email.Trim().ToLower().Equals(tbEmail.Text.Trim().ToLower())).Count() > 0)
                {
                    //Already present in Final List 
                    labelProgrammaticPopup0.Text = Resources.Resource.ExternalReviewerSelectionM3.Replace("@@EmailAddress@@", tbEmail.Text);                        
                    programmaticModalPopup0.Show();
                    return;
                }
            }
            else
            {
                if (lffer.Where(er => er.ExternalReviewer.Email.Trim().ToLower().Equals(tbEmail.Text.Trim().ToLower())).Count() > 1)
                {
                    //Already present in final List 
                    labelProgrammaticPopup0.Text = 
                        Resources.Resource.ExternalReviewerSelectionM3.Replace("@@EmailAddress@@", tbEmail.Text);
                    programmaticModalPopup0.Show();
                    return;
                }
            }
            if (ExternalReviewerID == -1 && erBAL.GetExtRevByEmail(tbEmail.Text.Trim()).Count > 0)
            {
                //Already present in Master External Reviewers List 
                labelProgrammaticPopup0.Text = 
                    Resources.Resource.ExternalReviewerSelectionM4.Replace("@@EmailAddress@@", tbEmail.Text);                
                programmaticModalPopup0.Show();
                tbSearch.Text = tbEmail.Text.Trim().ToLower();
                gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbEmail.Text.Trim().ToLower());
                gvExtRevSearchResult.DataBind();
                lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";

                return;
            }
            if (ExternalReviewerID != -1)
            {
                ExternalReviewer er = erBAL.GetExtRevByID(ExternalReviewerID)[0];
                if (er.Email != tbEmail.Text && erBAL.GetAllExtRev().Where(extRev => extRev.Email == tbEmail.Text).Count() == 1)
                {
                    labelProgrammaticPopup0.Text = "This External Reviewer with the Email Address: (" + tbEmail.Text + ") is already present in the External Reviewers list. Update cannot be done.";
                    programmaticModalPopup0.Show();
                    return;
                }
                if (er.Name != tbName.Text && erBAL.GetAllExtRev().Where(extRev => extRev.Name == tbName.Text).Count() == 1)
                {
                    labelProgrammaticPopup0.Text = "This External Reviewer with the Name: (" + tbName.Text + ") is already present in the External Reviewers list. Update cannot be done.";
                    programmaticModalPopup0.Show();
                    return;
                }
                erBAL.UpdateExtRev(ExternalReviewerID,
                                        tbName.Text,
                                        ddlRank.SelectedValue == "Others" ? tbOthers.Text : ddlRank.SelectedValue,
                                        tbMailingAddress.Text,
                                        tbEmail.Text,
                                        tbMajor.Text,
                                        tbSpecialty.Text,
                                        tbPhoneAndFax.Text,
                                        tbActiveAreaOfResearch.Text,
                                        er.PrevAreaOfResearch,
                                        tbWebpage.Text,
                                        er.Comments,
                                        int.Parse(tbTotPublications.Value == "" ? "0" : tbTotPublications.Value),
                                        int.Parse(tbNoOfJournals.Value == "" ? "0" : tbNoOfJournals.Value),
                                        int.Parse(tbHIndex.Value == "" ? "0" : tbHIndex.Value),
                                        int.Parse(tbCitations.Value == "" ? "0" : tbCitations.Value),
                                        RecordStatus.Active.ToString(), 
                                        er.Password, er.IBAN, er.PassportNo, tbName.Text,"" /*tbDescription.Text*/, 
                                        er.Name_1, er.Name_2, er.Name_3, er.Name_4, er.Salt);
            }

            else
            {
                    if (erBAL.GetAllExtRev().Where(extRev => extRev.Email == tbEmail.Text).Count() == 1)
                    {
                        labelProgrammaticPopup0.Text = "This External Reviewer with the Email Address: (" + tbEmail.Text + ") is already present in the External Reviewers list. Addition cannot be done.";
                        programmaticModalPopup0.Show();
                        return;
                    }
                    if (erBAL.GetAllExtRev().Where(extRev => extRev.Name == tbName.Text).Count() == 1)
                    {
                        labelProgrammaticPopup0.Text = "This External Reviewer with the Name: (" + tbName.Text + ") is already present in the External Reviewers list. Addition cannot be done.";
                        programmaticModalPopup0.Show();
                        return;
                    }
                    ExternalReviewerID = erBAL.InsertExtRev(
                                        tbName.Text,
                                        ddlRank.SelectedValue == "Others" ? tbOthers.Text : ddlRank.SelectedValue,
                                        tbMailingAddress.Text,
                                        tbEmail.Text,
                                        tbMajor.Text,
                                        tbSpecialty.Text,
                                        tbPhoneAndFax.Text,
                                        tbActiveAreaOfResearch.Text,
                                        "",
                                        tbWebpage.Text,
                                        "",
                                        int.Parse(tbTotPublications.Value == "" ? "0" : tbTotPublications.Value),
                                        int.Parse(tbNoOfJournals.Value == "" ? "0" : tbNoOfJournals.Value),
                                        int.Parse(tbHIndex.Value == "" ? "0" : tbHIndex.Value),
                                        int.Parse(tbCitations.Value == "" ? "0" : tbCitations.Value),
                                        RecordStatus.Active.ToString(), "", "", "", tbName.Text,
                                        ""/*tbDescription.Text*/, tbName.Text, "", "", "", "");

            }

            if (FormMode.Contains("Add"))
            {
                erBAL.InsertFormFinalExtRev(Master.ApplicationID, ExternalReviewerID
                    , erBAL.GetForm_FinalExtRev(Master.ApplicationID).Count + 1
                    , WillingessStatus.Not_Sent.ToString().Replace("_", " "), null, ""
                    , SendSelPubStatus.Material_Not_Sent.ToString().Replace("_", " ")
                    , null, EvaluationStatus.Not_Submitted.ToString().Replace("_", " ")
                    , null, "", null, null, "", "", erBAL.GetSourceForFinalExtRev(ExternalReviewerID, Master.ApplicationID, Master.CurRoleID)
                    , null, "", null);
            }
            //else if (FormMode == "EditExtRev")
            //{
            //    erBAL.DeleteFormFinalExtRev(
            //                Master.ApplicationID,
            //                ExternalReviewerID);
            //    erBAL.InsertFormExtRev(
            //                Master.ApplicationID,
            //                Type,
            //                Serial,
            //                ExternalReviewerID
            //                );

            //}
            if (lffer.Count >= (SizeOfListFinalER - 1))
            {
                ShowMessage(Resources.Resource.ExternalReviewerSelectionM1);

            }
            else
            {
                ShowError(Resources.Resource.ExternalReviewerSelectionM2.Replace("@@SizeOfListFinalER@@", SizeOfListFinalER.ToString()));
            }
            ExternalReviewerID = -1;
            FormMode = "ExtRev";
            DatabindControls();
        }

       

        protected void btnHideFinalExtRevDet_Click(object sender, EventArgs e)
        {

        }
        protected void btnHideSearch_Click(object sender, EventArgs e)
        {
            divExtRevSearch.Visible = false;
        }
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    tbName.Text = Regex.Replace(tbName.Text, PromotionApplication.specialCharacters, string.Empty);
        //    odsExtRevSearch.SelectParameters["MainSearchString"].DefaultValue = tbName.Text;
        //    gvExtRevSearchResult.DataBind();
        //    divExtRevSearch.Visible = true;
        //}
        protected void btnSearchName_Click(object sender, EventArgs e)
        {
            tbSearch.Text = tbName.Text;
            ddlListType.SelectedIndex = -1;
            gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbName.Text);
            gvExtRevSearchResult.DataBind();
            lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";
        }
        protected void btnSearchExtRev_Click(object sender, EventArgs e)
        {
            tbSearch.Text = Regex.Replace(tbSearch.Text, PromotionApplication.specialCharacters, string.Empty);
            gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbSearch.Text.ToLower());
            gvExtRevSearchResult.DataBind();
            lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";
        }
        protected void btnClearForm_Click(object sender, EventArgs e)
        {
            PopulateAddEditForm();
        }
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            FormMode = "Edit";
            string[] args = ((LinkButton)sender).CommandArgument.ToString().Split(';');            
            ExternalReviewerID = int.Parse(args[0]);          
            PopulateAddEditForm(ExternalReviewerID);
            DatabindControls();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string[] args = ((LinkButton)sender).CommandArgument.ToString().Split(';');            
            int appID = Master.ApplicationID;
            ExternalReviewerID = int.Parse(args[0]);            
            //Form_FinalExtRev ffer = erBAL.GetForm_FinalExtRev(appID)
            //    .Where(er => er.ExternalReviewerID == ExternalReviewerID).ToList()[0];
            //if (ffer.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_", " "))
            //{
            //    labelProgrammaticPopup0.Text = Resources.Resource.ExternalReviewerSelectionM5;
            //    programmaticModalPopup0.Show();
            //    return;
            //}

            foreach (Application_TaskLogExt row in bal.GetAppTaskLogExt()
                .Where(atle => atle.ApplicationID == appID && atle.ExternalReviewerID == ExternalReviewerID))
            {
                bal.DeleteApplication_TaskLogExt(row.TaskLogID);
            }
            erBAL.DeleteFormFinalExtRev(appID, ExternalReviewerID);
            int i = 1;
            foreach (Form_FinalExtRev row in erBAL.GetForm_FinalExtRev(appID))
            {
                erBAL.DeleteFormFinalExtRev(Master.ApplicationID, row.ExternalReviewerID);
                erBAL.InsertFormFinalExtRev(Master.ApplicationID, row.ExternalReviewerID, i++,
                       row.WLStatus,
                       row.WLDate,
                       row.CommentsWithWL,
                       row.MaterialSentStatus,
                       row.MaterialSentDate,
                       row.EvaluationStatus,
                       row.EvaluationDate,
                       row.CommentsWithEval,
                       row.ShowExtRev2PC,
                       row.ShowEval2PC,
                       row.UserName,
                       row.Password,
                       row.Source,
                            row.PromotionRecom,
                    row.Reasons,
                    row.Score
                       );
            }
            //if (erBAL.GetForm_FinalExtRev(appID).Count < sizeOfListFinalER)
            //{
            //    ShowError("List of Final External Reviewers has to be atleast " 
            //        + sizeOfListFinalER + ". List of Final External Reviewers is Incomplete.");
            //}
            DatabindControls();
        }

        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }
        #endregion
        #region TextBox Event Handlers
        protected void tbSearch_TextChanged(object sender, EventArgs e)
        {
            tbSearch.Text = Regex.Replace(tbSearch.Text, PromotionApplication.specialCharacters, string.Empty);
            gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbSearch.Text);
            gvExtRevSearchResult.DataBind();
            lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";
        }
        protected void tbName_TextChanged(object sender, EventArgs e)
        {
            string name = tbName.Text.Trim();
            //tbName.Text = name.Replace("Dr. ", "");
            //tbName.Text = name.Replace("Prof. ", "");
            //tbName.Text = name.Replace("Professor", "");
            tbSearch.Text = tbName.Text.ToLower();
            gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbName.Text);
            gvExtRevSearchResult.DataBind();
            lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";


        }
        protected void tbMailingAddress_TextChanged(object sender, EventArgs e)
        {
            string ma = tbMailingAddress.Text.Trim().ToLower();
            if (
                !ma.Contains("inc") && !ma.Contains("company") && !ma.Contains("hospital") &&
                !ma.Contains("academy") && !ma.Contains("center") && !ma.Contains("centre") &&
                !ma.Contains("clinic") && !ma.Contains("clinique") && !ma.Contains("college") &&
                !ma.Contains("foundation") && !ma.Contains("laboratory") && !ma.Contains("laboratories") &&
                !ma.Contains("office") && !ma.Contains("Socie") && !ma.Contains("inc") && !ma.Contains("company ") &&
                !ma.Contains("univ") && !ma.Contains("üniversite") && !ma.Contains("yliopisto") &&
                !ma.Contains("uniwersytet") && !ma.Contains("tech") && !ma.Contains("institute") &&
                !ma.Contains("iit") && !ma.Contains("mit") && !ma.Contains("uoit") && !ma.Contains("sch") &&
                !ma.Contains("Polytechnique") && !ma.Contains("ecole") && !ma.Contains("جامعه") &&
                !ma.Contains("imperial college"))
            { tbMailingAddress.BackColor = Color.Red; }
            else
            { tbMailingAddress.BackColor = Color.White; }
        }
        protected void tbEmail_TextChanged(object sender, EventArgs e)
        {
            tbEmail.Text = tbEmail.Text.Trim().ToLower();
            List<ExternalReviewer> lER = erBAL.GetExtRevByEmail(tbEmail.Text);
            if (lER.Count != 0)
            {
                tbEmail.BackColor = Color.Red;
                gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbEmail.Text);
                gvExtRevSearchResult.DataBind();
                divExtRevSearch.Visible = true;
                // btnAddExtRev.Enabled = false;
                tbEmail.ToolTip = Resources.Resource.ExternalReviewerSelectionM6;
            }
            else
            {
                tbEmail.BackColor = Color.White;
                btnAddExtRev.Enabled = true;
                tbEmail.ToolTip = Resources.Resource.ExternalReviewerSelectionM7; 
            }

        }
        #endregion
        #region  ddlListType
        protected void ddlListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlListType.SelectedValue == "-1")
            {
                tbSearch.Text = "";
                gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbSearch.Text.ToLower());
                gvExtRevSearchResult.DataBind();
                lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";
            }
            else
            {
                tbSearch.Text = "::" + ddlListType.SelectedItem.Text + "@" + Master.Applicant.DeptShort;
                gvExtRevSearchResult.DataSource = erBAL.SearchExternalReviewer(tbSearch.Text.ToLower());
                gvExtRevSearchResult.DataBind();
                lblSearchResultCount.Text = gvExtRevSearchResult.Rows.Count > 0 ? gvExtRevSearchResult.Rows.Count + " rows found" : "";
            }
        }
        #endregion
        protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRank.SelectedValue == "Others")
            {
                tbOthers.Visible = true;
            }
            else
            {
                tbOthers.Visible = false;
            }
        }
        #endregion        
        private void PopulateAddEditForm(int erID)
        {
            taExtRev.InnerText = "";
            tbEmail.BackColor = Color.White;
            List<ExternalReviewer> lER = erBAL.GetExtRevByID(erID);
            ExternalReviewer er = lER[0];
            //ExternalReviewerID = er.ExternalReviewerID;
            tbName.Text = er.NameString;
            //ddlRank.SelectedItem.Selected = false;
            //  ddlRank.SelectedIndex = -1;
            if (er.Rank != null && er.Rank != "")
            {
                if (ddlRank.Items.FindByText(er.Rank) != null)
                {
                    ddlRank.Items.FindByText(er.Rank).Selected = true;
                    ddlRank.SelectedValue = ddlRank.Items.FindByText(er.Rank).Value;
                }
                else
                {

                    tbOthers.Text = er.Rank;
                }
            }
            if (ddlRank.SelectedValue == "Others")
            {
                tbOthers.Visible = true;
                tbOthers.Text = er.Rank;
            }
            else
            {
                tbOthers.Visible = false;
                tbOthers.Text = "";
            }
            tbPhoneAndFax.Text = er.PhoneAndFax;
           // tbDescription.Text = er.Description;
            tbMailingAddress.Text = er.MailingAddress;
            tbMajor.Text = er.Major;
            tbSpecialty.Text = er.Specialty;
            tbActiveAreaOfResearch.Text = er.ActiveAreaOfResearch;
            tbWebpage.Text = er.Webpage;
            tbEmail.Text = er.Email;
            tbTotPublications.Value = er.TotalPublications.HasValue ? er.TotalPublications.Value.ToString() : "0";
            tbNoOfJournals.Value = er.NoOfJournals.HasValue ? er.NoOfJournals.Value.ToString() : "0";
            tbHIndex.Value = er.HIndex.HasValue ? er.HIndex.Value.ToString() : "0";
            tbCitations.Value = er.Citations.HasValue ? er.Citations.Value.ToString() : "0";

            tbEmail.BorderColor = Color.Gray;
            tbPhoneAndFax.BorderColor = Color.Gray;
         //   tbDescription.BorderColor = Color.Gray;
            tbMailingAddress.BorderColor = Color.Gray;
            tbMajor.BorderColor = Color.Gray;
            tbSpecialty.BorderColor = Color.Gray;
            tbActiveAreaOfResearch.BorderColor = Color.Gray;
            tbWebpage.BorderColor = Color.Gray;
            tbEmail.BorderColor = Color.Gray;
            tbTotPublications.Style["BorderColor"] = Color.Gray.ToString();
            tbNoOfJournals.Style["BorderColor"] = Color.Gray.ToString();
            tbHIndex.Style["BorderColor"] = Color.Gray.ToString();
            tbCitations.Style["BorderColor"] = Color.Gray.ToString();
            //tbSearch.Text = "";
            //lblSearchResultCount.Text = "";
        }
        private void PopulateAddEditForm()
        {
            tbEmail.BackColor = Color.White;
            tbEmail.BorderColor = Color.Gray;

            tbName.Text = "";
            //if (!ddlRank.SelectedItem.Selected)
            if (ddlRank.SelectedIndex != -1)
            {
                ddlRank.Items.FindByText("Professor").Selected = true;
            }

            tbPhoneAndFax.Text = "";
           // tbDescription.Text = "";
            tbMailingAddress.Text = "";
            tbMajor.Text = "";
            tbSpecialty.Text = "";
            tbActiveAreaOfResearch.Text = "";
            tbWebpage.Text = "";
            tbEmail.Text = "";
            tbTotPublications.Value = "0";
            tbNoOfJournals.Value = "0";
            tbHIndex.Value = "0";
            tbCitations.Value = "0";

            lblSearchResultCount.Text = "";

            tbEmail.BorderColor = Color.Gray;
            tbPhoneAndFax.BorderColor = Color.Gray;
           // tbDescription.BorderColor = Color.Gray;
            tbMailingAddress.BorderColor = Color.Gray;
            tbMajor.BorderColor = Color.Gray;
            tbSpecialty.BorderColor = Color.Gray;
            tbActiveAreaOfResearch.BorderColor = Color.Gray;
            tbWebpage.BorderColor = Color.Gray;
            tbEmail.BorderColor = Color.Gray;
            tbTotPublications.Style["BorderColor"] = Color.Gray.ToString();
            tbNoOfJournals.Style["BorderColor"] = Color.Gray.ToString();
            tbHIndex.Style["BorderColor"] = Color.Gray.ToString();
            tbCitations.Style["BorderColor"] = Color.Gray.ToString();
            tbSearch.Text = "";
            lblSearchResultCount.Text = "";
        }
        private void ShowError(string message)
        {
            Master.ReportFailure(message);
        }
        private void ShowMessage(string message)
        {
            Master.ReportSuccess(message);
        }
        private bool ValidationChecks()
        {
            bool isOK = true;
            //if (tbCitations.Text == "0" || tbCitations.Text == "")
            //{
            //    tbCitations.BorderColor = Color.OrangeRed;
            //    labelProgrammaticPopup0.Text = "Citations of external reviewer is must and must be greater than 0";
            //    programmaticModalPopup0.Show();
            //    isOK = false;
            //}
            //else
            //{
            //    tbCitations.BorderColor = Color.Gray;
            //}
            //if (tbHIndex.Text == "0" || tbHIndex.Text == "")
            //{
            //    tbHIndex.BorderColor = Color.OrangeRed;
            //    labelProgrammaticPopup0.Text = "H-Index of external reviewer is must and must be greater than 0";
            //    programmaticModalPopup0.Show();
            //    isOK = false;
            //}
            //else
            //{
            //    tbHIndex.BorderColor = Color.Gray;
            //}
            //if (tbNoOfJournals.Text == "0" || tbNoOfJournals.Text == "")
            //{
            //    tbNoOfJournals.BorderColor = Color.OrangeRed;
            //    labelProgrammaticPopup0.Text = "No Of Journals of external reviewer is must and must be greater than 0";
            //    programmaticModalPopup0.Show();
            //    isOK = false;
            //}
            //else
            //{
            //    tbNoOfJournals.BorderColor = Color.Gray;
            //}
            //if (tbTotPublications.Text == "0" || tbTotPublications.Text == "")
            //{
            //    tbTotPublications.BorderColor = Color.OrangeRed;
            //    labelProgrammaticPopup0.Text = "Total Publications of external reviewer is must and must be greater than 0";
            //    programmaticModalPopup0.Show();
            //    isOK = false;
            //}
            //else
            //{
            //    tbTotPublications.BorderColor = Color.Gray;
            //}
            if (tbActiveAreaOfResearch.Text == "")
            {
                tbActiveAreaOfResearch.BorderColor = Color.OrangeRed;
                labelProgrammaticPopup0.Text = Resources.Resource.ExternalReviewerSelectionM8;
                programmaticModalPopup0.Show();
                isOK = false;
            }
            else
            {
                tbActiveAreaOfResearch.BorderColor = Color.Gray;
            }

            //if (tbSpecialty.Text == "")
            //{
            //    tbSpecialty.BorderColor = Color.OrangeRed;
            //    labelProgrammaticPopup0.Text = Resources.Resource.ExternalReviewerSelectionM9;
            //    programmaticModalPopup0.Show();
            //    isOK = false;
            //}
            //else
            //{
            //    tbSpecialty.BorderColor = Color.Gray;
            //}

            if (tbMajor.Text == "")
            {
                tbMajor.BorderColor = Color.OrangeRed;
                labelProgrammaticPopup0.Text = Resources.Resource.ExternalReviewerSelectionM10;
                programmaticModalPopup0.Show();
                isOK = false;
            }
            else
            {
                tbMajor.BorderColor = Color.Gray;
            }
            if (!PromotionApplication.IsValidEmail(tbEmail.Text.Trim()))
            {
                tbEmail.BorderColor = Color.OrangeRed;
                labelProgrammaticPopup0.Text = Resources.Resource.ExternalReviewerSelectionM11;
                programmaticModalPopup0.Show();
                isOK = false;
            }
            else
            {
                tbEmail.BorderColor = Color.Gray;
            }
            if (!PromotionApplication.ValidateMailingAddress(tbMailingAddress.Text.Trim().ToLower()))
            {
                tbMailingAddress.BorderColor = Color.OrangeRed;
                labelProgrammaticPopup0.Text = Resources.Resource.ExternalReviewerSelectionM12;
                programmaticModalPopup0.Show();
                isOK = false;
            }
            else
            {
                tbMailingAddress.BorderColor = Color.Gray;
            }

            if (tbName.Text == "")
            {
                tbName.BorderColor = Color.OrangeRed;
                labelProgrammaticPopup0.Text = Resources.Resource.ExternalReviewerSelectionM13;
                programmaticModalPopup0.Show();
                isOK = false;
            }
            else
            {
                tbName.BorderColor = Color.Gray;

            }
            tbName.Text = Regex.Replace(tbName.Text, PromotionApplication.specialCharacters, string.Empty);
            tbMailingAddress.Text = Regex.Replace(tbMailingAddress.Text, PromotionApplication.specialCharacters, string.Empty);
            tbMajor.Text = Regex.Replace(tbMajor.Text, PromotionApplication.specialCharacters, string.Empty);
            tbSpecialty.Text = Regex.Replace(tbSpecialty.Text, PromotionApplication.specialCharacters, string.Empty);
            tbActiveAreaOfResearch.Text = Regex.Replace(tbActiveAreaOfResearch.Text, PromotionApplication.specialCharacters, string.Empty);
            tbPhoneAndFax.Text = Regex.Replace(tbPhoneAndFax.Text, PromotionApplication.specialCharacters, string.Empty);
        //    tbDescription.Text = Regex.Replace(tbDescription.Text, PromotionApplication.specialCharacters, string.Empty);
            tbWebpage.Text = Regex.Replace(tbWebpage.Text, PromotionApplication.specialCharacters.Replace("&", ""), string.Empty);

            if (isOK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #region Get Methods 
        //private string GetSource()
        //{
        //    if (Master.CurRoleID >= (byte)RoleID.SC_Subcommittee_Chair &&
        //        Master.CurRoleID <= (byte)RoleID.SC_Subcommittee_Member_4)
        //    {
        //        return ConfigurationManager.AppSettings["Permanent_Committee1"];
        //    }
        //    else
        //    {
        //        return ConfigurationManager.AppSettings["TopAuthority_TitleShort"];
        //    }
        //}
        public string GetStatus()
        {
            string value = Eval("Type").ToString();
            switch (value)
            {
                case "0":
                    return "Applicant";
                case "1":
                    return "Department";
                case "2":
                    return "College";
                case "3":
                    return ConfigurationManager.AppSettings["TopAuthority_TitleShort"];
                default:
                    return "SCSubCom";
            }
        }
        public bool GetSelectStatus(int ExtRevID)
        {
            return erBAL.GetForm_FinalExtRev(Master.ApplicationID, ExtRevID).Count == 0;
        }
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
        #region Properties
        public string FormMode
        {
            set
            {
                ViewState["FormMode"] = value;
            }
            get
            {
                if (ViewState["FormMode"] != null)
                    return (string)ViewState["FormMode"];
                else
                    return "";
            }
        }
        public int ExternalReviewerID
        {
            set
            {
                ViewState["ExternalReviewerID"] = value;
            }
            get
            {
                if (ViewState["ExternalReviewerID"] != null && (int)ViewState["ExternalReviewerID"] != -1)
                {
                    return (int)ViewState["ExternalReviewerID"];
                }
                else
                {
                    return -1;
                }

            }
        }
        public int Serial
        {
            set
            {
                ViewState["Serial"] = value;
            }
            get
            {
                if (ViewState["Serial"] != null && (int)ViewState["Serial"] != -1)
                {
                    return (int)ViewState["Serial"];
                }
                else
                {
                    return -1;
                }

            }          
        }
        public int Type
        {
            set
            {
                ViewState["Type"] = value;
            }
            get
            {
                if (ViewState["Type"] != null && (int)ViewState["Type"] != -1)
                {
                    return (int)ViewState["Type"];
                }
                else
                {
                    return -1;
                }

            }
        }

        #endregion
     
    }
