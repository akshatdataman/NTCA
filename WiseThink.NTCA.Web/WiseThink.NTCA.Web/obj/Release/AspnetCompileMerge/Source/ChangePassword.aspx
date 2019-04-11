<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="WiseThink.NTCA.ChangePassword" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<%@ Register Src="~/UserControls/BackButton.ascx" TagName="BackButton" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jQuery.WiseThink.Common.js" type="text/javascript"></script>
    <script src="js/jquery.crypt.js" type="text/javascript"></script>
    <script src="js/aes.js" type="text/javascript"></script>
    <script type="text/javascript">
        function clearText() {
            $("#<% =txtOldPassword.ClientID %>").val('');
            $("#<% =txtNewPassword.ClientID %>").val('');
            $("#<%=txtConfirmPassword.ClientID %>").val('');
        }
        $(document).ready(function () {
            $('#<%=btnSubmit.ClientID %>').click(function (e) {
                if (typeof (Page_ClientValidate) == 'function') {
                    var isPageValid = Page_ClientValidate('ChangePasswordValidationGroup');
                    if (isPageValid) {

                        var oldPassVal = $('#<%=txtOldPassword.ClientID %>').val();
                        var newPassVal = $('#<%=txtNewPassword.ClientID %>').val();
                        var confirmPassVal = $('#<%=txtConfirmPassword.ClientID %>').val();
                        var md5oldPass = Encript(oldPassVal);
                        var md5newPass = Encript(newPassVal);
                        var md5confirmPass = Encript(confirmPassVal);
                        var key = CryptoJS.enc.Utf8.parse('8080808080808080');
                        var iv = CryptoJS.enc.Utf8.parse('8080808080808080');


                        $('#<%=txtOldPassword.ClientID %>').val(md5oldPass);
                        $('#<%=txtNewPassword.ClientID %>').val(md5newPass);
                        $('#<%=txtConfirmPassword.ClientID %>').val(md5confirmPass);
                        var randomNo = Math.random();
                        var randomComp = newPassVal + "#" + randomNo;
                        var encryptedComp = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(randomComp), key,
                {
                    keySize: 128 / 8,
                    iv: iv,
                    mode: CryptoJS.mode.CBC,
                    padding: CryptoJS.pad.Pkcs7
                });

                        $('#<%=hdnComp.ClientID %>').val(encryptedComp);
                    }
                    else {
                        clearText();
                        e.preventDefault();
                    }
                }
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="DisplayErrorMessage" class="btn-danger" runat="server">
        <uc:ValidationMessage ID="vmError" runat="server" />
    </div>
    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
        <uc:ValidationMessage ID="vmSuccess" runat="server" />
    </div>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification"
            ValidationGroup="ChangePasswordValidationGroup" />
    </div>
    <div class="row">
        <div class="Border">
        </div>
        <div class="col-lg-7 well col-sm-offset-2 TopMargin ">
            <h2 class="text-center   ">
                <b>Change Password</b></h2>
            <hr />
            <div class="form-group col-sm-12">
                <div class="col-sm-6 control-label">
                    User Name:
                </div>
                <div class="col-sm-6">
                    <cc:CustomLabel ID="lblUserName" class="col-sm-6 control-label" runat="server"></cc:CustomLabel>
                </div>
            </div>
            <div class="form-group col-sm-12">
                <div class="col-sm-6 control-label">
                    Old Password: <b class="REerror">*</b>
                </div>
                <div class="col-sm-6">
                    <cc:CustomTextBox ID="txtOldPassword" runat="server" TextMode="Password"
                        CssClass="form-control textwidh autoDisable" MaxLength="32"></cc:CustomTextBox>
                    <asp:RequiredFieldValidator ID="OldPasswordRequired" runat="server" ControlToValidate="txtOldPassword"
                        ForeColor="#FF3300" ValidationGroup="ChangePasswordValidationGroup" ErrorMessage="Old Password is required.">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group col-sm-12">
                <div class="col-sm-6 control-label">
                    New Password: <b class="REerror">*</b>
                </div>
                <div class="col-sm-6">
                    <asp:HiddenField ID="hdnComp" runat="server" />
                    <cc:CustomTextBox ID="txtNewPassword" runat="server" CssClass="form-control textwidh autoDisable" MaxLength="32"
                        TextMode="Password"></cc:CustomTextBox>
                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="txtNewPassword"
                        ForeColor="#FF3300" ValidationGroup="ChangePasswordValidationGroup" ErrorMessage="New Password is required.">*</asp:RequiredFieldValidator>
                    <%--<asp:RegularExpressionValidator ID="Regex1" runat="server" ControlToValidate="txtNewPassword"
                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}" ErrorMessage="Password must contain: Minimum 8 and Maximum 10 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" CssClass="text-danger" />--%>
                </div>
            </div>
            <div class="form-group col-sm-12">
                <div class="col-sm-6 control-label">
                    Confirm Password: *
                </div>
                <div class="col-sm-6">
                    <cc:CustomTextBox ID="txtConfirmPassword" runat="server" CssClass="form-control textwidh autoDisable" MaxLength="32"
                        TextMode="Password"></cc:CustomTextBox>
                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtConfirmPassword"
                        ForeColor="#FF3300" ValidationGroup="ChangePasswordValidationGroup" ErrorMessage="Confirm Password is required.">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group col-sm-12">
                <div class="col-sm-6 control-label">
                    &nbsp;
                </div>
                <div class="col-sm-6">
                    <cc:CustomLabel ID="lblMessageDisplay" CssClass="text-danger" runat="server" Text=""></cc:CustomLabel>
                </div>
            </div>
            <div class="form-group  ">
                <div class="col-sm-12 text-center">
                    <cc:CustomButton ID="btnSubmit" CssClass="btn btn-primary rightmargin" runat="server"
                        Text="Submit" ValidationGroup="ChangePasswordValidationGroup" OnClick="btnSubmit_Click" />
                    <%--<cc:CustomButton ID="btnCancel" CssClass="btn btn-primary" PostBackUrl="~/Admin/Users.aspx"
                    runat="server" Text="Back" OnClick="btnCancel_Click" />
                    <%--<button id="btnCancel" type="button" onclick="window.location.href = 'Home.aspx'"
                        class="btn btn-primary">
                        Back</button>--%>
                    <uc1:BackButton ID="BackButton1" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <%--<h2 class="text-center row">Change Password</h2>--%>
</asp:Content>
