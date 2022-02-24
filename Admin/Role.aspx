<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_Role" CodeFile="Role.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <h1>Roles</h1>
  <button type="button" class="btn btn-outline-primary btn-sm" data-toggle="modal" data-target="#myModal"><i class="fa fa-plus" aria-hidden="true"></i> Add Task</button>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel">Add Role</h4>
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
                            <label for="inputEmail3" class="col-sm-3 control-label">Type</label>
                            <div class="col-sm-9">
                                <input runat="server" type="number" max="255" min="0" class="form-control" id="tbTypeAdd" placeholder="Type">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:LinkButton runat="server" ID="lbtnAddRole" CssClass="btn btn-primary" OnClick="lbtnAddRole_Click"> Add </asp:LinkButton>

                </div>
            </div>
        </div>
    </div>

    <hr>
          <hr>   
      <table class="table">
          
                <thead>
                    <tr>
                       <th width="10%" scope="col">#</th>                        
                        <th width="40" scope="col">Role</th>
                        <th width="40" scope="col">Type</th>
                        <th width="10" scope="col"></th>
                    </tr>
                </thead>
          <tbody>
        
    <asp:Repeater ID="rptRole" runat="server" ItemType="BL.Data.Role" OnItemCommand="rptRole_ItemCommand" >
        <ItemTemplate>                         
              <tr>
                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                  <td><%# Item.Title +" (Role ID:"+ Item.RoleID +")"%> 
                      <input runat="server" value='<%# Item.Title %>' type="text" visible="false" class="form-control" id="tbTitleEdit" required placeholder="Title">
                  </td>
                  <td><%# Item.Type%>
                      <input runat="server" type="number" value='<%# Item.Type %>' visible="false"  max="255" min="0" class="form-control" id="tbTypeEdit" placeholder="Type">
                  </td>
                   <td>
                            <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument="<%# Item.RoleID%>" CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                                <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);" runat="server" CommandArgument="<%# Item.RoleID%>" CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                            </div>
                            <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server"   CommandArgument="<%# Item.RoleID %>" CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" Tooltip="Cancel" runat="server" CommandArgument="<%# Item.RoleID %>" CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
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

