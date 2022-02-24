<%@ Page Language="C#" AutoEventWireup="true" Inherits="Main"
    MaintainScrollPositionOnPostback="true" CodeFile="Main.aspx.cs" %>

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

    <script type="text/javascript">
        var confirmed = false;

        function ShowConfirm(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Are you sure you want to delete this Promotion Application? The operation will delete all data related to this application including all attachements uploaded, External Reviewers List, Tracking informaiton  etc", function (result) {
                if (result) {
                    if (controlID != null) {
                        var controlToClick = document.getElementById(controlID);
                        if (controlToClick != null) {
                            confirmed = true;
                            controlToClick.click();
                            confirmed = false;
                        }
                    }
                }

            });

            return false;

        }

        var confirmed = false;
        function ShowConfirmStartNewApp(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Do you want to start a new application?", function (result) {
                if (result) {
                    if (controlID != null) {
                        var controlToClick = document.getElementById(controlID);
                        if (controlToClick != null) {
                            confirmed = true;
                            controlToClick.click();
                            confirmed = false;
                        }
                    }
                }

            });
            return false;
        }
    </script>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css" />
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#tableArchivedApplications').DataTable({
                "pageLength": 100,
                "responsive": true
            });
        });
         $(document).ready(function () {
            $('#tableApplications').DataTable({
                "pageLength": 100,
                "responsive": true
            });
        });

    </script>
</head>
<body dir="ltr">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div class="loading-container">
                    <div class="sk-folding-cube">
                        <div class="sk-cube1 sk-cube"></div>
                        <div class="sk-cube2 sk-cube"></div>
                        <div class="sk-cube4 sk-cube"></div>
                        <div class="sk-cube3 sk-cube"></div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <header>
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
                                    <asp:LinkButton runat="server" OnClick="lbtnApplications_Click" class="nav-link" ID="lbtnApplications">Applications</asp:LinkButton></li>
                                <li>
                                    <asp:HyperLink runat="server" NavigateUrl="~/Admin/Default.aspx" class="nav-link" ID="lbtnAdmin"
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
                                            <asp:LinkButton runat="server" OnClick="lbtnMyDetails_Click" class="nav-link" ID="lbtnMyDetails">My Details</asp:LinkButton></li>
                                        <li><a href="../Documents/Guidelines.pdf" class="nav-link" target="_blank">Guidelines</a></li>
                                        <li><a href="../Documents/Announcements.pdf" class="nav-link" target="_blank">Announcements</a></li>
                                        <li><a href="../UserManual.html" class="nav-link" target="_blank">User Manual</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="Logout.aspx"><i class="fa fa-power-off" aria-hidden="true"></i>Logout</a></li>
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


        <div class="container">
            <div runat="server" id="divMyDetails" visible='<%# FormMode == FormModeMain.MyDetails.ToString() %>'>

                <div style="float: left">
                    <h3 style="border-bottom: 1px solid gray; padding-bottom: 2px">User Details:</h3>
                    <table class="table-condensed">
                        <tbody>
                            <tr class="DetailViewTableRow">
                                <td class="DetailViewTableHeader">Name:</td>
                                <td class="DetailViewTabelRow">
                                    <asp:Label ID="lblDeputy" runat="server" Text="Deputy Of " Font-Italic="True" Visible="False"></asp:Label>
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="DetailViewTableRowAlt">
                                <td class="DetailViewTableHeaderAlt">Email:</td>
                                <td class="DetailViewTabelRowDataAlt">
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="DetailViewTableRow">
                                <td class="DetailViewTableHeader">ID:</td>
                                <td class="DetailViewTabelRow">
                                    <asp:Label ID="lblEmpID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="DetailViewTableRowAlt">
                                <td class="DetailViewTableHeaderAlt">Rank :</td>
                                <td class="DetailViewTabelRowDataAlt">
                                    <asp:Label ID="lblRank" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="DetailViewTableRowAlt">
                                <td class="DetailViewTableHeaderAlt">College :</td>
                                <td class="DetailViewTabelRowDataAlt">
                                    <asp:Label ID="lblDept" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div runat="server" id="divDeputyDetails" style="float: right">
                    <h3 style="border-bottom: 1px solid gray; padding-bottom: 2px">Deputy Details:</h3>

                    <asp:GridView ID="gvDeputy" runat="server" AutoGenerateColumns="False" DataKeyNames="DepartmentID" ItemType="BL.Data.Department"
                        CssClass="table-condensed" OnRowEditing="gvDeputy_RowEditing" OnRowCancelingEdit="gvDeputy_RowCancelingEdit"
                        OnRowUpdating="gvDeputy_RowUpdating">
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#1e9966" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit" CssClass="btn btn-default">
                         Edit <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> 
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server" CausesValidation="True" CommandName="Update" CssClass="btn btn-default">
                          Update <span class="glyphicon glyphicon-ok" aria-hidden="true"></span> 

                                    </asp:LinkButton>
                                    <br />
                                    <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="False" CommandName="Cancel" ToolTip="Cancel" CssClass="btn btn-default">
                              Cancel <span class="glyphicon glyphicon-remove" aria-hidden="true"></span> 
                                    </asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deputy For">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmp" runat="server" Text='<%# Item.HeadString %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deputy 1">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeputy" runat="server" Text='<%# GetDeputy(Item.DeputyEmail)%>'></asp:Label><br />
                                    <asp:Label ID="lblDeputyEmail" runat="server" Text='<%# "Email: " + Item.DeputyEmail%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <input type="email" id="tbDeputyEdit" title="Email" placeholder="Email" runat="server" value='<%# Item.DeputyEmail%>'>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deputy 2">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeputy2" runat="server" Text='<%# GetDeputy(Item.Deputy2Email)%>'></asp:Label><br />
                                    <asp:Label ID="lblDeputy2Email" runat="server" Text='<%# "Email: " + Item.Deputy2Email%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <input type="email" id="tbDeputy2Edit" title="Email" placeholder="Email" runat="server" value='<%# Item.Deputy2Email%>'>
                                </EditItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div runat="server" id="divApplications" visible='<%# FormMode == FormModeMain.Applications.ToString() %>'>
                <h3 style="border-bottom: 1px solid gray; padding-bottom: 2px"><%= Resources.Resource.MainM1 %></h3>

                
                <div class="row">
                    <div class="col-lg-12">
                        <div id="appli-container-table">
                             <table id="tableApplications">
                             <thead>
                                        <tr>
                                            <td class="col-lg-12">
                                               <div runat="server" id="divCurrentAppMsg">
                    <p>
                        <span style="font-size: 10pt"><em><%= Resources.Resource.MainM2 %></em></span>
                    </p>
                </div>
                <div runat="server" id="divCurrentAppAbsentMsg">
                    <p>
                        <span style="font-size: 10pt"><em><%= Resources.Resource.MainM3 %></em></span>
                    </p>
                </div>
                                            </td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                <asp:Repeater ID="rpApplications" runat="server" ItemType="BL.Data.Application_Role" OnItemCommand="rpApplications_ItemCommand">
                                    <ItemTemplate>
                                         <tr>
                                                    <td class='<%# GetStatus(Item.ApplicationID,Item.RoleID) ? "open" : "closed" %>'>
                                        <asp:Label runat="server" ID="lblApplicationID" Text='<%# Item.ApplicationID %>' Visible="false"></asp:Label>
                                       
                                            <a href='<%# GetURL(Item.ApplicationID,Item.RoleID) %>'></a>
                                            <span class="title">
                                                <asp:Label ID="lblApplicant" runat="server" Text='<%# Item.Application.Employee.NameString %>'></asp:Label>
                                                <br>
                                                <span class="dept"><%# Item.Application.Employee.Rank %> - <%# Item.Application.Employee.Department1.NameString %></span>
                                            </span>
                                            <span class="dec">Applied for<br>
                                                <strong><%# Item.Application.ForRank %></strong>
                                            </span>
                                            <span class="status">
                                                <strong><%# GetStatus(Item.ApplicationID,Item.RoleID) ? "Active" : "Inaccessible " %></strong>
                                                <br>
                                                <strong><%# "Active Task ("+ GetNoOfActiveTask(Item.ApplicationID).ToString() + ")" %></strong>
                                            </span>

                                            <%-- <asp:LinkButton ID="lbtnDelete" runat="server" ToolTip="Delete" CssClass="btn btn-default" CommandName="Delete"
                                                Visible='<%#  GetAppTaskLogCount(Item.ApplicationID) == 1%>'
                                                OnClientClick="return ShowConfirm(this.id);">Delete <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                            </asp:LinkButton>--%>
                                                  
                                        </td></tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                                 </table>
                        </div>
                    </div>
                </div>
                <div style="padding-left: 0px; padding-top: 5px; padding-right: 20px">
                    <h3 style="border-bottom: 1px solid gray; padding-bottom: 2px"><%= Resources.Resource.MainM4 %></h3>
                    <%-- <div runat="server" id="divClosedAppMsg">
                        <p>
                            <span style="font-size: 10pt"><em><%= Resources.Resource.MainM5 %></em></span>
                        </p>
                    </div>
                    <div runat="server" id="divClosedAppAbsentMsg">
                        <p>
                            <span style="font-size: 10pt"><em><%= Resources.Resource.MainM6 %></em></span>
                        </p>
                    </div>--%>
                    <div class="row">
                        <div class="col-lg-12">
                            <div id="archived-appli-container-table">
                                <table id="tableArchivedApplications">
                                    <thead>
                                        <tr>
                                            <td class="col-lg-12">
                                                <div runat="server" id="divClosedAppMsg">
                                                    <p>
                                                        <span style="font-size: 10pt"><em><%= Resources.Resource.MainM5 %></em></span>
                                                    </p>
                                                </div>
                                                <div runat="server" id="divClosedAppAbsentMsg">
                                                    <p>
                                                        <span style="font-size: 10pt"><em><%= Resources.Resource.MainM6 %></em></span>
                                                    </p>
                                                </div>
                                            </td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rpClosedApplications" runat="server" ItemType="BL.Data.Application_Role" OnItemCommand="rpClosedApplications_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="archi">

                                                        <asp:Label runat="server" ID="lblApplicationID" Text='<%# Item.ApplicationID %>' Visible="false"></asp:Label>
                                                        <a href='<%# GetURL(Item.ApplicationID,Item.RoleID) %>'></a>

                                                        <span class="title">
                                                            <asp:Label ID="lblApplicant" runat="server" Text='<%# Item.Application.Employee.NameString %>'></asp:Label>
                                                            <br>
                                                            <span class="dept"><%# Item.Application.Employee.Rank %> - <%# Item.Application.Employee.Department1.NameString %></span>
                                                        </span>

                                                        <span class="dec">Applied for: 
                                                    <strong><%# Item.Application.ForRank %></strong>
                                                            <br />
                                                            Applied on: <strong><%# Item.Application.StartDate.HasValue?
                                                                                Item.Application.StartDate.Value.ToLongDateString() : ""%></strong><br>
                                                            Final Decision: <strong><%# Item.Application.FinalDeicision ? "Promoted" : "Declined"%></strong>
                                                        </span>

                                                        <%--                                                <span class="status">Your responsibility as: <strong><%# Item.Role.Title %></strong><br />
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" ToolTip="Delete" CssClass="btn btn-default" CommandName="Delete"
                                                        Visible='<%#  GetAppTaskLogCount(Item.ApplicationID) == 1%>'
                                                        OnClientClick="return ShowConfirm(this.id);">Delete <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                                    </asp:LinkButton>
                                                </span>--%>

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div style="padding-left: 0px; padding-top: 5px; padding-right: 20px" runat="server" id="divApply">
                        <h3 style="border-bottom: 1px solid gray; padding-bottom: 2px">New Promotion Application</h3>
                        You can start a new Application for Promotion to the Rank of&nbsp;<asp:Label ID="lblNextRank" runat="server" Font-Bold="True" Font-Italic="true"></asp:Label>
                        <br />
                        <br />

                        <asp:Button ID="btnApply" class="btn btn-primary btn-md" runat="server"
                            OnClientClick="return ShowConfirmStartNewApp(this.id);"
                            Text="Start Your Own Promotion Application" OnClick="btnApply_Click" />
                            <br />
                        <br />
                    </div>


                </div>
            </div>
        </div>
        <!-- Footer -->

        <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
        <asp:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
            TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
            BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
            RepositionMode="RepositionOnWindowScroll">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="display: none; width: 300px; padding: 10px">
            <asp:Panel runat="Server" ID="programmaticPopupDragHandle0" Style="cursor: move; color: #555; margin-bottom: 15px; border-bottom: 1px solid #ddd; padding-bottom: 5px; font-family: Arial; font-size: 17px; font-weight: bold;">
                Message
            </asp:Panel>
            <asp:Label ID="lblProgrammaticPopup0" runat="server"></asp:Label>
            <br />
            <div align="center">
                <asp:LinkButton runat="server" ID="hideModalPopupViaServer0"
                    OnClick="hideModalPopupViaServer0_Click"><span style="color: #fff;    background: #096;    display: block;    padding: 5px 20px;    font-family: Arial;
    font-size: 13px;    text-decoration: none;    margin-top: 10px;"> OK </span></asp:LinkButton>
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
