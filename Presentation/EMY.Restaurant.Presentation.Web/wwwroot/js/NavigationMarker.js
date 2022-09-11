  (function($) {
    "use strict";
    var path = window.location.href; 
      $("#layoutSidenav_nav .sb-sidenav a.nav-link").each(function () {
          if (path.includes(this.href)) {
                $(this).addClass("active");
            }
        });

})(jQuery);