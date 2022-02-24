<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="Feedback" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
     <style type="text/css">
            .modalBackground
            {
                background-color: Gray;
                filter: alpha(opacity=60);
            }
            
            .modalPopup
            {
                border: 3px solid Gray;
                background-color: #99ccff;
                padding: 5px;
            }
        </style>
    <script type='text/javascript'>
        function cancelClick() {
            var label = $get('ctl00_SampleContent_Label1');
            label.innerHTML = 'You hit cancel in the Confirm dialog on ' + (new Date()).localeFormat("T") + '.';
        }
    </script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Faculty Promotion System</title>
</head>
<body dir="ltr" style="margin:0px; background-image: url(images/logo.gif); background-repeat: no-repeat">

    <div style="border: 0px solid; margin-left: 0px; margin-top: 78px;"></div>
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager><uc10:Alert ID="Alert1" runat="server" />
        <uc2:confirm ID="Confirm1" runat="server" />
        <h1>
            Feed Back</h1>
        
    <div style="padding-left:15px">
         <h3 style="border-bottom: 1px solid gray; padding-bottom: 2px">   User Details:</h3>
        <table cellpadding="3" cellspacing="0" border="0" style="background-color:#efefef">
        <tr><td class="PointsHeader">Name:</td><td>
            <asp:Label ID="lblDeputy" runat="server" Text="Deputy Of " Font-Italic="True" Visible="False"></asp:Label>
            <asp:Label ID="lblName" runat="server"></asp:Label></td></tr>
        <tr><td class="PointsHeader">KFUPM Email:</td><td>
            <asp:Label ID="lblEmail" runat="server"></asp:Label></td></tr>
        <tr><td class="PointsHeader">KFUPM ID:</td><td>
            <asp:Label ID="lblEmpID" runat="server"></asp:Label></td></tr>
        <tr><td class="PointsHeader">Rank:</td><td>
            <asp:Label ID="lblRank" runat="server"></asp:Label></td></tr></table>
        <asp:Panel ID="PanelFB" runat="server">
          
         <br /> <h3> Feedback of Faculty Promotion System Users</h3> 
                <br />
        <asp:GridView ID="GridViewAdminFB" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" DataSourceID="ObjectDataSourceFB" ForeColor="#333333" 
                    GridLines="None" AllowPaging="True" 
                    onselectedindexchanged="GridViewAdminFB_SelectedIndexChanged"
                    DataKeyNames="id"
                     >
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Select" Text="Details"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandName="Delete" Text="Delete" Visible = "true"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Name" 
                    SortExpression="Name" />
                <asp:BoundField DataField="Feedback" HeaderText="Feedback" 
                    SortExpression="Feedback" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
               
                <br />
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
                    CellPadding="4" DataSourceID="ObjectDataSourceFBDT" ForeColor="#333333" 
                    GridLines="None" Height="50px" Width="125px" DataKeyNames="id" 
                    Caption= "User Details" Visible="False">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <Fields>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Department" HeaderText="Department" 
                            SortExpression="Department" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                        <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Button ID="ButtonOK" runat="server" Text="OK" OnClick="ButtonOK_Click"/>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                    </Fields>
                <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:DetailsView>
                
                <asp:ObjectDataSource ID="ObjectDataSourceFBDT" runat="server" 
                    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
                    SelectMethod="GetDataByID" 
                    TypeName="FeedBackTableAdapters.Form_FeedbackTableAdapter">
                    <SelectParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="Department" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="Phone" Type="String" />
                        <asp:Parameter Name="Feedback" Type="String" />
                        <asp:Parameter Name="Date" Type="DateTime" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSourceFB" runat="server" 
                    InsertMethod="Insert" 
                    SelectMethod="GetData" 
                    TypeName="FeedBackTableAdapters.Form_FeedbackTableAdapter" 
                    DeleteMethod="Delete">
                    <DeleteParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="Department" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="Phone" Type="String" />
                        <asp:Parameter Name="Feedback" Type="String" />
                        <asp:Parameter Name="Date" Type="DateTime" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                </asp:Panel> 
                <br />
                <br />
                
                <asp:Panel ID="PanelFeedbackUser" runat="server" Height="321px">
                    <asp:TextBox ID="TextBoxFeedback" runat="server" 
    Height="173px" TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,1000);"  onPaste="return limitClipBoard(this,1000);"  Width="704px"></asp:TextBox>
                    <br />
                    <br />
                 <asp:Button ID="ButtonSendFB" runat="server" Text="Post" 
                        onclick="ButtonSendFB_Click" />
<%--                    <asp:ConfirmButtonExtender ID="ButtonSendFB_ConfirmButtonExtender" 
                        runat="server" ConfirmText="" 
                        Enabled="True" 
                        TargetControlID="ButtonSendFB" 
                        OnClientCancel="cancelClick"                       
                        DisplayModalPopupID="ModalPopupExtenderConfirm">
                    </asp:ConfirmButtonExtender>
                <asp:ModalPopupExtender ID="ModalPopupExtenderConfirm" 
                    runat="server" 
                    TargetControlID="ButtonSendFB"
                    PopupControlID="PNL" 
                    OkControlID="ButtonOk" 
                    CancelControlID="ButtonCancel" 
                    BackgroundCssClass="modalBackground" />
                <asp:Panel ID="PNL" 
                    runat="server" 
                    Style="display: none; width: 200px; background-color: White;
                    border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                    <asp:Label ID="LabelMessageConfirm" runat="server"></asp:Label>
                    <br />
                    <br />
                    <div style="text-align: right;">
                        <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                    </div>
                </asp:Panel>--%>
                    &nbsp;&nbsp;
                        </asp:Panel>
                    <br />
                    <br />
                    <asp:HyperLink ID="HyperLinkReturn" runat="server" NavigateUrl="~/Main.aspx">Return 
                    to Main Page</asp:HyperLink>
                
        <br />
        <br />        
        <br />
               
        <br />
        <br />
        <br />
        <br />
        <br /></div>
     <table width="100%" style="border-top:1px solid #3062af ">
            <tr>
            <td align ="center" style="width: 100%">
                <span style="font-size: 9pt">
                    Faculty Promotion System <br />
                    Copyright © 2009 King Fahd University of Petroleum & Minerals | Dhahran, 31261 | Saudi Arabia | +966 (3) 860 0000.<br />
                    
                  </span>
                 </td>
                </tr>
          </table>
    </form>    
 
</body>  

</html>
