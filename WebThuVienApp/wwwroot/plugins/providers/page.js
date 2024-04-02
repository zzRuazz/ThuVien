class PageProvider {
    static CopyToClipboard(element) {
        element.select();
        document.execCommand("copy");
    }

    static CopyTextToClipboard(text) {
        if (!navigator.clipboard) {
            fallbackCopyTextToClipboard(text);
            return true;
        }
        navigator.clipboard.writeText(text).then(function () {
            return true;
        }, function (err) {
            return false;
        });
    }

    static FallbackCopyTextToClipboard(text) {
        var textArea = document.createElement("textarea");
        textArea.value = text;
        document.body.appendChild(textArea);
        textArea.focus();
        textArea.select();

        var successful;
        try {
            successful = document.execCommand('copy');
        } catch (err) {
            console.error('Fallback: Oops, unable to copy', err);
        }

        document.body.removeChild(textArea);
        return successful;
    }

    //LOADING
    static ShowLoading() {
        let $loadingContainer = $("[data-loading='1']");
        if ($loadingContainer.length === 0) {
            $("body").append("<div class='loading-overlay' data-loading='1'><span><i class='fa fa-spinner fa-spin'></i></span></div>");
            $("[data-loading='1']").show();
        } else {
            $loadingContainer.show();
        }
    }

    static HideLoading() {
        $("[data-loading='1']").remove();
    }

    static LoadButtonElement(button) {
        let $button = $(button);
        if ($button.length > 0) {
            $button.prepend(`<span class="spinner"><i class='fa fa-spinner fa-spin'></i></span> `);
            $button.attr('disabled', 'disabled');
        }
    }

    static PerformClick(element) {
        if (element && document.createEvent) {
            var evt = document.createEvent("MouseEvents");
            evt.initEvent("click", true, false);
            element.dispatchEvent(evt);
        }
    }

    static UnloadButtonElement(button) {
        let $button = $(button);
        if ($button.length > 0) {
            $button.find('span.spinner').remove();
            $button.removeAttr('disabled');
        }
    }

    //static PushState() {
    //    let title = $(document).find('title').html();
    //    let queryString = "";
    //    history.pushState({}, title, location.pathname + )
    //}
    //END LOADING

    static GetQueryString(key) {
        return new URLSearchParams(window.location.search).get(key);
    }
}

$(function () {
    AjaxProvider.Inits();

    //Remove date validation in mvc
    $.validator.methods.date = function (value, element) {
        return true;
    };
    $.validator.methods.number = function (value, element) {
        return true;
    };

    $('.modal').on('shown.bs.modal', function () {
        let $modal = $(this);
        let $controls = $modal.find('[data-control]:visible');
        if ($controls.length > 0) {
            $controls.first().focus();
        }
    });

    $(document).on('shown.bs.modal', '.modal', function () {
        $($.fn.dataTable.tables(true)).DataTable().columns.adjust();
    });

    $(document).on('hidden.bs.modal', '.modal', function () {
        //$('.modal:visible').length && $(document.body).addClass('modal-open');

        //if ($('.modal:visible').length == 0)
        //    $('body').css({ 'overflow-y': 'auto' });

        //$($.fn.dataTable.tables(true)).DataTable().columns.adjust();
    });

    $(document).on('shown.bs.tab', '[data-toggle="tab"]', function () {
        var $a = $(this);
        let id = $a.attr('href');
        $.each($(id).find('table'), function (i, table) {
            if ($.fn.DataTable.isDataTable($(table))) {
                $(table).DataTable().columns.adjust();
            }
        })
    });

});