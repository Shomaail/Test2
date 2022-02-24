<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_TaskForm" MaintainScrollPositionOnPostback="true" CodeFile="TaskForm.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <h1>Task Form</h1>
 
          <hr>   
      <table class="table">
          
                <thead>
                    <tr>
                        <th  scope="col">#</th>                        
                        <th scope="col">Task Related </th>
                        <th scope="col">Form Related</th>
                        <th scope="col">Instruction  </th>
                        <th scope="col">Action </th>
                    </tr>
                </thead>
          <tbody>
        
    <asp:Repeater ID="rptTaskForm" runat="server" ItemType="BL.Data.TaskForm" OnItemCommand="rptTaskForm_ItemCommand" >
        <ItemTemplate>                         
              <tr>
                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                  <td><strong>Task:</strong><%# Item.Task.Title +" (Task ID:"+ Item.TaskID+")"%> <br />
                      <strong>Is Task External:</strong><%# Item.TaskExternal%>
                  </td>
                  <td><strong>Form:</strong><%# Item.Form.Title +" (Form ID:"+ Item.FormID+", Page: "+ Item.Form.Page +")"%> <br />
                      <strong>Rank:</strong><asp:Label runat="server" ID="lblRank"><%# Item.Rank %> </asp:Label><br />
                      <strong>Level:</strong><%# Item.Level%> <br />
                      <strong>Is Form Required:</strong>: <%# Item.Checkable%><br />
                  </td>                  
                  <td>
                      <textarea  id="tbInstructionEdit" runat="server" style="width: 400px; height: 100px" value='<%# Item.Instruction %>' type="text" disabled class="form-control" placeholder="Instructions"></textarea>                      
                  </td>                  
                  <td>
                            <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Item.TaskID +";"+ Item.FormID +";"+ Item.TaskExternal%>' CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                                <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);" runat="server" CommandArgument='<%# Item.TaskID +";"+ Item.FormID +";"+ Item.TaskExternal%>' CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                            </div>
                            <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server"   CommandArgument='<%# Item.TaskID +";"+ Item.FormID +";"+ Item.TaskExternal%>' CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" Tooltip="Cancel" runat="server" CommandArgument='<%# Item.TaskID +";"+ Item.FormID +";"+ Item.TaskExternal%>' CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
                            </div>
                        </td>
              </tr>
        </ItemTemplate>
    </asp:Repeater>
             </tbody>      
          </table>
   
   </asp:Content>

