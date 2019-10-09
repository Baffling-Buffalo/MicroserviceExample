var contact = (function () {

    function contactInit() {
        $('[data-mask]').inputmask();

        $("#btnCreate").click(function () {
            var selectedLists = [];
            if (typeof comboTreeLists !== 'undefined')
                selectedLists = comboTreeLists.getSelectedItemsId();
            $('#ListIds option').remove();
            $.each(selectedLists, function (i, item) {
                $('#ListIds').append($('<option>', { value: item }));
            });
            $('#ListIds option').prop('selected', true);
            $("#createContactForm").submit();
        });

        $("#AllowLogin").click(function () {
            if ($("#AllowLogin").is(':checked')) {
                $(".allowLogin").prop('disabled', false);
                $("#Username").focus();
            }
            else {
                $(".allowLogin").val('');
                $(".allowLogin").prop('disabled', true);
                $("#Username").focus();
            }
        });

    } return new contactInit();

})();
