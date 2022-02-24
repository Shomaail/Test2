<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddExtRev.ascx.cs" Inherits="Controls_AddExtRev" %>
<p>
    <h3>Old List of External Reviewers
    </h3> 
   
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem Value="9">Select a List</asp:ListItem>
        <asp:ListItem Value="1">Department List</asp:ListItem>
        <asp:ListItem Value="2">College List</asp:ListItem>
        <asp:ListItem Value="0">Applicants List</asp:ListItem>
    </asp:DropDownList>
    <br />
</p>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" 
    GridLines="None" onselectedindexchanged="GridView1_SelectedIndexChanged" 
    EnableModelValidation="True">
    <RowStyle BackColor="#EFF3FB" />
    <Columns>
        <asp:TemplateField HeaderText="Serial #">
        <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
        </asp:TemplateField>        
        <asp:CommandField ShowSelectButton="True" />
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
                    <asp:BoundField DataField="TotalPublications" HeaderText="Total Publications" 
            SortExpression="TotalPublications" ConvertEmptyStringToNull="False" 
            NullDisplayText="0" />
                    <asp:BoundField DataField="NoOfJournals" HeaderText="No Of Journals" 
            SortExpression="NoOfJournals" ConvertEmptyStringToNull="False" 
            NullDisplayText="0" />
                    <asp:BoundField DataField="HIndex" HeaderText="H-Index" 
            SortExpression="HIndex" ConvertEmptyStringToNull="False" 
            NullDisplayText="0" />
         <asp:BoundField DataField="HIndex" HeaderText="Citations" 
            SortExpression="Citations" ConvertEmptyStringToNull="False" 
            NullDisplayText="0" />
    </Columns>
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <EditRowStyle BackColor="#2461BF" />
    <AlternatingRowStyle BackColor="White" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
    
    SelectCommand="SELECT Form_Refree.Name, Form_Refree.Rank, Form_Refree.MailingAddress, Form_Refree.Email, Form_Refree.Major, Form_Refree.Speciality, Form_Refree.PhoneAndFax, Form_Refree.ActiveAreaOfResearch, Form_Refree.PrevAreaOfResearch, Form_Refree.Webpage, Form_Refree.Comments, Form_Refree.TotalPublications, Form_Refree.NoOfJournals, Form_Refree.HIndex FROM Form_Refree INNER JOIN Application ON Form_Refree.ApplicationID = Application.ApplicationID INNER JOIN Employee ON Application.EmployeeID = Employee.EmployeeID WHERE (Employee.Department = @Department) AND (Form_Refree.Type = @Type)">
    <SelectParameters>
        <asp:Parameter Name="Department" />
        <asp:ControlParameter ControlID="DropDownList1" DefaultValue="1" Name="Type" 
            PropertyName="SelectedValue" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:HiddenField ID="hdnApplicationID" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />

    <asp:HiddenField ID="hdnAppDepartment" runat="server" />
<uc10:Alert ID="Alert1" runat="server" />

