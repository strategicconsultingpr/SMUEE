$(document).ready(function () {

    var lblNotEpisode = document.getElementById("MainContent_lblNotEpisode");
    var divEpisodes = document.getElementById("MainContent_divEpisodes");
    var divParticipantInfo1 = document.getElementById("MainContent_divParticipantInfo1");

    var divParticipantInfo2 = document.getElementById("MainContent_divParticipantInfo2");
    var lblNotEpisode2 = document.getElementById("MainContent_lblNotEpisode2");
    var divEpisodes2 = document.getElementById("MainContent_divEpisodes2");



    divEpisodes.style.visibility = "hidden";
    divParticipantInfo1.style.visibility = "hidden";
    lblNotEpisode.style.visibility = "hidden";


    divParticipantInfo2.style.visibility = "hidden";
    lblNotEpisode2.style.visibility = "hidden";
    divEpisodes2.style.visibility = "hidden";


    $("#btnNextStep2").hide();
    $("#btnNextStep3").hide();
    $("#btnNextStep4").hide();
    $("#btnNextStep5").hide();

    $("#modalModule").modal();






});


//Eventos

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

$("#btnSearchIUP2").click(function () {


    var lblIUPInfo2 = document.getElementById("MainContent_lblIUPInfo2");
    var lblNameInfo2 = document.getElementById("MainContent_lblNameInfo2");
    var lblBornDateInfo2 = document.getElementById("MainContent_lblBornDateInfo2");
    var lblSexInfo2 = document.getElementById("MainContent_lblSexInfo2");
    var lblSSNInfo2 = document.getElementById("MainContent_lblSSNInfo2");
    var lblAgeInfo2 = document.getElementById("MainContent_lblAgeInfo2");
    var lblVeteranInfo2 = document.getElementById("MainContent_lblVeteranInfo2");
    var lblEtniaInfo2 = document.getElementById("MainContent_lblEtniaInfo2");


    var lblNotEpisode2 = document.getElementById("MainContent_lblNotEpisode2");
    var divEpisodes2 = document.getElementById("MainContent_divEpisodes2");
    var divParticipantInfo2 = document.getElementById("MainContent_divParticipantInfo2");

    var gvEpisodeListBody2 = document.getElementById("gvEpisodeListBody2");


    var iup2 = document.getElementById("MainContent_txtIUP2").value;
    var iup1 = document.getElementById("MainContent_lblIUPInfo1").innerText;
    var programa = document.getElementById("MainContent_lblPrograma").value;




    var rvIUP2 = document.getElementById("MainContent_rvIUP2");



    ValidatorValidate(rvIUP2);

    if (rvIUP2.isvalid) {

        if (iup2 != iup1) {
            $.ajax({
                type: "POST", //POST
                url: "ajax/MonitoreoHelpers.asmx/SearchIUP",
                data: `{iup: ${iup2}}`,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                beforeSend: sweetLoading(),

                success: function (obj) {

                    if (obj != null && obj.d != null) {
                        //Existe participante
                        var data = obj.d;
                        divParticipantInfo2.style.visibility = "visible";
                        lblIUPInfo2.innerText = data.PK_Persona;
                        lblNameInfo2.innerText = (data.NB_Segundo == "") ? `${data.NB_Primero} ${data.AP_Primero} ${data.AP_Segundo}` : `${data.NB_Primero} ${data.NB_Segundo} ${data.AP_Primero} ${data.AP_Segundo}`;
                        lblSexInfo2.innerText = data.DE_Sexo;
                        lblAgeInfo2.innerText = data.NR_Edad;
                        var fe_nacimiento = new Date(parseInt(data.FE_Nacimiento.substr(6)));
                        lblBornDateInfo2.innerText = fe_nacimiento.format("dd/MM/yyyy");
                        lblSSNInfo2.innerText = data.NR_SeguroSocial;
                        lblVeteranInfo2.innerText = data.DE_Veterano;
                        lblEtniaInfo2.innerText = data.DE_GrupoEtnico;

                        divParticipantInfo2.style.visibility = "visible";
                        lblNotEpisode2.style.visibility = "hidden";
                        divEpisodes2.style.visibility = "visible";

                        GetEpisode2(iup2);


                        $('#rdExpedienteList').html("");
                        $("#MainContent_lblExpedienteMsg").hide();

                        GetExpediente(iup2, programa);

                        $("#btnNextStep2").show();


                    }
                    else {


                        divEpisodes2.style.visibility = "hidden";
                        divParticipantInfo2.style.visibility = "hidden";
                        lblNotEpisode2.style.visibility = "hidden";
                        gvEpisodeListBody2.innerHTML = "";
                        sweetAlert('Participante no encontrado', `No existe participante con el IUP ${iup2} ingresado`, 'warning');
                        $("#btnNextStep2").hide();
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
        else {
            divEpisodes2.style.visibility = "hidden";
            divParticipantInfo2.style.visibility = "hidden";
            lblNotEpisode2.style.visibility = "hidden";
            gvEpisodeListBody2.innerHTML = "";
            sweetAlert('Verificar IUP', `No puede transferir un episodio a un mismo participante`, 'error');
            $("#btnNextStep2").hide();
        }
    }
});


$('#chkConfirmation').change(function () {
    $("#btnNextStep4").hide();

    if (this.checked) {
        $("#btnNextStep4").show();

    }
});

//Funciones

function txtExpedienteChange() {

    $("#rdExpediente2").val($("#MainContent_txtExpediente").val());
    if ($("#rdExpediente2").val() != "") {
        $("#btnNextStep3").hide();
        if ($("#rdExpediente2").val() == $("#rdExpediente0").val() || $("#rdExpediente2").val() == $("#rdExpediente1").val()) {
            sweetAlert('Expediente invalido', `El expediente ${$("#rdExpediente2").val()} ya existe dentro de las opciones anteriores`, 'warning');
            $("#MainContent_txtExpediente").val("");
            $("#rdExpediente2").val("");
            $("#btnNextStep3").hide();
        } else {
            $.ajax({
                type: "POST", //POST
                url: "ajax/MonitoreoHelpers.asmx/GetExpedienteByNRExpediente",
                data: `{nr_expediente: ${$("#rdExpediente2").val()} , programa:${$("#MainContent_lblPrograma").val()}}`,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: sweetLoading(),

                success: function (obj) {

                    if (obj != null && obj.d != null) {
                        sweetAlert('Expediente invalido', `El expediente ${$("#rdExpediente2").val()} ya existe en este programa`, 'warning');
                        $("#btnNextStep3").hide();
                        $("#MainContent_txtExpediente").val("");
                        $("#rdExpediente2").val("");
                        $("#btnNextStep3").hide();
                    }
                    else {
                        $("#btnNextStep3").show();

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



    }
    else {
        $("#btnNextStep3").hide();
    }

}

function rdExpedienteChange(radio) {
    if (radio == 1) {
        $("#btnNextStep3").show();
        $("#MainContent_txtExpediente").hide();
        $("#MainContent_txtExpediente").val("");
        $("#rdExpediente2").val("");

    }
    else {

        $("#MainContent_txtExpediente").val("");
        $("#MainContent_txtExpediente").show();
        $("#rdExpediente2").val("");
        $("#btnNextStep3").hide();
    }
}


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


                if (lista.length == 0) {

                    lblNotEpisode.style.visibility = "visible";
                    divEpisodes.style.visibility = "hidden";


                } else {
                    $.each(lista, function (index, value) {
                        var fe = new Date(parseInt(value.FE_Episodio.substr(6)));
                        var fe_s = fe.format("dd/MM/yyyy");
                        $('#gvEpisodeListBody').append(`<tr><td>${value.PK_Episodio}</td><td>${value.NB_Programa}</td><td>${fe_s}</td><td>${value.DE_ES_Episodio}</td><td><a class="btn btn-primary" data-toggle="tab" href="#wizard2" role="tab" aria-controls="wizard2" onclick="wizard1to2(${value.PK_Episodio},${value.FK_Programa},'${value.NB_Programa}',${value.FK_Persona},${value.NR_Expediente});" type="button">Elegir</a></td></tr>`);
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

function GetEpisode2(iup) {

    var lblNotEpisode2 = document.getElementById("MainContent_lblNotEpisode2");
    var divEpisodes2 = document.getElementById("MainContent_divEpisodes2");

    var lblTotal2 = document.getElementById("MainContent_lblTotal2");
    var gvEpisodeListBody2 = document.getElementById("gvEpisodeListBody2");





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
                gvEpisodeListBody2.innerHTML = "";


                if (lista.length == 0) {

                    lblNotEpisode2.style.visibility = "visible";
                    divEpisodes2.style.visibility = "hidden";


                } else {
                    $.each(lista, function (index, value) {
                        var fe = new Date(parseInt(value.FE_Episodio.substr(6)));
                        var fe_s = fe.format("dd/MM/yyyy");
                        $('#gvEpisodeListBody2').append(`<tr><td>${value.PK_Episodio}</td><td>${value.NB_Programa}</td><td>${fe_s}</td><td>${value.DE_ES_Episodio}</td></tr>`);
                    });
                }

                lblTotal2.innerText = 'Total: ' + lista.length;



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

function GetExpediente(iup, programa) {

    var lblIUPInfo1 = document.getElementById("MainContent_lblIUPInfo1").innerText;
    var lblNameInfo1 = document.getElementById("MainContent_lblNameInfo1").innerText;
    var lblIUPInfo2 = document.getElementById("MainContent_lblIUPInfo2").innerText;
    var lblNameInfo2 = document.getElementById("MainContent_lblNameInfo2").innerText;
    var lblNbPrograma = document.getElementById("MainContent_lblNbPrograma").value;



    $.ajax({
        type: "POST", //POST
        url: "ajax/MonitoreoHelpers.asmx/GetExpediente",
        data: `{iup:${iup} , programa:${programa}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        beforeSend: sweetLoading(),

        success: function (obj) {

            if (obj != null) {

                var p = obj.d;

                $("#btnNextStep3").hide();
                $("#MainContent_txtExpediente").hide();



                if (p != null) {
                    //Tiene expediente
                    if (p.NR_Expediente != "") {
                        $('#rdExpedienteList').append(`<input type="radio" onChange="rdExpedienteChange(1);" disabled checked name="expediente" id="rdExpediente1" value="${p.NR_Expediente}"><label for="rdExpediente1">${p.NR_Expediente} - ${lblNameInfo2} (IUP: ${lblIUPInfo2})</label><br>`);
                        $("#MainContent_lblExpedienteMsg").text(`(IUP: ${lblIUPInfo2}) ${lblNameInfo2} cuenta con un expediente en ${lblNbPrograma}`);
                        $("#MainContent_lblExpedienteMsg").show();
                        $("#btnNextStep3").show();

                    }
                }
                else {

                    $("#MainContent_lblExpedienteMsg").text(`(IUP: ${lblIUPInfo2}) ${lblNameInfo2} no cuenta con un expediente en ${lblNbPrograma}`);
                    $("#MainContent_lblExpedienteMsg").show();
                    $('#rdExpedienteList').append(`<input type="radio" onChange="rdExpedienteChange(2);"  name="expediente" id="rdExpediente2" value=""><label for="rdExpediente2">Crear Expediente</label><br>`);
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

function TransferEpisode(episodio, iup, expedienteOption, numberExpediente) {
    $.ajax({
        type: "POST", //POST
        url: "ajax/TransferEpisodePerson.asmx/TransferEpisode",
        data: `{episode:${episodio} , iup:${iup} , expedienteOption:'${expedienteOption}', numberExpediente:${numberExpediente} }`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),

        async: false,
        success: function (obj) {

            if (obj != null) {

                var p = obj.d;

                if (p == true) {

                    $("#lblConfirmacion").text(`Se ha transferido exitosamente el episodio #${$("#MainContent_lblEpisode").val()} ha (IUP: ${$("#MainContent_lblIUPInfo2").text()}) ${$("#MainContent_lblNameInfo2").text()}`);
                    $("#btnNextStep5").show();
                    $("#btnBackStep5").hide();


                }
                else {
                    $("#lblConfirmacion").text(`No se ha transferido exitosamente el episodio #${$("#MainContent_lblEpisode").val()} ha (IUP: ${$("#MainContent_lblIUPInfo2").text()}) ${$("#MainContent_lblNameInfo2").text()}`);
                    $("#btnNextStep5").hide();
                    $("#btnBackStep5").show();


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

function wizard1to2(episode, programa, nb_programa, persona, NR_Expeddiente) {



    if (episode != null && "" != episode && "" != persona) {

        $("#MainContent_lblEpisode").val(episode);
        $("#MainContent_lblPrograma").val(programa);
        $("#MainContent_lblNbPrograma").val(nb_programa);
        $("#MainContent_lblPersona").val(persona);
        $("#MainContent_lblExpedienteOriginal").val(NR_Expeddiente);
        $("#MainContent_lblExpedienteOriginalResume").text(NR_Expeddiente);



        document.getElementById("MainContent_wizard2Tab").click();




    }


}

function wizard2to3() {

    var iup2 = document.getElementById("MainContent_lblIUPInfo2").innerText;


    if (iup2 != null && "" != iup2) {

        $("#MainContent_lblIUPPersonToTransfer").val(iup2);



        document.getElementById("MainContent_wizard3Tab").click();

    }


}

function wizard3to4() {

    if ($('input[name="expediente"]:checked').val() != "") {




        $("#MainContent_lblEpisodeResume").text($("#MainContent_lblEpisode").val());
        $("#MainContent_lblProgramaResume").text($("#MainContent_lblNbPrograma").val());
        $("#MainContent_lblParResume").text(`(IUP: ${$("#MainContent_lblIUPInfo1").text()}) ${$("#MainContent_lblNameInfo1").text()}`);
        $("#MainContent_lblParResume2").text(`(IUP: ${$("#MainContent_lblIUPInfo2").text()}) ${$("#MainContent_lblNameInfo2").text()}`);
        $("#MainContent_lblExpedienteResume").text($('input[name="expediente"]:checked').val());


        document.getElementById("MainContent_wizard4Tab").click();

    }

}

function wizard4to5() {

    if ($("#MainContent_lblEpisode").val() != "" && $("#MainContent_lblPrograma").val() != "" && $("#MainContent_lblIUPPersonToTransfer").val() != "" &&
        $('input[name="expediente"]:checked').val() != "") {


        document.getElementById("MainContent_wizard5Tab").click();
        $("#btnNextStep5").hide();
        $("#btnBackStep5").show();

        TransferEpisode($("#MainContent_lblEpisode").val(), $("#MainContent_lblIUPPersonToTransfer").val(), $('input[name="expediente"]:checked').attr('id'), $('input[name="expediente"]:checked').val());

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

function wizard5to4() {

    document.getElementById("MainContent_wizard4Tab").click();


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






