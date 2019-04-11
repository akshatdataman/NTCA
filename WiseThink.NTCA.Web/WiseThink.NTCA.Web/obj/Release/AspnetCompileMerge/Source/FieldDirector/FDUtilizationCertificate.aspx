<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="FDUtilizationCertificate.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.FDUtilizationCertificate" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>Utilization Certificate</b></h4>
        <div class="col-sm-2">
            <div id="leftPartMy1" class="">
                <div id="leftNav">
                    <div class="panel-group" id="accordion">
                        <div class=" panel-default ">
                            <div id="panel1" class="panel-collapse collapse in menuleft">
                                <div class="table-responsive">
                                    <ul>
                                        <li class="FirstSubMenu1 BackgroundColor NonRecuring_left Links"><a class="anchorlink" href="SubmitAPONew.aspx">Step 1: Non-Recurring</a></li>
                                                <li class="BackgroundColor Links Recuring_left"><a class="anchorlink" href="SubmitAPONew.aspx">Step 2: Recurring</a></li>
                                      
                                        <li class="BackgroundColor  Current"><a href="FDUtilizationCertificate.aspx">Step 3: Utilization Certificate</a></li>
                                      
                                        <li class="BackgroundColor "><a href="ObligationFD.aspx">Step 4: Obligation Under Tri-MOU</a></li>
                                       
                                        <li class="BackgroundColor"><a href="CheckList.aspx?callfrom=SubmitAPO">Step 5: Check and Submit</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-10  leftBorder minHeight Home_margin" id="MainContent">
            <div class=" table-responsive">
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
                    Upload PFY Final UC :</label>
                
                    <div class="col-sm-4">
                        <cc:CustomFileUpload ID="fuUploadFinalUC" runat="server" CssClass="fileuploadSize" />
                        <cc:CustomLabel ID="lblFileName" runat="server" Text=""></cc:CustomLabel>
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="LinkColor" CommandName="download"></asp:LinkButton>
                        <asp:RegularExpressionValidator runat="server" ID="revfuUc" ControlToValidate="fuUploadFinalUC"
                            ErrorMessage="Allow only .pdf File" ValidationExpression="^.*\.(pdf|PDF)$"
                            CssClass="REerror" />
                            
                    </div>
                    <div class="col-sm-3 col-sm-offset-0">
                    <asp:LinkButton ID="lbtnFileName" runat="server" CssClass="LinkColor" OnClick="lbtnFileName_Click"></asp:LinkButton>
                    </div>
                    <div class="col-sm-2">
                    <cc:CustomButton ID="btnUploadFinalUc" runat="server" Text="Upload" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure? you want to upload previous financial year UC and move to step 4!!');" OnClick="btnUploadFinalUc_Click" />
                </div>
                </div>
                    <div>
                        <asp:Button ID="btnGenerateUC" runat="server" Text="Click here to Generate UC" CssClass="btn btn-primary"
                            OnClick="btnGenerateUC_Click" />
                        <span><b>(Consolidated Data of Quarterly Reports submitted till date)</b></span>
                    </div>
                </div>
                <asp:UpdatePanel ID="upnlObligation" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <%--<div class="row">
                            <div>
                                <asp:Button ID="btnGenerateUC" runat="server" Text="Click here to Generate UC" CssClass="btn btn-primary"
                                    OnClick="btnGenerateUC_Click" />
                                <span><b>(Consolidated Data of Quarterly Reports submitted till date)</b></span>
                            </div>
                        </div>--%>
                        <cc:CustomGridView ID="gvUtilizationCertificate" runat="server" CssClass="table  col-sm-12 TopMargin"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="5">
                            <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                            <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                            <EmptyDataTemplate>
                                <div class="EmpltyGridView">
                                    &quot; Sorry! There is no data &quot;</div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Released Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSanctionAmount" runat="server" Text='<%#Eval("SanctionAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sanction On" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSanctionOn" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UC Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spent Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpentAmount" runat="server" Text='<%#Eval("SpentAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unspent Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnspentAmount" runat="server" Text='<%#Eval("UnspentAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actiom">
                                    <ItemTemplate>
                                        <div class="col-sm-12">
                                            <a href="ViewUtilizationCertificate.aspx">
                                                <input type="button" class="btn btn-primary" value="View File" />
                                            </a>
                                        </div>
                                        <div class="col-sm-12 TopMargin">
                                            <div class="col-sm-8 lessmargin ">
                                                <asp:FileUpload ID="fuUpload" runat="server" />
                                                <asp:RegularExpressionValidator runat="server" ID="rvafuPdfFile" ControlToValidate="fuUpload"
                                                    ErrorMessage="Invalid File" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.png|.jpg|.jpeg|.gif|.txt|.TXT|.xls|.XLS|.xlsx|.XLSX|.doc|.DOC|.docx|.DOCX|.pdf|.PDF|.ppt|.PPT|.pptx|.PPTX|.kml|.KML)$"
                                                    CssClass="REerror" />
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Upload"
                                                    OnClick="btnUpload_Click" />
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="220px" />
                                </asp:TemplateField>
                            </Columns>
                        </cc:CustomGridView>
                        </div>
                        <div id="SettledUcDiv" runat="server" class="col-sm-12">
                            <div class="bottomMargin TopMargin">
                                <cc:CustomButton ID="btnSettledUc" runat="server" Text="Click Here To Settled  Unspent Amount"
                                    CssClass="btn btn-primary" OnClick="btnSettledUc_Click" />
                                <cc:CustomButton ID="btnBack" CssClass="btn-primary btn col-sm-offset-1" runat="server"
                                    PostBackUrl="FieldDirectorHome.aspx" Text="Back" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-sm-12 text-center">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
