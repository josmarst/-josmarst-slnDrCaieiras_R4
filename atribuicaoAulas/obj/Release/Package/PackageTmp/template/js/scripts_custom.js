
(function($) {
    "use strict";
    $(document).ready(function() {
        /*==Left Navigation Accordion ==*/
        if ($.fn.dcAccordion) {
            $('#nav-accordion').dcAccordion({
                eventType: 'click',
                autoClose: true,
                saveState: true,
                disableLink: true,
                speed: 'slow',
                showCount: false,
                autoExpand: true,
                classExpand: 'dcjq-current-parent'
            });
        }
        /*==Nice Scroll ==*/
        if ($.fn.niceScroll) {
            $(".leftside-navigation").niceScroll({
                cursorcolor: "#F78B1F",
                cursorborder: "0px solid #fff",
                cursorborderradius: "0px",
                cursorwidth: "3px"
            });
            $(".leftside-navigation").getNiceScroll().resize();
            if ($('#sidebar').hasClass('hide-left-bar')) {
                $(".leftside-navigation").getNiceScroll().hide();
            }
            $(".leftside-navigation").getNiceScroll().show();
            $(".right-stat-bar").niceScroll({
                cursorcolor: "#F78B1F",
                cursorborder: "0px solid #fff",
                cursorborderradius: "0px", 
                cursorwidth: "3px"
            });
        }


        /*==Sidebar Toggle==*/
        $(".leftside-navigation .sub-menu > a").click(function() {
            var o = ($(this).offset());
            var diff = 80 - o.top;
            if (diff > 0 && $(".leftside-navigation").scrollTo) $(".leftside-navigation").scrollTo("-=" + Math.abs(diff), 500);
            else if($(".leftside-navigation").scrollTo) $(".leftside-navigation").scrollTo("+=" + Math.abs(diff), 500);
        });
        $('.sidebar-toggle-box .fa-bars').click(function(e) {
            $(".leftside-navigation").niceScroll({
                cursorcolor: "#F78B1F",
                cursorborder: "0px solid #fff",
                cursorborderradius: "0px",
                cursorwidth: "3px"
            });
            $('#sidebar').toggleClass('hide-left-bar');
            if ($('#sidebar').hasClass('hide-left-bar')) {
                $(".leftside-navigation").getNiceScroll().hide();
            }
            $(".leftside-navigation").getNiceScroll().show();
            $('#main-content').toggleClass('merge-left');
            e.stopPropagation();
            if ($('#container').hasClass('open-right-panel')) {
                $('#container').removeClass('open-right-panel')
            }
            if ($('.right-sidebar').hasClass('open-right-bar')) {
                $('.right-sidebar').removeClass('open-right-bar')
            }
            if ($('.header').hasClass('merge-header')) {
                $('.header').removeClass('merge-header')
            }
        });
        $('.toggle-right-box .fa-bars').click(function(e) {
            $('#container').toggleClass('open-right-panel');
            $('.right-sidebar').toggleClass('open-right-bar');
            $('.header').toggleClass('merge-header');
            e.stopPropagation();
        });
        $('.header,#main-content,#sidebar').click(function() {
            if ($('#container').hasClass('open-right-panel')) {
                $('#container').removeClass('open-right-panel')
            }
            if ($('.right-sidebar').hasClass('open-right-bar')) {
                $('.right-sidebar').removeClass('open-right-bar')
            }
            if ($('.header').hasClass('merge-header')) {
                $('.header').removeClass('merge-header')
            }
        });
        $('.panel .tools .fa').click(function() {
            var el = $(this).parents(".panel").children(".panel-body");
            if ($(this).hasClass("fa-chevron-down")) {
                $(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
                el.slideUp(200);
            } else {
                $(this).removeClass("fa-chevron-up").addClass("fa-chevron-down");
                el.slideDown(200);
            }
        });
        //$('.panel-heading').click(function () {
        // $('.panel .tools .fa').click(); 
        //});
        // tool tips
        $('.tooltips').tooltip();
        // popovers
        $('.popovers').popover();
        


    });
    })

(jQuery);





//timepicker end

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
$(function () {
    $(window).bind("load resize", function() {
        topOffset = 50;
        width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.navbar-collapse').addClass('collapse');
            topOffset = 100; // 2-row-menu
        } else {
            $('div.navbar-collapse').removeClass('collapse');
        }

        height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
        height = height - topOffset;
        if (height < 1) height = 1;
        if (height > topOffset) {
            $("#page-wrapper").css("min-height", (height) + "px");
        }
    });

    var url = window.location;
    var element = $('ul.nav a').filter(function() {
        return this.href == url || url.href.indexOf(this.href) == 0;
    }).addClass('active').parent().parent().addClass('in').parent();
    if (element.is('li')) {
        element.addClass('active');
    }
});





//$('#pre-selected-options').multiSelect();



//$(document).ready(function(){
//  $('.date').mask('00/00/0000');
//  $('.monthyear').mask('00/0000');     
//   $('.year').mask('0000'); 
//  $('.time').mask('00:00:00');
//  $('.date_time').mask('00/00/0000 00:00:00');
//  $('.cep').mask('00000-000');
//  $('.enem').mask('000000000000000');
//  $('.cart_credit').mask('0000.0000.0000.0000');
//  $('.phone').mask('00000-0000'); 
//  $('.phone_with_ddd').mask('(00) 0000-00000');
//  $('.codigo_ddd').mask('00');  
//  $('.phone_us').mask('(000) 000-0000');
//  $('.mixed').mask('AAA 000-S0S');
//  $('.cpf').mask('000.000.000-00', {reverse: true});
//  $('.rg').mask('00.000.000-0', {reverse: true});
//  $('.cnpj').mask('00.000.000/0000-00', {reverse: true});
//  $('.money').mask('000.000.000.000.000,00', {reverse: true});
//  $('.money2').mask("#.##0,00", {reverse: true});
//  $('.ip_address').mask('0ZZ.0ZZ.0ZZ.0ZZ', {
//    translation: {
//      'Z': {
//        pattern: /[0-9]/, optional: true
//      }
//    }
//  });
//  $('.ip_address').mask('099.099.099.099');
//  $('.percent').mask('##0,00%', {reverse: true});
//  $('.clear-if-not-match').mask("00/00/0000", {clearIfNotMatch: true});
//  $('.placeholder').mask("00/00/0000", {placeholder: "__/__/____"});
//  $('.fallback').mask("00r00r0000", {
//      translation: {
//        'r': {
//          pattern: /[\/]/,
//          fallback: '/'
//        },
//        placeholder: "__/__/____"
//      }
//    });
//  $('.selectonfocus').mask("00/00/0000", {selectOnFocus: true});
//});

$(document).ready(function () {
    $("div.bhoechie-tab-menu>div.list-group>a").click(function (e) {
        e.preventDefault();
        $(this).siblings('a.active').removeClass("active");
        $(this).addClass("active");
        var index = $(this).index();
        $("div.bhoechie-tab>div.bhoechie-tab-content").removeClass("active");
        $("div.bhoechie-tab>div.bhoechie-tab-content").eq(index).addClass("active");
    });
});