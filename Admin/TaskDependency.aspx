<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_TaskDependency" CodeFile="TaskDependency.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <h1>Task Dependencies</h1>
 
          <hr>   
      <table class="table">
          
                <thead>
                    <tr>
                        <th width="10%" scope="col">#</th>                        
                        <th width="20" scope="col">Parent Task</th>
                        <th width="20" scope="col">Child Task</th>

                    </tr>
                </thead>
          <tbody>
        
    <asp:Repeater ID="rptTaskDependency" runat="server" ItemType="BL.Data.Task_Dependencies" >
        <ItemTemplate>                         
              <tr>
                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                  <td><%# Item.Task.Title +" (Parent Task ID:"+ Item.ParentTaskID+")"%> </td>
                  <td><%# Item.Task1.Title +" (Child Task ID:"+ Item.ChildTaskID+")"%> </td>
              </tr>
        </ItemTemplate>
    </asp:Repeater>
             </tbody>      
          </table>
   
   </asp:Content>

