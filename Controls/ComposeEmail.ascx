<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Controls_ComposeEmail" CodeFile="ComposeEmail.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<style type="text/css">
    .modalBackground
    {
        background-color: Gray;
        -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(opacity=80)"; /*IE8*/
        filter: alpha(opacity=80); /*internet explorer */
        opacity: 0.8; /* fx, safari, opera, chrome */
    }
    
    .modalPopup
    {
        border: 3px solid Gray;
        background-color: #99ccff;
        padding: 5px;
    }
    .ComposeEmailGV
    {
       margin-left:10px;        
    }
</style>
<asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
<ajaxToolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
    TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="PanelComposeEmail"
    BackgroundCssClass="modalBackground" DropShadow="True" RepositionMode="RepositionOnWindowScroll"></ajaxToolkit:ModalPopupExtender>
<div>
<%--    <fieldset style="margin-left: 30px; width: 90%; background-color: #f0f0f0; border: 1px solid #0088ce;
        padding: 5px; height: 100%;">
        <legend style="font-style: italic; font-weight: bold; background-color: #0088ce;
            color: white">Compose Email </legend>--%>
<asp:Panel ID="PanelComposeEmail" Width="900px" runat="server" Style="background: #2c9966; padding:10px">
    <div style="text-align:right">
     <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="False"  OnClick="ButtonCancel_Click"
     ToolTip="Cancel" CssClass="btn btn-default"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span> </asp:LinkButton>
        </div>
    <fieldset style="margin: 5px;  border: 2px solid #2c9966; height: 90%;">
        <legend style="font-style: italic; font-weight: bold; background-color: #2c9966;
            color: white">Compose Email </legend>
        <asp:GridView ID="GridViewEmail" runat="server" CellPadding="4" ForeColor="#333333" CssClass="table"
            GridLines="None" ShowFooter="True" ShowHeader="False" Width="100%" AutoGenerateColumns="False">
            <FooterStyle BackColor="#1e9966" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table ><tr><td> <b>To:</b></td><td style="padding-left:20px"> <asp:TextBox ID="TextBoxTo" Width="500px"  runat="server" Text='<%# Eval("To") %>' BorderWidth="0"
                            TextMode="MultiLine" ToolTip="You can add or delete any Email Address of your choice"></asp:TextBox></td>
                      
                       </tr>
                            <tr>
                                <td><b>Subject:</b></td>
                                <td style="padding-left:20px"><asp:TextBox  ID="TextBoxSubject" Width="500px" runat="server" Text='<%# Eval("Subject") %>'
                            BorderWidth='<%# Eval("Subject").Equals("") ? 1:0 %>'  ToolTip="Write Subject text here"></asp:TextBox></td></tr>
                        </table>
                        
                        
                        <br />
                        <br />
                        <b>Message Contents:</b><br />
                        <asp:TextBox ID="TextBoxBody" runat="server" Height="356px" TextMode="MultiLine"
                            Width="100%" Text='<%# Eval("Body") %>' ToolTip="Write Body text here"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="lbtnSendEmail" runat="server" OnClick="ButtonSendEmail_Click" class="btn btn-default" 
                            ToolTip="Press this button to send the email">Send <span class="glyphicon glyphicon-send" aria-hidden="true"></span></asp:LinkButton>
                        <asp:LinkButton ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" class="btn btn-default" 
                            ToolTip="Press this button to Cancel " >Cancel <span class="glyphicon glyphicon-remove" aria-hidden="true"></span> </asp:LinkButton>
                    </FooterTemplate>
                    <ItemStyle Width="100%" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <EditRowStyle BackColor="#999999" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
             <PagerStyle BackColor="#1A1718" ForeColor="White" HorizontalAlign="Center"
                        Font-Bold="True" Font-Italic="False" Font-Names="Copperplate Gothic Light"
                        Font-Size="X-Large" />
        <HeaderStyle BackColor="#e7f7ff" Font-Bold="True" ForeColor="#3a9fe1" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
  </fieldset>
</asp:Panel>
<%--&nbsp; &nbsp; </fieldset>--%>
</div>
