<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true" 
    CodeBehind="GenerateReports.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.GenerateReports" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div class="row minheight">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <h4>
                    <b>Generate Reports</b></h4>
                <div class="text-center col-sm-12 ">
                    <div id="DisplayErrorMessage" class="btn-danger " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                </div>
                <div class="col-sm-12 MarginTop">
                    <div class="col-sm-2">
                        <cc:CustomLabel ID="lblFinancialYear" runat="server" Text="Select Financial Year"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4">
                        <cc:CustomDropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control" >
                            <asp:ListItem>--Select Financial Year--</asp:ListItem>
                        </cc:CustomDropDownList>
                    </div>
                </div>
                <div class="col-sm-12 MarginTop">
                    <div class="col-sm-2">
                        <cc:CustomLabel ID="lblfromDate" runat="server" Text="Select From Date & To Date"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-3">
                        <cc:CustomTextBox ID="txtFromDate" runat="server" CssClass="form-control  cal" placeholder="From Date"></cc:CustomTextBox>
                    </div>
                    <div class="col-sm-3">
                        <cc:CustomTextBox ID="txtToDate" runat="server" CssClass="form-control cal" placeholder="To Date"></cc:CustomTextBox>
                    </div>
                </div>
                <div class="col-sm-12 MarginTop">
                    <div class="col-sm-2 " >
                        <cc:CustomLabel ID="lblState" runat="server" Text="Select State"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4" style=" height:170px; overflow:auto;border: 1px #808080 solid;">
                        <asp:CheckBoxList ID="chkState" runat="server" OnSelectedIndexChanged="chkState_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                    </div>
                    <div class="col-sm-2">
                        <cc:CustomLabel ID="lblTigerReserve" runat="server" Text="Select Tiger Reserve"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4" style="height:170px; overflow:auto;border: 1px #808080 solid;">
                        <asp:CheckBoxList ID="chkTigerReserve" runat="server" OnSelectedIndexChanged="chkTigerReserve_SelectedIndexChanged" AutoPostBack="true">
                        </asp:CheckBoxList>
                    </div>
                </div>

                <div class="col-sm-12 MarginTop">
                    <div class="col-sm-2">
                         <cc:CustomLabel ID="lblActivityType" runat="server" Text="Select Activity Type"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4" style="border: 1px #808080 solid;" >
                        <asp:CheckBoxList ID="chkActivityType" runat="server" OnSelectedIndexChanged="chkActivityType_SelectedIndexChanged" AutoPostBack="true">
                        </asp:CheckBoxList>
                    </div>
                      <div class="col-sm-2">
                         <cc:CustomLabel ID="lblArea" runat="server" Text="Select Area"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4" style="border: 1px #808080 solid;" >
                        <asp:CheckBoxList ID="chkArea" runat="server" OnSelectedIndexChanged="chkArea_SelectedIndexChanged" AutoPostBack="true">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="col-sm-12 MarginTop">
                    <div class="col-sm-2">
                         <cc:CustomLabel ID="lblActivity" runat="server" Text="Select Activity"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4" style="height:170px; overflow:auto;border: 1px #808080 solid;">
                        <asp:CheckBoxList ID="chkActivity" runat="server">
                        </asp:CheckBoxList>
                    </div>
                    <div class="col-sm-2">
                         <cc:CustomLabel ID="lblActivityItems" runat="server" Text="Select Activity Items"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4" style="height:170px; overflow:auto;border: 1px #808080 solid;">
                        <asp:CheckBoxList ID="chkActivityItems" runat="server">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="col-sm-12 MarginTop">
                    <div class="col-sm-2">
                         <cc:CustomLabel ID="lblSubActivity" runat="server" Text="Select Sub Activity Items"></cc:CustomLabel>
                    </div>
                    <div class="col-sm-4" style="height:170px; overflow:auto;border: 1px #808080 solid;">
                        <asp:CheckBoxList ID="chkSubActivity" runat="server">
                        </asp:CheckBoxList>
                    </div>
                </div>

                <div class="row MarginTop"></div>
               
                <%--<div class="col-sm-12 table-responsive MarginTop">
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
                                &quot; Sorry! There is no data &quot;</div>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="btnmobile" Width="50px"/>
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
                </div>--%>
                <div class="col-sm-12 text-center">
                <cc:CustomButton ID="btnAddNew" CssClass="btn-primary btn TopMargin" runat="server" Text="Generate Reports" />                
                <button id="btnBack" type="button" onclick="window.location.href = 'Home.aspx'" class="btn btn-primary TopMargin">Back</button>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
