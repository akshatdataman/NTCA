<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master" AutoEventWireup="true" CodeBehind="ViewEcoDevelopment.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.EcoDevelopment" %>
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
        <h4 id="SubmitApoHeader" runat="server" ><b>View Apo</b>
        <cc:CustomImageButton ID="imgbtnPdf" runat="server" ImageUrl="~/Images/samplePDFIcon.png" OnClick="imgbtnPdf_click" CssClass="col-sm-offset-9"/>
        <cc:CustomImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/Images/btn-print.png" OnClientClick = "return printGrid();"/>
        </h4>
        </div>
        <%--<div class="col-sm-2 TopImgPadding">
              <cc:CustomImageButton ID="imgbtnPdf" runat="server" ImageUrl="~/Images/samplePDFIcon.png" OnClick="imgbtnPdf_click" />
                            <cc:CustomLinkButton ID="lnkPrint" CommandName="print" runat="server" OnClientClick="return confirm('Are you sure?')"><img src="../Images/btn-print.png" alt="print group" /></cc:CustomLinkButton>
			 </div>--%>
        <div class="col-sm-3">
            <div id="leftPartMy1" class="">
                <div id="leftNav">
                    <div class="panel-group" id="accordion">
                        <div class=" panel-default ">
                            <div id="panel1" class="panel-collapse collapse in menuleft">
                                <div class="table-responsive">
                                    <ul>
                                        <li class="FirstSubMenu1  BackgroundColor"><a class="submenu" href="#Non-RecurringDiv">
                                            Non-Recurring</a></li>
                                        <li class="BackgroundColor"><a href="#RecuringDiv">Recurring</a></li>
                                        <%--<li class="BackgroundColor Current"><a href="ViewEcoDevelopment.aspx">Eco Development</a></li>--%>
                                        <li class="BackgroundColor"><a href="#Utilization">Utilization Certificate</a></li>
                                        <li class="BackgroundColor "><a href="#">Spill Over</a></li>
                                        <li class="BackgroundColor "><a href="#">Revalidation</a></li>
                                        <li class="BackgroundColor "><a href="#">Adjustment</a></li>
                                        <li class="BackgroundColor "><a href="FDUtilizationCertificate.aspx">Obligations</a></li>
                                        <li class="BackgroundColor "><a href="FDFeedbackMOU.aspx">Feedback of Compliance MOU</a></li>
                                        <li class="BackgroundColor"><a href="CheckList.aspx?callfrom=SubmitAPO">Checklist</a></li>
                                        <%--<li class="BackgroundColor"><a href="FDFlexiAmont.aspx">Flexi Amount</a></li>--%>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-9  leftBorder minHeight" id="MainContent">
            <!-- Non-Recurring-->
            <div id="Non-RecurringDiv" class="minHeight">
                <div id="">
                    <div class="row">
                        <div class="col-sm-6">
                           
                        </div>
                        <div class="col-sm-offset-2 col-sm-4 linkPrevious">
                            <a href="#">Click to View Previous Year</a></div>
                    </div>
                    <!--here set the tabs code-->
                    
                        <div class="tab-content">
                            <div id="core1" class="tab-pane fade">
                                <div class="tablettcolor">
                                    <cc:CustomGridView ID="gvNonRecurring" runat="server" AutoGenerateColumns="false"
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
                                            <asp:TemplateField HeaderText="Total (Financial Target)">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total") %>'></asp:Label>
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
                                            <asp:TemplateField HeaderText="Upload Document">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocument" runat="server" Text='<%#Eval("Document") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>
                                <div class="text-center TopMargin">
                                <asp:Button ID="btnBack" runat="server" Text="Back" 
                                        CssClass="btn btn-primary col-sm-offset-1" onclick="btnBack_Click" />
                                </div>
                            </div>
                        </div>
                    
                    <!--here set the tabs code-->
                    <div class="table-responsive tableWidthAPO">
                    </div>
                </div>
            </div>
            <div id="RecuringDiv" class="minHeight">
                <div id="rightPartMy1">
                    <div class="row">
                        <div class="col-sm-6">
                        </div>
                        <div class="col-sm-offset-2 col-sm-4 linkPrevious">
                            <a href="#">Click to View Previous Year</a></div>
                    </div>
                    <!--Here is Recuring-->
                   
                        <div class="tab-content">
                            <div id="core" class="tab-pane fade1">
                                <div class="tablettcolor">
                                    <cc:CustomGridView ID="gvRecuring" runat="server" AutoGenerateColumns="false"
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
                                            <asp:TemplateField HeaderText="Total (Financial Target)">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total") %>'></asp:Label>
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
                                            <asp:TemplateField HeaderText="Upload Document">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocument" runat="server" Text='<%#Eval("Document") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </div>

                                 <div class="text-center TopMargin">
                                <asp:Button ID="btnRecurring" runat="server" Text="Back" CssClass="btn btn-primary col-sm-offset-1" />
                                </div>
                            </div>
                            
                        </div>
                   
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
