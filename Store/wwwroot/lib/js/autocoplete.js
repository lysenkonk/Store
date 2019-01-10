//$(function () {
//    $("#category").autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: "@Url.Action('GetCategories', Admin)",
//                dataType: "json",
//                data: { search: $("#category").val() },
//                success: function (data) {
//                    response($.map(data, function (item) {
//                    }))
//                },
//                error: function()
//            })
//        }
//    })
//})