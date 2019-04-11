<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CreatePassword.aspx.cs" Inherits="WiseThink.NTCA.CreatePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function clearText() {
        $("#<% =txtNewPassword.ClientID %>").val('');
        $("#<%=txtConfirmPassword.ClientID %>").val('');
    }
    $(document).ready(function () {
        $('#<%=btnSubmit.ClientID %>').click(function (e) {
            if (typeof (Page_ClientValidate) == 'function') {
                var isPageValid = Page_ClientValidate('ChangePasswordValidationGroup');
                if (isPageValid) {

                    
                    var newPassVal = $('#<%=txtNewPassword.ClientID %>').val();
                    var confirmPassVal = $('#<%=txtConfirmPassword.ClientID %>').val();
                    
                    var md5newPass = Encript(newPassVal);
                    var md5confirmPass = Encript(confirmPassVal);

                    $('#<%=txtNewPassword.ClientID %>').val(md5newPass);
                    $('#<%=txtConfirmPassword.ClientID %>').val(md5confirmPass);
                }
                else {
                    clearText();
                   // e.preventDefault();
                }
            }
        });

    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row minheight">


        <h4 class="input-lg Border">
            <b>Create  Password</b></h4>

    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification"
            ValidationGroup="CreatePasswordValidationGroup" />
    </div>
   
        <div class="col-sm-10 col-sm-offset-1 border">
            <div class="form-group row">
                <div class="col-sm-4 control-label">
                    New Password: <b class="REerror"> *</b>
                </div>
                <div class="col-sm-8">
                    <cc:CustomTextBox ID="txtNewPassword" TextMode="Password" runat="server" MaxLength="32"
                        CssClass="form-control autoDisable" Width="50%"></cc:CustomTextBox>
                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="txtNewPassword"
                        ForeColor="Red" ValidationGroup="CreatePasswordValidationGroup" ErrorMessage="New Password is required.">*</asp:RequiredFieldValidator>
                </div>
            </div>
           <%--  --%>
            <div class="form-group">
                <div class="col-sm-4 control-label">
                    Confirm Password: <b class="REerror"> *</b>
                </div>
                <div class="col-sm-8">
                    <cc:CustomTextBox ID="txtConfirmPassword" TextMode="Password" runat="server" MaxLength="32" CssClass="form-control autoDisable"
                        Width="50%"></cc:CustomTextBox>
                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="txtConfirmPassword"
                        ForeColor="Red" ValidationGroup="CreatePasswordValidationGroup" ErrorMessage="Confirm Password is required.">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                &nbsp;
            </div>
            <div class="form-group">
                <div class="col-sm-4 control-label">
                    &nbsp;
                </div>
                <div class="col-sm-8">
                    <cc:CustomButton ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit"
                        ValidationGroup="ChangePasswordValidationGroup" OnClick="btnSubmit_Click" />
                    <cc:CustomButton ID="btnCancel" CssClass="btn btn-primary" runat="server" Text="Back" OnClick="btnCancel_Click" />
                    <%--<button id="btnCancel" type="button" onclick="window.location.href = 'Login.aspx?logout=true'"
                        class="btn btn-primary">
                        Back</button>--%>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-4 control-label">
                    &nbsp;
                </div>
                <div class="col-sm-8">
                    <cc:CustomLabel ID="lblMessageDisplay" CssClass="text-danger" runat="server" Text=""></cc:CustomLabel>
                </div>
            </div>
        </div>
   
        </div>
</asp:Content>
