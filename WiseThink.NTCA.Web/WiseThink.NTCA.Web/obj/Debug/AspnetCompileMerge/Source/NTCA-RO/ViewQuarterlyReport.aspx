<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ViewQuarterlyReport.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.ViewQuarterlyReport" %>
<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
<div class="row minheight">
        <div class="row">
            <h4 id="SubmitApoHeader" runat="server">
                <b>View Quarterly Reports</b>
                <cc:CustomImageButton ID="imgbtnWord" runat="server" ImageUrl="~/Images/Word-icon.png"
                    OnClick="imgbtnWord_click" CssClass="col-sm-offset-6" />
                <cc:CustomImageButton ID="imgbtnPdf" runat="server" ImageUrl="~/Images/samplePDFIcon.png"
                    OnClick="imgbtnPdf_click" CssClass="" />
                <cc:CustomImageButton ID="imgbtnExcel" runat="server" ImageUrl="~/Images/sampleExcelIcon.jpg"
                    OnClick="imgbtnExcel_click" CssClass="" />
            </h4>
        </div>
        <div class="row">
            <div class="col-sm-4">
                <cc:CustomLabel ID="lblApoTitle" runat="server" Text="Select Apo Title*:"></cc:CustomLabel>
            </div>
            <div class="col-sm-6">
                <cc:CustomDropDownList ID="ddlApoTitle" runat="server" req="1" Height="30px" class="form-control col-sm-8"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlApoTitle_SelectedIndexChanged">
                </cc:CustomDropDownList>
            </div>
        </div>
        <div class="row TopMargin" style="display:none" >
            <div class="col-sm-4">
                <cc:CustomLabel ID="lblNRQuarter" runat="server" Text="Select Quarter"></cc:CustomLabel>
            </div>
            <div class="col-sm-6" >
                <cc:CustomDropDownList ID="ddlNRQuarter" CssClass="form-control col-sm-8" runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlNRQuarter_SelectedIndexChanged">
                </cc:CustomDropDownList>
            </div>
        </div>
        <div class="TopMargin">
        </div>
        <div class="col-sm-12  minHeight" id="MainContent" runat="server">
        <div class="row text-center">
        <h3 id="ApoTitle" runat="server"></h3>
        </div>
        <div class="TopMargin">
        </div>
            <!-- Non-Recurring-->
            <div id="Non-RecurringDiv">
                <div class="headingCss">
                    <h3>
                        Non-Recurring</h3>
                </div>
                <div class="tablettcolor">
                    <cc:CustomGridView ID="gvNonRecurring" runat="server" AutoGenerateColumns="false"
                        ShowHeader="false" OnRowCreated="gvNonRecurring_RowCreated" DataKeyNames="ID"
                        CssClass="table table-bordered table-responsive tablett ">
                        <EmptyDataTemplate>
                            <div class="EmpltyGridView">
                                &quot; Sorry! There is no data &quot;</div>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Activity">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnId" runat="server" />
                                    <asp:Label ID="lblActivityTypeId" runat="server" Visible="false" Text='<%#Eval("ActivityTypeId") %>'></asp:Label>
                                    <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                    <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                    <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="500px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sanctioned Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sanctioned Location">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Central Share">
                                <ItemTemplate>
                                    <asp:Label ID="lblCentralShare" runat="server" Text='<%#Eval("CentralShare") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="StateShare">
                                <ItemTemplate>
                                    <asp:Label ID="lblStateShare" runat="server" Text='<%#Eval("StateShare") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lblReleasedAmount" runat="server" Text='<%#Eval("ReleasedAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Progress Quantity Assessment">
                                <ItemTemplate>
                                    <asp:Label ID="txtQuantity" runat="server" CssClass="less_size" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Progress Location">
                                <ItemTemplate>
                                    <asp:Label ID="txtLocation" runat="server" CssClass="less_size" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Progress Assessment Physical Target">
                                <ItemTemplate>
                                    <asp:Label ID="txtPhysicalTarget" runat="server" req="1" CssClass="less_size" Text=""
                                        TextMode="MultiLine" Rows="4" Columns="3"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Progress Assessment Financial Target">
                                <ItemTemplate>
                                    <asp:Label ID="txtFinancial" runat="server" CssClass="less_size" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </cc:CustomGridView>
                </div>
                <div class="table-responsive tableWidthAPO">
                </div>
            </div>
            <div id="RecuringDiv" runat="server">
                <div>
                    <div class="headingCss">
                        <h3>
                            Recurring</h3>
                    </div>
                    <!--Here is Recuring-->
                    <div class="tablettcolor">
                        <cc:CustomGridView ID="gvRecurring" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"
                            ShowHeader="false" OnRowCreated="gvRecurring_RowCreated" CssClass="table table-bordered table-responsive tablett ">
                            <EmptyDataTemplate>
                            <div class="EmpltyGridView">
                                &quot; Sorry! There is no data &quot;</div>
                        </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Activity">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnId" runat="server" />
                                        <asp:Label ID="lblActivityTypeId" runat="server" Visible="false" Text='<%#Eval("ActivityTypeId") %>'></asp:Label>
                                        <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                        <asp:Label ID="lblActivity" runat="server" Text='<%#Eval("Activity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActivityItemId" runat="server" Visible="false" Text='<%#Eval("ActivityItemId") %>'></asp:Label>
                                        <asp:Label ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="500px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sanctioned Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sanctioned Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Central Govt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCentralShare" runat="server" Text='<%#Eval("CentralShare") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State Govt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStateShare" runat="server" Text='<%#Eval("StateShare") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReleasedAmount" runat="server" Text='<%#Eval("ReleasedAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Progress Quantity Assessment">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQuantity" runat="server" CssClass="less_size" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Progress Location">
                                    <ItemTemplate>
                                        <asp:Label ID="txtLocation" runat="server" CssClass="less_size" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Progress Assessment Physical Target">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPhysicalTarget" runat="server" req="1" CssClass="less_size" Text=""
                                            TextMode="MultiLine" Rows="4" Columns="3"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Progress Assessment Financial Target">
                                    <ItemTemplate>
                                        <asp:Label ID="txtFinancial" runat="server" CssClass="less_size" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </cc:CustomGridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="TopMargin">
        </div>
        <div class="text-center TopMargin">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary col-sm-offset-1"
                OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>
