
$(function () {

    //MODAL POPUP BEGIN
    var placeHolderModal = $("#place-holder-modal");

    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done((data) => {
            placeHolderModal.html(data);
            placeHolderModal.find('.modal').modal('show');
        });
    });

    placeHolderModal.on('click', '[data-save="modal"]', function (event){
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        $.post(actionUrl, sendData).done((data) => {
            placeHolderModal.find('.modal').modal('hide');
            location.reload(true);
        });
    });
    //MODAL POPUP END

});