




$(document).on('change', '.file-input', function () {


    var filesCount = $(this)[0].files.length;

    var textbox = $(this).prev();



    if (filesCount === 1) {
        var fileName = $(this).val().split('\\').pop();
        textbox.text(fileName);

    }
    else {
        textbox.text(filesCount + ' files selected');
    }

});


function sweetLoadingProcesando() {
    var filesCount = $('.file-input')[0].files.length;

    if (filesCount > 0) {
        swal({
            title: 'Procesando',
            text: "Este proceso puede tomar varios minutos favor de no cerrar la pestaña o refrescar el navegador.",
            buttons: false,
            icon: 'warning',
            closeOnClickOutside: false

        });
    }



}





