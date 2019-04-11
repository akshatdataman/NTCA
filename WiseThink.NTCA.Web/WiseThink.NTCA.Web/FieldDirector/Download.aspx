<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="Download.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.WebForm1" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>Downloads</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
            <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="TopMargin col-sm-10 col-sm-offset-2 table-responsive">
            <cc:CustomGridView ID="cgvDocuments" runat="server" CssClass="table table-bordered col-sm-10 "
                DataKeyNames="DocumentName" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                OnPageIndexChanging="cgvDocuments_PageIndexChanging" OnRowCommand="cgvDocuments_RowCommand">
                <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                    Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                <EmptyDataTemplate>
                    <div class="EmpltyGridView">
                        &quot; Sorry! There is no data &quot;</div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="DocumentId" HeaderText="Sr. No.">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Document">
                        <ItemTemplate>
                            <cc:CustomLinkButton ID="lblDocumentFile" runat="server" Text='<%#Eval("DocumentName")%>'
                                CommandArgument='<%#Eval("DocumentId") %>' CommandName="download">></cc:CustomLinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <cc:CustomImageButton ID="imgbtnPdf" runat="server" ImageUrl="~/Images/Downloads.png"
                                RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnPdf_click" />
                            <cc:CustomLinkButton ID="lnkPrint" CommandName="print" runat="server" OnClientClick="return confirm('Are you sure?')"><img src="../Images/btn-print.png" alt="print group" /></cc:CustomLinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    </asp:TemplateField>
                </Columns>
            </cc:CustomGridView>
        </div>
        <div class=" col-sm-12 TopMargin text-center">
            <cc:CustomButton ID="btnBack" CssClass="btn-primary btn col-sm-offset-0" runat="server"
                PostBackUrl="FieldDirectorHome.aspx" Text="Back" />
        </div>
    </div>
</asp:Content>
