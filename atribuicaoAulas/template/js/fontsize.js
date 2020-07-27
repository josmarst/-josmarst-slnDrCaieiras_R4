var fontsize = {
    3: "/template/styles/css/fontlarge.css",
    2: "/template/styles/css/fontnormal.css",
    1: "/template/styles/css/fontmini.css",
}

 
function AlterarFonte(obj) {
 
 
    if ($(obj).attr('data-theme') == "fontlarge") {
 
        if (localStorage.fontAcessibilidade == null) {
            localStorage.fontAcessibilidade = fontsize[3];
        } else {
            if (localStorage.fontAcessibilidade == fontsize[1]) {
                localStorage.fontAcessibilidade = fontsize[2];
            } else {
                localStorage.fontAcessibilidade = fontsize[3];
            }
        }
 
    }
    else {
        if (localStorage.fontAcessibilidade == null) {
            localStorage.fontAcessibilidade = fontsize[1];
        } else {
            if (localStorage.fontAcessibilidade == fontsize[3]) {
                localStorage.fontAcessibilidade = fontsize[2];
            } else {
                localStorage.fontAcessibilidade = fontsize[1];
            }
        }
    }
 
 
    $(".font").attr("href", localStorage.fontAcessibilidade);
}
 



//var fontsize = {
	//"fontnormal" : "styles/css/fontnormal.css", 
   // "fontlarge" : "styles/css/fontlarge.css",
   // "fontmini" : "styles/css/fontmini.css",  
  
//}
//$(function(){
   //var themesheet = $('<link href="'+fontsize['fontnormal']+'" rel="stylesheet" />'); 
   // themesheet.appendTo('head');
   // $('.theme-link').click(function(){
    //   var fonturl = fontsize[$(this).attr('data-theme')];  
       // themesheet.attr('href',fonturl);  
   // });
//});
