var list = (function () {

    function listInit() {

        this.datatableInit = function (url, id) {
            //***** CONTACTS DATATABLE *****
            $('#dtContacts').DataTable({
                serverSide: true,
                processing: true,
                ajax: {
                    type: "POST",
                    url: url,
                    data: function (d) {
                        d.listIds = id;
                        return d;
                    },
                },
                responsive: {
                    details: {
                        type: 'column',
                        target: 0
                    }
                },
                columnDefs: [
                    { targets: [0], data: null, className: 'control', defaultContent: '', orderable: false },
                    { targets: [1], data: 'FirstName' },
                    { targets: [2], data: 'LastName' },
                    { targets: [3], data: 'Email' },
                    { targets: [4], data: 'Status' }
                ]
            });
        }

    } return new listInit();

})();
