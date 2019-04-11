var messageScript = "";
$('.required').next('.tooltip_outer_feedback').hide();
$('.required_feedback').next('.tooltip_outer').hide();
function GetMessage(m) {
    return "<div class='tooltip_outer'><div class='arrow-left'></div> <div class='tool_tip'>" + m + "</div></div>";
}
function numericsonly(ob) {
    var invalidChars = /[^0-9]/gi
    if (invalidChars.test(ob.value)) {
        ob.value = ob.value.replace(invalidChars, "");
    }
}
function decimalonly(ob) {
    var invalidChars = /[^0-9][^.][^0-9]/gi
    if (invalidChars.test(ob.value)) {
        ob.value = ob.value.replace(invalidChars, "");
    }
}
function noBack() {
            window.history.forward();
 }
 window.onpageshow = function (evt) { if (evt.persisted) noBack() }

window.onunload = function () { void (0) }
noBack();
jQuery(document).ready(function () {
     jQuery('.autoDisable').attr('autocomplete', 'off');
    jQuery('.autoDisable').attr('autocorrect', 'off');
    jQuery.AddDatePicker(".add_cal");
    BindValidationMethod();
    $('input[num="1"]').on("keyup", function () {
        $(this).next('.tooltip_outer').hide();
        numericsonly(this); // definition of this function is above
    });
    $('input[dec="1"]').on("keyup", function () {
        $(this).next('.tooltip_outer').hide();
        decimalonly(this); // definition of this function is above
    });
    // [0-9]+(\.[0-9][0-9]?)?
});
function BindRemoveError(o, m) {
    o.addClass("error");
    o.focus();
    o.after(GetMessage(m)).show("slow");
    o.on("blur", function () {
        o.next('.tooltip_outer').hide();
        o.removeClass("error");
    });
}
function BindValidationMethod() {
    jQuery(document).on("click", "input:submit", function (e) {
        var IsValid = true;
        if (jQuery(this).attr("disVal")) {
            return true;
        }
        jQuery("select").each(function () {
            var o = jQuery(this);
            if (IsValid) {
                if (o.attr("req") && jQuery('option:selected', jQuery(this)).index() == 0) {
                    e.preventDefault();
                    BindRemoveError(o, "Required");

                    IsValid = false;
                }
            }
        });
        jQuery("input:text").each(function () {
            var o = jQuery(this);

            if (IsValid) {
                if (o.attr("req") && jQuery.trim(o.val()) == '') {
                    e.preventDefault();
                    BindRemoveError(o, "Required");
                    IsValid = false;
                }

            }
            if (IsValid) {
                if (o.attr("mob") && (o.val().length != 10)) {
                    e.preventDefault();
                    BindRemoveError(o, "Should be of 10 digits");
                    IsValid = false;
                }
            }
            if (IsValid) {
                var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
                if (o.attr("email") && !(filter.test(o.val()))) {
                    e.preventDefault();
                    BindRemoveError(o, "Not a valid mail Id");
                    IsValid = false;
                }
            }
        });
        jQuery("input:password").each(function () {
            var o = jQuery(this);
            if (IsValid) {
                if (o.attr("req") && jQuery.trim(o.val()) == '') {
                    e.preventDefault();
                    BindRemoveError(o, "Required");
                    IsValid = false;
                }

            }
            if (IsValid) {
                if (o.attr("req") && (o.val().length < 6 || o.val().length > 8)) {
                    e.preventDefault();
                    BindRemoveError(o, "Must be between 6 to 8");
                    IsValid = false;
                }
            }
        });



    });
}


// Plug for common methods start from here
//Added by Indu on 03 Dec 2014
function CheckDates(mObject, from, to, message) {
    $(document).on("click", "#" + mObject, function (e) {
        if (new Date(jQuery("#" + from).val()) > new Date(jQuery("#" + to).val())) {
            e.preventDefault();
            alert(message);
        }
    });
}
// Get MD5 string
function Encript(strPass) {
    var md5Pass = $().crypt({
        method: "md5",
        source: strPass
    });
    return md5Pass;
}
//Added by Indu on 04 Dec 2014
function PrintDocuments(elementID, perPageRows) {
    //Get the HTML of div 
    var oldPage = document.body.innerHTML;
    if (perPageRows == 0) {
        var divElements = document.getElementById(elementID).innerHTML;
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body></html>";
        //Print Page
        window.print();
    }
    else {
        // var perPageRows = 15;
        var divElements = ApplyHeaderOnAllPage(elementID, perPageRows);
        //Get the HTML of whole page
        //Reset the page's HTML with div's HTML only
        if (divElements != 'Null') {
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body></html>";
            //Print Page
            window.print();
        }
        else {
            alert('There is no data to print');
        }
    }
    //Restore original HTML
    document.body.innerHTML = oldPage;
}
//Added by Indu on 04 Dec 2014
function ApplyHeaderOnAllPage(printDiv, perPageRows) {

    var newTable = "", theader = "", startTag = "", endTag = "", rowTag = "", tmpTable = "", divBrack = "";
    var gridRows = $("#" + printDiv + " table tr");
    var gridRowsCount = gridRows.length;
    if (gridRowsCount != 0) {
        var rCounter = 0;
        startTag = "<table width='100%' border='1'>";
        endTag = "</table>";
        divBrack = "<div class='page-break'></div>";
        if (gridRowsCount >= 0) {
            theader = "<tr>" + gridRows[0].innerHTML + "</tr>";
            for (i = 1; i < gridRowsCount; i++) {
                rCounter = rCounter + 1;
                if (rCounter >= perPageRows) {
                    rowTag += "<tr>" + gridRows[i].innerHTML + "</tr>";
                    tmpTable = startTag + theader + rowTag + endTag + divBrack;
                    newTable += tmpTable;
                    tmpTable = "";
                    rowTag = "";
                    rCounter = 0;
                }
                else {
                    rowTag += "<tr>" + gridRows[i].innerHTML + "</tr>";
                }
            }
            if (rCounter >= 1) {
                tmpTable = startTag + theader + rowTag + endTag + divBrack;
                newTable += tmpTable;
                tmpTable = "";
                rowTag = "";
                rCounter = 0;
            }
        }

        //alert(newTable);
    }
    else {
        return "Null";
    }

    return newTable;
}
Date.prototype.addDays = function (days) {
    this.setDate(this.getDate() + days);
    return this;
};
(function ($) {
   $.Encript=function(str) {    
        var md5Pass = $().crypt({
            method: "md5",
            source: str
        });
        return md5Pass;
    }
    $.AddDatePicker = function (o, n) {
        var culture = 'en-US';
        var oo = jQuery(o);
        jQuery.datepicker.setDefaults(jQuery.datepicker.regional[culture]);
        // oo.datepicker( "option", "dateFormat", "dd/MM/yyyy" );
        if (jQuery.trim(oo.val()) == '') {
            oo.datepicker({
                changeMonth: true,
                changeYear: true
            }, jQuery.datepicker.regional[culture]).datepicker('setDate', new Date().addDays(n)); //.datepicker( "option", "dateFormat", "dd/mm/yy" );
        }
        else {
            oo.datepicker({
                changeMonth: true,
                changeYear: true
            }, jQuery.datepicker.regional[culture]); //.datepicker( "option", "dateFormat", "dd/mm/yy" );
        }
    };
    $.AddDatePicker = function (o) {
        var culture = 'en-US';
        var oo = jQuery(o);
        jQuery.datepicker.setDefaults(jQuery.datepicker.regional[culture]);
        // oo.datepicker( "option", "dateFormat", "dd/MM/yyyy" );
        if (jQuery.trim(oo.val()) == '') {
            oo.datepicker({
                changeMonth: true,
                changeYear: true
            }, jQuery.datepicker.regional[culture]).datepicker('setDate', new Date().addDays(0)); //.datepicker( "option", "dateFormat", "dd/mm/yy" );
        }
        else {
            oo.datepicker({
                changeMonth: true,
                changeYear: true
            }, jQuery.datepicker.regional[culture]); //.datepicker( "option", "dateFormat", "dd/mm/yy" );
        }
    };
    $.CallWebMethods = function (methodName, data, callback) {
        var path = window.location.toString().split("?");
        $.ajax({
            cache: false,
            type: "POST",
            async: false,
            url: path[0] + "/" + methodName,
            data: $.getJSON(data),
            contentType: "application/json",
            dataType: "json",
            success: function (d) {
                callback(d);
            },
            error: function (xhr) {
                alert("Http error on SendData" + xhr.responseText + " " + xhr.statusCode);
            }
        });
    };
    $.URLEncode = function (c) {
        var o = '';
        var x = 0;
        c = c.toString();
        var r = /(^[a-zA-Z0-9_.]*)/;
        while (x < c.length) {
            var m = r.exec(c.substr(x));
            if (m != null && m.length > 1 && m[1] != '') {
                o += m[1];
                x += m[1].length;
            } else {
                if (c[x] == ' ')
                    o += '+';
                else {
                    var d = c.charCodeAt(x);
                    var h = d.toString(16);
                    o += '%' + (h.length < 2 ? '0' : '') + h.toUpperCase();
                }
                x++;
            }
        }
        return o;
    };
    $.URLDecode = function (s) {
        var o = s; var binVal, t;
        var r = /(%[^%]{2})/;
        while ((m = r.exec(o)) != null && m.length > 1 && m[1] != '') {
            b = parseInt(m[1].substr(1), 16);
            t = String.fromCharCode(b);
            o = o.replace(m[1], t);
        } return o;
    };
    $.getParameterQueryStringValue = function (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results == null)
            return "";
        else
            var str = $.URLDecode(decodeURIComponent(results[1])); //.replace(/\+/g, " ")
        return str;
    };
    $.getJSON = function (o) {
        if (typeof (JSON) == 'object' && JSON.stringify)
            return JSON.stringify(o);

        var type = typeof (o);

        if (o === null)
            return "null";

        if (type == "undefined")
            return undefined;

        if (type == "number" || type == "boolean")
            return o + "";

        if (type == "string")
            return $.convertString(o);

        if (type == 'object') {
            if (typeof o.getJSON == "function")
                return $.getJSON(o.getJSON());

            if (o.constructor === Date) {
                var month = o.getUTCMonth() + 1;
                if (month < 10) month = '0' + month;

                var day = o.getUTCDate();
                if (day < 10) day = '0' + day;

                var year = o.getUTCFullYear();

                var hours = o.getUTCHours();
                if (hours < 10) hours = '0' + hours;

                var minutes = o.getUTCMinutes();
                if (minutes < 10) minutes = '0' + minutes;

                var seconds = o.getUTCSeconds();
                if (seconds < 10) seconds = '0' + seconds;

                var milli = o.getUTCMilliseconds();
                if (milli < 100) milli = '0' + milli;
                if (milli < 10) milli = '0' + milli;

                return '"' + year + '-' + month + '-' + day + 'T' +
                             hours + ':' + minutes + ':' + seconds +
                             '.' + milli + 'Z"';
            }

            if (o.constructor === Array) {
                var ret = [];
                for (var i = 0; i < o.length; i++)
                    ret.push($.getJSON(o[i]) || "null");

                return "[" + ret.join(",") + "]";
            }

            var pairs = [];
            for (var k in o) {
                var name;
                var type = typeof k;

                if (type == "number")
                    name = '"' + k + '"';
                else if (type == "string")
                    name = $.convertString(k);
                else
                    continue;

                if (typeof o[k] == "function")
                    continue;

                var val = $.getJSON(o[k]);

                pairs.push(name + ":" + val);
            }

            return "{" + pairs.join(", ") + "}";
        }
    };
    $.convertString = function (string) {
        if (string.match(regex)) {
            return '"' + string.replace(regex, function (a) {
                var c = specialchar[a];
                if (typeof c === 'string') return c;
                c = a.charCodeAt();
                return '\\u00' + Math.floor(c / 16).toString(16) + (c % 16).toString(16);
            }) + '"';
        }
        return '"' + string + '"';
    };

    var regex = /["\\\x00-\x1f\x7f-\x9f]/g;

    var specialchar = {
        '\b': '\\b',
        '\t': '\\t',
        '\n': '\\n',
        '\f': '\\f',
        '\r': '\\r',
        '"': '\\"',
        '\\': '\\\\'
    };
 
   
})(jQuery);