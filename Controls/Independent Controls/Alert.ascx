<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Alert.ascx.cs" Inherits="Controls_Alert" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%--Author: Syed Shomaail Mansoor Jafri 
This is a re-useable ASP.net control 
Inorder to use this control, all you have to do is to 
1. put Alert.ascx and Alert.js in one directory 
2. put the following two stlyseet classes in the default Stylesheet of the website 
            .alertModalBackground
            {
                background-color: Gray;
                 -ms-filter:"progid:DXImageTransform.Microsoft.Alpha(opacity=80)"; /*IE8*/
                filter: alpha(opacity=80);  /*internet explorer */
                opacity: 0.8;           /* fx, safari, opera, chrome */               
            }           
            .alertModalPopup
            {
                border: 3px solid Gray;
                background-color: #99ccff;
                padding: 5px;
            }
3. Refer the control in the aspx file or in web.config file 

4. Call the method Message from the code behind aspx file as follows:
            Alert1.Messages("This is an alert!"); 
Enjoy !
--%>

 <asp:Panel ID="pnlModal" runat="server" CssClass="alertModalPopup" >
    <div align="center">
 <br />
                <asp:Label ID="LabelMessage" runat="server"></asp:Label>    
                <br /><br />
            
         <asp:Button ID="btnClose" runat="server" Text="OK" />
         </div>
</asp:Panel>
           <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" TargetControlID="HiddenButton"
                runat="server" DynamicServicePath="" Enabled="True" BackgroundCssClass="alertModalBackground"
                PopupControlID="pnlModal" CancelControlID="btnClose" DropShadow="true">
            </asp:ModalPopupExtender>
  
            <asp:Button ID="HiddenButton" runat="server" style="display:none"/>

         
