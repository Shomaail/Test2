<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="FinalDecision.aspx.cs" Inherits="Forms_FinalDecision" Title="Faculty Promotion System" %>
<%@ MasterType VirtualPath="Forms.master" %>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc2" %>

<%@ Register Src="../Controls/FinalDecision.ascx" TagName="FinalDecision"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:Instruction ID="Instruction1" runat="server" />
    <br />
    <br />
    <asp:Label ID="lblMessage" runat="server"></asp:Label><br />
    <br />
    <uc1:FinalDecision ID="FinalDecision1" runat="server" />
    <br />
    <br />
    <asp:Button ID="ButtonSaveSC" runat="server" Text="Save" Width="145px" OnClick="ButtonSaveSC_Click" /><br />
</asp:Content>

