<%@ Page Title="" Language="C#" MasterPageFile="~/CWW-Secretary/CWW-Secretary.master"
    AutoEventWireup="true" CodeBehind="MonitoringCentral-StateShare.aspx.cs" Inherits="WiseThink.NTCA.Web.CWW_Secretary.MonitoringCentral_StateShare" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            validation();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CWWPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>Monitoring Central : State Share and Releasing State Share</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-11 ">
            <div id="DisplayErrorMessage" class="btn-danger " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="col-sm-12 MarginTop">
            <div class="col-sm-4">
                <cc:CustomLabel ID="lblApoTitle" runat="server" Text="Select APO:"></cc:CustomLabel><b class="REerror"> *</b>
            </div>
            <div class="col-sm-8">
                <cc:CustomDropDownList ID="ddlApoTitle" runat="server" req="1" Height="30px" class="form-control col-sm-8"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlApoTitle_SelectedIndexChanged">
                </cc:CustomDropDownList>
            </div>
        </div>
        <div id="IFDdiv" runat="server" class="col-sm-12 MarginTop">
            <div class="col-sm-3">
                <cc:CustomLabel ID="lblIfdDiaryNumber" runat="server" Text="IFD Diary Number :"></cc:CustomLabel><b class="REerror"> *</b>
            </div>
            <div class="col-sm-3">
                <cc:CustomTextBox ID="txtIfdDiaryNumber" CssClass="form-control" req="1" runat="server"
                    placeholder="Enter IFD Diary Number"></cc:CustomTextBox>
            </div>
            <div class="col-sm-3">
                <cc:CustomLabel ID="lblIfdDate" runat="server" Text="IFD Date :"></cc:CustomLabel><b class="REerror"> *</b>
            </div>
            <div class="col-sm-3">
                <cc:CustomTextBox ID="txtIfdDate" runat="server" CssClass="form-control cal" req="1"
                    placeholder="Enter IFD Date"></cc:CustomTextBox>
            </div>
        </div>
        <div id="FistInstallmentDiv" runat="server" class="col-sm-12 MarginTop">
            <div class="col-sm-4">
                <cc:CustomLabel ID="lblFirstRelease" runat="server" Text="First Installment Releasing Amount:"></cc:CustomLabel>
            </div>
            <div class="col-sm-4">
                <cc:CustomTextBox ID="txtFirstRelease" CssClass="form-control" runat="server"></cc:CustomTextBox>
            </div>
            <div class="col-sm-4">
                <cc:CustomButton ID="btnFirstRelease" runat="server" Text="Click here to release first installment"
                    CssClass="btn-primary btn" OnClick="btnFirstRelease_Click" />
            </div>
        </div>
        <div id="SecondInstallmentDiv" runat="server" class="col-sm-12 MarginTop">
            <div class="col-sm-4">
                <cc:CustomLabel ID="lblSecondRelease" runat="server" Text="Second Installment Releasing Amount:"></cc:CustomLabel>
            </div>
            <div class="col-sm-4">
                <cc:CustomTextBox ID="txtSecondRelease" CssClass="form-control" runat="server"></cc:CustomTextBox>
            </div>
            <div class="col-sm-4">
                <cc:CustomButton ID="btnSecondRelease" runat="server" Text="Click here to release second installment"
                    CssClass="btn-primary btn" OnClick="btnSecondRelease_Click" />
            </div>
        </div>
        <div class="col-sm-12 table-responsive MarginTop">
            <cc:CustomGridView ID="cgvCentralStateShare" runat="server" CssClass="table  col-sm-12"
                AutoGenerateColumns="False" DataKeyNames="APOFileId" AllowPaging="True" PageSize="5"
                OnPageIndexChanging="cgvCentralStateShare_PageIndexChanging" OnRowCancelingEdit="cgvCentralStateShare_RowCancelingEdit"
                OnRowEditing="cgvCentralStateShare_RowEditing" OnRowUpdating="cgvCentralStateShare_RowUpdating">
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
                    <asp:TemplateField HeaderText="APOFileId" Visible="false">
                        <ItemTemplate>
                            <cc:CustomLabel ID="lblAPOFileId" runat="server" Text='<%#Eval("APOFileId")%>'></cc:CustomLabel>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="324px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Apo Amount Rs. (In Lakhs)">
                        <ItemTemplate>
                            <asp:Label ID="lblApoAmount" runat="server" Text='<%#Eval("ApoAmount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="324px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Flexi Amount Rs. (In Lakhs)">
                        <ItemTemplate>
                            <asp:Label ID="lblFlexiAmount" runat="server" Text='<%#Eval("FlexiAmount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="324px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Sanction Amount Rs. (In Lakhs)">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("SanctionAmount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="324px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Central Share Rs. (In Lakhs)">
                        <ItemTemplate>
                            <asp:Label ID="lblCentralShare" runat="server" Text='<%#Eval("CentralShare") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="324px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="State Share Rs. (In Lakhs)">
                        <ItemTemplate>
                            <asp:Label ID="lblStateShare" runat="server" Text='<%#Eval("StateShare") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="324px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="First Released Amount Rs. (In Lakhs)">
                        <ItemTemplate>
                            <asp:Label ID="lblFirstReleasedAmount" runat="server" Text='<%#Eval("FirstStateRelease") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <cc:CustomTextBox ID="txtFirstReleasedAmount" runat="server" CssClass="form-control" Text='<%#Eval("FirstStateRelease") %>'></cc:CustomTextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="324px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Second Released Amount Rs. (In Lakhs)">
                        <ItemTemplate>
                            <asp:Label ID="lblSecondReleasedAmount" runat="server" Text='<%#Eval("SecondStateRelease") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <cc:CustomTextBox ID="txtSecondReleasedAmount" runat="server" CssClass="form-control"
                                Text='<%#Eval("SecondStateRelease") %>'></cc:CustomTextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="324px" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <cc:CustomButton ID="btnEdit" runat="server" CssClass="btn btn-primary ButtonMargin"
                                Text="Edit" CommandName="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <cc:CustomButton ID="btnUpdate" runat="server" CssClass="btn btn-primary ButtonMargin"
                                Text="Update" CommandName="Update" />
                            <cc:CustomButton ID="btnCancel" runat="server" CssClass="btn btn-primary ButtonMargin"
                                Text="Cancel" CommandName="Cancel" />
                        </EditItemTemplate>
                        <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                    </asp:TemplateField>--%>
                </Columns>
            </cc:CustomGridView>
            <div class="TopMargin">
            </div>
        </div>
    </div>
</asp:Content>
