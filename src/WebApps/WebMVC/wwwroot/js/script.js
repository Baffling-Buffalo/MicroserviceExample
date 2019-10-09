/////////////////////////////////////////////////////////////////////////
// Opening and activating tree navigations if any child link is active //
// remove navigation tree if empty (unauthorized actions)              //
/////////////////////////////////////////////////////////////////////////
$(".has-treeview").each(function () {
    if ($(this).find("li").length == 0) {
        $(this).remove();
        return;
    }
    $(this).has("a.active").addClass("menu-open").children("a").first().addClass("active");
})

//The back() method loads the previous URL in the history list.
function goBack() {
    window.history.back();
}


// Remove empty dropdowns(due to unauthorization)
$(".dropdown-menu").each(function () {
    if ($(this).find("li").not(".dropdown-divider").length == 0) {
        $(this).parent().remove();
        return;
    }
    $(".dropdown-divider").each(function () {
        if ($(this).next() == null || $(this).next().hasClass(".dropdown - divider"))
            $(this).remove();
    })
})
