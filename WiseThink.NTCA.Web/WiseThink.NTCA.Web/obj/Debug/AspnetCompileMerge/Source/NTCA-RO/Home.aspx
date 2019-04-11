<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.Home" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/Remove.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(window).load(function () {
            GetSession_home();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div>
        <div class="row  minheight">
            <h4 id="NTCAHomeHeader" runat="server">
                <b>NTCA - Regional Officer Home</b></h4>
            <div class="">
                <div class="col-sm-8 NotificaitonBorder minheight Home_margin">
                
                    <div id="container">
                        <ul class="nav nav-tabs srchMargin">
                            <li class="active search liclass"><a href="#tabSearch" data-toggle="tab">Search</a></li>
                            <li class="liclass AdvanceSearch"><a href="#tabAdvanceSearch" data-toggle="tab">Advance Search</a></li>
                        </ul>
                        <div class="row SearchMargin">
                            <div>
                                <div id="tabContentMy" class="tab-content">
                                
                                    <div class="tab-pane active in" id="tabSearch">
                                        <cc:CustomTextBox ID="txtAPOTitle" runat="server" CssClass="form-control Search_Textbox TopMargin"
                                            placeHolder="APO Title"></cc:CustomTextBox>
                                        <cc:CustomButton ID="btnbasicSearch" runat="server" CssClass="btn btn-primary TopMargin"
                                            Text="Search" OnClick="btnbasicSearch_Click" OnClientClick="CreateSession_Search()" />
                                    </div>
                                    <div class="tab-pane fade" id="tabAdvanceSearch">
                                        <cc:CustomTextBox ID="txtAPOFIleNo" runat="server" CssClass="form-control Search_Textbox TopMargin"
                                            placeHolder="APO File No"></cc:CustomTextBox>
                                        <cc:CustomTextBox ID="txtApoTitles" runat="server" CssClass="form-control Search_Textbox TopMargin"
                                            placeHolder="APO Title"></cc:CustomTextBox>
                                        <cc:CustomTextBox ID="txtTigerReserve" runat="server" CssClass="form-control Search_Textbox TopMargin"
                                            placeHolder="Tiger Reserve"></cc:CustomTextBox>
                                        <cc:CustomTextBox ID="txtYear" runat="server" CssClass="form-control Search_Textbox TopMargin"
                                            placeHolder="Financial Year (E.g 2016-17)"></cc:CustomTextBox>
                                        <cc:CustomButton ID="btnAdvanceSearch" runat="server" CssClass="btn btn-primary TopMargin"
                                            Text="Search" OnClick="btnAdvanceSearch_Click" OnClientClick="CreateSession_AdvanceSearch()" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                                         
                    <div id="content" class="TopMargin">
                        <h3 class="panel image">
                            Status of APO <span id="APOCountSpan" runat="server" class="">&nbsp; (<asp:Label
                                ID="lblRow" Text="" runat="server"></asp:Label>)</span></h3>
                        <div id="divTable" class="collapse table-responsive">
                            <asp:UpdatePanel ID="uppnlStatus" runat="server">
                                <ContentTemplate>

                                
                            <cc:CustomGridView ID="gvAPoSubmitted" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="5" CssClass="table table-bordered tablett" Width="100%"
                                DataKeyNames="APOID" OnPageIndexChanging="gvAPoSubmitted_PageIndexChanging" OnRowCommand="gvAPoSubmitted_RowCommand"
                                OnRowDataBound="gvAPoSubmitted_RowDataBound">
                                <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                    Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                <EmptyDataTemplate>
                                    <div class="EmpltyGridView">
                                        &quot; Sorry! There is no data &quot;</div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <%--  <asp:BoundField HeaderText="Sr.No." DataField="SrNo">
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
                                                CommandArgument='<%#Eval("APOID") %>' CommandName="VIEWAPO"></asp:LinkButton>
                                        </ItemTemplate>
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
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <%-- <div id="viewbtnwithmenu" class="">                              
                                <cc:CustomImageButton ID="ibtnAction" runat="server" ImageUrl="../Images/viewall.png" />
                                <ul class="table-responsive"> 
                                    <li><a href="#">Approve</a></li>
                                    <li><a href="#">Reject</a></li>
                                       <li><a href="#">Send Notification</a></li>
                                </ul>
                            </div>--%>
                                            <span class="dropdown">
                                                <button class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" data-hover="dropdown">
                                                    Action <span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu ActionScroll">
                                                    <%--<li><a href="ApproveAPO.aspx">Approve/ Reject</a></li>--%>
                                                    <li>
                                                        <cc:CustomLinkButton ID="clbApprove" runat="server" CommandName="Approve" CommandArgument='<%# Eval("APOID")%>'>Approve/ Reject</cc:CustomLinkButton>
                                                    </li>
                                                    <%--<li><a href="#">Sanction APO Amount</a></li>--%>
                                                    <li>
                                                        <cc:CustomLinkButton ID="clbSanctioned" runat="server" CommandName="Sanctioned" CommandArgument='<%# Eval("APOID")%>'>Sanction APO Amount</cc:CustomLinkButton>
                                                    </li>
                                                    <%--<li><a href="ViewUtilizationCertificate.aspx">Utilization Certificate</a></li>--%>
                                                    <li>
                                                        <cc:CustomLinkButton ID="lbViewUc" runat="server" CommandName="GetUc" CommandArgument='<%# Eval("APOID")%>'>Utilization Certificate</cc:CustomLinkButton>
                                                    </li>
                                                    <li>
                                                        <cc:CustomLinkButton ID="clbProvisionalUc" runat="server" CommandArgument='<%#Eval("APOID") %>'
                                                            CommandName="downloadProvisionalUc">Download Provisional UC</cc:CustomLinkButton>
                                                    </li>
                                                    <li>
                                                        <cc:CustomLinkButton ID="clbFinalUc" runat="server" CommandArgument='<%#Eval("APOID") %>'
                                                            CommandName="downloadFinalUc">Download Final UC</cc:CustomLinkButton>
                                                    </li>
                                                    <li><a href="SendAlert.aspx">Send Alert</a></li>
                                                    <%--<li><a href="#">Edit/Modify</a></li>--%>
                                                    <li>
                                                        <cc:CustomLinkButton ID="clbEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("APOID")%>'>Edit / Modify</cc:CustomLinkButton>
                                                    </li>
                                                    <%--<li><a href="UpdateAPOStatus.aspx">Update APO Status</a></li>--%>
                                                    <li>
                                                        <cc:CustomLinkButton ID="clbUpdateStatus" runat="server" CommandName="UpdateStatus"
                                                            CommandArgument='<%# Eval("APOID")%>'>Update APO Status</cc:CustomLinkButton>
                                                    </li>
                                                    <li>
                                                        <cc:CustomLinkButton ID="CustomLinkButton1" runat="server" CommandName="ViewTRDetail"
                                                            CommandArgument='<%# Eval("APOID")%>'>View Tiger Reserve Details</cc:CustomLinkButton>
                                                    </li>
                                                    <%--<li><a href="ViewObligationFD.aspx">View Obligations-FD</a></li>--%>
                                                    <li>
                                                        <cc:CustomLinkButton ID="lbViewFDObligation" runat="server" CommandName="ViewFD"
                                                            CommandArgument='<%# Eval("APOID")%>'>View Obligations-FD</cc:CustomLinkButton>
                                                    </li>
                                                    <li>
                                                        <cc:CustomLinkButton ID="lbFeedbackFD" runat="server" CommandName="FeedBackFD" CommandArgument='<%# Eval("APOID")%>'>Feedback-FD</cc:CustomLinkButton>
                                                    </li>
                                                    <%--<li><a href="FDFeebBack.aspx">Feedback-FD</a></li>--%>
                                                    <%--<li><a href="ViewObligationCWW.aspx">View Obligations-CWW / SF</a></li>--%>
                                                    <li>
                                                        <cc:CustomLinkButton ID="lbViewCWWObligation" runat="server" CommandName="ViewCWW"
                                                            CommandArgument='<%# Eval("APOID")%>'>View Obligations-CWLW / SF</cc:CustomLinkButton>
                                                    </li>
                                                    <li>
                                                        <cc:CustomLinkButton ID="lbViewChecklist" runat="server" CommandName="ViewCheckList"
                                                            CommandArgument='<%# Eval("APOID")%>'>View Checklist</cc:CustomLinkButton>
                                                    </li>
                                                    <%--<li><a href="CWWFeedBack.aspx">Feedback of CWW / SF                                                       
                                                        </a></li>--%>
                                                    <li>
                                                        <cc:CustomLinkButton ID="lbFeedbackCWW" runat="server" CommandName="FeedBackCWW"
                                                            CommandArgument='<%# Eval("APOID")%>'>Feedback- CWLW / SF</cc:CustomLinkButton>
                                                    </li>
                                                </ul>
                                            </span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>
                                    </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="Additional" runat="server">
                            <h3 class="panel image">
                                Status of Additional APO <span id="AdditionalAPOCountSpan" runat="server" class="">&nbsp;(2)</span></h3>
                            <div id="div3" class="collapse table-responsive">
                                <asp:UpdatePanel ID="uppnlAdditionalAPO" runat="server">
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
                                        <%--  <asp:BoundField HeaderText="Sr.No." DataField="SrNo">
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
                                                    CommandArgument='<%#Eval("APOID") %>' CommandName="VIEWAPO"></asp:LinkButton>
                                            </ItemTemplate>
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
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <%-- <div id="viewbtnwithmenu" class="">                              
                                <cc:CustomImageButton ID="ibtnAction" runat="server" ImageUrl="../Images/viewall.png" />
                                <ul class="table-responsive"> 
                                    <li><a href="#">Approve</a></li>
                                    <li><a href="#">Reject</a></li>
                                       <li><a href="#">Send Notification</a></li>
                                </ul>
                            </div>--%>
                                                <span class="dropdown">
                                                    <button class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" data-hover="dropdown">
                                                        Action <span class="caret"></span>
                                                    </button>
                                                    <ul class="dropdown-menu ActionScroll">
                                                        <%--<li><a href="ApproveAPO.aspx">Approve/ Reject</a></li>--%>
                                                        <li>
                                                            <cc:CustomLinkButton ID="clbApprove" runat="server" CommandName="Approve" CommandArgument='<%# Eval("APOID")%>'>Approve/ Reject</cc:CustomLinkButton>
                                                        </li>
                                                        <%--<li><a href="#">Sanction APO Amount</a></li>--%>
                                                        <li>
                                                            <cc:CustomLinkButton ID="clbSanctioned" runat="server" CommandName="Sanctioned" CommandArgument='<%# Eval("APOID")%>'>Sanction APO Amount</cc:CustomLinkButton>
                                                        </li>
                                                        <%--<li><a href="ViewUtilizationCertificate.aspx">Utilization Certificate</a></li>--%>
                                                        <li>
                                                            <cc:CustomLinkButton ID="lbViewUc" runat="server" CommandName="GetUc" CommandArgument='<%# Eval("APOID")%>'>Utilization Certificate</cc:CustomLinkButton>
                                                        </li>
                                                        <li>
                                                        <cc:CustomLinkButton ID="clbProvisionalUc" runat="server" CommandArgument='<%#Eval("APOID") %>'
                                                            CommandName="downloadProvisionalUc">Download Provisional UC</cc:CustomLinkButton>
                                                    </li>
                                                    <li>
                                                        <cc:CustomLinkButton ID="clbFinalUc" runat="server" CommandArgument='<%#Eval("APOID") %>'
                                                            CommandName="downloadFinalUc">Download Final UC</cc:CustomLinkButton>
                                                    </li>
                                                        <li><a href="SendAlert.aspx">Send Alert</a></li>
                                                        <%--<li><a href="#">Edit/Modify</a></li>--%>
                                                        <li>
                                                            <cc:CustomLinkButton ID="clbEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("APOID")%>'>Edit / Modify</cc:CustomLinkButton>
                                                        </li>
                                                        <%--<li><a href="UpdateAPOStatus.aspx">Update APO Status</a></li>--%>
                                                        <li>
                                                            <cc:CustomLinkButton ID="clbUpdateStatus" runat="server" CommandName="UpdateStatus"
                                                                CommandArgument='<%# Eval("APOID")%>'>Update APO Status</cc:CustomLinkButton>
                                                        </li>
                                                        <li>
                                                            <cc:CustomLinkButton ID="CustomLinkButton1" runat="server" CommandName="ViewTRDetail"
                                                                CommandArgument='<%# Eval("APOID")%>'>View Tiger Reserve Details</cc:CustomLinkButton>
                                                        </li>
                                                        <%--<li><a href="ViewObligationFD.aspx">View Obligations-FD</a></li>--%>
                                                        <li>
                                                            <cc:CustomLinkButton ID="lbViewFDObligation" runat="server" CommandName="ViewFD"
                                                                CommandArgument='<%# Eval("APOID")%>'>View Obligations-FD</cc:CustomLinkButton>
                                                        </li>
                                                        <li>
                                                            <cc:CustomLinkButton ID="lbFeedbackFD" runat="server" CommandName="FeedBackFD" CommandArgument='<%# Eval("APOID")%>'>Feedback-FD</cc:CustomLinkButton>
                                                        </li>
                                                        <%--<li><a href="FDFeebBack.aspx">Feedback-FD</a></li>--%>
                                                        <%--<li><a href="ViewObligationCWW.aspx">View Obligations-CWW / SF</a></li>--%>
                                                        <li>
                                                            <cc:CustomLinkButton ID="lbViewCWWObligation" runat="server" CommandName="ViewCWW"
                                                                CommandArgument='<%# Eval("APOID")%>'>View Obligations-CWLW / SF</cc:CustomLinkButton>
                                                        </li>
                                                        <%--<li><a href="CWWFeedBack.aspx">Feedback of CWW / SF                                                       
                                                        </a></li>--%>
                                                        <li>
                                                            <cc:CustomLinkButton ID="lbFeedbackCWW" runat="server" CommandName="FeedBackCWW"
                                                                CommandArgument='<%# Eval("APOID")%>'>Feedback- CWLW / SF</cc:CustomLinkButton>
                                                        </li>
                                                    </ul>
                                                </span>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </cc:CustomGridView>
                                        </ContentTemplate>
                                </asp:UpdatePanel>  
                            </div>
                        </div>
                        <h3 class="panel image">
                            Approved APO <span id="ApprovedCountSpan" runat="server" class="NoColor">&nbsp; (3)</span></h3>
                        <div id="div2" class="collapse table-responsive">
                            <asp:UpdatePanel ID="uppnlApprovedAPO" runat="server">
                                <ContentTemplate>

                                
                            <cc:CustomGridView ID="gvApproved" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                Width="100%" DataKeyNames="APOID" OnPageIndexChanging="gvApproved_PageIndexChanging" OnRowDataBound="gvApproved_RowDataBound"
                                OnRowCommand="gvApproved_RowCommand" AllowPaging="true" PageSize="5">
                                <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                    Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                <EmptyDataTemplate>
                                    <div class="EmpltyGridView">
                                        &quot; Sorry! There is no data &quot;</div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        <ItemTemplate>
                                            <cc:CustomLabel ID="lblSrNo" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></cc:CustomLabel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="APO Title">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnAPoTitle" runat="server" CssClass="LinkColor" Text='<%#Eval("APOTitle")%>'
                                                CommandArgument='<%#Eval("APOID") %>' CommandName="VIEWAPO"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="APO File No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileNoumber" runat="server" Text='<%#Eval("APOFileNo")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Provisional UC">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="clbProvisionalUc" runat="server" CssClass="LinkColor" Text='<%#Eval("ProvisionalUcFileName")%>'
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
                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>

                                    </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <h3 class="panel image">
                            Quarterly Reports<span id="QuarterlyCountSpan" runat="server" class="NoColor">&nbsp;
                                (2/60)</span></h3>
                        <div id="div1" class="collapse table-responsive">
                            <asp:UpdatePanel ID="uppnlQuarterlyReport" runat="server">
                                <ContentTemplate>
                            <cc:CustomGridView ID="gvAPoReportSubmitted" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="5" CssClass="table table-bordered tablett" Width="100%"
                                DataKeyNames="APOID" OnPageIndexChanging="gvAPoReportSubmitted_PageIndexChanging">
                                <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                    Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                <EmptyDataTemplate>
                                    <div class="EmpltyGridView text-center">
                                        &quot; Sorry! There is no data &quot;</div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <%--  <asp:BoundField HeaderText="Sr.No." DataField="SrNo">
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
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <span class="dropdown">
                                                <button class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" data-hover="dropdown">
                                                    Action <span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu">
                                                    <li><a href="SendAlert.aspx">Send Alert</a></li>
                                                    <li><a href="#">Assign File No.</a></li>
                                                </ul>
                                            </span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>
                                      </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
               
                           
                             </div>
                <div class="">
                    <div class="col-sm-4  TopMargin">
                        <div class="panel">
                            <h3 class="leftRightPadding">
                                <b>ALERTS</b></h3>
                        </div>
                        <div id="DisplayAlertDiv" runat="server" class="alert alert-warning">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
