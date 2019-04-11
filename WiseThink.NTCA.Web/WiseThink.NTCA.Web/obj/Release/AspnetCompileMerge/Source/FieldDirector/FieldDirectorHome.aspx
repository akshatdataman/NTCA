
<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="FieldDirectorHome.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.WebForm5" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/jscript" language="javascript">
    $(document).ready(function () {
        AddActiveClass("Home", "", "");
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div>
        <%--<--Set DashBord -->--%>
        <div class="row minheight">
            <h4>
                <b>Field Director Home</b></h4>
            <div class="">
                <div class="col-sm-8 NotificaitonBorder minheight Home_margin">
                    <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                    <div id="content" class="TopMargin">
                        <h3 class="panel image">
                            Status of APO <span id="APOCountSpan" runat="server" class="">&nbsp;(2)</span></h3>
                        <div id="divTable" class="collapse table-responsive">
                            <asp:UpdatePanel ID="uppnlStatus" runat="server">
                                <ContentTemplate>
                                    <cc:CustomGridView ID="cgvAPoSubmitted" runat="server" AutoGenerateColumns="false"
                                        AllowPaging="true" PageSize="5" CssClass="table table-bordered tablett" Width="100%"
                                        DataKeyNames="APOID" OnPageIndexChanging="cgvAPoSubmitted_PageIndexChanging"
                                        OnRowCommand="cgvAPoSubmitted_RowCommand" OnRowDataBound="cgvAPoSubmitted_RowDataBound">
                                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                            Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                        <EmptyDataTemplate>
                                            <div class="EmpltyGridView">
                                                &quot; Sorry! There is no data &quot;</div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <%--<asp:BoundField HeaderText="Sr.No." DataField="SrNo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                <ItemTemplate>
                                                    <cc:CustomLabel ID="lblSrNo" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></cc:CustomLabel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="APO Title">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAPoTitle" runat="server" CssClass="LinkColor" Text='<%#Eval("APOTitle")%>'
                                                        CommandArgument='<%#Eval("APOID") %>' CommandName="VIEW"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="APO File No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileNoumber" runat="server" Text='<%#Eval("APOFileNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <%-- <div id="viewbtnwithmenu" class="">
                                                <cc:CustomImageButton ID="ibtnAction" runat="server" ImageUrl="../Images/viewall.png" />
                                                <ul class="table-responsive">
                                                    <li><a href="#">Send Notification</a></li>
                                                    <li><a href="#">View/Add Comments</a></li>
                                                    <li><a href="#">Versions</a></li>
                                                </ul>
                                            </div>--%>
                                                    <span class="dropdown">
                                                        <button class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" data-hover="dropdown">
                                                            Action <span class="caret"></span>
                                                        </button>
                                                        <ul class="dropdown-menu hover">
                                                            <li><a href="SendAlert.aspx">Send Alert</a></li>
                                                            <%--<li><a href="FDUtilizationCertificate.aspx">Utilization Certificate</a></li>--%>
                                                            <li>
                                                                <cc:CustomLinkButton ID="lbViewUc" runat="server" CommandName="GetUc" CommandArgument='<%# Eval("APOID")%>'>Utilization Certificate</cc:CustomLinkButton>
                                                            </li>
                                                            <%--<li><a href="SubmitAPO.aspx">Edit/Modify</a></li>--%>

                                                            <%--<li>
                                                                <cc:CustomLinkButton ID="clbProvisionalUc" runat="server" CommandArgument='<%#Eval("APOID") %>'
                                                                    CommandName="downloadProvisionalUc">Download Provisional UC</cc:CustomLinkButton>
                                                            </li>
                                                            <li>
                                                                <cc:CustomLinkButton ID="clbFinalUc" runat="server" CommandArgument='<%#Eval("APOID") %>'
                                                                    CommandName="downloadFinalUc">Download Final UC</cc:CustomLinkButton>
                                                            </li>--%>
                                                            <li>
                                                                <cc:CustomLinkButton ID="lbEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("APOID")%>'>Edit / Modify</cc:CustomLinkButton>
                                                            </li>
                                                            <li>
                                                                <cc:CustomLinkButton ID="CustomLinkButton1" runat="server" CommandName="ViewTRDetail"
                                                                    CommandArgument='<%# Eval("APOID")%>'>View Tiger Reserve Details</cc:CustomLinkButton>
                                                            </li>
                                                            <%--<li><a href="#">Versions</a></li>--%>
                                                            <%--<li><a href="UpdateAPOStatus.aspx">Update APO Status</a></li>--%>
                                                            <li>
                                                                <cc:CustomLinkButton ID="lbObligation" runat="server" CommandName="Submit" CommandArgument='<%# Eval("APOID")%>'>Obligation Under Tri-MOU</cc:CustomLinkButton>
                                                            </li>
                                                            <li><a href="FDFeedbackMOU.aspx">Feedback of Compliance MOU</a></li>
                                                        </ul>
                                                    </span>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cgvAPoSubmitted" EventName="PageIndexChanging" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div id="Additional" runat="server">
                            <h3 class="panel image">
                                Status of Additional APO <span id="AdditionalAPOCountSpan" runat="server" class="">&nbsp;(2)</span></h3>
                            <div id="div2" class="collapse table-responsive">
                                <asp:UpdatePanel ID="uppnlAdditional" runat="server">
                                    <ContentTemplate>
                                        <cc:CustomGridView ID="cgvAdditionalApo" runat="server" AutoGenerateColumns="false"
                                            AllowPaging="true" PageSize="5" CssClass="table table-bordered tablett" Width="100%"
                                            DataKeyNames="APOID" OnPageIndexChanging="cgvAdditionalApo_PageIndexChanging"
                                            OnRowCommand="cgvAdditionalApo_RowCommand" OnRowDataBound="cgvAdditionalApo_RowDataBound">
                                            <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                                Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                            <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                            <EmptyDataTemplate>
                                                <div class="EmpltyGridView">
                                                    &quot; Sorry! There is no data &quot;</div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <%--<asp:BoundField HeaderText="Sr.No." DataField="SrNo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblSrNo" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="APO Title">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAPoTitle" runat="server" CssClass="LinkColor" Text='<%#Eval("APOTitle")%>'
                                                            CommandArgument='<%#Eval("APOID") %>' CommandName="VIEW"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="APO File No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileNoumber" runat="server" Text='<%#Eval("APOFileNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <%-- <div id="viewbtnwithmenu" class="">
                                                <cc:CustomImageButton ID="ibtnAction" runat="server" ImageUrl="../Images/viewall.png" />
                                                <ul class="table-responsive">
                                                    <li><a href="#">Send Notification</a></li>
                                                    <li><a href="#">View/Add Comments</a></li>
                                                    <li><a href="#">Versions</a></li>
                                                </ul>
                                            </div>--%>
                                                        <span class="dropdown">
                                                            <button class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" data-hover="dropdown">
                                                                Action <span class="caret"></span>
                                                            </button>
                                                            <ul class="dropdown-menu hover">
                                                                <li><a href="SendAlert.aspx">Send Alert</a></li>
                                                                <%--<li><a href="FDUtilizationCertificate.aspx">Utilization Certificate</a></li>--%>
                                                                <li>
                                                                    <cc:CustomLinkButton ID="lbViewUc" runat="server" CommandName="GetUc" CommandArgument='<%# Eval("APOID")%>'>Utilization Certificate</cc:CustomLinkButton>
                                                                </li>
                                                                <li>
                                                                    <cc:CustomLinkButton ID="clbProvisionalUc" runat="server" OnClientClick="RemoveBlockUi();" CommandArgument='<%#Eval("APOID") %>'
                                                                        CommandName="downloadProvisionalUc">Download Provisional UC</cc:CustomLinkButton>
                                                                </li>
                                                                <li>
                                                                    <cc:CustomLinkButton ID="clbFinalUc" runat="server" CommandArgument='<%#Eval("APOID") %>'
                                                                        CommandName="downloadFinalUc">Download Final UC</cc:CustomLinkButton>
                                                                </li>
                                                                <%--<li><a href="SubmitAPO.aspx">Edit/Modify</a></li>--%>
                                                                <li>
                                                                    <cc:CustomLinkButton ID="lbEdit" runat="server" CommandName="EditAdditional" CommandArgument='<%# Eval("APOID")%>'>Edit / Modify</cc:CustomLinkButton>
                                                                </li>
                                                                <li>
                                                                    <cc:CustomLinkButton ID="CustomLinkButton1" runat="server" CommandName="ViewTRDetail"
                                                                        CommandArgument='<%# Eval("APOID")%>'>View Tiger Reserve Details</cc:CustomLinkButton>
                                                                </li>
                                                                <%--<li><a href="#">Versions</a></li>--%>
                                                                <%--<li><a href="UpdateAPOStatus.aspx">Update APO Status</a></li>--%>
                                                                <li>
                                                                    <cc:CustomLinkButton ID="lbObligation" runat="server" CommandName="Submit" CommandArgument='<%# Eval("APOID")%>'>Obligation Under Tri-MOU</cc:CustomLinkButton>
                                                                </li>
                                                                <li><a href="FDFeedbackMOU.aspx">Feedback of Compliance MOU</a></li>
                                                            </ul>
                                                        </span>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </cc:CustomGridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cgvAdditionalApo" EventName="PageIndexChanging" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <h3 class="panel image">
                            Approved APO <span id="ApprovedCountSpan" runat="server" class="">&nbsp;(2)</span></h3>
                        <div id="div1" class="collapse table-responsive">
                            <asp:UpdatePanel ID="uppnlApprovedAPO" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <cc:CustomGridView ID="gvApproved" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                        Width="100%" DataKeyNames="APOID" OnPageIndexChanging="gvApproved_PageIndexChanging"
                                        AllowPaging="true" PageSize="5" OnRowCommand="gvApproved_RowCommand" OnRowDataBound="gvApproved_RowDataBound">
                                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                            Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                        <EmptyDataTemplate>
                                            <div class="EmpltyGridView">
                                                &quot; Sorry! There is no data &quot;</div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <%-- <asp:BoundField HeaderText="Sr.No." DataField="SrNo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                <ItemTemplate>
                                                    <cc:CustomLabel ID="lblSrNo" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></cc:CustomLabel>
                                                    <asp:HiddenField ID="hdnId" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="APO Title">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAPoTitle" runat="server" CssClass="LinkColor" Text='<%#Eval("APOTitle")%>'
                                                        CommandArgument='<%#Eval("APOID") %>' CommandName="ApprovedApoView"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="APO File No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileNoumber" runat="server" Text='<%#Eval("APOFileNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Provisional UC">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="clbProvisionalUc" runat="server" CssClass="LinkColor" OnClientClick="RemoveBlockUi();" Text='<%#Eval("ProvisionalUcFileName")%>'
                                                        CommandArgument='<%#Eval("APOID") %>' CommandName="downloadProvisionalUc"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Final UC">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="clbFinalUc" runat="server" CssClass="LinkColor" Text='<%#Eval("FinnalUcFileName")%>'
                                                        CommandArgument='<%#Eval("APOID") %>' CommandName="downloadFinalUc"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="gvApproved" />
                                    <%-- <asp:AsyncPostBackTrigger ControlID="gvApproved" EventName="PageIndexChanging" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <h3 class="panel image">
                            Quarterly Report<span id="QuarterlyCountSpan" runat="server" class="NoColor">&nbsp;
                                (3)</span></h3>
                        <div class="collapse table-responsive">
                            <asp:UpdatePanel ID="uppnalQuarterlyReport" runat="server">
                                <ContentTemplate>
                                    <cc:CustomGridView ID="gvQuarterlyReport" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-responsive tablett" Width="100%" DataKeyNames="APOID"
                                        OnPageIndexChanging="gvQuarterlyReport_PageIndexChanging" OnRowCommand="gvQuarterlyReport_RowCommand"
                                        AllowPaging="true" PageSize="5">
                                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                            Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                        <EmptyDataTemplate>
                                            <div class="EmpltyGridView">
                                                &quot; Sorry! There is no data &quot;</div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <%--                                    <asp:BoundField HeaderText="Sr.No." DataField="SrNo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                <ItemTemplate>
                                                    <cc:CustomLabel ID="lblSrNo" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></cc:CustomLabel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="APO Title">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnAPoTitle" runat="server" CssClass="LinkColor" Text='<%#Eval("APOTitle")%>'
                                                        CommandArgument='<%#Eval("APOID") %>' CommandName="QuarterlyReportView"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="APO File No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileNoumber" runat="server" Text='<%#Eval("APOFileNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <span class="dropdown">
                                                        <button class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" data-hover="dropdown">
                                                            Action <span class="caret"></span>
                                                        </button>
                                                        <ul class="dropdown-menu hover">
                                                            <li><a href="SendAlert.aspx">Send Alert</a></li>
                                                            <%--<li><a href="SubmitAPO.aspx">Edit</a></li>--%>
                                                            <%--<li>
                                                        <cc:CustomLinkButton ID="lbEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("APOID")%>'>Edit</cc:CustomLinkButton>
                                                        </li>--%>
                                                        </ul>
                                                    </span>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc:CustomGridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="gvQuarterlyReport" />
                                    <%--<asp:AsyncPostBackTrigger ControlID="gvQuarterlyReport" EventName="PageIndexChanging" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="">
                    <div class="col-sm-4  TopMargin ">
                        <div class="panel">
                            <h3 class="leftRightPadding">
                                <b>ALERTS </b>
                            </h3>
                        </div>
                        <div id="DisplayAlertDiv" runat="server" class="alert alert-warning">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
