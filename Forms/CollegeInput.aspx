<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true"
    Inherits="Forms_CollegeInput" MaintainScrollPositionOnPostback="true" CodeFile="CollegeInput.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ MasterType VirtualPath="Forms.master" %>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="tbMeetingDate" />          --%>
        </Triggers>
        <ContentTemplate>
            <%--<div id="divCollegeInput" runat="server"
                class='<%# (Master.CurRoleID == (byte) RoleID.College_Dean
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority)
            && !Master.App.ApplicationClosed? "": "disabledbutton" %>'>--%>
                <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                    visible='<%#  !GetAppTaskFormCompleted() && Master.CurRoleID == (byte) RoleID.College_Dean
             && !Master.App.ApplicationClosed%>'>
                    <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                    <b>Incomplete: </b>
                    <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                        Font-Italic="True"></asp:Label>
                </div>
                <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                    visible='<%#  GetAppTaskFormCompleted() && Master.CurRoleID == (byte) RoleID.College_Dean 
            && !Master.App.ApplicationClosed%>'>
                    <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                    <b>Complete:</b>
                    <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                        Font-Italic="True"></asp:Label>
                </div>

                <uc1:Instruction ID="Instruction1" runat="server" />
                <h3><%=Resources.Resource.Form_CollegeInputF1%></h3>
                <%=Resources.Resource.Form_CollegeInputF1SubHeading %>
                <asp:TextBox ID="tbF1" runat="server" Font-Names="Georgia,Times" Height="600px"
                    TextMode="MultiLine" Width="100%" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_CollegeInputF20%></h3>
                <%=Resources.Resource.Form_CollegeInputF20SubHeading %>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:TextBox ID="tbMeetingNo" runat="server" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <%--</div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <h3><%=Resources.Resource.Form_CollegeInputF21%></h3>
    <%=Resources.Resource.Form_CollegeInputF21SubHeading %>
    <asp:TextBox ID="tbMeetingDate" runat="server" TextMode="Date" AutoPostBack="true" OnTextChanged="tb_TextChanged" OnPaste="false" Visible='<%# (Master.CurRoleID == (byte) RoleID.College_Dean
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority)
            && !Master.App.ApplicationClosed%>'></asp:TextBox>
    <asp:Label ID="lblMeetingDate" runat="server" Visible='<%# !((Master.CurRoleID == (byte) RoleID.College_Dean
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority)
            && !Master.App.ApplicationClosed)%>'></asp:Label>    
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
    <br />
</asp:Content>
