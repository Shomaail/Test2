<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Data Live from ERP</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Students Data<br />
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" EnableModelValidation="True">
         <Columns>
             <asp:BoundField DataField="STU_ID" HeaderText="STU_ID" SortExpression="STU_ID" />
             <asp:BoundField DataField="STU_FIRST_NAME" HeaderText="STU_FIRST_NAME" SortExpression="STU_FIRST_NAME" />
             <asp:BoundField DataField="STU_MIDDLE_NAME" HeaderText="STU_MIDDLE_NAME" SortExpression="STU_MIDDLE_NAME" />
             <asp:BoundField DataField="STU_LAST_NAME" HeaderText="STU_LAST_NAME" SortExpression="STU_LAST_NAME" />
             <asp:BoundField DataField="STU_NAME_ENGLISH" HeaderText="STU_NAME_ENGLISH" SortExpression="STU_NAME_ENGLISH" />
             <asp:BoundField DataField="STU_NAME_ARABIC" HeaderText="STU_NAME_ARABIC" SortExpression="STU_NAME_ARABIC" />
             <asp:BoundField DataField="STU_PHONE" HeaderText="STU_PHONE" SortExpression="STU_PHONE" />
             <asp:BoundField DataField="STU_EMAIL" HeaderText="STU_EMAIL" SortExpression="STU_EMAIL" />
             <asp:BoundField DataField="STU_COLL_CODE" HeaderText="STU_COLL_CODE" SortExpression="STU_COLL_CODE" />
             <asp:BoundField DataField="STU_COLL_DESC" HeaderText="STU_COLL_DESC" SortExpression="STU_COLL_DESC" />
             <asp:BoundField DataField="STU_COLL_CODE_DESC" HeaderText="STU_COLL_CODE_DESC" SortExpression="STU_COLL_CODE_DESC" />
             <asp:BoundField DataField="STU_DEPT_CODE" HeaderText="STU_DEPT_CODE" SortExpression="STU_DEPT_CODE" />
             <asp:BoundField DataField="STU_DEPT_DESC" HeaderText="STU_DEPT_DESC" SortExpression="STU_DEPT_DESC" />
             <asp:BoundField DataField="STU_DEPT_CODE_DESC" HeaderText="STU_DEPT_CODE_DESC" SortExpression="STU_DEPT_CODE_DESC" />
             <asp:BoundField DataField="STU_LEVEL_CODE" HeaderText="STU_LEVEL_CODE" SortExpression="STU_LEVEL_CODE" />
             <asp:BoundField DataField="STU_LEVEL_DESC" HeaderText="STU_LEVEL_DESC" SortExpression="STU_LEVEL_DESC" />
             <asp:BoundField DataField="STU_LEVEL_CODE_DESC" HeaderText="STU_LEVEL_CODE_DESC" SortExpression="STU_LEVEL_CODE_DESC" />
             <asp:BoundField DataField="STU_MAJOR_CODE" HeaderText="STU_MAJOR_CODE" SortExpression="STU_MAJOR_CODE" />
             <asp:BoundField DataField="STU_MAJOR_DESC" HeaderText="STU_MAJOR_DESC" SortExpression="STU_MAJOR_DESC" />
             <asp:BoundField DataField="STU_MAJOR_CODE_DESC" HeaderText="STU_MAJOR_CODE_DESC" SortExpression="STU_MAJOR_CODE_DESC" />
             <asp:BoundField DataField="STU_SEMESTER_CODE" HeaderText="STU_SEMESTER_CODE" SortExpression="STU_SEMESTER_CODE" />
             <asp:BoundField DataField="STU_SEMESTER_DESC" HeaderText="STU_SEMESTER_DESC" SortExpression="STU_SEMESTER_DESC" />
             <asp:BoundField DataField="STU_SEMESTER_CODE_DESC" HeaderText="STU_SEMESTER_CODE_DESC" SortExpression="STU_SEMESTER_CODE_DESC" />
         </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BannerCS %>" ProviderName="<%$ ConnectionStrings:BannerCS.ProviderName %>" SelectCommand="SELECT STU_ID, STU_FIRST_NAME, STU_MIDDLE_NAME, STU_LAST_NAME, STU_NAME_ENGLISH, STU_NAME_ARABIC, STU_PHONE, STU_EMAIL, STU_COLL_CODE, STU_COLL_DESC, STU_COLL_CODE_DESC, STU_DEPT_CODE, STU_DEPT_DESC, STU_DEPT_CODE_DESC, STU_LEVEL_CODE, STU_LEVEL_DESC, STU_LEVEL_CODE_DESC, STU_MAJOR_CODE, STU_MAJOR_DESC, STU_MAJOR_CODE_DESC, STU_SEMESTER_CODE, STU_SEMESTER_DESC, STU_SEMESTER_CODE_DESC FROM VIEW_ACTIVE_STUDENTS_VRGSSR"></asp:SqlDataSource>
        Employees Data<br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" EnableModelValidation="True">
            <Columns>
                <asp:BoundField DataField="ASSIGNMENT_ID" HeaderText="ASSIGNMENT_ID" SortExpression="ASSIGNMENT_ID" />
                <asp:BoundField DataField="EFFECTIVE_START_DATE" HeaderText="EFFECTIVE_START_DATE" SortExpression="EFFECTIVE_START_DATE" />
                <asp:BoundField DataField="EFFECTIVE_END_DATE" HeaderText="EFFECTIVE_END_DATE" SortExpression="EFFECTIVE_END_DATE" />
                <asp:BoundField DataField="ASSIGNMENT_STATUS_TYPE_DESC" HeaderText="ASSIGNMENT_STATUS_TYPE_DESC" SortExpression="ASSIGNMENT_STATUS_TYPE_DESC" />
                <asp:BoundField DataField="PRIMARY_FLAG" HeaderText="PRIMARY_FLAG" SortExpression="PRIMARY_FLAG" />
                <asp:BoundField DataField="PERIOD_OF_SERVICE_START" HeaderText="PERIOD_OF_SERVICE_START" SortExpression="PERIOD_OF_SERVICE_START" />
                <asp:BoundField DataField="ACTUAL_TERMINATION_DATE" HeaderText="ACTUAL_TERMINATION_DATE" SortExpression="ACTUAL_TERMINATION_DATE" />
                <asp:BoundField DataField="ACTUAL_RANK" HeaderText="ACTUAL_RANK" SortExpression="ACTUAL_RANK" />
                <asp:BoundField DataField="ASS_ATTRIBUTE1" HeaderText="ASS_ATTRIBUTE1" SortExpression="ASS_ATTRIBUTE1" />
                <asp:BoundField DataField="BUDGETED_RANK" HeaderText="BUDGETED_RANK" SortExpression="BUDGETED_RANK" />
                <asp:BoundField DataField="ASS_ATTRIBUTE14" HeaderText="ASS_ATTRIBUTE14" SortExpression="ASS_ATTRIBUTE14" />
                <asp:BoundField DataField="ASS_ATTRIBUTE16" HeaderText="ASS_ATTRIBUTE16" SortExpression="ASS_ATTRIBUTE16" />
                <asp:BoundField DataField="PERSON_ID" HeaderText="PERSON_ID" SortExpression="PERSON_ID" />
                <asp:BoundField DataField="ED_EFF_START_DATE" HeaderText="ED_EFF_START_DATE" SortExpression="ED_EFF_START_DATE" />
                <asp:BoundField DataField="ED_EFF_END_DATE" HeaderText="ED_EFF_END_DATE" SortExpression="ED_EFF_END_DATE" />
                <asp:BoundField DataField="ORGANIZATION_ID" HeaderText="ORGANIZATION_ID" SortExpression="ORGANIZATION_ID" />
                <asp:BoundField DataField="ORGANIZATION_NAME" HeaderText="ORGANIZATION_NAME" SortExpression="ORGANIZATION_NAME" />
                <asp:BoundField DataField="SUPERVISOR_ID" HeaderText="SUPERVISOR_ID" SortExpression="SUPERVISOR_ID" />
                <asp:BoundField DataField="SUP_EMPLOYEE_NUMBER" HeaderText="SUP_EMPLOYEE_NUMBER" SortExpression="SUP_EMPLOYEE_NUMBER" />
                <asp:BoundField DataField="SUP_EMPLOYEE_NAME" HeaderText="SUP_EMPLOYEE_NAME" SortExpression="SUP_EMPLOYEE_NAME" />
                <asp:BoundField DataField="PERSON_TYPE_DESC" HeaderText="PERSON_TYPE_DESC" SortExpression="PERSON_TYPE_DESC" />
                <asp:BoundField DataField="FULL_NAME" HeaderText="FULL_NAME" SortExpression="FULL_NAME" />
                <asp:BoundField DataField="CURRENT_EMPLOYEE_FLAG" HeaderText="CURRENT_EMPLOYEE_FLAG" SortExpression="CURRENT_EMPLOYEE_FLAG" />
                <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="EMAIL_ADDRESS" SortExpression="EMAIL_ADDRESS" />
                <asp:BoundField DataField="MAILSTOP" HeaderText="MAILSTOP" SortExpression="MAILSTOP" />
                <asp:BoundField DataField="NATIONALITY_DESC" HeaderText="NATIONALITY_DESC" SortExpression="NATIONALITY_DESC" />
                <asp:BoundField DataField="SEX" HeaderText="SEX" SortExpression="SEX" />
                <asp:BoundField DataField="DATE_OF_BIRTH" HeaderText="DATE_OF_BIRTH" SortExpression="DATE_OF_BIRTH" />
                <asp:BoundField DataField="OFF_PHONE_NUMBER" HeaderText="OFF_PHONE_NUMBER" SortExpression="OFF_PHONE_NUMBER" />
                <asp:BoundField DataField="MOBILE_NUMBER" HeaderText="MOBILE_NUMBER" SortExpression="MOBILE_NUMBER" />
                <asp:BoundField DataField="MOTHER_DEPARTMENT_NAME" HeaderText="MOTHER_DEPARTMENT_NAME" SortExpression="MOTHER_DEPARTMENT_NAME" />
                <asp:BoundField DataField="USER_ID" HeaderText="USER_ID" SortExpression="USER_ID" />
                <asp:BoundField DataField="TITLE" HeaderText="TITLE" SortExpression="TITLE" />
                <asp:BoundField DataField="ORIGINAL_DATE_OF_HIRE" HeaderText="ORIGINAL_DATE_OF_HIRE" SortExpression="ORIGINAL_DATE_OF_HIRE" />
                <asp:BoundField DataField="COUNTRY_OF_BIRTH" HeaderText="COUNTRY_OF_BIRTH" SortExpression="COUNTRY_OF_BIRTH" />
                <asp:BoundField DataField="PER_INFORMATION1" HeaderText="PER_INFORMATION1" SortExpression="PER_INFORMATION1" />
                <asp:BoundField DataField="PER_INFORMATION2" HeaderText="PER_INFORMATION2" SortExpression="PER_INFORMATION2" />
                <asp:BoundField DataField="PER_INFORMATION3" HeaderText="PER_INFORMATION3" SortExpression="PER_INFORMATION3" />
                <asp:BoundField DataField="PER_INFORMATION4" HeaderText="PER_INFORMATION4" SortExpression="PER_INFORMATION4" />
                <asp:BoundField DataField="PER_INFORMATION5" HeaderText="PER_INFORMATION5" SortExpression="PER_INFORMATION5" />
                <asp:BoundField DataField="PER_INFORMATION6" HeaderText="PER_INFORMATION6" SortExpression="PER_INFORMATION6" />
                <asp:BoundField DataField="COST_CENTER" HeaderText="COST_CENTER" SortExpression="COST_CENTER" />
                <asp:BoundField DataField="COST_CENTER_DESC" HeaderText="COST_CENTER_DESC" SortExpression="COST_CENTER_DESC" />
                <asp:BoundField DataField="FIRST_NAME" HeaderText="FIRST_NAME" SortExpression="FIRST_NAME" />
                <asp:BoundField DataField="LAST_NAME" HeaderText="LAST_NAME" SortExpression="LAST_NAME" />
                <asp:BoundField DataField="BLOOD_TYPE" HeaderText="BLOOD_TYPE" SortExpression="BLOOD_TYPE" />
                <asp:BoundField DataField="PER_INFORMATION7" HeaderText="PER_INFORMATION7" SortExpression="PER_INFORMATION7" />
                <asp:BoundField DataField="PER_INFORMATION8" HeaderText="PER_INFORMATION8" SortExpression="PER_INFORMATION8" />
                <asp:BoundField DataField="PER_INFORMATION9" HeaderText="PER_INFORMATION9" SortExpression="PER_INFORMATION9" />
                <asp:BoundField DataField="EMPLOYEE_NUMBER" HeaderText="EMPLOYEE_NUMBER" SortExpression="EMPLOYEE_NUMBER" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:BIHRCS %>" ProviderName="<%$ ConnectionStrings:BIHRCS.ProviderName %>" SelectCommand="SELECT        ASSIGNMENT_ID, EFFECTIVE_START_DATE, EFFECTIVE_END_DATE, ASSIGNMENT_STATUS_TYPE_DESC, PRIMARY_FLAG, PERIOD_OF_SERVICE_START, 
                         ACTUAL_TERMINATION_DATE, ACTUAL_RANK, ASS_ATTRIBUTE1, BUDGETED_RANK, ASS_ATTRIBUTE14, ASS_ATTRIBUTE16, PERSON_ID, ED_EFF_START_DATE, 
                         ED_EFF_END_DATE, ORGANIZATION_ID, ORGANIZATION_NAME, SUPERVISOR_ID, SUP_EMPLOYEE_NUMBER, SUP_EMPLOYEE_NAME, PERSON_TYPE_DESC, 
                         FULL_NAME, CURRENT_EMPLOYEE_FLAG, EMAIL_ADDRESS, MAILSTOP, NATIONALITY_DESC, SEX, DATE_OF_BIRTH, OFF_PHONE_NUMBER, MOBILE_NUMBER, 
                         MOTHER_DEPARTMENT_NAME, USER_ID, TITLE, ORIGINAL_DATE_OF_HIRE, COUNTRY_OF_BIRTH, PER_INFORMATION1, PER_INFORMATION2, 
                         PER_INFORMATION3, PER_INFORMATION4, PER_INFORMATION5, PER_INFORMATION6, COST_CENTER, COST_CENTER_DESC, FIRST_NAME, LAST_NAME, 
                         BLOOD_TYPE, PER_INFORMATION7, PER_INFORMATION8, PER_INFORMATION9, EMPLOYEE_NUMBER
FROM            EDW_OWNER.HR_DATA_VRGSSR_VW
WHERE        (EMPLOYEE_NUMBER IS NOT NULL) AND (PERSON_TYPE_DESC &lt;&gt; 'Student') AND (&quot;LENGTH&quot;(EMPLOYEE_NUMBER) = 7) AND (USER_ID IS NOT NULL) AND 
                         (PERSON_TYPE_DESC &lt;&gt; 'External Employee') AND (PRIMARY_FLAG = 'Y') AND (ASSIGNMENT_STATUS_TYPE_DESC &lt;&gt; 'Suspend Assignment') AND 
                         (ASSIGNMENT_STATUS_TYPE_DESC &lt;&gt; 'Terminate Assignment') AND (PERSON_TYPE_DESC &lt;&gt; 'Ex-employee')"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
