<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeBehind="ManageDesignation.aspx.cs" Inherits="WiseThink.NTCA.Web.Admin.ManageDesignation" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SuperAdminPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>Manage Designation</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-11 ">
            <div id="DisplayErrorMessage" class="btn-danger " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="col-sm-10 col-sm-offset-2 MarginTop">
            <div class="col-sm-2">
                <cc:CustomLabel ID="lblDesignation" runat="server" Text="Designation :"></cc:CustomLabel><b class="REerror"> *</b>
            </div>
            <div class="col-sm-4">
                <cc:CustomTextBox ID="txtDesignation" req="1" CssClass="form-control" runat="server"></cc:CustomTextBox>
            </div>
            <div class="col-sm-4">
                <cc:CustomButton ID="btnAdd" runat="server" Text="Add Designation" CssClass="btn-primary btn"
                    OnClick="btnAdd_Click" />
            </div>
        </div>
        <div class="col-sm-offset-2 table-responsive MarginTop col-sm-10 ">
            <cc:CustomGridView ID="cgvDesignation" runat="server" CssClass="table  col-sm-9"
                AutoGenerateColumns="False" DataKeyNames="DesignationId" AllowPaging="True" PageSize="5"
                OnPageIndexChanging="cgvDesignation_PageIndexChanging" OnPageIndexChanged="cgvDesignation_PageIndexChanged" OnRowCancelingEdit="cgvDesignation_RowCancelingEdit"
                OnRowEditing="cgvDesignation_RowEditing" OnRowUpdating="cgvDesignation_RowUpdating"
                OnRowDeleting="cgvDesignation_RowDeleting">
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
                        <ItemStyle HorizontalAlign="Center" CssClass="btnmobile" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DesignationId" Visible="false">
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblDesignationId" runat="server" Text='<%#Eval("DesignationId")%>'></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Designations">
                        <ItemTemplate>
                            <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <cc:CustomTextBox ID="txtDesignation" runat="server" CssClass="form-control" Text='<%#Eval("Designation") %>'></cc:CustomTextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="324px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <cc:CustomButton ID="btnEditDesignation" runat="server" OnClientClick="return confirm('Are you sure? you want to edit this designation!');" CssClass="btn btn-primary ButtonMargin"
                                Text="Edit" CommandName="Edit" />
                            <cc:CustomButton ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure? you want to delete this designation!');" CssClass="btn btn-primary ButtonMargin"
                                Text="Delete" CommandName="Delete" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <cc:CustomButton ID="btnUpdate" runat="server" OnClientClick="return confirm('Are you sure? you want to update this designation!');" CssClass="btn btn-primary ButtonMargin"
                                Text="Update" CommandName="Update" />
                            <cc:CustomButton ID="btnCancel" runat="server" CssClass="btn btn-primary ButtonMargin"
                                Text="Cancel" CommandName="Cancel" />
                        </EditItemTemplate>
                        <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </cc:CustomGridView>
            <div class="TopMargin">
            </div>
        </div>
        <div class="row text-center TopMargin">
            <button id="btnBack" type="button" onclick="window.location.href = 'Home.aspx'" class="btn btn-primary col-sm-offset-1">
                Back</button>
        </div>
    </div>
</asp:Content>
