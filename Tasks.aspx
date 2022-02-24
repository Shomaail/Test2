<%@ Page Language="C#" AutoEventWireup="true" Inherits="Tasks" CodeFile="Tasks.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <title><%= ConfigurationManager.AppSettings["System_Title"] %></title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../images/favicon.png">

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="../Scripts/jquery.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="../App_Themes/custom_b.css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css" />
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.js"></script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $('#gvApplicationTracking').DataTable({
        //        "pageLength": 100,
        //        "responsive": true
        //    });
        //});
        //$(document).ready(function () {
        //    $('#tableApplications').DataTable({
        //        "pageLength": 100,
        //        "responsive": true
        //    });
        //});
    </script>
    <script>
//function myFunction(id) {
//  var copyText = document.getElementById(id);
//  copyText.select();
//  document.execCommand("Copy");  
        //}
        window.onscroll = function () { scrollFunction() };
        function scrollFunction() {
            if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
                document.getElementById("bttBtn").style.display = "block";
            } else {
                document.getElementById("bttBtn").style.display = "none";
            }
        }
        // When the user clicks on the button, scroll to the top of the document
        function topFunction() {
            // document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
        }
</script>
       


</head>
<body dir="ltr">

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <header id="top">
            <div class="site-nav">
                <nav class="navbar navbar-inverse navbar-static-top">
                    <div class="container-fluid">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar3">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <a class="navbar-brand" href='<%= ConfigurationManager.AppSettings["OrganizationURL"] %>' target="_blank">
                                <img src="../images/org-logo-wide.png" alt='<%= ConfigurationManager.AppSettings["OrganizationURL"] %>' width="190">
                            </a>
                        </div>
                        <div id="navbar3" class="navbar-collapse collapse">
                            <ul class="nav navbar-nav">

                                <li runat="server" id="liApp">
                                    <asp:HyperLink runat="server" NavigateUrl="~/Main.aspx" class="nav-link" ID="lbtnApplications">Applications</asp:HyperLink></li>
                                <li>
                                    <asp:HyperLink runat="server" NavigateUrl="~/Admin/Default.aspx" class="nav-link" 
                                        ID="lbtnAdmin" 
                                        Visible='<%#(Session["IsAdmin"] != null) && ((bool)Session["IsAdmin"]) ||
                                             (Session["IsCollegeAdmin"] != null) && ((bool)Session["IsCollegeAdmin"]) %>'>Admin</asp:HyperLink></li>
                                <li>
                                    <asp:LinkButton runat="server" class="nav-link" ID="lbtnSearchEmp" 
                                       Visible='<%#(Session["IsAdmin"] != null) && ((bool)Session["IsAdmin"]) ||
                                             (Session["IsCollegeAdmin"] != null) && ((bool)Session["IsCollegeAdmin"]) %>'>Employee Search</asp:LinkButton></li>

                            </ul>

                            <ul class="nav navbar-nav navbar-right">
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><i class="fa fa-user-circle" aria-hidden="true"></i>
                                        <asp:Label runat="server" ID="lblUserName"></asp:Label>
                                        <span class="caret"></span></a>
                                    <ul class="dropdown-menu">
                                        <li runat="server" id="liMyDetails">
                                            <asp:LinkButton runat="server" PostBackUrl="~/Main.aspx?MyDetails=1" class="nav-link" ID="lbtnMyDetails">My Details</asp:LinkButton></li>
                                        <li><a href="../Documents/Guidelines.pdf" class="nav-link" target="_blank">Guidelines</a></li>
                                        <li><a href="../Documents/Announcements.pdf" class="nav-link" target="_blank">Announcements</a></li>
                                        <li><a href="../UserManual.html" class="nav-link" target="_blank">User Manual</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="../Logout.aspx"><i class="fa fa-power-off" aria-hidden="true"></i> Logout</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                        <!--/.nav-collapse -->
                    </div>
                    <!--/.container-fluid -->
                </nav>
            </div>
        </header>
        <div class="container-fluid" id="ctl00_promodetails" runat="server">
            <ul>
                <li class="emp-details">
                    <span class="title">
                        <asp:Label runat="server" ID="lblApplicantName"></asp:Label></span><br>
                    <span class="dpt">
                        <asp:Label runat="server" ID="lblApplicantRank"></asp:Label>
                        -
                        <asp:Label runat="server" ID="lblApplicantDept"></asp:Label></span>
                </li>
                <li class="desig-details">
                    <span class="appfor">Applied for: <strong>
                        <asp:Label runat="server" ID="lblForRank"></asp:Label></strong></span><br>
                    <span class="appdt">Applied on: <strong>
                        <asp:Label runat="server" ID="lblAppStartDate"></asp:Label></strong></span><br>
                </li>
                <li class="status">
                    <span class="appdt">Your Role: <strong>
                        <asp:Label ID="lblDeputy" runat="server" Font-Italic="True" Text="Deputy Of "
                            Visible='<%# (bool)Session["IsDeputy"] %>'></asp:Label>
                        <%--<asp:Label ID="lblRole" runat="server" Text='<%# GetRole(byte.Parse(Request.QueryString["RoleID"].Replace("ae54sdafaas5313qw", "").Replace("f98poj64lsks98336hujm9811336fsdf", ""))) %>'></asp:Label>--%>
                          <asp:Label ID="lblRole" runat="server" Text='<%# GetRole(CurRoleID) %>'></asp:Label>

                    </strong></span>
                                        <br>
   <%--                 <span class="appdt" >Your Task: <strong>
                        <asp:Label ID="lblTask" runat="server" ></asp:Label>
                    </strong></span>
                    <br>
                    <span class="appdt" >Status: <strong><asp:Label runat="server" ID="lblStatus"></asp:Label></strong></span>--%>
                </li>
            </ul>
        </div>
        <a href="#top" id="bttBtn" title="Go to top" style="display: block;">Top</a>
        <div style="padding-left: 15px; padding-top: 5px; padding-right: 20px">
            <h3>Active Tasks of Application:</h3>
            <asp:GridView ID="gvActiveTasks" runat="server" CellPadding="4" ForeColor="Black" class="table" ItemType="BL.Data.Application_TaskLog"
                GridLines="Vertical" BorderColor="#719ade" AutoGenerateColumns="False">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#F7F6F3" ForeColor="Black" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="Black" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#1e9966" ForeColor="#ffffff" />
                <AlternatingRowStyle BackColor="White" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Applicant">
                        <ItemTemplate>
                            <asp:Label ID="lblApplicant" runat="server" Text='<%# Item.Application.Employee.NameString%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applicant's Email" SortExpression="AppEmail">
                        <ItemTemplate>
                            <asp:Label Font-Names="Verdana,Arial" Font-Size="10pt"
                                ID="lblEmail" runat="server" Text='<%# Item.Application.Employee.Email  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Assigned To">
                        <ItemTemplate>
                            <strong>
                                <asp:Label ID="lblAssignedTo" runat="server" Text='<%# 
                                    leri.Contains(Item.Task.RoleID)? "-" :
                                    Item.Task.Role.Application_Role
                                    .Where(ar=>ar.ApplicationID == Item.ApplicationID)
                                    .Select(ar=>ar.Employee).First().EmployeeID != "0" ? 
                                     Item.Task.Role.Application_Role
                                    .Where(ar=>ar.ApplicationID == Item.ApplicationID)
                                    .Select(ar=>ar.Employee).First().NameString :
                                    Item.Task.Role.Application_Role
                                    .Where(ar=>ar.ApplicationID == Item.ApplicationID)
                                    .Select(ar=>ar.ExternalEmployee).First().NameString%>'></asp:Label></strong>
                            <br />
                            <asp:Label ID="lblRole" runat="server" Text='<%# Item.Task.Role.Title %>'></asp:Label>
                            <asp:Label ID="lblActingRole" runat="server" Visible='<%# Item.Task.Role.Application_Role
                                    .Where(ar=>ar.ApplicationID == Item.ApplicationID && ar.IsActing == "Acting").Count() != 0%>' Text="(Acting)"></asp:Label>
                            <br />
                            <asp:Label Font-Names="Verdana,Arial" Font-Size="10pt"
                                ID="lblEmail" runat="server" Text='<%# 
                                leri.Contains(Item.Task.RoleID)? "-" :
                                Item.Task.Role.Application_Role.Where(ar=>ar.ApplicationID == Item.ApplicationID).Select(ar=>ar.Employee).First().EmployeeID != "0" ?
                                Item.Task.Role.Application_Role.Where(ar=>ar.ApplicationID == Item.ApplicationID).Select(ar=>ar.Employee).First().Email 
                              : Item.Task.Role.Application_Role.Where(ar=>ar.ApplicationID == Item.ApplicationID).Select(ar=>ar.ExternalEmployee).First().Email %>'></asp:Label>
                            <%--<button  onclick="myFunction('gvActiveTasks_lblEmail_0')"><span class="glyphicon glyphicon-copy" aria-hidden="true"></span></button>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Email" SortExpression="Email">
                        <ItemTemplate>
                            <asp:Label Font-Names="Verdana,Arial" Font-Size="10pt"
                                ID="lblEmail" runat="server" Text='<%# 
                                Item.Task.Role.Application_Role.Where(ar=>ar.ApplicationID == Item.ApplicationID).Select(ar=>ar.Employee).First().EmployeeID != "0" ?
                                Item.Task.Role.Application_Role.Where(ar=>ar.ApplicationID == Item.ApplicationID).Select(ar=>ar.Employee).First().Email 
                              : Item.Task.Role.Application_Role.Where(ar=>ar.ApplicationID == Item.ApplicationID).Select(ar=>ar.ExternalEmployee).First().Email %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--                 <asp:TemplateField HeaderText="Role">
                        <ItemTemplate>
                            <asp:Label ID="lblRole" runat="server" Text='<%# Item.Task.Role.Title %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Task Name">
                        <ItemTemplate>
                            <asp:Label Font-Names="Verdana,Arial" Font-Size="10pt"
                                ID="lblTask" runat="server" Text='<%# Item.Task.Title
                                    .Replace("@@TopAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopAuthority_TitleShort"])
                                    .Replace("@@TopLowAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"])
                                    .Replace("@@TopMostAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"])
                                    %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ApplicationID" HeaderText="Status">
                        <ItemTemplate>
                           <%-- <asp:Image ID="Image1" runat="server" Height="48px"
                                ImageUrl="~/images/noEntry.jpg" Width="73px"
                                Visible='<%#  Item.Application.ApplicationClosed ? true : false %>'
                                ToolTip="The Promotion Application has been Closed" />--%>
                            <asp:HyperLink ID="lnkApplication" runat="server"
                                Text="Open" NavigateUrl='<%# "Forms/Message.aspx?applicationID=" + Item.ApplicationID.ToString()+"&roleID="+CurRoleID %>'
                                Enabled='<%# CheckTask(Item.ApplicationID.ToString(), Item.Task.Role.Application_Role
                                            .Where(ar=>ar.ApplicationID == Item.ApplicationID)
                                            .Select(ar=>ar.Employee)
                                            .First().EmployeeID,
                                             Item.Task.Role.Application_Role
                                            .Where(ar=>ar.ApplicationID == Item.ApplicationID)
                                            .Select(ar=>ar. ExternalEmployee)
                                            .First().ExternalEmployeeID,
                                            Item.Task.Role.RoleID.ToString())  %>'
                                Visible='<%# CheckTask(Item.Task.Role.Application_Role
    .Where(ar=>ar.ApplicationID == Item.ApplicationID)
    .Select(ar=>ar.Employee).First().EmployeeID,    
    Item.Task.Role.Application_Role
    .Where(ar=>ar.ApplicationID == Item.ApplicationID)
    .Select(ar=>ar.ExternalEmployee).First().ExternalEmployeeID,
    Item.Task.Role.RoleID.ToString()) %>'
                                Font-Bold="True" ForeColor="#00CF00"></asp:HyperLink>
                            <asp:Label ID="Label1" runat="server" Font-Italic="True"
                                Text='<%# GetStatus(Item.ApplicationID.ToString()) %>'
                                Visible='<%# !CheckTask(Item.Task.Role.Application_Role
    .Where(ar=>ar.ApplicationID == Item.ApplicationID)
    .Select(ar=>ar.Employee).First().EmployeeID,    
    Item.Task.Role.Application_Role
    .Where(ar=>ar.ApplicationID == Item.ApplicationID)
    .Select(ar=>ar.ExternalEmployee).First().ExternalEmployeeID, Item.Task.Role.RoleID.ToString()) %>'
                                ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblReminders" runat="server" ToolTip="Autogenerated Email Reminders Sent"
                                Text='<%# "Reminders: " + Item.Reminders.ToString() %>'
                                Visible='<%# Item.Reminders > 0 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    [<em>Currently no active tasks are available for this application</em>]
                </EmptyDataTemplate>
            </asp:GridView>
           
        
            <br />
            <asp:Label ID="lblConfi" runat="server" Text="The application is in confidential state. Therefore no active tasks will be visible" Visible="False" ForeColor="Blue"></asp:Label>
            <br />
            <div runat="server" id="divAppTracking"><%-- visible='<%# 
            CurRoleID == (byte) RoleID.Applicant
                 ||CurRoleID == (byte) RoleID.College_Dean
                 ||CurRoleID == (byte) RoleID.TopAuthority
                 %>'>--%>
             <h3>Application Tracking:</h3>
            <p>
                <asp:GridView ID="gvApplicationTracking" runat="server" CellPadding="4" ForeColor="Black" class="table"
                    BorderColor="#719ade" AutoGenerateColumns="False" DataKeyNames="LogID" ItemType="BL.Data.Application_Log" >
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#1e9966" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#333333" />
                     <Columns>
                        <asp:TemplateField HeaderText="Task" SortExpression="Task">
                            <ItemTemplate>
                                <asp:Label Wrap="False" Font-Bold="True" Font-Size="10pt"
                                    ID="lblTask" runat="server" Text='<%# Item.ActionID == null? 
                                        TaskExtID.External_Evaluation.ToString().Replace("_"," ") : Item.WFAction.Task.Title
                                        .Replace("@@TopAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopAuthority_TitleShort"])
                                        .Replace("@@TopLowAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"])
                                        .Replace("@@TopMostAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"])%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Person" SortExpression="Role">
                            <ItemStyle Wrap="False" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" 
                                    Text='<%# leri.Count > 0 && leri.Contains(Item.ActionID == null ?  (byte) RoleID.External_Reviewer: Item.WFAction.Task.RoleID)? "-" : Item.Actor %>'></asp:Label><br />
                                <%--<span style="font-size: 10pt; color: Maroon"><em>(<asp:Label ID="Label2" runat="server" 
                                    Text='<%# Item.ActionID == null ? "External Reviewer" : GetRoleFromApplicationRole(Item.WFAction.Task.RoleID) + GetIsActingFromApplicationRole(Item.WFAction.Task.RoleID)%>'></asp:Label>)</em></span>--%>
                                <span style="font-size: 10pt; color: Maroon"><em>(<asp:Label ID="Label2" runat="server" 
                                    Text='<%# Item.ActionID == null ? "External Reviewer" :Item.WFAction.Task.Role.Title + Item.Comments%>'></asp:Label>)</em></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" SortExpression="Action">
                            <ItemStyle Wrap="False" />
                            <ItemTemplate>
                                <asp:Label ID="lblAction" runat="server" Font-Bold="true" Font-Italic="true" 
                                    Font-Names="Arial,Verdana" Font-Size="10pt" 
                                    Text='<%# Item.ActionID == null? Item.Details :Item.WFAction.Title.Replace("@@TopMostAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"])
                .Replace("@@TopAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopAuthority_TitleShort"])
                .Replace("@@TopLowAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"]) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ActionTime">
                            <ItemStyle Wrap="False" Font-Names="Verdana,Arial" Font-Size="10pt"/>
                            <ItemTemplate>
                               <asp:label runat="server" 
                                   Font-Names="Verdana,Arial" Font-Size="10pt"
                                   Text='<%# Item.ActionTime.HasValue? Item.ActionTime.Value.ToString("MMMM dd, yyyy - HH:mm"):"" %>' ></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        [<em>No further tasks have been done after the initiation</em>]
                    </EmptyDataTemplate>
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>
            </p>
        </div>
        </div>
       
        <br />
        <br />
        <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" />
        <asp:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
            TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
            BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
            RepositionMode="RepositionOnWindowScroll">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="display: none; width: 300px; padding: 10px">
            <asp:Panel runat="Server" ID="programmaticPopupDragHandle0" Style="cursor: move; background-color: #DDDDDD; border: solid 1px Gray; color: Black; text-align: center;">
                Message
            </asp:Panel>
            <asp:Label ID="labelProgrammaticPopup0" runat="server"></asp:Label>
            <br />
            <div align="center">
                <asp:LinkButton runat="server" ID="hideModalPopupViaServer0" Text="OK" OnClick="hideModalPopupViaServer0_Click" />
            </div>
        </asp:Panel>
        <footer class="footer">
            <div class="container-fluid">
                <span class="small"><strong><%= ConfigurationManager.AppSettings["System_Title"] %></strong> © <%= ConfigurationManager.AppSettings["CopyRightYear"] %> <%= ConfigurationManager.AppSettings["OrganizationName"] %> </span>
            </div>
        </footer>
    </form>
</body>
</html>
