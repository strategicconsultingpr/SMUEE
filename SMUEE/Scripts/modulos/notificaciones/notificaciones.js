$("#MainContent_btnAdd").click(function () {
    $.ajax({
        type: "POST", //POST
        url: "ajax/WSnotificaciones.asmx/Add",
        data: `{iup: ${iup}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),
        success: function (obj) {

            if (obj != null && obj.d != null) {
                
            }
        

        },
        failure: function (response) {
            console.log(response);

        },
        error: function (response) {
            console.log(response);


        }
    });

});

$("#MainContent_btnModify").click(function () {
    $.ajax({
        type: "POST", //POST
        url: "ajax/WSnotificaciones.asmx/Modify",
        data: `{iup: ${iup}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),
        success: function (obj) {

            if (obj != null && obj.d != null) {

            }


        },
        failure: function (response) {
            console.log(response);

        },
        error: function (response) {
            console.log(response);


        }
    });

});

$("#MainContent_btnDelete").click(function () {
    $.ajax({
        type: "POST", //POST
        url: "ajax/WSnotificaciones.asmx/Delete",
        data: `{iup: ${iup}}`,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: sweetLoading(),
        success: function (obj) {

            if (obj != null && obj.d != null) {

            }


        },
        failure: function (response) {
            console.log(response);

        },
        error: function (response) {
            console.log(response);


        }
    });

});