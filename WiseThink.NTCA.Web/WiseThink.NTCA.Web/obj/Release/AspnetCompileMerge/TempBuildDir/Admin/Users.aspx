<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeBehind="Users.aspx.cs" Inherits="WiseThink.NTCA.Admin.Users" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SuperAdminPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>User List</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10">
            <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="rowHeight">
            <div class="col-sm-12 col-sm-offset-1 TopMargin table-responsive">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cc:CustomGridView ID="cgvUsers" class="table" runat="server" AutoGenerateColumns="False"
                            CssClass="table-bordered table col-sm-10" OnDataBinding="cgvUsers_DataBinding"
                            DataKeyNames="Sr.No." AllowPaging="True" PageSize="10" OnPageIndexChanging="cgvUsers_PageIndexChanging">
                            <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                            <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                            <EmptyDataTemplate>
                                <div class="EmpltyGridView">
                                    &quot; Sorry! There is no data &quot;</div>
                            </EmptyDataTemplate>
                            <Columns>
                                <%-- <asp:BoundField DataField="Sr.No." HeaderText="Sr. No.">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Sr.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                <asp:BoundField DataField="Role" HeaderText="Role" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <cc:CustomImageButton ID="imgbtnAction" runat="server" OnClientClick="return confirm('Are you sure? you want to edit this user!');" 
                                        ImageUrl="../Images/edit.png" RowIndex='<%# Container.DisplayIndex %>' OnClick="imgbtnAction_click" />
                                        <%--<cc:CustomLinkButton ID="clbtnEdit" runat="server" ImageUrl="../Images/edit.png"
                                     PostBackUrl="~/Admin/add-User.aspx" />--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </cc:CustomGridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-sm-10 text-center">
                    <cc:CustomButton ID="btnAddUser" CssClass="btn btn-primary TopMargin" runat="server"
                        Text="Add User" OnClick="btnAddUser_Click" />
                    <cc:CustomButton ID="btnCancel" CssClass="btn btn-primary TopMargin " runat="server"
                        Text="Back" PostBackUrl="~/Admin/Home.aspx" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
