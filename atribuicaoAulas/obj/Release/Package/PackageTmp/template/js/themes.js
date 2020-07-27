
var themes = {
    "default": "styles/css/tema1.css",
    "blue" : "styles/css/tema2.css",
    "orange" : "styles/css/tema3.css",
  
}
$(function(){
   var themesheet = $('<link href="'+themes['default']+'" rel="stylesheet" />');
    themesheet.appendTo('head');
    $('.theme-link').click(function(){
       var themeurl = themes[$(this).attr('data-theme')];  
        themesheet.attr('href',themeurl); 
    }); 
});
