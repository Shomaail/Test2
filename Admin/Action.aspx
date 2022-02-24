<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_Action" CodeFile="~/Admin/Action.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>Action</h3>
    <asp:Repeater ID="rpAction" runat="server" ItemType="BL.Data.WFAction" OnItemCommand="rpAction_ItemCommand">
        <ItemTemplate>
            <div runat="server" id="divApp" class="bg-open-promo">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-4 col-sm-12">
                            <h5 class="mt-0 mb-1"> <strong>Action:</strong> <%#  Item.Title + "(" +Item.Status +")"%>
                            </h5>
                            <strong>Action ID: </strong> <%#  Item.ActionID%><br />
                             <strong>Next Phase: </strong> <%#  Item.NextPhaseID !=null ? Item.Phase.Title + "(Phase ID:" +Item.Phase.PhaseID+")" : ""%><br />
                             <strong>Current Task: </strong> <%#  Item.TaskID !=null ? Item.Task.Title +  "(Task ID:" +Item.Task.TaskID+")" : ""%><br />
                                                <br />

                        </div>
                        <div class="col-lg-4 col-sm-12">
                             <strong>Task: </strong> <%# Item.NextTaskID !=null ?  Item.Task1.Title + "(Next Task ID:" +Item.Task1.TaskID+")" : ""%><br />                            
                             <strong>Does this action require complete of CurrentTask: </strong> <%#  Item.RequireCompleteTask%><br />
                             <strong>Attached Action: </strong> <%#  Item.AttActionID !=null ? Item.WFAction2.Title+  "(Action ID:" +Item.WFAction2.ActionID +")" :""%><br />
                            
                        </div>
                        <div class="col-lg-2 col-sm-12">

                            <br />
                            <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false"
                                CommandArgument="1"
                                CommandName="Delete" ToolTip="Delete" CssClass="btn btn-default"
                                OnClientClick="return ShowConfirm(this.id);">Delete <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                            </asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>

        </ItemTemplate>
    </asp:Repeater>
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
        <div>
            <asp:LinkButton runat="server" ID="hideModalPopupViaServer0" Text="OK" OnClick="hideModalPopupViaServer0_Click" />
        </div>
    </asp:Panel>
</asp:Content>
