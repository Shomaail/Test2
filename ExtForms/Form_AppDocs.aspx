<%@ Page Title="" Language="C#" MasterPageFile="~/ExtForms/ExtForm.master" AutoEventWireup="true" CodeFile="Form_AppDocs.aspx.cs" Inherits="ExtForms_Form_AppDocs" %>
<%@ MasterType VirtualPath="~/ExtForms/ExtForm.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="gvForm_Attachment" runat="server" AutoGenerateColumns="false" CssClass="table" ItemType="BL.Data.Form_AttachmentAd" 
        DataKeyNames="Form_AttachmentAdID"
        EmptyDataText="There is no document selected for Evaluation">
        <Columns>
             <asp:TemplateField>
                <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
            </asp:TemplateField>  
                        <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDownload" runat="server" CausesValidation="false"
                                    Visible='<%# Item.DocumentName != "" %>' OnPreRender="lbtnDownload_PreRender"
                                    CommandName="Select" ToolTip='<%# Item.DocumentName %>' CssClass="btn btn-default">
                       Open <span class="glyphicon glyphicon-download-alt" aria-hidden="true"></span> 
                                </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Document Name">
                <ItemTemplate>
                    <asp:Label ID="lblDocumentName" runat="server" Text='<%# Item.DocumentName %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Document Size">
                <ItemTemplate>
                    <asp:Label ID="lblDocumentSize" runat="server" Text='<%# Item.DocumentSize %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type">
                <ItemTemplate>
                    <asp:Label ID="lblType" runat="server" Text='<%# Item.AttachmentCategory.Title %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
          <%--  <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Item.Description %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
                
        </Columns>
    </asp:GridView>

      <%--<asp:GridView ID="gvApplication_Attachments" runat="server" AutoGenerateColumns="False" CellPadding="5" class="table"
        ForeColor="Black" GridLines="Vertical" BorderColor="#0088ce" 
         DataKeyNames="Form_AttachmentID" OnSelectedIndexChanged="gvApplication_Attachments_SelectedIndexChanged">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
            Font-Size="Larger" Font-Strikeout="False" />
        <RowStyle BackColor="#F7F6F3" ForeColor="Black" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="Black" />
        <HeaderStyle BackColor="#e7f7ff" Font-Bold="True" ForeColor="#3a9fe1" />
        <AlternatingRowStyle BackColor="White" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
            </asp:TemplateField>            
            <asp:BoundField DataField="Form_AttachmentID" HeaderText="AttachmentID" Visible="False" />
            <asp:BoundField DataField="ApplicationID" HeaderText="ApplicationID" Visible="False" />                
            <asp:TemplateField HeaderText='<%$ Resources:Resource, Documentname %>' SortExpression="DocumentName">                
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("DocumentName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText='<%$ Resources:Resource, Description %>' SortExpression="Description">                
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Wrap="True" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText='<%$ Resources:Resource, DocumentType %>'>
               <ItemTemplate>
                    <asp:Label ID="lblAttachmentCategory" runat="server" Text='<%#((AttachmentCategoryID)(int.Parse(Eval("AttachmentCategoryID").ToString()))).ToString().Replace("_"," ") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText='<%$ Resources:Resource, UploadedBy %>'>
               <ItemTemplate>
                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%# GetEmployeeName()%>'> </asp:Label>
                    <br />
                    <asp:Label ID="lblRole" runat="server"  CssClass="label label-default" Text='<%#((RoleID)(int.Parse(Eval("RoleID").ToString()))).ToString().Replace("_"," ") %>' ></asp:Label>
                
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDownload" runat="server" CausesValidation="false" CommandName="Select" ToolTip="Download" CssClass="btn btn-default">
                       Download <span class="glyphicon glyphicon-download-alt" aria-hidden="true"></span> 
                    </asp:LinkButton>  
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            [<em>No documents were uploaded for this application</em>]
        </EmptyDataTemplate>
    </asp:GridView>--%>
</asp:Content>


