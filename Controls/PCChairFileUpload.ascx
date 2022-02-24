<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PCChairFileUpload.ascx.cs" Inherits="Controls_PCChairFileUpload" %>
<%@ Register Src="ViewPCChairReport.ascx" TagName="ViewPCChairReport" TagPrefix="uc6" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Panel ID="pnlUpload" runat="server" Width="100%" >    
    <uc6:ViewPCChairReport ID="ViewPCChairReport2" runat="server" />
    <br />
    <br />
    <fieldset style="width: 90%; background-color:#efefef; border:1px solid #007d40;">
    <br />
    You can upload .pdf, .doc or .docx file
        <br />
        <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
     
        <legend style="font-style:italic; font-weight:bold; background-color:#007d40; color:white">Upload a File:</legend>
        &nbsp;&nbsp; Select the file:<br />
        &nbsp;&nbsp;
        <asp:FileUpload ID="FileUploader" runat="server" />
        <asp:TextBox ID="txtDescription" runat="server" Width="230px" Height="53px"></asp:TextBox>
        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_txtDescription" runat="server" Enabled="True" TargetControlID="txtDescription"  WatermarkText="Describe the file contents here" WatermarkCssClass="watermarked"></asp:TextBoxWatermarkExtender>
        
        <br />
        
        <asp:Button ID="btnUpload" runat="server" Text="Upload" class="btn btn-primary btn-md" 
            OnClick="btnUpload_Click" Width="169px" Height="33px" />
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <br />
    </fieldset>
    <br />
</asp:Panel>
