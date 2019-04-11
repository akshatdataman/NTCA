<%@ Page Title="" Language="C#" MasterPageFile="~/FieldDirector/FieldDirector.Master"
    AutoEventWireup="true" CodeBehind="ObligationFD.aspx.cs" Inherits="WiseThink.NTCA.Web.FieldDirector.ObligationFD" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            BindValidationMethod();
            stopevent(this);
        });
        function blinker() {
            $('.blinking').fadeOut(200);
            $('.blinking').fadeIn(200);
        }
        setInterval(blinker, 6000);
    </script>
    <style type="text/css">
        

    </style>
    <script type="text/javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=cgvObligationFD.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
    </script>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $('#<%=cgvObligationFD.ClientID %>').Scrollable({
                ScrollHeight: 500
            });
            
        });
    </script>--%>
    <%--<script type="text/javascript" language="javascript">

    Sys.Application.add_load(FirejQuery);

    function FirejQuery() {

        $(document).ready(function () {
            $('#<%=cgvObligationFD.ClientID %>').Scrollable({
                ScrollHeight: 500
            });

        });
    }

</script>--%>
    <%--<script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            $('#<%=cgvObligationFD.ClientID %>').Scrollable({
                ScrollHeight: 500
            });
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $('#<%=cgvObligationFD.ClientID %>').Scrollable({
                            ScrollHeight: 500
                        });
                    }

                });
            }
            else {
                $('#<%=cgvObligationFD.ClientID %>').Scrollable({
                    ScrollHeight: 500
                });
            }
        });
    </script>--%>
    <script type="text/javascript" language="javascript">
        $(function () {

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FieldDirectorPlaceHolder" runat="server">
    <div>
        <div class="row minheight">
            <h4>
                <b>FIELD DIRECTOR OBLIGATIONS</b></h4>
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
                                      
                                        <li class="BackgroundColor Current"><a href="ObligationFD.aspx">Step 4: Obligation Under Tri-MOU</a></li>
                                       
                                        <li class="BackgroundColor"><a href="CheckList.aspx?callfrom=SubmitAPO">Step 5: Check and Submit</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-10  leftBorder minHeight Home_margin" id="MainContent">
                <div class="col-sm-12 margin_bottom text-center">
                    <b class="blinking">Level of Compliance score Categories: </b><span class="text-danger blinking">
                        1. (0 - 30)</span> <span class="text-success col-sm-offset-1 blinking">2. (30 - 60)</span>
                    <span class="col-sm-offset-1 blinking" style="color: Teal">3. (60 - 100)</span>
                   
                   
                  <%-- <span class="col-sm-offset-1 blinking" style="color: Teal">Select All</span>
                     <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true"/>--%>
                </div>
                <div class="col-md-offset-9 col-md-3 text-right">
                     <asp:LinkButton ID="LbtnSelectAll" runat="server" OnClick="LbtnSelectAll_Click" CssClass="leftPadddingclass" CausesValidation="false">Select All</asp:LinkButton>
                </div>
                <div class="text-center  col-sm-12 MessagePadding">
                    <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                        <uc:ValidationMessage ID="vmError" runat="server" />
                    </div>
                    <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                        <uc:ValidationMessage ID="vmSuccess" runat="server" />
                    </div>
                </div>
                <div class=" table-responsive TopMargin ">
                    <asp:UpdatePanel ID="upnlObligation" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cc:CustomGridView ID="cgvObligationFD" runat="server" CssClass="table  col-sm-12 "
                                AutoGenerateColumns="False" DataKeyNames="ObligationId" AllowPaging="False" OnPageIndexChanging="cgvObligationFD_PageIndexChanging"
                                PageSize="5">
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
                                    <asp:TemplateField HeaderText="Description of Obligations of the Field Director">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObligationId" runat="server" Visible="false" Text='<%#Eval("ObligationId") %>'></asp:Label>
                                            <asp:Label ID="lblObligation" runat="server" Text='<%#Eval("Descriptions") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Complied /</br>Not Complied /</br>Not Applicable">
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rblCompiledOrNot" AutoPostBack="true" EnableViewState="true"
                                                runat="server" OnSelectedIndexChanged="rblCompiledOrNot_SelectedIndexChanged">
                                                <asp:ListItem Text="Complied" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Not Complied" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Not Applicable" Value="3"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("ObligationId")%>' />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblCompiledOrNot"
                                                ErrorMessage="Required Field" CssClass="REerror">
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
                    <div class="col-sm-10 text-center TopMargin bottomMargin">
                        <cc:CustomButton ID="btnSave" runat="server" Text="Save" CssClass="btn-primary btn"
                            OnClick="btnSave_Click" />
                        <cc:CustomButton ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-primary btn"
                            OnClientClick="return confirm('Are you sure? you want to submit the obligation under Tri-MOU and move to final step 5!!');"
                            OnClick="btnSubmit_Click" />
                        <cc:CustomButton ID="btnBack" CssClass="btn-primary btn " runat="server" PostBackUrl="FieldDirectorHome.aspx"
                            Text="Back" />
                    </div>
                    <div class="TopMargin">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
