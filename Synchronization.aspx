<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Synchronization.aspx.cs" Inherits="Synchronization" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CellPadding="4" DataSourceID="ObjectDataSource1" ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" SortExpression="EmployeeID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                <asp:BoundField DataField="College" HeaderText="College" SortExpression="College" />
                <asp:BoundField DataField="KFUPMEmail" HeaderText="KFUPMEmail" SortExpression="KFUPMEmail" />
                <asp:BoundField DataField="KFUPMUserID" HeaderText="KFUPMUserID" SortExpression="KFUPMUserID" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                <asp:BoundField DataField="POBox" HeaderText="POBox" SortExpression="POBox" />
                <asp:BoundField DataField="JoiningDate" HeaderText="JoiningDate" SortExpression="JoiningDate" />
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="DataSet1TableAdapters.EmployeeERPTableAdapter">
        </asp:ObjectDataSource>
        <br />
        <br />
        <br />
        &nbsp;<asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2"
            DataKeyNames="EmployeeID" DataSourceID="ObjectDataSource2" ForeColor="Black"
            GridLines="None">
            <FooterStyle BackColor="Tan" />
            <Columns>
                <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" ReadOnly="True" SortExpression="EmployeeID" />
                <asp:BoundField DataField="KFUPMEmail" HeaderText="KFUPMEmail" SortExpression="KFUPMEmail" />
                <asp:BoundField DataField="KFUPMUserID" HeaderText="KFUPMUserID" SortExpression="KFUPMUserID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                <asp:BoundField DataField="College" HeaderText="College" SortExpression="College" />
                <asp:BoundField DataField="POBox" HeaderText="POBox" SortExpression="POBox" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                <asp:BoundField DataField="JoinDate" HeaderText="JoinDate" SortExpression="JoinDate" />
            </Columns>
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DeleteMethod="Delete"
            InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
            TypeName="DataSet1TableAdapters.EmployeeTableAdapter" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_EmployeeID" Type="String" />
                <asp:Parameter Name="Original_KFUPMEmail" Type="String" />
                <asp:Parameter Name="Original_KFUPMUserID" Type="String" />
                <asp:Parameter Name="Original_Name" Type="String" />
                <asp:Parameter Name="Original_Rank" Type="String" />
                <asp:Parameter Name="Original_Department" Type="String" />
                <asp:Parameter Name="Original_College" Type="String" />
                <asp:Parameter Name="Original_POBox" Type="String" />
                <asp:Parameter Name="Original_Phone" Type="String" />
                <asp:Parameter Name="Original_JoinDate" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="EmployeeID" Type="String" />
                <asp:Parameter Name="KFUPMEmail" Type="String" />
                <asp:Parameter Name="KFUPMUserID" Type="String" />
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="Rank" Type="String" />
                <asp:Parameter Name="Department" Type="String" />
                <asp:Parameter Name="College" Type="String" />
                <asp:Parameter Name="POBox" Type="String" />
                <asp:Parameter Name="Phone" Type="String" />
                <asp:Parameter Name="JoinDate" Type="String" />
                <asp:Parameter Name="Original_EmployeeID" Type="String" />
                <asp:Parameter Name="Original_KFUPMEmail" Type="String" />
                <asp:Parameter Name="Original_KFUPMUserID" Type="String" />
                <asp:Parameter Name="Original_Name" Type="String" />
                <asp:Parameter Name="Original_Rank" Type="String" />
                <asp:Parameter Name="Original_Department" Type="String" />
                <asp:Parameter Name="Original_College" Type="String" />
                <asp:Parameter Name="Original_POBox" Type="String" />
                <asp:Parameter Name="Original_Phone" Type="String" />
                <asp:Parameter Name="Original_JoinDate" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="EmployeeID" Type="String" />
                <asp:Parameter Name="KFUPMEmail" Type="String" />
                <asp:Parameter Name="KFUPMUserID" Type="String" />
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="Rank" Type="String" />
                <asp:Parameter Name="Department" Type="String" />
                <asp:Parameter Name="College" Type="String" />
                <asp:Parameter Name="POBox" Type="String" />
                <asp:Parameter Name="Phone" Type="String" />
                <asp:Parameter Name="JoinDate" Type="String" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <br />
        <br />
        Dept Organization<br />
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
            DataSourceID="ObjectDataSource3" ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundField DataField="DEPARTMENT" HeaderText="DEPARTMENT" SortExpression="DEPARTMENT" />
                <asp:BoundField DataField="DEPARTMENT_A" HeaderText="DEPARTMENT_A" SortExpression="DEPARTMENT_A" />
                <asp:BoundField DataField="SHORT_DEPARTMENT" HeaderText="SHORT_DEPARTMENT" SortExpression="SHORT_DEPARTMENT" />
                <asp:BoundField DataField="CHAIRMAN_NUMBER" HeaderText="CHAIRMAN_NUMBER" SortExpression="CHAIRMAN_NUMBER" />
                <asp:BoundField DataField="CHAIRMAN_NAME" HeaderText="CHAIRMAN_NAME" SortExpression="CHAIRMAN_NAME" />
                <asp:BoundField DataField="CHAIRMAN_ACTUAL_POSITION" HeaderText="CHAIRMAN_ACTUAL_POSITION"
                    SortExpression="CHAIRMAN_ACTUAL_POSITION" />
                <asp:BoundField DataField="COLLEGE" HeaderText="COLLEGE" SortExpression="COLLEGE" />
                <asp:BoundField DataField="COLLEGE_A" HeaderText="COLLEGE_A" SortExpression="COLLEGE_A" />
                <asp:BoundField DataField="SHORT_COLLEGE" HeaderText="SHORT_COLLEGE" SortExpression="SHORT_COLLEGE" />
                <asp:BoundField DataField="DEAN_NUMBER" HeaderText="DEAN_NUMBER" SortExpression="DEAN_NUMBER" />
                <asp:BoundField DataField="DEAN_NAME" HeaderText="DEAN_NAME" SortExpression="DEAN_NAME" />
                <asp:BoundField DataField="DEAN_ACTUAL_POSITION" HeaderText="DEAN_ACTUAL_POSITION"
                    SortExpression="DEAN_ACTUAL_POSITION" />
            </Columns>
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="DataSet1TableAdapters.DepartmentManagerERPTableAdapter">
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
