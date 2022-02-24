<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Cryption.aspx.cs" Inherits="Admin_Cryption" Title="Faculty Promotion System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
    <textarea runat="server" id="tbText" style="width:300px;height:300px"></textarea>
        <br />
        <asp:Button runat="server" ID="btnEncrypt" OnClick="btnEncrypt_Click" Text="Encrypt" /><asp:Button runat="server" ID="btnDecrypt" OnClick="btnDecrypt_Click" Text="Decrypt" />
        <br />
        <textarea runat="server" id="tbResult" disabled="disabled" style="width:300px;height:300px"></textarea>
    </div>
</asp:Content>
