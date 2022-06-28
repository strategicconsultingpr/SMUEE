
/*
 Modulo Monitoreo - Eliminar Personas
 */

$(document).ready(function () {

    var lblNotEpisode = document.getElementById("MainContent_lblNotEpisode");
    var divEpisodes = document.getElementById("MainContent_divEpisodes");
    var divParticipantInfo1 = document.getElementById("MainContent_divParticipantInfo1");

    divEpisodes.style.visibility = "hidden";
    divParticipantInfo1.style.visibility = "hidden";
    lblNotEpisode.style.visibility = "hidden";

    $("#btnNextStep1").hide();
    $("#btnNextStep2").hide();
    $("#btnNextStep3").hide();
    $("#btnNextStep4").hide();

});


//Eventos

/*
 Jose A. Ramos De La Cruz
 Prpoposito: Buscar participante con el IUP insertado
 Fecha: 6/21/2022
 */
$("#btnSearchIUP").click(function () {


    var lblIUPInfo1 = document.getElementById("MainContent_lblIUPInfo1");
    var lblNameInfo1 = document.getElementById("MainContent_lblNameInfo1");
    var lblBornDateInfo1 = document.getElementById("MainContent_lblBornDateInfo1");
    var lblSexInfo1 = document.getElementById("MainContent_lblSexInfo1");
    var lblSSNInfo1 = document.getElementById("MainContent_lblSSNInfo1");
    var lblAgeInfo1 = document.getElementById("MainContent_lblAgeInfo1");
    var lblVeteranInfo1 = document.getElementById("MainContent_lblVeteranInfo1");
    var lblEtniaInfo1 = document.getElementById("MainContent_lblEtniaInfo1");
    var lblNotEpisode = document.getElementById("MainContent_lblNotEpisode");
    var divEpisodes = document.getElementById("MainContent_divEpisodes");
    var divParticipantInfo1 = document.getElementById("MainContent_divParticipantInfo1");
    var gvEpisodeListBody = document.getElementById("gvEpisodeListBody");
    var iup = document.getElementById("MainContent_txtIUP").value;
    var rvIUP1 = document.getElementById("MainContent_rvIUP1");

    ValidatorValidate(rvIUP1);

    //Validar que se ha ingresado un IUP valido
    if (rvIUP1.isvalid) {

        $.ajax({
            type: "POST", //POST
            url: "ajax/MonitoreoHelpers.asmx/SearchIUP",
            data: `{iup: ${iup}}`,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: sweetLoading(),
            success: function (obj) {


                $("#MainContent_lblPersona").val("");

                if (obj != null && obj.d != null) {
                    //Existe participante
                    //Poblar campos con informacion del participante
                    var data = obj.d;
                    divParticipantInfo1.style.visibility = "visible";
                    $("#MainContent_lblPersona").val(data.PK_Persona);
                    lblIUPInfo1.innerText = data.PK_Persona;
                    lblNameInfo1.innerText = (data.NB_Segundo == "") ? `${data.NB_Primero} ${data.AP_Primero} ${data.AP_Segundo}` : `${data.NB_Primero} ${data.NB_Segundo} ${data.AP_Primero} ${data.AP_Segundo}`;
                    lblSexInfo1.innerText = data.DE_Sexo;
                    lblAgeInfo1.innerText = data.NR_Edad;
                    var fe_nacimiento = new Date(parseInt(data.FE_Nacimiento.substr(6)));
                    lblBornDateInfo1.innerText = fe_nacimiento.format("dd/MM/yyyy");
                    lblSSNInfo1.innerText = data.NR_SeguroSocial;
                    lblVeteranInfo1.innerText = data.DE_Veterano;
                    lblEtniaInfo1.innerText = data.DE_GrupoEtnico;
                    divParticipantInfo1.style.visibility = "visible";
                    lblNotEpisode.style.visibility = "hidden";
                    divEpisodes.style.visibility = "visible";
                    $("#btnNextStep1").hide();
                    GetEpisode(iup);
                }
                //No existe
                //Mostrar mensaje de que no existe
                else {
                    $("#btnNextStep1").hide();
                    divEpisodes.style.visibility = "hidden";
                    divParticipantInfo1.style.visibility = "hidden";
                    lblNotEpisode.style.visibility = "hidden";
                    gvEpisodeListBody.innerHTML = "";
                    sweetAlert('Participante no encontrado', `El participante con IUP ${iup} no ha sido encontrado`, 'warning');


                }

            },
            failure: function (response) {
                console.log(response);

            },
            error: function (response) {
                console.log(response);


            }
        });
    }
});



/*
 Jose A. Ramos De La Cruz
 Prpoposito: Buscar episodios de participantes 
 Fecha: 6/21/2022
 */
function GetEpisode(iup) {

    var lblNotEpisode = document.getElementById("MainContent_lblNotEpisode");
    var divEpisodes = document.getElementById("MainContent_divEpisodes");
    var lblTotal = document.getElementById("MainContent_lblTotal");
    var gvEpisodeListBody = document.getElementById("gvEpisodeListBody");

    $.ajax({
        type: "POST", //POST
        url: "ajax/MonitoreoHelpers.asmx/GetEpisodes",
        data: `{iup:${iup}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),

        success: function (obj) {


            if (obj != null && obj.d != null) {
                var lista = obj.d;
                gvEpisodeListBody.innerHTML = "";

                //No existen episodios
                if (lista.length == 0) {

                    lblNotEpisode.style.visibility = "hidden";
                    divEpisodes.style.visibility = "hidden";
                    $("#btnNextStep1").show();
                    GetExpediente(iup);



                }
                //Existen Epidosidos
                else {
                    $.each(lista, function (index, value) {
                        var fe = new Date(parseInt(value.FE_Episodio.substr(6)));
                        var fe_s = fe.format("dd/MM/yyyy");
                        $('#gvEpisodeListBody').append(`<tr><td>${value.PK_Episodio}</td><td>${value.NB_Programa}</td><td>${fe_s}</td><td>${value.DE_ES_Episodio}</td></tr>`);
                    });
                    lblNotEpisode.style.visibility = "visible";
                    divEpisodes.style.visibility = "visible";
                    $("#btnNextStep1").hide();

                }

                lblTotal.innerText = 'Total: ' + lista.length;

            }

        },
        failure: function (response) {
            alert(response.d);

        },
        error: function (response) {
            alert(response.d);
        }
    });

}

/*
 Jose A. Ramos De La Cruz
 Prpoposito: Buscar expedientes existente
 Fecha: 6/21/2022
 */
function GetExpediente(iup) {

    $.ajax({
        type: "POST", //POST
        url: "ajax/MonitoreoHelpers.asmx/GetExpedienteById",
        data: `{iup:${iup}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        beforeSend: sweetLoading(),

        success: function (obj) {

            if (obj != null) {

                var lista = obj.d;
                $('#lstExpedientes').html("");
                //Contiene expedientes
                if (lista != null) {
                    $.each(lista, function (index, value) {
                  
                        $('#lstExpedientes').append(`<li class="list-group-item">${value.NB_Programa} - ${value.NR_Expediente}</li>`);
                    });
                   
                }
                else
                //No contiene expedientes
                {
                    $('#lstExpedientes').append(`<li class="list-group-item">No tiene expedientes</li>`);

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

    $("#btnNextStep2").show();

}
/*
 Jose A. Ramos De La Cruz
 Prpoposito: Eliminar participante selecionado
 Fecha: 6/21/2022
 */
function Delete(iup) {
    $.ajax({
        type: "POST", //POST
        url: "ajax/EliminarPersona.asmx/EliminarPersonas",
        data: `{iup:${iup}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),

        async: false,
        success: function (obj) {

            if (obj != null) {

                var p = obj.d;

                if (p == true) {

                    $("#lblConfirmacion").text(`Se ha eliminado exitosamente el participante (IUP: ${$("#MainContent_lblIUPInfo1").text()}) ${$("#MainContent_lblNameInfo1").text()}`);
                    $("#btnNextStep4").show();
                    $("#btnBackStep4").hide();
                }
                else {
                    $("#lblConfirmacion").text(`No se ha podido eliminar la participante (IUP: ${$("#MainContent_lblIUPInfo1").text()}) ${$("#MainContent_lblNameInfo1").text()}`);
                    $("#btnNextStep4").hide();
                    $("#btnBackStep4").show();
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



/*
 Jose A. Ramos De La Cruz
 Prpoposito: Confirmar la transaccion la cual se desea efectuar
 Fecha: 6/21/2022
 */
$('#chkConfirmation').change(function () {
    $("#btnNextStep3").hide();

    if (this.checked) {
        $("#btnNextStep3").show();

    }
});



function wizard1to2() {


    if ($("#MainContent_lblPersona").val() != "") {
        document.getElementById("MainContent_wizard2Tab").click();
    }
  


}

function wizard2to3() {

    var iup = document.getElementById("MainContent_lblIUPInfo1").innerText;
    var name = document.getElementById("MainContent_lblNameInfo1").innerText;

    if (iup != null) {

        $("#MainContent_lblPersonaResume").text(`( IUP: ${iup} ) - ${name} `);
        document.getElementById("MainContent_wizard3Tab").click();

    }

}

function wizard3to4() {

    if ($('input[name="expediente"]:checked').val() != "" && $("#MainContent_lblPersona").val() != "") {


        Delete($("#MainContent_lblPersona").val());
        document.getElementById("MainContent_wizard4Tab").click();

    }

}

function wizard2to1() {

    document.getElementById("MainContent_wizard1Tab").click();
}

function wizard3to2() {

    document.getElementById("MainContent_wizard2Tab").click();

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






