<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="LoginHistory.aspx.cs" Inherits="WiseThink.NTCA.Web.Admin.LoginHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SuperAdminPlaceHolder" runat="server">
<div class="row minheight">
        <h4>
            <b>Login History</b></h4>
        <div class="row text-center MarginTop">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="col-sm-12 table-responsive MarginTop">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <cc:CustomGridView ID="gvLoginHistory" runat="server" CssClass="table  col-sm-12"
                        AllowPaging="true" PageSize="20" AutoGenerateColumns="False" OnPageIndexChanging="gvLoginHistory_PageIndexChanging">
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                            Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                        <EmptyDataTemplate>
                            <div class="EmpltyGridView">
                                &quot; Sorry! There is no data &quot;</div>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="AuditTrailId" HeaderText="Sr. No." />                            
                            <asp:BoundField DataField="LoginDateTime" HeaderText="Login Date and Time" DataFormatString="{0:dd MMMM, yyyy hh:mm tt }" />
                            <asp:BoundField DataField="UserName" HeaderText="UserName" />
                            <asp:BoundField DataField="IPAddress" HeaderText="IP Address" />                              
                        </Columns>
                    </cc:CustomGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="MarginTop">
            <button id="btnBack" type="button" onclick="window.location.href = 'Home.aspx'" class="btn btn-primary col-sm-offset-5">
                Back</button>
        </div>
    </div>
</asp:Content>
