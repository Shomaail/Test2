<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="DepartmentManagers.aspx.cs" Inherits="Admin_DepartmentManagers" Title="Faculty Promotion System" %>
<%@ Register src="../Controls/ComposeEmail.ascx" tagname="ComposeEmail" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        CellPadding="4" DataKeyNames="Department,RoleID,EmployeeID" DataSourceID="ManagersDataSource"
        Font-Size="Smaller" ForeColor="#333333" GridLines="None" 
        OnRowDataBound="GridView1_RowDataBound" 
        OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
        EnableModelValidation="True" onrowcommand="GridView1_RowCommand">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>  
         <%--   <asp:TemplateField>
                <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:CommandField ShowSelectButton="True" />            
            <asp:BoundField DataField="Department" HeaderText="Department" ReadOnly="True" SortExpression="Department" />
            <asp:BoundField DataField="RoleID" HeaderText="RoleID" ReadOnly="True" SortExpression="RoleID" Visible="False" />
            <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
            <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" SortExpression="EmployeeID" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Email" HeaderText="ITC ID" SortExpression="Email" />
            <asp:BoundField DataField="DeputyEmployeeID" HeaderText="Deputy ITC ID" SortExpression="DeputyEmployeeID" />
            <asp:BoundField DataField="DeputyEmail" DataFormatString="{0}" HeaderText="Deputy Email"
                SortExpression="DeputyEmail" />
            <asp:ButtonField CommandName="ShowEmailPopup" HeaderText="" Text="Send a welcome note" >
                <ItemStyle horizontalAlign="Center" />                        
            </asp:ButtonField>
        </Columns>
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    
    <asp:ObjectDataSource ID="ManagersDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="PromAdminTableAdapters.DepartmentManagersTableAdapter">
    </asp:ObjectDataSource>
    <uc10:Alert ID="Alert1" runat="server" />
    <br />
    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" CellPadding="4"
        DataKeyNames="Department,RoleID" DataSourceID="SelectedManagerDataSource" ForeColor="#333333"
        GridLines="None" Height="50px" Width="393px">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <Fields>
            <asp:BoundField DataField="Department" HeaderText="Department" ReadOnly="True" SortExpression="Department" />
            <asp:BoundField DataField="RoleID" HeaderText="RoleID" ReadOnly="True" SortExpression="RoleID" Visible="False" />
            <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" ReadOnly="True" />
            <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" SortExpression="EmployeeID" ReadOnly="True" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ReadOnly="True" />
            <asp:TemplateField HeaderText="ITC ID" SortExpression="Email">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("Email") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Deputy Employee ID" SortExpression="DeputyEmployeeID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DeputyEmployeeID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("DeputyEmployeeID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DeputyEmail" DataFormatString="{0}" HeaderText="Deputy Email"
                SortExpression="DeputyEmail" />
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" class="btn btn-primary btn-md" 
                        Text="Update" />&nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False"
                            CommandName="Cancel" OnClick="Button1_Click" Text="Cancel" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Edit" class="btn btn-primary btn-md" 
                        Text="Edit" />&nbsp;<asp:Button ID="Button1" runat="server" CausesValidation="false"
                            CommandName="Cancel" OnClick="Button1_Click" Text="Cancel" />
                </ItemTemplate>
            </asp:TemplateField>
        </Fields>
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" Width="170px" />
    <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:DetailsView>
    <asp:ObjectDataSource ID="SelectedManagerDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataByDepartment" TypeName="PromAdminTableAdapters.DepartmentManagersTableAdapter"
        UpdateMethod="AssignManager" OnSelecting="SelectedManagerDataSource_Selecting" OnUpdating="SelectedManagerDataSource_Updating">
        <UpdateParameters>
            <asp:Parameter Name="EmployeeID" Type="String" />
            <asp:Parameter Name="DeputyEmployeeID" Type="String" />
            <asp:Parameter Name="original_Department" Type="String" />
            <asp:Parameter Name="original_RoleID" Type="Byte" />
            <asp:Parameter Name="original_EmployeeID" Type="String" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="Department" Type="String" />
            <asp:Parameter DefaultValue="0" Name="RoleID" Type="Byte" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <br />
</asp:Content>

