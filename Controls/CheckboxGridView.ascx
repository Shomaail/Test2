<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckboxGridView.ascx.cs" Inherits="Controls_CheckboxGridView" %>
&nbsp;&nbsp;
<asp:BulletedList ID="BulletedList1" runat="server">
    <asp:ListItem>qwqw</asp:ListItem>
    <asp:ListItem>qwqw</asp:ListItem>
    <asp:ListItem>qwqw</asp:ListItem>
    <asp:ListItem>qwqw</asp:ListItem>
    <asp:ListItem>qwqw</asp:ListItem>
</asp:BulletedList>
&nbsp;&nbsp;<br />
<br />
<br />
<asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
   AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="EmployeeID" 
   DataSourceID="ObjectDataSource1" Width="366px" CellPadding="4" 
   ForeColor="#333333" GridLines="None">
 <Columns>
     <asp:CommandField ShowSelectButton="True" />
     <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" ReadOnly="True" SortExpression="EmployeeID" />
     <asp:BoundField DataField="KFUPMEmail" HeaderText="KFUPMEmail" SortExpression="KFUPMEmail" />
     <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
     <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
     <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
     <asp:BoundField DataField="College" HeaderText="College" SortExpression="College" />
     <asp:BoundField DataField="POBox" HeaderText="POBox" SortExpression="POBox" />
     <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
     <asp:BoundField DataField="JoinDate" HeaderText="JoinDate" SortExpression="JoinDate" />
     <asp:BoundField DataField="KFUPMUserID" HeaderText="KFUPMUserID" SortExpression="KFUPMUserID" />
   <asp:TemplateField HeaderText="Select">
    <ItemTemplate>
       <asp:CheckBox ID="chkSelect" runat="server" />
    </ItemTemplate>
    <HeaderTemplate>
    </HeaderTemplate>
   </asp:TemplateField>
 </Columns>

 <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
 <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
 <PagerStyle BackColor="#284775" ForeColor="White" 
                           HorizontalAlign="Center" />
 <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
 <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
 <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <EditRowStyle BackColor="#999999" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetEmployees" TypeName="PromotionTableAdapters.EmployeeTableAdapter">
</asp:ObjectDataSource>

