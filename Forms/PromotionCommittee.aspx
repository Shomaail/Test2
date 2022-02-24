<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="PromotionCommittee.aspx.cs" Inherits="Forms_PromotionCommittee" Title="Faculty Promotion System" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../Controls/CollegeFaculty.ascx" TagName="CollegeFaculty" TagPrefix="uc3" %>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
    $(document).ready(function () {
    $('#tableEmployee').DataTable({
                               "pageLength": 100,
                                    });
    });

    </script>
    <uc2:Instruction ID="Instruction1" runat="server" />
    <br />
    <asp:Label ID="lblMessage" runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
    <br />
    <div id="divSuggestedPC" runat="server">
        <table cellpadding="3" cellspacing="0" border="1" bordercolor="#3062af" style="border-collapse: collapse;" class="table-condensed">
            <tr>
                <th class="PointsHeader"></th>
                <th class="PointsHeader"></th>
                <th class="PointsHeader">KFUPM Email UserID</th>
                <th class="PointsHeader">Name</th>
                <th class="PointsHeader">Rank</th>
                <th class="PointsHeader">Department</th>
                <th class="PointsHeader">Email</th>
            </tr>
            <tr>
                <td colspan="6" style="font-size: 10pt; font-weight: bold; background-color: #efefef; font-style: italic">Nominees for Committee Chairman</td>
            </tr>
            <tr>
                <td>1</td>
                <td>
                    <asp:LinkButton ID="lbtnDelete1" runat="server" OnClick="lbtnDelete_Click" >Delete</asp:LinkButton></td>
                <td>
                    <asp:TextBox ID="tbSugPC1EmailID" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbSugPC1EmailID_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnInfo1" runat="server" Text="Info" AutoPostBack="True" OnClick="tbSugPC1EmailID_TextChanged" /><br />
                    <asp:Label ID="lblError1" runat="server" ForeColor="red" />

                </td>
                <td>
                    <asp:Label ID="lblName1" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblRank1" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblDepartment1" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblEmail1" runat="server" BorderWidth="0px" Width="300px" Font-Names="Georgia, Times" /></td>
            </tr>
            <tr>
                <td>2</td>
                <td>
                    <asp:LinkButton ID="lbtnDelete2" runat="server" OnClick="lbtnDelete_Click">Delete</asp:LinkButton></td>

                <td>
                    <asp:TextBox ID="tbSugPC2EmailID" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbSugPC2EmailID_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnInfo2" runat="server" Text="Info" OnClick="tbSugPC2EmailID_TextChanged" /><br />

                    <asp:Label ID="lblError2" runat="server" ForeColor="red" />

                </td>
                <td>
                    <asp:Label ID="lblName2" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblRank2" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblDepartment2" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblEmail2" runat="server" BorderWidth="0px" Width="300px" Font-Names="Georgia, Times" /></td>
            </tr>
            <tr>
                <td>3</td>
                <td>
                    <asp:LinkButton ID="lbtnDelete3" runat="server" OnClick="lbtnDelete_Click">Delete</asp:LinkButton></td>
                <td>
                    <asp:TextBox ID="tbSugPC3EmailID" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbSugPC3EmailID_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnInfo3" runat="server" Text="Info" OnClick="tbSugPC3EmailID_TextChanged" /><br />
                    <asp:Label ID="lblError3" runat="server" ForeColor="red" />

                </td>
                <td>
                    <asp:Label ID="lblName3" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblRank3" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblDepartment3" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblEmail3" runat="server" BorderWidth="0px" Width="300px" Font-Names="Georgia, Times" /></td>

            </tr>
            <tr>
                <td colspan="6" style="font-size: 10pt; font-weight: bold; background-color: #efefef; font-style: italic">Nominees from Speciality Area</td>
            </tr>
            <tr>
                <td>4</td>
                <td>
                    <asp:LinkButton ID="lbtnDelete4" runat="server" OnClick="lbtnDelete_Click">Delete</asp:LinkButton></td>

                <td>
                    <asp:TextBox ID="tbSugPC4EmailID" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbSugPC4EmailID_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnInfo4" runat="server" Text="Info" OnClick="tbSugPC4EmailID_TextChanged" /><br />

                    <asp:Label ID="lblError4" runat="server" ForeColor="red" />

                </td>
                <td>
                    <asp:Label ID="lblName4" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblRank4" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblDepartment4" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblEmail4" runat="server" BorderWidth="0px" Width="300px" Font-Names="Georgia, Times" /></td>
            </tr>
            <tr>
                <td>5</td>
                <td>
                    <asp:LinkButton ID="lbtnDelete5" runat="server" OnClick="lbtnDelete_Click">Delete</asp:LinkButton></td>

                <td>
                    <asp:TextBox ID="tbSugPC5EmailID" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbSugPC5EmailID_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnInfo5" runat="server" Text="Info" OnClick="tbSugPC5EmailID_TextChanged" /><br />

                    <asp:Label ID="lblError5" runat="server" ForeColor="red" />

                </td>
                <td>
                    <asp:Label ID="lblName5" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblRank5" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblDepartment5" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblEmail5" runat="server" BorderWidth="0px" Width="300px" Font-Names="Georgia, Times" /></td>
            </tr>
            <tr>
                <td>6</td>
                <td>
                    <asp:LinkButton ID="lbtnDelete6" runat="server" OnClick="lbtnDelete_Click">Delete</asp:LinkButton></td>

                <td>
                    <asp:TextBox ID="tbSugPC6EmailID" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbSugPC6EmailID_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnInfo6" runat="server" Text="Info" OnClick="tbSugPC6EmailID_TextChanged" /><br />

                    <asp:Label ID="lblError6" runat="server" ForeColor="red" />

                </td>
                <td>
                    <asp:Label ID="lblName6" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblRank6" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblDepartment6" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblEmail6" runat="server" BorderWidth="0px" Width="300px" Font-Names="Georgia, Times" /></td>
            </tr>
            <tr>
                <td>7</td>
                <td>
                    <asp:LinkButton ID="lbtnDelete7" runat="server" OnClick="lbtnDelete_Click">Delete</asp:LinkButton></td>

                <td>
                    <asp:TextBox ID="tbSugPC7EmailID" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbSugPC7EmailID_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnInfo7" runat="server" Text="Info" OnClick="tbSugPC7EmailID_TextChanged" /><br />

                    <asp:Label ID="lblError7" runat="server" ForeColor="red" />

                </td>
                <td>
                    <asp:Label ID="lblName7" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblRank7" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblDepartment7" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblEmail7" runat="server" BorderWidth="0px" Width="300px" Font-Names="Georgia, Times" /></td>
            </tr>
            <tr>
                <td colspan="6" style="font-size: 10pt; font-weight: bold; background-color: #efefef; font-style: italic">Nominees from Related Areas</td>
            </tr>
            <tr>
                <td>8</td>
                <td>
                    <asp:LinkButton ID="lbtnDelete8" runat="server" OnClick="lbtnDelete_Click">Delete</asp:LinkButton></td>

                <td>
                    <asp:TextBox ID="tbSugPC8EmailID" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbSugPC8EmailID_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnInfo8" runat="server" Text="Info" OnClick="tbSugPC8EmailID_TextChanged" /><br />

                    <asp:Label ID="lblError8" runat="server" ForeColor="red" />

                </td>
                <td>
                    <asp:Label ID="lblName8" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblRank8" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblDepartment8" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblEmail8" runat="server" BorderWidth="0px" Width="300px" Font-Names="Georgia, Times" /></td>
            </tr>
            <tr>
                <td>9</td>
                <td>
                    <asp:LinkButton ID="lbtnDelete9" runat="server" OnClick="lbtnDelete_Click">Delete</asp:LinkButton></td>

                <td>
                    <asp:TextBox ID="tbSugPC9EmailID" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbSugPC9EmailID_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnInfo9" runat="server" Text="Info" OnClick="tbSugPC9EmailID_TextChanged" /><br />

                    <asp:Label ID="lblError9" runat="server" ForeColor="red" />

                </td>
                <td>
                    <asp:Label ID="lblName9" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblRank9" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblDepartment9" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblEmail9" runat="server" BorderWidth="0px" Width="300px" Font-Names="Georgia, Times" /></td>
            </tr>
            <tr>
                <td>10</td>
                <td>
                    <asp:LinkButton ID="lbtnDelete10" runat="server" OnClick="lbtnDelete_Click">Delete</asp:LinkButton></td>

                <td>
                    <asp:TextBox ID="tbSugPC10EmailID" runat="server" TabIndex="100" AutoPostBack="True" OnTextChanged="tbSugPC10EmailID_TextChanged"></asp:TextBox>
                    <asp:Button ID="btnInfo10" runat="server" Text="Info" OnClick="tbSugPC10EmailID_TextChanged" /><br />

                    <asp:Label ID="lblError10" runat="server" ForeColor="red" />

                </td>
                <td>
                    <asp:Label ID="lblName10" runat="server" BorderWidth="0px" Width="200px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblRank10" runat="server" BorderWidth="0px" Width="150px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblDepartment10" runat="server" BorderWidth="0px" Width="100px" Font-Names="Georgia, Times" /></td>
                <td>
                    <asp:Label ID="lblEmail10" runat="server" BorderWidth="0px" Width="300px" Font-Names="Georgia, Times" /></td>
            </tr>
        </table>
    </div>
    <br />


    <uc3:CollegeFaculty ID="CollegeFaculty1" runat="server" />
</asp:Content>

