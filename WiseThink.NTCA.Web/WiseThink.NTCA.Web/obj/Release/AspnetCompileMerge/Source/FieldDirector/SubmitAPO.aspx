<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" ValidateRequest="false" CodeBehind="SubmitAPO.aspx.cs"
    Inherits="WiseThink.NTCA.Web.FieldDirector.SubmitAPO1" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<%--<%@ Register Src="~/UserControls/ShowGuidelines.ascx" TagPrefix="uc" TagName="ShowGuidelines" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
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
            setActiveTab_PageLoad();
            rightshowhide();
            setTimeout(function () {
                $('#wait').hide();
            }, 10000);
        });
    </script>
    <script type="text/javascript">

        function GetTotalPrice() {
            var No = 1, unit = 1, Total = 1;
            $(".No").each(function () {
                $(this).addClass("Noitem" + No);
                No = No + 1;
            });
            $(".unit").each(function () {
                $(this).addClass("unititem" + unit);
                unit = unit + 1;
            });
            $(".Total").each(function () {
                $(this).addClass("Totalitem" + Total);
                Total = Total + 1;
            });
            for (var i = 1; i <= Total; i++) {
                var noofitem = 0, unitprice = 0, multiple = 0;
                var noofitem = $(".Noitem" + i).val();
                var unitprice = $(".unititem" + i).val();
                var multiple = parseFloat(noofitem) * parseFloat(unitprice);
                if (isNaN(multiple)) {
                    $(".Totalitem" + i).val("0"); //here is message ("Total Generate value")
                }
                else {
                    $(".Totalitem" + i).val(multiple);
                    var completeTotal = 0;
                    $(".NrCoreTotal").each(function () {
                        completeTotal = completeTotal + parseFloat($(this).val());
                    });
                    if (isNaN(completeTotal)) {
                        $(".NrCoreSubTotal").val("Sub Total");
                    }
                    else {
                        $(".NrCoreSubTotal").val(completeTotal);
                    }

                    completeTotal = 0;
                    $(".NrBufferTotal").each(function () {
                        completeTotal = completeTotal + parseFloat($(this).val());
                    });
                    if (isNaN(completeTotal)) {
                        $(".NrBufferSubTotal").val("Sub Total");
                    }
                    else {
                        $(".NrBufferSubTotal").val(completeTotal);
                    }
                    completeTotal = 0;
                    $(".RCoreTotal").each(function () {
                        completeTotal = completeTotal + parseFloat($(this).val());
                    });
                    if (isNaN(completeTotal)) {
                        $(".RCoreSubTotal").val("Sub Total");
                    }
                    else {
                        $(".RCoreSubTotal").val(completeTotal);
                    }

                    completeTotal = 0;
                    $(".RBufferTotal").each(function () {
                        completeTotal = completeTotal + parseFloat($(this).val());
                    });
                    if (isNaN(completeTotal)) {
                        $(".RBufferSubTotal").val("Sub Total");
                    }
                    else {
                        $(".RBufferSubTotal").val(completeTotal);
                    }
                }
            }
        }

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

        $(function () {
            GetTotalPrice();
            $(".key").keyup(function () {
                GetTotalPrice();
            });
            $(".key").focusout(function () {
                GetTotalPrice();
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

        $("#Note1").stop(false, true, true);
        $("#Note2").stop(false, true, true);
        $("#Note3").stop(false, true, true);

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <!-- Modal -->
    <div id="detailModal" class="modal fade" role="dialog">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog ">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">CSS PT Guidelines View</h4>
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
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header row">

                            <h4 class="modal-title pull-left" style="margin-top: 10px">GPS Coordinates</h4>
                            <button type="button" class="closebtn pull-right" onclick="closeGPSpopup(this)" data-dismiss="modal">
                                &times;</button>

                        </div>
                        <div class="modal-body row">

                            <asp:Label ID="Label1" runat="server" CssClass="col-sm-offset-0 col-sm-12 text-left blinking BlinkingNote"
                                Text="Note : GPS should be in the format 'Latitude, Longitude'. (Eg. : 90.00, 180.00)"></asp:Label>

                            <div class="text-center ">
                                <cc:CustomLabel ID="lblMessage" runat="server" class="REerror" Visible="false"></cc:CustomLabel>
                            </div>
                            <div class="col-sm-12 TopMargin">
                                <cc:CustomLabel runat="server" class="col-sm-4" Text="Enter the GPS Coordinate"></cc:CustomLabel>
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
                                    Width="100%" DataKeyNames="ActivityItemId" OnPageIndexChanging="cgvGps_PageIndexChanging"
                                    OnPageIndexChanged="cgvGps_PageIndexChanged" AllowPaging="True" PageSize="5"
                                    EnableSortingAndPagingCallbacks="true" OnRowCancelingEdit="cgvGps_RowCancelingEdit"
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
                                                <cc:CustomLabel ID="lblSerialNumber" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></cc:CustomLabel>
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
                                                    CssClass="btn btn-primary" Text="Edit" CommandName="Edit" Style="margin-top: -5px;" />
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
                        <%--<div class="modal-footer">
                            <button id="btnClose" type="button" class="btn btn-default close" data-dismiss="modal">
                                Close</button>
                        </div>--%>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cgvGps" EventName="PageIndexChanging" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="row minheight">
        <h4 id="SubmitApoHeader" runat="server">
            <b>Submit Apo</b>
        </h4>
        <div class="col-sm-2">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="leftPartMy1" class="">
                        <div id="leftNav">
                            <div class="panel-group" id="accordion">
                                <div class=" panel-default ">
                                    <div id="panel1" class="panel-collapse collapse in menuleft">
                                        <div class="table-responsive">
                                            <ul>
                                                <li class="FirstSubMenu1 Current BackgroundColor NonRecuring_left Links"><a class="anchorlink"
                                                    href="#Non-RecurringDiv">Step 1: Non-Recurring</a></li>
                                                <li class="BackgroundColor Links Recuring_left"><a class="anchorlink" href="#RecuringDiv">Step 2: Recurring</a></li>
                                                <li class="BackgroundColor"><a href="FDUtilizationCertificate.aspx">Step 3: Utilization
                                                    Certificate</a></li>
                                                <li class="BackgroundColor "><a href="ObligationFD.aspx">Step 4: Obligation Under Tri-MOU</a></li>
                                                <li class="BackgroundColor"><a href="CheckList.aspx?callfrom=SubmitAPO">Step 5: Check and Submit</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-sm-10  leftBorder minHeight Home_margin" id="MainContent">
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
                        </div>
                        <div class="col-sm-offset-2 col-sm-4 linkPrevious">
                            <a href="ViewPfyAPO.aspx?PFY=True">Click to View Previous Year</a>
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
                                    <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                    <cc:CustomGridView ID="gvNRCore" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                        CssClass="table table-bordered table-responsive tablett" OnRowCreated="gvNRCore_RowCreated"
                                        OnRowCommand="gvNRCore_RowCommand" ShowFooter="true" DataKeyNames="ActivityItemId"
                                        OnRowDataBound="gvNRCore_RowDataBound" AllowPaging="False" PageSize="30" OnPageIndexChanging="gvNRCore_PageIndexChanging"
                                        OnPageIndexChanged="gvNRCore_PageIndexChanged">
                                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                            Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                        <EmptyDataTemplate>
                                            <div class="EmpltyGridView">
                                                &quot; Sorry! There is no data &quot;
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Non Recuring Core">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Style="display: none" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Style="display: none" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'
                                                        Width="250px"></asp:Label>
                                                    <asp:Label ID="lblIsGpsRequired" runat="server" Visible="false" Text='<%#Eval("GPSStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <b>
                                                        <asp:Label ID="lblSubTotal" runat="server" Text="Total (Non-Recurring Core):"></asp:Label></b>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%--<a href="#" data-toggle="modal" data-target="#myModal"><%#Eval("ParaNoCSSPTGuidelines") %></a>--%>
                                                    <asp:LinkButton ID="lbtnParaNo" runat="server" CssClass="LinkColor" CommandName="detail"
                                                        Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblSubItemId" runat="server" Visible="false" Text='<%#Eval("SubItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblSubItem" runat="server" Text='<%#Eval("SubItem") %>'
                                                        Width="200px" Visible = "false"></asp:Label>--%>
                                                    <asp:DropDownList ID="ddlSubItem" Width="120px" CssClass="form-control dropdown"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTcpParaNo" runat="server" />
                                                    <cc:CustomTextBox ID="txtParaNo" runat="server" CssClass="less_size" Text="" placeholder="Para No. TCP"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="120px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSubItemName" runat="server" CssClass="" Text="" placeholder="Enter Sub-Item"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtNumberOfItem" runat="server" req="1" CssClass="less_size No key"
                                                        Text="" placeholder="No. of Item" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumberOfItem" runat="server" ControlToValidate="txtNumberOfItem"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px" Visible="false">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSpcification" runat="server" CssClass="" Text="" TextMode="MultiLine"
                                                        Rows="12" placeholder="Specification"></cc:CustomTextBox>
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
                                                    <cc:CustomTextBox ID="txtTotal" runat="server" CssClass="less_size Total NrCoreTotal"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <cc:CustomTextBox ID="txtSubTotal" runat="server" CssClass="less_size NrCoreSubTotal"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomLabel ID="lblGpsNote" runat="server" CssClass="less_size text-left" Text="Click icon to add GPS"></cc:CustomLabel>
                                                    <cc:CustomImageButton ID="updateSomething" runat="server" CssClass="text-center"
                                                        Style="width: 50px; height: 25px;" ImageUrl="../Images/gps1.jpg" OnClientClick="setNonRecuring_Core(this); openDialogue(this,'gvNRCore','1','1','SubmitAPO'); return false;" /><br />
                                                    <asp:Label ID="lblGPS" runat="server" Text="" CssClass="less_size"></asp:Label>
                                                    <%--<asp:DetailsView ID="GpsDetailsView" runat="server" CssClass="table table-bordered table-hover"
                                                        BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" BorderStyle="Groove" AutoGenerateRows="False">
                                                        <Fields>
                                                            <asp:BoundField DataField="GPSId" />
                                                            <asp:BoundField DataField="GPS" />
                                                        </Fields>
                                                    </asp:DetailsView>--%>
                                                    <div class="GPSDetailScroll">
                                                        <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                                            Width="160PX" DataKeyNames="ActivityItemId" AllowPaging="false" PageSize="5"
                                                            EnableSortingAndPagingCallbacks="false" ShowHeader="false" Visible="false">
                                                            <Columns>
                                                                <%--<asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                                                    <ItemTemplate>
                                                                        <%#Eval("RowNumber")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
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
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtJustification" runat="server" TextMode="MultiLine" Rows="6"
                                                        placeholder="Justification" CssClass="" Text=""></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvJustification" runat="server" ControlToValidate="txtJustification"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" onchange='prvimg.UpdatePreview(this)' />
                                                    <cc:CustomLinkButton ID="clbDocumentFile" runat="server" Text=""
                                                        OnClientClick="SetSessionValues(this,'gvNRCore','1','1','Download') "></cc:CustomLinkButton>
                                                    <asp:RegularExpressionValidator runat="server" ID="rvafuPdfFile" ControlToValidate="fuUploadDocument"
                                                        ErrorMessage="Invalid File" ValidationExpression="^.*\.(png|PNG|jpg|JPG|jpeg|JPEG|gif|GIF|txt|TXT|xls|XLS|xlsx|XLSX|doc|DOC|docx|DOCX|pdf|PDF|ppt|PPT|pptx|PPTX|kml|KML)$"
                                                        CssClass="REerror" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <%--<cc:CustomButton ID="btnEditDesignation" runat="server" OnClientClick="return confirm('Are you sure? you want to add more items!');" CssClass="btn btn-primary ButtonMargin"
                                Text="Add More" CommandName="AddMoreItem" />--%>
                                                    <%-- <cc:CustomImageButton ID="imgbtnAddSubItemNrCore" CssClass="btn btn-primary ButtonMargin" runat="server" OnClientClick="return confirm('Are you sure? you want to add more items!');"
                                                        ImageUrl="../Images/plus.png" RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnAddSubItemNrCore_click" />CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"--%>
                                                    <cc:CustomImageButton ID="imgbtnAddSubItemNrCore" CssClass="btn btn-primary ButtonMargin"
                                                        runat="server" OnClientClick="return AddAPOSubItem(this,'gvNRCore','1','1','SubmitAPO')" ImageUrl="../Images/plus.png" />
                                                    <cc:CustomImageButton ID="imgbtnDeleteNrCore" runat="server" OnClientClick="DeleteAPOSubItems(this,'gvNRCore','1','1','SubmitAPO'); return false;"
                                                        ImageUrl="../Images/remove.png" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                    <%--</ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvNRCore" EventName="PageIndexChanging" />
                                        <asp:AsyncPostBackTrigger ControlID="btnClose" EventName="Click" />
                                    </Triggers>
                                    </asp:UpdatePanel>--%>
                                </div>
                                <div class="text-center TopMargin margin_bottom">
                                    <asp:Button ID="btnNRCoreSave" runat="server" Text="Save As Draft" CssClass="btn btn-primary"
                                        OnClientClick="setNonRecuring_Core(); SaveAsDraft(); return false;" TabIndex="5" UseSubmitBehavior="false" />

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
                                                &quot; Sorry! There is no data &quot;
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Non-Recuring Buffers">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Style="display: none" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Style="display: none" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'
                                                        Width="250px"></asp:Label>
                                                    <asp:Label ID="lblIsGpsRequired" runat="server" Visible="false" Text='<%#Eval("GPSStatus") %>'></asp:Label>
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
                                                    <%--<asp:Label ID="lblSubItemId" runat="server" Visible="false" Text='<%#Eval("SubItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblSubItem" runat="server" Text='<%#Eval("SubItem") %>'
                                                        Width="200px" Visible = "false"></asp:Label>--%>
                                                    <asp:DropDownList ID="ddlSubItem" Width="120px" CssClass="form-control dropdown"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTcpParaNo" runat="server" />
                                                    <cc:CustomTextBox ID="txtParaNo" runat="server" CssClass="less_size" Text="" placeholder="Para No. TCP"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="120px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSubItemName" runat="server" CssClass="" Text="" placeholder="Enter Sub-Item"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtNumberOfItem" runat="server" req="1" CssClass="less_size No key"
                                                        Text="" placeholder="No. of Item" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumberOfItem" runat="server" ControlToValidate="txtNumberOfItem"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px" Visible="false">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSpcification" runat="server" CssClass="" Text="" TextMode="MultiLine"
                                                        Rows="6" placeholder="Specification"></cc:CustomTextBox>
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
                                                    <cc:CustomTextBox ID="txtTotal" runat="server" CssClass="less_size Total NrBufferTotal"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <cc:CustomTextBox ID="txtSubTotal" runat="server" CssClass="less_size NrBufferSubTotal"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomLabel ID="lblGpsNote" runat="server" CssClass="less_size text-left" Text="Click icon to add GPS"></cc:CustomLabel>
                                                    <cc:CustomImageButton ID="updateSomething" runat="server" CssClass="text-center"
                                                        Style="width: 50px; height: 25px;" ImageUrl="../Images/gps1.jpg" OnClientClick="setNonRecuring_Buffer(this); openDialogue(this,'gvNRBuffer','2','1','SubmitAPO'); return false;" />
                                                    <br />
                                                    <%--<cc:CustomImageButton ID="imgbtnAddNrBufferGPS" runat="server" CssClass="text-center" style="width:50px; height:25px;"
                                                        ImageUrl="../Images/gps1.jpg" RowIndex='<%# Container.DisplayIndex %>' OnClientClick="setNonRecuring_Buffer()" OnClick="imgbtnAddNrBufferGPS_click" /><br />--%>
                                                    <%--<asp:Label ID="lblGPS" runat="server" Text="" CssClass="less_size"></asp:Label>--%>
                                                    <div class="GPSDetailScroll">
                                                        <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                                            Width="160PX" DataKeyNames="ActivityItemId" AllowPaging="false" PageSize="5"
                                                            EnableSortingAndPagingCallbacks="false" ShowHeader="false" Visible="false">
                                                            <Columns>
                                                                <%--<asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                                                    <ItemTemplate>
                                                                        <%#Eval("RowNumber")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
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
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtJustification" runat="server" TextMode="MultiLine" Rows="6"
                                                        placeholder="Justification" CssClass="" Text=""></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvJustification" runat="server" ControlToValidate="txtJustification"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" />
                                                    <cc:CustomLinkButton ID="clbDocumentFile" runat="server" Text="" CommandArgument='<%#Eval("ActivityItemId") %>'
                                                        CommandName="download"></cc:CustomLinkButton>
                                                    <asp:RegularExpressionValidator runat="server" ID="rvafuPdfFile" ControlToValidate="fuUploadDocument"
                                                        ErrorMessage="Invalid File" ValidationExpression="^.*\.(png|PNG|jpg|JPG|jpeg|JPEG|gif|GIF|txt|TXT|xls|XLS|xlsx|XLSX|doc|DOC|docx|DOCX|pdf|PDF|ppt|PPT|pptx|PPTX|kml|KML)$"
                                                        CssClass="REerror" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <%--<cc:CustomImageButton ID="imgbtnAddSubItemNrBuffer" CssClass="btn btn-primary ButtonMargin"
                                                        runat="server" OnClientClick="return confirm('Are you sure? you want to add more items!');"
                                                        ImageUrl="../Images/plus.png" RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnAddSubItemNrBuffer_click" />--%>
                                                    <cc:CustomImageButton ID="imgbtnAddSubItemNrBuffer" CssClass="btn btn-primary ButtonMargin"
                                                        runat="server" OnClientClick="return AddAPOSubItem(this,'gvNRBuffer','2','1','SubmitAPO')" ImageUrl="../Images/plus.png" />
                                                    <cc:CustomImageButton ID="imgbtnDeleteNrBuffer" runat="server" OnClientClick="DeleteAPOSubItems(this,'gvNRBuffer','2','1','SubmitAPO'); return false;  "
                                                        ImageUrl="../Images/remove.png" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin margin_bottom">
                                    <asp:Button ID="btnNRBufferSave" runat="server" Text="Save As Draft" CssClass="btn btn-primary"
                                        OnClientClick="setNonRecuring_Buffer(); SaveAsDraft(); return false;" />
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
            <div id="1234AV">
            </div>
            <div id="RecuringDiv" class="minHeight TopDiv RecuringDiv">
                <div id="rightPartMy1">
                    <div class="row">
                        <div class="col-sm-6">
                        </div>
                        <div class="col-sm-offset-2 col-sm-4 linkPrevious">
                            <a href="ViewPfyAPO.aspx?PFY=True">Click to View Previous Year</a>
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
                                                &quot; Sorry! There is no data &quot;
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Style="display: none" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Style="display: none" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'
                                                        Width="250px"></asp:Label>
                                                    <asp:Label ID="lblIsGpsRequired" runat="server" Visible="false" Text='<%#Eval("GPSStatus") %>'></asp:Label>
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
                                                    <%--<asp:Label ID="lblSubItemId" runat="server" Visible="false" Text='<%#Eval("SubItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblSubItem" runat="server" Text='<%#Eval("SubItem") %>'
                                                        Width="200px" Visible = "false"></asp:Label>--%>
                                                    <asp:DropDownList ID="ddlSubItem" Width="120px" CssClass="form-control dropdown"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTcpParaNo" runat="server" />
                                                    <cc:CustomTextBox ID="txtParaNo" runat="server" CssClass="less_size" Text="" placeholder="Para No. TCP"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="120px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSubItemName" runat="server" CssClass="" Text="" placeholder="Enter Sub-Item"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtNumberOfItem" runat="server" req="1" CssClass="less_size No key"
                                                        Text="" placeholder="No. of Item" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumberOfItem" runat="server" ControlToValidate="txtNumberOfItem"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px" Visible="false">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSpcification" runat="server" CssClass="" Text="" TextMode="MultiLine"
                                                        Rows="12" placeholder="Specification"></cc:CustomTextBox>
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
                                                    <cc:CustomTextBox ID="txtTotal" runat="server" CssClass="less_size Total RCoreTotal"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <cc:CustomTextBox ID="txtSubTotal" runat="server" CssClass="less_size RCoreSubTotal"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomLabel ID="lblGpsNote" runat="server" CssClass="less_size text-left" Text="Click icon to add GPS"></cc:CustomLabel>
                                                    <cc:CustomImageButton ID="updateSomething" runat="server" CssClass="text-center"
                                                        Style="width: 50px; height: 25px;" ImageUrl="../Images/gps1.jpg"
                                                        OnClientClick="setRecuring_core(this); openDialogue(this,'gvRCore','2','1','SubmitAPO'); return false;" />
                                                    <br />
                                                    <%--<cc:CustomImageButton ID="imgbtnAddRCoreGPS" runat="server" CssClass="text-center" style="width:50px; height:25px;"
                                                        ImageUrl="../Images/gps1.jpg" RowIndex='<%# Container.DisplayIndex %>' OnClientClick="setRecuring_core()" OnClick="imgbtnAddRCoreGPS_click" /><br />--%>
                                                    <%--<asp:Label ID="lblGPS" runat="server" Text="" CssClass="less_size"></asp:Label>--%>
                                                    <div class="GPSDetailScroll">
                                                        <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                                            Width="160px" DataKeyNames="ActivityItemId" AllowPaging="false" PageSize="5"
                                                            EnableSortingAndPagingCallbacks="false" ShowHeader="false" Visible="false">
                                                            <Columns>
                                                                <%--<asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                                                    <ItemTemplate>
                                                                        <%#Eval("RowNumber")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
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
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtJustification" runat="server" TextMode="MultiLine" Rows="6"
                                                        placeholder="Justification" CssClass="" Text=""></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvJustification" runat="server" ControlToValidate="txtJustification"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" />
                                                    <cc:CustomLinkButton ID="clbDocumentFile" runat="server" Text="" CommandArgument='<%#Eval("ActivityItemId") %>'
                                                        CommandName="download"></cc:CustomLinkButton>
                                                    <asp:RegularExpressionValidator runat="server" ID="rvafuPdfFile" ControlToValidate="fuUploadDocument"
                                                        ErrorMessage="Invalid File" ValidationExpression="^.*\.(png|PNG|jpg|JPG|jpeg|JPEG|gif|GIF|txt|TXT|xls|XLS|xlsx|XLSX|doc|DOC|docx|DOCX|pdf|PDF|ppt|PPT|pptx|PPTX|kml|KML)$"
                                                        CssClass="REerror" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <%--<cc:CustomImageButton ID="imgbtnAddSubItemRCore" CssClass="btn btn-primary ButtonMargin"
                                                        runat="server" OnClientClick="return confirm('Are you sure? you want to add more items!');"
                                                        ImageUrl="../Images/plus.png" RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnAddSubItemRCore_click" />--%>
                                                    <cc:CustomImageButton ID="imgbtnAddSubItemRCore" CssClass="btn btn-primary ButtonMargin"
                                                        runat="server" OnClientClick="return AddAPOSubItem(this,'gvRCore','2','1','SubmitAPO')" ImageUrl="../Images/plus.png" />
                                                    <cc:CustomImageButton ID="imgbtnDeleteRcore" runat="server" OnClientClick="DeleteAPOSubItems(this,'gvRCore','2','1','SubmitAPO'); return false;  "
                                                        ImageUrl="../Images/remove.png" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin">
                                    <asp:Button ID="btnRCoreSave" runat="server" Text="Save As Draft" CssClass="btn btn-primary"
                                        OnClientClick="setRecuring_Core(); SaveAsDraft(); return false;" />
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
                                                &quot; Sorry! There is no data &quot;
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Style="display: none" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Style="display: none" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'
                                                        Width="250px"></asp:Label>
                                                    <asp:Label ID="lblIsGpsRequired" runat="server" Visible="false" Text='<%#Eval("GPSStatus") %>'></asp:Label>
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
                                                    <%--<asp:Label ID="lblSubItemId" runat="server" Visible="false" Text='<%#Eval("SubItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblSubItem" runat="server" Text='<%#Eval("SubItem") %>'
                                                        Width="200px" Visible = "false"></asp:Label>--%>
                                                    <asp:DropDownList ID="ddlSubItem" Width="120px" CssClass="form-control dropdown"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTcpParaNo" runat="server" />
                                                    <cc:CustomTextBox ID="txtParaNo" runat="server" CssClass="less_size" Text="" placeholder="Para No. TCP"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="120px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSubItemName" runat="server" CssClass="" Text="" placeholder="Enter Sub-Item"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtNumberOfItem" runat="server" req="1" CssClass="less_size No key"
                                                        Text="" placeholder="No. of Item" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNumberOfItem" runat="server" ControlToValidate="txtNumberOfItem"
                                                        Enabled="false" ErrorMessage="Required Field" CssClass="REerror"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px" Visible="false">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSpcification" runat="server" CssClass="" Text="" TextMode="MultiLine"
                                                        Rows="12" placeholder="Specification"></cc:CustomTextBox>
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
                                                    <cc:CustomTextBox ID="txtTotal" runat="server" CssClass=" less_size Total RBufferTotal"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <cc:CustomTextBox ID="txtSubTotal" runat="server" CssClass="less_size RBufferSubTotal"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomLabel ID="lblGpsNote" runat="server" CssClass="less_size text-left" Text="Click icon to add GPS"></cc:CustomLabel>
                                                    <cc:CustomImageButton ID="updateSomething" runat="server" CssClass="text-center"
                                                        Style="width: 50px; height: 25px;" ImageUrl="../Images/gps1.jpg"
                                                        OnClientClick="setRecuring_Buffer(this); openDialogue(this,'gvRBuffer','2','2','SubmitAPO'); return false;" /><br />
                                                    <%--<cc:CustomImageButton ID="imgbtnAddRBufferGPS" runat="server" CssClass="text-center" style="width:50px; height:25px;"
                                                        ImageUrl="../Images/gps1.jpg" RowIndex='<%# Container.DisplayIndex %>' OnClientClick="setRecuring_Buffer()" OnClick="imgbtnAddRBufferGPS_click" /><br />--%>
                                                    <%--<asp:Label ID="lblGPS" runat="server" Text="" CssClass="less_size"></asp:Label>--%>
                                                    <div class="GPSDetailScroll">
                                                        <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                                            Width="160px" DataKeyNames="ActivityItemId" AllowPaging="false" PageSize="5"
                                                            EnableSortingAndPagingCallbacks="false" ShowHeader="false" Visible="false">
                                                            <Columns>
                                                                <%--<asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                                                    <ItemTemplate>
                                                                        <%#Eval("RowNumber")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
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
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtJustification" runat="server" TextMode="MultiLine" Rows="6"
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
                                                        ErrorMessage="Invalid File" ValidationExpression="^.*\.(png|PNG|jpg|JPG|jpeg|JPEG|gif|GIF|txt|TXT|xls|XLS|xlsx|XLSX|doc|DOC|docx|DOCX|pdf|PDF|ppt|PPT|pptx|PPTX|kml|KML)$"
                                                        CssClass="REerror" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <%--<cc:CustomImageButton ID="imgbtnAddSubItemRbuffer" CssClass="btn btn-primary ButtonMargin"
                                                        runat="server" OnClientClick="return confirm('Are you sure? you want to add more items!');"
                                                        ImageUrl="../Images/plus.png" RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnAddSubItemRbuffer_click" />--%>
                                                    <cc:CustomImageButton ID="imgbtnAddSubItemRbuffer" CssClass="btn btn-primary ButtonMargin"
                                                        runat="server" OnClientClick="return AddAPOSubItem(this,'gvRBuffer','2','2','SubmitAPO')" ImageUrl="../Images/plus.png" />
                                                    <cc:CustomImageButton ID="imgbtnDeleteRbuffer" runat="server" OnClientClick="DeleteAPOSubItems(this,'gvRBuffer','2','2','SubmitAPO'); return false;"
                                                        ImageUrl="../Images/remove.png" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin">
                                    <asp:Button ID="btnRBufferSave" runat="server" Text="Save As Draft" CssClass="btn btn-primary"
                                        OnClientClick="setRecuring_Buffer(); SaveAsDraft(); return false;" />
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
