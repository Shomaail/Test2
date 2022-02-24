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

public partial class Forms_ChairmanInput : System.Web.UI.Page
    {
        BAL bal = new BAL();
        private enum Mode { InputByChairman = 1, View = 2 }

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
                case Mode.InputByChairman:                  
                    break;
                case Mode.View:
                   
                    
                    break;
            }
        }
        public void DatabindControls()
        {           
            List<Form_ChairmanInput> lfci =  bal.GetChairmanInput(Master.ApplicationID);

            if(lfci.Count > 0 )
            {
                 Form_ChairmanInput fci = lfci[0];                
                    if( fci.F1 == null &&
                       fci.F2 == null &&
                       fci.F3 == null &&
                       fci.F4 == null &&
                       fci.F5 == null &&
                       fci.F6 == null &&
                       fci.F7 == null &&
                       fci.F8 == null &&
                       fci.F9 == null &&
                       fci.F10== null &&
                       fci.F11== null &&
                       fci.F12== null &&
                       fci.F13== null &&
                       fci.F14== null &&
                       fci.F15== null &&
                       fci.F16== null &&
                       fci.F17== null &&
                       fci.F18== null &&
                       fci.F19 == null )
                    {
                        fci.F1 = fci.Teaching;
                        fci.F7 = fci.Research;
                        fci.F17 = fci.CommunityService;
                        fci.F19 = fci.CommentsAndRecommendation;
                    }
                     tbF1.Text = fci.F1;
                     tbF2.Text = fci.F2;
                     tbF3.Text = fci.F3;
                     tbF4.Text = fci.F4;
                     tbF5.Text = fci.F5;
                     tbF6.Text = fci.F6;
                     tbF7.Text = fci.F7;
                     tbF8.Text = fci.F8;
                     tbF9.Text = fci.F9;
                     tbF10.Text = fci.F10;
                     tbF11.Text = fci.F11;
                     tbF12.Text = fci.F12;
                     tbF13.Text = fci.F13;
                     tbF14.Text = fci.F14;
                     tbF15.Text = fci.F15;
                     tbF16.Text = fci.F16;
                     tbF17.Text = fci.F17;
                     tbF18.Text = fci.F18;
                     tbF19.Text = fci.F19;
                     tbMeetingNo.Text = fci.MeetingNo;
            tbMeetingDate.Text = fci.MeetingDate.HasValue?fci.MeetingDate.Value.ToString("yyyy-MM-dd"):"";
            lblMeetingDate.Text = fci.MeetingDate.HasValue ? fci.MeetingDate.Value.ToLongDateString(): ""; 
            if (   !string.IsNullOrEmpty(fci.F1) && fci.F1.Length > 100 && !string.IsNullOrEmpty(fci.F2) && fci.F2.Length > 100 
                && !string.IsNullOrEmpty(fci.F3) && fci.F3.Length > 100 && !string.IsNullOrEmpty(fci.F4) && fci.F4.Length > 100 
                && !string.IsNullOrEmpty(fci.F5) && fci.F5.Length > 100 && !string.IsNullOrEmpty(fci.F6) && fci.F6.Length > 100 
                && !string.IsNullOrEmpty(fci.F7) && fci.F7.Length > 100 && !string.IsNullOrEmpty(fci.F8) && fci.F8.Length > 100 
                && !string.IsNullOrEmpty(fci.F9) && fci.F9.Length > 100 && !string.IsNullOrEmpty(fci.F10) && fci.F10.Length > 100 
                && !string.IsNullOrEmpty(fci.F11) && fci.F11.Length > 100 && !string.IsNullOrEmpty(fci.F12) && fci.F12.Length > 100 
                && !string.IsNullOrEmpty(fci.F13) && fci.F13.Length > 100 && !string.IsNullOrEmpty(fci.F14) && fci.F14.Length > 100 
                && !string.IsNullOrEmpty(fci.F15) && fci.F15.Length > 100 && !string.IsNullOrEmpty(fci.F16) && fci.F16.Length > 100 
                && !string.IsNullOrEmpty(fci.F17) && fci.F17.Length > 100 && !string.IsNullOrEmpty(fci.F18) && fci.F18.Length > 100 
                && !string.IsNullOrEmpty(fci.F19) && fci.F19.Length > 100 && !string.IsNullOrEmpty(fci.MeetingNo) 
                && fci.MeetingDate != null)
            {
                ShowMessage("The Chairman Input is complete");
            }
            else
            {
                ShowError("Form Incomplete, Each Field need to have 100 characters to complete");
            }
            //ShowMessage("The Chairman Input is complete");
        }
            else
            {
                ShowError("Form Incomplete");
            }
       // divCollegeInput.DataBind();
        tbMeetingDate.DataBind();
        lblMeetingDate.DataBind();
        divTopOfFormMsgFailure.DataBind();
            lblMessageFailure.DataBind();
            divTopOfFormMsgSuccess.DataBind();
            lblMessageSuccess.DataBind();
        }
        protected void tb_TextChanged(object sender, EventArgs e)
        {
            if(Master.CurRoleID != (byte)RoleID.DepartmentChairman &&
                Master.CurRoleID != (byte)RoleID.TopAuthority)
            {
                return;
            }
            List<Form_ChairmanInput> lfci = bal.GetChairmanInput(Master.ApplicationID);
            if (lfci.Count > 0)
            {
            Form_ChairmanInput fci = lfci[0];
            fci.F1 = tbF1.Text;
            fci.F2 = tbF2.Text;
            fci.F3 = tbF3.Text;
            fci.F4 = tbF4.Text;
            fci.F5 = tbF5.Text;
            fci.F6 = tbF6.Text;
            fci.F7 = tbF7.Text;
            fci.F8 = tbF8.Text;
            fci.F9 = tbF9.Text;
            fci.F10 = tbF10.Text;
            fci.F11 = tbF11.Text;
            fci.F12 = tbF12.Text;
            fci.F13 = tbF13.Text;
            fci.F14 = tbF14.Text;
            fci.F15 = tbF15.Text;
            fci.F16 = tbF16.Text;
            fci.F17 = tbF17.Text;
            fci.F18 = tbF18.Text;
            fci.F19 = tbF19.Text;
            fci.MeetingNo = tbMeetingNo.Text;
            DateTime mDate;
            //fci.MeetingDate = tbMeetingDate.Text == ""? null :(DateTime.TryParse(tbMeetingDate.Text, out mDate)? DateTime.Parse(tbMeetingDate.Text): (DateTime?)null);            
            fci.MeetingDate = tbMeetingDate.Text == "" ? null : (DateTime.TryParse(tbMeetingDate.Text, out mDate)
                      ? (DateTime.Parse(tbMeetingDate.Text) < DateTime.Now.AddYears(-30) || DateTime.Parse(tbMeetingDate.Text) > DateTime.Now)
                      ? (DateTime?)null : DateTime.Parse(tbMeetingDate.Text) : null);
            bal.UpdateForm_ChairmanInput(Master.ApplicationID, fci.F1, fci.F2, fci.F3, fci.F4, fci.F5
                    , fci.F6, fci.F7, fci.F8, fci.F9, fci.F10, fci.F11, fci.F12, fci.F13, fci.F14, fci.F15
                    , fci.F16, fci.F17, fci.F18, fci.F19, fci.MeetingNo,fci.MeetingDate);
            }
            else
            {
            Form_ChairmanInput fci = new Form_ChairmanInput();
            fci.F1 = tbF1.Text;
            fci.F2 = tbF2.Text;
            fci.F3 = tbF3.Text;
            fci.F4 = tbF4.Text;
            fci.F5 = tbF5.Text;
            fci.F6 = tbF6.Text;
            fci.F7 = tbF7.Text;
            fci.F8 = tbF8.Text;
            fci.F9 = tbF9.Text;
            fci.F10 = tbF10.Text;
            fci.F11 = tbF11.Text;
            fci.F12 = tbF12.Text;
            fci.F13 = tbF13.Text;
            fci.F14 = tbF14.Text;
            fci.F15 = tbF15.Text;
            fci.F16 = tbF16.Text;
            fci.F17 = tbF17.Text;
            fci.F18 = tbF18.Text;
            fci.F19 = tbF19.Text;
            fci.MeetingNo = tbMeetingNo.Text;
            DateTime mDate;
            //fci.MeetingDate = tbMeetingDate.Text == "" ? null : (DateTime.TryParse(tbMeetingDate.Text, out mDate) ? DateTime.Parse(tbMeetingDate.Text) : (DateTime?)null);
            fci.MeetingDate = tbMeetingDate.Text == "" ? null : (DateTime.TryParse(tbMeetingDate.Text, out mDate)
                      ? (DateTime.Parse(tbMeetingDate.Text) < DateTime.Now.AddYears(-30) || DateTime.Parse(tbMeetingDate.Text) > DateTime.Now)
                      ? (DateTime?)null : DateTime.Parse(tbMeetingDate.Text) : null);

            bal.InsertForm_ChairmanInput(Master.ApplicationID, fci.F1, fci.F2, fci.F3, fci.F4, fci.F5
                    , fci.F6, fci.F7, fci.F8, fci.F9, fci.F10, fci.F11, fci.F12, fci.F13, fci.F14, fci.F15
                    , fci.F16, fci.F17, fci.F18, fci.F19,fci.MeetingNo,fci.MeetingDate);
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
