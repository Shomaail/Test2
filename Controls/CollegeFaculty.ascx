<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CollegeFaculty.ascx.cs" Inherits="Controls_DepartmentFaculty" %>
<h3 style="border-bottom: 1px solid gray; padding-bottom: 5px">
    <em>List of College Faculty (for Reference):
    </em></h3>
College:
<asp:DropDownList ID="ddlDept" runat="server" ItemType="Department"
    DataTextField="Text"
    DataValueField="Value" AutoPostBack="True"
    OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
</asp:DropDownList>
<br />
<table id="tableEmployee">
    <thead>
        <tr>
                <th class="PointsHeader"></th>
                <th class="PointsHeader">Email</th>                
                <th class="PointsHeader">Name</th>
                <th class="PointsHeader">Rank</th>
                <th class="PointsHeader">Department</th>                
        </tr>

    </thead>

    <asp:Repeater ID="rptEmployee" runat="server" ItemType="BL.Data.Employee">
        <ItemTemplate>
            <tr>
                <td><%# Container.ItemIndex +1 %></td>
                <td><%# Item.Email %></td>
                <td><%# Item.NameString %></td>
                <td><%# Item.Rank %></td>
                <td><%# Item.Department1.Name %></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<%--<asp:GridView ID="gvEmployee" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table" ItemType="BL.Data.Employee"
    CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Smaller">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle BackColor="#2461BF" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#0088ce" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
    <Columns>
         <asp:TemplateField>
        <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="EmailID">
            <ItemTemplate><%# Item.Email %> </ItemTemplate>
        </asp:TemplateField>
               <asp:TemplateField HeaderText="Name">
            <ItemTemplate><%# Item.NameString %> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Rank">
            <ItemTemplate><%# Item.Rank %> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Department">
            <ItemTemplate><%# Item.Department1.Name%> </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>--%>

<br />
