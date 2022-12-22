<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ExtraccionesTEDS.aspx.cs" Inherits="SMUEE.App.Mod_TEDS.ExtraccionesTEDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        a.noclick {
            pointer-events: none;
        }

        table,
        th,
        td {
            border: 1px solid black;
            border-collapse: collapse;
        }
        /* setting the text-align property to center*/
        td {
            padding: 5px;
            text-align: center;
        }

        .center {
            display: block;
            margin-left: auto;
            margin-right: auto;
            width: 50%;
        }


        div.overflow {
            max-height: 250px;
            overflow-y: auto
        }


        .file-drop-area {
            position: relative;
            display: flex;
            align-items: center;
            max-width: 100%;
            padding: 25px;
            border: 1px dashed black;
            border-radius: 3px;
            transition: 0.2s;
        }

        .choose-file-button {
            flex-shrink: 0;
            border: 1px solid black;
            border-radius: 3px;
            padding: 8px 15px;
            margin-right: 10px;
            font-size: 12px;
            text-transform: uppercase;
        }

        .file-message {
            font-size: small;
            font-weight: 300;
            line-height: 1.4;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .file-input {
            position: absolute;
            left: 0;
            top: 0;
            height: 100%;
            width: 100%;
            cursor: pointer;
            opacity: 0;
        }

        .mt-100 {
            margin-top: 100px;
        }
    </style>






    <main>
        <header class="page-header page-header-compact page-header-light border-bottom bg-white mb-4">
            <div class="container-fluid px-4">
                <div class="page-header-content">
                    <div class="row align-items-center justify-content-between pt-3">
                        <div class="col-auto mb-3">
                            <h3 class="page-header-title">
                                <div class="page-header-icon">
                                    <i class="fa fa-download" aria-hidden="true"></i>
                                    Generación de Extracciones TEDS
                                </div>

                            </h3>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <!-- Main page content-->
        <div class="container-xl px-4 mt-n10">
            <!-- Wizard card example with navigation-->
            <div class="card">
                <div class="card-header border-bottom">
                    <ul class="nav nav-pills nav-justified flex-column flex-xl-row nav-wizard" id="cardTab" role="tablist">
                        <li class="nav-item" role="presentation" id="wizard1TabCard">
                            <a class="nav-link active noclick" id="wizard1Tab" runat="server" href="#wizard1" data-toggle="tab" role="tab" aria-controls="wizard1" aria-selected="true">
                                <div class="wizard-step-icon">1</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Rango de Fecha</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation" id="wizard2TabCard">
                            <a class="nav-link noclick" id="wizard2Tab" data-toggle="tab" runat="server" href="#wizard2" role="tab" aria-controls="wizard2" aria-selected="false">
                                <div class="wizard-step-icon">2</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Resumen</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation" id="wizard3TabCard">
                            <a class="nav-link noclick" id="wizard3Tab" data-toggle="tab" runat="server" href="#wizard3" role="tab" aria-controls="wizard3" aria-selected="false">
                                <div class="wizard-step-icon">3</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Descarga</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>

                        <li class="nav-item " role="presentation">
                            <a class="nav-link btn-info btn" href="#" data-toggle="modal" data-target="#modalModule">
                                <div class="wizard-step-icon"><i class="fa fa-info-circle fa-lg" aria-hidden="true"></i></div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Información</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>

               

                    </ul>
                </div>
                <div class="card-body">
                    <div class="tab-content" id="cardTabContent">
                        <!-- Wizard tab pane item 1-->
                        <div class="tab-pane py-5 py-xl-10 fade show active" id="wizard1" role="tabpanel" aria-labelledby="wizard1Tab">
                            <div class="row justify-content-center">
                                <div class="col-xxl-6 col-xl-8">
                                    <h3 class="text-primary">Paso 1</h3>
                                    <h5 class="card-title mb-4">Seleccionar programas:</h5>
                                    <div id="ddlProgram"></div>
                                    <br />
                                    <br />
                                    <div id="divDate">
                                        <h5 class="card-title mb-4">Seleccionar un rango de fecha para la generación de archivos TEDS</h5>

                                        <div>
                                            <label>Elige un rango de fecha:</label>

                                            <div class="row form-group">
                                                <div class="col-sm-5 d-flex justify-content-center align-items-center">
                                                    <div class="input-group date" style="width: 16.5rem;">
                                                        <asp:TextBox ID="startDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <span class="input-group-append">
                                                            <span class="input-group-text bg-white">
                                                                <i class="fa fa-calendar"></i>
                                                            </span>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2 text-center">
                                                    <asp:Label Text="al" runat="server" />
                                                </div>
                                                <div class="col-sm-5 d-flex justify-content-center align-items-center">
                                                    <div class="input-group date" style="width: 16.5rem;">
                                                        <asp:TextBox ID="endDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <span class="input-group-append">
                                                            <span class="input-group-text bg-white">
                                                                <i class="fa fa-calendar"></i>
                                                            </span>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>


                                        <hr class="my-4">
                                        <a class="btn btn-primary" data-toggle="tab" href="#wizard2" id="btnNextStep1" role="tab" aria-controls="wizard2" type="button">Siguiente</a>

                                        <div class="d-flex justify-content-between">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Wizard tab pane item 2-->
                        <div class="tab-pane py-5 py-xl-10 fade" id="wizard2" role="tabpanel" aria-labelledby="wizard2Tab">

                            <div class="row justify-content-center">
                                <div class="col-xxl-6 col-xl-8">
                                    <h3 class="text-primary">Paso 2</h3>
                                    <h5 class="card-title mb-4">Resumen de la transacción que desea realizar</h5>

                                    <br />
                                    <p>Programas seleccionados: </p>
                                    <hr class="my-4">
                                    <div id="divResume" class="overflow"></div>
                                    <br />
                                    <label id="lblResumen"></label>


                                    <br />
                                    <input type="checkbox" id="chkConfirmation" /><span><label for="chkConfirmation">Para finalizar debe confirmar la transacción marcando el encasillado.</label></span>

                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">
                                        <a class="btn btn-light" data-toggle="tab" href="#wizard1" role="tab" id="btnBackStep2" aria-controls="wizard1" onclick="wizard2to1();" type="button">Anterior</a>
                                        <a class="btn btn-primary" data-toggle="tab" href="#wizard3" id="btnNextStep2" role="tab" aria-controls="wizard3" type="button">Siguiente</a>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <!-- Wizard tab pane item 4-->

                        <div class="tab-pane py-5 py-xl-10 fade" id="wizard3" role="tabpanel" aria-labelledby="wizard3Tab">
                            <div class="row justify-content-center">
                                <div class="col-xxl-6 col-xl-8">
                                    <h3 class="text-primary">Paso 3</h3>
                                    <h5 class="card-title mb-4">Descargue sus documentos</h5>


                                    <table class="table table-bordered">

                                        <tr>
                                            <%--<td colspan="2">Admisión</td>--%>
                                            <td colspan="2">Transacciones Eliminadas</td>
                                        </tr>

                                        <tr>
                                        <%--    <td>
                                                <a onclick="GenerateFile('MHAD')" id="btnMHAD" href="#">Salud Mental</a>
                                            </td>
                                            <td>
                                                <a onclick="GenerateFile('SAAD')" id="btnSAAD" href="#">Abuso de Sustancias</a>

                                            </td>--%>
                                               <td>
                                                <a onclick="GenerateFile('DDIS')" id="btnDDIS" href="#">Paso 1 - Altas</a>

                                            </td>
                                                <td>
                                                <a onclick="GenerateFile('DAD')" id="btnDAD" href="#">Paso 2 - Admisiones</a>
                                            </td>
                                         
                                        </tr>


                                    </table>
                                    <br />
                                    <table class="table table-bordered">

                                        <tr>
                                  <%--          <td colspan="2">Altas</td>--%>
                                                      <td colspan="2">Archivos TEDS</td>
                                        </tr>


                                        <tr>
                                      <%--      <td>
                                                <a type="button" onclick="GenerateFile('MHDIS')" id="btnMHDIS" href="#">Salud Mental</a>

                                            </td>
                                            <td>
                                                <a type="button" onclick="GenerateFile('SADIS')" id="btnSADIS" href="#">Abuso de Sustancias</a>

                                            </td>--%>

                                                  <td>
                                                <a type="button" onclick="GenerateFile('AD')" id="btnAD" href="#">Paso 3 - Admisiones</a>

                                            </td>
                                            <td>
                                                <a type="button" onclick="GenerateFile('DIS')" id="btnDIS" href="#">Paso 4 - Altas</a>
                                            </td>
                                        </tr>

                                    </table>

                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">
                                        <a class="btn btn-light" id="btnBackStep3" data-toggle="tab" href="#wizard2" role="tab" aria-controls="wizard2" onclick="wizard3to2();" type="button">Anterior</a>
                                        <a class="btn btn-primary" id="btnNextStep3" href="<%=ResolveClientUrl("~/App/Mod_TEDS/ExtraccionesTEDS.aspx")%>">Terminar</a>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </main>

    <!-- Modal -->
    <div class="modal fade" id="modalModule" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">Generación de Extracciones TEDS
                    </h5>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <div>
                        <img src="../../Images/samhsalogo.png" height="200" class="center" />
                    </div>
                    <p>
                        Luego de seleccionar el rango de fecha el sistema generará los cuatro archivos en Excel en formato TEDS que pueden ser al Sistema DSS de SAMHSA y luego validados.
                       <br />
                        <br />
                        Deberá ver los siguientes archivos de transacciones de clientes: un archvo de admisión de salud mental (MHAD) ,
                        un archivo de admisiones de sustancias (SAAD), un archivo de altas/updates de salud mental (MHDIS) y un archivo de altas/updates sustancias (SADIS)
                    </p>

                    <br />
                    <br />

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Ok</button>
                    </div>
                </div>
            </div>
        </div>
    </div>




    <script type="text/javascript" src="<%=ResolveClientUrl("~/Scripts/modulos/TEDS/ExtraccionesTEDS.js?ver=5")%>"></script>

</asp:Content>


