<%@ Page Language="C#" MasterPageFile="~/Forms/Forms.master" AutoEventWireup="true"
    Inherits="Forms_ChairmanInput" MaintainScrollPositionOnPostback="true" CodeFile="ChairmanInput.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ MasterType VirtualPath="Forms.master" %>
<%@ Register Src="../Controls/Instruction.ascx" TagName="Instruction" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.2;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="tbMeetingDate" />          --%>
        </Triggers>
        <ContentTemplate>
 <%--           <div id="divCollegeInput" runat="server"
                class='<%# (Master.CurRoleID == (byte) RoleID.DepartmentChairman
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority)
            && !Master.App.ApplicationClosed? "": "disabledbutton" %>'>--%>
                <div runat="server" id="divTopOfFormMsgFailure" class="alert alert-danger"
                    visible='<%#  !GetAppTaskFormCompleted() && Master.CurRoleID == (byte) RoleID.DepartmentChairman
             && !Master.App.ApplicationClosed%>'>
                    <span class="glyphicon glyphicon-thumbs-down" aria-hidden="true"></span>
                    <b>Incomplete: </b>
                    <asp:Label ID="lblMessageFailure" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                        Font-Italic="True"></asp:Label>
                </div>
                <div runat="server" id="divTopOfFormMsgSuccess" class="alert alert-success" role="alert"
                    visible='<%#  GetAppTaskFormCompleted() && Master.CurRoleID == (byte) RoleID.DepartmentChairman 
            && !Master.App.ApplicationClosed%>'>
                    <span class="glyphicon glyphicon-check" aria-hidden="true"></span>
                    <b>Complete:</b>
                    <asp:Label ID="lblMessageSuccess" Text='<%# GetAppTaskFormMessage() %>' runat="server" EnableViewState="False"
                        Font-Italic="True"></asp:Label>
                </div>

                <uc1:Instruction ID="Instruction1" runat="server" />
                <h3>
                    <%=Resources.Resource.Form_ChairmanInputF1%></h3>
                <%=Resources.Resource.Form_ChairmanInputF1SubHeading %>
                <asp:TextBox ID="tbF1" runat="server" Font-Names="Georgia,Times" Height="150px"
                    TextMode="MultiLine" Width="100%" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF2%></h3>
                <%=Resources.Resource.Form_ChairmanInputF2SubHeading %>
                <asp:TextBox ID="tbF2" runat="server" Font-Names="Georgia,Times" Height="150px"
                    TextMode="MultiLine" Width="100%" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF3%></h3>
                <%=Resources.Resource.Form_ChairmanInputF3SubHeading %>
                <asp:TextBox ID="tbF3" runat="server" Font-Names="Georgia,Times" Height="150px"
                    TextMode="MultiLine" Width="100%" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF4%></h3>
                <%=Resources.Resource.Form_ChairmanInputF4SubHeading %>
                <asp:Label ID="lblF4LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF4" runat="server" Font-Names="Georgia,Times" Height="150px"
                    TextMode="MultiLine" Width="100%" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF5%></h3>
                <%=Resources.Resource.Form_ChairmanInputF5SubHeading %>
                <asp:TextBox ID="tbF5" runat="server" Width="100%" AutoPostBack="true" Height="150px"
                    TextMode="MultiLine" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF6%></h3>
                <%=Resources.Resource.Form_ChairmanInputF6SubHeading %>
                <asp:Label ID="lblF6LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF6" runat="server" Width="100%" AutoPostBack="true" Height="150px"
                    TextMode="MultiLine" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF7%></h3>
                <%=Resources.Resource.Form_ChairmanInputF7SubHeading %>
                <asp:Label ID="lblF7LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF7" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF8%></h3>
                <%=Resources.Resource.Form_ChairmanInputF8SubHeading %>
                <asp:Label ID="lblF8LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF8" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF9%></h3>
                <%=Resources.Resource.Form_ChairmanInputF9SubHeading %>
                <asp:Label ID="lblF9LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF9" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF10%></h3>
                <%=Resources.Resource.Form_ChairmanInputF10SubHeading %>
                <asp:Label ID="lblF10LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF10" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF11%></h3>
                <%=Resources.Resource.Form_ChairmanInputF11SubHeading %>
                <asp:Label ID="lblF11LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF11" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF12%></h3>
                <%=Resources.Resource.Form_ChairmanInputF12SubHeading %>
                <asp:Label ID="lblF12LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF12" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF13%></h3>
                <%=Resources.Resource.Form_ChairmanInputF13SubHeading %>
                <asp:Label ID="lblF13LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF13" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF14%></h3>
                <%=Resources.Resource.Form_ChairmanInputF14SubHeading %>
                <asp:Label ID="lblF14LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF14" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF15%></h3>
                <%=Resources.Resource.Form_ChairmanInputF15SubHeading %>
                <asp:Label ID="lblF15LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF15" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF16%></h3>
                <%=Resources.Resource.Form_ChairmanInputF16SubHeading %>
                <asp:Label ID="lblF16LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF16" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF17%></h3>
                <%=Resources.Resource.Form_ChairmanInputF17SubHeading %>
                <asp:Label ID="lblF17LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF17" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF18%></h3>
                <%=Resources.Resource.Form_ChairmanInputF18SubHeading %>
                <asp:Label ID="lblF18LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF18" runat="server" Width="100%" Height="150px"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF19%></h3>
                <%=Resources.Resource.Form_ChairmanInputF19SubHeading %>
                <asp:Label ID="lblF19LabelSubHeading" runat="server"></asp:Label>
                <asp:TextBox ID="tbF19" runat="server" Height="150px" Width="100%"
                    TextMode="MultiLine" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
                <h3><%=Resources.Resource.Form_ChairmanInputF20%></h3>
                <%=Resources.Resource.Form_ChairmanInputF20SubHeading %>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:TextBox ID="tbMeetingNo" runat="server" AutoPostBack="true" OnTextChanged="tb_TextChanged"></asp:TextBox>
               <%-- </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <h3><%=Resources.Resource.Form_ChairmanInputF21%></h3>
    <%=Resources.Resource.Form_ChairmanInputF21SubHeading %>
    <asp:TextBox ID="tbMeetingDate" runat="server" TextMode="Date" AutoPostBack="true" OnTextChanged="tb_TextChanged" Visible='<%# (Master.CurRoleID == (byte) RoleID.DepartmentChairman
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority)
            && !Master.App.ApplicationClosed%>'></asp:TextBox>
    <asp:Label ID="lblMeetingDate" runat="server" Visible='<%# !((Master.CurRoleID == (byte) RoleID.DepartmentChairman
            ||Master.CurRoleID ==(byte) RoleID.TopAuthority)
            && !Master.App.ApplicationClosed)%>'></asp:Label>    
    <br />
    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
    <ajaxtoolkit:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
        TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
        BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
        RepositionMode="RepositionOnWindowScroll">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="background: #0088ce; padding: 10px">
        <fieldset style="margin: 5px; border: 2px solid #0088ce; height: 90%;">
            <legend style="font-style: italic; font-weight: bold; background-color: #0088ce; color: white">Message </legend>
            <br />
            <div style="justify-content: center">
                <asp:Label ForeColor="White" ID="labelProgrammaticPopup0" Font-Size="Large" runat="server"></asp:Label>
            </div>
            <div align="center">
                <br />
                <asp:LinkButton ID="hideModalPopupViaServer0" runat="server" OnClick="hideModalPopupViaServer0_Click" class="btn btn-default"
                    ToolTip="Press this button to send the email">OK <span class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
            </div>
        </fieldset>
    </asp:Panel>
    <br />
</asp:Content>
