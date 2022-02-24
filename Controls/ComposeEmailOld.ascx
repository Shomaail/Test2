<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ComposeEmailOld.ascx.cs"
    Inherits="Controls_ComposeEmail" %>
<div align="left">
    <fieldset style="margin-left: 30px; width: 90%; background-color: #f0f0f0; border: 1px solid #007d40;
        padding: 5px; height: 100%;">
        <legend style="font-style: italic; font-weight: bold; background-color: #007d40;
            color: white">Compose Email</legend>
        <br />
        <asp:GridView ID="GridViewEmail" runat="server" CellPadding="4" ForeColor="#333333" CssClass="table"
            GridLines="None" ShowFooter="True" ShowHeader="False" Width="100%" AutoGenerateColumns="False">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <b>To:</b><asp:TextBox ID="TextBoxTo" runat="server" Text='<%# Eval("To") %>' BorderWidth="0"
                            TextMode="MultiLine" Width="80%" ToolTip="You can add or delete any Email Address of your choice"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="regEmailTo" ControlToValidate="TextBoxTo" Text="(Invalid email)"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" />--%>
                            <%--Not Accepting multiple email addresses--%>
                        <br />
                        <br />
                        <b>Subject:</b><asp:TextBox ID="TextBoxSubject" runat="server" Text='<%# Eval("Subject") %>'
                            BorderWidth='<%# Eval("Subject").Equals("") ? 1:0 %>' Width="80%" ToolTip="Write Subject text here"></asp:TextBox>
                        <br />
                        <br />
                        <b>Message Contents:</b><br />
                        <asp:TextBox ID="TextBoxBody" runat="server" Height="356px" TextMode="MultiLine"
                            Width="100%" Text='<%# Eval("Body") %>' ToolTip="Write Body text here"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="ButtonSendEmail" runat="server" OnClick="ButtonSendEmail_Click" Text="Send" class="btn btn-primary btn-md" 
                            Width="148px" ToolTip="Press this button to send the email" />
                        <asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Cancel" class="btn btn-primary btn-md" 
                            Width="151px" ToolTip="Press this button to Cancel " />
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
        <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
&nbsp; &nbsp; </fieldset>
</div>
