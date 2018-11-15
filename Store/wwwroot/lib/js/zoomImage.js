    //var urlHome = "https://flowers.ua";
    //var urlDesign = "https://flowers.ua/design/Flowers";
    var lang = "ru";
    
    var itemId = 2333;
    var productUrl = "buket-25-krasnyh-roz";
    var showedVideo = true;
    var variantId = 0;
    var prevVariantId = 0;
    var isDynamic = false;
    var isVariatedProduct = false;
    var activeTab = 1;
    var promoTxtTemplate = 'Вы получаете %NUM% роз(у/ы) в подарок!';
    var activeStar = 1;
    //var activeStarImgUrl = "https://flowers.ua/design/Flowers/StalLightBIG.png";
    //var inactiveStarImgUrl = "https://flowers.ua/design/Flowers/StalDarkBIG.png";
    var videoCode = '';
    var videoIsEmbed = false;
    var mustStartVideo = false;
var variant_desr = {};
var variant_name = {};
vat itemId = 
var variant_image = {};
var variant_ext_images = {};
var variant_zoom_images = {};
var variantNameHash = {};
    var itemVariantName = '';

    
    variant_zoom_images['0'] = [];
    variant_zoom_images['0'].push("@Url.Content(" / Files / Bg / " + Model[0].Name)" );
    variant_zoom_images['0'].push('/Files/Bg/1/1/5.jpg');
    variant_zoom_images['0'].push('/Files/Bg/1/2/5.jpg');
    
    
$(function () {

    var zoomImgList = [];
    $.each(variant_zoom_images, function (i, variant) {
        $.each(variant, function (j, url) {
            zoomImgList.push(url);
        });
    });

    preloadImages(zoomImgList);


    $('#productImage')
        .addClass('zoom-in')
        .click(function (event) {
            event = event ? event : window.event;
            zoomImage(event, 'https://flowers.ua/images/Flowers/zoom/1/0/2333.jpg');
        });


    $('.extimgs li[id^="img_"]').each(function (ind, el) {
        var zoomUrl = '';
        if (ind in variant_zoom_images['0']) {
            zoomUrl = variant_zoom_images['0'][ind];
        }

        $(el)
            .click(function (e) {
                e = e ? e : window.event;
                e.preventDefault();
                changeImage(el, 0, 0, zoomUrl);
            });
    });

    $('.zoomwrap').mousemove(function (move_event) {
        move_event = move_event ? move_event : window.event;

        var img = $('img', this);
        var newImagePos = _getZoomPos(move_event, this);

        $(img).css({
            'top': -newImagePos.posY + 'px',
            'left': -newImagePos.posX + 'px'
        });
    });


    //function _getZoomPos(event, wrap) {
    //var wrapOffset = $(wrap).offset();
    //                        var wrapWidth = $(wrap).width();
    //                        var wrapHeight = $(wrap).height();
    //                        var img = $('img', wrap);
    //                        var imgWidth = $(img).width();
    //                        var imgHeight = $(img).height();
    //                        var cursorX = event.pageX - wrapOffset.left;
    //                        var cursorY = event.pageY - wrapOffset.top;

    //                        var newImagePosX = cursorX * ((imgWidth - wrapWidth) / wrapWidth);
    //                        var newImagePosY = cursorY * ((imgHeight - wrapHeight) / wrapHeight);

    //return {'posX' : newImagePosX, 'posY' : newImagePosY};
    //                        }

    //                        function getVariant(num) {
    //                            $('.visual.image, .quality-mark').show();
    //                        $('.visual.video').hide();

    //                        $('#productImage')
    //.attr({
    //                            'src' : variant_image[num],
    //                        'alt' : variant_name[num]
    //                        })
    //                        .unbind('click')
    //                        .removeClass('zoom-in');

    //if (variant_zoom_images[num]['0']) {
    //                            $('#productImage')
    //                                .addClass('zoom-in')
    //                                .click(function (event) {
    //                                    event = event ? event : window.event;
    //                                    zoomImage(event, variant_zoom_images[num]['0']);
    //                                });
    //                        }

    //                        $('#itemDescr').html(variant_desr[num]);
    //                        $('#variant').val(num);

    //                        prevVariantId = variantId;
    //                        variantId = num;
    //if (isDynamic) {
    //                            delay('dynamicRecalc()', 100);
    //                        }

    //if (variant_ext_images[num].length > 0 || videoCode.length) {
    //var list = $();
    //                        var zoomUrl = variant_zoom_images[num]['0'] ? variant_zoom_images[num]['0'] : '';

    //                        list = list.add(
    //$('<li>')
    //.click(function(e){
    //                                e = e ? e : window.event;
    //                            e.preventDefault();
    //                            changeImage(this, 0, 0, zoomUrl);
    //                            })
    //.attr({
    //                                'id' : 'img_',
    //                            'href' : '#'
    //                            })
    //                            .append(
    //$('<img>')
    //.attr({
    //                                    'alt' : 'common',
    //                                'src' : variant_image[num],
    //                                })
    //                                )
    //                                /*
    //                                .append(
    //$('<a>')
    //.click(function(e){
    //                                        e = e ? e : window.event;
    //                                    e.preventDefault();
    //                                    changeImage(this, 0, 0, zoomUrl);
    //                                    })
    //.attr({
    //                                        'id' : 'img_',
    //                                    'href' : '#'
    //                                    })
    //                                    .append(
    //$('<img>')
    //.attr({
    //                                            'alt' : 'common',
    //                                        'src' : variant_image[num],
    //                                        })
    //                                        )

    //                                        )
    //                                        */
    //                                        );

    //$.each(variant_ext_images[num], function(ind, imgUrl){
    //var view = ind + 1;
    //                                        var zoomUrl = variant_zoom_images[num][view] ? variant_zoom_images[num][view] : '';

    //                                        list = list.add(
    //$('<li>')
    //.click(function(e){
    //                                                e = e ? e : window.event;
    //                                            e.preventDefault();
    //                                            changeImage(this, 0, 0, zoomUrl);
    //                                            })
    //.attr({
    //                                                'id' : 'img_' + ind,
    //                                            'href' : '#'
    //                                            })
    //                                            .append(
    //$('<img>')
    //.attr({
    //                                                    'alt' : 'ext_' + ind,
    //                                                'src' : imgUrl,
    //                                                })
    //                                                )
    //                                                /*
    //                                                .append(
    //$('<a>')
    //.click(function(e){
    //                                                        e = e ? e : window.event;
    //                                                    e.preventDefault();
    //                                                    changeImage(this, 0, 0, zoomUrl);
    //                                                    })
    //.attr({
    //                                                        'id' : 'img_' + ind,
    //                                                    'href' : '#'
    //                                                    })
    //                                                    .append(
    //$('<img>')
    //.attr({
    //                                                            'alt' : 'ext_' + ind,
    //                                                        'src' : imgUrl,
    //                                                        })
    //                                                        )
    //                                                        )
    //                                                        */
    //                                                        );
    //                                                        });

    //if (videoCode.length) {
    //                                                            list = list.add($('.ico-video'));
    //                                                        }

    //if (list.length > 3) {
    //                                                            $('.extimgs ul.list').addClass('perFour');
    //                                                        } else {
    //                                                            $('.extimgs ul.list').removeClass('perFour');
    //                                                        }

    //                                                        $('.extimgs ul.list li').remove();
    //                                                        $('.extimgs ul.list').append(list);
    //                                                        }
    //                                                        }

    //function changeImage(element, shift, type, zoom_url) {
    //if (type) {
    //                                                            $('.visual.image, .quality-mark, .zoomwrap').hide();
    //                                                        if (!videoIsEmbed) {
    //                                                            $('.videowrap').html(videoCode);
    //                                                        videoIsEmbed = true;
    //                                                        }
    //                                                        $('.visual.video, .videowrap').show();
    //} else {
    //                                                            $('.visual.image, .quality-mark').show();
    //                                                        $('.visual.video, .videowrap, .zoomwrap').hide();

    //                                                        $('#productImage')
    //                                                        .unbind('click')
    //                                                        .removeClass('zoom-in')
    //                                                        .attr('src', $('img', element).attr('src'))
    //                                                        .attr("alt", $("img", element).attr("alt"))
    //                                                        .attr("title", $("img", element).attr("title"))

    //                                                    if (zoom_url) {
    //                                                            $('#productImage')
    //                                                                .addClass('zoom-in')
    //                                                                .click(function (event) {
    //                                                                    event = event ? event : window.event;
    //                                                                    zoomImage(event, zoom_url);
    //                                                                });
    //                                                        }
    //                                                        }

    //                                                        return false;
    //                                                        }

    //                                            function zoomImage(clk_event, zoom_url) {
    //                                                if (zoom_url) {
    //                                                            $('.zoomwrap')
    //                                                                .empty()
    //                                                                .append(
    //                                                                    $('<img>')
    //                                                                        .attr({
    //                                                                            'src': zoom_url
    //                                                                        })
    //                                                                        .click(function () {
    //                                                                            $('.visual.video').hide();
    //                                                                            $('.visual.image, .quality-mark').show();
    //                                                                        })
    //                                                                );

    //                                                        $('.visual.image, .quality-mark').hide();
    //                                                        $('.visual.video, .zoomwrap').show();

    //                                                        var newImagePos = _getZoomPos(clk_event, $('.zoomwrap'));
    //                                                $('.zoomwrap img').css({
    //                                                            'top' : (- newImagePos.posY) + 'px',
    //                                                        'left' : (- newImagePos.posX) + 'px'
    //                                                        });
    //                                                        }

    //                                                        return false;
    //                                                        }



    (function ($) {
        $.fn.popupWindow = function (instanceSettings) {

            return this.each(function () {

                $(this).click(function () {

                    $.fn.popupWindow.defaultSettings = {
                        centerBrowser: 0, // center window over browser window? {1 (YES) or 0 (NO)}. overrides top and left
                        centerScreen: 0, // center window over entire screen? {1 (YES) or 0 (NO)}. overrides top and left
                        height: 500, // sets the height in pixels of the window.
                        left: 0, // left position when the window appears.
                        location: 0, // determines whether the address bar is displayed {1 (YES) or 0 (NO)}.
                        menubar: 0, // determines whether the menu bar is displayed {1 (YES) or 0 (NO)}.
                        resizable: 0, // whether the window can be resized {1 (YES) or 0 (NO)}. Can also be overloaded using resizable.
                        scrollbars: 0, // determines whether scrollbars appear on the window {1 (YES) or 0 (NO)}.
                        status: 0, // whether a status line appears at the bottom of the window {1 (YES) or 0 (NO)}.
                        width: 500, // sets the width in pixels of the window.
                        windowName: null, // name of window set from the name attribute of the element that invokes the click
                        windowURL: null, // url used for the popup
                        top: 0, // top position when the window appears.
                        toolbar: 0 // determines whether a toolbar (includes the forward and back buttons) is displayed {1 (YES) or 0 (NO)}.
                    };

                    settings = $.extend({}, $.fn.popupWindow.defaultSettings, instanceSettings || {});

                    var windowFeatures = 'height=' + settings.height +
                        ',width=' + settings.width +
                        ',toolbar=' + settings.toolbar +
                        ',scrollbars=' + settings.scrollbars +
                        ',status=' + settings.status +
                        ',resizable=' + settings.resizable +
                        ',location=' + settings.location +
                        ',menuBar=' + settings.menubar;

                    settings.windowName = this.name || settings.windowName;
                    settings.windowURL = this.href || settings.windowURL;
                    var centeredY, centeredX;

                    if (settings.centerBrowser) {

                        if ($.browser.msie) {//hacked together for IE browsers
                            centeredY = (window.screenTop - 120) + ((((document.documentElement.clientHeight + 120) / 2) - (settings.height / 2)));
                            centeredX = window.screenLeft + ((((document.body.offsetWidth + 20) / 2) - (settings.width / 2)));
                        } else {
                            centeredY = window.screenY + (((window.outerHeight / 2) - (settings.height / 2)));
                            centeredX = window.screenX + (((window.outerWidth / 2) - (settings.width / 2)));
                        }
                        window.open(settings.windowURL, settings.windowName, windowFeatures + ',left=' + centeredX + ',top=' + centeredY).focus();
                    } else if (settings.centerScreen) {
                        centeredY = (screen.height - settings.height) / 2;
                        centeredX = (screen.width - settings.width) / 2;
                        window.open(settings.windowURL, settings.windowName, windowFeatures + ',left=' + centeredX + ',top=' + centeredY).focus();
                    } else {
                        window.open(settings.windowURL, settings.windowName, windowFeatures + ',left=' + settings.left + ',top=' + settings.top).focus();
                    }
                    return false;
                });

            });
        };
    })(jQuery);
    $('#share').popupWindow({
        width: 550,
        height: 400,
        centerBrowser: 1
    });
);