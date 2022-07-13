
/*
 Modulo Monitoreo - Episodios Cerrados
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
    $("#modalModule").modal();


});


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

    if (rvIUP1.isvalid) {

        $.ajax({
            type: "POST", //POST
            url: "ajax/MonitoreoHelpers.asmx/SearchIUP",
            data: `{iup: ${iup}}`,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: sweetLoading(),
            success: function (obj) {



                if (obj != null && obj.d != null) {
                    //Existe participante
                    var data = obj.d;
                    divParticipantInfo1.style.visibility = "visible";
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

                    GetEpisode(iup);


                }
                else {


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



function GetEpisode(iup) {

    var lblNotEpisode = document.getElementById("MainContent_lblNotEpisode");
    var divEpisodes = document.getElementById("MainContent_divEpisodes");
    var lblTotal = document.getElementById("MainContent_lblTotal");
    var gvEpisodeListBody = document.getElementById("gvEpisodeListBody");





    $.ajax({
        type: "POST", //POST
        url: "ajax/MonitoreoHelpers.asmx/GetCloseEpisodeWithoutDischargeByIUP",
        data: `{iup:${iup}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),

        success: function (obj) {


            if (obj != null && obj.d != null) {
                var lista = obj.d;
                gvEpisodeListBody.innerHTML = "";


                if (lista.length == 0) {

                    lblNotEpisode.style.visibility = "visible";
                    divEpisodes.style.visibility = "hidden";


                } else {
                    $.each(lista, function (index, value) {
                        var fe = new Date(parseInt(value.FE_Episodio.substr(6)));
                        var fe_s = fe.format("dd/MM/yyyy");
                        $('#gvEpisodeListBody').append(`<tr><td>${value.PK_Episodio}</td><td>${value.NB_Programa}</td><td>${fe_s}</td><td>${value.DE_ES_Episodio}</td><td><a class="btn btn-primary" data-toggle="tab" href="#wizard2" role="tab" aria-controls="wizard2" onclick="wizard1to2(${value.PK_Episodio},${value.FK_Programa},'${value.NB_Programa}',${value.FK_Persona});" type="button">Reabrir</a></td></tr>`);
                    });
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
 Prpoposito: Confirmar la transaccion la cual se desea efectuar
 Fecha: 6/21/2022
 */
$('#chkConfirmation').change(function () {
    $("#btnNextStep2").hide();

    if (this.checked) {
        $("#btnNextStep2").show();

    }
});

function OpenEpisode(episode)
{
    $.ajax({
        type: "POST", //POST
        url: "ajax/EpisodiosCerrados.asmx/OpenEpisode",
        data: `{episode:${episode}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),

        async: false,
        success: function (obj) {

            if (obj != null) {

                var p = obj.d;

                if (p == 1) {

                    $("#lblConfirmacion").text(`Se ha reabierto exitosamente el episodio #${$("#MainContent_lblEpisode").val()}`);
                    $("#btnNextStep3").show();
                    $("#btnBackStep3").hide();


                }
                else if (p == 2)
                {
                    $("#lblConfirmacion").text(`No se ha reabir el episodio #${$("#MainContent_lblEpisode").val()} debido a que ya existe un episodio abierto bajo el mismo programa`);
                    $("#btnNextStep3").hide();
                    $("#btnBackStep3").show();
                }

                else  {
                    $("#lblConfirmacion").text(`No se ha reabir el episodio #${$("#MainContent_lblEpisode").val()}`);
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

function wizard1to2(episode, programa, nb_programa, persona) {

    if (episode != null && "" != episode && "" != persona) {

        $("#MainContent_lblEpisode").val(episode);
        $("#MainContent_lblPrograma").val(programa);
        $("#MainContent_lblNbPrograma").val(nb_programa);
        $("#MainContent_lblPersona").val(persona);
        var iup = document.getElementById("MainContent_lblIUPInfo1").innerText;
        var name = document.getElementById("MainContent_lblNameInfo1").innerText;
        $("#MainContent_lblPersonaResume").text(`( IUP: ${iup} ) - ${name} `);
        $("#MainContent_lblEpisodeResume").text($("#MainContent_lblEpisode").val());
        $("#MainContent_lblProgramaResume").text($("#MainContent_lblNbPrograma").val());


        if ($("#MainContent_lblEpisode").val() != null) {
            document.getElementById("MainContent_wizard2Tab").click();
        }
    }


}

function wizard2to3() {

   

     if ($("#MainContent_lblEpisode").val() != null) {

         OpenEpisode($("#MainContent_lblEpisode").val());
        document.getElementById("MainContent_wizard3Tab").click();

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






