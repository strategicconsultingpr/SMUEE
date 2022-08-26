
/*
 Modulo Monitoreo - Episodios Cerrados
 */


var array = [];

$(document).ready(function () {

    var lblNotEpisode = document.getElementById("MainContent_lblNotEpisode");
    var divEpisodes = document.getElementById("MainContent_divEpisodes");

    divEpisodes.style.visibility = "hidden";
    lblNotEpisode.style.visibility = "hidden";

    $("#btnNextStep2").hide();
    $("#btnNextStep3").hide();
    $("#modalModule").modal();




});

$("#MainContent_ddlPrograma").change(function () {
    var pk = this.value;

    if (pk != "-1") {
        GetEpisode(pk);
        array = [];
        $("#divResume").html("");


    }
});



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

                $('.altasAdDataTable').DataTable().clear().draw();
                if (lista.length == 0) {

                    lblNotEpisode.style.visibility = "visible";
                    divEpisodes.style.visibility = "hidden";
                }

                else {



                    $.each(lista, function (index, value) {
                        var fe = new Date(parseInt(value.Fecha_Admsión.substr(6)));
                        var fe_s = fe.format("dd/MM/yyyy");
                        const tr = $(`<tr id="row{value.Número_de_Episodio}"><td> <div class="form-check">
                    <input class="form-check-input select-checkbox"  type="checkbox" value="${value.Número_de_Episodio}" name="episodesAl" id="episode${value.Número_de_Episodio}">
                    </div></td><td>${value.Número_de_Episodio}</td><td>${value.Nombre_Participante}</td><td>${fe_s}</td><td>${value.Tipo_de_Último_Perfil}</td> <td>${value.Meses_sin_Perfiles_de_Evaluación_de_Progreso}</td></tr>`);
                        $('.altasAdDataTable').DataTable().row.add(tr[0]).draw();

                    });





                    divEpisodes.style.visibility = "visible";
                    lblNotEpisode.style.visibility = "hidden";
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




$('#btnNextStep1').click(function () {


    var $boxes = $('input[name=episodesAl]:checked');
    array = [];
    if ($boxes.length > 0) {
        $boxes.each(function () {

            array.push(this.value);

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
                        var htmlStr = "";
                        $.each(lista, function (index, value) {

                            htmlStr += `<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Participante:</em></div><div class="col" runat="server">${value.Nombre_Participante}</div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Episodio:</em></div><div class="col" runat="server" >${value.Número_de_Episodio}</div></div>
<div class="row small text-muted"><div class="col-sm-3 text-truncate"><em>Programa:</em></div><div class="col" runat="server">${value.Nombre_Programa}</div></div><hr>`;
                        });

            $("#divResume").html(htmlStr);


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

        document.getElementById("MainContent_wizard2Tab").click();
    } else {

        swal({
            title: 'Ups!',
            text: 'Debe seleccionar mínimo un episodio para crear un alta administrativa.',
            icon: 'warning'
        });
        document.getElementById("btnBackStep2").click();

        document.getElementById("MainContent_wizard1Tab").click();

    }




});

/*
 Jose A. Ramos De La Cruz
 Prpoposito: Confirmar la transaccion la cual se desea efectuar
 Fecha: 6/21/2022
 */
$('#chkConfirmation').change(function () {
    $("#btnNextStep2").hide();

    if (this.checked) {
        $("#btnNextStep2").show();

    }
});

function AltaAdministrativa(episode) {
    $.ajax({
        type: "POST", //POST
        url: "ajax/AltasAdministrativas.asmx.asmx/OpenEpisode",
        data: JSON.stringify({ arr: array }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),

        async: false,
        success: function (obj) {

            if (obj != null) {

                var p = obj.d;

                if (p == 1) {

                    $("#lblConfirmacion").text(`Se ha creado exitosamente el alta administrativa para el episodio #${$("#MainContent_lblEpisode").val()}`);
                    $("#btnNextStep3").show();
                    $("#btnBackStep3").hide();

                }

                else {
                    $("#lblConfirmacion").text(`No se ha crear el alta administrativa para el episodio #${$("#MainContent_lblEpisode").val()}`);
                    $("#btnNextStep3").hide();
                    $("#btnBackStep3").show();


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



    if (array.length > 0) {

        AltaAdministrativa($("#MainContent_lblEpisode").val());
        document.getElementById("MainContent_wizard3Tab").click();

    }
    else {
        document.getElementById("btnBackStep3").click();

    }

}



function wizard2to1() {

    document.getElementById("MainContent_wizard1Tab").click();
}

function wizard3to2() {

    document.getElementById("MainContent_wizard2Tab").click();

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






