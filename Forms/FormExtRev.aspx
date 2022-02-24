<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true"
    ValidateRequest="false"
    CodeFile="FormExtRev.aspx.cs" Inherits="Forms_FormExtRev" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%--<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            
            $('#tableApplicantList').DataTable({  
                 responsive: {
                    details: {
                     type: 'column'
                        }
            },
            columnDefs: [ {
                className: 'control',
                orderable: false,
                targets:   0
            } ],
            order: [ 0, 'asc' ],
                 dom: 'Bfrtip',
                 buttons: [
                    'excel'      ]
            });
            $('#tableDeptList').DataTable({    
                responsive: {
                    details: {
                     type: 'column'
                        }
            },
            columnDefs: [ {
                className: 'control',
                orderable: false,
                targets:   0
            } ],
            order: [ 0, 'asc' ],
                 dom: 'Bfrtip',
                 buttons: [
                    'excel'        ]
            });
                 $('#tableCollegeList').DataTable({                
                     responsive: {
                    details: {
                     type: 'column'
                        }
            },
            columnDefs: [ {
                className: 'control',
                orderable: false,
                targets:   0
            } ],
                 order: [ 0, 'asc' ],
                     dom: 'Bfrtip',
                 buttons: [
                     'excel'       ]
            });
        });

    </script>
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
    <%--    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFillTB" />
            <asp:PostBackTrigger ControlID="btnAddExtRev" />
            <asp:PostBackTrigger ControlID="btnShowDivExcel" />
            <asp:PostBackTrigger ControlID="ddlRank" />
        </Triggers>
        <ContentTemplate>
            <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                visible='<%# !GetAppTaskFormCompleted()
                          && 
                         (Master.CurRoleID == (byte) RoleID.Applicant ||
                          Master.CurRoleID == (byte) RoleID.DepartmentChairman ||
                          Master.CurRoleID == (byte) RoleID.College_Dean ||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Chairman||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_1||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_2||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_3||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_4
                          )
                           && !(FormMode.Contains("Edit") || FormMode.Contains("Add"))
                          && !Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                <b>Incomplete: </b>
                <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
            <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                visible='<%# GetAppTaskFormCompleted() 
                         && (
                          Master.CurRoleID == (byte) RoleID.Applicant ||
                          Master.CurRoleID == (byte) RoleID.DepartmentChairman ||
                          Master.CurRoleID == (byte) RoleID.College_Dean ||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Chairman||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_1||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_2||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_3||
                          Master.CurRoleID == (byte) RoleID.Departmental_Committee_Member_4
                          )
                          && !(FormMode.Contains("Edit") || FormMode.Contains("Add"))
                         &&!Master.App.ApplicationClosed%>'>
                <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                <b>Complete:</b>
                <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False" Font-Italic="True"></asp:Label>
            </div>
            <%--<asp:Label ID="lblMessage" runat="server" Text="" Visible='<%#FormMode == "View" || FormMode == "ViewApplicant" || FormMode == "ViewDepartment"|| FormMode == "ViewDepartmentCommittee" || FormMode == "ViewCollege" %>'></asp:Label>--%>
            <div id="divFormExtRevCandidate" runat="server" visible='<%#FormMode == "View" || FormMode == "ViewApplicant" || FormMode == "ViewDepartment"|| FormMode == "ViewDepartmentCommittee" || FormMode == "ViewCollege" %>' >
                <%--style='overflow: auto; width: 1500px;'--%>
                <h3>Applicant's List</h3>
                <div id="divTableOutputApplicantList" runat="server" visible='<%# Master.CurRoleID != (byte)RoleID.Applicant %>'>
                  <table id="tableApplicantList" >
                    <thead>
                        <tr style="color: White; background-color: #007D40; font-weight: bold;">
                            <th scope="col">S. No.</th>
                            <th scope="col">Name</th>
                            <th scope="col">Rank</th>
                            <th scope="col">Mailing Address</th>                 
                            <th scope="col">Email</th>
                            <th scope="col">Major</th>
                            <th scope="col">Specialty</th>
                            <th scope="col">Phone and Fax</th>
                            <th scope="col">Active Area Of Research</th>
                            <th scope="col">Webpage</th>                       
                            <th scope="col">Total Publications</th>
                            <th scope="col">No Of Journals</th>
                            <th scope="col">HIndex</th>
                            <th scope="col">Citations</th>
                        </tr>

                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ItemType="BL.Data.Form_ExtRev" ID="rptExternalReviewersApplicant">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex +1 %>   </td>
                                    <td><%# Item.ExternalReviewer.NameString %></td>
                                    <td><%# Item.ExternalReviewer.Rank %></td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.MailingAddress %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.Email %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.Major %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.Specialty %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.PhoneAndFax %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.ActiveAreaOfResearch %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; width:100px; height: 75px;'><%# Item.ExternalReviewer.Webpage %></div>
                                    </td>                                   
                                    <td><%# Item.ExternalReviewer.TotalPublications %></td>
                                    <td><%# Item.ExternalReviewer.NoOfJournals %></td>
                                    <td><%# Item.ExternalReviewer.HIndex %></td>
                                    <td><%# Item.ExternalReviewer.Citations %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                </div>
                <div id="divTableOutputApplicantList4Other" runat="server" visible='<%# Master.CurRoleID == (byte)RoleID.Applicant%>'>
                    <asp:GridView ID="gvExternalReviewersCand" runat="server" AutoGenerateColumns="False" ItemType="BL.Data.Form_ExtRev"
                    DataSourceID="odsFormExtRevCand" EnableModelValidation="True" CellPadding="4"
                    CssClass="table-condensed" DataKeyNames="ApplicationID,Type,Serial"
                    OnRowCommand="gvExternalReviewersCand_RowCommand" OnRowDataBound="gvExternalReviewersCand_RowDataBound"
                    ForeColor="#333333" GridLines="None" EmptyDataText="No External Reviewer is selected. "
                    OnRowEditing="gvExternalReviewersCand_RowEditing">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serial" SortExpression="Serial">
                            <ItemTemplate>
                                <div style="align-content: center; text-align: center;">
                                    <div>
                                        <asp:ImageButton ID="iBtnArrowUp" ImageUrl="~/images/Actions-arrow-up-icon.png" runat="server"
                                            ToolTip="Click to move the row up"
                                            CommandName="MoveRowUp" CommandArgument='<%# Item.Serial %>' />
                                    </div>
                                    <div>
                                        <asp:Label ID="lblSerial" runat="server" Text='<%# (Item.Serial+ 1).ToString() %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:ImageButton ID="iBtnArrowDn" ImageUrl="~/images/Actions-arrow-Down-icon.png" runat="server"
                                            ToolTip="Click to move the row down"
                                            CommandName="MoveRowDn" CommandArgument='<%# Item.Serial %>' />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit" CssClass="btn btn-default">
                         Edit <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> 
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>

                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" CssClass="btn btn-default"
                                    CommandArgument='<%# Item.ApplicationID + "," + Item.Type + "," + Item.Serial  %>'
                                    OnClientClick="return ShowConfirm(this.id);">
                        Delete <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.NameString %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.Rank %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mailing Address" SortExpression="MailingAddress">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblMailingAddress" runat="server" Text='<%# Item.ExternalReviewer.MailingAddress %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" SortExpression="Email">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Item.ExternalReviewer.Email %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Major" SortExpression="Major">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblMajor" runat="server" Text='<%# Item.ExternalReviewer.Major %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specialty" SortExpression="Specialty">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblSpecialty" runat="server" Text='<%# Item.ExternalReviewer.Specialty %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone & Fax" SortExpression="PhoneAndFax">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblPhoneAndFax" runat="server" Text='<%# Item.ExternalReviewer.PhoneAndFax %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active Area Of Research" SortExpression="ActiveAreaOfResearch">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblActiveAreaOfResearch" runat="server" Text='<%# Item.ExternalReviewer.ActiveAreaOfResearch %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Previous Area Of Research" SortExpression="PrevAreaOfResearch">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblPrevAreaOfResearch" runat="server" Text='<%# Bind("PrevAreaOfResearch") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Webpage" SortExpression="Webpage">
                            <ItemTemplate>
                                <div style='overflow: auto; width:100px; height: 75px;'>
                                    <asp:Label ID="lblWebpage" runat="server" Text='<%# Item.ExternalReviewer.Webpage %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>                      
                        <asp:TemplateField HeaderText="Total Publications">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.TotalPublications %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No Of Journals">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.NoOfJournals%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HIndex">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.HIndex %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Citations">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.Citations %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                </div>
                <asp:ObjectDataSource ID="odsFormExtRevCand" runat="server" OnDeleted="odsFormExtRevCand_Deleted" SelectMethod="GetFormExtRevByAppIDType" TypeName="ExtRevBAL" DeleteMethod="DeleteFormExtRev">
                    <DeleteParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
                        <asp:Parameter Name="Type" Type="Byte" />
                        <asp:Parameter Name="Serial" Type="Byte" />
                    </DeleteParameters>

                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
                        <asp:Parameter Name="Type" Type="Byte" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:Button ID="btnAddFormExtRevCand" runat="server" OnClick="btnAddFormExtRevCand_Click" CssClass="btn btn-primary btn-md"
                    Text="Add External Reviewer in Applicant List" CausesValidation="false" Visible='<%# ListIncomplete("CandidateList") && FormMode == "ViewApplicant" %>' />
            </div>
            <div id="divFormExtRevDepartment" runat="server" visible='<%#FormMode == "View" || FormMode == "ViewDepartment"|| FormMode == "ViewDepartmentCommittee" || FormMode == "ViewCollege"  %>'
                >
                <%--style='overflow: auto; width: 1500px;'--%>
                <h3>Department's List</h3>
                It is  desired that those external reviewers may not be chosen who have personal association or co-authorship with the applicant, which must be ascertained from the CV of the applicant.
                <div id="divTableOutputDeptList" runat="server" class="display nowrap" visible='<%# Master.CurRoleID != (byte)RoleID.Departmental_Committee_Chairman
                                                                                                 && Master.CurRoleID != (byte)RoleID.Departmental_Committee_Member_1
                                                                                                 && Master.CurRoleID != (byte)RoleID.Departmental_Committee_Member_2
                                                                                                 && Master.CurRoleID != (byte)RoleID.Departmental_Committee_Member_3
                                                                                                 && Master.CurRoleID != (byte)RoleID.Departmental_Committee_Member_4%>'>
                  <table id="tableDeptList">
                    <thead>
                        <tr style="color: White; background-color: #007D40; font-weight: bold;">
                            <th scope="col">S. No.</th>
                            <th scope="col">Name</th>
                            <th scope="col">Rank</th>
                            <th scope="col">Mailing Address</th>                 
                            <th scope="col">Email</th>
                            <th scope="col">Major</th>
                            <th scope="col">Specialty</th>
                            <th scope="col">Phone and Fax</th>
                            <th scope="col">Active Area Of Research</th>
                            <th scope="col">Webpage</th>                            
                            <th scope="col">Total Publications</th>
                            <th scope="col">No Of Journals</th>
                            <th scope="col">HIndex</th>
                            <th scope="col">Citations</th>
                        </tr>

                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ItemType="BL.Data.Form_ExtRev" ID="rptExternalReviewersDept">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex +1 %>   </td>
                                    <td><%# Item.ExternalReviewer.NameString %></td>
                                    <td><%# Item.ExternalReviewer.Rank %></td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.MailingAddress %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.Email %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.Major %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.Specialty %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.PhoneAndFax %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.ActiveAreaOfResearch %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto;width:100px; height: 75px;'><%# Item.ExternalReviewer.Webpage %></div>
                                    </td>                          
                                    <td><%# Item.ExternalReviewer.TotalPublications %></td>
                                    <td><%# Item.ExternalReviewer.NoOfJournals %></td>
                                    <td><%# Item.ExternalReviewer.HIndex %></td>
                                    <td><%# Item.ExternalReviewer.Citations %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                </div>
                <div id="divTableOutputDeptList4Other" runat="server" visible='<%# Master.CurRoleID == (byte)RoleID.Departmental_Committee_Chairman
                                                                                                 || Master.CurRoleID == (byte)RoleID.Departmental_Committee_Member_1
                                                                                                 || Master.CurRoleID == (byte)RoleID.Departmental_Committee_Member_2
                                                                                                 || Master.CurRoleID == (byte)RoleID.Departmental_Committee_Member_3
                                                                                                 || Master.CurRoleID == (byte)RoleID.Departmental_Committee_Member_4%>'>
                    <asp:GridView ID="gvExternalReviewersDept" runat="server" AutoGenerateColumns="False"
                    DataSourceID="odsFormExtRevDept" EnableModelValidation="True" CellPadding="4"
                    ItemType="BL.Data.Form_ExtRev"
                    CssClass="table-condensed" DataKeyNames="ApplicationID,Type,Serial" OnRowCommand="gvExternalReviewersDept_RowCommand" OnRowDataBound="gvExternalReviewersDept_RowDataBound"
                    ForeColor="#333333" GridLines="None" EmptyDataText="No External Reviewer is selected. " OnRowEditing="gvExternalReviewersDept_RowEditing">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serial" SortExpression="Serial">
                            <ItemTemplate>
                                <div style="align-content: center; text-align: center;">
                                    <div>
                                        <asp:ImageButton ID="iBtnArrowUp" ImageUrl="~/images/Actions-arrow-up-icon.png" runat="server"
                                            ToolTip="Click to move the row up"
                                            CommandName="MoveRowUp" CommandArgument='<%# Item.Serial %>' />
                                    </div>
                                    <div>
                                        <asp:Label ID="lblSerial" runat="server" Text='<%# (Item.Serial+ 1).ToString() %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:ImageButton ID="iBtnArrowDn" ImageUrl="~/images/Actions-arrow-Down-icon.png" runat="server"
                                            ToolTip="Click to move the row down"
                                            CommandName="MoveRowDn" CommandArgument='<%# Item.Serial %>' />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit" CssClass="btn btn-default">
                         Edit <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> 
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>

                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" CssClass="btn btn-default"
                                    CommandArgument='<%# Item.ApplicationID + "," + Item.Type + "," + Item.Serial  %>'
                                    OnClientClick="return ShowConfirm(this.id);">
                        Delete <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.NameString %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.Rank %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mailing Address" SortExpression="MailingAddress">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblMailingAddress" runat="server" Text='<%# Item.ExternalReviewer.MailingAddress %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" SortExpression="Email">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Item.ExternalReviewer.Email %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Major" SortExpression="Major">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblMajor" runat="server" Text='<%# Item.ExternalReviewer.Major %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specialty" SortExpression="Specialty">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblSpecialty" runat="server" Text='<%# Item.ExternalReviewer.Specialty %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone & Fax" SortExpression="PhoneAndFax">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblPhoneAndFax" runat="server" Text='<%# Item.ExternalReviewer.PhoneAndFax %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active Area Of Research" SortExpression="ActiveAreaOfResearch">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblActiveAreaOfResearch" runat="server" Text='<%# Item.ExternalReviewer.ActiveAreaOfResearch %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Previous Area Of Research" SortExpression="PrevAreaOfResearch">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblPrevAreaOfResearch" runat="server" Text='<%# Bind("PrevAreaOfResearch") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Webpage" SortExpression="Webpage">
                            <ItemTemplate>
                                <div style='overflow: auto; width:100px; height: 75px;'>
                                    <asp:Label ID="lblWebpage" runat="server" Text='<%# Item.ExternalReviewer.Webpage %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>                     
                        <asp:TemplateField HeaderText="Total Publications">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.TotalPublications %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No Of Journals">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.NoOfJournals%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HIndex">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.HIndex %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Citations">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.Citations %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                </div>
                <asp:ObjectDataSource ID="odsFormExtRevDept" runat="server" OnDeleted="odsFormExtRevDept_Deleted" SelectMethod="GetFormExtRevByAppIDType" TypeName="ExtRevBAL" DeleteMethod="DeleteFormExtRev">
                    <DeleteParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
                        <asp:Parameter Name="Type" Type="Byte" />
                        <asp:Parameter Name="Serial" Type="Byte" />
                    </DeleteParameters>

                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
                        <asp:Parameter Name="Type" Type="Byte" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:Button ID="btnAddFormExtRevDept" runat="server" OnClick="btnAddFormExtRevDept_Click" CssClass="btn btn-primary btn-md"
                    Text="Add External Reviewer in Department List" CausesValidation="false" Visible='<%# ListIncomplete("DepartmentList") && FormMode == "ViewDepartmentCommittee" %>' />
                <small>
                    <a href="#" data-toggle="modal" data-target="#MemoModal">
                        <label class="alert-danger">Important Instructions <span class="glyphicon glyphicon-alert" aria-hidden="true"></span></label>
                    </a>
                </small>
                <div class="modal fade" id="MemoModal" tabindex="-1" role="dialog" aria-labelledby="resModalLabel" style="display: none;">
                    <div class="modal-dialog" style="width: 700px" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span>
                                </button>
                                <h4 class="modal-title">Memo</h4>
                            </div>
                            <div class="modal-body">
                                <img src="../images/Memo4SelectionofExtRev.JPG" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divFormExtRevCollege" runat="server" visible='<%#FormMode == "View" || FormMode == "ViewCollege"%>' >
                <%--style='overflow: auto; width: 1500px;'--%>
                <h3>College's List</h3>
                The list of college should be different from the department as is mentioned in Guidelines Section 5.2.4- page No. 24
                Moreover, it is  desired that those external reviewers may not be chosen who have personal association or co-authorship with the applicant, which must be ascertained from the CV of the applicant.
                <div id="divTableOutputCollegeList" runat="server" class="display nowrap" visible='<%# Master.CurRoleID != (byte)RoleID.College_Dean %>'>
                  <table id="tableCollegeList">
                    <thead>
                        <tr style="color: White; background-color: #007D40; font-weight: bold;">
                            <th scope="col">S. No.</th>
                            <th scope="col">Name</th>
                            <th scope="col">Rank</th>
                            <th scope="col">Mailing Address</th>                 
                            <th scope="col">Email</th>
                            <th scope="col">Major</th>
                            <th scope="col">Specialty</th>
                            <th scope="col">Phone and Fax</th>
                            <th scope="col">Active Area Of Research</th>
                            <th scope="col">Webpage</th>
                            <th scope="col">Total Publications</th>
                            <th scope="col">No Of Journals</th>
                            <th scope="col">HIndex</th>
                            <th scope="col">Citations</th>
                        </tr>

                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ItemType="BL.Data.Form_ExtRev" ID="rptExternalReviewersCollege">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.ItemIndex +1 %>   </td>
                                    <td><%# Item.ExternalReviewer.NameString %></td>
                                    <td><%# Item.ExternalReviewer.Rank %></td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.MailingAddress %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.Email %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.Major %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.Specialty %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.PhoneAndFax %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; height: 75px;'><%# Item.ExternalReviewer.ActiveAreaOfResearch %></div>
                                    </td>
                                    <td>
                                        <div style='overflow: auto; width:100px; height: 75px;'><%# Item.ExternalReviewer.Webpage %></div>
                                    </td>
                                   
                                    <td><%# Item.ExternalReviewer.TotalPublications %></td>
                                    <td><%# Item.ExternalReviewer.NoOfJournals %></td>
                                    <td><%# Item.ExternalReviewer.HIndex %></td>
                                    <td><%# Item.ExternalReviewer.Citations %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                </div>
                <div id="divTableOutputCollegeList4Other" runat="server" visible='<%# Master.CurRoleID == (byte)RoleID.College_Dean%>'>
                <asp:GridView ID="gvExternalReviewersColl" runat="server" AutoGenerateColumns="False"
                    DataSourceID="odsFormExtRevColl" EnableModelValidation="True" CellPadding="4"
                    ItemType="BL.Data.Form_ExtRev"
                    CssClass="table-condensed" DataKeyNames="ApplicationID,Type,Serial" OnRowCommand="gvExternalReviewersColl_RowCommand" OnRowDataBound="gvExternalReviewersColl_RowDataBound"
                    ForeColor="#333333" GridLines="None" EmptyDataText="No External Reviewer is selected. " OnRowEditing="gvExternalReviewersColl_RowEditing">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serial" SortExpression="Serial">
                            <ItemTemplate>
                                <div style="align-content: center; text-align: center;">
                                    <div>
                                        <asp:ImageButton ID="iBtnArrowUp" ImageUrl="~/images/Actions-arrow-up-icon.png" runat="server"
                                            ToolTip="Click to move the row up"
                                            CommandName="MoveRowUp" CommandArgument='<%# Item.Serial %>' />
                                    </div>
                                    <div>
                                        <asp:Label ID="lblSerial" runat="server" Text='<%# (Item.Serial+ 1).ToString() %>'></asp:Label>
                                    </div>
                                    <div>
                                        <asp:ImageButton ID="iBtnArrowDn" ImageUrl="~/images/Actions-arrow-Down-icon.png" runat="server"
                                            ToolTip="Click to move the row down"
                                            CommandName="MoveRowDn" CommandArgument='<%# Item.Serial %>' />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ToolTip="Edit" CssClass="btn btn-default">
                         Edit <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> 
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>

                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" CssClass="btn btn-default"
                                    CommandArgument='<%# Item.ApplicationID + "," + Item.Type + "," + Item.Serial  %>'
                                    OnClientClick="return ShowConfirm(this.id);">
                        Delete <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.NameString %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.Rank %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mailing Address" SortExpression="MailingAddress">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblMailingAddress" runat="server" Text='<%# Item.ExternalReviewer.MailingAddress %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" SortExpression="Email">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Item.ExternalReviewer.Email %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Major" SortExpression="Major">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblMajor" runat="server" Text='<%# Item.ExternalReviewer.Major %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Specialty" SortExpression="Specialty">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblSpecialty" runat="server" Text='<%# Item.ExternalReviewer.Specialty %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone & Fax" SortExpression="PhoneAndFax">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblPhoneAndFax" runat="server" Text='<%# Item.ExternalReviewer.PhoneAndFax %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active Area Of Research" SortExpression="ActiveAreaOfResearch">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblActiveAreaOfResearch" runat="server" Text='<%# Item.ExternalReviewer.ActiveAreaOfResearch %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Previous Area Of Research" SortExpression="PrevAreaOfResearch">
                            <ItemTemplate>
                                <div style='overflow: auto; height: 75px;'>
                                    <asp:Label ID="lblPrevAreaOfResearch" runat="server" Text='<%# Bind("PrevAreaOfResearch") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Webpage" SortExpression="Webpage">
                            <ItemTemplate>
                                <div style='overflow: auto;width:100px; height: 75px;'>
                                    <asp:Label ID="lblWebpage" runat="server" Text='<%# Item.ExternalReviewer.Webpage %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>                      
                        <asp:TemplateField HeaderText="Total Publications">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.TotalPublications %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No Of Journals">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.NoOfJournals%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HIndex">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.HIndex %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Citations">
                            <ItemTemplate>
                                <%# Item.ExternalReviewer.Citations %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                    </div>
                <asp:ObjectDataSource ID="odsFormExtRevColl" runat="server" OnDeleted="odsFormExtRevColl_Deleted" SelectMethod="GetFormExtRevByAppIDType" TypeName="ExtRevBAL" DeleteMethod="DeleteFormExtRev">
                    <DeleteParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
                        <asp:Parameter Name="Type" Type="Byte" />
                        <asp:Parameter Name="Serial" Type="Byte" />
                    </DeleteParameters>

                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="-1" Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
                        <asp:Parameter Name="Type" Type="Byte" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:Button ID="btnAddFormExtRevColl" runat="server" OnClick="btnAddFormExtRevColl_Click" CssClass="btn btn-primary btn-md"
                    Visible='<%# ListIncomplete("CollegeList") && FormMode == "ViewCollege"%>'
                    Text="Add External Reviewer in College List" />
                <small>
                    <a href="#" data-toggle="modal" data-target="#MemoModal">
                        <label class="alert-danger">Important Instructions <span class="glyphicon glyphicon-alert" aria-hidden="true"></span></label>
                    </a>
                </small>
            </div>
            <div id="divInsertFormExtRev" runat="server" visible='<%#FormMode == "Edit" || FormMode == "Add" %>'>
                <div id="divInsertControls" style="float: left;">
                    <asp:HiddenField ID="hfExtRevID" runat="server" />
                    <asp:HiddenField ID="hfSerial" runat="server" />
                    <asp:HiddenField ID="hfType" runat="server" />
                    <table class="table">
                        <tr>
                            <td colspan="3">
                                <%-- <button id="btnShowDivExcel" type="button" 
                                    title="Do You have an Excel file having External Reviewers detail" onclick="showDiv()" >Do You have an Excel file having External Reviewers detail?</button>
                                <div  id="divExcel" style="width:537px; display:none; background-color:yellowgreen; padding:10px;">
                                    In the textbox below paste one row of external reviewer data from Excel with same number as in the list of texboxes below then click on the Fill Button to place the data in the Textboxes
                                    <br />
                                    <textarea id="taExtRev" style="width: 517px" runat="server"
                                        title="Enter Excel Row here"
                                        placeholder="Enter Excel Row here"></textarea>
                                    <br />
                                    <button id="btnFillTB2" type="button" title="Fill" onclick="fillTBs()">Fill </button>
                                    <asp:Button ID="btnFillTB" OnClick="btnFillTB_Click" runat="server" Text="Fill" Visible="false" />
                                    <button id="btnHideDivExcel" type="button" title="Hide"  onclick="hideDiv()" >Hide</button>
                                </div>--%>
                                <asp:Button ID="btnShowDivExcel" Text="Click here if you have an Excel file having External Reviewers detail?"
                                    OnClick="btnShowDivExcel_Click" runat="server"></asp:Button>
                                <div id="divExcel" visible="false" runat="server" style="width: 537px; background-color: yellowgreen; padding: 10px;">
                                    In the textbox below paste one row of external reviewer data from Excel with same number as in the list of texboxes below then click on the Fill Button to place the data in the Textboxes
                                    <br />
                                    <textarea id="taExtRev" style="width: 517px" runat="server"
                                        title="Enter Excel Row here"
                                        placeholder="Enter Excel Row here"></textarea>
                                    <br />
                                    <asp:Button ID="btnFillTB" OnClick="btnFillTB_Click" runat="server" Text="Fill" />
                                    <asp:Button ID="btnHideDivExcel" OnClick="btnHideDivExcel_Click" Text="Hide" runat="server"></asp:Button>
                                </div>
                            </td>

                        </tr>
                        <tr>
                            <td>Name: </td>
                            <td>
                                <div>
                                    <%-- <asp:TextBox ID="tbName" runat="server" ValidationGroup="AddFormExternalReviewer"
                                        ToolTip="Enter only NAMES without salutation like Dr. or Mr. etc"></asp:TextBox>--%>
                                    <asp:TextBox ID="tbName" runat="server" class="form-control"
                                        ValidationGroup="AddFormExternalReviewer"
                                        placeholder="Enter Name"
                                        ToolTip="Enter only NAMES without salutation like Dr. or Mr. etc"></asp:TextBox>
                                </div>
                                <div><span style="font-size: xx-small">Enter only NAMES without salutation like Dr. or Mr. etc</span></div>
                            </td>
                            <td>

                                <%--<ajaxtoolkit:TextBoxWatermarkExtender ID="tbName_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="tbName"
                                    WatermarkText="Enter Name" WatermarkCssClass="watermarked">
                                </ajaxtoolkit:TextBoxWatermarkExtender>--%>

                            </td>
                        </tr>
                        <tr>
                            <td>Rank: </td>
                            <td>
                                <%--<asp:DropDownList ID="ddlRank" runat="server" ToolTip="Rank">
                                    <asp:ListItem Selected="True">Professor</asp:ListItem>
                                    <asp:ListItem>Associate Professor</asp:ListItem>
                                    <asp:ListItem>Senior Lecturer</asp:ListItem>
                                    <asp:ListItem>Reader</asp:ListItem>
                                    <asp:ListItem>Others..</asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:DropDownList runat="server" ID="ddlRank" class="form-control"
                                    OnSelectedIndexChanged="ddlRank_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="Professor" Selected="True">Professor</asp:ListItem>
                                    <asp:ListItem Value="Associate Professor">Associate Professor</asp:ListItem>
                                    <asp:ListItem Value="Assistant Professor">Assistant Professor</asp:ListItem>
                                    <asp:ListItem Value="Senior Lecturer">Senior Lecturer</asp:ListItem>
                                    <asp:ListItem Value="Reader">Reader</asp:ListItem>
                                    <asp:ListItem>Others</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="tbOthers" runat="server" class="form-control"
                                    Visible="false"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <%--      <tr>
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
                                        ToolTip="Make sure the University name is present in the Mailing Address"></asp:TextBox>
                                </div>
                                <div>
                                    <span style="font-size: xx-small">Make sure the University name is present in the Mailing Address.</span>
                                </div>
                            </td>
                            <%--           <td>
                                <div>
                                    <asp:TextBox ID="tbMailingAddress" runat="server" ValidationGroup="AddFormExternalReviewer" TextMode="multiLine"
                                        ToolTip="Make sure the University name is present in the Mailing Address"></asp:TextBox>
                                </div>
                                <div>
                                    <span style="font-size: xx-small">Make sure the University name is present</span>
                                </div>
                            </td>

                            <td>

                                <ajaxtoolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_tbMailingAddress" runat="server" Enabled="True" TargetControlID="tbMailingAddress"
                                    WatermarkText="Mailing Address" WatermarkCssClass="watermarked">
                                </ajaxtoolkit:TextBoxWatermarkExtender>
                            </td>--%>
                        </tr>
                        <tr>
                            <td>Phone and Fax: </td>
                            <td>
                                <asp:TextBox class="form-control" ID="tbPhoneAndFax" runat="server" TextMode="multiLine"
                                    placeholder="Phone and Fax" ToolTip="Phone and Fax"></asp:TextBox>
                                <%-- <asp:TextBox ID="tbPhoneAndFax" runat="server" TextMode="multiLine" Text="Phone and Fax"></asp:TextBox>
                                <ajaxtoolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_tbPhoneAndFax" runat="server" Enabled="True" TargetControlID="tbPhoneAndFax"
                                    WatermarkText="Phone and Fax" WatermarkCssClass="watermarked">
                                </ajaxtoolkit:TextBoxWatermarkExtender>--%>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Email Address: </td>
                            <td>
                                <div>
                                    <asp:TextBox class="form-control" ID="tbEmail" runat="server"
                                        placeholder="Email Address" TextMode="Email"
                                        ToolTip="Enter only one email address"
                                        ValidationGroup="AddFormExternalReviewer"></asp:TextBox>
                                    <%-- <asp:TextBox ID="tbEmail" runat="server" TextMode="multiLine" ToolTip="Enter only one email address"
                                        ValidationGroup="AddFormExternalReviewer"></asp:TextBox>--%>
                                </div>
                                <div><span style="font-size: xx-small">Enter only one email address</span></div>

                            </td>
                            <%--        <td>



                                <ajaxtoolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_tbEmail" runat="server" Enabled="True" TargetControlID="tbEmail"
                                    WatermarkText="Email" WatermarkCssClass="watermarked">
                                </ajaxtoolkit:TextBoxWatermarkExtender>
                            </td>--%>
                        </tr>
                        <tr>
                            <td>Major: </td>
                            <td>
                                <asp:TextBox class="form-control" ID="tbMajor" runat="server"
                                    TextMode="multiLine" ValidationGroup="AddFormExternalReviewer"
                                    placeholder="Major"
                                    ToolTip="Major"></asp:TextBox></td>
                            <%--<td> <asp:TextBox ID="tbMajor" runat="server" TextMode="multiLine" ValidationGroup="AddFormExternalReviewer" ToolTip="Major"></asp:TextBox></td>
                            <td>

                                <ajaxtoolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_tbMajor" runat="server" Enabled="True" TargetControlID="tbMajor"
                                    WatermarkText="Major" WatermarkCssClass="watermarked">
                                </ajaxtoolkit:TextBoxWatermarkExtender>
                            </td>--%>
                        </tr>
                        <tr>
                            <td>Specialty: </td>
                            <td>
                                <asp:TextBox class="form-control" ID="tbSpecialty" runat="server" TextMode="multiLine"
                                    ValidationGroup="AddFormExternalReviewer"
                                    placeholder="Specialty"
                                    ToolTip="Specialty"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Active Area<br />
                                &nbsp;of Research: </td>
                            <td>
                                <asp:TextBox ID="tbActiveAreaOfResearch" class="form-control" runat="server"
                                    TextMode="multiLine" ValidationGroup="AddFormExternalReviewer"
                                    placeholder="Active Area of Research"
                                    ToolTip="Active Area of Research"></asp:TextBox></td>
                            <%-- <td>
                                <asp:TextBox ID="tbActiveAreaOfResearch" runat="server" TextMode="multiLine" ValidationGroup="AddFormExternalReviewer" ToolTip="Active Area of Research"></asp:TextBox></td>
                            <td>
                                <ajaxtoolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_tbActiveAreaOfResearch" runat="server" Enabled="True" TargetControlID="tbActiveAreaOfResearch"
                                    WatermarkText="Active Area Of Research" WatermarkCssClass="watermarked">
                                </ajaxtoolkit:TextBoxWatermarkExtender>
                            </td>--%>
                        </tr>
                        <tr>
                            <td>Webpage: </td>
                            <td>
                                <asp:TextBox ID="tbWebpage" runat="server" class="form-control" TextMode="multiLine"
                                    placeholder="Enter Webpage URL "
                                    TootlTip="Enter Webpage URL  "></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"><strong>The Following Information should be considered from Scopus only</strong> </td>
                        </tr>
                        <tr>
                            <td>Total Publications: </td>
                            <td>
                                <%--<ew:NumericBox runat="server" MaxLength="9" PositiveNumber="true" RealNumber="false" ID="tbTotPublications" ToolTip="Total Publications" BorderWidth="1" Width="128px"  > 0</ew:NumericBox>--%>
                                <input type="number" class="form-control" runat="server" id="tbTotPublications" min="0" max="2147483647"
                                    step="1" onkeypress="return event.charCode >= 48 && event.charCode <= 57"
                                    title="Numbers only" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>No of Journals: </td>
                            <td>
                                <%--                                <ew:NumericBox runat="server" MaxLength="9" PositiveNumber="true" RealNumber="false" ID="tbNoOfJournals" ToolTip="No of Journals" BorderWidth="1" Width="128px">0</ew:NumericBox>--%>
                                <input type="number" class="form-control" runat="server" id="tbNoOfJournals" min="0" max="2147483647"
                                    step="1" onkeypress="return event.charCode >= 48 && event.charCode <= 57"
                                    title="Numbers only" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>H-Index: </td>
                            <td>
                                <%--<ew:NumericBox runat="server" MaxLength="9" PositiveNumber="true" RealNumber="false" ID="tbHIndex" ToolTip="H-Index" BorderWidth="1" Width="128px">0</ew:NumericBox>--%>
                                <input type="number" class="form-control" runat="server" id="tbHIndex" min="0" max="2147483647" step="1"
                                    onkeypress="return event.charCode >= 48 && event.charCode <= 57"
                                    title="Numbers only" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Citations: </td>
                            <td>
                                <%--<ew:NumericBox runat="server" MaxLength="9" PositiveNumber="true" RealNumber="false" ID="tbCitations" ToolTip="Citations" BorderWidth="1" Width="128px">0</ew:NumericBox>--%>
                                <input type="number" class="form-control" runat="server" id="tbCitations" min="0" step="1" max="2147483647" onkeypress="return event.charCode >= 48 && event.charCode <= 57"
                                    title="Numbers only" />
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    <asp:Button ID="btnAddExtRev" runat="server" ValidationGroup="AddFormExternalReviewer" CausesValidation="true" Text='<%# FormMode == "Edit" ? "Update":"Add to List"%>' OnClick="btnAddExtRev_Click" CssClass="btn btn-primary btn-md" />
                    <asp:Button ID="btnReturn" runat="server" Text="Return to List" OnClick="btnReturn_Click" CssClass="btn btn-primary btn-md" />
                    <asp:Button ID="btnClearForm" runat="server" Text="Clear Form" OnClick="btnClearForm_Click" CssClass="btn btn-primary btn-md" />
                </div>
                <div style="float: right">
                    <%--<asp:Button ID="btnShowSearch" Visible="false" runat="server" Text="Show Search" OnClick="btnShowSearch_Click" CssClass="btn btn-primary btn-md" />--%>
                    <div id="divExtRevSearch" runat="server">
                        <h3>Search</h3>
                        <asp:DropDownList ID="ddlListType" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlListType_SelectedIndexChanged">
                            <asp:ListItem Value="-1">Select a List</asp:ListItem>
                            <asp:ListItem Value="2">Department List</asp:ListItem>
                            <asp:ListItem Value="3">College List</asp:ListItem>
                            <asp:ListItem Value="1">Applicants List</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="tbSearch" runat="server" OnTextChanged="tbSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <asp:Button ID="btnSearchExtRev" runat="server" Text="Search" OnClick="btnSearchExtRev_Click" CssClass="btn btn-primary btn-md" />

                        <br />
                        <asp:Label ID="lblSearchResultCount" runat="server"></asp:Label>
                        <asp:GridView ID="gvExtRevSearchResult" runat="server"
                            EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" ItemType="BL.Data.ExternalReviewer"
                            OnRowCommand="gvExtRevSearchResult_RowCommand" CssClass="table-condensed" EmptyDataText="No search result found. ">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <Columns>
                                <%-- <asp:CommandField ShowSelectButton="True" />--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button runat="server" CommandArgument='<%# Item.ExternalReviewerID %>' CommandName="Select" Text="Select" />
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
                                            <asp:Label ID="lblName" runat="server" Text='<%# Item.Name %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" SortExpression="Email">
                                    <ItemTemplate>
                                        <div style='overflow: auto; height: 75px; width: 200px'>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Item.Email%>'></asp:Label>
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
</asp:Content>

