<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilterReport.ascx.cs"
    Inherits="WiseThink.NTCA.UserControls.FilterReport" %>
<script type="text/javascript">

    function BindDates() {
        DatePicker('<%=txtAssignFrom.ClientID %>');
        DatePicker('<%=txtAssignTo.ClientID %>');
        DatePicker('<%=txtEvaluationFrom.ClientID %>');
        DatePicker('<%=txtEvaluationTo.ClientID %>');
        DatePicker('<%=txtValidUpDateStart.ClientID %>');
        DatePicker('<%=txtValidUpDateEnd.ClientID %>');
        DatePicker('<%=txtRecognitionFrom.ClientID %>');
        DatePicker('<%=txtRecognitionTo.ClientID %>');
        DatePicker('<%=txtApplyDateFrom.ClientID %>');
        DatePicker('<%=txtApplyDateTo.ClientID %>');
    }
    $(document).ready(function () {
        BindDates();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
        function EndRequest(sender, args) {
            BindDates();
        }
    })   
</script>
<style type="text/css">
    .test tr
    {
        background-color: #DADADA;
    }
    .check input
    {
        float: left !important;
    }
    .check label
    {
        float: left !important;
        font-size: 12px !important;
        color: #666 !important;
        width: 90% !important;
    }
    
    .ChkBoxList tr td label
    {
        margin-left: 10px !important;
        width: 150px !important;
    }
    .linkBtn1
    {
        width: 100%;
        font-size: 14px;
        color: #000000;
        font-weight: 200; /* background-color: #FFCC66; */
        padding-top: 2px;
        padding-bottom: 2px;
        padding-left: 5px;
        font-family: "Helvetica Neue" , "Lucida Grande" , "Segoe UI" , Arial, Helvetica, Verdana, sans-serif;
    }
    .linkBtn1 td
    {
        padding: 0px !important;
        padding-bottom: 3px !important;
    }
</style>
<div class="row">
    <table cellpadding="3" cellspacing="3" width="100%">
        <tr class="linkBtn1" id="trRecognition" runat="server">
            <td>
                Recognition Date From
            </td>
            <td>
                <asp:TextBox ID="txtRecognitionFrom" runat="server" CssClass="add_cal"></asp:TextBox>
            </td>
            <td>
                To
            </td>
            <td>
                <asp:TextBox ID="txtRecognitionTo" runat="server" CssClass="add_cal"></asp:TextBox>
            </td>
        </tr>
        <tr class="linkBtn1" id="trAssign" runat="server">
            <td>
                Assign Date From
            </td>
            <td>
                <asp:TextBox ID="txtAssignFrom" runat="server" CssClass="add_cal"></asp:TextBox>
            </td>
            <td>
                To
            </td>
            <td>
                <asp:TextBox ID="txtAssignTo" runat="server" CssClass="add_cal"></asp:TextBox>
            </td>
        </tr>
        <tr class="linkBtn1" id="trEvaluation" runat="server">
            <td>
                Evaluation Date From
            </td>
            <td>
                <asp:TextBox ID="txtEvaluationFrom" runat="server" CssClass="add_cal"></asp:TextBox>
            </td>
            <td>
                To
            </td>
            <td>
                <asp:TextBox ID="txtEvaluationTo" runat="server" CssClass="add_cal"></asp:TextBox>
            </td>
        </tr>
        <tr class="linkBtn1" id="trApply" runat="server">
            <td>
                Apply Date From
            </td>
            <td>
                <asp:TextBox ID="txtApplyDateFrom" runat="server" CssClass="add_cal"></asp:TextBox>
            </td>
            <td>
                To
            </td>
            <td>
                <asp:TextBox ID="txtApplyDateTo" runat="server" CssClass="add_cal"></asp:TextBox>
            </td>
        </tr>
        <tr class="linkBtn1" id="trValidUpto" runat="server">
            <td>
                Valid Upto Date From
            </td>
            <td>
                <asp:TextBox ID="txtValidUpDateStart" runat="server" CssClass="add_cal"></asp:TextBox>
            </td>
            <td>
                To
            </td>
            <td>
                <asp:TextBox ID="txtValidUpDateEnd" runat="server" CssClass="add_cal"></asp:TextBox>
            </td>
        </tr>
        <tr class="linkBtn1" id="trYear" runat="server">
            <td>
                By Year Of Establishment
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlYear" runat="server" Width="155px" Height="25px" Style="background-color: white;
                    border: 1px solid #C0ABAB;" AutoPostBack="false">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</div>
<div runat="server" id="fieldSet_div">
    <fieldset>
        <legend>Interlinked Fields:</legend>
        <table cellpadding="3" cellspacing="3" width="100%">
            <tr class="linkBtn1">
                <td class="TopCss">
                    State
                </td>
                <td>
                    <div style="" class="test filterStyle ScrollCSS">
                        <cc:MultipleSelectCheckBox ID="mChkState" runat="server" CssClass="linkBtn1 check"
                            OnSelectedIndexChanged="mChkState_SelectedIndexChanged" AutoPostBack="True">
                        </cc:MultipleSelectCheckBox>
                    </div>
                </td>
                <td class="TopCss">
                    City
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="" class="test filterStyle ScrollCSS">
                                <cc:MultipleSelectCheckBox ID="mChkCity" runat="server" CssClass="linkBtn1 check"
                                    OnSelectedIndexChanged="mChkCity_OnSelectedIndexChanged" AutoPostBack="True">
                                </cc:MultipleSelectCheckBox>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="mChkState" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr class="linkBtn1">
                <td class="TopCss ">
                    Category
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="" class="test filterStyle ScrollCSS  ">
                                <cc:MultipleSelectCheckBox ID="mChkCategory" runat="server" CssClass="linkBtn1 check"
                                    OnSelectedIndexChanged="mChkCategory_OnSelectedIndexChanged" AutoPostBack="True">
                                </cc:MultipleSelectCheckBox>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="mChkCity" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="mChkState" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="TopCss">
                    Zoo Name
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="" class="test filterStyle ScrollCSS ">
                                <cc:MultipleSelectCheckBox ID="mChkZoo" runat="server" CssClass="linkBtn1 check">
                                </cc:MultipleSelectCheckBox>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="mChkCategory" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="mChkCity" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="mChkState" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
