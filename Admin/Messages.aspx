<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Messages.aspx.cs" Inherits="Admin_Messages" Title="Edit Messages Templates" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="font-size: small">
        &nbsp;<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ActionID,NextTaskID"
            DataSourceID="MessagesDataSource" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="ActionID" HeaderText="ActionID" ReadOnly="True" SortExpression="ActionID" Visible="False" />
                <asp:BoundField DataField="NextTaskID" HeaderText="NextTaskID" ReadOnly="True" SortExpression="NextTaskID" Visible="False" />
                <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
                <asp:BoundField DataField="Task" HeaderText="Task" SortExpression="Task" />
                <asp:BoundField DataField="Action" HeaderText="Action" SortExpression="Action" />
                <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" Visible="False" />
                <asp:BoundField DataField="NextTask" HeaderText="NextTask" SortExpression="NextTask" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <asp:ObjectDataSource ID="MessagesDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="PromAdminTableAdapters.ActionMessagesTableAdapter">
        </asp:ObjectDataSource>
        &nbsp;&nbsp;<br />
        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" CellPadding="4"
            DataKeyNames="ActionID,NextTaskID" DataSourceID="ObjectDataSource1" ForeColor="#333333"
            GridLines="None" Height="50px" Width="90%">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
            <EditRowStyle BackColor="#999999" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <Fields>
                <asp:BoundField DataField="ActionID" HeaderText="ActionID" ReadOnly="True" SortExpression="ActionID"
                    Visible="False" />
                <asp:BoundField DataField="NextTaskID" HeaderText="NextTaskID" ReadOnly="True" SortExpression="NextTaskID"
                    Visible="False" />
                <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="Action" HeaderText="Action" SortExpression="Action" ReadOnly="True" />
                <asp:BoundField DataField="NextTask" HeaderText="NextTask" SortExpression="NextTask" ReadOnly="True" />
                <asp:TemplateField HeaderText="Message" SortExpression="Message">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Message") %>' TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);"  Width="100%" Height="250px" Font-Names="Tahoma"></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Message") %>' TextMode="MultiLine" Width="100%" Height="250px" Font-Names="Tahoma"></asp:TextBox>
                    </InsertItemTemplate>
                    <ItemTemplate>
<asp:TextBox ID="Label1" runat="server" style="border-width:0px" ReadOnly="true" Text='<%# Bind("Message") %>' Width="100%" Height="250px" Font-Names="Tahoma" TextMode="MultiLine"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                            Text="Update"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="Edit"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" OnClick="LinkButton2_Click"
                            Text="Cancel"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Fields>
            <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" Width="100px" Wrap="True" />
        <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:DetailsView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetDataByKey" TypeName="PromAdminTableAdapters.ActionMessagesTableAdapter"
            UpdateMethod="UpdateQuery" OnSelecting="ObjectDataSource1_Selecting" OnUpdating="ObjectDataSource1_Updating">
            <UpdateParameters>
                <asp:Parameter Name="Message" Type="String" />
                <asp:Parameter Name="original_ActionID" Type="Int32" />
                <asp:Parameter Name="original_NextTaskID" Type="Int32" />
            </UpdateParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="GridView1" Name="ActionID" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="GridView1" Name="NextTaskID" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        &nbsp;
        <br />
    </div>
</asp:Content>

