var contact = (function () {

    function contactInit() {
        //Initialize Select2 Elements
        $('.select2').select2();

        $("#btnRemoveFromLists").click(function () {
            var selectedListIds = [];
            if (typeof listTree !== 'undefined')
                selectedListIds = listTree.getSelectedItemsId();
            $('#ListIds option').remove();
            $.each(selectedListIds, function (i, item) {
                $('#ListIds').append($('<option>', { value: item }));
            });
            $('#ListIds option').prop('selected', true);
            $("#removeContactsFromListsForm").submit();
        })

    } return new contactInit();

})();
