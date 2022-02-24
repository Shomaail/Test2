<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Forms_test" %>

<%@ Register Src="../Controls/Search.ascx" TagName="Search" TagPrefix="uc1" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
<script type="text/javascript" language="javascript">window.name="sdf gasdfgDefaultPage"</script>
</head>
<body>



    <form id="form1" runat="server">
    <div>
        <uc1:Search ID="Search1" runat="server" />
    
    </div>
    </form>
</body>
</html>
