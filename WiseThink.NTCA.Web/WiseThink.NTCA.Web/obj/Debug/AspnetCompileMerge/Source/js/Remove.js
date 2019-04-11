/// <reference path="Remove.js" />
function showconfirm_NonRecuringCore() {
    //setRecuring_core();
    if (confirm("Are you sure? you want to clear this entry!") == true) {
        setNonRecuring_Core();
       
    } else {        
        return false;
    }
}

function showconfirm_NonRecuringBuffer() {
    //setRecuring_core();
    if (confirm("Are you sure? you want to clear this entry") == true) {
        setNonRecuring_Buffer();        
    } else {
        return false;
    }
}

function showconfirm_RecuringCore() {
    //setRecuring_core();
    if (confirm("Are you sure? you want to clear this entry") == true) {
        setRecuring_core()
        
    } else {
       
        return false;
    }
}

function showconfirm_RecuringBuffer() {
    //setRecuring_core();
    if (confirm("Are you sure? you want to clear this entry") == true) {
        setRecuring_Buffer()
       
    } else {
        
        return false;
    }
}

/*************************************************************************************************/
/*This function is usedto show and hide right panel in APO Form*/
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
            $(".Rbuffer").removeClass("active");
        }
        else if (id == "#Non-RecurringDiv") {
            $("#core1").addClass("in");
            $("#core1").addClass("active");
            $(".NRcore").addClass("active");
            $(".NRbuffer").removeClass("active");
        }
        else {
        }
    });
}

function closeGPSpopup(e) {
    PageMethods.SetModelPopupFlag("False");
    $("#gpsModal").removeClass("in");
    $("#gpsModal").css("display", "none");
    $.session.set("GPS_Modal", "");
    //window.location.reload();
   // setNonRecuring_Core()
}

function GetSession() {
    
    var lefttab = sessionStorage.getItem("LeftTab"); 
    var maintab = sessionStorage.getItem("mainTab");
    var getsession = sessionStorage.getItem("Temp");

    if (lefttab != null || lefttab != undefined) {
        if (maintab != null || maintab != undefined) {
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
            if (lefttab == "Recuring" && maintab == "Core") {
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
            else if (lefttab == "Recuring" && maintab == "Buffer") {
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

            else if (lefttab == "NonRecuring" && maintab == "Buffer") {
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
            else if (lefttab == "NonRecuring" && maintab == "Core") {
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


            else if (lefttab == "NonRecuring") {
                $(".NonRecuring_left").addClass("Current");

                $("div#Non-RecurringDiv").show();
            }
            else {
            }
            //$.session.set("LeftTab", "");
            //$.session.set("mainTab", "");

        }

    }
    
}

function CreateSession_Search() {
    $.session.set("SearchSession", "Search");   
}

function CreateSession_AdvanceSearch() {
    $.session.set("SearchSession", "AdvanceSearch");   
}

function GetSession_home() {
    var sessionvariable=$.session.get("SearchSession");
    if (sessionvariable != null || sessionvariable != undefined) {
      
            /*Remove li active tag code*/
            $(".liclass").each(function () {
                $(this).removeClass("active");
            });

       
        $(".tab-pane").each(function () {
            $(this).removeClass("in");
            $(this).removeClass("active");
        });
        if (sessionvariable == "Search") {
            $(".search").addClass("active");
            $("#tabSearch").addClass("active");
            $("#tabSearch").addClass("in");
            //Code to add active class on li or anchoar tag of li in search tag
            //code to show search div
        }
        else if (sessionvariable == "AdvanceSearch") {
            //Code to add active class on li or anchoar tag of li in Advance search tag
            //code to show Advance search div
            $(".AdvanceSearch").addClass("active");
            $("#tabAdvanceSearch").addClass("active");
            $("#tabAdvanceSearch").addClass("in");
        }
        else {
            //Code to add active class on li or anchoar tag of li in search tag(Bydefult)
            //code to show search div(Bydefult)
            $(".search").addClass("active");
            $("#tabSearch").addClass("active");
            $("#tabSearch").addClass("in");
        }
    }
    else {
        //Code to add active class on li or anchoar tag of li in search tag(Bydefult)
        //code to show search div(Bydefult)
        $(".search").addClass("active");
        $("#tabSearch").addClass("active");
        $("#tabSearch").addClass("in");
    }
}

function setRecuring_core() {
    $.session.set("LeftTab", "Recuring");
    $.session.set("mainTab", "Core");
    alert("Activity items of recuring core area will be saved in draft.\nPlease fill and save activity items of recuring buffer area in draft also(if any) before moving to step 3.");
}

function setRecuring_Buffer() {
    $.session.set("LeftTab", "Recuring");
    $.session.set("mainTab", "Buffer");
    alert("Activity items of recuring buffer area will be saved in draft,and move to step 3.");
}

function setNonRecuring_Core() {
    $.session.set("LeftTab", "NonRecuring");
    $.session.set("mainTab", "Core");
    
   // $("#gpsModal").addClass("in");
   // $("#gpsModal").css("display","block");
   // alert("Activity items of non-recuring core area will be saved in draft.\nPlease fill and save activity items of non-recuring buffer area in draft also(if any) before moving to step 2.");
}

function CloseModalPop() {
   //sessionStorage.setItem("gpsmodalstatus","False")
   
    $(".close").click(function () {
        $("#gpsModal").removeClass("in");
        $("#gpsModal").css("display", "none");
        $.session.set("GPS_Modal", "");
       // window.location.reload();
    });
}

$(document).ready(function () {
    var value = $.session.get("GPS_Modal");
    if (value == undefined) {
        $.session.set("GPS_Modal", "");
    }
    else if (value != "") {
        $("#gpsModal").addClass("in");
        $("#gpsModal").css("display", "block");
        $.session.set("GPS_Modal", "");
    }
    else {
        $.session.set("GPS_Modal", "");
    }
    CloseModalPop();
});

function setNonRecuring_Buffer() {
    $.session.set("LeftTab", "NonRecuring");
    $.session.set("mainTab", "Buffer");
    alert("Activity items of non-recuring buffer area will be saved in draft,and move to step 2.");
}

/*check pressed F5(refresh) event*/
function clearSession_Fresh(e) {
    window.onbeforeunload = $(function () {
        $.session.remove('LeftTab');
        $.session.remove('mainTab');

    });
}

document.onkeydown = fkey;
document.onkeypress = fkey
document.onkeyup = fkey;

var wasPressed = false;

function fkey(e) {
    e = e || window.event;
    if (wasPressed) return;

    if (e.keyCode == 116) {
        $.session.remove('tableft');
        $.session.remove('tabmain');
        wasPressed = true;
    }

    else {
        $.session.remove('LeftTab');
        $.session.remove('mainTab');
        wasPressed = true;
    }
}

function validate(val, o) {
    var req = /^[0-9]\d*(\.\d{1,3})?$/;
    // var req = /^([1-9]{0,3})([0-9]{1})(\.[0-9])?$/;
    if (req.test(val)) {
        return true;
    }
    else {
        if (val != '') {
            alert("only three digits are allowed after decimal");
            $(o).val("");
            return false;
        }
        // $("#lblerromsg_referal").text("Enter only decimal digits or number");
        // $("#lblerromsg_referal").addClass("errorMsg");
    }
}
