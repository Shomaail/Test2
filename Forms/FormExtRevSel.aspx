<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true"
    Inherits="Forms_FormExtRevSel" MaintainScrollPositionOnPostback="true" ValidateRequest="false" CodeFile="FormExtRevSel.aspx.cs" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%--<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>--%>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <script type="text/javascript">
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
    </script>--%>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFillTB" />
            <asp:PostBackTrigger ControlID="btnAddExtRev" />
            <asp:PostBackTrigger ControlID="btnShowDivExcel" />     
            <asp:PostBackTrigger ControlID="btnShowInsertExtRev" />                 
        </Triggers>
        <ContentTemplate>
            <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                visible='<%# !GetAppTaskFormCompleted() && !(FormMode.Contains("Edit") || FormMode.Contains("Add") || FormMode.Contains("ExtRevDetail") )
                          && !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                <b>Incomplete: </b>
                <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
            <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                visible='<%# GetAppTaskFormCompleted() && !(FormMode.Contains("Edit") || FormMode.Contains("Add") || FormMode.Contains("ExtRevDetail") )
                         &&!Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                <b>Complete:</b>
                <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
             <div runat="server" id="divMsg" class="alert alert-warning" role="alert" 
        visible='<%# Master.App.ApplicationClosed %>'>
        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
        <b>NOTE: </b> <%= Resources.Resource.ExternalReviewerSelectionM17 %>
    </div>
            <div id="divFinalExtRev" runat="server" visible='<%#FormMode == "ExtRev" %>' 
                style='overflow: auto; width: 1150px;'>
                <h3>Final External Reviewers List</h3>
                <asp:GridView ID="gvFinalExtRev" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4"
                    CssClass="table" DataKeyNames="ApplicationID, ExternalReviewerID" OnRowCommand="gvFinalExtRev_RowCommand"
                    OnRowDeleting="gvFinalExtRev_RowDeleting" ItemType="BL.Data.Form_FinalExtRev"
                    OnRowDataBound="gvFinalExtRev_RowDataBound" OnRowEditing="gvFinalExtRev_RowEditing"
                    OnDataBound="gvFinalExtRev_DataBound"
                    ForeColor="#333333" GridLines="None" EmptyDataText="No Final External Reviewer is selected">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Priority" SortExpression="Serial">
                            <ItemTemplate>
                                <div style="align-content: center; text-align: center;">
                                    <div>
                                        <asp:ImageButton ID="iBtnArrowUp" ImageUrl="~/images/Actions-arrow-up-icon.png" runat="server"
                                            Visible='<%# !Master.App.ApplicationClosed %>'
                                            ToolTip="Click to move the row up"
                                            CommandName="MoveRowUp" CommandArgument='<%# Item.ExternalReviewerID %>' />
                                    </div>
                                    <div>
                                        <asp:Label ID="lblSerial" runat="server" Text='<%# Item.Serial%>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:ImageButton ID="iBtnArrowDn" ImageUrl="~/images/Actions-arrow-Down-icon.png" runat="server"
                                            Visible='<%# !Master.App.ApplicationClosed %>'
                                            ToolTip="Click to move the row down"
                                            CommandName="MoveRowDn" CommandArgument='<%# Item.ExternalReviewerID %>' />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit" 
                                    CssClass="btn btn-default" OnClick="lbtnEdit_Click"
                                    Visible='<%# !Master.App.ApplicationClosed %>'
                                    CommandArgument='<%# Item.ExternalReviewerID %>'>
                         Edit <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> 
                                </asp:LinkButton>
                                <br />
                                <br />
                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="Delete" 
                                    ToolTip='<%# Item.WLStatus == 
                                        WillingessStatus.Not_Sent.ToString().Replace("_", " ") ? 
                                         Resources.Resource.Delete : Resources.Resource.ExternalReviewerSelectionM5 %>' 
                                    Enabled='<%# Item.WLStatus == WillingessStatus.Not_Sent.ToString().Replace("_", " ") %>'
                                    class="btn btn-danger btn-md" OnClick="lbtnDelete_Click"
                                    Visible='<%# !Master.App.ApplicationClosed %>'
                                    CommandArgument='<%# Item.ExternalReviewerID %>' OnClientClick="return ShowConfirm(this.id);">
                        Delete <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Source" HeaderText="Source" SortExpression="Source" />
                        

                        <asp:TemplateField ShowHeader="False">

                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDetails" runat="server" CausesValidation="false" CommandName="SelectDetail" ToolTip="Select Detail" CssClass="btn btn-default"
                                    CommandArgument='<%# Item.ExternalReviewerID %>'>
                        Details <span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:BoundField DataField="ExternalReviewerID" HeaderText="ExternalReviewerID" SortExpression="ExternalReviewerID" Visible="false" />
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <strong><asp:Label ID="lblName" runat="server" Text='<%# Item.ExternalReviewer.NameString%>'></asp:Label></strong>
                                <br />
                                <strong>Email:</strong>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Item.ExternalReviewer.Email %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Publications">
                    <ItemTemplate>
                        <%# Item.ExternalReviewer.TotalPublications%>
                        <br />
                        No of Journals: <%# Item.ExternalReviewer.NoOfJournals%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="H-Index">
                    <ItemTemplate>
                        <%# Item.ExternalReviewer.HIndex%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Citations">
                    <ItemTemplate>
                        <%# Item.ExternalReviewer.Citations%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total journal publications and H-index">
                                    <ItemTemplate><asp:label runat="server" Text='<%# Item.ExternalReviewer.HIndex.HasValue && Item.ExternalReviewer.NoOfJournals.HasValue 
                                                          ? (Item.ExternalReviewer.HIndex.Value + Item.ExternalReviewer.NoOfJournals.Value).ToString() :"" %>'></asp:label></ItemTemplate>                                    
                 </asp:TemplateField>  <asp:TemplateField HeaderText="Affiliation">
                    <ItemTemplate>
                        <div style='overflow: auto; width: 100px; height: 90px;'>
                            <asp:Label ID="lblMailingAddress" runat="server" Text='<%# Item.ExternalReviewer.MailingAddress %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Specialty">
                    <ItemTemplate>
                        <div style='overflow: auto; width: 100px; height: 90px;'>
                            <asp:Label ID="lblSpecialty" runat="server" Text='<%# Item.ExternalReviewer.Specialty %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

           
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#009966" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                <asp:LinkButton ID="lbtnExtRevList4VR" ValidationGroup="ExtRevSaveBtn" runat="server">External Reviewers List (printable view)</asp:LinkButton>

            </div>

            <div id="divFormExtRevSuggested" runat="server" visible='<%#FormMode == "ExtRev" %>'
                 style='overflow: auto; width: 1150px;'>
                <h3>Suggested External Reviewers List</h3>
                <asp:GridView ID="gvExternalReviewersSugg" runat="server" AutoGenerateColumns="False" DataSourceID="odsFormExtRevSugg" EnableModelValidation="True" CellPadding="4"
                    ItemType="BL.Data.Form_ExtRev"
                    CssClass="table" DataKeyNames="ApplicationID,Type,Serial" OnRowCommand="gvExternalReviewersSugg_RowCommand"
                    ForeColor="#333333" GridLines="None" EmptyDataText="No External Reviewer is suggested">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>

                        <asp:TemplateField HeaderText="Serial" SortExpression="Serial">
                            <ItemTemplate>
                                <div style="align-content: center; text-align: center;">
                                    <div>
                                        <asp:Label ID="lblSerial" runat="server" Text='<%# (int.Parse(Item.Serial.ToString()) + 1).ToString() %>'></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSelect" runat="server" CausesValidation="False" class="btn btn-primary btn-md"
                                    Enabled='<%# GetSelectStatus(Item.ExternalReviewerID) && !Master.App.ApplicationClosed ? true : false %>'
                                    Tooltip='<%# GetSelectStatus(Item.ExternalReviewerID) 
                                        && !Master.App.ApplicationClosed ? Resources.Resource.ExternalReviewerSelectionM15: Resources.Resource.ExternalReviewerSelectionM14%>'                                    
                                    CommandName="Select" CommandArgument='<%# Item.ExternalReviewerID %>'
                                    Text='<%# GetSelectStatus(Item.ExternalReviewerID) ? "Select " : "Selected" %>'
                                    > </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDetails" runat="server" CausesValidation="false" CommandName="SelectDetail" ToolTip="Select Detail" CssClass="btn btn-default"
                                    CommandArgument='<%# Item.ExternalReviewerID %>'>
                        Details <span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Suggested by" SortExpression="Type">
                            <ItemTemplate>
                                <asp:Label ID="LabelSuggestedBy" runat="server" Text='<%# GetStatus() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ExternalReviewerID" HeaderText="ExternalReviewerID" SortExpression="ExternalReviewerID" Visible="false" />
                        <asp:TemplateField HeaderText="Name" SortExpression="NameString">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.NameString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Affiliation" >
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.MailingAddress%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Major Specialty" >
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.Major%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Specialty" >
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.Specialty%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#009966" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsFormExtRevSugg" runat="server" SelectMethod="GetForm_ExtRevByAppID" TypeName="ExtRevBAL">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:Button ID="btnShowInsertExtRev" runat="server" OnClick="btnShowInsertExtRev_Click" CssClass="btn btn-primary btn-md"
                    Visible='<%# !Master.App.ApplicationClosed %>'
                    Text="Add External Reviewer" CausesValidation="false" />
            </div>
            <div id="divViewDetail" runat="server" visible='<%#FormMode == "ExtRevDetail"%>'>
                <asp:DetailsView ID="dvExtReviewers" runat="server" Height="50px" Width="125px" CellPadding="4" class="table-condensed"
                    DataSourceID="odsFormExtRevSuggDet" EnableModelValidation="True" GridLines="None" AutoGenerateRows="False">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                    <EditRowStyle BackColor="#999999" />
                    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                    <Fields>
                        <asp:BoundField DataField="ExternalReviewerID" HeaderText="ExternalReviewerID" SortExpression="ExternalReviewerID" Visible="false" />
                        <asp:BoundField DataField="NameString" HeaderText="Name" SortExpression="NameString" />
                        <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank" />
                        <asp:BoundField DataField="Description" HeaderText="Position Description" SortExpression="Description" />
                        <asp:BoundField DataField="MailingAddress" HeaderText="MailingAddress" SortExpression="MailingAddress" />
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="Major" HeaderText="Major Specialty" SortExpression="Major" />
                        <asp:BoundField DataField="Specialty" HeaderText="Sub-Specialty (if applicable)" SortExpression="Specialty" />
                        <asp:BoundField DataField="PhoneAndFax" HeaderText="PhoneAndFax" SortExpression="PhoneAndFax" />
                        <asp:BoundField DataField="ActiveAreaOfResearch" HeaderText="ActiveAreaOfResearch" SortExpression="ActiveAreaOfResearch" />
                        <asp:BoundField DataField="PrevAreaOfResearch" HeaderText="PrevAreaOfResearch" SortExpression="PrevAreaOfResearch" />
                        <asp:BoundField DataField="Webpage" HeaderText="Webpage" SortExpression="Webpage" />
                        <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" />
                        <asp:BoundField DataField="TotalPublications" HeaderText="TotalPublications" SortExpression="TotalPublications" />
                        <asp:BoundField DataField="NoOfJournals" HeaderText="NoOfJournals" SortExpression="NoOfJournals" />
                        <asp:BoundField DataField="HIndex" HeaderText="HIndex" SortExpression="HIndex" />
                        <asp:BoundField DataField="Citations" HeaderText="Citations" SortExpression="Citations" />
                        <asp:BoundField DataField="IBAN" HeaderText="Bank Details" SortExpression="IBAN" />
                        
                        
                    </Fields>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#e7f7ff" Font-Bold="True" ForeColor="#3a9fe1" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                </asp:DetailsView>
                <asp:ObjectDataSource ID="odsFormExtRevSuggDet" runat="server" SelectMethod="GetExtRevByID" TypeName="ExtRevBAL" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter Name="ExternalReviewerID" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:Button ID="btnHideDetail" runat="server" Text="Return to List" OnClick="btnHideDetail_Click" CssClass="btn btn-primary btn-md" />
            </div>
            <div id="divInsertFormExtRev" runat="server" 
                style='overflow: auto; width: 1150px;'
                visible='<%#FormMode.Contains("Edit") || FormMode.Contains("Add")%>'>
                <div id="divInsertControls" style="float: left;">
                    <%--<asp:HiddenField ID="hfExtRevID" runat="server" />--%>
<%--                    <asp:HiddenField ID="hfSerial" runat="server" />
                    <asp:HiddenField ID="hfType" runat="server" />--%>
                    <table class="table">
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnShowDivExcel" 
                                    CssClass="form-control" 
                                    Text="Click here if you have an Excel file having External Reviewers details?"
                                    OnClick="btnShowDivExcel_Click" runat="server"></asp:Button>
                                <div id="divExcel" visible="false" runat="server" style="width:550px; background-color: yellowgreen; padding: 10px;">
                                    <%=Resources.Resource.ExternalReviewerSelectionF1 %>
                                    <br />
                                    <textarea id="taExtRev" style="width: 517px" runat="server"
                                        title='<%# Resources.Resource.ExternalReviewerSelectionF2%>' 
                                        placeholder='<%# Resources.Resource.ExternalReviewerSelectionF2%>'></textarea>
                                    <br />
                                    <asp:Button  ID="btnFillTB" OnClick="btnFillTB_Click" runat="server" Text="Fill" />
                                    <asp:Button  ID="btnHideDivExcel" OnClick="btnHideDivExcel_Click" Text="Hide" runat="server"></asp:Button>
                                    <asp:Button  ID="btnClear" OnClick="btnClear_Click" Text="Clear" runat="server"></asp:Button>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td>Name: </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="tbName" runat="server" CssClass="form-control"
                                        ValidationGroup="AddFormExternalReviewer"
                                        placeholder="Enter Name"                                        
                                        ToolTip="Enter Title, First Name, Middle Name and Last Name"></asp:TextBox>
                                    <%--<asp:Button ID="btnSearchName" runat="server" Text="..." OnClick="btnSearchName_Click" />--%>
                                </div>
                                <div><span style="font-size: xx-small">Enter Title, First Name, Middle Name and Last Name</span></div>
                            </td>
                            <td>

  <%--                              <ajaxtoolkit:TextBoxWatermarkExtender ID="tbName_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="tbName"
                                    WatermarkText="Enter Name" WatermarkCssClass="watermarked"></ajaxtoolkit:TextBoxWatermarkExtender>--%>

                            </td>
                        </tr>
                        <tr>
                            <td>Rank: </td>
                            <td>
                                   <asp:DropDownList runat="server" ID="ddlRank" class="form-control" 
                                    OnSelectedIndexChanged="ddlRank_SelectedIndexChanged" AutoPostBack="true"
                                     >
                                    <asp:ListItem Value="Professor" Selected="True">Professor</asp:ListItem>
                                    <asp:ListItem Value="Associate Professor">Associate Professor</asp:ListItem>
                                    <asp:ListItem Value="Assistant Professor">Assistant Professor</asp:ListItem>
                                    <asp:ListItem>Others</asp:ListItem>
                                 </asp:DropDownList>
                                 <asp:TextBox ID="tbOthers" runat="server" class="form-control" 
                                     Visible="false"></asp:TextBox>

                     
                            </td>
                            <td></td>
                        </tr>

                      <%--  <tr>
                            <td>Position Description: </td>
                            <td>
                                <asp:TextBox CssClass="form-control" ID="tbDescription" runat="server" 
                                    TextMode="multiLine" placeholder="Position Description" ToolTip="Position Description">
                                </asp:TextBox>
                                </td>
                            <td></td>
                        </tr>--%>
                        <tr>
                            <td>Mailing Address: </td>
                            <td>
                                <div>
                                    <asp:TextBox class="form-control" ID="tbMailingAddress" runat="server" 
                                        ValidationGroup="AddFormExternalReviewer" TextMode="multiLine"
                                        placeholder="Mailing Address"
                                        ToolTip="Make sure the University/College/School/Institute name is present in the Mailing Address."></asp:TextBox>
                                </div>
                                <div>
                                    <span style="font-size: xx-small">Make sure the University/College/School/Institute name is present in the Mailing Address.</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>Phone and Fax: </td>
                            <td>
                                <asp:TextBox class="form-control" ID="tbPhoneAndFax" runat="server" TextMode="multiLine" 
                                placeholder="Phone and Fax" ToolTip="Phone and Fax"></asp:TextBox>
                                
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Email Address: </td>
                            <td>
                                <div>
                                    <asp:TextBox class="form-control" ID="tbEmail" runat="server"
                                        placeholder="Email Address"
                                        ToolTip="Enter only one email address"
                                        ValidationGroup="AddFormExternalReviewer"></asp:TextBox>
                                </div>
                                <div><span style="font-size: xx-small">Enter only one email address</span></div>
                            </td>
                        </tr>
                        <tr>
                            <td>Major Specialty: </td>
                            <td>
                                <asp:TextBox class="form-control" ID="tbMajor" runat="server" 
                                    TextMode="multiLine" ValidationGroup="AddFormExternalReviewer" 
                                    placeholder="Major Specialty"
                                    ToolTip="Major Specialty"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Sub-Specialty  &nbsp; (if applicable): </td>
                            <td>
                                <asp:TextBox class="form-control" ID="tbSpecialty" runat="server" TextMode="multiLine"
                                     ValidationGroup="AddFormExternalReviewer" 
                                    placeholder="Sub-Specialty (if applicable)"
                                    ToolTip="Sub-Specialty (if applicable)"></asp:TextBox></td>
                           
                        </tr>
                        <tr>
                            <td>Active Area<br />
                                &nbsp;of Research: </td>
                            <td>
                                <asp:TextBox ID="tbActiveAreaOfResearch" class="form-control" runat="server" 
                                    TextMode="multiLine" ValidationGroup="AddFormExternalReviewer" 
                                    placeholder="Active Area of Research"
                                    ToolTip="Active Area of Research"></asp:TextBox></td>
                            
                        </tr>
                        <tr>
                            <td>Webpage: </td>
                            <td>

                                <asp:TextBox ID="tbWebpage" runat="server" class="form-control" TextMode="multiLine" 
                                    placeholder="Enter Webpage URL "
                                    TootlTip="Enter Webpage URL starting with http:// or https:// "></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Total Publications: </td>
                            <td>
                                <input type="number" class="form-control" runat="server" id="tbTotPublications" min="0"
                                    step="1" onkeypress="return event.charCode >= 48 && event.charCode <= 57"
                                    title="Numbers only" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>No of Journals: </td>
                            <td>
                                <input type="number" class="form-control" runat="server" id="tbNoOfJournals" min="0"
                                    step="1" onkeypress="return event.charCode >= 48 && event.charCode <= 57"
                                    title="Numbers only" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>H-Index: </td>
                            <td>
                                <input type="number" class="form-control" runat="server" id="tbHIndex" min="0" step="1"
                                    onkeypress="return event.charCode >= 48 && event.charCode <= 57"
                                    title="Numbers only" />

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Citations: </td>
                            <td>
                                <input type="number" class="form-control" runat="server" id="tbCitations" min="0" step="1" onkeypress="return event.charCode >= 48 && event.charCode <= 57"
                                    title="Numbers only" />
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    <asp:Button ID="btnAddExtRev" runat="server" ValidationGroup="AddFormExternalReviewer" CausesValidation="true" Text='<%# FormMode.Contains("Edit") ? "Update":"Add to List"%>' OnClick="btnAddExtRev_Click" CssClass="btn btn-primary btn-md" />
                    <asp:Button ID="btnReturn" runat="server" Text="Return to List" OnClick="btnReturn_Click" CssClass="btn btn-primary btn-md" />
                    <asp:Button ID="btnClearForm" runat="server" Text="Clear Form" OnClick="btnClearForm_Click" CssClass="btn btn-primary btn-md" />
                </div>
                <div id="divSearchExtRev" style="padding-top:100px; float: right;" runat="server">
                    <%--<asp:Button ID="btnShowSearch" Visible="false" runat="server" Text="Show Search" OnClick="btnShowSearch_Click" CssClass="btn btn-primary btn-md" />--%>
                    <div id="divExtRevSearch" runat="server">
                        <h3>Search</h3>
                        <asp:DropDownList ID="ddlListType" runat="server" AutoPostBack="True" 
                            Visible='<%# Type != (int)ExternalReviewerSelectionType.Applicant %>'
                            OnSelectedIndexChanged="ddlListType_SelectedIndexChanged">
                            <asp:ListItem Value="-1">Select a List</asp:ListItem>
                            <asp:ListItem Value="3">College List</asp:ListItem>
                            <asp:ListItem Value="1">Applicants List</asp:ListItem>
                        </asp:DropDownList><asp:TextBox ID="tbSearch" runat="server" OnTextChanged="tbSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <asp:Button ID="btnSearchExtRev" runat="server" Text="Search" OnClick="btnSearchExtRev_Click" CssClass="btn btn-primary btn-md" />

                        <br />
                        <asp:Label ID="lblSearchResultCount" runat="server"></asp:Label>
                        <asp:GridView ID="gvExtRevSearchResult" runat="server" ItemType="BL.Data.ExternalReviewer"
                            EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ExternalReviewerID"
                            OnRowCommand="gvExtRevSearchResult_RowCommand" CssClass="table-condensed" EmptyDataText="No search result found. ">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1e9966" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" 
                                            CssClass="btn btn-default" ToolTip="Select"                                            
                                            CommandName="Select"
                                            CommandArgument='<%# Item.ExternalReviewerID %>'>
                                            <span class="glyphicon glyphicon-pushpin" aria-hidden="true"></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex +1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                    <ItemTemplate>
                                        <div style='overflow: auto; height: 75px; width: 100px'>
                                            <strong>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Item.NameString %>'></asp:Label></strong>
                                            <br />

                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Item.Email%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rank">
                                    <ItemTemplate>
                                        <div style='overflow: auto; height: 75px; width: 100px'>
                                            <asp:Label ID="lblRank" runat="server" Text='<%# Item.Rank%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mailing Address" SortExpression="MailingAddress">
                                    <ItemTemplate>
                                        <div style='overflow: auto; height: 75px; width: 100px'>
                                            <asp:Label ID="lblMailingAddress" runat="server" Text='<%# Item.MailingAddress %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                        <%--<asp:Button ID="btnHideSearch" runat="server" Text="Hide Search" OnClick="btnHideSearch_Click" CssClass="btn btn-primary btn-md" />--%>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

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
</asp:Content>

