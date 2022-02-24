<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="ApplicatioRolesOld.aspx.cs" Inherits="Forms_ApplicatioRoles" %>

<%@ Register src="../Controls/AppRoles.ascx" tagname="AppRoles" tagprefix="uc1" %>

<%@ Register src="../Controls/Instruction.ascx" tagname="Instruction" tagprefix="uc3" %>
<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc3:Instruction ID="Instruction1" runat="server" />
    <br />
    <uc1:AppRoles ID="AppRoles1" runat="server" />
</asp:Content>

