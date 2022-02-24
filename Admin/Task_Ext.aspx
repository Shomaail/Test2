<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_Task_Ext" MaintainScrollPositionOnPostback="true" CodeFile="~/Admin/Task_Ext.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <h1>External Task</h1>
     <button type="button" class="btn btn-outline-primary btn-sm" data-toggle="modal" data-target="#myModal"><i class="fa fa-plus" aria-hidden="true"></i> Add external Task</button>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none;">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                  <h4 class="modal-title" id="myModalLabel">Add External Task</h4>
                </div>
                <div class="modal-body">                  
                  <div class="form-horizontal">

                   <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Title</label>
                      <div class="col-sm-9"><input  runat="server" type="text" required class="form-control" id="tbTitleAdd" placeholder="Title"></div>
                    </div>
                    <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Remind After</label>
                      <div class="col-sm-9"><input  runat="server" type="number" min="3" max="255" class="form-control" id="tbRemindAfterAdd" placeholder="Remind After"></div>
                    </div>
                    <div class="form-group">
                      <label for="inputEmail3" class="col-sm-3 control-label">Remind Again After</label>
                      <div class="col-sm-9"><input  runat="server" type="number" min="3" max="255" class="form-control" id="tbRemindAgainAfterAdd" placeholder="Remind Again After"></div>
                    </div>
                       <div class="form-group">
                      <label for="inputEmail3" class="col-sm-3 control-label">Role</label>
                      <div class="col-sm-9"><asp:DropDownList  runat="server" type="text" class="form-control"  DataTextField="Text" DataValueField="Value" id="ddlRoleAdd" ></asp:DropDownList></div>
                    </div>       
                      

                  </div>                  
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                  <asp:LinkButton runat="server" id="lbtnAddTask_Ext" CssClass="btn btn-primary" OnClick="lbtnAddTask_Ext_Click"> Add </asp:LinkButton>

                </div>
              </div>
            </div>
          </div>
 
   

                   
          <hr>   
      <table class="table">
          
                <thead>
                    <tr>
                        <th width="10%" scope="col">#</th>                                                
                        <th width="20" scope="col">External Task Title</th>                        
                        <th width="20" scope="col">Remind After </th>
                        <th width="20" scope="col">Remind again after</th>
                        <th width="20" scope="col">Role ID</th>

                    </tr>
                </thead>
          <tbody>
        
    <asp:Repeater ID="rptTask_Ext" runat="server" ItemType="BL.Data.Task_Ext" OnItemCommand="rptTask_Ext_ItemCommand" >
        <ItemTemplate>                         
              <tr>
                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                  <td><asp:Label ID="lblTitle" runat="server" Text='<%# Item.Title +" (Task ID:"+ Item.TaskID +")"%>'>

                      </asp:Label> <input type="text" runat="server" class="form-control" id="tbTitleEdit" Visible="false"  placeholder="Input Name" value='<%# Item.Title %>' /></td>                                  
                  <td>
                      <asp:Label ID="lblRemindAfter" runat="server" Text='<%# Item.RemindAfter %>'></asp:Label> 
                      <input type="number" runat="server" min="3" max="255" class="form-control" id="tbRemindAfterEdit" Visible="false"   value='<%# Item.RemindAfter %>' />
                  </td>
                  <td>
                      <asp:Label ID="lblRemindAgainAfter" runat="server" Text='<%# Item.RemindAgainAfter %>'></asp:Label> 
                      <input type="number" runat="server" min="3" max="255" class="form-control" id="tbRemindAgainAfterEdit" Visible="false"   value='<%# Item.RemindAgainAfter %>' />
                  </td>  
                  <td>
                      <asp:Label ID="lblRoleID" runat="server" Text='<%# Item.Role.Title +"( Role ID:"+ Item.RoleID +")"%> '></asp:Label>                       
                      <asp:DropDownList  runat="server"  class="form-control"  
                          SelectedValue='<%# Item.RoleID %>'
                          DataTextField="Text" DataValueField="Value" id="ddlRoleIDEdit" OnDataBinding="ddlRoleIDEdit_DataBinding" Visible="false"  ></asp:DropDownList>
                  </td>                              <%-- SelectedValue='<%# Item.RoleID %>'        --%>               
                  <td>
                  <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument=<%# Item.TaskID%> CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);" runat="server" CommandArgument=<%# Item.TaskID%> CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                  </div>                  
                  <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                    <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server"   CommandArgument=<%# Item.TaskID %> CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                    <asp:LinkButton ID="lbtnCancel" Tooltip="Cancel" runat="server" CommandArgument=<%# Item.TaskID %> CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
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
            <asp:Label ID="lblProgrammaticPopup0" runat="server"></asp:Label>
            <br />
            <div align="center">
                <asp:LinkButton runat="server" ID="hideModalPopupViaServer0" Text="OK" OnClick="hideModalPopupViaServer0_Click" />
            </div>
        </asp:Panel>
   </asp:Content>

