<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeInputSC.ascx.cs" Inherits="Controls_EmployeeInputSC" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="mp" TagName="MyMP" Src="~/Forms/Forms.master"  %>
<td nowrap="nowrap">
    <asp:HiddenField ID="hdnEmployeeID" runat="server" />
    <anthem:TextBox ID="txtKFUPMUserID" runat="server" TabIndex="100" AutoUpdateAfterCallBack="True" OnTextChanged="button_Click"></anthem:TextBox>
    <anthem:Button id="button" runat="server" Text="Info" AutoUpdateAfterCallBack="True" EnabledDuringCallBack="False" TextDuringCallBack="" /><br />
    <anthem:CheckBox runat="server" ID="chkKFUPM" Text="KFUPM?" AutoUpdateAfterCallBack="true" AutoCallBack="True" Checked="True" OnCheckedChanged="chkKFUPM_CheckedChanged" Visible="False" Font-Size="10pt" />
    
    <anthem:Panel id="pnlError" runat="server" AutoUpdateAfterCallBack="true" Visible="false">
        <anthem:Label id="lblError" runat="server" AutoUpdateAfterCallBack="true" ForeColor="red" />
    </anthem:Panel>
</td>
<td>
    <anthem:TextBox id="lblName" runat="server" AutoUpdateAfterCallBack="true" ReadOnly="true" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" />
</td>
<td>
    <anthem:TextBox id="lblRank" runat="server" AutoUpdateAfterCallBack="true" ReadOnly="true" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" />
</td>
<asp:PlaceHolder id="pnlFullDetails" runat="server" Visible="false">
    <td>
        <anthem:TextBox id="txtDepartment" runat="server" AutoUpdateAfterCallBack="true" ReadOnly="true" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" />
    </td>
    <td>
        <anthem:TextBox id="txtEmail" runat="server" AutoUpdateAfterCallBack="true" ReadOnly="true" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" />
    </td>
    <td>
        <anthem:TextBox id="txtMajor" runat="server" AutoUpdateAfterCallBack="true" ReadOnly="true" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" />
    </td>
    <td>
        <anthem:TextBox id="txtMailingAddress" runat="server" AutoUpdateAfterCallBack="true" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,200);"  onPaste="return limitClipBoard(this,200);"  ReadOnly="true" />
    </td>
       <td>
        <anthem:TextBox id="txtPhone" runat="server" AutoUpdateAfterCallBack="true" ReadOnly="true" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" />
    </td>
</asp:PlaceHolder>