<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_ManageAdmins" CodeFile="ManageAdmins.aspx.cs" %>
<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
              <h1>Manage admin</h1>
          <hr>
          <div class="form-inline">
            <div class="form-group">
              <label for="exampleInputName2">Employee ID:</label>
              <input type="text" runat="server" class="form-control" id="tbEmployeeIDAdd" placeholder="Input Employee ID">              
                <br />
                <asp:Label ID="lblNameAdd" runat="server" ></asp:Label>
            </div>
            <asp:LinkButton runat="server" ID="lbtnSearch" ToolTip="Search" CssClass="btn btn-primary" OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
            <asp:LinkButton runat="server" ID="lbtnAdd"    ToolTip="Search" CssClass="btn btn-success" OnClick="lbtnAdd_Click"><i class="fa fa-plus" aria-hidden="true"></i>Add</asp:LinkButton>            
          </div>   
          <hr>   
      <table class="table">
          
                <thead>
                    <tr>
                        <th width="10%" scope="col">#</th>
                        <th width="70%" scope="col">Name</th>
                        <th width="20" scope="col">Action</th>
                    </tr>
                </thead>
          <tbody>
        
    <asp:Repeater ID="rptAdmin" runat="server" ItemType="BL.Data.Admin" OnItemCommand="rptAdmin_ItemCommand">
        <ItemTemplate>                         
              <tr>
                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                <td><%# Item.Employee.NameString %><br /><input type="text" runat="server" class="form-control" id="tbEmployeeIDEdit" Visible="false"  placeholder="Input Employee ID" value='<%# Item.EmployeeID %>' /> </td>
                <td>
                  <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument=<%# Item.AdminID %> CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirmDeleteAdmin(this.id);" runat="server" CommandArgument=<%# Item.AdminID %> CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                  </div>                  
                  <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                    <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server"   CommandArgument=<%# Item.AdminID %> CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                    <asp:LinkButton ID="lbtnCancel" Tooltip="Cancel" runat="server" CommandArgument=<%# Item.AdminID %> CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
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
                <div style="text-align:center">
                    <asp:LinkButton runat="server" ID="hideModalPopupViaServer0" Text="OK" OnClick="hideModalPopupViaServer0_Click" />
                </div>
            </asp:Panel>
</asp:Content>

