<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true"
     Inherits="Forms_DeptCommittee" CodeFile="DeptCommittee.aspx.cs" %>
<%@ Register Src="../Controls/CollegeFaculty.ascx" TagName="CollegeFaculty" TagPrefix="uc3" %>

<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
function copy() {
  var copyText = document.getElementById("myInput");
  copyText.select();
  document.execCommand("Copy");  
}
</script>
 <script type="text/javascript">
    $(document).ready(function () {
    $('#tableEmployee').DataTable({
                               "pageLength": 100,
                                    });
    });

    </script>
    <uc1:Instruction ID="Instruction1" runat="server" />
    &nbsp;<br />
     <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                visible='<%# !GetAppTaskFormCompleted() && !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                <b>Incomplete: </b>
                <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
            <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                visible='<%# GetAppTaskFormCompleted()                     
                    && !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                <b>Complete:</b>
                <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
            <asp:Panel ID="pnlDeptComm" runat="server">
                <table cellpadding="3" cellspacing="0" border="1" bordercolor="#3062af" style="border-collapse: collapse;" class="table-condensed">
                    <tr>
                        <th style="background-color: #1e9966;color:white"></th>
                        <th style="background-color: #1e9966;color:white"></th>
                        <th style="background-color: #1e9966;color:white">Email</th>
                        <th style="background-color: #1e9966;color:white">Name</th>
                        <th style="background-color: #1e9966;color:white">Rank</th>
                        <th style="background-color: #1e9966;color:white">Department</th>
                        <th style="background-color: #1e9966;color:white">Organization</th>
                    </tr>
                    <tr>
                        <td>1</td>
                        <td>
                             <asp:LinkButton ID="lbtnDelete1" runat="server" CausesValidation="false" 
                                 OnClick="lbtnDelete_Click"
                                  ToolTip="Delete" CssClass="btn btn-default">
                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                </asp:LinkButton>
                        <td>
                            <asp:TextBox ID="tbDC1Email" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbDC1Email_TextChanged"></asp:TextBox>
                            <ajaxtoolkit:TextBoxWatermarkExtender ID="tbDC1Email_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="tbDC1Email"
                                WatermarkText="Enter Email " WatermarkCssClass="watermarked"></ajaxtoolkit:TextBoxWatermarkExtender>
                            <%-- <asp:Button ID="btnInfo1" runat="server" Text="Info" AutoPostBack="True" OnClick="tbDC1Email_TextChanged" />--%><br />
                            <asp:Label ID="lblError1" runat="server" ForeColor="red" />

                        </td>
                        <td>
                            <asp:Label ID="lblName1" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" />(Chair)</td>
                        <td>
                            <asp:Label ID="lblRank1" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblDepartment1" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblOrganization1" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>
                             <asp:LinkButton ID="lbtnDelete2" runat="server" CausesValidation="false" 
                                 OnClick="lbtnDelete_Click"
                                  ToolTip="Delete" CssClass="btn btn-default">
                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                </asp:LinkButton></td>

                        <td>
                            <asp:TextBox ID="tbDC2Email" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbDC2Email_TextChanged"></asp:TextBox>
                            <ajaxtoolkit:TextBoxWatermarkExtender ID="tbDC2Email_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="tbDC2Email"
                                WatermarkText="Enter Email " WatermarkCssClass="watermarked"></ajaxtoolkit:TextBoxWatermarkExtender>
                            <%-- <asp:Button ID="btnInfo2" runat="server" Text="Info" OnClick="tbDC2Email_TextChanged" />--%><br />

                            <asp:Label ID="lblError2" runat="server" ForeColor="red" />

                        </td>
                        <td>
                            <asp:Label ID="lblName2" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblRank2" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblDepartment2" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblOrganization2" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                    </tr>
                    <tr>
                        <td>3</td>
                        <td>
                            <asp:LinkButton ID="lbtnDelete3" runat="server" CausesValidation="false" 
                                 OnClick="lbtnDelete_Click"
                                  ToolTip="Delete" CssClass="btn btn-default">
                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                </asp:LinkButton></td>
                        <td>
                            <asp:TextBox ID="tbDC3Email" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbDC3Email_TextChanged"></asp:TextBox>
                            <ajaxtoolkit:TextBoxWatermarkExtender ID="tbDC3Email_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="tbDC3Email"
                                WatermarkText="Enter Email " WatermarkCssClass="watermarked"></ajaxtoolkit:TextBoxWatermarkExtender>

                            <%--<asp:Button ID="btnInfo3" runat="server" Text="Info" OnClick="tbDC3Email_TextChanged" />--%><br />
                            <asp:Label ID="lblError3" runat="server" ForeColor="red" />

                        </td>
                        <td>
                            <asp:Label ID="lblName3" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblRank3" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblDepartment3" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblOrganization3" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>

                    </tr>
                    <tr>
                        <td>4</td>
                        <td>
                            <asp:LinkButton ID="lbtnDelete4" runat="server" CausesValidation="false" 
                                 OnClick="lbtnDelete_Click"
                                  ToolTip="Delete" CssClass="btn btn-default">
                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                </asp:LinkButton></td>

                        <td>
                            <asp:TextBox ID="tbDC4Email" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbDC4Email_TextChanged"></asp:TextBox>
                            <ajaxtoolkit:TextBoxWatermarkExtender ID="tbDC4Email_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="tbDC4Email"
                                WatermarkText="Enter Email " WatermarkCssClass="watermarked"></ajaxtoolkit:TextBoxWatermarkExtender>

                            <%-- <asp:Button ID="btnInfo4" runat="server" Text="Info" OnClick="tbDC4Email_TextChanged" />--%><br />

                            <asp:Label ID="lblError4" runat="server" ForeColor="red" />

                        </td>
                        <td>
                            <asp:Label ID="lblName4" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblRank4" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblDepartment4" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblOrganization4" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                    </tr>
                    <tr>
                        <td>5</td>
                        <td>
                           <asp:LinkButton ID="lbtnDelete5" runat="server" CausesValidation="false" 
                                 OnClick="lbtnDelete_Click"
                                  ToolTip="Delete" CssClass="btn btn-default">
                        <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                </asp:LinkButton></td>

                        <td>
                            <asp:TextBox ID="tbDC5Email" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbDC5Email_TextChanged"></asp:TextBox>
                            <ajaxtoolkit:TextBoxWatermarkExtender ID="tbDC5Email_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="tbDC5Email"
                                WatermarkText="Enter Email " WatermarkCssClass="watermarked"></ajaxtoolkit:TextBoxWatermarkExtender>

                            <%--<asp:Button ID="btnInfo5" runat="server" Text="Info" OnClick="tbDC5Email_TextChanged" />--%><br />

                            <asp:Label ID="lblError5" runat="server" ForeColor="red" />

                        </td>
                        <td>
                            <asp:Label ID="lblName5" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblRank5" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblDepartment5" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                        <td>
                            <asp:Label ID="lblOrganization5" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                    </tr>
                </table>


            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
     <uc3:CollegeFaculty ID="CollegeFaculty1" runat="server" />
    <%--<uc3:DepartmentFaculty ID="DepartmentFaculty" runat="server" />--%>
    <br />
    </asp:Content>
