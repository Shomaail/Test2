<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="ReviewersSummary.aspx.cs" Inherits="Forms_ReviewersSummary" %>
<%@ MasterType VirtualPath="Forms.master" %>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript">
        $(document).ready(function () {
            
            $('#ctl00_ContentPlaceHolder1_TableSummary').DataTable({                   
                 dom: 'Bfrtip',
                 buttons: [
                   'excel']

            });    });</script>
    <uc2:Instruction ID="Instruction1" runat="server" />
    <asp:Label runat="server" ID="lblMessage"></asp:Label>
    <br />
     <asp:Button ID="btnExportToWord" runat="server"  Text="Export to Word" class="btn btn-primary btn-md" ToolTip="Press this button to open a window for exporting the above table into MS Word" />
<%--     <div class="rating-legend">
            <strong>Legend:</strong>
            <span class="rating excellent">Excellent (9 - 10)</span>
            <span class="rating very-good">Very Good (8)</span>
            <span class="rating good">Good (7)</span>
            <span class="rating fair">Fair (6)</span>
            <span class="rating poor">Poor (5 or less)</span>
          </div>--%>
    <%--<table class="table-condensed" style="border:groove">
        <tr><td style="border-right:ridge; border-bottom:ridge">9 or 10	</td><td style="border-bottom:ridge">Excellent</td></tr>
        <tr><td style="border-right:ridge; border-bottom:ridge">8</td><td style="border-bottom:ridge">Very good</td></tr>
        <tr><td style="border-right:ridge; border-bottom:ridge">7</td><td style="border-bottom:ridge">good</td></tr>
        <tr><td style="border-right:ridge; border-bottom:ridge">6</td><td style="border-bottom:ridge">Fair</td></tr>
        <tr><td style="border-right:ridge; border-bottom:ridge">5 or less</td><td style="border-bottom:ridge">Poor</td></tr>
    </table>--%>
    <asp:Table ID="TableSummary" runat="server" >
      <%--  <asp:TableHeaderRow TableSection="TableHeader"><asp:TableHeaderCell>test</asp:TableHeaderCell></asp:TableHeaderRow>
       CssClass="table table-bordered" width="100%" 
        border="0" cellspacing="0" cellpadding="0"--%>
</asp:Table>
     <br />
   

</asp:Content>

