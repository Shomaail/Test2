Imports System.Collections.Generic
Imports KFUPM.CAS
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim vUserType As String = ""
    Dim vUID As String = ""
    Protected Sub validate_user(ByVal vUID As String)

        ' ''Dim states As List(Of String) = New List(Of String) _
        ' ''    (New String() {"AL", "AZ", "CA", "CO", "NV", "OK"})

        ' ''Dim val() As String = {"DR", "GR", "G1", "G2", "PG"}

        ' ''Dim LEVEL_CODE_LIST As New List(Of String)

        ' ''LEVEL_CODE_LIST.AddRange(val)
        ' ''If LEVEL_CODE_LIST.Contains("b") Then
        ' ''End If
        Dim vType_Code As String = ""
        Dim vType_Desc As String = ""
        Dim vLevel_Code As String = ""
        Dim vMSG As String = ""
        Dim vFull_Part_time_flag As String = ""
        Dim vQualification_status As String = ""
        Dim vQualification_status_desc As String = ""
        Dim vStatus As String = ""
        Dim vStatus_Desc As String = ""
        Dim vCOMMENT_FLAG As String = ""
        '   vMSG = vUID
        '        Loan.ShowClientMessage(Me, vMSG, Loan.ClientMessageType.warning_msg)
        '         Exit Sub

        If vUID = "" Then Exit Sub
        If IsNumeric(vUID.Substring(1, 1)) Then

            vUserType = "S"

        Else
            vUserType = "E"

        End If


        If vUserType = "S" Then
            read_Studenet_Comments(vCOMMENT_FLAG)
            If vCOMMENT_FLAG = "BLOCK" Then

                vMSG = "—ﬁ„ «·ÿ«·»  „ ≈Ìﬁ«›Â «·—Ã«¡ „—«Ã⁄… ’‰œÊﬁ «·ÿ·«» "
                Loan.ShowClientMessage(Me, vMSG, Loan.ClientMessageType.warning_msg)
                Exit Sub
            End If

            read_Studenet_stipend_Qualification(vFull_Part_time_flag, vQualification_status, vQualification_status_desc, vStatus, vStatus_Desc)
            If vFull_Part_time_flag = "P" Then

                vMSG = "·« ÌÕﬁ ·ﬂ «· ﬁœ„ »ÿ·» ”·›…(part time student) "
                Loan.ShowClientMessage(Me, vMSG, Loan.ClientMessageType.warning_msg)
                Exit Sub
            End If
            If vQualification_status.Length > 1 Then
                If vQualification_status.Substring(0, 2) = "NQ" Then

                    vMSG = "·« ÌÕﬁ ·ﬂ «· ﬁœ„ »ÿ·» ”·›… -- " & vQualification_status_desc
                    Loan.ShowClientMessage(Me, vMSG, Loan.ClientMessageType.warning_msg)
                    Exit Sub
                End If
            End If
            If vStatus = "AS" Or vStatus = "EG" Then
            Else
                vMSG = "·« ÌÕﬁ ·ﬂ «· ﬁœ„ »ÿ·» ”·›… -- " & vStatus_Desc
                Loan.ShowClientMessage(Me, vMSG, Loan.ClientMessageType.warning_msg)
                Exit Sub

            End If

            ' ''get_student_type(Me.Login1.UserName, vType_Code, vType_Desc, vLevel_Code)

            ' ''If vLevel_Code = "DR" Or vLevel_Code = "GR" Or vLevel_Code = "G1" Or vLevel_Code = "G2" Or vLevel_Code = "PG" Then
            ' ''    If vType_Code = "MR" Then
            ' ''        FormsAuthentication.RedirectFromLoginPage(Me.Login1.UserName, False)
            ' ''    Else

            ' ''        vMSG = "·« ÌÕﬁ ·ﬂ «· ﬁœ„ »ÿ·» ”·›… -- " & vType_Desc
            ' ''        Loan.ShowClientMessage(Me, vMSG, Loan.ClientMessageType.warning_msg)
            ' ''    End If
            ' ''End If
        End If
        FormsAuthentication.SetAuthCookie(vUID, False)
        'FormsAuthentication.RedirectFromLoginPage(vUID, False)



    End Sub
    Public Sub KFUPMCASAuthentication()
        Dim clsCAS As New CASAuthentication(Request)
        Dim myUser As New KFUPMuser()

        If clsCAS.doAuthenticate() Then
            myUser = clsCAS.getKFUPMUser()
            Session("UID") = myUser.getUID
            Session("UName") = myUser.getCN
        Else

            Response.Redirect(clsCAS.getCasURL())
        End If


        '' To test and debug comment the abouve lines and uncomment  the following
        ''  myUser = clsCAS.getKFUPMUser()
        ' Session("UID") = "s201032600"
        'Session("UID") = "alshammari"



    End Sub

    ''Function LDAP_Authinticate() As Boolean

    ''    ' Dim adPath As String = "LDAP://196.15.32.161/cn=KFUPM-People,o=KFUPM,dc=kfupm,dc=edu,dc=sa"
    ''    Dim adPath As String = "LDAP://10.140.5.101/cn=KFUPM-People,o=KFUPM,dc=kfupm,dc=edu,dc=sa"
    ''    Dim userDN As String = "uid=" & Me.Login1.UserName & ",cn=KFUPM-People,o=KFUPM,dc=kfupm,dc=edu,dc=sa"  'this is what is used to bind with
    ''    Dim root As New System.DirectoryServices.DirectoryEntry(adPath, userDN, Me.Login1.Password, System.DirectoryServices.AuthenticationTypes.None)
    ''    Try 'try'
    ''        Dim obj As Object
    ''        obj = root.NativeObject
    ''        '  Login1.UserName = "s200860900"
    ''        ' Login1.UserName = "s200438400"

    ''        If IsNumeric(Me.Login1.UserName.Substring(1, 1)) Then
    ''            If Me.Login1.UserName.Length < 10 Then
    ''                Return False
    ''            Else
    ''                vUserType = "S"
    ''                Return True
    ''            End If
    ''        Else
    ''            vUserType = "E"
    ''            Return True
    ''        End If

    ''    Catch ex As Exception ' : try
    ''        Me.Login1.FailureText = "«”„ «·„” Œœ„ √Ê ﬂ·„… «·„—Ê— €Ì— ’ÕÌÕ… / Õ«Ê· „—… √Œ—Ï"
    ''        Loan.ShowClientMessage(Me, ex.Message, Loan.ClientMessageType.error_msg)
    ''        Return False
    ''    End Try

    ''End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            KFUPMCASAuthentication()
            vUID = Session("UID")
            '   vUID = "s201531210"
            '  vUID = "s201302150"
            '  vUID = "hali"
            validate_user(vUID)

            ' Try

            Me.mnu0.Items(0).Enabled = False
            Me.mnu0.Items(4).Enabled = False
            Me.mnu1.Visible = False
            Me.mnu2.Visible = False
            Me.mnu3.Visible = False
            Me.mnu4.Visible = False

            Select Case Loan.getAuthorityCode(vUID)
                Case 0
                    Me.mnu0.Items(0).Enabled = True
                    Me.mnu0.Items(4).Enabled = True
                Case 1
                    Me.mnu1.Visible = True
                    Me.mnu2.Visible = True
                Case 2
                    Me.mnu2.Visible = True
                Case 3
                    Me.mnu3.Visible = True
                Case 4
                    Me.mnu4.Visible = True
                Case 20
                    Me.mnu1.Visible = True
                    Me.mnu1.Items(0).Enabled = True
                    Me.mnu1.Items(1).Enabled = False
                    Me.mnu1.Items(2).Enabled = False
                    Me.mnu1.Items(3).Enabled = False
                    Me.mnu1.Items(4).Enabled = False
                    Me.mnu1.Items(5).Enabled = False
                    Me.mnu1.Items(6).Enabled = False
                    Me.mnu2.Visible = True
                    Me.mnu3.Visible = True
                    Me.mnu4.Visible = True
            End Select

            '   Catch ex As Exception

            '   End Try
        End If


    End Sub
    Protected Sub get_student_type(ByVal vUserID As String, ByRef vStd_Type_Code As String, ByRef vStd_Type_desc As String, ByRef vLevel_Code As String)
        vStd_Type_Code = ""
        Dim vSQL As String = " SELECT spriden_pidm, curr_term.stvterm_code AS stu_term, spriden_id  AS stu_id, " & _
        " f_class_calc_fnc(sgbstdn_pidm, sgbstdn_levl_code, curr_term.stvterm_code) AS stu_Class, " & _
        "  stvclas_desc AS stu_class_desc , sgbstdn_levl_code  " & _
        "  FROM spriden, " & _
        "  sgbstdn, " & _
        " stvclas, " & _
        " (SELECT stvterm_code FROM stvterm " & _
        " WHERE sysdate BETWEEN stvterm_housing_start_date AND stvterm_housing_end_date) curr_term, " & _
        " (SELECT sgbstdn_pidm sgb_pidm, MAX(sgbstdn_term_code_eff) AS max_term FROM sgbstdn " & _
        " WHERE sgbstdn_term_code_eff <= (SELECT stvterm_code  FROM stvterm " & _
        "       WHERE sysdate BETWEEN stvterm_housing_start_date AND stvterm_housing_end_date) " & _
        "  GROUP BY sgbstdn_pidm) mtce " & _
        "     WHERE(spriden_change_ind Is NULL) " & _
        " AND spriden_last_name NOT LIKE 'Student%' " & _
        " AND spriden_pidm                         = sgbstdn_pidm " & _
        " AND sgbstdn_pidm                         = mtce.sgb_pidm " & _
        " AND sgbstdn_term_code_eff                = mtce.max_term " & _
        " AND sgbstdn_stst_code                    ='AS' " & _
        " AND f_class_calc_fnc(sgbstdn_pidm, sgbstdn_levl_code, curr_term.stvterm_code)      = stvclas_code " & _
        " and spriden_id = '" & vUserID.Substring(1, 9) & "'"
        Try
            With Loan.getDataReader(vSQL, Loan.ConnectionStrings.Banner_Connection_String)
                .Read()
                If .HasRows Then
                    vStd_Type_Code = IIf(IsDBNull(!stu_Class), "", !stu_Class)
                    vStd_Type_desc = IIf(IsDBNull(!stu_class_desc), "", !stu_class_desc)
                    vLevel_Code = IIf(IsDBNull(!sgbstdn_levl_code), "", !sgbstdn_levl_code)
                End If


                .Close()
            End With
        Catch ex As Exception

        End Try
    End Sub
    Public Function get_student_effictive_term(ByVal vPIDM As String, ByVal vTerm As String) As String

        Dim vEffictive_Term As String

        'Read effictive term
        get_student_effictive_term = ""

        Dim vSQL As String = "select sb_learner.f_query_current(" & vPIDM & " , '" & vTerm & "') as eff_term from  dual "

        Try
            With Loan.getDataReader(vSQL, Loan.ConnectionStrings.Banner_Connection_String)
                .Read()
                If .HasRows Then
                    vEffictive_Term = IIf(IsDBNull(!eff_term), "", !eff_term)

                End If


                .Close()
            End With
        Catch ex As Exception

        End Try

    End Function
    Protected Sub read_Studenet_stipend_Qualification(ByRef vFull_Part_time_flag As String, ByRef vQualification_status As String, ByRef vQualification_status_desc As String, ByRef vStatus As String, ByRef vStatus_desc As String)
        'Read the full time part time flag for the effictive term
        'GETstudent PIDM 

        Dim vEffictive_Term As String
        Dim vCurrent_Term As String

        Dim vPIDM As String
        Dim sql As String
        sql = "select spriden_pidm from spriden where spriden_change_ind is null and spriden_id= '" & vUID.Substring(1, 9) & "'"
        With Loan.getDataReader(sql, Loan.ConnectionStrings.Banner_Connection_String)
            .Read()
            If .HasRows Then
                vPIDM = IIf(IsDBNull(!spriden_pidm), "", !spriden_pidm)
            Else
                Dim vMSG As String = "—ﬁ„ «·ÿ«·» €Ì— „ÊÃÊœ ›Ì ‰Ÿ«„ «·ÿ·«» "
                Loan.ShowClientMessage(Me, vMSG, Loan.ClientMessageType.warning_msg)
            End If

            .Close()
        End With

        'GET Last effictive TERM 
        sql = "select sb_learner.f_query_current(" & vPIDM & " ,   (SELECT stvterm_code  FROM stvterm    WHERE sysdate BETWEEN stvterm_housing_start_date AND stvterm_housing_end_date))  as eff_term from  dual "
        With Loan.getDataReader(sql, Loan.ConnectionStrings.Banner_Connection_String)
            .Read()
            If .HasRows Then
                vEffictive_Term = IIf(IsDBNull(!eff_term), "", !eff_term)
            End If

            .Close()
        End With
        'read part time full time flag 
        sql = "select  SGBSTDN_FULL_PART_IND  " _
                      & " from SGBSTDN  " _
                      & "    where  SGBSTDN_PIDM ='" & vPIDM & "' and SGBSTDN_TERM_CODE_EFF  = '" & vEffictive_Term & "'"
        With Loan.getDataReader(sql, Loan.ConnectionStrings.Banner_Connection_String)
            .Read()
            If .HasRows Then
                vFull_Part_time_flag = IIf(IsDBNull(!SGBSTDN_FULL_PART_IND), "", !SGBSTDN_FULL_PART_IND)
            End If

            .Close()
        End With
        'Read qualification status
        '>>>> Hussain   update on 11-01-2016     sgbuser_suda_code was changed to sgbuser_sudf_code to get the correct qualification status 
        'sql = " select  sgbuser_pidm,  sgbuser_term_code,  sgbuser_sudf_code, sgbuser_activity_date ,stvsuda_desc " _
        '& " from sgbuser,  stvsuda " _
        '& " where sgbuser_pidm= " & vPIDM & "  and sgbuser_term_code = (select max(sgbuser_term_code)  from sgbuser where sgbuser_pidm= " & vPIDM & ") " _
        '& " And stvsuda_code = sgbuser_suda_code "


        '>>> Hussain update on 19-05-2016   

        ' ''sql = " select  sgbuser_pidm,  sgbuser_term_code,  sgbuser_sudf_code, sgbuser_activity_date   " _
        ' ''& " from sgbuser  " _
        ' ''& " where sgbuser_pidm= " & vPIDM & "  and sgbuser_term_code = (select max(sgbuser_term_code)  from sgbuser where sgbuser_pidm= " & vPIDM & ") " 


        '?????????????????? if student is undergraduate then he is allowed to apply for loan even if he is not qualified
        '????       undergraduate check should be added STVLEVL

        vCurrent_Term = Loan.GetCurrentTermCode

        sql = " select  sgbuser_pidm,  sgbuser_term_code,  sgbuser_sudf_code, sgbuser_activity_date ,stvsudf_desc  " _
       & " from sgbuser ,  stvsudf " _
       & " where sgbuser_pidm= " & vPIDM & "  and sgbuser_term_code =  '" & vCurrent_Term & "'" _
       & " And stvsudf_code = sgbuser_sudf_code "


        With Loan.getDataReader(sql, Loan.ConnectionStrings.Banner_Connection_String)
            .Read()
            If .HasRows Then
                vQualification_status = IIf(IsDBNull(!sgbuser_sudf_code), "", !sgbuser_sudf_code)

                vQualification_status_desc = IIf(IsDBNull(!stvsudf_desc), "", !stvsudf_desc)
            Else
                vQualification_status = ""
            End If

            .Close()
        End With

        'Read Drop Flag 
        sql = " select  SGBSTDN_PIDM  STUD_PIDM,SGBSTDN_STST_CODE, SGBSTDN_TERM_CODE_EFF, STVSTST_DESC " _
             & " from SGBSTDN  join stvstst on STVSTST_CODE=SGBSTDN_STST_CODE    where  SGBSTDN_PIDM = " & vPIDM & "  and SGBSTDN_TERM_CODE_EFF = '" & vEffictive_Term & "'"
        With Loan.getDataReader(sql, Loan.ConnectionStrings.Banner_Connection_String)
            .Read()
            If .HasRows Then
                vStatus = IIf(IsDBNull(!SGBSTDN_STST_CODE), "", !SGBSTDN_STST_CODE)
                vStatus_desc = IIf(IsDBNull(!STVSTST_DESC), "", !STVSTST_DESC)
            Else
                vStatus = ""
                vStatus_desc = ""
            End If

            .Close()
        End With

    End Sub



    Protected Sub read_Studenet_Comments(ByRef vCOMMENT_FLAG As String)
        'Read Students Comments table to get if a student is blocked from loan requests 
        Try
            Dim sql As String = "select SC_COMMENT_FLAG from STUDENTS_COMMENTS where STD_ID = '" & vUID.Substring(1, 9) & "'"
            With Loan.getDataReader(sql, Loan.ConnectionStrings.Loan_Connection_String)
                .Read()
                If .HasRows Then
                    vCOMMENT_FLAG = IIf(IsDBNull(!SC_COMMENT_FLAG), "", !SC_COMMENT_FLAG)

                End If

                .Close()
            End With
        Catch ex As Exception

        End Try

    End Sub



End Class


