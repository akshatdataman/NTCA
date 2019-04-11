<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="FieldDirectorFeedback.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.FieldDirectorFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <h4>
                <b>FIELD DIRECTOR OBLIGATIONS</b></h4>
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
                                            <li class="BackgroundColor"><a href="FDUtilizationCertificate.aspx">Utilization Certificate</a></li>
                                            <li class="BackgroundColor "><a href="#">Spill Over</a></li>
                                            <li class="BackgroundColor "><a href="#">Revalidation</a></li>
                                            <li class="BackgroundColor "><a href="#">Adjustment</a></li>
                                            <li class="BackgroundColor Current"><a href="ViewFieldDirectorObligations.aspx">View
                                                FD Obligations</a></li>
                                            <li class="BackgroundColor "><a href="FDFeedbackMOU.aspx">Feedback of Compliance MOU</a></li>
                                            <li class="BackgroundColor"><a href="CheckList.aspx?callfrom=ViewAPO">Checklist</a></li>
                                            <%--<li class="BackgroundColor"><a href="FDFlexiAmont.aspx">Flexi Amount</a></li>--%>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-9  leftBorder minHeight Home_margin" id="MainContent">
                <div class="row">
                    <div>
                        <b><span style="padding: 5px;">Score Categories:</span></b> <span style="padding: 3px;
                            color: Red;">POOR: 0-20</span>&nbsp;&nbsp;&nbsp;&nbsp; <span style="padding: 3px;
                                color: Green;">FAIR: 20-40</span>&nbsp;&nbsp;&nbsp;&nbsp; <span style="padding: 3px;
                                    color: Teal;">GOOD :40-60</span>&nbsp;&nbsp;&nbsp;&nbsp; <span style="padding: 3px;
                                        color: Olive;">VERY GOOD: &gt;60-100</span>
                    </div>
                </div>
                <div class=" table-responsive TopMargin ">
                    <asp:UpdatePanel ID="upnlObligation" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cc:CustomGridView ID="cgvFeedBackFD" runat="server" CssClass="table  col-sm-12"
                                AutoGenerateColumns="False" DataKeyNames="ObligationId" AllowPaging="True" PageSize="5"
                                OnPageIndexChanging="cgvFeedBackFD_PageIndexChanging">
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
                                            <asp:Label ID="lblObligation" runat="server" Text='<%#Eval("Descriptions") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Complied /</br>Not Complied /</br>Not Applicable">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompiledOrNot" runat="server" Text='<%#Eval("CompledOrNotOrNotApplicable") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="If not, reason there for">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReason" runat="server" Text='<%#Eval("ReasonIfNotCompiled") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Score">
                                        <ItemTemplate>
                                            <cc:CustomTextBox ID="txtScore" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Compliance process Underway">
                                        <ItemTemplate>
                                            <cc:CustomTextBox ID="txtCompliance" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <cc:CustomTextBox ID="txtRemarks" runat="server" CssClass="less_size" Text=""></cc:CustomTextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="col-sm-10 text-center TopMargin">
                        <%-- <cc:CustomButton ID="btnSave" runat="server" Text="Save" CssClass="btn-primary btn"
                        OnClick="btnSave_Click" />--%>
                        <cc:CustomButton ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-primary btn"
                            OnClick="btnSubmit_Click" />
                        <cc:CustomButton ID="btnBack" CssClass="btn-primary btn " runat="server" PostBackUrl="Home.aspx"
                            Text="Back" />
                    </div>
                    <div class="TopMargin">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
