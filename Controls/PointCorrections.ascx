<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PointCorrections.ascx.cs" Inherits="Controls_PointCorrections" %>
<h3>Points Corrections</h3>
<table style="border-collapse:collapse;" border="1px" bordercolor="#3062af">
<tr>
    <th class="PointsHeader">Field Address</th><th class="PointsHeader">From</th><th class="PointsHeader">To</th><th class="PointsHeader">Justification</th>
</tr>
<tr style="color:Green; font-style:italic; text-align:center">
    <td><b>Example:</b> col a row 2</td><td>3</td><td>2</td><td>The document already counted before</td>
</tr>
<tr>
    <td>
        <asp:TextBox ID="txtR1Address" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR1From" Width="50px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR1To" Width="50px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR1Justification" Width="500px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
</tr>
<tr>
    <td>
        <asp:TextBox ID="txtR2Address" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR2From" Width="50px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR2To" Width="50px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR2Justification" Width="500px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
</tr>

<tr>
    <td>
        <asp:TextBox ID="txtR3Address" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR3From" Width="50px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR3To" Width="50px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR3Justification" Width="500px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
</tr>

<tr>
    <td>
        <asp:TextBox ID="txtR4Address" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR4From" Width="50px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR4To" Width="50px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR4Justification" Width="500px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
</tr>

<tr>
    <td>
        <asp:TextBox ID="txtR5Address" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR5From" Width="50px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR5To" Width="50px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
    <td>
        <asp:TextBox ID="txtR5Justification" Width="500px" runat="server" Font-Names="Georgia, Arial"></asp:TextBox></td>
</tr>
</table>