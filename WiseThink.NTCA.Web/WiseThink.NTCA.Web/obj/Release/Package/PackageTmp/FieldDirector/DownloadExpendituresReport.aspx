<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DownloadExpendituresReport.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.DownloadExpendituresReport" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div class="row form-group minheight">
        <h4>
            <b>Download Expenditure Report</b>
            <cc:CustomImageButton ID="imgbtnExcel" runat="server" ImageUrl="~/Images/sampleExcelIcon.jpg" CssClass="col-sm-offset-8"
                OnClientClick="RemoveBlockUi();" OnClick="imgbtnExcel_Click" />
        </h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
            <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="col-sm-12 text-center  form-group">
            <%--<cc:CustomLabel ID="lblReports" runat="server" Text="Select Expenditure" CssClass="col-sm-4 col-sm-offset-2"></cc:CustomLabel>
            <cc:CustomCheckBox ID="chkMonthly" runat="server" Text="Monthly Reports" CssClass="pull-left col-sm-4"
                AutoPostBack="true" OnCheckedChanged="chkMonthly_CheckedChanged" />
            <cc:CustomCheckBox ID="chkPeriodic" runat="server" Text="Periodic Reports" CssClass="pull-left col-sm-4"
                AutoPostBack="true" OnCheckedChanged="chkPeriodic_CheckedChanged" />--%>
            <cc:CustomRadioButton ID="rdoMonthly" runat="server" GroupName="select" Text="Monthly Expenditure" CssClass="col-sm-2 col-sm-offset-4"
                AutoPostBack="true" OnCheckedChanged="rdoMonthly_CheckedChanged" />
            <cc:CustomRadioButton ID="rdoPeriodic" runat="server" GroupName="select" Text="Periodic Expenditure" CssClass="pull-left col-sm-2"
                AutoPostBack="true" OnCheckedChanged="rdoMonthly_CheckedChanged" />

        </div>
        <div class="col-sm-8 col-sm-offset-3  form-group" id="dvMonth" runat="server" visible ="false">
            <cc:CustomLabel ID="SelectMonth" runat="server" CssClass="col-sm-2" Text="Select Month"></cc:CustomLabel>
            <%--<cc:CustomDropDownList ID="ddlMonth" runat="server" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></cc:CustomDropDownList>--%>
            <div class="col-sm-6">
            <cc:CustomDropDownList ID="ddlMonth" CssClass="form-control textwidh" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                <asp:ListItem Value="0">--Select Month--</asp:ListItem>
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </cc:CustomDropDownList>
                            </div>
        </div>
        <div class="col-sm-8 col-sm-offset-2 text-center  form-group" id="dvPeriodic" runat="server" visible="false">
            <cc:CustomLabel ID="lblFromDate" runat="server" Text="Select From Date: " CssClass="col-sm-2"></cc:CustomLabel>
            <div class="col-sm-4">
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control cal" OnTextChanged="txtFromDate_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>

            <cc:CustomLabel ID="lblToDate" runat="server" Text="Select To Date: " CssClass="col-sm-2"></cc:CustomLabel>
            <div class="col-sm-4">
            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control cal" OnTextChanged="txtToDate_TextChanged" AutoPostBack ="true"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-12 form-group" id="MainContent" runat="server">
            <div id="divTable" class="col-sm-12 table-responsive form-group TopMargin text-center">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cc:CustomGridView ID="gvExpenditureReports" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-bordered tablett" Width="100%"
                            AllowPaging="False" PageSize="5" OnPageIndexChanging="gvExpenditureReports_PageIndexChanging"
                            OnPageIndexChanged="gvExpenditureReports_PageIndexChanged"
                            ShowHeaderWhenEmpty="true">
                            <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                            <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                            <EmptyDataTemplate>
                                <div class="EmpltyGridView">
                                    &quot; Sorry! There is no data &quot;
                                </div>
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
                                <asp:TemplateField HeaderText="Download" Visible="false">
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
        </div>
        <div class="col-sm-12 bottom text-center">
            <cc:CustomButton ID="btnBack" CssClass="btn-primary btn " runat="server"
                PostBackUrl="FieldDirectorHome.aspx" Text="Back" />
        </div>
    </div>
</asp:Content>
