<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Confirm.ascx.cs" Inherits="Controls_Independent_Controls_Confirm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>                 
     <style type="text/css">
            .modalBackground
            {
                background-color: Gray;
                filter: alpha(opacity=60);
            }
            
            .modalPopup
            {
                border: 3px solid Gray;
                background-color: #99ccff;
                padding: 5px;
            }
        </style>

                 <asp:Button ID="btnDummy" runat="server" Text="Dummy" 
                         Style="display: none;"/>
                    <asp:ConfirmButtonExtender ID="btnDummy_ConfirmButtonExtender" 
                        runat="server" ConfirmText="" 
                        Enabled="True" 
                        TargetControlID="btnDummy" 
                        OnClientCancel="cancelClick"                       
                        DisplayModalPopupID="ModalPopupExtenderConfirm">
                    </asp:ConfirmButtonExtender>
                <asp:ModalPopupExtender ID="ModalPopupExtenderConfirm" 
                    runat="server" 
                    TargetControlID="btnDummy"
                    PopupControlID="PNL" 
                    OkControlID="ButtonOk" 
                    CancelControlID="ButtonCancel" 
                    BackgroundCssClass="modalBackground" />
                <asp:Panel ID="PNL" 
                    runat="server" 
                    Style="display: none; width: 200px; background-color: White;
                    border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                    <asp:Label ID="LabelMessageConfirm" runat="server"></asp:Label>
                    <br />
                    <br />
                    <div style="text-align: right;">
                        <asp:Button ID="ButtonOk" runat="server" Text="OK" onclick="ButtonOk_Click" />
                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" 
                            onclick="ButtonCancel_Click" />
                    </div>
                </asp:Panel>