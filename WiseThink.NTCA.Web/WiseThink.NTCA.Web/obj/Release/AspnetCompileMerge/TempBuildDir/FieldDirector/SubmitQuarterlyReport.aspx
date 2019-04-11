<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="SubmitQuarterlyReport.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.SubmitQuarterlyReport" %>

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
            if (afterDecimal.length > 2) {
                alert("Only two decimal places allowed");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div class="row minheight">
        <h4 id="SubmitApoHeader" runat="server">
            <b>Submit Monthly Report</b></h4>

        <%--<div class="col-sm-2">
            <div id="leftPartMy1" class="">
                <div id="leftNav">
                    <div class="panel-group" id="accordion">
                        <div class=" panel-default ">
                            <div id="panel1" class="panel-collapse collapse in menuleft">
                                <div class="table-responsive">
                                    <ul>
                                        <li  class="FirstSubMenu1 Current BackgroundColor">
                                            <asp:LinkButton ID="lbtnNonRecurring" runat="server" CssClass="SubMenu" OnClick="lbtnNonRecurring_Click"> Step 1: Non-Recurring</asp:LinkButton></li>
                                       <li class="BackgroundColor">
                                           <asp:LinkButton ID="lbtnRecurring" runat="server" CssClass="SubMenu" OnClick="lbtnRecurring_Click"> Step 2: Recurring</asp:LinkButton></li>
                                      
                                        <li class="BackgroundColor"><a href="FDUtilizationCertificate.aspx">Step 3: Utilization Certificate</a></li>
                                      
                                        <li class="BackgroundColor "><a href="ObligationFD.aspx">Step 4: Obligation Under Tri-MOU</a></li>
                                       
                                        <li class="BackgroundColor"><a href="CheckList.aspx?callfrom=SubmitAPO">Step 5: Check and Submit</a></li>


                                         <%--  <li class="FirstSubMenu1 Current BackgroundColor"><a class="submenu" href="SubmitQuarterlyReport.aspx">
                                            Step 1: Non-Recurring</a></li>
                                        <li class="BackgroundColor"><a href="#RecuringDiv">Step 2: Recurring</a></li>
                                        <li class="BackgroundColor"><a href="#Utilization">Step 3: Utilization Certificate</a></li>                                       
                                        <li class="BackgroundColor "><a href="ObligationFD.aspx">Step 4: Obligation Under Tri-MOU</a></li>
                                        <li class="BackgroundColor"><a href="CheckList.aspx?callfrom=SubmitAPO">Step 5: Check and Submit</a></li>
                                      
                                       
                                       
                                        
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="col-sm-12  leftBorder minHeight " id="MainContent">
            <!-- Non-Recurring-->
            <div id="NonRecurringDiv" class="minHeight" runat="server">

                <div class="text-center row MessagePadding form-group">
                    <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                </div>
                <div id="" class="text-center">
                    <div id="Monthly" runat="server" class="row TopMargin">
                        <div class="col-sm-4">
                            <cc:CustomLabel ID="lblMonth" runat="server" Text="Select Month"></cc:CustomLabel>
                        </div>
                        <div class="col-sm-4">
                            <cc:CustomDropDownList ID="ddlNRMonth" CssClass="form-control textwidh" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlNRMonth_SelectedIndexChanged">
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
                    <div id="Periodic" runat="server" class="row TopMargin">
                        <div class="col-sm-2">
                            <cc:CustomLabel ID="lblFromdate" runat="server" Text="Select From Date"></cc:CustomLabel>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control cal"></asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <cc:CustomLabel ID="lblToDate" runat="server" Text="Select To Date"></cc:CustomLabel>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control cal"></asp:TextBox>
                        </div>
                    </div>

                    <!--here set the tabs code-->
                    <div class="bs-example TopMargin">
                        <div class="tab-content">
                            <div id="core1" class="tab-pane fade">
                                <div class="tablettcolor">
                                    <cc:CustomGridView ID="gvNonRecurring" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ShowHeader="false"
                                        OnRowCreated="gvNonRecurring_RowCreated" DataKeyNames="ID"
                                        CssClass="table table-bordered table-responsive tablett TopMargin">
                                        <%-- <EmptyDataTemplate>
                                            <div class="EmpltyGridView">
                                                &quot; Sorry! There is no data &quot;</div>
                                        </EmptyDataTemplate>--%>
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                    <asp:Label ID="lblActivityTypeId" runat="server" Visible="false" Text='<%#Eval("ActivityTypeId") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Activity">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityTypeId" runat="server" Visible="false" Text='<%#Eval("ActivityTypeId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="500px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sanctioned Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sanctioned Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Central Share">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCentralShare" runat="server" Text='<%#Eval("CentralShare") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="StateShare">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStateShare" runat="server" Text='<%#Eval("StateShare") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReleasedAmount" runat="server" Text='<%#Eval("ReleasedAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Quantity Assessment">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtQuantity" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Location">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtLocation" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Assessment Physical Target">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtPhysicalProgress" runat="server" req="1" CssClass="less_size" Text=""
                                                        TextMode="MultiLine" Rows="4" Columns="3"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Assessment Financial Target">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtCentralFinancialProgress" runat="server" CssClass="less_size" Text="" OnTextChanged="txtCentralFinancialProgress_TextChanged" AutoPostBack="true"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtStateFinancialProgress" runat="server" CssClass="less_size" Text="" OnTextChanged="txtStateFinancialProgress_TextChanged" AutoPostBack="true"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtTotalProgress" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span class="text-danger">Sorry ! There is no Data to Display></span>
                                        </EmptyDataTemplate>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin">
                                    <cc:CustomButton ID="btnNonRecurringSave" runat="server" Text="Submit" CssClass="btn btn-primary"
                                        OnClientClick="return confirm('Are you sure? you want to save non-recurring quartly report data!');" OnClick="btnNonRecurringSave_Click" />
                                    <%--<cc:CustomButton ID="btnBack" CssClass="btn-primary btn col-sm-offset-0" runat="server"
                                        PostBackUrl="FieldDirectorHome.aspx" Text="Back" />--%>
                                    <button id="Button1" type="button" onclick="window.location.href = 'FieldDirectorHome.aspx'"
                                        class="btn-primary btn col-sm-offset-0">
                                        Back</button>
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
            <div id="RecuringDiv" class="minHeight TopMargin" runat="server">

                <div id="rightPartMy1">
                    <div class="row">
                        <div class="col-sm-2">
                            <cc:CustomLabel ID="lblRQuarter" runat="server" Text="Select Month"></cc:CustomLabel>
                        </div>
                        <div class="col-sm-4">
                            <cc:CustomDropDownList ID="ddlRQuarter" CssClass="form-control textwidh" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlNRMonth_SelectedIndexChanged">
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
                            <asp:RequiredFieldValidator ID="reqFavoriteColor" Text="(Required)" InitialValue="0" ControlToValidate="ddlRQuarter" runat="server" />
                        </div>
                    </div>
                    <!--Here is Recuring-->
                    <div class="bs-example TopMargin">
                        <div class="tab-content">
                            <div id="core" class="tab-pane fade1">
                                <div class="tablettcolor">
                                    <cc:CustomGridView ID="gvRecurring" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" ShowHeaderWhenEmpty="true"
                                        ShowHeader="false" OnRowCreated="gvRecurring_RowCreated" CssClass="table table-bordered table-responsive tablett ">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                    <asp:Label ID="lblActivityTypeId" runat="server" Visible="false" Text='<%#Eval("ActivityTypeId") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Activity">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                    <asp:Label ID="lblActivityTypeId" runat="server" Visible="false" Text='<%#Eval("ActivityTypeId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="500px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sanctioned Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sanctioned Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Central Govt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCentralShare" runat="server" Text='<%#Eval("CentralShare") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State Govt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStateShare" runat="server" Text='<%#Eval("StateShare") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReleasedAmount" runat="server" Text='<%#Eval("ReleasedAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Quantity Assessment">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtQuantity" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Location">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtLocation" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Assessment Physical Target">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtPhysicalProgress" runat="server" req="1" CssClass="less_size" Text=""
                                                        TextMode="MultiLine" Rows="4" Columns="3"></cc:CustomTextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress Assessment Financial Target">
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtCentralFinancialProgress" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtStateFinancialProgress" runat="server" CssClass="less_size" Text="" OnTextChanged="txtStateFinancialProgress_TextChanged" AutoPostBack="true"></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <cc:CustomTextBox ID="txtTotalProgress" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span class="text-danger">Sorry ! There is no Data to Display</span>
                                        </EmptyDataTemplate>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin">
                                    <asp:Button ID="btnRecurringSave" runat="server" Text="Submit" CssClass="btn btn-primary"
                                        OnClientClick="return confirm('Are you sure? you want to save recurring quartly report data!');" OnClick="btnRecurringSave_Click" />
                                    <%-- <cc:CustomButton ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-primary btn col-sm-offset-1"
                          OnClientClick="return confirm('Are you sure? you want to submit quartly report!');"  OnClick="btnSubmit_Click" />--%>
                                    <button id="Button2" type="button" onclick="window.location.href = 'FieldDirectorHome.aspx'"
                                        class="btn-primary btn col-sm-offset-0">
                                        Back</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
