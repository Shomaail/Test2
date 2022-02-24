<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Forms_Message" %>
<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="pnlMessage" runat="server" Width="100%">
    <asp:Repeater runat=server ID=rptMessages OnItemCommand="rptMessages_ItemCommand">
    <ItemTemplate>
        <div style="border: 1px solid black; background-color:#f8f8f8; padding:5px">
            <asp:TextBox runat=server ID="txtMessage" ReadOnly="true" TextMode="MultiLine" BackColor="transparent" BorderWidth="0px" Font-Names="Georgia, Times" Font-Size="12pt" Width="100%" Height="350px"
                Text='<%# Cryptography.Decrypt(Eval("Message").ToString()) %>'></asp:TextBox>
        </div>
    </ItemTemplate>
    </asp:Repeater>
    
    </asp:Panel>
</asp:Content>