var list = (function () {

    function listInit() {

        $("#btnUpdate").click(function () {
            var parentId = null;
            if (typeof parentComboTree !== 'undefined')
                parentId = parentComboTree.getSelectedItemsId();
            $("#parentId").val(parentId);
            $("#updateListForm").submit();
        })
    } return new listInit();

})();
