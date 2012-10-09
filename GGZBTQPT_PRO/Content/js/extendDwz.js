var SL_DWZ = {
    ajaxError: function (xhr, ajaxOptions, thrownError) {
        if (ajaxOptions == "parsererror") {
            //return $($.pdialog.getCurrent).find(".pageContent").html(xhr.responseText);
            var _currentDialog = $.pdialog._current;
            _currentDialog.find(".dialogContent").html(xhr.responseText);
            $(":button.close", _currentDialog).click(function () {
                $.pdialog.close(_currentDialog);
            });
            return;
        }
        if (alertMsg) {
            alertMsg.error("<div>Http status: " + xhr.status + " " + xhr.statusText + "</div>"
				+ "<div>ajaxOptions: " + ajaxOptions + "</div>"
				+ "<div>thrownError: " + thrownError + "</div>"
				+ "<div>" + xhr.responseText + "</div>");
        } else {
            alert("Http status: " + xhr.status + " " + xhr.statusText + "\najaxOptions: " + ajaxOptions + "\nthrownError:" + thrownError + "\n" + xhr.responseText);
        }
    }
};