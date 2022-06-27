$(document).ready(function () {

    var lblNotEpisode = document.getElementById("MainContent_lblNotEpisode");
    var divEpisodes = document.getElementById("MainContent_divEpisodes");
    var divParticipantInfo1 = document.getElementById("MainContent_divParticipantInfo1");
    divEpisodes.style.visibility = "hidden";
    divParticipantInfo1.style.visibility = "hidden";
    lblNotEpisode.style.visibility = "hidden";

    $("#btnNextStep2").hide();
    $("#btnNextStep3").hide();
    $("#btnNextStep4").hide();
    $("#btnNextStep5").hide();
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



$('#chkConfirmation').change(function () {
    $("#btnNextStep5").hide();

    if (this.checked) {
        $("#btnNextStep5").show();

    }
});


$('#MainContent_ddlNvlMH').change(function () {

    if (this.value == "" || $('#MainContent_ddlNvlAS').val() == "") {
        $("#btnNextStep4").hide();
    } else
        $("#btnNextStep4").show();


});

$('#MainContent_ddlNvlAS' ).change(function () {
    if (this.value == "" || $('#MainContent_ddlNvlMH').val() == "") {
        $("#btnNextStep4").hide();
    } else
        $("#btnNextStep4").show();
});


$('#MainContent_ddlPrograma').change(function () {

    $("#btnNextStep2").hide();
    if ($("#MainContent_ddlPrograma").val() != $("#MainContent_lblPrograma").val()) {
        if (this.value != -1) {
            GetNivelCuidado(this.value);
            $("#btnNextStep2").show();
        }
    } else {
        $("#MainContent_ddlPrograma").val("-1")
        sweetAlert('Programa invalido', `No puede transferir el episodio al mismo programa`, 'warning');
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
                    console.log(response.d);
                },
                error: function (response) {
                    console.log(response.d);
                }
            });
        }
    }
    else {
        $("#btnNextStep3").hide();
    }

}

function rdExpedienteChange(radio) {
    if (radio == 1 || radio == 3) {
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

function GetNivelCuidado(programa) {

    $("#MainContent_ddlNvlMH").empty();
    $("#MainContent_ddlNvlAS").empty();
    $("#divMsgNvlAs").hide();
    $("#divMsgNvlMh").hide();

    $.ajax({
        type: "POST", //POST
        url: "ajax/MonitoreoHelpers.asmx/GetNivelCuidadoMhByProgram",
        data: `{programa:${programa}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,

        beforeSend: sweetLoading(),
        success: function (obj) {
            if (obj != null && obj.d != null) {
                var lista = obj.d;
                if (lista.length == 0) {

                    if ($("#MainContent_lblNvlMh").val() == 99) {
                        $('#MainContent_ddlNvlMH').append($('<option></option>').val($("#MainContent_lblNvlMh").val()).html($("#MainContent_lblDENvlMh").val()));

                    } else {
                        $('#MainContent_ddlNvlMH').append($('<option></option>').val("99").html("No tiene niveles de cuidado registrados"));

                    }
                    $("#MainContent_ddlNvlMH").prop("disabled", true);


                } else {
                    $("#MainContent_ddlNvlMH").prop("disabled", false);
                    var exist = false;
                    $('#MainContent_ddlNvlMH').append($('<option></option>').val("").html("Elegir"));
                    $('#MainContent_ddlNvlMH').val("");


                    $.each(lista, function (index, value) {
                        $('#MainContent_ddlNvlMH').append($('<option></option>').val(value.PK_Mental).html(value.DE_SaludMental));
                        if ($("#MainContent_lblNvlMh").val() == value.PK_Mental) {
                            $('#MainContent_ddlNvlMH').val(value.PK_Mental);
                            $("#MainContent_ddlNvlMH").prop("disabled", true);
                            exist = true;
                        }
                    });

                    if (exist == false) {
                        $("#divMsgNvlMh").text(`(Salud Mental) ${$("#MainContent_lblDENvlMh").val()} no existe en  favor de actualizar este nivel de cuidado`);
                        $("#divMsgNvlMh").show();

                    }
                }
            }
        },
        failure: function (response) {
            alert(response.d);

        },
        error: function (response) {
            alert(response.d);
        }
    });



    $.ajax({
        type: "POST", //POST
        url: "ajax/MonitoreoHelpers.asmx/GetNivelCuidadoAsByProgram",
        data: `{programa:${programa}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,

        beforeSend: sweetLoading(),
        success: function (obj) {
            if (obj != null && obj.d != null) {
                var lista = obj.d;
                if (lista.length == 0) {

                    if ($("#MainContent_lblNvlAs").val() == 99) {
                        $('#MainContent_ddlNvlAS').append($('<option></option>').val($("#MainContent_lblNvlAs").val()).html($("#MainContent_lblDENvlAs").val()));

                    } else {
                        $('#MainContent_ddlNvlAS').append($('<option></option>').val("99").html("No tiene niveles de cuidado registrados"));

                    }
                    $("#MainContent_ddlNvlAS").prop("disabled", true);



                }
                else
                {
                    $("#MainContent_ddlNvlAS").prop("disabled", false);
                    $('#MainContent_ddlNvlAS').append($('<option></option>').val("").html("Elegir"));
                    $('#MainContent_ddlNvlAS').val("");


                    var exist = false;
                    $.each(lista, function (index, value) {
                        $('#MainContent_ddlNvlAS').append($('<option></option>').val(value.PK_Sustancia).html(value.DE_AbusoSustancias));
                        
                        if ($("#MainContent_lblNvlAs").val() == value.PK_Sustancia)
                        {

                            $('#MainContent_ddlNvlAS').val(value.PK_Sustancia);
                            $("#MainContent_ddlNvlAS").prop("disabled", true);
                            exist = true;
                        }

                    });

                    if (exist == false) {
                        $("#divMsgNvlAs").text(`(Abuso de Sustancias) ${$("#MainContent_lblDENvlAs").val()} no existe en ${$("#MainContent_ddlPrograma option:selected").text()} favor de actualizar este nivel de cuidado`);
                        $("#divMsgNvlAs").show();

                    }




                }
            }
        },
        failure: function (response) {
            alert(response.d);

        },
        error: function (response) {
            alert(response.d);
        }
    });


    if ($("#MainContent_lblNvlAs").val() == $('#MainContent_ddlNvlAS').val() && $("#MainContent_lblNvlMh").val() == $('#MainContent_ddlNvlMH').val())
    {
        $("#btnNextStep4").show();

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
                        $('#gvEpisodeListBody').append(`<tr><td>${value.PK_Episodio}</td><td>${value.NB_Programa}</td><td>${fe_s}</td><td>${value.DE_ES_Episodio}</td><td><a class="btn btn-primary" data-toggle="tab" href="#wizard2" role="tab" aria-controls="wizard2" onclick="wizard1to2(${value.PK_Episodio},${value.FK_Programa},'${value.NB_Programa}',${value.FK_Persona},${value.FK_NivelCuidadoSustancias},${value.FK_NivelCuidadoMental},'${value.DE_AbusoSustancias}','${value.DE_SaludMental}');" type="button">Elegir</a></td></tr>`);
                    });


                    $('#chkDeleteExpediente').prop('checked', false);
                    if (lista.length == 1) {
                        $('#divDeleteExpediente').show();
                    }
                    else {
                        $('#divDeleteExpediente').hide();

                    }
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

function GetExpedienteOriginal(iup, programa) {
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
                if (p != null) {
                    $('#lblDeleteExpediente').text(`Eliminar expediente de ${$("#MainContent_lblNbPrograma").val()}`);
                    $('#rdExpedienteList').append(`<input type="radio" onChange="rdExpedienteChange(3);"  name="expediente" id="rdExpediente3" value="${p.NR_Expediente}"><label> ${p.NR_Expediente} - Copiar Expediente de ${$("#MainContent_lblNbPrograma").val()}</label><br>`);
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
function GetExpediente(iup, programa) {

    var lblIUPInfo1 = document.getElementById("MainContent_lblIUPInfo1").innerText;
    var lblNameInfo1 = document.getElementById("MainContent_lblNameInfo1").innerText;

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
                $("#rdExpedienteList").html("");
                if (p != null) {
                    //Tiene expediente
                    if (p.NR_Expediente != "") {
                        $('#rdExpedienteList').append(`<input type="radio" onChange="rdExpedienteChange(1);" disabled checked name="expediente" id="rdExpediente1" value="${p.NR_Expediente}"><label>${p.NR_Expediente} - ${lblNameInfo1} (IUP: ${lblIUPInfo1})</label><br>`);
                        $("#MainContent_lblExpedienteMsg").text(`(IUP: ${lblIUPInfo1}) ${lblNameInfo1} cuenta con un expediente en ${$("#MainContent_ddlPrograma option:selected").text()}`);
                        $("#MainContent_lblExpedienteMsg").show();
                        $("#btnNextStep3").show();
                    }
                }
                else {

                    $("#MainContent_lblExpedienteMsg").text(`(IUP: ${lblIUPInfo1}) ${lblNameInfo1} no cuenta con un expediente en ${$("#MainContent_ddlPrograma option:selected").text()}`);
                    $("#MainContent_lblExpedienteMsg").show();
                    GetExpedienteOriginal(iup, $("#MainContent_lblPrograma").val());
                    $('#rdExpedienteList').append(`<input type="radio" onChange="rdExpedienteChange(2);"  name="expediente" id="rdExpediente2" value=""><label>Crear Expediente</label><br>`);
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

function wizard1to2(episode, programa, nb_programa, persona, FK_NivelCuidadoSustancias, FK_NivelCuidadoMental, DE_NivelCuidadoSustancias, DE_NivelCuidadoMental) {
    if (episode != null && "" != episode && "" != persona) {
        $("#MainContent_lblEpisode").val(episode);
        $("#MainContent_lblPrograma").val(programa);
        $("#MainContent_lblNbPrograma").val(nb_programa);
        $("#MainContent_lblPersona").val(persona);
        $("#MainContent_lblNvlAs").val(FK_NivelCuidadoSustancias);
        $("#MainContent_lblNvlMh").val(FK_NivelCuidadoMental);
        $("#MainContent_lblDENvlAs").val(DE_NivelCuidadoSustancias);
        $("#MainContent_lblDENvlMh").val(DE_NivelCuidadoMental);

        document.getElementById("MainContent_wizard2Tab").click();
    }
}

function wizard2to3() {
    if ($("#MainContent_ddlPrograma").val() != -1) {
        GetExpediente($("#MainContent_lblPersona").val(), $("#MainContent_ddlPrograma").val());
        document.getElementById("MainContent_wizard3Tab").click();
    }
}

function wizard3to4() {

    if ($('input[name="expediente"]:checked').val() != "") {
       
        document.getElementById("MainContent_wizard4Tab").click();

    }
}

function wizard4to5() {

    if ($("#MainContent_ddlNvlMH").val() != "" && $("#MainContent_ddlNvlAS").val() != "") {
        $("#MainContent_lblEpisodeResume").text(`${$("#MainContent_lblEpisode").val()} - ${$("#MainContent_lblNameInfo1").text()}`);
        $("#MainContent_lblProgramaResume").text($("#MainContent_lblNbPrograma").val());
        $("#MainContent_lblProgramaResume2").text(`${$("#MainContent_ddlPrograma option:selected").text()}`);
        $("#MainContent_lblExpedienteResume").text($('input[name="expediente"]:checked').val());
        $("#MainContent_lblResumeNvlAs").text($("#MainContent_ddlNvlAS option:selected").text());
        $("#MainContent_lblResumeNvlMh").text($("#MainContent_ddlNvlMH option:selected").text());
        document.getElementById("MainContent_wizard5Tab").click();


    }

}

function wizard5to6() {

    if ($("#MainContent_lblEpisode").val() != "" && $("#MainContent_ddlNvlMH").val() != "" && $("#MainContent_ddlNvlAS").val() != "" && $('input[name="expediente"]:checked').val() != ""
        && $("#MainContent_ddlPrograma").val() != -1)
    {
        $("#btnNextStep6").hide();
        $("#btnBackStep6").show();
        TransferEpisode($("#MainContent_lblEpisode").val(), $("#MainContent_ddlPrograma").val(), $("#MainContent_ddlNvlMH").val(), $("#MainContent_ddlNvlAS").val(), $('input[name="expediente"]:checked').val(), $('input[name="expediente"]:checked').attr('id'), $('#chkDeleteExpediente').is(':checked') );
        document.getElementById("MainContent_wizard6Tab").click();
       
    }
}


function TransferEpisode(episode, program, nvlMh, nvlAs, expedienteNumber,expedienteOption,deleteExpediente) {
    $.ajax({
        type: "POST", //POST
        url: "ajax/TransferEpisodioPrograma.asmx/TransferEpisode",
        data: `{episode:${episode} , program:${program} , nvlMh:${nvlMh}, nvlAs:${nvlAs} , expedienteNumber:'${expedienteNumber}', expedienteOption:'${expedienteOption}' , deleteExpediente: ${deleteExpediente}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),

        async: false,
        success: function (obj) {

            if (obj != null) {

                var p = obj.d;

                if (p == true) {

                    $("#lblConfirmacion").text(`Se ha transferido exitosamente el episodio #${$("#MainContent_lblEpisode").val()} ha ${$("#MainContent_ddlPrograma option:selected").text()}`);
                    $("#btnNextStep6").show();
                    $("#btnBackStep6").hide();


                }
                else {
                    $("#lblConfirmacion").text(`No se ha transferido exitosamente el episodio #${$("#MainContent_lblEpisode").val()} ha ${$("#MainContent_ddlPrograma option:selected").text()}`);
                    $("#btnNextStep6").hide();
                    $("#btnBackStep6").show();


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

function wizard6to5() {
    document.getElementById("MainContent_wizard5Tab").click();
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






