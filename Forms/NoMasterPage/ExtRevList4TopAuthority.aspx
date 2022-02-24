<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExtRevList4TopAuthority.aspx.cs" Inherits="Forms_NoMasterPage_ExtRevList4TopAuthority" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #ctl00_lblPageTitle {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%" class="table-condensed">
                <tr>
                    <th class="PointsHeader" style="text-align: left">Application of:</th>
                    <td style="border-style: solid; border-width: thin">
                        <em>
                            <asp:Label ID="lblApplicantEmail" runat="server" ForeColor="maroon">
                            </asp:Label></em></td>
                    <td></td>
                    <td>&nbsp;</td>
                    <td>
                        <span>A=Applicant</span><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>C=College<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span>D=Department</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span>VPRI=Vice President of Research and Innovation</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span>SCSubCom=Scientific Council Sub Committee</span></td>
                </tr>
                <tr>
                    <th class="PointsHeader" style="text-align: left">Speciality:</th>
                    <td style="border-style: solid; border-width: thin"><em>
                        <asp:Label ID="lblSpeciality" runat="server" ForeColor="maroon"></asp:Label></em></td>
                    <td>&nbsp;</td>
                    <th  class="PointsHeader" style="text-align: left">PhdFrom</th>
                    <td style="border-style: solid; border-width: thin"><em>
                        <asp:Label ID="lblPhdFrom" runat="server" ForeColor="maroon"></asp:Label></em></td>
                    
                </tr>

                <tr>
                    <th class="PointsHeader" style="text-align: left">Promotion to:</th>
                    <td style="border-style: solid; border-width: thin"><em>
                        <asp:Label ID="lblToRank" runat="server" ForeColor="maroon"></asp:Label></em></td>
                    <td></td>
                    <td></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5"></td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: center; font-weight: 700; font-size: large">List of Final External Reviewers selected by Scientific Council Subcommittee</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:GridView ID="gvExtRevList4VR" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                            CssClass="table-condensed"
                            ItemType="BL.Data.Form_FinalExtRev"
                             EnableModelValidation="True" ForeColor="#333333">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate><%# Container.DataItemIndex + 1%> </ItemTemplate>
                                </asp:TemplateField >
                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate><%# Item.Source%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate><%# Item.ExternalReviewer.NameString%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank">
                                    <ItemTemplate><%# Item.ExternalReviewer.Rank%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MailingAddress">
                                    <ItemTemplate><%# Item.ExternalReviewer.MailingAddress%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TotalPublications">
                                    <ItemTemplate><%# Item.ExternalReviewer.TotalPublications%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NoOfJournals">
                                    <ItemTemplate><%# Item.ExternalReviewer.NoOfJournals%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HIndex">
                                    <ItemTemplate><%# Item.ExternalReviewer.HIndex%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Citations">
                                    <ItemTemplate><%# Item.ExternalReviewer.Citations%> </ItemTemplate>
                                </asp:TemplateField>                     
                                <asp:TemplateField HeaderText="Total journal publications and H-index">
                                    <ItemTemplate><asp:label runat="server" Text='<%# Item.ExternalReviewer.HIndex.HasValue && Item.ExternalReviewer.NoOfJournals.HasValue 
                                                          ? (Item.ExternalReviewer.HIndex.Value + Item.ExternalReviewer.NoOfJournals.Value).ToString() :"" %>'></asp:label></ItemTemplate>                                    
                                </asp:TemplateField>                               
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="odsExtRevList4VR" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="FormTableAdapters.Form_FinalRefreeTableAdapter">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>                       

                        <br />
                    </td>
                </tr>

            </table>
            <p style="page-break-after: always; text-align: center;"></p>
            <table>
                <tr>
                    <td colspan="5" style="text-align: center; font-weight: 700; font-size: large">List of Suggested External Reviewers Suggested by Candidate, Department and College</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:GridView ID="gvSugExtRevList" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                            CssClass="table-condensed" ItemType="BL.Data.Form_ExtRev"
                            EnableModelValidation="True" ForeColor="#333333">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                               <%-- <asp:TemplateField>
                                    <ItemTemplate><%# Container.DataItemIndex +1%> </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField>
                                    <ItemTemplate><%# Item.Serial + 1%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Suggested by" SortExpression="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelSuggestedBy" runat="server" Text='<%#GetSuggestedBy(Item.Type) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate><%# Item.ExternalReviewer.NameString%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank">
                                    <ItemTemplate><%# Item.ExternalReviewer.Rank%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MailingAddress">
                                    <ItemTemplate><%# Item.ExternalReviewer.MailingAddress%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TotalPublications">
                                    <ItemTemplate><%# Item.ExternalReviewer.TotalPublications%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NoOfJournals">
                                    <ItemTemplate><%# Item.ExternalReviewer.NoOfJournals%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HIndex">
                                    <ItemTemplate><%# Item.ExternalReviewer.HIndex%> </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Citations">
                                    <ItemTemplate><%# Item.ExternalReviewer.Citations%> </ItemTemplate>
                                </asp:TemplateField>                     
                                <asp:TemplateField HeaderText="Total journal publications and H-index">
                                    <ItemTemplate><asp:label runat="server" Text='<%# Item.ExternalReviewer.HIndex.HasValue && Item.ExternalReviewer.NoOfJournals.HasValue 
                                                          ? (Item.ExternalReviewer.HIndex.Value + Item.ExternalReviewer.NoOfJournals.Value).ToString() :"" %>'></asp:label></ItemTemplate>                                    
                                </asp:TemplateField>                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>                      
                        <br />
                    </td>
                </tr>

            </table>







        </div>
    </form>
</body>
</html>
