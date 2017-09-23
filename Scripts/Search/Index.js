$(document).ready(function () {
    $(".forms").hide();
    $("#warning").hide();
    $("#posts").show();

    $("#formSelector").change(function () {
        $(".forms").hide();
        $("#warning").hide();

        var option = $(this).find('option:selected').val();
        $("#" + option).show();
    });
});

function SeniorityCheckbox() {
    if ($('#seniority_checkbox').is(':checked')) {
        $("#seniority").removeAttr('disabled');
        $("#volume").val($("#seniority").val());
    }
    else {
        $("#seniority").attr('disabled', 'disabled');
        $("#volume").val("");
    }
}

function outputUpdate(vol) {
    document.querySelector('#volume').value = vol;
}

function ValidateInput(className, allOrAny) {
    var isEmpty = true;
    var isFull = true;

    $("." + className).each(function (index, currInput) {
        // For any search criteria - runns on all of them, untill one is not empty.
        if ((allOrAny == "any") && ($(currInput).val() != "") && (isEmpty)) {
            isEmpty = false;
        }
            // For all search criteria - runns on all of them, and validates the each one is not empty.
        else if ((allOrAny == "all") && ($(currInput).val() == "") && (isFull)) {
            isFull = false;
        }
    });

    if ((allOrAny == "any") && (isEmpty)) {
        $("#warning").text("Please fill in at least one search criteria.")
        $("#warning").show();
    }
    else if ((allOrAny == "all") && (!isFull)) {
        $("#warning").text("Please fill in all search criterias.")
        $("#warning").show();
    }

    return (((allOrAny == "any") && (!isEmpty)) || ((allOrAny == "all") && (isFull)));
}