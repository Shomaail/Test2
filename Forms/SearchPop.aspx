<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchPop.aspx.cs" Inherits="Forms_SearchPop" %>

<%@ Register src="../Controls/Search.ascx" tagname="Search" tagprefix="uc1" %>

<%@ Register src="../Controls/CollegeFaculty.ascx" tagname="CollegeFaculty" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <script src="../Scripts/JScript.aspx.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.js" type="text/javascript"></script> 
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css" />  
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.js"></script>

    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

     <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
    <uc1:Search ID="Search1" runat="server" />
    
    </div>
    <uc2:CollegeFaculty ID="CollegeFaculty1" runat="server" />
    </form>
</body>
</html>
