<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master" AutoEventWireup="true" CodeBehind="RelevantAPODocuments.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.RelevantAPODocuments" %>
<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
<div class="row minheight">
          <h4>
            <b>Relevant APO Documents</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
            <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>

         <div id="divTable" class="col-sm-12 table-responsive TopMargin text-center">
                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                     <cc:CustomGridView ID="cgvActivityItems" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett" 
                                      Width="100%" DataKeyNames="ActivityItemId" AllowPaging="True" PageSize="5"  OnPageIndexChanging="cgvActivityItems_PageIndexChanging"
                                      onpageindexchanged="cgvActivityItems_PageIndexChanged" ShowHeaderWhenEmpty="true"
                                         OnRowCommand="cgvActivityItems_RowCommand" >
                                            <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                                Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                            <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />                                         
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                                    <ItemTemplate>
                                                        <%#Eval("RowNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tiger Reserve Name" >
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblTigerReserve" runat="server" Text='<%#Eval("TigerReserveName")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Financial Year">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblYear" runat="server" Text='<%#Eval("FinancialYear")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                   <%-- <EditItemTemplate>
                                                        <cc:CustomLabel ID="lblAreaId" runat="server" Text='<%#Eval("AreaId")%>' Visible="false"></cc:CustomLabel>
                                                        <asp:DropDownList ID="ddlArea" Width="100px" CssClass="form-control dropdown" runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>--%>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity Item">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblActivity" runat="server" Text='<%#Eval("ActivityItem")%>'></cc:CustomLabel>
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Relevant Document">
                                                    <ItemTemplate>
                                                       <%-- <cc:CustomLabel ID="lblDocument" runat="server" Text='<%#Eval("Document")%>'></cc:CustomLabel>--%>
                                                     <cc:CustomLinkButton ID="lbtnDocumentFile" runat="server" Text='<%#Eval("Document")%>' 
                                                      OnClientClick="RemoveBlockUi();" CommandName="download"></cc:CustomLinkButton>
                                                    </ItemTemplate>
                                                    
                                                    <ItemStyle />
                                                </asp:TemplateField>                                               
                                            </Columns>
                                            <EmptyDataTemplate>
                                       <span class="text-danger">Sorry ! There is no Data to Display</span>
                                        </EmptyDataTemplate>
                                        </cc:CustomGridView>
                                    <%--</ContentTemplate>                                   
                                </asp:UpdatePanel>--%>
                            </div>
       
         <div class=" col-sm-12 TopMargin text-center">
            <cc:CustomButton ID="btnBack" CssClass="btn-primary btn col-sm-offset-0" runat="server"
                PostBackUrl="FieldDirectorHome.aspx" Text="Back" />
        </div>
     </div>
</asp:Content>
