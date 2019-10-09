var contact = (function () {

    function contactInit() {
        $('[data-mask]').inputmask();

        $("#btnUpdate").click(function () {
            var selectedLists = [];
            if (typeof comboTreeLists !== 'undefined')
                selectedLists = comboTreeLists.getSelectedItemsId();
            $('#ListIds option').remove();
            $.each(selectedLists, function (i, item) {
                $('#ListIds').append($('<option>', { value: item }));
            });
            $('#ListIds option').prop('selected', true);
            $("#updateContactForm").submit();
        })

    } return new contactInit();

})();
