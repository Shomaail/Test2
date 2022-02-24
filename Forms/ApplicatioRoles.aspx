<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="ApplicatioRoles.aspx.cs" 
    Inherits="Forms_ApplicatioRoles" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%@ Register src="../Controls/Instruction.ascx" tagname="Instruction" tagprefix="uc3" %>
<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <script type="text/javascript">
        var confirmed = false;

        function ShowConfirm(controlID) {
            if (confirmed) { return true; }

            bootbox.confirm("Are you sure you want to delete?", function (result) {
                if (result) {
                    if (controlID != null) {
                        var controlToClick = document.getElementById(controlID);
                        if (controlToClick != null) {
                            confirmed = true;
                            controlToClick.click();
                            confirmed = false;
                        }
                    }
                }

            });

            return false;

        }
    </script>
    <uc3:Instruction ID="Instruction1" runat="server" />
    <br />
          <div id="divAddApplicationRole" runat="server" class="form-inline"
               visible='<%# Master.CurRoleID == (byte) RoleID.TopAuthority%>'>
            <div class="form-group">
             Employee Type: <asp:DropDownList runat="server" ID="ddlEmployeeType"  AutoPostBack="true"
                 class="form-control" 
                 OnSelectedIndexChanged="ddlEmployeeType_SelectedIndexChanged" >
                 <asp:ListItem Text="Internal" Value="Internal" Selected="True"></asp:ListItem>
                 <asp:ListItem Text="External" Value="External" ></asp:ListItem>
             </asp:DropDownList><br>
              <asp:Label runat="server" ID="lblID" Text="Employee ID:"></asp:Label>
              <input type="text" runat="server" class="form-control" id="tbEmployeeIDAdd" >               
              <asp:LinkButton runat="server" ID="lbtnSearch" ToolTip="Search" CssClass="btn btn-primary" OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
              <asp:Label ID="lblNameAdd" runat="server" ></asp:Label>
            </div>
            
              <br>
                  Role: <asp:DropDownList runat="server" ID="ddlRole" class="form-control" DataTextField="Text" DataValueField="Value" ></asp:DropDownList>
             <br>
                  <asp:LinkButton runat="server" ID="lbtnAdd"    ToolTip="Search" CssClass="btn btn-success" OnClick="lbtnAdd_Click"><i class="fa fa-plus" aria-hidden="true"></i>Add</asp:LinkButton>            
          </div>  
<asp:GridView ID="gvAppLicationRole" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="table" 
    ItemType="BL.Data.Application_Role" DataKeyNames="ApplicationID,RoleID" OnRowEditing="gvAppLicationRole_RowEditing"
    OnRowUpdating="gvAppLicationRole_RowUpdating" OnRowDeleting="gvAppLicationRole_RowDeleting" 
    OnRowCancelingEdit="gvAppLicationRole_RowCancelingEdit"
    CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Smaller">
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <RowStyle BackColor="#EFF3FB" />
    <EditRowStyle />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
    <Columns>
         <asp:TemplateField>
        <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate><%# "<b>"+Item.Employee.NameString+ "</b><br> Employee ID: "+ Item.EmployeeID%> </ItemTemplate>
            <EditItemTemplate>
                <table>
                    <tr><td>Employee ID : </td><td><asp:TextBox runat="server" ID="tbEmployeeIDEdit" Text=<%# Item.EmployeeID %>></asp:TextBox></td></tr>
                    
                </table>
               
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Rank">
            <ItemTemplate><%# Item.Employee.Rank %> </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Department">
            <ItemTemplate><%# Item.Employee.Department %> </ItemTemplate>
        </asp:TemplateField>
                <asp:TemplateField HeaderText="Role">
            <ItemTemplate><%# Item.Role.Title %> </ItemTemplate>
                    <EditItemTemplate><asp:Label runat="server" ID="lblRoleID" Text=<%# Item.RoleID %>></asp:Label></EditItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField ShowHeader="False" ControlStyle-Width="100px">
            <EditItemTemplate>
                <asp:LinkButton ID="lbtnUpdate" runat="server" CausesValidation="True" CommandName="Update" ToolTip="Update" CssClass="btn btn-default">
              Update <span class="glyphicon glyphicon-ok" aria-hidden="true"></span> 
                </asp:LinkButton>
                &nbsp;<asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="False" CommandName="Cancel" ToolTip="Cancel" CssClass="btn btn-default">
                  Cancel <span class="glyphicon glyphicon-remove" aria-hidden="true"></span> 
                </asp:LinkButton>
            </EditItemTemplate>
            <ItemStyle Width="10px" />
            <ItemTemplate>
                <div id="divEdit" runat="server" visible='<%# Master.CurRoleID == (byte) RoleID.TopAuthority 
                        && Item.RoleID != (byte) RoleID.Applicant%>'>
                    <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit"
                        CssClass="btn btn-default">
             Edit <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> 
                    </asp:LinkButton>
                    <br />
                    <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete, You can delete only members of a Committee or Council" CssClass="btn btn-default"
                        OnClientClick="return ShowConfirm(this.id);" Visible='<%# Item.Role.Title.Contains("Member") %>'>
            Delete <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                    </asp:LinkButton>
                </div>
            </ItemTemplate>
            </asp:TemplateField> 
    </Columns>
</asp:GridView>
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

