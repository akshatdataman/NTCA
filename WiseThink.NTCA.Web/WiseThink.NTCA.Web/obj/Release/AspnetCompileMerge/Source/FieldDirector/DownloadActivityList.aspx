<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="DownloadActivityList.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.DownloadActivityList" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>Download Activity List</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
            <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="col-md-offset-9 col-md-3 text-right">
            <cc:CustomImageButton ID="imgbtnExcel" runat="server"  OnClientClick="RemoveBlockUi();"
                ImageUrl="../Images/sampleExcelIcon.jpg" OnClick="imgbtnExcel_Click" />
        </div>
        <div id="divTable" class="col-sm-12 table-responsive TopMargin text-center">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <cc:CustomGridView ID="cgvActivityItems" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-bordered tablett" Width="100%" DataKeyNames="ActivityItemId"
                        AllowPaging="True" PageSize="5" OnPageIndexChanging="cgvActivityItems_PageIndexChanging"
                        OnPageIndexChanged="cgvActivityItems_PageIndexChanged" EnableSortingAndPagingCallbacks="true"
                        ShowHeaderWhenEmpty="true">
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                            Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                        <%--  <EmptyDataTemplate>
                                                <div class="EmpltyGridView">
                                                    &quot; Sorry! There is no data &quot;</div>
                                            </EmptyDataTemplate>--%>
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                <ItemTemplate>
                                    <%#Eval("RowNumber")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ActivityItemId" Visible="false">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblActivityItemId" runat="server" Text='<%#Eval("ActivityItemId")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Area Type">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblAreaType" runat="server" Text='<%#Eval("Area")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <cc:CustomLabel ID="lblAreaId" runat="server" Text='<%#Eval("AreaId")%>' Visible="false"></cc:CustomLabel>
                                    <asp:DropDownList ID="ddlArea" Width="100px" CssClass="form-control dropdown" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Type">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblActivityType" runat="server" Text='<%#Eval("ActivityType")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <cc:CustomLabel ID="lblActivityTypeId" runat="server" Text='<%#Eval("ActivityTypeId")%>'
                                        Visible="false"></cc:CustomLabel>
                                    <asp:DropDownList ID="ddlActivityType" Width="100px" CssClass="form-control dropdown"
                                        runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblActivity" runat="server" Text='<%#Eval("ActivityName")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <cc:CustomLabel ID="lblActivityId" runat="server" Text='<%#Eval("ActivityId")%>'
                                        Visible="false"></cc:CustomLabel>
                                    <asp:DropDownList ID="ddlActivity" Width="120px" CssClass="form-control dropdown"
                                        runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Para No. CSS PT Guidelines">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblCSSPTParaNo" runat="server" Text='<%#Eval("ParaNoCSSPTGuidelines")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <cc:CustomTextBox ID="txtCSSPTParaNo" runat="server" CssClass="form-control GridTxt"
                                        Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></cc:CustomTextBox>
                                </EditItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Items">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <cc:CustomTextBox ID="txtActivityItem" runat="server" CssClass="form-control GridTxt"
                                        Text='<%#Eval("ActivityItem") %>' TextMode="MultiLine" Rows="6" Width="150px"></cc:CustomTextBox>
                                </EditItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <span class="text-danger">Sorry ! There is no Data to Display</span>
                        </EmptyDataTemplate>
                    </cc:CustomGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class=" col-sm-12 TopMargin text-center">
            <cc:CustomButton ID="btnBack" CssClass="btn-primary btn col-sm-offset-0" runat="server"
                PostBackUrl="FieldDirectorHome.aspx" Text="Back" />
        </div>
    </div>
</asp:Content>
