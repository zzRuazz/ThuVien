(function ($) {
    $.fn.dataTable.ext.errMode = 'none';

    $.fn.MainTables = function (options) {
        return DataTableProvider.MainTables($(this), options);
    };

    $(document).on('click', 'input.checkbox-header', function () {
        let checked = $(this).prop('checked');
        let $checkboxRow = $(this).parents('.dataTables_wrapper').first().find('input.checkbox-row');
        $checkboxRow.prop('checked', checked);
    })

    $(document).on('click', 'input.checkbox-row', function () {
        let checked = $(this).prop('checked');
        let $checkboxHeader = $(this).parents('.dataTables_wrapper').first().find('input.checkbox-header').first();
        if (!checked) {
            $checkboxHeader.prop('checked', false);
        } else {
            let $table = $(this).parents('.dataTables_wrapper').first();
            let $checkboxRow = $table.find('input.checkbox-row');
            let $checkboxRowChecked = $table.find('input.checkbox-row:checked');

            if ($checkboxRow.length == $checkboxRowChecked.length) {
                $checkboxHeader.prop('checked', true);
            }
        }
    })
}(jQuery));

class DataTableProvider {
    static CreateTable($control, options) {
        if (options === undefined) {
            options = {};
        }

        let table = $control.DataTable(options);
        let id = $control.attr('id');

        if (options.searching == true) {
            $('#' + id + '_filter input').unbind();
            $('#' + id + '_filter input').bind('keyup', function (e) {
                if (e.keyCode == 13) {
                    table.search(this.value).draw();

                    setTimeout(function () {
                        $($.fn.dataTable.tables(true)).DataTable().columns.adjust();
                    }, 300);
                }
            });

            $('#' + id + '_filter input').bind('blur', function (e) {
                table.search($(this).val());
            });
        }

        $('#' + id + ' tbody').on('dblclick', 'tr', function () {
            var data = table.row(this).data();

            if (options.dblclick != undefined) {
                options.dblclick(data.Id);
            }
        });

        table.on('user-select', function (e, dt, type, cell, originalEvent) {
            if ($(cell.node()).parent().hasClass('selected')) {
                e.preventDefault();
            }
        });

        var curPosition = 0;

        table.on('preDraw', function () {
            curPosition = $control.parent().scrollTop();
        }).on('draw.dt', function () {
            $control.parent().scrollTop(curPosition);
        });

        return table;
    }

    static MainTables($control, options) {

        let defaultOptions = {
            "lengthMenu": [[50, 100, 200, -1], [50, 100, 200, "Tất cả"]],
            "scrollX": true,
            "processing": true,
            "scrollCollapse": true,
            "serverSide": true,
            "searching": false,
            "ordering": false,
            "info": false,
            "paging": true,
            "bFilter": true,
            "select": 'single',
            "language": {
                "sProcessing": "Đang xử lý...",
                "sLengthMenu": "Xem _MENU_ mục",
                "sZeroRecords": "Không tìm thấy dòng nào phù hợp",
                "sInfo": "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ mục",
                "sInfoEmpty": "Đang xem 0 đến 0 trong tổng số 0 mục",
                "sInfoFiltered": "(được lọc từ _MAX_ mục)",
                "sInfoPostFix": "",
                "sSearch": "Tìm:",
                "sUrl": "",
                "oPaginate": {
                    "sFirst": "Đầu",
                    "sPrevious": "Trước",
                    "sNext": "Tiếp",
                    "sLast": "Cuối"
                },
                "select": {
                    "rows": ""
                }
            },
            "createdRow": function (row, data, dataIndex) {

            },
            "ajax": {
                "type": "POST"
            },
        };

        if (options !== undefined) {
            merge(defaultOptions, options);
        }

        if (defaultOptions.serverSide == true) {
            if (defaultOptions.ajax === undefined) {
                defaultOptions.ajax = {};
            }

            if (defaultOptions.ajax.type === undefined) {
                defaultOptions.ajax.type = "POST";
            }

            if (defaultOptions.ajax.dataType === undefined) {
                defaultOptions.ajax.dataType = "json";
            }

            defaultOptions.ajax.dataFilter = function (res) {
                res = JSON.parse(res);
                let dataObj = {};
                dataObj = res.Data;
                let totalRecords = res.PaginationCount;
                let result = {};
                result.data = dataObj;
                result.recordsFiltered = totalRecords;
                result.recordsTotal = totalRecords;
                return JSON.stringify(result);
            }
        }

        return DataTableProvider.CreateTable($control, defaultOptions);
    }
}