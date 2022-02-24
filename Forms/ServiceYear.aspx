<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="ServiceYear.aspx.cs" Inherits="Forms_ServiceYear" %>

<%@ MasterType VirtualPath="Forms.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                visible='<%# !GetAppTaskFormCompleted() 
                    && 
                         (Master.CurRoleID == (byte) RoleID.Applicant ||
                          Master.CurRoleID == (byte) RoleID.DepartmentChairman ||
                          Master.CurRoleID == (byte) RoleID.DeanofFacultyAffairs
                          )
                    && !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                <b>Incomplete: </b>
                <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
            <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                visible='<%# GetAppTaskFormCompleted()&& 
                         (Master.CurRoleID == (byte) RoleID.Applicant ||
                          Master.CurRoleID == (byte) RoleID.DepartmentChairman ||
                          Master.CurRoleID == (byte) RoleID.DeanofFacultyAffairs
                          )&&
                          !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                <b>Complete:</b>
                <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>

             <div id="divServiceYears" runat="server"
        class='<%#
            (Master.CurRoleID == (byte) RoleID.Applicant 
            ||Master.CurRoleID ==(byte) RoleID.DeanofFacultyAffairs            
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority )
            && !Master.App.ApplicationClosed         
            ? "": "disabledbutton" %>'>
                <table border="1" bordercolor="#0088ce" style="border-collapse: collapse" cellpadding="3" cellspacing="0" class="table-condensed">
                    <tr runat="server" id="trServiceYears">

                        <td class="PointsHeader">Number of years in the current rank (Months and years):</td>
                        <td>No. of Years:</td>
                        <td>
                            <asp:TextBox ID="tbYears" TextMode="Number" min="0" max="50" runat="server" AutoPostBack="true"
                                OnTextChanged="UpdateServiceYears" Width="50px"></asp:TextBox>
                        </td>
                        <td>No. of Months:</td>
                        <td>
                            <asp:TextBox ID="tbMonths" TextMode="Number" min="0" max="11" runat="server" AutoPostBack="true"
                                OnTextChanged="UpdateServiceYears" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                

                    <tr runat="server" id="trVerfServiceYears">
                        <td class="PointsHeader">Number of years in the current rank as certified by Faculty Affairs:</td>
                        <td>No. of Years:</td>
                        <td>
                            <asp:TextBox ID="tbVerifiedYears" TextMode="Number" min="0" max="50" runat="server" AutoPostBack="true"
                                OnTextChanged="UpdateServiceYears" Width="50px"></asp:TextBox>
                        </td>
                        <td>No. of Months:</td>
                        <td>
                            <asp:TextBox ID="tbVerifiedMonths" TextMode="Number" min="0" max="11" runat="server" AutoPostBack="true"
                                OnTextChanged="UpdateServiceYears" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                
                    <tr runat="server" id="trSaudiYears">
                        <td class="PointsHeader">Number of years served in Saudi University (Months and years):</td>
                        <td>No. of Years:</td>
                        <td>
                            <asp:TextBox ID="tbYearsInSaudi" TextMode="Number" min="0" max="50" runat="server" AutoPostBack="true"
                                OnTextChanged="UpdateServiceYears" Width="50px"></asp:TextBox>
                        </td>
                        <td>No. of Months:</td>
                        <td>
                            <asp:TextBox ID="tbMonthsInSaudi" TextMode="Number" min="0" max="11" runat="server" AutoPostBack="true"
                                OnTextChanged="UpdateServiceYears" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                
                    <tr runat="server" id="trVerfSaudiYears">
                        <td class="PointsHeader">Number of years served in Saudi University (Months and years)<br />
                            as certified by Faculty Affairs:</:</td>
                        <td>No. of Years:</td>
                        <td>
                            <asp:TextBox ID="tbVerifiedYearsInSaudi" TextMode="Number" min="0" max="50" runat="server" AutoPostBack="true"
                                OnTextChanged="UpdateServiceYears" Width="50px"></asp:TextBox>
                        </td>

                        <td>No. of Months:</td>
                        <td>
                            <asp:TextBox ID="tbVerifiedMonthsInSaudi" TextMode="Number" min="0" max="11" runat="server" AutoPostBack="true"
                                OnTextChanged="UpdateServiceYears" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblDifferent" runat="server" Font-Italic="True" Font-Size="10pt" ForeColor="DarkRed"
        Text="-Note: The numbers certified by the Academic Promotion Committee are different from what the applicant's supplied."
        Visible="False"></asp:Label>
</asp:Content>
