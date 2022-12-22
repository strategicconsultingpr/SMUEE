if (window.history.replaceState) {
    window.history.replaceState(null, null, window.location.href);
}


$(document).ready(function () {



    $.ajax({
        type: "POST", //POST
        url: "ajax/Extracciones.asmx/GetProgramasTEDS",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading2(),

        success: function (obj) {


            if (obj != null && obj.d != null) {
                var lista = obj.d;

                if (lista.length > 0) {
            
                    VirtualSelect.init({
                        ele: '#ddlProgram',
                        search: true,
                        multiple: true,
                        maxWidth: '100%',
                        tooltipMaxWidth: '100%',
                        hideClearButton: false,
                        searchPlaceholderText: 'Buscar..',
                        markSearchResults: true,
                        selectAllText: 'Seleccionar todo',
                        required : true,
                        options: [
                            {
                                label: 'Salud Mental',
                                options: lista[0]
                            },

                            {
                                label: 'Abuso de Sustancia',
                                options: lista[1]
                            },


                            {
                                label: 'Salud Mental y Abuso de Sustancia',
                                options: lista[2]
                            },

                        ]
                    });
                }
            }

        },
        failure: function (response) {
            console.log(response.d);

        },
        error: function (response) {
            console.log(response);
        }
    });



});


$('#ddlProgram').change(function () {
    var flag = document.querySelector('#ddlProgram').validate();

    if (flag == true) {
        $('#divDate').show();
    }
    else {
        $('#divDate').hide();
    }
});

function lodingOn() {
    $('#divLoading').modal('show');
}
function lodingOff() {
    $('#divLoading').modal('hide');
}


$(document).ready(function () {



    $('[id*=startDate]').datepicker({
        weekStart: 1,
        daysOfWeekHighlighted: "6,0",
        autoclose: true,
        todayHighlight: true,
    }).on('changeDate', function (selected) {
        var minDate = new Date(selected.date.valueOf());
        $('[id*=endDate]').datepicker('setStartDate', minDate);
        ValDate();

    });

    $('[id*=endDate]').datepicker({
        weekStart: 1,
        daysOfWeekHighlighted: "6,0",
        autoclose: true,
        todayHighlight: true,
    }).on('changeDate', function (selected) {
        var maxDate = new Date(selected.date.valueOf());
        $('[id*=startDate]').datepicker('setEndDate', maxDate);
        ValDate();

    });


    $('#divDate').hide();

});


$('[id*=startDate]').focusout(function () {
    ValDate();

});

$('[id*=endDate]').focusout(function () {
    ValDate();
});



function ValDate() {
    var min = $('[id*=startDate]').datepicker('getDate');
    var max = $('[id*=endDate]').datepicker('getDate');

    if (min == null || max == null) {
        $("#btnNextStep1").hide();
    }
    else if (min > max) {
        $("#btnNextStep1").hide();
    }
    else {
        $("#btnNextStep1").show();

    }
}


$(document).ready(function () {
    var date = new Date();
    date.setFullYear(date.getFullYear() - 1);
    $('[id*=startDate]').datepicker("setDate", date);
});

$(document).ready(function () {
    $('[id*=endDate]').datepicker("setDate", new Date());
});


function sweetAlert(titulo, texto, icono) {
    swal({
        title: titulo,
        text: texto,
        icon: icono
    });
}


$('#chkConfirmation').change(function () {
    $("#btnNextStep2").hide();
    if (this.checked) {
        $("#btnNextStep2").show();

    }
});


$('#btnNextStep1').click(function () {
    document.getElementById("MainContent_wizard2Tab").click();

    var min = $('[id*=startDate]').datepicker('getDate');
    var max = $('[id*=endDate]').datepicker('getDate');

    var lst = document.querySelector('#ddlProgram').getSelectedOptions();

    var str = "";
    if (lst.length > 0) {

        $.each(lst, function (index, value) {

            str += `<p>${value.label}</p>`;

        });
    }

    $("#divResume").html(str);

    $("#lblResumen").text(`Se va a generar las transacciones TEDS desde ${min.toLocaleDateString("en-US")} hasta ${max.toLocaleDateString("en-US")} `);


});


$('#btnNextStep2').click(function () {
    document.getElementById("MainContent_wizard3Tab").click();


});





function wizard2to1() {

    document.getElementById("MainContent_wizard1Tab").click();
    $("#btnNextStep2").hide();
    $('#chkConfirmation').prop('checked', false);
}

function wizard3to2() {
    document.getElementById("MainContent_wizard2Tab").click();
    $("#btnNextStep2").hide();
    $('#chkConfirmation').prop('checked', false);
}


$(document).ready(function () {
    $("#btnNextStep2").hide();
    $("#modalModule").modal();
});


$('#btnNextStep3').click(function () {
    document.getElementById("MainContent_wizard2Tab").click();
    var min = $('[id*=startDate]').datepicker('getDate');
    var max = $('[id*=endDate]').datepicker('getDate');
    $("#lblResumen").text(`Se va a generar las transacciones TEDS desde ${min.toLocaleDateString("en-US")} hasta ${max.toLocaleDateString("en-US")} `);


});


function GenerateFile(file) {

    var min = $('[id*=startDate]').datepicker('getDate').toLocaleDateString();
    var max = $('[id*=endDate]').datepicker('getDate').toLocaleDateString();

    $.ajax({
        type: "POST", //POST
        url: "ajax/Extracciones.asmx/GenerateExcel",
        data: `{file:'${file}',min:'${min}',max:'${max}',programs: [${document.querySelector('#ddlProgram').value}]}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),

        success: function (obj) {


            if (obj != null && obj.d != null) {
                var pass = obj.d;
                if (pass.Type == '3') {
                    var filename = pass.FileName;
                    var arr = pass.File;
                    var byteArray = new Uint8Array(arr);
                    var a = window.document.createElement('a');

                    a.href = window.URL.createObjectURL(new Blob([byteArray], { type: 'application/octet-stream' }));
                    a.download = filename;
                    // Append anchor to body.
                    document.body.appendChild(a)
                    a.click();
                    // Remove anchor from body
                    document.body.removeChild(a)
                    $('#btn' + file).css('color', 'green');
                    sweetAlert(pass.Title, pass.Message, pass.Icon);
                }
                else
                {
                    sweetAlert(pass.Title, pass.Message, pass.Icon);

                }
            }
            else {
                sweetAlert('Aviso', 'No se pudo descargar su archivo, inténtelo nuevamente', 'warning');

            }

        },
        failure: function (response) {
            console.log(response.d);

        },
        error: function (response) {
            console.log(response.d);
        }
    });
}




function sweetLoading() {
    swal({
        title: 'Descargando',

        buttons: false,
        icon: 'warning',

    });
}



function sweetLoadingProcesando() {
    swal({
        title: 'Procesando',

        buttons: false,
        icon: 'warning',

    });
}


function sweetLoading2() {
    swal({
        title: 'Cargando',
        buttons: false,
        icon: 'warning',
        timer: 1000


    });
}


