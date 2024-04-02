class AjaxProvider {
    static Get(ajaxOptions) {
        if (ajaxOptions === undefined)
            throw "Ajax options is required.";

        ajaxOptions.method = "GET";

        let hasLoading = ajaxOptions.hasLoading;
        if (hasLoading === undefined)
            hasLoading === true;
        ajaxOptions.beforeSend = function () {
            if (hasLoading)
                PageProvider.ShowLoading();
        };

        //ajaxOptions.complete = function () {
        //    if (hasLoading) {
        //        PageProvider.HideLoading();
        //    }
        //};

        return AjaxProvider.Ajax(ajaxOptions);
    }
    static GetSync(ajaxOptions) {
        if (ajaxOptions === undefined)
            throw "Ajax options is required.";

        ajaxOptions.method = "GET";
        ajaxOptions.async = false;
        return AjaxProvider.Ajax(ajaxOptions);
    }

    static Post(ajaxOptions) {
        if (ajaxOptions === undefined)
            throw "Ajax options is required.";

        ajaxOptions.method = "POST";
        return AjaxProvider.Ajax(ajaxOptions);
    }
    static PostSync(ajaxOptions) {
        if (ajaxOptions === undefined)
            throw "Ajax options is required.";

        ajaxOptions.method = "POST";
        ajaxOptions.async = false;
        return AjaxProvider.Ajax(ajaxOptions);
    }

    static PostFormData(ajaxOptions) {
        if (ajaxOptions === undefined)
            throw "Ajax options is required.";

        ajaxOptions.method = "POST";
        ajaxOptions.contentType = false;
        ajaxOptions.processData = false;
        return AjaxProvider.Ajax(ajaxOptions);
    }
    static PostFormDataSync(ajaxOptions) {
        if (ajaxOptions === undefined)
            throw "Ajax options is required.";

        ajaxOptions.method = "POST";
        ajaxOptions.async = false;
        ajaxOptions.contentType = false;
        ajaxOptions.processData = false;
        return AjaxProvider.Ajax(ajaxOptions);
    }

    static Ajax(ajaxOptions) {
        if (ajaxOptions === undefined)
            throw "Ajax options is required.";

        if (ajaxOptions.hasLoading === undefined)
            ajaxOptions.hasLoading = true;

        ajaxOptions.beforeSend = function () {
            if (ajaxOptions.hasLoading)
                PageProvider.ShowLoading();

            let button = ajaxOptions.button;
            if (button !== undefined) {
                PageProvider.LoadButtonElement(button);
            }
        };



        let successFn = ajaxOptions.success;

        //ajaxOptions.success = async function (res) {
        //await successFn(res);
        //if (ajaxOptions.hasLoading)
        //  PageProvider.HideLoading();
        //};
        ajaxOptions.complete = function () {
            if (ajaxOptions.hasLoading)
                PageProvider.HideLoading();

            let button = ajaxOptions.button;
            if (button !== undefined) {
                PageProvider.UnloadButtonElement(button);
            }
        };

        return $.ajax(ajaxOptions);
    }

    static Inits() {
        $(function () {
            $(document).ajaxError(function (event, jqXHR, ajaxSettings, thrownError) {
                if (jqXHR.responseText.includes("A potentially dangerous Request")) {
                    Message.ShowErrorMessage("Dữ liệu chứa các kí tự không hợp lệ");
                } else {
                    if (jqXHR.status === 401) {
                        var d = $.parseJSON(jqXHR.responseText);
                        Message.ShowErrorMessage("Phiên làm việc của bạn đã hết hạn, đăng nhập lại");
                        window.location.href = d.LoginUrl;
                    }
                    else if (jqXHR.status === 403) {
                        Message.ShowErrorMessage("Bạn không có quyền truy cập");
                    }
                    else {
                        Message.ShowErrorMessage("Hệ thống bị lỗi, vui lòng thử lại sau");
                    }
                }
                PageProvider.HideLoading();
            });
            $(document).ajaxComplete(function () {
                FormProvider.BindValidate();
                FormProvider.AppendRequiredToControl();
            });
        });
    }
}