<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageAllActivities.ascx.cs"
    Inherits="WiseThink.NTCA.Web.UserControls.ManageAllActivities" %>
<div class="col-md-12 table-bordered">
    <div class="form-group">
        <div class=" form-control  headingCss">
            <label id="lblNonRecurring">
                Non Recurring / Recurring / Eco Development</label>
        </div>
    </div>
    <div class="form-group row">
        <label for="lblActivityType" class="col-sm-2 control-label">
            Activity Type</label>
        <div class="col-sm-6 ">
            <cc:MultipleSelectCheckBox ID="mChkCategory" runat="server" OnSelectedIndexChanged="mChkCategory_OnSelectedIndexChanged"
                AutoPostBack="True">
            </cc:MultipleSelectCheckBox>
        </div>
        <div class="col-sm-4 " id="AddNew" runat="server">
            <asp:Button ID="btnAdd" runat="server" Text="Add More" class="btn btn-default btn-primary col-md-offset-3"
                OnClick="btnAdd_Click" />
        </div>
        <div class="col-sm-3" id="additionalActivityType" runat="server" style="margin-top:-15px" >
            <%--<div class="dropdown">
                                <cc:CustomDropDownList CssClass="form-control" ID="ddlCategory" runat="server" placeholder="Select Activity Type">
                                </cc:CustomDropDownList>
                            </div>--%>&nbsp;
            <cc:CustomTextBox ID="txtAddMore" class="form-control" runat="server" placeholder="Enter Additional Activity Type"
                Text=""></cc:CustomTextBox>
        </div>
        </div>
        <div class="form-group row text-center">
            <div class="col-md-6 TopMargin">
                <asp:Button ID="btnSubmit" runat="server" Text="Add" class="btn btn-default btn-primary col-md-offset-10"
                    OnClick="btnSubmit_Click" />
            </div>
        </div>
    
    <div class="form-group">
        <div class=" form-control  headingCss">
            <label id="lblActivities">
                List of Activities</label>
        </div>
    </div>
    <div class="form-group row">
        <label for="lblActivities" class="col-sm-2 control-label">
            Activities</label>
        <div class="col-sm-6 ">
            <cc:MultipleSelectCheckBox ID="mChkActivities" runat="server" OnSelectedIndexChanged="mChkActivities_OnSelectedIndexChanged"
                AutoPostBack="True">
            </cc:MultipleSelectCheckBox>
        </div>
        <div class="col-sm-4 ">
            <asp:Button ID="btnAddActivity" runat="server" Text="Add More" class="btn btn-default btn-primary col-md-offset-3"
                OnClick="btnAddActivity_Click" />&nbsp;
        </div>
        <div class="col-sm-3" id="additionalActivity" runat="server">
        <div class="col-sm-3"></div>
            <cc:CustomDropDownList CssClass="form-control" ID="ddlCategory" runat="server">
            </cc:CustomDropDownList>
            &nbsp;
            <cc:CustomTextBox ID="txtAddActivity" class="form-control" runat="server" placeholder="Enter Additional Activity"
                Text=""></cc:CustomTextBox>
        </div>
    </div>
    <div class="form-group row text-center">
        <div class="col-md-6 TopMargin">
            <asp:Button ID="btnSubmitActivity" runat="server" Text="Add Activity" class="btn btn-default btn-primary col-md-offset-10"
                OnClick="btnSubmitActivity_Click" />
        </div>
    </div>
    <div class="form-group">
        <div class=" form-control  headingCss">
            <label id="lblActivityItems">
                List of Activity Items</label>
        </div>
    </div>
    <div class="form-group row">
        <label for="lblActivityType" class="col-sm-2 control-label">
            Activity Items</label>
        <div class="col-sm-6 ">
            <cc:MultipleSelectCheckBox ID="mChkItems" runat="server" OnSelectedIndexChanged="mChkItems_OnSelectedIndexChanged"
                AutoPostBack="True">
            </cc:MultipleSelectCheckBox>
        </div>
        <div class="col-sm-4 ">
            <asp:Button ID="btnAddActivityItem" runat="server" Text="Add More" class="btn btn-default btn-primary col-md-offset-3"
                OnClick="btnAddActivityItem_Click" />
        </div>
        <div class="col-sm-3" id="AddActivityItem" runat="server">
            <div class="dropdown">
            <div class="col-sm-3"></div>
                <cc:CustomDropDownList CssClass="form-control" ID="ddlActivities" runat="server">
                </cc:CustomDropDownList>
            </div>
            &nbsp;
            <cc:CustomTextBox ID="txtAddActivityItem" class="form-control" runat="server" placeholder="Enter Additional Activity Items"
                Text=""></cc:CustomTextBox>
        </div>
    </div>
    <div class="form-group row text-center">
        <div class="col-md-6 TopMargin">
            <asp:Button ID="btnSubmitActivityItem" runat="server" Text="Add Activity Item" class="btn btn-default btn-primary col-md-offset-10"
                OnClick="btnSubmitActivityItem_Click" />
        </div>
    </div>
    <div class="form-group">
        <div class=" form-control  headingCss">
            <label id="Label1">
                List of Sub Activity Items</label>
        </div>
    </div>
    <div class="form-group row">
        <label for="lblSubActivityItem" class="col-sm-2 control-label">
            Sub Activity Items</label>
        <div class="col-sm-6 ">
            <cc:MultipleSelectCheckBox ID="mChkSubItems" runat="server">
            </cc:MultipleSelectCheckBox>
        </div>
        <div class="col-sm-4 ">
            <asp:Button ID="btnAddSubItem" runat="server" Text="Add More" class="btn btn-default btn-primary col-md-offset-3"
                OnClick="btnAddSubItem_Click" />
        </div>
        <div class="col-sm-3" id="AddSubItem" runat="server">
        <div class="col-sm-3"></div>
            <div class="dropdown">
                <cc:CustomDropDownList CssClass="form-control" ID="ddlActivityItems" runat="server">
                </cc:CustomDropDownList>
            </div>
            &nbsp;
            <cc:CustomTextBox ID="txtAddSubActivityItem" class="form-control" runat="server"
                placeholder="Enter Additional Sub Activity Items" Text=""></cc:CustomTextBox>
        </div>
    </div>
    <div class="form-group row text-center">
        <div class="col-md-6 TopMargin">
            <asp:Button ID="btnSubActivityItem" runat="server" Text="Add Sub Activity Item" class="btn btn-default btn-primary col-md-offset-10"
                OnClick="btnSubActivityItem_Click" />
        </div>
    </div>
    <div class="form-group row text-center">
        <div class="col-md-6 TopMargin">
            <asp:Button ID="btnback" runat="server" class="btn btn-default btn-primary" Text="Back" />
            &nbsp;
        </div>
    </div>
</div>
