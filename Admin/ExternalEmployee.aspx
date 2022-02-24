<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_ExternalEmployee" MaintainScrollPositionOnPostback="true" CodeFile="ExternalEmployee.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      
    <h1>External employees</h1>
          <hr>
    <button type="button" class="btn btn-outline-primary btn-sm" data-toggle="modal" data-target="#myModal"><i class="fa fa-plus" aria-hidden="true"></i> Add external employee</button>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none;">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                  <h4 class="modal-title" id="myModalLabel">Add external employee</h4>
                </div>
                <div class="modal-body">                  
                  <div class="form-horizontal">
                    <h5>Personal details</h5>
                    <hr>
                   <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Title</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbTitleAdd" placeholder="Title"></div>
                    </div>
                    <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Name</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbNameAdd" placeholder="Name"></div>
                    </div>
                    <div class="form-group">
                      <label for="inputEmail3" class="col-sm-3 control-label">Rank</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbRankAdd" placeholder="Rank"></div>
                    </div>
                    <div class="form-group">
                      <label for="inputEmail3" class="col-sm-3 control-label">Major</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbMajorAdd" placeholder="Major"></div>
                    </div>
                    <div class="form-group">
                      <label for="inputEmail3" class="col-sm-3 control-label">Website</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbWebsiteAdd" placeholder="Website"></div>
                    </div>
                    <div class="form-group">
                      <label for="inputEmail3" class="col-sm-3 control-label">Department</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbDepartmentAdd" placeholder="Department"></div>
                    </div>
                    <div class="form-group">
                      <label for="inputEmail3" class="col-sm-3 control-label">Organization</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbOrganizationAdd" placeholder="Organization"></div>
                    </div>


                    <h5>Contact details</h5>
                    <hr>
                    <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Email</label>
                      <div class="col-sm-9"><input  runat="server" type="email" class="form-control" id="tbEmailAdd" placeholder="Email"></div>
                    </div>
                    <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Second email</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbEmail2Add" placeholder="Second email"></div>
                    </div>
                    <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Address</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbAddressAdd" placeholder="Address"></div>
                    </div>
                    <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Country</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbCountryAdd" placeholder="Country"></div>
                    </div>
                    <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Office Phone</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbPhoneAdd" placeholder="Phone"></div>
                    </div>
                    <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Mobile</label>
                      <div class="col-sm-9"><input  runat="server" type="text" class="form-control" id="tbMobileAdd" placeholder="Mobile"></div>
                    </div>

                    <h5>User details</h5>
                    <hr>
    <%--                <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Username</label>
                      <div class="col-sm-9"><input runat="server" type="text" class="form-control" id="tbUsernameAdd" placeholder="Username"></div>
                    </div>--%>
                    <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Password</label>
                      <div class="col-sm-9"><input runat="server" type="text" class="form-control" id="tbPasswordAdd" placeholder="Password"></div>
                    </div>                    

                  </div>                  
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                  <asp:LinkButton runat="server" id="lbtnAddExtEmployee" CssClass="btn btn-primary" OnClick="lbtnAddExtEmployee_Click"> Add to List</asp:LinkButton>

                </div>
              </div>
            </div>
          </div>
<%--          <div class="form-inline">
            <div class="form-group">
              <label for="exampleInputName2">Employee ID:</label>
              <input type="text" runat="server" class="form-control" id="tbEmployeeIDAdd" placeholder="Input Employee ID">              
                <br />
                <asp:Label ID="lblNameAdd" runat="server" ></asp:Label>
            </div>
            <asp:LinkButton runat="server" ID="lbtnSearch" ToolTip="Search" CssClass="btn btn-primary" OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
            <asp:LinkButton runat="server" ID="lbtnAdd"    ToolTip="Search" CssClass="btn btn-success" OnClick="lbtnAdd_Click"><i class="fa fa-plus" aria-hidden="true"></i>Add</asp:LinkButton>            
          </div> --%>  
          <hr>   
      <table class="table">
            <thead>
              <tr>
                <th scope="col">Personal details</th>
                <th scope="col">Contact details</th>
                <th scope="col">User details</th>
                <th scope="col">Action</th>
              </tr>
            </thead>
          <tbody>
        
    <asp:Repeater ID="rptExternalEmployee" runat="server" ItemType="BL.Data.ExternalEmployee" OnItemCommand="rptExternalEmployee_ItemCommand">
        <ItemTemplate>      
            <tr>
                <td>
                    <div style="overflow: auto; width: 300px;">
                  <strong>  <asp:Label ID="lblNameString" runat="server" Text='<%# Item.NameString +" ("+ Item.Status + ")"%>'></asp:Label>
                    <input type="text" runat="server" class="form-control" id="tbNameStringEdit" Visible="false"  placeholder="Input Name" value='<%# Item.NameString %>' /></strong><br>                  
                  <strong>Rank:</strong> <asp:Label ID="lblRank" runat="server" Text='<%# Item.Rank %>'></asp:Label>
                    <input type="text" runat="server" class="form-control" id="tbRankEdit" Visible="false"  placeholder="Input Rank" value='<%# Item.Rank%>' /><br>
                  <strong>Department:</strong> <asp:Label ID="lblDepartment" runat="server" Text='<%# Item.Department %>'></asp:Label>
                    <input type="text" runat="server" class="form-control" id="tbDepartmentEdit" Visible="false"  placeholder="Input Department" value='<%# Item.Department%>' /><br>
                  <strong>Major:</strong> <asp:Label ID="lblMajor" runat="server" Text='<%# Item.Major %>'></asp:Label>
                    <input type="text" runat="server" class="form-control" id="tbMajorEdit" Visible="false"  placeholder="Input Major" value='<%# Item.Major%>' /><br>
                  <strong>Website:</strong> <asp:Label ID="lblWebsite" runat="server" Text='<%# Item.Website %>'></asp:Label>
                    <input type="text" runat="server" class="form-control" id="tbWebsiteEdit" Visible="false"  placeholder="Input Website" value='<%# Item.Website%>' /><br>
                  <strong>Organization:</strong> <asp:Label ID="lblOrganization" runat="server" Text='<%# Item.Organization%>'></asp:Label>
                    <input type="text" runat="server" class="form-control" id="tbOrganizationEdit" Visible="false"  placeholder="Input Organization" value='<%# Item.Organization%>' /><br>
                        </div>
                </td>
                <td>
                   <div style="overflow: auto; width: 300px;">
                     Email: <%# Item.Email %>	<asp:Label ID="lblEmail" runat="server" Text='<%# Item.Email%>'></asp:Label>
                    </a><input type="text" runat="server" class="form-control" id="tbEmailEdit" Visible="false"  placeholder="Input Email" value='<%# Item.Email%>' /><br>
                    Second Email: <a href= <%# "mailto:"+Item.Email2%>><asp:Label ID="lblEmail2" runat="server" Text='<%# Item.Email2%>'></asp:Label>
                    </a><input type="text" runat="server" class="form-control" id="tbEmail2Edit" Visible="false"  placeholder="Input Second Email" value='<%# Item.Email2%>' /><br>

                  Address: <asp:Label ID="lblAddress" runat="server" Text='<%# Item.Address + ", "+ Item.Country%>'></asp:Label>
                    <input type="text" runat="server" class="form-control" id="tbAddressEdit" Visible="false"  placeholder="Input Address" value='<%# Item.Address%>' />  
                     <input type="text" runat="server" class="form-control" id="tbCountryEdit" Visible="false"  placeholder="Input Country" value='<%# Item.Country%>' /><br>
                  Mobile:<asp:Label ID="lblMobile" runat="server" Text='<%# Item.Mobile%>'></asp:Label>
                    <input type="text" runat="server" class="form-control" id="tbMobileEdit" Visible="false"  placeholder="Input Mobile" value='<%# Item.Mobile%>' /><br>                  
                       </div>
                </td>
                <td>
                    <div style="overflow: auto; width: 200px;">
                  Username: <strong><%# Item.Email%></strong><br>
                  Parssord: <strong>			<asp:Label ID="lblPassword" runat="server" Text='<%# Item.Password%>'></asp:Label>
                    <input type="text" runat="server" class="form-control" id="tbPasswordEdit" Visible="false"  placeholder="Input Password" value='<%# Item.Password%>' /></strong>
                        </div>
                </td>
                <td>
                  <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument=<%# Item.ExternalEmployeeID%> CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                    <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);" runat="server" CommandArgument=<%# Item.ExternalEmployeeID%> CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                  </div>                  
                  <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                    <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server"   CommandArgument=<%# Item.ExternalEmployeeID %> CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                    <asp:LinkButton ID="lbtnCancel" Tooltip="Cancel" runat="server" CommandArgument=<%# Item.ExternalEmployeeID %> CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
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
