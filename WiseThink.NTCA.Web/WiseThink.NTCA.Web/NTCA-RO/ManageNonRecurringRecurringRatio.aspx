<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="ManageNonRecurringRecurringRatio.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.ManageNonRecurringRecurringRatio" %>

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
            <b>Manage Non-Recurring and Recurring Ratio</b></h4>
        <asp:UpdatePanel ID="upNRandR" runat="server">
            <ContentTemplate>
                <div class="text-center col-sm-offset-1 col-sm-11 ">
                    <div id="DisplayErrorMessage" class="btn-danger " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                </div>
                <div class="col-sm-10 col-sm-offset-1 MarginTop">
                    <div class="col-sm-12 form-group">
                        <div class="col-sm-4">
                            <cc:CustomLabel ID="lblTigerReserve" runat="server" Text="Select Tiger Reserve:"></cc:CustomLabel><b class="REerror"> *</b>
                        </div>
                        <div class="col-sm-4">
                            <cc:CustomDropDownList ID="ddlTigerReserve" runat="server" req="1" Height="30px"
                                class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlTigerReserve_SelectedIndexChanged">
                            </cc:CustomDropDownList>
                        </div>
                    </div>
                    
                    <div class="col-sm-12 form-group">
                        <b><span class="col-sm-offset-5">Central Ratios</span></b> 
                        <b><span class="col-sm-offset-3">State Ratios</span></b>
                    </div>
                    <div class="col-sm-12 form-group">
                        <div class="col-sm-4">
                            <cc:CustomLabel ID="lblNonRecurringRatio" runat="server" Text="Non-Recurring Ratio (In %):"></cc:CustomLabel><b class="REerror"> *</b>
                        </div>
                        <div class="col-sm-4">
                            <cc:CustomTextBox ID="txtCentralNonRecurringRatio" req="1" num="1" CssClass="form-control"
                                runat="server" placeholder="Central Non Recurring Ratio" AutoPostBack="True"
                                OnTextChanged="txtCentralNonRecurringRatio_TextChanged" MaxLength="3"></cc:CustomTextBox>
                        </div>
                        <div class="col-sm-4">
                            <cc:CustomTextBox ID="txtStateNonRecurringRatio" req="1" num="1" CssClass="form-control"
                                runat="server" AutoPostBack="True" MaxLength="3"></cc:CustomTextBox>
                        </div>
                    </div>
                   
                    <div class="col-sm-12 form-group">
                        <div class="col-sm-4">
                            <cc:CustomLabel ID="lblRecurringRatio" runat="server" Text="Recurring Ratio (In %):"></cc:CustomLabel>
                        </div>
                        <div class="col-sm-4">
                            <cc:CustomTextBox ID="txtCentralRecurringRatio" CssClass="form-control" runat="server"
                                placeholder="Central Recurring Ratio" MaxLength="3" OnTextChanged="txtCentralRecurringRatio_TextChanged"
                                AutoPostBack="True"></cc:CustomTextBox>
                        </div>
                        <div class="col-sm-4">
                            <cc:CustomTextBox ID="txtStateRecurringRatio" CssClass="form-control" runat="server"
                                MaxLength="3"></cc:CustomTextBox>
                        </div>
                    </div>
                    <div class="col-sm-12 form-group text-center">
                      
                        <cc:CustomButton ID="btnSubmit" runat="server" Text="Update" CssClass="btn-primary btn"
                            OnClientClick="return confirm('Are you sure? you want to add / edit these ratio!');"
                            OnClick="btnSubmit_Click" />
                        <button id="btnBack" type="button" onclick="window.location.href = 'Home.aspx'" class="btn btn-primary col-sm-offset-1">
                            Back</button>
                    
                    </div>
                  
                </div>
               
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
