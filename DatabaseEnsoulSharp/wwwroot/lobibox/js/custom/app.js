var Site = (function ($, window) {
    var loadPartialView = function (element, url, callbalck) {
        $(element).load(url,
            function () {
                callbalck && callbalck();
            });
    };

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

    var ajaxForm = function (url, data, success, error) {
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            processData: false,
            contentType: false,
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

    var fileUpload = function (element) {
        element && $(element).fileinput({
            previewFileType: "image",
            browseClass: "btn btn-success",
            browseLabel: "Pick Image",
            browseIcon: '<i class="glyphicon glyphicon-picture"></i> ',
            removeClass: "btn btn-danger",
            removeLabel: "Delete",
            removeIcon: '<i class="glyphicon glyphicon-trash"></i> ',
            uploadClass: "btn btn-info",
            uploadLabel: "Upload",
            uploadIcon: '<i class="glyphicon glyphicon-upload"></i> ',
        });
    };

    var getFile = function (element) {
        var files = element.prop('files') || [];

        if (files.length > 1) return files;
        return files[0] || null;
    };

    var singleSelect = function (element) {
        element && $(element).select2().attr('style', 'display:block; position:absolute; bottom: 0; left: 0; clip:rect(0,0,0,0); ');
    };

    var table = function (element, height) {
        element && $(element).DataTable({
            "scrollY": height,
        });
    };

    var alertSuccess = function (message, callback) {
        Swal.fire({
            type: 'success',
            title: message,
            showConfirmButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, OK!'
        }).then((result) => {
            callback(result);
        });
    };

    return {
        loadPartialView: loadPartialView,
        ajax: ajax,
        getValueElement: getValueElement,
        fileUpload: fileUpload,
        ajaxForm: ajaxForm,
        getFile: getFile,
        singleSelect: singleSelect,
        alertSuccess: alertSuccess,
        table: table
    };
})(jQuery, window);