<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExtPCLogin.aspx.cs" Inherits="_ExtPCLogin" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>Faculty Promotion System</title>
    <meta content="MSHTML 6.00.3790.1830" name="GENERATOR">
    <meta content="FrontPage.Editor.Document" name="ProgId">
    <meta http-equiv="Content-Language" content="ar-sa">
    <meta content="Microsoft FrontPage 5.0" name="GENERATOR">
    <meta content="FrontPage.Editor.Document" name="ProgId">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1256">
    <style type="text/css">A:link {
	COLOR: #666666; TEXT-DECORATION: underline
}
A:visited {
	COLOR: #666666; TEXT-DECORATION: underline
}
A:active {
	COLOR: #666666; TEXT-DECORATION: underline
}
A:hover {
	COLOR: #455886; TEXT-DECORATION: underline
}
.text {
	FONT-SIZE: 13px; COLOR: #777777; FONT-FAMILY: Georgia, Times New Roman, serif
}
.fmtext {
	FONT-SIZE: 12px; COLOR: #777777; FONT-FAMILY: Tahoma, Arial, Helvetica, sans-serif
}
.button {
	BORDER-RIGHT: #aad5ff 1px solid; PADDING-RIGHT: 3px; BORDER-TOP: #aad5ff 1px solid; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(images/button_bg.gif); PADDING-BOTTOM: 3px; BORDER-LEFT: #aad5ff 1px solid; CURSOR: hand; COLOR: #717171; PADDING-TOP: 3px; BORDER-BOTTOM: #aad5ff 1px solid; FONT-FAMILY: Tahoma, Arial, sans-serif; HEIGHT: 25px; BACKGROUND-COLOR: #ffffff
}
TEXTAREA {
	BORDER-RIGHT: #f0d8a9 1px solid; PADDING-RIGHT: 3px; BACKGROUND-POSITION: left top; BORDER-TOP: #f0d8a9 1px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(images/input_bg.gif); PADDING-BOTTOM: 3px; BORDER-LEFT: #f0d8a9 1px solid; COLOR: #777777; PADDING-TOP: 3px; BORDER-BOTTOM: #f0d8a9 1px solid; BACKGROUND-REPEAT: repeat-x; FONT-FAMILY: Tahoma, Arial, Helvetica, sans-serif; BACKGROUND-COLOR: #fbfbd1
}
SELECT {
	BORDER-RIGHT: #f0d8a9 1px solid; BORDER-TOP: #f0d8a9 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #f0d8a9 1px solid; COLOR: #777777; BORDER-BOTTOM: #f0d8a9 1px solid; FONT-FAMILY: Tahoma, Arial, Helvetica, sans-serif; BACKGROUND-COLOR: #fbfbd1
}
.MsgBox {
	BORDER-RIGHT: #ffbfbf 1px dotted; PADDING-RIGHT: 10px; BORDER-TOP: #ffbfbf 1px dotted; PADDING-LEFT: 10px; FONT-SIZE: 12pt; BACKGROUND: #f6f9ff; PADDING-BOTTOM: 10px; MARGIN: 5px 0px 5px 5px; BORDER-LEFT: #ffbfbf 1px dotted; WIDTH: 80%; COLOR: #660000; PADDING-TOP: 10px; BORDER-BOTTOM: #ffbfbf 1px dotted; FONT-FAMILY: Tahoma
}
</style>

    <script language="JavaScript">
<!--
function mmLoadMenus() {
 if (window.mm_menu_directions) return;


  window.mm_menu_directions = new Menu("root",500,18,"Georgia, Arial, Helvetica, sans-serif",16,"#002888","#000000","#eeeeee","#ffffff","left","middle",3,0,100,0,0,true,true,true,0,true,true);
  	  
  mm_menu_directions.addMenuItem("For the Applicant","window.open('default.htm', '_self');");
  mm_menu_directions.addMenuItem("For the Department Chairman","window.open('default.htm', '_self');");
  mm_menu_directions.addMenuItem("For the Dean of Faculty and Personnel Affairs","window.open('default.htm', '_self');");
  mm_menu_directions.addMenuItem("For the Department Promotion Committee","window.open('default.htm', '_self');");
  mm_menu_directions.addMenuItem("For the College Dean","window.open('default.htm', '_self');");
  
  mm_menu_directions.fontWeight="bold";
  mm_menu_directions.hideOnMouseOut=true;
  mm_menu_directions.menuBorder=1;
  mm_menu_directions.menuLiteBgColor='#ffffff';
  mm_menu_directions.menuBorderBgColor='#999999';
  mm_menu_directions.bgColor='#ffffff';

  mm_menu_directions.writeMenus();





} // mmLoadMenus()

//-->
    </script>

    <script language="JavaScript1.2" src="images/mm_menua.js"></script>

</head>
<body bgcolor="#f1f3f8" leftmargin="0" topmargin="0" style="font-size: 12pt">
    <form id="form1" runat="server">

        <script language="JavaScript1.2">mmLoadMenus();</script>

        <div align="center">
            <center>
                <table style="border-collapse: collapse" bordercolor="#000080" height="100%" cellpadding="0"
                    width="740" bgcolor="#f1f3f8" border="0">
                    <tbody>
                        <tr>
                            <td valign="bottom" width="16" background="images/bg_left.gif" height="517" rowspan="2">
                                <img height="16" alt="" src="images/bg_left.gif" width="17" border="0"></td>
                            <td valign="top" align="center" width="740" style="height: 59px">
                                <map name="FPMap0">
                                    <area shape="RECT" coords="4,140,139,157" href="http://www.kfupm.edu.sa/">
                                </map>
                                <img height="168" src="images/header.jpg" width="750" usemap="#FPMap0" border="0"></td>
                            <td valign="bottom" width="17" background="images/bg_right.gif" height="517" rowspan="2">
                                <img height="16" alt="" src="images/bg_right.gif" width="17" border="0"></td>
                        </tr>
                        <tr>
                            <td valign="top" align="left" height="398">
                                <div align="center">
                                    <table style="border-collapse: collapse" bordercolor="#ff00ff" height="100%" cellpadding="0"
                                        width="750" align="right" border="0">
                                        <tbody>
                                            <tr>
                                                <td valign="top" nowrap align="center" width="750" height="137">
                                                    <p>
                                                        <img height="118" src="images/imgbody.jpg" width="376" border="0">
                                                    <p>
                                                        <em>Login Page For External Promotion Committee Members</em></p>
                                                        <p>
                                                            <em>Please login using the username and password provided to you through email.</em></p>
                                                        <p>
                                                            <asp:Label ID="lblUserName" runat="server" Text="Username:"></asp:Label><asp:TextBox ID="txtUserName" runat="server" Height="22px" Width="155px"></asp:TextBox>&nbsp;&nbsp;<br />
                                                        <asp:RequiredFieldValidator
                                                            ID="rfvUserName" runat="server" ErrorMessage="* Username cannot be empty" Font-Size="X-Small" ControlToValidate="txtUserName"></asp:RequiredFieldValidator><br />
                                                        <asp:Label ID="lblPassword" runat="server" Text="Password:" Height="18px" Width="78px"></asp:Label><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Height="22px" Width="155px"></asp:TextBox>&nbsp;&nbsp;<br />
                                                        <asp:RequiredFieldValidator
                                                            ID="rfvPassword" runat="server" ErrorMessage="* Password cannot be empty" Font-Size="X-Small" ControlToValidate="txtPassword"></asp:RequiredFieldValidator><br />
                                                        <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" /><br />
                                                        </p><br />
                                                        <asp:Label ID="lblEx" runat="server" Visible="False" Font-Size="X-Small" ForeColor="Red"></asp:Label>
                                                        <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td dir="rtl" style="border-right: 0px dashed; border-top: 0px dashed; border-left: 0px dashed;
                                                    color: #ffffff; border-bottom: 0px dashed" bordercolor="#663300" align="center"
                                                    width="765" background="images/footerBG.jpg" bgcolor="#f1f3f8" height="40">
                                                    <p dir="rtl" style="margin-top: 0px; margin-bottom: 0px" align="center">
                                                        <b><font color="#336699"><font face="Tahoma" size="1">???? ?????? ??????&nbsp; 200<span
                                                            lang="en-us">6 </span></font><span lang="en"><font face="Times New Roman" size="1">?</font></span></font></b></p>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    </div>
                                    </table>
            </center>
        </div>
    </form>
</body>
</html>
