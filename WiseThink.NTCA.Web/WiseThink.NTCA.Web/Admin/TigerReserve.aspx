<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeBehind="TigerReserve.aspx.cs" Inherits="WiseThink.NTCA.Web.Admin.WebForm9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SuperAdminPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>Tiger Reserve List</b></h4>
        <div class="TopMargin col-sm-offset-2 table-responsive">
            <asp:UpdatePanel ID="uppanel" runat="server">
                <ContentTemplate>
                    <cc:CustomGridView ID="gvTigerReserve" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-bordered col-sm-10 col-xs-1" DataKeyNames="TigerReserveId"
                        AllowPaging="True" PageSize="10" OnPageIndexChanging="gvTigerReserve_PageIndexChanging">
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                            Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                        <EmptyDataTemplate>
                            <div class="EmpltyGridView">
                                &quot; Sorry! There is no data &quot;</div>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSrNo" Text='<%#Container.DataItemIndex+1 %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <%--<asp:BoundField HeaderText="Tiger Reserve Code" DataField="TigerReserveId">
                <ItemStyle Width="140px"  />
                 </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Tiger Reserve Name">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnTigerReservename" CssClass="LinkColor" runat="server" Text='<%#Eval("TigerReserveName") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="State" DataField="StateName">
                                <ItemStyle Width="140px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Area of the core / critical tiger habitat (in Sq. Kms)"
                                DataField="CoreArea">
                                <ItemStyle Width="140px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Area of the buffer / Peripheral (in Sq. Kms)" DataField="BufferArea">
                                <ItemStyle Width="140px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Total Area (in Sq. Kms)" DataField="TotalArea">
                                <ItemStyle Width="140px" />
                            </asp:BoundField>
                            <%--<asp:BoundField HeaderText="Location" DataField="Address">
                        <ItemStyle />
                    </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <cc:CustomImageButton ID="imgbtnAction" runat="server" OnClientClick="return confirm('Are you sure? you want to edit this tiger reserve!');"
                                        ImageUrl="../Images/edit.png" RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnAction_click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </cc:CustomGridView>
                    <div class="col-sm-offset-0 col-sm-10 text-center bottomMargin">
                        <cc:CustomButton ID="btnAddNew" CssClass=" TopMargin btn-primary btn" runat="server"
                            Text="Add New" onclick="btnAddNew_Click" />
                        <cc:CustomButton ID="btnCancel" CssClass="btn btn-primary TopMargin col-sm-offset-1"
                            runat="server" Text="Back" PostBackUrl="~/Admin/Home.aspx" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
