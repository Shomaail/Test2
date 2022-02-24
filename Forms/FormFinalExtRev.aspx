<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="FormFinalExtRev.aspx.cs" Inherits="Forms_FormFinalExtRev" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<%@ MasterType VirtualPath="Forms.master" %>

<%@ Register Src="../Controls/ComposeEmail.ascx" TagName="ComposeEmail" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
function basicPopup(appID,extRevID) 
{
    window.open ('../ExtForms/NoMasterPage/NMPForm_Evaluation.aspx?appID='+ appID + '&extRevID=' + extRevID ,null,'scrollbars=yes, status= no, resizable = yes, toolbar=no,location=no,height = 700, width = 1200, left = 200, top= 200, screenx=10,screeny=600,menubar=no');    
}
</script>
    <script type="text/javascript">
        var confirmed = false;

        function ShowConfirmWLStatusDelete(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Are you sure you want to delete the willingess status information of this External Reviewer?", function (result) {
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
        function ShowConfirmMSStatusDelete(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Are you sure you want to delete the Material Sent Status information of this External Reviewer?", function (result) {
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
        function ShowConfirmEvaluationDelete(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Are you sure you want to delete the Evaluation Records of this External Reviewer?", function (result) {
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
    <div id="divFormFinalExtRev" runat="server" visible='<%#FormMode == "View" %>' style='overflow: auto; width: 100%;'>
        <asp:HiddenField ID="hfExtRevID" runat="server" />
        <div id="divMaterialOKStatus" runat="server" visible='<%# Master.CurRoleID ==100 %>'>
            <%--(byte) RoleID.TopAuthority--%>
            <h3>Promotion Committee opinion about material (CV and Publications)</h3>
            <table class="table">
                <tr>
                    <td></td>
                    <td>Is Material OK?</td>
                    <td>Material OK Email</td>
                    <td>Material Not OK Email</td>
                    <td>Email from <%= ConfigurationManager.AppSettings["TopAuthority_TitleShort"] %></td>
                </tr>
                <tr>
                    <td>Promotion Committee Message for Material to be sent to External Reviewers: </td>
                    <td style="align-items: center">
                        <div runat="server" style="align-content: center" visible='<%# GetMaterialOKStatus() %>'>
                            <span class="text-success" title="Material suitable to be sent to external reviewers"><i class="fa fa-check-circle-o fa-2x" aria-hidden="true"></i></span>
                            <br />
                        </div>
                        <div runat="server" style="align-content: center" visible='<%# !GetMaterialOKStatus() %>'>
                            <span class="text-danger" title="Material not suitable to be sent to external reviewers"><i class="fa fa-exclamation -circle-o fa-2x" aria-hidden="true"></i></span>
                            <br />
                        </div>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnViewEmailFromPCMaterialOK" runat="server" CausesValidation="False"
                            Visible='<%# GetMaterialOKStatus() %>' OnClick="lbtnViewEmailFromPCMaterialOK_Click"
                            ToolTip="View Email sent from Promotion Committee saying Material is appropriate" CssClass="btn btn-default">
                            <span class="glyphicon glyphicon-sunglasses" aria-hidden="true"></span> 
                        </asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnViewEmailFromPCMaterialNotOK" runat="server"
                            Visible='<%# !GetMaterialOKStatus() %>' OnClick="lbtnViewEmailFromPCMaterialNotOK_Click" CausesValidation="False"
                            ToolTip="View Email sent from Promotion Committee saying Material is not appropriate" CssClass="btn btn-default">
                            <span class="glyphicon glyphicon-sunglasses" aria-hidden="true"></span> 
                        </asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnViewEmailFromTopAuthority" runat="server"
                            OnClick="lbtnViewEmailFromTopAuthority_Click" CausesValidation="False"
                            CssClass="btn btn-default">
                            <span class="glyphicon glyphicon-sunglasses" aria-hidden="true"></span> 
                        </asp:LinkButton><br />
                        <asp:Label ID="lblPCMessage" runat="server"></asp:Label></td>
                </tr>
            </table>
        </div>
        <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                visible='<%# !GetAppTaskFormCompleted()
                          && !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                <b>Incomplete: </b>
                <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
            <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                visible='<%# GetAppTaskFormCompleted()                         
                         &&!Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                <b>Complete:</b>
                <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
        <h3>Final External Reviewers </h3>
        <asp:GridView ID="gvFinalExtRev" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4"
            CssClass="table-condensed" DataKeyNames="ApplicationID,ExternalReviewerID" OnRowCommand="gvFinalExtRev_RowCommand" OnRowDataBound="gvFinalExtRev_RowDataBound"
            ItemType="BL.Data.Form_FinalExtRev"
            ForeColor="#333333" GridLines="None" EmptyDataText="No Final External Reviewer present">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
                    <HeaderTemplate>
                        <asp:LinkButton ID="lbtnRefresh" runat="server" CausesValidation="False" CommandName="Refresh" ToolTip="Refresh" CssClass="btn btn-default">
                            <span class="glyphicon glyphicon-refresh" aria-hidden="true"></span> 
                        </asp:LinkButton>
                    </HeaderTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDetails" runat="server" CausesValidation="False" CommandName="SelectDetail" ToolTip="Details" CssClass="btn btn-default" CommandArgument='<%# Item.ExternalReviewerID %>'>
                          <span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span> 
                        </asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ExternalReviewerID" HeaderText="ExternalReviewerID" SortExpression="ExternalReviewerID" Visible="false" />
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" ForeColor='<%# GetColor(Item.WLStatus)%>' Text='<%# Item.ExternalReviewer.NameString + (Item.ExternalReviewer.Status == RecordStatus.Active.ToString()? "": " (Record Inactive)") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Willingness Letter">
                    <ItemTemplate>
                        <div style="align-content: center; text-align: center;">
                            <div>
                                <asp:LinkButton ID="lbtnWillingnessLetter" runat="server"
                                    Enabled='<%# Item.WLStatus == WillingessStatus.Not_Sent.ToString().Replace("_"," ") 
                                        && !Item.Application.ApplicationClosed ? true : false 
                                        && Item.ExternalReviewer.Status == RecordStatus.Active.ToString()%>'
                                    class="btn btn-primary btn-md"
                                    CommandName="SendWL" CommandArgument='<%# Item.ExternalReviewerID %>'>
                                    <%# Item.WLStatus == WillingessStatus.Not_Sent.ToString().Replace("_"," ") ? "Send Email" : "Email Sent" %>
                                    <span class="glyphicon glyphicon-send" aria-hidden="true"></span> </asp:LinkButton>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Willingness Letter Status">
                    <ItemTemplate>
                        <div style='overflow: auto; width: 110px;'>
                            <asp:Label ID="lblWLStatus" runat="server" ForeColor='<%# GetColor(Item.WLStatus) %>' Text='<%# Item.WLStatus %>'></asp:Label>
                            <br />
                            <span>Reminders: <%# GetReminders(TaskExtID.Willingness_Letter_for_External_Reviewers) %></span>
                            <br />
                            <asp:Label ID="lblDateWL" runat="server" Text='<%# Item.WLDate %>'></asp:Label>
                            <br />
                            <asp:LinkButton ID="lbtnViewWLEmail" runat="server" CausesValidation="False" CommandArgument='<%# Item.ExternalReviewerID %>'
                                Visible='<%# Item.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_"," ")                                     
                                    ? true : false %>'
                                CommandName="ViewWLEmail" ToolTip="View Willingness Letter Email" CssClass="btn btn-default">
                            <span class="glyphicon glyphicon-sunglasses" aria-hidden="true"></span> 
                            </asp:LinkButton>
                            <asp:HyperLink ID="hlViewLink" runat="server" CausesValidation="False" CommandArgument='<%# Item.ExternalReviewerID %>'
                                Visible='<%# Item.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_"," ")
                                    && Master.CurRoleID == (int) RoleID.TopAuthority? true : false %>'
                                NavigateUrl='<%# "~/ExtForms/ShowWillingnessExtReviewer.aspx?appID="+Master.ApplicationID +"&erID=" + Item.ExternalReviewerID %>'
                                ToolTip="View Sent Willingess Letter" CssClass="btn btn-default" Target="_blank">
                            <span class="glyphicon glyphicon-link" aria-hidden="true"></span> 
                            </asp:HyperLink>
                            <asp:LinkButton ID="lbtnDeleteWLStatus" runat="server" CausesValidation="False" CommandArgument='<%# Item.ExternalReviewerID %>'
                                Visible='<%# Item.WLStatus != WillingessStatus.Not_Sent.ToString().Replace("_"," ") 
                                && Item.MaterialSentStatus !=  SendSelPubStatus.Material_Sent.ToString().Replace("_"," ") 
                                && Master.CurRoleID == (int) RoleID.TopAuthority
                                ? true : false %>'
                                Enabled='<%# !Item.Application.ApplicationClosed %>'
                                CommandName="DeleteWLStatus" ToolTip="Delete Williness Status" class="btn btn-default"
                                OnClientClick="return ShowConfirmWLStatusDelete(this.id);">
                            <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comments with Willingness" SortExpression="CommentsWithWL">
                    <ItemTemplate>
                        <div style='overflow: auto; height: 90px;'>
                            <asp:Label ID="lblCommentsWithWL" runat="server" Text='<%# Item.CommentsWithWL %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Send Selected Documents">
                    <ItemTemplate>
                        <div style="align-content: center; text-align: center;">
                            <div>
                                <asp:LinkButton ID="lbtnSendSelDoc" runat="server"
                                    Enabled='<%# Item.MaterialSentStatus != SendSelPubStatus.Material_Sent.ToString().Replace("_"," ")
                                        && Item.WLStatus == WillingessStatus.Accepted.ToString()
                                         //&& Item.Application.Form_MaterialFlag.PCMaterialReady4ExtRevFlag.HasValue? 
                                         //Item.Application.Form_MaterialFlag.PCMaterialReady4ExtRevFlag.Value:false ? true : false
                                         && Item.ExternalReviewer.Status == RecordStatus.Active.ToString()%>'
                                    class="btn btn-primary btn-md"
                                    
                                    CommandName="SendSelDoc" CommandArgument='<%# Item.ExternalReviewerID %>'>
                                    <%# Item.MaterialSentStatus !=  SendSelPubStatus.Material_Sent.ToString().Replace("_"," ") ? "Send Material" : "Material Sent" %>
                                    <span class="glyphicon glyphicon-send" aria-hidden="true"></span> </asp:LinkButton>

                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Material Sent Status">
                    <ItemTemplate>
                        <div style='overflow: auto;'>
                            <asp:Label ID="lblMaterialSentStatus" runat="server" Text='<%# Item.MaterialSentStatus %>' ForeColor='<%# GetColor(Item.MaterialSentStatus)%>'></asp:Label>
                            <br />
                            <span>Reminders: <%# GetReminders(TaskExtID.External_Evaluation) %></span>

                            <br />
                            <asp:Label ID="lblMaterialSentDate" runat="server" Text='<%# Item.MaterialSentDate %>'></asp:Label>
                            <br />
                            <asp:LinkButton ID="lbtnViewMSEmail" runat="server" CausesValidation="False" CommandArgument='<%# Item.ExternalReviewerID %>'
                                Visible='<%# Item.MaterialSentStatus != SendSelPubStatus.Material_Not_Sent.ToString().Replace("_"," ")                                     
                                    ? true : false %>'
                                CommandName="ViewMSEmail" ToolTip="View Material Sent Email" CssClass="btn btn-default">
                            <span class="glyphicon glyphicon-sunglasses" aria-hidden="true"></span> 
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtnDeleteMSStatus" runat="server" CausesValidation="False" CommandArgument='<%# Item.ExternalReviewerID %>'
                                Visible='<%# Item.MaterialSentStatus != SendSelPubStatus.Material_Not_Sent.ToString().Replace("_"," ") 
                                    && Item.EvaluationStatus == EvaluationStatus.Not_Submitted.ToString().Replace("_"," ") 
                                    ? true : false %>'
                                Enabled='<%# !Item.Application.ApplicationClosed %>'
                                CommandName="DeleteMSStatus" ToolTip="Delete Material Sent Status" class="btn btn-default"
                                OnClientClick='<%# !Item.Application.ApplicationClosed? "return ShowConfirmMSStatusDelete(this.id);":""%>'>
                            <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Evaluation Status">
                    <ItemTemplate>
                        <div style='overflow: auto;'>
                            <asp:Label ID="lblEvaluationStatus" runat="server" Text='<%# Item.EvaluationStatus %>' ForeColor='<%# GetColor(Item.EvaluationStatus)%>'></asp:Label>
                            <br />
                            <asp:Label ID="lblEvaluationDate" runat="server" Text='<%# Item.EvaluationDate %>'></asp:Label>
                            <br />
                            <asp:LinkButton ID="lbtnReturnEvaluation" runat="server" CausesValidation="False" CommandArgument='<%# Item.ExternalReviewerID %>'
                                Visible='<%# (Item.EvaluationStatus == EvaluationStatus.Submitted.ToString().Replace("_"," ")                                     
                                    ? true : false) && UserMode != "ViewByPC" %>'
                                Enabled='<%# !Item.Application.ApplicationClosed %>'
                                CommandName="ReturnEvaluation" ToolTip="Return Evaluation" class="btn btn-default">
                            <span class="glyphicon glyphicon-repeat" aria-hidden="true"></span> 
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtnDeleteEvaluation" runat="server" CausesValidation="False" CommandArgument='<%# Item.ExternalReviewerID %>'
                                Enabled='<%# !Item.Application.ApplicationClosed %>'
                                CommandName="DeleteEvaluation" ToolTip="Delete Evaluation" class="btn btn-default"
                                Visible='<%#  Item.EvaluationStatus != EvaluationStatus.Not_Submitted.ToString().Replace("_"," ") && UserMode != "ViewByPC" %>'
                                OnClientClick='<%# !Item.Application.ApplicationClosed ? "return ShowConfirmEvaluationDelete(this.id);" : "" %>'>
                            <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> 
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Evaluation">
                    <ItemTemplate>
                        <div style="align-content: center; text-align: center;">
                            <div>
                                <asp:LinkButton ID="btnShowForm" runat="server" class="btn btn-primary btn-md"
                                    Enabled='<%# Item.EvaluationStatus == EvaluationStatus.Submitted.ToString() ? true : false %>'                           
                                    OnClientClick='<%# "basicPopup("+Item.ApplicationID+","+Item.ExternalReviewerID +"); return false;"%>' >
                                    Show Evaluation  <span class="glyphicon glyphicon-sunglasses" aria-hidden="true"></span> 
                                </asp:LinkButton>                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comments">
                    <ItemTemplate>
                        <div style='overflow: auto; height: 90px;'>

                            <asp:Label ID="lblCommentsWithEval" runat="server" Text='<%# Item.CommentsWithEval %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Allow PC to see" Visible="false">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="cbShowExtRev2PC" runat="server"
                            Checked='<%#  GetcbShowExtRev2PCCheckStatus(Item.ApplicationID, Item.ExternalReviewerID) %>'
                            Visible="true"
                            AutoPostBack="True"
                            OnCheckedChanged="cbShowExtRev2PC_CheckedChanged"
                            ToolTip="Check to enable this External Reviewer to be viewed by Promotion Committee" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Allow PC to See Evaluation" Visible="false">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="cbShowEval2PC" runat="server"
                            Checked='<%#  GetcbShowEval2PCCheckStatus(Item.ApplicationID, Item.ExternalReviewerID) %>'
                            Visible="true"
                            AutoPostBack="True"
                            OnCheckedChanged="cbShowEval2PC_CheckedChanged"
                            ToolTip="Check to enable the Evaluation of this External Reviewer to be viewed by Promotion Committee" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
      
    </div>
    <div id="divViewDetail" runat="server" visible='<%#FormMode == "ExtRevDetail"%>'>
        <asp:DetailsView ID="dvExtReviewers" runat="server" Height="50px" Width="125px" CellPadding="4" class="table-condensed"
            DataSourceID="odsFormExtRevDet" EnableModelValidation="True" GridLines="None" AutoGenerateRows="False">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
            <EditRowStyle BackColor="#999999" />
            <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
            <Fields>
                <asp:BoundField DataField="ExternalReviewerID" HeaderText="ExternalReviewerID" SortExpression="ExternalReviewerID" Visible="false" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                <asp:BoundField DataField="MailingAddress" HeaderText="MailingAddress" SortExpression="MailingAddress" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="Major" HeaderText="Major" SortExpression="Major" />
                <asp:BoundField DataField="Specialty" HeaderText="Specialty" SortExpression="Specialty" />
                <asp:BoundField DataField="PhoneAndFax" HeaderText="PhoneAndFax" SortExpression="PhoneAndFax" />
                <asp:BoundField DataField="ActiveAreaOfResearch" HeaderText="ActiveAreaOfResearch" SortExpression="ActiveAreaOfResearch" />
                <asp:BoundField DataField="PrevAreaOfResearch" HeaderText="PrevAreaOfResearch" SortExpression="PrevAreaOfResearch" />
                <asp:BoundField DataField="Webpage" HeaderText="Webpage" SortExpression="Webpage" />
                <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" />
                <asp:BoundField DataField="TotalPublications" HeaderText="TotalPublications" SortExpression="TotalPublications" />
                <asp:BoundField DataField="NoOfJournals" HeaderText="NoOfJournals" SortExpression="NoOfJournals" />
                <asp:BoundField DataField="HIndex" HeaderText="HIndex" SortExpression="HIndex" />
                <asp:BoundField DataField="Citations" HeaderText="Citations" SortExpression="Citations" />
                <asp:BoundField DataField="PassportNo" HeaderText="PassportNo" SortExpression="PassportNo" />
                <asp:BoundField DataField="IBAN" HeaderText="IBAN" SortExpression="IBAN" />
            </Fields>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#e7f7ff" Font-Bold="True" ForeColor="#3a9fe1" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        </asp:DetailsView>
        <asp:ObjectDataSource ID="odsFormExtRevDet" runat="server" SelectMethod="GetExtRevByID" TypeName="ExtRevBAL" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:Parameter Name="ExternalReviewerID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Button ID="btnHideDetail" runat="server" Text="Return to List" OnClick="btnHideDetail_Click" CssClass="btn btn-primary btn-md" />
    </div>
    <div id="divSendWL" runat="server" visible='<%#FormMode == "SendWL" || FormMode == "SendSelDoc" ||FormMode == "ViewMaterialStatusEmail"%>'>
        <uc4:ComposeEmail ID="ComposeEmail1" runat="server" OnEmailNotSent="ComposeEmail_OnEmailNotSent" OnEmailSent="ComposeEmail_OnEmailSent" />
    </div>
    <br />
    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
    <ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
        TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
        BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
        RepositionMode="RepositionOnWindowScroll">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="background: #0088ce; padding: 10px">
        <fieldset style="margin: 5px; border: 2px solid #0088ce; height: 90%;">
            <legend style="font-style: italic; font-weight: bold; background-color: #0088ce; color: white">Message </legend>
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

