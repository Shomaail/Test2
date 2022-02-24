<%@ Page Language="C#" MasterPageFile="Forms.master" AutoEventWireup="true" CodeFile="Exclusion.aspx.cs" Inherits="Forms_Exclusion" %>

<%@ Register Src="../Controls/Exclusion.ascx" TagName="Exclusion" TagPrefix="uc2" %>

<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Instruction ID="Instruction1" runat="server" />
    <asp:Label ID="lblMessage" runat="server" EnableViewState="False" Font-Italic="True"></asp:Label><br />
    <div style="padding-left: 20px; padding-top: 15px">
    <uc2:Exclusion ID="Exclusion1" runat="server" />
    <p>
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save Choice" class="btn btn-primary btn-md"  /></p>
    </div>
    
</asp:Content>