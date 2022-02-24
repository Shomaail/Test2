﻿using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for Synchronization
/// </summary>
public class Synchronization
{
	public Synchronization()
	{

	}
    //public static void UpdateRanksOfClosedApps()
    //{
    //    Adapters.InitializeSP();
    //    Adapters.connectionSP.Open();            
    //    try
    //    {
    //        Adapters.pr_UpdateRanksSqlCmd.ExecuteNonQuery();
    //    }
    //    finally
    //    {
    //        Adapters.connectionSP.Close();
    //    }
    //}
    //public static void SynchronizeHRfromERP()
    //{
    //    HRTableAdapters.EmployeeERPTableAdapter empERPAdapter = new HRTableAdapters.EmployeeERPTableAdapter();
    //    HR.EmployeeERPDataTable empERPTable = empERPAdapter.GetData();
    //    HRTableAdapters.EmployeeHRTableAdapter empAdapter = new HRTableAdapters.EmployeeHRTableAdapter();
    //    for (int i = 0; i < empERPTable.Count; i++)
    //    {
    //        for (int j = i + 1; j < empERPTable.Count; j++)
    //        {

    //            if (empERPTable[i].FACULTY_NUMBER == empERPTable[j].FACULTY_NUMBER)
    //            {
    //                empERPTable[i].SetFACULTY_NUMBERNull();
    //                break;
    //            }
    //        }
    //    }
    //    foreach (HR.EmployeeERPRow empERPRow in empERPTable)
    //    {
    //        if (empERPRow.IsFACULTY_NUMBERNull())
    //            continue;
    //        else
    //        {
    //            if (empAdapter.GetDataByEmpID(empERPRow.FACULTY_NUMBER).Count == 1)
    //            {
    //                HR.EmployeeHRRow empRow = empAdapter.GetDataByEmpID(empERPRow.FACULTY_NUMBER)[0];

    //                if (empERPRow.IsEMAIL_ADDRESSNull())
    //                    empERPRow.EMAIL_ADDRESS = empRow.SecondEmail;

    //                if (empRow.KFUPMUserID != "")
    //                    empERPRow.USER_ID = empRow.KFUPMUserID;

    //                if (empERPRow.IsFACULTY_NAMENull() && empRow.Name != "")
    //                    empERPRow.FACULTY_NAME = empRow.Name;

    //                if (empERPRow.IsRANKNull() && empRow.Rank != "")
    //                    empERPRow.RANK = empRow.Rank;

    //                if (empERPRow.IsCURRENT_DEPARTMENTNull() && empRow.Department != "")
    //                    empERPRow.CURRENT_DEPARTMENT = empRow.Department;

    //                if (empERPRow.IsCURRENT_COLLEGENull() && empRow.College != "")
    //                    empERPRow.CURRENT_COLLEGE = empRow.College;

    //                if (empERPRow.IsP_O_BOXNull() && empRow.POBox != "")
    //                    empERPRow.P_O_BOX = empRow.POBox;

    //                if (empRow.JoinDate != "")
    //                    empERPRow.JOINING_DATE = DateTime.Parse(empRow.JoinDate);

    //                //if (empERPRow.KFUPMEmail.Contains("@"))
    //                //    empERPRow.KFUPMEmail = empERPRow.KFUPMEmail.Remove(empERPRow.KFUPMEmail.IndexOf('@'));

    //                empAdapter.Update(
    //                    empERPRow.IsEMAIL_ADDRESSNull() ? "" : empERPRow.EMAIL_ADDRESS,
    //                    empERPRow.USER_ID,
    //                    empERPRow.IsFACULTY_TITLENull() ? PromotionApplication.GetSalutation(empERPRow.RANK) : empERPRow.FACULTY_TITLE,
    //                    empERPRow.IsFACULTY_NAMENull() ? "" : empERPRow.FACULTY_NAME,
    //                    empERPRow.IsRANKNull() ? "" : empERPRow.RANK,
    //                    empERPRow.IsCURRENT_DEPARTMENTNull() ? "" : empERPRow.CURRENT_DEPARTMENT,
    //                    empERPRow.IsCURRENT_COLLEGENull() ? "" : empERPRow.CURRENT_COLLEGE,
    //                    empERPRow.IsP_O_BOXNull() ? "" : empERPRow.P_O_BOX,
    //                    empERPRow.IsWORK_PHONENull() ? "" : empERPRow.WORK_PHONE,
    //                    empERPRow.JOINING_DATE.ToString(),
    //                    empERPRow.FACULTY_NUMBER);
    //            }
    //            else
    //            {
    //                //if (!empERPRow.IsKFUPMEmailNull() && empERPRow.KFUPMEmail.Contains("@"))
    //                //    empERPRow.KFUPMEmail = empERPRow.KFUPMEmail.Remove(empERPRow.KFUPMEmail.IndexOf('@'));

    //                empAdapter.Insert(
    //                    empERPRow.FACULTY_NUMBER,
    //                    empERPRow.IsEMAIL_ADDRESSNull() ? "" : empERPRow.EMAIL_ADDRESS,
    //                    empERPRow.USER_ID,
    //                    empERPRow.IsFACULTY_TITLENull() ? PromotionApplication.GetSalutation(empERPRow.RANK) : empERPRow.FACULTY_TITLE,
    //                    empERPRow.IsFACULTY_NAMENull() ? "" : empERPRow.FACULTY_NAME,
    //                        empERPRow.IsRANKNull() ? "" : empERPRow.RANK,
    //                        empERPRow.IsCURRENT_DEPARTMENTNull() ? "" : empERPRow.CURRENT_DEPARTMENT,
    //                        empERPRow.IsCURRENT_COLLEGENull() ? "" : empERPRow.CURRENT_COLLEGE,
    //                        empERPRow.IsP_O_BOXNull() ? "" : empERPRow.P_O_BOX,
    //                        empERPRow.IsWORK_PHONENull() ? "" : empERPRow.WORK_PHONE,
    //                        empERPRow.JOINING_DATE.ToString());
    //            }

    //        }

    //    }

    
    //}
    //public static void SynchronizeFPSDBfromERP()
    //{
    //    HRTableAdapters.EmployeeERPTableAdapter empERPAdapter = new HRTableAdapters.EmployeeERPTableAdapter();
    //    HR.EmployeeERPDataTable empERPTable = empERPAdapter.GetData();
    //    HRTableAdapters.EmployeeTableAdapter empAdapter = new HRTableAdapters.EmployeeTableAdapter();
    //    for (int i = 0; i < empERPTable.Count; i++)
    //    {
    //        for (int j = i + 1; j < empERPTable.Count; j++)
    //        {

    //            if (empERPTable[i].FACULTY_NUMBER == empERPTable[j].FACULTY_NUMBER)
    //            {
    //                empERPTable[i].SetFACULTY_NUMBERNull();
    //                break;
    //            }
    //        }
    //    }
    //    foreach (HR.EmployeeERPRow empERPRow in empERPTable)
    //    {
    //        if (empERPRow.IsFACULTY_NUMBERNull())
    //            continue;
    //        else
    //        {
    //            if (empAdapter.GetDataByEmpID(empERPRow.FACULTY_NUMBER).Count == 1)
    //            {
    //                HR.EmployeeRow empRow = empAdapter.GetDataByEmpID(empERPRow.FACULTY_NUMBER)[0];

    //                if (empERPRow.IsEMAIL_ADDRESSNull())
    //                    empERPRow.EMAIL_ADDRESS = empRow.SecondEmail;

    //                if (empRow.KFUPMUserID != "")
    //                    empERPRow.USER_ID = empRow.KFUPMUserID;

    //                if (empERPRow.IsFACULTY_NAMENull() && empRow.Name != "")
    //                    empERPRow.FACULTY_NAME = empRow.Name;

    //                if (empERPRow.IsRANKNull() && empRow.Rank != "")
    //                    empERPRow.RANK = empRow.Rank;

    //                if (empERPRow.IsCURRENT_DEPARTMENTNull() && empRow.Department != "")
    //                    empERPRow.CURRENT_DEPARTMENT = empRow.Department;

    //                if (empERPRow.IsCURRENT_COLLEGENull() && empRow.College != "")
    //                    empERPRow.CURRENT_COLLEGE = empRow.College;

    //                if (empERPRow.IsP_O_BOXNull() && empRow.POBox != "")
    //                    empERPRow.P_O_BOX = empRow.POBox;

    //                if (empRow.JoinDate != "")
    //                    empERPRow.JOINING_DATE = DateTime.Parse(empRow.JoinDate);

    //                //if (empERPRow.KFUPMEmail.Contains("@"))
    //                //    empERPRow.KFUPMEmail = empERPRow.KFUPMEmail.Remove(empERPRow.KFUPMEmail.IndexOf('@'));

    //                empAdapter.Update(
    //                    empERPRow.IsEMAIL_ADDRESSNull() ? "" : empERPRow.EMAIL_ADDRESS,
    //                    empERPRow.USER_ID,
    //                    empERPRow.IsFACULTY_TITLENull() ? PromotionApplication.GetSalutation(empERPRow.RANK) : empERPRow.FACULTY_TITLE,
    //                    empERPRow.IsFACULTY_NAMENull() ? "" : empERPRow.FACULTY_NAME,
    //                    empERPRow.IsRANKNull() ? "" : empERPRow.RANK,
    //                    empERPRow.IsCURRENT_DEPARTMENTNull() ? "" : empERPRow.CURRENT_DEPARTMENT,
    //                    empERPRow.IsCURRENT_COLLEGENull() ? "" : empERPRow.CURRENT_COLLEGE,
    //                    empERPRow.IsP_O_BOXNull() ? "" : empERPRow.P_O_BOX,
    //                    empERPRow.IsWORK_PHONENull() ? "" : empERPRow.WORK_PHONE,
    //                    empERPRow.JOINING_DATE.ToString(),
    //                    empERPRow.FACULTY_NUMBER);
    //            }
    //            else
    //            {
    //                //if (!empERPRow.IsKFUPMEmailNull() && empERPRow.KFUPMEmail.Contains("@"))
    //                //    empERPRow.KFUPMEmail = empERPRow.KFUPMEmail.Remove(empERPRow.KFUPMEmail.IndexOf('@'));

    //                empAdapter.Insert(
    //                    empERPRow.FACULTY_NUMBER,
    //                    empERPRow.IsEMAIL_ADDRESSNull() ? "" : empERPRow.EMAIL_ADDRESS,
    //                    empERPRow.USER_ID,
    //                    empERPRow.IsFACULTY_TITLENull() ? PromotionApplication.GetSalutation(empERPRow.RANK) : empERPRow.FACULTY_TITLE,
    //                    empERPRow.IsFACULTY_NAMENull() ? "" : empERPRow.FACULTY_NAME,
    //                        empERPRow.IsRANKNull() ? "" : empERPRow.RANK,
    //                        empERPRow.IsCURRENT_DEPARTMENTNull() ? "" : empERPRow.CURRENT_DEPARTMENT,
    //                        empERPRow.IsCURRENT_COLLEGENull() ? "" : empERPRow.CURRENT_COLLEGE,
    //                        empERPRow.IsP_O_BOXNull() ? "" : empERPRow.P_O_BOX,
    //                        empERPRow.IsWORK_PHONENull() ? "" : empERPRow.WORK_PHONE,
    //                        empERPRow.JOINING_DATE.ToString());
    //            }

    //        }

    //    }

    //    UpdateRanksOfClosedApps();
    //}
 
}