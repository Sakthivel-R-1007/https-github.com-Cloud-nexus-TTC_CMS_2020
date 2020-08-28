//<!-- Load include files -->
jQuery(document).ready(function($) {
	$('header').load('includes/header.html');	
	$('.footer').load('includes/footer.html');
	$('.latestArticle').load('includes/latest-articles.html');
	
	$('iframe').each(function(){
		var url = $(this).attr("src");
		var char = "?";
		if(url.indexOf("?") != -1){
			var char = "&";
		}
		$(this).attr("src",url+char+"wmode=transparent");
	});
	
	$(window).scroll(function () {
		var wValue = $(window).scrollTop();
		if(wValue > 200) { 
			$('.header .mainMenu').addClass('fixed');
		}else{
			$('.header .mainMenu').removeClass('fixed');
		}
		
		if(wValue > 250) { 
			$('.backtop .backTopBtn').css('bottom','60px')
		}else{
			$('.backtop .backTopBtn').css('bottom','-200px');
		}
	});	
});

function backToTop()
{
	$("html, body").animate({ scrollTop: 0 }, "slow");	
}

(function() {
    var link = document.querySelector("link[rel*='icon']") || document.createElement('link');
    link.type = 'image/x-icon';
    link.rel = 'shortcut icon';
    link.href = 'https://www.tampines.org.sg/images/favicon.ico';
    document.getElementsByTagName('head')[0].appendChild(link);
})();

<!---------------------------     GA     ---------------------------!>
(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
})(window,document,'script','//www.google-analytics.com/analytics.js','ga');

ga('create', 'UA-170832575-1', 'auto');
ga('require', 'displayfeatures');
ga('send', 'pageview');

<!--------------------     GA link tracking     -------------------!>
if (typeof jQuery != 'undefined') {
    jQuery(document).ready(function($) {
        var filetypes = /\.(zip|exe|pdf|doc*|xls*|ppt*|mp3)$/i;
        var baseHref = '';
        if (jQuery('base').attr('href') != undefined)
            baseHref = jQuery('base').attr('href');
        jQuery('a').each(function() {
            var href = jQuery(this).attr('href');
            if (href && (href.match(/^https?\:/i)) && (!href.match(document.domain))) {
                jQuery(this).click(function() {
                    var extLink = href.replace(/^https?\:\/\//i, '');
                    _gaq.push(['_trackEvent', 'External', 'Click', extLink]);
                    if (jQuery(this).attr('target') != undefined && jQuery(this).attr('target').toLowerCase() != '_blank') {
                        setTimeout(function() { location.href = href; }, 200);
                        return false;
                    }
                });
            }
            else if (href && href.match(/^mailto\:/i)) {
                jQuery(this).click(function() {
                    var mailLink = href.replace(/^mailto\:/i, '');
                    _gaq.push(['_trackEvent', 'Email', 'Click', mailLink]);
                });
            }
            else if (href && href.match(filetypes)) {
                jQuery(this).click(function() {
                    var extension = (/[.]/.exec(href)) ? /[^.]+$/.exec(href) : undefined;
                    var filePath = href;
                    _gaq.push(['_trackEvent', 'Download', 'Click-' + extension, filePath]);
                    if (jQuery(this).attr('target') != undefined && jQuery(this).attr('target').toLowerCase() != '_blank') {
                        setTimeout(function() { location.href = baseHref + href; }, 200);
                        return false;
                    }
                });
            }
        });
    });
} 