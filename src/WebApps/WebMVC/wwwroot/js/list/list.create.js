var list = (function () {

    function listInit() {

        $("#btnCreate").click(function () {
            var parentId = null;
            if (typeof parentComboTree !== 'undefined')
                parentId = parentComboTree.getSelectedItemsId();
            $("#parentId").val(parentId);
            $("#createListForm").submit();
        })

    } return new listInit();

})();
