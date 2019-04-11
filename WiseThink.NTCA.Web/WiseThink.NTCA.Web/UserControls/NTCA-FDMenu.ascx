<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NTCA-FDMenu.ascx.cs" Inherits="WiseThink.NTCA.Web.UserControls.NTCA_FDMenu" %>
<div id="horizontalmenu">
                <ul class="mainmenu padMenu">
                    <li><a href="../FieldDirector/FieldDirectorHome.aspx">Home</a></li>
                    
                    <li style="width:147px;"><a href="#">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Manage APO</a>
                    <ul class="SubMenu">
                            <li><a href="#" style="width: 95px;">Submit APO</a></li>
                            <li><a href="#" style="width: 95px;">APO Due For Submission</a></li>                            
                        </ul>
                    </li>
                    <li><a href="#">&nbsp;List of Application&nbsp;</a>
                        <ul class="SubMenu">
                            <li><a href="#" class="ApplicationSubMenuWidth">Re-validation Application</a></li>
                            <li><a href="#" class="ApplicationSubMenuWidth">Adjustment Application</a></li>
                            <li><a href="#" class="ApplicationSubMenuWidth">Spill Over Application</a></li>
                        </ul>
                    </li>
                    <li class="active"><a href="../FieldDirector/Download.aspx">Download</a>
                        </li>
                    
                    <li><a href="../FieldDirector/FieldDirectorNotification.aspx">NOTIFICATIONS</a></li>
                    <li><a href="#">Reports</a></li>
                </ul>
            </div>