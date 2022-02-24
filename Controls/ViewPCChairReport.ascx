<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewPCChairReport.ascx.cs" Inherits="Controls_ViewPCChairReport" %>
<style type="text/css">
    .style1
    {
        color: #FF3300;
    }
    .style2
    {
        color: red;
    }
</style>
<script type="text/javascript">

function confirm_delete()
{
  if (confirm("This will erase all the Digital Signatures. Are you sure you want to remove this promotion report?")==true)
    return true;

  else

    return false;
}
</script>

    <h3>Promotion Committee Report</h3>
    <span class="style1">Your Adobe Acrobat Reader may be outdated!</span>
<asp:HyperLink ID="HyperLink1" runat="server" 
    NavigateUrl="~/Download/Adobe Acrobat 9/AdbeRdr920_en_US.exe">Click here to download Adobe Acrobat Reader 9.2</asp:HyperLink>

<asp:GridView ID="grdFiles" runat="server" AutoGenerateColumns="False" BorderColor="#3062AF" CssClass="table"
    CellPadding="4" DataKeyNames="ApplicationID" ForeColor="Black" GridLines="Vertical"
    OnSelectedIndexChanged="grdFiles_SelectedIndexChanged" Width="90%" OnRowDeleting="grdFiles_RowDeleting" OnDataBound="grdFiles_DataBound">
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#F7F6F3" ForeColor="Black" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="Black" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" ForeColor="#333333" />
    <EditRowStyle BackColor="#999999" />
    <Columns>        
        <asp:BoundField DataField="ApplicationID" HeaderText="ApplicationID" SortExpression="ApplicationID"
            Visible="False" />
        <asp:BoundField DataField="FileName" HeaderText="File Name" SortExpression="FileName" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="Comments" HeaderText="Description" SortExpression="Comments" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="ApplicantName" HeaderText="Applicant's Name" SortExpression="ApplicantName" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:ButtonField CommandName="Select" Text="Download" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:ButtonField>        
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="Delete"
                    Text="Delete"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EmptyDataTemplate>
        The Promotion Committee Chairman has not uploaded any report for this application
    </EmptyDataTemplate>
    <EmptyDataRowStyle BorderStyle="Groove" />
</asp:GridView>
<br />
<asp:HiddenField ID="HiddenFieldApplicationID" runat="server" />

