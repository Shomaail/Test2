<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true"
    CodeFile="PCFeedback.aspx.cs" Inherits="Forms_PCFeedback" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="Forms.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Instruction ID="Instruction1" runat="server" />
    <br />
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                    visible='<%#  !GetAppTaskFormCompleted() 
             && !Master.App.ApplicationClosed%>'>
                    <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                    <b>Incomplete: </b>
                    <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                        Font-Italic="True"></asp:Label>
                </div>
                <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                    visible='<%#  GetAppTaskFormCompleted() 
            && !Master.App.ApplicationClosed%>'>
                    <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                    <b>Complete:</b>
                    <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                        Font-Italic="True"></asp:Label>
                </div>
            <%--                class='<%# (
              Master.CurRoleID == (byte) RoleID.Promotion_Committee_Chairman
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Coordinator
            ||Master.CurRoleID == (byte) RoleID.Promotion_Committee_Member_1
            ||Master.CurRoleID == (byte) RoleID.Promotion_Committee_Member_2
            ||Master.CurRoleID == (byte) RoleID.Promotion_Committee_Member_3
            ||Master.CurRoleID == (byte) RoleID.Promotion_Committee_Member_4
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_1
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_2
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_3
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_4
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_5
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_6
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_7
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_8
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_9
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_10
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_11
            ||Master.CurRoleID == (byte) RoleID.Scientific_Council_Member_12
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority )
            && !Master.App.ApplicationClosed? "": "disabledbutton" %>'--%>
            <div id="divFeeback" runat="server">
                <table border="1" class="table-condensed">
                    <tr>
                        <td colspan="3">
                            <h3>Promotion Criteria</h3>
                        </td>
                        <td style="">
                            <h3>Concise Feedback</h3>
                            <%--                <i>Provided as main observations and reference points for future directions and improvements</i>--%>
                            <i><strong>The feedback form is basically extracted from the opinion and comments expressed by the promotion committee, ratings from external reviewers and assessment of the Scientific Council. It is only meant for applicant’s future direction and improvement and does not constitute the basis for acceptance/rejection of promotion application.</strong></i>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <i><b>
                                <asp:Label ID="LabelTP" runat="server" Text="Teaching Performance"></asp:Label>
                            </b></i>
                        </td>
                        <td style="width: 522px">
                            <asp:TextBox ID="TextBoxTP" runat="server" Height="90px" TextMode="MultiLine" onDrop="return false;"    
                                  onPaste="return limitCharsLength(this,2000);" oncut="return false" 
                                title="Enter text here. Use indirect form without addressing the candidate."
                                placeholder="Enter text here. Use indirect form without addressing the candidate."
                                Width="100%" AutoPostBack="true" OnTextChanged="TextBoxTP_TextChanged"></asp:TextBox>
                           <%-- <asp:TextBoxWatermarkExtender ID="TextBoxTP_TextBoxWatermarkExtender" 
                                runat="server" Enabled="True" TargetControlID="TextBoxTP" WatermarkText="Enter text here. Use indirect form without addressing the candidate." WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter your comments about Teaching Performance"
                                ControlToValidate="TextBoxTP"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please write one or two sentences"
                                OnServerValidate="ValidateTextLength" ClientValidationFunction="ValidateTextLength"
                                ControlToValidate="TextBoxTP"></asp:CustomValidator>--%> 
                            <asp:Label ID="LabelPrTP" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="8" style="font-weight: 700; color: #800000; font-style: italic; text-align: left; font-size: large;">
                            <img alt="" src="../images/PCFeedbackImg1.jpg" style="width: 28px; height: 577px" />
                        </td>
                        <td rowspan="5" style="color: #0000FF">
                            <img alt="" src="../images/PCFeedbackImg2.jpg" style="width: 19px; height: 135px" />
                        </td>
                        <td style="width: 162px">
                            <asp:Label ID="LabelProd" runat="server" Text="Productivity of work in terms of publication, frequency and quantity"></asp:Label>
                        </td>
                        <td style="width: 522px">
                            <asp:TextBox ID="TextBoxProd" runat="server" Height="90px" TextMode="MultiLine" onDrop="return false;"
                                onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitCharsLength(this,2000);"  onCut="return false"
                                title="Enter text here. Use indirect form without addressing the candidate."
                                placeholder="Enter text here. Use indirect form without addressing the candidate."
                                Width="100%" AutoPostBack="True" OnTextChanged="TextBoxProd_TextChanged"></asp:TextBox>
                            <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_TextBoxProd"
                                runat="server" Enabled="True" TargetControlID="TextBoxProd" WatermarkText="Enter text here. Use indirect form without addressing the candidate." WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter your comments about Productivity"
                                ControlToValidate="TextBoxProd"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator
                                ID="CustomValidator2" runat="server" ErrorMessage="Please write one or two sentences"
                                OnServerValidate="ValidateTextLength" ClientValidationFunction="ValidateTextLength"
                                ControlToValidate="TextBoxProd"></asp:CustomValidator>--%>
                            <asp:Label ID="LabelPrProd" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">
                            <asp:Label ID="LabelQualJ" runat="server" Text="Quality of the journals in which papers were published"></asp:Label>
                        </td>
                        <td style="width: 522px">
                            <asp:TextBox ID="TextBoxQualJ" runat="server" Height="90px" TextMode="MultiLine"
                                onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitCharsLength(this,2000);"  onCut="return false"
                                title="Enter text here. Use indirect form without addressing the candidate."
                                placeholder="Enter text here. Use indirect form without addressing the candidate."
                                Width="100%" AutoPostBack="True" OnTextChanged="TextBoxQualJ_TextChanged"></asp:TextBox>
                          <%--  <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_TextBoxQualJ"
                                runat="server" Enabled="True" TargetControlID="TextBoxQualJ" WatermarkText="Enter text here. Use indirect form without addressing the candidate." WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter your comments about Quality of the journals "
                                ControlToValidate="TextBoxQualJ"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator
                                ID="CustomValidator3" runat="server" ErrorMessage="Please write one or two sentences"
                                OnServerValidate="ValidateTextLength" ClientValidationFunction="ValidateTextLength"
                                ControlToValidate="TextBoxQualJ"></asp:CustomValidator>--%>
                            <asp:Label ID="LabelPrQualJ" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">
                            <asp:Label ID="LabelQualC" runat="server" Text="Quality of conferences in which papers were published "></asp:Label>
                        </td>
                        <td style="width: 522px">
                            <asp:TextBox ID="TextBoxQualC" runat="server" Height="90px" TextMode="MultiLine"
                                onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitCharsLength(this,2000);"  onCut="return false"
                                title="Enter text here. Use indirect form without addressing the candidate."
                                placeholder="Enter text here. Use indirect form without addressing the candidate."
                                Width="100%" AutoPostBack="True" OnTextChanged="TextBoxQualC_TextChanged"></asp:TextBox>
                           <%-- <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_TextBoxQualC"
                                runat="server" Enabled="True" TargetControlID="TextBoxQualC" WatermarkText="Enter text here. Use indirect form without addressing the candidate." WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter your comments about Quality of the Conferences "
                                ControlToValidate="TextBoxQualC"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator
                                ID="CustomValidator4" runat="server" ErrorMessage="Please write one or two sentences"
                                OnServerValidate="ValidateTextLength" ClientValidationFunction="ValidateTextLength"
                                ControlToValidate="TextBoxQualC"></asp:CustomValidator>--%>
                            <asp:Label ID="LabelPrQualC" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">
                            <asp:Label ID="LabelOrig" runat="server" Text="Originality of publications"></asp:Label>
                        </td>
                        <td style="width: 522px">
                            <asp:TextBox ID="TextBoxOrig" runat="server" Height="90px" TextMode="MultiLine" onDrop="return false;"
                                onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitCharsLength(this,2000);"  onCut="return false"
                                title="Enter text here. Use indirect form without addressing the candidate."
                                placeholder="Enter text here. Use indirect form without addressing the candidate."
                                Width="100%" AutoPostBack="True" OnTextChanged="TextBoxOrig_TextChanged"></asp:TextBox>
                          <%--  <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_TextBoxOrig"
                                runat="server" Enabled="True" TargetControlID="TextBoxOrig" WatermarkText="Enter text here. Use indirect form without addressing the candidate." WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter your comments about Originality of publications "
                                ControlToValidate="TextBoxOrig"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator
                                ID="CustomValidator5" runat="server" ErrorMessage="Please write one or two sentences"
                                OnServerValidate="ValidateTextLength" ClientValidationFunction="ValidateTextLength"
                                ControlToValidate="TextBoxOrig"></asp:CustomValidator>--%>
                            <asp:Label ID="LabelPrOrig" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 162px">
                            <asp:Label ID="LabelSigni" runat="server" Text="Significance of publications"></asp:Label>
                        </td>
                        <td style="width: 522px">
                            <asp:TextBox ID="TextBoxSigni" runat="server" Height="90px" TextMode="MultiLine"
                                onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitCharsLength(this,2000);"  onCut="return false"
                                title="Enter text here. Use indirect form without addressing the candidate."
                                placeholder="Enter text here. Use indirect form without addressing the candidate."
                                Width="100%" AutoPostBack="True" OnTextChanged="TextBoxSigni_TextChanged"></asp:TextBox>
                           <%-- <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_TextBoxSigni"
                                runat="server" Enabled="True" TargetControlID="TextBoxSigni" WatermarkText="Enter text here. Use indirect form without addressing the candidate." WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter your comments about Significance of publications"
                                ControlToValidate="TextBoxSigni"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator
                                ID="CustomValidator6" runat="server" ErrorMessage="Please write one or two sentences"
                                OnServerValidate="ValidateTextLength" ClientValidationFunction="ValidateTextLength"
                                ControlToValidate="TextBoxSigni"></asp:CustomValidator>--%>
                            <asp:Label ID="LabelPrSigni" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 162px">
                            <i><b>
                                <asp:Label ID="LabelIR" runat="server" Text="Independence in research "></asp:Label>
                            </b></i>
                        </td>
                        <td style="width: 522px">
                            <asp:TextBox ID="TextBoxIR" runat="server" Height="90px" TextMode="MultiLine" onDrop="return false;"
                                onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitCharsLength(this,2000);"  onCut="return false"
                                title="Enter text here. Use indirect form without addressing the candidate."
                                placeholder="Enter text here. Use indirect form without addressing the candidate."
                                Width="100%" AutoPostBack="True" OnTextChanged="TextBoxIR_TextChanged"></asp:TextBox>
                          <%--  <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_TextBoxIR"
                                runat="server" Enabled="True" TargetControlID="TextBoxIR" WatermarkText="Enter text here. Use indirect form without addressing the candidate." WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter your comments about Independence in research "
                                ControlToValidate="TextBoxIR"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator ID="CustomValidator7"
                                runat="server" ErrorMessage="Please write one or two sentences"
                                OnServerValidate="ValidateTextLength"
                                ClientValidationFunction="ValidateTextLength"
                                ControlToValidate="TextBoxIR"></asp:CustomValidator>--%>
                            <asp:Label ID="LabelPrIR" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 162px">
                            <i><b>
                                <asp:Label ID="LabelCR" runat="server" Text="Citations received by candidate's work "></asp:Label>
                            </b></i>
                        </td>
                        <td style="width: 522px">
                            <asp:TextBox ID="TextBoxCR" runat="server" Height="90px" TextMode="MultiLine" onDrop="return false;"
                                onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitCharsLength(this,2000);"  onCut="return false"
                                title="Enter text here. Use indirect form without addressing the candidate."
                                placeholder="Enter text here. Use indirect form without addressing the candidate."
                                Width="100%" AutoPostBack="True" OnTextChanged="TextBoxCR_TextChanged"></asp:TextBox>
                            <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_TextBoxCR"
                                runat="server" Enabled="True" TargetControlID="TextBoxCR" WatermarkText="Enter text here. Use indirect form without addressing the candidate." WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Enter your comments about Citations received "
                                ControlToValidate="TextBoxCR"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator ID="CustomValidator8"
                                runat="server" ErrorMessage="Please write one or two sentences" OnServerValidate="ValidateTextLength"
                                ClientValidationFunction="ValidateTextLength" ControlToValidate="TextBoxCR"></asp:CustomValidator>--%>
                            <asp:Label ID="LabelPrCR" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 162px">
                            <b><i>
                                <asp:Label ID="LabelSS" runat="server" Text="Candidate's Scholarly stature"></asp:Label>
                            </i></b>
                        </td>
                        <td style="width: 522px">
                            <asp:TextBox ID="TextBoxSS" runat="server" Height="90px" TextMode="MultiLine" onDrop="return false;"
                                onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitCharsLength(this,2000);"  onCut="return false"
                                title="Enter text here. Use indirect form without addressing the candidate."
                                placeholder="Enter text here. Use indirect form without addressing the candidate."
                                Width="100%" AutoPostBack="True" OnTextChanged="TextBoxSS_TextChanged"></asp:TextBox>
                          <%--  <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_TextBoxSS"
                                runat="server" Enabled="True" TargetControlID="TextBoxSS" WatermarkText="Enter text here. Use indirect form without addressing the candidate." WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter your comments about Scholarly stature"
                                ControlToValidate="TextBoxSS"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator ID="CustomValidator9"
                                runat="server" ErrorMessage="Please write one or two sentences" OnServerValidate="ValidateTextLength"
                                ClientValidationFunction="ValidateTextLength" ControlToValidate="TextBoxSS"></asp:CustomValidator>--%>
                            <asp:Label ID="LabelPrSS" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 192px">
                            <i><b>
                                <asp:Label ID="LabelCU_PS" runat="server" Text="Contribution to University and Public Services"></asp:Label>
                            </b></i>
                        </td>
                        <td style="width: 522px">
                            <asp:TextBox ID="TextBoxCU_PS" runat="server" Height="90px" TextMode="MultiLine"
                                onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitCharsLength(this,2000);"  onCut="return false"
                                title="Enter text here. Use indirect form without addressing the candidate."
                                placeholder="Enter text here. Use indirect form without addressing the candidate."
                                Width="100%" AutoPostBack="True" OnTextChanged="TextBoxCU_PS_TextChanged"></asp:TextBox>
                           <%-- <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_TextBoxCU_PS"
                                runat="server" Enabled="True" TargetControlID="TextBoxCU_PS" WatermarkText="Enter text here. Use indirect form without addressing the candidate." WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter your comments about Contribution to University and Public Services"
                                ControlToValidate="TextBoxCU_PS" Display="Dynamic"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator
                                ID="CustomValidator10" runat="server" ErrorMessage="Please write one or two sentences"
                                OnServerValidate="ValidateTextLength" ClientValidationFunction="ValidateTextLength"
                                ControlToValidate="TextBoxCU_PS"></asp:CustomValidator>--%>
                            <asp:Label ID="LabelPrCU_PS" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="LabelGenRmrk" runat="server" Text="General Remarks: "></asp:Label>
                            <asp:TextBox ID="TextBoxGenRmrk" runat="server" Height="90px" TextMode="MultiLine"
                                title="Enter text here. Use indirect form without addressing the candidate."
                                placeholder="Enter text here. Use indirect form without addressing the candidate."
                                onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);" onPaste="return limitCharsLength(this,2000);"  onCut="return false"
                                Width="100%" AutoPostBack="True"
                                OnTextChanged="TextBoxGenRmrk_TextChanged"></asp:TextBox>
                          <%--  <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_TextBoxGenRmrk"
                                runat="server" Enabled="True" TargetControlID="TextBoxGenRmrk" WatermarkText="Enter text here. Use indirect form without addressing the candidate." WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Enter some general remarks"
                                ControlToValidate="TextBoxGenRmrk"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator
                                ID="CustomValidator11" runat="server" ErrorMessage="Please write one or two sentences"
                                OnServerValidate="ValidateTextLength" ClientValidationFunction="ValidateTextLength"
                                ControlToValidate="TextBoxGenRmrk"></asp:CustomValidator>--%>
                            <asp:Label ID="LabelPrGenRmrk" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                    DisplayMode="BulletList" HeaderText="Form is not Saved!"
                    runat="server" />
                <br />
                <asp:Label ID="LabelValicationSummary" runat="server" ForeColor="#FF0000">
    All Fields are mandatory</asp:Label>
                <br />
                <br />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary btn-md" Visible="false" />
            </div>
            <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
            <asp:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
                TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
                BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
                RepositionMode="RepositionOnWindowScroll">
            </asp:ModalPopupExtender>
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
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
