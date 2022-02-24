<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FinalDecision.ascx.cs" Inherits="Controls_FinalDecision" %>
<%--<%@ Register Src="ComposeEmailOld.ascx" TagName="ComposeEmail" TagPrefix="uc1" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<h3>Scientific Council Decision &amp; Comments
</h3>
<p>
    <asp:RadioButtonList ID="RadioButtonListSCDecision" runat="server">
        <asp:ListItem Selected="True">Promotable</asp:ListItem>
        <asp:ListItem>Unpromotable</asp:ListItem>
    </asp:RadioButtonList>&nbsp;
</p>
<asp:TextBox ID="TextBoxSCComments" runat="server" Height="170px" TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitClipBoard(this,2000);" Width="729px"></asp:TextBox><br />
<h3>President Decision &amp; Comments</h3>
<asp:RadioButtonList ID="RadioButtonListRectorDecision" runat="server">
    <asp:ListItem Selected="True" Value="Promotable">Promotable</asp:ListItem>
    <asp:ListItem>Unpromotable</asp:ListItem>
</asp:RadioButtonList><br />
<asp:TextBox ID="TextBoxRectorCom" runat="server" Height="149px" TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitClipBoard(this,2000);" Width="722px"></asp:TextBox>

<h3>Final Decision</h3>
<asp:RadioButtonList ID="RadioButtonListFinalDec" runat="server" Height="52px" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListFinalDec_SelectedIndexChanged" Width="128px">
    <asp:ListItem Value="True">Promotable</asp:ListItem>
    <asp:ListItem Value="False">Unpromotable</asp:ListItem>
</asp:RadioButtonList>
<%--<uc1:ComposeEmail ID="ComposeEmail1" runat="server" Visible="false" OnEmailNotSent="ComposeEmail_OnEmailNotSent" OnEmailSent="ComposeEmail_OnEmailSent" />--%>

<asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
<ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
    TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
    BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
    RepositionMode="RepositionOnWindowScroll">
</ajaxtoolkit:ModalPopupExtender>
<asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="display: none; width: 300px; padding: 10px">
    <asp:Panel runat="Server" ID="programmaticPopupDragHandle0" Style="cursor: move; color: Black; font-weight: 700; text-align: left;">
        Message:
    </asp:Panel>
    <asp:Label ID="labelProgrammaticPopup0" runat="server"></asp:Label>
    <br />
    <div align="center">
        <br />
        <asp:Button runat="server" ValidationGroup="OKBtn" ID="hideModalPopupViaServer0" Text="OK" OnClick="hideModalPopupViaServer0_Click" CssClass="btn btn-primary btn-sm" />
    </div>
</asp:Panel>
