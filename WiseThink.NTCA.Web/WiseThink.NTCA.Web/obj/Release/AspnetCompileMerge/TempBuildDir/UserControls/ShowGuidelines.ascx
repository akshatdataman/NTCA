<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowGuidelines.ascx.cs" Inherits="WiseThink.NTCA.Web.UserControls.ShowGuidelines" %>
<div class="col-sm-12 table-responsive MarginTop">
                    <cc:CustomGridView ID="cgvManageGuidelines" runat="server" CssClass="table  col-sm-12"
                        AllowPaging="true" PageSize="5" AutoGenerateColumns="False" DataKeyNames="ID"
                        OnPageIndexChanging="cgvManageGuidelines_PageIndexChanging">
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
                                    <asp:TextBox ID="txtCSSPTParaNumber" runat="server" CssClass="form-control" Text='<%#Eval("CSSPTParaNumber") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CSSPT Guidelines">
                                <ItemTemplate>
                                    <asp:Label ID="lblCSSPTGuideline" runat="server" Text='<%#Eval("CSSPTGuideline") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCSSPTGuideline" TextMode="MultiLine" Rows="4" Columns="80" runat="server"
                                        CssClass="form-control" Text='<%#Eval("CSSPTGuideline") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Width="600px" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <cc:CustomButton ID="btnEditDesignation" runat="server" CssClass="btn btn-primary ButtonMargin"
                                        Text="Edit" CommandName="Edit" />
                                    <cc:CustomButton ID="btnDelete" runat="server" CssClass="btn btn-primary ButtonMargin"
                                        Text="Delete" CommandName="Delete" />
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
                </div>