<%@ Page Title="" Language="C#" MasterPageFile="~/NTCA-RO/NTCA-RO.master" AutoEventWireup="true"
    CodeBehind="SendAlert.aspx.cs" Inherits="WiseThink.NTCA.Web.NTCA_RO.SendAlert" %>

<%@ Register Src="~/UserControls/ValidationMessage.ascx" TagPrefix="uc" TagName="ValidationMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            validation();
        });
    </script>
    <script type="text/javascript">
        var atLeast = 1
        function Validate() {
            var CHK = document.getElementById("<%=mChkName.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var counter = 0;
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    counter++;
                }
            }
            if (atLeast > counter) {
                alert("Please select atleast " + atLeast + " item(s) from the name list.");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NTCAPlaceHolder" runat="server">
    <div class="row">
        <h4>
            <b>Send Alert</b></h4>
        <div class="text-center col-sm-offset-1 col-sm-10 TopMargin MessagePadding">
            <div id="DisplayErrorMessage" class="btn-danger  " runat="server">
                <uc:ValidationMessage ID="vmError" runat="server" />
            </div>
            <div id="DisplaySuccessMessage" class="btn-success" runat="server">
                <uc:ValidationMessage ID="vmSuccess" runat="server" />
            </div>
        </div>
        <div class="sendAlertMinheigt" id="divSendNotification">
            <div class="form-group col-sm-12">
                <label class="col-sm-4">
                    Select Name :<b class="REerror"> *</b></label>
                <div class="col-sm-6" style="overflow: auto">
                    <cc:MultipleSelectCheckBox ID="mChkName" runat="server" Height="200px" class="form-control">
                    </cc:MultipleSelectCheckBox>
                </div>
            </div>
            <div class="form-group col-sm-12">
                <label class="col-sm-4">
                    Subject :<b class="REerror"> *</b></label>
                <div class="col-sm-6">
                    <cc:CustomTextBox ID="txtSubject" runat="server" req="1" class="form-control" placeHolder="Subject"></cc:CustomTextBox>
                </div>
            </div>
            <div class="form-group col-sm-12">
                <label class="col-sm-4">
                    Comments :<b class="REerror"> *</b></label>
                <div class="col-sm-8">
                    <cc:CustomTextBox ID="txtComments" TextMode="MultiLine" Rows="4" Columns="50" runat="server"
                        req="1" class="form-control" placeHolder="Comments"></cc:CustomTextBox>
                </div>
            </div>
            <div class="col-sm-offset-5 col-sm-2 marginButton">
                <cc:CustomButton ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary"
                    OnClientClick="return Validate()" OnClick="btnSend_Click" />

                     <button id="Button1" type="button" onclick="window.location.href = 'Alerts.aspx'"
                    class="btn btn-primary col-sm-offset-1">
                    Back</button>
            </div>
            <div class="col-sm-2 marginButton bottomMargin">
                <%--      <cc:CustomButton ID="btnCancel" runat="server" Text="Back" 
                    CssClass="btn btn-primary" onclick="btnCancel_Click" />--%>
               
            </div>
            <br />
        </div>
    </div>
</asp:Content>
