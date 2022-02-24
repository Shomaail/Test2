<%@ Page Language="C#" MasterPageFile="Forms.master" AutoEventWireup="true" CodeFile="Requester.aspx.cs" Inherits="Forms_Requester" %>
<%@ Register Src="../Controls/AreaOfSpecialization.ascx" TagName="AreaOfSpecialization"
    TagPrefix="uc4" %>
   
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="Forms.master" %>
<%@ Register Src="../Controls/Applicant.ascx" TagName="Applicant" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:Instruction id="Instruction1" runat="server">
    </uc2:Instruction>
     <asp:Label ID="lblMessage" runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
    <div id="divApplicant" runat="server"
        class='<%# (Master.CurRoleID == (byte) RoleID.Applicant             
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority )
            && !Master.App.ApplicationClosed? "": "disabledbutton" %>'>
    <uc1:Applicant ID="Applicant1" runat="server" />
     <uc4:AreaOfSpecialization id="AreaOfSpecialization1" runat="server">
    </uc4:AreaOfSpecialization>
    <asp:Button ID="btnSave" runat="server" class="btn btn-primary btn-md" OnClick="btnSave_Click" Text="Save" Width="140px" />
    </div>
</asp:Content>

