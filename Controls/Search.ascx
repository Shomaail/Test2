<%@ Control Language="C#" CodeFile="Search.ascx.cs" Inherits="Controls_Search" %>
<script type="text/javascript">
    $(document).ready(function () {
    $('#tableSearch').DataTable({
                               "pageLength": 100,
                                    });
    });

    </script>
<%--Write the Search String in the test Box below and Click on the Search Button.<br />
<br />
<asp:TextBox ID="TextBoxSearch" runat="server" OnTextChanged="ButtonSearch_Click" Width="449px" ></asp:TextBox>
<asp:Button ID="ButtonSearch" runat="server" CssClass="btn btn-default" OnClick="ButtonSearch_Click" Text="Search" />
<br />--%>
<br />
<table id="tableSearch">
    <thead><tr><td>S. No. </td>
        <td>ID</td>
        <td>Email</td>
        <td>Name</td>
        <td>Rank</td>
        <td>Phone</td>
        <td>Status</td>
        <td>Department</td>
        <td>College</td></tr></thead>
    <asp:Repeater runat="server" ItemType="BL.Data.Employee" ID="rptSearch">
        <ItemTemplate> <tr>
        <td>  <%# Container.ItemIndex +1 %>   </td>
            <td>  <%# Item.EmployeeID%>   </td>
            <td>  <%# Item.Email%>   </td>
            <td>  <%# Item.NameString%>   </td>
            <td>  <%# Item.Rank%>   </td>
            <td>  <%# Item.Phone%>   </td>
            <td>  <%# Item.Status%>   </td>
            <td>  <%# Item.DepartmentID != null ? Item.Department1.NameString: ""%>   </td>
            <td>  <%# Item.ParentDept%>   </td>
    </tr> </ItemTemplate>
    </asp:Repeater>
    
</table>
<%--<asp:GridView ID="GridViewSrcResult" runat="server" CellPadding="4" ItemType="BL.Data.Employee"
    EmptyDataText="There is no search result" ForeColor="#333333" GridLines="Horizontal" AllowPaging="True" AllowSorting="True" HorizontalAlign="Center" 
    AutoGenerateColumns="false" CssClass="table"
    OnPageIndexChanging="GridViewSrcResult_PageIndexChanging" OnSorting="GridViewSrcResult_Sorting" PageSize="50">
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <EditRowStyle BackColor="#999999" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"  />
    <HeaderStyle BackColor="#0088ce" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:TemplateField HeaderText="Serial #"> 
            <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ID"> 
            <ItemTemplate><%# Item.EmployeeID%> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="EmailID"> 
            <ItemTemplate><%# Item.Email %> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name"> 
            <ItemTemplate><%# Item.NameString %> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Rank"> 
            <ItemTemplate><%# Item.Rank%> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Phone"> 
            <ItemTemplate><%# Item.Phone%> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Rank"> 
            <ItemTemplate><%# Item.Status%> </ItemTemplate>
        </asp:TemplateField>        
        <asp:TemplateField HeaderText="Dept"> 
            <ItemTemplate><%# Item.Department%> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="College/Parent Dept."> 
            <ItemTemplate><%# Item.College%> </ItemTemplate>
        </asp:TemplateField>

    </Columns>

</asp:GridView>--%>
&nbsp;
