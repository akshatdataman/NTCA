<%@ Page Title="" Language="C#" MasterPageFile="~/CWW-Secretary/CWW-Secretary.master"
    AutoEventWireup="true" CodeBehind="ObligationCWW.aspx.cs" Inherits="WiseThink.NTCA.Web.CWW_Secretary.ObligationCWW" %>
<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     $(function () {
         BindValidationMethod();
     });
     function blinker() {
         $('.blinking').fadeOut(200);
         $('.blinking').fadeIn(200);
     }
     setInterval(blinker, 6000);
    </script>
    <%--<script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            $('#<%=cgvObligationCWW.ClientID %>').Scrollable({
                ScrollHeight: 500
            });
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $('#<%=cgvObligationCWW.ClientID %>').Scrollable({
                            ScrollHeight: 500
                        });
                    }

                });
            }
            else {
                $('#<%=cgvObligationCWW.ClientID %>').Scrollable({
                    ScrollHeight: 500
                });
            }
        });
        
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CWWPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <h4>
                <b>CHIEF WILDLIFE WARDEN/SECRETARY FOREST'S OBLIGATIONS</b></h4>
                <div class ="row">
                <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                </div>
            <div class="col-sm-12 table-responsive TopMargin ">
                <div class="col-sm-12 margin_bottom text-center">
                    <b class="blinking">Level of Compliance score Categories: </b> <span class="text-danger blinking"> 1. 
                        (0 - 30)</span> <span class="text-success col-sm-offset-1 blinking"> 2.  (30 - 60)</span> <span class="col-sm-offset-1 blinking"
                            style="color: Teal"> 3.  (60 - 100)</span>
                </div>
                 <div class="col-md-offset-9 col-md-3 text-right">
                     <asp:LinkButton ID="LbtnSelectAll" runat="server" OnClick="LbtnSelectAll_Click" CssClass="leftPadddingclass" CausesValidation="false">Select All</asp:LinkButton>
                </div>
                <asp:UpdatePanel ID="upnlObligation" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cc:CustomGridView ID="cgvObligationCWW" runat="server" CssClass="table  col-sm-12"
                            AutoGenerateColumns="False" DataKeyNames="ObligationId" AllowPaging="False" PageSize="5"
                            OnPageIndexChanging="cgvObligationCWW_PageIndexChanging">
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
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Obligations of the Chief Wildlife Warden / Secretary">
                                    <ItemTemplate>
                                        <asp:Label ID="lblObligationId" runat="server" Visible="false" Text='<%#Eval("ObligationId") %>'></asp:Label>
                                        <asp:Label ID="lblObligation" runat="server" Text='<%#Eval("Descriptions") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Complied /</br>Not Complied /</br>Not Applicable">
                                    <ItemTemplate>
                                        <asp:RadioButtonList ID="rblCompiledOrNot" AutoPostBack="true" EnableViewState="true"
                                            req="1" runat="server" OnSelectedIndexChanged="rblCompiledOrNot_SelectedIndexChanged">
                                            <asp:ListItem Text="Complied" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Not Complied" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Not Applicable" Value="3"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblCompiledOrNot"
                                            ErrorMessage="Required Field" ForeColor="Red" Font-Bold="true">
                                        </asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Level of Compliance">
                                    <ItemTemplate>
                                        <cc:CustomTextBox ID="txtLevelofComplaince" runat="server" Enabled="false" CssClass="less_size"
                                            Text=""></cc:CustomTextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="If not, reason there for">
                                    <ItemTemplate>
                                        <cc:CustomTextBox ID="txtReason" runat="server" Enabled="false" CssClass="less_size"
                                            Text=""></cc:CustomTextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </cc:CustomGridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-sm-10 text-center TopMargin">
                    <cc:CustomButton ID="btnSave" runat="server" Text="Save" CssClass="btn-primary btn"
                        OnClick="btnSave_Click" />
                    <cc:CustomButton ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-primary btn"
                        OnClientClick="return confirm('Are you sure? you want to obligation under Tri-MOU!');"
                        OnClick="btnSubmit_Click" />
                    <cc:CustomButton ID="btnBack" CssClass="btn-primary btn " runat="server" PostBackUrl="Home.aspx"
                        Text="Back" />
                </div>
                <div class="TopMargin">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
