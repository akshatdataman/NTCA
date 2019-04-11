<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeBehind="EditTigerReserve.aspx.cs" Inherits="WiseThink.NTCA.Web.Admin.WebForm5" %>
<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content2" ContentPlaceHolderID="SuperAdminPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            validation();
            var sum = 0;
            $('.fetch').change(function () {
                $('.setvalue').val('');
                var gg = parseFloat($(this).val());
                sum = sum + gg;
                if (parseInt(sum)) {
                    $('.setvalue').val(sum);
                }
                else {
                    sum = 0;
                    var msg = 'Character is not allowed'
                    $('.setvalue').val(msg);
                }
            });

        });
        function RefreshCaptcha() {

            $("#captchaImage").attr("src", "../../CaptchaImage/JpegImage.aspx?" + (new Date()).getTime());
        }
    </script>
    <div class="row minheight">
        <h4 id="TigerReserveHeader" runat="server">
            <b>Add Tiger Reserve</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
            <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                <uc:validationmessage id="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:validationmessage id="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="col-sm-12">
            <div class="form-group">
                <div class="  headingCss">
                    <label id="lblLoginDetails">
                        Tiger Reserve Detail</label>
                </div>
            </div>
            <div id="div" class="left">
                <div class="form-group row">
                    <label for="UserName" class="col-sm-3 control-label">
                        State:<b class="REerror"> *</b></label>
                    <div class="col-sm-3 ">
                        <cc:CustomDropDownList ID="ddlState" runat="server" req="1" CssClass="form-control textwidh"
                            AutoPostBack="True">
                        </cc:CustomDropDownList>
                    </div>
                    <label for="UserName" class="col-sm-3 control-label">
                        District:</label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtDistrict" class="form-control textwidh" placeholder="Enter District"
                            runat="server"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="UserName" class="col-sm-3 control-label">
                        City:</label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtCity" class="form-control textwidh" placeholder="Enter City"
                            runat="server"></cc:CustomTextBox>
                    </div>
                    <label for="UserName" class="col-sm-3 control-label">
                        Name of Tiger Reserve:<b class="REerror"> *</b></label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtTigerReserve" class="form-control textwidh" req="1" placeholder="Enter Name of Tiger Reserve"
                            runat="server"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="UserName" class="col-sm-3 control-label">
                        Area of the core / Critical tiger habitat (In Sq. Kms):</label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtCoreArea" class="form-control textwidh" dec="1" placeholder="Enter Core / Critical Area"
                            runat="server"></cc:CustomTextBox>
                    </div>
                    <label for="UserName" class="col-sm-3 control-label">
                        Area of the buffer / Peripheral (In Sq. Kms):</label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtBufferArea" class="form-control textwidh" dec="1" placeholder="Enter Buffer / Peripheral Area"
                            runat="server"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="UserName" class="col-sm-3 control-label">
                        Total Area (In Sq. Kms):</label>
                    <div class="col-sm-3 ">
                      <%--  <cc:CustomTextBox ID="txtTotalArea1" Enabled="false" class="form-control textwidh"
                            placeholder="Total Area (In Sq. Kms)" runat="server"></cc:CustomTextBox>--%>
                        <cc:CustomTextBox ID="txtTotalArea" runat="server" Enabled="false" CssClass="form-control textwidh"  placeholder="Total Area (In Sq. Kms)"></cc:CustomTextBox>
                    </div>
                    <label for="UserName" class="col-sm-3 control-label">
                        Date of Registration (dd/mm/yyyy):<b class="REerror"> *</b></label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtDOR" class="form-control textwidh cal" req="1" runat="server"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="UserName" class="col-sm-3 control-label">
                        Address:</label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtAddress" class="form-control textwidh" placeholder="Address"
                            runat="server"></cc:CustomTextBox>
                    </div>
                    <label for="UserName" class="col-sm-3 control-label">
                        Pin Code:</label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtPinCode" class="form-control textwidh" placeholder=" Pin Code"
                            runat="server"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-3 control-label">
                        Field Director:</label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtFieldDirector" class="form-control textwidh" placeholder=" Enter Field Director Name"
                            runat="server"></cc:CustomTextBox>
                    </div>
                    <label class="col-sm-3 control-label">
                        Phone Number:</label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtPhoneNumber" class="form-control textwidh" num="1" placeholder="Enter Phone Number"
                            runat="server"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-3 control-label">
                        Alternate Phone Number:</label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtAlternatePhoneNumber" class="form-control textwidh" num="1"
                            placeholder="Enter Alternate Phone Number" runat="server"></cc:CustomTextBox>
                    </div>
                    <label class="col-sm-3 control-label">
                        Mobile Number:<b class="REerror"> *</b></label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtMobileNumber" class="form-control textwidh" req="1" mob="1"
                            placeholder="Enter Mobile Number" runat="server"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-3 control-label">
                        Email:<b class="REerror"> *</b></label>
                    <div class="col-sm-3 ">
                        <cc:CustomTextBox ID="txtEmail" class="form-control textwidh" req="1" email="1" placeholder="Enter Email Id"
                            runat="server"></cc:CustomTextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 center-block form-group">
                <div class="col-sm-9 col-sm-offset-4">
                    <label>
                        Enter the word below:<b class="REerror"> *</b>
                    </label>
                </div>
                <div class="col-sm-offset-5 col-sm-8 bottomMargin">
                    <img src="./../CaptchaImage/JpegImage.aspx" alt="captcha image" id="captchaImage"
                        class="CaptureImagesCSs LeftMargin " />
                    <img onclick="javascript:RefreshCaptcha();" src="../Images/Refresh.png" />
                </div>
                <div class="col-sm-4 col-sm-offset-4">
                    <cc:CustomTextBox ID="txtCaptchaCode" runat="server" req="1" class="form-control"
                        placeholder="Enter Captcha Code"></cc:CustomTextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-11 text-center TopMargin bottomMargin">
                    <cc:CustomButton ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit"
                        OnClick="btnSubmit_Click" />
                    <input type="button" onclick="window.location.href = 'TigerReserve.aspx'" value="Back"
                        class="btn btn-primary col-sm-offset-1" />
                    <%-- <cc:CustomButton ID="btnBack" CssClass="btn btn-primary left" runat="server" Text="Back"
                        OnClick="btnBack_Click" />--%>
                </div>
                <div class="col-sm-2 TopMargin bottomMargin">
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
