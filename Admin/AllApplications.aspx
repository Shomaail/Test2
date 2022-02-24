<%@ Page Title="Faculty Promotion System" Language="C#" MasterPageFile="~/Admin/Admin.master"
    MaintainScrollPositionOnPostback="true"
     AutoEventWireup="true" CodeFile="AllApplications.aspx.cs" Inherits="Admin_AllApplications" %>
<%@ MasterType VirtualPath="~/Admin/Admin.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
              <h1>All Applications</h1>
          <hr>
        
          <hr>   
      <table class="table">
          
                <thead>
                    <tr>
                        <th width="5%" scope="col">#</th>
                        <th width="20%" scope="col">Application ID</th>
                        <th width="20%" scope="col">Applicant Name</th>
                        <th width="20%" scope="col">Dept</th>
                        <th width="20%" scope="col">For Rank</th>
                        <th width="15" scope="col">Action</th>
                    </tr>
                </thead>
          <tbody>
        
    <asp:Repeater ID="rptAllApplication" runat="server" ItemType="BL.Data.Application" OnItemCommand="rptAllApplication_ItemCommand">
        <ItemTemplate>                         
              <tr>
                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                <td><%# Item.ApplicationID%><br /></td>
                <td><%# Item.Employee.NameString %><br />
                    Email ID: <%# Item.Employee.Email%><br />
                </td>
                <td><%# Item.Employee.Department1 == null ? "" : Item.Employee.Department1.NameString %><br /></td>
                <td><%# Item.ForRank %><br /></td>

                <td>
                  <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">                    
                    <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirmDeleteApplication(this.id);" runat="server" CommandArgument=<%# Item.ApplicationID%> CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
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

