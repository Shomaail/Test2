<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ActionLog.aspx.cs" Inherits="Admin_ActionLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Action_LogID" DataSourceID="SqlDataSource1" EnableModelValidation="True"
        CssClass="table" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="50">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Action_LogID" HeaderText="Action_LogID" InsertVisible="False" ReadOnly="True" SortExpression="Action_LogID" />
            <asp:BoundField DataField="Detail" HeaderText="Detail" SortExpression="Detail" />
            <asp:BoundField DataField="TimeStamp" HeaderText="TimeStamp" SortExpression="TimeStamp" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
         <PagerStyle BackColor="#1A1718" ForeColor="White" HorizontalAlign="Center"
                        Font-Bold="True" Font-Italic="False" Font-Names="Copperplate Gothic Light"
                        Font-Size="X-Large" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Action_Log] ORDER BY [TimeStamp] DESC"></asp:SqlDataSource>
</asp:Content>

