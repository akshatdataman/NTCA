<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="EditEcoDevelopment.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.EditEcoDevelopment" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $('.dropdown').hover(function () {
            $(this).find('.dropdown-menu').first().stop(true, true).slideDown(150);
        }, function () {
            $(this).find('.dropdown-menu').first().stop(true, true).slideUp(105)
        });
    </script>
    <script type="text/javascript">
        $(function () {

            $("#core1").addClass("in");
            $("#core1").addClass("active");

            $("#mytab li a").click(function () {

                var a = $(this).attr("href");
                $(document).find(".active").removeClass("active");
                $(this).parent().addClass("active");
                $(".fade").removeClass("in");
                $(".fade").removeClass("active");
                $("div" + a).addClass("in");
                $("div" + a).addClass("active");
            });
            $("#core").addClass("in");
            $("#core").addClass("active1");
            $("#mytab1 li a").click(function () {
                var a = $(this).attr("href");
                $(document).find(".active1").removeClass("active1");
                $(this).parent().addClass("active1");
                $(".fade1").removeClass("in");
                $(".fade1").removeClass("active1");
                $("div" + a).addClass("in");
                $("div" + a).addClass("active1")

            });

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
    </script>
    <script type="text/javascript">
        function validate(val) {
            var e = event || evt; // for trans-browser compatibility
            var charCode = e.which || e.keyCode;

            // for only numeric and dot allowed
            if (!(charCode == 46 || (charCode >= 48 && charCode <= 57))) {
                alert("Only numeric and dot allowed");
                return false;
            }
            //for press char value
            var curchar = String.fromCharCode(charCode);

            //concate previous value with current press value
            var mainstring = val + curchar;

            //for only one dot allowed
            if (mainstring.indexOf('.') > -1) {
                if (mainstring.split('.').length > 2) {
                    alert("Only one dot allowed");
                    return false;
                }
            }
            //for get beforeDecimal value string
            var beforeDecimal = mainstring;
            if (mainstring.indexOf('.') != -1) {
                beforeDecimal = mainstring.substring(0, mainstring.indexOf('.') - 1);
            }
            //for get afterDecimal value string
            var afterDecimal = '';
            if (mainstring.indexOf('.') != -1) {
                afterDecimal = mainstring.substring(mainstring.indexOf('.') + 1, mainstring.length);
            }
            //for check before decimal digit length
            if (beforeDecimal.length > 10) {
                alert("Maximum 10 digit allowed before decimal");
                return false;
            }
            //for check after decimal digit length
            if (afterDecimal.length > 5) {
                alert("Only five decimal places allowed");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div class="row minheight">
        <h4 id="SubmitApoHeader" runat="server">
            <b>Submit Apo</b>
        </h4>
        <div class="col-sm-3">
            <div id="leftPartMy1" class="">
                <div id="leftNav">
                    <div class="panel-group" id="accordion">
                        <div class=" panel-default ">
                            <div id="panel1" class="panel-collapse collapse in menuleft">
                                <div class="table-responsive">
                                    <ul>
                                        <li class="FirstSubMenu1  BackgroundColor"><a class="submenu" href="SubmitAPO.aspx">
                                            Non-Recurring</a></li>
                                        <li class="BackgroundColor"><a href="SubmitAPO.aspx">Recurring</a></li>
                                        <%--<li class="BackgroundColor Current"><a href="EditEcoDevelopment.aspx">Eco Development</a></li>--%>
                                        <li class="BackgroundColor"><a href="FDUtilizationCertificate.aspx">Utilization Certificate</a></li>
                                        <%--<li class="BackgroundColor "><a href="Spillover.aspx">Spill Over</a></li>--%>
                                        <%--<li class="BackgroundColor "><a href="#">Revalidation</a></li>
                                        <li class="BackgroundColor "><a href="#">Adjustment</a></li>--%>
                                        <li class="BackgroundColor "><a href="ObligationFD.aspx">Obligation Under Tri-MOU</a></li>
                                        <%--<li class="BackgroundColor "><a href="FDFeedbackMOU.aspx">Feedback of Compliance MOU</a></li>--%>
                                        <li class="BackgroundColor"><a href="CheckList.aspx?callfrom=SubmitAPO">Checklist</a></li>
                                        <li class="BackgroundColor"><a href="FDFlexiAmont.aspx">Flexi Amount</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-9  leftBorder minHeight Home_margin" id="MainContent">
            <!-- Non-Recurring-->
            <div id="Non-RecurringDiv" class="minHeight">
                <div class="col-sm-12">
                    <div class="text-center col-sm-9  MessagePadding">
                        <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                            <uc:ValidationMessage ID="vmError" runat="server" />
                        </div>
                        <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                            <uc:ValidationMessage ID="vmSuccess" runat="server" />
                        </div>
                    </div>
                </div>
                <div id="">
                    <div class="row">
                        <div class="col-sm-6">
                        </div>
                        <div class="col-sm-offset-2 col-sm-4 linkPrevious">
                            <a href="ViewPfyAPO.aspx?PFY=True">Click to View Previous Year</a></div>
                    </div>
                    <!--here set the tabs code-->
                    <div class="bs-example">
                        <ul class="nav nav-tabs" id="mytab">
                            <li class="active"><a data-toggle="tab" href="#core1">Core</a></li>
                            <li><a data-toggle="tab" href="#Buffer1">Buffer</a></li>
                        </ul>
                        <div class="tab-content">
                            <div id="core1" class="tab-pane fade">
                                <div class="tablettcolor">
                                    <cc:CustomGridView ID="gvEcoDevCore" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-responsive tablett ">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Activity">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Para No. CSS PT Guidelines">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnParaNo" runat="server" CssClass="LinkColor" Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Para No. TCP">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTcpParaNo" runat="server" />
                                                    <cc:CustomTextBox ID="txtParaNo" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. of Items (Physical Target)">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtNumberOfItem" runat="server" req="1" CssClass="less_size" Text=""
                                                        onKeyPress="return validate(this.value);"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification / Description">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSpcification" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Price">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtUnitPrice" runat="server" CssClass="less_size" Text="" onKeyPress="return validate(this.value);"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total (Financial Target)">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtTotal" runat="server" CssClass="less_size" Text="" Enabled="False"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GPS">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtGPS" runat="server" CssClass="less_size" Text="" MaxLength="50"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Justification">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtJustification" runat="server" TextMode="MultiLine" Rows="5" CssClass="less_size"
                                                        Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload Document">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin margin_bottom">
                                    <asp:Button ID="btnEcoDevCoreSave" runat="server" Text="Save" CssClass="btn btn-primary"
                                        OnClick="btnEcoDevCoreSave_Click" />
                                    <asp:Button ID="btnCoreCancel" runat="server" Text="Back" CssClass="btn btn-primary col-sm-offset-1"
                                        OnClick="btnCoreCancel_Click" />
                                </div>
                            </div>
                            <div id="Buffer1" class="tab-pane fade">
                                <div class="tablettcolor">
                                    <cc:CustomGridView ID="gvEcoDevBuffer" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-responsive tablett ">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Activity">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Para No. CSS PT Guidelines">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnParaNo" runat="server" CssClass="LinkColor" Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Para No. TCP">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnTcpParaNo" runat="server" />
                                                    <cc:CustomTextBox ID="txtParaNo" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. of Items (Physical Target)">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtNumberOfItem" runat="server" req="1" CssClass="less_size" Text=""
                                                        onKeyPress="return validate(this.value);"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification / Description">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtSpcification" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Price">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtUnitPrice" runat="server" CssClass="less_size" Text="" onKeyPress="return validate(this.value);"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total (Financial Target)">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtTotal" runat="server" CssClass="less_size" Text="" Enabled="False"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GPS">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtGPS" runat="server" CssClass="less_size" Text=""MaxLength="50"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Justification">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtJustification" runat="server" TextMode="MultiLine" Rows="5" CssClass="less_size"
                                                        Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload Document">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fuUploadDocument" runat="server" CssClass="fileuploadSize" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin margin_bottom">
                                    <asp:Button ID="btnEcoDevBufferSave" runat="server" Text="Save" CssClass="btn btn-primary"
                                        OnClick="btnEcoDevBufferSave_Click" />
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
        </div>
    </div>
</asp:Content>
