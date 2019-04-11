<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Message.ascx.cs" Inherits="WiseThink.NTCA.Web.UserControls.Message" %>
<div class="message">
  <div class="b-warning" id="successContainer" runat="server" visible="false">
    <div class="icon succes-icon">
    </div>
    <div class="title"><asp:Literal ID="ltrInfoMessage" runat="server"></asp:Literal></div>
    <a class="i-8 i-close_8" href="#" onclick="$(this).parent('.b-warning').hide();return false;"></a>
  </div>
  <div class="b-warning" id="errorContainer" runat="server" visible="false">
    <div class="icon error-icon">
    </div>
    <div class="title"><asp:Literal ID="ltrErrorMessage" runat="server"></asp:Literal></div>
    <a class="i-8 i-close_8" href="#" onclick="$(this).parent('.b-warning').hide();return false;"></a>
  </div>
  <div class="b-warning" id="warningContainer" runat="server" visible="false">
    <div class="icon warning-icon">
	</div>
	<div class="title"><asp:Literal ID="ltrWarningMessage" runat="server"></asp:Literal></div>
	<a class="i-8 i-close_8" href="#" onclick="$(this).parent('.b-warning').hide();return false;"></a>
  </div>
</div>
