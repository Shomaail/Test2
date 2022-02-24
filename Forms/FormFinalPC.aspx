<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="FormFinalPC.aspx.cs" Inherits="Forms_FormFinalPC" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc2" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ MasterType VirtualPath="Forms.master" %>

<%@ Register Src="../Controls/ComposeEmail.ascx" TagName="ComposeEmail" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        //function setUploadButtonState() {

        //    var maxFileSize = 4194304; // 4MB -> 4 * 1024 * 1024
        //    var fileUpload = $('#content_PromotionReport');

        //    if (fileUpload.val() == '') {
        //        return false;
        //    }
        //    else {
        //        if (fileUpload[0].files[0].size < maxFileSize) {
        //            $('#lbtnUpload_Click').prop('disabled', false);
        //            return true;
        //        } else {
        //            $('#lblMessagePromotionReport').text('File too big! Files less than 4 MB is acceptable')
        //            return false;
        //        }
        //    }
        //}
        var confirmed = false;

        function ShowConfirmWLStatusDelete(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Are you sure you want to delete the willingess status information?", function (result) {
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
        function ShowConfirmPCDelete(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Are you sure you want to delete the Chairman/Member from the Promotion Committee?", function (result) {
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
        function ShowConfirm(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Are you sure you want to delete?", function (result) {
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
        function ShowConfirmEraseDS(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Are you sure you want to delete all the digital Signatures along with their Timestamps?", function (result) {
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
     <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanelFileUpload" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnUpload" />
            <asp:PostBackTrigger ControlID="lbtnDigitalSign" />
            <asp:PostBackTrigger ControlID="lbtnEraseDS" />

        </Triggers>
        <ContentTemplate>   
   <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                    visible='<%#  !GetAppTaskFormCompleted() && Master.CurRoleID == (byte) RoleID.DepartmentChairman
             && !Master.App.ApplicationClosed%>'>
                    <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                    <b>Incomplete: </b>
                    <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                        Font-Italic="True"></asp:Label>
                </div>
                <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                    visible='<%#  GetAppTaskFormCompleted() && Master.CurRoleID == (byte) RoleID.DepartmentChairman 
            && !Master.App.ApplicationClosed%>'>
                    <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                    <b>Complete:</b>
                    <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                        Font-Italic="True"></asp:Label>
                </div>
            <div runat="server" id="divMsg" class="alert alert-warning" role="alert">
                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                        <b>NOTE: </b>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
            <%--style='overflow: auto;'--%>
    <div id="divFormFinalPC" runat="server" visible='<%#FormMode == "View" %>' >
        <asp:HiddenField ID="hfEmployeeID" runat="server" />
        <asp:HiddenField ID="hfExternalEmployeeID" runat="server" />
        <h3>Final Promotion Committee 
            <asp:LinkButton ID="lbtnSendEmailToPC" runat="server" CausesValidation="false"  Visible='<%# UserMode != "ViewByPC" %>'
                ToolTip="Send email to Promotion Committee inorder to acknowledge that the Applicant has updated CV and Publications"
                 class="btn btn-primary btn-md" OnClick="lbtnSendEmailToPC_Click">
                        Send Email to Promotion Committee <span class="glyphicon glyphicon-send" aria-hidden="true"></span>
            </asp:LinkButton>
        </h3>
        <uc2:Instruction ID="Instruction1" runat="server" Visible="false" />
        <asp:GridView ID="gvFinalPC" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" Width="100%"
            CssClass="table-condensed" DataKeyNames="ApplicationID,EmployeeID,ExternalEmployeeID" OnRowCommand="gvFinalPC_RowCommand" ItemType="BL.Data.Form_FinalPC"
            OnRowDeleted="gvFinalPC_RowDeleted"
            OnRowDataBound="gvFinalPC_RowDataBound"
            ForeColor="#333333" GridLines="Both" EmptyDataText="No Final Promtoion Committee present">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:LinkButton ID="lbtnRefresh" runat="server" CausesValidation="False" CommandName="Refresh" ToolTip="Refresh" CssClass="btn btn-default">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span> 
                        </asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDetails" runat="server" CausesValidation="false" CommandName="SelectDetail" ToolTip="Select Detail" CssClass="btn btn-default"
                            CommandArgument='<%# Eval("ExternalEmployeeID") +";"+ Eval("EmployeeID") %>'>
                        <span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="DeletePC" ToolTip="Delete" class="btn btn-default"
                            Visible='<%# UserMode != "ViewByPC" %>'
                            Enabled='<%# !Item.Application.ApplicationClosed %>'
                            CommandArgument='<%# Eval("ExternalEmployeeID") +";"+ Eval("EmployeeID") %>'
                            OnClientClick="return ShowConfirmPCDelete(this.id);">
                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" SortExpression="EmployeeID" Visible="false" />
                <asp:BoundField DataField="ExternalEmployeeID" HeaderText="ExternalEmployeeID" SortExpression="ExternalEmployeeID" Visible="false" />
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" ForeColor='<%# GetColor(Eval("WLStatus").ToString())%>' Text='<%# Item.ExternalEmployeeID == 0 ? Item.Employee.NameString : Item.ExternalEmployee.NameString%>'></asp:Label>
                        <br />
                        <asp:Label ID="lblPosition" runat="server" ForeColor='<%# GetColor(Eval("WLStatus").ToString())%>'
                            CssClass="label" Text='<%#((PCPosition)Item.Position).ToString().Replace("_"," ")%>'></asp:Label>
                        <br />
                        <asp:Label ID="lblDeptShortname" runat="server" ForeColor='<%# GetColor(Eval("WLStatus").ToString())%>'
                            CssClass="label" Text='<%# Item.ExternalEmployeeID == 0 ? Item.Employee.Department1.ShortName: Item.ExternalEmployee.Organization %>'
                            ToolTip= '<%# Item.ExternalEmployeeID == 0 ? Item.Employee.Department1.NameString: Item.ExternalEmployee.Organization %>'></asp:Label>                       
                        <br />
                        <asp:Label ID="lblEmail" runat="server" ForeColor='<%# GetColor(Eval("WLStatus").ToString())%>'
                            Text='<%# Item.ExternalEmployeeID == 0 ? "Email: " + Item.Employee.Email :"Email: " +Item.ExternalEmployee.Email%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Phone">
                    <ItemTemplate>
                        <asp:Label ID="lblPhone" runat="server" Text='<%# Item.ExternalEmployeeID == 0 ? Item.Employee.Phone : Item.ExternalEmployee.Phone%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Willingness Letter">
                    <ItemTemplate>
                        <div style="align-content: center; text-align: center;">
                            <div>
                                <asp:Button ID="btnWillingnessLetter" runat="server" Text='<%# Eval("WLStatus").ToString() == WillingessStatus.Not_Sent.ToString().ToString().Replace("_"," ") ? "Send Email" : "Email Sent" %>'
                                    Enabled='<%# Eval("WLStatus").ToString() == WillingessStatus.Not_Sent.ToString().Replace("_"," ") && !Item.Application.ApplicationClosed ?   true : false %>' class="btn btn-primary btn-md"
                                    CommandName="SendWL" CommandArgument='<%# Eval("ExternalEmployeeID") +";"+ Eval("EmployeeID") %>'></asp:Button>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Willingness Letter Status">
                    <ItemTemplate>
                        <div style='overflow: auto; width: 200px; height: 110px;'>
                            <asp:Label ID="lblWLStatus" runat="server" ForeColor='<%# GetColor(Item.WLStatus) %>' Text='<%# Item.WLStatus %>'></asp:Label>
                            <br />
                            <span>Reminders: <%# GetReminders(Item.Position, Item.EmployeeID, Item.ExternalEmployeeID) %></span>
                            <br />
                            <asp:Label ID="lblDateWL" runat="server" Text='<%# Item.WLDate %>'></asp:Label>
                            <br />
                            <asp:LinkButton ID="lbtnViewWLEmail" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ExternalEmployeeID") +";"+ Eval("EmployeeID") %>'
                                Visible='<%# Item.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_"," ")                                     
                                    ? true : false %>'
                                CommandName="ViewWLEmail" ToolTip="View Willingness Letter Email" CssClass="btn btn-default">
                            <span class="glyphicon glyphicon-sunglasses" aria-hidden="true"></span> 
                            </asp:LinkButton>
                            <asp:HyperLink ID="hlViewLink" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ExternalEmployeeID") +";"+ Eval("EmployeeID") %>'
                                Visible='<%# Item.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_"," ")  ? true : false %>'
                                NavigateUrl='<%# "~/ExtForms/ShowWillingnessPC.aspx?appID="+Master.ApplicationID +"&empID=" + Item.EmployeeID+"&eeID=" +  Item.ExternalEmployeeID%>'
                                ToolTip="View Sent Willingess Letter" CssClass="btn btn-default" Target="_blank">
                            <span class="glyphicon glyphicon-link" aria-hidden="true"></span> 
                            </asp:HyperLink>
                            <asp:LinkButton ID="lbtnDeleteWLStatus" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ExternalEmployeeID") +";"+ Eval("EmployeeID") %>'
                                Visible='<%# Item.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_"," ") ? true : false %>'
                                Enabled='<%# !Item.Application.ApplicationClosed %>'
                                CommandName="DeleteWLStatus" ToolTip="Delete Williness Status" class="btn btn-default"
                                OnClientClick="return ShowConfirmWLStatusDelete(this.id);">
                            <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="Willingness Letter Status">
                    <ItemTemplate>
                        <asp:Label ID="lblWLStatus" runat="server" ForeColor='<%# GetColor(Item.WLStatus) %>' Text='<%# Item.WLStatus.ToString().Replace("_"," ") %>'></asp:Label>
                        <asp:Label ID="lblDateWL" runat="server" Text='<%#Item.WLDate %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comments with Willingness Letter" SortExpression="CommentsWithWL">
                    <ItemTemplate>
                        <div style='overflow: auto; height: 75px;'>
                            <asp:Label ID="lblCommentsWithWL" runat="server" Text='<%# Item.CommentsWithWL%>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Comments with Williness">
                    <ItemTemplate>
                        <div style='overflow: auto; width: 200px; height: 90px;'>
                            <asp:Label ID="lblCommentsWithWL" runat="server" Text='<%# Item.CommentsWithWL %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
               
               <%-- <asp:TemplateField HeaderText="Organization">
                    <ItemTemplate>
                        <asp:Label ID="lblOrganization" runat="server" Text='<%# Item.ExternalEmployeeID == 0 ? "Internal" : Item.ExternalEmployee.Organization%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Digital Signature on Promotion Report" SortExpression="DigitalSignature">
                    <ItemTemplate>
                        <div style='overflow: auto; height: 100px;'>
                            <asp:Panel runat="server" HorizontalAlign="Center" Visible='<%# Item.DigitalSignature %>'>
                                <span class="text-success"><i class="fa fa-check-circle-o fa-3x" aria-hidden="true"></i></span>                                
                                <br />
                                <%# Item.ExternalEmployeeID == 0 ? Item.Employee.NameString.Split(' ').Last(): Item.ExternalEmployee.NameString.Split(' ').Last() %> Signed
                                <br />
                                
                                <%--<asp:Image ID="imgOK" runat="server" Height="31px" Width="25px" ImageUrl="~/images/Tick.jpg"></asp:Image>--%>
                                <asp:Label ID="lblDigitalSignature" runat="server" Text='<%#Item.DSDate.HasValue ? Item.DSDate.Value.ToLongDateString() +" "+ Item.DSDate.Value.ToShortTimeString() : "" %>'></asp:Label>
                            </asp:Panel>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Source of Selection" SortExpression="Source">
                    <ItemTemplate>
                        <div style='overflow: auto; height: 75px;'>
                            <asp:Label ID="lblSource" runat="server" Text='<%#Item.Source %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>



            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#057f43" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
        <%--        <asp:ObjectDataSource ID="odsFormFinalPC" runat="server" SelectMethod="GetForm_FinalPC" TypeName="BAL" >
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>--%>
    </div>
    <div id="divViewDetail" runat="server" visible='<%#FormMode == "PCMemberDetail"%>'>
        <asp:DetailsView ID="dvPCMemberInternal" runat="server" Height="50px" Width="125px" CellPadding="4" 
            class="table-condensed"
            EnableModelValidation="True" GridLines="None" AutoGenerateRows="False" 
            Visible='<%# ExternalEmployeeID == 0 %>'>
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
            <EditRowStyle BackColor="#999999" />
            <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
            <Fields>
                <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" SortExpression="EmployeeID" Visible="false" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                <asp:BoundField DataField="Email" HeaderText="EmailID" SortExpression="Email" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                <asp:BoundField DataField="POBox" HeaderText="POBox" SortExpression="POBox" />
            </Fields>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#e7f7ff" Font-Bold="True" ForeColor="#3a9fe1" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        </asp:DetailsView>
        <asp:DetailsView ID="dvPCMemberExternal" runat="server" Height="50px" Width="125px" CellPadding="4" class="table-condensed"
            EnableModelValidation="True" GridLines="None" AutoGenerateRows="False" Visible='<%# ExternalEmployeeID != 0 %>'>
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
            <EditRowStyle BackColor="#999999" />
            <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
            <Fields>
                <asp:BoundField DataField="ExternalEmployeeID" HeaderText="EmployeeID" SortExpression="EmployeeID" Visible="false" />
                <asp:BoundField DataField="NameString" HeaderText="Name" SortExpression="NameString" />
                <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                <asp:BoundField DataField="Organization" HeaderText="Organization" SortExpression="Organization" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="Email2" HeaderText="Second Email" SortExpression="Email2" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" />
                <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />

            </Fields>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#e7f7ff" Font-Bold="True" ForeColor="#3a9fe1" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        </asp:DetailsView>
        <%--        <asp:ObjectDataSource ID="odsPCMemberInternal" runat="server" SelectMethod="GetEmployeeByPK" TypeName="BAL" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter Name="EmployeeID" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsPCMemberExternal" runat="server" SelectMethod="GetExternalEmployeeByPK" TypeName="BAL" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter Name="ExternalEmployeeID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>--%>
        <asp:Button ID="btnHideDetail" runat="server" Text="Return to List" OnClick="btnHideDetail_Click" CssClass="btn btn-primary btn-md" />
    </div>

    <div id="divPCPromotionReport" runat="server" style='overflow: auto;'>
        <div id="divPCPromotionReportUpload" runat="server" visible='<%# Master.CurRoleID == (byte) RoleID.Promotion_Committee_Chairman%>'>
            <h3>Upload Promotion Committee Report</h3>

            <table class="table" border="1">
                <tr>
                    <td>
                        <%--<asp:CustomValidator ID="customValidatorUpload" runat="server" ErrorMessage="" ControlToValidate="PromotionReport" ClientValidationFunction="setUploadButtonState();" />--%>
                        <label class="control-label" for="PromotionReport">
                            <input id="PromotionReport" runat="server" accept=".pdf" type="file" title="Browse" />
                        </label>
                        <br />
                        <%--<asp:Label runat="server" ID="lblMessagePromotionReport"></asp:Label>--%>
                    </td>
                    <td>
                        <%--<asp:TextBox ID="tbPCReportDesc" runat="server"></asp:TextBox>--%>
                        <input type="text" id="tbPCReportDesc" runat="server" placeholder="Description" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnUpload" ToolTip="Upload" runat="server" OnClick="lbtnUpload_Click" 
                            CssClass="btn btn-default">Upload Promotion Report
                            <span class="glyphicon glyphicon-cloud-upload" aria-hidden="true"></span> </asp:LinkButton>
                    </td>
                </tr>
            </table>
            <asp:Label ID="Span" runat="server"></asp:Label>
            <asp:LinkButton ID="lbtnContactPCByPCChair" runat="server" CausesValidation="false"  Visible='<%# UserMode == "ViewByPC" %>'
                ToolTip="Send email to Promotion Committee to request to digitally Sign promotion report"
                 class="btn btn-primary btn-md" OnClick="lbtnContactPCByPCChair_Click">
                        Send Email to Promotion Committee <span class="glyphicon glyphicon-send" aria-hidden="true"></span>
            </asp:LinkButton>
           <%-- <a runat="server" id="aLink">Send Email to Promotion Committee</a>--%>
        </div>
        <h3>Promotion Committee Report</h3>
        <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" CellPadding="5" 
            class="table" ItemType="BL.Data.Form_AttachmentAd"
            DataKeyNames="Form_AttachmentAdID" ForeColor="Black" GridLines="Vertical" BorderColor="#0088ce"
            OnRowDeleting="gvAttachments_RowDeleting"
            OnRowDataBound="gvAttachments_RowDataBound"
            ShowFooter="True">
            <FooterStyle BackColor="#057f43"
                Font-Size="Larger" Font-Strikeout="False" />
            <RowStyle BackColor="#F7F6F3" ForeColor="Black" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="Black" />
            <PagerSettings Mode="NumericFirstLast" NextPageText="Page" />
            <PagerStyle BackColor="#1A1718" ForeColor="White" HorizontalAlign="Center"
                Font-Bold="True" Font-Italic="False" Font-Names="Copperplate Gothic Light"
                Font-Size="X-Large" />
            <HeaderStyle BackColor="#057f43" Font-Bold="True" ForeColor="White" Width="100px" />
            <AlternatingRowStyle BackColor="White" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblSerialNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" ControlStyle-Width="100px">

                    <ItemStyle Width="10px" />
                    <ItemTemplate>
                        <div id="divEditDelDownload" runat="server">
                            <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" CssClass="btn btn-default"
                                Visible='<%# Master.CurRoleID  == (byte) RoleID.Promotion_Committee_Chairman %>'
                                OnClientClick="return ShowConfirm(this.id);">
                        Delete <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                            </asp:LinkButton>

                            <br />
                            <asp:LinkButton ID="lbtnDownload" runat="server" CausesValidation="false"
                                Visible='<%# Item.DocumentName != "" %>' OnPreRender="lbtnDownload_PreRender"
                                CommandName="Select" ToolTip='<%# Item.DocumentName %>' CssClass="btn btn-default">
                       Open <span class="glyphicon glyphicon-download-alt" aria-hidden="true"></span> 
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                    <%-- <FooterTemplate>
                            <asp:UpdatePanel ID="UpdatePanelFileUpload2" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lbtnAdd" />
                                </Triggers>
                                <ContentTemplate>
                                    <div style="width: 100px">
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CausesValidation="False" CommandName="Add" ToolTip="Add" CssClass="btn btn-default" OnClick="lbtnAdd_Click">
                                            Add <span class="glyphicon glyphicon-plus" aria-hidden="true"></span> 
                                        </asp:LinkButton>
                                        <br />
                                        <input id="ApplicationAttachmentAddSingle" runat="server" type="file" title="Browse" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </FooterTemplate>--%>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document Type">
                    <ItemTemplate>
                        <asp:Label ID="lblAttachmentCategory" runat="server" Text="Promotion Committee Report"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" ToolTip='<%# Item.Description %>' Text='<%# Item.Description %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                [<em>No Promotion Committee Report is uploaded for this application</em>]
            </EmptyDataTemplate>
        </asp:GridView>

        <asp:LinkButton ID="lbtnDigitalSign" ToolTip="Digitally Sign the Uploaded Report " runat="server" OnClick="lbtnDigitalSign_Click" CssClass="btn btn-default">
             Digitally Sign the above Promotion Committee Report
            <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span> </asp:LinkButton>
        <asp:LinkButton ID="lbtnEraseDS" ToolTip="Erase All Digital Signatures" runat="server" OnClick="lbtnEraseDS_Click" CssClass="btn btn-default"
            OnClientClick="return ShowConfirmEraseDS(this.id);">
             Erase All Digital Signatures
            <span class="glyphicon glyphicon-erase" aria-hidden="true"></span> </asp:LinkButton>
    </div>

    <div id="divSendWL" runat="server" visible='<%#FormMode == "SendWL" || FormMode == "SendEmailMaterialUpdated" || FormMode == "SendEmailRequestToDigitalSign"%>'>
        <uc4:ComposeEmail ID="ComposeEmail1" runat="server" OnEmailNotSent="ComposeEmail_OnEmailNotSent" OnEmailSent="ComposeEmail_OnEmailSent" />
    </div>
    <div id="divPCSuggestedByCollege" runat="server">
        <h3>Suggested Promotion Committee By College</h3>
        <asp:GridView ID="gvPCSuggestedByCollege" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataSourceID="" ForeColor="#333333" CssClass="table-condensed"
            GridLines="None" OnSelectedIndexChanged="gvPCSuggestedByCollege_SelectedIndexChanged" 
            CaptionAlign="Right" DataKeyNames="ApplicationID,Position"
            EmptyDataText="No Suggested Promtoion Committee from college is present" ItemType="BL.Data.Form_PromotionCommittee"
            OnDataBound="gvPCSuggestedByCollege_DataBound" Font-Bold="False">

            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlAsWhat" runat="server">
                            <asp:ListItem Value="Chairman">As Chairman</asp:ListItem>
                            <asp:ListItem Value="Member_Specialty_Area">As Member in Specialty Area</asp:ListItem>
                            <asp:ListItem Value="Member_Related_Area">As member in Related Area</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnSelect" runat="server" CausesValidation="False" class="btn btn-default"
                            Enabled='<%# !Item.Application.ApplicationClosed %>'
                            CommandName="Select">Select <span class="glyphicon glyphicon-pushpin" aria-hidden="true"></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate><%# Item.ExternalEmployee.ExternalEmployeeID == 0 ? Item.Employee.NameString :Item.ExternalEmployee.NameString %> </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rank">
                    <ItemTemplate><%# Item.ExternalEmployee.ExternalEmployeeID == 0 ? Item.Employee.Rank :Item.ExternalEmployee.Rank %> </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department">
                    <ItemTemplate><%# Item.ExternalEmployee.ExternalEmployeeID == 0 ? Item.Employee.Department1.ShortName :Item.ExternalEmployee.Department%> </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate><%# Item.ExternalEmployee.ExternalEmployeeID == 0 ? Item.Employee.Email :Item.ExternalEmployee.Email%> </ItemTemplate>
                </asp:TemplateField>
<%--                <asp:TemplateField HeaderText="Major">
                    <ItemTemplate><%# Item.ExternalEmployee.ExternalEmployeeID == 0 ? Item.Employee.Major :Item.ExternalEmployee.Major%> </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Phone">
                    <ItemTemplate><%# Item.ExternalEmployee.ExternalEmployeeID == 0 ? Item.Employee.Phone :Item.ExternalEmployee.Phone%> </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Organization">
                    <ItemTemplate><%# Item.ExternalEmployee.ExternalEmployeeID == 0 ? "Internal":Item.ExternalEmployee.Organization%> </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
    </div>
    <br />
    <div id="divNewSelection" runat="server">
        <h3>New Selection for Promotion Committee</h3>
        <table cellpadding="3" cellspacing="0" border="1" bordercolor="#3062af" style="border-collapse: collapse;" class="table-condensed">
            <tr>
                <th class="PointsHeader"></th>
                <th class="PointsHeader"></th>
                <th class="PointsHeader">Email</th>
                <th class="PointsHeader">Name</th>
                <th class="PointsHeader">Rank</th>
                <th class="PointsHeader">Department</th>
                <th class="PointsHeader">Organization</th>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="lbtnSelect" runat="server" CausesValidation="False" class="btn btn-default" OnClick="lbtnSelect_Click">Select <span class="glyphicon glyphicon-pushpin" aria-hidden="true"></span></asp:LinkButton>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNewSelection" runat="server">
                        <asp:ListItem Value="Chairman">As Chairman</asp:ListItem>
                        <asp:ListItem Value="Member_Specialty_Area">As Member in Specialty Area</asp:ListItem>
                        <asp:ListItem Value="Member_Related_Area">As member in Related Area</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    <asp:TextBox ID="tbNewSelectionPCEmail" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbNewSelectionPCEmail_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnInfo1" runat="server" Text="Info" AutoPostBack="True" OnClick="tbNewSelectionPCEmail_TextChanged" /><br />
                    <asp:Label ID="lblError1" runat="server" ForeColor="red" />

                </td>
                <td>
                    <asp:Label ID="lblName1" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblRank1" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblDepartment1" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblOrganization1" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
            </tr>
        </table>
        <br />
        <br />
    </div>

             </ContentTemplate></asp:UpdatePanel>
             <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
            <ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
                TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
                BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
                RepositionMode="RepositionOnWindowScroll">
            </ajaxtoolkit:ModalPopupExtender>
            <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="background: #007d40; padding: 10px">
                <fieldset style="margin: 5px; border: 2px solid #007d40; height: 90%;">
                    <legend style="font-style: italic; font-weight: bold; background-color: #007d40; color: white">Message </legend>
                    <br />
                    <div style="justify-content: center">
                        <asp:Label ForeColor="White" ID="labelProgrammaticPopup0" Font-Size="Large" runat="server"></asp:Label>
                    </div>
                    <div align="center">
                        <br />
                        <asp:LinkButton ID="hideModalPopupViaServer0" runat="server" OnClick="hideModalPopupViaServer0_Click" class="btn btn-default"
                            ToolTip="Press this button to send the email">OK <span class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
                    </div>
                </fieldset>
            </asp:Panel>
</asp:Content>

