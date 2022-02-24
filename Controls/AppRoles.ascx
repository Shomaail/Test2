<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AppRoles.ascx.cs" Inherits="Controls_AppRoles" %>
<asp:GridView ID="gvAppRole" runat="server" AutoGenerateColumns="False" class="table"
    CellPadding="4" DataSourceID="SqlDataSource1" EnableModelValidation="True"  DataKeyNames="ApplicationID,RoleID"
    ForeColor="#333333" GridLines="None">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:CommandField ShowEditButton="True" />
        <asp:BoundField DataField="ApplicationID" HeaderText="ApplicationID" 
            SortExpression="ApplicationID" Visible="False" ReadOnly="True" />
        <asp:BoundField DataField="RoleID" HeaderText="RoleID" ReadOnly="True" 
            SortExpression="RoleID" Visible="False" />
        <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" 
            SortExpression="EmployeeID" Visible="False" />
        <asp:BoundField DataField="Role" HeaderText="Role" ReadOnly="True" 
            SortExpression="Role" />
        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" 
            ReadOnly="True" />
        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" 
            SortExpression="Name" />
        <asp:BoundField DataField="Rank" HeaderText="Rank" ReadOnly="True" 
            SortExpression="Rank" />
        <asp:BoundField DataField="Department" HeaderText="Department" ReadOnly="True" 
            SortExpression="Department" />
        <asp:BoundField DataField="KFUPMUserID" HeaderText="Portal Username" 
            SortExpression="KFUPMUserID" />
    </Columns>
    <EditRowStyle BackColor="#999999" />
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>
<asp:ObjectDataSource ID="odsAppRoles" runat="server" SelectMethod="GetApplicationRole" TypeName="BAL" UpdateMethod="UpdateAppRoles">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="-1" Name="applicationID" QueryStringField="applicationID" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="ApplicationID" Type="Int32" />
        <asp:Parameter Name="RoleID" Type="Byte" />
        <asp:Parameter Name="EmployeeID" Type="String" />
        <asp:Parameter Name="NewEmployeeID" Type="String" />
    </UpdateParameters>
</asp:ObjectDataSource>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
    SelectCommand="SELECT ApplicationID, RoleID, KFUPMUserID, Title, Name, Rank, Department, Role, EmployeeID FROM vw_Application_Role WHERE (ApplicationID = @ApplicationID) order by Role" 
    
    UpdateCommand="UPDATE Application_Role SET EmployeeID = (SELECT EmployeeID FROM Employee WHERE (KFUPMUserID = @KFUPMUserID)) WHERE (ApplicationID = @ApplicationID) AND (RoleID = @RoleID)">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" 
            QueryStringField="applicationID" Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="KFUPMUserID" Type="String"/>
        <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" 
            QueryStringField="applicationID" Type="Int32" />
        <asp:Parameter Name="RoleID" Type="Byte"/>
    </UpdateParameters>
</asp:SqlDataSource>


