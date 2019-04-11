<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="GenerateNTCAReports.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.GenerateNTCAReports" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div class="row minheight">
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
                <h4>
                    <b>Generate Reports</b></h4>
                <div class="text-center col-sm-12 ">
                    <div id="DisplayErrorMessage" class="btn-danger " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                </div>
                <div class="col-sm-12 MarginTop">
                    <div class="col-sm-offset-2 col-sm-2">
                        <cc:CustomLabel ID="lblReportsType" runat="server" Text="Select Reports Type"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4">
                        <cc:CustomDropDownList ID="ddlReportsType" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="ddlReportsType_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem>--Select Reports Type--</asp:ListItem>
                            <asp:ListItem>Activity Wise</asp:ListItem>
                            <asp:ListItem>Activity Item Wise</asp:ListItem>
                            <asp:ListItem>Sub Item Wise</asp:ListItem>
                            <%--<asp:ListItem>APO Sanction Status</asp:ListItem>--%>
                            <asp:ListItem>Amount Release & Utilize State Wise</asp:ListItem>
                            <asp:ListItem>Tiger Reserve Wise</asp:ListItem>
                            <asp:ListItem>State Wise</asp:ListItem>
                        </cc:CustomDropDownList>
                    </div>
                </div>
                <div id="FinancialYear" runat="server" class="col-sm-12 MarginTop" visible="false">
                    <div class="col-sm-2">
                        <cc:CustomLabel ID="lblfromYear" runat="server" Text="From Financial Year"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtFromYear" runat="server" CssClass="form-control" placeholder="From Year"></cc:CustomTextBox>
                    </div>
                    <div class="col-sm-2">
                        <cc:CustomLabel ID="lblToYear" runat="server" Text="To Financial Year"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4">
                        <cc:CustomTextBox ID="txtToYear" runat="server" CssClass="form-control" placeholder="To Year"></cc:CustomTextBox>
                    </div>
                </div>
                <div id="Items" runat="server" class="col-sm-12 MarginTop" visible="false">
                    <div class="col-sm-offset-2 col-sm-2">
                        <cc:CustomLabel ID="lblActivity" runat="server" Text="Select Activity"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4">
                        <cc:CustomDropDownList ID="ddlActivity" runat="server" CssClass="form-control controlwidth">
                        </cc:CustomDropDownList>
                        <asp:RequiredFieldValidator ID="ddlActivityRequired" runat="server" ControlToValidate="ddlActivity"
                        ForeColor="#FF3300" ErrorMessage="Activity is required.">* Activity is required.</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div id="Activity" runat="server" class="col-sm-12 MarginTop" visible="false">
                    <div class="col-sm-offset-2 col-sm-2">
                        <cc:CustomLabel ID="lblActivityItem" runat="server" Text="Select Activity Item"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4" >
                        <cc:CustomDropDownList ID="ddlActivityItem" runat="server" CssClass="form-control " OnSelectedIndexChanged="ddlActivityItem_SelectedIndexChanged"
                            AutoPostBack="true" EnableViewState ="true" >
                        </cc:CustomDropDownList>
                        <asp:RequiredFieldValidator ID="ddlActivityItemRequired" runat="server" ControlToValidate="ddlActivityItem"
                        ForeColor="#FF3300" ErrorMessage="Activity item is required.">* Activity Item is required.</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div id="SubItems" runat="server" class="col-sm-12 MarginTop" visible="false">
                    <div class=" col-sm-offset-2 col-sm-2">
                        <cc:CustomLabel ID="lblSubItem" runat="server" Text="Select Sub Item"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4">
                        <cc:CustomDropDownList ID="ddlSubItem" runat="server" CssClass="form-control controlwidth">
                        </cc:CustomDropDownList>
                        <asp:RequiredFieldValidator ID="ddlSubItemRequired" runat="server" ControlToValidate="ddlSubItem"
                        ForeColor="#FF3300" ErrorMessage="Sub item is required.">* Sub Item is required.</asp:RequiredFieldValidator>
                    </div>
                </div>
         <%--   </ContentTemplate>
        </asp:UpdatePanel>--%>
        <div id="GenerateReport" runat="server" class="col-sm-12 text-center" visible="true">
            <cc:CustomButton ID="btnGenerateReports" CssClass="btn-primary btn TopMargin" runat="server"
                Text="Generate Report" OnClick="btnGenerateReports_Click" />
            <button id="btnBack" type="button" onclick="window.location.href = 'Home.aspx'" class="btn btn-primary TopMargin">
                Back</button>
        </div>
        <div id="divTable" class="col-sm-12 table-responsive TopMargin text-center">
              <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <cc:CustomGridView ID="gvItemReports" runat="server" AutoGenerateColumns="false"
                CssClass="table table-bordered tablett" Width="100%" DataKeyNames="TigerReserveId"
                AllowPaging="True" PageSize="5" OnPageIndexChanging="gvItemReports_PageIndexChanging"
                OnPageIndexChanged="gvItemReports_PageIndexChanged" ShowHeaderWhenEmpty="true">
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
                            <%#Eval("RowNumber")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tiger Reserve">
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblTigerReserveId" runat="server" Text='<%#Eval("TigerReserveId")%>'
                                Visible="false"></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblTigerReserveId" runat="server" Text='<%#Eval("TigerReserveName")%>'></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="State">
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblStateId" runat="server" Text='<%#Eval("StateId")%>' Visible="false"></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblState" runat="server" Text='<%#Eval("StateName")%>'></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Sanction (In Lakhs)</br>2016-17">
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblSanction" runat="server"  Text='<%#Eval("SanctionedAmount","{0:#0.000}")%>'></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                </Columns>
                  <EmptyDataTemplate>
                            <span class="text-danger">Sorry ! There is no Data to Display</span>
                        </EmptyDataTemplate>
            </cc:CustomGridView>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="divStateWise" runat="server" class="col-sm-12 table-responsive TopMargin text-center">
              <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <cc:CustomGridView ID="gvStateWiseReport" runat="server" AutoGenerateColumns="false"
                CssClass="table table-bordered tablett" Width="100%" DataKeyNames="StateId"
                AllowPaging="True" PageSize="5" OnPageIndexChanging="gvStateWiseReport_PageIndexChanging"
                OnPageIndexChanged="gvStateWiseReport_PageIndexChanged" ShowHeaderWhenEmpty="true">
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
                            <%#Eval("RowNumber")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="State">
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblStateId" runat="server" Text='<%#Eval("StateId")%>' Visible="false"></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblState" runat="server" Text='<%#Eval("StateName")%>'></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tiger Reserve">
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblTigerReserveId" runat="server" Text='<%#Eval("TigerReserveId")%>'
                                Visible="false"></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblTigerReserveId" runat="server" Text='<%#Eval("TigerReserveName")%>'></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Sanction (In Lakhs)</br>2016-17">
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblSanction" runat="server" Text='<%#Eval("SanctionedAmount")%>'></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                </Columns>
                  <EmptyDataTemplate>
                            <span class="text-danger">Sorry ! There is no Data to Display</span>
                        </EmptyDataTemplate>
            </cc:CustomGridView>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
