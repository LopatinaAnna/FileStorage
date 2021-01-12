$('.choose-files').change(function () {
    $('.upload-message').text('');
    let total_size = 0;
    if ($(this).val() != '') {
        $(this).prev().text('Choosen files: ' + $(this)[0].files.length);

        if ($(this)[0].files.length > 10 || $(this)[0].files.length < 1) {
            $('.upload-error-message').text('Choose from 1 to 10 files.');
            document.getElementById("run-upload").disabled = true;
        }
        else {
            for (var i = 0; i < $(this)[0].files.length; i++) {
                total_size += $(this)[0].files[i].size;
            }
            if (total_size > 524288000) {
                $('.upload-error-message').text('Total size should not exceed 500MB.');
                document.getElementById("run-upload").disabled = true;
            }
            else {
                $('.upload-error-message').text('');
                document.getElementById("run-upload").disabled = false;
            }
        }
    }
    else
        $(this).prev().text('Choose files');
});

$('.sort-select').change(function () {
    document.forms['submit-sort'].submit();
});

function confirm_submit(form, text) {
    if (confirm(text)) form.submit();
}

$(function () {
    $('.menuBurger').on('click', function () {
        $('.menu').slideToggle(1700, function () {
            if ($(this).css('display') === 'none') {
                $(this).removeAttr('style');
            }
        });
    });
});