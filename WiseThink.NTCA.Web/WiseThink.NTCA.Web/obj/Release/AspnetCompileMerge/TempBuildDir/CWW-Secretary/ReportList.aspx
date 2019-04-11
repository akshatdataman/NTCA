<%@ Page Title="" Language="C#" MasterPageFile="~/CWW-Secretary/CWW-Secretary.master"
    AutoEventWireup="true" CodeBehind="ReportList.aspx.cs" Inherits="WiseThink.NTCA.Web.CWW_Secretary.ReportList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CWWPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <h4>
                <b>List of Reports</b></h4>
            <div class="col-sm-offset-2 table-responsive TopMargin ">
                <cc:CustomGridView ID="cgvAlerts" runat="server" CssClass="table  col-sm-9" AutoGenerateColumns="False"
                    DataKeyNames="ID" AllowPaging="True" PageSize="5">
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
                        <asp:BoundField HeaderText="Sent By" DataField="LoggedInUser" />
                        <asp:BoundField HeaderText="Date & Time" DataField="DateOfSubmission" />
                        <asp:BoundField HeaderText="Subject" DataField="Subject" />
                        <asp:BoundField HeaderText="APO File No." DataField="APOTitle" />
                        <asp:BoundField HeaderText="Alert" DataField="Body" />
                    </Columns>
                </cc:CustomGridView>
            </div>
        </div>
    </div>
</asp:Content>
