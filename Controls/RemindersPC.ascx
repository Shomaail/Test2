<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RemindersPC.ascx.cs" Inherits="Controls_RemindersPC" %>
    <h3>Reminders in Promotion Committee Formation</h3>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table"
    CellPadding="4" DataSourceID="SqlDataSourceRemindersPC" ForeColor="#333333" 
    GridLines="None" onselectedindexchanged="GridView1_SelectedIndexChanged" 
        DataKeyNames="AutoNumberPK">
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <Columns>
        <asp:CommandField SelectText="Show Email" ShowSelectButton="True" />
    <asp:TemplateField>
        <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
        </asp:TemplateField>    
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
        <asp:BoundField DataField="Department" HeaderText="Department" 
            SortExpression="Department" />
        <asp:BoundField DataField="Willingness Status" HeaderText="Willingness Status" 
            SortExpression="Willingness Status" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Sending Date" HeaderText="Sending Date" 
            SortExpression="Sending Date" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="No. Of Reminders" HeaderText="No. Of Reminders" 
            SortExpression="No. Of Reminders" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="Reminder Status" 
            SortExpression="Reminder Status">
            <ItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="server" 
                    Checked='<%# !CheckStatus(Eval("[Reminder Status]").ToString()) %>' 
                    AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged" 
                    ToolTip="Click here to Start or Stop the Reminders" />
            </ItemTemplate>
            
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            
        </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <EmptyDataTemplate>
        There is no record available
    </EmptyDataTemplate>
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
<HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
    <EditRowStyle BackColor="#999999" />
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
</asp:GridView>
        
<asp:SqlDataSource ID="SqlDataSourceRemindersPC" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
    
        SelectCommand="SELECT * FROM [vw_RemindersPC] WHERE ([ApplicationID] = @ApplicationID)">
    <SelectParameters>
        <asp:Parameter Name="ApplicationID" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
    <asp:HiddenField ID="hdnApplicationID" runat="server" />

    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" class="table-condensed"
        CellPadding="4" DataSourceID="SqlDataSourceRemindersPC" ForeColor="#333333" 
        GridLines="None" Height="50px" Width="125px">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
        <RowStyle BackColor="#EFF3FB" />
        <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <Fields>
            <asp:TemplateField HeaderText="Message" SortExpression="Message">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Height="134px" 
                        Text='<%# Cryptography.Decrypt(Eval("Message").ToString()) %>' TextMode="MultiLine" Width="821px"></asp:TextBox>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Cryptography.Decrypt(Eval("Message").ToString()) %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Cryptography.Decrypt(Eval("Message").ToString()) %>'></asp:TextBox>
                </InsertItemTemplate>
            </asp:TemplateField>
        </Fields>
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:DetailsView>


