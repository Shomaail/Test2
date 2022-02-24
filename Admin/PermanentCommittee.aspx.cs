using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL.Data;

    public partial class Admin_PermanentCommittee : System.Web.UI.Page
    {
        BAL bal = new BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            DatabindControls();
            ReOrderPermanentCommitteeInAppRoles();
    }
        private void ShowError(string message)
        {
            // Master.ReportFailure(message);
            lblMessageFailure.Text = message;
            lblMessageSuccess.Text = "";
            divTopOfFormMsgFailure.Visible = true;
            divTopOfFormMsgSuccess.Visible = false;
        }

        private void ShowMessage(string message)
        {
            //            Master.ReportSuccess(message);
            lblMessageSuccess.Text = message;
            lblMessageFailure.Text = "";
            divTopOfFormMsgFailure.Visible = false;
            divTopOfFormMsgSuccess.Visible = true;
        }
        private void DatabindControls()
        {
            List<PermanentCommittee> lpcSC = bal.GetPermanentCommitteeByTypeID((int)PermanentCommitteeTypeTitle.Scientific_Council);
            List<PermanentCommittee> lpcSCC = bal.GetPermanentCommitteeByTypeID((int)PermanentCommitteeTypeTitle.Scientific_Council_Coordinator);
            List<PermanentCommittee> lpcPS = bal.GetPermanentCommitteeByTypeID((int)PermanentCommitteeTypeTitle.SC_Subcommittee);
            int MinScientificCouncilMembers = int.Parse(bal.GetWorkflowAttribute()
                .Where(a => a.Attribute == GlobalAttribute.MinScientificCouncilMembers.ToString()).ToList()[0].Value);
            if (lpcSC.Count < MinScientificCouncilMembers)
            {
                ShowError(Resources.Resource.ScientificCouncilM6
                    .Replace("@@MinScientificCouncilMembers@@", MinScientificCouncilMembers.ToString()));
            }
            rptPermanentCommitteeSC.DataSource = lpcSC;
            rptPermanentCommitteeSCCoordinator.DataSource = lpcSCC;
            int MinPromotionSubcommitteeMembers = int.Parse(bal.GetWorkflowAttribute()
                .Where(a => a.Attribute == GlobalAttribute.MinPromotionSubcommitteeMembers.ToString()).ToList()[0].Value);
            if (lpcPS.Count < MinPromotionSubcommitteeMembers)
            {
                ShowError(Resources.Resource.ScientificCouncilM7
                    .Replace("@@MinPromotionSubcommitteeMembers@@", MinPromotionSubcommitteeMembers.ToString()));
            }
            rptPermanentCommitteePSubComm.DataSource = lpcPS;
            if(lpcSC.Count >= MinScientificCouncilMembers && lpcPS.Count >= MinPromotionSubcommitteeMembers)
            {
                ShowMessage(Resources.Resource.ScientificCouncilM8);
            }

            ddlCommitteeType.DataSource = bal.GetPermanentCommitteeType().Select(a => new { Text = a.Title + " (" + a.PermanentCommitteeTypeID + ")", Value = a.PermanentCommitteeTypeID });
            ddlCommitteeType.DataBind();
            rptPermanentCommitteeSC.DataBind();
            rptPermanentCommitteeSCCoordinator.DataBind();
            rptPermanentCommitteePSubComm.DataBind();
            divSC.DataBind();
            divSCSubComm.DataBind();

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
            else if ((item.Parent as Repeater).ID == "rptPermanentCommitteeSCCoordinator")
            {
                item.FindControl("lbtnSearchSCC").Visible = isEdit;

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
            if (bal.GetPermanentCommittee().Where(ad => ad.EmployeeID == employeeID 
            && ad.PermanentCommitteeTypeID == PermanentCommitteeTypeID).Count() != 0)
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM5
                    .Replace("@@EmployeeID@@", employeeID)
                    .Replace("@@PermanentCommittee@@", ((PermanentCommitteeTypeTitle)PermanentCommitteeTypeID)
                    .ToString()
                    .Replace("_", " "))
                ;
                programmaticModalPopup0.Show();
                return false;
            }
            return true;

        }
        private void ReOrderPermanentCommitteeInAppRoles()
        {
            List<PermanentCommittee> permanentCommittee = bal.GetPermanentCommitteeByTypeID((int)PermanentCommitteeTypeTitle.Scientific_Council);
            foreach (int appID in bal.GetApplicationIDWithApplicationClosed(false))
            {
                
                int n = 0; 
                //foreach (byte roleID in bal.GetApplicationRoleID(appID, (byte)RoleID.Scientific_Council_Member_1, (byte)RoleID.Scientific_Council_Member_12))
                //{                   
                    //if(roleID != (byte)(RoleID.Scientific_Council_Member_1 + n++))
                    //{                        
                        bal.DeleteApplicationRoles(appID, (byte)RoleID.Scientific_Council_Member_1, (byte)RoleID.Scientific_Council_Member_12);
                        bal.DeleteApplicationRoles(appID, (byte)RoleID.Scientific_Council_Coordinator);
                        for (int i = 0; i < permanentCommittee.Count(); i++)
                        {
                            bal.InsertApplicationRoles(appID, (byte)(RoleID.Scientific_Council_Member_1 + i), permanentCommittee[i].EmployeeID, 0);
                        }
                        bal.InsertApplicationRoles(appID, (byte)(RoleID.Scientific_Council_Coordinator), bal.GetPermanentCommitteeByTypeID((int)PermanentCommitteeTypeTitle.Scientific_Council_Coordinator)[0].EmployeeID, 0);                       
                    //}
                //}
            }
            permanentCommittee = bal.GetPermanentCommitteeByTypeID((int)PermanentCommitteeTypeTitle.SC_Subcommittee);
            foreach (int appID in bal.GetApplicationIDWithApplicationClosed(false))
            {
                bal.DeleteApplicationRoles(appID, (byte)RoleID.SC_Subcommittee_Chair, (byte)RoleID.SC_Subcommittee_Member_4);                        
                for (int i = 0; i < permanentCommittee.Count(); i++)
                {
                    bal.InsertApplicationRoles(appID, (byte)(RoleID.SC_Subcommittee_Chair + i), permanentCommittee[i].EmployeeID, 0);
                }                        
            }
        }
       
        #region Event Handler
        protected void rptPermanentCommitteeSCCoordinator_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
             if (e.CommandName == "Delete")
             {
                int PermanentCommitteeID = int.Parse(e.CommandArgument.ToString());
                if (bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.Scientific_Council_Coordinator).Count() == 1)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM12;
                    programmaticModalPopup0.Show();
                    return;
                }
                PermanentCommittee pc = bal.GetPermanentCommittee()
                .Where(a => a.PermanentCommitteeID == PermanentCommitteeID).ToList()[0];
                List<Application_Role> lar = bal.GetApplicationRoleForActiveApp((byte)RoleID.Scientific_Council_Coordinator, (byte)RoleID.Scientific_Council_Coordinator).ToList();
                foreach (Application_Role ar in lar)
                {
                    if (ar.EmployeeID == pc.EmployeeID)
                    {
                        bal.DeleteApplicationRoles(ar.ApplicationID, ar.RoleID);
                    }
                }
                bal.DeletetPermanentCommittee(PermanentCommitteeID);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {   
                //  (e.Item.FindControl("ddlCommitteeTypeEdit") as DropDownList).DataSource = bal.GetPermanentCommitteeType().Select(a => new { Text = a.Title + " (" + a.PermanentCommitteeTypeID + ")", Value = a.PermanentCommitteeTypeID });
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                int PermanentCommitteeID = int.Parse(e.CommandArgument.ToString());
                string empID = (e.Item.FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value;
                //DropDownList ddl = e.Item.FindControl("ddlCommitteeTypeEdit") as DropDownList;
                //int permanentCommitteeTypeID = int.Parse(ddl.SelectedValue);
                PermanentCommittee pc = bal.GetPermanentCommittee(PermanentCommitteeID).ToList()[0];
                if (pc.EmployeeID != empID && !IsEmployeeIDOK(empID, (int)PermanentCommitteeTypeTitle.Scientific_Council_Coordinator))
                {
                    return;
                }

                List<Application_Role> lar = bal.GetApplicationRoleForActiveApp((byte)RoleID.Scientific_Council_Coordinator, (byte)RoleID.Scientific_Council_Coordinator).ToList();
                if (lar.Where(a => a.EmployeeID == pc.EmployeeID).Count() > 1)
                {
                    foreach (Application_Role ar in lar.Where(a => a.EmployeeID == pc.EmployeeID).Skip(1))
                    {
                        bal.DeleteApplicationRoles(ar.ApplicationID, ar.RoleID);
                    }
                }
                foreach (Application_Role ar in lar)
                {
                    if (ar.EmployeeID == pc.EmployeeID)
                    {
                        bal.UpdateApplicationRoles(ar.ApplicationID, ar.RoleID, empID, 0, ar.IsActing);
                    }
                }
                if (bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.Scientific_Council_Coordinator, empID).Count > 1)
                {
                    foreach (PermanentCommittee pcAdditional in bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.Scientific_Council, empID).Skip(1))
                    {
                        bal.DeletetPermanentCommittee(pcAdditional.PermanentCommitteeID);
                    }
                }
                bal.UpdatePermanentCommittee(PermanentCommitteeID, (int)PermanentCommitteeTypeTitle.Scientific_Council_Coordinator, empID, 0);
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
                //ToggleElements(e.Item, false);
            }
        }
        protected void rptPermanentCommitteeSC_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int PermanentCommitteeID = int.Parse(e.CommandArgument.ToString());
                if (bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.Scientific_Council).Count() == 1)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM12;
                    programmaticModalPopup0.Show();
                    return;
                }
                PermanentCommittee pc = bal.GetPermanentCommittee()
                    .Where(a => a.PermanentCommitteeID == PermanentCommitteeID).ToList()[0];
                List<Application_Role> lar = bal.GetApplicationRoleForActiveApp((byte)RoleID.Scientific_Council_Member_1, (byte)RoleID.Scientific_Council_Member_12).ToList();
                foreach (Application_Role ar in lar)
                {
                    if (ar.EmployeeID == pc.EmployeeID)
                    {
                        bal.DeleteApplicationRoles(ar.ApplicationID, ar.RoleID);
                    }

                }
                bal.DeletetPermanentCommittee(PermanentCommitteeID);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {                
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
        {
            int PermanentCommitteeID = int.Parse(e.CommandArgument.ToString());
            string empID = (e.Item.FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value;
            PermanentCommittee pc = bal.GetPermanentCommittee(PermanentCommitteeID).ToList()[0];
            if (pc.EmployeeID != empID && !IsEmployeeIDOK(empID, (int)PermanentCommitteeTypeTitle.Scientific_Council))
            {
                return;
            }

            List<Application_Role> lar = bal.GetApplicationRoleForActiveApp((byte)RoleID.Scientific_Council_Member_1, (byte)RoleID.Scientific_Council_Member_12);
            foreach (Application app in bal.GetApplicationWithApplicationClosed(false))
            {
                if (lar.Where(a => a.EmployeeID == pc.EmployeeID && a.ApplicationID == app.ApplicationID).Count() > 1)
                {
                    foreach (Application_Role ar in lar.Where(a => a.EmployeeID == pc.EmployeeID && a.ApplicationID == app.ApplicationID).Skip(1))
                    {
                        bal.DeleteApplicationRoles(ar.ApplicationID, ar.RoleID);
                    }
                }
            }
            foreach (Application app in bal.GetApplicationWithApplicationClosed(false))
            {                
                foreach (Application_Role ar in bal.GetApplicationRole(app.ApplicationID, (byte)RoleID.Scientific_Council_Member_1, (byte)RoleID.Scientific_Council_Member_12))
                {
                    if (ar.EmployeeID == pc.EmployeeID)
                    {
                        bal.UpdateApplicationRoles(ar.ApplicationID, ar.RoleID, empID, 0, ar.IsActing);
                        break;
                    }
                }
            }
            foreach (Application app in bal.GetApplicationWithApplicationClosed(false))
            {
                if(bal.GetApplicationRole(app.ApplicationID, (byte)RoleID.Scientific_Council_Member_1, (byte)RoleID.Scientific_Council_Member_12).Where(a=>a.EmployeeID == empID).Count() == 0)
                {
                    bal.InsertApplicationRoles(app.ApplicationID, GetMaxRoleIDinAppRoleOfPerComm(app.ApplicationID,(int) PermanentCommitteeTypeTitle.Scientific_Council), empID, 0);
                }               
            }

            if (bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.Scientific_Council, empID).Count > 1)
            {
                foreach (PermanentCommittee pcAdditional in bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.Scientific_Council, empID).Skip(1))
                {
                    bal.DeletetPermanentCommittee(pcAdditional.PermanentCommitteeID);
                }
            }
            bal.UpdatePermanentCommittee(PermanentCommitteeID, (int)PermanentCommitteeTypeTitle.Scientific_Council, empID, 0);            
            DatabindControls();
        }
            else if (e.CommandName == "Cancel")
            {
                DatabindControls();
                //ToggleElements(e.Item, false);
            }
        }

        private byte GetMaxRoleIDinAppRoleOfPerComm(int applicationID, int PermanentCommitteeTypeID)
    {
        List<byte> roleIDs = bal.GetApplicationRole(applicationID).Select(a => a.RoleID).ToList();
        roleIDs.Sort();
        
        //var result = Enumerable.Range(roleIDs.Min(), roleIDs.Max()- roleIDs.Min() +1).Except(roleIDs); 
        //if(PermanentCommitteeTypeID == (int) PermanentCommitteeTypeTitle.Scientific_Council)
        //{
        //    int MaxScientificCouncilMembers = int.Parse(bal.GetWorkflowAttribute()
        //           .Where(a => a.Attribute == GlobalAttribute.MaxScientificCouncilMembers.ToString()).ToList()[0].Value);
        //    if((byte) (RoleID.Scientific_Council_Member_1 + MaxScientificCouncilMembers) > (byte)(roleIDs.Last() + 1) )
        //    {

        //    }
        //}
        
        return (byte)(roleIDs.Last() + 1);

    }

        protected void rptPermanentCommitteePSubComm_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int PermanentCommitteeID = int.Parse(e.CommandArgument.ToString());
                if (bal.GetPermanentCommittee((int) PermanentCommitteeTypeTitle.SC_Subcommittee).Count() == 1)                    
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM13;
                    programmaticModalPopup0.Show();
                    return;
                }
                PermanentCommittee pc = bal.GetPermanentCommittee()
                    .Where(a => a.PermanentCommitteeID == PermanentCommitteeID).ToList()[0];
                List<Application_Role> lar = bal.GetApplicationRoleForActiveApp((byte)RoleID.SC_Subcommittee_Chair, (byte)RoleID.SC_Subcommittee_Member_4).ToList();
                foreach (Application_Role ar in lar)
                {
                    if (ar.EmployeeID == pc.EmployeeID)
                    {
                        bal.DeleteApplicationRoles(ar.ApplicationID, ar.RoleID);
                    }
                }
                bal.DeletetPermanentCommittee(PermanentCommitteeID);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {               
                ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                int PermanentCommitteeID = int.Parse(e.CommandArgument.ToString());
                string empID = (e.Item.FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value;               
                PermanentCommittee pc = bal.GetPermanentCommittee(PermanentCommitteeID).ToList()[0];
                if (pc.EmployeeID != empID && !IsEmployeeIDOK(empID, (int)PermanentCommitteeTypeTitle.SC_Subcommittee))
                {
                    return;
                }
                List<Application_Role> lar = bal.GetApplicationRole((byte)RoleID.SC_Subcommittee_Chair, (byte)RoleID.SC_Subcommittee_Member_4);                
                foreach (Application app in bal.GetApplicationWithApplicationClosed(false))
                {
                    if (lar.Where(a => a.EmployeeID == pc.EmployeeID && a.ApplicationID == app.ApplicationID).Count() > 1)
                    {
                        foreach (Application_Role ar in lar.Where(a => a.EmployeeID == pc.EmployeeID && a.ApplicationID == app.ApplicationID).Skip(1))
                        {
                            bal.DeleteApplicationRoles(ar.ApplicationID, ar.RoleID);
                        }
                    }
                }
                foreach (Application app in bal.GetApplicationWithApplicationClosed(false))
                {                
                    foreach (Application_Role ar in bal.GetApplicationRole(app.ApplicationID, (byte)RoleID.SC_Subcommittee_Chair, (byte)RoleID.SC_Subcommittee_Member_4))
                    {
                        if (ar.EmployeeID == pc.EmployeeID)
                        {
                            bal.UpdateApplicationRoles(ar.ApplicationID, ar.RoleID, empID, 0, ar.IsActing);
                            break;
                        }
                    }
                }
                foreach (Application app in bal.GetApplicationWithApplicationClosed(false))
                {
                    if(bal.GetApplicationRole(app.ApplicationID, (byte)RoleID.SC_Subcommittee_Chair, (byte)RoleID.SC_Subcommittee_Member_4).Where(a=>a.EmployeeID == empID).Count() == 0)
                    {
                        bal.InsertApplicationRoles(app.ApplicationID, GetMaxRoleIDinAppRoleOfPerComm(app.ApplicationID,(int) PermanentCommitteeTypeTitle.SC_Subcommittee), empID, 0);
                    }               
                }

                if (bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.SC_Subcommittee, empID).Count > 1)
                {
                    foreach (PermanentCommittee pcAdditional in bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.SC_Subcommittee, empID).Skip(1))
                    {
                        bal.DeletetPermanentCommittee(pcAdditional.PermanentCommitteeID);
                    }
                }
                bal.UpdatePermanentCommittee(PermanentCommitteeID, (int)PermanentCommitteeTypeTitle.SC_Subcommittee, empID, 0);            
                DatabindControls();                
            
            
            
            
            
            
            
            
            
                ////foreach (Application_Role ar in lar)
                ////{
                ////    if (ar.EmployeeID == pc.EmployeeID)
                ////    {
                ////        bal.UpdateApplicationRoles(ar.ApplicationID, ar.RoleID, empID, 0);
                ////        if (bal.GetForm_CommitteeByPK(ar.ApplicationID, empID, ar.ExternalEmployeeID
                ////            , (int)PermanentCommitteeTypeTitle.Promotion_Subcommittee).Count > 0)
                ////        {
                ////            Form_Committee fc = bal.GetForm_CommitteeByPK(ar.ApplicationID, empID,
                ////                ar.ExternalEmployeeID, (int)PermanentCommitteeTypeTitle.Promotion_Subcommittee)[0];
                ////            bal.UpdateForm_Committee(ar.ApplicationID, fc.Position, empID, ar.ExternalEmployeeID
                ////                , (int)PermanentCommitteeTypeTitle.Promotion_Subcommittee
                ////                , null, null, "", RecordStatus.Active.ToString());
                ////            PromotionApplication.ReorderPositionInForm_Committee(ar.ApplicationID);
                ////        }

                ////    }
                ////}
                //bal.UpdatePermanentCommittee(PermanentCommitteeID, (int)PermanentCommitteeTypeTitle.SC_Subcommittee, empID, 0);
                //foreach(Application app in bal.GetApplication().Where(a=>!a.ApplicationClosed))
                //{
                //    List<PermanentCommittee> subc = bal.GetPermanentCommittee()
                //        .Where(pcSubc => pcSubc.PermanentCommitteeType.Title == PermanentCommitteeTypeTitle.SC_Subcommittee.ToString()
                //        .Replace("_", " ")).ToList();
                //    // DeleteApplicationRoles(applicationID, (byte)RoleID.Promotion_Subcommittee_Chair);
                //    foreach (Application_Role ar in bal.GetApplicationRole(app.ApplicationID).Where
                //         (a => a.RoleID >= (byte)RoleID.SC_Subcommittee_Chair &&
                //         a.RoleID <= (byte)RoleID.SC_Subcommittee_Member_4))
                //    {
                //        bal.DeleteApplicationRoles(app.ApplicationID, ar.RoleID);
                //    }
                //    var context = new FPSDBEntities();
                //    //context.Application_Role.Add(new Application_Role
                //    //{
                //    //    ApplicationID = app.ApplicationID,
                //    //    EmployeeID = subc[0].EmployeeID,
                //    //    RoleID = (byte)RoleID.Promotion_Subcommittee_Chair,
                //    //    ExternalEmployeeID = 0,
                //    //    UpdateDate = DateTime.Now,
                //    //    InsertDate = DateTime.Now
                //    //});
                //    //context.SaveChanges();
                //    for (int i = 0; i < subc.Count() ; i++)
                //    {
                //        if(i == 0)
                //        {
                //            context.Application_Role.Add(new Application_Role
                //            {
                //                ApplicationID = app.ApplicationID,
                //                EmployeeID = subc[0].EmployeeID,
                //                RoleID = (byte)RoleID.SC_Subcommittee_Chair,
                //                ExternalEmployeeID = 0,
                //                UpdateDate = DateTime.Now,
                //                InsertDate = DateTime.Now
                //            });
                //            context.SaveChanges();
                //        }
                //        else
                //        {
                //            context.Application_Role.Add(new Application_Role
                //            {
                //                ApplicationID = app.ApplicationID,
                //                EmployeeID = subc[i].EmployeeID,
                //                RoleID = bal.GetRole().Where(r => r.Title.Contains("Subcommittee Member " + (i))).First().RoleID,
                //                ExternalEmployeeID = 0,
                //                UpdateDate = DateTime.Now,
                //                InsertDate = DateTime.Now
                //            });
                //            context.SaveChanges();
                //        }

                       

                //    }
                //}

                //DatabindControls();
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
            int MaxSubcommitteeMembers = int.Parse(bal.GetWorkflowAttribute()
                  .Where(a => a.Attribute == GlobalAttribute.MaxPromotionSubcommitteeMembers.ToString()).ToList()[0].Value);
            string employeeID = tbEmployeeIDAdd.Value.Trim();
            if (employeeID == "")
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM1;
                programmaticModalPopup0.Show();
                return;
            }
            if (bal.GetEmployeeByPK(employeeID).Count == 0)
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM2.Replace("@@EmployeeID@@", employeeID);
                programmaticModalPopup0.Show();
                return;
            }
            List<PermanentCommittee> committee;
            if (int.Parse(ddlCommitteeType.SelectedValue) == (int)PermanentCommitteeTypeTitle.Scientific_Council)
            {
                if (bal.GetAppTaskLogClosedApp((int)TaskIDs.Scientific_Council_Membership_1, false).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM22.Replace("@@Applicant@@", bal.GetAppTaskLogClosedApp((int)TaskIDs.Scientific_Council_Membership_1, false).ToList()[0].Application.Employee.NameString);
                    programmaticModalPopup0.Show();
                    return;
                }
                committee = bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.Scientific_Council);
                if (committee.Count == MaxScientificCouncilMembers && int.Parse(ddlCommitteeType.SelectedValue) == (int)PermanentCommitteeTypeTitle.Scientific_Council)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM4
                        .Replace("@@MaxScientificCouncilMembers@@", MaxScientificCouncilMembers.ToString());
                    programmaticModalPopup0.Show();
                    return;
                }
            }
            else if (int.Parse(ddlCommitteeType.SelectedValue) == (int)PermanentCommitteeTypeTitle.SC_Subcommittee)
            {
                if (bal.GetAppTaskLogClosedApp((int)TaskIDs.SC_Subcommittee_Chairmanship, false).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM23.Replace("@@Applicant@@", bal.GetAppTaskLogClosedApp((int)TaskIDs.SC_Subcommittee_Chairmanship, false).ToList()[0].Application.Employee.NameString);
                    programmaticModalPopup0.Show();
                    return;
                }
                committee = bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.SC_Subcommittee);
                if (committee.Count == MaxSubcommitteeMembers && int.Parse(ddlCommitteeType.SelectedValue) == (int)PermanentCommitteeTypeTitle.SC_Subcommittee)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM3
                        .Replace("@@MaxScientificCouncilMembers@@", MaxSubcommitteeMembers.ToString());
                    programmaticModalPopup0.Show();
                    return;
                }
            //if (bal.GetAppTaskLogClosedApp((int)TaskIDs.SC_Subcommittee_Chairmanship, false).Count() > 0)
            //{
            //    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM23.Replace("@@Applicant@@", bal.GetAppTaskLogClosedApp((int)TaskIDs.SC_Subcommittee_Chairmanship, false).ToList()[0].Application.Employee.NameString);
            //    programmaticModalPopup0.Show();
            //    return;
            //}
            //committee = bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.SC_Subcommittee);
            //if (committee.Count == MaxPromotionSubcommitteeMembers && int.Parse(ddlCommitteeType.SelectedValue) == (int)PermanentCommitteeTypeTitle.SC_Subcommittee)
            //{
            //    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM3
            //        .Replace("@@MaxPromotionSubcommitteeMembers@@", MaxPromotionSubcommitteeMembers.ToString());
            //    programmaticModalPopup0.Show();
            //    return;
            //}
            }
            else
            {
                if (bal.GetAppTaskLogClosedApp((int)TaskIDs.Scientific_Council_Membership_1, false).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM22.Replace("@@Applicant@@", bal.GetAppTaskLogClosedApp((int)TaskIDs.Scientific_Council_Membership_1, false).ToList()[0].Application.Employee.NameString);
                    programmaticModalPopup0.Show();
                    return;
                }
                committee = bal.GetPermanentCommittee((int)PermanentCommitteeTypeTitle.Scientific_Council_Coordinator);
                if (committee.Count == 1 && int.Parse(ddlCommitteeType.SelectedValue) == (int)PermanentCommitteeTypeTitle.Scientific_Council_Coordinator)
                {
                    labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM26;
                    programmaticModalPopup0.Show();
                    return;
                }
            }
            if (bal.GetPermanentCommittee(int.Parse(ddlCommitteeType.SelectedValue), employeeID).Count != 0 )
            {
                labelProgrammaticPopup0.Text = Resources.Resource.ScientificCouncilM5
                  .Replace("@@EmployeeID@@", employeeID)
                  .Replace("@@PermanentCommittee@@", ddlCommitteeType.SelectedValue);
                programmaticModalPopup0.Show();
                return;
            }

            tbEmployeeIDAdd.Value = "";
            lblNameAdd.Text = "";
            bal.InsertPermanentCommittee(int.Parse(ddlCommitteeType.SelectedValue), employeeID, 0);
            foreach (Application app in bal.GetApplicationWithApplicationClosed(false))
            {
                bal.InsertApplicationRoles(app.ApplicationID);
            }
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
        protected void lbtnSearchSCC_Click(object sender, EventArgs e)
        {
            int rowIndex = ((sender as LinkButton).NamingContainer as RepeaterItem).ItemIndex;
            string employeeID = (rptPermanentCommitteeSCCoordinator.Items[rowIndex].FindControl("tbEmployeeIDEdit") as HtmlInputControl).Value;
            if (employeeID == "")
            {
                (rptPermanentCommitteeSCCoordinator.Items[rowIndex].FindControl("lblNameEdit") as Label).Text = " No Employee found";
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
                (rptPermanentCommitteeSCCoordinator.Items[rowIndex].FindControl("lblNameEdit") as Label).Text = " No Employee found";
                return;
            }
               (rptPermanentCommitteeSCCoordinator.Items[rowIndex].FindControl("lblNameEdit") as Label).Text =
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
        public bool GetStatus(PermanentCommitteeTypeTitle pc)
        {
            if(pc== PermanentCommitteeTypeTitle.Scientific_Council)
            {
                if (bal.GetAppTaskLog().Where(a => a.TaskID == (int)TaskIDs.Scientific_Council_Membership_1 && !a.Completed).Count() > 0)
                {
                    return false;
                }
            }
            else
            {
                if (bal.GetAppTaskLog().Where(a => a.TaskID == (int)TaskIDs.SC_Subcommittee_Chairmanship && !a.Completed).Count() > 0)
                {
                    return false;
                }
            }
            return true;
        }
        //public bool GetAppTaskFormCompleted()
    //{
    //    if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0).Count > 0)
    //    {
    //        return bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0)[0].Completed;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    //public string GetAppTaskFormMessage()
    //{
    //    if (bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0).Count > 0)
    //    {
    //        return bal.GetApplicationTaskForm(Master.ApplicationID, Master.TaskID, Master.CurrentFormID, false, 0)[0].Message;
    //    }
    //    else
    //    {
    //        return "";
    //    }

    //}
    #endregion

    
}
