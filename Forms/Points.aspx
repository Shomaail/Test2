<%@ Page Language="C#" MasterPageFile="Forms.master" AutoEventWireup="true" CodeFile="Points.aspx.cs" Inherits="Forms_Points" Debug="true" %>



<%@ Register Src="../Controls/PointCorrections.ascx" TagName="PointCorrections" TagPrefix="uc3" %>

<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="Forms.master" %>
<%@ Register Src="../Controls/Points.ascx" TagName="Points" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <uc2:Instruction ID="Instruction1" runat="server" />
       <%-- <asp:Label ID="lblMessage" runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>--%>
<div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                visible='<%# !GetAppTaskFormCompleted()
                          && 
                         (Master.CurRoleID == (byte) RoleID.Applicant ||
                          Master.CurRoleID == (byte) RoleID.DepartmentChairman ||                          
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Chairman||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_1||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_2||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_3||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_4
                          )                           
                          && !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                <b>Incomplete: </b>
                <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
            <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                visible='<%# GetAppTaskFormCompleted() 
                         && (
                          Master.CurRoleID == (byte) RoleID.Applicant ||
                          Master.CurRoleID == (byte) RoleID.DepartmentChairman ||                          
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Chairman||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_1||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_2||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_3||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_4
                          )
                          &&!Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                <b>Complete:</b>
                <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>

    <asp:Panel ID="pnlApplicant" runat="server" Width="100%">
    <h3>Applicant's Input
    </h3>
    <uc1:Points ID="Points1" runat="server" />
        <br />
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Width="140px" class="btn btn-primary btn-md" /><br />
    </asp:Panel>
    <asp:Panel ID="pnlDepartment" runat="server" Width="100%">
    <h3 style="padding-top:20px">Department's Input
    </h3>
    <uc1:Points ID="Points2" runat="server" />
        <br />
        <uc3:PointCorrections id="PointCorrections1" runat="server">
        </uc3:PointCorrections>
    <asp:Button ID="btnSaveDept" runat="server" OnClick="btnSaveDept_Click" Text="Save" Width="139px" class="btn btn-primary btn-md"  /><br />
        <br />
    </asp:Panel>
</asp:Content>