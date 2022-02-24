using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using BL.Data;
public partial class Controls_Search : System.Web.UI.UserControl
{
    BAL bal = new BAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        rptSearch.DataSource = bal.GetEmployees().Where(a=>a.EmployeeID!="0");
        rptSearch.DataBind();
    }
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        // TextBoxSearch.Text = Regex.Replace(TextBoxSearch.Text, PromotionApplication.specialCharacters, string.Empty);
        
        //GridViewSrcResult.DataSource = Search(TextBoxSearch.Text);
        //GridViewSrcResult.DataBind();
    }
    //protected void GridViewSrcResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridViewSrcResult.PageIndex = e.NewPageIndex;
    //    GridViewSrcResult.DataSource = Search(TextBoxSearch.Text);
    //    GridViewSrcResult.DataBind();
    //}
    //protected void GridViewSrcResult_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    //DataView m_DataView = new DataView(Search(TextBoxSearch.Text));
    //    //m_DataView.Sort = e.SortExpression; 
    //    //GridViewSrcResult.DataSource = m_DataView;
    //    //GridViewSrcResult.DataBind();
    //}
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
        foreach (Employee row in bal.GetEmployees())
        {

            if (row.Email != null && row.Email.ToLower().Contains(MainSearchString.ToLower())
                || row.Name.ToLower().Contains(MainSearchString.ToLower())
                || row.Department != null && row.Department.ToLower().Contains(MainSearchString.ToLower())
                || row.ParentDept != null && row.ParentDept.ToLower().Contains(MainSearchString.ToLower())
                || row.EmployeeID.ToLower().Contains(MainSearchString.ToLower())
                || row.Rank != null && row.Rank.ToLower().Contains(MainSearchString.ToLower()))
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
            foreach (Employee row in bal.GetEmployees())
            {
                if (row.Email != null && row.Email.ToLower().Contains(MainSearchString.ToLower())
                    || row.Name.ToLower().Contains(srchStr.ToLower())
                    || row.Department != null && row.Department.ToLower().Contains(srchStr.ToLower())
                    || row.ParentDept != null && row.ParentDept.ToLower().Contains(srchStr.ToLower())
                    || row.EmployeeID.ToLower().Contains(srchStr.ToLower())
                    || row.Rank.ToLower().Contains(srchStr.ToLower()))
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



}
