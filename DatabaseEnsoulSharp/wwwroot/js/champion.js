(function ($, Site) {
    'use strict';
    var pluginName = 'champion';

    function Plugin(element, options) {
        this.element = $(element);
        this.options = $.extend({}, $.fn[pluginName].defaults, this.element.data(), options);
        this.init();
    }

    function initGoogleAnalytics() {
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-119923845-2');
    }

    function inItParallaxWindow() {
        var options = this.options || {};
        $(options.parallaxWindow).parallax({ imageSrc: '/images/jumbotron.jpg' });
    }

    function inItModal() {
        $('#modal-large').on('hidden.bs.modal', function (e) {
            $('#modal-large .modal-content').empty();
        });
        $('#modal-small').on('hidden.bs.modal', function (e) {
            $('#modal-large .modal-content').empty();
        });
    }

    function inItEvent() {
        var options = this.options || {};
        var that = this;

        $(options.dataCheck).click(function () {
            Site.loadPopup.call(this, function () {
                voteOnload.call();
                starClick.call();
                starHover.call();
                statusClick.call();
            });
        });

        $("#search-input").keyup(function () {
            var value = this.value;

            $(".champion-item").show();

            var array = searchIgnoreChampion.call(that, value);

            mainSearch.call(that, array);
        });
    }

    function statusClick() {
        $(".change-status").click(function () {
            var result = confirm("Are you sure change status ?");

            if (result) {
                var model = {
                    IdScript: $(this).data('id'),
                    IdChampion: $("#id-champion").val()
                };
                Site.ajax("/Home/ActionStatus", JSON.stringify(model), function (res) {
                    alert(res.message);
                });
            }
        });
    }

    function voteOnload() {
        $.each($(".script-info"), function (index, value) {
            var valueRating = $(value).data("rating");
            var stars = $(this).find('li.star');

            for (var i = 0; i < valueRating; i++) {
                $(stars[i]).addClass('selected');
            }
        });
    }

    function starClick() {
        $('#stars li').on('click', function () {
            var result = confirm("Are you sure voted ?");
            if (result) {
                var i;
                var onStar = parseInt($(this).data('value'), 10);
                var stars = $(this).parent().find('li.star');

                for (i = 0; i < stars.length; i++) {
                    $(stars[i]).removeClass('selected');
                }

                for (i = 0; i < onStar; i++) {
                    $(stars[i]).addClass('selected');
                }

                var model = {
                    IdScript: $($(this).parent()).data('script'),
                    Point: parseInt($(this).parent().find('li.selected').last().data('value'), 10),
                    IdChampion: $("#id-champion").val()
                };

                Site.ajax("/Home/ActionRating", JSON.stringify(model), function (res) {
                    alert(res.message);
                });
            }
        });
    }

    function starHover() {
        $('#stars li').on('mouseover', function () {
            var onStar = parseInt($(this).data('value'), 10);
            $(this).parent().children('li.star').each(function (e) {
                if (e < onStar) {
                    $(this).addClass('hover');
                }
                else {
                    $(this).removeClass('hover');
                }
            });
        }).on('mouseout', function () {
            $(this).parent().children('li.star').each(function (e) {
                $(this).removeClass('hover');
            });
        });
    }

    function getFbUserData() {
        FB.api('/me', { locale: 'en_US', fields: 'first_name,last_name,email' },
            function (response) {
                var model = {
                    FirstName: response.first_name,
                    LastName: response.last_name,
                    Email: response.email
                };
                Site.ajax("/Home/Login", JSON.stringify(model), function () {
                    location.reload();
                });
            });
    }

    function searchIgnoreChampion(keyword) {
        var options = this.options || {};
        var array = options.listChampion;
        var arrayNew = [];

        for (var i = 0; i < array.length; i++) {
            var item = array[i];

            if (item.Name.toLowerCase().indexOf(keyword.toLowerCase()) === -1) {
                arrayNew.push(item);
            }
        }
        return arrayNew;
    }

    function mainSearch(array) {
        for (var i = 0; i < array.length; i++) {
            var itemI = array[i];
            $(itemI.Value).hide();
        }
    }

    function createDataChampion() {
        var data = $(".champion-item");
        var array = [];

        for (var i = 0; i < data.length; i++) {
            var value = data[i];

            var name = $(value).find(".name-champion").text();

            array.push({ Name: name, Value: value });
        }

        return array;
    }

    Plugin.prototype = {
        init: function () {
            initGoogleAnalytics.call(this);
            inItParallaxWindow.call(this);
            inItModal.call(this);
            inItEvent.call(this);
            this.options.listChampion = createDataChampion.call(this);
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
        dataCheck: "[data-check]",
        parallaxWindow: ".parallax-window",
        listChampion: null
    };

    $(function () {
        $('[data-' + pluginName + ']')[pluginName]({
            key: 'custom'
        });
    });
}(jQuery, Site));