<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" Inherits="Admin_Department" CodeFile="~/Admin/Departments.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>Departments </h3>
    <button type="button" class="btn btn-outline-primary btn-sm" data-toggle="modal" data-target="#myModal"><i class="fa fa-plus" aria-hidden="true"></i> Add Department</button>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel">Add Department</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Name</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" required class="form-control" id="tbNameAdd" placeholder="Name"></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Short Name</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbShortNameAdd" placeholder="Short Name"></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Parent Dept</label>
                            <div class="col-sm-9">
                                <asp:DropDownList runat="server" type="text" class="form-control" DataTextField="Text" DataValueField="Value" ID="ddlParentDeptIDAdd"></asp:DropDownList></div>
                        </div>

                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Head</label>
                            <div class="col-sm-9">
                               <%-- <asp:TextBox runat="server" class="form-control" AutoPostBack="true" OnTextChanged="tbEmployeeIDAdd_TextChanged" id="tbEmployeeIDAdd" placeholder="Input Employee ID"></asp:TextBox>--%>
                                <input type="text" runat="server" class="form-control" id="tbEmployeeIDAdd" placeholder="Input Employee ID" />
                                <br />
                                <asp:Label ID="lblNameAdd" runat="server"></asp:Label>

                            </div>
                        </div>

                        <%--<asp:LinkButton runat="server" ID="lbtnSearch" ToolTip="Search" CssClass="btn btn-primary" OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>--%>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Phone</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" required class="form-control" id="tbPhoneAdd" placeholder="Phone"></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Website</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" required class="form-control" id="tbWebsiteAdd" placeholder="Website"></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Fax</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" required class="form-control" id="tbFaxAdd" placeholder="Fax"></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Head Role ID</label>
                            <div class="col-sm-9">
                                <asp:DropDownList runat="server" type="text" class="form-control" DataTextField="Text" DataValueField="Value" ID="ddlRoleIDAdd"></asp:DropDownList></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Type</label>
                            <div class="col-sm-9">
                                <asp:DropDownList runat="server" type="text" class="form-control" DataTextField="Text" DataValueField="Value" ID="ddlTypeAdd"></asp:DropDownList></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Status</label>
                            <div class="col-sm-9">
                                <asp:DropDownList runat="server" type="text" class="form-control" ID="ddlStatusAdd">
                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Deputy Email</label>
                            <div class="col-sm-9">
                                <input runat="server" type="email" required class="form-control" id="tbDeputyEmailADD" placeholder="Deputy Email "></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Deputy 2 Email</label>
                            <div class="col-sm-9">
                                <input runat="server" type="email" required class="form-control" id="tbDeputy2EmailADD" placeholder="Deputy Email "></div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:LinkButton runat="server" ID="lbtnAddDept" CssClass="btn btn-primary" OnClick="lbtnAddDept_Click"> Add </asp:LinkButton>

                </div>
            </div>
        </div>
    </div>




    <hr>
    <table class="table">
        <thead>
            <tr>
                <th scope="col">Department details</th>
                <th scope="col">Head details</th>
                <th scope="col">Action</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rpDepartment" runat="server" ItemType="BL.Data.Department" OnItemCommand="rpDepartment_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td>
                            <div style="overflow: auto; width: 300px;">
                                <asp:Label ID="lblName" runat="server" Text='<%# Item.NameString +" ("+ (Item.Status != "" && Item.Status != null? Item.Status : RecordStatus.Inactive.ToString() ) + ")"%>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbNameEdit" visible="false" placeholder="Input Name" value='<%# Item.Name %>' />
                                <asp:DropDownList ID="ddlStatusEdit" runat="server" Visible="false" SelectedValue='<%# Item.Status == RecordStatus.Active.ToString()? RecordStatus.Active.ToString() : RecordStatus.Inactive.ToString()%>'>
                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                </asp:DropDownList>
                                <br>
                                <strong>Short Name:</strong>
                                <asp:Label ID="lblShortName" runat="server" Text='<%# Item.ShortName %>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbShortNameEdit" visible="false" placeholder="Input Rank" value='<%# Item.ShortName%>' /><br>
                                <strong>Parent Dept:</strong>
                                <asp:Label ID="lblParentDept" runat="server" Text='<%# Item.ParentDeptString %>'></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlParentDeptIDEdit" type="text" Visible="false" 
                                    SelectedValue='<%# Item.ParentDeptID == null ? "0" : Item.ParentDeptID.ToString() %>' AppendDataBoundItems="True"
                                    class="form-control" DataTextField="Text" DataValueField="Value" OnDataBinding="ddlParentDeptIDEdit_DataBinding" >                                    
                                </asp:DropDownList><br>
                                <strong>Phone:</strong>
                                <asp:Label ID="lblPhone" runat="server" Text='<%# Item.Phone %>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbPhoneEdit" visible="false" placeholder="Input Phone" value='<%# Item.Phone%>' /><br>
                                <strong>Website:</strong>
                                <asp:Label ID="lblWebsite" runat="server" Text='<%# Item.Website %>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbWebsiteEdit" visible="false" placeholder="Input Website" value='<%# Item.Website%>' /><br>
                                <strong>Fax:</strong>
                                <asp:Label ID="lblFax" runat="server" Text='<%# Item.Fax %>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbFaxEdit" visible="false" placeholder="Input Fax" value='<%# Item.Fax%>' /><br>
                                <strong>Type:</strong>
                                <asp:Label ID="lblType" runat="server" Text='<%# Item.Type %>'></asp:Label>
                                <asp:DropDownList runat="server" type="text" Visible="false" class="form-control"
                                    SelectedValue='<%# Item.Type %>'
                                     DataTextField="Text" DataValueField="Value" ID="ddlTypeEdit" OnDataBinding="ddlTypeEdit_DataBinding"></asp:DropDownList><br>
                            </div>
                        </td>
                        <td>
                            <div style="overflow: auto; width: 300px;">
                                <%# Item.HeadString%>
                                    <br />
                                    <input type="text" runat="server" class="form-control" id="tbHeadEmpIDEdit" visible="false" placeholder="Input Head Employee ID" value='<%# Item.HeadEmpID%>' />                       
                                    <asp:LinkButton runat="server" ID="lbtnSearch" ToolTip="Search" 
                                        Visible="false"
                                        CommandArgument='<%#Item.DepartmentID %>' CssClass="btn btn-primary" 
                                        OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i>
                                    </asp:LinkButton><asp:Label ID="lblNameEdit" runat="server"></asp:Label>
                                <br />
                                 <strong>Head Email: </strong> <asp:Label runat="server" ID="lblEmail" Text='<%# string.IsNullOrEmpty(Item.HeadEmpID) ? "" : Item.Employee.Email%>'></asp:Label>                                       
                                <br />
                                <strong>Head Role: </strong> <asp:Label runat="server" ID="lblHeadRole" Text='<%# Item.HeadRoleID == null ? "" : Item.Role.Title %>'></asp:Label>                               
                                 <asp:DropDownList ID="ddlHeadRoleIDEdit" runat="server" type="text" class="form-control" DataTextField="Text" 
                                     SelectedValue='<%# Item.HeadRoleID %>'
                                     Visible="false" OnDataBinding="ddlHeadRoleIDEdit_DataBinding" DataValueField="Value" ></asp:DropDownList>
                                <br />    <strong>Deputy:</strong><asp:Label ID="lblDeputy" runat="server" Text='<%# Item.DeputyEmail%>'></asp:Label>
                                    <input type="email" runat="server" class="form-control" id="tbDeputyEmailEdit" visible="false" placeholder="Input Deputy Email" value='<%# Item.DeputyEmail%>' /><br>
                                    <strong>Deputy 2:</strong><asp:Label ID="lblDeputy2" runat="server" Text='<%# Item.Deputy2Email%>'></asp:Label>
                                    <input type="email" runat="server" class="form-control" id="tbDeputy2EmailEdit" visible="false" placeholder="Input Deputy2 Email" value='<%# Item.Deputy2Email%>' /><br>
                            </div>
                        </td>
                        <td>
                            <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument="<%# Item.DepartmentID%>" CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                                <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);" runat="server" CommandArgument="<%# Item.DepartmentID%>" CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                            </div>
                            <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server"   CommandArgument="<%# Item.DepartmentID %>" CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" Tooltip="Cancel" runat="server" CommandArgument="<%# Item.DepartmentID %>" CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
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
