<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NTCAHeader.ascx.cs"
    Inherits="WiseThink.NTCA.Web.UserControls.NTCAHeader" %>
	
<div class=" fullMarginImg">
<% if (Request.Url.AbsolutePath.Contains("Login.aspx") || Request.Url.AbsolutePath.Contains("CreatePassword.aspx") || 
       Request.Url.AbsolutePath.Contains("ChangePassword.aspx") || Request.Url.AbsolutePath.Contains("ForgotPassword.aspx") 
       || Request.Url.AbsolutePath.Contains("EditProfile.aspx"))
   { %>  <div class="col-md-2">
        <img src="./Images/logo.png" class="img-responsive" />
    </div>
     <div class="img-responsive col-md-8 ">
        <img src="./Images/logotxt.png" class="img-responsive imgMargin" />
    </div>
    <div class="col-sm-1">
        <img src="./Images/elum.png" class="img-responsive" />
    </div>
    <div class="col-sm-1">
        <img src="./Images/tree.png" />
    </div>
     <% }
    %>    
<% else
   { %> <div class="col-md-2">
        <img src="../Images/logo.png" class="img-responsive" />
    </div>
     <div class="img-responsive col-md-8 ">
        <img src="../Images/logotxt.png" class="img-responsive imgMargin" />
    </div>
    <div class="col-sm-1">
        <img src="../Images/elum.png" class="img-responsive" />
    </div>
    <div class="col-sm-1">
        <img src="../Images/tree.png" />
    </div> <% } %>
</div>