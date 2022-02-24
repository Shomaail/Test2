<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentCommittee.ascx.cs" Inherits="Controls_DepartmentCommittee" %>
<%@ Register Src="EmployeeInput.ascx" TagName="EmployeeInput" TagPrefix="uc1" %>
<table cellpadding="3" cellspacing="0" border="1" bordercolor="black" style="border-collapse:collapse;" class="table-condensed"> 
<tr>
<th style="background-color:#007d40; color:white"></th>
<th style="background-color:#007d40; color:white">
    KFUPM Email User ID</th>
<th style="background-color:#007d40; color:white">Name</th>
<th style="background-color:#007d40; color:white">Rank</th>
</tr>
<tr><td>1.</td><uc1:EmployeeInput ID="EmployeeInput1" runat="server" /><td style="font-size:10pt"><b>(Chairman)</b></td></tr>
<tr><td>2.</td><uc1:EmployeeInput ID="EmployeeInput2" runat="server" /><td style="font-size:10pt">(Member)</td></tr>
<tr><td>3.</td><uc1:EmployeeInput ID="EmployeeInput3" runat="server" /><td style="font-size:10pt">(Member)</td></tr>
<tr><td>4.</td><uc1:EmployeeInput ID="EmployeeInput4" runat="server" /><td style="font-size:10pt">(Member)</td></tr>
<tr><td>5.</td><uc1:EmployeeInput ID="EmployeeInput5" runat="server" /><td style="font-size:10pt">(Member)</td></tr>
</table>
