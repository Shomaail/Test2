<%@ Page Language="C#" MasterPageFile="~/Forms/WillingnessLetter.master" AutoEventWireup="true" CodeFile="ShowWillingnessPC.aspx.cs" Inherits="Forms_ShowWillingnessPC" 
    MaintainScrollPositionOnPostback="true"
     %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="LabelWebageUnavailable" runat="server" Visible="False" 
        ForeColor="Red"></asp:Label>
     <asp:Label ID="lblWillingnessLetterContent" runat="server"></asp:Label>
    <hr >

				<div class="form-check">
				 <asp:RadioButton ID="RadioButtonAccept" runat="server" Checked="True" GroupName="Williingness"
                Text="Accept"  /><br />
                 <br />
                <asp:RadioButton ID="RadioButtonReject" runat="server" GroupName="Williingness" Height="20px"
                Text="Decline" Width="89px" />
				</div>
       <hr />
         <div class="form-group">
	              <label class="col-md-3 control-label" for="Comments">Comments</label>
	              <div class="col-md-9">
	                <textarea runat="server" class="form-control" id="tbComments" maxlength="500" name="Comments" placeholder="Please enter your Comments here..." rows="5"></textarea>
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

    
</asp:Content>

