<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Directions.aspx.cs" Inherits="Admin_Directions" Title="Faculty Promotion System" MaintainScrollPositionOnPostback="true"%>
<%--<%@ Register src="../Controls/Alert.ascx" tagname="Alert" tagprefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h3>Main FPS Forms</h3>
    <uc10:Alert ID="Alert1" runat="server" />
    <asp:GridView ID="GridView1" runat="server" DataSourceID="DirectionsDataSource" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="TaskID,FormID" ForeColor="#333333" BorderColor="Black" HorizontalAlign="Center">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
        <asp:TemplateField>
        <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
        </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="Task" HeaderText="Task" ReadOnly="True" SortExpression="Task" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="RoleText" HeaderText="Role" ReadOnly="True" SortExpression="RoleText" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Form" HeaderText="Form" ReadOnly="True" SortExpression="Form" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Instruction" SortExpression="Instruction">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Height="215px" Text='<%# Bind("Instruction") %>'
                        TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);"  Width="602px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    &nbsp;<asp:TextBox ID="TextBox2" runat="server" Height="215px" ReadOnly="True" Text='<%# Bind("Instruction") %>'
                        TextMode="MultiLine" Width="603px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <asp:ObjectDataSource ID="DirectionsDataSource" runat="server"  SelectMethod="GetData" TypeName="GeneralDALTableAdapters.TaskFormTskFrmRlTableAdapter" UpdateMethod="UpdateInstructions" OldValuesParameterFormatString="original_{0}">
        <UpdateParameters>
            <asp:Parameter Name="Instruction" Type="String" />
            <asp:Parameter Name="Original_TaskID" Type="Int32" />
            <asp:Parameter Name="Original_FormID" Type="Byte" />
        </UpdateParameters>
    </asp:ObjectDataSource>    
    <h3>External Forms</h3>      
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyNames="FormID" DataSourceID="ExternalForms" ForeColor="#333333" 
        AllowSorting="True" EnableModelValidation="True">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <Columns>
        <asp:TemplateField>
        <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
        </asp:TemplateField>
        
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="FormID" HeaderText="FormID"
                Visible="False" />
            <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True" SortExpression="Title" />
            <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" ReadOnly="True" />
            <asp:TemplateField HeaderText="Instruction" SortExpression="Instruction">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Height="165px" Text='<%# Bind("Instruction") %>'
                        TextMode="MultiLine" Width="769px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="TextBox0" runat="server" Height="166px" Text='<%# Bind("Instruction") %>'
                        TextMode = "MultiLine" ReadOnly="True" Width ="772px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <asp:ObjectDataSource ID="ExternalForms" runat="server" 
        SelectMethod="GetDataAll" UpdateMethod="UpdateQuery" 
        TypeName="ExternalFormsTableAdapters.ExtFormInstructionsTableAdapter" 
        DeleteMethod="Delete" InsertMethod="Insert" >
        <DeleteParameters>
            <asp:Parameter Name="Original_FormID" Type="Byte" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormID" Type="Byte" />
            <asp:Parameter Name="Instruction" Type="String" />
            <asp:Parameter Name="Type" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Instruction" Type="String" />
            <asp:Parameter Name="FormID" Type="Byte" />
            <asp:Parameter Name="Type" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>

