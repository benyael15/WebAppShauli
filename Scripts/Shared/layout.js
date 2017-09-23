$(document).ready(function () {
    var canvas = document.getElementById("canvas");
    var ctx = canvas.getContext("2d");
    ctx.font = "24px Comic Sans MS";
    ctx.fillStyle = "grey";
    ctx.textAlign = "left";
    ctx.fillText("Welcome to Shauli blog! Or blue or purple", 200, canvas.height);

    $.ajax({
        url: '/statistics/GetPopularComments',
        type: 'POST',
        contentType: 'application/json'
    }).done(function (posts) {

        jQuery.each(posts, function (index, currPost) {
            var newLine = '<li><a href="/#post ' + currPost.Id + '" >' + currPost.Post + '</a></li>';
            $('#popularList').append(newLine);
        });

    });

});