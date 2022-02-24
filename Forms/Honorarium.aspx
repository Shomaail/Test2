<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="Honorarium.aspx.cs" Inherits="Forms_Honorarium" %>

<%@ Register src="../Controls/ComposeEmail.ascx" tagname="ComposeEmail" tagprefix="uc1" %>
<%@ MasterType VirtualPath="Forms.master" %>
<%@ Register src="../Controls/Instruction.ascx" tagname="Instruction" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <br />
            <uc2:Instruction ID="Instruction1" runat="server" />
    <asp:RadioButtonList ID="rbltHonorarium" runat="server" 
        onselectedindexchanged="rbltHonorarium_SelectedIndexChanged" 
                AutoPostBack="True">
        <asp:ListItem Value="Promotion_Committee">Honorarium for Promotion Committee</asp:ListItem>
         <asp:ListItem Value="External_Reviewers">Honorarium for External Reviewers
        </asp:ListItem>
    </asp:RadioButtonList>
        
        <br />
    <uc1:ComposeEmail ID="ComposeEmail1" runat="server" Visible="False" OnEmailSent="ComposeEmail_OnEmailSent" OnEmailNotSent="ComposeEmail_OnEmailNotSent" />
    <br />
</asp:Content>

