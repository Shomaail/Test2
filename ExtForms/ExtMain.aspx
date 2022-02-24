<%@ Page Language="C#" AutoEventWireup="true" Inherits="ExtForms_ExtMain" CodeFile="ExtMain.aspx.cs" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
    <title><%=ConfigurationManager.AppSettings["System_Title"] %></title>
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
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="../App_Themes/custom_b.css" rel="stylesheet">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/4.4.0/bootbox.min.js"></script>
    <%-- <script>
    $(document).ready(function()
    {
      setTimeout(DoPostBack, 60000); // call DoPostBack in 10 minutes
    })
    function DoPostBack()
    {
        //var buttonClientId = '<%= btnSave.ClientID %>';
        //__doPostBack(buttonClientId, ''); // simulate a button click
        __doPostBack('btnSave', ''); // simulate a button click        
        //PageMethods.TestWebMethod();
    }
  </script>--%>
</head>
<body>
  
    <form id="form1" runat="server">
          <asp:ScriptManager ID="scriptmanager1" runat="server">
        </asp:ScriptManager>
        <header>
            <div class="site-nav">
                <nav class="navbar navbar-inverse navbar-static-top">
                    <div class="container-fluid">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar">
                                <span class="sr-only">Toggle navigation</span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <a class="navbar-brand" href='<%= ConfigurationManager.AppSettings["OrganizationURL"] %>' target="_blank">
                                <img src="../images/org-logo-wide.png" alt='<%= ConfigurationManager.AppSettings["OrganizationURL"] %>' width="190">
                            </a>
                        </div>
                        <div id="navbar" class="navbar-collapse collapse">
                            <ul class="nav navbar-nav">
                                <li runat="server" id="liApp">
                                    <asp:LinkButton runat="server" OnClick="lbtnApplications_Click" class="nav-link" ID="lbtnApplications">Applications</asp:LinkButton></li>
                            </ul>
                            <ul class="nav navbar-nav navbar-right">
                                <li runat="server" id="liMyDetails">
                                    <asp:LinkButton runat="server" OnClick="lbtnMyDetails_Click" class="nav-link" ID="lbtnMyDetails">Details for Honoraraium</asp:LinkButton>
                                </li>
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><i class="fa fa-user-circle" aria-hidden="true"></i>
                                        <asp:Label runat="server" ID="lblUserName"></asp:Label>
                                        <span class="caret"></span></a>
                                    <ul class="dropdown-menu">
                                        <li><a href="../Logout.aspx"><i class="fa fa-power-off" aria-hidden="true"></i>Logout</a></li>
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
            <div id="divMyDetails" visible='<%# FormMode == FormModeMain.MyDetails.ToString() %>' runat="server" style="padding-left: 15px; padding-top: 5px; padding-right: 20px">
                <asp:Button ID="btnSaveTop" runat="server" Text="Save" meta:resourceKey="btnSave" class="btn-primary" OnClick="btnSave_Click" />
                <table>
                    <tr>
                        <td><span><%=Resources.Resource.Name%> </span></td>
                        <td>
                            <input runat="server" id="taName" type="text" class="form-control" required="required"
                                maxlength="100"
                                style="width: 600px;" /></td>
                    </tr>
                    <tr>
                        <td><span><%=Resources.Resource.Address%> </span>
                            <br />
                            <small>(Where would you like to receive check)</small></td>
                        <td>
                            <textarea runat="server" id="taAddress" type="text" class="form-control" required="required"
                                maxlength="250"
                                style="width: 600px; height: 200px"></textarea></td>
                    </tr>
                    <tr>
                        <td><span><%=Resources.Resource.PassportNo%></span></td>
                        <td>
                            <input runat="server" id="taPassportNo" type="text" class="form-control"
                                maxlength="50" required="required"
                                style="width: 150px;" />
                        </td>
                    </tr>
                    <tr><td>Scanned copy of Passport </td>
                        <td>
                            <table>
                                <tr>
                                    <td><span class="glyphicon glyphicon-paperclip">Passport Attachment</span> </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnPassportAttachment" runat="server" OnClick="lbtnPassportAttachment_Click"></asp:LinkButton></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnDelPassportAttachment"
                                            OnClick="btnDelPassportAttachment_Click" Text="Delete" CssClass="btn"></asp:Button></td>
                                </tr>
                                <tr>
                                    <td>
                                        <input type="file" id="PassportAttachment" runat="server" /></td>
                                </tr>
                                </table>
                     </td>
                    <tr>
                        <td><span>Bank Details</span></td>
                        <td>
                            <textarea runat="server" id="taBank" type="text" class="form-control" required="required"
                                maxlength="250" style="width: 600px; height: 200px" /></td>
                    </tr>

                <tr>
                    <td><span><%=Resources.Resource.Website%> </span></td>
                    <td>
                        <textarea runat="server" id="taWebsite" class="form-control" type="text"
                            maxlength="500"
                            style="width: 600px;"></textarea></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.Email%> </span></td>
                    <td>
                        <input runat="server" id="taEmail" type="email" class="form-control" readonly
                            style="width: 600px;" /></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.Rank%> </span></td>
                    <td>
                        <textarea runat="server" id="taRank" type="text" class="form-control"
                            maxlength="100"
                            style="width: 600px;"></textarea></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.Description%> </span></td>
                    <td>
                        <input runat="server" id="taDescription" type="text" class="form-control"
                            maxlength="100"
                            style="width: 600px;" /></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.Major%> </span></td>
                    <td>
                        <textarea runat="server" id="taMajor" type="text" class="form-control" required="required" maxlength="500" style="width: 600px;"></textarea></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.Specialty%> </span></td>
                    <td>
                        <textarea runat="server" id="taSpecialty" type="text" class="form-control" required="required" maxlength="500" style="width: 600px;"></textarea></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.ActiveAreaOfResearch%> </span></td>
                    <td>
                        <textarea runat="server" id="taActiveAreaOfResearch" type="text" class="form-control" required="required"
                            maxlength="1000"
                            style="width: 600px;"></textarea></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.PreviousAreaOfResearch%> </span></td>
                    <td>
                        <textarea runat="server" id="taPreviousAreaOfResearch" type="text" class="form-control"
                            maxlength="500"
                            style="width: 600px;"></textarea></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.PhoneNFax%> </span></td>
                    <td>
                        <textarea runat="server" id="taPhoneNFax" type="tel" class="form-control"
                            maxlength="500"
                            style="width: 600px;"></textarea></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.TotalPublications%></span></td>
                    <td>
                        <input runat="server" id="taTotalPublications" class="form-control" type="number" min="0" style="width: 600px;" /></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.NoOfJournals%></span></td>
                    <td>
                        <input runat="server" id="taNoOfJournals" class="form-control" type="number" min="0" max="100000" style="width: 600px;" /></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.HIndex%></span></td>
                    <td>
                        <input runat="server" id="taHIndex" class="form-control" type="number" min="0" max="1000" style="width: 600px;" /></td>
                </tr>
                <tr>
                    <td><span><%=Resources.Resource.Citations%></span></td>
                    <td>
                        <input runat="server" id="taCitations" class="form-control" type="number" min="0" max="100000" style="width: 600px;" /></td>
                </tr>
                </table>
                <asp:Button ID="btnSave" runat="server" Text="Save" meta:resourceKey="btnSave" class="btn-primary" OnClick="btnSave_Click" />

            </div>
            <div id="divApplications" runat="server" visible='<%# FormMode == FormModeMain.Applications.ToString() %>'>

                <div id="divCurrentEvaluations" runat="server" style="padding-left: 15px; padding-top: 5px; padding-right: 20px">
                    <h3>Applications For Promotion Under Review</h3>
                    <div runat="server" id="divCurrentEvalMsg">
                        <p>
                            <span style="font-size: 10pt"><em>The following lists the application/s that you are
                assigned to evaluate.</em></span>
                        </p>
                    </div>
                    <div runat="server" id="divCurrentEvalAbsentMsg">
                        <p>
                            <span style="font-size: 10pt"><em>There are no promotion application(s) you are assigned to evaluate.</em></span>
                        </p>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div id="appli-container">
                                <ul>

                                    <asp:Repeater ID="rpCurrentEvaluation" runat="server"
                                        ItemType="BL.Data.Form_FinalExtRev">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblApplicationID" Text='<%# Item.ApplicationID %>' Visible="false"></asp:Label>
                                            <li class="open">
                                                <a href='<%# GetURL(Item.ApplicationID) %>'></a>
                                                <%-- <asp:LinkButton ID="lbtnCurrentApplicantions" runat="server" OnClick="lbtn_Click"></asp:LinkButton>--%>
                                                <span class="title">
                                                    <%# Item.Application.Employee.NameString%>
                                                    <br />
                                                    <span class="dept"><%# Item.Application.Employee.Rank %> - <%# Item.Application.Employee.Department %></span>
                                                </span>
                                                <%-- <span class="dec"></span>--%>
                                                <span class="dec">Appliying for the Rank:
                                                    <br />
                                                    <strong><%# Item.Application.ForRank%></strong><br />
                                                </span>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divSubmittedEvaluations" runat="server" style="padding-left: 15px; padding-top: 5px; padding-right: 20px">
                    <h3>Submitted Reviews</h3>
                    <div runat="server" id="divSubmittedEvalMsg">
                        <p>
                            <span style="font-size: 10pt"><em>The following is the list of evaluations that you have submitted.</em></span>
                        </p>
                    </div>
                    <div runat="server" id="divSubmittedEvalAbsentMsg">
                        <p>
                            <span style="font-size: 10pt"><em>There are no submitted evalautions.</em></span>
                        </p>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div id="appli-container">
                                <ul>
                                    <asp:Repeater ID="rpSubmittedEvaluation" runat="server"
                                        ItemType="BL.Data.Form_FinalExtRev">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblApplicationID" Text='<%# Item.ApplicationID %>' Visible="false"></asp:Label>
                                            <li class="open">
                                                <a href='<%# GetURL(Item.ApplicationID) %>'></a>
                                                <span class="title">
                                                    <%# Item.Application.Employee.NameString%>
                                                    <br />
                                                    <span class="dept"><%# Item.Application.Employee.Rank %>
                                                        - 
                                                        <%# Item.Application.Employee.Department %></span><br />
                                                    <span class="dept">Appliying for the Rank of: <strong><%# Item.Application.ForRank%></strong></span>
                                                </span>
                                                <span class="dec">Evaluation Date: <strong><%# Item.EvaluationDate.HasValue ? Item.EvaluationDate.Value.ToString("MMMM dd, yyyy"):""%></strong><br />
                                                    <br />
                                                    <br />
                                                    <%-- Reviewer’s Recommendation: <strong><%# Item.PromotionRecom != null? bool.Parse(Item.PromotionRecom.Value.ToString()) ? "Recommended" : "Not Recommended" : ""%></strong> <br />
                                                    Score: <strong><%# Item.Score %></strong><br /> --%>                                                   
                                                </span>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>

                </div>
                <div id="divWithdrawnEvaluations" runat="server" style="padding-left: 15px; padding-top: 5px; padding-right: 20px">
                    <h3>Applications For Promotion Withdrawn</h3>
                    <div runat="server" id="divWithdrawnEvalMsg">
                        <p>
                            <span style="font-size: 10pt"><em>The following is the list of evaluations that you have withdrawn.</em></span>
                        </p>
                    </div>
                    <div runat="server" id="divWithdrawnEvalAbsentMsg">
                        <p>
                            <span style="font-size: 10pt"><em>There are no withdrawn evaluations.</em></span>
                        </p>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div id="appli-container">
                                <ul>
                                    <asp:Repeater ID="rpWithdrawnEvaluations" runat="server"
                                        ItemType="BL.Data.Form_FinalExtRev">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblApplicationID" Text='<%# Item.ApplicationID %>' Visible="false"></asp:Label>
                                            <li class="open">
                                                <a href='<%# GetURL(Item.ApplicationID) %>'></a>

                                                <span class="title">
                                                    <%# Item.Application.Employee.NameString%>
                                                    <br />
                                                    <span class="dept"><%# Item.Application.Employee.Rank %>
                                                        <br />
                                                        <%# Item.Application.Employee.Department %></span>
                                                </span>
                                                <span class="dec">Appliying for the Rank:
                                                   
                                                    <strong><%# Item.Application.ForRank%></strong><br />
                                                </span>Withdrawal Date: <strong><%# Item.EvaluationDate%></strong>
                                                <br />
                                                <br />
                                                <%-- <span class="status">Promotion Recommendation: <strong><%# Item.PromotionRecom != null? bool.Parse(Item.PromotionRecom.Value.ToString()) ? "Positive" : "Negative" : ""%></strong><br />
                                                    Score: <strong><%# Item.Score %></strong>
                                                </span></li>--%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <footer class="footer">
            <div class="container-fluid">
                <span class="small"><strong><%= ConfigurationManager.AppSettings["System_Title"] %></strong> © <%= ConfigurationManager.AppSettings["CopyRightYear"] %> <%= ConfigurationManager.AppSettings["OrganizationName"] %> </span>
            </div>
        </footer>
         <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
    <asp:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
        TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
        BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
        RepositionMode="RepositionOnWindowScroll">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="background: #009966; padding: 10px">
        <fieldset style="margin: 5px; border: 2px solid #009966; height: 90%;">
            <legend style="font-style: italic; font-weight: bold; background-color: #009966; color: white">Message </legend>
            <br />
            <asp:Label ForeColor="White" ID="labelProgrammaticPopup0" Font-Size="Large" runat="server"></asp:Label>
            <div align="center">
                <br />
                <asp:LinkButton ID="hideModalPopupViaServer0" runat="server" OnClick="hideModalPopupViaServer0_Click" class="btn btn-default"
                    ToolTip="Press this button to send the email">OK <span class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
            </div>
        </fieldset>
    </asp:Panel>
    </form>
</body>
</html>
