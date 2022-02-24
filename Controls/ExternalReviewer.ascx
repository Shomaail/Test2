<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExternalReviewer.ascx.cs" Inherits="Controls_ExternalReviewer" %>
<asp:GridView ID="gvExternalReviewers" runat="server" AutoGenerateColumns="False" DataSourceID="odsFormExtRev" EnableModelValidation="True" CellPadding="4"
    CssClass="table-condensed" DataKeyNames="ApplicationID,Type,Serial"
    ForeColor="#333333" GridLines="None" EmptyDataText="No External Reviewer is selected. Click on the Add button below" OnSelectedIndexChanged="gvExternalReviewers_SelectedIndexChanged">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>

        <asp:TemplateField>
            <ItemTemplate>
                <%# Container.DataItemIndex +1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" 
                    CommandArgument='<%# Eval("ApplicationID") + "," + Eval("Type") + "," + Eval("Serial")  %>'></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:BoundField DataField="ExternalReviewerID" HeaderText="ExternalReviewerID" SortExpression="ExternalReviewerID" Visible="false"/>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
        <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
        <asp:BoundField DataField="MailingAddress" HeaderText="MailingAddress" SortExpression="MailingAddress" />
        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
        <asp:BoundField DataField="Major" HeaderText="Major" SortExpression="Major" />
        <asp:BoundField DataField="Speciality" HeaderText="Speciality" SortExpression="Speciality" />
        <asp:BoundField DataField="PhoneAndFax" HeaderText="PhoneAndFax" SortExpression="PhoneAndFax" />
        <asp:BoundField DataField="ActiveAreaOfResearch" HeaderText="ActiveAreaOfResearch" SortExpression="ActiveAreaOfResearch" />
        <asp:BoundField DataField="PrevAreaOfResearch" HeaderText="PrevAreaOfResearch" SortExpression="PrevAreaOfResearch" />
        <asp:BoundField DataField="Webpage" HeaderText="Webpage" SortExpression="Webpage" />
        <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" />
        <asp:BoundField DataField="TotalPublications" HeaderText="TotalPublications" SortExpression="TotalPublications" />
        <asp:BoundField DataField="NoOfJournals" HeaderText="NoOfJournals" SortExpression="NoOfJournals" />
        <asp:BoundField DataField="HIndex" HeaderText="HIndex" SortExpression="HIndex" />
        <asp:BoundField DataField="Citations" HeaderText="Citations" SortExpression="Citations" />
        <asp:BoundField DataField="SWIFTCode" HeaderText="SWIFTCode" SortExpression="SWIFTCode" />
        <asp:BoundField DataField="IBAN" HeaderText="IBAN" SortExpression="IBAN" />
    </Columns>
    <EditRowStyle BackColor="#999999" />
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>
<asp:ObjectDataSource ID="odsFormExtRev" runat="server" OnDeleted="odsFormExtRev_Deleted" InsertMethod="InsertFormExtRev" SelectMethod="GetFormExtRevByAppIDType" TypeName="ExtRevBAL" DeleteMethod="DeleteFormExtRev">
    <DeleteParameters>
        <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
        <asp:Parameter Name="Type" Type="Int32" />
        <asp:Parameter Name="Serial" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="ApplicationID" Type="Int32" />
        <asp:Parameter Name="Type" Type="Int32" />
        <asp:Parameter Name="Serial" Type="Int32" />
        <asp:Parameter Name="ExternalReviewerID" Type="Int32" />
    </InsertParameters>
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
        <asp:Parameter Name="Type" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

