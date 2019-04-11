<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WiseThink.NTCA.Web.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery1.10.2.js" type="text/javascript"></script>
    <script src="Scripts/knockout-3.2.0.debug.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            function WebmailViewModel() {
    // Data
    var self = this;
    self.folders = ['Inbox', 'Archive', 'Sent', 'Spam'];
    self.chosenFolderId = ko.observable();
     self.chosenFolderData = ko.observable();
     self.chosenMailData = ko.observable();
     // Behaviours
    self.goToFolder = function(folder) { self.chosenFolderId(folder);
        self.chosenMailData(null); 
        $.get('/mail', { folder: folder }, self.chosenFolderData); 
   };
    self.goToMail = function(mail) { 
        self.chosenFolderId(mail.folder);
        self.chosenFolderData(null); // Stop showing a folder
        $.get("/mail", { mailId: mail.id }, self.chosenMailData);
    };
   self.goToFolder('Inbox');
};

ko.applyBindings(new WebmailViewModel());
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ul data-bind="foreach: folders">
    <li data-bind="text: $data"></li>
</ul>
    </form>
</body>
</html>
