<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="ManageInstallment.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.ManageInstallment" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(function () {
        validation();
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>Manage Installments</b></h4>
        <asp:UpdatePanel ID="upInstallment" runat="server">
            <ContentTemplate>
                <div class="text-center col-sm-offset-1 col-sm-11 ">
                    <div id="DisplayErrorMessage" class="btn-danger " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                </div>
                <div class="col-sm-11 col-sm-offset-1 MarginTop">
                <div class="col-sm-12 form-group">
                        <div class="col-sm-5">
                            <cc:CustomLabel ID="lblTigerReserve" runat="server" Text="Select Tiger Reserve*:"></cc:CustomLabel>
                        </div>
                        <div class="col-sm-6">
                            <cc:CustomDropDownList ID="ddlTigerReserve" runat="server" req="1" Height="30px" class="form-control" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTigerReserve_SelectedIndexChanged">
                                    </cc:CustomDropDownList>
                        </div>
                    </div>
                    <div class="TopMargin">
                    </div>
                    <div class="col-sm-12 form-group">
                        <div class="col-sm-5">
                            <cc:CustomLabel ID="lblSetFirstInstallment" runat="server" Text="Set First Installment Amount Release (In %):"></cc:CustomLabel><b class="REerror"> *</b>
                        </div>
                        <div class="col-sm-6">
                            <cc:CustomTextBox ID="txtSetFirstInstallment" req="1" dec="1" CssClass="form-control" runat="server"
                                AutoPostBack="True" OnTextChanged="txtSetFirstInstallment_TextChanged" MaxLength="6"></cc:CustomTextBox>
                        </div>
                    </div>
                   <!-- <div class="TopMargin">
                    </div> -->
                    <div class="col-sm-12 form-group">
                        <div class="col-sm-5">
                            <cc:CustomLabel ID="lblSetSecondInstallment" runat="server" Text="Set Second Installment Amount Release (In %):"></cc:CustomLabel>
                        </div>
                        <div class="col-sm-6">
                            <cc:CustomTextBox ID="txtSetSecondInstallment" req="1" dec="1" CssClass="form-control" runat="server" MaxLength="6"></cc:CustomTextBox>
                        </div>
                    </div>
                    <div class="col-sm-10 form-group text-center">
                    
                        <cc:CustomButton ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return confirm('Are you sure? you want to add / edit these installments!');" CssClass="btn-primary btn"
                            OnClick="btnSubmit_Click" />
                        <button id="btnBack" type="button" onclick="window.location.href = 'Home.aspx'" class="btn btn-primary col-sm-offset-1">
                            Back</button>
                    
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
