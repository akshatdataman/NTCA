<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="TigerReserveDetails.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.TigerReserveDetails" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .form-control {
            width: 88% !important;
            padding: 6px 12px 6px !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            AddActiveClass("TigerReserve", "", "");
            BindValidationMethod();
            validation();

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div>
        <%--<--Set DashBord -->--%>
        <div class="row minheight">
            <h4>
                <b>Update Tiger Reserve Details</b></h4>
            <div class="text-center col-sm-offset-1 col-sm-10 MessagePadding">
                <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                    <uc:ValidationMessage ID="vmError" runat="server" />
                </div>
                <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                    <uc:ValidationMessage ID="vmSuccess" runat="server" />
                </div>
            </div>
            <div class="">
                <div class="col-sm-12  minheight Home_margin">
                    <div id="content" class="TopMargin">
                        <h3 class="panel image">Administrative <span id="APOCountSpan" runat="server" class=""></span>
                        </h3>
                        <div id="divTable" class="collapse table-responsive">

                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Name of Tiger Reserve
                                </label>
                                <div class="col-sm-3">
                                    <div class="dropdown">
                                        <cc:CustomTextBox ID="txtTigerReserve" placeholder="Enter Name of Tiger Reserve"
                                            runat="server" class="form-control"></cc:CustomTextBox>
                                        <asp:RegularExpressionValidator
                                            ID="regxTigerReserve"
                                            runat="server"
                                            ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                            ControlToValidate="txtTigerReserve"
                                            ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ])*"
                                            ForeColor="Red" SetFocusOnError="True">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Legal Status</label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtLegalStatus" placeholder="Enter Legal Status" runat="server"
                                        class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxLegalStatus"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtLegalStatus"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[/]|[ ])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Core Area
                                </label>
                                <div class="col-sm-3">
                                    <div class="dropdown">
                                        <cc:CustomTextBox ID="txtCoreArea" placeholder="Enter Core Area " runat="server" dec="1"
                                            class="form-control"></cc:CustomTextBox>
                                    </div>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    No of villages inside the core area</label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtCoreAreaVillageNumber" placeholder="Enter No of villages inside the core area"
                                        runat="server" num="1" class="form-control"></cc:CustomTextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Status of settlement of rights
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtSettlementStatus" placeholder="Enter Status of settlement of rights  "
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxSettlementStatus"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtSettlementStatus"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Tiger conservation plan/ Indicative Tiger conservation plan</label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtTigerConservationPlan" placeholder="Enter Tiger conservation plan/ Indicative Tiger conservation plan"
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxTigerConservationPlan"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtTigerConservationPlan"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Name of the post
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtNameofPost" placeholder="Enter Name of the post " runat="server"
                                        class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxNameofPost"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtNameofPost"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Sanctioned strength</label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtSanctionedStrength" placeholder="Enter Sanctioned strength"
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxSanctionedStrength"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtSanctionedStrength"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Staff in position
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtStaffInPosition" placeholder="Enter  Staff in position "
                                        runat="server" num="1" class="form-control"></cc:CustomTextBox>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Vacant</label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtVacant" placeholder="Enter Vacant" runat="server" num="1"
                                        class="form-control"></cc:CustomTextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Wildlife training
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtWildlifeTraining" placeholder="Enter Wildlife training  "
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxWildlifeTraining"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtWildlifeTraining"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Wildlife trained staff</label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtWildlifeTrainedStaff" placeholder="Enter Wildlife trained staff"
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxWildlifeTrainedStaff"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtWildlifeTrainedStaff"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Casualty of staff/others
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtCasualothersStaff" placeholder="Enter Casualty of staff/others  "
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxCasualothersStaff"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtCasualothersStaff"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Type and no of weapons
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtTypeOfWeaponsNumber" placeholder="Enter Type and no of weapons "
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxTypeOfWeaponsNumber"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtTypeOfWeaponsNumber"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Shooting of films/documentaries
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtShootingOfFilmsDocumentaries" placeholder="Enter Shooting of films/documentaries "
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxShootingOfFilmsDocumentaries"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtShootingOfFilmsDocumentaries"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Postal address
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtAddress" placeholder="Enter Postal address" runat="server"
                                        class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxAddress"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtAddress"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Type of vehicles
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtVehicleType" placeholder="Enter vehicle types" TextMode="MultiLine"
                                        Rows="4" runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxVehicleType"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtVehicleType"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Wireless
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtWireless" placeholder="Enter wireless details" TextMode="MultiLine"
                                        Rows="4" runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxWireless"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtWireless"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Details of barriers
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtBarriersDetails" placeholder="Enter Details of barriers "
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxBarriersDetails"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtBarriersDetails"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    No of barriers
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtNumberOfBarriers" placeholder="Enter no of barriers" runat="server" num="1"
                                        class="form-control"></cc:CustomTextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Buffer Area
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtBufferArea" placeholder="Enter Tiger conservation foundation" dec="1"
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Area of division
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtDivisionArea" placeholder="Enter area of division" TextMode="MultiLine"
                                        Rows="4" runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxDivisionArea"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtDivisionArea"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Area of Subdivission
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtSubDivisionArea" placeholder="Enter Area of Subdivission "
                                        TextMode="MultiLine" Rows="4" runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxSubDivisionArea"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtSubDivisionArea"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Ranges
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtRange" placeholder="Enter ranges" runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxRange"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtRange"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Beats
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtBeats" placeholder="Enter beats " runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxBeats"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtBeats"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Sections
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtSections" placeholder="Enter sections" runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxSections"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtSections"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Anti poaching camp details
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtAntiPoachingCampDetails" placeholder="Enter Anti poaching camp details  "
                                        TextMode="MultiLine" Rows="4" runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxAntiPoachingCampDetails"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtAntiPoachingCampDetails"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Watch tower
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtWatchTower" placeholder="Enter watch tower" runat="server"
                                        class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxWatchTower"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtWatchTower"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Name And Tenure Of Incumbents
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtNameAndTenureOfIncumbents" placeholder="Enter Name and tenure of incumbents "
                                        TextMode="MultiLine" Rows="4" runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxNameAndTenureOfIncumbents"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtNameAndTenureOfIncumbents"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Captive elephants, Dog squads, country boats, rescue teams, speed boats,
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtCaptiveElephants" placeholder="Enter Captive elephants, Dog squads, country boats, rescue teams, speed boats"
                                        TextMode="MultiLine" Rows="4" runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxCaptiveElephants"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtCaptiveElephants"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 control-label">
                                    Special Tiger Protection Force
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtSpecialTigerProtectionForce" placeholder="Enter Special Tiger Protection Force  "
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxSpecialTigerProtectionForce"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtSpecialTigerProtectionForce"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label for="Title" class="col-sm-3 control-label">
                                    Steering committee and foundation status
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtSteeringCommitteeFoundationStatus" placeholder="Enter Steering committee and foundation status"
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxSteeringCommitteeFoundationStatus"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtSteeringCommitteeFoundationStatus"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="form-group row  ">
                                <label class="col-sm-3 control-label">
                                    Tiger conservation foundation
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtTigerConservationFoundation" placeholder="Enter Tiger conservation foundation   "
                                        runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxTigerConservationFoundation"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtTigerConservationFoundation"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[(?m:(^\r?\n?)+)])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <label class="col-sm-3 control-label">
                                    Area of Tiger Reserve
                                </label>
                                <div class="col-sm-3">
                                    <cc:CustomTextBox ID="txtTotalArea" Enabled="false" runat="server" class="form-control"></cc:CustomTextBox>
                                </div>
                            </div>
                            <div class="form-group row  margin_bottom">
                                <label class="col-sm-3 control-label">
                                    Any other important wild life information until last year
                                </label>
                                <div class="col-sm-9">
                                    <cc:CustomTextBox ID="txtWildlifeOtherInformation" placeholder="Enter Any other  important wild life information until last year "
                                        TextMode="MultiLine" Rows="4" Columns="9" runat="server" class="form-control"></cc:CustomTextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegxWildlifeOtherInformation"
                                        runat="server"
                                        ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                        ControlToValidate="txtWildlifeOtherInformation"
                                        ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+])*"
                                        ForeColor="Red" SetFocusOnError="True">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>

                        </div>
                        <h3 class="panel image">Ecological</h3>
                        <div class="collapse table-responsive">
                            <asp:UpdatePanel ID="uppnlEcology" runat="server">
                                <ContentTemplate>
                                    <div class="form-group row">
                                        <label class="col-sm-3 control-label">
                                            Estimation report for last 3 years (Tiger and preybase)
                                        </label>
                                        <div class="col-sm-6">
                                            <cc:CustomTextBox ID="txtEstimationReportForLast3Years" TextMode="MultiLine" Rows="4"
                                                placeholder="Enter Estimation report for last 3 years (Tiger and preybase)" runat="server"
                                                class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxEstimationReportForLast3Years"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtEstimationReportForLast3Years"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t]|[(?m:(^\r?\n?)+)])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-3 control-label">
                                            Important species/animals found in TR
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtImportantSpeciesAnimalsFoundInTR" placeholder="Enter Important species/animals found in TR  "
                                                runat="server" class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxImportantSpeciesAnimalsFoundInTR"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtImportantSpeciesAnimalsFoundInTR"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <label for="Title" class="col-sm-3 control-label">
                                            Wild Life Population estimates
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtWildlifePopulationEstimates" placeholder="Enter Wild Life Population estimates "
                                                runat="server" class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxWildlifePopulationEstimates"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtWildlifePopulationEstimates"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-3 control-label">
                                            Death of animals/Mortality report
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtDeathOfAnimals" placeholder="Enter Important species/animals found in TR  "
                                                runat="server" class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxDeathOfAnimals"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtDeathOfAnimals"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <label for="Title" class="col-sm-3 control-label">
                                            Firelines
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtFirelines" placeholder="Enter Firelines " runat="server"
                                                class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxFirelines"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtFirelines"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-3 control-label">
                                            Forest Type
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtForestType" placeholder="Enter Forest Type  " runat="server"
                                                class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxForestType"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtForestType"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row  margin_bottom">
                                        <label class="col-sm-3 control-label">
                                            Any other important wild life information until last year
                                        </label>
                                        <div class="col-sm-9">
                                            <cc:CustomTextBox ID="txtAnyotherImportantWildlifeInformationUntilLastYear" placeholder="Enter Any other  important wild life information until last year "
                                                TextMode="MultiLine" Rows="4" Columns="9" runat="server" class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxAnyotherImportantWildlifeInformationUntilLastYear"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtAnyotherImportantWildlifeInformationUntilLastYear"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t]|[(?m:(^\r?\n?)+)])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <h3 class="panel image">Financial
                        </h3>
                        <div id="div1" class="collapse table-responsive">
                            <asp:UpdatePanel ID="uppnlFinancial" runat="server">
                                <ContentTemplate>
                                    <div class="form-group row">
                                        <label class="col-sm-3 control-label">
                                            Revenue generated in last 5 years tourism
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtRevenueGeneratedInLast5YearsTourism" runat="server" class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxRevenueGeneratedInLast5YearsTourism"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtRevenueGeneratedInLast5YearsTourism"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <label for="Title" class="col-sm-3 control-label">
                                            Revenue generated in last 5 years others
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtRevenueGeneratedInLast5YearsOthers" runat="server" class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxRevenueGeneratedInLast5YearsOthers"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtRevenueGeneratedInLast5YearsOthers"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-3 control-label">
                                            No of tourists/ no of visitors(Annual)
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtAnnualNoOfTourists" placeholder="Enter No of tourists/ no of visitors(Annual)   "
                                                runat="server" num="1" class="form-control"></cc:CustomTextBox>
                                        </div>
                                        <label for="Title" class="col-sm-3 control-label">
                                            Funds provided under the state plan in last 5 years
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtFundsProvidedUnderStatePlanInLast5Years" placeholder="Enter Funds provided under the state plan in last 5 years "
                                                runat="server" class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxFundsProvidedUnderStatePlanInLast5Years"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtFundsProvidedUnderStatePlanInLast5Years"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-3 control-label">
                                            Funds from the CAMPA and other resources
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtFundsFromCAMPAandOtherResources" placeholder="Enter Funds from the CAMPA and other  resources   "
                                                runat="server" class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxFundsFromCAMPAandOtherResources"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtFundsFromCAMPAandOtherResources"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <label for="Title" class="col-sm-3 control-label">
                                            Funds provided by CSSPT
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtFundsProvidedByCSSPT" placeholder="Enter Funds provided by CSSPT  "
                                                runat="server" class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxFundsProvidedByCSSPT"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtFundsProvidedByCSSPT"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-3 control-label">
                                            Cattle killed; EX-Gratia Paid in Last 5 Years.
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtExGratiaPaidInLast5Years" placeholder="Enter Cattle killed; EX-Gratia paid in last 5 years."
                                                runat="server" class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxExGratiaPaidInLast5Years"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtExGratiaPaidInLast5Years"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                        <label for="Title" class="col-sm-3 control-label">
                                            Crop Damage
                                        </label>
                                        <div class="col-sm-3">
                                            <cc:CustomTextBox ID="txtCropDamage" placeholder="Enter Crop Damage  " runat="server"
                                                class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxCropDamage"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtCropDamage"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="form-group row  margin_bottom">
                                        <label class="col-sm-3 control-label">
                                            Any other important wild life information until last year
                                        </label>
                                        <div class="col-sm-9">
                                            <cc:CustomTextBox ID="txtAnyOtherImportantFinancialCWWInformationUntilLastYear" placeholder="Enter Any other  important wild life information until last year "
                                                TextMode="MultiLine" Rows="4" Columns="9" runat="server" class="form-control"></cc:CustomTextBox>
                                            <asp:RegularExpressionValidator
                                                ID="RegxAnyOtherImportantFinancialCWWInformationUntilLastYear"
                                                runat="server"
                                                ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                                                ControlToValidate="txtAnyOtherImportantFinancialCWWInformationUntilLastYear"
                                                ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t]|[(?m:(^\r?\n?)+)])*"
                                                ForeColor="Red" SetFocusOnError="True">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <%-- <asp:UpdatePanel ID="uppnlbutton" runat="server">
                        <ContentTemplate>--%>
                    <div class="col-sm-offset-4 col-sm-2 marginButton">
                        <cc:CustomButton ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary"
                            OnClick="btnUpdate_Click" />
                    </div>
                    <div class="col-sm-2 marginButton">
                        <button id="Button1" type="button" onclick="window.location.href = 'FieldDirectorHome.aspx'"
                            class="btn btn-primary col-sm-offset-1">
                            Back</button>
                    </div>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
