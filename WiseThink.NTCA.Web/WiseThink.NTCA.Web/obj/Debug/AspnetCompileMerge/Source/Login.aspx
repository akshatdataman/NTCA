<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WiseThink.NTCA.Login" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function noBack() { window.history.forward(); }
        window.onpageshow = function (evt) { if (evt.persisted) noBack() }
        window.onunload = function () { void (0) }
        noBack();
        
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#<%=btnLogin.ClientID %>').click(function (e) {
                if (typeof (Page_ClientValidate) == 'function') {
                    var isPageValid = Page_ClientValidate('LoginUserValidationGroup');
                    if (isPageValid) {

                        var usrVal = $('#<%=txtUserName.ClientID %>').val();
                        var passVal = $('#<%=txtPassword.ClientID %>').val();
                        var capVal = $('#<%=txtCaptcha.ClientID %>').val();
                        var md5Pass = Encript(passVal);
                        var finalValue = usrVal + '#' + md5Pass + '#' + capVal;
                        var strMD5 = Encript(finalValue);
                        $('#<%=txtPassword.ClientID %>').val(strMD5);
                        $('#<%=hdnPassword.ClientID %>').val(md5Pass);
                        
                    }
                    else {
                        clearText();
                        e.preventDefault();
                    }
                }            
            });
        });
    </script>
    <script type="text/javascript">
        function clearText() {
            $("#<% =txtUserName.ClientID %>").val('');
            $("#<% =txtPassword.ClientID %>").val('');
            $("#<%=txtCaptcha.ClientID %>").val('');
            $('#<%=hdnPassword.ClientID %>').val('');
        }
    </script>

    
<%--<meta charset="utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta name="viewport" content="width=device-width, initial-scale=1" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(function () {
            validation();
        });
        function RefreshCaptcha() {
            $("#captchaImage").attr("src", "CaptchaImage/JpegImage.aspx?" + (new Date()).getTime());
        }        
    </script>    
  <div class="row loginpagemin-height">
            <div class="Border">
             </div>
      <div class="row">
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification REerror"
                        ValidationGroup="LoginUserValidationGroup" />
      </div>
        <div class="col-sm-offset-3 col-sm-6 well MarginTop">
            <h2 class="text-center">Login</h2>
           
            <div class="form-group row bakcolor">
                <label for="UserName" class="  col-sm-4 control-label">
                    Username :<b class="REerror"> *</b>
               </label>
                <div class="col-sm-7">

                    <cc:CustomTextBox ID="txtUserName" runat="server" autocomplete="off" CssClass="form-control textwidh"></cc:CustomTextBox>
                    <asp:RequiredFieldValidator ID="UsernameRequired" runat="server" ControlToValidate="txtUserName"
                                    ForeColor="#FF3300" ValidationGroup="LoginUserValidationGroup" ErrorMessage="User Name is required."></asp:RequiredFieldValidator>
                    
                </div>

            </div>
           
            <div class="form-group row bakcolor">
                 <label for="Password" class="col-sm-4 control-label">
                    Password :<b class="REerror"> *</b>
                </label>
                <div class="col-sm-7">
                <asp:HiddenField ID="hdnPassword" runat="server" />
                    <cc:CustomTextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="32" CssClass="form-control textwidh"></cc:CustomTextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                    ForeColor="#FF3300" ValidationGroup="LoginUserValidationGroup" ErrorMessage="Password is required."></asp:RequiredFieldValidator>
                </div>

            </div>
            <div class="form-group row bakcolor text-center">
               
              
                <div class="col-sm-offset-4 col-sm-4">
                    <img src="./CaptchaImage/JpegImage.aspx" alt="captcha image" id="captchaImage" />

                </div>
                 <div class="col-sm-2 MarginTop">
                    <img onclick="javascript:RefreshCaptcha();" src="Images/Refresh.png" />

                </div>
            </div>
            <div class="form-group text-center row bakcolor">
          
              <div class="col-sm-offset-4 col-sm-6">
                    <cc:CustomTextBox ID="txtCaptcha" MaxLength="6" runat="server" autocomplete="off" CssClass="form-control textwidh" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCaptch" runat="server" ControlToValidate="txtCaptcha"
                                    ForeColor="#FF3300" ValidationGroup="LoginUserValidationGroup" ErrorMessage="Captcha code as shown in Image is required."></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group text-center row bakcolor">
               
                <div class="col-sm-offset-4 col-sm-4">
                    <asp:HyperLink ID="hlnForgetPassword" runat="server" NavigateUrl="ForgotPassword.aspx">Forgot Password?</asp:HyperLink>
                </div>
            </div>
           
            <div class="form-group text-center row bakcolor">
                
                <div class="col-sm-offset-4 col-sm-4">
                    <cc:CustomButton ID="btnLogin" CssClass="btn btn-primary" runat="server"
                        Text="Login" OnClick="btnLogin_Click" ValidationGroup="LoginUserValidationGroup" />
                </div>

            </div>

            <div class="form-group row bakcolor">
                
                <div class="col-sm-8">
                    <cc:CustomLabel ID="lblMessageDisplay" CssClass="text-danger" runat="server" Text=""></cc:CustomLabel>
                </div>

            </div>

            
        </div>
     </div>
   
</asp:Content>
