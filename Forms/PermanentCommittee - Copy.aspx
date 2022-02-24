<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" Inherits="Forms_PermanentCommittee" MaintainScrollPositionOnPostback="true" CodeFile="PermanentCommittee - Copy.aspx.cs" %>

<%@ MasterType VirtualPath="~/Forms/Forms.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                visible='<%# !GetAppTaskFormCompleted() && !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                <b>Incomplete: </b>
                <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
            <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                visible='<%# GetAppTaskFormCompleted()                     
                    && !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                <b>Complete:</b>
                <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
            <div runat="server" id="divWarningMsg" class="alert alert-warning" role="alert">
                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                    <b>NOTE: </b> <%= Resources.Resource.ScientificCouncilM15 %>
            </div>
            <hr>
            <hr>
            <div class="form-inline">
                <div class="form-group">
                    <label for="exampleInputName2">Employee ID:</label>
                    <input type="text" runat="server" class="form-control" id="tbEmployeeIDAdd" placeholder="Input Employee ID">
                    <br />
                    <asp:Label ID="lblNameAdd" runat="server"></asp:Label>
                </div>
                <asp:LinkButton runat="server" ID="lbtnSearch" ToolTip="Search" CssClass="btn btn-primary" OnClick="lbtnSearch_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                <asp:DropDownList runat="server" ID="ddlCommitteeType" DataTextField="Text" DataValueField="Value"></asp:DropDownList>
                <asp:LinkButton runat="server" ID="lbtnAdd" ToolTip="Search" CssClass="btn btn-success" OnClick="lbtnAdd_Click"><i class="fa fa-plus" aria-hidden="true"></i>Add</asp:LinkButton>
            </div>
            <hr>
            <h3>Scientific Council Members</h3>
            <table class="table">

                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Employee</th>
                        <%--<th scope="col">External Employee</th>   --%>
                    </tr>
                </thead>
                <tbody>

                    <asp:Repeater ID="rptPermanentCommitteeSC" runat="server" ItemType="BL.Data.Application_Role"
                        OnItemCommand="rptPermanentCommitteeSC_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                                <td><strong><%# Item.Employee.NameString %> </strong>
                                    <br />
                                    <%# "(Employee ID:"+ Item.EmployeeID +", Email:"+ Item.Employee.Email+" )"%>
                                    <br />
                                    <input type="text" runat="server" class="form-control" id="tbEmployeeIDEdit" visible="false" placeholder="Input Employee ID" value='<%# Item.EmployeeID %>' />
                                    <asp:LinkButton runat="server" ID="lbtnSearchSC" ToolTip="Search" 
                                        Visible="false"
                                        CssClass="btn btn-primary" 
                                        OnClick="lbtnSearchSC_Click"><i class="fa fa-search" aria-hidden="true"></i>
                                    </asp:LinkButton><asp:Label ID="lblNameEdit" runat="server"></asp:Label>
                                </td>
                                <%--<td><%# Item.ExternalEmployeeID != 0 && Item.ExternalEmployeeID != null ? Item.ExternalEmployee.NameString +" (External Employee ID:"+ Item.ExternalEmployeeID +")" : ""%> </td>                                    --%>
                                <td>
                                    <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Item.RoleID %>' CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);"  runat="server" CommandArgument='<%# Item.RoleID %>' CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                                    </div>
                                    <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                        <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server" CommandArgument='<%# Item.RoleID %>' CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" ToolTip="Cancel" runat="server" CommandArgument='<%# Item.RoleID %>' CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <h3>Scientific Council Coordinator</h3>
            <table class="table">

                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Employee</th>
                        <%--<th scope="col">External Employee</th>   --%>
                    </tr>
                </thead>
                <tbody>

                    <asp:Repeater ID="rptPermanentCommitteeSCC" runat="server" ItemType="BL.Data.Application_Role"
                        OnItemCommand="rptPermanentCommitteeSCC_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                                <td><strong><%# Item.Employee.NameString %> </strong>
                                    <br />
                                    <%# "(Employee ID:"+ Item.EmployeeID +", Email:"+ Item.Employee.Email+" )"%>
                                    <br />
                                    <input type="text" runat="server" class="form-control" id="tbEmployeeIDEdit" visible="false" placeholder="Input Employee ID" value='<%# Item.EmployeeID %>' />
                                    <asp:LinkButton runat="server" ID="lbtnSearchSC" ToolTip="Search" 
                                        Visible="false"
                                        CssClass="btn btn-primary" 
                                        OnClick="lbtnSearchSC_Click"><i class="fa fa-search" aria-hidden="true"></i>
                                    </asp:LinkButton><asp:Label ID="lblNameEdit" runat="server"></asp:Label>
                                </td>
                                <%--<td><%# Item.ExternalEmployeeID != 0 && Item.ExternalEmployeeID != null ? Item.ExternalEmployee.NameString +" (External Employee ID:"+ Item.ExternalEmployeeID +")" : ""%> </td>                                    --%>
                                <td>
                                    <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Item.RoleID %>' CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);"  runat="server" CommandArgument='<%# Item.RoleID %>' CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                                    </div>
                                    <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                        <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server" CommandArgument='<%# Item.RoleID %>' CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" ToolTip="Cancel" runat="server" CommandArgument='<%# Item.RoleID %>' CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <h3>Scientific Council Subcommittee Members</h3>
            <table class="table">

                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Employee</th>
                        <%--   <th scope="col">External Employee</th>                      --%>
                    </tr>
                </thead>
                <tbody>

                    <asp:Repeater ID="rptPermanentCommitteePSubComm" runat="server" ItemType="BL.Data.Application_Role"
                        OnItemCommand="rptPermanentCommitteePSubComm_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Container.ItemIndex + 1 %></th>
                                <td><strong><%# Item.Employee.NameString %> </strong>
                                    <br />
                                    <%# "(Employee ID:"+ Item.EmployeeID +", Email:"+ Item.Employee.Email+" )"%>
                                    <br />
                                    <input type="text" runat="server" class="form-control" id="tbEmployeeIDEdit" visible="false" placeholder="Input Employee ID" value='<%# Item.EmployeeID %>' />
                                    <asp:LinkButton runat="server" ID="lbtnSearchSCSubComm" ToolTip="Search" 
                                        Visible="false"
                                        CssClass="btn btn-primary" 
                                        OnClick="lbtnSearchSCSubComm_Click"><i class="fa fa-search" aria-hidden="true"></i>
                                    </asp:LinkButton><asp:Label ID="lblNameEdit" runat="server"></asp:Label>
                                </td>

                                <%--<td><%# Item.ExternalEmployeeID != 0 && Item.ExternalEmployeeID != null ? Item.ExternalEmployee.NameString +" (External Employee ID:"+ Item.ExternalEmployeeID +")" : ""%> </td>                                   --%>
                                <td>
                                    <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                        <asp:LinkButton ID="lbtnEdit" runat="server" 
                                            CommandArgument='<%# Item.RoleID %>' CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);" runat="server" 
                                            CommandArgument='<%# Item.RoleID %>' CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                                    </div>
                                    <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                        <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server" 
                                            CommandArgument='<%# Item.RoleID %>' CommandName="Update" Visible="false" 
                                            CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" ToolTip="Cancel" runat="server" 
                                            CommandArgument='<%# Item.RoleID %>' CommandName="Cancel" Visible="false" 
                                            CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
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
        <div style="text-align: center">
            <asp:LinkButton runat="server" ID="hideModalPopupViaServer0" Text="OK" OnClick="hideModalPopupViaServer0_Click" />
        </div>
    </asp:Panel>
</asp:Content>

