var mySwiper = 1;

$(function () {
    $(document).on('click', '.btn-lan', function () {
        var flag = $(this).attr('data-lan');
        if (flag == '1') {
            $(this).text('Cn').attr('data-lan', '2');
        } else {
            $(this).text('En').attr('data-lan', '1');
        }
    });

    $(document).on('click','.btn_article_before',function () {
        $('.blue_article_before').hide();
        $('.blue_article_after').slideDown();
    });
    $(document).on('click','.btn_article_after',function () {
        $('.blue_article_before').show();
        $('.blue_article_after').slideUp();
    });
    
    $(document).on('click','.btn_fixed_close',function () {
        $('.blue_fixed').hide();
    });
    
    $('.blue_wx').hover(function () {
        $('.blue_share_wx').addClass('active');
    },function () {
        $('.blue_share_wx').removeClass('active');
    });
    $('.blue_mail').hover(function () {
        $('.blue_share_mail').addClass('active');
    },function () {
        $('.blue_share_mail').removeClass('active');
    });

    swiperMove();
    $(window).resize(function () {
        swiperMove()
    });
});

function swiperMove() {
    var offWidth = $(window).width();
    if (offWidth <= 767) {
        if(mySwiper != 1){
            mySwiper.destroy();
        }
        mySwiper = new Swiper('.swiper-container', {

            direction: 'horizontal',
            loop: true,

            nextButton: '.swiper-button-next',
            prevButton: '.swiper-button-prev'
        });
    } else {
        if(mySwiper != 1){
            mySwiper.destroy();
        }
        mySwiper = new Swiper('.swiper-container', {

            direction: 'horizontal',
            loop: true,

            nextButton: '.swiper-button-next',
            prevButton: '.swiper-button-prev',

            slidesPerView: 6,
            spaceBetween: 20
        });
    }
}
