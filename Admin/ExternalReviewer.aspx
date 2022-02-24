<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_ExternalReviewer" MaintainScrollPositionOnPostback="true" CodeFile="~/Admin/ExternalReviewer.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>External Reviewers</h3>
    <hr>
    <button type="button" class="btn btn-outline-primary btn-sm" data-toggle="modal" data-target="#myModal"><i class="fa fa-plus" aria-hidden="true"></i> Add External Reviewer</button>
    Sort by:
            <asp:DropDownList AutoPostBack="true" runat="server" class="form-control"
                ID="ddlSortBy" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged">
                <asp:ListItem Value="" Text="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Value="Specialty" Text="Specialty"></asp:ListItem>                
                <asp:ListItem Value="Mailing Address" Text="Mailing Address"></asp:ListItem>
                <asp:ListItem Value="Name" Text="Name"></asp:ListItem>
                <asp:ListItem Value="Major" Text="Major"></asp:ListItem>
            </asp:DropDownList>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel">Add External Reviewer</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <h5>Personal details</h5>
                        <hr>
                        <%--<div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Title</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbTitleAdd" placeholder="Title"></div>
                        </div>--%>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Name</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbNameAdd" required placeholder="Name"></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Rank</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbRankAdd" placeholder="Rank"></div>
                        </div>
                         <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Poistion Description</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbDescriptionAdd" placeholder="Poistion Description"></div>
                        </div>
                         
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Major</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbMajorAdd" placeholder="Major"></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Specialty</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbSpecialtyAdd" placeholder="Specialty"></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Active Area of Research</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbActiveAreaOfResearchAdd" placeholder="Active Area of Research"></div>
                        </div>
                        
                       
                        <%--<div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Organization</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbOrganizationAdd" placeholder="Organization"></div>
                        </div>--%>


                        <h5>Contact details</h5>
                        <hr>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Email</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbEmailAdd" placeholder="Email"></div>
                        </div>
                        <%--<div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Second email</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbEmail2Add" placeholder="Second email"></div>
                        </div>--%>
                        <%--<div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Address</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbAddressAdd" placeholder="Address"></div>
                        </div>--%>
                       <%-- <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Country</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbCountryAdd" placeholder="Country"></div>
                        </div>--%>
                       
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Phone and Fax</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbPhoneAndFaxAdd" placeholder="Phone and Fax"></div>
                        </div>
                         <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Webpage</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbWebpageAdd" placeholder="Webpage"></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Mailing Address</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbMailingAddressAdd" placeholder="Mailing Address"></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Comments</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbCommentsAdd" placeholder="Comments"></div>
                        </div>

                        <h5>User details</h5>
                        <hr>
                        <%--                <div class="form-group">                      
                      <label for="inputEmail3" class="col-sm-3 control-label">Username</label>
                      <div class="col-sm-9"><input runat="server" type="text" class="form-control" id="tbUsernameAdd" placeholder="Username"></div>
                    </div>--%>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Password</label>
                            <div class="col-sm-9">
                                <input runat="server" type="text" class="form-control" id="tbPasswordAdd" placeholder="Password"></div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:LinkButton runat="server" ID="lbtnAddExtReviewer"  CssClass="btn btn-primary" OnClick="lbtnAddExtReviewer_Click" > Add to List</asp:LinkButton>
                    
                </div>
            </div>
        </div>
    </div>
    <hr />
    <table class="table">
        <thead>
            <tr>
                <th scope="col">Serial #</th>
                <th scope="col">Personal details</th>
                <th scope="col">Contact details</th>
                <th scope="col">User details</th>
                <th scope="col">Action</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rpExternalReviewer" runat="server" ItemType="BL.Data.ExternalReviewer" OnItemCommand="rpExternalReviewer_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Container.ItemIndex + 1 %></th>
                        <td>
                            <div style="overflow: auto; width: 300px;">
                                <strong>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Item.Name 
                                            +" ("+ (Item.Status != "" && Item.Status != null? Item.Status 
                                            : RecordStatus.Inactive.ToString()) + ")" %>'></asp:Label>
                                    <input type="text" runat="server" class="form-control" id="tbNameEdit" visible="false" placeholder="Input Name" 
                                        value='<%# Item.Name %>' /></strong>
                                <asp:DropDownList id="ddlStatusEdit" runat="server" Visible="false"  
                                    SelectedValue='<%# Item.Status == RecordStatus.Active.ToString()
                                        ? RecordStatus.Active.ToString() : RecordStatus.Inactive.ToString()%>'>
                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                </asp:DropDownList>
                                <br>
                                <strong>ID:</strong>
                                <br>
                                <asp:Label ID="lblExtRevID" runat="server" Text='<%# Item.ExternalReviewerID%>'></asp:Label>
                                <br>
                                <strong>Involvement:</strong>
                                <br>
                                <asp:Label ID="lblInvolvement" runat="server" Text='<%# "Selection: " + Item.Form_ExtRev.Count %>'></asp:Label>
                                <br>
                                <asp:Label ID="lblFinalInvolvement" runat="server" Text='<%# "Final Selection: " + Item.Form_FinalExtRev.Count %>'></asp:Label>

                                <br>
                                <strong>Rank:</strong>
                                <asp:Label ID="lblRank" runat="server" Text='<%# Item.Rank %>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbRankEdit" visible="false" placeholder="Input Rank" value='<%# Item.Rank%>' /><br>
                                <strong>Description:</strong>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Item.Description%>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbDescriptionEdit" 
                                    visible="false" placeholder="Input Position Description" value='<%# Item.Description%>' /><br>
                                <strong>Major:</strong>

                                <asp:Label ID="lblMajor" runat="server" Text='<%# Item.Major %>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbMajorEdit" visible="false" placeholder="Input Major" value='<%# Item.Major%>' /><br>
                                <strong>Specialty:</strong>
                                <asp:Label ID="lblSpecialty" runat="server" Text='<%# Item.Specialty%>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbSpecialtyEdit" visible="false" placeholder="Input Specialty" value='<%# Item.Specialty%>' /><br>

                                <strong>Active Area of Research:</strong>
                                <asp:Label ID="lblActiveAreaOfResearch" runat="server" Text='<%# Item.ActiveAreaOfResearch %>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbActiveAreaOfResearchEdit" visible="false" placeholder="Input Active Area of Research" value='<%# Item.ActiveAreaOfResearch%>' /><br>

                                                           </div>
                        </td>
                        <td>
                            <div style="overflow: auto; width: 300px;">
                                 <strong>Email: </strong><a href='<%# "mailto:"+Item.Email %>'>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Item.Email%>'></asp:Label>
                                </a>
                                <input type="text" runat="server" class="form-control" id="tbEmailEdit" visible="false" placeholder="Input Email" value='<%# Item.Email%>' /><br>
                                 <strong>Office Phone and Fax:</strong><asp:Label ID="lblPhoneAndFax" runat="server" Text='<%# Item.PhoneAndFax%>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbPhoneAndFaxEdit" visible="false" placeholder="Input Office Phone and Fax" value='<%# Item.PhoneAndFax%>' /><br>
                                 <strong>Webpage:</strong>
                                <asp:Label ID="lblWebpage" runat="server" Text='<%# Item.Webpage%>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbWebpageEdit" visible="false" placeholder="Input Webpage" value='<%# Item.Webpage%>' /><br>
                                <strong>Mailing Address:</strong>
                                <asp:Label ID="lblMailingAddress" runat="server" Text='<%# Item.MailingAddress %>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbMailingAddressEdit" visible="false" placeholder="Input Mailing Address" value='<%# Item.MailingAddress%>' /><br>

                                 <strong>Comments:</strong><asp:Label ID="lblComments" runat="server" Text='<%# Item.Comments%>'></asp:Label>
                                <input type="text" runat="server" class="form-control" id="tbCommentsEdit" visible="false" placeholder="Input Comments" value='<%# Item.Comments%>' /><br>
                            </div>
                        </td>
                        <td>
                            <div style="overflow: auto; width: 200px;">
                                Username: <strong><%# Item.Email%></strong><br>
                                Parssord: <strong>
                                    <asp:Label ID="lblPassword" runat="server" Text='<%# Item.Password%>'></asp:Label>
                                    <input type="text" runat="server" class="form-control" id="tbPasswordEdit" visible="false" placeholder="Input Password" value='<%# Item.Password%>' /></strong>
                            </div>
                        </td>
                        <td>
                            <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument="<%# Item.ExternalReviewerID%>" CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                                <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);" runat="server" CommandArgument="<%# Item.ExternalReviewerID%>" CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                            </div>
                            <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server"   CommandArgument="<%# Item.ExternalReviewerID %>" CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" Tooltip="Cancel" runat="server" CommandArgument="<%# Item.ExternalReviewerID %>" CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
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
