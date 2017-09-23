$(document).ready(function () {

    $('.addCommentForm').submit(function (e) {

        var currComment = JSON.stringify({
            'PostId': $(this).find('#PostId').val(),
            'Headline': $(this).find('#Headline').val(),
            'Author': $(this).find('#Author').val(),
            'Website': $(this).find('#Website').val(),
            'Content': $(this).find('#Content').val()
        });

        $.ajax({
            url: '/comments/create',
            type: 'POST',
            contentType: 'application/json',
            data: currComment
        }).done(function (response) {

            var reG = /-?\d+/;
            var m = reG.exec(response.Timestamp);
            var date = new Date(parseInt(m[0])).toLocaleString('fr-FR', { hour12: false });

            var newComment = "<article> " +
                                "<header>" +
                                    "<a href=\"" + response.Website + "\">" + response.Headline + " by " + response.Author + "</a> on <time>" + date + "</time>" +
                                 "</header>" +
                                 "<p>" + response.Content + "</p>" +
                            "</article>";

            $("section[id='comments " + response.PostId + "']").append(newComment);

            var currentCounter = parseInt($('label[id="commentCounter ' + response.PostId + '"]').text(), 10);
            currentCounter++;
            $('label[id="commentCounter ' + response.PostId + '"]').text(currentCounter);

            ClearCommentForm();
        });

        e.preventDefault();
    });

    function ClearCommentForm() {
        $('.addCommentForm input[type=text]').val('');
        $('.addCommentForm textarea').val('');
    }

});