<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="Checklist.aspx.cs" Inherits="Forms_Checklist" Title="Faculty Promotion System" %>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Instruction ID="Instruction1" runat="server" />
    <asp:Label ID="lblMessage" runat="server" EnableViewState="False" Font-Italic="True"></asp:Label><br />
    put here<br />
</asp:Content>
