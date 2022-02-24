<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AreaOfSpecialization.ascx.cs" Inherits="Controls_AreaOfSpecialization" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<h3>Area of Research</h3>
<asp:TextBox ID="tbArOfSp" runat="server" Width="485px"></asp:TextBox>
<ajaxtoolkit:TextBoxWatermarkExtender ID="tbArOfSp_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="tbArOfSp"
    WatermarkText="Enter the Area of Specialization" WatermarkCssClass="watermarked">
</ajaxtoolkit:TextBoxWatermarkExtender>
<h3>Phd from which University</h3>
<asp:TextBox ID="tbPhdFrom" runat="server" Width="485px"></asp:TextBox>
<ajaxtoolkit:TextBoxWatermarkExtender ID="tbPhdFrom_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="tbPhdFrom"
    WatermarkText="Enter only the University Name" WatermarkCssClass="watermarked">
</ajaxtoolkit:TextBoxWatermarkExtender>
