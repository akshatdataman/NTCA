<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidationMessage.ascx.cs" Inherits="WiseThink.NTCA.UserControls.ValidationMessage" %>
<% if (!string.IsNullOrEmpty(Message)) {%>
<span class="field-validation-error" style="padding: inherit; margin: auto; font-family: Arial; font-size: medium; font-weight: bold; font-style: normal; font-variant: normal"><%= Message %></span>
<%} %>