<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EditProfile.aspx.cs" Inherits="WiseThink.NTCA.EditProfile" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<%@ Register Src="~/UserControls/BackButton.ascx" TagName="BackButton" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     $(function () {
         validation();
     });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row minheight">
            <h4 class="input-lg Border">
            <b>EDIT PROFILE</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin">
            <div id="DisplayErrorMessage" class="btn-danger " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="col-sm-offset-0">
            <div class="col-sm-12">
                <div class="form-group">
                    <div class="form-control form-group headingCss">
                        <label id="lblLoginDetails">
                             Log In Details</label>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="UserName" class="col-sm-2 control-label">
                        Username</label>
                    <div class="col-sm-4 ">
                        <cc:CustomTextBox ID="txtUsername" class="form-control  textwidh" runat="server"  placeholder="Enter Username"
                            Text=""></cc:CustomTextBox>
                        &nbsp;
                        <cc:CustomLabel ID="lblUserCheck" runat="server"></cc:CustomLabel>
                    </div>
                </div>
                <div class="form-control form-group headingCss">
                    <label id="Label1">
                         Personal Informations</label>
                </div>
                <div id="Div1" class="form-group row" runat="server">
                    <label id="lblTitle" class="col-sm-2 control-label">
                        Title : <b class="REerror"> *</b></label>
                    <div class="col-sm-4">
                        <div class="dropdown">
                            <cc:CustomDropDownList CssClass="form-control  textwidh" ID="ddlTitle" runat="server" req="1">
                            </cc:CustomDropDownList>
                        </div>
                    </div>
                    <label for="Title" class="col-sm-2 control-label">
                        First Name: <b class="REerror"> *</b></label>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtFirstName" placeholder="Enter First Name"  runat="server"
                            req="1" class="form-control  textwidh"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="MiddleName" class="col-sm-2 control-label">
                        Middle Name</label>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtMiddleName" placeholder="Enter Middle Name" runat="server"
                            class="form-control  textwidh">
                        </cc:CustomTextBox>
                    </div>
                    <label for="LasteName" class="col-sm-2 control-label">
                        Last Name : <b class="REerror"> *</b></label>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtLastName" runat="server" req="1" class="form-control  textwidh" placeholder="Enter Last Name">
                        </cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="Designation" class="col-sm-2 control-label">
                        Designation :</label>
                    <div class="col-sm-4">
                        <div class="control-group">
                            <cc:CustomDropDownList ID="ddlDesignation" CssClass="form-control  textwidh" runat="server" >
                            </cc:CustomDropDownList>
                        </div>
                    </div>
                    <label for="Gender" class="col-sm-2 control-label">
                        Gender</label>
                    <div class="col-sm-4 RadioButtonPadding">
                        <cc:CustomRadioButton ID="rbtnMale" runat="server" Text="Male" GroupName="Gender" />
                        <cc:CustomRadioButton ID="rbtnFemale" runat="server" Text="Female" GroupName="Gender" />
                    </div>
                </div>
                <div class="form-group row">
                    <label for="Role" class="col-sm-2 control-label">
                        Role : <b class="REerror"> *</b></label>
                    <div class="col-sm-4">
                        <div class="control-group">
                            <cc:CustomDropDownList CssClass="form-control  textwidh" ID="ddlRole" req="1" runat="server"
                                AutoPostBack="True">
                            </cc:CustomDropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-control form-group headingCss">
                        <label id="Label2">
                             Communication Particulars</label>
                </div>
                <div class="form-group row">
                    <label for="Address" class="col-sm-2 control-label">
                        Address
                    </label>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtArea" class="form-control  textwidh" placeholder="Enter Address" Rows="2"
                            TextMode="multiline" runat="server"></cc:CustomTextBox>
                    </div>
                    <label for="State" class="col-sm-2 control-label">
                        Country</label>
                    <div class="col-sm-4">
                        <div class="dropdown">
                            <cc:CustomDropDownList CssClass="form-control  textwidh" ID="ddlCountry" runat="server">
                            </cc:CustomDropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="State" class="col-sm-2 control-label">
                        State : <b class="REerror"> *</b></label>
                    <div class="col-sm-4 col-sm-offset-0">
                        <div class="dropdown">
                            <cc:CustomDropDownList CssClass="form-control  textwidh" ID="ddlState" req="1" runat="server" AutoPostBack="True">
                            </cc:CustomDropDownList>
                        </div>
                    </div>
                    <label for="District" class="col-sm-2 control-label">
                        District :</label>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtDistrict" class="form-control  textwidh" placeholder="Enter District"
                            runat="server"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row ">
                    <label for="City" class="col-sm-2 control-label">
                        City :</label>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtCity" runat="server" class="form-control  textwidh" placeholder="Enter City"></cc:CustomTextBox>
                    </div>
                    <label for="PinCode" class="col-sm-2 control-label">
                        Pin Code</label>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtPincode" runat="server" MaxLength="6" num="1" req="1" class="form-control  textwidh"
                            placeholder="Enter Pin Code"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="PhoneNumber" class="col-sm-2 control-label">
                        Phone Number :</label>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtPhoneNo" class="form-control  textwidh" placeholder="Enter Phone Number"
                            runat="server" num="1" MaxLength="12"></cc:CustomTextBox>
                    </div>
                    <label for="MobileNumber" class="col-sm-2 control-label">
                        Mobile Number :<b class="REerror"> *</b></label>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtMobileNO" class="form-control  textwidh" placeholder="Enter Mobile Number"
                            MaxLength="11" runat="server" mob="1" num="1" req="1"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="FaxNumber" class="col-sm-2 control-label">
                        Fax Number :</label>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtFaxNo" class="form-control  textwidh" placeholder="Enter Fax Number"
                            MaxLength="12" runat="server" num="1"></cc:CustomTextBox>
                    </div>
                    <label for="FaxNumber" class="col-sm-2 control-label">
                        Email Id :<b class="REerror"> *</b></label>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtEmail" class="form-control  textwidh" placeholder="Enter Email Id"
                            runat="server" email="1" req="1"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row TopMargin bottomMargin text-center">
                    <div class="col-sm-12">
                        <cc:CustomButton ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Register"
                            OnClick="btnSubmit_Click" />
                       <%-- <uc1:BackButton ID="BackButton1" runat="server" />--%>
                        <cc:CustomButton ID="btnback" CssClass="btn btn-primary" runat="server" Text="Back"
                            OnClick="btnback_Click" />
                            
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
