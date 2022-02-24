<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_History" CodeFile="History.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

                <asp:GridView ID="gvApplicationTracking" runat="server" CellPadding="4" ForeColor="Black" class="table"
                    BorderColor="#719ade" AutoGenerateColumns="False" ItemType="BL.Data.Application_Log" >
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#1e9966" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField HeaderText="Task" SortExpression="Task">
                            <ItemTemplate>
                                <asp:Label Wrap="False" Font-Bold="True" Font-Size="10pt"
                                    ID="lblTask" runat="server" Text='<%# Item.ActionID == null? 
                                        TaskExtID.External_Evaluation.ToString().Replace("_"," ") 
                                        : Item.WFAction.Task.Title
                                        .Replace("@@TopAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopAuthority_TitleShort"])
                                        .Replace("@@TopLowAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"])
                                        .Replace("@@TopMostAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"])%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Person" SortExpression="Role">
                            <ItemStyle Wrap="False" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" 
                                    Text='<%# leri.Count > 0 && leri.Contains(Item.ActionID == null ?  (byte) RoleID.External_Reviewer: Item.WFAction.Task.RoleID)? "-" : Item.Actor %>'></asp:Label><br />
                                <span style="font-size: 10pt; color: Maroon"><em>(<asp:Label ID="Label2" runat="server" 
                                    Text='<%# Item.ActionID == null ? "External Reviewer" : Item.WFAction.Task.Role.Title + Item.Comments%>'></asp:Label>)</em></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" SortExpression="Action">
                            <ItemStyle Wrap="False" />
                            <ItemTemplate>
                                <asp:Label ID="lblAction" runat="server" Font-Bold="true" Font-Italic="true" 
                                    Font-Names="Arial,Verdana" Font-Size="10pt" 
                                    Text='<%# Item.ActionID == null? Item.Details : Item.WFAction.Title.Replace("@@TopMostAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"])
                .Replace("@@TopAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopAuthority_TitleShort"])
                .Replace("@@TopLowAuthority_TitleShort@@", ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"]) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ActionTime">
                            <ItemStyle Wrap="False" Font-Names="Verdana,Arial" Font-Size="10pt"/>
                            <ItemTemplate>
                               <asp:label runat="server" 
                                   Font-Names="Verdana,Arial" Font-Size="10pt"
                                   Text='<%# Item.ActionTime.HasValue? Item.ActionTime.Value.ToString("ddd dd MMM yyyy, hh:mm tt"):"" %>' ></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        [<em>No further tasks have been done after the initiation</em>]
                    </EmptyDataTemplate>
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>

<asp:Button runat="server" ID="hdnTargetControlForModalPopup1" Style="display: none" />
<asp:ModalPopupExtender runat="server" ID="programmaticModalPopup1" BehaviorID="programmaticModalPopupBehavior1"
    TargetControlID="hdnTargetControlForModalPopup1" PopupControlID="programmaticPopup1"
    BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle1"
    RepositionMode="RepositionOnWindowScroll">
</asp:ModalPopupExtender>
<asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup1"  Style="display: none;
    width: 600px; padding: 10px">
    <asp:Panel runat="Server" ID="programmaticPopupDragHandle1" Style="cursor: move;
        background-color: #DDDDDD; border: solid 1px Gray; color: Black; text-align: center;">
        Email
    </asp:Panel>
    <div style="width: 100%; height: 400px; overflow: scroll">
    <asp:GridView ID="gvEmail" runat="server" AutoGenerateColumns="False" 
        EnableModelValidation="True">
        <Columns>
            <asp:TemplateField >
                <EditItemTemplate>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" TextMode="MultiLine" runat="server" 
                        Text='<%# Bind("Message") %>' Height="369px" Width="594px"></asp:TextBox>
                    
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <br />
    <div align="center">
        <asp:LinkButton runat="server" ID="hideModalPopupViaServer1" Text="OK" OnClick="hideModalPopupViaServer1_Click" />
    </div>
</asp:Panel>
