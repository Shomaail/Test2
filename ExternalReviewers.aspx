<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExternalReviewers.aspx.cs" Inherits="ExternalReviewers" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>
<script type="text/javascript">

    function Confirm_NewApp() {
        if (confirm("Are you sure you want to start a new application?") == true)
            return true;

        else

            return false;
    }
</script>
<html lang="en">
<head id="Head1" runat="server">
    <title>Faculty Promotion System</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="images/favicon.png">
    <link rel="SHORTCUT ICON" href="images/favicon.png" />
    <!-- Bootstrap Core CSS -->
    <link href="App_Themes/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="App_Themes/3-col-portfolio.css" rel="stylesheet">
</head>
<body dir="ltr">
    <form id="form1" runat="server">
        <div style="border-style: solid; border-color: inherit; border-width: 0px; margin-left: 20px; margin-top: 20px; margin-right: 20px; height: 93px; background-color: #F8F8F8;"
            id="divTop" runat="server">

            <div style="float: left">
                <img alt="KFUPM Logo" style="width: 405px; height: 93px;" src="images/logo.png" />
            </div>
            <div style="float: left; padding-left: 20px">
                <h1>Faculty Promotion System</h1>
            </div>
        </div>
        <div style="border: 0px solid; margin-left: 10px; margin-top: 78px;">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Main.aspx">Return</asp:HyperLink>

            <br />
            <br />

            <asp:LinkButton ID="linkButtonExportExcel" runat="server" Text="Export To Excel"
                OnClick="linkButtonExportExcel_Click" />
            <h1>List Of All External Reviewers</h1>

            <asp:GridView ID="GridViewExtReviewers" runat="server" CssClass="table-condensed"
                AutoGenerateColumns="False" CellPadding="4"
                DataSourceID="ObjectDataSourceExtRev" EnableModelValidation="True" ForeColor="#333333"
                GridLines="None" OnRowCommand="GridViewExtReviewers_RowCommand"
                AllowSorting="True">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Serial #">
                        <ItemTemplate>
                            <%# Container.DataItemIndex +1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                        <ItemTemplate>
                            <asp:Label ID="LabelName" runat="server" Text='<%# Eval("Name")%>'
                                Style="text-align: left" />
                            <br />
                            <asp:Button ID="HyperLinkName" runat="server" CommandName="ShowAllRecords" CommandArgument='<%# Eval("Name")%>'
                                Text='Details' BackColor="#999999" BorderStyle="None" ToolTip="Fetch All Records with this name and detail"
                                BorderWidth="0px" EnableTheming="True" Font-Italic="False" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Major" HeaderText="Major" SortExpression="Major" />
                    <asp:BoundField DataField="ActiveAreaOfResearch"
                        HeaderText="ActiveAreaOfResearch" SortExpression="ActiveAreaOfResearch" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSourceExtRev" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetUniqNames"
                TypeName="ViewsTableAdapters.vw_ExtReviewersTableAdapter"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSourceSelExtRev" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetDetailByName"
                TypeName="ViewsTableAdapters.vw_ExtReviewersTableAdapter">
                <SelectParameters>
                    <asp:Parameter Name="Name" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:GridView ID="GridViewSelectedExtRev" runat="server" CellPadding="4"
                AutoGenerateColumns="False" CssClass="table-condensed"
                DataSourceID="ObjectDataSourceSelExtRev" EnableModelValidation="True"
                ForeColor="#333333" GridLines="None" ShowFooter="True"
                OnSelectedIndexChanged="GridViewSelectedExtRev_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <FooterTemplate>
                            <asp:Button ID="ButtonReturn" runat="server" Text="Return"
                                OnClick="ButtonReturn_Click" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False"
                                CommandName="Select" Text="Select"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                    <asp:BoundField DataField="MailingAddress" HeaderText="MailingAddress"
                        SortExpression="MailingAddress" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Major" HeaderText="Major" SortExpression="Major" />
                    <asp:BoundField DataField="Speciality" HeaderText="Speciality"
                        SortExpression="Speciality" />
                    <asp:BoundField DataField="PhoneAndFax" HeaderText="PhoneAndFax"
                        SortExpression="PhoneAndFax" />
                    <asp:BoundField DataField="ActiveAreaOfResearch"
                        HeaderText="ActiveAreaOfResearch" SortExpression="ActiveAreaOfResearch" />
                    <asp:BoundField DataField="PrevAreaOfResearch" HeaderText="PrevAreaOfResearch"
                        SortExpression="PrevAreaOfResearch" />
                    <asp:BoundField DataField="Webpage" HeaderText="Webpage"
                        SortExpression="Webpage" />
                    <asp:BoundField DataField="Comments" HeaderText="Comments"
                        SortExpression="Comments" />
                    <asp:BoundField DataField="TotalPublications" HeaderText="TotalPublications"
                        SortExpression="TotalPublications" />
                    <asp:BoundField DataField="NoOfJournals" HeaderText="NoOfJournals"
                        SortExpression="NoOfJournals" />
                    <asp:BoundField DataField="HIndex" HeaderText="HIndex"
                        SortExpression="HIndex" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
