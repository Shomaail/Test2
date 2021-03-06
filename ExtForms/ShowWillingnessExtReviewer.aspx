<%@ Page Language="C#" MasterPageFile="~/Forms/WillingnessLetter.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    ValidateRequest="false" Inherits="Forms_ShowWillingessExtReviewer"  CodeFile="ShowWillingnessExtReviewer.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="LabelWebageUnavailable" runat="server" Visible="False" 
        ForeColor="Red"></asp:Label>
                <asp:Label ID="lblWillingnessLetterContent" runat="server"></asp:Label>
  

      			<hr >

				<div class="form-check">
				 <asp:RadioButton ID="RadioButtonAccept" runat="server" Checked="True" GroupName="Williingness"
                Text="Accept" AutoPostBack="true" OnCheckedChanged="RadioButtonAccept_CheckedChanged" /><br />
                 <br />
                <asp:RadioButton ID="RadioButtonReject" runat="server" GroupName="Williingness" Height="20px"
                Text="Decline" Width="89px" AutoPostBack="true" OnCheckedChanged="RadioButtonReject_CheckedChanged" />
				</div>
				
				<hr >
				<div id="divUserInfo" runat="server" class="well well-lg">

					
					  <div class="form-group row">
					    <label for="inputMyname" class="col-sm-3 col-form-label">My name <br><small>Write full name as per passport.</small></label>
					    <div class="col-sm-9">
					      <input type="text" runat="server" class="form-control" id="tbName" placeholder="Full name" maxlength="100" />
					    </div>
					  </div>
                    

					  <div class="form-group row">
					    <label for="inputInstitute" class="col-sm-3 col-form-label">University / Institution name and mailing address <br><small>Complete address postal / zip code.</small></label>
					    <div class="col-sm-9">
					    	<textarea runat="server" class="form-control" id="tbMailingAddress" placeholder="University / Institution name and mailing address" 
                                rows="5" maxlength="250"></textarea>
					    </div>
					  </div>	

					  <div class="form-group row">
					    <label class="col-sm-3 col-form-label"></label>
					    <div class="col-sm-9">					    	
                            <asp:CheckBox ID="chbxCinfrm" runat="server" Checked="false"                    
                    AutoPostBack="true" Text="I confirm all the information to be correct." OnCheckedChanged="chbxCinfrm_CheckedChanged" />
					    	
					    </div>
					  </div>					  			  

					

				</div>			

				<hr >

	            <div class="form-group">
	              <label class="col-md-3 control-label" for="Comments">Comments</label>
	              <div class="col-md-9">
	                <textarea runat="server" class="form-control" id="tbComments" maxlength="490" name="Comments" placeholder="Please enter your Comments here..." rows="5"></textarea>
	              </div>
	            </div>			            

	            <!-- Form actions -->
	            <div class="form-group">
	              <div class="col-md-12">
	              	<hr >	                
                      <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" class="btn btn-primary"  /><br />
       
	                <br><br><br>
                      <asp:Label ID="lblThanks" runat="server" Width="386px" Height="82px"></asp:Label>
	              </div>
	            </div>


                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup0" Style="display: none" class="btn btn-primary btn-md" />
            <asp:ModalPopupExtender runat="server" ID="programmaticModalPopup0" BehaviorID="programmaticModalPopupBehavior0"
                TargetControlID="hiddenTargetControlForModalPopup0" PopupControlID="programmaticPopup0"
                BackgroundCssClass="modalBackground" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle0"
                RepositionMode="RepositionOnWindowScroll">
            </asp:ModalPopupExtender>
            <asp:Panel runat="server" CssClass="modalPopup" ID="programmaticPopup0" Style="display: none; width: 300px; padding: 10px">
                <asp:Panel runat="Server" ID="programmaticPopupDragHandle0" Style="cursor: move; background-color: #1e9966; border: solid 1px Gray; color: Black; text-align: center;">
                    Message
                </asp:Panel>
                <asp:Label ID="labelProgrammaticPopup0" runat="server"></asp:Label>
                <br />
                <div align="center">
                    <asp:LinkButton runat="server" ID="hideModalPopupViaServer0" Text="OK" OnClick="hideModalPopupViaServer0_Click" />
                </div>
            </asp:Panel>
</asp:Content>

