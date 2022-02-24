<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SummaryExport - Copy.aspx.cs" Inherits="SummaryExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Faculty Promotion System  Export to MS WORD</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:Table ID="TableSummary" runat="server" GridLines="Both" BackColor="Transparent" BorderColor="Transparent" HorizontalAlign="Center">
</asp:Table>
        <br />
        <br />
        <br />

        <asp:LinkButton ID="LinkButtonExport" runat="server" OnClick="LinkButtonExport_Click" ToolTip="Click here to Export this webpage into a Word Document">Export</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="LinkButtonClose" runat="server" OnClick="LinkButtonClose_Click" ToolTip="Click here to close this window">Close This Window</asp:LinkButton>
         </div>
    </form>
</body>
</html>
