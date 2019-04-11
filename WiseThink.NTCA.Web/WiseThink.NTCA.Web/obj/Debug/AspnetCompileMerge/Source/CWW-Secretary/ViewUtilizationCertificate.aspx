<%@ Page Title="" Language="C#" MasterPageFile="~/CWW-Secretary/CWW-Secretary.master"
    AutoEventWireup="true" CodeBehind="ViewUtilizationCertificate.aspx.cs" Inherits="WiseThink.NTCA.Web.CWW_Secretary.ViewUtilizationCertificate"
    EnableEventValidation="false" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function printUC() {
            var ucData = document.getElementById('<%= ucDiv.ClientID %>');
            var windowUrl = 'about:blank';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
        'left=100,top=100,right=100,bottom=100,width=1200,height=600');
            prtWindow.document.write('<html><head><b><title>Utilization Certificate</title></b></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(ucData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CWWPlaceHolder" runat="server">
    <div id="ucDiv" runat="server" class="row minheight">
        <div class="row">
            <h4>
                <b>View Utilization Certificate</b></h4>
            <cc:CustomImageButton ID="imgbtnPdf" runat="server" ImageUrl="~/Images/samplePDFIcon.png"
                OnClick="imgbtnPdf_click" CssClass="col-sm-offset-9" />
            <cc:CustomImageButton ID="imgbtnPrint" Visible="false" runat="server" ImageUrl="~/Images/btn-print.png"
                OnClientClick="return printUC();" />
        </div>
        <div class="row">
            <div id="DisplayErrorMessage" class="btn-danger" runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="row TopMargin">
            <div id="PFYUcDiv" class="row MarginTop" runat="server">
                <label class="col-sm-3">
                    Upload Signed PFY Final UC :</label>
                <div class="col-sm-4">
                    <cc:CustomFileUpload ID="fuUploadFinalUC" runat="server" CssClass="fileuploadSize" />
                    <cc:CustomLabel ID="lblFileName" runat="server" Text=""></cc:CustomLabel>
                    <asp:LinkButton ID="lbtnDownload" runat="server" CssClass="LinkColor" CommandName="download"></asp:LinkButton>
                    <asp:RegularExpressionValidator runat="server" ID="revfuUc" ControlToValidate="fuUploadFinalUC"
                        ErrorMessage="Allow only .pdf File" ValidationExpression="^.*\.(pdf|PDF)$" CssClass="REerror" />
                </div>
                <div class="col-sm-3 col-sm-offset-0">
                    <asp:LinkButton ID="lbtnFileName" runat="server" CssClass="LinkColor" OnClick="lbtnFileName_Click"></asp:LinkButton>
                </div>
                <div class="col-sm-2">
                    <cc:CustomButton ID="btnUploadFinalUc" runat="server" Text="Upload" CssClass="btn btn-primary"
                        OnClientClick="return confirm('Are you sure? you want to upload previous financial year UC and move to step 4!!');"
                        OnClick="btnUploadFinalUc_Click" />
                </div>
            </div>
        </div>
        <div class="col-sm-10 col-sm-offset-1 TopMargin table-responsive">
            <cc:CustomGridView ID="gvView" runat="server" CssClass="table table-bordered col-sm-12 table-responsive "
                AutoGenerateColumns="False" AllowPaging="true" PageSize="5">
                <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                    Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                <EmptyDataTemplate>
                    <div class="EmpltyGridView">
                        &quot; Sorry! There is no data &quot;</div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Sr.No">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Letter No. and Date">
                        <ItemTemplate>
                            <span id="letter1">GOI letter no: </span><b>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("letter1") %>'></asp:Label></b><br />
                            <br />
                            <span id="Span1">dated: </span><b>
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("letter2") %>'></asp:Label></b><br />
                            <br />
                            <span id="Span2">Fresh Release: </span>
                            <br />
                            <br />
                            <span id="Span4">Unspent Adjustment: </span>
                            <br />
                            <br />
                            <span id="Span5">State Share: </span>
                            <br />
                            <br />
                            <span id="Span6">GOI letter no: </span><b>
                                <asp:Label ID="Label7" runat="server" Text='<%#Eval("letter7") %>'></asp:Label></b><br />
                            <br />
                            <span id="Span7">dated: </span><b>
                                <asp:Label ID="Label8" runat="server" Text='<%#Eval("letter8") %>'></asp:Label></b><br />
                            <br />
                            <span id="Span8">2nd Instalment: </span>
                            <br />
                            <br />
                            <b><span id="Span9">Total: </span><b />
                        </ItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <b>
                                <asp:Label ID="lblAmount1" runat="server" Text='<%#Eval("Amount1") %>'></asp:Label></b><br />
                            <br />
                            <b>
                                <asp:Label ID="lblAmount2" runat="server" Text='<%#Eval("Amount2") %>'></asp:Label></b><br />
                            <br />
                            <b>
                                <asp:Label ID="lblAmount3" runat="server" Text='<%#Eval("Amount3") %>'></asp:Label></b><br />
                            <br />
                            <b>
                                <asp:Label ID="lblAmount4" runat="server" Text='<%#Eval("Amount4") %>'></asp:Label></b><br />
                            <br />
                            <b>
                                <asp:Label ID="lblAmount5" runat="server" Text='<%#Eval("Amount5") %>'></asp:Label></b><br />
                            <br />
                            <b>
                                <asp:Label ID="lblAmount6" runat="server" Text='<%#Eval("Amount6") %>'></asp:Label></b><br />
                            <br />
                            <b>
                                <asp:Label ID="lblAmount7" runat="server" Text='<%#Eval("Amount7") %>'></asp:Label></b><br />
                            <br />
                            <b>
                                <asp:Label ID="lblAmount8" runat="server" Text='<%#Eval("Amount8") %>'></asp:Label></b><br />
                            <br />
                            <b>
                                <asp:Label ID="lblAmount9" runat="server" Text='<%#Eval("Amount9") %>'></asp:Label></b><br />
                            <br />
                        </ItemTemplate>
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="5" CssClass="form-control"
                                runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </cc:CustomGridView>
        </div>
        <div class="col-sm-12 ">
            <span>Certified that I have satisfied myself that the condition on which the grants-in-aid
                was sanctioned has been duly fulfilled and that I have exercised following check
                to see that the money was actually utilied for the purpose for which it was sanctioned.
            </span>
            <br />
            <br />
            <span>Kinds of checks exercised:<br />
                <br />
                1. The works was executed as per the norms and procedure of Forest Department.<br />
                2. The Works are executed by the Range officers randomly by the Field Director,
                the Depty Director and the ACF of the Manas Tiger Reserve in the field.<br />
                3. The Monitoring and Evaluation Wings of the Department headed by CCF rank and
                the P & D Department of the Govt. of Assam also conduct random physical checking
                of the works in the field.<br />
                4. The Accounts are the subject to internal Audit. The Final Account are being audited
                by the Accountant General.<br />
                <br />
            </span>
            <div class="col-sm-12 text-center">
                <cc:CustomButton ID="btnAddNew" CssClass="btn-primary btn col-sm-offset-0" runat="server"
                    PostBackUrl="Home.aspx" Text="Back" />
            </div>
        </div>
        <div class="col-sm-12 TopMargin">
        </div>
    </div>
</asp:Content>
