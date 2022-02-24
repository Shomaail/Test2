<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" Inherits="Admin_ActionMessage" MaintainScrollPositionOnPostback="true" CodeFile="~/Admin/ActionMessages.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>Action Messages</h3>
    <hr>
    <button type="button" class="btn btn-outline-primary btn-sm" data-toggle="modal" data-target="#myModal"><i class="fa fa-plus" aria-hidden="true"></i>Add Action Message</button>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" id="myModalLabel">Add Action Message</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Action</label>
                            <div class="col-sm-9">
                                <asp:DropDownList runat="server" type="text" class="form-control" DataTextField="Text" DataValueField="Value" ID="ddlActionIDAdd"></asp:DropDownList></div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Next Task</label>
                            <div class="col-sm-9">
                                <asp:DropDownList runat="server" type="text" class="form-control" DataTextField="Text" DataValueField="Value" ID="ddlNextTaskIDAdd"></asp:DropDownList></div>

                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-3 control-label">Message</label>
                            <div class="col-sm-9">
                                <textarea required runat="server" style="height: 300px" type="text" class="form-control" id="tbMessageAdd" placeholder="Message"></textarea></div>
                        </div>
                        <div class="form-group">
                            The Following Variables can be used in a message: 
                            <ul>
                                <li>@@ForRank@@</li>
                                <li>@@ContractType@@</li>
                                <li>@@ParaForJointAppointee@@</li>
                                <li>@@Specialty@@</li>
                                <li>@@WebsiteURL@@</li>
                                <li>@@TopAuthority_Title@@</li>
                                <li>@@TopAuthority_TitleShort@@</li>
                                <li>@@TopLowAuthority_TitleShort@@</li>
                                <li>@@TopLowAuthority_Title@@</li>
                                <li>@@TopMostAuthority_TitleShort@@</li>
                                <li>@@TopMostAuthority_Title@@</li>
                                <li>@@PromotionSubcommittee1@@</li>
                                <li>@@PromotionSubcommittee1Dept@@</li>
                                <li>@@ApplicantRank@@</li>
                                <li>@@Applicant@@</li>
                                <li>@@ApplicantCollege@@</li>
                                <li>@@Sender@@</li>
                                <li>@@SenderDepartment@@</li>
                                <li>@@SenderCollege@@</li>
                                <li>@@Receiver@@</li>
                                <li>@@ReceiverDepartment@@</li>
                                <li>@@ReceiverCollege@@</li>                               
                            </ul>
                      
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:LinkButton runat="server" ID="lbtnAddActionMessage" CssClass="btn btn-primary" OnClick="lbtnAddActionMessage_Click"> Add </asp:LinkButton>

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
    <asp:Repeater ID="rpActionMessage" runat="server" ItemType="BL.Data.ActionMessage" OnItemCommand="rpActionMessage_ItemCommand">
        <ItemTemplate>
            <div runat="server" id="divApp" class="bg-open-promo">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-3 col-sm-12">
                            <strong> <%# Container.ItemIndex + 1 %></strong><br />
                            <strong>Action: </strong><%# Item.WFAction.Title +" (Action ID: "+ Item.ActionID+ ")"%>
                            <br />
                            <strong>Task:</strong> <%# Item.WFAction.Task.Title+"  (Task ID:"+ Item.WFAction.TaskID+ ")"%>
                            <br />
                            <strong>Next Task:</strong> <%# Item.Task.Title+"  (Next Task ID:"+ Item.NextTaskID+ ")"%>
                        </div>
                        <div class="col-lg-7 col-sm-12">
                            <strong>Message:</strong><textarea runat="server" disabled id="tbMessageEdit" style="width: 500px; height: 100px"> <%# Item.Message%></textarea>
                            <div class="form-group" id="divVariables" runat="server" visible="false">
                            The Following Variables can be used in a message: 
                            <ul>
                                <li>@@ForRank@@</li>
                                <li>@@ContractType@@</li>
                                <li>@@ParaForJointAppointee@@</li>
                                <li>@@SCMeetingNo@@</li>
                                <li>@@SCMeetingDate@@</li>
                                <li>@@Specialty@@</li>
                                <li>@@WebsiteURL@@</li>
                                <li>@@TopAuthority_Title@@</li>
                                <li>@@TopAuthority_TitleShort@@</li>
                                <li>@@TopLowAuthority_TitleShort@@</li>
                                <li>@@TopLowAuthority_Title@@</li>
                                <li>@@TopMostAuthority_TitleShort@@</li>
                                <li>@@TopMostAuthority_Title@@</li>
                                <li>@@PromotionSubcommittee1@@</li>
                                <li>@@PromotionSubcommittee1Dept@@</li>
                                <li>@@ApplicantRank@@</li>
                                <li>@@Applicant@@</li>
                                <li>@@ApplicantCollege@@</li>
                                <li>@@Sender@@</li>
                                <li>@@SenderDepartment@@</li>
                                <li>@@SenderCollege@@</li>
                                <li>@@Receiver@@</li>
                                <li>@@ReceiverDepartment@@</li>
                                <li>@@ReceiverCollege@@</li>                               
                            </ul>
                      
                        </div>
                        </div>
                        <div class="col-lg-1 col-sm-12">
                            <div runat="server" id="divNormalMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Item.ActionID+";"+Item.NextTaskID %>' CommandName="Edit" CssClass="btn btn-success"><i class="fa fa-pencil-square" aria-hidden="true"></i> Edit</asp:LinkButton>
                                <br />
                                <asp:LinkButton ID="lbtnDelete" OnClientClick="return ShowConfirm(this.id);" runat="server" CommandArgument='<%# Item.ActionID+";"+Item.NextTaskID%>' CommandName="Delete" CssClass="btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i> Delete</asp:LinkButton>
                            </div>
                            <div runat="server" id="divEditMode" class="btn-groupb btn-group-sm" role="group" aria-label="...">
                                <asp:LinkButton ID="lbtnUpdate" ToolTip="Update" runat="server" CommandArgument='<%# Item.ActionID+";"+Item.NextTaskID%>' CommandName="Update" Visible="false" CssClass="btn btn-success"><i class="fa fa-check-circle" aria-hidden="true"></i> Update</asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" ToolTip="Cancel" runat="server" CommandArgument='<%# Item.ActionID+";"+Item.NextTaskID%>' CommandName="Cancel" Visible="false" CssClass="btn btn-success"><i class="fa fa-close" aria-hidden="true"></i> Cancel</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ItemTemplate>
    </asp:Repeater>
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

