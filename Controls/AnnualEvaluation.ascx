<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AnnualEvaluation.ascx.cs" Inherits="Controls_AnnualEvaluation" %>
<h3>
    Annual Evaluations</h3>
<p>
    <asp:TextBox ID="txtEvaluations" runat="server" Font-Names="Georgia,Times" Height="150px"
        TextMode="MultiLine" onDrop="return false;" onKeyPress="return limitCharsLength(this,2000);"  onPaste="return limitClipBoard(this,2000);"  Width="100%"></asp:TextBox></p>