$(document).ready(
    function () {
        $('#jqueryColorField').keyup(
            function (event) {
                try {
                    $('#jqueryColorSwatch').css("background-color", $('#jqueryColorField').val());
                }
                catch (err) {
                }
            }
        );

        $('#jqueryColorField').keyup();

        $(".datepicker").datepicker();
    }
);