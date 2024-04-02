class Select2Provider {
    static Inits() {
        var $controls = $('[data-plugin="select2"].init');
        if ($controls.length > 0) {
            $.each($controls, function (index, control) {
                let $control = $(control);
                $control.select2();
                $control.removeClass('init');
            });
        }
    }
}

$(function () {
    Select2Provider.Inits();
});

$(document).ajaxComplete(function () {
    Select2Provider.Inits();
});