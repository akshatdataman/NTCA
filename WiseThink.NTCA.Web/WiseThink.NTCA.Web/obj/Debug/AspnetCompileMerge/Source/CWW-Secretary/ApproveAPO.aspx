<%@ Page Title="" Language="C#" MasterPageFile="~/CWW-Secretary/CWW-Secretary.master"
    AutoEventWireup="true" CodeBehind="ApproveAPO.aspx.cs" Inherits="WiseThink.NTCA.Web.CWW_Secretary.ApproveAPO" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CWWPlaceHolder" runat="server">
    <div class="row ">
        <h4 id="ApoStatusHeader" runat="server">
            <b>Approve / Modify APO</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
            <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="col-sm-10 col-sm-offset-1">
            <div class="form-group col-sm-12">
                <label for="UserName" class="col-sm-4 control-label fontsizeOblig">
                    APO Title</label>
                <div class="col-sm-6 ">
                    <span id="ApoTitle" runat="server" />
                </div>
            </div>
            <div class="form-group col-sm-12">
                <label for="UserName" class="col-sm-4 control-label fontsizeOblig">
                    Tiger Reserve:</label>
                <div class="col-sm-6 ">
                    <span id="TigerReserveName" runat="server" />
                </div>
            </div>
            <div class="form-group col-sm-12">
                <label for="UserName" class="col-sm-4 control-label fontsizeOblig">
                    File No.:</label>
                <div class="col-sm-6 ">
                    <span id="ApoFileNumber" runat="server" />
                </div>
            </div>
            <div class="form-group col-sm-12">
                <label for="UserName" class="col-sm-4 control-label fontsizeOblig">
                    FD Name:</label>
                <div class="col-sm-6 ">
                    <span id="FDName" runat="server" />
                </div>
            </div>
            <div class="form-group ">
                <label for="UserName" class="col-sm-4 control-label fontsizeOblig">
                    Remarks / Comments:</label>
                <div class="col-sm-6 ">
                    <cc:CustomTextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine"
                        Rows="4" Columns="50"></cc:CustomTextBox>
                </div>
            </div>
            <div class="form-group col-sm-12 TopMargin bottomMargin">
                <div class="col-sm-3">
                    <cc:CustomButton ID="btnApprove" runat="server" CssClass="btn btn-primary" Text="Approve"
                       OnClientClick="return confirm('Are you sure? you want to approve and forward this apo to regional officer!');" OnClick="btnApprove_Click" />
                </div>
                <div class="col-sm-3">
                    <cc:CustomButton ID="btnModify" runat="server" CssClass="btn btn-primary" Text="Modify"
                       OnClientClick="return confirm('Are you sure? you want to edit this apo!');" OnClick="btnModify_Click" />
                </div>
                <div class="col-sm-3">
                    <cc:CustomButton ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back"
                        OnClick="btnBack_Click" />
                </div>
            </div>
        </div>
    </div>
    </span></span></span></span>
</asp:Content>
