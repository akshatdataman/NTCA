<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeBehind="EditStateMaster.aspx.cs" Inherits="WiseThink.NTCA.Web.Admin.EditStateMaster" %>
<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .jumbotron
        {
            min-height: 200px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            validation();
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="SuperAdminPlaceHolder" runat="server">
    <div class="row minheight">
        <asp:UpdatePanel ID="uppanel" runat="server">
            <ContentTemplate>
                <h4 id="StateHeader" runat="server">
                    <b>Add State</b></h4>
                <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
                    <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                        <uc:validationmessage id="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:validationmessage id="vmSuccess" runat="server" />
                    </div>
                </div>
                <div class="jumbotron" id="divSendNotification">
                    <div class="form-group col-sm-12">
                        <div class="col-sm-4">
                            <label>
                                State Name:<b class="REerror"> *</b></label>
                        </div>
                        <div class="col-sm-6">
                            <cc:CustomTextBox ID="txtState" req="1" class="form-control" runat="server" placeholder="Enter State"></cc:CustomTextBox>
                        </div>
                    </div>
                    <div class="form-group col-sm-12">
                        <div class="col-sm-4">
                            <label>
                                Is North East State:</label>
                        </div>
                        <div class="col-sm-6">
                            <cc:CustomCheckBox ID="chkIsNorthEastState" runat="server" AutoPostBack="true" OnCheckedChanged="chkIsNorthEastState_CheckedChanged" />
                        </div>
                    </div>
                    <div class="form-group text-center">
                        <div class="col-sm-10 TopMargin">
                            <cc:CustomButton ID="btnSave" runat="server" Text="Save" class="btn btn-default btn-primary "
                                OnClick="btnSave_Click" />
                            <%-- <cc:CustomButton ID="btnBack" runat="server" class="btn btn-default btn-primary"
                            Text="Back" PostBackUrl="~/Admin/StateMaster.aspx" />--%>
                            <input type="button" onclick="window.location.href = 'StateMaster.aspx'" value="Back"
                                class="btn btn-primary col-sm-offset-1" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
