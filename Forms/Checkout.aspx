<%@ Page Language="C#" Debug="true" MasterPageFile="Forms.master" AutoEventWireup="true"
    Inherits="Forms_Checkout" MaintainScrollPositionOnPostback="true" CodeFile="Checkout.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="../Controls/ComposeEmail.ascx" TagName="ComposeEmail" TagPrefix="uc1" %>


<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div >
                <div id="actions-feedback">
                    <div id="divChecklist" runat="server" class="ations-check">
                        <h4>Checklist</h4>
                        <ul>
                            <asp:DataList ID="lstFormsStatus" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <h5>
                                            <asp:Label ID="lblTask" runat="server" Text='<%# Eval("Task") %>'></asp:Label></h5>
                                        <span class='<%# (bool) Eval("Status")? "label label-success btn-complete":"label label-danger btn-incomplete"%>'>
                                            <span class='<%# (bool) Eval("Status")? "glyphicon glyphicon-ok":"glyphicon glyphicon-remove" %>'></span>
                                            <%#(bool) Eval("Status")?"Complete" : "Incomplete" %></span>
                                        <asp:Label ID="lblStatus" runat="server"
                                            Text='<%# Eval("Message") %>'>
                                        </asp:Label>
                                    </li>
                                </ItemTemplate>
                            </asp:DataList>
                        </ul>
                    </div>
                    <div runat="server" id="divMsg" class="alert alert-warning" role="alert">
                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                        <b>NOTE: </b>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    <div runat="server" id="divMsgForLockedActions" class="alert alert-warning" role="alert">
                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                        <b>NOTE: </b>
                        <asp:Label ID="lblMsgForLockedActions" runat="server"></asp:Label>
                    </div>
                    <div class="ations-action">
                        <h4>Actions</h4>
                        <div style="padding-left: 20px">
                            <asp:RadioButtonList ID="lstActions" runat="server" AutoPostBack="True" CssClass="radio"
                                DataTextField="Text" DataValueField="Value" OnSelectedIndexChanged="lstActions_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </div>
                       
                        <div class="panel panel-default" id="divMessages" runat="server" visible="false">
                            <div class="panel-heading grey">
                                <h3 class="panel-title">Message</h3>
                            </div>
                            <div class="l-grey">
                                <ul>
                                    <asp:Repeater ID="rpMessages" runat="server" ItemType="vw_NextTaskDTO">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lblTaskID" runat="server" Text='<%# Item.NextTaskID %>' Visible="false"></asp:Label>
                                                <div class="panel-body">
                                                    <div class="msg-to"><strong>To:</strong> <%# Item.NextRole %></div>
                                                    <div class="msg-sub">
                                                        <strong>Subject:</strong> <%# Item.NextTask                                                                                                    
                                                .Replace("@@TopAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopAuthority_TitleShort"])
                                                .Replace("@@TopLowAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"])
                                                .Replace("@@TopMostAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"])%>
                                                    </div>
                                                    <div class="msg-msg"><strong>Message:</strong></div>
                                                    <div>
                                                        <asp:TextBox TextMode="MultiLine" runat="server" ID="tbMessage" class="form-control" Rows="8" Text='<%# Item.Message %>'>
                                                        </asp:TextBox>
                                                    </div>

                                                </div>

                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <div class="msg-note"><strong>NOTE:</strong> Message content is editable, please click the message to edit</div>
                                </ul>
                            </div>
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="Confirm Selected Action" class="btn btn-primary btn-md" OnClick="btnSubmit_Click" Width="235px" Visible="False" Height="30px" />
                            <%--<a href="" class="btn btn-primary">Send for Processing Application</a>--%>
                        </div>
                         <div id="divMaterialCheck" runat="server" visible='<%# Master.CurRoleID == (byte) RoleID.Promotion_Committee_Chairman %>'>
<h4>Actions Exclusively for Promotion Committee Chairman</h4>
                <asp:RadioButtonList ID="rbltMaterialCheck" runat="server"
                    OnSelectedIndexChanged="rbltMaterialCheck_SelectedIndexChanged"
                    AutoPostBack="True">
                    <asp:ListItem Value="MaterialOK">  Material is approved to be sent to the External Reviewers</asp:ListItem>
                    <asp:ListItem Value="MaterialNotOK"> Request Applicant to update CV and Publication</asp:ListItem>
                </asp:RadioButtonList>

                <br />
                
                <br />
                </div>
                        <uc1:ComposeEmail ID="ComposeEmail1" runat="server" Visible="False" OnEmailSent="ComposeEmail_OnEmailSent" OnEmailNotSent="ComposeEmail_OnEmailNotSent" />
                        <div id="divMaterialOKStatus" runat="server" visible='<%# Master.CurRoleID == (byte) RoleID.Promotion_Committee_Chairman || Master.CurRoleID == (byte) RoleID.TopAuthority%>'>
                    <h5>Promotion Committee opinion about material (CV and Publications)</h5>
                    <table class="table">
                        <tr>
                            <td></td>
                            <td>Is Material OK?</td>
                            <td>Material OK Email</td>
                            <td>Material Not OK Email</td>
                            <td>Email from <%= ConfigurationManager.AppSettings["TopAuthority_TitleShort"] %></td>
                        </tr>
                        <tr>
                            <td>Promotion Committee Message for Material to be sent to External Reviewers: </td>
                            <td style="align-items: center">
                                <div runat="server" style="align-content: center" visible='<%# GetMaterialOKStatus() %>'>
                                    <span class="text-success" title="Material suitable to be sent to external reviewers"><i class="fa fa-check-circle-o fa-2x" aria-hidden="true"></i></span>
                                    <br />
                                </div>
                                <div runat="server" style="align-content: center" visible='<%# !GetMaterialOKStatus() %>'>
                                    <span class="text-danger" title="Material not suitable to be sent to external reviewers"><i class="fa fa-exclamation -circle-o fa-2x" aria-hidden="true"></i></span>
                                    <br />
                                </div>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnViewEmailFromPCMaterialOK" runat="server" CausesValidation="False"
                                    Visible='<%# GetMaterialOKStatus() %>' OnClick="lbtnViewEmailFromPCMaterialOK_Click"
                                    ToolTip="View Email sent from Promotion Committee saying Material is appropriate" CssClass="btn btn-default">
                            <span class="glyphicon glyphicon-sunglasses" aria-hidden="true"></span> 
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnViewEmailFromPCMaterialNotOK" runat="server"
                                    Visible='<%# !GetMaterialOKStatus() %>' OnClick="lbtnViewEmailFromPCMaterialNotOK_Click" CausesValidation="False"
                                    ToolTip="View Email sent from Promotion Committee saying Material is not appropriate" CssClass="btn btn-default">
                            <span class="glyphicon glyphicon-sunglasses" aria-hidden="true"></span> 
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnViewEmailFromTopAuthority" runat="server"
                                    OnClick="lbtnViewEmailFromTopAuthority_Click" CausesValidation="False"
                                    CssClass="btn btn-default">
                            <span class="glyphicon glyphicon-sunglasses" aria-hidden="true"></span> 
                                </asp:LinkButton><br />
                                <asp:Label ID="lblPCMessage" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                    </div>
                </div>
            </div>


            <%-- <h3>Actions:</h3>--%>

            <%-- <asp:RadioButtonList ID="lstActions" runat="server" AutoPostBack="True" CssClass="radio"
                DataTextField="Text" DataValueField="Value" OnSelectedIndexChanged="lstActions_SelectedIndexChanged">
            </asp:RadioButtonList>--%>
            <br />
            <%--    <asp:Repeater ID="rpMessages" runat="server" ItemType="WebApplication.ClassFiles.vw_NextTaskDTO">
                <ItemTemplate>
                </ItemTemplate>
            </asp:Repeater>--%>
            <%--  <asp:GridView ID="grdMessages" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="table"
                ForeColor="black" GridLines="None" Width="100%">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="TaskID" SortExpression="TaskID" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskID" runat="server" Text='<%# Bind("NextTaskID") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Message" SortExpression="Message">
                        <ItemTemplate>
                            <b>To:</b> <%# Eval("NextRole") %>
                            <br />
                            <b>For:</b> <%# Eval("NextTask").ToString()
                                                .Replace("@@TopAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopAuthority_TitleShort"])
                                                .Replace("@@TopLowAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopLowAuthority_TitleShort"])
                                                .Replace("@@TopMostAuthority_TitleShort@@",ConfigurationManager.AppSettings["TopMostAuthority_TitleShort"])
                            %>
                            <br />
                            <br />
                            <b>Message Contents:</b><i>The following message is editable</i><br />
                            <asp:TextBox ID="txtMessage" runat="server" Height="250px" Text='<%# Bind("Message") %>'
                                TextMode="multiLine" Font-Names="Georgia, Times" Width="100%"
                                MaxLength="4000" CausesValidation="False"></asp:TextBox><br />

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="black" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#1e9966" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>--%>

            <%--   <p align="center">
                <asp:Button ID="btnSubmit" runat="server" Text="Confirm Selected Action" class="btn btn-primary btn-md" OnClick="btnSubmit_Click" Width="235px" Visible="False" Height="30px" />&nbsp;
            </p>--%>
            <p>
                &nbsp;
            </p>
            <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
            <ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
                TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
                BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
                RepositionMode="RepositionOnWindowScroll">
            </ajaxtoolkit:ModalPopupExtender>
            <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="background: #007d40; padding: 10px">
                <fieldset style="margin: 5px; border: 2px solid #007d40; height: 90%;">
                    <legend style="font-style: italic; font-weight: bold; background-color: #007d40; color: white">Message </legend>
                    <br />
                    <div style="justify-content: center">
                        <asp:Label ForeColor="White" ID="labelProgrammaticPopup0" Font-Size="Large" runat="server"></asp:Label>
                    </div>
                    <div align="center">
                        <br />
                        <asp:LinkButton ID="hideModalPopupViaServer0" runat="server" OnClick="hideModalPopupViaServer0_Click" class="btn btn-default"
                            ToolTip="Press this button to send the email">OK <span class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
                    </div>
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

