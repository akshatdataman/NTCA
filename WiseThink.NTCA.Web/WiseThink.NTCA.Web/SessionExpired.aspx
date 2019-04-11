<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionExpired.aspx.cs" Inherits="WiseThink.NTCA.Web.SessionExpired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Session Expired</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class = "row text-center">
    <h1>Session Expired!!!</h1>
    <p class = "text-center">
    Your session has expired.  
    Please <a href="Login.aspx">return to the login page</a> 
    and log in again to continue accessing your account.</p>
    </div>
    </form>
</body>
</html>
