<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master" AutoEventWireup="true" CodeBehind="ViewCheckList.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.ViewCheckList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
<div>
        <div class="row minheight">
            <h4>
                <b>FIELD DIRECTOR CHECKLIST</b></h4>
            <div class="col-sm-3">
                <div id="leftPartMy1" class="">
                    <div id="leftNav">
                        <div class="panel-group" id="accordion">
                            <div class=" panel-default ">
                                <div id="panel1" class="panel-collapse collapse in menuleft">
                                    <div class="table-responsive">
                                         <ul>
                                        <li class="FirstSubMenu1  BackgroundColor"><a class="submenu" href="ViewAPO.aspx">
                                            Non-Recurring</a></li>
                                        <li class="BackgroundColor"><a href="ViewAPO.aspx">Recurring</a></li>
                                        <%--<li class="BackgroundColor"><a href="ViewEcoDevelopment.aspx">Eco Development</a></li>--%>
                                        <li class="BackgroundColor"><a href="#Utilization">Utilization Certificate</a></li>
                                        <li class="BackgroundColor "><a href="#">Spill Over</a></li>
                                        <li class="BackgroundColor "><a href="#">Revalidation</a></li>
                                        <li class="BackgroundColor "><a href="#">Adjustment</a></li>
                                        <li class="BackgroundColor "><a href="FDUtilizationCertificate.aspx">Obligations</a></li>
                                        <li class="BackgroundColor "><a href="FDFeedbackMOU.aspx">Feedback of Compliance MOU</a></li>
                                        <li class="BackgroundColor Current"><a href="ViewCheckList.aspx">Checklist</a></li>
                                        <%--<li class="BackgroundColor"><a href="FDFlexiAmont.aspx">Flexi Amount</a></li>--%>
                                    </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-9  leftBorder minHeight Home_margin" id="MainContent">
                <div class=" table-responsive TopMargin ">
                    <asp:UpdatePanel ID="upnlObligation" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cc:CustomGridView ID="cgvFDCheckList" runat="server" CssClass="table  col-sm-12"
                                AutoGenerateColumns="False" DataKeyNames="ActivityId" AllowPaging="True" OnRowCreated="cgvFDCheckList_RowCreated" >
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="col-sm-10 text-center TopMargin margin_bottom">
                        <cc:CustomButton ID="btnSave" runat="server" Text="Save" CssClass="btn-primary btn "
                             />
                        <cc:CustomButton ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-primary btn col-sm-offset-1"
                             />
                        <cc:CustomButton ID="btnBack" CssClass="btn-primary btn col-sm-offset-1 " runat="server" PostBackUrl="FieldDirectorHome.aspx"
                            Text="Back" />
                    </div>
                    <div class="TopMargin">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
