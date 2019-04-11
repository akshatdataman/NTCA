<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true" CodeBehind="ApoDueForSubmission.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.ApoDueForSubmission" %>
<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
<div class="row minheight">
        <h4>
            <b>Due for Submission</b></h4>
         <div class="text-center col-sm-12 ">
            <div id="DisplayErrorMessage" class="btn-danger " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        
        <div id="DueApoSubmissionDiv" runat="server" class="col-sm-12 table-responsive MarginTop">
        <h3><b>APO Due for Submission</b></h3>
            <cc:CustomGridView ID="cgvDueApo" runat="server" CssClass="table  col-sm-12 MarginTop"
                AllowPaging="true" PageSize="10" AutoGenerateColumns="False" DataKeyNames="APOID"
                OnPageIndexChanging="cgvDueApo_PageIndexChanging" OnRowCommand="cgvDueApo_RowCommand" OnPageIndexChanged="cgvDueApo_PageIndexChanged">
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
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                        <asp:Label ID="lblIsFDObligation" runat="server" Visible="false" Text='<%#Eval("IsFDObligationSubmitted")%>'></asp:Label>
                                        <asp:Label ID="lblUnspentAmountSettled" runat="server" Visible="false" Text='<%#Eval("IsUnspentAmountSettled")%>'></asp:Label>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <span class="dropdown">
                                                <button class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" data-hover="dropdown">
                                                    Action <span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu ActionScroll">
                                                    
                                                    <li><a href="SendAlert.aspx">Send Alert</a></li>
                                                    <li>
                                                        <cc:CustomLinkButton ID="clbEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("APOID")%>'>Edit / Modify</cc:CustomLinkButton>
                                                    </li>
                                                    <li>
                                                        <cc:CustomLinkButton ID="clbUpdateStatus" runat="server" CommandName="UpdateStatus"
                                                            CommandArgument='<%# Eval("APOID")%>'>Allow Submission</cc:CustomLinkButton>
                                                    </li>
                                                </ul>
                                            </span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
            </cc:CustomGridView>
        </div>
        <div id="DueAdditionalApoDiv" runat="server" class="col-sm-12 table-responsive MarginTop">
        <h3><b>Additional APO Due for Submission</b></h3>
            <cc:CustomGridView ID="cgvDueAdditionalApo" runat="server" CssClass="table  col-sm-12"
                AllowPaging="true" PageSize="5" AutoGenerateColumns="False" DataKeyNames="APOID"
                OnPageIndexChanging="cgvDueAdditionalApo_PageIndexChanging" OnRowCommand="cgvDueAdditionalApo_RowCommand" OnPageIndexChanged="cgvDueAdditionalApo_PageIndexChanged">
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
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                         <asp:Label ID="lblIsFDObligation" runat="server" Visible="false" Text='<%#Eval("IsFDObligationSubmitted")%>'></asp:Label>
                                        <asp:Label ID="lblUnspentAmountSettled" runat="server" Visible="false" Text='<%#Eval("IsUnspentAmountSettled")%>'></asp:Label>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <span class="dropdown">
                                                <button class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" data-hover="dropdown">
                                                    Action <span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu ActionScroll">
                                                    
                                                    <li><a href="SendAlert.aspx">Send Alert</a></li>
                                                    <li>
                                                        <cc:CustomLinkButton ID="clbEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("APOID")%>'>Edit / Modify</cc:CustomLinkButton>
                                                    </li>
                                                    <li>
                                                        <cc:CustomLinkButton ID="clbUpdateStatus" runat="server" CommandName="UpdateStatus"
                                                            CommandArgument='<%# Eval("APOID")%>'>Allow Submission</cc:CustomLinkButton>
                                                    </li>
                                                </ul>
                                            </span>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
            </cc:CustomGridView>
        </div>

        <div>
        <button id="btnBack" type="button" onclick="window.location.href = 'Home.aspx'" class="btn btn-primary col-sm-offset-5">
                            Back</button>
        </div>
    </div>
</asp:Content>
