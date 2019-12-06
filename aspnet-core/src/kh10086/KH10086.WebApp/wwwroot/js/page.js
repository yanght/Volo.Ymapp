$(function () {
    $(document).on('click', '.btn-lan', function () {
        var flag = $(this).attr('data-lan');
        if (flag == '1') {
            $(this).text('Cn').attr('data-lan', '2');
        } else {
            $(this).text('En').attr('data-lan', '1');
        }
    });
    $('.blue_page_list dt').on('click',function () {
        var text = $(this).attr('data-flag');
        if(text == '0'){
            $(this).parents('.blue_page_list').find('dt').attr('data-flag','0');
            $(this).attr('data-flag','1');
            $(this).parents('.blue_page_list').find('dd').slideUp();
            $(this).parents('.blue_page_list').find('dl').css({
                'border-top':'none',
                'margin-top':'0'
            });
            $(this).css('margin-bottom','5px');
            $(this).parent('dl').find('dd').slideDown();
            $(this).parent('dl').next('dl').css({
                'border-top':'1px solid #9ca5b1',
                'margin-top':'5px'
            });
        }else{
            $(this).parents('.blue_page_list').find('dt').attr('data-flag','0');
            $(this).parents('.blue_page_list').find('dd').slideUp();
            $(this).css('margin-bottom','0');
            $(this).parents('.blue_page_list').find('dl').css({
                'border-top':'none',
                'margin-top':'0'
            });
        }
    });
    $('.blue_trip_line_title').on('click',function () {
        var text = $(this).find('.blue_trip_after').attr('data-flag');
        if(text == '0'){
            $(this).parents('.blue_trip_line').siblings('.blue_trip_line').find('.blue_trip_text').slideUp();
            $(this).find('.blue_trip_after').addClass('active').attr('data-flag','1').parents('.blue_trip_line').find('.blue_trip_text').slideDown();
        }else{
            $(this).find('.blue_trip_after').removeClass('active').attr('data-flag','0').parents('.blue_trip_line').find('.blue_trip_text').slideUp();
        }
    });
    $('.blue_nav_menu a').on('click',function () {
        var index = $(this).attr('data-index');
        var goScroll = 0;
        switch (index){
            case '1':
                goScroll = $('#1').offset().top;
                break;
            case '2':
                goScroll = $('#2').offset().top;
                break;
            case '3':
                goScroll = $('#3').offset().top;
                break;
            case '4':
                goScroll = $('#4').offset().top;
                break;
            case '5':
                goScroll = $('#5').offset().top;
                break;
            case '6':
                goScroll = $('#6').offset().top;
                break;
            default:alert('出错了！');
        }
        $('html, body').animate({scrollTop:goScroll - 150}, 'slow');
    });
    
    $('.blue_sel_option a').on('click',function () {
        $(this).parents('.input-group-btn').find('.btn-text').text($(this).text());
    });


    $(document).on('click','.blue_dialog_img',function () {
        var _src = $(this).find('img').attr('src');
        $('body').css('overflow','hidden');
        $('.blue_img_dialog').find('img').attr('src',_src);
        $('.blue_img_dialog').show();
    });
    $(document).on('click','.blue_img_dialog_close,.blue_img_dialog_bg',function () {
        $('.blue_img_dialog').hide();
    });
});