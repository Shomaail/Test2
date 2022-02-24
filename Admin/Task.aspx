<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_Task" MaintainScrollPositionOnPostback="true" CodeFile="Task.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Task</h1>
    <button type="button" class="btn btn-outline-primary btn-sm" data-toggle="modal" data-target="#myModal"><i class="fa fa-plus" aria-hidden="true"></i> Add Task</button>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel">Add Task</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Title</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" required class="form-control" id="tbTitleAdd" placeholder="Title">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Phase</label>
                            <div class="col-sm-9">
                                <asp:DropDownList runat="server" type="text" class="form-control" DataTextField="Text" DataValueField="Value" ID="ddlPhaseIDAdd"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Remind After</label>
                            <div class="col-sm-9">
                                <input runat="server" type="number" max="255" min="3" class="form-control" id="tbRemindAfterAdd" placeholder="Remind After">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Remind Again After</label>
                            <div class="col-sm-9">
                                <input runat="server" type="number" max="255" min="3" class="form-control" id="tbRemindAgainAfterAdd" placeholder="Remind Again After">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Role</label>
                            <div class="col-sm-9">
                                <asp:DropDownList runat="server" type="text" class="form-control" DataTextField="Text" DataValueField="Value" ID="ddlRoleIDAdd"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Action Required</label>
                            <div class="col-sm-9">
                                <asp:DropDownList runat="server" type="text" class="form-control" ID="ddlActionRequired">
                                    <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
                                    <asp:ListItem Value="true">yes</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:LinkButton runat="server" ID="lbtnAddTask" CssClass="btn btn-primary" OnClick="lbtnAddTask_Click"> Add </asp:LinkButton>

                </div>
            </div>
        </div>
    </div>

    <hr>
    <table class="table">

        <thead>
            <tr>
                <th  scope="col">#</th>
                <th scope="col">Task</th>
                <th scope="col">Phase</th>
                <th scope="col">Role</th>
                <th scope="col">Remind After </th>
                <th scope="col">Remind again after</th>
                <th scope="col">Action Required</th>
                <th scope="col">Action</th>

            </tr>
        </thead>
        <tbody>

            <asp:Repeater ID="rptTask" runat="server" ItemType="BL.Data.Task" OnItemCommand="rptTask_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Container.ItemIndex + 1 %></th>
                        <td>
                            <asp:Label runat="server" ID="lblTitle"><%# Item.Title +" (Task ID:"+ Item.TaskID +")"%> </asp:Label>
                            <div class="col-sm-9">
                                <input runat="server" value='<%# Item.Title %>' type="text" visible="false" class="form-control" id="tbTitleEdit" required placeholder="Title">
                            </div>
                        </td>
                        <td>
                            <asp:label id="lblPhaseID" runat="server" Text='<%# Item.PhaseID != 0? Item.Phase.Title +"( Phase ID:"+ Item.PhaseID +")": "Phase ID: " + Item.PhaseID.ToString() %>'></asp:label>
                           
                                <asp:DropDownList ID="ddlPhaseIDEdit" visible="false" OnDataBinding="ddlPhaseIDEdit_DataBinding" runat="server" type="text" class="form-control" DataTextField="Text" DataValueField="Value" SelectedValue='<%# Item.PhaseID %>' ></asp:DropDownList>
                           
                        </td>
                        <td>
                            <asp:label id="lblRoleID" runat="server" Text='<%# Item.RoleID != 0 ? Item.Role.Title +"( Role ID:"+ Item.RoleID +")": "" %>'></asp:label>
                           
                                <asp:DropDownList ID="ddlRoleIDEdit" OnDataBinding="ddlRoleIDEdit_DataBinding" visible="false" runat="server" 
                                    type="text" class="form-control" DataTextField="Text" DataValueField="Value" 
                                    SelectedValue='<%# Item.RoleID %>'></asp:DropDownList>
                           
                        </td>
                        <td>
                             <asp:label id="lblRemindAfter" runat="server" Text='<%# Item.RemindAfter %>'></asp:label>
                           
                                <input runat="server" type="number" value='<%# Item.RemindAfter %>' visible="false" class="form-control" id="tbRemindAfterEdit" required placeholder="Remind After">
                            
                        </td>

                        <td>
                             <asp:label id="lblRemindAgainAfter" runat="server" Text='<%# Item.RemindAgainAfter%>'></asp:label>
                           
                                <input runat="server" type="number"  visible="false" value='<%# Item.RemindAgainAfter %>' class="form-control" id="tbRemindAgainAfterEdit" required placeholder="Remind Again After">
                            
                        </td>
                        <td>
                            <label for="inputEmail3" id="lblActionRequired" class="col-sm-3 control-label"><%# Item.ActionRequired.Value ? "Yes" : "No" %> </label>
                           
                                <asp:DropDownList ID="ddlActionRequiredEdit" runat="server" Visible="false" SelectedValue='<%# Item.ActionRequired.Value ? "true" : "false"%>'>
                                    <asp:ListItem  value="true">Yes</asp:ListItem>
                                    <asp:ListItem value="false">No</asp:ListItem>
                                
                                </asp:DropDownList>
                            
                        </td>
                         <td>
                            <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument="<%# Item.TaskID%>" CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                                <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);" runat="server" CommandArgument="<%# Item.TaskID%>" CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                            </div>
                            <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server"   CommandArgument="<%# Item.TaskID %>" CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" Tooltip="Cancel" runat="server" CommandArgument="<%# Item.TaskID %>" CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
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

