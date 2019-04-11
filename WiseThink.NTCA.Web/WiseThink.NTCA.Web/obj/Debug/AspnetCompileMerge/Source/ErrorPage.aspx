<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="WiseThink.NTCA.ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">
         function noBack() {
             window.history.forward();
         }

         window.onpageshow = function (evt) { if (evt.persisted) noBack() }

         window.onunload = function () { void (0) }
         window.onload = noBack();
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="outer">
        <div class="page">
            <div class="header">
                <div class="title">
                    <h1>
                    </h1>
                </div>
            </div>
            <table align="center" cellpadding="2" cellspacing="2" width="50%">
                <tr>
                    <td width="20%" class="style2" valign="top">
                    </td>
                    <td class="style2">
                    </td>
                </tr>
                <tr>
                    <td width="20%" valign="top">
                        <img alt="Error" class="style1" src="Images/Error.jpg" />
                    </td>
                    <td>
                        <h2>
                            This is custom user friendly error page</h2>
                    </td>
                </tr>
                <tr>
                    <td width="20%" colspan="2" valign="top">
                        I'm afraid we are currently having problem on the site. An email has been sent
                        to the site owner to report the problem.&nbsp; Go to back page<asp:HyperLink ID="HyperLink1"
                            runat="server" NavigateUrl="~/Login.aspx">Click Here</asp:HyperLink> 
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="style3" colspan="2" valign="top">
                    </td>
                </tr>
            </table>
        </div>
        <div class="footer">
        </div>
    </div>
    </form>
</body>
</html>
