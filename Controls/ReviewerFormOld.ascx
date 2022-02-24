<div style="text-align: center">
    <span style="font-size: 10pt; color: #000000; font-family: Georgia">Research Performance
        Evaluation Form For<br />
        Promotion to the Rank of
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></span></div>
<table>
    <tr>
        <td style="width: 658px; height: 140px;">
            <span style="font-size: 10pt"><span style="font-family: Arial">
                            <span style="font-size: 10pt; color: #000000; font-family: Georgia">
                        <asp:Label ID="Label1" runat="server" Style="z-index: 100; left: 18px; position: absolute;
                            top: 75px" Text="Name of Reviewer:"></asp:Label>
                        <br />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <asp:TextBox ID="TextBoxName" runat="server" Width="317px"></asp:TextBox><br />
                        <br />
                        &nbsp; &nbsp; &nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server" Style="z-index: 102; left: 21px; position: absolute;
                            top: 116px" Text="Address:"></asp:Label>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <asp:TextBox ID="TextBoxAddress" runat="server" Height="78px" Width="317px"></asp:TextBox><br />
                    </span>
            </span></span><span style="font-size: 10pt"></span><span style="font-size: 10pt"></span>
            </td>
    </tr>
</table>

<p>
    <table>
        <tr>
            <td style="width: 658px">
                <span style="font-size: 10pt"><span style="font-family: Arial">
                    <div id="u6_rtf">
                        <span style="font-size: 10pt; color: #000000; font-family: Georgia">Please indicate
                            how familiar you are with applicant's area of specialization:</span></div>
                </span></span>
                <p>
                    <span style="font-size: 10pt">
                        <table style="width: 586px; font-family: Georgia;">
                            <tr>
                                <td style="width: 261px">
                                    &nbsp; &nbsp;&nbsp;
                                    <asp:RadioButton ID="RadioButtonFamiliarity1" runat="server" Text="I am actively engaged in research in the applicant's specific area." GroupName="familiarity" Width="528px" /></td>
                            </tr>
                            <tr>
                                <td style="width: 261px; height: 22px">
                                    &nbsp; &nbsp;&nbsp;
                                    <asp:RadioButton ID="RadioButtonFamiliarity2" runat="server" Text="I have carried out research in the past in the applicant's specific area" GroupName="familiarity" /></td>
                            </tr>
                            <tr>
                                <td style="width: 261px">
                                    &nbsp; &nbsp;&nbsp;
                                    <asp:RadioButton ID="RadioButtonFamiliarity3" runat="server" Text="My experience is in the general area but I have not worked in the applicant's specific area." GroupName="familiarity" Width="560px" /></td>
                            </tr>
                            <tr>
                                <td style="width: 261px; height: 22px">
                                    &nbsp;&nbsp;
                                    <asp:RadioButton ID="RadioButtonFamiliarity4" runat="server" Text="Others" GroupName="familiarity" />
                                    <br />
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    <asp:TextBox ID="TextBoxOthers" runat="server" Height="85px" Width="423px"></asp:TextBox></td>
                            </tr>
                        </table>
                    </span>
                </p>
                <span style="font-size: 10pt">Questions <span
            style="font-family: Arial">1</span> through <span style="font-family: Arial">
            8</span>
            deal with the Candidate's attributes concerning his research achievements through
            his publications. For each of these attributes, please provide rating and justification
            of that rating.<br />
                </span>
            </td>
        </tr>
        <tr>
            <td style="width: 658px">
                <p>
                    <span style="font-size: 10pt"><span style="font-family: Arial">1. </span>How would you
                        rate the <strong>productivity</strong> of the Candidate's work in terms of publication
                        frequency and quantity?</span></p>
                <p>
                    <span style="font-size: 10pt">
                        <table style="width: 586px">
                            <tr>
                                <td style="width: 194px">
                                    &nbsp; &nbsp;&nbsp;
                                    <asp:RadioButton ID="RadioButtonQ1a" runat="server" Text="Excellent" GroupName="Q1" /></td>
                            </tr>
                            <tr>
                                <td style="width: 194px; height: 22px">
                                    &nbsp; &nbsp;&nbsp;
                                    <asp:RadioButton ID="RadioButtonQ1b" runat="server" Text="Very Good" GroupName="Q1" /></td>
                            </tr>
                            <tr>
                                <td style="width: 194px">
                                    &nbsp; &nbsp;&nbsp;
                                    <asp:RadioButton ID="RadioButtonQ1c" runat="server" Text="Good" GroupName="Q1" /></td>
                            </tr>
                            <tr>
                                <td style="width: 194px; height: 22px">
                                    &nbsp; &nbsp;&nbsp;
                                    <asp:RadioButton ID="RadioButtonQ1d" runat="server" Text="Fair" GroupName="Q1" /></td>
                            </tr>
                            <tr>
                                <td style="width: 194px">
                                    &nbsp; &nbsp;&nbsp;
                                    <asp:RadioButton ID="RadioButtonQ1e" runat="server" Text="Poor" GroupName="Q1" /></td>
                            </tr>
                            <tr>
                                <td style="width: 194px">
                                    &nbsp; &nbsp;&nbsp; Justification<br />
                                    &nbsp; &nbsp;&nbsp;
                                    <asp:TextBox ID="TextBoxJust1" runat="server" Height="85px" Width="548px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Justification is necessary"
                                        Style="z-index: 100; left: 586px; position: absolute; top: 790px">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 194px">
                                </td>
                            </tr>
                        </table>
                    </span>
                </p>
            </td>
        </tr>
        <tr>
            <td style="width: 658px">
                <p>
                    <span style="font-size: 10pt"><span style="font-family: Arial">2. </span>How would you
                        rate the overall <strong>quality of the journals </strong>in which the applicant
                        has published?</span></p>
                <p>
                    <table style="width: 586px">
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton2a" runat="server" Text="Excellent" GroupName="Q2"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton2b" runat="server" Text="Very Good" GroupName="Q2"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton2c" runat="server" Text="Good" GroupName="Q2"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px; height: 22px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton2d" runat="server" Text="Fair" GroupName="Q2"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton2e" runat="server" Text="Poor" GroupName="Q2"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp; Justification<br />
                                &nbsp; &nbsp;&nbsp;
                                <asp:TextBox ID="TextBoxJustification2" runat="server" Height="85px" Width="548px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                            </td>
                        </tr>
                    </table>
                </p>
                <p>
                    <span style="font-size: 10pt"></span>&nbsp;</p>
            </td>
        </tr>
        <tr>
            <td style="width: 658px">
                <span style="font-size: 10pt"><span style="font-family: Arial">3. </span>How would you
                    rate the overall <strong>quality of conferences</strong>?<br />
                    <br />
                    <table style="width: 586px">
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton3a" runat="server" Text="Excellent" GroupName="Q3"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton3b" runat="server" Text="Very Good" GroupName="Q3"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton3c" runat="server" Text="Good" GroupName="Q3"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px; height: 22px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton3d" runat="server" Text="Fair" GroupName="Q3"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton3e" runat="server" Text="Poor" GroupName="Q3"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp; Justification<br />
                                &nbsp; &nbsp;&nbsp;<asp:TextBox ID="TextBoxJustification3" runat="server" Height="85px" Width="548px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                            </td>
                        </tr>
                    </table>
                </span>
            </td>
        </tr>
        <tr>
            <td style="width: 658px">
                <span style="font-size: 10pt"><span style="font-family: Arial">4. </span>How would you
                    rate the <strong>originality</strong> of the Candidate's publications?<br />
                    <br />
                    <table style="width: 586px">
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton4a" runat="server" Text="Excellent" GroupName="Q4"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton4b" runat="server" Text="Very Good" GroupName="Q4"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton4c" runat="server" Text="Good" GroupName="Q4"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px; height: 22px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton4d" runat="server" Text="Fair" GroupName="Q4"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton4e" runat="server" Text="Poor" GroupName="Q4"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp; Justification<br />
                                &nbsp; &nbsp;&nbsp;
                                <asp:TextBox ID="TextBoxJustification4" runat="server" Height="85px" Width="548px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                            </td>
                        </tr>
                    </table>
                </span>
            </td>
        </tr>
        <tr>
            <td style="width: 658px; height: 17px">
                <span style="font-size: 10pt"><span style="font-family: Arial">5. </span>How would you
                    rate the <strong>significance</strong> of the Candidate's publications?<br />
                    <br />
                    <table style="width: 586px">
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton5a" runat="server" Text="Excellent" GroupName="Q5"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton5b" runat="server" Text="Very Good" GroupName="Q5"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton5c" runat="server" Text="Good" GroupName="Q5" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px; height: 22px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton5d" runat="server" Text="Fair" GroupName="Q5" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton5e" runat="server" Text="Poor" GroupName="Q5" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp; Justification<br />
                                &nbsp; &nbsp;&nbsp;<asp:TextBox ID="TextBoxJustification5" runat="server" Height="85px" Width="548px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                            </td>
                        </tr>
                    </table>
                </span>
            </td>
        </tr>
        <tr>
            <td style="width: 658px">
                <span style="font-size: 10pt"><span style="font-family: Arial">6. </span>How would rate
                    the Candidate's <strong>independence in research</strong>?<br />
                    <br />
                    <table style="width: 586px">
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButtonQ6a" runat="server" GroupName="Q6" Text="Excellent" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButtonQ6b" runat="server" GroupName="Q6" Text="Very Good" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px; height: 22px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButtonQ6c" runat="server" GroupName="Q6" Text="Good" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px; height: 22px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButtonQ6d" runat="server" GroupName="Q6" Text="Fair" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButtonQ6e" runat="server" GroupName="Q6" Text="Poor" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp; Justification<br />
                                &nbsp; &nbsp;&nbsp;<asp:TextBox ID="TextBoxJustification6" runat="server" Height="85px" Width="548px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                            </td>
                        </tr>
                    </table>
                </span>
            </td>
        </tr>
        <tr>
            <td style="width: 658px">
                <span style="font-size: 10pt"><span style="font-family: Arial">7. </span>How would you
                    rate the <strong>citations </strong>received by the Candidate's work?<br />
                    <br />
                    <br />
                    <table style="width: 586px">
                        <tr>
                            <td style="width: 194px; height: 22px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton7a" runat="server" Text="Excellent" GroupName="Q7"  /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton7b" runat="server" Text="Very Good" GroupName="Q7" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px; height: 21px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton7c" runat="server" Text="Good" GroupName="Q7" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px; height: 22px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton7d" runat="server" Text="Fair" GroupName="Q7" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton7e" runat="server" Text="Poor" GroupName="Q7" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp; Justification<br />
                                &nbsp; &nbsp;&nbsp;<asp:TextBox ID="TextBoxJustification7" runat="server" Height="85px" Width="548px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                            </td>
                        </tr>
                    </table>
                </span>
            </td>
        </tr>
        <tr>
            <td style="width: 658px">
                <span style="font-size: 10pt"><span style="font-family: Arial">8- </span>To what degree
                    you think the applicant has achieved <strong>scholarly stature</strong> ?<br />
                    <br />
                    <table style="width: 586px">
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton8a" runat="server" Text="Excellent" GroupName="Q8" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton8b" runat="server" Text="Very Good" GroupName="Q8" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton8c" runat="server" Text="Good" GroupName="Q8"  /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px; height: 22px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton8d" runat="server" Text="Fair" GroupName="Q8"  /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton8e" runat="server" Text="Poor" GroupName="Q8"  /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp; Justification<br />
                                &nbsp; &nbsp;<asp:TextBox ID="TextBoxJustification8" runat="server" Height="85px" Width="548px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                            </td>
                        </tr>
                    </table>
                </span>
            </td>
        </tr>
        <tr>
            <td style="width: 658px; text-align: justify;">
                <span style="font-size: 10pt"><span style="font-family: Arial">9. </span>If your institution
                    were to adopt the same research criteria for promotion to the rank of&nbsp; as that
                    of </span>
                <asp:Label ID="LabelRank" runat="server" Text="Label"></asp:Label><span style="font-size: 10pt">
                    KFUPM (please refer to the attached criteria), what would be your assessment of
                    the likelihood that the applicant will be promoted in your institution
                    <br />
                    <br />
                    <table style="width: 586px">
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton9a" runat="server" Text="Promotable" GroupName="Q9"/></td>
                        </tr>
                        <tr>
                            <td style="width: 194px; height: 22px">
                                &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<asp:RadioButton ID="RadioButton9b" runat="server" Text="Marginally Promotable" GroupName="Q9" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp;
                                <asp:RadioButton ID="RadioButton9c" runat="server" Text="Unpromotable" GroupName="Q9" /></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                                &nbsp; &nbsp;&nbsp; Justification<br />
                                &nbsp;&nbsp;
                                <asp:TextBox ID="TextBoxJustification9" runat="server" Height="85px" Width="548px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 194px">
                            </td>
                        </tr>
                    </table>
                </span>
            </td>
        </tr>
        <tr>
            <td style="width: 658px">
                Comments<br />
                <asp:TextBox ID="TextBoxComments" runat="server" Height="108px" Width="634px"></asp:TextBox></td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Style="z-index: 100;
        left: 15px; position: absolute; top: 3724px" />
</p>
<br />
<br />
&nbsp;<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
