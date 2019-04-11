<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeBehind="WebForm12.aspx.cs" Inherits="WiseThink.NTCA.Web.Admin.WebForm12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SuperAdminPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <h4>
                <b>TIGER RANGE STATES</b></h4>
            <div class="col-sm-offset-2 table-responsive TopMargin ">
                <cc:CustomGridView ID="cgvStateMaster" runat="server" CssClass="table  col-sm-9"
                    AutoGenerateColumns="False" DataKeyNames="StateId" AllowPaging="True" PageSize="10"
                    OnPageIndexChanging="cgvStateMaster_PageIndexChanging">
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
                        <%-- <asp:BoundField HeaderText="State Code" DataField="StateCode">
                            <ItemStyle Width="130px" />
                            
                            </asp:BoundField> --%>
                        <asp:BoundField HeaderText="State Name" DataField="StateName" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <cc:CustomImageButton ID="imgbtnAction" runat="server" OnClientClick="return confirm('Are you sure? you want to edit this state!');" ImageUrl="../Images/edit.png"
                                    RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnAction_click" />
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </cc:CustomGridView>
                <div class="col-sm-9 text-center">
                    <cc:CustomButton ID="btnAddNew" CssClass="btn-primary btn TopMargin" runat="server"
                        PostBackUrl="EditStateMaster.aspx" Text="Add New" />
                    <cc:CustomButton ID="btnCancel" CssClass="btn btn-primary TopMargin " runat="server"
                        Text="Back" PostBackUrl="~/Admin/Home.aspx" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
