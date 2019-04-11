<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="UpdateAPOStatus.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.UpdateAPOStatus" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .jumbotron
        {
            min-height:400px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            validation();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div class="row">
        <h4>
            <b>Update Apo Status / By Pass Apo</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
            <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="jumbotron sendAlertMinheigt" id="divSendNotification">
            <div class="form-group col-sm-12">
                <label class="col-sm-5">
                    Current APO Status:</label>
                <div class="col-sm-7">
                    <b><span id="CurrentStatus" runat="server" /></b>
                </div>
            </div>
            <div class="form-group col-sm-12">
                <label class="col-sm-5">
                    Update Status To:<b class="REerror"> *</b></label>
                <div class="col-sm-7">
                    <cc:CustomDropDownList ID="ddlName" req="1" runat="server" class="form-control textwidh">
                    </cc:CustomDropDownList>
                </div>
            </div>
            <div id="RoDiaryDiv" runat="server" class="form-group col-sm-12">
                <label class="col-sm-5">
                    Regional Officer's Diary Number:<b class="REerror"> *</b></label>
                <div class="col-sm-7">
                    <cc:CustomTextBox ID="txtRODiaryNum" req="1" runat="server" class="form-control textwidh" placeHolder="Please enter diary number"></cc:CustomTextBox>
                </div>
            </div>
            <div class="form-group col-sm-12">
                <label class="col-sm-5">
                    Comments:<b class="REerror"> *</b></label>
                <div class="col-sm-7">
                    <cc:CustomTextBox ID="txtComments" req="1" TextMode="MultiLine" Rows="4" Columns="50"
                        runat="server" class="form-control textwidh" placeHolder="Comments"></cc:CustomTextBox>
                </div>
            </div>
            <div id="BypassReasonDiv" runat="server" class="form-group col-sm-12">
                <label class="col-sm-5">
                    Reason For Bypass:<b class="REerror"> *</b></label>
                <div class="col-sm-7">
                    <cc:CustomTextBox ID="txtBypass" req="1" TextMode="MultiLine" Rows="4" Columns="50"
                        runat="server" class="form-control textwidh" placeHolder="Reason For Bypass"></cc:CustomTextBox>
                </div>
            </div>
            <div class="col-sm-offset-5 col-sm-12 marginButton">
               <cc:CustomButton ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary"
                    OnClientClick="return confirm('Are you sure? you want to update the status of this apo!');" OnClick="btnUpdate_Click" />
                    <button id="btnBack" type="button" onclick="window.location.href = 'Home.aspx'"
                            class="btn btn-primary col-sm-offset-1">
                            Back</button>
            </div>
        </div>
    </div>
</asp:Content>
