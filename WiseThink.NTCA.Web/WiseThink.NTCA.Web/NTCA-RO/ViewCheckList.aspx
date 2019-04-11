<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="ViewCheckList.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.ViewCheckList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div class="row minheight">
        <div class="row">
            <h4 id="SubmitApoHeader" runat="server">
                <b>View Field Director's Checklist</b>
            </h4>
        </div>
        <div style="overflow: auto">
            <cc:CustomGridView ID="cgvFDCheckList" runat="server" CssClass="table  col-sm-12"
                                AutoGenerateColumns="False" DataKeyNames="ActivityId" AllowPaging="False" OnRowCreated="cgvFDCheckList_RowCreated" >
                                <EmptyDataTemplate>
                                <div class="EmpltyGridView">
                                    &quot; Sorry! There is no data &quot;</div>
                            </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Check list of the Field Director">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                            <asp:Label ID="lblActivityName" runat="server" Text='<%#Eval("ActivityName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Checked /</br>Not Applicable">
                                        <ItemTemplate>
                                            <asp:Label ID="lblViewCheck" runat="server" Text="Checked"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>
        </div>
    </div>
</asp:Content>
