<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChairmanInput.ascx.cs" Inherits="Controls_ChairmanInput" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<h3>Teaching</h3>
<p>
    <asp:TextBox ID="txtTeaching" runat="server" Font-Names="Georgia,Times" Height="150px"
        TextMode="MultiLine" onDrop="return false;"
        onKeyPress="return limitCharsLength(this,2000);"
        onPaste="return limitClipBoard(this,2000);" Width="100%" AutoPostBack="True"
        OnTextChanged="txtTeaching_TextChanged"></asp:TextBox>
    <asp:Label ID="LabelTeaching" runat="server" Font-Names="Georgia,Times" Visible="false"></asp:Label>

</p>
<h3>Research</h3>
<p>
    <asp:TextBox ID="txtResearch" runat="server" Font-Names="Georgia,Times" Height="150px"
        TextMode="MultiLine" onDrop="return false;"
        onKeyPress="return limitCharsLength(this,2000);"
        onPaste="return limitClipBoard(this,2000);" Width="100%" AutoPostBack="True"
        OnTextChanged="txtResearch_TextChanged"></asp:TextBox>
    <asp:Label ID="LabelResearch" runat="server" Font-Names="Georgia,Times" Visible="false"></asp:Label>
</p>

<h3>Community Service</h3>
<p>
    <asp:TextBox ID="txtCommunityService" runat="server" Font-Names="Georgia,Times" Height="150px"
        TextMode="MultiLine" onDrop="return false;"
        onKeyPress="return limitCharsLength(this,2000);"
        onPaste="return limitClipBoard(this,2000);" Width="100%" AutoPostBack="True"
        OnTextChanged="txtCommunityService_TextChanged"></asp:TextBox>
    <asp:Label ID="LabelCommunityService" runat="server" Font-Names="Georgia,Times" Visible="false"></asp:Label>
</p>

<h3>Comments and Recommendations</h3>
<p>
    <asp:TextBox ID="txtComments" runat="server" Font-Names="Georgia,Times" Height="150px"
        TextMode="MultiLine" onDrop="return false;"
        onKeyPress="return limitCharsLength(this,2000);"
        onPaste="return limitClipBoard(this,2000);" Width="100%" AutoPostBack="True"
        OnTextChanged="txtComments_TextChanged"></asp:TextBox>
    <asp:Label ID="LabelComments" runat="server" Font-Names="Georgia,Times" Visible="false"></asp:Label>
</p>
<h3>Council Meeting Number</h3>

<asp:TextBox ID="tbDeptCouncilMeetingNo" runat="server" Width="376px" OnTextChanged="tbDeptCouncilMeetingNo_TextChanged" AutoPostBack="True"></asp:TextBox>
 <asp:Label ID="lblDeptCouncilMeetingNo" runat="server" Font-Names="Georgia,Times" Visible="false"></asp:Label>
<h3>Meeting Date</h3>
<asp:TextBox ID="tbDeptCouncilMeetingDate" runat="server" Width="376px" OnTextChanged="tbDeptCouncilMeetingDate_TextChanged" AutoPostBack="True"></asp:TextBox>
 <asp:Label ID="lblDeptCouncilMeetingDate" runat="server" Font-Names="Georgia,Times" Visible="false"></asp:Label>
<ajaxtoolkit:TextBoxWatermarkExtender ID="tbDeptCouncilMeetingDate_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="tbDeptCouncilMeetingDate"
    WatermarkText="Meeting Date" WatermarkCssClass="watermarked">
</ajaxtoolkit:TextBoxWatermarkExtender>
<ajaxtoolkit:CalendarExtender ID="tbDeptCouncilMeetingDate_CalendarExtender" runat="server" Format="dd MMMM yyyy"
    Enabled="True" TargetControlID="tbDeptCouncilMeetingDate">
</ajaxtoolkit:CalendarExtender>
<p>
    &nbsp;</p>

