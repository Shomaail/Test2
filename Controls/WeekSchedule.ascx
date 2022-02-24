<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WeekSchedule.ascx.cs" Inherits="Controls_WeekSchedule" %>
<asp:HiddenField ID="hdnApplicationID" runat="server" />

<p>
</p>
<p>
</p>
<fieldset style="margin-left: 30px; width: 90%; background-color: #f0f0f0; border: 1px solid #3062af; padding: 5px">
    <legend style="font-style: italic; font-weight: bold; background-color: #007d40; color: white">Busy Week Schedule of the week</legend>
    <br />
    <asp:Label ID="LabelWeek" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        CellPadding="4" DataSourceID="SqlDataSourcePC" ForeColor="#333333" CssClass="table-condensed"
        GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate><%# Container.DataItemIndex +1 %> </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Department" HeaderText="Department"
                SortExpression="Department" />
            <asp:BoundField DataField="Posting Time" HeaderText="Posting Time"
                SortExpression="Posting Time" />
            <asp:TemplateField HeaderText="Busy Schedule" SortExpression="Busy Schedule">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server"
                        Text='<%# bool.Parse(Eval("Busy Schedule").ToString())? "Posted": "Not Posted" %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#007d40" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSourcePC" runat="server"
        ConnectionString="<%$ ConnectionStrings:FPSDBConnectionString %>"
        SelectCommand="SELECT Form_FinalPromotionCommittee.Name, Form_FinalPromotionCommittee.Department, Form_MeetingMembers.PostingDateTime AS [Posting Time], Form_MeetingMembers.SchedulePostStatus AS [Busy Schedule] FROM Form_Meetings INNER JOIN Form_MeetingMembers ON Form_Meetings.MeetingID = Form_MeetingMembers.MeetingID LEFT OUTER JOIN Form_FinalPromotionCommittee ON Form_MeetingMembers.MembersID = Form_FinalPromotionCommittee.AutoNumberPK WHERE (Form_Meetings.ApplicationID = @ApplicationID) AND (Form_Meetings.MeetingID = @MeetingID) ORDER BY Form_FinalPromotionCommittee.Position">
        <SelectParameters>
            <asp:Parameter Name="ApplicationID" Type="Int32" />
            <asp:Parameter Name="MeetingID" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <table style="border-collapse: collapse; width: 90%" class="table-condensed">
        <tr class="PointsHeader">
            <td style="text-align: center;">Periods</td>
            <td style="width: 150px; text-align: center">Sunday
            </td>
            <td style="width: 150px; text-align: center">Monday</td>
            <td style="width: 150px; text-align: center">Tuesday</td>
            <td style="width: 150px; text-align: center">Wednesday</td>
            <td style="width: 150px; text-align: center">Thursday</td>

        </tr>
        <tr>
            <td style="border-style: solid; border-width: 1px; border-color: inherit; text-align: center;"
                nowrap="nowrap">7:00 - 8:00 AM</td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button1" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button2" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button3" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button4" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button5" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
        </tr>
        <tr>
            <td style="border-style: solid; border-width: 1px; border-color: inherit; text-align: center;"
                nowrap="nowrap">8:00 - 9:00 AM</td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button6" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button7" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button8" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button9" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button10" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
        </tr>
        <tr>
            <td style="border-style: solid; border-width: 1px; border-color: inherit; text-align: center;"
                nowrap="nowrap">9:00 - 10:00 AM</td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button11" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button12" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button13" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button14" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button15" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
        </tr>
        <tr>
            <td style="border-style: solid; border-width: 1px; border-color: inherit; text-align: center;"
                nowrap="nowrap">10:00 - 11:00 AM</td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button16" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button17" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button18" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button19" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button20" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
        </tr>
        <tr>
            <td style="border-style: solid; border-width: 1px; border-color: inherit; text-align: center;"
                nowrap="nowrap">11:00 - 12:00 Noon
            </td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button21" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button22" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button23" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button24" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button25" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
        </tr>
        <tr>
            <td style="border-style: solid; border-width: 1px; border-color: inherit; text-align: center;"
                nowrap="nowrap">12:00 - 1:00 PM</td>
            <td style="width: 100px; height: 19px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button26" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 19px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button27" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 19px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button28" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 19px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button29" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 19px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button30" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
        </tr>
        <tr>
            <td style="border-style: solid; border-width: 1px; border-color: inherit; text-align: center;"
                nowrap="nowrap">1:00 - 2:00 PM</td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button31" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button32" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button33" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button34" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button35" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
        </tr>
        <tr>
            <td style="border-style: solid; border-width: 1px; border-color: inherit; text-align: center;"
                nowrap="nowrap">2:00 - 3:00 PM</td>
            <td style="width: 100px; height: 18px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button36" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 18px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button37" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 18px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button38" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 18px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button39" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 18px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button40" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
        </tr>
        <tr>
            <td style="border-style: solid; border-width: 1px; border-color: inherit; text-align: center;"
                nowrap="nowrap">3:00 - 4:00 PM</td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button41" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button42" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button43" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button44" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
            <td style="width: 100px; height: 20px; border-style: solid; border-width: 1px; border-color: inherit; text-align: center">
                <asp:Button ID="Button45" runat="server" Text="0" BorderStyle="None" BackColor="White" Font-Names="Arial" Font-Size="Medium" ForeColor="DimGray" /></td>
        </tr>
    </table>

</fieldset>
