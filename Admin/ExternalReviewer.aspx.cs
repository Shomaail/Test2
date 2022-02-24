using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web.UI.HtmlControls;
using BL.Data;


public partial class Admin_ExternalReviewer : System.Web.UI.Page
    {
        ExtRevBAL erBAL = new ExtRevBAL();
        BAL bal = new BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            DatabindControls();

        }

        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }
        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabindControls();
        }
        private void DatabindControls()
        {
            foreach (ExternalReviewer er in erBAL.GetAllExtRev().Where(a => a.ExternalReviewerID != 0))
            {
                if (er.Email == null || er.Email == "")
                {
                   erBAL.UpdateExtRev(er.ExternalReviewerID, er.Name, er.Rank, er.MailingAddress, er.Email, er.Major, er.Specialty, er.PhoneAndFax, er.ActiveAreaOfResearch, er.PrevAreaOfResearch
                        , er.Webpage, er.Comments, er.TotalPublications, er.NoOfJournals, er.HIndex, er.Citations, RecordStatus.Inactive.ToString()
                        , er.Password, er.IBAN, er.PassportNo, er.NameString, er.Description, er.Name_1, er.Name_2, er.Name_3,er.Name_4,er.Salt);
                }

            }
            if (ddlSortBy.SelectedValue == "Specialty")
            {
                rpExternalReviewer.DataSource = erBAL.GetAllExtRev().Where(a => a.ExternalReviewerID != 0)
                    .OrderBy(o=>o.Specialty);
            }
            else if (ddlSortBy.SelectedValue == "Mailing Address")
            {
                rpExternalReviewer.DataSource = erBAL.GetAllExtRev().Where(a => a.ExternalReviewerID != 0)
                    .OrderBy(o => o.MailingAddress);
            }else if(ddlSortBy.SelectedValue == "Name")
            {
                rpExternalReviewer.DataSource = erBAL.GetAllExtRev().Where(a => a.ExternalReviewerID != 0)
                    .OrderBy(o => o.NameString);
            }
            else if (ddlSortBy.SelectedValue == "Major")
            {
                rpExternalReviewer.DataSource = erBAL.GetAllExtRev().Where(a => a.ExternalReviewerID != 0)
                    .OrderBy(o => o.Major);
            }
            
            else
            {
                rpExternalReviewer.DataSource = erBAL.GetAllExtRev().Where(a => a.ExternalReviewerID != 0);
            }
            
            rpExternalReviewer.DataBind();
        }
        private void ToggleElements(RepeaterItem item, bool isEdit)
        {
            //Toggle Buttons.
            item.FindControl("lbtnEdit").Visible = !isEdit;
            item.FindControl("lbtnUpdate").Visible = isEdit;
            item.FindControl("lbtnCancel").Visible = isEdit;
            item.FindControl("lbtnDelete").Visible = !isEdit;

            //Toggle Labels.
            //item.FindControl("lblContactName").Visible = !isEdit;
            //item.FindControl("lblCountry").Visible = !isEdit;

            item.FindControl("lblComments").Visible = !isEdit;
            item.FindControl("lblName").Visible = !isEdit;
            item.FindControl("lblRank").Visible = !isEdit;
            item.FindControl("lblMailingAddress").Visible = !isEdit;
            item.FindControl("lblSpecialty").Visible = !isEdit;
            item.FindControl("lblEmail").Visible = !isEdit;

            // item.FindControl("lblPhone").Visible = !isEdit;
            item.FindControl("lblPhoneAndFax").Visible = !isEdit;
            // item.FindControl("lblCountry").Visible = !isEdit;
            //item.FindControl("lblNameString").Visible = !isEdit;
            item.FindControl("lblActiveAreaOfResearch").Visible = !isEdit;
            item.FindControl("lblPassword").Visible = !isEdit;
            item.FindControl("lblMajor").Visible = !isEdit;
            item.FindControl("lblWebpage").Visible = !isEdit;
            //Toggle TextBoxes.
            item.FindControl("ddlStatusEdit").Visible = isEdit;
            item.FindControl("tbCommentsEdit").Visible = isEdit;
            item.FindControl("tbNameEdit").Visible = isEdit;
            item.FindControl("tbRankEdit").Visible = isEdit;
            item.FindControl("tbMailingAddressEdit").Visible = isEdit;
            item.FindControl("tbActiveAreaOfResearchEdit").Visible = isEdit;
            item.FindControl("tbSpecialtyEdit").Visible = isEdit;
            item.FindControl("tbEmailEdit").Visible = isEdit;
            //item.FindControl("tbEmail2Edit").Visible = isEdit;

            item.FindControl("tbPhoneAndFaxEdit").Visible = isEdit;
            //item.FindControl("tbNameStringEdit").Visible = isEdit;
            //   item.FindControl("tbStatusEdit").Visible = isEdit;
            item.FindControl("tbPasswordEdit").Visible = isEdit;
            item.FindControl("tbMajorEdit").Visible = isEdit;
            item.FindControl("tbWebpageEdit").Visible = isEdit;

        }

        protected void rpExternalReviewer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int erID = int.Parse(e.CommandArgument.ToString());
                if (erBAL.GetForm_ExtRev(erID).Count() > 0 || erBAL.GetForm_FinalExtRev().Where(a => a.ExternalReviewerID == erID).Count() > 0)
                {
                    lblProgrammaticPopup0.Text = "You cannot delete this external reviewer since he/she has involvement in one or more promotion applications.";
                    programmaticModalPopup0.Show();
                    return;
                }
                bal.DeletetAppTskFrmByExtRevID(erID);
                erBAL.DeleteExtRev(erID);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                int erID = int.Parse(e.CommandArgument.ToString());
                ExternalReviewer er = erBAL.GetExtRevByID(erID)[0];
                string Name = (e.Item.FindControl("tbNameEdit") as HtmlInputControl).Value;
                string Specialty = (e.Item.FindControl("tbSpecialtyEdit") as HtmlInputControl).Value;
                string ActiveAreaOfResearch = (e.Item.FindControl("tbActiveAreaOfResearchEdit") as HtmlInputControl).Value;
                // string PrevAreaOfResearch = (e.Item.FindControl("tbPrevAreaOfResearchEdit") as HtmlInputControl).Value;            
                string Rank = (e.Item.FindControl("tbRankEdit") as HtmlInputControl).Value;
                string Description = (e.Item.FindControl("tbDescriptionEdit") as HtmlInputControl).Value;
                string MailingAddress = (e.Item.FindControl("tbMailingAddressEdit") as HtmlInputControl).Value;


                string Email = (e.Item.FindControl("tbEmailEdit") as HtmlInputControl).Value;
                Email = Email.Trim();
                string Status = (e.Item.FindControl("ddlStatusEdit") as DropDownList).SelectedValue;
                string Comments = (e.Item.FindControl("tbCommentsEdit") as HtmlInputControl).Value;
                string PhoneAndFax = (e.Item.FindControl("tbPhoneAndFaxEdit") as HtmlInputControl).Value;
                //string NameString = (e.Item.FindControl("tbNameStringEdit") as HtmlInputControl).Value;
                //string Status       = (e.Item.FindControl("tbStatusEdit") as HtmlInputControl).Value;
                //string Password = (e.Item.FindControl("tbPasswordEdit") as HtmlInputControl).Value;
                string Major = (e.Item.FindControl("tbMajorEdit") as HtmlInputControl).Value;
                string Webpage = (e.Item.FindControl("tbWebpageEdit") as HtmlInputControl).Value;
                // ExternalReviewer er = erbal.GetExtRevByID(erID)[0];
                if (!IsEmailOK(Email, typeofDBOperation.Update))
                { return; }

               erBAL.UpdateExtRev(erID, Name, Rank, MailingAddress, Email, Major, Specialty, PhoneAndFax, ActiveAreaOfResearch, "", Webpage, Comments, 0, 0, 0, 0
                    , Status, er.Password, "", "", Name, Description,er.Name_1, er.Name_2, er.Name_3, er.Name_4, er.Salt);
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
                //ToggleElements(e.Item, false);
            }

        }

        private bool IsEmailOK(string Email, typeofDBOperation type)
        {
            if (!PromotionApplication.IsValidEmail(Email))
            {
                lblProgrammaticPopup0.Text = "Email is not valid!.";
                programmaticModalPopup0.Show();
                return false;
            }
            if (erBAL.GetExtRevByEmail(Email).Count > 1 && type == typeofDBOperation.Update ||
                erBAL.GetExtRevByEmail(Email).Count > 0 && type == typeofDBOperation.Insert)
            {
                lblProgrammaticPopup0.Text = "External Reviewer with this email address already present.";
                programmaticModalPopup0.Show();
                return false;
            }
            return true;
        }

        protected void lbtnAddExtReviewer_Click(object sender, EventArgs e)
        {
            string email = tbEmailAdd.Value;
            email = email.Trim();
            if (!IsEmailOK(email, typeofDBOperation.Insert))
            { return; }
             erBAL.InsertExtRev(tbNameAdd.Value, tbRankAdd.Value, tbMailingAddressAdd.Value, tbEmailAdd.Value, tbMajorAdd.Value,
                tbSpecialtyAdd.Value, tbPhoneAndFaxAdd.Value, tbActiveAreaOfResearchAdd.Value, "", tbWebpageAdd.Value, tbCommentsAdd.Value, 0, 0, 0, 0
                , RecordStatus.Active.ToString(), tbPasswordAdd.Value, "", "", tbNameAdd.Value, tbDescriptionAdd.Value, tbNameAdd.Value, 
                "","","","");
            DatabindControls();
        }
    }
