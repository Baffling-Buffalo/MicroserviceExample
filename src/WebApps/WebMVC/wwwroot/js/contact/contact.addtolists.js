var contact = (function () {

    function contactInit() {

        $("#btnAddToLists").click(function () {
            var selectedListIds = [];
            if (typeof listTree !== 'undefined')
                selectedListIds = listTree.getSelectedItemsId();
            $('#ListIds option').remove();
            $.each(selectedListIds, function (i, item) {
                $('#ListIds').append($('<option>', { value: item }));
            });
            $('#ListIds option').prop('selected', true);
            $("#addContactsToListsForm").submit();
        })
    } return new contactInit();

})();
