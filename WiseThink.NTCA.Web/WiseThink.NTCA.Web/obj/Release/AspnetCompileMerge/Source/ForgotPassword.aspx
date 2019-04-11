<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="WiseThink.NTCA.ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(function () {
        validation();
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-2">
            &nbsp;
        </div>
        <div class="col-lg-7 well">
            <h2 class="text-center"><b>Forgot Password</b></h2>
            <hr />
            <div class="form-group">
                &nbsp;
            </div>
            <div class="form-group">
                <div class="col-sm-4 control-label">
                    Enter Username: <b class="REerror"> *</b>
                </div>
                <div class="col-sm-8">
                    <cc:CustomTextBox ID="txtUserName" runat="server"
                        CssClass="form-control autoDisable">
                    </cc:CustomTextBox>
                </div>
            </div>
            <div class="form-group">
                &nbsp;
            </div>
            <div class="form-group">
                <div class="col-sm-12 control-label">
                    OR 
                </div>
                <div class="col-sm-4 control-label">
                    Enter Email: <b class="REerror"> *</b>
                </div>
                <div class="col-sm-8">
                    <cc:CustomTextBox ID="txtEmail" runat="server" 
                        CssClass="form-control autoDisable">
                    </cc:CustomTextBox>
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
                    <cc:CustomButton ID="btnSubmit" CssClass="btn btn-primary"  runat="server" Text="Submit"
                        OnClick="btnSubmit_Click" />
                    <%--<cc:CustomButton ID="btnCancel" CssClass="btn btn-primary" PostBackUrl="~/Admin/Users.aspx"
                    runat="server" Text="Back" OnClick="btnCancel_Click" />--%>
                    <button id="btnCancel" type="button" onclick="window.location.href = 'Login.aspx?logout=true'" class="btn btn-primary">Back</button>
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

            <div class="col-lg-3">
                &nbsp;
            </div>
        </div>
    </div>
</asp:Content>
