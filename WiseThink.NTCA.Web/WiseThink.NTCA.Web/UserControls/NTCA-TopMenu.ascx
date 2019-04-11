<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NTCA-TopMenu.ascx.cs" Inherits="WiseThink.NTCA.Web.UserControls.NTCA_TopMenu" %>
<div class="topcontainer">
                <ul class="toplink">
                <%if (Request.Url.AbsolutePath.Contains("Admin"))

                    {%>
                   <li><a href="../Admin/NTCASuperAdminHome.aspx">Home </a></li>
                   <li><a href="../Admin/EditUser.aspx">Edit Profile </a></li>
                    <li class="active"><a href="../Admin/ChangePassword.aspx">Change Password</a></li><% } %>
                   <%if (Request.Url.AbsolutePath.Contains("ChiefWildlifeWarden"))

                    {%>
                    <li><a href="../ChiefWildlifeWarden/ChiefWildLifeWardenHome.aspx">Home </a></li>
                   <li><a href="../ChiefWildlifeWarden/ChiefWildlifeWardenEditUser.aspx">Edit Profile </a></li>
                    <li class="active"><a href="../ChiefWildlifeWarden/ChiefWildlifeWardenChangePassword.aspx">Change Password</a></li><% } %>
                   <%if (Request.Url.AbsolutePath.Contains("FieldDirector"))

                    {%>
                    <li><a href="../FieldDirector/FieldDirectorHome.aspx">Home </a></li>
                   <li><a href="../FieldDirector/FieldDirectorEditProfile.aspx">Edit Profile </a></li>
                    <li class="active"><a href="../FieldDirector/FieldDirectoreChangePassword.aspx">Change Password</a></li><% } %>
                    <%--<li><a href="../Admin/NTCASuperAdminHome.aspx">Home </a></li>
                    <li><a href="../Admin/EditUser.aspx">Edit Profile </a></li>
                    <li class="active"><a href="../Admin/ChangePassword.aspx">Change Password</a></li>--%>
                    <li><a href="../Login.aspx">Logout</a></li>
                </ul>
            </div>