<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReviewersForm.aspx.cs" Inherits="Forms_ReviewersForm"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div style="text-align: center">
            <h3>Research Performance Evaluation Form For Promotion to the Rank of
                <asp:Label ID="lblRank" runat="server" meta:resourcekey="lblRankResource1"></asp:Label>
            </h3>
            <h4>Name of Applicant:
             <asp:Label ID="LabelApplicant" runat="server"></asp:Label>
            </h4>
        </div>
        <br />
        <table style="font-size: 12pt">
            <tr>
                <td style="width: 658px;">
                    <br />
                    <asp:Table ID="Table1" runat="server" Width="99%" Height="89px" meta:resourcekey="Table1Resource1">
                        <asp:TableRow ID="TableRow1" runat="server" meta:resourcekey="TableRowResource1">
                            <asp:TableCell ID="TableCell1" runat="server" VerticalAlign="Top" meta:resourcekey="TableCellResource1"><label>Name of Reviewer: </label></asp:TableCell>
                            <asp:TableCell ID="TableCell2" runat="server" VerticalAlign="Top" meta:resourcekey="TableCellResource2">
                                <asp:Label ID="lblName" runat="server" Font-Bold="True" meta:resourcekey="lblNameResource1"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow2" runat="server" meta:resourcekey="TableRowResource2">
                            <asp:TableCell ID="TableCell3" runat="server" Width="15%" VerticalAlign="Top" meta:resourcekey="TableCellResource3"><label>
                   University/Institute Name and Address:</label></asp:TableCell>
                            <asp:TableCell ID="TableCell4" runat="server" VerticalAlign="Top" meta:resourcekey="TableCellResource4">
                                <asp:Label ID="lblAddress" runat="server" Font-Bold="True" meta:resourcekey="lblAddressResource1"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <br />
                </td>
            </tr>
        </table>

        <table style="font-size: 12pt">
            <tr>
                <td style="width: 658px">
                    <span style="color: #000000; font-family: Georgia">Please indicate how familiar you are with applicant's area of specialization:<br />
                    </span>
                    <br />
                    <asp:RadioButtonList ID="rblFamiliarity" runat="server" meta:resourcekey="rblFamiliarityResource1">
                        <asp:ListItem Value="1" Text="I am actively engaged in research in the applicant's specific area." Selected="True" meta:resourcekey="ListItemResource1" />
                        <asp:ListItem Value="2" Text="I have carried out research in the past in the applicant's specific area." meta:resourcekey="ListItemResource2" />
                        <asp:ListItem Value="3" Text="My experience is in the general area but I have not worked in the applicant's specific area." meta:resourcekey="ListItemResource3" />
                        <asp:ListItem Value="4" Text="Others:" meta:resourcekey="ListItemResource4" />
                    </asp:RadioButtonList>
                    <asp:TextBox ID="TextBoxOthers" runat="server" Height="85px" Width="423px" TextMode="MultiLine" meta:resourcekey="TextBoxOthersResource1"></asp:TextBox>
                    <asp:Label ID="LabelOthers" runat="server" Visible="False" BorderStyle="Solid"
                        BorderWidth="1px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 658px">
                    <span style="font-size: 10pt">
                        <br />
                        <span style="font-size: 12pt">Questions 1 through 8 deal with the Candidate's attributes concerning his research achievements through his publications. For each of these attributes, please provide rating and justification of that rating.</span></span>
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 658px">
                    <span>
                        <br />
                        1. How would you rate the <strong>productivity</strong> of the Candidate's work in terms of publication frequency and quantity?</span><br />
                    <br />
                    <asp:RadioButtonList ID="rblQ1" runat="server" meta:resourcekey="rblQ1Resource1">
                        <asp:ListItem Value="1" Text="Excellent" Selected="True" meta:resourcekey="ListItemResource5" />
                        <asp:ListItem Value="2" Text="Very Good" meta:resourcekey="ListItemResource6" />
                        <asp:ListItem Value="3" Text="Good" meta:resourcekey="ListItemResource7" />
                        <asp:ListItem Value="4" Text="Fair" meta:resourcekey="ListItemResource8" />
                        <asp:ListItem Value="5" Text="Poor" meta:resourcekey="ListItemResource9" />
                    </asp:RadioButtonList><br />
                    <span>Justification</span>
                    <asp:RequiredFieldValidator ID="rfvJust1" runat="server" ControlToValidate="TextBoxJust1" ErrorMessage="Justification of Question #1 is required" Text="*" meta:resourcekey="rfvJust1Resource1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="TextBoxJust1" runat="server" Height="85px" Width="548px" TextMode="MultiLine" meta:resourcekey="TextBoxJust1Resource1"></asp:TextBox>
                    <asp:Label ID="LabelJust1" runat="server" Visible="False" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 658px">
                    <span>
                        <br />
                        2. How would you rate the overall <strong>quality of the journals </strong>in which the applicant has published?</span><br />
                    <br />
                    <asp:RadioButtonList ID="rblQ2" runat="server" meta:resourcekey="rblQ2Resource1">
                        <asp:ListItem Value="1" Text="Excellent" Selected="True" meta:resourcekey="ListItemResource10" />
                        <asp:ListItem Value="2" Text="Very Good" meta:resourcekey="ListItemResource11" />
                        <asp:ListItem Value="3" Text="Good" meta:resourcekey="ListItemResource12" />
                        <asp:ListItem Value="4" Text="Fair" meta:resourcekey="ListItemResource13" />
                        <asp:ListItem Value="5" Text="Poor" meta:resourcekey="ListItemResource14" />
                    </asp:RadioButtonList><br />
                    <span>Justification</span>
                    <asp:RequiredFieldValidator ID="rfvJust2" runat="server" ControlToValidate="TextBoxJustification2" ErrorMessage="Justification of Question #2 is required" Text="*" meta:resourcekey="rfvJust2Resource1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="TextBoxJustification2" runat="server" Height="85px" Width="548px" TextMode="MultiLine" meta:resourcekey="TextBoxJustification2Resource1"></asp:TextBox>
                    <asp:Label ID="LabelJustification2" runat="server" Visible="False" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 658px">
                    <span>
                        <br />
                        3. How would you rate the overall <strong>quality of conferences</strong>?</span><br />
                    <br />
                    <asp:RadioButtonList ID="rblQ3" runat="server" meta:resourcekey="rblQ3Resource1">
                        <asp:ListItem Value="1" Text="Excellent" Selected="True" meta:resourcekey="ListItemResource15" />
                        <asp:ListItem Value="2" Text="Very Good" meta:resourcekey="ListItemResource16" />
                        <asp:ListItem Value="3" Text="Good" meta:resourcekey="ListItemResource17" />
                        <asp:ListItem Value="4" Text="Fair" meta:resourcekey="ListItemResource18" />
                        <asp:ListItem Value="5" Text="Poor" meta:resourcekey="ListItemResource19" />
                    </asp:RadioButtonList><br />
                    <span>Justification</span>
                    <asp:RequiredFieldValidator ID="rfvJust3" runat="server" ControlToValidate="TextBoxJustification3" ErrorMessage="Justification of Question #3 is required" Text="*" meta:resourcekey="rfvJust3Resource1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="TextBoxJustification3" runat="server" Height="85px" Width="548px" TextMode="MultiLine" meta:resourcekey="TextBoxJustification3Resource1"></asp:TextBox>
                    <asp:Label ID="LabelJustification3" runat="server" Visible="False" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 658px">
                    <span>
                        <br />
                        4. How would you rate the <strong>originality</strong> of the Candidate's publications?</span><br />
                    <br />
                    <asp:RadioButtonList ID="rblQ4" runat="server" meta:resourcekey="rblQ4Resource1">
                        <asp:ListItem Value="1" Text="Excellent" Selected="True" meta:resourcekey="ListItemResource20" />
                        <asp:ListItem Value="2" Text="Very Good" meta:resourcekey="ListItemResource21" />
                        <asp:ListItem Value="3" Text="Good" meta:resourcekey="ListItemResource22" />
                        <asp:ListItem Value="4" Text="Fair" meta:resourcekey="ListItemResource23" />
                        <asp:ListItem Value="5" Text="Poor" meta:resourcekey="ListItemResource24" />
                    </asp:RadioButtonList><br />
                    <span>Justification</span>
                    <asp:RequiredFieldValidator ID="rfvJust4" runat="server" ControlToValidate="TextBoxJustification4" ErrorMessage="Justification of Question #4 is required" Text="*" meta:resourcekey="rfvJust4Resource1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="TextBoxJustification4" runat="server" Height="85px" Width="548px" TextMode="MultiLine" meta:resourcekey="TextBoxJustification4Resource1"></asp:TextBox>
                    <asp:Label ID="LabelJustification4" runat="server" Visible="False" BorderStyle="Solid" BorderWidth="1px"></asp:Label><br />
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 658px; height: 17px">
                    <span>
                        <br />
                        5. How would you rate the <strong>significance</strong> of the Candidate's publications?</span><br />
                    <br />
                    <asp:RadioButtonList ID="rblQ5" runat="server" meta:resourcekey="rblQ5Resource1">
                        <asp:ListItem Value="1" Text="Excellent" Selected="True" meta:resourcekey="ListItemResource25" />
                        <asp:ListItem Value="2" Text="Very Good" meta:resourcekey="ListItemResource26" />
                        <asp:ListItem Value="3" Text="Good" meta:resourcekey="ListItemResource27" />
                        <asp:ListItem Value="4" Text="Fair" meta:resourcekey="ListItemResource28" />
                        <asp:ListItem Value="5" Text="Poor" meta:resourcekey="ListItemResource29" />
                    </asp:RadioButtonList><br />
                    <span>Justification</span>
                    <asp:RequiredFieldValidator ID="rfvJust5" runat="server" ControlToValidate="TextBoxJustification5" ErrorMessage="Justification of Question #5 is required" Text="*" meta:resourcekey="rfvJust5Resource1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="TextBoxJustification5" runat="server" Height="85px" Width="548px" TextMode="MultiLine" meta:resourcekey="TextBoxJustification5Resource1"></asp:TextBox>
                    <asp:Label ID="LabelJustification5" runat="server" Visible="False" BorderStyle="Solid" BorderWidth="1px"></asp:Label><br />
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 658px">
                    <span>
                        <br />
                        6. How would rate the Candidate's <strong>independence in research</strong>?</span><br />
                    <br />
                    <asp:RadioButtonList ID="rblQ6" runat="server" meta:resourcekey="rblQ6Resource1">
                        <asp:ListItem Value="1" Text="Excellent" Selected="True" meta:resourcekey="ListItemResource30" />
                        <asp:ListItem Value="2" Text="Very Good" meta:resourcekey="ListItemResource31" />
                        <asp:ListItem Value="3" Text="Good" meta:resourcekey="ListItemResource32" />
                        <asp:ListItem Value="4" Text="Fair" meta:resourcekey="ListItemResource33" />
                        <asp:ListItem Value="5" Text="Poor" meta:resourcekey="ListItemResource34" />
                    </asp:RadioButtonList><br />
                    <span>Justification</span>
                    <asp:RequiredFieldValidator ID="rfvJust6" runat="server" ControlToValidate="TextBoxJustification6" ErrorMessage="Justification of Question #6 is required" Text="*" meta:resourcekey="rfvJust6Resource1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="TextBoxJustification6" runat="server" Height="85px" Width="548px" TextMode="MultiLine" meta:resourcekey="TextBoxJustification6Resource1"></asp:TextBox>
                    <asp:Label ID="LabelJustification6" runat="server" Visible="False" BorderStyle="Solid" BorderWidth="1px"></asp:Label><br />
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 658px">
                    <span>
                        <br />
                        7. How would you rate the <strong>citations </strong>received by the Candidate's work?</span><br />
                    <br />
                    <asp:RadioButtonList ID="rblQ7" runat="server" meta:resourcekey="rblQ7Resource1">
                        <asp:ListItem Value="1" Text="Excellent" Selected="True" meta:resourcekey="ListItemResource35" />
                        <asp:ListItem Value="2" Text="Very Good" meta:resourcekey="ListItemResource36" />
                        <asp:ListItem Value="3" Text="Good" meta:resourcekey="ListItemResource37" />
                        <asp:ListItem Value="4" Text="Fair" meta:resourcekey="ListItemResource38" />
                        <asp:ListItem Value="5" Text="Poor" meta:resourcekey="ListItemResource39" />
                    </asp:RadioButtonList><br />
                    <span>Justification</span>
                    <asp:RequiredFieldValidator ID="rfvJust7" runat="server" ControlToValidate="TextBoxJustification7" ErrorMessage="Justification of Question #7 is required" Text="*" meta:resourcekey="rfvJust7Resource1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="TextBoxJustification7" runat="server" Height="85px" Width="548px" TextMode="MultiLine" meta:resourcekey="TextBoxJustification7Resource1"></asp:TextBox>
                    <asp:Label ID="LabelJustification7" runat="server" Visible="False" BorderStyle="Solid" BorderWidth="1px"></asp:Label><br />
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 658px">
                    <span>
                        <br />
                        8. To what degree you think the applicant has achieved <strong>scholarly stature</strong> ?</span><br />
                    <br />
                    <asp:RadioButtonList ID="rblQ8" runat="server" meta:resourcekey="rblQ8Resource1">
                        <asp:ListItem Value="1" Text="Excellent" Selected="True" meta:resourcekey="ListItemResource40" />
                        <asp:ListItem Value="2" Text="Very Good" meta:resourcekey="ListItemResource41" />
                        <asp:ListItem Value="3" Text="Good" meta:resourcekey="ListItemResource42" />
                        <asp:ListItem Value="4" Text="Fair" meta:resourcekey="ListItemResource43" />
                        <asp:ListItem Value="5" Text="Poor" meta:resourcekey="ListItemResource44" />
                    </asp:RadioButtonList><br />
                    <span>Justification</span>
                    <asp:RequiredFieldValidator ID="rfvJust8" runat="server" ControlToValidate="TextBoxJustification8" ErrorMessage="Justification of Question #8 is required" Text="*" meta:resourcekey="rfvJust8Resource1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="TextBoxJustification8" runat="server" Height="85px" Width="548px" TextMode="MultiLine" meta:resourcekey="TextBoxJustification8Resource1"></asp:TextBox>
                    <asp:Label ID="LabelJustification8" runat="server" Visible="False" BorderStyle="Solid" BorderWidth="1px"></asp:Label><br />
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 658px">
                    <span>
                        <br />
                        9. Please indicate your assessment of the promotability of the applicant </span>
                    <br />
                    <br />
                    <asp:RadioButtonList ID="rblQ9" runat="server" meta:resourcekey="rblQ9Resource1">
                        <asp:ListItem Value="1" Text="Promotable" Selected="True" meta:resourcekey="ListItemResource45" />
                        <asp:ListItem Value="2" Text="Marginally Promotable" meta:resourcekey="ListItemResource46" />
                        <asp:ListItem Value="3" Text="Unpromotable" meta:resourcekey="ListItemResource47" />
                    </asp:RadioButtonList><br />
                    <span>Justification</span>
                    <asp:RequiredFieldValidator ID="rfvJust9" runat="server" ControlToValidate="TextBoxJustification9" ErrorMessage="Justification of Question #9 is required" Text="*" meta:resourcekey="rfvJust9Resource1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:TextBox ID="TextBoxJustification9" runat="server" Height="85px" Width="548px" TextMode="MultiLine" meta:resourcekey="TextBoxJustification9Resource1"></asp:TextBox>
                    <asp:Label ID="LabelJustification9" runat="server" Visible="False" BorderStyle="Solid" BorderWidth="1px"></asp:Label><br />
                </td>
            </tr>
            <tr style="font-size: 12pt">
                <td style="width: 658px">
                    <span>
                        <br />
                        <br />
                        Comments</span>
                    <br />

                    <asp:TextBox ID="TextBoxComments" runat="server" Height="108px" Width="634px" TextMode="MultiLine" meta:resourcekey="TextBoxCommentsResource1"></asp:TextBox>
                    <asp:Label ID="LabelComments" runat="server" Visible="False" BorderStyle="Solid" BorderWidth="1px"></asp:Label><br />
                    <br />
                    <asp:Panel ID="PanelUploadSupDoc" runat="server">
                        <fieldset style="margin-left: 30px; width: 500px; background-color: #f0f0f0; border: 1px solid #007d40; padding: 5px; text-align: center;">
                            <legend style="font-style: italic; font-weight: bold; background-color: #007d40; color: white">Supporting Document</legend>
                            <asp:LinkButton ID="LinkButtonSuppDoc" runat="server" OnClick="LinkButtonSuppDoc_Click" OnPreRender="lbtnEvaluationAttachment_PreRender"></asp:LinkButton>
                            <asp:Label ID="LabelSize" runat="server"></asp:Label><br />

                        </fieldset>
                    </asp:Panel>
                    &nbsp;&nbsp;</td>
            </tr>
        </table>
        <asp:ValidationSummary ID="vsRevForm" runat="server" meta:resourcekey="vsRevFormResource1" />
        <br />
        <br />
        <asp:ObjectDataSource ID="odsRevForm" runat="server" UpdateMethod="UpdateReviewerForm" TypeName="ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter" SelectMethod="GetReviewerFormByApplication" OldValuesParameterFormatString="original_{0}" InsertMethod="InsertReviewerForm" DeleteMethod="DeleteReviewerForm">
            <DeleteParameters>
                <asp:Parameter Name="Original_ApplicationID" Type="Int32" />
                <asp:Parameter Name="Original_Type" Type="Int32" />
                <asp:Parameter Name="Original_Serial" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="ApplicationID" Type="Int32" />
                <asp:Parameter Name="Type" Type="Int32" />
                <asp:Parameter Name="Serial" Type="Int32" />
                <asp:Parameter Name="familiarity" Type="String" />
                <asp:Parameter Name="Q1" Type="Int32" />
                <asp:Parameter Name="Q1Justification" Type="Object" />
                <asp:Parameter Name="Q2" Type="Int32" />
                <asp:Parameter Name="Q2Justification" Type="String" />
                <asp:Parameter Name="Q3" Type="Int32" />
                <asp:Parameter Name="Q3Justification" Type="String" />
                <asp:Parameter Name="Q4" Type="Int32" />
                <asp:Parameter Name="Q4Justification" Type="String" />
                <asp:Parameter Name="Q5" Type="Int32" />
                <asp:Parameter Name="Q5Justification" Type="String" />
                <asp:Parameter Name="Q6" Type="Int32" />
                <asp:Parameter Name="Q6Justification" Type="String" />
                <asp:Parameter Name="Q7" Type="Int32" />
                <asp:Parameter Name="Q7Justification" Type="String" />
                <asp:Parameter Name="Q8" Type="Int32" />
                <asp:Parameter Name="Q8Justification" Type="String" />
                <asp:Parameter Name="Q9" Type="Int32" />
                <asp:Parameter Name="Q9Justification" Type="String" />
                <asp:Parameter Name="Comments" Type="String" />
                <asp:Parameter Name="Original_ApplicationID" Type="Int32" />
                <asp:Parameter Name="Original_Type" Type="Int32" />
                <asp:Parameter Name="Original_Serial" Type="Int32" />
            </UpdateParameters>
            <SelectParameters>
                <asp:SessionParameter DefaultValue="1" Name="ApplicationID" SessionField="ApplicationID"
                    Type="Int32" />
                <asp:SessionParameter DefaultValue="1" Name="Type" SessionField="Type" Type="Int32" />
                <asp:SessionParameter DefaultValue="1" Name="Serial" SessionField="Serial" Type="Int32" />
            </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="ApplicationID" Type="Int32" />
                <asp:Parameter Name="Type" Type="Int32" />
                <asp:Parameter Name="Serial" Type="Int32" />
                <asp:Parameter Name="familiarity" Type="String" />
                <asp:Parameter Name="Q1" Type="Int32" />
                <asp:Parameter Name="Q1Justification" Type="Object" />
                <asp:Parameter Name="Q2" Type="Int32" />
                <asp:Parameter Name="Q2Justification" Type="String" />
                <asp:Parameter Name="Q3" Type="Int32" />
                <asp:Parameter Name="Q3Justification" Type="String" />
                <asp:Parameter Name="Q4" Type="Int32" />
                <asp:Parameter Name="Q4Justification" Type="String" />
                <asp:Parameter Name="Q5" Type="Int32" />
                <asp:Parameter Name="Q5Justification" Type="String" />
                <asp:Parameter Name="Q6" Type="Int32" />
                <asp:Parameter Name="Q6Justification" Type="String" />
                <asp:Parameter Name="Q7" Type="Int32" />
                <asp:Parameter Name="Q7Justification" Type="String" />
                <asp:Parameter Name="Q8" Type="Int32" />
                <asp:Parameter Name="Q8Justification" Type="String" />
                <asp:Parameter Name="Q9" Type="Int32" />
                <asp:Parameter Name="Q9Justification" Type="String" />
                <asp:Parameter Name="Comments" Type="String" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HiddenFieldSupDoc" runat="server" />
        <asp:HiddenField ID="HiddenFieldSize" runat="server" />
    </form>
</body>
</html>
