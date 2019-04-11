<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="EditApoCopy.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.EditApoCopy" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquerysession.js" type="text/javascript"></script>
    <script src="../js/Remove.js" type="text/javascript"></script>
    <script src="../js/bootstrap.min.js" type="text/javascript"></script>
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
        function ConfirmDelete() {
            if (confirm("Are you sure to delete the record?")) {
                return true;
            }
            else {
                return false;
            }
        }
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

<%--        function SetTotalValues(nrCore, nrBuffer, rCore, rBuffer) {


            document.getElementById("ctl00_ctl00_MainContentPlaceHolder_NTCAPlaceHolder_gvNRCore_ctl23_txtNRCoreTotal").innerText = nrCore;
            document.getElementById("ctl00_ctl00_MainContentPlaceHolder_NTCAPlaceHolder_gvNRBuffer_ctl12_txtNRBufferTotal").innerText = nrBuffer;
            document.getElementById("ctl00_ctl00_MainContentPlaceHolder_NTCAPlaceHolder_gvRCore_ctl21_txtRCoreTotal").innerText = rCore;
            document.getElementById("ctl00_ctl00_MainContentPlaceHolder_NTCAPlaceHolder_gvRBuffer_ctl09_txtRBufferTotal").innerText = rBuffer;

           document.getElementById("<%=txtNRBufferTotal.ClientID %>").val(nrCore);
            document.getElementById("<%=txtRCoreTotal.ClientID %>").innerHTML = rCore;
            document.getElementById("<%=txtRBufferTotal.ClientID %>").innerHTML = rBuffer;
       }--%>

        function GetTotalCPriceChild() {
            var NoC = 1, unitC = 1, TotalC = 1;
            $(".NoC").each(function () {
                $(this).addClass("NoCitem" + NoC);
                NoC = NoC + 1;
            });
            $(".unitC").each(function () {
                $(this).addClass("unitCitem" + unitC);
                unitC = unitC + 1;
            });
            $(".TotalC").each(function () {
                $(this).addClass("TotalCitem" + TotalC);
                TotalC = TotalC + 1;
            });
            for (var i = 1; i <= TotalC; i++) {
                var NoCofitem = 0, unitCprice = 0, multiple = 0;
                var NoCofitem = $(".NoCitem" + i).val();
                var unitCprice = $(".unitCitem" + i).val();
                var multiple = parseFloat(NoCofitem) * parseFloat(unitCprice);
                if (isNaN(multiple)) {
                    $(".TotalCitem" + i).val("0"); //here is message ("TotalC Generate value")
                }
                else {
                    $(".TotalCitem" + i).val(multiple);

                }
            }
        }

        $(function () {
            //$('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
            //  //  alert($(e.target).attr('href'));
            // //   localStorage.setItem('leftTab', $(e.target).attr('href'));

            //}); 
            $("#MainContent > div:gt(0)").hide();
            $("#leftNav ul a").on("click", function (e) {
                var col = jQuery('col');

                var href = $(this).attr("href");
                //localStorage.setItem('tabmain', href);
                var string = href.substring(1, href.length - 1);
                $('.Current').removeClass('Current');
                $('a[href=' + href + ']').parent().addClass('Current');

                $("#MainContent > " + href).show();

                $("#MainContent > :not(" + href + ")").hide();
            });
        });

        $(function () {
            GetTotalPrice();
            GetTotalCPriceChild();
            $(".key").keyup(function () {
                GetTotalPrice();
                GetTotalCPriceChild();
            });
            $(".key").focusout(function () {
                GetTotalPrice();
                GetTotalCPriceChild();
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
        //        $('#Note1').hover(
        //       function () { $(this).removeClass('blinking') }
        //       )
        //       $('#Note2').hover(
        //       function () { $(this).removeClass('blinking') }
        //       )
        //       $('#Note3').hover(
        //       function () { $(this).removeClass('blinking') }
        //)
    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">

    <div id="detailModal" class="modal fade" role="dialog">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog">
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
                            <div class="text-center">
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
                                                <cc:CustomLabel ID="lblSubItemName" runat="server" Style="display:none" Text='<%#Eval("SubitemName")%>'></cc:CustomLabel>
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
            <b>Edit Apo</b></h4>
        <div class="col-sm-2">
            <div id="leftPartMy1" class="">
                <div id="leftNav">
                    <div class="panel-group" id="accordion">
                        <div class=" panel-default ">
                            <div id="panel1" class="panel-collapse collapse in menuleft">
                                <div class="table-responsive">
                                    <ul>
                                        <li class="FirstSubMenu1 Current BackgroundColor NonRecuring_left Links"><a class="anchorlink" href="#Non-RecurringDiv">Non-Recurring</a></li>
                                        <li class="BackgroundColor Links Recuring_left"><a class="anchorlink" href="#RecuringDiv">Recurring</a></li>
                                        <li class="BackgroundColor"><a href="ViewUtilizationCertificate.aspx">Utilization Certificate</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
                    <div id="DisplayErrorMessage" class="btn-danger" runat="server">
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
                </div>

                <!--here set the tabs code-->
                <div class="bs-example">
                    <ul class="nav nav-tabs" id="mytab">
                        <li class="active a NRcore" id="nonrecurringcore"><a data-toggle="tab" href="#core1">Core</a></li>
                        <li class="a NRbuffer" id="nonrecurringbuffer"><a data-toggle="tab" href="#Buffer1">Buffer</a></li>
                    </ul>
                    <div class="tab-content">
                        <div id="core1" class="tab-pane">
                            <div class="tablettcolor">
                           <%--     NR Core Main Grid--%>
                                <cc:CustomGridView ID="gvNRCore" runat="server" AutoGenerateColumns="false" ShowHeader="false"
                                    CssClass="table table-bordered table-responsive tablett" OnRowDataBound="gvNRCore_RowDataBound"
                                    OnRowCommand="gvNRCore_RowCommand" OnRowCreated="gvNRCore_RowCreated"
                                    ShowFooter="true" DataKeyNames="ActivityItemId">
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
                                                <cc:CustomTextBox ID="txtNRCoreTotal" runat="server" CssClass="less_size NrCoreSubTotal"
                                                    Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="200px">
                                            <ItemTemplate>
                                                <cc:CustomLabel ID="lblGpsNote" runat="server" CssClass="less_size text-left" Text="Click icon to add GPS"></cc:CustomLabel>
                                                <cc:CustomImageButton ID="updateSomething" runat="server" CssClass="text-center"
                                                    Style="width: 50px; height: 25px;" ImageUrl="../Images/gps1.jpg" OnClientClick="setNonRecuring_Core(this); openDialogue(this,'gvNRCore','1','1','EditAPO'); return false;" /><br />
                                                <asp:Label ID="lblGPS" runat="server" Text="" CssClass="less_size"></asp:Label>
                                                <div class="GPSDetailScroll">
                                                    <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                                        Width="160PX" DataKeyNames="ActivityItemId" AllowPaging="false" PageSize="5"
                                                        EnableSortingAndPagingCallbacks="false" ShowHeader="false" Visible="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
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
                                                <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" onchange='previewFile(this)' />
                                                <cc:CustomLinkButton ID="clbDocumentFile" runat="server" Text=""
                                                    OnClientClick="SetSessionValues(this,'gvNRCore','1','1','Download') "></cc:CustomLinkButton>
                                                <asp:RegularExpressionValidator runat="server" ID="rvafuPdfFile" ControlToValidate="fuUploadDocument"
                                                    ErrorMessage="Invalid File" ValidationExpression="^.*\.(png|PNG|jpg|JPG|jpeg|JPEG|gif|GIF|txt|TXT|xls|XLS|xlsx|XLSX|doc|DOC|docx|DOCX|pdf|PDF|ppt|PPT|pptx|PPTX|kml|KML)$"
                                                    CssClass="REerror" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <cc:CustomImageButton ID="imgbtnAddSubItemNrCore" CssClass="btn btn-primary ButtonMargin"
                                                    runat="server" OnClientClick="return AddAPOSubItem(this,'gvNRCore','1','1','EditAPO')" ImageUrl="../Images/plus.png" />
                                                <cc:CustomImageButton ID="imgbtnDeleteNrCore" runat="server" OnClientClick="setNonRecuring_Core(); DeleteAPOSubItems(this,'gvNRCore','1','1','EditAPO'); return false;"
                                                    ImageUrl="../Images/remove.png" />
                                            </ItemTemplate>
                                            <%--<ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="13">
                                                        <div style="display: block; position: relative; left: 15px; overflow: auto; width: 97%">

                 <%--     NR Core Child Grid--%>
                                                            <cc:CustomGridView ID="ChildRecordsRecurring" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="ActivityItemId" ShowFooter="false"
                                                                OnRowUpdating="ChildRecordsRecurring_RowUpdating" OnRowCommand="ChildRecordsRecurring_RowCommand"
                                                                OnRowEditing="ChildRecordsRecurring_RowEditing" OnRowUpdated="ChildRecordsRecurring_RowUpdated" OnRowCancelingEdit="ChildRecordsRecurring_CancelingEdit"
                                                                OnRowDataBound="ChildRecordsRecurring_RowDataBound" OnRowDeleting="ChildRecordsRecurring_RowDeleting" OnRowDeleted="ChildRecordsRecurring_RowDeleted">

                                                                <Columns>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblActivityItemId" runat="server" Style="display: none" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Sr. No" ItemStyle-Width="60px">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblId" runat="server" Style="display: none" Text='<%#Eval("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSubItemTypeId" runat="server" Style="display: none" Text='<%#Eval("SubItemId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Sub-Item Type" ItemStyle-Width="150px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%#Eval("SubItemType") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="ParaNo TCP" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                        <ItemTemplate>
                                                                            <%#Eval("ParaNoTCP") %>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtParaNoTCPChild" Text='<%#Eval("ParaNoTCP") %>' runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Name of Sub-Item" ItemStyle-Width="500px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Justify">
                                                                        <ItemTemplate>
                                                                            <%#Eval("SubItem") %>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtSubItemChild" Text='<%#Eval("SubItem") %>' runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Number of Items" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%#Eval("NumberOfItems") %>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <cc:CustomTextBox ID="txtNumberOfItemsChild" AutoPostBack="true" Text='<%#Eval("NumberOfItems") %>' runat="server" CssClass="less_size" onFocusout="return validate(this.value,this);" OnTextChanged="txtNumberOfItemsChild_TextChanged" ></cc:CustomTextBox>
                                                                            <%--<cc:CustomTextBox ID="txtNumberOfItem" runat="server" req="1" CssClass="less_size No key" Text="" placeholder="No. of Item" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>--%>

                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <%#Eval("UnitPrice") %>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <cc:CustomTextBox ID="txtUnitPriceChild" AutoPostBack="true" Text='<%#Eval("UnitPrice") %>' runat="server" CssClass="less_size" onFocusout="return validate(this.value,this);" OnTextChanged="txtUnitPriceChild_TextChanged"></cc:CustomTextBox>

                                                                            <%--<cc:CustomTextBox ID="txtUnitPrice" runat="server" CssClass="less_size unit key" Text="" placeholder="Unit Price key" onFocusout="return validate(this.value,this);"></cc:CustomTextBox>--%>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Total" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <cc:CustomTextBox ID="lblTotalChild" runat="server" Text='<%#Eval("Total") %>' Enabled="false" CssClass="less_size" ></cc:CustomTextBox>
                                                                             <%--<cc:CustomTextBox ID="txtTotal" runat="server" CssClass="less_size Total NrCoreTotal" Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="GPS" ItemStyle-Width="300px" ItemStyle-VerticalAlign="Top">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGPSChild" runat="server" Text='<%#Eval("GPS") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Justification" ItemStyle-Width="500px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Justify">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Justification") %>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <cc:CustomTextBox ID="txtJustificationChild" TextMode="MultiLine" Rows="3" Text='<%#Eval("Justification") %>' runat="server"></cc:CustomTextBox>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Document" ItemStyle-Width="200px">
                                                                        <ItemTemplate>
                                                                            <%--<a href="../downloadFileHandler.ashx?fileName=<%#Eval("Document") %>"><%#Eval("Document") %></a>--%>
                                                                            <cc:CustomLinkButton ID="downloadDocumentID" Text='<%#Eval("Document") %>' runat="server" OnClick="downloadBtn_Click" OnClientClick="RemoveBlockUi();"></cc:CustomLinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Wrap="false">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="linkEditCust" CommandName="Edit" runat="server">Edit</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:LinkButton ID="linkUpdateCust" CommandName="Update" runat="server" OnClientClick="return confirm('Are you sure update record?');">Update</asp:LinkButton>
                                                                            <asp:LinkButton ID="linkCancelCust" CommandName="Cancel" runat="server">Cancel</asp:LinkButton>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="linkDeleteCust" CommandName="Delete" runat="server" OnClientClick="return ConfirmDelete();">Delete</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </cc:CustomGridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </cc:CustomGridView>
                            </div>
                            <div class="text-center TopMargin margin_bottom"  style="margin-left: -10vh;">
                               <%-- <asp:Button ID="btnNRCoreSave" runat="server" Text="Save As Draft" CssClass="btn btn-primary"
                                    OnClientClick="setNonRecuring_Core(); SaveAsDraft(); return false;" TabIndex="5" UseSubmitBehavior="false" />--%>

                                <asp:Button ID="btnCoreCancel" runat="server" Text="Back" CssClass="btn btn-primary col-sm-offset-1"
                                    OnClick="btnCoreCancel_Click" />
                            </div>
                        </div>

                        <div id="Buffer1" class="tab-pane">
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
                                                <cc:CustomTextBox ID="txtNRBufferTotal" runat="server" CssClass="less_size NrBufferSubTotal"
                                                    Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="200px">
                                            <ItemTemplate>
                                                <cc:CustomLabel ID="lblGpsNote" runat="server" CssClass="less_size text-left" Text="Click icon to add GPS"></cc:CustomLabel>
                                                <cc:CustomImageButton ID="updateSomething" runat="server" CssClass="text-center"
                                                    Style="width: 50px; height: 25px;" ImageUrl="../Images/gps1.jpg" OnClientClick="setNonRecuring_Buffer(this); openDialogue(this,'gvNRBuffer','1','2','EditAPO'); return false;" />
                                                <br />
                                                <div class="GPSDetailScroll">
                                                    <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                                        Width="160PX" DataKeyNames="ActivityItemId" AllowPaging="false" PageSize="5"
                                                        EnableSortingAndPagingCallbacks="false" ShowHeader="false" Visible="false">
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
                                                <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" onchange='previewFile(this)' />
                                                <cc:CustomLinkButton ID="clbDocumentFile" runat="server" Text="" CommandArgument='<%#Eval("ActivityItemId") %>'
                                                    CommandName="download"></cc:CustomLinkButton>
                                                <asp:RegularExpressionValidator runat="server" ID="rvafuPdfFile" ControlToValidate="fuUploadDocument"
                                                    ErrorMessage="Invalid File" ValidationExpression="^.*\.(png|PNG|jpg|JPG|jpeg|JPEG|gif|GIF|txt|TXT|xls|XLS|xlsx|XLSX|doc|DOC|docx|DOCX|pdf|PDF|ppt|PPT|pptx|PPTX|kml|KML)$"
                                                    CssClass="REerror" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <cc:CustomImageButton ID="imgbtnAddSubItemNrBuffer" CssClass="btn btn-primary ButtonMargin"
                                                    runat="server" OnClientClick="return AddAPOSubItem(this,'gvNRBuffer','2','1','EditAPO')" ImageUrl="../Images/plus.png" />
                                                <cc:CustomImageButton ID="imgbtnDeleteNrBuffer" runat="server" OnClientClick="setNonRecuring_Buffer(); DeleteAPOSubItems(this,'gvNRBuffer','2','1','EditAPO'); return false;  "
                                                    ImageUrl="../Images/remove.png" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%">
                                                        <div style="display: block; position: relative; left: 15px; overflow: auto; width: 97%">
                                  <%--    NR Buffer Child Grid--%>
                                                            <cc:CustomGridView ID="ChildRecordsNonRecurringBuffer" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="ActivityItemId" ShowFooter="false"
                                                                OnRowUpdating="ChildRecordsNonRecurringBuffer_RowUpdating" OnRowCommand="ChildRecordsNonRecurringBuffer_RowCommand"
                                                                OnRowEditing="ChildRecordsNonRecurringBuffer_RowEditing" OnRowUpdated="ChildRecordsNonRecurringBuffer_RowUpdated" OnRowCancelingEdit="ChildRecordsNonRecurringBuffer_CancelingEdit"
                                                                OnRowDataBound="ChildRecordsNonRecurringBuffer_RowDataBound" OnRowDeleting="ChildRecordsNonRecurringBuffer_RowDeleting" OnRowDeleted="ChildRecordsNonRecurringBuffer_RowDeleted">

                                                                <Columns>

                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblActivityItemId" runat="server" Style="display: none" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Sr. No" ItemStyle-Width="60px">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblId" runat="server" Style="display: none" Text='<%#Eval("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSubItemTypeId" runat="server" Style="display: none" Text='<%#Eval("SubItemId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Sub-Item Type" ItemStyle-Width="150px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%#Eval("SubItemType") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="ParaNo TCP" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                        <ItemTemplate>
                                                                            <%#Eval("ParaNoTCP") %>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtParaNoTCPChild" Text='<%#Eval("ParaNoTCP") %>' runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Name of Sub-Item" ItemStyle-Width="500px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Justify">
                                                                        <ItemTemplate>
                                                                            <%#Eval("SubItem") %>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtSubItemChild" Text='<%#Eval("SubItem") %>' runat="server"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Number of Items" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <%#Eval("NumberOfItems") %>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <cc:CustomTextBox ID="txtNumberOfItemsChildNRBuffer" AutoPostBack="true" Text='<%#Eval("NumberOfItems") %>' runat="server" CssClass="less_size" onFocusout="return validate(this.value,this);" OnTextChanged="txtNumberOfItemsChildNRBuffer_TextChanged" ></cc:CustomTextBox>

                                                                            <%--<asp:TextBox ID="txtNumberOfItemsChild" Text='<%#Eval("NumberOfItems") %>' runat="server"></asp:TextBox>--%>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <%#Eval("UnitPrice") %>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                          <cc:CustomTextBox ID="txtUnitPriceChildNRBuffer" AutoPostBack="true" Text='<%#Eval("UnitPrice") %>' runat="server" CssClass="less_size" onFocusout="return validate(this.value,this);" OnTextChanged="txtUnitPriceChildNRBuffer_TextChanged"></cc:CustomTextBox>

                                                                            <%--<asp:TextBox ID="txtUnitPriceChild" Text='<%#Eval("UnitPrice") %>' runat="server"></asp:TextBox>--%>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Total" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <cc:CustomTextBox ID="lblTotalChild" runat="server" Text='<%#Eval("Total") %>' Enabled="false" ></cc:CustomTextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="GPS" ItemStyle-Width="300px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGPSChild" runat="server" Text='<%#Eval("GPS") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Justification" ItemStyle-Width="500px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Justify">
                                                                        <ItemTemplate>
                                                                            <%#Eval("Justification") %>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <cc:CustomTextBox ID="txtJustificationChild" TextMode="MultiLine" Rows="3" Text='<%#Eval("Justification") %>' runat="server"></cc:CustomTextBox>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Document" ItemStyle-Width="200px">
                                                                        <ItemTemplate>
                                                                            <%--<a href="../downloadFileHandler.ashx?fileName=<%#Eval("Document") %>"><%#Eval("Document") %></a>--%>
                                                                            <cc:CustomLinkButton ID="downloadDocumentID2" Text='<%#Eval("Document") %>' runat="server" OnClick="downloadBtn2_Click" OnClientClick="RemoveBlockUi();"></cc:CustomLinkButton>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Wrap="false">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="linkEditCust2" CommandName="Edit" runat="server">Edit</asp:LinkButton>

                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:LinkButton ID="linkUpdateCust2" CommandName="Update" runat="server" OnClientClick="return confirm('Are you sure update record?');">Update</asp:LinkButton>
                                                                            <asp:LinkButton ID="linkCancelCust2" CommandName="Cancel" runat="server">Cancel</asp:LinkButton>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="linkDeleteCust2" CommandName="Delete" runat="server" OnClientClick="return ConfirmDelete();">Delete</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </cc:CustomGridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </cc:CustomGridView>
                            </div>
                            <div class="text-center TopMargin margin_bottom" style="margin-left: -10vh;" >
                            <%--    <asp:Button ID="btnNRBufferSave" runat="server" Text="Save As Draft" CssClass="btn btn-primary"
                                    OnClientClick="setNonRecuring_Buffer(); SaveAsDraft(); return false;" />--%>
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
                                                    <cc:CustomTextBox ID="txtRCoreTotal" runat="server" CssClass="less_size RCoreSubTotal"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomLabel ID="lblGpsNote" runat="server" CssClass="less_size text-left" Text="Click icon to add GPS"></cc:CustomLabel>
                                                    <cc:CustomImageButton ID="updateSomething" runat="server" CssClass="text-center"
                                                        Style="width: 50px; height: 25px;" ImageUrl="../Images/gps1.jpg"
                                                        OnClientClick="setRecuring_core(this); openDialogue(this,'gvRCore','2','1','EditAPO'); return false;" />
                                                    <br />
                                                    <%--<cc:CustomImageButton ID="imgbtnAddRCoreGPS" runat="server" CssClass="text-center" style="width:50px; height:25px;"
                                                        ImageUrl="../Images/gps1.jpg" RowIndex='<%# Container.DisplayIndex %>' OnClientClick="setRecuring_core()" OnClick="imgbtnAddRCoreGPS_click" /><br />--%>
                                                    <%--<asp:Label ID="lblGPS" runat="server" Text="" CssClass="less_size"></asp:Label>--%>
                                                    <div class="GPSDetailScroll">
                                                        <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                                            Width="160px" DataKeyNames="ActivityItemId" AllowPaging="false" PageSize="5"
                                                            EnableSortingAndPagingCallbacks="false" ShowHeader="false" Visible="false">
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
                                                    <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" onchange='previewFile(this)' />
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
                                                        runat="server" OnClientClick="return AddAPOSubItem(this,'gvRCore','1','2','EditAPO')" ImageUrl="../Images/plus.png" />
                                                    <cc:CustomImageButton ID="imgbtnDeleteRcore" runat="server" OnClientClick="setRecuring_core(); DeleteAPOSubItems(this,'gvRCore','1','2','EditAPO'); return false;  "
                                                        ImageUrl="../Images/remove.png" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td colspan="100%">
                                                            <div style="display: block; position: relative; left: 15px; overflow: auto; width: 97%">

                                                                <%--R Core Child Grid--%>
                                                                <cc:CustomGridView ID="ChildRecordsRecurringNormal" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="ActivityItemId" ShowFooter="false"
                                                                    OnRowUpdating="ChildRecordsRecurringNormal_RowUpdating" OnRowCommand="ChildRecordsRecurringNormal_RowCommand"
                                                                    OnRowEditing="ChildRecordsRecurringNormal_RowEditing" OnRowUpdated="ChildRecordsRecurringNormal_RowUpdated" OnRowCancelingEdit="ChildRecordsRecurringNormal_CancelingEdit"
                                                                    OnRowDataBound="ChildRecordsRecurringNormal_RowDataBound" OnRowDeleting="ChildRecordsRecurringNormal_RowDeleting" OnRowDeleted="ChildRecordsRecurringNormal_RowDeleted">

                                                                    <Columns>

                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblActivityItemId" runat="server" Style="display: none" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Sr. No" ItemStyle-Width="60px">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblId" runat="server" Style="display: none" Text='<%#Eval("ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSubItemTypeId" runat="server" Style="display: none" Text='<%#Eval("SubItemId") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Sub-Item Type" ItemStyle-Width="150px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <%#Eval("SubItemType") %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ParaNo TCP" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <ItemTemplate>
                                                                                <%#Eval("ParaNoTCP") %>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtParaNoTCPChild" Text='<%#Eval("ParaNoTCP") %>' runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Name of Sub-Item" ItemStyle-Width="500px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Justify">
                                                                            <ItemTemplate>
                                                                                <%#Eval("SubItem") %>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtSubItemChild" Text='<%#Eval("SubItem") %>' runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Number of Items" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <%#Eval("NumberOfItems") %>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <cc:CustomTextBox ID="txtNumberOfItemsChildRCore" AutoPostBack="true" Text='<%#Eval("NumberOfItems") %>' runat="server" CssClass="less_size" onFocusout="return validate(this.value,this);" OnTextChanged="txtNumberOfItemsChildRCore_TextChanged"></cc:CustomTextBox>
                                                                                <%--<asp:TextBox ID="txtNumberOfItemsChild" Text='<%#Eval("NumberOfItems") %>' runat="server"></asp:TextBox>--%>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <%#Eval("UnitPrice") %>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                               <cc:CustomTextBox ID="txtUnitPriceChildRCore" AutoPostBack="true" Text='<%#Eval("UnitPrice") %>' runat="server" CssClass="less_size" onFocusout="return validate(this.value,this);" OnTextChanged="txtUnitPriceChildRCore_TextChanged"></cc:CustomTextBox>

                                                                                <%--<asp:TextBox ID="txtUnitPriceChild" Text='<%#Eval("UnitPrice") %>' runat="server"></asp:TextBox>--%>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Total" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <cc:CustomTextBox ID="lblTotalChild" runat="server" Text='<%#Eval("Total") %>' Enabled="false"></cc:CustomTextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="GPS" ItemStyle-Width="300px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGPSChild" runat="server" Text='<%#Eval("GPS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Justification" ItemStyle-Width="500px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Justify">
                                                                            <ItemTemplate>
                                                                                <%#Eval("Justification") %>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <cc:CustomTextBox ID="txtJustificationChild" TextMode="MultiLine" Rows="3" Text='<%#Eval("Justification") %>' runat="server"></cc:CustomTextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Document" ItemStyle-Width="200px">
                                                                            <ItemTemplate>
                                                                                <%--<a href="../downloadFileHandler.ashx?fileName=<%#Eval("Document") %>"><%#Eval("Document") %></a>--%>
                                                                                <cc:CustomLinkButton ID="downloadDocumentID3" Text='<%#Eval("Document") %>' runat="server" OnClick="downloadBtn3_Click" OnClientClick="RemoveBlockUi();"></cc:CustomLinkButton>

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-Wrap="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="linkEditCust3" CommandName="Edit" runat="server">Edit</asp:LinkButton>

                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:LinkButton ID="linkUpdateCust3" CommandName="Update" runat="server" OnClientClick="return confirm('Are you sure update record?');">Update</asp:LinkButton>
                                                                                <asp:LinkButton ID="linkCancelCust3" CommandName="Cancel" runat="server">Cancel</asp:LinkButton>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="linkDeleteCust3" CommandName="Delete" runat="server" OnClientClick="return ConfirmDelete();">Delete</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </cc:CustomGridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin"  style="margin-left: -10vh;">
                                   <%-- <asp:Button ID="btnRCoreSave" runat="server" Text="Save As Draft" CssClass="btn btn-primary"
                                        OnClientClick="setRecuring_Core(); SaveAsDraft(); return false;" />--%>
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
                                                    <cc:CustomTextBox ID="txtRBufferTotal" runat="server" CssClass="less_size RBufferSubTotal"
                                                        Text="" Enabled="False" placeholder="Total Auto-Generated"></cc:CustomTextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <cc:CustomLabel ID="lblGpsNote" runat="server" CssClass="less_size text-left" Text="Click icon to add GPS"></cc:CustomLabel>
                                                    <cc:CustomImageButton ID="updateSomething" runat="server" CssClass="text-center"
                                                        Style="width: 50px; height: 25px;" ImageUrl="../Images/gps1.jpg"
                                                        OnClientClick="setRecuring_Buffer(this); openDialogue(this,'gvRBuffer','2','2','EditAPO'); return false;" /><br />
                                                    <%--<cc:CustomImageButton ID="imgbtnAddRBufferGPS" runat="server" CssClass="text-center" style="width:50px; height:25px;"
                                                        ImageUrl="../Images/gps1.jpg" RowIndex='<%# Container.DisplayIndex %>' OnClientClick="setRecuring_Buffer()" OnClick="imgbtnAddRBufferGPS_click" /><br />--%>
                                                    <%--<asp:Label ID="lblGPS" runat="server" Text="" CssClass="less_size"></asp:Label>--%>
                                                    <div class="GPSDetailScroll">
                                                        <cc:CustomGridView ID="gvGpsDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                                            Width="160px" DataKeyNames="ActivityItemId" AllowPaging="false" PageSize="5"
                                                            EnableSortingAndPagingCallbacks="false" ShowHeader="false" Visible="false">
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
                                                    <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" onchange='previewFile(this)' />
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
                                                        runat="server" OnClientClick="return AddAPOSubItem(this,'gvRBuffer','2','2','EditAPO')" ImageUrl="../Images/plus.png" />
                                                    <cc:CustomImageButton ID="imgbtnDeleteRbuffer" runat="server" OnClientClick="setRecuring_Buffer(); DeleteAPOSubItems(this,'gvRBuffer','2','2','EditAPO'); return false;"
                                                        ImageUrl="../Images/remove.png" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td colspan="100%">
                                                            <div style="display: block; position: relative; left: 15px; overflow: auto; width: 97%">
                                                                <%--R Buffer Child Grid--%>
                                                                <cc:CustomGridView ID="ChildRecordsRecurringBuffer" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="ActivityItemId" ShowFooter="false"
                                                                    OnRowUpdating="ChildRecordsRecurringBuffer_RowUpdating" OnRowCommand="ChildRecordsRecurringBuffer_RowCommand"
                                                                    OnRowEditing="ChildRecordsRecurringBuffer_RowEditing" OnRowUpdated="ChildRecordsRecurringBuffer_RowUpdated" OnRowCancelingEdit="ChildRecordsRecurringBuffer_CancelingEdit"
                                                                    OnRowDataBound="ChildRecordsRecurringBuffer_RowDataBound" OnRowDeleting="ChildRecordsRecurringBuffer_RowDeleting" OnRowDeleted="ChildRecordsRecurringBuffer_RowDeleted">

                                                                    <Columns>

                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblActivityItemId" runat="server" Style="display: none" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Sr. No">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblId" runat="server" Style="display: none" Text='<%#Eval("ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSubItemTypeId" runat="server" Style="display: none" Text='<%#Eval("SubItemId") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Sub-Item Type" ItemStyle-Width="150px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <%#Eval("SubItemType") %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ParaNo TCP" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <ItemTemplate>
                                                                                <%#Eval("ParaNoTCP") %>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtParaNoTCPChild" Text='<%#Eval("ParaNoTCP") %>' runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Name of Sub-Item" ItemStyle-Width="500px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Justify">
                                                                            <ItemTemplate>
                                                                                <%#Eval("SubItem") %>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtSubItemChild" Text='<%#Eval("SubItem") %>' runat="server"></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Number of Items" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <%#Eval("NumberOfItems") %>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <cc:CustomTextBox ID="txtNumberOfItemsChildRBuffer" AutoPostBack="true" Text='<%#Eval("NumberOfItems") %>' runat="server" CssClass="less_size" onFocusout="return validate(this.value,this);" OnTextChanged="txtNumberOfItemsChildRBuffer_TextChanged"></cc:CustomTextBox>

                                                                                <%--<asp:TextBox ID="txtNumberOfItemsChild" Text='<%#Eval("NumberOfItems") %>' runat="server"></asp:TextBox>--%>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <%#Eval("UnitPrice") %>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                               <cc:CustomTextBox ID="txtUnitPriceChildRBuffer" AutoPostBack="true" Text='<%#Eval("UnitPrice") %>' runat="server" CssClass="less_size" onFocusout="return validate(this.value,this);" OnTextChanged="txtUnitPriceChildRBuffer_TextChanged"></cc:CustomTextBox>

                                                                                <%--<asp:TextBox ID="txtUnitPriceChild" Text='<%#Eval("UnitPrice") %>' runat="server"></asp:TextBox>--%>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Total" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right">
                                                                            <ItemTemplate>
                                                                                <cc:CustomTextBox ID="lblTotalChild" runat="server" Text='<%#Eval("Total") %>' Enabled="false"></cc:CustomTextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="GPS" ItemStyle-Width="300px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGPSChild" runat="server" Text='<%#Eval("GPS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Justification" ItemStyle-Width="500px" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Justify">
                                                                            <ItemTemplate>
                                                                                <%#Eval("Justification") %>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <cc:CustomTextBox ID="txtJustificationChild" TextMode="MultiLine" Rows="3" Text='<%#Eval("Justification") %>' runat="server"></cc:CustomTextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Document" ItemStyle-Width="200px">
                                                                            <ItemTemplate>
                                                                                <%--<a href="../downloadFileHandler.ashx?fileName=<%#Eval("Document") %>"><%#Eval("Document") %></a>--%>
                                                                                <cc:CustomLinkButton ID="downloadDocumentID4" Text='<%#Eval("Document") %>' runat="server" OnClick="downloadBtn4_Click" OnClientClick="RemoveBlockUi();"></cc:CustomLinkButton>

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-Wrap="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="linkEditCustreB4" runat="server" CommandName="Edit">Edit</asp:LinkButton>

                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:LinkButton ID="linkUpdateCustreB4" CommandName="Update" runat="server" OnClientClick="return confirm('Are you sure update record?');">Update</asp:LinkButton>
                                                                                <asp:LinkButton ID="linkCancelCustreB4" CommandName="Cancel" runat="server">Cancel</asp:LinkButton>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delete">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="linkDeleteCustreB4" CommandName="Delete" runat="server" OnClientClick="return ConfirmDelete();">Delete</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </cc:CustomGridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin" style="margin-left: -10vh;">
                               <%--     <asp:Button ID="btnRBufferSave" runat="server" Text="Save As Draft" CssClass="btn btn-primary"
                                        OnClientClick="setRecuring_Buffer(); SaveAsDraft(); return false;" />--%>
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
