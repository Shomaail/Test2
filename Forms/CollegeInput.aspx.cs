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
using System.Collections.Generic;
using BL.Data;

public partial class Forms_CollegeInput : System.Web.UI.Page
    {
        BAL bal = new BAL();
        private enum Mode { InputByDean = 1, View = 2 }

        public override string StyleSheetTheme
        {
            get
            {
                return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            if (Master.CurrentFormLevel == -1)
            {
                Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
                return;
            }
            Instruction1.Text = Master.CurrentFormInstruction;
            DatabindControls();
           // SwitchMode((Mode)Master.CurrentFormLevel);
            

            //hide buttons when printing
            if (Utils.IsPrintMode())
            {
            
                Instruction1.Visible = false;
            }
        }
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    bool success = ChairmanInput1.SaveComments(Master.ApplicationID);
        //    if (success)
        //    {
        //        labelProgrammaticPopup0.Text = "Saved Successfully.";
        //        programmaticModalPopup0.Show();
        //        ShowMessage("Saved Successfully");
        //    }
        //    else
        //    {
        //        labelProgrammaticPopup0.Text = "Please make sure you input all the fields.";
        //        programmaticModalPopup0.Show();
        //        ShowError("Please make sure you input all the fields.");
        //    }


        //}
        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
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
                case Mode.InputByDean:                  
                    break;
                case Mode.View:
                   
                    
                    break;
            }
        }
        public void DatabindControls()
        {           
            List<Form_CollegeInput> lfci =  bal.GetForm_CollegeInput(Master.ApplicationID);

            if(lfci.Count > 0 )
            {
            Form_CollegeInput fci = lfci[0];
            //if(
            //   fci.F1 == null &&
            //   fci.F2 == null &&
            //   fci.F3 == null &&
            //   fci.F4 == null &&
            //   fci.F5 == null &&
            //   fci.F6 == null &&
            //   fci.F7 == null &&
            //   fci.F8 == null &&
            //   fci.F9 == null &&
            //   fci.F10== null &&
            //   fci.F11== null &&
            //   fci.F12== null &&
            //   fci.F13== null &&
            //   fci.F14== null &&
            //   fci.F15== null &&
            //   fci.F16== null &&
            //   fci.F17== null &&
            //   fci.F18== null &&
            //   fci.F19 == null )
            //{
            //    fci.F1 = fci.Teaching;
            //    fci.F7 = fci.Research;
            //    fci.F17 = fci.CommunityService;
            //    fci.F19 = fci.CommentsAndRecommendation;
            //}
            tbF1.Text = fci.F1;
            tbMeetingNo.Text = fci.MeetingNo;
            tbMeetingDate.Text = fci.MeetingDate.HasValue?fci.MeetingDate.Value.ToString("yyyy-MM-dd"):"";
            lblMeetingDate.Text = fci.MeetingDate.HasValue ? fci.MeetingDate.Value.ToLongDateString(): ""; 
            if (!string.IsNullOrEmpty(fci.F1) && fci.F1.Length > 100 && !string.IsNullOrEmpty(fci.MeetingNo) 
                && fci.MeetingDate != null)
            {
                ShowMessage("The College Input is complete");
            }
            else
            {
                ShowError("Form Incomplete, College Input need to have 100 characters moreover, Meeting Number and Date are necessary");
            }            
        }
            else
            {
                ShowError("Form Incomplete");
            }
        //divCollegeInput.DataBind();
        tbMeetingDate.DataBind();
        lblMeetingDate.DataBind();
        divTopOfFormMsgFailure.DataBind();
            lblMessageFailure.DataBind();
            divTopOfFormMsgSuccess.DataBind();
            lblMessageSuccess.DataBind();
        }
        protected void tb_TextChanged(object sender, EventArgs e)
        {
            if (Master.CurRoleID != (byte)RoleID.College_Dean &&
               Master.CurRoleID != (byte)RoleID.TopAuthority)
            {
                return;
            }
            List<Form_CollegeInput> lfci = bal.GetForm_CollegeInput(Master.ApplicationID);
            if (lfci.Count > 0)
            {
            Form_CollegeInput fci = lfci[0];
            fci.F1 = tbF1.Text;
            fci.MeetingNo = tbMeetingNo.Text;
            DateTime mDate;
            fci.MeetingDate = tbMeetingDate.Text == ""? null :(DateTime.TryParse(tbMeetingDate.Text, out mDate)
                ? (DateTime.Parse(tbMeetingDate.Text) < DateTime.Now.AddYears(-30) || DateTime.Parse(tbMeetingDate.Text) > DateTime.Now)
                ? (DateTime?)null: DateTime.Parse(tbMeetingDate.Text) : null);            

            bal.UpdateForm_CollegeInput(Master.ApplicationID, fci.F1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, fci.MeetingNo,fci.MeetingDate);
            }
            else
            {
            Form_CollegeInput fci = new Form_CollegeInput();
            fci.F1 = tbF1.Text;
            fci.MeetingNo = tbMeetingNo.Text;
            DateTime mDate;
            //            fci.MeetingDate = tbMeetingDate.Text == "" ? null : (DateTime.TryParse(tbMeetingDate.Text, out mDate) ? DateTime.Parse(tbMeetingDate.Text) : (DateTime?)null);
            fci.MeetingDate = tbMeetingDate.Text == "" ? null : (DateTime.TryParse(tbMeetingDate.Text, out mDate)
                      ? (DateTime.Parse(tbMeetingDate.Text) < DateTime.Now.AddYears(-30) || DateTime.Parse(tbMeetingDate.Text) > DateTime.Now)
                      ? (DateTime?)null : DateTime.Parse(tbMeetingDate.Text) : null);

            bal.InsertForm_CollegeInput(Master.ApplicationID, fci.F1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, fci.MeetingNo,fci.MeetingDate);
            }
            DatabindControls();
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
