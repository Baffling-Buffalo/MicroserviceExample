var form = (function () {

    function formInit() {

        var selectedRows = []; //array of selected row ids

        var localizer = {};
        this.localization = function (obj) {
            localizer = obj;
        }

        this.datatableInit = function (url) {

             $('#dtForms').DataTable({
                language: GetLocalizedLang(), //"_DataTableLocalize view"
                serverSide: true,
                processing: true,
                ajax: {
                    type: "POST",
                    url: url
                },
                responsive: {
                    details: {
                        type: 'column',
                        target: 0
                    }
                },
                rowId: 'Id',
                rowCallback: function (row, data) {
                    if ($.inArray(data.Id.toString(), selectedRows) !== -1) {
                        $(row).addClass('selected');
                    }
                },
                columnDefs: [
                    { targets: [0], data: null, className: 'control', defaultContent: '', orderable: false },
                    { targets: [1], data: 'Name' },
                    //{
                    //    targets: [1],
                    //    render: function (data, type, full, meta) {
                    //        return '<a href="/Form/ViewForm?report=' + full.FullPath+'">' + full.Name + '</a>';
                    //    }
                    //},
                    { targets: [2], data: 'FullPath' },
                    { targets: [3], data: 'Description' },
                    { targets: [4], data: 'ChkoutFolder' },
                    {
                        targets: [5],
                        className: 'text-center',
                        render: function (data, type, full, meta) {
                            var id = full.Id;
                            return '<a href="/Form/ViewForm?report=' + full.FullPath + '" class="btn btn-xs btn-default">' + localizer.display + '</a>';
                        }
                    }
                ]
            });
        }

        //select row in datatable
        $(document).on('click', '#dtForms tbody td', function () {
            //if cell(column) with index of 0 or 5 was clicked, then return
            //note: column with set 'visible: false' will be ignored
            var cellIndex = $(this).index();
            if (cellIndex == 0 || cellIndex == 5)
                return;

            //get row of td that was clicked
            var row = $(this).closest('tr');
            var id = row.attr("id");

            var index = $.inArray(id, selectedRows);

            if (index === -1) {
                selectedRows.push(id);
            } else {
                selectedRows.splice(index, 1);
            }

            row.toggleClass('selected');
        });

    } return new formInit();

})();





