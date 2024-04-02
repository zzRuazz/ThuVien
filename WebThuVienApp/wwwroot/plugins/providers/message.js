toastr.options.closeButton = true;
toastr.options.closeButton = true;
toastr.options.timeOut = 5000;
class Message {
    static ShowErrorMessage(message) {
        Message.ShowToastrMessage(message, 'error');
    }

    static ShowInfoMessage(message) {
        Message.ShowToastrMessage(message, 'info');
    }

    static ShowSuccessMessage(message) {
        Message.ShowToastrMessage(message, 'success');
    }

    static ShowQuestionMessage(message) {
        Message.ShowToastrMessage(message, 'question');
    }

    static ShowWarningMessage(message) {
        Message.ShowToastrMessage(message, 'warning');
    }

    static ShowToastrMessage(message, type) {
        if (type === 'error') {
            toastr.error(message);
        } else if (type === 'success') {
            toastr.success(message);
        } else if (type === 'warning') {
            toastr.warning(message);
        } else if (type === 'info') {
            toastr.info(message);
        }
    }

    static ShowConfirmMessage(confirmOptions) {
        bootbox.dialog({
            message: confirmOptions.message,
            title: confirmOptions.title,
            buttons: {
                success: {
                    label: "Yes",
                    className: "btn-primary green-meadow",
                    callback: function () {
                        let yesCallback = confirmOptions.yes;
                        if (yesCallback !== undefined)
                            yesCallback();
                    }
                },
                danger: {
                    label: "No",
                    className: "btn-outline dark",
                    callback: function () {
                        let noCallback = confirmOptions.no;
                        if (noCallback !== undefined)
                            noCallback();
                    }
                }
            }
        });
    }

    static ShowDeleteConfirmMessage(confirmOptions) {
        Swal.fire({
            title: confirmOptions.message,
            text: confirmOptions.title,
            icon: "question",
            showCancelButton: true,
            confirmButtonText: "Yes",
            cancelButtonText: "No",
            customClass: {
                confirmButton: "btn btn-danger px-7",
                cancelButton: "btn btn-secondary px-7"
            }
        }).then(function (result) {
            if (result.value) {
                let yesCallback = confirmOptions.yes;
                if (yesCallback !== undefined)
                    yesCallback();
            } else if (result.dismiss === "cancel") {
                let noCallback = confirmOptions.no;
                if (noCallback !== undefined)
                    noCallback();
            }
        });
    }
}