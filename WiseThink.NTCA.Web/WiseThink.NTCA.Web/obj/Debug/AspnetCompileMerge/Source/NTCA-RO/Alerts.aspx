<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="Alerts.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.Alerts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <h4>
                <b>Alert List</b></h4>
            <div class="col-sm-offset-2 table-responsive TopMargin ">
                <cc:CustomGridView ID="cgvAlerts" runat="server" CssClass="table  col-sm-9" AutoGenerateColumns="False"
                    DataKeyNames="ID" AllowPaging="True" PageSize="10" OnPageIndexChanging="cgvAlerts_PageIndexChanging">
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
                        <%-- <asp:boundfield headertext="APO File No." datafield="APOTitle" />--%>
                        <asp:BoundField HeaderText="Alert" DataField="Body" />
                    </Columns>
                </cc:CustomGridView>
                <div class="col-sm-10 text-center TopMargin">
                    <cc:CustomButton ID="btnSendAlert" runat="server" Text="Send Alert" CssClass="btn-primary btn"
                        OnClick="btnSendAlert_Click" />
                    <cc:CustomButton ID="btnBack" CssClass="btn-primary btn " runat="server" PostBackUrl="Home.aspx"
                        Text="Back" OnClick="btnBack_Click" />
                </div>
                <div class="TopMargin">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
