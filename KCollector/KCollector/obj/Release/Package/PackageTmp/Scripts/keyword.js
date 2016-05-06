 $(function () {
     $("#btnSearch").on('click', function () {
            $.ajax({
                type: "get",
                dataType: "jsonp",
                url: "http://192.168.100.75/kc/api/KeyWord?word=" + $("#txt_1_value1").val(),
            });
        });
    });