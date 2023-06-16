(function ($) {

    const pathHandler = () => {

        $('a[url-push]').unbind('click').bind('click', function (e) {
            e.preventDefault();

            let $href = $(this).attr('href');

            if ($href === window.location.pathname) {
                return;
            }

            window.history.pushState({}, "", $href);
            locationHandler();
        });
    }

    const GoPath = ($href) => {

        if ($href === window.location.pathname) {
            return;
        }

        window.history.pushState({}, "", $href);
        locationHandler();
    }

    const RefreshPath = () => {

        window.history.pushState({}, "", window.location.pathname);
        locationHandler();
    }

    const locationHandler = async () => {

        let $path = window.location.pathname + '?hash';

        if ($path.length == 0) {
            $path = "/";
        }

        const html = await fetch($path).then((response) => {

            if (response.ok) {
                return response.text();
            }
            else {

                if (response.status === 404) {
                    return page404();
                }

                throw new Error(response.status + " Failed Fetch");
            }

        }).catch((e) => {
            console.log('Connection error', e);
            toastr.error(e.message);
        });

        $('#render-body').html(html);

        $.MenuItemActive();

        pathHandler();

        const arrTitle = document.title.split('|');
        const companyName = arrTitle.length >= 2 ? ' | ' + arrTitle[1] : '';
        const title = $('[data-value="title"]').text();

        document.title = `${title} ${companyName}`;

        $('body,html').animate({ scrollTop: 0 }, 'slow');

        $.AjaxPageInit();
    };

    const page404 = () => {

        let get = $.get('/404/?hash', async (html) => {

            return html;
        });

        return get;
    }

    window.onpopstate = locationHandler;

    pathHandler();

    $.extend({
        GoPath,
        RefreshPath
    });

})(jQuery)