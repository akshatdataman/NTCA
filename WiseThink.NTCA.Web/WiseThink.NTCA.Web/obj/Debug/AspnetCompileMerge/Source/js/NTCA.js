/*This functin is used to manage the Manager activity*/
function ManageActivity() {
    $("#divOpen2").addClass("hide1");
    $("#divOpen3").addClass("hide1");

    $(".btnAddMore2").click(function () {
        $("#divOpen2").removeClass("hide1");
        $("#divOpen2").addClass("show1");
        $(".btnAddMore2").parent().addClass("hide1");
    });
    $(".btncancel2").click(function () {
        $("#divOpen2").removeClass("show1");
        $("#divOpen2").addClass("hide1");
        $(".btnAddMore2").parent().removeClass("hide1");
    });

    $(".btnAddMore3").click(function () {
        $("#divOpen3").removeClass("hide1");
        $("#divOpen3").addClass("show1");
        $(".btnAddMore3").parent().addClass("hide1");
    });
    $(".btncancel3").click(function () {
        $("#divOpen3").removeClass("show1");
        $("#divOpen3").addClass("hide1");
        $(".btnAddMore3").parent().removeClass("hide1");
        //$(".btnAddMore2").show1();

    });
    $(".btnAddMore3").click(function () {
        $("#divOpen2").hide1();
        $("#divOpen3").show1();
        $(".btnAddMore3").hide1();

    });
    $(".btncancel3").click(function () {
        $("#divOpen3").hide1();
        $(".btnAddMore3").show1();
    });
}

/*Datepicker*/
var fl;
function DatePicker() {
    $(".cal").datepicker({
        //comment the beforeShow handler if you want to see the ugly overlay
        beforeShow: function () {
            setTimeout(function () {
                $('.ui-datepicker').css('z-index', 99999999999999);
            }, 0);
        }
    });
}

/*Load event of the page*/
$(function () {
    DatePicker();

});

function ManageActivity_Js() {
    ManageActivity();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                ManageActivity();
            }
        });
    }
}

/*This jquery is used to manage the view obligation */
function ViewObligation() {
    var i = 1;
    $(".Radiobuttonlist").each(function () {
        $(this).children().children().children().children().first().addClass("Complie" + i);
        $(this).children().children().next().children().children().first().addClass("NotComplie" + i);
        $(this).children().children().next().next().children().children().first().addClass("NotApplicable" + i);
        $(this).parent().next().children().first().addClass("Complie" + i);
        $(this).parent().next().next().children().first().addClass("NotComplie" + i);
        i++;
    });

    $("input[type='radio']").click(function () {
        var rdoclass = $(this).attr("class");
        var number = rdoclass.substr(-1);
        if (rdoclass == "Complie" + number) {
            $("textarea." + rdoclass).removeAttr("disabled");
            $("textarea.NotComplie" + number).attr("disabled", "disabled");
        }
        else if (rdoclass == "NotComplie" + number) {
            $("textarea." + rdoclass).removeAttr("disabled");
            $("textarea.Complie" + number).attr("disabled", "disabled");
        }
        else if (rdoclass == "NotApplicable" + number) {
            $("textarea.NotComplie" + number).attr("disabled", "disabled");
            $("textarea.Complie" + number).attr("disabled", "disabled");
        }
        else {
        }


    });
}

function viewObligation1() {
    ViewObligation();
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                ViewObligation();
            }
        });
    }
}

/*manage session in sumit APO*/

function rightshowhide() {
    $("a.anchorlink").click(function () {
        $(".Links").each(function () {
            $(this).removeClass("Current");
        });
        $(this).parent().addClass("Current");
        var id = $(this).attr("href");
        $(id).removeAttr("style");
        $(".TopDiv").each(function () {
            $(this).hide();
        });
        $(id).show();
        $(".tab-pane").each(function () {
            $(this).removeClass("in");
            $(this).removeClass("active");
        });
        if (id == "#RecuringDiv") {
            $("#core").addClass("in");
            $("#core").addClass("active");
            $(".Rcore").addClass("active");
        }
        else if (id == "#Non-RecurringDiv") {
            $("#core1").addClass("in");
            $("#core1").addClass("active");
            $(".NRcore").addClass("active");
        }
        else {
        }
    });
}

function SaveAsDraft() {
    $.ajax({
        type: "POST",
        url: "SubmitAPONew.aspx/SaveAsDraftData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //data: "{'apo': '" + str + "'}",
        success: function (response) {
            //location.reload(true);
            if (response) {
                location.reload(true);
                //alert("Data saved in Draft successfully...!");
            }
            else {
                //alert("You can not change the status of this APO, since action already has been taken by higher authority.");
                location.reload(true);
                console.log("Data saved successfully..!!");
            }
            //alert("Pop up added successfully..!!");
            //  location.reload();
        },
        error: function (error) {

            console.log(error);

        }
    });
}

window.onbeforeunload = function (event) {
    $.blockUI({ message: 'Please wait...' });
    setInterval(3000);
};

$(window).load(function () {
    $.unblockUI();
    //window.onbeforeunload = $(window).data('beforeunload'); 
});

$(document).load(function () {
    $.blockUI({ message: 'Please wait...' });
});

function RemoveBlockUi() {
    window.onbeforeunload = null;
    window.onbeforeunload = $(window).load('beforeunload');
}

function AddBlockUi() {
    //window.onbeforeunload = null;
    window.onbeforeunload = $(window).load('beforeunload');
}

function closeLoading() {
    $.unblockUI();
}

function downloadFile(htmlObj) {
    var id = htmlObj.id;
    var text = document.getElementById(id).innerText
    window.location = '../downloadFileHandler.ashx?fileName=' + text;
}

/*This function is used to save data in db in submit apo*/
var setActionTab = function (Area_Id, ActivityType_Id) {
    if ((Area_Id == 1) && (ActivityType_Id == 1)) {
        /*Non-Recuring Core*/
        sessionStorage.setItem("tableft", "NonRecuring");
        sessionStorage.setItem("tabmain", "Core");

    }
    else if ((Area_Id == 2) && (ActivityType_Id == 1)) {
        /*Non-Recuring Buffer*/
        sessionStorage.setItem("tableft", "NonRecuring");
        sessionStorage.setItem("tabmain", "Buffer");

    }
    else if ((Area_Id == 1) && (ActivityType_Id == 2)) {
        /*Core recuring*/
        sessionStorage.setItem("tableft", "Recuring");
        sessionStorage.setItem("tabmain", "Core");


    }
    else if ((Area_Id == 2) && (ActivityType_Id == 2)) {
        /*Buffer recuring*/
        sessionStorage.setItem("tableft", "Recuring");
        sessionStorage.setItem("tabmain", "Buffer");

    }
    var getleft = sessionStorage.getItem("tableft");
    var getmain = sessionStorage.getItem("tabmain");

};

var setActiveTab_PageLoad = function () {
    var getleft = sessionStorage.getItem("tableft");
    var getmain = sessionStorage.getItem("tabmain");

    if (getleft != null || getleft != undefined) {
        if (getmain != null || getmain != undefined) {
            $(".Links").each(function () {
                $(this).removeClass("Current");
            });
            $(".tab-pane").each(function () {
                $(this).removeClass("in");
                $(this).removeClass("active");
            });
            $(".TopDiv").each(function () {
                $(this).hide();
            });
            if (getleft == "Recuring" && getmain == "Core") {
                $(".Recuring_left").addClass("Current");

                //$("div#RecuringDiv").show();
                $("#RecuringDiv").removeAttr("style");

                //$("#1234AV").attr("display", "table-row");
                $("#core").addClass("in");
                $("#core").addClass("active");
                $(".a").each(function () {
                    $(this).removeClass("active");
                });
                $(".Rcore").addClass("active");

            }
            else if (getleft == "Recuring" && getmain == "Buffer") {
                $(".Recuring_left").addClass("Current");

                // $("div#RecuringDiv").show();
                $("#RecuringDiv").removeAttr("style");
                $("#RecuringDiv").attr("himanshu", "jain");
                $("#Buffer").addClass("in");
                $("#Buffer").addClass("active");
                $(".a").each(function () {
                    $(this).removeClass("active");
                });
                $(".Rbuffer").addClass("active");
            }

            else if (getleft == "NonRecuring" && getmain == "Buffer") {
                $(".NonRecuring_left").addClass("Current");

                // $("div#RecuringDiv").show();
                $("#Non-RecurringDiv").removeAttr("style");
                $("#Buffer1").addClass("in");
                $("#Buffer1").addClass("active");
                $(".a").each(function () {
                    $(this).removeClass("active");
                });
                $(".NRbuffer").addClass("active");
            }
            else if (getleft == "NonRecuring" && getmain == "Core") {
                $(".NonRecuring_left").addClass("Current");

                // $("div#RecuringDiv").show();
                $("#Non-RecurringDiv").removeAttr("style");
                $("#core1").addClass("in");
                $("#core1").addClass("active");
                $(".a").each(function () {
                    $(this).removeClass("active");
                });
                $(".NRcore").addClass("active");
            }
            else if (getleft == "NonRecuring") {
                $(".NonRecuring_left").addClass("Current");

                $("div#Non-RecurringDiv").show();
            }
            else {
            }
            //$.session.set("LeftTab", "");
            //$.session.set("mainTab", "");

        }

    }
    else {
        $(".NonRecuring_left").addClass("Current");
        $("#Non-RecurringDiv").removeAttr("style");
        $("#Non-RecurringDiv").show();
        $("#core1").addClass("in");
        $("#core1").addClass("active");
        $(".NRcore").addClass("active");
        $.session.set("LeftTab", "");
        $.session.set("mainTab", "");

    }

};

function AddAPOSubItem(lnk, grid_view, Area_Id, ActivityType_Id, CallType) {

    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var Gridview = grid_view;/*"gvRCore";*/
    var AreaId = Area_Id;/*1";*/
    var ActivityTypeId = ActivityType_Id;/*"2";*/
    var controls = row.getElementsByTagName("*");
    var activityId, item, subitemId, ActivityItemId, ParaNo, Subitem, TCP, NameSubitem, unitprice, NO_Of_Item, Total, Justification, filePath, fileName, GPS;
    //Loop through the fetched controls.
    for (var i = 0; i < controls.length; i++) {
        //Find the TextBox control.
        if (controls[i].id.indexOf("lblActivityId") != -1) {
            activityId = controls[i].innerText;
        }
        if (controls[i].id.indexOf("lblActivityItemId") != -1) {
            ActivityItemId = controls[i].innerText;
        }
        if (controls[i].id.indexOf("txtParaNo") != -1) {
            ParaNo = controls[i].value;
        }
        //Find the DropDownList control.
        if (controls[i].id.indexOf("ddlSubItem") != -1) {
            subitemId = controls[i].value;
        }
        if (controls[i].id.indexOf("txtSubItemName") != -1) {
            Subitem = controls[i].value;
        }
        if (controls[i].id.indexOf("txtNumberOfItem") != -1) {
            NO_Of_Item = controls[i].value;
        }
        if (controls[i].id.indexOf("txtUnitPrice") != -1) {
            unitprice = controls[i].value;
        }
        if (controls[i].id.indexOf("txtTotal") != -1) {
            Total = controls[i].value;
        }
        if (controls[i].id.indexOf("txtJustification") != -1) {
            Justification = controls[i].value;
        }
        if (controls[i].id.indexOf("fuUploadDocument") != -1) {
            filePath = controls[i].value;
            var lastslashindex = filePath.split('\\');
            fileName = lastslashindex[lastslashindex.length - 1];
            fileUpload = controls[i];
        }
    }
    var obj = {
        "AreaId": AreaId,
        "ActivityTypeId": ActivityTypeId,
        "ActivityId": activityId,
        "ActivityItemId": ActivityItemId,
        "SubItemId": subitemId,
        "SubItem": Subitem,
        "ParaNoTCP": ParaNo,
        "NumberOfItems": NO_Of_Item,
        "UnitPrice": unitprice,
        "Total": Total,
        "Justification": Justification,
        "Document": fileName
    };

    var str = JSON.stringify(obj);

    if (CallType == "EditAPO") {
        if (rowIndex > 0) {
            $.ajax({
                type: "POST",
                url: "EditAPOCopy.aspx/InsertSubItem",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'apo': '" + str + "'}",
                success: function (response) {
                    location.reload(true);
                    sendFile(fl);
                    //  location.reload();
                },
                error: function (error) {

                    console.log(error);
                }
            });
        }
    }
    else if (CallType == "AdditionalAPO") {
        if (rowIndex > 0) {
            $.ajax({
                type: "POST",
                url: "AdditionalAPO.aspx/InsertSubItem",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'apo': '" + str + "'}",
                success: function (response) {
                    location.reload(true);
                    console.log("Data saved successfully..!!");
                    alert("Sub-item added successfully..!!");
                    //  location.reload();
                },
                error: function (error) {

                    console.log(error);

                }
            });
        }
    }
    else {
        if (rowIndex > 0) {
            $.ajax({
                type: "POST",
                url: "SubmitAPONew.aspx/InsertSubItem",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'apo': '" + str + "'}",
                success: function (response) {
                    location.reload(true);
                    console.log("Data saved successfully..!!");
                    alert("Sub-item added successfully..!!");
                    //  location.reload();
                },
                error: function (error) {

                    console.log(error);

                }
            });
        }
    }
    setActionTab(Area_Id, ActivityType_Id);
}

function sendFile(file) {
   
    var formData = new FormData();
    formData.append('file', file);

    $.ajax({
        type: 'post',
        url: '../uploader.ashx',
        data: formData,
        success: function (status) {
           
        },
        processData: false,
        contentType: false,
        error: function () {
            alert("Whoops something went wrong!");
        }
    });
}

function previewFile(ele) {
    var file;
   var fl = ele.files[0];
   
   sendFile(fl);
}

function DeleteAPOSubItems(lnk, grid_view, Area_Id, ActivityType_Id, CallType) {

    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var Gridview = grid_view;/*"gvRCore";*/
    var AreaId = Area_Id;/*1";*/
    var ActivityTypeId = ActivityType_Id;/*"2";*/
    var controls = row.getElementsByTagName("*");
    var activityId, item, subitemId, ActivityItemId, ParaNo, Subitem, TCP, NameSubitem, unitprice, NO_Of_Item, Total, Justification, filePath, fileName, GPS;
    //Loop through the fetched controls.
    for (var i = 0; i < controls.length; i++) {
        //Find the TextBox control.
        if (controls[i].id.indexOf("lblActivityId") != -1) {
            activityId = controls[i].innerText;
        }
        if (controls[i].id.indexOf("lblActivityItemId") != -1) {
            ActivityItemId = controls[i].innerText;
        }
        if (controls[i].id.indexOf("txtParaNo") != -1) {
            ParaNo = controls[i].value;
        }
        //Find the DropDownList control.
        if (controls[i].id.indexOf("ddlSubItem") != -1) {
            subitemId = controls[i].value;
        }
        if (controls[i].id.indexOf("txtSubItemName") != -1) {
            Subitem = controls[i].value;
        }
        if (controls[i].id.indexOf("txtNumberOfItem") != -1) {
            NO_Of_Item = 0;
        }
        if (controls[i].id.indexOf("txtUnitPrice") != -1) {
            unitprice = 0;
        }
        if (controls[i].id.indexOf("txtTotal") != -1) {
            Total = 0;
        }
        if (controls[i].id.indexOf("txtJustification") != -1) {
            Justification = controls[i].value;
        }
        if (controls[i].id.indexOf("fuUploadDocument") != -1) {
            filePath = controls[i].value;
            var lastslashindex = filePath.split('\\');
            //var ss = lastslashindex.last();
            fileName = lastslashindex[lastslashindex.length - 1];
            fileUpload = controls[i];
            //alert(fileName);
        }
    }
    var obj = {
        "AreaId": AreaId,
        "ActivityTypeId": ActivityTypeId,
        "ActivityId": activityId,
        "ActivityItemId": ActivityItemId,
        "SubItemId": subitemId,
        "SubItem": Subitem,
        "ParaNoTCP": ParaNo,
        "NumberOfItems": NO_Of_Item,
        "UnitPrice": unitprice,
        "Total": Total,
        "Justification": Justification,
        "Document": fileName
    };

    var str = JSON.stringify(obj);
    if (CallType == "EditAPO") {
        if (rowIndex > 0) {
            if (confirm("Are you sure? you want to clear this entry") == true) {
                $.ajax({
                    type: "POST",
                    url: "EditAPOCopy.aspx/DeleteAPOEntries",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{'apo': '" + str + "'}",
                    success: function (response) {
                        //location.reload(true);
                        if (response) {
                            setTimeout(function () {
                                console.log("File Downloaded successfully..!!");
                                alert("Entries deleted successfully..!!");
                                location.reload();
                            }, 2000);
                        }
                        else {
                            alert("Entries could not be deleted try again later..!!");
                            location.reload();
                        }
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            } else {
                return false;
            }
        }
    }
    else if (CallType == "AdditionalAPO") {
        if (rowIndex > 0) {
            if (confirm("Are you sure? you want to clear this entry") == true) {
                $.ajax({
                    type: "POST",
                    url: "AdditionalAPO.aspx/DeleteAPOEntries",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{'apo': '" + str + "'}",
                    success: function (response) {
                        //location.reload(true);
                        if (response) {
                            setTimeout(function () {
                                console.log("File Downloaded successfully..!!");
                                alert("Entries deleted successfully..!!");
                                location.reload();
                            }, 2000);
                        }
                        else {
                            alert("Entries could not be deleted try again later..!!");
                            location.reload();
                        }
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            } else {
                return false;
            }
        }
    }
    else {
        if (rowIndex > 0) {
            if (confirm("Are you sure? you want to clear this entry") == true) {
                $.ajax({
                    type: "POST",
                    url: "SubmitAPONew.aspx/DeleteAPOEntries",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{'apo': '" + str + "'}",
                    success: function (response) {
                        //location.reload(true);
                        if (response) {
                            setTimeout(function () {
                                console.log("File Downloaded successfully..!!");
                                alert("Entries deleted successfully..!!");
                                location.reload();
                            }, 2000);
                        }
                        else {
                            alert("Entries could not be deleted try again later..!!");
                            location.reload();
                        }
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            } else {
                return false;
            }
        }
    }
    setActionTab(Area_Id, ActivityType_Id);
}

function openDialogue(lnk, grid_view, Area_Id, ActivityType_Id, CallType) {
    var row = lnk.parentNode.parentNode;
    var rowIndex = row.rowIndex - 1;
    var Gridview = grid_view;/*"gvRCore";*/
    var AreaId = Area_Id;/*1";*/
    var ActivityTypeId = ActivityType_Id;/*"2";*/
    var controls = row.getElementsByTagName("*");
    var rowIndex, activityId, item, subitemId, ActivityItemId, ParaNo, Subitem, TCP, NameSubitem, unitprice, NO_Of_Item, Total, Justification, filePath, fileName, GPS;
    //Loop through the fetched controls.
    for (var i = 0; i < controls.length; i++) {
        //Find the TextBox control.
        if (controls[i].id.indexOf("lblActivityId") != -1) {
            activityId = controls[i].innerText;
        }
        if (controls[i].id.indexOf("lblActivityItemId") != -1) {
            ActivityItemId = controls[i].innerText;
        }
        if (controls[i].id.indexOf("txtParaNo") != -1) {
            ParaNo = controls[i].value;
        }
        //Find the DropDownList control.
        if (controls[i].id.indexOf("ddlSubItem") != -1) {
            subitemId = controls[i].value;
        }
        if (controls[i].id.indexOf("txtSubItemName") != -1) {
            Subitem = controls[i].value;
        }
        if (controls[i].id.indexOf("txtNumberOfItem") != -1) {
            NO_Of_Item = 0; //controls[i].value;
        }
        if (controls[i].id.indexOf("txtUnitPrice") != -1) {
            unitprice = 0;//controls[i].value;
        }
        if (controls[i].id.indexOf("txtTotal") != -1) {
            Total = 0;// controls[i].value;
        }
        if (controls[i].id.indexOf("txtJustification") != -1) {
            Justification = controls[i].value;
        }
        if (controls[i].id.indexOf("clbDocumentFile") != -1) {
            //filePath = controls[i].value;
            //var lastslashindex = filePath.split('\\');
            //var ss = lastslashindex.last();
            fileName = controls[i].innerText; //lastslashindex[lastslashindex.length - 1];
            //fileUpload = controls[i];
            //alert(fileName);

        }
    }
    var obj = {
        "rowIndex": rowIndex,
        "AreaId": AreaId,
        "ActivityTypeId": ActivityTypeId,
        "ActivityId": activityId,
        "ActivityItemId": ActivityItemId,
        "SubItemId": subitemId,
        "SubItem": Subitem,
        "ParaNoTCP": ParaNo,
        "NumberOfItems": NO_Of_Item,
        "UnitPrice": unitprice,
        "Total": Total,
        "Justification": Justification,
        "Document": fileName
    };

    var str = JSON.stringify(obj);
    if (CallType == "EditAPO") {
        if (rowIndex > 0) {
            $.ajax({
                type: "POST",
                url: "EditAPOCopy.aspx/SetValueinSession",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'apo': '" + str + "'}",
                success: function (response) {
                    $("#gpsModal").modal("show");
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    }
    else if (CallType == "AdditionalAPO") {
        if (rowIndex > 0) {
            $.ajax({
                type: "POST",
                url: "AdditionalAPO.aspx/SetValueinSession",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'apo': '" + str + "'}",
                success: function (response) {
                    $("#gpsModal").modal("show");
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    }
    else {
        if (rowIndex > 0) {
            $.ajax({
                type: "POST",
                url: "SubmitAPONew.aspx/SetValueinSession",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'apo': '" + str + "'}",
                success: function (response) {
                    $("#gpsModal").modal("show");
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    }
}


