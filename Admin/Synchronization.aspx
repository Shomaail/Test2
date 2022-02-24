<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Synchronization.aspx.cs" Inherits="Admin_Synchronization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="ButtonSynchronizeHRfromERP" runat="server" 
        Text="Synchronize HR from ERP" onclick="ButtonSynchronizeHRfromERP_Click" 
        Enabled="true" />
    <br />
    <br />
    <asp:Button ID="ButtonSynchronizeFPSDBfromERP" runat="server" Text="Synchronize FPSDB from ERP" onclick="ButtonSynchronizeFPSDBfromERP_Click" />
    <br />
    <br />
</asp:Content>

