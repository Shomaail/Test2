using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AllClasses
/// </summary>
/// 
#region Enum Specifiers
public enum ActionID
{
    Start_Application = 0,
    Initiate_and_Forward_to_the_Chairman	=	1	,
    Forward_to_Faculty_Affairs	=	2	,
    Submit_to_Departmental_Committee	=	4	,
    Return_to_the_Applicant	=	5	,
    Recommend_and_Send_to_the_College_Dean	=	6	,
    Not_Recommended_and_Return_to_Department	=	7	,
    Verify_and_Return_to_the_Department_Chairman	=	8	,
    Return_to_the_Department_Chairman_From_FacultyAffairs	=	9	,
    Return_to_the_Department_Chairman_From_Applicant	=	10	,
    Forward_to_VRAA	=	21	,
    Return_to_the_Dept_Chairman	=	22	,
    Return_to_the_College_Dean	=	24	,
    Recommend_and_Send_to_the_VRR	=	25	,
    Forward_to_SC_Subcomittee_Chair	=	27	,
    Forward_to_Promotion_Committee	=	28	,
    Return_to_the_College_Dean_from_VRR	=	29	,
    Share_with_Scientific_Council	=	30	,
    Promotable	=	33	,
    Unpromotable	=	34	,
    Recommend_and_Send_to_VRR	=	35	,
    File_Incomplete_and_Return_to_VRR	=	36	,
    Share_with_SC_Sucommittee	=	38	,
    Return_to_VRR	=	42	,
    Return_to_VRR_without_Promotion_Committee_Report	=	48	,
    Update_CV_and_Publications	=	54	,
    Update_Chairman_Input	=	62	,
    Update_Annual_Evaluation	=	63	,
    Unarchive_Promotion_Application	=	64	,
    Unarchive_Promotion_Application_After_Unpromotable	=	65	,
    Return_to_VRR_From_Applicant	=	66	,
    Return_to_VRR_From_Dept	=	67	,
    Return_to_VRR_From_FacultyAffairs	=	68	
    

}
public enum ActionType { Forward = 1, Return = 2, ForwardAlive = 3, ReturnAlive = 4, ForwardAliveLock = 5, ReturnAliveUnlock = 6, Share = 7, Unshare = 8, Save = 9, ReturnNotRec}

public enum PCMemberSelType { College = 2, TopAuthority = 3 }

public enum AttachmentCategoryID
{
    CV = 2, Publication = 3,  List_of_Publication = 4, Others = 5, PCReport = 6
}
public enum RankProfessorial { Assistant_Professor, Associate_Professor, Professor, Visiting_Associate_Professor }
public enum DepartmentType { Department = 1, College = 2, NonAcademic_Department = 3, Council = 4, Deanship = 5, Program = 6 , Vice_Rector = 7}
public enum GlobalAttribute
{
    Password = 1, StopEmail, MaxNoOfReminders, PointsNeededForAssociateProfessor, PointsNeededForProfessor
            , MinScientificCouncilMembers, MinPromotionSubcommitteeMembers
            , MaxScientificCouncilMembers, MaxPromotionSubcommitteeMembers, SizeOfListFinalER, ParaForJointAppointee
}
public enum ViewType
{
    Candidate = 0, Department = 1, College= 2, VRR = 4
}
//public enum TaskExtID
//{

//    Willingness_for_being_Promotion_Committee_Chairman = 2,
//    Willingness_Letter_for_External_Reviewers = 3,
//    External_Evaluation = 5,
//    External_Promotion_Committee_Member = 6,
//    All_Members_Have_Signed = 10,
//    Welcome_note_to_new_department_managers = 11,
//    Promotion_Decision_Notification_Positive_Eng = 13,
//    Promotion_Decision_Notification_Negative_Eng = 15,
//    Acceptance_Notification = 17,
//    Welcome_Message_External_Evaluation = 18,
//    Willingness_for_being_Promotion_Committee_Member = 19,
//    Acceptance_Notification_Promotion_Committee = 20,
//    Submit_External_Evaluation = 21,
//    Withdraw_External_Evaluation = 23,
//    Honorarium_External_Reviewer = 25,
//    Honorarium_External_Promotion_Committee = 26,
//    Willingness_Letter_Content_External_Reviewer = 27,
//    Willingness_for_being_Promotion_Committee_Member_External = 28,
//    Willingness_Letter_Content_Promotion_Committee_Chairman = 29,
//    Willingness_Letter_Content_Promotion_Committee_Member = 30,
//    Willingness_Letter_Content_Promotion_Committee_Member_External = 31,
//    All_PCMembers_Have_Accepted = 32,
//    Material_Ready_to_be_Sent = 33,
//    Material_Not_Ready_to_be_Sent = 34,
//    Check_System_Notification = 35,
//    Material_Updated
//}
public enum TaskIDs
{
    Dummy = 0,
    Apply_for_Promotion = 1,
    Department_Approval = 2,
    Departmental_Committee_Chairmanship = 3,
    Verification_and_Preparation_of_Annual_Evaluation = 4,
    Departmental_Evaluation__NotInUse = 5,
    Update_Promotion_Request = 6,
    College_Approval= 7,
    TopLowAuthority_TitleShort_Approval = 8,
    TopAuthority_Approval = 9,
    Promotion_Committee_Membership1 = 14,
    SC_Subcommittee_Chairmanship = 22,
    VRR1_NotInUse = 25,
    Promotion_Committee_Chairmanship =27,
    Promotion_Committee_Membership2 = 29,
    Promotion_Committee_Membership3 = 30,
    Promotion_Committee_Membership4 = 31,
    VRR2_NotInUse = 32,
    VRR3_NotInUse = 33,
    Update_CV_and_Publications = 36,
    Departmental_Committee_Membership1 = 37,
    Departmental_Committee_Membership2 = 38,
    Departmental_Committee_Membership3 = 39,
    Departmental_Committee_Membership4 = 40,
    Applicant_NotInUse = 42,


    
    Scientific_Council_Membership_1 = 43,
    Scientific_Council_Membership_2 = 44,
    Scientific_Council_Membership_3 = 45,
    Scientific_Council_Membership_4 = 46,
    Scientific_Council_Membership_5 = 47,
    Scientific_Council_Membership_6 = 48,
    Scientific_Council_Membership_7 = 51,
    Scientific_Council_Membership_8 = 52,
    Scientific_Council_Membership_9 = 53,
    Scientific_Council_Membership_10 = 54,
    VRR4_NotInUse = 55,
    TopAuthority_Application_Closed_promoted = 56,
    Applicant_Application_Closed_promoted = 112,
    Update_Chairman_Input = 113,
    Update_Annual_Evaluation = 114,
    Scientific_Council_Coordination = 115,
    Scientific_Council_Membership_11 = 116,
    Update_Promotion_Request_From_TopAuthority = 117,
    SC_Subcommittee_Membership_1 = 118,
    SC_Subcommittee_Membership_2 = 119,
    SC_Subcommittee_Membership_3 = 120,
    SC_Subcommittee_Membership_4 = 121,
    TopAuthority_Application_Closed_Denied = 122,
    Applicant_Application_Closed_Denied = 123
}
public enum EmployeeType { Internal, External }
public enum DepartmentID { VPRI = 46, Deanship_Of_Faculty_Affairs = 22, VPAA = 45, President = 42 }
public enum WillingessStatus { Not_Sent = 0, Waiting = 1, Accepted = 2, Declined = 3, Withdrawn = 4 }
public enum SendSelPubStatus { Material_Not_Sent = 0, Material_Sent = 1 }
public enum EvaluationStatus { Not_Submitted = 0, Saved_But_Not_Submitted = 1, Submitted = 2, Withdrawn = 3, Returned = 4 }
public enum PCPosition { Chairman = 10, Member_Specialty_Area = 11, Member_Related_Area = 12 }
public enum RecordStatus { Active, Inactive }
public enum typeofDBOperation { Insert, Delete, Update, Get }
public enum PermanentCommitteeTypeTitle { Scientific_Council = 1, SC_Subcommittee = 2, Scientific_Council_Coordinator = 4}
public enum RoleID
{
    Applicant = 1,
    DepartmentChairman = 2,
    Departmental_Committee_Chairman = 3,
    College_Dean = 4,
    TopLowAuthority = 5,
    TopAuthority = 6,
    ScientificCouncil = 7,
    DeanofFacultyAffairs = 8,
    ScientificCouncilSubCommittee = 9,
    Promotion_Committee_Chairman = 10,
    Promotion_Committee_Member_1 = 11,
    Promotion_Committee_Member_2 = 12,
    Promotion_Committee_Member_3 = 13,
    Promotion_Committee_Member_4 = 14,
    External_Reviewer = 19,
    NonKFUPMPromotionCommitteeMember = 20,
    Departmental_Committee_Member_1 = 24,
    Departmental_Committee_Member_2 = 25,
    Departmental_Committee_Member_3 = 26,
    Departmental_Committee_Member_4 = 27,
    Scientific_Council_Member_1 = 28,
    Scientific_Council_Member_2 = 29,
    Scientific_Council_Member_3 = 30,
    Scientific_Council_Member_4 = 31,
    Scientific_Council_Member_5 = 32,
    Scientific_Council_Member_6 = 33,
    Scientific_Council_Member_7 = 34,
    Scientific_Council_Member_8 = 35,
    Scientific_Council_Member_9 = 36,
    Scientific_Council_Member_10 = 37,
    Scientific_Council_Member_11 = 38,
    Scientific_Council_Member_12 = 39,
    Scientific_Council_Coordinator = 40,
    SC_Subcommittee_Chair = 42,
    SC_Subcommittee_Member_1 = 43,
    SC_Subcommittee_Member_2 = 44,
    SC_Subcommittee_Member_3 = 45,
    SC_Subcommittee_Member_4 = 46,
    President = 48
}
public enum FormCategoryID
{
    Faculty_Information = 1, Reviewers_Information = 2, Committee_Information = 3, Application_Links = 4
}
public enum FormID
{
    Requester_aspx = 1,
    Points_aspx = 2,
    ServiceYears_aspx = 3,
    FormExtRev_aspx = 4,
    Exclusion_aspx = 5,
    Checkout_aspx = 6,
    FilesUpload_aspx = 7,
    History_aspx = 8,
    Message_aspx = 9,
    DeptCommittee_aspx = 10,
    ChairmanInput_aspx = 11,
    ServiceEvaluations_aspx = 12,
    PromotionCommittee_aspx = 13,
    Report_aspx = 14,
    PCCommunicate_aspx = 18,
    FormFinalExtRev_aspx = 19,
    EligibilityChecklist_aspx = 20,
    FormExtRevSel_aspx = 21,
    PromotionReportPCChair_aspx = 22,
    PromotionReportPCMem_aspx = 23,
    FinalDecision_aspx = 24,
    ExtReviewerListPCChair_aspx = 25,
    ExtReviewerListPCMem_aspx = 26,
    LaunchBusySchedule_aspx = 27,
    PostBusySchedule_aspx = 28,
    ViewBusySchedule_aspx = 29,
    ViewChairReport_aspx = 31,
    ReviewersSummary_aspx = 32,
    ViewChairReport4VRGSSR_aspx = 33,
    Form_ApplicationFiles_aspx = 34,
    Form_ReviewerForm_aspx = 35,
    Form_ReviewerAction_aspx = 36,
    ExtMain_aspx = 37,
    ExtPCMain_aspx = 38,
    Form_BusyScheduleMember_aspx = 39,
    Form_ExtReviewerList_aspx = 40,
    Form_SummaryReviewersForm_aspx = 41,
    Form_ExtPCAction_aspx = 42,
    Form_PromotionReportPCMem_aspx = 43,
    ContactPromotionCommittee_aspx = 44,
    OldMeetings_aspx = 45,
    Reminders_aspx = 46,
    Honorarium_aspx = 47,
    ContactVRGSSR_aspx = 48,
    ScientificCouncil_aspx = 49,
    PCFinal4PC_aspx = 50,
    PCFeedback_aspx = 51,
    ApplicatioRoles_aspx = 53,
    ExtMainAr_aspx = 54,
    Form_ReviewersFormAr_aspx = 55,
    Correspondence_aspx = 56,
    PublicationsEvaluation_aspx = 60,
ExtRevProfile_aspx = 61,
    Form_AppDocs_aspx = 63,
    Form_Evaluation_aspx = 64,
    ExtMessage_aspx = 66,
    ExtAction_aspx = 67

}
public enum TaskExtID
{

    Willingness_for_being_Promotion_Committee_Chairman = 2,
    Willingness_Letter_for_External_Reviewers = 3,
    Post_Busy_Schedule = 4,
    External_Evaluation = 5,
    Non_KFUPM_Promotion_Committee_Member = 6,
    Meeting_Reminder = 9,
    All_MembersHaveSigned = 10,
    Welcome_note_to_new_department_managers = 11,
    Willingness_Letter_for_External_Reviewers_in_Arabic = 12,
    Promotion_Decision_Notification_Positive_Eng = 13,
    Promotion_Decision_Notification_Positive_Arabic = 14,
    Promotion_Decision_Notification_Negative_Eng = 15,
    Promotion_Decision_Notification_Negative_Arabic = 16,
    Acceptance_Notification = 17,
    Welcome_Message_External_Evaluation = 18,
    Willingness_for_being_Promotion_Committee_Member = 19,
    Acceptance_Notification_Promotion_Committee = 20,
    Submit_External_Evaluation = 21,
    Withdraw_External_Evaluation = 22,
    Honorarium_External_Reviewer = 23,
    Honorarium_External_Promotion_Committee = 24,
    Willingness_Letter_Content_External_Reviewer = 25,
    Willingness_for_being_Promotion_Committee_Member_External = 26,
    Willingness_Letter_Content_Promotion_Committee_Chairman = 27,
    Willingness_Letter_Content_Promotion_Committee_Member = 28,
    Willingness_Letter_Content_Promotion_Committee_Member_External = 29,
    All_PCMembers_Have_Accepted = 30,
    Material_Ready_to_be_Sent = 31,
    Material_Not_Ready_to_be_Sent = 32,
    Check_System_Notification = 33,
    Material_Updated = 34,
    Notification_Excessive_Reminders_Employee = 35,
    Notification_of_Excessive_Reminders_External_Reviewer = 36,
    Request_Members_to_Digitally_Sign = 37


}
public enum ExternalReviewerSelectionType { Applicant = 0, Department = 1, College = 2, SCSubCom = 3 }

#endregion
public class ApplicationList
{
    public int ApplicationID { get; set; }
    public string Role { get; set; }
    public string Employee { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public string Rank { get; set; }
    public string ToRank { get; set; }
    public string Status { get; set; }
    public DateTime? AppDate { get; set; }
    public byte RoleID { get; set; }
}
public class TaskFormDTO
{
    public int TaskID { get; set; }
    public byte FormID { get; set; }
    public byte? Rank { get; set; }
    public byte? Level { get; set; }
    public string Instruction { get; set; }
    public bool Checkable { get; set; }

    public bool TaskExternal { get; set; }
    public int ExternalReviewerID { get; set; }

    public string Title { get; set; }
    public string Page { get; set; }
    public int ApplicationID { get; set; }
    public bool Completed { get; set; }
    public string Message { get; set; }

}
public class EmailContent
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
public struct FormStatusStruct
{

    public string task;
    public string Task
    {
        get { return task; }
    }
    private bool status;
    public bool Status
    {
        get { return status; }
    }
    private string message;

    public string Message
    {
        get { return message; }
        set { message = value; }
    }


    public FormStatusStruct(string t, bool s, string m)
    {
        task = t;
        status = s;
        message = m;
    }
}
public class Style
{
    public string text { get; set; }
    public string value { get; set; }
}
public class Attribute
{
    public string text { get; set; }
    public string value { get; set; }
}
#region Views
public class vw_NextTaskDTO
{

    public int? TaskID { get; set; }
    public string Task { get; set; }
    public int? ActionID { get; set; }
    public string Action { get; set; }

    public byte? NextPhaseID { get; set; }
    public int? NextTaskID { get; set; }
    public string NextTask { get; set; }
    public string NextRole { get; set; }
    public string Message { get; set; }
    public bool? HasCommitteeInfo { get; set; }
    public byte? PhaseID { get; set; }
    public string Type { get; set; }
    public byte? NextRoleID { get; set; }
}
public class vw_ActiveTask1
{
    public int ApplicationID { get; set; }
    public string AppID { get; set; }
    public string AppEmail { get; set; }
    public string Applicant { get; set; }
    public string EmployeeID { get; set; }
    public string Employee { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public int RoleID { get; set; }
    public string Task { get; set; }
    public int TaskID { get; set; }
    public string AppDeptSN { get; set; }
    public byte? Reminders { get; set; }

}
#endregion

public class Application_Role1
{
    public int ApplicationID { get; set; }
    public int RoleID { get; set; }
    public string EmployeeID { get; set; }
}
public class Application1
{
    public Int32 ApplicationID { get; set; }
    public bool ApplicationClosed { get; set; }
    public string EmployeeID { get; set; }
    public byte? PhaseID { get; set; }
    public string Rank { get; set; }
    public DateTime? StartDate { get; set; }
    public bool? ExclusionList { get; set; }
    public string Comment { get; set; }
    public bool? HardCopy { get; set; }
    public string SCComments { get; set; }
    public bool RectorDecision { get; set; }
    public bool FinalDeicision { get; set; }
    public bool SCDecision { get; set; }
    public string RectorComments { get; set; }
}

public class Action_Log1
{
    public int Action_LogID { get; set; }
    public string Detail { get; set; }
    public DateTime TimeStamp  { get; set; }
}

public class Task_ExtMessage1
{
    public int taskID { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}
//public class Employee1 { 

//public  string  EmployeeID              { get; set; }
//public  string  SecondEmail             { get; set; }
//public  string  Email             { get; set; }
//public  string  Title                   { get; set; }
//public  string  Name                     { get; set; }
//public  string  Rank                     { get; set; }
//public  string  Department            { get; set; }
//public  string  College            { get; set; }
//public  string  POBox            { get; set; }
//public  string  Phone            { get; set; }
//public  string  JoinDate            { get; set; }
//public  string  Status            { get; set; }
		
//}
public class Form_PromotionCommittee1
{ 
public int     ApplicationID        { get; set; }
public char    Type                 { get; set; }
public byte    Position             { get; set; }
public bool    IsKFUPM              { get; set; }
public string   EmployeeID         { get; set; }
public string   Name               { get; set; }
public string   Department         { get; set; }
public string   Rank               { get; set; }
public string   Email              { get; set; }
public string   MailingAddress     { get; set; }
public string   Major              { get; set; }
public string   Phone              { get; set; }

}
public class Form_DepartmentCommittee1
{
    public int ApplicationID { get; set; }
    public byte Position { get; set; }
    public string EmployeeID { get; set; }
}
public class Application_TaskLog1
{
    public int TaskLogID { get; set; }
    public int ApplicationID { get; set; }
    public int? TaskID { get; set; }
    public int? ActionID { get; set; }
    public DateTime? SentDate { get; set; }
    public bool Completed { get; set; }
    public byte? Reminders { get; set; }
    public string Message { get; set; }
}
public class ResourceFile
{
    public ResourceFile()
    {

    }
    public string Key { get; set; }
    public string Value { get; set; }
}
public class EmployeeERP
{
    public string username { get; set; }
    public string kfupm_id { get; set; }
    public string name_en { get; set; }
    public string name_ar { get; set; }
    public string active_status { get; set; }
    public string department_id { get; set; }
    public string email { get; set; }
    public string mobile { get; set; }
    public string nationality { get; set; }
    public string type { get; set; }
    public string rank { get; set; }
}

public class RootObject
{
    public List<EmployeeERP> data { get; set; }
}

public class HeadDept
{
    public string id { get; set; }
    public string name_en { get; set; }
    public string name_ar { get; set; }
    public string username { get; set; }
}

public class ParentDepartment
{
    public string id { get; set; }
    public string name_en { get; set; }
    public string name_ar { get; set; }
}

public class DepartmentERP
{
    public string id { get; set; }
    public string name_en { get; set; }
    public string name_ar { get; set; }
    public HeadDept manager { get; set; }
    public ParentDepartment mother_department { get; set; }
}

public class DepartmentERPList
{
    public List<DepartmentERP> data { get; set; }
}