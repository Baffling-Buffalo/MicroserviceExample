var group = (function () {

    function groupInit() {

        $("#btnUpdate").click(function () {
            var parentId = null;
            if (typeof parentComboTree !== 'undefined')
                parentId = parentComboTree.getSelectedItemsId();
            $("#parentId").val(parentId);
            $("#updateGroupForm").submit();
        })

    } return new groupInit();

})();
