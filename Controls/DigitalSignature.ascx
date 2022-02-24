<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DigitalSignature.ascx.cs" Inherits="Controls_DigitalSignature" %>
<h3>Digital Signatures Status</h3>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
    CellPadding="4" Width="100%" CssClass="table-condensed"
    DataKeyNames="ApplicationID,Type,Position"
    DataSourceID="ObjectDataSourceFinalPromotionCommittee" ForeColor="#333333"
    GridLines="Vertical" EnableModelValidation="True">
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <Columns>
        <asp:TemplateField>
            <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Position" SortExpression="Position">
            <EditItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Position") %>'></asp:Label>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="LabelPosition" runat="server" Text="<%# GetStatus() %>"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
        <asp:TemplateField HeaderText="Digital Signature">
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:CheckBox ID="CheckBoxDigitalSignature" runat="server" Checked='<%# Eval("DigitalSignature") %>'
                    Enabled="False"
                    Style="z-index: 100; left: 0px; position: relative; top: 0px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="DSDate" HeaderText="Date and Time" SortExpression="DSDate" />
    </Columns>
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <EditRowStyle BackColor="#999999" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
</asp:GridView>
<asp:ObjectDataSource ID="ObjectDataSourceFinalPromotionCommittee" runat="server"
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="PromotionTableAdapters.PromotionCommitteeTempTableAdapter">
    <SelectParameters>
        <asp:SessionParameter Name="ApplicationID" SessionField="ApplicationIDGlobal" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
&nbsp;
