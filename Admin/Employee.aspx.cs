using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Linq;
using System.Configuration;
using System.Web.UI.HtmlControls;
using BL.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

public partial class Admin_Employee : System.Web.UI.Page
    {
        BAL bal = new BAL();
        
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (IsPostBack)
            {
                return;
            }
            ddlCollege.DataSource = bal.GetDepartments("Active").Where(d => d.Type == DepartmentType.Department.ToString())
               .Select(a => new { Text = a.NameString, Value = a.DepartmentID });
            ddlCollege.DataBind();
            if (Session["user"] != null)
            {
                Employee emp = new Employee();
                ExternalEmployee eEmp = new ExternalEmployee();
                if ((bool)Session["ExtEmp"] == false)
                {
                    emp = Session["user"] as Employee;
                    if(ddlCollege.Items.FindByValue(emp.DepartmentID.ToString())!= null)
                    {
                        ddlCollege.Items.FindByValue(emp.DepartmentID.ToString()).Selected = true;
                    }                    
                }
             }
            foreach (var emp in bal.GetEmployeesInactive())
        {
            if (string.IsNullOrEmpty(emp.NameString))
            {
                if(!string.IsNullOrEmpty(emp.Rank) && emp.Rank.Contains(RankProfessorial.Professor.ToString()))
                {
                    emp.NameString = "Dr. " + emp.Name;
                    bal.UpdateEmployee(emp.EmployeeID, emp.SecondEmail, emp.Email, emp.Title, emp.Name, emp.Rank, emp.POBox, emp.Phone
                     , emp.JoinDate, emp.Status, emp.DepartmentID, emp.NameString
                     , emp.Department, emp.ParentDept, emp.Sex, emp.Password, emp.Major, emp.DeptShort, emp.Name_1
                     , emp.Name_2, emp.Name_3, emp.Name_4, emp.ContractDate, emp.DateOfBirth, emp.ContractType
                     , emp.Mobile, emp.Specialty, emp.Nationality, emp.Pager);
                }
                else
                {
                    emp.NameString = emp.Name;
                    bal.UpdateEmployee(emp.EmployeeID, emp.SecondEmail, emp.Email, emp.Title, emp.Name, emp.Rank, emp.POBox, emp.Phone
                     , emp.JoinDate, emp.Status, emp.DepartmentID, emp.NameString
                     , emp.Department, emp.ParentDept, emp.Sex, emp.Password, emp.Major, emp.DeptShort, emp.Name_1
                     , emp.Name_2, emp.Name_3, emp.Name_4, emp.ContractDate, emp.DateOfBirth, emp.ContractType
                     , emp.Mobile, emp.Specialty, emp.Nationality, emp.Pager);
                }
            }
        }
            rptEmployee.DataSource = bal.GetEmployees().Where(a => a.EmployeeID != "" && a.DepartmentID != null
                        && a.DepartmentID.Value.ToString() == ddlCollege.SelectedValue).ToList();
            //rptEmployee.DataSource = bal.GetEmployees().Where(a => a.DepartmentID != null && a.DepartmentID.Value.ToString() == ddlCollege.SelectedValue).ToList();
            DatabindControls();
        }

        private void DatabindControls()
        {
            ddlDepartmentAdd.DataSource = bal.GetDepartments(RecordStatus.Active.ToString())
                .Where(d => d.Type == DepartmentType.College.ToString())
                .Select(a => new { Text = a.NameString + " (" + a.DepartmentID + ")", Value = a.DepartmentID });
            ddlDepartmentAdd.DataBind();
            rptEmployee.DataBind();
        }
        private void ToggleElements(RepeaterItem item, bool isEdit)
        {
            //Toggle Buttons.
            item.FindControl("lbtnEdit").Visible = !isEdit;
            item.FindControl("lbtnUpdate").Visible = isEdit;
            item.FindControl("lbtnCancel").Visible = isEdit;
            item.FindControl("lbtnDelete").Visible = !isEdit;

            //Toggle Labels.
            item.FindControl("lblSecondEmail").Visible = !isEdit;
            item.FindControl("lblEmail").Visible = !isEdit;
            item.FindControl("lblName").Visible = !isEdit;
            item.FindControl("lblRank").Visible = !isEdit;
            item.FindControl("lblPOBox").Visible = !isEdit;
            item.FindControl("lblPhone").Visible = !isEdit;
            item.FindControl("lblJoinDate").Visible = !isEdit;
            // item.FindControl("lblStatus") .Visible = !isEdit;
            item.FindControl("lblDepartment").Visible = !isEdit;
            //item.FindControl("lblParentDept").Visible = !isEdit;
            item.FindControl("lblSex").Visible = !isEdit;
            item.FindControl("lblMajor").Visible = !isEdit;
            item.FindControl("lblSpecialty").Visible = !isEdit;



            //Toggle TextBoxes.
            item.FindControl("tbSecondEmailEdit").Visible = isEdit;
            item.FindControl("tbEmailEdit").Visible = isEdit;
            item.FindControl("ddlTitleNameEdit").Visible = isEdit;
            //item.FindControl("tbTitleEdit").Visible = isEdit;
            //item.FindControl("tbRankEdit").Visible = isEdit;
            item.FindControl("ddlRankEdit").Visible = isEdit;

            item.FindControl("tbPOBoxEdit").Visible = isEdit;
            item.FindControl("tbPagerEdit").Visible = isEdit;
            item.FindControl("ddlNationalityEdit").Visible = isEdit;
            item.FindControl("tbPhoneEdit").Visible = isEdit;
            item.FindControl("tbMobileEdit").Visible = isEdit;
            item.FindControl("tbJoinDateEdit").Visible = isEdit;
            item.FindControl("tbContractDateEdit").Visible = isEdit;
            // item.FindControl("tbStatusEdit").Visible = isEdit;
            item.FindControl("ddlDepartmentEdit").Visible = isEdit;
            item.FindControl("ddlContractTypeEdit").Visible = isEdit;
            item.FindControl("tbDateOfBirthEdit").Visible = isEdit;
            
            item.FindControl("ddlSexEdit").Visible = isEdit;

            item.FindControl("tbMajorEdit").Visible = isEdit;
            item.FindControl("tbSpecialtyEdit").Visible = isEdit;            
            item.FindControl("tbName_1Edit").Visible = isEdit;
            item.FindControl("tbName_2Edit").Visible = isEdit;
            item.FindControl("tbName_3Edit").Visible = isEdit;
            item.FindControl("tbName_4Edit").Visible = isEdit;



        }
        #region Event Handlers
        protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
            rptEmployee.DataSource = bal.GetEmployees().Where(emp => emp.DepartmentID != null
                                && emp.DepartmentID.ToString() == ddlCollege.SelectedValue);
            DatabindControls();
        }
        //protected void lbtnSearch_Click(object sender, EventArgs e)
        //{
        //    rptEmployee.DataSource = Search(tbSearch.Text);
        //    DatabindControls();
        //}
        //protected void tbSearch_TextChanged(object sender, EventArgs e)
        //{
        //    rptEmployee.DataSource = Search(tbSearch.Text);
        //    DatabindControls();
        //}
        protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
        {
            programmaticModalPopup0.Hide();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            RepeaterItem rItem = (sender as LinkButton).Parent as RepeaterItem;
            string empID = (rItem.FindControl("lblEmployeeID") as Label).Text;
            if (bal.GetApplicationRole(empID, 0).Count() > 0)
            {
                labelProgrammaticPopup0.Text = "You cannot delete this employee since he/she has involvement in one or more promotion applications.";
                programmaticModalPopup0.Show();
                return;
            }
            bal.DeleteEmployee(empID);
            DatabindControls();
        }
        protected void rptEmployee_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string empID = e.CommandArgument.ToString();
                if (bal.GetEmployees().Count() == 1)
                {
                    labelProgrammaticPopup0.Text = "There has to be atleast one  employee. You cannot delete this employee";
                    programmaticModalPopup0.Show();
                    return;
                }
                if (bal.GetApplicationRole(empID,0).Count() > 0)
                {
                    string involvement = ""; 
                    foreach(Application_Role ar in bal.GetApplicationRole(empID, 0))
                    {
                        involvement += ar.Application.Employee.NameString+ " <br />";
                    }

                    labelProgrammaticPopup0.Text = "The employee has involvement in one or more promotion application and cannot be deleted. The employee is involved in the promotion application of the following Applicant(s):<br />" + involvement;
                    programmaticModalPopup0.Show();
                    return;
                }
                if (bal.GetPermanentCommittee().Where(a=>a.EmployeeID == empID).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = "The employee is either a member of Scientific Council or Promotion Subcommittee therefore cannot be deleted";
                    programmaticModalPopup0.Show();
                    return;
                }
                if (bal.GetAdmin(empID).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = "The employee is in the Administration group therefore cannot be deleted";
                    programmaticModalPopup0.Show();
                    return;
                }
                if (bal.GetDepartmentByEmp(empID).Count() > 0)
                {
                    labelProgrammaticPopup0.Text = "The employee is in the Head of any College or non-Academic Department therefore cannot be deleted";
                    programmaticModalPopup0.Show();
                    return;
                }
                //bal.DeleteEmployee(empID);
                Employee emp = bal.GetEmployeeByPK(empID)[0];
                emp.Status = RecordStatus.Inactive.ToString();
                bal.UpdateEmployee(emp.EmployeeID, emp.SecondEmail, emp.Email, emp.Title, emp.Name, emp.Rank, emp.POBox, emp.Phone
                    , emp.JoinDate, emp.Status, emp.DepartmentID, emp.NameString
                    , emp.Department, emp.ParentDept, emp.Sex, emp.Password, emp.Major, emp.DeptShort, emp.Name_1
                    , emp.Name_2, emp.Name_3, emp.Name_4, emp.ContractDate, emp.DateOfBirth, emp.ContractType
                    , emp.Mobile, emp.Specialty,emp.Nationality, emp.Pager);
                //if (tbSearch.Text == "")
                //{
                //    rptEmployee.DataSource = bal.GetEmployees().Where(a => a.EmployeeID != "" && a.DepartmentID != null
                //        && a.DepartmentID.ToString() == ddlCollege.SelectedValue);
                //}
                //else
                //{
                //    rptEmployee.DataSource = Search(tbSearch.Text);
                //}
                rptEmployee.DataSource = bal.GetEmployees().Where(a => a.EmployeeID != "" && a.DepartmentID != null
                 && a.DepartmentID.ToString() == ddlCollege.SelectedValue);
                DatabindControls();
            }
            else if (e.CommandName == "Edit")
            {
                string empID = e.CommandArgument.ToString();
                Employee emp = bal.GetEmployeeByPK(empID)[0];
                if((e.Item.FindControl("tbName_1Edit") as HtmlInputControl).Value == "")
                {
                    (e.Item.FindControl("tbName_1Edit") as HtmlInputControl).Value = emp.Name.Split(' ')[0];
                }
                if ((e.Item.FindControl("tbName_2Edit") as HtmlInputControl).Value == "")
                {
                    (e.Item.FindControl("tbName_2Edit") as HtmlInputControl).Value = emp.Name.Split(' ').Count() > 2 ? emp.Name.Split(' ')[1]: ""; 
                }
                if ((e.Item.FindControl("tbName_3Edit") as HtmlInputControl).Value == "")
                {
                    (e.Item.FindControl("tbName_3Edit") as HtmlInputControl).Value = emp.Name.Split(' ').Count() > 3 ? emp.Name.Split(' ')[2] : "";
                }
                if ((e.Item.FindControl("tbName_4Edit") as HtmlInputControl).Value == "")
                {
                    (e.Item.FindControl("tbName_4Edit") as HtmlInputControl).Value = emp.Name.Split(' ').Last();
                }
            ToggleElements(e.Item, true);
            }
            else if (e.CommandName == "Update")
            {
                string JoinDateStr = (e.Item.FindControl("tbJoinDateEdit") as HtmlInputControl).Value;
                string DOBStr = (e.Item.FindControl("tbDateOfBirthEdit") as HtmlInputControl).Value;
                string ContractDateStr = (e.Item.FindControl("tbContractDateEdit") as HtmlInputControl).Value;
                DateTime result;
                string empID = e.CommandArgument.ToString();
                Employee emp = bal.GetEmployeeByPK(empID)[0];
                emp.SecondEmail = (e.Item.FindControl("tbSecondEmailEdit") as HtmlInputControl).Value;
                emp.Email = (e.Item.FindControl("tbEmailEdit") as HtmlInputControl).Value;
                //emp.Title = (e.Item.FindControl("tbTitleEdit") as HtmlInputControl).Value;
                emp.Title = (e.Item.FindControl("ddlTitleNameEdit") as DropDownList).SelectedValue;

                emp.Rank = (e.Item.FindControl("ddlRankEdit") as DropDownList).SelectedValue;

//                emp.Rank = (e.Item.FindControl("tbRankEdit") as HtmlInputControl).Value;
                emp.POBox = (e.Item.FindControl("tbPOBoxEdit") as HtmlInputControl).Value;
                emp.Pager = (e.Item.FindControl("tbPagerEdit") as HtmlInputControl).Value;
                emp.Nationality= (e.Item.FindControl("ddlNationalityEdit") as DropDownList).SelectedValue == "Select Country" 
                    ? "" : (e.Item.FindControl("ddlNationalityEdit") as DropDownList).SelectedValue;
                emp.Phone = (e.Item.FindControl("tbPhoneEdit") as HtmlInputControl).Value;
                emp.Mobile = (e.Item.FindControl("tbMobileEdit") as HtmlInputControl).Value;
                emp.JoinDate = DateTime.TryParse(JoinDateStr, out result) ? result : (DateTime?)null;
                emp.DateOfBirth = DateTime.TryParse(DOBStr, out result) ? result : (DateTime?)null;
                //  emp.Status      =(e.Item.FindControl("tbStatusEdit") as HtmlInputControl).Value;
                emp.DepartmentID = int.Parse((e.Item.FindControl("ddlDepartmentEdit") as DropDownList).SelectedValue);
                emp.Department = bal.GetDepartmentByPK(emp.DepartmentID).NameString;
                emp.Sex = bool.Parse((e.Item.FindControl("ddlSexEdit") as DropDownList).SelectedValue);
                emp.Major = (e.Item.FindControl("tbMajorEdit") as HtmlInputControl).Value;
                emp.Specialty = (e.Item.FindControl("tbSpecialtyEdit") as HtmlInputControl).Value;
                emp.Name_1 = (e.Item.FindControl("tbName_1Edit") as HtmlInputControl).Value;
                emp.Name_2 = (e.Item.FindControl("tbName_2Edit") as HtmlInputControl).Value;
                emp.Name_3 = (e.Item.FindControl("tbName_3Edit") as HtmlInputControl).Value;
                emp.Name_4 = (e.Item.FindControl("tbName_4Edit") as HtmlInputControl).Value;
                emp.ContractDate = DateTime.TryParse(ContractDateStr, out result) ? result : (DateTime?)null;
                emp.NameString = emp.Title + " " + emp.Name_1.Trim() + " " + emp.Name_2.Trim() + " " + emp.Name_3.Trim() + " " + emp.Name_4.Trim();
                emp.Name = emp.Name_1.Trim() + " " + emp.Name_2.Trim() + " " + emp.Name_3.Trim() + " " + emp.Name_4.Trim();
                emp.DeptShort = bal.GetDepartmentByPK(emp.DepartmentID).ShortName;
                emp.ParentDept = bal.GetDepartmentByPK(emp.DepartmentID).ParentDeptString;
                if (bal.GetEmployeeByEmail(emp.Email).Count > 0 && bal.GetEmployeeByEmail(emp.Email)[0].EmployeeID != emp.EmployeeID)
                {
                    labelProgrammaticPopup0.Text = "An employee with email: " + emp.Email + " is already present in the employees Table with an employee ID " + emp.EmployeeID;
                    programmaticModalPopup0.Show();
                    return;
                }
                bal.UpdateEmployee(emp.EmployeeID, emp.SecondEmail, emp.Email, emp.Title, emp.Name, emp.Rank, emp.POBox, emp.Phone
                    , emp.JoinDate, emp.Status, emp.DepartmentID, emp.NameString,
                    emp.Department, emp.ParentDept, emp.Sex, emp.Password, emp.Major, emp.DeptShort, emp.Name_1
                    , emp.Name_2, emp.Name_3, emp.Name_4, emp.ContractDate, emp.DateOfBirth, emp.ContractType
                    , emp.Mobile, emp.Specialty,emp.Nationality, emp.Pager);
                //    if(tbSearch.Text == "")
                //    {
                //        rptEmployee.DataSource = bal.GetEmployees().Where(a => a.EmployeeID != "" && a.DepartmentID != null
                //&& a.DepartmentID.ToString() == ddlCollege.SelectedValue);
                //    }
                //    else
                //    {
                //        rptEmployee.DataSource = Search(tbSearch.Text);
                //    }
                rptEmployee.DataSource = bal.GetEmployees().Where(a => a.EmployeeID != "" && a.DepartmentID != null
                 && a.DepartmentID.ToString() == ddlCollege.SelectedValue);
                DatabindControls();
            }
            else if (e.CommandName == "Cancel")
            {
                //if (tbSearch.Text == "")
                //{
                //    rptEmployee.DataSource = bal.GetEmployees().Where(a => a.EmployeeID != "" && a.DepartmentID != null
                //         && a.DepartmentID.ToString() == ddlCollege.SelectedValue);
                //}
                //else
                //{
                //    rptEmployee.DataSource = Search(tbSearch.Text);
                //}
                rptEmployee.DataSource = bal.GetEmployees().Where(a => a.EmployeeID != "" && a.DepartmentID != null
                 && a.DepartmentID.ToString() == ddlCollege.SelectedValue);
                DatabindControls();
                //ToggleElements(e.Item, false);
            }

        }
        protected void lbtnAddEmployee_Click(object sender, EventArgs e)
        {

            string email = tbEmailAdd.Value;
            if (email == "")
            {
                labelProgrammaticPopup0.Text = "Email cannot be left empty!.";
                programmaticModalPopup0.Show();
                return;
            }
            //if (bal.GetEmployeeByEmail(email).Count == 0)
            //{
            //    labelProgrammaticPopup0.Text = "No Employee with this EmployeeID (" + employeeID + " ) exists .";
            //    programmaticModalPopup0.Show();
            //    return;
            //}
            if (bal.GetEmployeeByEmail(email).Count != 0)
            {
                labelProgrammaticPopup0.Text = "The  Employee with the Email (" + email + " ) is already present.";
                programmaticModalPopup0.Show();
                return;
            }
            if (bal.GetEmployeeByPK(tbEmployeeIDAdd.Value).Count != 0)
            {
                labelProgrammaticPopup0.Text = "The  Employee with the Employee ID (" + tbEmployeeIDAdd.Value + " ) is already present.";
                programmaticModalPopup0.Show();
                return;
            }

            string JoinDateStr = "";
            string ContractDateStr = "";
            string DOBStr = "";
            JoinDateStr = tbJoinDateAdd.Value;
            DOBStr = tbDateOfBirthAdd.Value;
            ContractDateStr = tbContractDateAdd.Value;
            DateTime result;
            bal.InsertEmployee(tbEmployeeIDAdd.Value, tbSecondEmailAdd.Value.ToLower(), tbEmailAdd.Value.ToLower()
                , ddlTitleNameAdd.SelectedValue, tbName_1Add.Value + " " + tbName_2Add.Value + " " + tbName_3Add.Value + " " + tbName_4Add.Value,
                ddlRankAdd.SelectedValue, tbPOBoxAdd.Value, tbPhoneAdd.Value,
                DateTime.TryParse(JoinDateStr, out result) ? result : (DateTime?)null,
                RecordStatus.Active.ToString(), int.Parse(ddlDepartmentAdd.SelectedValue),
                ddlTitleNameAdd.SelectedValue + tbName_1Add.Value + " " + tbName_2Add.Value + " " + tbName_3Add.Value + " " + tbName_4Add.Value,
                bal.GetDepartmentByPK(int.Parse(ddlDepartmentAdd.SelectedValue)).NameString,
                bal.GetDepartmentByPK(int.Parse(ddlDepartmentAdd.SelectedValue)).ParentDeptString,
                bool.Parse(ddlSexAdd.SelectedValue),
                "", tbMajorAdd.Value,
                bal.GetDepartmentByPK(int.Parse(ddlDepartmentAdd.SelectedValue)).ShortName,
                tbName_1Add.Value,
                tbName_2Add.Value,
                tbName_3Add.Value,
                tbName_4Add.Value, DateTime.TryParse(ContractDateStr, out result) ? result : (DateTime?)null,
                DateTime.TryParse(DOBStr, out result) ? result : (DateTime?)null, ddlContractTypeAdd.SelectedValue,
                tbMobileAdd.Value,tbSpecialtyAdd.Value,
                ddlNationalityAdd.SelectedValue == "Select Country" ? "" : ddlNationalityAdd.SelectedValue,
                tbPagerAdd.Value
            );

            rptEmployee.DataSource = bal.GetEmployees().Where(a => a.EmployeeID != "" && a.DepartmentID != null
                && a.DepartmentID.ToString() == ddlDepartmentAdd.SelectedValue);
            DatabindControls();
        }

        protected void lbtnAddClient_Click(object sender, EventArgs e)
        {

        }
        protected void ddlDepartmentEdit_DataBinding(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)(sender);

            ddl.DataSource = bal.GetDepartments(RecordStatus.Active.ToString())
                .Where(d => d.Type == DepartmentType.Department.ToString())
                .Select(a => new { Text = a.NameString + " (" + a.DepartmentID + ")", Value = a.DepartmentID });
            ddl.Items.Add(new ListItem("No Dept", "0"));
        }
        protected void lbtnSynch_Click(object sender, EventArgs e)
        {
        if (!bal.isEmployeeSynchNeeded(DateTime.Now.AddMonths(-1)))
        {
            labelProgrammaticPopup0.Text = "All records are updated";
            programmaticModalPopup0.Show();
            return;
        }
        using (var _client = new HttpClient())
        {
            string strUrl = string.Concat(ConfigurationManager.AppSettings["staff-web-service-url"],
                       ConfigurationManager.AppSettings["staff-web-service-employee-endpoint"]);

            //if (!string.IsNullOrEmpty(strFilterType) && !string.IsNullOrEmpty(strFilterType))
            //    strUrl += $"&{strFilterType}=" + strFilterText;

            string username = ConfigurationManager.AppSettings["web-service-username"];
            string pwd = ConfigurationManager.AppSettings["web-service-pwd"];

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{pwd}")));
            HttpResponseMessage resp = _client.GetAsync(strUrl).Result;
            if (!resp.IsSuccessStatusCode)
            {
                labelProgrammaticPopup0.Text = "The Web service authentication failed for the user "+username;
                programmaticModalPopup0.Show();
                return;
            }

            var json = resp.Content.ReadAsStringAsync().Result;

            try
            {
                RootObject employeeERPList = JsonConvert.DeserializeObject<RootObject>(json);
            

            List<EmployeeERP> listEmployeeERP = new List<EmployeeERP>(employeeERPList.data);

            strUrl = string.Concat(ConfigurationManager.AppSettings["dept-web-service-url"],
                       ConfigurationManager.AppSettings["dept-web-service-alldept-endpoint"]);
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{pwd}")));
            resp = _client.GetAsync(strUrl).Result;
            json = resp.Content.ReadAsStringAsync().Result;
            DepartmentERPList departmentERPList = JsonConvert.DeserializeObject<DepartmentERPList>(json);
            List<DepartmentERP> listDepartmentERP = new List<DepartmentERP>(departmentERPList.data);

            if (!resp.IsSuccessStatusCode)
            {
                return;
            }
            int updateCounter = 0, insertCounter = 0;
            foreach (var emp in bal.GetEmployeesAll())
            {
                if (emp.UpdateDate == null || emp.UpdateDate < DateTime.Now.AddMonths(-1))
                {
                    if (listEmployeeERP.Where(a => a.kfupm_id == emp.EmployeeID).Count() == 0)
                    {
                        emp.Status = RecordStatus.Inactive.ToString();
                        emp.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        EmployeeERP empERP = listEmployeeERP.Where(a => a.kfupm_id == emp.EmployeeID).First();
                        emp.Title = (!string.IsNullOrEmpty(empERP.rank) && empERP.rank.Contains(RankProfessorial.Professor.ToString()) ? "Dr. " : "");
                        emp.Email = empERP.email;
                        emp.Name = empERP.name_en;
                        emp.Mobile = empERP.mobile;
                        emp.Nationality = empERP.nationality;
                        emp.Rank = empERP.rank;
                        emp.Status = RecordStatus.Active.ToString();
                        emp.UpdateDate = DateTime.Now;
                        emp.Department = GetDeptName(listDepartmentERP, empERP.department_id);
                        emp.DepartmentID = emp.DepartmentID == null ? GetDeptID(listDepartmentERP, empERP.department_id) : emp.DepartmentID;
                        emp.ParentDept = GetParentDeptName(listDepartmentERP, empERP.department_id);
                        emp.NameString = (!string.IsNullOrEmpty(empERP.rank) && empERP.rank.Contains(RankProfessorial.Professor.ToString()) ? "Dr. " : "") + empERP.name_en;
                        if (emp.InsertDate == null)
                        {
                            emp.InsertDate = DateTime.Now;
                        }
                        updateCounter++;

                    }
                    bal.UpdateEmployee(emp.EmployeeID, emp.SecondEmail, emp.Email, emp.Title, emp.Name, emp.Rank, emp.POBox, emp.Phone
                      , emp.JoinDate, emp.Status, emp.DepartmentID, emp.NameString
                      , emp.Department, emp.ParentDept, emp.Sex, emp.Password, emp.Major, emp.DeptShort, emp.Name_1
                      , emp.Name_2, emp.Name_3, emp.Name_4, emp.ContractDate, emp.DateOfBirth, emp.ContractType
                      , emp.Mobile, emp.Specialty, emp.Nationality, emp.Pager);

                }
            }
            foreach (var item in listEmployeeERP.Where(a => !string.IsNullOrEmpty(a.rank) && a.rank.Contains(RankProfessorial.Professor.ToString())))
            {
                if (bal.GetEmployeeByPK(item.kfupm_id).Count == 0 && bal.GetEmployeeInactiveByPK(item.kfupm_id).Count == 0)
                {
                    Employee emp = new Employee()
                    {
                        EmployeeID = item.kfupm_id,
                        Email = item.email,
                        Name = item.name_en,
                        Rank = item.rank,
                        Mobile = item.mobile,
                        Status = RecordStatus.Active.ToString(),
                        Nationality = item.nationality,
                        DepartmentID = GetDeptID(listDepartmentERP, item.department_id),
                        ParentDept = GetParentDeptName(listDepartmentERP, item.department_id),
                        NameString = (!string.IsNullOrEmpty(item.rank) && item.rank.Contains(RankProfessorial.Professor.ToString()) ? "Dr. " : "") + item.name_en,
                        UpdateDate = DateTime.Now,
                        InsertDate = DateTime.Now
                    };
                    insertCounter++;
                    bal.InsertEmployee(emp.EmployeeID, emp.SecondEmail, emp.Email, emp.Title, emp.Name, emp.Rank, emp.POBox, emp.Phone
                      , emp.JoinDate, emp.Status, emp.DepartmentID, emp.NameString
                      , emp.Department, emp.ParentDept, emp.Sex, emp.Password, emp.Major, emp.DeptShort, emp.Name_1
                      , emp.Name_2, emp.Name_3, emp.Name_4, emp.ContractDate, emp.DateOfBirth, emp.ContractType
                      , emp.Mobile, emp.Specialty, emp.Nationality, emp.Pager);
                }
            }

            if (updateCounter == 0)
            {
                labelProgrammaticPopup0.Text = "No record updated";
            }
            else
            {
                labelProgrammaticPopup0.Text = "Update of Employees complete." + updateCounter + " record" + (updateCounter > 1 ? "s" : "") + " updated, "
                    + insertCounter + " new record" + (insertCounter > 1 ? "s" : "") + " inserted";
            }
            }
            catch (Exception)
            {

                labelProgrammaticPopup0.Text = "The Staff Web service is under Maintenance. No record is updated. Please try again later.";
            }
            programmaticModalPopup0.Show();
        }

    }
    #endregion


    public List<Employee> Search(string MainSearchString)
        {
            string[] srchStrArr = new string[5];
            char[] separator = new char[2];
            string srchStr;
            separator[0] = ' ';
            separator[1] = '@';
            //HRTableAdapters.EmployeeTableAdapter empAdapter = new HRTableAdapters.EmployeeTableAdapter();        

            List<Employee> employeeSeachResult = new List<Employee>();
            // HR.EmployeeDataTable empSrchResultTable = new HR.EmployeeDataTable();
            foreach (Employee row in bal.GetEmployees()
                .Where(a => a.EmployeeID != "" && a.DepartmentID != null &&
                a.DepartmentID.ToString() == ddlCollege.SelectedValue))
            {

                if (row.Email != null && row.Email.ToLower().Contains(MainSearchString.ToLower())
                    || row.Name.ToLower().Contains(MainSearchString.ToLower())
                    || row.Department != null && row.Department.ToLower().Contains(MainSearchString.ToLower())
                    || row.ParentDept != null && row.ParentDept.ToLower().Contains(MainSearchString.ToLower())
                    || row.EmployeeID.ToLower().Contains(MainSearchString.ToLower())
                    || row.Specialty != null && row.Specialty != "" && row.Specialty.ToLower().Contains(MainSearchString.ToLower())
                    || row.Rank != null && row.Rank!= "" && row.Rank.ToLower().Contains(MainSearchString.ToLower()))
                {
                    //                row.Name = row.Name.ToLower().Replace(TextBoxSearch.Text.ToLower(), "<I>" + TextBoxSearch.Text + "</I>");
                    try
                    {
                        employeeSeachResult.Add(row);
                        // empSrchResultTable.ImportRow(row);
                    }
                    catch (Exception)
                    {

                    }

                }
            }

            srchStrArr = MainSearchString.Split(separator);
            for (int i = 0; i < srchStrArr.Length; i++)
            {
                srchStr = srchStrArr[i];
                foreach (Employee row in bal.GetEmployees()
                        .Where(a => a.EmployeeID != "" && a.DepartmentID != null &&
                        a.DepartmentID.ToString() == ddlCollege.SelectedValue))
                {
                    if (row.Email != null && row.Email.ToLower().Contains(MainSearchString.ToLower())
                        || row.Name.ToLower().Contains(srchStr.ToLower())
                        || row.Department != null && row.Department.ToLower().Contains(srchStr.ToLower())
                        || row.ParentDept != null && row.ParentDept.ToLower().Contains(srchStr.ToLower())
                        || row.EmployeeID.ToLower().Contains(srchStr.ToLower())
                        || row.Specialty != null && row.Specialty != "" && row.Specialty.ToLower().Contains(MainSearchString.ToLower())
                        || row.Rank != null && row.Rank != "" && row.Rank.ToLower().Contains(MainSearchString.ToLower()))
                    {
                        //                row.Name = row.Name.ToLower().Replace(TextBoxSearch.Text.ToLower(), "<I>" + TextBoxSearch.Text + "</I>");
                        try
                        {
                            if (employeeSeachResult.Where(emp => emp.EmployeeID == row.EmployeeID).Count() == 0)
                                employeeSeachResult.Add(row);
                        }
                        catch (Exception)
                        {

                        }


                    }
                }
            }
            return employeeSeachResult;
        }
        

        private string GetParentDeptName(List<DepartmentERP> listDepartmentERP, string department_id)
    {
        if (listDepartmentERP.Where(a => a.id == department_id).Count() != 0)
        {
            return listDepartmentERP.Where(a => a.id == department_id).ToList()[0].mother_department.name_en;
        }
        else
        {
            return null;
        }
    }

        private int? GetDeptID(List<DepartmentERP> listDepartmentERP, string department_id)
    {
        if(listDepartmentERP.Where(a => a.id == department_id).Count() != 0 )
        {
            string deptName = listDepartmentERP.Where(a => a.id == department_id).ToList()[0].name_en;
            if(bal.GetDepartmentByName(deptName).Count > 0 )
            {
                return bal.GetDepartmentByName(deptName)[0].DepartmentID;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
        
    }
        private string GetDeptName(List<DepartmentERP> listDepartmentERP, string department_id)
    {
        if (listDepartmentERP.Where(a => a.id == department_id).Count() != 0)
        {
            return listDepartmentERP.Where(a => a.id == department_id).ToList()[0].name_en;
            
        }
        else
        {
            return null;
        }

    }
}
