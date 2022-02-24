<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Applicant.ascx.cs" Inherits="Controls_Applicant" %>
<div>
    <h3>Applicant's Information</h3>

    <table cellpadding="3" cellspacing="0" border="0" class="table-condensed">
        <tr class="DetailViewTableRow">
            <td class="DetailViewTableHeader">Name:</td>
            <td class="DetailViewTabelRow">
                <asp:Label ID="lblName" runat="server"></asp:Label></td>
        </tr>
       <tr class="DetailViewTableRowAlt">
            <td class="DetailViewTableHeaderAlt">ID:</td>
            <td class="DetailViewTabelRowDataAlt">
                <asp:Label ID="lblID" runat="server"></asp:Label></td>
        </tr>
         <tr class="DetailViewTableRow">
            <td class="DetailViewTableHeader">Department:</td>
            <td class="DetailViewTabelRowData">
                <asp:Label ID="lblDepartment" runat="server"></asp:Label></td>
        </tr>
        <tr class="DetailViewTableRowAlt">
            <td class="DetailViewTableHeaderAlt">College:</td>
            <td class="DetailViewTabelRowDataAlt">
                <asp:Label ID="lblCollege" runat="server"></asp:Label></td>
        </tr>
         <tr class="DetailViewTableRow">
            <td class="DetailViewTableHeader">POBox:</td>
            <td class="DetailViewTabelRowData">
                <asp:Label ID="lblPOBox" runat="server"></asp:Label></td>
        </tr>
       <tr class="DetailViewTableRowAlt">
            <td class="DetailViewTableHeaderAlt">Phone:</td>
            <td class="DetailViewTabelRowDataAlt">
                <asp:Label ID="lblPhone" runat="server"></asp:Label></td>
        </tr>
         <tr class="DetailViewTableRow">
            <td class="DetailViewTableHeader">Email:</td>
            <td class="DetailViewTabelRowData">
                <asp:Label ID="lblEmail" runat="server"></asp:Label></td>
        </tr>
       <tr class="DetailViewTableRowAlt">
            <td class="DetailViewTableHeaderAlt">Rank:</td>
            <td class="DetailViewTabelRowDataAlt">
                <asp:Label ID="lblRank" runat="server"></asp:Label></td>
        </tr>
         <tr class="DetailViewTableRow">
            <td class="DetailViewTableHeader">To the Rank:</td>
            <td class="DetailViewTabelRowData">
                <asp:Label ID="lblToRank" runat="server"></asp:Label></td>
        </tr>
       <tr class="DetailViewTableRowAlt">
            <td class="DetailViewTableHeaderAlt">Application Date:</td>
            <td class="DetailViewTabelRowDataAlt">
                <asp:Label ID="lblStartDate" runat="server"></asp:Label></td>
        </tr>
         <tr class="DetailViewTableRow">
            <td class="DetailViewTableHeader">Joining Date:</td>
            <td class="DetailViewTabelRowData">
                <asp:Label ID="lblJoinDate" runat="server"></asp:Label></td>
        </tr>

    </table>
</div>
