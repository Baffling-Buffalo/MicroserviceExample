var contact = (function () {

    function contactInit() {

        var contactsTable; //contacts table obj
        //FOR DATATABLE DATA
        var active = true; //active filter on datatable
        var listIds = []; //lists filter on datatable
        var selectedRows = []; //array of selected row ids

        var localizer = {};
        this.localization = function (obj) {
            localizer = obj;
        }

        hasClaim = {};
        this.claims = function (obj) {
            hasClaim = obj;
        }

        this.datatableInit = function (controllerURL) {

            contactsTable = $('#dtContacts').DataTable({
                language: GetLocalizedLang(), //"_DataTableLocalize view"
                serverSide: true,
                processing: true,
                ajax: {
                    type: "POST",
                    url: controllerURL,
                    data: function (d) {
                        if (listIds.length > 0)
                            d.listIds = listIds;
                        d.active = active;
                        return d;
                    },
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
                    { targets: [1], data: 'FirstName' },
                    { targets: [2], data: 'LastName' },
                    { targets: [3], data: 'Email' },
                    { targets: [4], data: 'Phone' },
                    { targets: [5], data: 'NumberOfLists' },
                    { targets: [6], data: 'Status' },
                    {
                        targets: [7],
                        className: 'text-center',
                        render: function (data, type, full, meta) {
                            var id = full.Id;
                            if (hasClaim.update == "True") {
                                return '<a href="/Contact/Update?id=' + id + '" class="btn btn-xs btn-default">' + localizer.update + '</a> <a href="/Contact/Details?id=' + id + '" class="btn btn-xs btn-default">' + localizer.details + '</a>';
                            } else {
                                return '<a href="/Contact/Details?id=' + id + '" class="btn btn-xs btn-default">' + localizer.details + '</a>';
                            }
                        }
                    }
                ]
            });
        }

        this.datatableRefresh = function () {
            if (typeof listTreeFilter !== 'undefined')
                listIds = listTreeFilter.getSelectedItemsId();
            active = $("#activeContactsSwitch").is(':checked');

            contactsTable.ajax.reload();
        }

        //select row in datatable
        $(document).on('click', '#dtContacts tbody td', function () {
            //if cell(column) with index of 0 or 7 was clicked, then return
            //note: column with set 'visible: false' will be ignored
            var cellIndex = $(this).index();
            if (cellIndex == 0 || cellIndex == 7)
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

        $("#btnActivate").click(function () {
            if (selectedRows.length > 0) {
                $('#activateContactsModal').modal('show');
            } else {
                toastr.warning(localizer.toastr.selectAtLeastOneContactMessage);
            }
        });

        $("#btnDeactivate").click(function () {
            if (selectedRows.length > 0) {
                $('#deactivateContactsModal').modal('show');
            } else {
                toastr.warning(localizer.toastr.selectAtLeastOneContactMessage);
            }
        });

        $("#btnDelete").click(function () {
            if (selectedRows.length > 0) {
                $('#deleteContactsModal').modal('show');
            } else {
                toastr.warning(localizer.toastr.selectAtLeastOneContactMessage);
            }
        });

        $(".getContactsBeforeSubmit").submit(function (eventObj) {
            if (selectedRows.length > 0) {
                for (var i = 0; i < selectedRows.length; i++) {
                    $(this).append(`<input type="hidden" name="ContactIds" value="${selectedRows[i]}" /> `);
                }
                return true;
            } else {
                toastr.warning(localizer.toastr.selectAtLeastOneContactMessage);
                return false;
            }
        });

    } return new contactInit();

})();





