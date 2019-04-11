<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="SettledUc.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.SettledUc" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <h4>
                <b>SETTLED DOWN UNSPENT AMOUNT</b></h4>
            <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
                <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                    <uc:ValidationMessage ID="vmError" runat="server" />
                </div>
                <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                    <uc:ValidationMessage ID="vmSuccess" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 minHeight Home_margin" id="MainContent">
                <div class=" table-responsive TopMargin ">
                    <asp:UpdatePanel ID="upnlUnspentAmount" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cc:CustomGridView ID="cgvUnspent" runat="server" CssClass="table  col-sm-12" AutoGenerateColumns="False"
                                DataKeyNames="ActivityItemId" AllowPaging="True" PageSize="5" OnPageIndexChanging="cgvUnspent_PageIndexChanging">
                                <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                    Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                <EmptyDataTemplate>
                                    <div class="EmpltyGridView">
                                        &quot; Sorry! There is no data &quot;</div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activity">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnId" runat="server" />
                                            <asp:Label ID="lblAreaId" runat="server" Visible="false" Text='<%#Eval("AreaId") %>'></asp:Label>
                                            <asp:Label ID="lblActivityTypeId" runat="server" Visible="false" Text='<%#Eval("ActivityTypeId") %>'></asp:Label>
                                            <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                            <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                            <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Para No. CSS PT Guidelines">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnParaNo" runat="server" CssClass="LinkColor" Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unspent Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReleasedAmount" runat="server" Visible="false" Text='<%#Eval("ReleasedAmount") %>'></asp:Label>
                                            <asp:Label ID="lblUnspentAmount" runat="server" Text='<%#Eval("UnspentAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Is Re-Validation Adjustment">
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rblRevalidateAdjustment" AutoPostBack="true" EnableViewState="true"
                                                runat="server" OnSelectedIndexChanged="rblRevalidateAdjustment_SelectedIndexChanged">
                                                <asp:ListItem Text="Re-Validate" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Adjustment" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblRevalidateAdjustment"
                                                ErrorMessage="Required Field" CssClass="REerror">
                                            </asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Is Spillover Adjustment">
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rblSpilloverAdjustment" AutoPostBack="true" EnableViewState="true"
                                            runat="server" OnSelectedIndexChanged="rblSpilloverAdjustment_SelectedIndexChanged" >
                                            <asp:ListItem Text="Spillover" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Adjustment" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Justification">
                                        <ItemTemplate>
                                            <cc:CustomTextBox ID="txtJustification" runat="server" Enabled="false" TextMode="MultiLine"
                                                Rows="4" CssClass="less_size" Text=""></cc:CustomTextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="col-sm-10 text-center TopMargin bottomMargin">
                        <cc:CustomButton ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-primary btn"
                            OnClick="btnSubmit_Click" />
                        <cc:CustomButton ID="btnBack" CssClass="btn-primary btn " runat="server" PostBackUrl="FDUtilizationCertificate.aspx"
                            Text="Back" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
