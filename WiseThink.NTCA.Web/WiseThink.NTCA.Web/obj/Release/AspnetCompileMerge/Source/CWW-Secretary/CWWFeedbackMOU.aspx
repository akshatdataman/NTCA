<%@ Page Title="" Language="C#" MasterPageFile="~/CWW-Secretary/CWW-Secretary.master"
    AutoEventWireup="true" CodeBehind="CWWFeedbackMOU.aspx.cs" Inherits="WiseThink.NTCA.Web.CWW_Secretary.CWWFeedbackMOU" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CWWPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <h4>
                <b>Feedback of Compliance MOU</b></h4>
            <%--<div class="col-sm-3">
                <div id="leftPartMy1" class="">
                    <div id="leftNav">
                        <div class="panel-group" id="accordion">
                            <div class=" panel-default ">
                                <div id="panel1" class="panel-collapse collapse in menuleft">
                                    <div class="table-responsive">
                                        <ul>
                                        <li class="FirstSubMenu1 BackgroundColor"><a class="submenu" href="EditApo.aspx">
                                            Non-Recurring</a></li>
                                        <li class="BackgroundColor"><a href="#RecuringDiv">Recurring</a></li>
                                        <li class="BackgroundColor"><a href="ViewUtilizationCertificate.aspx">Utilization Certificate</a></li>
                                        <li class="BackgroundColor "><a href="#">Spill Over</a></li>
                                        <li class="BackgroundColor "><a href="#">Revalidation</a></li>
                                        <li class="BackgroundColor "><a href="#">Adjustment</a></li>
                                        <li class="BackgroundColor "><a href="ViewFieldDirectorObligations.aspx">View Field Director's Obligations</a></li>
                                        <li class="BackgroundColor "><a href="ObligationCWW.aspx">Obligations CWW / SF</a></li>
                                        <li class="BackgroundColor Current"><a href="CWWFeedbackMOU.aspx">Feedback of Compliance MOU</a></li>
                                        <li class="BackgroundColor"><a href="ViewCheckList.aspx">Check and Submit</a></li>
                                        <li class="BackgroundColor"><a href="#">Flexi Amount</a></li>
                                    </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="col-sm-12 minHeight Home_margin" id="MainContent">
                <b>Score Categories:</b> <span class="text-danger">POOR: 0-20</span> <span class="text-success col-sm-offset-1">
                    FAIR: 20-40</span> <span class="col-sm-offset-1" style="color: Teal">GOOD: 40-60</span>
                <span class="text-warning col-sm-offset-1">VERY GOOD: >60-100</span>
                <br />
                <div class=" table-responsive TopMargin ">
                    <asp:UpdatePanel ID="upnlObligation" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                            </div>
                            <cc:CustomGridView ID="gvFeebbackMOU" runat="server" CssClass="table  col-sm-12"
                                AutoGenerateColumns="False" DataKeyNames="ObligationId" AllowPaging="false" PageSize="5">
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
                                            <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Obligations of the Field Director">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObligationId" runat="server" Visible="false" Text='<%#Eval("ObligationId") %>'></asp:Label>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Descriptions") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="250px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Complied / Not Complied">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompileOrNot" runat="server" Text='<%#Eval("CompledOrNotOrNotApplicable") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="If not, reason there for">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReason" runat="server" Text='<%#Eval("ReasonIfNotCompiled") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Score">
                                        <ItemTemplate>
                                            <asp:Label ID="lblScore" runat="server" Text='<%#Eval("Score") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Compliance process Underway">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComplianceprocessUnderway" runat="server" Text='<%#Eval("ComplianceprocessUnderway") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>
                            <div class="col-sm-10 text-center TopMargin">
                                <cc:CustomButton ID="btnBack" CssClass="btn-primary btn " runat="server" PostBackUrl="Home.aspx"
                                    Text="Back" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
