<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeptCommitteeMessage.ascx.cs"
    Inherits="Controls_DeptCommitteeMessage" %>
<p>
    <asp:TextBox ID="lblHeader" runat="server" TextMode="MultiLine" Width="100%" Height="75px" Font-Names="Georgia,Times"></asp:TextBox></p>
<p>
    <asp:RadioButton ID="rdo1" runat="server" Text="The candidate is eligible to apply for promotion for the following reasons:" AutoPostBack="True" OnCheckedChanged="rdo1_CheckedChanged" GroupName="commOptions" Font-Bold="True" />
    <ul style="list-style:none; margin-top:2px">
        <li><asp:CheckBox ID="chkR1C1" runat="server" Text="- The dossier is complete" /></li>
        <li><asp:CheckBox ID="chkR1C2" runat="server" Text="- The units calculations are correct and satisfy the minimum requirements" /></li>
        <li><asp:CheckBox ID="chkR1C3" runat="server" Text="- The units calculations were modified as indicated in the table and were found to satisfy the minimum requirements" /></li>
    </ul>
</p>
<p>
    <asp:RadioButton ID="rdo2" runat="server" Text="The candidate is not eligible to apply for promotion for the following reasons:" AutoPostBack="True" OnCheckedChanged="rdo1_CheckedChanged" GroupName="commOptions" Font-Bold="True" />
    <ul style="list-style:none; margin-top:2px">
        <li><asp:CheckBox ID="chkR2C1" runat="server" Text="- The dossier in not complete" /></li>
        <li><asp:CheckBox ID="chkR2C2" runat="server" Text="- The units do not satisfy the minimum requirements" /></li>
        <li><asp:CheckBox ID="chkR2C3" runat="server" Text="- The units calculations were modified as indicated in the table and the modified units do not satisfy the minimum requirements" /></li>
    </ul>
</p>
<p>
    <asp:RadioButton ID="rdo3" runat="server" Text="The dossier has been checked and found incomplete.
The following information is needed:" AutoPostBack="True" OnCheckedChanged="rdo1_CheckedChanged" GroupName="commOptions" Font-Bold="True" />
    <ul style="list-style:none; margin-top:2px">
        <li><em>(list the requirements here or attach as an PDF file)</em></li>
    </ul>

<p>
    <asp:RadioButton ID="rdo4" runat="server" Text="The dossier has been checked and further clarifications on the following issues are needed:" AutoPostBack="True" OnCheckedChanged="rdo1_CheckedChanged" GroupName="commOptions" Font-Bold="True" />
    <ul style="list-style:none; margin-top:2px">
        <li><em>(list the requirements here or attach as an PDF file)</em></li>
    </ul>
</p>
<p>
    Additional comments or justifications (if any):<br />
    <asp:TextBox ID="txtAdditionalComments" runat="server" Height="100px" TextMode="MultiLine" Width="100%" Font-Names="Georgia,Times"></asp:TextBox></p>
<p>
    <asp:TextBox ID="lblFooter" runat="server" TextMode="MultiLine" Width="100%" Height="75px" Font-Names="Georgia,Times"></asp:TextBox></p>
