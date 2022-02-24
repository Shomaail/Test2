
<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="EligibilityChecklist.aspx.cs" Inherits="Forms_EligibilityChecklist" Title="Faculty Promotion System" %>

<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/ChecklistForm.ascx" TagName="CheckList" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Instruction ID="Instruction1" runat="server" />
    <br />
    <br />
    <asp:Label ID="lblMessage" runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
     <div id="divEligibilityChecklist" runat="server"
        class='<%# (Master.CurRoleID == (byte) RoleID.SC_Subcommittee_Chair
            ||Master.CurRoleID == (byte) RoleID.SC_Subcommittee_Member_1
            ||Master.CurRoleID == (byte) RoleID.SC_Subcommittee_Member_2
            ||Master.CurRoleID == (byte) RoleID.SC_Subcommittee_Member_3
            ||Master.CurRoleID == (byte) RoleID.SC_Subcommittee_Member_4
            ||Master.CurRoleID == (byte) RoleID.College_Dean
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority )
            && !Master.App.ApplicationClosed? "": "disabledbutton" %>'>
    <uc2:CheckList id="CheckList1" runat="server">
    </uc2:CheckList>
         
    <br />
    <br />
    <asp:Button ID="ButtonSave" runat="server" OnClick="ButtonSave_Click" Text="Save" class="btn btn-primary btn-md"/>
         </div>
</asp:Content>

