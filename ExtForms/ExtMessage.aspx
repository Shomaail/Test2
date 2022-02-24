<%@ Page Language="C#" MasterPageFile="~/ExtForms/ExtForm.master" AutoEventWireup="true" CodeFile="ExtMessage.aspx.cs" Inherits="ExtForms_ExtMessage" MaintainScrollPositionOnPostback="true"%>
<%@ MasterType VirtualPath="ExtForm.master" %>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h3><asp:Label ID="lblPageHeading" runat="server" Text=""></asp:Label></h3>
    <asp:TextBox ID="TextBoxMessage" runat="server" Height="408px" TextMode="MultiLine"
        Width="100%" CssClass=""></asp:TextBox>
</asp:Content>

