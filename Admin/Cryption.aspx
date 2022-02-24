<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Cryption.aspx.cs" Inherits="Admin_Cryption" Title="Faculty Promotion System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding:10px">
              <h4>Text</h4>
    <textarea runat="server" id="tbText" style="width:600px;height:300px" placeholder="Enter Text"></textarea>
        <br />
         <br />
        <asp:Button runat="server" ID="btnEncrypt" OnClick="btnEncrypt_Click" Text="Encrypt" class="PointsHeader"/> <asp:Button runat="server" ID="btnDecrypt" OnClick="btnDecrypt_Click" Text="Decrypt" class="PointsHeader"/>
        <br />
         <br />
        <h4>Result</h4>
        <textarea runat="server" id="tbResult" disabled="disabled" style="width:600px;height:300px"></textarea>
    </div>
</asp:Content>