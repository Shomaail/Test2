<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Refrees.ascx.cs" Inherits="Controls_Refrees" %>
<%@ Register Src="RefreeInput.ascx" TagName="RefreeInput" TagPrefix="uc2" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>

<%@ Register Src="AddExtRev.ascx" TagName="AddExtRev" TagPrefix="uc1" %>


<style type="text/css">
    .auto-style1 {
        height: 61px;
    }
</style>


<asp:Panel runat="server" ID="pnlRefreesInput" Visible="false" ScrollBars="Auto" Width="1200px">
    <textarea id="taExtRevData" style="width: 777px; height: 85px;" runat="server" title="Paste here one row of external reviewer data from Excel with same number and title as in the table below, then select the position in the dropdownlist beside and finally click on the Fill Button to place the data in the Textbox in the right position in the table below" 
        placeholder="Paste here one row of external reviewer data from Excel with same number and title as in the table below, then select the position in the dropdownlist beside and finally click on the Fill Button to place the data in the Textbox in the right position in the table below"></textarea>
    <select runat="server" id="ddlExtRevRank" title="Select Ranking of Ext Reviewer in the list below"> 
        <option value="-1">Select Ranking of Ext Reviewer in the list below</option>
        <option value="1">1</option>
        <option value="2">2</option>
        <option value="3">3</option>
        <option value="4">4</option>
        <option value="5">5</option>
        <option value="6">6</option>
        <option value="7">7</option>
        <option value="8">8</option>
        <option value="9">9</option>
        <option value="10">10</option>
    </select>
    <input type="button" id="btnFillTB" onserverclick="btnFillTB_ServerClick" class="btn btn-primary btn-md" runat="server" value="Fill" title="Click on this button to place the data in the Textbox beside on the rank of selected external reviewer in the table below">
    <table cellpadding="3" cellspacing="0" border="1" bordercolor="#3062af" style="border-collapse: collapse;" class="table-condensed">
        <tr>
            <td colspan="12"></td>
            <td colspan="3" style="text-align: center">
                <asp:HyperLink Target="_blank" ToolTip="This document shows you how to use Scopus for filling the last four columns for external reviewers in the in the Faculty Promotion System namely Total Publications., No. Of Journal Publications, H-index and No. Of Citations" ID="HyperLinkHowfindTP_NoJ_HIndex" runat="server" NavigateUrl="~/Documents/HowtofindTotPublication_NoofJournals_h-Index_CitationForExternalReviewers.pdf">How to find?</asp:HyperLink>
                <img alt="" src="../images/New.jpg" style="width: 26px; height: 21px" />
                <asp:HyperLink  Target="_blank" ToolTip="Scopus" ID="hlScopus" runat="server" NavigateUrl="http://www.scopus.com/search/form.url?display=authorLookup">Scopus</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td class="PointsHeader"></td>
            <td class="PointsHeader">#</td>
            <th class="PointsHeader">Name</th>
            <th class="PointsHeader">Rank</th>
            <th class="PointsHeader">Mailing Address</th>
            <th class="PointsHeader">Phone and Fax</th>
            <th class="PointsHeader">Email</th>
            <th class="PointsHeader">Major Field</th>
            <th class="PointsHeader">Speciality</th>
            <th class="PointsHeader">Active Area of Research</th>
            <th class="PointsHeader">Webpage</th>
            <th class="PointsHeader">Comments</th>
            <th class="PointsHeader">Total Publications</th>
            <th class="PointsHeader">No. of Journal Publications</th>
            <th class="PointsHeader">H-index</th>
            <th class="PointsHeader">Citations</th>
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="ImageButton1" runat="server"
                    ImageUrl="../images/delete_black.JPG" Height="16px" Width="16px"
                    OnClick="ImageButton1_Click"
                    ToolTip="Click here to DELETE the External Reviewer"
                    CausesValidation="false" /></td>
            <td>1</td>
            <uc2:RefreeInput ID="RefreeInput1" runat="server" />
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:ImageButton ID="ImageButton2" runat="server"
                    ImageUrl="../images/delete_black.JPG" Height="16px" Width="16px"
                    OnClick="ImageButton2_Click"
                    ToolTip="Click here to DELETE the External Reviewer"
                    CausesValidation="false" /></td>
            <td class="auto-style1">2</td>
            <uc2:RefreeInput ID="RefreeInput2" runat="server" />
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="ImageButton3" runat="server"
                    ImageUrl="../images/delete_black.JPG" Height="16px" Width="16px"
                    OnClick="ImageButton3_Click"
                    ToolTip="Click here to DELETE the External Reviewer"
                    CausesValidation="false" /></td>
            <td>3</td>
            <uc2:RefreeInput ID="RefreeInput3" runat="server" />
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="ImageButton4" runat="server"
                    ImageUrl="../images/delete_black.JPG" Height="16px" Width="16px"
                    OnClick="ImageButton4_Click"
                    ToolTip="Click here to DELETE the External Reviewer"
                    CausesValidation="false" /></td>
            <td>4</td>
            <uc2:RefreeInput ID="RefreeInput4" runat="server" />
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="ImageButton5" runat="server"
                    ImageUrl="../images/delete_black.JPG" Height="16px" Width="16px"
                    OnClick="ImageButton5_Click"
                    ToolTip="Click here to DELETE the External Reviewer"
                    CausesValidation="false" /></td>
            <td>5</td>
            <uc2:RefreeInput ID="RefreeInput5" runat="server" />
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="ImageButton6" runat="server"
                    ImageUrl="../images/delete_black.JPG" Height="16px" Width="16px"
                    OnClick="ImageButton6_Click"
                    ToolTip="Click here to DELETE the External Reviewer"
                    CausesValidation="false" /></td>
            <td>6</td>
            <uc2:RefreeInput ID="RefreeInput6" runat="server" />
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="ImageButton7" runat="server"
                    ImageUrl="../images/delete_black.JPG" Height="16px" Width="16px"
                    OnClick="ImageButton7_Click"
                    ToolTip="Click here to DELETE the External Reviewer"
                    CausesValidation="false" /></td>
            <td>7</td>
            <uc2:RefreeInput ID="RefreeInput7" runat="server" />
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="ImageButton8" runat="server"
                    ImageUrl="../images/delete_black.JPG" Height="16px" Width="16px"
                    OnClick="ImageButton8_Click"
                    ToolTip="Click here to DELETE the External Reviewer"
                    CausesValidation="false" /></td>
            <td>8</td>
            <uc2:RefreeInput ID="RefreeInput8" runat="server" />
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="ImageButton9" runat="server"
                    ImageUrl="../images/delete_black.JPG" Height="16px" Width="16px"
                    OnClick="ImageButton9_Click"
                    ToolTip="Click here to DELETE the External Reviewer"
                    CausesValidation="false" /></td>
            <td>9</td>
            <uc2:RefreeInput ID="RefreeInput9" runat="server" />
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="ImageButton10" runat="server"
                    ImageUrl="../images/delete_black.JPG" Height="16px" Width="16px"
                    OnClick="ImageButton10_Click"
                    ToolTip="Click here to DELETE the External Reviewer"
                    CausesValidation="false" /></td>
            <td>10</td>
            <uc2:RefreeInput ID="RefreeInput10" runat="server" />
        </tr>
    </table>
</asp:Panel>

<asp:Panel runat="server" ID="pnlRefreesList" Visible="False" ScrollBars="Auto" Width="1200px">
    <asp:GridView ID="grdRefrees" runat="server" AutoGenerateColumns="False" CellPadding="4"
        DataKeyNames="ApplicationID,Type,Serial" ForeColor="Black" GridLines="Vertical" BorderColor="#3062AF">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="Black" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="Black" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate><%# Container.DataItemIndex+1 %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ApplicationID" HeaderText="ApplicationID" ReadOnly="True"
                SortExpression="ApplicationID" Visible="False">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="True" SortExpression="Type"
                Visible="False" />
            <asp:BoundField DataField="Serial" HeaderText="Serial" ReadOnly="True" SortExpression="Serial"
                Visible="False">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="Rank" HeaderText="Rank" SortExpression="Rank">
                <ItemStyle Font-Size="X-Small" Wrap="True" />
            </asp:BoundField>
            <asp:BoundField DataField="MailingAddress" HeaderText="Mailing Address" SortExpression="MailingAddress">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="PhoneAndFax" HeaderText="Phone &amp; Fax" SortExpression="PhoneAndFax">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Major" HeaderText="Major Field" SortExpression="Major">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Speciality" HeaderText="Speciality" SortExpression="Speciality">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="ActiveAreaOfResearch" HeaderText="Active Area of Research"
                SortExpression="ActiveAreaOfResearch">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Webpage" HeaderText="Webpage"
                SortExpression="Webpage">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Comments" HeaderText="Comments"
                SortExpression="Comments">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="TotalPublications" HeaderText="Total Publications"
                SortExpression="TotalPublications">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="NoOfJournals" HeaderText="No Of Journals"
                SortExpression="NoOfJournals">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="HIndex" HeaderText="H-Index"
                SortExpression="HIndex">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="Citations" HeaderText="Citations"
                SortExpression="Citations">
                <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <span style="font-size: 0.8em"></span>
    <br />
    <asp:DataList ID="dlRefrees" runat="server" BackColor="White" BorderColor="Black"
        BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyField="ApplicationID"
        GridLines="Both" Width="100%" Visible="false">
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <SelectedItemStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <ItemTemplate>
            <asp:Label ID="NameLabel" runat="server" ForeColor="black" Font-Bold="true" Text='<%# Eval("Name") %>'></asp:Label><br />
            <asp:Label ID="RankLabel" runat="server" ForeColor="black" Font-Italic="true" Text='<%# Eval("Rank") %>'></asp:Label><br />
            <asp:Label ID="MailingAddressLabel" runat="server" ForeColor="black" Text='<%# Eval("MailingAddress") %>' Width="100%"></asp:Label><br />
            <br />
            <span style="font-size: 8pt; font-weight: bold">Major Field</span>:
            <asp:Label ID="MajorLabel" runat="server" ForeColor="black" Text='<%# Eval("Major") %>'></asp:Label><br />
            <span style="font-size: 8pt; font-weight: bold">Speciality</span>:<br />
            <asp:Label ID="SpecialityLabel" runat="server" ForeColor="black" Text='<%# Eval("Speciality") %>'></asp:Label><br />
            <br />
            <span style="font-size: 8pt; font-weight: bold">Active Area of Research</span>:
            <br />
            <asp:Label ID="ActiveAreaOfResearchLabel" runat="server" ForeColor="black" Text='<%# Eval("ActiveAreaOfResearch") %>'></asp:Label><br />
            <span style="font-size: 8pt; font-weight: bold">Email</span>:
            <asp:Label ID="EmailLabel" runat="server" ForeColor="black" Text='<%# Eval("Email") %>'></asp:Label><br />
            <span style="font-size: 8pt; font-weight: bold">Phone/Fax</span>:<br />
            <asp:Label ID="PhoneAndFaxLabel" runat="server" ForeColor="black" Text='<%# Eval("PhoneAndFax") %>'></asp:Label><br />
            <br />
            <span style="font-size: 8pt; font-weight: bold">Webpage</span>:<br />
            <asp:Label ID="WebpageLabel" runat="server" ForeColor="black" Text='<%# Eval("Webpage") %>'></asp:Label><br />
            <br />
            <span style="font-size: 8pt; font-weight: bold">Comments</span>:<br />
            <asp:Label ID="CommentsLabel" runat="server" ForeColor="black" Text='<%# Eval("Comments") %>'></asp:Label><br />
            <br />
            <span style="font-size: 8pt; font-weight: bold">Total Publications</span>:<br />
            <asp:Label ID="TotalPublicationsLabel" runat="server" ForeColor="black" Text='<%# Eval("TotalPublications") %>'></asp:Label><br />
            <br />
            <span style="font-size: 8pt; font-weight: bold">No Of Journals</span>:<br />
            <asp:Label ID="NoOfJournalsLabel" runat="server" ForeColor="black" Text='<%# Eval("NoOfJournals") %>'></asp:Label><br />
            <br />
            <span style="font-size: 8pt; font-weight: bold">H-Index</span>:<br />
            <asp:Label ID="HIndexLabel" runat="server" ForeColor="black" Text='<%# Eval("HIndex") %>'></asp:Label><br />
            <br />
            <span style="font-size: 8pt; font-weight: bold">Citations</span>:<br />
            <asp:Label ID="CitationsLabel" runat="server" ForeColor="black" Text='<%# Eval("Citations") %>'></asp:Label><br />
            <br />


        </ItemTemplate>
        <ItemStyle BackColor="White" ForeColor="#3062af" />
        <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="#CCCCFF" />
    </asp:DataList>
</asp:Panel>
<p>
    <uc1:AddExtRev ID="AddExtRev1" runat="server" Visible="false" />
</p>
<asp:HiddenField ID="hdnApplicationID" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />

