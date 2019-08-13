(function ($, Site) {
    'use strict';
    var pluginName = 'film';
    var config = {
        heightTable: 400
    };

    function Plugin(element, options) {
        this.element = $(element);
        this.options = $.extend({}, $.fn[pluginName].defaults, this.element.data(), options);
        this.init();
    }

    var submitCreateFilm = function () {
        var that = this;
        var options = that.options || {};

        $(that.options.idCreateForm).submit(function (event) {
            var formData = new FormData();

            formData.append('NameVn', Site.getValueElement("#NameVn"));
            formData.append('NameEn', Site.getValueElement("#NameEn"));
            formData.append('Slug', Site.getValueElement("#Slug"));
            formData.append('Description', Site.getValueElement("#Description"));
            formData.append('StarIMdb', Site.getValueElement("#StarIMdb"));
            formData.append('YearOfManufacture', Site.getValueElement("#YearOfManufacture"));
            formData.append("FileToUpload", Site.getFile($("#Image")));
            formData.append("IdCategogyFilm", Site.getValueElement($("#IdCategogyFilm")));
            formData.append("IdCountry", Site.getValueElement($("#IdCountry")));

            Site.ajaxForm(options.urlCreateForm, formData, function (res) {
                if (res.status) {
                    Site.alertSuccess(res.message, function () {

                    });
                }
            });
        });
    };

    var loadCreateFilm = function () {
        var that = this;
        var options = this.options || {};
        Site.loadPartialView.call(this, options.idCreateFilm, options.urlCreateFilm, function (e) {
            Site.fileUpload.call(this, options.idFieldImage);
            Site.singleSelect.call(this, $(options.idCategogyFilm));
            Site.singleSelect.call(this, $(options.idCountry));

            submitCreateFilm.call(that);
        });
    };

    var loadTableFilm = function () {
        var options = this.options || {};
        Site.loadPartialView.call(this, options.idTableFilm, options.urlTableFilm, function () {
            Site.table($(options.idDataTableFilm), config.heightTable);
        });
    };

    Plugin.prototype = {
        init: function () {
            loadCreateFilm.call(this);
            loadTableFilm.call(this);
        },
        destroy: function () {
            $.removeData(this.element[0], pluginName);
        }
    };

    $.fn[pluginName] = function (options, params) {
        return this.each(function () {
            var instance = $.data(this, pluginName);
            if (!instance) {
                $.data(this, pluginName, new Plugin(this, options));
            } else if (instance[options]) {
                instance[options](params);
            }
        });
    };

    $.fn[pluginName].defaults = {
        idCreateFilm: "#create",
        idTableFilm: "#table",
        idCreateForm: "#createForm",
        idFieldImage: "#Image",
        idCategogyFilm: "#IdCategogyFilm",
        idCountry: "#IdCountry",
        idDataTableFilm: "#TableFilm",

        urlCreateFilm: "/Film/CreateFirm",
        urlTableFilm: "/Film/TableFilm",
        urlCreateForm: "/Film/SubmitCreateFilm"
    };

    $(function () {
        $('[data-' + pluginName + ']')[pluginName]({
            key: 'custom'
        });
    });
}(jQuery, Site));