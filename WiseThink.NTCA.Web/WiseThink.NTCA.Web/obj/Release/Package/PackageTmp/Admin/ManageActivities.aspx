<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeBehind="ManageActivities.aspx.cs" Inherits="WiseThink.NTCA.Web.Admin.ManageActivities" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .form-control
    {
        width:100%;
    } 
    .TopMargin{margin-bottom:5px;}
    </style>
   <script type="text/javascript">
       $(window).load(function () {
           $('#<%=gvSubItems.ClientID %>').parent().addClass("table-responsive");
       });
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SuperAdminPlaceHolder" runat="server">
    <div class="row minheight">
        <h4>
            <b>Manage Activities</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
            <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="col-sm-12">
            <div id="content" class="col-sm-12 ">
                <h3 class="panel image">
                    AREA <span runat="server" class=""></span>
                </h3>
                <div id="divTable" class="collapse table-responsive">
                    <cc:CustomGridView ID="cgvArea" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                        Width="100%" DataKeyNames="AreaId" OnPageIndexChanging="cgvArea_PageIndexChanging"
                        AllowPaging="True" PageSize="5">
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                            Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                        <EmptyDataTemplate>
                            <div class="EmpltyGridView">
                                &quot; Sorry! There is no data &quot;</div>
                        </EmptyDataTemplate>
                        <Columns>
                            <%--<asp:BoundField HeaderText="Sr.No." DataField="SrNo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblSrNo" runat="server" Text='<%# Container.DisplayIndex + 1 %>'></cc:CustomLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Area Type">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblAreaType" runat="server" Text='<%#Eval("Area")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Active / Inactive">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblStatus" runat="server" Text='<%#Eval("IsActive")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <cc:CustomLinkButton ID="lbAction" runat="server" CssClass="LinkColor" Text="Edit"></cc:CustomLinkButton>
                                        </ItemTemplate>
                                        <ItemStyle  />
                                    </asp:TemplateField>--%>
                        </Columns>
                    </cc:CustomGridView>
                </div>
                <h3 class="panel image">
                    ACTIVITY TYPE<span runat="server" class="NoColor"></span></h3>
                <div id="divTable3" class="collapse table-responsive">
                    <cc:CustomGridView ID="cgvActivityType" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-bordered table-responsive tablett" Width="100%" DataKeyNames="ActivityTypeId"
                        OnPageIndexChanging="cgvActivityType_PageIndexChanging" AllowPaging="True" PageSize="5">
                        <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                            Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                        <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                        <EmptyDataTemplate>
                            <div class="EmpltyGridView">
                                &quot; Sorry! There is no data &quot;</div>
                        </EmptyDataTemplate>
                        <Columns>
                            <%--                                    <asp:BoundField HeaderText="Sr.No." DataField="SrNo">
                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Area Type">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblAreaType" runat="server" Text='<%#Eval("Area")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Activity Type">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblActivityType" runat="server" Text='<%#Eval("ActivityType")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Active / Inactive">
                                <ItemTemplate>
                                    <cc:CustomLabel ID="lblStatus" runat="server" Text='<%#Eval("IsActive")%>'></cc:CustomLabel>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <cc:CustomLinkButton ID="lbAction" runat="server" CssClass="LinkColor" Text="Edit"></cc:CustomLinkButton>
                                        </ItemTemplate>
                                        <ItemStyle  />
                                    </asp:TemplateField>--%>
                        </Columns>
                    </cc:CustomGridView>
                </div>
               
                <div class="row Activiy">
                    <h3 class="panel image mobilefont">
                        MANAGE ACTIVITY<span runat="server" class=""></span></h3>
                    <div class="collapse table-responsive">
                      
                                <div id="divTable4" class="col-sm-8 MActivi_leftPadding">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <cc:CustomGridView ID="cgvActivity" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tablett"
                                                Width="100%" DataKeyNames="ActivityId" OnPageIndexChanging="cgvActivity_PageIndexChanging"
                                                AllowPaging="True" PageSize="5" OnRowCancelingEdit="cgvActivity_RowCancelingEdit"
                                                OnRowEditing="cgvActivity_RowEditing" OnRowUpdating="cgvActivity_RowUpdating"
                                                OnRowDataBound="cgvActivity_RowDataBound" 
                                                onpageindexchanged="cgvActivity_PageIndexChanged" >
                                                <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                                    Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                                <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                                <EmptyDataTemplate>
                                                    <div class="EmpltyGridView">
                                                        &quot; Sorry! There is no data &quot;</div>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                                        <ItemTemplate>
                                                            <%#Eval("RowNumber")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Activity Id" Visible="false">
                                                        <ItemTemplate>
                                                            <cc:CustomLabel ID="lblId" runat="server" Text='<%#Eval("ActivityId")%>'></cc:CustomLabel>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Area Type">
                                                        <ItemTemplate>
                                                            <cc:CustomLabel ID="lblAreaType" runat="server" Text='<%#Eval("Area")%>'></cc:CustomLabel>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Activity Type">
                                                        <ItemTemplate>
                                                            <cc:CustomLabel ID="lblActivityType" runat="server" Text='<%#Eval("ActivityType")%>'></cc:CustomLabel>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Activity">
                                                        <ItemTemplate>
                                                            <cc:CustomLabel ID="lblActivity" runat="server" Text='<%#Eval("ActivityName")%>'></cc:CustomLabel>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                        <EditItemTemplate>
                                                            <cc:CustomTextBox ID="txtActivity" runat="server" CssClass="form-control" Text='<%#Eval("ActivityName") %>'
                                                                TextMode="MultiLine" Rows="3"></cc:CustomTextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Active / Inactive">
                                                        <ItemTemplate>
                                                            <cc:CustomLabel ID="lblStatus" runat="server" Text='<%#Eval("IsActive")%>'></cc:CustomLabel>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <%-- <cc:CustomLinkButton ID="lbAction" runat="server" CssClass="LinkColor" Text="Edit"></cc:CustomLinkButton>--%>
                                                            <asp:Button ID="btnEditActivity" runat="server" OnClientClick="return confirm('Are you sure? you want to edit this activity!');" Text="Edit" CssClass="btn btn-primary"
                                                                CommandName="Edit" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <cc:CustomButton ID="btnUpdate" runat="server" OnClientClick="return confirm('Are you sure? you want to update this activity!');" Text="Update" CssClass="btn btn-primary ButtonMargin"
                                                                CommandName="Update" />
                                                            <cc:CustomButton ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary ButtonMargin"
                                                                CommandName="Cancel" />
                                                        </EditItemTemplate>
                                                        <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </cc:CustomGridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cgvActivity" EventName="PageIndexChanging" />
                                            <asp:AsyncPostBackTrigger ControlID="btnAddActivity" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="content2" class="col-sm-4  TopMargin MActivi_RightPadding">
                                    
                                       <%-- <a href="#" class="btn btn-labeled btn-primary btnAddMore2"><span class="Addmore"><i
                                            class="glyphicon glyphicon-plus-sign i"></i></span><b>Add More </b></a>--%>

                                            <h3 class="panel  image ">
                        Add More<span id="Span1" runat="server" class=""></span></h3>


                                    
                                    <div id="divTable5" class="collapse table-responsive divborder">
                                        <%--<div class="dropdown">
                                            <cc:CustomDropDownList CssClass="form-control controlvalid" req1="1" ID="ddlAreaForActivity" runat="server">
                                            </cc:CustomDropDownList>
                                        </div>
                                        <div class="dropdown">
                                            <cc:CustomDropDownList CssClass="form-control controlvalid" req1="1" ID="ddlActivityTypeForActivity" runat="server">
                                            </cc:CustomDropDownList>
                                        </div>--%>
                                        <div class="dropdown">
                                            <cc:CustomTextBox ID="txtActivity1" placeholder="Enter Activity" req1="1" runat="server"
                                                TextMode="MultiLine" Rows="6" class="form-control"></cc:CustomTextBox>
                                        </div>
                                        <div class="TopMargin text-center">
                                            <cc:CustomButton ID="btnAddActivity" runat="server" CssClass="btn btn-sm btn-primary btncontrol rightmargin"
                                                Text="Add" OnClick="btnAddActivity_Click" />
                                            <cc:CustomButton ID="btnActivityCancel" runat="server" CssClass="btn btn-sm btn-primary btncancel2 "
                                                Text="Cancel" OnClick="btnActivityCancel_Click" />
                                        </div>
                                    </div>
                                </div>
                    </div>
                </div>
                  <div class="row Activiy">
                <h3 class="panel image ">
                    MANAGE ACTIVITY ITEMS<span runat="server" class=""></span></h3>
                <div class="collapse table-responsive">
                    <asp:UpdatePanel ID="uppnlActivityItem" runat="server">
                    <ContentTemplate>
                    
                            <div class="form-group col-sm-12">
                                <label class="col-sm-2">
                                    Select Area</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlItemAreaSearch" runat="server" CssClass="form-control controlwidth"
                                        OnSelectedIndexChanged="ddlActivityItemSearch_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2">
                                    Select Activity Type</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlItemActivityTypeSearch" runat="server" CssClass="form-control controlwidth"
                                        OnSelectedIndexChanged="ddlActivityItemSearch_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-sm-12 bottomMargin">
                                <label class="col-sm-2">
                                    Select Activity</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlActivityItemSearch" runat="server" CssClass="form-control controlwidth"
                                        OnSelectedIndexChanged="ddlActivityItemSearch_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                          
                          
                            <div class="bottomMargin">
                            </div>
                            <div id="divTable6" class="col-sm-8 MActivi_leftPadding">
                               
                                        <cc:CustomGridView ID="cgvActivityItems" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-bordered tablett" Width="100%" DataKeyNames="ActivityItemId"
                                            OnPageIndexChanging="cgvActivityItems_PageIndexChanging" AllowPaging="True" PageSize="5"
                                            EnableSortingAndPagingCallbacks="true" OnRowCancelingEdit="cgvActivityItems_RowCancelingEdit"
                                            OnRowEditing="cgvActivityItems_RowEditing" OnRowUpdating="cgvActivityItems_RowUpdating"
                                            OnRowDataBound="cgvActivityItems_RowDataBound" onpageindexchanged="cgvActivityItems_PageIndexChanged">
                                            <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                                Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                            <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                          <%--  <EmptyDataTemplate>
                                                <div class="EmpltyGridView">
                                                    &quot; Sorry! There is no data &quot;</div>
                                            </EmptyDataTemplate>--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                                    <ItemTemplate>
                                                        <%#Eval("RowNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ActivityItemId" Visible="false">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblActivityItemId" runat="server" Text='<%#Eval("ActivityItemId")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Area Type">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblAreaType" runat="server" Text='<%#Eval("Area")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomLabel ID="lblAreaId" runat="server" Text='<%#Eval("AreaId")%>' Visible="false"></cc:CustomLabel>
                                                        <asp:DropDownList ID="ddlArea" Width="100px" CssClass="form-control dropdown" runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity Type">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblActivityType" runat="server" Text='<%#Eval("ActivityType")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomLabel ID="lblActivityTypeId" runat="server" Text='<%#Eval("ActivityTypeId")%>'
                                                            Visible="false"></cc:CustomLabel>
                                                        <asp:DropDownList ID="ddlActivityType" Width="100px" CssClass="form-control dropdown"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblActivity" runat="server" Text='<%#Eval("ActivityName")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomLabel ID="lblActivityId" runat="server" Text='<%#Eval("ActivityId")%>'
                                                            Visible="false"></cc:CustomLabel>
                                                        <asp:DropDownList ID="ddlActivity" Width="120px" CssClass="form-control dropdown"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Para No. CSS PT Guidelines">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblCSSPTParaNo" runat="server" Text='<%#Eval("ParaNoCSSPTGuidelines")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomTextBox ID="txtCSSPTParaNo" runat="server" CssClass="form-control GridTxt" Text='<%#Eval("ParaNoCSSPTGuidelines") %>'></cc:CustomTextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Items">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblActivityItem" runat="server" Text='<%#Eval("ActivityItem")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomTextBox ID="txtActivityItem" runat="server" CssClass="form-control GridTxt"
                                                            Text='<%#Eval("ActivityItem") %>' TextMode="MultiLine" Rows="6" Width="150px"></cc:CustomTextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                             <asp:TemplateField HeaderText="GPS Mandatory Status">
                                               <ItemTemplate>
                                                  <cc:CustomCheckBox ID="chkGPSActive" runat="server"  Checked='<%#Eval("GPSStatus") %>' AutoPostBack="true" 
                                                      OnCheckedChanged="chkGPSActive_CheckedChanged"></cc:CustomCheckBox>
                                               </ItemTemplate>
                                             <ItemStyle Width="100px" CssClass="GridWidth text-center" />
                                            </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Active / Inactive">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblStatus" runat="server" Text='<%#Eval("IsActive")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <cc:CustomButton ID="btnEditActivityItem" runat="server" OnClientClick="return confirm('Are you sure? you want to edit this activity item!');" CssClass="btn btn-primary"
                                                            Text="Edit" CommandName="Edit" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomButton ID="btnUpdate" runat="server" OnClientClick="return confirm('Are you sure? you want to update this activity item!');" CssClass="btn btn-primary ButtonMargin"
                                                            Text="Update" CommandName="Update" />
                                                        <cc:CustomButton ID="btnCancel" runat="server" CssClass="btn btn-primary ButtonMargin"
                                                            Text="Cancel" CommandName="Cancel" />
                                                    </EditItemTemplate>
                                                    <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                       <span class="text-danger">Sorry ! There is no Data to Display</span>
                                        </EmptyDataTemplate>
                                        </cc:CustomGridView>
                                    </ContentTemplate>
                            <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="ddlItemAreaSearch" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>

                                  <asp:AsyncPostBackTrigger ControlID="cgvActivityItems" EventName="PageIndexChanging" />
                                        <asp:AsyncPostBackTrigger ControlID="btnAddActivityItem" EventName="Click" />
                          </Triggers>
                    </asp:UpdatePanel>
                            </div>
                            <div id="content3" class="col-sm-4  TopMargin MActivi_RightPadding">
                                 <h3 class="panel image anchartag  ">
                        Add More</h3>
                                <div id="divTable7" class="collapse table-responsive ">
                                    <div class="dropdown">
                                        <cc:CustomDropDownList CssClass="form-control controlvalid1" req1="1" ID="ddlAreaForActivityItem"
                                            runat="server">
                                        </cc:CustomDropDownList>
                                    </div>
                                    <div class="dropdown">
                                        <cc:CustomDropDownList CssClass="form-control controlvalid1" req1="1" ID="ddlActivityTypeForActivityItem"
                                            runat="server">
                                        </cc:CustomDropDownList>
                                    </div>
                                    <div class="dropdown">
                                        <cc:CustomDropDownList CssClass="form-control controlvalid1" req1="1" ID="ddlActivityForActivityItem"
                                            runat="server">
                                        </cc:CustomDropDownList>
                                    </div>
                                    <div class="dropdown">
                                        <cc:CustomTextBox ID="txtCSSPTParaNo" placeholder="Enter Para No. CSS PT Guidelines"
                                            req1="1" runat="server" class="form-control controlvalid1"></cc:CustomTextBox>
                                    </div>
                                    <div class="dropdown">
                                        <cc:CustomTextBox ID="txtActivityItem" placeholder="Enter Activity Item" req1="1"
                                            runat="server" class="form-control controlvalid1" TextMode="MultiLine" Rows="6"></cc:CustomTextBox>
                                    </div>
                                     <div class="dropdown">
                                        <%-- <cc:CustomLabel ID="lblGPS" Text="GPS Required" runat="server"></cc:CustomLabel>--%>
                                       <cc:CustomCheckBox ID="chkGPS" runat="server" Text="GPS Required" AutoPostBack="true" class="form-control controlvalid1" />
                                        
                                    </div>
                                    <div class="TopMargin text-center ">
                                        <cc:CustomButton ID="btnAddActivityItem" runat="server" CssClass="btn btn-sm btn-primary rightmargin btnvalid1"
                                            Text="Add" OnClick="btnAddActivityItem_Click" />
                                        <cc:CustomButton ID="btnActivityItemCancel" runat="server" CssClass="btn btn-sm btn-primary btncancel3"
                                            Text="Cancel" OnClick="btnActivityItemCancel_Click" />
                                    </div>
                                </div>
                            </div>
                </div>
                      </div>
              
                 <h3 class="panel image ">
                    MANAGE ACTIVITY SUB ITEMS<span runat="server" class=""></span></h3>
                <div class="collapse table-responsive">
                      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                            <div class="form-group col-sm-12">
                                <label class="col-sm-2">
                                    Select Area</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlItemAreaForActivitySubItem" runat="server" CssClass="form-control controlwidth"
                                        OnSelectedIndexChanged="ddlItemAreaForActivitySubItem_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2">
                                    Select Activity Type</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlItemActivityTypeForActivitySubItem" runat="server" CssClass="form-control controlwidth"
                                        OnSelectedIndexChanged="ddlItemActivityTypeForActivitySubItem_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-sm-12 bottomMargin">
                                 <label class="col-sm-2">
                                    Select Activity</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlItemActivityForActivity" runat="server" CssClass="form-control controlwidth"
                                        OnSelectedIndexChanged="ddlItemActivityForActivity_SelectedIndexChanged" AutoPostBack="True" Enabled="false" >
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2">
                                    Select Activity Item</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlItemActivityForActivitySubItem" runat="server" CssClass="form-control controlwidth"
                                        OnSelectedIndexChanged="ddlItemActivityForActivitySubItem_SelectedIndexChanged" AutoPostBack="True" Enabled="false" >
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="bottomMargin">
                            </div>
                            <div id="divTable1" class="col-sm-8 MActivi_leftPadding table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <cc:CustomGridView ID="gvSubItems" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-bordered tablett" Width="100%" DataKeyNames="SubItemId"
                                            OnPageIndexChanging="gvSubItems_PageIndexChanging" AllowPaging="True" PageSize="5"
                                            EnableSortingAndPagingCallbacks="true" OnRowCancelingEdit="gvSubItems_RowCancelingEdit"
                                            OnRowEditing="gvSubItems_RowEditing" OnRowUpdating="gvSubItems_RowUpdating"
                                            OnRowDataBound="gvSubItems_RowDataBound" onpageindexchanged="gvSubItems_PageIndexChanged" ShowHeaderWhenEmpty="true">
                                            <PagerSettings FirstPageText="First Page" LastPageText="Last Page" PageButtonCount="4"
                                                Mode="NextPreviousFirstLast" NextPageText="Next Page" PreviousPageText="Previous Page" />
                                            <PagerStyle HorizontalAlign="Center" CssClass="pagination-ys" />
                                           <%-- <EmptyDataTemplate>
                                                <div class="">
                                                    &quot; Sorry! There is no data &quot;</div>
                                            </EmptyDataTemplate>--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="btnmobile" />
                                                    <ItemTemplate>
                                                        <%#Eval("RowNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               

                                                <%--<asp:TemplateField HeaderText="Area Type">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblSubAreaType" Width="50px" runat="server" Text='<%#Eval("Area")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomLabel ID="lblSubAreaId" runat="server" Text='<%#Eval("AreaId")%>' Visible="false"></cc:CustomLabel>
                                                        <asp:DropDownList ID="ddlSubArea" Width="100px" CssClass="form-control dropdown" runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity Type">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblSubActivityType" Width="70px" runat="server" Text='<%#Eval("ActivityType")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomLabel ID="lblSubActivityTypeId" runat="server" Text='<%#Eval("ActivityTypeId")%>'
                                                            Visible="false"></cc:CustomLabel>
                                                        <asp:DropDownList ID="ddlSubActivityType" Width="100px" CssClass="form-control dropdown"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>--%>
                                                 <asp:TemplateField HeaderText="ActivitySubItemId" Visible="false">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblSubItemId" runat="server" Text='<%#Eval("SubItemId")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activity" >
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblSubActivity" Width="120px" runat="server" Text='<%#Eval("ActivityName")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomLabel ID="lblSubActivityId" runat="server" Text='<%#Eval("ActivityId")%>'
                                                            Visible="false"></cc:CustomLabel>
                                                        <asp:DropDownList ID="ddlSubActivity" Width="120px" CssClass="form-control dropdown" Enabled="false" OnSelectedIndexChanged="ddlSubActivity_SelectedIndexChanged"
                                                          runat="server" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Para No. CSS PT Guidelines">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblSubCSSPTParaNo" Width="50px" runat="server" Text='<%#Eval("ParaNo")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomTextBox ID="txtSubCSSPTParaNo" Width="50px" runat="server" CssClass="form-control GridTxt" Text='<%#Eval("ParaNo") %>'></cc:CustomTextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Items">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblSubActivityItem" Width="150px" runat="server" Text='<%#Eval("ActivityItem")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                  <%--  <EditItemTemplate>
                                                        <cc:CustomTextBox ID="txtSubActivity" runat="server" CssClass="form-control GridTxt"
                                                            Text='<%#Eval("ActivityItem") %>' TextMode="MultiLine" Rows="6" Width="150px"></cc:CustomTextBox>
                                                    </EditItemTemplate>--%>
                                                     <EditItemTemplate>
                                                        <cc:CustomLabel ID="lblSubActivityItemId" runat="server" Text='<%#Eval("ActivityItemId")%>'
                                                            Visible="false"></cc:CustomLabel>
                                                        <asp:DropDownList ID="ddlSubItem" Width="120px" CssClass="form-control dropdown" 
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Sub Items">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblActivitySubItem" Width="150px" runat="server" Text='<%#Eval("SubItem")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomTextBox ID="txtActivitySubItem" runat="server" CssClass="form-control GridTxt"
                                                            Text='<%#Eval("SubItem") %>' TextMode="MultiLine" Rows="6" Width="150px"></cc:CustomTextBox>
                                                    </EditItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Active / Inactive">
                                                    <ItemTemplate>
                                                        <cc:CustomLabel ID="lblStatus" runat="server" Text='<%#Eval("IsActive")%>'></cc:CustomLabel>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <cc:CustomButton ID="btnEditActivityItem" runat="server" OnClientClick="return confirm('Are you sure? you want to edit this activity item!');" CssClass="btn btn-primary"
                                                            Text="Edit" CommandName="Edit" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <cc:CustomButton ID="btnUpdate" runat="server" OnClientClick="return confirm('Are you sure? you want to update this activity item!');" CssClass="btn btn-primary ButtonMargin"
                                                            Text="Update" CommandName="Update" />
                                                        <cc:CustomButton ID="btnCancel" runat="server" CssClass="btn btn-primary ButtonMargin"
                                                            Text="Cancel" CommandName="Cancel" />
                                                    </EditItemTemplate>
                                                    <ItemStyle CssClass=" btnmobile" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                             <span class="text-danger">Sorry ! There is no Data to Display</span>
                                            </EmptyDataTemplate>
                                        </cc:CustomGridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvSubItems" EventName="PageIndexChanging" />
                                        <asp:AsyncPostBackTrigger ControlID="btnAddSubItems" EventName="Click" />
                                        <%--<asp:AsyncPostBackTrigger ControlID="ddlActivityForActivity" EventName="SelectedIndexChanged" />

                                        <asp:AsyncPostBackTrigger ControlID="ddlItemAreaForActivitySubItem" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlItemActivityTypeForActivitySubItem" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlItemActivityForActivity" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlItemActivityForActivitySubItem" EventName="SelectedIndexChanged" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </ContentTemplate>
                          <Triggers>

                               <asp:AsyncPostBackTrigger ControlID="ddlItemAreaForActivitySubItem" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                               <asp:AsyncPostBackTrigger ControlID="ddlActivityForActivity" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                  <asp:AsyncPostBackTrigger ControlID="gvSubItems" EventName="PageIndexChanging" />
                                        <asp:AsyncPostBackTrigger ControlID="btnAddSubItems" EventName="Click" />

                          </Triggers>
                          </asp:UpdatePanel>
                            <div id="content1" class="col-sm-4  TopMargin MActivi_RightPadding">
                                 <h3 class="panel image anchartag  ">
                        Add More</h3>
                                <div id="divTable2" class="collapse table-responsive ">
                                    <%--<div class="dropdown">
                                        <cc:CustomDropDownList CssClass="form-control controlvalid1" req1="1" ID="ddlAreaForActivitySubItem"
                                            runat="server">
                                        </cc:CustomDropDownList>
                                    </div>
                                    <div class="dropdown">
                                        <cc:CustomDropDownList CssClass="form-control controlvalid1" req1="1" ID="ddlActivityTypeForActivitySubItem"
                                            runat="server">
                                        </cc:CustomDropDownList>
                                    </div>--%>
                                    <div class="dropdown">
                                    <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                        <cc:CustomDropDownList CssClass="form-control controlvalid1" req1="1" ID="ddlActivityForActivity"
                                            runat="server" OnSelectedIndexChanged="ddlActivityForActivity_SelectedIndexChanged" AutoPostBack="True">
                                        </cc:CustomDropDownList>
                                        <%--</ContentTemplate>
                                        <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlActivityForActivity" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                        </asp:UpdatePanel>--%>
                                    </div>
                                    <div class="dropdown">
                                        <cc:CustomDropDownList CssClass="form-control controlvalid1" req1="1" ID="ddlActivityForActivitySubItem"
                                            runat="server" >
                                        </cc:CustomDropDownList>
                                    </div>
                                    <div class="dropdown">
                                        <cc:CustomTextBox ID="txtParaNo" placeholder="Enter Para No. CSS PT Guidelines"
                                            req1="1" runat="server" class="form-control controlvalid1"></cc:CustomTextBox>
                                    </div>
                                    <div class="dropdown">
                                        <cc:CustomTextBox ID="txtSubActivityItem" placeholder="Enter Activity Sub Item" req1="1"
                                            runat="server" class="form-control controlvalid1" TextMode="MultiLine" Rows="6"></cc:CustomTextBox>
                                    </div>
                                    <div class="TopMargin text-center ">
                                        <cc:CustomButton ID="btnAddSubItems" runat="server" CssClass="btn btn-sm btn-primary rightmargin btnvalid1"
                                            Text="Add" OnClick="btnAddSubItems_Click" />
                                        <cc:CustomButton ID="btnSubCancel" runat="server" CssClass="btn btn-sm btn-primary btncancel3"
                                            Text="Cancel" OnClick="btnSubCancel_Click"/>
                                    </div>
                                </div>
                            </div>
                </div>

                     
            </div>
        </div>
        <div class="form-group text-center">
            <div class="col-sm-10 TopMargin">
                <input type="button" onclick="window.location.href = 'Home.aspx'" value="Back" class="btn btn-primary col-sm-offset-1" />
            </div>
        </div>
    
</asp:Content>
