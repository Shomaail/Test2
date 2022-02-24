<%@ Page Title="" Language="C#" MasterPageFile="~/ExtForms/ExtForm.master" AutoEventWireup="true" CodeFile="ExtAction.aspx.cs" Inherits="ExtForms_ExtAction" %>
<%@ MasterType VirtualPath="~/ExtForms/ExtForm.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <uc1:Instruction ID="Instruction1" runat="server" />
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
            <div id="divChecklist" runat="server" visible="false">

                <h3>Checklist:</h3>
                <ul>
                    <asp:DataList ID="lstFormsStatus" runat="server" Width="595px" CellPadding="4" ForeColor="#333333" CssClass="table-condensed">
                        <ItemTemplate>
                            <li>
                                <asp:Label ID="lblTask" runat="server" Text='<%# Eval("Task") %>'></asp:Label>
                                <asp:Label ID="lblStatus" runat="server" Font-Italic="true" ForeColor='<%# (bool)Eval("Status")?System.Drawing.Color.Green:System.Drawing.Color.Maroon %>' Text='<%# (bool)Eval("Status")?"OK!":"<b>Incomplete Data Entry!</b>: " + Eval("Message") %>'></asp:Label>
                                &nbsp;</li>
                        </ItemTemplate>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <HeaderStyle BackColor="#e7f7ff" Font-Bold="True" ForeColor="#3a9fe1" />
                    </asp:DataList>
                </ul>
            </div>           
            <h3>Actions:</h3><%--Style="margin-left: 10px"--%>
            <asp:RadioButtonList ID="lstActions" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="lstActions_SelectedIndexChanged">                        
                <asp:ListItem Text="Submit Evaluation" Value="Submit_External_Evaluation"></asp:ListItem>
                <asp:ListItem Text="Withdraw" Value="Withdraw_External_Evaluation"></asp:ListItem>
            </asp:RadioButtonList>
    


            <br />
            <asp:GridView ID="grdMessages" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="table"
                ForeColor="black" GridLines="None" Width="100%">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="TaskID" SortExpression="TaskID" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskID" runat="server" Text='<%# Bind("NextTaskID") %>'></asp:Label>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason to Withdraw" SortExpression="Message">
                        <ItemTemplate>
                          
                            <b>Message Contents:</b><br />
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
                <HeaderStyle BackColor="#0088ce" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <p align="center">
                <asp:Label ID="lblTaskMessage" runat="server" Text="" Style="color: #FF3300"></asp:Label>
            </p>
            <p align="center">
                <asp:Button ID="btnSubmit" runat="server" Text="Confirm Selected Action" class="btn btn-primary btn-md" OnClick="btnSubmit_Click" 
                    Width="235px" Visible="False" Height="30px" />&nbsp;
            </p>
            <p>
                &nbsp;
            </p>
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
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
<%--     <asp:UpdatePanel ID="UpdatePanelFileUpload" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
                 <li>
                                <asp:Label ID="lblTask" runat="server" Text="Evaluation Form"></asp:Label>


                                </li>
    <asp:RadioButtonList ID="rblExtAction" runat="server" 
        OnSelectedIndexChanged="rblExtAction_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem Text="Submit Evaluation" Value="Submit Evaluation" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Withdraw" Value="Withdraw" ></asp:ListItem>
    </asp:RadioButtonList></ContentTemplate></asp:UpdatePanel>--%>
</asp:Content>

