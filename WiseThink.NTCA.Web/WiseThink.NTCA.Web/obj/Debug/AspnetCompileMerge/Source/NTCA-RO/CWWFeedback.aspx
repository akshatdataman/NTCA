<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="CWWFeedback.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.CWWFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        $('#<%=gvFeebbackCWW.ClientID %>').Scrollable({
            ScrollHeight: 500
        });
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#<%=gvFeebbackCWW.ClientID %>').Scrollable({
                        ScrollHeight: 500
                    });
                }

            });
        }
        else {
            $('#<%=gvFeebbackCWW.ClientID %>').Scrollable({
                ScrollHeight: 500
            });
        }
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <h4>
                <b>Feedback of CWW / SF</b></h4>
            <%--<div class="col-sm-3">
                <div id="leftPartMy1" class="">
                    <div id="leftNav">
                        <div class="panel-group" id="accordion">
                            <div class=" panel-default ">
                                <div id="panel1" class="panel-collapse collapse in menuleft">
                                    <div class="table-responsive">
                                        <ul>
                                            <li class="FirstSubMenu1  BackgroundColor"><a class="submenu" href="ViewAPO.aspx">Non-Recurring</a></li>
                                            <li class="BackgroundColor"><a href="ViewAPO.aspx">Recurring</a></li>
                                            <li class="BackgroundColor "><a href="#">Utilization Certificate</a></li>
                                            <li class="BackgroundColor "><a href="#">Spill Over</a></li>
                                            <li class="BackgroundColor "><a href="#">Revalidation</a></li>
                                            <li class="BackgroundColor "><a href="#">Adjustment</a></li>
                                            <li class="BackgroundColor "><a href="ViewFieldDirectorObligations.aspx">View Obligations
                                                - FD</a></li>
                                            <li class="BackgroundColor "><a href="ViewCWWObligations.aspx">View Obligations - CWW
                                                / SF</a></li>
                                            <li class="BackgroundColor "><a href="FDFeedback.aspx">Feedback - FD </a></li>
                                            <li class="BackgroundColor Current"><a href="FDFeedback.aspx">Feedback - CWW / SF </a>
                                            </li>
                                            <li class="BackgroundColor "><a href="#">Checklist</a></li>
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
                            <cc:CustomGridView ID="gvFeebbackCWW" runat="server" CssClass="table  col-sm-12"
                                AutoGenerateColumns="False" DataKeyNames="ObligationId" AllowPaging="False" PageSize="5">
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
                                            <asp:Label ID="lblSanctionAmount" runat="server" Text='<%#Eval("Descriptions") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="250px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Compiled / Not Compiled">
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
                                            <cc:CustomTextBox ID="txtScore" runat="server" CssClass="less_size" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Compliance process Underway">
                                        <ItemTemplate>
                                            <cc:CustomTextBox ID="txtCompliance" runat="server" CssClass="less_size" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <cc:CustomTextBox ID="txtRemarks" runat="server" CssClass="less_size" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-offset-4 col-sm-12 marginButton margin_bottom">
                    <cc:CustomButton ID="btnSave" runat="server" Text="Save" CssClass="btn-primary btn"
                        OnClick="btnSave_Click" />
                    <cc:CustomButton ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary col-sm-offset-1"
                        OnClick="btnSubmit_Click" />
                    <cc:CustomButton ID="btnBack" CssClass="btn btn-primary col-sm-offset-1" runat="server" PostBackUrl="Home.aspx"
                        Text="Back" />
                </div>
                <div class="TopMargin">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
