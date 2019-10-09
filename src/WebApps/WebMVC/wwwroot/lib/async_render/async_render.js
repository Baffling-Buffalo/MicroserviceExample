$(document).ready(function () {

    //Check URL
    var type = window.location.hash.substr(1);
    if (type != "") {
        makePageRequest();
    }
    //Set css
    $("[async_load]").css("cursor", "pointer");

    //Click event
    $("[async_load]").click(function (e) {
        makePageRequest()
    });

    //Make page request
    function makePageRequest() {
        var requestUrl = $("[async_load]").attr("async_load");
        $.ajax({
            type: "get",
            url: requestUrl,
            success: function (data, text) {
                $('async_render').html(data);
                window.history.pushState({ "html": data, "pageTitle": data.pageTitle }, "", "#" + requestUrl);
            },
            error: function (request, status, error) {
                alert(request.responseText);
            }
        });
    }

});