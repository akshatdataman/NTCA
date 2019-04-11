<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="UpdateAPOStatus.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.UpdateAPOStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div class="row">
        <h4 id="UpdateApoStatusHeader" runat="server">
            <b>Update Apo Status</b></h4>
        <div class="jumbotron" id="divSendNotification">
            <div>
                <b>Current APO Status: <span id="CurrentStatus" runat="server" /></b>
                <br />
                <h6>
                    Update Status To :<b class="REerror"> *</b></h6>
            </div>
            <div>
                <cc:CustomDropDownList ID="ddlName" runat="server" class="form-control">
                </cc:CustomDropDownList>
            </div>
            <div>
                <h6>
                    Any Comments : <b class="REerror"> *</b></h6>
            </div>
            <div class="form-group form-group-lg">
                <cc:CustomTextBox ID="txtComments" TextMode="MultiLine" Rows="3" runat="server" class="form-control"
                    placeHolder="Please write your comments here"></cc:CustomTextBox>
            </div>
            <div class="col-sm-offset-4 col-sm-2 marginButton">
                <cc:CustomButton ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary"
                    OnClick="btnUpdate_Click" />
            </div>
            <div class="col-sm-2 marginButton">
                <cc:CustomButton ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-primary"
                    OnClick="btnCancel_Click" />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
