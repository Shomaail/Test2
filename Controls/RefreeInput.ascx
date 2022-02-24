<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RefreeInput.ascx.cs" Inherits="Controls_RefreeInput" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
   <%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<td>
    <asp:TextBox ID="txtName" runat="server" ToolTip="Enter only NAMES without salutation like Dr. or Mr. etc"></asp:TextBox>
</td>
<td>
    <anthem:DropDownList ID="ddlRank" runat="server" AutoCallBack="true" OnSelectedIndexChanged="ddlRank_SelectedIndexChanged">
        <asp:ListItem>Associate Professor</asp:ListItem>
        <asp:ListItem>Professor</asp:ListItem>
        <asp:ListItem>Senior Lecturer</asp:ListItem>
        <asp:ListItem>Reader</asp:ListItem>
        <asp:ListItem>Others..</asp:ListItem>
    </anthem:DropDownList>
    <anthem:Panel runat="server" AutoUpdateAfterCallBack="true" ID="pnlRank" Visible="False">
        <asp:TextBox ID="txtRank" runat="server"></asp:TextBox>
    </anthem:Panel>
</td>
<td>
    <asp:TextBox ID="txtMailAddress" runat="server" TextMode="multiLine" AutoPostBack="true" OnTextChanged="txtMailAddress_TextChanged" ToolTip="Make sure the University name is present in the Mailing Address"></asp:TextBox>
</td>
<td>
    <asp:TextBox ID="txtPhoneAndFax" runat="server" TextMode="multiLine"></asp:TextBox>
</td>
<td>
    <asp:TextBox ID="txtEmail" runat="server" TextMode="multiLine" ToolTip="For more than one email addresses"></asp:TextBox>
<asp:RegularExpressionValidator ID="regEmailTo" ControlToValidate="txtEmail" Text="(Invalid email)"
    ValidationExpression="^\s*(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*\s*$" runat="server" /></td>

<td>
    <asp:TextBox ID="txtMajor" runat="server" TextMode="multiLine"></asp:TextBox>
</td>
<td>
    <asp:TextBox ID="txtSpeciality" runat="server" TextMode="multiLine"></asp:TextBox>
</td>
<td>
    <asp:TextBox ID="txtActiveAreaOfResearch" runat="server" TextMode="multiLine"></asp:TextBox>
</td>
<td>
    <asp:TextBox ID="txtWebpage" runat="server" TextMode="multiLine"></asp:TextBox>
</td>
<td>
    <asp:TextBox ID="txtComments" runat="server" TextMode="multiLine" MaxLength="500"></asp:TextBox>
</td>
<td>
    <%--<asp:TextBox ID="TextBoxTotPublications" runat="server" TextMode="SingleLine" MaxLength="50">0</asp:TextBox>--%>
    <ew:numericbox runat="server" MaxLength="9" PositiveNumber="true" RealNumber="false" ID="TextBoxTotPublications" BorderWidth="1" Width="128px" style="text-align:center">0</ew:numericbox>
</td>
<td>
    <%--<asp:TextBox ID="TextBoxNoOfJournals" runat="server" TextMode="SingleLine" MaxLength="50">0</asp:TextBox>--%>
    <ew:numericbox runat="server" MaxLength="9" PositiveNumber="true" RealNumber="false" ID="TextBoxNoOfJournals" BorderWidth="1" Width="128px" style="text-align:center">0</ew:numericbox>
</td>
<td>
    <%--<asp:TextBox ID="TextBoxHIndex" runat="server" TextMode="SingleLine" MaxLength="50">0</asp:TextBox>--%>
    <ew:numericbox runat="server" MaxLength="9" PositiveNumber="true" RealNumber="false" ID="TextBoxHIndex" BorderWidth="1" Width="128px" style="text-align:center">0</ew:numericbox>
</td>
<td>
    
    <ew:numericbox runat="server" MaxLength="9" PositiveNumber="true" RealNumber="false" ID="TextBoxCitations" BorderWidth="1" Width="128px" style="text-align:center">0</ew:numericbox>
</td>