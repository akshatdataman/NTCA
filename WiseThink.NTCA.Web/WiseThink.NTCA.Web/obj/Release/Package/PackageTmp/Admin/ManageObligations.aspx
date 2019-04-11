<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeBehind="ManageObligations.aspx.cs" Inherits="WiseThink.NTCA.Web.Admin.ManageObligations" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SuperAdminPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>Obligation under tri-partite MOU</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding ">
            <div id="DisplayErrorMessage" class="btn-danger " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="col-sm-10 col-sm-offset-2 MarginTop">
            <div class="col-sm-2">
                <cc:CustomLabel ID="lblObligationFor" runat="server" Text="Obligation For :"></cc:CustomLabel><b class="REerror"> *</b>
            </div>
            <div class="col-sm-6">
                <cc:CustomDropDownList ID="ddlObligationFor" CssClass="form-control " runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlObligationFor_SelectedIndexChanged">
                </cc:CustomDropDownList>
            </div>
        </div>
        <div class="col-sm-10 col-sm-offset-2 MarginTop">
            <div class="col-sm-2">
                <cc:CustomLabel ID="lblObligation" runat="server" Text="Descriptions :"></cc:CustomLabel><b class="REerror"> *</b>
            </div>
            <div class="col-sm-6">
                <cc:CustomTextBox ID="txtObligation" CssClass="form-control" runat="server" TextMode="MultiLine"
                    Rows="4" Columns="30"></cc:CustomTextBox>
                <asp:RegularExpressionValidator
                    ID="RegxObligation"
                    runat="server"
                    ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                    ControlToValidate="txtObligation"
                    ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t])*"
                    ForeColor="Red" SetFocusOnError="True">
                </asp:RegularExpressionValidator>
            </div>
            <div class="col-sm-2">
                <cc:CustomButton ID="btnAdd" runat="server" Text="Add Obligation" CssClass="btn-primary btn"
                    OnClick="btnAdd_Click" />
            </div>
        </div>
        <div class="col-sm-offset-2 table-responsive MarginTop col-sm-10 ">
            <cc:CustomGridView ID="cgvManageFDObligation" runat="server" CssClass="table  col-sm-10"
                AutoGenerateColumns="False" DataKeyNames="ObligationId" AllowPaging="True" PageSize="5"
                OnPageIndexChanging="cgvManageFDObligation_PageIndexChanging" OnRowCancelingEdit="cgvManageFDObligation_RowCancelingEdit"
                OnRowEditing="cgvManageFDObligation_RowEditing" OnRowUpdating="cgvManageFDObligation_RowUpdating"
                OnRowDeleting="cgvManageFDObligation_RowDeleting">
                <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                    Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                <EmptyDataTemplate>
                    <div class="EmpltyGridView">
                        &quot; Sorry! There is no data &quot;
                    </div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="Sr.No">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ObligationId" Visible="false">
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblObligationId" runat="server" Text='<%#Eval("ObligationId")%>'></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Obligations">
                        <ItemTemplate>
                            <asp:Label ID="lblObligation" runat="server" Text='<%#Eval("Descriptions") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <cc:CustomTextBox ID="txtObligation" runat="server" TextMode="MultiLine" Rows="4" Columns="60"
                                CssClass="form-control" Text='<%#Eval("Descriptions") %>'></cc:CustomTextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <cc:CustomButton ID="btnEditObligation" runat="server" OnClientClick="return confirm('Are you sure? you want to edit this item!');" CssClass="btn btn-primary ButtonMargin"
                                Text="Edit" CommandName="Edit" />
                            <cc:CustomButton ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure? you want to delete this item!');" CssClass="btn btn-primary" Text="Delete"
                                CommandName="Delete" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <cc:CustomButton ID="btnUpdate" runat="server" OnClientClick="return confirm('Are you sure? you want to update this obligation!');" CssClass="btn btn-primary ButtonMargin"
                                Text="Update" CommandName="Update" />
                            <cc:CustomButton ID="btnCancel" runat="server" CssClass="btn btn-primary" Text="Cancel"
                                CommandName="Cancel" />
                        </EditItemTemplate>
                        <ItemStyle CssClass="editButtonwidh btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                        <%--  <ItemStyle CssClass="editActionButtonwidh" />--%>
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
