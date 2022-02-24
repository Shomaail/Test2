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

public partial class Controls_DepartmentCommittee : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public int SaveCommittee(int applicationID)
    {
        int counter = 0;
        PromotionTableAdapters.DepartmentCommittteTableAdapter adapter = new PromotionTableAdapters.DepartmentCommittteTableAdapter();
        PromotionTableAdapters.Application_RoleTableAdapter roleAdapter = new PromotionTableAdapters.Application_RoleTableAdapter();

        adapter.DeleteByApplication(applicationID);

        ASP.controls_employeeinput_ascx[] inputs = new ASP.controls_employeeinput_ascx[5];
        inputs[0] = EmployeeInput1;
        inputs[1] = EmployeeInput2;
        inputs[2] = EmployeeInput3;
        inputs[3] = EmployeeInput4;
        inputs[4] = EmployeeInput5;

        for (byte i = 0; i < inputs.Length; i++)
        {
            if (inputs[i].IsValidEmployee)
            {
                try
                {
                    adapter.InsertQuery(
                        applicationID,
                        i,
                        inputs[i].EmployeeID);
                    counter++;

                    if (i == 0)
                    {

                        roleAdapter.DeleteDeptCommittee(applicationID);
                        //roleAdapter.Update("", 1, 1, "");
                        roleAdapter.InsertDeptCommitteeChair(applicationID, inputs[i].EmployeeID);
                    }
                    else
                    {
                       // roleAdapter.Delete(applicationID, Convert.ToByte(23 + i), inputs[i].EmployeeID);
                        roleAdapter.Insert(applicationID, Convert.ToByte(23 + i ), inputs[i].EmployeeID);
                    }
                }
                    

                catch 
                {
                    return 0;
                }
            }
        }
        
        return counter;
    }


    public bool LoadCommittee(int applicationID)
    {
        
        PromotionTableAdapters.DepartmentCommittteTableAdapter adapter = new PromotionTableAdapters.DepartmentCommittteTableAdapter();
        
        Promotion.DepartmentCommittteDataTable table = adapter.GetByApplication(applicationID);
        if (table.Count == 0) return false;

        ASP.controls_employeeinput_ascx[] inputs = new ASP.controls_employeeinput_ascx[5];
        inputs[0] = EmployeeInput1;
        inputs[1] = EmployeeInput2;
        inputs[2] = EmployeeInput3;
        inputs[3] = EmployeeInput4;
        inputs[4] = EmployeeInput5;
        
        for (int i = 0; i < table.Count; i++)
        {
            inputs[i].KFUPMUserID = table[i]["KFUPMUserID"].ToString();
            inputs[i].LoadEmployee();
        }


        return true;
    }

    public void SetReadonly(bool status)
    {
        EmployeeInput1.SetReadOnly(status);
        EmployeeInput2.SetReadOnly(status);
        EmployeeInput3.SetReadOnly(status);
        EmployeeInput4.SetReadOnly(status);
        EmployeeInput5.SetReadOnly(status);
    }

}