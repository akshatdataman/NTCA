<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="DownloadExpenditureReport.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.DownloadExpenditureReport" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>Download Expenditure Reports</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
            <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="col-sm-10 col-sm-offset-1 text-center  form-group">
            <cc:CustomLabel ID="lblReports" runat="server" Text="Select Reports" CssClass="col-sm-2"></cc:CustomLabel>
            <cc:CustomCheckBox ID="chkMonthly" runat="server" Text="Monthly Reports" CssClass="pull-left"
                AutoPostBack="true" OnCheckedChanged="chkMonthly_CheckedChanged" />
            <cc:CustomCheckBox ID="chkPeriodic" runat="server" Text="Periodic Reports" CssClass="pull-left"
                AutoPostBack="true" OnCheckedChanged="chkPeriodic_CheckedChanged" />
        </div>
        <div id="divTable" class="col-sm-12 table-responsive TopMargin text-center">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <cc:CustomGridView ID="gvExpenditureReports" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-bordered tablett" Width="100%" DataKeyNames="ActivityId"
                        AllowPaging="True" PageSize="5" OnPageIndexChanging="gvExpenditureReports_PageIndexChanging"
                        OnPageIndexChanged="gvExpenditureReports_PageIndexChanged" EnableSortingAndPagingCallbacks="true"
                        ShowHeaderWhenEmpty="true">
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                            Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                        <EmptyDataTemplate>
                            <div class="EmpltyGridView">
                                &quot; Sorry! There is no data &quot;</div>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></cc:CustomLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblActivity" runat="server" Text='<%#Eval("ActivityId")%>' Visible="false"></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblActivity" runat="server" Text='<%#Eval("ActivityName")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Items">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblItem" runat="server" Text='<%#Eval("ActivityItemId")%>' Visible="false"></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Financial Year">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblFinancialYear" runat="server" Text='<%#Eval("FinancialYear")%>'></cc:CustomLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblQuantity" runat="server" Text='<%#Eval("NumberOfItems")%>'></cc:CustomLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Physical Progress">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblPhysicalProgress" runat="server" Text='<%#Eval("PhysicalProgress")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Utilized Central Finance">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblCentralFinance" runat="server" Text='<%#Eval("CentralFinancialProgress")%>'></cc:CustomLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Utilized State Finance">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblStateFinance" runat="server" Text='<%#Eval("StateFinancialProgress")%>'></cc:CustomLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Utilized">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblTotal" runat="server" Text='<%#Eval("FinancialTotal")%>'></cc:CustomLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Download">
                                <ItemTemplate>
                                    <cc:CustomImageButton ID="imgbtnExcel" runat="server" OnClientClick="return showconfirm_NonRecuringCore()"
                                        ImageUrl="../Images/sampleExcelIcon.jpg" OnClick="imgbtnExcel_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </cc:CustomGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class=" col-sm-12 TopMargin text-center">
            <cc:CustomButton ID="btnBack" CssClass="btn-primary btn col-sm-offset-0" runat="server"
                PostBackUrl="FieldDirectorHome.aspx" Text="Back" />
        </div>
    </div>
</asp:Content>
