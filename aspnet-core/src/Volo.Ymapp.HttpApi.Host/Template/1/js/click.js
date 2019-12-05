// dispatch(document);

$(document).on('click', function (e) {
    if (e.target.className != 'span-city') {
        $('.blue_title_content').hide().find('.title_active').hide();
        $('.blue_title_slide').find('.swiper-slide').removeClass('active');
    }
    if (e.target.className != 'btn-country') {
        $('.blue_country_select ul').hide();
        $('.blue_country_select').removeAttr('data-flag');
    }
    if (e.target.className != 'btn-obj') {
        $('.blue_btn_select .blue_all_select').hide();
        $('.blue_btn_select').removeAttr('data-flag');
    }
    if (e.target.className != 'icon-bar' && e.target.className != 'navbar-toggle') {
        $('#bs-example-navbar-collapse-1').collapse('hide');
    }
});
$(function () {
    $(document).find('*').addEvent('click', function (e) {
    });
});
$.fn.extend({
    addEvent: function (type, handle, bool) {
        var el, thisLen = this.length;
        bool ? bool = bool : bool = false;
        if (thisLen == 1) {
            el = this[0];        //jquery对象转成 js对象
            el.addEventListener ? el.addEventListener(type, handle, bool) :
                el.attachEvent('on' + type, handle);
        } else {
            for (var i = 0; i < thisLen; i++) {
                el = this[i];
                el.addEventListener ? el.addEventListener(type, handle, bool) :
                    el.attachEvent('on' + type, handle);
            }
        }
    }
});