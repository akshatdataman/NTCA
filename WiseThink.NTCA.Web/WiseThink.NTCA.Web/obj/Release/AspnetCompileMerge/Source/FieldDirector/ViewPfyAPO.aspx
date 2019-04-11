<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="ViewPfyAPO.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.ViewPfyAPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function printGrid() {
            var gridData = document.getElementById('<%= gvNonRecurring.ClientID %>');
            var windowUrl = 'about:blank';
            //set print document name for gridview
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
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div class="row minheight">
        <div class="row">
            <h4 id="SubmitApoHeader" runat="server">
                <b>View Previous Financial Year Apo</b>
                <cc:CustomImageButton ID="imgbtnWord" runat="server" ImageUrl="~/Images/Word-icon.png"
                    OnClick="imgbtnWord_click" CssClass="col-sm-offset-7" />
                <cc:CustomImageButton ID="imgbtnPdf" runat="server" ImageUrl="~/Images/samplePDFIcon.png"
                    OnClick="imgbtnPdf_click" CssClass="" />
                <cc:CustomImageButton ID="imgbtnExcel" runat="server" ImageUrl="~/Images/sampleExcelIcon.jpg"
                    OnClick="imgbtnExcel_click" CssClass="" />
                <cc:CustomImageButton ID="imgbtnPrint" Visible="false" runat="server" ImageUrl="~/Images/btn-print.png"
                    OnClientClick="return printGrid();" />
            </h4>
        </div>
        <div class="col-sm-12  leftBorder minHeight" id="MainContent" runat="server">
            <div class="row text-center">
                <h3 id="ApoTitle" runat="server">
                </h3>
            </div>
            <!-- Non-Recurring-->
            <div id="Non-RecurringDiv">
                <div class="headingCss">
                    <h3>
                        Non-Recurring</h3>
                </div>
                <div>
                    <cc:CustomGridView ID="gvNonRecurring" runat="server" AutoGenerateColumns="false"
                        AllowPaging="true" PageSize="10" CssClass="table table-bordered table-responsive tablett ">
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                            Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                        <EmptyDataTemplate>
                            <div class="EmpltyGridView">
                                &quot; Sorry! There is no data &quot;</div>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Activity">
                                <ItemTemplate>
                                    <asp:Label ID="lblRActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                </ItemTemplate>
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
                            <asp:TemplateField HeaderText="No. of Items (Physical Target)">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumberOfItems" runat="server" Text='<%#Eval("NumberOfItems") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Specification / Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Specification") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Price">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PFY Total (Financial Target)">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("PFYTotal") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GPS">
                                <ItemTemplate>
                                    <asp:Label ID="lblGPS" runat="server" Text='<%#Eval("GPS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Justification">
                                <ItemTemplate>
                                    <asp:Label ID="lblJustification" runat="server" Text='<%#Eval("Justification") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Upload Document">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocument" runat="server" Text='<%#Eval("Document") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                        </Columns>
                    </cc:CustomGridView>
                </div>
                <div class="table-responsive tableWidthAPO">
                </div>
            </div>
            <div id="RecuringDiv" runat="server">
                <div>
                    <div class="headingCss">
                        <h3>
                            Recurring</h3>
                    </div>
                    <!--Here is Recuring-->
                    <div>
                        <cc:CustomGridView ID="gvRecuring" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-responsive tablett"
                            AllowPaging="true" PageSize="10">
                            <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                            <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                            <EmptyDataTemplate>
                                <div class="EmpltyGridView">
                                    &quot; Sorry! There is no data &quot;</div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Activity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                    </ItemTemplate>
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
                                <asp:TemplateField HeaderText="Specification / Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Specification") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" PFY Total (Financial Target)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("PFYTotal") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GPS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGPS" runat="server" Text='<%#Eval("GPS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Justification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJustification" runat="server" Text='<%#Eval("Justification") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Upload Document">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocument" runat="server" Text='<%#Eval("Document") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                            </Columns>
                        </cc:CustomGridView>
                    </div>
                </div>
            </div>
            <%--<div id="echoDevelopmentDiv" runat="server">
                <div>
                    <div class="headingCss">
                        <h3>
                            Eco-Development</h3>
                    </div>
                    <div>
                        <cc:CustomGridView ID="gvEcoDevelopment" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-bordered table-responsive tablett ">
                            <Columns>
                                <asp:TemplateField HeaderText="Activity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                    </ItemTemplate>
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
                                <asp:TemplateField HeaderText="No. of Items (Physical Target)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumberOfItems" runat="server" Text='<%#Eval("NumberOfItems") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specification / Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpecification" runat="server" Text='<%#Eval("Specification") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PFY Total (Financial Target)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("PFYTotal") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GPS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGPS" runat="server" Text='<%#Eval("GPS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Justification">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJustification" runat="server" Text='<%#Eval("Justification") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </cc:CustomGridView>
                    </div>
                </div>
            </div>--%>
        </div>
        <div class="text-center TopMargin">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary col-sm-offset-1"
                OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>
