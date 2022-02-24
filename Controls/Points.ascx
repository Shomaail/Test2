<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Points.ascx.cs" Inherits="Controls_Points" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<style type="text/css">

    .style2
    {
        height: 32px;
    }
   
</style>

<table class="PointsTable" cellpadding="0" cellspacing="0" border="1" bordercolor="#3062af" style="border-collapse:collapse" id="Table1" runat="server">


<tr align="center" class="PointsHeader">
    <td colspan="3" rowspan="4" class="PointsWhiteBorder">AUTHORSHIP<br />
        CLASSIFICATION</td>
    <td rowspan="4" class="PointsWhiteBorder">Unit per Publication Work<br />(f)</td>
    <td rowspan="1" colspan="18" class="PointsWhiteBorder">TYPES OF SCHOLARLY WORK</td>
</tr>

<tr align="center" class="PointsHeader">
    <td colspan="2" class="PointsLeftHeader">
        <span style="font-size: 10pt">Journal Publications</span></td>
    <td colspan="2" class="PointsLeftHeader">
        <span style="font-size: 10pt">Conference / Symposium Publications</span></td>
    <td colspan="2" class="PointsLeftHeader">
        <span style="font-size: 10pt">Refreed Technical Reports</span></td>
    <td colspan="2" class="PointsLeftHeader">
        <span style="font-size: 10pt">Refreed Text / Reference Books</span></td>
    <td colspan="2" class="PointsLeftHeader">
        <span style="font-size: 10pt">Refreed Authentication Reviews of Rare Books</span></td>
    <td colspan="2" class="PointsLeftHeader">
        <span style="font-size: 10pt">Refreed Translation of Specialized Scientific Books</span></td>
    <td colspan="2" class="PointsLeftHeader">
        <span style="font-size: 10pt">Refreed Books and Reports Published by Scientific Societies</span></td>
    <td colspan="2" class="PointsLeftHeader">
        <span style="font-size: 10pt">Inventions and Novelties with Patent from Recognized Agencied</span></td>
    <td colspan="2" class="PointsLeftHeader">
        <span style="font-size: 10pt">Distinguished Creative Activities</span></td>
</tr>
<tr align="center" class="PointsHeader">
    <td colspan="2" class="PointsHeaderSecondRow">
        <span style="font-size: 10pt">(a)</span></td>
    <td colspan="2" class="PointsHeaderSecondRow">
        <span style="font-size: 10pt">(b)</span></td>
    <td colspan="2" class="PointsHeaderSecondRow">
        <span style="font-size: 10pt">(c)</span></td>
    <td colspan="2" class="PointsHeaderSecondRow">
        <span style="font-size: 10pt">(d)</span></td>
    <td colspan="2" class="PointsHeaderSecondRow">
        <span style="font-size: 10pt">(e)</span></td>
    <td colspan="2" class="PointsHeaderSecondRow">
        <span style="font-size: 10pt">(g)</span></td>
    <td colspan="2" class="PointsHeaderSecondRow">
        <span style="font-size: 10pt">(h)</span></td>
    <td colspan="2" class="PointsHeaderSecondRow">
        <span style="font-size: 10pt">(i)</span></td>
    <td colspan="2" class="PointsHeaderSecondRow">
        <span style="font-size: 10pt">(j)</span></td>
</tr>

<tr align="center" class="PointsHeader">
    <td class="PointsWhiteBorder" style="font-size:10pt;">n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">Units<br />f*n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">Units<br />f*n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">Units<br />f*n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">Units<br />f*n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">Units<br />f*n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">Units<br />f*n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">Units<br />f*n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">Units<br />f*n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">n</td>
    <td class="PointsWhiteBorder" style="font-size:10pt;">Units<br />f*n</td>
</tr>


<tr align="center">
    <td rowspan="2" class="PointsHeader" style="border-right:none; padding: 1px 3px">1)</td>
    <td class="PointsHeader" style="border-left:none; padding: 1px 2px" rowspan="2" nowrap="noWrap" align="left">Single&nbsp;Author</td>
    <td class="PointsHeader">
        <span style="font-size: 8pt; padding: 1px 3px">Total&nbsp;Units</span></td>
    <td class="PointsHeader">1</td>
    <td class="style2"><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR1Ca" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color: #efefef;height: 32px;"><asp:TextBox runat="server" ID="txtR1CaU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td class="style2"><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR1Cb" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color: #efefef;height: 32px;"><asp:TextBox runat="server" ID="txtR1CbU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td class="style2"><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR1Cc" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color: #efefef;height: 32px;"><asp:TextBox runat="server" ID="txtR1CcU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td class="style2"><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR1Cd" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color: #efefef;height: 32px;"><asp:TextBox runat="server" ID="txtR1CdU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td class="style2"><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR1Ce" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color: #efefef;height: 32px;"><asp:TextBox runat="server" ID="txtR1CeU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td class="style2"><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR1Cf" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td  style="background-color: #efefef;height: 32px;"><asp:TextBox runat="server" ID="txtR1CfU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td class="style2"><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR1Cg" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td  style="background-color: #efefef;height: 32px;"><asp:TextBox runat="server" ID="txtR1CgU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td class="style2"><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR1Ch" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td  style="background-color: #efefef;height: 32px;"><asp:TextBox runat="server" ID="txtR1ChU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td class="style2"><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR1Ci" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td  style="background-color: #efefef;height: 32px;"><asp:TextBox runat="server" ID="txtR1CiU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td style="padding: 2px 3px; border:0px" nowrap="noWrap" class="style2">
        <span style="font-size: 8pt"><strong>Single Authorship<br />Units Counted</strong></span></td>
</tr>


<tr align="center">
    <td class="PointsHeader">
        <span style="font-size: 8pt">Units Counted</span></td>
    <td class="PointsHeader">1</td>
    <td colspan="2"  style="background-color: #efefef;"><asp:TextBox runat="server" ID="R1CaCounted" BorderStyle="none" Width="40px" BackColor="#efefef" ReadOnly="True" style="text-align:center" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2"  style="background-color: #efefef;"><asp:TextBox runat="server" ID="R1CbCounted" BorderStyle="none" Width="40px" BackColor="#efefef" ReadOnly="True" style="text-align:center" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2"  style="background-color: #efefef;"><asp:TextBox runat="server" ID="R1CcCounted" BorderStyle="none" Width="40px" BackColor="#efefef" ReadOnly="True" style="text-align:center" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2"  style="background-color: #efefef;"><asp:TextBox runat="server" ID="R1CdCounted" BorderStyle="none" Width="40px" BackColor="#efefef" ReadOnly="True" style="text-align:center" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2"  style="background-color: #efefef;"><asp:TextBox runat="server" ID="R1CeCounted" BorderStyle="none" Width="40px" BackColor="#efefef" ReadOnly="True" style="text-align:center" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2"  style="background-color: #efefef;"><asp:TextBox runat="server" ID="R1CfCounted" BorderStyle="none" Width="40px" BackColor="#efefef" ReadOnly="True" style="text-align:center" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2"  style="background-color: #efefef;"><asp:TextBox runat="server" ID="R1CgCounted" BorderStyle="none" Width="40px" BackColor="#efefef" ReadOnly="True" style="text-align:center" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2"  style="background-color: #efefef;"><asp:TextBox runat="server" ID="R1ChCounted" BorderStyle="none" Width="40px" BackColor="#efefef" ReadOnly="True" style="text-align:center" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2"  style="background-color: #efefef;"><asp:TextBox runat="server" ID="R1CiCounted" BorderStyle="none" Width="40px" BackColor="#efefef" ReadOnly="True" style="text-align:center" Font-Bold="True"></asp:TextBox></td>
    <td style="padding: 2px 1px; border:0px"><asp:TextBox runat="server" ID="txtAuthoshipTotal" Width="40px" BackColor="#efefef" style="text-align:center" Font-Bold="True" BorderColor="black"></asp:TextBox></td>
</tr>



<tr align="center">
    <td class="PointsHeader" style="border-right:none; padding: 1px 3px">2)</td>
    <td class="PointsHeader" style="border-left:none; padding:2px" nowrap="noWrap" colspan="2" align="left">
        <span style="font-size: 10pt">Main or Co-Author in<br />two authors work</span></td>
    <td class="PointsHeader">0.5</td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR2Ca" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR2CaU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR2Cb" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR2CbU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR2Cc" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef;"><asp:TextBox runat="server" ID="txtR2CcU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR2Cd" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR2CdU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR2Ce" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR2CeU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR2Cf" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR2CfU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR2Cg" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR2CgU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR2Ch" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR2ChU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR2Ci" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR2CiU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
</tr>




<tr align="center">
    <td class="PointsHeader" style="border-right:none; padding: 1px 3px">3)</td>
    <td class="PointsHeader" style="border-left:none; padding:2px" nowrap="noWrap" colspan="2" align="left">
        <span style="font-size: 10pt">Main Author* in more than<br /> two authors Pub. or Tech. Reports</span></td>
    <td class="PointsHeader">0.5</td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR3Ca" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR3CaU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR3Cb" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR3CbU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR3Cc" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef;"><asp:TextBox runat="server" ID="txtR3CcU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR3Cd" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR3CdU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR3Ce" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR3CeU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR3Cf" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR3CfU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR3Cg" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR3CgU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR3Ch" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR3ChU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR3Ci" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR3CiU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
</tr>




<tr align="center">
    <td class="PointsHeader" style="border-right:none; padding: 1px 3px">4)</td>
    <td class="PointsHeader" style="border-left:none; padding:2px" nowrap="noWrap" colspan="2" align="left">
        <span style="font-size: 10pt">Co-author in more than two authors<br />
            Pub. or Tech. Reports</span></td>
    <td class="PointsHeader">
        0.25</td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR4Ca" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR4CaU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR4Cb" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR4CbU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR4Cc" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR4CcU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td colspan="12">
        &nbsp;</td>
</tr>




<tr align="center">
    <td class="PointsHeader" style="border-right:none; padding: 1px 3px">5)</td>
    <td class="PointsHeader" style="border-left:none; padding:2px" nowrap="noWrap" colspan="2" align="left">
        <span style="font-size: 10pt">Main or Co-author* in more than<br />two authors
            other scholarly
            work</span></td>
    <td class="PointsHeader">
        0.25</td>
    <td colspan="6">
        &nbsp;</td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR5Cd" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR5CdU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR5Ce" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR5CeU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR5Cf" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR5CfU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR5Cg" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR5CgU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR5Ch" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR5ChU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td><ew:numericbox runat="server" PositiveNumber="true" RealNumber="false" ID="txtR5Ci" BorderWidth="0" Width="25px" style="text-align:center"></ew:numericbox></td>
    <td style="background-color:#efefef"><asp:TextBox runat="server" ID="txtR5CiU" BorderStyle="none" Width="30px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
</tr>




<tr align="center">
    <td colspan="4">
        <span style="font-size: 8pt">Total Units</span></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtTotalCa" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtTotalCb" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtTotalCc" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtTotalCd" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtTotalCe" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtTotalCf" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtTotalCg" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtTotalCh" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtTotalCi" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true"></asp:TextBox></td>
</tr>




<tr align="center">
    <td colspan="4">
        <span style="font-size: 8pt">Maximum Units Counted</span></td>
    <td colspan="2" style="background-color:#efefef">
        &nbsp;<asp:TextBox ID="TextBox1" runat="server" BackColor="#efefef" BorderStyle="none"
            Width="40px" ReadOnly="true" Style="text-align: center">-</asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="TextBox2" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true">1.00</asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="TextBox3" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true">1.00</asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="TextBox4" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true">1.00</asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="TextBox5" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true">1.00</asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="TextBox6" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true">1.00</asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="TextBox7" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true">1.00</asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="TextBox8" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true">-</asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="TextBox9" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true">1.00</asp:TextBox></td>
    <td style="padding: 2px 3px; border:0px" nowrap="noWrap">
        <span style="font-size: 8pt"><strong>Total<br />Units Counted</strong></span></td>
</tr>




<tr align="center">
    <td colspan="4">
        <span style="font-size: 10pt"><strong>Units Counted</strong></span></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtCountedCa" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtCountedCb" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtCountedCc" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtCountedCd" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtCountedCe" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtCountedCf" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtCountedCg" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtCountedCh" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true" Font-Bold="True"></asp:TextBox></td>
    <td colspan="2" style="background-color:#efefef"><asp:TextBox runat="server" ID="txtCountedCi" BorderStyle="none" Width="40px" BackColor="#efefef" style="text-align:center" ReadOnly="true" Font-Bold="True"></asp:TextBox></td>
    <td style="padding: 2px 1px; border:0px"><asp:TextBox runat="server" ID="txtCountedTotal" Width="40px" BackColor="#efefef" style="text-align:center" Font-Bold="True" BorderColor="black"></asp:TextBox></td>
    
</tr>
</table>

