using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL.Data;

    public partial class Forms_PermanentCommittee : System.Web.UI.Page
    {
        BAL bal = new BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }


            DatabindControls();
        }
        private void ShowError(string message)
        {
            Master.ReportFailure(message);
        }

        private void ShowMessage(string message)
        {
            Master.ReportSuccess(message);
        }
        private void DatabindControls()
        {
            List<Application_Role> larSC = bal.GetApplicationRole(Master.ApplicationID)
                .Where(a => a.RoleID >= (byte)RoleID.Scientific_Council_Member_1 &&
                a.RoleID <= (byte)RoleID.Scientific_Council_Member_12).ToList();

            int MinScientificCouncilMembers = int.Parse(bal.GetWorkflowAttribute()
                .Where(a => a.Attribute == GlobalAttribute.MinScientificCouncilMembers.ToString()).ToList()[0].Value);
            if (larSC.Count < MinScientificCouncilMembers)
            {
                ShowError(Resources.Resource.ScientificCouncilM6
                    .Replace("@@MinScientificCouncilMembers@@", MinScientificCouncilMembers.ToString()));
            }
            rptPermanentCommitteeSC.DataSource = larSC;

            List<Application_Role> larSCC = bal.GetApplicationRole(Master.ApplicationID)
                .Where(a => a.RoleID == (byte)RoleID.Scientific_Council_Coordinator).ToList();

            
            if (larSC.Count < 1)
            {
                ShowError("There has to be one Scientific Council Coordinator");
            }
            rptPermanentCommitteeSCC.DataSource = larSCC;

            List<Application_Role> larPS = bal.GetApplicationRole(Master.ApplicationID )
             .Where(a =>a.RoleID >= (byte)RoleID.SC_Subcommittee_Chair&&
             a.RoleID <= (byte)RoleID.SC_Subcommittee_Member_4).ToList();
            int MinPromotionSubcommitteeMembers = int.Parse(bal.GetWorkflowAttribute()
                .Where(a => a.Attribute == GlobalAttribute.MinPromotionSubcommitteeMembers.ToString()).ToList()[0].Value);
            if (larPS.Count < MinPromotionSubcommitteeMembers)
            {
                ShowError(Resources.Resource.ScientificCouncilM7
                    .Replace("@@MinPromotionSubcommitteeMembers@@", MinPromotionSubcommitteeMembers.ToString()));
            }
            rptPermanentCommitteePSubComm.DataSource = larPS;
            if(larSC.Count >= MinScientificCouncilMembers && larPS.Count >= MinPromotionSubcommitteeMembers)
            {
                ShowMessage(Resources.Resource.ScientificCouncilM8);
            }

            ddlCommitteeType.DataSource = bal.GetPermanentCommitteeType().Select(a => new { Text = a.Title + " (" + a.PermanentCommitteeTypeID + ")", Value = a.PermanentCommitteeTypeID });
            ddlCommitteeType.DataBind();
            rptPermanentCommitteeSC.DataBind();
            rptPermanentCommitteeSCC.DataBind();
            rptPermanentCommitteePSubComm.DataBind();
            divTopOfFormMsgFailure.DataBind();
            lblMessageFailure.DataBind();
            divTopOfFormMsgSuccess.DataBind();
            lblMessageSuccess.DataBind();
            divWarningMsg.DataBind();
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
            //Toggle DDL
           // item.FindControl("ddlCommitteeTypeEdit").Visible = isEdit;

            //Toggle TextBoxes.
            item.FindControl("tbEmployeeIDEdit").Visible = isEdit;
            if((item.Parent as Repeater).ID == "rptPermanentCommitteeSC")
            {
                item.FindControl("lbtnSearchSC").Visible = isEdit;
            }
            else
            {
                item.FindControl("lbtnSearchSCSubComm").Visible = isEdit;
            }
            
            
        }
        private bool IsEmployeeIDOK(string employeeID, int PermanentCommitteeTypeID)
        {
            if (employeeID == "")
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM10;
                programmaticModalPopup0.Show();
                return false;
            }
            if (bal.GetEmployeeByPK(employeeID).Count == 0)
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM11.Replace("@@EmployeeID@@",employeeID);
                programmaticModalPopup0.Show();
                return false;
            }
            if(PermanentCommitteeTypeID == (int) PermanentCommitteeTypeTitle.Scientific_Council)
            {
                if (bal.GetApplicationRole(Master.ApplicationID)
                    .Where(a => a.RoleID >= (byte)RoleID.Scientific_Council_Member_1 &&
                    a.RoleID <= (byte)RoleID.Scientific_Council_Coordinator&&
                    a.EmployeeID == employeeID).Count() != 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM5
                                        .Replace("@@EmployeeID@@", employeeID)
                                        .Replace("@@PermanentCommittee@@", ((PermanentCommitteeTypeTitle)PermanentCommitteeTypeID)
                                        .ToString()
                                        .Replace("_", " "));
                }

            }
            else
            {
                if (bal.GetApplicationRole(Master.ApplicationID)
                    .Where(a => a.RoleID >= (byte)RoleID.SC_Subcommittee_Chair &&
                    a.RoleID <= (byte)RoleID.SC_Subcommittee_Member_4 &&
                    a.EmployeeID == employeeID).Count() != 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM5
                                        .Replace("@@EmployeeID@@", employeeID)
                                        .Replace("@@PermanentCommittee@@", ((PermanentCommitteeTypeTitle)PermanentCommitteeTypeID)
                                        .ToString()
                                        .Replace("_", " "));
                }
            }
            //if (bal.GetPermanentCommittee().Where(ad => ad.EmployeeID == employeeID 
            //&& ad.PermanentCommitteeTypeID == PermanentCommitteeTypeID).Count() != 0)
            //{
            //    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM5
            //        .Replace("@@EmployeeID@@", employeeID)
            //        .Replace("@@PermanentCommittee@@", ((PermanentCommitteeTypeTitle)PermanentCommitteeTypeID)
            //        .ToString()
            //        .Replace("_", " "))
            //    ;
            //    programmaticModalPopup0.Show();
            //    return false;
            //}
            return true;

        }
        #region Event Handler
        

        protected void rptPermanentCommitteeSC_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                byte roleID = byte.Parse(e.CommandArgument.ToString());
                if (bal.GetApplicationRole(Master.ApplicationID)
                    .Where(a=> a.RoleID >= (byte)RoleID.Scientific_Council_Member_1 &&
                    a.RoleID <= (byte)RoleID.Scientific_Council_Member_12)
                    .Count() == 1)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM12;
                    programmaticModalPopup0.Show();
                    return;
                }
                if (bal.GetAppTaskLog(Master.ApplicationID).Where(a=>a.TaskID == (int) TaskIDs.Scientific_Council_Membership_1 && !a.Completed).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM16;
                    programmaticModalPopup0.Show();
                    return;

                }
                bal.DeleteApplicationRoles(Master.ApplicationID, roleID);
                bal.ReorderMembersInPermanentCommitteeofApp(Master.ApplicationID, PermanentCommitteeTypeTitle.Scientific_Council);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                //  (e.Item.FindControl("ddlCommitteeTypeEdit") as DropDownList).DataSource = bal.GetPermanentCommitteeType().Select(a => new { Text = a.Title + " (" + a.PermanentCommitteeTypeID + ")", Value = a.PermanentCommitteeTypeID });
                if (bal.GetAppTaskLog(Master.ApplicationID).Where(a => a.TaskID == (int)TaskIDs.Scientific_Council_Membership_1 && !a.Completed).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM17;
                    programmaticModalPopup0.Show();
                    return;

                }
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                byte roleID = byte.Parse(e.CommandArgument.ToString());
                string empID = (e.Item.FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value;
                Application_Role ar = bal.GetApplicationRole(Master.ApplicationID)
                    .Where(a => a.RoleID == roleID && a.ApplicationID == Master.ApplicationID).ToList()[0];
                if (ar.EmployeeID != empID && !IsEmployeeIDOK(empID, (int) PermanentCommitteeTypeTitle.Scientific_Council))
                {
                    return;
                }
                bal.UpdateApplicationRoles(ar.ApplicationID, ar.RoleID, empID, 0, ar.IsActing);
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
                //ToggleElements(e.Item, false);
            }
        }
        protected void rptPermanentCommitteeSCC_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                byte roleID = byte.Parse(e.CommandArgument.ToString());
                if (bal.GetApplicationRole(Master.ApplicationID)
                    .Where(a => a.RoleID == (byte)RoleID.Scientific_Council_Coordinator)
                    .Count() == 1)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM24;
                    programmaticModalPopup0.Show();
                    return;
                }
                //if (bal.GetAppTaskLog(Master.ApplicationID).Where(a => a.TaskID == (int)TaskIDs.Scientific_Council_Coordination && !a.Completed).Count() > 0)
                //{
                //    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM16;
                //    programmaticModalPopup0.Show();
                //    return;

                //}
                //bal.DeleteApplicationRoles(Master.ApplicationID, roleID);
                //bal.ReorderMembersInPermanentCommitteeofApp(Master.ApplicationID, PermanentCommitteeTypeTitle.Scientific_Council_Coordinator);
                //DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                //  (e.Item.FindControl("ddlCommitteeTypeEdit") as DropDownList).DataSource = bal.GetPermanentCommitteeType().Select(a => new { Text = a.Title + " (" + a.PermanentCommitteeTypeID + ")", Value = a.PermanentCommitteeTypeID });
                if (bal.GetAppTaskLog(Master.ApplicationID).Where(a => a.TaskID == (int)TaskIDs.Scientific_Council_Coordination && !a.Completed).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM25;
                    programmaticModalPopup0.Show();
                    return;

                }
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                byte roleID = byte.Parse(e.CommandArgument.ToString());
                string empID = (e.Item.FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value;
                Application_Role ar = bal.GetApplicationRole(Master.ApplicationID)
                    .Where(a => a.RoleID == roleID && a.ApplicationID == Master.ApplicationID).ToList()[0];
                if (ar.EmployeeID != empID && !IsEmployeeIDOK(empID, (int)PermanentCommitteeTypeTitle.Scientific_Council_Coordinator ))
                {
                    return;
                }
                bal.UpdateApplicationRoles(ar.ApplicationID, ar.RoleID, empID, 0, ar.IsActing);
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
                //ToggleElements(e.Item, false);
            }
        }
        protected void rptPermanentCommitteePSubComm_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                byte roleID = byte.Parse(e.CommandArgument.ToString());
                if (bal.GetApplicationRole(Master.ApplicationID)
                    .Where(a => a.RoleID >= (byte)RoleID.SC_Subcommittee_Chair &&
                    a.RoleID <= (byte)RoleID.SC_Subcommittee_Member_4)
                    .Count() == 1)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM13;
                    programmaticModalPopup0.Show();
                    return;
                }
                if (bal.GetAppTaskLog(Master.ApplicationID).Where(a => a.TaskID == (int)TaskIDs.SC_Subcommittee_Chairmanship && !a.Completed).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM18;
                    programmaticModalPopup0.Show();
                    return;

                }
                bal.DeleteApplicationRoles(Master.ApplicationID, roleID);
                bal.ReorderMembersInPermanentCommitteeofApp(Master.ApplicationID, PermanentCommitteeTypeTitle.SC_Subcommittee);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                if (bal.GetAppTaskLog(Master.ApplicationID).Where(a => a.TaskID == (int)TaskIDs.SC_Subcommittee_Chairmanship && !a.Completed).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM19;
                    programmaticModalPopup0.Show();
                    return;

                }
                //  (e.Item.FindControl("ddlCommitteeTypeEdit") as DropDownList).DataSource = bal.GetPermanentCommitteeType().Select(a => new { Text = a.Title + " (" + a.PermanentCommitteeTypeID + ")", Value = a.PermanentCommitteeTypeID });
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                byte roleID = byte.Parse(e.CommandArgument.ToString());
                string empID = (e.Item.FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value;
                Application_Role ar = bal.GetApplicationRole(Master.ApplicationID)
                    .Where(a => a.RoleID == roleID && a.ApplicationID == Master.ApplicationID).ToList()[0];
                if (ar.EmployeeID != empID && !IsEmployeeIDOK(empID, (int)PermanentCommitteeTypeTitle.SC_Subcommittee))
                {
                    return;
                }
                bal.UpdateApplicationRoles(ar.ApplicationID, ar.RoleID, empID, 0, ar.IsActing);
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
                //ToggleElements(e.Item, false);
            }
        }
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            int MaxScientificCouncilMembers = int.Parse(bal.GetWorkflowAttribute()
                   .Where(a => a.Attribute == GlobalAttribute.MaxScientificCouncilMembers.ToString()).ToList()[0].Value);
            int MaxPromotionSubcommitteeMembers = int.Parse(bal.GetWorkflowAttribute()
                  .Where(a => a.Attribute == GlobalAttribute.MaxPromotionSubcommitteeMembers.ToString()).ToList()[0].Value);
            string employeeID = tbEmployeeIDAdd.Value.Trim();
            int extEmployeeID = 0;
            if (employeeID == "")
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM1;
                programmaticModalPopup0.Show();
                return;
            }
            if (bal.GetAppTaskLog(Master.ApplicationID).Where(a => a.TaskID == (int)TaskIDs.Scientific_Council_Membership_1 && !a.Completed).Count() > 0)
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM20;
                programmaticModalPopup0.Show();
                return;

            }
            if (bal.GetAppTaskLog(Master.ApplicationID).Where(a => a.TaskID == (int)TaskIDs.SC_Subcommittee_Chairmanship && !a.Completed).Count() > 0)
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM21;
                programmaticModalPopup0.Show();
                return;

            }

            if (bal.GetEmployeeByPK(employeeID).Count == 0)
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM2.Replace("@@EmployeeID@@", employeeID);
                programmaticModalPopup0.Show();
                return;
            }
            List<Application_Role> committee;
            committee = bal.GetApplicationRole(Master.ApplicationID)
                .Where(a => a.RoleID >= (byte)RoleID.SC_Subcommittee_Chair &&
                    a.RoleID <= (byte)RoleID.SC_Subcommittee_Member_4).ToList();
            if (committee.Count == MaxPromotionSubcommitteeMembers && int.Parse(ddlCommitteeType.SelectedValue) == (int)PermanentCommitteeTypeTitle.SC_Subcommittee)
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM3
                    .Replace("@@MaxPromotionSubcommitteeMembers@@", MaxPromotionSubcommitteeMembers.ToString());
                programmaticModalPopup0.Show();
                return;
            }
            committee = bal.GetApplicationRole(Master.ApplicationID)
              .Where(a => a.RoleID >= (byte)RoleID.Scientific_Council_Member_1 &&
                    a.RoleID <= (byte)RoleID.Scientific_Council_Member_12).ToList();
            if (committee.Count == MaxScientificCouncilMembers && int.Parse(ddlCommitteeType.SelectedValue) == (int)PermanentCommitteeTypeTitle.Scientific_Council)
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM4
                    .Replace("@@MaxScientificCouncilMembers@@", MaxScientificCouncilMembers.ToString());
                programmaticModalPopup0.Show();
                return;
            }
            if(int.Parse(ddlCommitteeType.SelectedValue) == (int) PermanentCommitteeTypeTitle.Scientific_Council)
            {
                if (bal.GetApplicationRole(Master.ApplicationID).Where(a => a.RoleID >= (byte)RoleID.Scientific_Council_Member_1 &&
                    a.RoleID <= (byte)RoleID.Scientific_Council_Coordinator && a.EmployeeID == employeeID).Count() != 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM5
                  .Replace("@@EmployeeID@@", employeeID)
                  .Replace("@@PermanentCommittee@@", PermanentCommitteeTypeTitle.Scientific_Council.ToString().Replace("_"," "));
                    programmaticModalPopup0.Show();
                    return;
                }
                else
                {
                    //bal.InsertApplicationRoles(Master.ApplicationID,, employeeID, 0);
                    bal.InsertMemberInPermanentCommitteeOfApp(Master.ApplicationID, employeeID, extEmployeeID , PermanentCommitteeTypeTitle.Scientific_Council);
                }
            }
            else if (int.Parse(ddlCommitteeType.SelectedValue) == (int)PermanentCommitteeTypeTitle.SC_Subcommittee)
            {
                if (bal.GetApplicationRole(Master.ApplicationID).Where(a => a.RoleID >= (byte)RoleID.SC_Subcommittee_Chair &&
                    a.RoleID <= (byte)RoleID.SC_Subcommittee_Member_4 && a.EmployeeID == employeeID).Count() != 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM5
                  .Replace("@@EmployeeID@@", employeeID)
                  .Replace("@@PermanentCommittee@@", PermanentCommitteeTypeTitle.SC_Subcommittee.ToString().Replace("_", " "));
                    programmaticModalPopup0.Show();
                    return;
                }
                else
                {
                    //bal.InsertApplicationRoles(Master.ApplicationID,, employeeID, 0);
                    bal.InsertMemberInPermanentCommitteeOfApp(Master.ApplicationID, employeeID, extEmployeeID , PermanentCommitteeTypeTitle.SC_Subcommittee);
                }
            }
            else if (int.Parse(ddlCommitteeType.SelectedValue) == (int)PermanentCommitteeTypeTitle.Scientific_Council_Coordinator)
            {
                if (bal.GetApplicationRole(Master.ApplicationID).Where(a => a.RoleID == (byte)RoleID.Scientific_Council_Coordinator && a.EmployeeID == employeeID).Count() != 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM5
                  .Replace("@@EmployeeID@@", employeeID)
                  .Replace("@@PermanentCommittee@@", PermanentCommitteeTypeTitle.Scientific_Council_Coordinator.ToString().Replace("_", " "));
                    programmaticModalPopup0.Show();
                    return;
                }
                else
                {
                    //bal.InsertApplicationRoles(Master.ApplicationID,, employeeID, 0);
                    bal.InsertMemberInPermanentCommitteeOfApp(Master.ApplicationID, employeeID, extEmployeeID , PermanentCommitteeTypeTitle.Scientific_Council_Coordinator);
                }
            }
            tbEmployeeIDAdd.Value = "";
            lblNameAdd.Text = "";


            DatabindControls();
        }
        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            string employeeID = tbEmployeeIDAdd.Value;
            if (employeeID == "")
            {
                lblNameAdd.Text = Resources.Resource.ScientificCouncilM9;
                return;
            }
            //if (!employeeID.Contains('@'))
            //{
            //    employeeID = employeeID.PadLeft(8, '0');
            //    employeeID = "@" + employeeID;
            //    tbEmployeeIDAdd.Value = employeeID;
            //}
            if (bal.GetEmployeeByPK(employeeID).Count == 0)
            {
                lblNameAdd.Text = Resources.Resource.ScientificCouncilM9;
                return;
            }
            lblNameAdd.Text = bal.GetEmployeeByPK(employeeID)[0].NameString;
        }
        protected void lbtnSearchSC_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as RepeaterItem).ItemIndex;
            string employeeID = (rptPermanentCommitteeSC.Items[rowIndex].FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value;
            if (employeeID == "")
            {
                (rptPermanentCommitteeSC.Items[rowIndex].FindControl("lblNameEdit") as Label).Text = " No Employee found";
                return;
            }
            //if (!employeeID.Contains('@'))
            //{
            //    employeeID = employeeID.PadLeft(8, '0');
            //    employeeID = "@" + employeeID;
            //    (rptPermanentCommitteeSC.Items[rowIndex].FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value = employeeID;
            //}
            if (bal.GetEmployeeByPK(employeeID).Count == 0)
            {
                (rptPermanentCommitteeSC.Items[rowIndex].FindControl("lblNameEdit") as Label).Text = " No Employee found";
                return;
            }
           (rptPermanentCommitteeSC.Items[rowIndex].FindControl("lblNameEdit") as Label).Text =
               " " + bal.GetEmployeeByPK(employeeID)[0].NameString
               + " (" + bal.GetEmployeeByPK(employeeID)[0].Department1.ShortName + ")";
        }
        protected void lbtnSearchSCSubComm_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as RepeaterItem).ItemIndex;
            string employeeID = (rptPermanentCommitteePSubComm.Items[rowIndex].FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value;
            if (employeeID == "")
            {
                (rptPermanentCommitteePSubComm.Items[rowIndex].FindControl("lblNameEdit") as Label).Text = " No Employee found";
                return;
            }
            //if (!employeeID.Contains('@'))
            //{
            //    employeeID = employeeID.PadLeft(8, '0');
            //    employeeID = "@" + employeeID;
            //    (rptPermanentCommitteePSubComm.Items[rowIndex].FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value = employeeID;
            //}
            if (bal.GetEmployeeByPK(employeeID).Count == 0)
            {
                (rptPermanentCommitteePSubComm.Items[rowIndex].FindControl("lblNameEdit") as Label).Text = " No Employee found";
                return;
            }
           (rptPermanentCommitteePSubComm.Items[rowIndex].FindControl("lblNameEdit") as Label).Text =
               " " + bal.GetEmployeeByPK(employeeID)[0].NameString
               + " (" + bal.GetEmployeeByPK(employeeID)[0].Department1.ShortName + ")";
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
    }
