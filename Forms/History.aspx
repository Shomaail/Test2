<%@ Page Language="C#" MasterPageFile="Forms.master" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="Forms_History" Title="Faculty Promotion System" %>

<%@ Register Src="../Controls/History.ascx" TagName="History" TagPrefix="uc2" %>

<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <uc1:Instruction ID="Instruction1" runat="server" />
    <br />
    <br />
    <br />
    <uc2:History id="History1" runat="server">
    </uc2:History>
</asp:Content>
