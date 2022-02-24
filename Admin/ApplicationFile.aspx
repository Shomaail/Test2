<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ApplicationFile.aspx.cs" Inherits="Admin_ApplicationFile" Title="Faculty Promotion System" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p><strong>Application File:</strong></p>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" BorderColor="Black" BorderStyle="Solid"
        BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote"
        Width="100%" InteractiveDeviceInfos="(Collection)" 
    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <ServerReport ReportPath="/Promotion Reports2/PromotionFile" 
            ReportServerUrl="http://fedoraweb2/ReportServer" />
    </rsweb:ReportViewer>
  
    

    
</asp:Content>

