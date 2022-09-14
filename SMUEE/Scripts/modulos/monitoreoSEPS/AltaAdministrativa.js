
/*
 Modulo Monitoreo - Episodios Cerrados
 */


var array = [];
var OptionRazonAlta = "";
var listFinalEpisodes = [];

var tb1;
var tb2;


$(document).ready(function () {

    var lblNotEpisode = document.getElementById("MainContent_lblNotEpisode");
    var divEpisodes = document.getElementById("MainContent_divEpisodes");

    divEpisodes.style.display = "none";
    lblNotEpisode.style.display = "none";

    $("#btnNextStep1").hide();
    $("#btnNextStep2").hide();
    $("#btnNextStep3").hide();
    $("#modalModule").modal();
    PopulateDdlAlta();

    tb1 = $('.altasAdDataTable').DataTable({
        select: {
            style: 'multi'
        }
    });


    tb2 = $('.altasDataTable').DataTable({
        "autoWidth": false,
        "paging": false,
        scrollY: '300px',
        scrollCollapse: true,
    });

});

$("#MainContent_ddlPrograma").change(function () {
    var pk = this.value;

    if (pk != "-1") {
        GetEpisode(pk);
        array = [];
        $("#btnNextStep1").hide();

        $("#divResume").html("");
    }
    else {
        var lblNotEpisode = document.getElementById("MainContent_lblNotEpisode");
        var divEpisodes = document.getElementById("MainContent_divEpisodes");
        $("#btnNextStep1").hide();
        divEpisodes.style.display = "none";
        lblNotEpisode.style.display = "none";
        ClearList();
    }
});


function ClearList() {
    tb2.clear();
    listFinalEpisodes = [];
    array = [];
}



function GetEpisode(pk) {

    var lblNotEpisode = document.getElementById("MainContent_lblNotEpisode");
    var divEpisodes = document.getElementById("MainContent_divEpisodes");

    $.ajax({
        type: "POST", //POST
        url: "ajax/MonitoreoHelpers.asmx/GetEpisodesSAEP",
        data: `{pk_programa:${pk}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),

        success: function (obj) {


            if (obj != null && obj.d != null) {
                var lista = obj.d;

                tb1.clear().draw();

                if (lista.length == 0) {

                    lblNotEpisode.style.display = "block";
                    divEpisodes.style.display = "none";
                }

                else {



                    $.each(lista, function (index, value) {
                        var fe = new Date(parseInt(value.Fecha_Admsión.substr(6)));
                        var fe_s = fe.format("dd/MM/yyyy");
                        const tr = $(`<tr id="row${value.Número_de_Episodio}"><td>${value.Número_de_Episodio}</td><td>${value.Nombre_Participante}</td><td>${fe_s}</td><td>${value.Tipo_de_Último_Perfil}</td> <td>${value.Meses_sin_Perfiles_de_Evaluación_de_Progreso}</td></tr>`);

                        tb1.row.add(tr[0]).draw();

                    });





                    divEpisodes.style.display = "block";
                    lblNotEpisode.style.display = "none";
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

}


function RazonChange(id) {

    $('#chkConfirmation').prop('checked', false);
    $("#btnNextStep2").hide();

    $('#ddlAlta' + id).css('border-color', '');
    $('#txtFechaAlta' + id).css('border-color', '');

    if ($("#ddlAlta" + id).val() != "-1") {



        if ($("#ddlAlta" + id).val() == "97") {
            $('#txtFechaAlta' + id).datepicker("setDate", 'today');
            $('#txtFechaAlta' + id).hide();
        }
        else {
            $('#txtFechaAlta' + id).show();

        }


    }

}


$(document).ready(function () {

    var table = tb1;
    table.on('select', function (e, dt, type, indexes) {
        if (type === 'row') {
            var count = table.rows({ selected: true }).count();


            if (count > 0) {

                $("#btnNextStep1").show();


            }
            else {
                $("#btnNextStep1").hide();
            }

        }
    });

    table.on('deselect', function (e, dt, type, indexes) {
        if (type === 'row') {
            var count = table.rows({ selected: true }).count();


            if (count > 0) {

                $("#btnNextStep1").show();


            }
            else {
                $("#btnNextStep1").hide();
            }

        }
    });


});




$('#btnNextStep1').click(function () {


    var count = tb1.rows({ selected: true }).count();

    ClearList();
    if (count > 0) {
        tb1.rows({ selected: true }).every(function (rowIdx, tableLoop, rowLoop) {
            var data = this.data();

            array.push(data[0]);
        });



        $.ajax({
            type: "POST", //POST
            url: "ajax/MonitoreoHelpers.asmx/GetEpisodesSAEPByEpisode",
            data: JSON.stringify({ arr: array }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: sweetLoading(),

            success: function (obj) {


                if (obj != null && obj.d != null) {
                    var lista = obj.d;

                    if (lista.length > 0) {
                        var fe_now = new Date();


                        $.each(lista, function (index, value) {

                            var fe = new Date(parseInt(value.Fecha_Admsión.substr(6)));


                            var tr = $(`<tr>
<td>${value.Número_de_Episodio}</td>
<td>${value.Nombre_Participante}</td>
<td> <select name="ddlAlta" onChange="RazonChange(${value.Número_de_Episodio})" id="ddlAlta${value.Número_de_Episodio}" class="form-control">
`+ OptionRazonAlta + `
</td>
<td><input type="text"  id="txtFechaAlta${value.Número_de_Episodio}" name="txtFechaAlta" class="form-control txtFechaAltaC"></td>
</tr>`);
                            tb2.row.add(tr[0]).draw();

                            $(`#txtFechaAlta${value.Número_de_Episodio}`).datepicker('setStartDate', fe);

                        });

                        $('.txtFechaAltaC').datepicker({
                            weekStart: 1,
                            daysOfWeekHighlighted: "6,0",
                            autoclose: true,
                            todayHighlight: true,
                        });

                        $('.txtFechaAltaC').datepicker("setDate", 'today');
                        $('.txtFechaAltaC').datepicker('setEndDate', 'today');
                        $('.txtFechaAltaC').hide();

                        document.getElementById("MainContent_wizard2Tab").click();


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

    } else {

        swal({
            title: 'Ups!',
            text: 'Debe seleccionar mínimo un episodio para crear un alta.',
            icon: 'warning'
        });
        document.getElementById("btnBackStep2").click();


    }

    $('#chkConfirmation').prop('checked', false);
    $("#btnNextStep2").hide();



});





function PopulateDdlAlta() {


    $.ajax({
        type: "POST", //POST
        url: "ajax/MonitoreoHelpers.asmx/GetRazonAlta",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),

        success: function (obj) {


            if (obj != null && obj.d != null) {
                var lista = obj.d;
                OptionRazonAlta = '<option value="-1">Seleccione una opción</option>';
                if (lista.length > 0) {
                    $.each(lista, function (index, value) {

                        OptionRazonAlta += `<option value="${value.PK_Alta}">${value.DE_Alta}</option>`;

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



}

/*
 Jose A. Ramos De La Cruz
 Prpoposito: Confirmar la transaccion la cual se desea efectuar
 Fecha: 6/21/2022
 */
$('#chkConfirmation').change(function () {
    $("#btnNextStep2").hide();

    if (this.checked) {
        var count = tb1.rows({ selected: true }).count();
        if (count > 0) {

            var flag = true;

            tb1.rows({ selected: true }).every(function (rowIdx, tableLoop, rowLoop) {
                var data = this.data();


                $('#ddlAlta' + data[0]).css('border-color', '')
                $('#txtFechaAlta' + data[0]).css('border-color', '')

                if ($('#ddlAlta' + data[0]).val() == "-1") {
                    flag = false;
                    $('#ddlAlta' + data[0]).css('border-color', 'red')

                }

                if ($('#txtFechaAlta' + data[0]).val() == "") {
                    flag = false;
                    $('#txtFechaAlta' + data[0]).css('border-color', 'red')

                }

            });

            if (flag == true) {
                $("#btnNextStep2").show();

            } else {

                $('#chkConfirmation').prop('checked', false);

                swal({
                    title: 'Ups!',
                    text: 'No ha completado la razón de alta o ingresado una fecha de alta válida.',
                    icon: 'warning'
                });

                $("#btnNextStep2").hide();

            }

        }
        else {
            $('#chkConfirmation').prop('checked', false);

            $("#btnNextStep2").hide();
        }
    }
});

function AltaAdministrativa() {

    var dataToSend = JSON.stringify({ 'lst': listFinalEpisodes });

    $.ajax({
        type: "POST", //POST
        url: "ajax/AltasAdministrativas.asmx/DichargeBySystem",
        data: dataToSend,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),
        success: function (obj) {

            if (obj != null) {

                var lista = obj.d;


                if (lista != null) {
                    console.log(lista);
                    $.each(lista, function (index, value) {


                        if (value.Perfil != null)
                        {
                            $(`#status${value.Episodio}`).text("Completado");
                            $(`#status${value.Episodio}`).css('color', 'green');
                            $(`#perfil${value.Episodio}`).text(value.Perfil);
                        }
                        else
                        {
                            $(`#status${value.Episodio}`).text("No Completado");
                            $(`#status${value.Episodio}`).css('color', 'red');


                        }


                    });
                }



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


function wizard2to3() {

    var count = tb2.data().count();
    if (count > 0) {
        var htmlStr = "";
        var htmlStr2 = "";





        tb2.rows().every(function (rowIdx, tableLoop, rowLoop) {
            var data = this.data();


            var dateF = $('#txtFechaAlta' + data[0]).datepicker('getDate').toLocaleDateString();


            htmlStr += `
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Episodio: </em></div><div class="col" runat="server">${data[0]}</div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Programa: </em></div><div class="col" runat="server">${$('#MainContent_ddlPrograma option:selected').text()}</div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Participante: </em></div><div class="col" runat="server">${data[1]}</div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Razón de Alta: </em></div><div class="col" runat="server" >${$('#ddlAlta' + data[0] + ' option:selected').text()}</div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Fecha de Alta: </em></div><div class="col" runat="server">${$('#txtFechaAlta' + data[0]).val()}</div></div><hr>`;


            htmlStr2 += `
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Episodio: </em></div><div class="col" runat="server">${data[0]}</div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Programa: </em></div><div class="col" runat="server">${$('#MainContent_ddlPrograma option:selected').text()}</div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Participante: </em></div><div class="col" runat="server">${data[1]}</div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Razón de Alta: </em></div><div class="col" runat="server" >${$('#ddlAlta' + data[0] + ' option:selected').text()}</div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Fecha de Alta: </em></div><div class="col" runat="server">${$('#txtFechaAlta' + data[0]).val()}</div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Estatus: </em></div><div class="col" runat="server" id="status${data[0]}"></div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Perfil de Alta: </em></div><div class="col" runat="server" id="perfil${data[0]}"></div></div><hr>`;



            listFinalEpisodes.push({
                Episodio: `${data[0]}`,
                Razon: `${$('#ddlAlta' + data[0]).val()}`,
                FechaStr: `${dateF}`

            });


        });


        console.log(listFinalEpisodes);
        $("#btnNextStep3").show();

        $("#divResume").html(htmlStr);
        $("#divResume2").html(htmlStr2);


        document.getElementById("MainContent_wizard3Tab").click();

    }
    else {
        document.getElementById("btnBackStep3").click();
        $("#btnNextStep3").hide();

    }

}



function wizard2to1() {
    ClearList();
    document.getElementById("MainContent_wizard1Tab").click();
}

function wizard3to2() {

    document.getElementById("MainContent_wizard2Tab").click();

}


function wizard3to4() {
    AltaAdministrativa();
    document.getElementById("MainContent_wizard4Tab").click();
}


function wizard4to3() {

    document.getElementById("MainContent_wizard3Tab").click();
}



function sweetAlert(titulo, texto, icono) {
    swal({
        title: titulo,
        text: texto,
        icon: icono
    });
}

function sweetLoading() {
    swal({
        title: 'Cargando',

        buttons: false,
        icon: '/Images/loading.gif',
        timer: 1000

    });
}






