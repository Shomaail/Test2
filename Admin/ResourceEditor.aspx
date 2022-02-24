<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ResourceEditor.aspx.cs" Inherits="Admin_ResourceEditor" %>
<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div>
            <h4>Resource Editor</h4>
            <table>
                <thead>
                    <tr>
                        <th width="10%" scope="col">#</th>
                        <th width="35%" scope="col">Key</th>
                        <th width="35%" scope="col">Value</th>
                        <th width="20" scope="col">Action</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptResource" ItemType="ResourceFile" OnItemCommand="rptResource_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.ItemIndex + 1 %></td>
                            <td><%# Item.Key %></td>
                            <td><%# Item.Value %>
                                <input type="text" runat="server" class="form-control" id="tbValueEdit" visible="false"
                                    placeholder="Input Value" value='<%# Item.Value %>' />

                            </td>
                            <td>
                                <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Item.Key%>' CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                                    <%--                    <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirmDeleteWorkflowAttribute(this.id);" runat="server" CommandArgument='<%# Item.Key%>' CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>--%>
                                </div>
                                <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CommandArgument='<%# Item.Key%>' CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" runat="server" CommandArgument='<%# Item.Key%>' CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </tbody>
            </table>
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
            <div style="text-align: center">
                <asp:LinkButton runat="server" ID="hideModalPopupViaServer0" Text="OK" OnClick="hideModalPopupViaServer0_Click" />
            </div>
        </asp:Panel>
</asp:Content>
