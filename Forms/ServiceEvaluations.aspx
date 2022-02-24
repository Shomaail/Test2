<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" Inherits="Forms_ServiceEvaluations" CodeFile="ServiceEvaluations.aspx.cs" %>

<%@ Register Src="../Controls/AnnualEvaluation.ascx" TagName="AnnualEvaluation" TagPrefix="uc3" %>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="Forms.master" %>

<%@ Register Src="../Controls/DepartmentFaculty.ascx" TagName="DepartmentFaculty" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc2:Instruction ID="Instruction1" runat="server" />
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="divCollegeInput" runat="server"
        class='<%# (
            Master.CurRoleID ==(byte) RoleID.DeanofFacultyAffairs            
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority )
            && !Master.App.ApplicationClosed? "": "disabledbutton" %>'>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                    visible='<%# !GetAppTaskFormCompleted() &&
                        
                        (Master.CurRoleID == (byte) RoleID.Applicant ||
                          Master.CurRoleID == (byte) RoleID.DepartmentChairman ||
                          Master.CurRoleID == (byte) RoleID.DeanofFacultyAffairs
                          )
                    && !Master.App.ApplicationClosed%>'>
                    <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                    <b>Incomplete: </b>
                    <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                        Font-Italic="True"></asp:Label>
                </div>
                <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                    visible='<%# GetAppTaskFormCompleted()  && 
                        (Master.CurRoleID == (byte) RoleID.Applicant ||
                          Master.CurRoleID == (byte) RoleID.DepartmentChairman ||
                          Master.CurRoleID == (byte) RoleID.DeanofFacultyAffairs
                          )
                    && 
                        !Master.App.ApplicationClosed%>'>
                    <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                    <b>Complete:</b>
                    <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                        Font-Italic="True"></asp:Label>
                </div>
                <h3>
                    <%=Resources.Resource.AnnualEvaluation%></h3>
                <%=Resources.Resource.AnnualEvaluationSubHeading %>
                <asp:TextBox ID="tbF1" runat="server" Font-Names="Georgia,Times" Height="150px"
                    TextMode="MultiLine" Width="100%" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

