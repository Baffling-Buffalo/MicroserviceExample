//lists table obj
var listsTable;

//array of selected row ids
var parentId = null;
var selectedRows = [];

//FOR DATATABLE DATA
var listIds = []; //lists filter on datatable

function datatableInit(controllerURL) {

    //***** CONTACTS DATATABLE SERVER SIDE *****
    listsTable = $('#dtLists').DataTable({
        serverSide: true,
        processing: true,
        ajax: {
            type: "POST",
            url: controllerURL,
            data: function (d) {
                if (parentId != null)
                    d.parentId = parentId;
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
            { targets: [1], data: 'Name' },
            { targets: [2], data: 'Description' },
            { targets: [3], data: 'NumberOfChildren' },
            { targets: [4], data: 'ParentName' },
            { targets: [5], data: 'NumberOfContacts' },
            {
                targets: [6],
                className: 'text-center',
                render: function (data, type, full, meta) {
                    var id = full.Id;
                    return '<a href="/List/Update?id=' + id + '" class="btn btn-xs btn-default">Update</a> | <a href="/List/Details?id=' + id +'" class="btn btn-xs btn-default">Details</a>';
                }
            }
        ]
    });

    //select row in datatable
    $('#dtLists tbody').on('click', 'td', function () {
        //if cell(column) with index of 0 or 5 was clicked, then return
        //note: column with set 'visible: false' will be ignored
        var cellIndex = $(this).index();
        if (cellIndex == 0 || cellIndex == 6)
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

}



$(".getListsBeforeSubmit").submit(function (eventObj) {
    if (selectedRows.length > 0) {
        for (var i = 0; i < selectedRows.length; i++) {
            $(this).append(`<input type="hidden" name="ListIds" value="${selectedRows[i]}" /> `);
        }
        return true;
    } else {
        toastr.warning("Please select at least one list for this action");
        return false;
    }

});

//Refresh datatable
function RefreshTable() {
    parentId = parentListTreeFilter.getSelectedItemsId();
    listsTable.ajax.reload();
}

function OpenModal(action) {
    if (selectedRows.length > 0) {
        $('#' + action + 'ListsModal').modal('show');
    } else {
        toastr.warning("Please select at least one list for this action");
    }
}







