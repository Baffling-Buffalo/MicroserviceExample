var group = (function () {

    function groupInit() {
        $("#btnCreate").click(function () {
            var parentId = null;
            if (typeof parentComboTree !== 'undefined')
                parentId = parentComboTree.getSelectedItemsId();
            $("#parentId").val(parentId);
            $("#createGroupForm").submit();
        })

    } return new groupInit();

})();
