<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="_Help" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<head id="Head1" runat="server">


    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Faculty Promotion System</title>

    <link rel="icon" href="images/favicon.png">
    <link rel="SHORTCUT ICON" href="images/favicon.png" />
    <!-- Bootstrap Core CSS -->
    <link href="../App_Themes/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="../App_Themes/3-col-portfolio.css" rel="stylesheet">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>
<body id="bodyMaster" dir="ltr" runat="server">

    <form id="form1" runat="server">
        <script src="../Scripts/JScript.aspx.js" type="text/javascript">  
        </script>
       <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <div style="border-style: solid; border-color: inherit; border-width: 0px; margin-left: 20px; margin-top: 20px; margin-right: 20px; height: 93px; background-color: #F8F8F8;"
            id="divTop" runat="server">

            <div style="float: left">
                <img alt="KFUPM Logo" style="width: 405px; height: 93px;" src="images/logo.png" />
            </div>
            <div style="float: left; padding-left: 20px">
                <h1>Faculty Promotion System</h1>
            </div>
        </div>

        <h1 style="padding-left: 15px">Faculty Promotion System Help</h1>
        <div style="padding-left: 15px">
            Welcome to the Faculty Promotion System online Help. The Faculty Promotion System is meant for 
        automating the promotion of faculty members of all the academinc departments. 
        The system facilitates faculty promotion 
        <br />
            <br />
            1.&nbsp;&nbsp;&nbsp; from Assistant Professor to Associate and 
        <br />
            2.&nbsp;&nbsp;&nbsp; from Associate Professor to 
        Full Professorial Rank.
        <br />
            <br />
            All Faculty Memnbers can access the system using the KFUPM Portal Email Username 
        and Password. The Chairmen, Dean, Vice President of Academic Affairs, Scientific 
        Council Sub Committee Chair and Vice President of Research and Innovation can also assign Deputies to work on their behalf
        after logging to the system.
        <br />
            <br />
            Click on the relevant help below.<br />
            <br />
        </div>
        <div style="padding-left: 15px">
            <ul>
                <li>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/help.aspx#1">Directions for Applicant</asp:HyperLink>
                </li>

                <li>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/help.aspx#2">Directions
        for Deparment Chairman</asp:HyperLink></li>

                <li>
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/help.aspx#3">Directions
        for Departmental Promotion Committee</asp:HyperLink>
                </li>

                <li>
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/help.aspx#4">
        Directions for the Dean of Faculty and Personnel Affairs</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/help.aspx#5">Directions
        for College Dean</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="~/help.aspx#6">Directions
        for Scientific Council Sub-committee</asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/help.aspx#7">Directions
        for Ad Hoc Promotion Committee Chairman </asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink17a" runat="server" NavigateUrl="~/help.aspx#7a">Directions
        for Ad Hoc Promotion Committee Members </asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="HyperLink18" runat="server" NavigateUrl="~/help.aspx#8">Directions
        for Vice President of Academic Affairs</asp:HyperLink>
                </li>
            </ul>
        </div>
        <p style="text-align: center">
            For further Questions and Clarifications please call 7545 for the webmaster of Faculty
            Promotion System during working hours&nbsp;
        </p>
        <p style="text-align: center">
            or
        </p>
        <p style="text-align: center">
            Email: <a href="mailto:facpromote@kfupm.edu.sa">facpromote@kfupm.edu.sa</a>.
        </p>
        <a name="1"></a>
        <h3>Directions for the Applicant</h3>
        <p>
            <span style="font-family: Times New Roman">The following is a list of items the applicant needs to consider for his promotion:
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">1- &nbsp; Please go through the <a href="FacProm-Handbook.pdf">KFUPM Faculty Promotion
                    Guidelines</a> and start a new Promotion Application through the online Faculty Promotion
                    System. A list of menu items like Message, Applicant, Points etc are shown on the
                    left side. Each Menu Item when clicked, opens a Form.The
                form which is required
                    to be filled must be saved inorder to complete the task. Once all the required forms are filled and saved it can be Forwarded to the Chairman
                of the department electronically or Saved using the Action menu
                    item.
                The applicant can fill different forms and
                modify his input at any stage, as long as the application has not been forwarded
                to the department chairman.
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">Also make sure the system displays all your relvant
                    information correctly.</span>
        </p>
        <p>
            <span style="font-family: Times New Roman">2- &nbsp; &nbsp; The required forms for the
                Applicant are as follows:</span>
        </p>
        <p>
            <span style="font-family: Times New Roman">&nbsp; &nbsp; &nbsp; a) <strong>Points</strong>:
                fill the
                eligibility points Calculation sheet </span>
        </p>
        <p>
            <span style="font-family: Times New Roman">&nbsp; &nbsp; &nbsp; b) <strong>Years of
                Service:</strong>
                Provide the Years of services in the current rank and in Saudi University</span>
        </p>
        <p>
            <span style="font-family: Times New Roman">&nbsp; &nbsp; &nbsp; c) <strong>External
                Reviewers:</strong>
                Provide a list of at least ten external referrers holding the rank of full professor
                (or equivalent ranks) with the information shown in the form.</span>
        </p>
        <p>
            <span style="font-family: Times New Roman">&nbsp; &nbsp; &nbsp; d) <strong>Attachments (Files)</strong>: Upload the following:
            </span>
        </p>

        <ul>
            <li>Your updated CV. (Make sure it is according to Faculty  Promotion Guidelines)</li>
            <li>List of publications. (A document having the list ) </li>
            <li>Publications. (Upload all publication for rapid process Or send the Hard copy of all the publications) </li>
            <li>Publications for External Reviewers. (Upload 5-7 publication for External Reviewers. Check the Checkbox to select for External Reviewers) </li>
        </ul>

        <p>
            &nbsp; &nbsp; &nbsp; You may include a file containing any additional comments to be part of your promotion application.
        </p>
        <p>
            <span style="font-family: Times New Roman">&nbsp; &nbsp; &nbsp; e) <strong>Action:</strong>
                Either you can Initiate and forward to the Department Chairman or Save for future
                modifications.</span>
        </p>
        <p>
            <span style="font-family: Times New Roman">3- &nbsp; Along with the above mentioned necessary forms, there are some optional form that can be viewed and filled by the
                applicant. These include: </span>
        </p>
        <p>
            <span style="font-family: Times New Roman">&nbsp; &nbsp; &nbsp; a) <strong>Committee Exclusion:</strong> If you wish any person to be excluded from the later Ad-hoc
                promotion committee, you express your desire by clicking on yes in this form.&nbsp;</span>
        </p>
        <p>
            <span style="font-family: Times New Roman"></span>
            <img alt="" src="images/App1.JPG" width="1410" height="735"/>
        </p>
        <p>
            <img alt="" src="images/App2.JPG" width="1402" height="606" />
        </p>
        <p>
            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/help.aspx#">Go To Top</asp:HyperLink>&nbsp;
        </p>
        <a name="2"></a>
        <h3>Directions for the Department Chairman</h3>


        <p>
            <span style="font-family: Times New Roman">The following is a list of items to be considered by the department chairman for promotion request of a faculty member in the department:
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman"></span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">1- &nbsp; &nbsp;The Department Chairman forms
                a Departmental Committee&nbsp; and forward the application to the Chairman of the
                Dept. Committee and Dean of Faculty and Personnel Affairs</span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">2- &nbsp; &nbsp;Upon recieving back the application,
                the Department Chairman is supposed to write its input and forward it to the College
                Dean.&nbsp;
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">3- &nbsp; &nbsp;The required forms for
            the Department Chairman are as follows: </span>
        </p>
        <p>
            <span style="font-family: Times New Roman">&nbsp; &nbsp; &nbsp; &nbsp; a) <strong>Dept.
                Committee:</strong>
                Form a departmental committee (or utilize the already appointed committee) to review the application for completeness, to calculate the points for the scholarly work of the applicant, and to propose a list of at least ten external referees holding the rank of full professor (or equivalent ranks).</span>
        </p>
        <p>
            <span style="font-family: Times New Roman">&nbsp; &nbsp; &nbsp; &nbsp; b) <strong>Chairman Input: </strong>
                Provide written input about the candidates’ teaching, research and services. There are fields to be used for such input. In addition, the recommendation for the promotion should be clearly stated.</span>
        </p>
        <p>
            <span style="font-family: Times New Roman">&nbsp; &nbsp; &nbsp; &nbsp; c) <strong>Action</strong>:
                Forward the
                application to the Chairman of the Dept. Committee and Dean of Faculty and Personnel
                Affairs.</span>
        </p>
        <p>
            <span style="font-family: Times New Roman">&nbsp; &nbsp; &nbsp; &nbsp; d) <strong>Action</strong>:
                On Return
                from both the Chairman of the Dept. Committee and Dean of Faculty and Personnel
                Affairs forward the application to the College Dean if approved. </span>
        </p>
        <p>
            <span style="font-family: Times New Roman"><span style="font-family: Times New Roman">&nbsp; &nbsp; &nbsp; &nbsp; e) <strong>Action</strong>: On Return from any one of the Chairman of the Dept.
                Committee or Dean of Faculty and Personnel Affairs, return the application to the
                Applicant if not approved.&nbsp;</span></span>
        </p>
        <p>
            <span style="font-family: Times New Roman"><span style="font-family: Times New Roman"></span></span>
            <span style="font-family: Times New Roman">4- &nbsp; &nbsp;Discuss the application in the department council upon receiving input from the departmental promotion committee and the Dean of faculty and personnel affairs indicating the completeness of the application and the eligibility of the applicant. 
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">5- &nbsp; &nbsp;Forward the application, if approved, to the Dean of the College with chairman’ evaluation and the approved council minutes No. and date.
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">6- &nbsp; &nbsp;Return the application, if not approved, to the applicant with necessary justifications or request any additional clarifications/information form the candidate at any stage. 
            </span>
        </p>

        <p style="text-align: justify">
            <span style="font-family: Times New Roman">7- &nbsp; &nbsp;Request verifications for time eligibility and the annual evaluation from the Dean of Faculty and Personnel Affairs. This is done by forwarding the application to the Dean of Faculty and Personnel Affairs. 
            </span>
        </p>
        <p>
            <span style="font-family: Times New Roman">The department chairman can do things simultaneously and fill different forms and modify his input at any stage, as long as the form has not been forwarded to the college dean or returned to the applicant. The data need to be saved before closing the session. The chairman can re-open the forms and modify them at different times as applicable. </span>
        </p>

        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/help.aspx#">Go To Top</asp:HyperLink>
        <a name="3"></a>
        <h3>Directions for the Dean of Faculty and Personnel Affairs</h3>
        <p>
            <span style="font-family: Times New Roman">The following is a list of items to be considered by the Dean of Faculty and Personal Affairs for promotion request of a faculty member in the University:
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">1- &nbsp; &nbsp;<strong>Years of Service:</strong>
                Provide the Years of services in the current rank. 
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">2- &nbsp; &nbsp;<strong>Years of Service:</strong>Provide the years of services at a Saudi university. 
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">3- &nbsp; &nbsp;<strong>Annual Evaluation:</strong>
                Provide the annual performance evaluation reports of the candidate for the period since the candidate's appointment or his last promotion. The information can be provided as hard copies or soft copy attachments.
            </span>
        </p>

        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/help.aspx#">Go To Top</asp:HyperLink>
        <a name="4"></a>
        <h3>Directions for the Departmental Promotion Committee</h3>
        <p>
            <span style="font-family: Times New Roman">The following is a list of items to be considered by the Chairman of the Department Promotion Committee for promotion request of a faculty member in the Department:
            </span>
        </p>

        <p style="text-align: justify">
            <span style="font-family: Times New Roman">1- &nbsp; &nbsp;Review the promotion dossier and make sure it is prepared according to the university promotion guidelines.
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">2- &nbsp; &nbsp;<strong>Points:</strong>In the Points Form fill the
                eligibility points Calculation sheet and compare it to the one provided by the applicant and report any deviation with proper justification.
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">3- &nbsp; &nbsp;<strong>External Reviewers:</strong>
                Provide a list of at least ten external referrers holding the rank of full professor (or equivalent ranks) with the information shown in the form.
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">4- &nbsp; &nbsp;<strong>Action:</strong>
                Return the Dossier with input and comments to the department chairman as soon as possible.
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">Note that the chairman of the committee can do things simultaneously and can fill different forms and modify the input at any stage, as long as the form has not been returned to the department chairman. 
The committee chairman can re-open the forms and modify (as applicable) them at different times as long as the application has not been sent back to the department chairman.
            </span>
        </p>

        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/help.aspx#">Go To Top</asp:HyperLink>
        <a name="5"></a>
        <h3>Directions for the College Dean</h3>
        <p>
            <span style="font-family: Times New Roman">The following is a list of items to be considered by the college dean for promotion request of a faculty member in the college:
            </span>
        </p>

        <p style="text-align: justify">
            <span style="font-family: Times New Roman">1- &nbsp; &nbsp;Discuss promotion application in the college council.
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">2- &nbsp; &nbsp;<strong>External Reviewers:
            </strong>Provide a list of at least ten external referrers holding the rank of full professor (or equivalent ranks) with the information shown in the form.
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">3- &nbsp; &nbsp;<strong>Action:</strong>
                Forward, if approved, the promotion application along with the approved council minutes No. and Date to the Vice President of Academic Affairs.
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">4- &nbsp; &nbsp;<strong>Suggested Promotion
                Committee:</strong> Provide a list of faculty members to serve in the Ad-hoc Promotion Committee.
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">5- &nbsp; &nbsp;Communicate verbally to the VRGSSR the names to be excluded from the Ad-hoc Promotion Committee. (If the candidate has mentioned any)
            </span>
        </p>

        <p style="text-align: justify">
            <span style="font-family: Times New Roman">6- &nbsp; &nbsp;<strong>Action:</strong>
                Return, if not approved, the promotion application along with approved minutes to the Department Chairman with justification notes.
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">The college dean can do things simultaneously and fill different forms and modify his input at any stage, as long as the form has not been forwarded to the VRAA or returned to the department chairman. The college dean can re-open the forms and modify them at different times as applicable and as long as the form has not been forwarded to the VRAA. </span>
        </p>
        <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/help.aspx#">Go To Top</asp:HyperLink>
        <br />
        <br />
        <a name="6"></a>
        <h3>Directions for the Scientific Council Sub-Committee</h3>
        <p>
            <span style="font-family: Times New Roman">The following is a list of items to be considered by the Sientific Council Sub-Committee for promotion request of a faculty member:
            </span>
        </p>

        <p style="text-align: justify">
            <span style="font-family: Times New Roman">1- &nbsp; &nbsp;Discuss promotion application.
                Following is a typical screen for a role of&nbsp; Scientific Council Subcommittee Member. The menu items in green show the mandatory forms need to be filled and saved.</span>
        </p>
        <p style="text-align: justify">
            <img alt="" class="auto-style5" src="images/Help%20Screenshots/SCSubCommittee1.JPG" width="1392" height="655" />
        </p>
        <p style="text-align: justify">
            2- Edit and verify the Eligibility Checklist. See the picture below&nbsp;
        </p>
        <p style="text-align: justify">
            <img alt="" class="auto-style6" src="images/Help%20Screenshots/SCSubCommittee2.JPG" width="1409" height="746" />
        </p>
        <p style="text-align: justify">
            &nbsp;
        </p>
        <p style="text-align: justify">
            &nbsp;3- Select a list of atleast 10 External Reviewers. However you can add more than 10 External Reviewers by either Selecting from the Applicant, Dep and College List or entering New entry&nbsp;
        </p>
        <p style="text-align: justify">
            <img alt="" src="images/Help%20Screenshots/SCSubCommittee4.JPG" width="623" height="495" />
        </p>
        <p style="text-align: justify">
            4 Go to The Action Menu Item to Finish the Task :
        </p>
        <p style="text-align: justify">
            
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">The SC Sub-Committee can do things simultaneously
                and fill different forms and modify his input at any stage, as long as the form
                has not been returned to the VRGSSR. The SC Sub-Committee can re-open the forms
                and modify them at different times as applicable and as long as the form has not
                been returnes to the VRGSSR. </span>
        </p>
        <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/help.aspx#">Go To Top</asp:HyperLink>



        <a name="7"></a>
        <h3>Directions for the Ad Hoc Promotion Committee Chairman</h3>
        <p>
            <span style="font-family: Times New Roman">The Ad Hoc Promotion Committee is formed by the VRGSSR with one chairman and 4 
            members. The VRGSSR seeks willingness of the volunteers. After accepting 
            the willingness to work as a promotion committee chairman, the chairman and all 
            the promotion committee members receive an acknowledgment email sent by the 
            VRGSSR through the online system.Now, the Promotion Committee chairman can log 
            on to the online system and can open the electronic promotion application.&nbsp; The following is a list of items to be considered by the Promotion Commitee (PC) Chairman for promotion request of a faculty member:
            </span>
        </p>

        <p style="text-align: justify">
            <span style="font-family: Times New Roman">1-&nbsp;&nbsp; &nbsp;Discuss promotion application with meeting with all the members 
            of the promotion committee
            </span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">2-&nbsp;&nbsp;&nbsp; Review the comments provided by the external reviewers
            </span>
        </p>

        <p style="text-align: justify">
            <span style="font-family: Times New Roman">2-&nbsp;&nbsp;&nbsp; Make a promotion commitee report and let it signed by all the 
            members&nbsp;</span>
        </p>
        <p style="text-align: justify">
            <span style="font-family: Times New Roman">The online System facilitates the PC Chair to perform all the above 
            mentioned tasks. Following are the screen shot of all the activities the PC 
            Chairman is supposed to perform after logging on to the system and opeening the promotion application
            </span>
        </p>


        <p style="text-align: justify">
            &nbsp;
        </p>
        <%--<p style="text-align: justify">
            <a href="images/Help%20Screenshots/PCChair6aScreen.htm">PCChair6aScreen.htm</a> 
            (This screen shot is long. click on the link to see the webpage)</p>--%>

        <p style="text-align: justify">
            <span style="font-family: Times New Roman">The Promotion Committee Chairman can do things simultaneously and fill different forms and modify his input at any stage, as long as the form has not been returned to the VRGSSR. The the PC Chairman can re-open the forms and
                modify them at different times as applicable and as long as the form has not been
                returned to the VRGSSR. </span>
        </p>
        <a name="7a"></a>
        <h3>Directions for the Ad Hoc Promotion Committee Members</h3>

        <a name="8"></a>
        The Promotion Committee (PC) Members can open the promotion application and post their busy schedule (after the request from the PC Chairman) and digitally sign the promtoion report uploaded by the PC Chair.<br />
        <br />
        
        

        <br />
        <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/help.aspx#">Go To Top</asp:HyperLink>
        &nbsp;<h3>Directions for Vice President of Academic Affairs (VPAA)</h3>
        <p>
            The Vice Rector for Academic Affairs is involved in the Faculty promotion system for Acknowledgement purpose only. The College dean forwards the promotion application to the 
            the VPAA who is required to forward it to the Vice President of Research and Innovation (VPRI). 
            However, Before forwarding it to the VPRI, the VPAA can view all the details of the promotion application in different forms accessible through the menu items 

        </p>
        <p>
            &nbsp;</p>
        <br />
        <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/help.aspx#">Go To Top</asp:HyperLink>
        <br />
        <br />
        <br />


        <table width="100%" style="border-top: 1px solid #3062af">
            <tr>
                <td align="center" style="width: 100%">
                    <span style="font-size: 9pt">Faculty Promotion System
                        <br />
                        Copyright © 2008 King Fahd University of Petroleum & Minerals | Dhahran, 31261 | Saudi Arabia | +966 (3) 860 0000.<br />

                    </span>
                </td>
            </tr>
        </table>

    </form>
</body>

