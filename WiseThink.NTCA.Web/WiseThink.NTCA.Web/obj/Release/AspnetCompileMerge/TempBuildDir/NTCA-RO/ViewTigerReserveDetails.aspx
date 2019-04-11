<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="ViewTigerReserveDetails.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.ViewTigerReserveDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>View Tiger Reserve Details</b></h4>
        <div class="row">
            <cc:CustomImageButton ID="imgbtnWord" runat="server" ImageUrl="~/Images/Word-icon.png"
                OnClick="imgbtnWord_click" CssClass="col-sm-offset-9" />
            <cc:CustomImageButton ID="imgbtnPdf" runat="server" ImageUrl="~/Images/samplePDFIcon.png"
                OnClick="imgbtnPdf_click" CssClass="" />
        </div>
        <div>
            <div class="col-sm-12  minheight Home_margin">
                <div id="MainContent" runat="server" class="TopMargin">
                    <h3 class="text-center panel">
                        Administrative Details <span id="APOCountSpan" runat="server" class=""></span>
                    </h3>
                    <div id="divTable" class="table-responsive">
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Name of Tiger Reserve:
                            </label>
                            <div class="col-sm-3">
                                <div class="dropdown">
                                    <cc:CustomLabel ID="txtTigerReserve" runat="server"></cc:CustomLabel>
                                </div>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Legal Status:</label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtLegalStatus" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Core Area:
                            </label>
                            <div class="col-sm-3">
                                <div class="dropdown">
                                    <cc:CustomLabel ID="txtCoreArea" runat="server"></cc:CustomLabel>
                                </div>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                No of villages inside the core area:</label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtCoreAreaVillageNumber" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Status of settlement of rights:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtSettlementStatus" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Tiger conservation plan/ Indicative Tiger conservation plan:</label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtTigerConservationPlan" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Name of the post:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtNameofPost" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Sanctioned strength:</label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtSanctionedStrength" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Staff in position:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtStaffInPosition" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Vacant:</label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtVacant" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Wildlife training:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtWildlifeTraining" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Wildlife trained staff:</label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtWildlifeTrainedStaff" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Casualty of staff/others:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtCasualothersStaff" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Type and no of weapons:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtTypeOfWeaponsNumber" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Shooting of films/documentaries:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtShootingOfFilmsDocumentaries" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Postal address:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtAddress" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Type of vehicles:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtVehicleType" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Wireless:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtWireless" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Details of barriers:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtBarriersDetails" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                No of barriers:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtNumberOfBarriers" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Buffer Area:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtBufferArea" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Area of division:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtDivisionArea" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Area of Subdivission:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtSubDivisionArea" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Ranges:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtRange" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Beats:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtBeats" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Sections:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtSections" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Anti poaching camp details:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtAntiPoachingCampDetails" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Watch tower:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtWatchTower" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Name And Tenure Of Incumbents:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtNameAndTenureOfIncumbents" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Captive elephants, Dog squads, country boats, rescue teams, speed boats:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtCaptiveElephants" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Special Tiger Protection Force:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtSpecialTigerProtectionForce" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Steering committee and foundation status:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtSteeringCommitteeFoundationStatus" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row  ">
                            <label class="col-sm-3 control-label">
                                Tiger conservation foundation:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtTigerConservationFoundation" runat="server"></cc:CustomLabel>
                            </div>
                            <label class="col-sm-3 control-label">
                                Area of Tiger Reserve:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtTotalArea" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row  margin_bottom">
                            <label class="col-sm-3 control-label">
                                Any other important wild life information until last year:
                            </label>
                            <div class="col-sm-9">
                                <cc:CustomLabel ID="txtWildlifeOtherInformation" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                    </div>
                    <h3 class="text-center panel">
                        Ecological Details</h3>
                    <div class="table-responsive">
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Estimation report for last 3 years (Tiger and preybase):
                            </label>
                            <div class="col-sm-6">
                                <cc:CustomLabel ID="txtEstimationReportForLast3Years" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Important species/animals found in TR:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtImportantSpeciesAnimalsFoundInTR" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Wild Life Population estimates:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtWildlifePopulationEstimates" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Death of animals/Mortality report:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtDeathOfAnimals" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Firelines:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtFirelines" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Forest Type:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtForestType" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row  margin_bottom">
                            <label class="col-sm-3 control-label">
                                Any other important wild life information until last year:
                            </label>
                            <div class="col-sm-9">
                                <cc:CustomLabel ID="txtAnyotherImportantWildlifeInformationUntilLastYear" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                    </div>
                    <h3 class="text-center panel">
                        Financial Details
                    </h3>
                    <div id="div1" class="table-responsive">
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Revenue generated in last 5 years tourism:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtRevenueGeneratedInLast5YearsTourism" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Revenue generated in last 5 years others:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtRevenueGeneratedInLast5YearsOthers" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                No of tourists/ no of visitors(Annual):
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtAnnualNoOfTourists" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Funds provided under the state plan in last 5 years:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtFundsProvidedUnderStatePlanInLast5Years" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Funds from the CAMPA and other resources:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtFundsFromCAMPAandOtherResources" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Funds provided by CSSPT:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtFundsProvidedByCSSPT" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 control-label">
                                Cattle killed; EX-Gratia Paid in Last 5 Years:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtExGratiaPaidInLast5Years" runat="server"></cc:CustomLabel>
                            </div>
                            <label for="Title" class="col-sm-3 control-label">
                                Crop Damage:
                            </label>
                            <div class="col-sm-3">
                                <cc:CustomLabel ID="txtCropDamage" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                        <div class="form-group row  margin_bottom">
                            <label class="col-sm-3 control-label">
                                Any other important wild life information until last year:
                            </label>
                            <div class="col-sm-9">
                                <cc:CustomLabel ID="txtAnyOtherImportantFinancialCWWInformationUntilLastYear" runat="server"></cc:CustomLabel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-12 marginButton">
            <button id="Button1" type="button" onclick="window.location.href = 'Home.aspx'" class="btn btn-primary col-sm-offset-5">
                Back</button>
        </div>
    </div>
</asp:Content>
