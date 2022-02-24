<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="ContactVRGSSR.aspx.cs" Inherits="Forms_ContactVRGSSR" Title="Faculty Promotion System" MaintainScrollPositionOnPostback="true"%>

<%--<%@ Register Src="../Controls/ComposeEmailOld.ascx" TagName="ComposeEmail" TagPrefix="uc1" %>--%>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <uc2:Instruction ID="Instruction1" runat="server" />
    <br />
    <asp:RadioButtonList ID="RadioButtonListPCChairtoVR" runat="server" Enabled="false"
        AutoPostBack="True" 
        onselectedindexchanged="RadioButtonListPCChairtoVR_SelectedIndexChanged">
        <asp:ListItem Value="1">Send Material to External Reviewers</asp:ListItem>
        <asp:ListItem Value="2">Update CV and Publications</asp:ListItem>
    </asp:RadioButtonList>
    <strong><span style="font-size: medium; color: #FF9933">
    <br />
    Please go to the Attachments Menu Item to perform the above actions 
    <br />
    </span></strong>
<%--    <uc1:ComposeEmail ID="ComposeEmail1" runat="server" OnEmailSent="ComposeEmail_OnEmailSent" OnEmailNotSent="ComposeEmail_OnEmailNotSent" />--%>
    <uc10:Alert ID="Alert1" runat="server" />
</asp:Content>

