<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="CheckList.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.CheckList" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <h4>
                <b>FIELD DIRECTOR CHECKLIST</b></h4>
            <div class="col-sm-2">
                <div id="leftPartMy1" class="">
                    <div id="leftNav">
                        <div class="panel-group" id="accordion">
                            <div class=" panel-default ">
                                <div id="panel1" class="panel-collapse collapse in menuleft">
                                    <div class="table-responsive">
                                        <ul>
                                        <li class="FirstSubMenu1 BackgroundColor NonRecuring_left Links"><a class="anchorlink" href="SubmitAPONew.aspx">Step 1: Non-Recurring</a></li>
                                                <li class="BackgroundColor Links Recuring_left"><a class="anchorlink" href="SubmitAPONew.aspx">Step 2: Recurring</a></li>
                                      
                                       <li class="BackgroundColor"><a href="FDUtilizationCertificate.aspx">Step 3: Utilization Certificate</a></li>
                                      
                                       <li class="BackgroundColor "><a href="ObligationFD.aspx">Step 4: Obligation Under Tri-MOU</a></li>
                                       
                                       <li class="BackgroundColor Current"><a href="CheckList.aspx?callfrom=SubmitAPO">Step 5: Check and Submit</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-10  leftBorder minHeight Home_margin" id="MainContent">
                <div class=" table-responsive ">
                    <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                    <div id="PFYUcDiv" class="row MarginTop" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <label class="col-sm-3">
                                    Upload PFY Provisional UC :</label>
                                <div class="col-sm-4">
                                    <cc:CustomFileUpload ID="fuUploadProvisionalUC" runat="server" CssClass="fileuploadSize" />
                                    <cc:CustomLabel ID="lblFileName" runat="server" Text=""></cc:CustomLabel>
                                    <asp:RegularExpressionValidator runat="server" ID="revfuUc" ControlToValidate="fuUploadProvisionalUC"
                                        ErrorMessage="Allow only .pdf File" ValidationExpression="^.*\.(pdf|PDF)$"
                                        CssClass="REerror" />
                                </div>
                                </ContentTemplate>
                        </asp:UpdatePanel>
                                <div class="col-sm-3 col-sm-offset-0">
                                    <asp:LinkButton ID="lbtnFileName" runat="server" CssClass="LinkColor" OnClick="lbtnFileName_Click"></asp:LinkButton>
                                </div>
                                <div class="col-sm-2">
                                    <cc:CustomButton ID="btnUploadProvisionalUc" runat="server" Text="Upload" CssClass="btn btn-primary"
                                        OnClick="btnUploadProvisionalUc_Click" />
                                </div>
                           
                    </div>
                    <asp:UpdatePanel ID="upnlObligation" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cc:CustomGridView ID="cgvFDCheckList" runat="server" CssClass="table  col-sm-12"
                                AutoGenerateColumns="False" DataKeyNames="ActivityId" AllowPaging="false" PageSize="10"
                                OnPageIndexChanging="cgvFDCheckList_PageIndexChanging" OnRowCreated="cgvFDCheckList_RowCreated">
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
                                    <asp:TemplateField HeaderText="Check list of the Field Director">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivityId" runat="server" Visible="false" Text='<%#Eval("ActivityId") %>'></asp:Label>
                                            <asp:Label ID="lblActivityName" runat="server" Text='<%#Eval("ActivityName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Checked /</br>Not Applicable">
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rblCheckedOrNot" AutoPostBack="true" EnableViewState="true"
                                                runat="server" OnSelectedIndexChanged="rblCheckedOrNot_SelectedIndexChanged">
                                                <asp:ListItem Text="Checked" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Not Applicable" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblCheckedOrNot"
                                                ErrorMessage="Required Field" CssClass="REerror">
                                            </asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                            </cc:CustomGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div id="ButtonDiv" runat="server" class="col-sm-10 text-center TopMargin margin_bottom">
                        <cc:CustomButton ID="btnSave" runat="server" Text="Save" CssClass="btn-primary btn "
                            OnClientClick="return confirm('Are you sure? you want to submit checklist');"
                            OnClick="btnSave_Click" />
                        <cc:CustomButton ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-primary btn col-sm-offset-1"
                            OnClientClick="return confirm('Are you sure? you want to submit this apo to CWLW.\nModification can not be done after the final submission!!');"
                            OnClick="btnSubmit_Click" />
                        <%--<cc:CustomButton ID="btnBack" CssClass="btn-primary btn col-sm-offset-1 " runat="server"
                            PostBackUrl="FieldDirectorHome.aspx" Text="Back" />--%>
                        <button id="btnBack" type="button" onclick="window.location.href = 'FieldDirectorHome.aspx'"
                            class="btn btn-primary col-sm-offset-1">
                            Back</button>
                    </div>
                    <div class="TopMargin">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
