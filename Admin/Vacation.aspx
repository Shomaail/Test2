<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Vacation.aspx.cs" Inherits="Admin_Vacation" Title="Faculty Promotion System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" DataKeyNames="PeriodID" 
        DataSourceID="SqlDataSourceReminderFreeze" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" ShowFooter="True" >
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                        CommandName="Edit" Text="Edit"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                        CommandName="Delete" Text="Delete"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                        CommandName="Update" Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                        CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="ButtonAddNewRow" runat="server" CommandName="AddRecord" Text="Add" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PeriodID" HeaderText="PeriodID" 
                InsertVisible="False" ReadOnly="True" SortExpression="PeriodID" />
            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="TextBoxDescription" runat="server"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="StartDate" SortExpression="StartDate">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("StartDate") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Calendar ID="CalendarStartDate" runat="server" BackColor="White" 
                        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                        Width="200px">
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        
                    </asp:Calendar>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EndDate" SortExpression="EndDate">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("EndDate") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Calendar ID="CalendarEndDate" runat="server" BackColor="White" 
                        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                        Width="200px">
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    </asp:Calendar>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourceReminderFreeze" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FPSDBConnectionString %>" 
        DeleteCommand="DELETE FROM RemindersFreeze WHERE (PeriodID = @PeriodID)" 
        InsertCommand="INSERT INTO RemindersFreeze(Description, StartDate, EndDate) VALUES (@Description, @StartDate, @EndDate)" 
        SelectCommand="SELECT * FROM [RemindersFreeze] order by  StartDate DESC" 
        UpdateCommand="UPDATE RemindersFreeze SET Description = @Description, StartDate = @StartDate, EndDate = @EndDate WHERE (PeriodID = @PeriodID)">
        <DeleteParameters>
            <asp:Parameter Name="PeriodID" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Description" />
            <asp:Parameter Name="StartDate" />
            <asp:Parameter Name="EndDate" />
            <asp:Parameter Name="PeriodID" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Description" />
            <asp:Parameter Name="StartDate" />
            <asp:Parameter Name="EndDate" />
        </InsertParameters>
    </asp:SqlDataSource>

</asp:Content>

