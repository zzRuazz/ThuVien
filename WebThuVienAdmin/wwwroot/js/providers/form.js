class FormProvider {
    //generate JS Object from controls
    static BindToFormData($form) {
        let formData = new FormData();
        $.each($form.find("[name]"), function (index, control) {
            let $control = $(control);
            let name = $control.attr("name");
            let value = $control.val();

            //HTML
            if ($control.attr("data-plugin") === "summernote") {
                formData.append(name, $control.summernote('code'));
            }
            else if ($control.attr("data-plugin") === "ckeditor") {
                formData.append(name, CKEDITOR.instances[name].getData());
            }
            //DATE AND DATETIME
            else if ($control.attr("data-type") === "date" || $control.attr("data-type") === "datetime") {
                if (value !== '' && value !== null && value !== undefined)
                    formData.append(name, DateTimeProvider.ConvertToServerDateTime(value));
                else {
                    //formData.append(name, null);
                }
            }
            //NUMBER
            else if ($control.attr("data-input-type") === "number") {
                if (value !== '' && value !== null && value !== undefined) {
                    formData.append(name, value.replace(/\,/g, ""));
                } else {
                    formData.append(name, value);
                }
            }
            //CHECKBOX
            else if ($control.attr("type") === 'checkbox') {
                formData.append(name, $control.prop('checked'));
            }
            //FILE(IMAGE AND FILE)
            else if ($control.attr("data-file") === "1") {
                //let files = $control[0].files;
                let myDropzone = Dropzone.forElement("#" + name);
                let files = myDropzone.files;
                if (files.length > 0 && files[0] !== undefined) {
                    formData.append(name + ".File", files[0]);
                }
            }
            //FILES(IMAGES AND FILES)
            else if ($control.attr("data-file") === "2") {
                let myDropzone = Dropzone.forElement("#" + name);
                let files = myDropzone.files;
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        formData.append(name + "[" + i + "].File", files[i]);
                    }
                }
            }
            //TEXTBOX, SELECT OR MULTIPLE SELECT
            else {
                if ($control.is("select") && $control.attr("multiple") === "multiple") {
                    $.each(value, function (index, valueItem) {
                        formData.append(name + "[" + index + "]", valueItem);
                    });
                } else {
                    formData.append(name, value);
                }
            }
        });

        return formData;
    }

    //generate FormData from controls
    static BindToModel($form) {
        let result = {};
        $.each($form.find("[name]"), function (index, control) {
            let $control = $(control);
            let name = $control.attr("name");
            let value = $control.val();
            //HTML
            if ($control.attr("data-plugin") === "summernote") {
                result[name] = $control.summernote().code();
            }
            else if ($control.attr("data-plugin") === "ckeditor") {
                result[name] = CKEDITOR.instances[name].getData();;
            }
            //DATE AND DATETIME
            else if ($control.attr("data-type") === "date" || $control.attr("data-type") === "datetime") {
                if (value !== '' && value !== null && value !== undefined)
                    result[name] = DateTimeProvider.ConvertToServerDateTime(value);
                else {
                    result[name] = null;
                }
            }
            //NUMBER
            else if ($control.attr("data-input-type") === "number") {
                if (value !== '' && value !== null && value !== undefined) {
                    result[name] = value.replace(/\,/g, "");
                } else {
                    result[name] = value;
                }
            }
            //CHECKBOX
            else if ($control.attr("type") === 'checkbox') {
                result[name] = $control.prop('checked');
            }
            //TEXTBOX, SELECT OR MULTIPLE SELECT
            else {
                result[name] = value;
            }
        });

        return result;
    }

    //bind value from model to HTML controls
    static bindToForm($form, model) {
        if (model === undefined || model === null)
            model = {};
        $.each($form.find("[name]"), function (index, control) {
            let $control = $(control);
            let name = $control.attr("name");

            let value = model[name];

            if ($control.attr("data-plugin") === "summernote") {
                $control.summernote('code', value);
            } else if ($control.is("select") && $control.attr("multiple")) {
                let $hiddenControlFor = $control.prev("input[type='hidden']");
                if ($hiddenControlFor.length > 0) {
                    let value = JSON.parse($hiddenControlFor.first().val());
                    $control.val(value);
                }
                $control.change();
            } else {
                $control.val(value);
            }
        });
    }

    static BindValidate() {
        var $form = $("form");
        if ($form.length > 0) {
            $form.unbind();
            $form.data("validator", null);

            $.validator.unobtrusive.parse(document);
            // Re add validation with changes
            let unobtrusiveValidation = $form.data("unobtrusiveValidation");
            if (unobtrusiveValidation !== undefined) {
                $form.validate(unobtrusiveValidation.options);
            }
        }
    }

    static CheckBox(name, text, value = "", htmlAttributes = "") {
        return `<label class="rememberme mt-checkbox mt-checkbox-outline">
                    <input class="icheck" type="checkbox" name="${name}" data-value="${value}" ${htmlAttributes}> ${text}
                    <span></span>
                </label>`;
    }

    //generate options in select HTML controls
    static GenerateSelectHTML($select, data, bindValue = false, replace = true, emptyText, disabledEmptyText = true) {
        let optionContent = "";
        if (emptyText !== undefined && emptyText !== "") {
            if (disabledEmptyText) {
                optionContent = "<option value='' disabled='disabled'>" + emptyText + "</option>";
            } else {
                optionContent = "<option value=''>" + emptyText + "</option>";
            }
        }
        $.each(data, function (index, dataItem) {
            optionContent += `<option value='${dataItem.Value}'>${dataItem.Text}</option>`;
        });
        if (replace) {
            $select.html(optionContent);
        } else {
            $select.append(optionContent);
        }

        if (bindValue) {
            FormProvider.BindSelectValue($select);
        }
    }

    static GenerateFormSelectHTML($form) {
        return new Promise(function (resolve, reject) {
            $form.find("a[data-selector='cms-select-refresh']").click();
            resolve();
        });
    }

    static BindMultipleSelectValue($select) {
        $select.val(JSON.parse($select.prev('input').first().val()));
    }

    static BindSelectValue($select) {
        $select.val($select.prev('input').first().val());
    }

    static GetTextareCaret(el) {
        if (el.selectionStart) {
            return el.selectionStart;
        } else if (document.selection) {
            el.focus();
            var r = document.selection.createRange();
            if (r === null) {
                return 0;
            }
            var re = el.createTextRange(), rc = re.duplicate();
            re.moveToBookmark(r.getBookmark());
            rc.setEndPoint('EndToStart', re);
            return rc.text.length;
        }
        return 0;
    }

    static Inits() {
        $('body').on("submit", "form", function () {
            return false;
        });

        //Submit form when press enter in input
        $('body').on('keyup', '[data-control="1"]:not(textarea)', function (event) {
            if (event.keyCode === 13) {
                event.preventDefault();
                let $form = $(this).parents('form');
                if ($form.length > 0) {
                    let $submitButton = $form.first().find('[data-submit="1"]');
                    if ($submitButton.length) {
                        $submitButton.first().click();
                    }
                }
            }
        });

        //Shift Enter and Enter event in textarea
        $('body').on('keyup', 'textarea[data-control="1"]:not(.note-codable):not([data-plugin="summernote"])', function (event) {
            if (event.keyCode === 13) {
                var content = this.value;
                var caret = FormProvider.GetTextareCaret(this);
                if (event.shiftKey) {
                    this.value = content.substring(0, caret - 1) + "\n" + content.substring(caret, content.length);
                    event.stopPropagation();
                } else {
                    this.value = content.substring(0, caret - 1) + content.substring(caret, content.length);
                    let $form = $(this).parents('form');
                    if ($form.length > 0) {
                        let $submitButton = $form.first().find('button[data-submit="1"]');
                        if ($submitButton.length) {
                            $submitButton.first().click();
                        }
                    }
                }
            }
        });

        //Refresh select control when click refresh button
        $('body').on('click', "a[data-selector='cms-select-refresh']", function () {
            let $this = $(this);
            let $form = $this.parents("form").first();
            let $select = $form.find('#' + $this.prev('label').attr("for"));
            if ($select !== undefined) {
                AjaxProvider.Get({
                    url: $this.attr("data-url"),
                    success: function (response) {
                        if (response.status === 200) {
                            let selectValue = $select.val();
                            FormProvider.GenerateSelectHTML($select, response.data);

                            let $input = $select.prev("input[type='hidden']");
                            if (selectValue === null || selectValue === "" || selectValue === undefined)
                                selectValue = $input.val();
                            else {
                                $input.val(selectValue);
                            }

                            if (selectValue !== null && selectValue !== "" && selectValue !== undefined) {
                                if ($select.attr("multiple") === "multiple") {
                                    FormProvider.BindMultipleSelectValue($select);
                                } else {
                                    FormProvider.BindSelectValue($select);
                                }

                                if ($select.find('option[value="' + selectValue + '"]').length > 0) {
                                    $select.val(selectValue);
                                } else {
                                    $select.val($($select.find('option')[0]).val());
                                }
                                $select.change();
                            }
                        }
                    }
                });
            }
        });

        //Validate control when input
        //$('body').on('keyup', '[data-control="1"]:not([data-plugin="summernote"])', function () {
        //    FormProvider.ValidateControl($(this));
        //});

        $('body').on('input change keyup focusout', '[data-control="1"]:not([data-plugin="summernote"])', function () {
            FormProvider.ValidateControl($(this));
        });

        ////Validate control when focusout
        //$('body').on('focusout', '[data-control="1"]:not([data-plugin="summernote"])', function () {
        //    FormProvider.ValidateControl($(this));
        //});
        FormProvider.BindValidate();
        FormProvider.AppendRequiredToControl();
    }

    static ValidateControl($control) {
        if ($control.attr('data-file') || $control.valid()) {
            var $validatingControl = $control;

            var controlValue = $validatingControl.val();
            let controlName = $validatingControl.attr("name");

            let labelFor = $control.parents("form").first().find("label[for='" + controlName + "']");

            let errorMessages = [];
            var patt = new RegExp("<\/?[A-Za-z]+>?");
            if (patt.test(controlValue)) {
                errorMessages.push('Dữ liệu chứa kí tự không hợp lệ');
            }
            //Date and DateTime
            let dateValidate = $validatingControl.attr('data-type');
            if (dateValidate === "date" || dateValidate === "datetime") {
                if (controlValue !== '' && controlValue !== null) {
                    //Validate datetime format
                    if (!DateTimeProvider.ValidateDateTime(controlValue)) {
                        errorMessages.push('Định dạng không hợp lệ');
                    }
                }
            }

            //file dropzone
            let fileValidate = $validatingControl.attr('data-file');
            if (fileValidate) {
                let required = $validatingControl.attr('data-val-required');
                if (required) {
                    if ($validatingControl.find('.dropzone-items').first().children().length == 0) {
                        errorMessages.push(required);
                    }
                }
            }
            let $errorMessageContainer = $validatingControl.parents(".form-group").first().find(".field-validation-valid[data-valmsg-for='" + controlName + "'], .field-validation-error").first();
            if (errorMessages.length > 0) {
                var message = errorMessages.join("<br />");
                $errorMessageContainer.html(message);
                $errorMessageContainer.addClass("field-validation-error");
                return false;
            }
            else {
                $errorMessageContainer.html("");
                $errorMessageContainer.removeClass("field-validation-error");
                return true;
            }
        }
        return false;
    }

    static Validate($form) {
        var valid = true;
        if ($form.valid()) {

        } else {
            $form.find('.input-validation-error:visible').first().focus();
            valid = false;
        }

        var validatingControls = $form.find('[data-file]');
        if (validatingControls.length > 0) {
            $.each(validatingControls, function (index, validatingControl) {
                if (!FormProvider.ValidateControl($(validatingControl))) {
                    valid = false;
                }
            });
        }
        return valid;
    }

    static AppendRequiredToControl() {
        var controls = $('[data-val="true"]:not(.added-required-simple)');
        $.each(controls, function (index, control) {
            let $control = $(control);
            let dataValRequiredAttribute = $control.attr("data-val-required");
            if (dataValRequiredAttribute !== undefined && dataValRequiredAttribute !== "" && dataValRequiredAttribute !== null) {
                controls.parents("form").first().find('label[for="' + $control.attr("name") + '"]').append("<span style='color: red;margin-left: 2px;'>*</span>");
            }
            $control.addClass("added-required-simple");
        });
    }

    static CreateModal(id, html) {
        if ($('#' + id).length)
            $('#' + id).remove();
        $("body").append(html);
        let $modal = $('#' + id);
        return $modal;
    }

    static CreateSlug(str) {
        str = str.toLowerCase();
        str = str.replace(/(à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ)/g, 'a');
        str = str.replace(/(è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ)/g, 'e');
        str = str.replace(/(ì|í|ị|ỉ|ĩ)/g, 'i');
        str = str.replace(/(ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ)/g, 'o');
        str = str.replace(/(ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ)/g, 'u');
        str = str.replace(/(ỳ|ý|ỵ|ỷ|ỹ)/g, 'y');
        str = str.replace(/(đ)/g, 'd');
        str = str.replace(/([^0-9a-z-\s])/g, '');
        str = str.replace(/(\s+)/g, '-');
        str = str.replace(/-+/g, '-');
        str = str.replace(/^-+/g, '');
        str = str.replace(/-+$/g, '');
        return str;
    };

    static GetCheckedRowTable($table, showMessage = true) {
        let $checkboxRows = $table.find('input.checkbox-row');
        let result = [];
        $.each($checkboxRows, function (i, checkbox) {
            if ($(checkbox).prop('checked')) {
                result.push($(checkbox).val());
            }
        })

        if (result.length == 0 && showMessage) {
            Message.ShowErrorMessage("Không có dòng nào được chọn");
        }
        return result;
    }
}

$(function () {
    //$.validator.setDefaults({ ignore: ":hidden:not(#summernote),.note-editable.panel-body" });
    $.validator.setDefaults({ ignore: ".note-editor *" });
    FormProvider.Inits();
});