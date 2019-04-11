<%@ Page Title="" Language="C#" MasterPageFile="~/CWW-Secretary/CWW-Secretary.master"
    AutoEventWireup="true" CodeBehind="ViewCheckList.aspx.cs" Inherits="WiseThink.NTCA.Web.CWW_Secretary.ViewCheckList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CWWPlaceHolder" runat="server">
    <%--<div>
        <div class="row minheight">
            <h4>
                <b>FIELD DIRECTOR CHECKLIST</b></h4>
            <%--<div class="col-sm-3">
                <div id="leftPartMy1" class="">
                    <div id="leftNav">
                        <div class="panel-group" id="accordion">
                            <div class=" panel-default ">
                                <div id="panel1" class="panel-collapse collapse in menuleft">
                                    <div class="table-responsive">
                                        <ul>
                                        <li class="FirstSubMenu1 Current BackgroundColor"><a class="submenu" href="#Non-RecurringDiv">
                                            Non-Recurring</a></li>
                                        <li class="BackgroundColor"><a href="#RecuringDiv">Recurring</a></li>
                                        <li class="BackgroundColor"><a href="#">Utilization Certificate</a></li>
                                        <li class="BackgroundColor "><a href="#">Spill Over</a></li>
                                        <li class="BackgroundColor "><a href="#">Revalidation</a></li>
                                        <li class="BackgroundColor "><a href="#">Adjustment</a></li>
                                        <li class="BackgroundColor "><a href="ViewFieldDirectorObligations.aspx">View Field Director's Obligations</a></li>
                                        <li class="BackgroundColor "><a href="ObligationCWW.aspx">Obligations CWW / SF</a></li>
                                        <li class="BackgroundColor "><a href="#">Feedback of Compliance MOU</a></li>
                                        <li class="BackgroundColor"><a href="ViewCheckList.aspx">Checklist</a></li>
                                        <li class="BackgroundColor"><a href="#">Flexi Amount</a></li>
                                    </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 minHeight Home_margin" id="MainContent">
                <div class=" table-responsive TopMargin ">
                    <asp:UpdatePanel ID="upnlObligation" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cc:CustomGridView ID="cgvFDCheckList" runat="server" CssClass="table  col-sm-12"
                                AutoGenerateColumns="False" DataKeyNames="ActivityId" AllowPaging="False" PageSize="5" OnRowCreated="cgvFDCheckList_RowCreated">
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
                                    <asp:TemplateField HeaderText="Check list of the Field Director">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                            <asp:Label ID="lblActivityName" runat="server" Text='<%#Eval("ActivityName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Checked /</br>Not Applicable">
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rblCheckedOrNot" AutoPostBack="true" EnableViewState="true"
                                                runat="server">
                                                <asp:ListItem Text="Checked" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Not Applicable" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="col-sm-10 text-center TopMargin margin_bottom">
                        <cc:CustomButton ID="btnSave" runat="server" Text="Save" CssClass="btn-primary btn " />
                        <cc:CustomButton ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-primary btn col-sm-offset-1" />
                        <cc:CustomButton ID="btnBack" CssClass="btn-primary btn col-sm-offset-1 " runat="server"
                            PostBackUrl="Home.aspx" Text="Back" />
                    </div>
                    <div class="TopMargin">
                    </div>
                </div>
            </div>
        </div>
    </div>--%>


    <div class="row minheight">
        <div class="row">
            <h4 id="SubmitApoHeader" runat="server">
                <b>View Field Director's Checklist</b>
            </h4>
        </div>
        <div style="overflow: auto">
            <cc:CustomGridView ID="cgvFDCheckList" runat="server" CssClass="table  col-sm-12"
                                AutoGenerateColumns="False" DataKeyNames="ActivityId" AllowPaging="False" OnRowCreated="cgvFDCheckList_RowCreated" >
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
                                    <asp:TemplateField HeaderText="Check list of the Field Director">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                            <asp:Label ID="lblActivityName" runat="server" Text='<%#Eval("ActivityName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Checked /</br>Not Applicable">
                                        <ItemTemplate>
                                            <asp:Label ID="lblViewCheck" runat="server" Text="Checked"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>
        </div>
    </div>
</asp:Content>
