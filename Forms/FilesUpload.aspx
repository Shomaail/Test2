<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true" CodeFile="FilesUpload.aspx.cs"
    Inherits="Forms_FilesUpload" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../Controls/ComposeEmail.ascx" TagName="ComposeEmail" TagPrefix="uc1" %>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="Forms.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <%--    <input id="tbDateOfPublicationEscfdit" runat="server"  placeholder="Date Of Publication" title="Date Of Publication" type="date"/>--%>

    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanelFileUpload" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnUpload" />
            <asp:PostBackTrigger ControlID="rbltMaterialCheck" />

        </Triggers>
        <ContentTemplate>
            <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                visible='<%# !GetAppTaskFormCompleted() &&
                        (Master.CurRoleID == (byte) RoleID.Applicant||
                        Master.CurRoleID == (byte) RoleID.TopAuthority) && 
                        !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                <b>Incomplete: </b>
                <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                    Font-Italic="True"></asp:Label>
            </div>
            <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                visible='<%# GetAppTaskFormCompleted()  && 
                        (Master.CurRoleID == (byte) RoleID.Applicant||
                        Master.CurRoleID == (byte) RoleID.TopAuthority)&&
                        !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                <b>Complete:</b>
                <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                    Font-Italic="True"></asp:Label>
            </div>
            <div id="divFileUpload" runat="server"
                class='<%# (Master.CurRoleID == (byte) RoleID.Applicant             
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority )
            && !Master.App.ApplicationClosed? "": "disabledbutton" %>'>
                <div id="divUploadMultipleFiles" runat="server">
                    <table class="table" border="1">
                        <tr>
                            <td>
                                <label class="control-label" for="ApplicationAttachment">
                                    <input id="ApplicationAttachment" runat="server" type="file" title="Browse" accept=".pdf" multiple="multiple" />
                                </label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAttCatAddMulFiles" runat="server" DataTextField="Text" DataValueField="Value"
                                    DataSource='<%#  Enum.GetNames(typeof(AttachmentCategoryID)).Select(s => new { Text = s.Replace("_", " "),Value = (int)(Enum.Parse(typeof(AttachmentCategoryID),s)) }).Where(s=>s.Value != (int)AttachmentCategoryID.PCReport)%>'>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnUpload" ToolTip="Upload" runat="server" CausesValidation="true" OnClick="lbtnUpload_Click" CssClass="btn btn-default">Upload File(s) <span class="glyphicon glyphicon-cloud-upload" aria-hidden="true"></span> </asp:LinkButton>
                            </td>
                        </tr>
                    </table>

                    <asp:Label ID="Span" runat="server"></asp:Label>
                </div>
            </div>
            <div id="divMaterialCheck" runat="server" visible='<%# Master.CurRoleID == (byte) RoleID.Promotion_Committee_Chairman %>'>
                <asp:RadioButtonList ID="rbltMaterialCheck" runat="server"
                    OnSelectedIndexChanged="rbltMaterialCheck_SelectedIndexChanged"
                    AutoPostBack="True">
                    <asp:ListItem Value="MaterialOK">Approve and request the Vice President to send the Material to External Reviewer</asp:ListItem>
                    <asp:ListItem Value="MaterialNotOK">Request Applicant to update CV and Publication</asp:ListItem>
                </asp:RadioButtonList>

                <br />
                <br />
                </div>
                            <uc1:ComposeEmail ID="ComposeEmail1" runat="server" Visible="False" OnEmailSent="ComposeEmail_OnEmailSent" OnEmailNotSent="ComposeEmail_OnEmailNotSent" />

                <div id="divMaterialOKStatus" runat="server" visible='<%# Master.CurRoleID == (byte) RoleID.Promotion_Committee_Chairman || Master.CurRoleID == (byte) RoleID.TopAuthority%>'>
                    <h3>Promotion Committee opinion about material (CV and Publications)</h3>
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

           
            <asp:HiddenField ID="hdnRoleID" runat="server" />
            <%--<asp:UpdatePanel ID="UpdatePanelMain" runat="server">
    <ContentTemplate>--%>

            <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" CellPadding="5" class="table" ItemType="BL.Data.Form_AttachmentAd"
                DataKeyNames="Form_AttachmentAdID" ForeColor="Black" GridLines="Vertical" BorderColor="#0088ce"
                OnRowDeleting="gvAttachments_RowDeleting" OnRowCommand="gvAttachments_RowCommand"
                OnRowEditing="gvAttachments_RowEditing" OnRowCancelingEdit="gvAttachments_RowCancelingEdit"
                OnRowUpdating="gvAttachments_RowUpdating" OnRowDataBound="gvAttachments_RowDataBound"
                AllowPaging="True" OnPageIndexChanging="gvAttachments_PageIndexChanging" PageSize="200" ShowFooter="false">
                <FooterStyle BackColor="#0088ce"
                    Font-Size="Larger" Font-Strikeout="False" />
                <RowStyle BackColor="#F7F6F3" ForeColor="Black" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="Black" />
                <PagerSettings Mode="NumericFirstLast" NextPageText="Page" />
                <PagerStyle BackColor="#1A1718" ForeColor="White" HorizontalAlign="Center"
                    Font-Bold="True" Font-Italic="False" Font-Names="Copperplate Gothic Light"
                    Font-Size="X-Large" />
                <HeaderStyle BackColor=" #007D40" Font-Bold="True" ForeColor="White" Width="100px" />
                <AlternatingRowStyle BackColor="White" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblSerialNo" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                        </ItemTemplate>
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
                            <div id="divEditDelDownload" runat="server" visible='<%# (Item.RoleID == Master.CurRoleID|| Master.CurRoleID ==(byte) RoleID.TopAuthority) && Item.EmployeeID != "-1"
                                && !Master.App.ApplicationClosed%>'>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit"
                                    CssClass="btn btn-default">
                         Edit <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> 
                                </asp:LinkButton>
                                <br />
                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" CssClass="btn btn-default"
                                    OnClientClick="return ShowConfirm(this.id);">
                        Delete <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:UpdatePanel ID="UpdatePanelFileUpload2" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lbtnAdd" />
                                </Triggers>
                                <ContentTemplate>
                                    <div style="width: 100px">
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CausesValidation="False" CommandName="Add" ToolTip="Add" CssClass="btn btn-default" OnClick="lbtnAdd_Click">
                                            Add <span class="glyphicon glyphicon-plus" aria-hidden="true"></span> 
                                        </asp:LinkButton>
                                        <br />
                                        <input id="ApplicationAttachmentAddSingle" runat="server" type="file" title="Browse" accept=".pdf" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Document Type">
                        <ItemTemplate>
                            <asp:Label ID="lblAttachmentCategory" runat="server" Text='<%# Item.EmployeeID != "-1"? ((AttachmentCategoryID)(int.Parse(Item.AttachmentCategoryID.ToString()))).ToString().Replace("_"," ") : "" %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlAttCat" runat="server" SelectedValue='<%# Item.AttachmentCategoryID %>' DataTextField="Text" DataValueField="Value"
                                DataSource='<%# Enum.GetNames(typeof(AttachmentCategoryID)).Select(s => new { Text = s.Replace("_", " "),Value = (int)(Enum.Parse(typeof(AttachmentCategoryID),s)) }).Where(s=>s.Value != (int)AttachmentCategoryID.PCReport)%>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlAttCatAdd" runat="server" DataTextField="Text" DataValueField="Value"
                                DataSource='<%# Enum.GetNames(typeof(AttachmentCategoryID)).Select(s => new { Text = s.Replace("_", " "),Value = (int)(Enum.Parse(typeof(AttachmentCategoryID),s)) }).Where(s=>s.Value != (int)AttachmentCategoryID.PCReport)%>'>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title" SortExpression="Description">

                        <EditItemTemplate>
                            <asp:TextBox ID="tbDescriptionEdit" TextMode="MultiLine" runat="server" Text='<%# Item.Description %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%--<a href="#" data-toggle="tooltip" data-placement="bottom" title='<%# Item.Description %>'>Bottom</a>--%>

                            <asp:Label ID="lblDescription" runat="server" ToolTip='<%# Item.Description %>' Text='<%# Item.Description %>'></asp:Label>
                            (<asp:Label ID="lblSize" runat="server" ToolTip="File Size" Text='<%# Item.DocumentSize%>'></asp:Label>)
                            <br />
                            <asp:LinkButton ID="lbtnDownload" runat="server" CausesValidation="false"
                                Visible='<%# Item.DocumentName != "" %>' OnPreRender="lbtnDownload_PreRender"
                                CommandName="Select" ToolTip='<%# "Download "+ Item.DocumentName %>' CssClass="btn btn-default">
                       <span class="glyphicon glyphicon-download-alt" aria-hidden="true"></span> 
                            </asp:LinkButton>

                        </ItemTemplate>
                        <FooterTemplate>
                            <textarea id="tbDescriptionAdd" runat="server" textmode="MultiLine" placeholder="Title" title="Title"></textarea>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Of Publication" SortExpression="DateOfPublication" Visible="false">
                        <EditItemTemplate>
                            <input id="tbDateOfPublicationEdit" runat="server" type="text" placeholder="Date Of Publication" title="Date Of Publication"
                                value='<%# Item.DateOfPublication == null ? "" : Item.DateOfPublication.Value.ToShortDateString() %>' />
                            <%--<asp:TextBox ID="tbDateOfPublicationEdit" TextMode="Datetime" ClientIDMode="Static"  runat="server"  Text='<%# Item.DateOfPublication == null ? DateTime.Now.Date.ToString() : Item.DateOfPublication.Value.Date.ToString() %>'></asp:TextBox>                       --%>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDateOfPublication" runat="server" Text='<%# Item.DateOfPublication == null ? "" : Item.DateOfPublication.Value.ToShortDateString() %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <%--                    <input type='text' class="form-control" id="tbDateOfPublicationAdd" runat="server" />
                    <span class="input-group-addon">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </span>--%>
                            <input id="tbDateOfPublicationAdd" runat="server" type="text" placeholder="Date Of Publication" title="Date Of Publication" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Journal" SortExpression="Journal" Visible="false">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbJournalEdit" TextMode="MultiLine" runat="server" Text='<%# Item.Journal %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblJournal" runat="server" Text='<%# Item.Journal %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <textarea id="tbJournalAdd" runat="server" textmode="MultiLine" placeholder="Journal " title="Journal "></textarea>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uploaded By">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Item.Employee == null ? "": Item.Employee.NameString%>'> </asp:Label>
                            <br />
                            <asp:Label ID="lblRole" runat="server" CssClass="label label-default" Text='<%# Item.Employee == null ? " ": Item.Role.Title %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Selection For Ext. Reviewers">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSel4ExtRev" runat="server"
                                Checked='<%#  Item.SelectionForExtRev == null ? false: Item.SelectionForExtRev.Value%>'
                                Visible='<%#Item.RoleID == (byte) RoleID.Applicant 
                                    || (Master.CurRoleID ==(byte) RoleID.TopAuthority)%>'
                                Enabled='<%# (Item.EmployeeID != "-1" && Master.CurRoleID == (byte) RoleID.Applicant
                                    && Item.RoleID == (byte) RoleID.Applicant && !Item.Application.ApplicationClosed)
                                    || (Master.CurRoleID ==(byte) RoleID.TopAuthority)%>'
                                AutoPostBack="True"
                                OnCheckedChanged="cbSel4ExtRev_CheckedChanged"
                                ToolTip="Check to enable this document to be viewed by External Reviewers" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No Of Authors" Visible="false">
                        <ItemTemplate>
                            <%--            <input type="number" value="1" min="1" max="50" runat="server" id="tbNoOfAuthors" onserverchange="tbNoOfAuthors_OnChange"/>--%>

                            <asp:TextBox runat="server" MaxLength="50" AutoPostBack="true" Text='<%# Item.NoOfAuthors%>' TextMode="Number" ID="tbNoOfAuthors"
                                OnTextChanged="tbNoOfAuthors_TextChanged" Width="50px"></asp:TextBox>
                            <%-- <asp:RangeValidator ID="rvNoOfAuthors" runat="server"
                                        ControlToValidate="tbNoOfAuthors" ErrorMessage="Must be &gt; 0 & &lt; 20" MinimumValue="1" MaximumValue="19" />--%>
                            <%--<asp:CompareValidator ID="CompareValidatorNoOfAuthors" runat="server"
      ControlToValidate="tbNoOfAuthors" ErrorMessage="Must be &gt; 0"
      Operator="GreaterThan" Type="Integer"
      ValueToCompare="0" />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Is First Author?" Visible="false">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="cbFirstAuthor" runat="server"
                                Checked='<%#  Item.FirstAuthor%>'
                                AutoPostBack="True"
                                OnCheckedChanged="cbFirstAuthor_CheckedChanged"
                                ToolTip="Check if the applicant is the First Author" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="Points" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPoints" runat="server" Text='<%# Item.Points %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            Total:
                            <asp:Label ID="lblPoints" runat="server" Text='<%# GetTotalPoints() %>'></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="Acceptable?" Visible="false">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbAccept" runat="server"
                                Checked='<%#  Item.Accept%>'
                                AutoPostBack="True"
                                OnCheckedChanged="cbAcceptable_CheckedChanged"
                                ToolTip="Check if the publication is acceptable" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks" Visible="false">
                        <EditItemTemplate>
                            <asp:TextBox ID="tbRemarksEdit" TextMode="MultiLine" runat="server" Text='<%# Item.Remarks %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Item.Remarks %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <textarea id="tbRemarksAdd" runat="server" textmode="MultiLine" placeholder="Remarks" title="Remarks"></textarea>
                        </FooterTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <EmptyDataTemplate>
                    [<em>No files were uploaded for this application</em>]
                </EmptyDataTemplate>
            </asp:GridView>
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

