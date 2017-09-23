function VideoCheckbox() {
    if ($('#video_checkbox').is(':checked')) {
        $("#video").removeAttr('disabled');
    }
    else {
        $("#video").attr('disabled', 'disabled');
        $("#video").val("");
    }
}

function ImageCheckbox() {
    if ($('#image_checkbox').is(':checked')) {
        $("#image").removeAttr('disabled');
    }
    else {
        $("#image").attr('disabled', 'disabled');
        $("#image").val("");
    }
}