using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BL.Data;


    public partial class Controls_ComposeEmail : System.Web.UI.UserControl
    {
        public event EventHandler EmailSent;
        public event EventHandler EmailNotSent;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //    return;
        }
        public void LoadData(List<EmailContent> Emails)
        {
            GridViewEmail.DataSource = Emails;
            GridViewEmail.ShowFooter = true;
            GridViewEmail.DataBind();
            programmaticModalPopup.Show();

        }
        public void LoadDataForPreview(List<EmailContent> Emails)
        {
            GridViewEmail.DataSource = Emails;
            GridViewEmail.ShowFooter = false;
            GridViewEmail.DataBind();
            programmaticModalPopup.Show();

        }
        protected void ButtonSendEmail_Click(object sender, EventArgs e)
        {
            int appID = -1;
            string Email = "";
            try
            {
                appID = int.Parse(Request.Params.Get("applicationID").ToString());
            }
            catch
            {
                try
                {
                    appID = int.Parse(Session["applicationID"].ToString());
                }
                catch
                {

                }
            }
            try
            {
                Employee employee = (Employee)Session["user"];
                Email = employee.Email;
            }
            catch
            {

            }

            foreach (GridViewRow row in GridViewEmail.Rows)
            {
                if (GridViewEmail.Rows.Count == 1)
                {
                    EmailBody = (row.FindControl("TextBoxBody") as TextBox).Text;
                }
                Emailer.Send((row.FindControl("TextBoxTo") as TextBox).Text,
                    (row.FindControl("TextBoxSubject") as TextBox).Text,
                    (row.FindControl("TextBoxBody") as TextBox).Text, Email, appID);

            }
            if (EmailSent != null)
            {
                EmailSent(this, e);
            }
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (EmailNotSent != null)
            {
                EmailNotSent(this, e);
            }
        }
        public void SetReadOnly(bool status)
        {
            //txtEmail.ReadOnly = status;
            //lblName.ReadOnly = status;
            //lblRank.ReadOnly = status;

            //button.Visible = !status;
        }
        private string emailBody;
        public string EmailBody
        {
            get
            {

                return emailBody;
            }
            set
            {
                emailBody = value;
            }
        }


    }
