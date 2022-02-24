<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentFaculty.ascx.cs" Inherits="Controls_DepartmentFaculty" %>
<h3>
List of Faculty in all the Departments in the College (for Reference):<br />
</h3>
<table id="tableListOfDeptFaculty">
    <thead style="background-color:#1e9966;color:white">
        <tr><td>Serial No</td><td>Name </td><td>Email</td><td>Rank</td><td>Department</td><td>Specialty</td></tr>
    </thead>
    <asp:Repeater runat="server" ID="rptDeptFaculty" ItemType="BL.Data.Employee">
        <ItemTemplate>  
            <tr>
               <td ><%# Container.ItemIndex +1 %>   </td>
                <td><%# Item.NameString %>    </td>
                <td><%# Item.Email %>    </td>
                <td><%# Item.Rank %>    </td>
                <td><%# Item.Department1.NameString%>    </td>
                <td><%# Item.Specialty%>    </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    
</table>
<%--<asp:GridView ID="rptDeptFaculty" runat="server" AllowSorting="True" AutoGenerateColumns="False" class="table"
    CellPadding="4"  ForeColor="#333333" GridLines="None" Font-Size="Smaller" OnSorting="gvDeptFaculty_Sorting">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="NameString" HeaderText="Name" />
        <asp:BoundField DataField="Rank" HeaderText="Rank" />
        <asp:BoundField DataField="Specialty" HeaderText="Specialty"  />
    </Columns>
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#1e9966" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
</asp:GridView>--%>
<%--<asp:SqlDataSource ID="SqlDataSourceFaculty" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
    
    SelectCommand="SELECT Email, Name, Rank FROM Employee AS e WHERE (Department = (SELECT e1.Department FROM Employee AS e1 INNER JOIN Application AS app ON app.EmployeeID = e1.EmployeeID WHERE (app.ApplicationID = @ApplicationID))) AND (Status = 'Active')">
    <SelectParameters>
        <asp:Parameter Name="ApplicationID" />
    </SelectParameters>
</asp:SqlDataSource>--%>
