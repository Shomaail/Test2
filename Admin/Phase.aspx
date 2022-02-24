<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_Phase" CodeFile="Phase.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <h1>Task</h1>
 
          <hr>   
      <table class="table">
          
                <thead>
                    <tr>
                        <th width="10%" scope="col">#</th>                        
                        <th width="20" scope="col">Phase</th>
 
                    </tr>
                </thead>
          <tbody>
        
    <asp:Repeater ID="rptPhase" runat="server" ItemType="BL.Data.Phase" >
        <ItemTemplate>                         
              <tr>
                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                  <td><%# Item.Title +" (Phase ID:"+ Item.PhaseID +")"%> </td>

              </tr>
        </ItemTemplate>
    </asp:Repeater>
             </tbody>      
          </table>
   
   </asp:Content>

