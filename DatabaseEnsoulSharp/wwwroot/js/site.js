var Site = (function ($, window) {
    var ajax = function (url, data, success, error) {
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            contentType: 'application/json; charset=utf-8',
            success: function (res) {
                success && success(res);
            },
            dataType: "json",
            error: function (err) {
                if (error) {
                    error(err);
                } else {
                    alert("Error !!!");
                }
            }
        });

        return false;
    };

    var getValueElement = function (element) {
        return $(element).val();
    };

    var table = function (element, height) {
        element && element.DataTable({
            "scrollY": height,
            "scrollX": true
        });
    };

    var loadPopup = function (callback) {
        var url = this.href;
        $('#modal-large .modal-content').load(url, callback);
    };

    var loadPopupSmall = function (callback) {
        var url = this.href;
        $('#modal-small .modal-content').load(url, callback);
    };

    return {
        ajax: ajax,
        getValueElement: getValueElement,
        table: table,
        loadPopup: loadPopup,
        loadPopupSmall: loadPopupSmall
    };
})(jQuery, window);