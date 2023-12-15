window.setDatePicker = function (id) {
    var options = {
        format: 'dd-mm-yyyy',
        autoHide : true
    };
    if (id !== undefined) {
        $(id).datepicker(options);
    }
    else {
        $('.date-picker').datepicker(options);
    }
}

window.getDatePicker = function (id) {
    return $(id).val();
}