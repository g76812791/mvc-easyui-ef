$(document).ready(function () {
    $("#btnSearch").bind('click', function () {
            $.ajax({
                type: "get",
                dataType: "jsonp",
                url: "http://localhost:22479/api/KeyWord?word=" + $("#txt_1_value1").val(),
            });
        });
    });