<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_NameExclusion" CodeFile="NameExclusion.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <h1>Name Exclusion</h1>
 
          <hr>   
      <table class="table">
          
                <thead>
                    <tr>
                        <th width="10%" scope="col">#</th>                        
                        <th width="20" scope="col">Role</th>
                        <th width="20" scope="col">Role to be hidden</th>
                    </tr>
                </thead>
          <tbody>
        
    <asp:Repeater ID="rptNameExclusion" runat="server" ItemType="BL.Data.NameExclusion" >
        <ItemTemplate>                         
              <tr>
                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                  <td><%# Item.Role.Title +" (Role ID:"+ Item.RoleID +")"%> </td>
                  <td><%# Item.Role1.Title +" (HiddenRole ID:"+ Item.ExcludedRoleID+")"%> </td>                  
              </tr>
        </ItemTemplate>
    </asp:Repeater>
             </tbody>      
          </table>
   
   </asp:Content>

