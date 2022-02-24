<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReviewersSummary.ascx.cs" Inherits="Controls_ReviewersSummary" %>
<h3 style="text-align: left">Summary of Reviewers Form</h3> 

    <asp:Label ID="LabelNoForm" runat="server"></asp:Label>
    <asp:Table ID="TableSummary" runat="server" GridLines="Both" BackColor="Transparent" BorderColor="Transparent" HorizontalAlign="Center" CssClass="table-condensed">
</asp:Table>
<br />
<br />
<asp:Button ID="ButtonExport" runat="server"  Text="Export to Word" class="btn btn-primary btn-md" ToolTip="Press this button to open a window for exporting the above table into MS Word" />
<br />
&nbsp;
<br />
<%--                    <asp:Panel ID="PanelUploadSupDoc" runat="server">
                        <fieldset style="margin-left: 30px; width: 500px; background-color: #f0f0f0; border: 1px solid #3062af; padding: 5px; text-align: center;">
                            <legend style="font-style: italic; font-weight: bold; background-color: #3062af; color: white">Supporting Document</legend>
                            <asp:LinkButton ID="LinkButtonSuppDoc" runat="server" OnClick="LinkButtonSuppDoc_Click"></asp:LinkButton>
                            <asp:Label ID="LabelSize" runat="server"></asp:Label><br />

                        </fieldset>
                    </asp:Panel>--%>