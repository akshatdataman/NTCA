<%@ Page Title="" Language="C#" MasterPageFile="~/CWW-Secretary/CWW-Secretary.master" AutoEventWireup="true" CodeBehind="UpdateAPOStatus.aspx.cs" Inherits="WiseThink.NTCA.Web.CWW_Secretary.UpdateAPOStatus" %>
<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .jumbotron
        {
            min-height:320px;
        }
    </style>
<script type="text/javascript">
    $(function () {
        validation();
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CWWPlaceHolder" runat="server">
<div class="row">
        <h4>
            <b>Update Apo Status</b></h4>
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
                <label class="col-sm-4">
                    Current APO Status:</label>
                <div class="col-sm-8">
                    <b><span id="CurrentStatus" runat="server" /></b>
                </div>
            </div>
            <div class="form-group col-sm-12">
                <label class="col-sm-4">
                    Update Status To :<b class="REerror"> *</b></label>
                <div class="col-sm-8">
                    <cc:CustomDropDownList ID="ddlName" req="1" runat="server" class="form-control">
                    </cc:CustomDropDownList>
                </div>
            </div>
            
            <div class="form-group col-sm-12">
                <label class="col-sm-4">
                    Comments :<b class="REerror"> *</b></label>
                <div class="col-sm-8">
                    <cc:CustomTextBox ID="txtComments" req="1" TextMode="MultiLine" Rows="4" Columns="50"
                        runat="server" class="form-control" placeHolder="Comments"></cc:CustomTextBox>
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
