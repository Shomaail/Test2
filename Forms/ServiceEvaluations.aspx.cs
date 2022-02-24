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
using BL.Data;
using System.Collections.Generic;
using System.Linq;

    public partial class Forms_ServiceEvaluations : System.Web.UI.Page
    {
        BAL bal = new BAL();
        private enum Mode { InputByFA = 1, View = 2 }
        public override string StyleSheetTheme
        {
            get
            {
                return Utils.IsPrintMode() ? Utils.PrinterStyleSheet : base.StyleSheetTheme;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (Master.CurrentFormLevel == -1)
            {
                Response.Redirect("Message.aspx?applicationID=" + Master.ApplicationID + "&roleID=" + Master.CurRoleID);
                return;
            }
            DatabindControls();
            Instruction1.Text = Master.CurrentFormInstruction;

            SwitchMode((Mode)Master.CurrentFormLevel);

            //hide buttons when printing
            if (Utils.IsPrintMode())
            {
              
            }
        }

        public void DatabindControls()
        {
            List<Form_ServiceYears> lfse = bal.GetForm_ServiceYears(Master.ApplicationID);
            if (lfse.Count > 0)
            {
                Form_ServiceYears fsy = lfse[0];
                tbF1.Text = fsy.Evaluations;
              //  ValidationChecks(fsy);
                if (fsy.Evaluations != null && fsy.Evaluations.Length > 2 )
                {
                    ShowMessage(Resources.Resource.AnnualEvaluationM1);
                }
                else
                {
                    ShowError(Resources.Resource.AnnualEvaluationM2);
                }
            }
            else
            {
                ShowError(Resources.Resource.AnnualEvaluationM3);
            }
            divCollegeInput.DataBind();
            divTopOfFormMsgFailure.DataBind();
            lblMessageFailure.DataBind();
            divTopOfFormMsgSuccess.DataBind();
            lblMessageSuccess.DataBind();
        }
        private void ValidationChecks(Form_ServiceYears obj)
        {
            if (obj.Evaluations == null || obj.Evaluations.Length < 2)
            {
                tbF1.BorderColor = System.Drawing.Color.OrangeRed;
            }
            else
            {
                tbF1.BorderColor = System.Drawing.Color.Gray;
            }
            //if (obj.F2.Length < 100)
            //{
            //    tbF2.BorderColor = System.Drawing.Color.OrangeRed;
            //}
            //else
            //{
            //    tbF2.BorderColor = System.Drawing.Color.Gray;
            //}
            //if (obj.F3.Length < 100)
            //{
            //    tbF3.BorderColor = System.Drawing.Color.OrangeRed;
            //}
            //else
            //{
            //    tbF3.BorderColor = System.Drawing.Color.Gray;
            //}
            //if (obj.F4.Length < 100)
            //{
            //    tbF4.BorderColor = System.Drawing.Color.OrangeRed;
            //}
            //else
            //{
            //    tbF4.BorderColor = System.Drawing.Color.Gray;
            //}
            //if (obj.F5 == "")
            //{
            //    tbF5.BorderColor = System.Drawing.Color.OrangeRed;
            //}
            //else
            //{
            //    tbF5.BorderColor = System.Drawing.Color.Gray;
            //}
            //if (obj.F6 == "")
            //{
            //    tbF6.BorderColor = System.Drawing.Color.OrangeRed;
            //}
            //else
            //{
            //    tbF6.BorderColor = System.Drawing.Color.Gray;
            //}
        }
        #region Event Handler
        protected void tb_TextChanged(object sender, EventArgs e)
        {
            List<Form_ServiceYears> lfsy = bal.GetForm_ServiceYears(Master.ApplicationID);
            if (lfsy.Count > 0)
            {
                Form_ServiceYears fsy = lfsy[0];
                fsy.Evaluations = tbF1.Text;
                bal.UpdateForm_ServiceYears(fsy.ApplicationID, fsy.App_Service_NumYears, fsy.App_Service_NumMonths, fsy.App_Saudi_NumYears, fsy.App_Saudi_NumMonths
           , fsy.Service_NumYears, fsy.Service_NumMonths, fsy.Saudi_NumYears, fsy.Saudi_NumMonths, fsy.Evaluations
           , fsy.TeachingScoreYear1, fsy.ResearchScoreYear1, fsy.ComServiceScoreYear1, fsy.TeachingScoreYear2, fsy.ResearchScoreYear2, fsy.ComServiceScoreYear2
           , fsy.Year1, fsy.Year2
        );
            }
            else
            {
                Form_ServiceYears fsy = new Form_ServiceYears();
                fsy.Evaluations = tbF1.Text;              
                bal.InsertForm_ServiceYears(fsy.ApplicationID, fsy.App_Service_NumYears, fsy.App_Service_NumMonths, fsy.App_Saudi_NumYears, fsy.App_Saudi_NumMonths
           , fsy.Service_NumYears, fsy.Service_NumMonths, fsy.Saudi_NumYears, fsy.Saudi_NumMonths, fsy.Evaluations
           , fsy.TeachingScoreYear1, fsy.ResearchScoreYear1, fsy.ComServiceScoreYear1, fsy.TeachingScoreYear2, fsy.ResearchScoreYear2, fsy.ComServiceScoreYear2
           , fsy.Year1, fsy.Year2);
            }
            DatabindControls();
        }
        #endregion

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
                case Mode.InputByFA:
                    // AnnualEvaluation1.LoadEvaluation(Master.ApplicationID);
                    break;
                case Mode.View:
                    //
                   
                    break;
            }
        }
    }
