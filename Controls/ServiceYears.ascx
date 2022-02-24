<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServiceYears.ascx.cs" Inherits="Controls_ServiceYears" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

    <h3>
        Eligibility Checklist</h3>
    <table border="1px" bordercolor="#3062af" style="border-collapse:collapse" cellpadding="3" cellspacing="0" class="table-condensed">
        <tr runat="server" id="trKFUPMYears">
            <td class="PointsHeader">
                Number of years in the current rank (Months and years):</td>
            <td nowrap="nowrap">
                No. of Years:
                <ew:NumericBox ID="txtYears" runat="server" Columns="2"></ew:NumericBox>
                No. of Months:
                <ew:NumericBox ID="txtMonths" runat="server" Columns="2"></ew:NumericBox>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="(Invalid)"
                    MaximumValue="11" MinimumValue="0" Type="Integer" ControlToValidate="txtMonths"></asp:RangeValidator></td>
        </tr>
        <tr runat="server" id="trVerfKFUPMYears">
            <td class="PointsHeader">
                Number of years in the current rank as certified by Faculty Affairs:</td>
            <td nowrap="nowrap">
                No. of Years:
                <ew:NumericBox ID="txtVerfYears" runat="server" Columns="2"></ew:NumericBox>
                No. of Months:
                <ew:NumericBox ID="txtVerfMonths" runat="server" Columns="2"></ew:NumericBox>
                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="(Invalid)"
                    MaximumValue="11" MinimumValue="0" Type="Integer" ControlToValidate="txtVerfMonths"></asp:RangeValidator></td>
        </tr>
        <tr runat="server" id="trSaudiYears">
            <td class="PointsHeader">
                Number of years served in Saudi University (Months and years):</td>
            <td nowrap="nowrap">
                No. of Years:
                <ew:NumericBox ID="txtSaudiYears" runat="server" Columns="2"></ew:NumericBox>
                No. of Months:
                <ew:NumericBox ID="txtSaudiMonths" runat="server" Columns="2"></ew:NumericBox>
                <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="(Invalid)"
                    MaximumValue="11" MinimumValue="0" Type="Integer" ControlToValidate="txtSaudiMonths"></asp:RangeValidator></td>
        </tr>
        <tr runat="server" id="trVerfSaudiYears">
            <td class="PointsHeader">
                Number of years served in Saudi University (Months and years)<br />as certified by Faculty Affairs:</:</td>
            <td nowrap="nowrap">
                No. of Years:
                <ew:NumericBox ID="txtVerfSaudiYears" runat="server" Columns="2"></ew:NumericBox>
                No. of Months:
                <ew:NumericBox ID="txtVerfSaudiMonths" runat="server" Columns="2"></ew:NumericBox>
                <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="(Invalid)"
                    MaximumValue="11" MinimumValue="0" Type="Integer" ControlToValidate="txtVerfSaudiMonths"></asp:RangeValidator></td>
        </tr>
    </table>
<br />
<asp:Label ID="lblDifferent" runat="server" Font-Italic="True" Font-Size="10pt" ForeColor="DarkRed"
    Text="-Note: The numbers certified by the Faculty Affairs are different from what the applicant's supplied."
    Visible="False"></asp:Label>
