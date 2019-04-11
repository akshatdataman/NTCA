<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="FDFlexiAmont.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.FDFlexiAmont" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <h4>
                <b>Flexi Amount</b></h4>
            <div class="col-sm-3">
                <div id="leftPartMy1" class="">
                    <div id="leftNav">
                        <div class="panel-group" id="accordion">
                            <div class=" panel-default ">
                                <div id="panel1" class="panel-collapse collapse in menuleft">
                                    <div class="table-responsive">
                                        <ul>
                                            <li class="FirstSubMenu1 Current BackgroundColor NonRecuring_left Links"><a class="anchorlink" href="#Non-RecurringDiv">Step 1: Non-Recurring</a></li>
                                                <li class="BackgroundColor Links Recuring_left"><a class="anchorlink" href="#RecuringDiv">Step 2: Recurring</a></li>

                                                <li class="BackgroundColor"><a href="FDUtilizationCertificate.aspx">Step 3: Utilization Certificate</a></li>

                                                <li class="BackgroundColor "><a href="ObligationFD.aspx">Step 4: Obligation Under Tri-MOU</a></li>

                                                <li class="BackgroundColor"><a href="CheckList.aspx?callfrom=SubmitAPO">Step 5: Check and Submit</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-9  leftBorder minHeight Home_margin" id="MainContent">
                <div class="col-sm-offset-1">
                    <span>Flexi Amount will be calculated as 10% of total current APO budget amount for
                        the cetral : state share.</span>
                    <br />
                    <br />
                    <cc:CustomButton ID="btnCalculateFlexi" runat="server" Text="Click Here To Calculate Flexi Amount"
                        CssClass="btn btn-primary" OnClick="btnCalculateFlexi_Click" />
                </div>
                <div class="form-group row  margin_bottom">
                    <label class="col-sm-4 control-label">
                        Central Flexi Amount(In Lakhs):
                    </label>
                    <div class="col-sm-8">
                        <b><span>Rs. </span></b>
                        <cc:CustomTextBox ID="txtCentralFlexiAmount" Enabled="false" runat="server" class="form-control"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="form-group row  margin_bottom">
                    <label class="col-sm-4 control-label">
                        State Flexi Amount(In Lakhs):
                    </label>
                    <div class="col-sm-8">
                        <b><span>Rs. </span></b>
                        <cc:CustomTextBox ID="txtStateFlexiAmount" Enabled="false" runat="server" class="form-control"></cc:CustomTextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
