<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="WiseThink.NTCA.Web.Admin.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SuperAdminPlaceHolder" runat="server">
    <div>
        <%--<--Set DashBord -->--%>
        <div class="row minheight">
            <h4>
                <b>NTCA Super Admin</b></h4>
            <div class="">
                <%--<div class="col-sm-8 NotificaitonBorder minheight ">
                    <div id="content" class=" TopMargin ">
                        
                    </div>
                </div>--%>
                <div class="col-sm-8 NotificaitonBorder minheight  Home_margin">
                    <div class="col-sm-8 col-sm-offset-1 TopMargin ">
                        <div class="col-sm-4">
                            <cc:CustomLinkButton ID="CustomLinkButton1" runat="server" ToolTip="Manage Users">
                <cc:CustomImageButton ID="imgbtnManageUser" runat="server"  style="height:100px; width:125px"
                        ImageUrl="../Images/MangeUsers.jpg" onclick="imgbtnManageUser_Click" /></cc:CustomLinkButton><br />
                            <div class="text-center">
                                <span><b>Manage User</b></span></div>
                        </div>
                        <div class="col-sm-4 col-sm-offset-4">
                            <cc:CustomLinkButton ID="CustomLinkButton2" runat="server" ToolTip="Manage States">
                <cc:CustomImageButton ID="imgbtnManageState" runat="server" style="height:100px; width:125px" 
                        ImageUrl="../Images/ManageStates.jpg" onclick="imgbtnManageState_Click" /></cc:CustomLinkButton><br />
                            <div class="text-center">
                                <span><b>Manage State</b></span></div>
                        </div>
                    </div>
                    <div class="col-sm-8 col-sm-offset-1 TopMargin">
                        <div class="col-sm-4">
                            <cc:CustomLinkButton ID="CustomLinkButton4" runat="server" ToolTip="Manage Activities">
                <cc:CustomImageButton ID="imgbtnManageActivities" runat="server" style="height:100px; width:125px"  
                        ImageUrl="../Images/ManageActivites.jpg" onclick="imgbtnManageActivities_Click" /></cc:CustomLinkButton><br />
                            <div class="text-center">
                                <span><b>Manage Activities</b></span></div>
                        </div>
                        <div class="col-sm-4 col-sm-offset-4">
                            <cc:CustomLinkButton ID="CustomLinkButton3" runat="server" ToolTip="Manage Tiger Reserve">
                <cc:CustomImageButton ID="imgbtnManageTR"  runat="server" style="height:100px; width:125px" 
                        ImageUrl="../Images/manageTigerReserve.png" onclick="imgbtnManageTR_Click" /></cc:CustomLinkButton><br />
                            <div class="text-center">
                                <span><b>Manage Tiger Reserve</b></span></div>
                        </div>
                    </div>
                </div>
                <div class="">
                    <div class="col-sm-4">
                        <div class="panel">
                            <h3 class="leftRightPadding">
                                <b>Alerts</b>
                            </h3>
                        </div>
                        <div id="DisplayAlertDiv" runat="server" class="alert alert-warning">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
