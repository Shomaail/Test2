<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChecklistForm.ascx.cs" Inherits="Controls_ChecklistForm" %>

<p>
    <br />
</p>

<asp:FormView ID="FormView1" runat="server" DataKeyNames="ApplicationID" 
    DataSourceID="ObjectDataSource_CheckList" DefaultMode="Edit">
    <EditItemTemplate>
        <table border="1" class="table-condensed">
            <tr>
                <td >
                    <b>No.</b>
                </td>
                <td>
                    
                        <b>Requirement</b>
                </td>
                <td>
                   
                        <b>Status/Comments</b>
                </td>
            </tr>
            <tr>
               <td>
                    <p style="text-align: center">
                        1.</p>
                </td>
                <td>
                    
                        Service at an Assistant Professor rank (or Associate rank in case of applying for
                        Full Professorship) <b>must be at least three and a half years.</b>
                        <br />
                        Years of service (as certified by Faculty Affairs):
                        Years:&nbsp;
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Service_NumYears") %>' ReadOnly="True" Width="45px"></asp:TextBox>
                    
                        Months:&nbsp;
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Service_NumMonths") %>' ReadOnly="True" Width="45px"></asp:TextBox>
                    
                </td>
                <td>
                    <p style="text-align: center">    
                            <asp:CheckBox id="Field1StatusCheckBox" runat="server" Checked='<%# Bind("Field1Status") %>'></asp:CheckBox>
                            <br />
                            <asp:TextBox id="Field1CommentsTextBox" runat="server" Text='<%# Bind("Field1Comments") %>' TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);" ></asp:TextBox>
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <p style=" text-align: center">
                        2.</p>
                </td>
                <td>
                    
                        Service at a Saudi University <b>at least one year</b>
                    <br />
                        
                        Number of years served at a Saudi University (as certified by Faculty Affairs):
                        Years<asp:TextBox ID="TextBox3" runat="server" ReadOnly="True" Text='<%# Bind("Saudi_NumYears") %>' Width="41px"></asp:TextBox> 
                        Months<asp:TextBox ID="TextBox4" runat="server" ReadOnly="True" Text='<%# Bind("Saudi_NumMonths") %>' Width="45px"></asp:TextBox>
                </td>
                <td>
                    <p align="center"  style="text-align: center">
               
                        <asp:CheckBox id="Field2StatusCheckBox" runat="server" Checked='<%# Bind("Field2Status") %>' Font-Bold="False"></asp:CheckBox> 
                        <br />
                        <asp:TextBox id="Field2CommentsTextBox" runat="server" Text='<%# Bind("Field2Comments") %>' TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);" ></asp:TextBox>
               
                    </p>
                </td>
            </tr>
            <tr>
              <td>
                    <p style=" text-align: center">
                        3.</p>
                </td>
                <td>
                        Number of units from all scholarly work <b>greater than or equal to 4 (6) </b>for
                        promotion to Associate Professor (Full Professor). 
                        <br/>Units Calculation for Promotion Eligibility<br />
                   No. of units&nbsp;<asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("SumTotal") %>' Width="45px"></asp:TextBox><o:p></o:p></td>
                <td>
                    <p align="center" >
                        
                            <asp:CheckBox id="Field3StatusCheckBox" runat="server" Checked='<%# Bind("Field3Status") %>' Font-Italic="False"></asp:CheckBox>
                            <br />
                            <asp:TextBox id="Field3CommentsTextBox" runat="server" Text='<%# Bind("Field3Comments") %>' TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);" ></asp:TextBox>
                        </p>
                </td>
            </tr>
            <tr>
                <td>
                    <p style=" text-align: center">
                        4.</p>
                </td>
                <td>
                    <p >
                        Single authorship units <b>greater or equal to 2 (3) </b>for promotion to Associate
                        Professor (Full Professor).<br/>Units Calculation for Promotion Eligibility.<br />
                        No. of single authorship units&nbsp;
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("SumSigle") %>' 
                            Width="45px"></asp:TextBox>
                        </p>
                </td>
                <td>
                    <p align="center"  style="text-align: center">
                        
                            <asp:CheckBox id="Field4StatusCheckBox" runat="server" Checked='<%# Bind("Field4Status") %>'></asp:CheckBox> 
                            <br />
                            <asp:TextBox id="Field4CommentsTextBox" runat="server" Text='<%# Bind("Field4Comments") %>' TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);" ></asp:TextBox>
                        
                    </p>
                </td>
            </tr>
            <tr>
<td>
                    <p style=" text-align: center">
                        5.</p>
                </td>
                <td>Units from Journal Publications <b>greater than or equal to 1 (2) </b>
                            for promotion to Associate Professor (Full Professor).<br/>Units Calculation for Promotion Eligibility.<br/>
                    No. of units &nbsp; <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("SumJournal") %>'
                                            Width="45px"></asp:TextBox>
                </td>
                <td>
                    <p align="center"  style="text-align: center">
                        
                            <asp:CheckBox id="Field5StatusCheckBox" runat="server" Checked='<%# Bind("Field5Status") %>' Font-Bold="False"></asp:CheckBox> 
                            <asp:TextBox id="Field5CommentsTextBox" runat="server" Text='<%# Bind("Field5Comments") %>' TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);" ></asp:TextBox>
                        
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    <p style="text-align: center">
                        6.</p>
                </td>
                <td>
                    <p >
                        Work is published <b>in more than one</b> publication channel (i.e.
                        different journal publishers and University and Scientific Establishments).
                        </p>
                </td>
                <td>
                    <p align="center"  style="text-align: center">                        
                            <asp:CheckBox id="Field6StatusCheckBox" runat="server" Checked='<%# Bind("Field6Status") %>'></asp:CheckBox>
                            <br /><asp:TextBox id="Field6CommentsTextBox" runat="server" Text='<%# Bind("Field6Comments") %>' TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);" ></asp:TextBox>
                    </p>
                </td>
            </tr>
            <tr>
              <td>
                    <p style=" text-align: center">
                        7.</p>
                </td>
                <td>
                    <p >
                        Papers <b>extracted from M.S. or Ph.D. Thesis </b>and papers <b>published prior to holding
                            current rank </b>are indicated in the list of publication and are <b>not used </b>
                        to determine the number of units from scholarly work
                        <o:p></o:p>
                    </p>
                </td>
                <td>
                    <p align="center"  style="margin: 0in 0in 0pt; text-align: center">
                        
                            <asp:CheckBox id="Field7StatusCheckBox" runat="server" Checked='<%# Bind("Field7Status") %>'></asp:CheckBox>
                            <br />
                            <asp:TextBox id="Field7CommentsTextBox" runat="server" Text='<%# Bind("Field7Comments") %>' TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);" ></asp:TextBox>
                        
                    </p>
                </td>
            </tr>
            <tr>
             <td>
                    <p style=" text-align: center">
                        8.</p>
                </td>
                <td>
                    <p >
                        In case of applying for the same rank a second time, the applicant must acquire
                        <b>at least 1 </b>additional unit to his previous record.</p>
                    <p >
                        Number of units acquired since last application.</p>
                </td>
                <td>
                    <p align="center"  style="text-align: center">
                        <span style="font-size: 14pt">
                            <asp:CheckBox id="Field8StatusCheckBox" runat="server" __designer:wfdid="w41" Checked='<%# Bind("Field8Status") %>'></asp:CheckBox> <asp:TextBox id="Field8CommentsTextBox" runat="server" __designer:wfdid="w42" Text='<%# Bind("Field8Comments") %>' TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);" ></asp:TextBox>&nbsp;
                        </span>
                    </p>
                </td>
            </tr>
        </table>
        <br />
        Overall Status<asp:CheckBox ID="OverallStatusCheckBox" runat="server" Checked='<%# Eval("OverallStatus").ToString() == "false" || Eval("OverallStatus").ToString() == "true" ? bool.Parse(Eval("OverallStatus").ToString()): false%>' />
        &nbsp;<asp:TextBox ID="RemarksTextBox" runat="server" Text='<%# Bind("Remarks") %>' TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);" ></asp:TextBox>
        <br />
        <asp:Button ID="ButtonUpdate" runat="server" CommandName="Update" Text="Update" Visible="false" />
    </EditItemTemplate>
    <InsertItemTemplate>
        ApplicationID:
        <asp:TextBox ID="ApplicationIDTextBox" runat="server" Text='<%# Bind("ApplicationID") %>'>
        </asp:TextBox><br />
        Field1Status:
        <asp:CheckBox ID="Field1StatusCheckBox" runat="server" Checked='<%# Bind("Field1Status") %>' /><br />
        Field1Comments:
        <asp:TextBox ID="Field1CommentsTextBox" runat="server" Text='<%# Bind("Field1Comments") %>'>
        </asp:TextBox><br />
        Field2Status:
        <asp:CheckBox ID="Field2StatusCheckBox" runat="server" Checked='<%# Bind("Field2Status") %>' /><br />
        Field2Comments:
        <asp:TextBox ID="Field2CommentsTextBox" runat="server" Text='<%# Bind("Field2Comments") %>'>
        </asp:TextBox><br />
        Field3Status:
        <asp:CheckBox ID="Field3StatusCheckBox" runat="server" Checked='<%# Bind("Field3Status") %>' /><br />
        Field3Comments:
        <asp:TextBox ID="Field3CommentsTextBox" runat="server" Text='<%# Bind("Field3Comments") %>'>
        </asp:TextBox><br />
        Field4Status:
        <asp:CheckBox ID="Field4StatusCheckBox" runat="server" Checked='<%# Bind("Field4Status") %>' /><br />
        Field4Comments:
        <asp:TextBox ID="Field4CommentsTextBox" runat="server" Text='<%# Bind("Field4Comments") %>'>
        </asp:TextBox><br />
        Field5Status:
        <asp:CheckBox ID="Field5StatusCheckBox" runat="server" Checked='<%# Bind("Field5Status") %>' /><br />
        Field5Comments:
        <asp:TextBox ID="Field5CommentsTextBox" runat="server" Text='<%# Bind("Field5Comments") %>'>
        </asp:TextBox><br />
        Field6Status:
        <asp:CheckBox ID="Field6StatusCheckBox" runat="server" Checked='<%# Bind("Field6Status") %>' /><br />
        Field6Comments:
        <asp:TextBox ID="Field6CommentsTextBox" runat="server" Text='<%# Bind("Field6Comments") %>'>
        </asp:TextBox><br />
        Field7Status:
        <asp:CheckBox ID="Field7StatusCheckBox" runat="server" Checked='<%# Bind("Field7Status") %>' /><br />
        Field7Comments:
        <asp:TextBox ID="Field7CommentsTextBox" runat="server" Text='<%# Bind("Field7Comments") %>'>
        </asp:TextBox><br />
        Field8Status:
        <asp:CheckBox ID="Field8StatusCheckBox" runat="server" Checked='<%# Bind("Field8Status") %>' /><br />
        Field8Comments:
        <asp:TextBox ID="Field8CommentsTextBox" runat="server" Text='<%# Bind("Field8Comments") %>'>
        </asp:TextBox><br />
        OverallStatus:
        <asp:CheckBox ID="OverallStatusCheckBox" runat="server" Checked='<%# Bind("OverallStatus") %>' /><br />
        Remarks:
        <asp:TextBox ID="RemarksTextBox" runat="server" Text='<%# Bind("Remarks") %>'>
        </asp:TextBox><br />
        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
            Text="Insert">
        </asp:LinkButton>
        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
            Text="Cancel">
        </asp:LinkButton>
    </InsertItemTemplate>
    
</asp:FormView>
<asp:ObjectDataSource ID="ObjectDataSource_CheckList" runat="server" DeleteMethod="Delete"
    InsertMethod="Insert" OnInserting="ObjectDataSource_CheckList_Inserting"
    OnSelecting="ObjectDataSource_CheckList_Selecting" SelectMethod="GetDataByApplicationID"
    TypeName="ApplicationCheckListTableAdapters.ApplicationChecklistTableAdapter"
    UpdateMethod="UpdateQuery">
    <DeleteParameters>
        <asp:Parameter Name="Original_ApplicationID" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="ApplicationID" Type="Int32" />
        <asp:Parameter Name="Field1Status" Type="Boolean" />
        <asp:Parameter Name="Field1Comments" Type="String" />
        <asp:Parameter Name="Field2Status" Type="Boolean" />
        <asp:Parameter Name="Field2Comments" Type="String" />
        <asp:Parameter Name="Field3Status" Type="Boolean" />
        <asp:Parameter Name="Field3Comments" Type="String" />
        <asp:Parameter Name="Field4Status" Type="Boolean" />
        <asp:Parameter Name="Field4Comments" Type="String" />
        <asp:Parameter Name="Field5Status" Type="Boolean" />
        <asp:Parameter Name="Field5Comments" Type="String" />
        <asp:Parameter Name="Field6Status" Type="Boolean" />
        <asp:Parameter Name="Field6Comments" Type="String" />
        <asp:Parameter Name="Field7Status" Type="Boolean" />
        <asp:Parameter Name="Field7Comments" Type="String" />
        <asp:Parameter Name="Field8Status" Type="Boolean" />
        <asp:Parameter Name="Field8Comments" Type="String" />
        <asp:Parameter Name="OverallStatus" Type="Boolean" />
        <asp:Parameter Name="Remarks" Type="String" />
        <asp:Parameter Name="Original_ApplicationID" Type="Int32" />
    </UpdateParameters>
    <SelectParameters>
        <asp:QueryStringParameter Name="ApplicationID" QueryStringField="applicationID" Type="Int32" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Name="ApplicationID" Type="Int32" />
        <asp:Parameter Name="Field1Status" Type="Boolean" />
        <asp:Parameter Name="Field1Comments" Type="String" />
        <asp:Parameter Name="Field2Status" Type="Boolean" />
        <asp:Parameter Name="Field2Comments" Type="String" />
        <asp:Parameter Name="Field3Status" Type="Boolean" />
        <asp:Parameter Name="Field3Comments" Type="String" />
        <asp:Parameter Name="Field4Status" Type="Boolean" />
        <asp:Parameter Name="Field4Comments" Type="String" />
        <asp:Parameter Name="Field5Status" Type="Boolean" />
        <asp:Parameter Name="Field5Comments" Type="String" />
        <asp:Parameter Name="Field6Status" Type="Boolean" />
        <asp:Parameter Name="Field6Comments" Type="String" />
        <asp:Parameter Name="Field7Status" Type="Boolean" />
        <asp:Parameter Name="Field7Comments" Type="String" />
        <asp:Parameter Name="Field8Status" Type="Boolean" />
        <asp:Parameter Name="Field8Comments" Type="String" />
        <asp:Parameter Name="OverallStatus" Type="Boolean" />
        <asp:Parameter Name="Remarks" Type="String" />
    </InsertParameters>
</asp:ObjectDataSource>
