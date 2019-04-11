<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditProfile.ascx.cs" Inherits="WiseThink.NTCA.Web.UserControls.EditProfile" %>
<div class=" row minheight">
<div class="input-lg Border">
            Edit Profile<br />
           
        </div>

            <div class="col-md-12 table-bordered">
                <div class="form-group">
               <div class=" form-control  headingCss">
                 
                       <label id="lblLoginDetails">
                           Log In Details</label>
              
               </div>
               </div>
                <div class="form-group row">
                    <label for="UserName" class="col-sm-2 control-label">
                        Username</label>
                    <div class="col-sm-4 ">
                        <asp:TextBox ID="txtUsername" class="form-control" runat="server" placeholder="Enter Username" Text="" Enabled=""></asp:TextBox>
                    </div>
                </div>
                <%--UserName--%>
                <div>
                 <div class=" form-control form-group headingCss TopMargin">
                    
                        <label id="Label1">
                            Personal Informations</label>
                   
                </div>
                <div id="Div1" class="form-group row" runat="server">
                    <label id="lblConfirm"  class="col-sm-2 control-label">
                        Title</label>
                  
                    <div class="col-sm-2">
                        <div class="dropdown">
                            <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control" Enabled="">
                                <asp:ListItem>--Select Title--</asp:ListItem>
                                <asp:ListItem Selected="True">Mr.</asp:ListItem>
                                <asp:ListItem>Ms.</asp:ListItem>
                                <asp:ListItem>Mrs.</asp:ListItem>
                                   <asp:ListItem>Dr.</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        
                    </div>
                    <div class="col-sm-2"></div>
                    <label for="Title" class="col-sm-2 control-label">
                        First Name</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtFirstName" class="form-control" placeholder="Enter First Name" runat="server"></asp:TextBox>
                    </div>
                </div>
                <%--Confirm Password--%>
                <div class="form-group row">
                    <label for="MiddleName" class="col-sm-2 control-label">
                        Middle Name</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtMiddleName" class="form-control" placeholder="Enter Middle Name"
                            runat="server"></asp:TextBox>
                    </div>
                    <label for="LasteName" class="col-sm-2 control-label">
                        Last Name</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtLastName" class="form-control" placeholder="Enter Last Name"
                            runat="server"></asp:TextBox>
                    </div>
                </div>
                <%--FirstName--%>
                <div class="form-group row">
                    <label for="DateOfBirth" class="col-sm-2 control-label">
                        Date of Birth</label>
                    <div class="col-sm-2">
                        <div class="control-group">
                            <asp:TextBox ID="txtDOB" class="form-control date" placeholder="Select any date" runat="server" Text=""></asp:TextBox>
                        </div>
                       
                    </div>
                    <div class="col-sm-2"></div>
                    <label for="Gender" class="col-sm-2 control-label">
                        Gender</label>
                    <div class="col-sm-4 RadioButtonPadding">
                        <asp:RadioButtonList ID="radiobtn" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Male" Text="&nbsp;Male&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" />
                            <asp:ListItem Value="Female" Text="&nbsp;Female" />
                        </asp:RadioButtonList>
                    </div>
                </div>
                <%--LastName--%>
                <div class="form-group row">
                    <label for="Role" class="col-sm-2 control-label">
                        Role</label>
                    <div class="col-sm-2">
                        <div class="dropdown">
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" Enabled="false">
                                <asp:ListItem>--Select Role--</asp:ListItem>
                                <asp:ListItem Selected="True">NTCA</asp:ListItem>
                                <asp:ListItem>CWW</asp:ListItem>
                                <asp:ListItem>FD</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                       
                    </div>
                   
                </div>
              
                <div class=" form-control headingCss TopMargin">
                    <div class="col-md-12 ">
                        <label id="Label2">
                            Communication Particulars</label>
                    </div>
                </div>
                <%--Gender--%>
                <div class="form-group row">
                    <label for="Address" class="col-sm-2 control-label">
                        Address
                    </label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtAddress" class="form-control" placeholder="Enter Address" Rows="2"
                            TextMode="multiline" runat="server" Text=""></asp:TextBox>
                    </div>
                    <label for="State" class="col-sm-2 control-label">
                        Country</label>
                    <div class="col-sm-4">
                        <div class="dropdown">
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" Enabled="false">
                                <asp:ListItem>--Select Country--</asp:ListItem>
                                <asp:ListItem>India</asp:ListItem>
                                <asp:ListItem>China</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <%--Email--%>
                <div class="form-group row">
                    <label for="State" class="col-sm-2 control-label">
                        State</label>
                    <div class="col-sm-4 col-md-offset-0">
                        <div class="dropdown">
                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control">
                                <asp:ListItem>--Select State--</asp:ListItem>
                                <asp:ListItem>Assam</asp:ListItem>
                                <asp:ListItem>Uttar Pradesh</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <label for="District" class="col-sm-2 control-label">
                        District</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtDistrict" class="form-control" placeholder="Enter District" runat="server"></asp:TextBox>
                    </div>
                </div>
                <%--Country--%>
                <div class="form-group row ">
                    <label for="City" class="col-sm-2 control-label">
                        City</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtCity" class="form-control" placeholder="Enter City" runat="server"></asp:TextBox>
                    </div>
                    <label for="PinCode" class="col-sm-2 control-label">
                        Pin Code</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtPinCode" class="form-control" placeholder="Enter Pin Code" runat="server" MaxLength="6"></asp:TextBox>
                    </div>
                </div>
                <%--District--%>
                <div class="form-group row">
                    <label for="PhoneNumber" class="col-sm-2 control-label">
                        Phone Number</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtPhoneNumber" class="form-control" placeholder="Enter Phone Number" MaxLength="12"
                            runat="server"></asp:TextBox>
                    </div>
                    <label for="MobileNumber" class="col-sm-2 control-label">
                        Mobile Number</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtMobileNumber" class="form-control" placeholder="Enter Mobile Number" MaxLength="11"
                            runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="FaxNumber" class="col-sm-2 control-label">
                        Fax Number</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtFaxNumber" class="form-control" placeholder="Enter Fax Number" MaxLength="12"
                            runat="server"></asp:TextBox>
                    </div>
                    <label for="FaxNumber" class="col-sm-2 control-label">
                        Email Id</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtEmail" class="form-control" placeholder="Enter Email Id" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row text-center">
                    <div class="col-md-6 TopMargin">
                        <asp:Button ID="btnLogin" runat="server" Text="Submit" class="btn btn-default btn-primary col-md-offset-10"
                            PostBackUrl="~/Admin/ManageUsers.aspx" OnClick="btnLogin_Click" />
                    </div>
                    <div class="col-md-1  TopMargin" >
                        <asp:Button ID="btnupdate" runat="server" class="btn btn-default btn-primary" Text="Back"
                            PostBackUrl="~/Admin/ManageUsers.aspx" />
                        &nbsp;
                    </div>
                </div>
            </div>




        </div>
        </div>