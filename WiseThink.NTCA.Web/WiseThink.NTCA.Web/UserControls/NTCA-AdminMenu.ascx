<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NTCA-AdminMenu.ascx.cs" Inherits="WiseThink.NTCA.Web.UserControls.NTCAAdminMenu" %>
<div id="horizontalmenu">
                <ul class="mainmenu padMenu">
                    <li><a href="../Admin/NTCASuperAdminHome.aspx">Home </a></li>
                    <li><a href="../Admin/ManageUsers.aspx">Manage User</a> </li>
                    <li><a href="#">Manage Master</a>
                        <ul class="SubMenu" style="width: 100%">
                            <li><a href="../Admin/StateMaster.aspx" style="width: 82px;">State<br />
                                Master</a></li>
                            <li><a href="../Admin/TigerReserve.aspx" style="width: 82px;">Tiger Reserve</a></li>
                            <li><a href="../Admin/ManageHead.aspx" style="width: 82px;">Manage Head</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Manage Applications</a>
                        <ul class="SubMenu">
                            <li><a href="#">Re-Validation Application</a></li>
                            <li><a href="#">Adjustment Application</a></li>
                            <li><a href="#">Spill Over Application</a></li>
                        </ul>
                    </li>
                    <li><a href="#">Manage Documents</a>
                        <ul class="SubMenu">
                            <li><a href="../Admin/UtilizationCertificate.aspx">Utilization<br />
                                Certificate (UC)</a></li>
                            <li><a href="../Admin/TriPartyAgreement.aspx">Tri-Party Agreement (TPA)</a></li>
                        </ul>
                    </li>
                    <li><a href="../Admin/NTCASuperAdminNotification.htm">NOTIFICATIONS</a></li>
                    <li><a href="#">Reports</a></li>
                </ul>
            </div>