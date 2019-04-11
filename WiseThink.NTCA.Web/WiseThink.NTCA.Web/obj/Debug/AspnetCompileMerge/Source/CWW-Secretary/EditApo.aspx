<%@ Page Title="" Language="C#" MasterPageFile="~/CWW-Secretary/CWW-Secretary.master"
    AutoEventWireup="true" CodeBehind="EditApo.aspx.cs" Inherits="WiseThink.NTCA.Web.CWW_Secretary.EditApo" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquerysession.js" type="text/javascript"></script>
    <script src="../js/Remove.js" type="text/javascript"></script>
    <script type="text/javascript">
        $('.dropdown').hover(function () {
            $(this).find('.dropdown-menu').first().stop(true, true).slideDown(150);
        }, function () {
            $(this).find('.dropdown-menu').first().stop(true, true).slideUp(105)
        });
    </script>
    <script type="text/javascript">
        $(window).load(function () {
            GetSession();
            rightshowhide();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("#MainContent > div:gt(0)").hide();
            $("#leftNav ul a").on("click", function (e) {
                var col = jQuery('col');

                var href = $(this).attr("href");
                var string = href.substring(1, href.length - 1);
                $('.Current').removeClass('Current');
                $('a[href=' + href + ']').parent().addClass('Current');

                $("#MainContent > " + href).show();

                $("#MainContent > :not(" + href + ")").hide();
            });
        });
    </script>
    <script type="text/javascript">
        var selectIds = $('#panel1,#panel2,#panel3,#panel4');
        $(function ($) {
            selectIds.on('show.bs.collapse hidden.bs.collapse', function () {
                $(this).prev().find('.glyphicon').toggleClass('glyphicon-minus glyphicon-plus');
            })
        });
        function blinker() {
            $('.blinking').fadeOut(200);
            $('.blinking').fadeIn(200);
        }
        setInterval(blinker, 6000);
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CWWPlaceHolder" runat="server">
<div id="detailModal" class="modal fade" role="dialog">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                                CSS PT Guidelines View</h4>
                        </div>
                        <div class="modal-body">
                            <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-hover"
                                BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" FieldHeaderStyle-Font-Bold="true"
                                FieldHeaderStyle-BackColor="LavenderBlush" FieldHeaderStyle-ForeColor="Black"
                                BorderStyle="Groove" AutoGenerateRows="False">
                                <Fields>
                                    <asp:BoundField DataField="CSSPTParaNumber" HeaderText="CSS-PT Para Number" />
                                    <asp:BoundField DataField="CSSPTGuideline" HeaderText="CSS-PT Guideline" />
                                </Fields>
                            </asp:DetailsView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="gpsModal" class="modal fade" role="dialog">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">GPS Coordinates</h4>
                            <asp:Label ID="Label1" runat="server" CssClass="col-sm-offset-0 col-sm-12 text-left blinking BlinkingNote"
                                Text="Note : GPS should be in the format 'Latitude, Longitude'. (Eg. : 90.00, 180.00)"></asp:Label>
                        </div>

                        <div class="modal-body row">
                            <div class="text-center">
                                <cc:CustomLabel ID="lblMessage" runat="server" class="REerror" Visible="false"></cc:CustomLabel>
                            </div>
                            <div class="col-sm-12">
                                <cc:CustomLabel ID="CustomLabel1" runat="server" class="col-sm-4" Text="Enter the GPS Coordinate"></cc:CustomLabel>
                                <cc:CustomTextBox ID="txtGPS" runat="server" req="1" CssClass="col-sm-3" />
                                <asp:RegularExpressionValidator ID="regexpName" runat="server" ErrorMessage="Invalid GPS Format."
                                    CssClass="REerror col-sm-3" ControlToValidate="txtGPS" ValidationExpression="^^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$" />

                                <cc:CustomButton ID="btnAdd" Text="Add GPS" runat="server" CssClass="btn btn-primary col-sm-2"
                                    OnClick="btnAdd_Click" />
                            </div>
                            <div class="row">
                            </div>
                            <div class="TopMargin">
                                <cc:CustomGridView ID="cgvGps" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                    Width="100%" DataKeyNames="ActivityItemId" OnPageIndexChanging="cgvGps_PageIndexChanging" OnPageIndexChanged="cgvGps_PageIndexChanged"
                                    AllowPaging="True" PageSize="5" EnableSortingAndPagingCallbacks="true" OnRowCancelingEdit="cgvGps_RowCancelingEdit"
                                    OnRowEditing="cgvGps_RowEditing" OnRowUpdating="cgvGps_RowUpdating" OnRowDeleting="cgvGps_RowDeleting">
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
                                                <%#Eval("RowNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GPSId" Visible="false">
                                            <ItemTemplate>
                                                <cc:CustomLabel ID="lblGPSId" runat="server" Text='<%#Eval("GPSId")%>'></cc:CustomLabel>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ActivityItemId" Visible="false">
                                            <ItemTemplate>
                                                <cc:CustomLabel ID="lblActivityItemId" runat="server" Text='<%#Eval("ActivityItemId")%>'></cc:CustomLabel>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GPS Details">
                                            <ItemTemplate>
                                                <cc:CustomLabel ID="lblGPS" runat="server" Text='<%#Eval("GPS")%>'></cc:CustomLabel>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <cc:CustomTextBox ID="txtGPS" runat="server" CssClass="form-control GridTxt" Text='<%#Eval("GPS") %>'
                                                    Width="150px"></cc:CustomTextBox>
                                                    <asp:RegularExpressionValidator ID="regexpName" runat="server" ErrorMessage="Invalid GPS Format."
                                    CssClass="REerror col-sm-3" ControlToValidate="txtGPS" ValidationExpression="^^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$" />
                                            </EditItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <cc:CustomButton ID="btnEditGps" runat="server" OnClientClick="return confirm('Are you sure? you want to edit this GPS Coordinate!');"
                                                    CssClass="btn btn-primary" Text="Edit" CommandName="Edit" />
                                                <cc:CustomButton ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure? you want to delete this GPS Coordinate!');"
                                                    CssClass="btn btn-primary ButtonMargin" Text="Delete" CommandName="Delete" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <cc:CustomButton ID="btnUpdate" runat="server" OnClientClick="return confirm('Are you sure? you want to update this GPS Coordinate!');"
                                                    CssClass="btn btn-primary ButtonMargin" Text="Update" CommandName="Update" />
                                                <cc:CustomButton ID="btnCancel" runat="server" CssClass="btn btn-primary ButtonMargin"
                                                    Text="Cancel" CommandName="Cancel" />
                                            </EditItemTemplate>
                                            <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </cc:CustomGridView>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="row minheight">
        <h4 id="SubmitApoHeader" runat="server">
            <b>Submit Apo</b></h4>
        <div class="col-sm-3">
            <div id="leftPartMy1" class="">
                <div id="leftNav">
                    <div class="panel-group" id="accordion">
                        <div class=" panel-default ">
                            <div id="panel1" class="panel-collapse collapse in menuleft">
                                <div class="table-responsive">
                                    <ul>
                                        <li class="FirstSubMenu1 Current BackgroundColor NonRecuring_left Links"><a class="anchorlink" href="#Non-RecurringDiv">
                                            Non-Recurring</a></li>
                                        <li class="BackgroundColor Links Recuring_left"><a class="anchorlink" href="#RecuringDiv">Recurring</a></li>
                                        <li class="BackgroundColor"><a href="ViewUtilizationCertificate.aspx">Utilization Certificate</a></li>
                                      
                                        <%--<li class="BackgroundColor "><a href="ViewFieldDirectorObligations.aspx">View Field
                                            Director's Obligations</a></li>
                                        <li class="BackgroundColor "><a href="ObligationCWW.aspx">Obligation Under Tri-MOU CWLW
                                            / SF</a></li>
                                        <li class="BackgroundColor "><a href="CWWFeedbackMOU.aspx">Feedback of Compliance MOU</a></li>--%>
                                        <li class="BackgroundColor"><a href="ViewCheckList.aspx">Checklist</a></li>
                                      
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-9  leftBorder minHeight Home_margin" id="MainContent">
            <asp:Label ID="lblNote1" runat="server" CssClass="col-sm-offset-0 col-sm-12 text-left blinking BlinkingNote"
                Text=""></asp:Label>
            <asp:Label ID="lblNote2" runat="server" CssClass="col-sm-offset-0 col-sm-12 text-left blinking BlinkingNote"
                Text=""></asp:Label>
            <asp:Label ID="lblNote3" runat="server" CssClass="col-sm-offset-0 col-sm-12 text-left blinking BlinkingNote"
                Text=""></asp:Label>
            <!-- Non-Recurring-->
            <div id="Non-RecurringDiv" class="minHeight TopDiv nonRecuringDiv">
                <div class="text-center  col-sm-12 MessagePadding">
                    <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                </div>
                <div id="">
                    <div class="row">
                        <div class="col-sm-6">
                            <!-- <h4>
                                        <strong>Anti-Poaching</strong></h4> -->
                        </div>
                        <div class="col-sm-offset-2 col-sm-4 linkPrevious">
                            <%--<a href="#">Click to View Previous Year</a>--%>
                            </div>
                    </div>
                    <!--here set the tabs code-->
                    <div class="bs-example">
                        <ul class="nav nav-tabs" id="mytab">
                            <li class="active a NRcore"><a data-toggle="tab" href="#core1">Core</a></li>
                            <li class="a NRbuffer"><a data-toggle="tab" href="#Buffer1">Buffer</a></li>
                        </ul>
                        <div class="tab-content">
                            <div id="core1" class="tab-pane fade">
                                <div class="tablettcolor">
                                    <cc:CustomGridView ID="gvNRCore" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                        CssClass="table table-bordered table-responsive tablett" OnRowCreated="gvNRCore_RowCreated"
                                        OnRowCommand="gvNRCore_RowCommand" ShowFooter="true" DataKeyNames="ActivityItemId"
                                        OnRowDataBound="gvNRCore_RowDataBound">
                                        <EmptyDataTemplate>
                                            <div class="EmpltyGridView">
                                                &quot; Sorry! There is no data &quot;</div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblSubTotal" runat="server" Text="Total (Non-Recurring Core):"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnParaNo" runat="server" CssClass="LinkColor" CommandName="detail"
                                                        Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTcpParaNo" runat="server" />
                                                    <cc:CustomTextBox ID="txtParaNo" runat="server" CssClass="less_size" Text="" placeholder="Para No. TCP"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtNumberOfItem" runat="server" req="1" CssClass="less_size No key"
                                                        Text="" placeholder="No. of Item" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumberOfItem" runat="server" ControlToValidate="txtNumberOfItem"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSpcification" runat="server" CssClass="" Text=""
                                                        TextMode="MultiLine" Rows="12" placeholder="Specification"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtUnitPrice" runat="server" CssClass="less_size unit key"
                                                        Text="" placeholder="Unit Price key" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvUnitPrice" runat="server" ControlToValidate="txtUnitPrice"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtTotal" runat="server" CssClass="less_size"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="txtSubTotal" runat="server" Text='<%#Eval("NRCoreTotal") %>'></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="140px">
                                                <ItemTemplate>
                                                    <cc:CustomImageButton ID="imgbtnAddNrCoreGPS" runat="server" CssClass="less_size text-center"
                                                        ImageUrl="../Images/gps1.jpg" RowIndex='<%# Container.DisplayIndex %>' OnClientClick="setNonRecuring_Core()" OnClick="imgbtnAddNrCoreGPS_click" /><br />
                                                    <asp:Label ID="lblGPS" runat="server" Text="" CssClass="less_size"></asp:Label>
                                                    <div class="GPSDetailScroll">
                                                    <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
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
                                                    </cc:CustomGridView>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Height="200px" HorizontalAlign="Center"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtJustification" runat="server" TextMode="MultiLine" Rows="12"
                                                        placeholder="Justification" CssClass="" Text=""></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvJustification" runat="server" ControlToValidate="txtJustification"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                                <ItemStyle Height="200px" HorizontalAlign="Center"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" />
                                                    <cc:CustomLinkButton ID="clbDocumentFile" runat="server" Text="" CommandArgument='<%#Eval("ActivityItemId") %>'
                                                        CommandName="download"></cc:CustomLinkButton>
                                                    <asp:RegularExpressionValidator runat="server" ID="rvafuPdfFile" ControlToValidate="fuUploadDocument"
                                                        ErrorMessage="Invalid File" ValidationExpression="^.*\.(png|PNG|jpg|JPG|jpeg|JPEG|gif|GIF|txt|TXT|xls|XLS|xlsx|XLSX|doc|DOC|docx|DOCX|pdf|PDF|ppt|PPT|pptx|PPTX)$"
                                                        CssClass="REerror" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <cc:CustomImageButton ID="imgbtnDeleteNrCore" runat="server" OnClientClick="return showconfirm_NonRecuringCore()"
                                                        ImageUrl="../Images/delete.png" RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnDeleteNrCore_click" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin margin_bottom">
                                    <asp:Button ID="btnNRCoreSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="setNonRecuring_Core()"
                                        OnClick="btnNRCoreSave_Click" />
                                    <asp:Button ID="btnCoreCancel" runat="server" Text="Back" CssClass="btn btn-primary col-sm-offset-1"
                                        OnClick="btnCoreCancel_Click" />
                                </div>
                            </div>
                            <div id="Buffer1" class="tab-pane fade">
                                <div class="tablettcolor">
                                    <cc:CustomGridView ID="gvNRBuffer" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                        OnRowCreated="gvNRBuffer_RowCreated" OnRowCommand="gvNRBuffer_RowCommand" DataKeyNames="ActivityItemId"
                                        CssClass="table table-bordered table-responsive tablett" ShowFooter="true" OnRowDataBound="gvNRBuffer_RowDataBound">
                                        <EmptyDataTemplate>
                                            <div class="EmpltyGridView">
                                                &quot; Sorry! There is no data &quot;</div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblSubTotal" runat="server" Text="Total (Non-Recurring Buffer):"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnParaNo" runat="server" CssClass="LinkColor" CommandName="detail"
                                                        Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTcpParaNo" runat="server" />
                                                    <cc:CustomTextBox ID="txtParaNo" runat="server" CssClass="less_size" Text="" placeholder="Para No. TCP"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtNumberOfItem" runat="server" req="1" CssClass="less_size No key"
                                                        Text="" placeholder="No. of Item" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumberOfItem" runat="server" ControlToValidate="txtNumberOfItem"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSpcification" runat="server" CssClass="" Text=""
                                                        TextMode="MultiLine" Rows="12" placeholder="Specification"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtUnitPrice" runat="server" CssClass="less_size unit key"
                                                        Text="" placeholder="Unit Price key" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvUnitPrice" runat="server" ControlToValidate="txtUnitPrice"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtTotal" runat="server" CssClass="less_size"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="txtSubTotal" runat="server" Text='<%#Eval("NRBufferTotal") %>'></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="140px">
                                                <ItemTemplate>
                                                    <cc:CustomImageButton ID="imgbtnAddNrBufferGPS" runat="server" CssClass="less_size text-center"
                                                        ImageUrl="../Images/gps1.jpg" RowIndex='<%# Container.DisplayIndex %>' OnClientClick="setNonRecuring_Buffer()" OnClick="imgbtnAddNrBufferGPS_click" /><br />
                                                    <%--<asp:Label ID="lblGPS" runat="server" Text="" CssClass="less_size"></asp:Label>--%>
                                                    <div class="GPSDetailScroll">
                                                    <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
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
                                                    </cc:CustomGridView>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Height="200px" HorizontalAlign="Center"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtJustification" runat="server" TextMode="MultiLine" Rows="12"
                                                        placeholder="Justification" CssClass="" Text=""></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvJustification" runat="server" ControlToValidate="txtJustification"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" />
                                                    <cc:CustomLinkButton ID="clbDocumentFile" runat="server" Text="" CommandArgument='<%#Eval("ActivityItemId") %>'
                                                        CommandName="download"></cc:CustomLinkButton>
                                                    <asp:RegularExpressionValidator runat="server" ID="rvafuPdfFile" ControlToValidate="fuUploadDocument"
                                                        ErrorMessage="Invalid File" ValidationExpression="^.*\.(png|PNG|jpg|JPG|jpeg|JPEG|gif|GIF|txt|TXT|xls|XLS|xlsx|XLSX|doc|DOC|docx|DOCX|pdf|PDF|ppt|PPT|pptx|PPTX)$"
                                                        CssClass="REerror" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <cc:CustomImageButton ID="imgbtnDeleteNrBuffer" runat="server" OnClientClick="return showconfirm_NonRecuringBuffer()"
                                                        ImageUrl="../Images/delete.png" RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnDeleteNrBuffer_click" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin margin_bottom">
                                    <asp:Button ID="btnNRBufferSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="setNonRecuring_Buffer()"
                                        OnClick="btnNRBufferSave_Click" />
                                    <asp:Button ID="btnBufferCancel" runat="server" Text="Back" CssClass="btn btn-primary col-sm-offset-1"
                                        OnClick="btnBufferCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--here set the tabs code-->
                    <div class="table-responsive tableWidthAPO">
                    </div>
                </div>
            </div>
            <!-- Recurring-->
            <div id="RecuringDiv" class="minHeight TopDiv RecuringDiv">
                <div id="rightPartMy1">
                    <div class="row">
                        <div class="col-sm-6">
                        </div>
                        <div class="col-sm-offset-2 col-sm-4 linkPrevious">
                            <%--<a href="#">Click to View Previous Year</a>--%>
                            </div>
                    </div>
                    <!--Here is Recuring-->
                    <div class="bs-example">
                        <ul class="nav nav-tabs" id="mytab1">
                            <li class="active a Rcore"><a data-toggle="tab" href="#core">Core</a></li>
                            <li class="a Rbuffer"><a data-toggle="tab" href="#Buffer">Buffer</a></li>
                        </ul>
                        <div class="tab-content">
                            <div id="core" class="tab-pane fade1">
                                <div class="tablettcolor">
                                    <cc:CustomGridView ID="gvRCore" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-responsive tablett"
                                        OnRowCreated="gvRCore_RowCreated" OnRowCommand="gvRCore_RowCommand" DataKeyNames="ActivityItemId"
                                        ShowHeader="false" ShowFooter="true" OnRowDataBound="gvRCore_RowDataBound">
                                        <EmptyDataTemplate>
                                            <div class="EmpltyGridView">
                                                &quot; Sorry! There is no data &quot;</div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblSubTotal" runat="server" Text="Total (Recurring Core):"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnParaNo" runat="server" CssClass="LinkColor" CommandName="detail"
                                                        Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTcpParaNo" runat="server" />
                                                    <cc:CustomTextBox ID="txtParaNo" runat="server" CssClass="less_size" Text="" placeholder="Para No. TCP"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtNumberOfItem" runat="server" req="1" CssClass="less_size No key"
                                                        Text="" placeholder="No. of Item" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumberOfItem" runat="server" ControlToValidate="txtNumberOfItem"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSpcification" runat="server" CssClass="" Text=""
                                                        TextMode="MultiLine" Rows="12" placeholder="Specification"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtUnitPrice" runat="server" CssClass="less_size unit key"
                                                        Text="" placeholder="Unit Price key" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvUnitPrice" runat="server" ControlToValidate="txtUnitPrice"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtTotal" runat="server" CssClass="less_size"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="txtSubTotal" runat="server" Text='<%#Eval("RCoreTotal") %>'></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="140px">
                                                <ItemTemplate>
                                                    <cc:CustomImageButton ID="imgbtnAddRCoreGPS" runat="server" CssClass="less_size text-center"
                                                        ImageUrl="../Images/gps1.jpg" RowIndex='<%# Container.DisplayIndex %>' OnClientClick="setRecuring_core()" OnClick="imgbtnAddRCoreGPS_click" /><br />
                                                    <%--<asp:Label ID="lblGPS" runat="server" Text="" CssClass="less_size"></asp:Label>--%>
                                                    <div class="GPSDetailScroll">
                                                    <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
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
                                                    </cc:CustomGridView>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Height="200px" HorizontalAlign="Center"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtJustification" runat="server" TextMode="MultiLine" Rows="12"
                                                        placeholder="Justification" CssClass="" Text=""></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvJustification" runat="server" ControlToValidate="txtJustification"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" />
                                                    <cc:CustomLinkButton ID="clbDocumentFile" runat="server" Text="" CommandArgument='<%#Eval("ActivityItemId") %>'
                                                        CommandName="download"></cc:CustomLinkButton>
                                                    <asp:RegularExpressionValidator runat="server" ID="rvafuPdfFile" ControlToValidate="fuUploadDocument"
                                                        ErrorMessage="Invalid File" ValidationExpression="^.*\.(png|PNG|jpg|JPG|jpeg|JPEG|gif|GIF|txt|TXT|xls|XLS|xlsx|XLSX|doc|DOC|docx|DOCX|pdf|PDF|ppt|PPT|pptx|PPTX)$"
                                                        CssClass="REerror" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <cc:CustomImageButton ID="imgbtnDeleteRcore" runat="server" OnClientClick="return showconfirm_RecuringCore()"
                                                        ImageUrl="../Images/delete.png" RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnDeleteRcore_click" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin">
                                    <asp:Button ID="btnRCoreSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="setRecuring_core()"
                                        OnClick="btnRCoreSave_Click" />
                                    <asp:Button ID="btnRCoreCancel" runat="server" Text="Back" CssClass="btn btn-primary col-sm-offset-1"
                                        OnClick="btnRCoreCancel_Click" />
                                </div>
                            </div>
                            <div id="Buffer" class="tab-pane fade1">
                                <div class="tablettcolor">
                                    <cc:CustomGridView ID="gvRBuffer" runat="server" AutoGenerateColumns="false" OnRowCreated="gvRBuffer_RowCreated"
                                        OnRowCommand="gvRBuffer_RowCommand" DataKeyNames="ActivityItemId" ShowHeader="false"
                                        CssClass="table table-bordered table-responsive tablett" ShowFooter="true" OnRowDataBound="gvRBuffer_RowDataBound">
                                        <EmptyDataTemplate>
                                            <div class="EmpltyGridView">
                                                &quot; Sorry! There is no data &quot;</div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblSubTotal" runat="server" Text="Total (Recurring Buffer):"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnParaNo" runat="server" CssClass="LinkColor" CommandName="detail"
                                                        Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTcpParaNo" runat="server" />
                                                    <cc:CustomTextBox ID="txtParaNo" runat="server" CssClass="less_size" Text="" placeholder="Para No. TCP"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtNumberOfItem" runat="server" req="1" CssClass="less_size No key"
                                                        Text="" placeholder="No. of Item" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumberOfItem" runat="server" ControlToValidate="txtNumberOfItem"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSpcification" runat="server" CssClass="" Text=""
                                                        TextMode="MultiLine" Rows="12" placeholder="Specification"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtUnitPrice" runat="server" CssClass="less_size unit key"
                                                        Text="" placeholder="Unit Price" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvUnitPrice" runat="server" ControlToValidate="txtUnitPrice"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtTotal" runat="server" CssClass=" less_size"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="txtSubTotal" runat="server" Text='<%#Eval("RBufferTotal") %>'></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="140px">
                                                <ItemTemplate>
                                                    <cc:CustomImageButton ID="imgbtnAddRBufferGPS" runat="server" CssClass="less_size text-center"
                                                        ImageUrl="../Images/gps1.jpg" RowIndex='<%# Container.DisplayIndex %>' OnClientClick="setRecuring_Buffer()" OnClick="imgbtnAddRBufferGPS_click" /><br />
                                                    <%--<asp:Label ID="lblGPS" runat="server" Text="" CssClass="less_size"></asp:Label>--%>
                                                    <div class="GPSDetailScroll">
                                                    <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
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
                                                    </cc:CustomGridView>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Height="200px" HorizontalAlign="Center"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtJustification" runat="server" TextMode="MultiLine" Rows="12"
                                                        placeholder="Justification" CssClass="" Text=""></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvJustification" runat="server" ControlToValidate="txtJustification"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" />
                                                    <cc:CustomLinkButton ID="clbDocumentFile" runat="server" Text="" CommandArgument='<%#Eval("ActivityItemId") %>'
                                                        CommandName="download"></cc:CustomLinkButton>
                                                    <asp:RegularExpressionValidator runat="server" ID="rvafuPdfFile" ControlToValidate="fuUploadDocument"
                                                        ErrorMessage="Invalid File" ValidationExpression="^.*\.(png|PNG|jpg|JPG|jpeg|JPEG|gif|GIF|txt|TXT|xls|XLS|xlsx|XLSX|doc|DOC|docx|DOCX|pdf|PDF|ppt|PPT|pptx|PPTX)$"
                                                        CssClass="REerror" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <cc:CustomImageButton ID="imgbtnDeleteRbuffer" runat="server" OnClientClick="return showconfirm_RecuringBuffer()"
                                                        ImageUrl="../Images/delete.png" RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnDeleteRbuffer_click" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin">
                                    <asp:Button ID="btnRBufferSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="setRecuring_Buffer()"
                                        OnClick="btnRBufferSave_Click" />
                                    <asp:Button ID="btnRBufferCancel" runat="server" Text="Back" CssClass="btn btn-primary col-sm-offset-1"
                                        OnClick="btnRBufferCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Here is recuring-->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
