$(document).ready(function () {

    $('.subscribeMailForm').submit(function (e) {

        var currComment = JSON.stringify({
            'FullName': $(this).find('#FullName').val(),
            'Email': $(this).find('#Email').val()
        });

        $.ajax({
            url: '/subscribe/register',
            type: 'POST',
            contentType: 'application/json',
            data: currComment
        }).done(function (response) {

            $("#status").text(response);
            $('.subscribeMailForm input[type=text]').val('');
            $('.subscribeMailForm input[type=email]').val('');

        });
        e.preventDefault();
    });
});