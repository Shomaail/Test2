<%@ Page Title="" Language="C#" MasterPageFile="~/ExtForms/ExtForm.master" AutoEventWireup="true" CodeFile="Form_Evaluation.aspx.cs" Inherits="ExtForms_Form_Evaluation" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/ExtForms/ExtForm.master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <script>
    $(document).ready(function()
    {
      setTimeout(DoPostBack, 600000); // call DoPostBack in 10 minutes
    })
    function DoPostBack()
    {
        //var buttonClientId = '<%= btnSaveHidden.ClientID %>';
        //__doPostBack(buttonClientId, ''); // simulate a button click
        __doPostBack('ctl00$MainContent$btnSaveHidden', ''); // simulate a button click        
        //PageMethods.TestWebMethod();
    }
  </script>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

    <%--    <div>
      this?: <asp:Label ID="Label1" Text="<%$Resources:Resource, Name %>" runat="server" Font-Bold="true" />
      
    </div>--%>

    <div runat="server" id="divFormTitle1" >
        <label runat="server" id="lblFormTitle1" />
    </div>
    <%--<div runat="server" id="divMsg" class="alert alert-warning" role="alert">
        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
        <b>NOTE: </b>
        IMPORTANT: Please make sure to save after each answer to avoid data loss after 15 minutes of inactivity             
    </div>--%>
    <div style="padding-left: 10px">
        <div runat="server" id="divQ1" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ1Title" />
            </h3>
            <div runat="server" id="divQ1Text" style="padding: 10px 10px; width: 300px">
                <label runat="server" id="lblQ1Text" />
            </div>

            <div runat="server" id="divQ1Answer" style="float: left">
                <div runat="server" id="divQ1ARB">


                    <label runat="server" id="lblQ1AnswerRB" />

                    <div id="divQ1AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ1AnswerCB">


                    <label runat="server" id="lblQ1AnswerCB" />

                    <div id="divQ1AnswerCBL" runat="server" />


                </div>
                <div runat="server" id="divQ1AnswerDDL">


                    <label runat="server" id="lblQ1AnswerDDL" />


                    <select id="SelectQ1Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ1AnswerTA">

                    <label runat="server" id="lblQ1AnswerTA" />


                    <textarea runat="server" id="Q1AnswerTA" type="text" style="display: block" />


                </div>
                <label id="lblQ1AnswerInstruction" runat="server" style="color: crimson" />
            </div>
        </div>
        <div runat="server" id="divQ2" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ2Title" />
            </h3>
            <div runat="server" id="divQ2Text" style="padding: 10px 10px; width: 300px">
                <label runat="server" id="lblQ2Text" />
            </div>
            <div runat="server" id="divQ2Answer" style="float: left">
                <div runat="server" id="divQ2ARB">

                    <label runat="server" id="lblQ2AnswerRB" />

                    <div id="divQ2AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ2AnswerCB">


                    <label runat="server" id="lblQ2AnswerCB" />

                    <div id="divQ2AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ2AnswerDDL">


                    <label runat="server" id="lblQ2AnswerDDL" />

                    <select id="SelectQ2Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ2AnswerTA">


                    <label runat="server" id="lblQ2AnswerTA" />

                    <textarea runat="server" id="Q2AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ2AnswerInstruction" runat="server" style="color: crimson" />
            </div>
        </div>
        <div runat="server" id="divQ3" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ3Title" />
            </h3>
            <div runat="server" id="divQ3Text" style="padding: 10px 10px; width: 300px">
                <label runat="server" id="lblQ3Text" />
            </div>
            <div runat="server" id="divQ3Answer" style="float: left">
                <div runat="server" id="divQ3ARB">


                    <label runat="server" id="lblQ3AnswerRB" />

                    <div id="divQ3AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ3AnswerCB">


                    <label runat="server" id="lblQ3AnswerCB" />

                    <div id="divQ3AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ3AnswerDDL">


                    <label runat="server" id="lblQ3AnswerDDL" />

                    <select id="SelectQ3Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ3AnswerTA">


                    <label runat="server" id="lblQ3AnswerTA" />

                    <textarea runat="server" id="Q3AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ3AnswerInstruction" runat="server" style="color: crimson" />
            </div>
        </div>
        <div runat="server" id="divQ4" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ4Title" />
            </h3>
            <div runat="server" id="divQ4Text" style="padding: 10px 10px; width: 300px">
                <label runat="server" id="lblQ4Text" />
            </div>
            <div runat="server" id="divQ4Answer" style="float: left;">
                <div runat="server" id="divQ4ARB">


                    <label runat="server" id="lblQ4AnswerRB" />

                    <div id="divQ4AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ4AnswerCB">


                    <label runat="server" id="lblQ4AnswerCB" />

                    <div id="divQ4AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ4AnswerDDL">


                    <label runat="server" id="lblQ4AnswerDDL" />

                    <select id="SelectQ4Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ4AnswerTA">


                    <label runat="server" id="lblQ4AnswerTA" />

                    <textarea runat="server" id="Q4AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ4AnswerInstruction" runat="server" style="color: crimson" />
            </div>
        </div>


        <div runat="server" id="divQ5" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ5Title" />
            </h3>
            <div runat="server" id="divQ5Text" style="padding: 10px 10px;">
                <label runat="server" id="lblQ5Text" />
            </div>
            <div runat="server" id="divQ5Answer">
                <div runat="server" id="divQ5ARB">


                    <label runat="server" id="lblQ5AnswerRB" />

                    <div id="divQ5AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ5AnswerCB">


                    <label runat="server" id="lblQ5AnswerCB" />

                    <div id="divQ5AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ5AnswerDDL">


                    <label runat="server" id="lblQ5AnswerDDL" />

                    <select id="SelectQ5Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ5AnswerTA">


                    <label runat="server" id="lblQ5AnswerTA" />

                    <textarea runat="server" id="Q5AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ5AnswerInstruction" runat="server" style="color: crimson" />
            </div>
        </div>
        <div runat="server" id="divQ6" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ6Title" />
            </h3>
            <div runat="server" id="divQ6Text" style="padding: 10px 10px;">
                <label runat="server" id="lblQ6Text" />
            </div>
            <div runat="server" id="divQ6Answer">
                <div runat="server" id="divQ6ARB">


                    <label runat="server" id="lblQ6AnswerRB" />

                    <div id="divQ6AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ6AnswerCB">


                    <label runat="server" id="lblQ6AnswerCB" />

                    <div id="divQ6AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ6AnswerDDL">


                    <label runat="server" id="lblQ6AnswerDDL" />

                    <select id="SelectQ6Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ6AnswerTA">


                    <label runat="server" id="lblQ6AnswerTA" />

                    <textarea runat="server" id="Q6AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ6AnswerInstruction" runat="server" style="color: crimson" />
            </div>

        </div>
        <div runat="server" id="divQ7" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ7Title" />
            </h3>
            <div runat="server" id="divQ7Text" style="padding: 10px 10px;">
                <label runat="server" id="lblQ7Text" />
            </div>
            <div runat="server" id="divQ7Answer">
                <div runat="server" id="divQ7ARB">


                    <label runat="server" id="lblQ7AnswerRB" />

                    <div id="divQ7AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ7AnswerCB">


                    <label runat="server" id="lblQ7AnswerCB" />

                    <div id="divQ7AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ7AnswerDDL">


                    <label runat="server" id="lblQ7AnswerDDL" />

                    <select id="SelectQ7Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ7AnswerTA">


                    <label runat="server" id="lblQ7AnswerTA" />

                    <textarea runat="server" id="Q7AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ7AnswerInstruction" runat="server" style="color: crimson" />
            </div>

        </div>
        <div runat="server" id="divQ8" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ8Title" />
            </h3>
            <div runat="server" id="divQ8Text" style="padding: 10px 10px;">
                <label runat="server" id="lblQ8Text" />
            </div>
            <div runat="server" id="divQ8Answer">
                <div runat="server" id="divQ8ARB">


                    <label runat="server" id="lblQ8AnswerRB" />

                    <div id="divQ8AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ8AnswerCB">


                    <label runat="server" id="lblQ8AnswerCB" />

                    <div id="divQ8AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ8AnswerDDL">


                    <label runat="server" id="lblQ8AnswerDDL" />

                    <select id="SelectQ8Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ8AnswerTA">


                    <label runat="server" id="lblQ8AnswerTA" />

                    <textarea runat="server" id="Q8AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ8AnswerInstruction" runat="server" style="color: crimson" />

            </div>

        </div>
        <div runat="server" id="divQ9" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ9Title" />
            </h3>
            <div runat="server" id="divQ9Text" style="padding: 10px 10px;">
                <label runat="server" id="lblQ9Text" />
            </div>
            <div runat="server" id="divQ9Answer">
                <div runat="server" id="divQ9ARB">


                    <label runat="server" id="lblQ9AnswerRB" />

                    <div id="divQ9AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ9AnswerCB">


                    <label runat="server" id="lblQ9AnswerCB" />

                    <div id="divQ9AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ9AnswerDDL">

                    <label runat="server" id="lblQ9AnswerDDL" />

                    <select id="SelectQ9Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ9AnswerTA">

                    <label runat="server" id="lblQ9AnswerTA" />

                    <textarea runat="server" id="Q9AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ9AnswerInstruction" runat="server" style="color: crimson" />
            </div>

        </div>
        <div runat="server" id="divQ10" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ10Title" />
            </h3>
            <div runat="server" id="divQ10Text" style="padding: 10px 10px;">
                <label runat="server" id="lblQ10Text" />
            </div>
            <div runat="server" id="divQ10Answer">
                <div runat="server" id="divQ10ARB">


                    <label runat="server" id="lblQ10AnswerRB" />

                    <div id="divQ10AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ10AnswerCB">


                    <label runat="server" id="lblQ10AnswerCB" />

                    <div id="divQ10AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ10AnswerDDL">


                    <label runat="server" id="lblQ10AnswerDDL" />

                    <select id="SelectQ10Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ10AnswerTA">


                    <label runat="server" id="lblQ10AnswerTA" />

                    <textarea runat="server" id="Q10AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ10AnswerInstruction" runat="server" style="color: crimson" />
            </div>
        </div>
        <div runat="server" id="divQ11" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ11Title" />
            </h3>
            <div runat="server" id="divQ11Text" style="padding: 10px 10px;">
                <label runat="server" id="lblQ11Text" />
            </div>
            <div runat="server" id="divQ11Answer">
                <div runat="server" id="divQ11ARB">


                    <label runat="server" id="lblQ11AnswerRB" />

                    <div id="divQ11AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ11AnswerCB">


                    <label runat="server" id="lblQ11AnswerCB" />

                    <div id="divQ11AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ11AnswerDDL">


                    <label runat="server" id="lblQ11AnswerDDL" />

                    <select id="SelectQ11Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ11AnswerTA">


                    <label runat="server" id="lblQ11AnswerTA" />

                    <textarea runat="server" id="Q11AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ11AnswerInstruction" runat="server" style="color: crimson" />
            </div>
        </div>

        <div runat="server" id="divQ12" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ12Title" />
            </h3>
            <div runat="server" id="divQ12Text" style="padding: 10px 10px;">
                <label runat="server" id="lblQ12Text" />
            </div>
            <div runat="server" id="divQ12Answer">
                <div runat="server" id="divQ12ARB">


                    <label runat="server" id="lblQ12AnswerRB" />

                    <div id="divQ12AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ12AnswerCB">


                    <label runat="server" id="lblQ12AnswerCB" />

                    <div id="divQ12AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ12AnswerDDL">


                    <label runat="server" id="lblQ12AnswerDDL" />

                    <select id="SelectQ12Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ12AnswerTA">


                    <label runat="server" id="lblQ12AnswerTA" />

                    <textarea runat="server" id="Q12AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ12AnswerInstruction" runat="server" style="color: crimson" />
            </div>
        </div>

        <div runat="server" id="divQ13" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ13Title" />
            </h3>
            <div runat="server" id="divQ13Text" style="padding: 10px 10px;">
                <label runat="server" id="lblQ13Text" />
            </div>
            <div runat="server" id="divQ13Answer">
                <div runat="server" id="divQ13ARB">


                    <label runat="server" id="lblQ13AnswerRB" />

                    <div id="divQ13AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ13AnswerCB">


                    <label runat="server" id="lblQ13AnswerCB" />

                    <div id="divQ13AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ13AnswerDDL">


                    <label runat="server" id="lblQ13AnswerDDL" />

                    <select id="SelectQ13Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ13AnswerTA">


                    <label runat="server" id="lblQ13AnswerTA" />

                    <textarea runat="server" id="Q13AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ13AnswerInstruction" runat="server" style="color: crimson" />
            </div>
        </div>

        <div runat="server" id="divQ14" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ14Title" />
            </h3>
            <div runat="server" id="divQ14Text" style="padding: 10px 10px;">
                <label runat="server" id="lblQ14Text" />
            </div>
            <div runat="server" id="divQ14Answer">
                <div runat="server" id="divQ14ARB">


                    <label runat="server" id="lblQ14AnswerRB" />

                    <div id="divQ14AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ14AnswerCB">


                    <label runat="server" id="lblQ14AnswerCB" />

                    <div id="divQ14AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ14AnswerDDL">


                    <label runat="server" id="lblQ14AnswerDDL" />

                    <select id="SelectQ14Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ14AnswerTA">


                    <label runat="server" id="lblQ14AnswerTA" />

                    <textarea runat="server" id="Q14AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ14AnswerInstruction" runat="server" style="color: crimson" />
            </div>
        </div>

        <div runat="server" id="divQ15" visible="false" style="float: left; width: 100%">
            <h3>
                <label runat="server" id="lblQ15Title" />
            </h3>
            <div runat="server" id="divQ15Text" style="padding: 10px 10px;">
                <label runat="server" id="lblQ15Text" />
            </div>
            <div runat="server" id="divQ15Answer">
                <div runat="server" id="divQ15ARB">


                    <label runat="server" id="lblQ15AnswerRB" />

                    <div id="divQ15AnswerRBL" runat="server" />

                </div>
                <div runat="server" id="divQ15AnswerCB">


                    <label runat="server" id="lblQ15AnswerCB" />

                    <div id="divQ15AnswerCBL" runat="server" />

                </div>
                <div runat="server" id="divQ15AnswerDDL">


                    <label runat="server" id="lblQ15AnswerDDL" />

                    <select id="SelectQ15Answer" runat="server">
                    </select>

                </div>
                <div runat="server" id="divQ15AnswerTA">


                    <label runat="server" id="lblQ15AnswerTA" />

                    <textarea runat="server" id="Q15AnswerTA" type="text" style="display: block" />

                </div>
                <label id="lblQ15AnswerInstruction" runat="server" style="color: crimson" />
            </div>
        </div>

        <div id="divEvaluationAttachment" runat="server" style="float: left; width: inherit; padding-top: 10px; padding-bottom: 10px; width: 100%">
            <table class="table-condensed table-bordered">
                <tr>
                    <td><span class="glyphicon glyphicon-paperclip">Attachment</span> </td>
                    <td>
                        <asp:LinkButton ID="lbtnEvaluationAttachment" runat="server" OnClick="lbtnEvaluationAttachment_Click"></asp:LinkButton></td>
                    <td>
                        <asp:Button runat="server" ID="btnDelEvaluationAttachment"
                            OnClick="btnDelEvaluationAttachment_Click" Text="Delete" CssClass="btn"></asp:Button></td>
                </tr>
                <tr>
                    <td>
                        <input type="file" accept=".pdf" id="EvaluationAttachment" runat="server" /></td>
                </tr>
            </table>
        </div>
        <div runat="server" id="divMsg" class="alert alert-warning" role="alert">
                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                        <b>NOTE: </b>
                        Please make sure  you fill in <a href="../ExtForms/ExtMain.aspx?MyDetails=1">Details for processing Honrarium</a>. 
                    </div>
               
        <div runat="server" id="divSubmit" style="float: left; width: inherit; padding-top: 10px; padding-bottom: 10px;">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn" UseSubmitBehavior="False" />
            <asp:Button ID="btnSaveHidden" Visible="false" runat="server" Text="Save" OnClick="btnSaveHidden_Click" CssClass="btn" UseSubmitBehavior="False" />
            <asp:Button Text="Submit" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn" />

        </div>
    </div>
    <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
    <asp:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
        TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
        BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
        RepositionMode="RepositionOnWindowScroll">
    </asp:ModalPopupExtender>
    <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="background: #0088ce; padding: 10px">
        <fieldset style="margin: 5px; border: 2px solid #0088ce; height: 90%;">
            <legend style="font-style: italic; font-weight: bold; background-color: #0088ce; color: white">Message </legend>
            <br />
            <asp:Label ForeColor="White" ID="labelProgrammaticPopup0" Font-Size="Large" runat="server"></asp:Label>
            <div align="center">
                <br />
                <asp:LinkButton ID="hideModalPopupViaServer0" runat="server" OnClick="hideModalPopupViaServer0_Click" class="btn btn-default"
                    ToolTip="Press this button to send the email">OK <span class="glyphicon glyphicon-ok" aria-hidden="true"></span></asp:LinkButton>
            </div>
        </fieldset>
    </asp:Panel>
</asp:Content>

