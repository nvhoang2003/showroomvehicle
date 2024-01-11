document.addEventListener('DOMContentLoaded', function () {
    var mySwiper = new Swiper('.swiper', {
        slidesPerView: 1,
        loop: true,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
        autoplay: {
            delay: 5000,
        },
        effect: 'fade'
    });
});
$(document).ready(function () {
    $('.menu_car > a').on('click', function (e) {
        e.preventDefault(); 
        toggleDropdown(); 
    });
    $('#mega_oto_tab .nav-link').on('click', function () {
        toggleDropdown();
    });
});
