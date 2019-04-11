<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="ManageGuideLines.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.ManageGuideLines" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            validation();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div class="row minheight">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <h4>
                    <b>Manage CSS-PT Guidelines</b></h4>
                <div class="text-center col-sm-12 ">
                    <div id="DisplayErrorMessage" class="btn-danger " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                </div>
                <div class="col-sm-8 col-sm-offset-2 MarginTop">
                    <div class="col-sm-3">
                        <cc:CustomLabel ID="lblCSSPTParaNumber" runat="server" Text="CSS-PT Para Number*:"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-6">
                        <cc:CustomTextBox ID="txtCSSPTParaNumber" CssClass="form-control" runat="server"
                            placeholder="Enter CSS-PT Para Number" MaxLength="5"></cc:CustomTextBox>
                        <asp:RegularExpressionValidator
                            ID="RegxCSSPTParaNumber"
                            runat="server"
                            ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                            ControlToValidate="txtCSSPTParaNumber"
                            ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t])*"
                            ForeColor="Red" SetFocusOnError="True">
                        </asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-8 col-sm-offset-2 MarginTop">
                    <div class="col-sm-3">
                        <cc:CustomLabel ID="lblCSSPTGuidelines" runat="server" Text="CSS-PT Guideline*:"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-6">
                        <cc:CustomTextBox ID="txtCSSPTGuidelines" runat="server" CssClass="form-control cal"
                            TextMode="MultiLine" Rows="6" Columns="80" placeholder="Enter CSS-PT Guideline"></cc:CustomTextBox>
                        <asp:RegularExpressionValidator
                            ID="RegxCSSPTGuidelines"
                            runat="server"
                            ErrorMessage="Special characters( +_&lt;/&gt;`~&amp;\]%'&quot;) are not allowed."
                            ControlToValidate="txtCSSPTGuidelines"
                            ValidationExpression="([a-z]|[A-Z]|[0-9]|[-]|[#]|[?]|[!]|[@]|[$]|[*]|[(]|[)]|[-]|[=]|[[]|[{]|[}]|[;]|[:]|[,]|[.]|[ ]|[/]|[+]|[\n]|[\t])*"
                            ForeColor="Red" SetFocusOnError="True">
                        </asp:RegularExpressionValidator>
                    </div>

                </div>
                <div class="row MarginTop"></div>
                <div class="row text-center TopMargin">
                    <cc:CustomButton ID="btnAdd" runat="server" Text="Click here to add new guideline"
                        CssClass="btn-primary btn" OnClick="btnAdd_Click" />
                </div>
                <div class="col-sm-12 table-responsive MarginTop">
                    <cc:CustomGridView ID="cgvManageGuidelines" runat="server" CssClass="table  col-sm-12"
                        AllowPaging="true" PageSize="5" AutoGenerateColumns="False" DataKeyNames="ID"
                        OnPageIndexChanging="cgvManageGuidelines_PageIndexChanging" OnRowCancelingEdit="cgvManageGuidelines_RowCancelingEdit"
                        OnRowEditing="cgvManageGuidelines_RowEditing" OnRowUpdating="cgvManageGuidelines_RowUpdating"
                        OnRowDeleting="cgvManageGuidelines_RowDeleting" OnPageIndexChanged="cgvManageGuidelines_PageIndexChanged">
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
                                <ItemStyle HorizontalAlign="Center" CssClass="btnmobile" Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GuidelineId" Visible="false">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblId" runat="server" Text='<%#Eval("ID")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CSSPT Para Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblCSSPTParaNumber" runat="server" Text='<%#Eval("CSSPTParaNumber") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <cc:CustomTextBox ID="txtCSSPTParaNumber" runat="server" CssClass="form-control" Text='<%#Eval("CSSPTParaNumber") %>'></cc:CustomTextBox>
                                </EditItemTemplate>
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CSSPT Guidelines">
                                <ItemTemplate>
                                    <asp:Label ID="lblCSSPTGuideline" runat="server" Text='<%#Eval("CSSPTGuideline") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <cc:CustomTextBox ID="txtCSSPTGuideline" TextMode="MultiLine" Rows="4" Columns="80" runat="server"
                                        CssClass="form-control" Text='<%#Eval("CSSPTGuideline") %>'></cc:CustomTextBox>
                                </EditItemTemplate>
                                <ItemStyle Width="600px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <cc:CustomButton ID="btnEditDesignation" runat="server" OnClientClick="return confirm('Are you sure? you want to edit this guideline!');"
                                        CssClass="btn btn-primary ButtonMargin" Text="Edit" CommandName="Edit" />
                                    <cc:CustomButton ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure? you want to delete this guideline!');"
                                        CssClass="btn btn-primary ButtonMargin" Text="Delete" CommandName="Delete" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <cc:CustomButton ID="btnUpdate" runat="server" OnClientClick="return confirm('Are you sure? you want to update this guideline!');"
                                        CssClass="btn btn-primary ButtonMargin" Text="Update" CommandName="Update" />
                                    <cc:CustomButton ID="btnCancel" runat="server" CssClass="btn btn-primary ButtonMargin"
                                        Text="Cancel" CommandName="Cancel" />
                                </EditItemTemplate>
                                <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </cc:CustomGridView>
                </div>
                <div class="row MarginTop"></div>
                <div class="row text-center MarginTop ">
                    <button id="btnBack" type="button" onclick="window.location.href = 'Home.aspx'" class="btn btn-primary">
                        Back</button>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
