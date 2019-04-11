<%@ Page Title="" Language="C#" MasterPageFile="~/CWW-Secretary/CWW-Secretary.master"
    AutoEventWireup="true" CodeBehind="ViewAPO.aspx.cs" Inherits="WiseThink.NTCA.Web.CWW_Secretary.ViewAPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= gvNonRecurring.ClientID %>');
            var windowUrl = 'about:blank';
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
        'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html><head><b><title>APO</title></b></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CWWPlaceHolder" runat="server">
    <div class="row minheight">
        <div class="row">
            <h4 id="SubmitApoHeader" runat="server">
                <b>View Current Financial Year Apo</b>
                <cc:CustomImageButton ID="imgbtnWord" runat="server" ImageUrl="~/Images/Word-icon.png"
                    OnClientClick="RemoveBlockUi();" OnClick="imgbtnWord_click" CssClass="col-sm-offset-7" />
                <cc:CustomImageButton ID="imgbtnPdf" runat="server" ImageUrl="~/Images/samplePDFIcon.png"
                    OnClientClick="RemoveBlockUi();" OnClick="imgbtnPdf_click" CssClass="" />
                <cc:CustomImageButton ID="imgbtnExcel" runat="server" ImageUrl="~/Images/sampleExcelIcon.jpg"
                    OnClientClick="RemoveBlockUi();" OnClick="imgbtnExcel_click" CssClass="" />
                <cc:CustomImageButton ID="imgbtnPrint" Visible="false" runat="server" ImageUrl="~/Images/btn-print.png"
                    OnClientClick="return printGrid();" />
            </h4>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="col-sm-12 minHeight" id="MainContent" runat="server">
                    <div class="row text-center">
                        <h3 id="ApoTitle" runat="server"></h3>
                    </div>
                    <!-- Non-Recurring-->
                    <div id="Non-RecurringDiv">
                        <div class="headingCss">
                            <h3 id="Group1Title" runat="server">Non-Recurring (Core + Buffer)</h3>
                        </div>
                        <div style="overflow: auto">
                            <cc:CustomGridView ID="gvNonRecurring" runat="server" AutoGenerateColumns="false"
                                ShowHeader="false" OnRowCreated="gvNonRecurring_RowCreated" CssClass="table table-bordered table-responsive tablett"
                                AllowPaging="false" PageSize="5" ShowFooter="true" OnRowDataBound="gvNonRecurring_RowDataBound">
                                <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                    Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                <EmptyDataTemplate>
                                    <div class="EmpltyGridView">
                                        &quot; Sorry! There is no data &quot;
                                    </div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Activity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ActivityItemId" Visible="false">
                                        <ItemTemplate>
                                            <cc:CustomLabel ID="lblActivityItemId" runat="server" Text='<%#Eval("ActivityItemId")%>'></cc:CustomLabel>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item" ControlStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <b>
                                                <asp:Label ID="lblSubTotal" runat="server" Text="Total (Non-Recurring):"></asp:Label></b>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Para No. CSS PT Guidelines">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCssPT" runat="server" Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Para No. TCP">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParaNoTCP" runat="server" Text='<%#Eval("ParaNoTCP") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount released PFY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPFYAmount" runat="server" Text='<%#Eval("PFYTotal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. of Items (Physical Target)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumberOfItems" runat="server" Text='<%#Eval("NumberOfItems") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub-item Details" ControlStyle-Width="250px">
                                        <ItemTemplate>
                                            <div id="SubItemDetailsdiv" runat="server">
                                                <ul id="SubItemDetailsUl" style="-webkit-padding-start: 10px;" runat="server">
                                                </ul>
                                            </div>
                                            <%--<asp:DetailsView ID="SubItemDetailsView" runat="server" CssClass="table table-bordered table-hover"
                                                        BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" BorderStyle="Groove" AutoGenerateRows="False">
                                                        <Fields>
                                                            <asp:BoundField DataField="SubItemDetails" />
                                                        </Fields>
                                                    </asp:DetailsView>--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="100%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification / Description" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Specification") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>' Visible="false"></asp:Label>
                                            <div id="UnitPricediv" runat="server">
                                                <ul id="UnitPriceUl" style="-webkit-padding-start: 10px;" runat="server">
                                                </ul>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total (Financial Target)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="txtSubTotal" runat="server" Text='<%#Eval("NRTotal") %>'></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GPS" ControlStyle-Width="130px">
                                        <ItemTemplate>
                                            <%--<cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                        Width="100%" DataKeyNames="ActivityItemId" AllowPaging="false" PageSize="5" EnableSortingAndPagingCallbacks="false"
                                        ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                                <ItemTemplate>
                                                    <%#Eval("RowNumber")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GPS Details">
                                                <ItemTemplate>
                                                    <cc:CustomLabel ID="lblGPS" runat="server" Text='<%#Eval("GPS")%>'></cc:CustomLabel>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>--%>
                                            <div id="GpsDetailsdiv" runat="server">
                                                <ul id="GpsDetailsUl" style="-webkit-padding-start: 10px;" runat="server">
                                                </ul>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Justification" ControlStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJustification" runat="server" Text='<%#Eval("Justification") %>' CssClass="word_wrap"></asp:Label>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>
                        </div>
                        <div class="table-responsive tableWidthAPO">
                        </div>
                    </div>
                    <div id="RecuringDiv" runat="server">
                        <div>
                            <div class="headingCss">
                                <h3 id="Grouptitle2" runat="server">Recurring (Core + Buffer)</h3>
                            </div>
                            <!--Here is Recuring-->
                            <div style="overflow: auto">
                                <cc:CustomGridView ID="gvRecuring" runat="server" AutoGenerateColumns="false" ShowHeader="false" ShowFooter="true"
                                    AllowPaging="false" PageSize="5" OnRowCreated="gvRecuring_RowCreated" CssClass="table table-bordered table-responsive tablett " OnRowDataBound="gvRecuring_RowDataBound">
                                    <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                        Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                    <EmptyDataTemplate>
                                        <div class="EmpltyGridView">
                                            &quot; Sorry! There is no data &quot;
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Activity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ActivityItemId" Visible="false">
                                            <ItemTemplate>
                                                <cc:CustomLabel ID="lblActivityItemId" runat="server" Text='<%#Eval("ActivityItemId")%>'></cc:CustomLabel>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item" ControlStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <b>
                                                    <asp:Label ID="lblSubTotal" runat="server" Text="Total (Recurring):"></asp:Label></b>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Para No. CSS PT Guidelines">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCssPT" runat="server" Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Para No. TCP">
                                            <ItemTemplate>
                                                <asp:Label ID="lblParaNoTCP" runat="server" Text='<%#Eval("ParaNoTCP") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount released PFY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPFYAmount" runat="server" Text='<%#Eval("PFYTotal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of Items (Physical Target)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumberOfItems" runat="server" Text='<%#Eval("NumberOfItems") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub-item Details" ControlStyle-Width="250px">
                                            <ItemTemplate>
                                                <div id="SubItemDetailsdiv" runat="server">
                                                    <ul id="SubItemDetailsUl" style="-webkit-padding-start: 10px;" runat="server">
                                                    </ul>
                                                </div>
                                                <%--<asp:DetailsView ID="SubItemDetailsView" runat="server" CssClass="table table-bordered table-hover"
                                                        BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" BorderStyle="Groove" AutoGenerateRows="False">
                                                        <Fields>
                                                            <asp:BoundField DataField="SubItemDetails" />
                                                        </Fields>
                                                    </asp:DetailsView>--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="100%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specification / Description" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Specification") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>' Visible="false"></asp:Label>
                                                <div id="UnitPricediv" runat="server">
                                                    <ul id="UnitPriceUl" style="-webkit-padding-start: 10px;" runat="server">
                                                    </ul>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total (Financial Target)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="txtSubTotal" runat="server" Text='<%#Eval("RTotal") %>'></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GPS" ControlStyle-Width="130px">
                                            <ItemTemplate>
                                                <%--<cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                        Width="100%" DataKeyNames="ActivityItemId" AllowPaging="false" PageSize="5" EnableSortingAndPagingCallbacks="false"
                                        ShowHeader="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                                <ItemTemplate>
                                                    <%#Eval("RowNumber")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GPS Details">
                                                <ItemTemplate>
                                                    <cc:CustomLabel ID="lblGPS" runat="server" Text='<%#Eval("GPS")%>'></cc:CustomLabel>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>--%>
                                                <div id="GpsDetailsdiv" runat="server">
                                                    <ul id="GpsDetailsUl" style="-webkit-padding-start: 10px;" runat="server">
                                                    </ul>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Justification" ControlStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJustification" runat="server" CssClass="word_wrap" Text='<%#Eval("Justification") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </cc:CustomGridView>
                            </div>
                        </div>
                    </div>

                    <div id="ApoAmountBreakupDiv" runat="server">
                        <div>
                            <div class="headingCss">
                                <h3 id="H1" runat="server">APO Amount Breakup</h3>
                            </div>
                            <!--Here is Recuring-->
                            <div style="overflow: auto">
                                <cc:CustomGridView ID="gvApoAmountBreakup" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                    PageSize="10" CssClass="table table-bordered table-responsive tablett" OnRowCreated="gvApoAmountBreakup_RowCreated"
                                    ShowHeader="false" ShowFooter="true" Width="100%" ShowHeaderWhenEmpty="true" OnRowDataBound="gvApoAmountBreakup_RowDataBound">
                                    <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                        Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                    <EmptyDataTemplate>
                                        <div class="EmpltyGridView">
                                            &quot; Sorry! There is no data &quot;
                                        </div>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="" FooterStyle-BackColor="lightgray">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSRNo" runat="server" Text='<%#Eval("SrNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <b>
                                                    <asp:Label ID="lblSr" runat="server" Text='<%#Eval("Sr") %>'></asp:Label>
                                                </b>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Central Share(50)" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle" FooterStyle-BackColor="lightgray">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCentralShareCore" runat="server" Text='<%#Eval("RCentralShare") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <b>
                                                    <asp:Label ID="lblRCentral" runat="server" Text='<%#Eval("RCentral") %>'></asp:Label>
                                                </b>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State Share(50)" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle" FooterStyle-BackColor="lightgray">
                                            <ItemTemplate>
                                                <cc:CustomLabel ID="lblStateShareCore" runat="server" Text='<%#Eval("RStateShare")%>'></cc:CustomLabel>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <b>
                                                    <asp:Label ID="lblRState" runat="server" Text='<%#Eval("RState") %>'></asp:Label>
                                                </b>
                                            </FooterTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Central Share (60)" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle" FooterStyle-BackColor="lightgray">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCentralShareBuffer" runat="server" Text='<%#Eval("NRCentralShare") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <b>
                                                    <asp:Label ID="lblNRCentral" runat="server" Text='<%#Eval("NRCentral") %>'></asp:Label>
                                                </b>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State Share(40)" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle" FooterStyle-BackColor="lightgray">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStateShareBuffer" runat="server" Text='<%#Eval("NRStateShare") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <b>
                                                    <asp:Label ID="lblNRState" runat="server" Text='<%#Eval("NRState") %>'></asp:Label>
                                                </b>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle" FooterStyle-BackColor="lightgray">
                                            <ItemTemplate>
                                                <b>
                                                    <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total") %>'></asp:Label></b>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <b>
                                                    <asp:Label ID="lblGrandTotal1" runat="server" Text='<%#Eval("GTotal") %>'></asp:Label>
                                                </b>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </cc:CustomGridView>
                            </div>
                        </div>
                    </div>

                    <div id="GrandTotal" runat="server" class="form-group row">
                        <b>
                            <label for="GrandTotalLabel" class="col-sm-3 control-label">
                                Total (Non-Recurring + Recurring):</label></b>
                        <div class="col-sm-4">
                            <b><cc:CustomLabel CssClass="text-left" ID="lblRorNRAmount" runat="server"></cc:CustomLabel>
                            
                                <cc:CustomLabel CssClass="text-left" ID="lblGrandTotal" runat="server"></cc:CustomLabel></b>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="text-center TopMargin">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary col-sm-offset-1"
                OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>
