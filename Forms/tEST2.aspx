<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tEST2.aspx.cs" Inherits="Forms_tEST2" %>

<%@ Register Src="../Controls/Search.ascx" TagName="Search" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    
<%--
 <script type="text/javascript">
   function popWin(){
        window.open('test.aspx', '', '');");
   }
   </script>--%>

</head>


<body>
<%--<BUTTON onclick="OpenChild()" type="button">Open Child Window</BUTTON>--%>

    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
        sd f
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Forms/tEST2.aspx#name">LinkButton</asp:LinkButton>
        
        response.redirect("yourpage.aspx#your_anchor_name") 

         sad
         f asd
         f as d<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/images/Binocular.jpg"
            NavigateUrl="~/Forms/tEST2.aspx#Search" ToolTip="Search for KFUPM Employees">HyperLink</asp:HyperLink><br />
        <br />
        <br />
        <br />
        <br />
        f
         sda
          fsd
          a f<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl = "~/forms/test2.aspx#">HyperLink</asp:HyperLink>
        <asp:label id="labelid" style="style" runat="server"><a name="Search"></a></asp:label> 
        <br />
        <uc1:Search ID="Search1" runat="server" />
        
        </div>
        
   </form>
</body>
</html>
