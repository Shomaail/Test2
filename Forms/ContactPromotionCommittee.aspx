<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="ContactPromotionCommittee.aspx.cs" Inherits="Forms_ContactPromotionCommittee" Title="Faculty Promotion System" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<%--<%@ Register Src="../Controls/ComposeEmailOld.ascx" TagName="ComposeEmail" TagPrefix="uc1" %>--%>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <uc2:Instruction ID="Instruction1" runat="server" />
    
    <br />
    <h3>Promotion Committee </h3>
    <asp:GridView ID="gvForm_FinalPC" runat="server" AutoGenerateColumns="False"  CssClass="table"
        CellPadding="4" EnableModelValidation="True" ItemType="BL.Data.Form_FinalPC"
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
            </asp:TemplateField>    
            
            <asp:TemplateField HeaderText="Position" SortExpression="Position">
                <ItemTemplate>
                    <asp:Label ID="LabelPosition" runat="server" Text="<%# GetStatus(Item.Position) %>"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" >
                <ItemTemplate>
                    <%#Item.Employee.NameString %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Department" >
                <ItemTemplate>
                    <%#Item.Employee.Department%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rank" >
                <ItemTemplate>
                    <%#Item.Employee.Rank%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email" >
                <ItemTemplate>
                    <%#Item.Employee.Email%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone" >
                <ItemTemplate>
                    <%#Item.Employee.Phone%>
                </ItemTemplate>
            </asp:TemplateField>
           
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>
    
    <br />
    <br />
    
    <asp:Button ID="ButtonSend" runat="server" OnClick="ButtonSend_Click" Text="Send Email to all Promotion Committee Members" ToolTip="Click this button to open a Compose Email Form" 
        CssClass="btn btn-primary btn-md" Visible="false"/><br />
   <a runat="server" id="aLink">Send Email to Promotion Committee</a>
    <br />
     <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
    <ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
        TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
        BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
        RepositionMode="RepositionOnWindowScroll">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="background: #0088ce; padding: 10px">
        <fieldset style="margin: 5px; border: 2px solid #0088ce; height: 90%;">
            <legend style="font-style: italic; font-weight: bold; background-color: #0088ce; color: white">Message </legend>
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
<%--    <uc1:ComposeEmail ID="ComposeEmail1" runat="server" OnEmailSent="ComposeEmail_OnEmailSent" OnEmailNotSent="ComposeEmail_OnEmailNotSent" />--%>
    <uc10:Alert ID="Alert1" runat="server" />
</asp:Content>

