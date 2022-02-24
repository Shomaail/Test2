<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_SentEmails" MaintainScrollPositionOnPostback="true" CodeFile="~/Admin/SentEmails.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>Sent Emails</h3>
      <asp:Repeater ID="rpSentEmail" runat="server" ItemType="BL.Data.SentEmail" OnItemCommand="rpSentEmail_ItemCommand">
                        <ItemTemplate>
                                <div runat="server" id="divApp" class="bg-open-promo" >
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-lg-3 col-sm-12">
                                                <%# Container.ItemIndex + 1 + (PageNumber * PageSize) %>
                                                <%--ID: <%# Item.ID %>--%>
                                                From: <%# Item.FromEmail%><br />
                                                To: <%# Item.ToEmail%><br />
                                                Subject: <%# Item.Subject%><br />
                                                Sent Date: <%# Item.SentDate%><br />
                                            </div>
                                            <div class="col-lg-6 col-sm-12">
                                                Email:<textarea style="width:450px; height:100px"> <%# Cryptography.Decrypt(Item.Body)%></textarea>
                                            </div>
                                            <div class="col-lg-1 col-sm-12">
                                               <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);" runat="server" CommandArgument="<%# Item.ID%>" CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </ItemTemplate>
       </asp:Repeater>
    <div style="overflow: hidden;">

<asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
 <ItemTemplate>
  <asp:LinkButton ID="btnPage"
   style="padding:8px;margin:2px;background:#009966;border:solid 1px #eee;font:8pt tahoma;"
   CommandName="Page" CommandArgument="<%# Container.DataItem %>"
   runat="server" ForeColor="White" Font-Bold="True">
    <%# Container.DataItem %>
  </asp:LinkButton>
 </ItemTemplate>
</asp:Repeater>

</div>
     <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
        <asp:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
            TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
            BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
            RepositionMode="RepositionOnWindowScroll">
        </asp:ModalPopupExtender>
        <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="display: none; width: 300px; padding: 10px">
            <asp:Panel runat="Server" ID="programmaticPopupDragHandle0" Style="cursor: move; background-color: #DDDDDD; border: solid 1px Gray; color: Black; text-align: center;">
                Message
            </asp:Panel>
            <asp:Label ID="labelProgrammaticPopup0" runat="server"></asp:Label>
            <br />
            <div align="center">
                <asp:LinkButton runat="server" ID="hideModalPopupViaServer0" Text="OK" OnClick="hideModalPopupViaServer0_Click" />
            </div>
        </asp:Panel>
</asp:Content>
