﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="TransferenciaEpisodiosProgramas.aspx.cs" Inherits="SMUEE.App.Mod_MonitoreoSEPS.TransferenciaDeEpisodiosProgramas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        a.noclick {
            pointer-events: none;
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
                                   <i class="fa fa-wrench" aria-hidden="true"></i>Transferencia de Episodios a Otro Programa
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
                                    <div class="wizard-step-text-name">Seleccionar Episodio</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation" id="wizard2TabCard">
                            <a class="nav-link noclick" id="wizard2Tab" data-toggle="tab" runat="server" href="#wizard2" role="tab" aria-controls="wizard2" aria-selected="false">
                                <div class="wizard-step-icon">2</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Seleccionar Programa</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation" id="wizard3TabCard">
                            <a class="nav-link noclick" id="wizard3Tab" data-toggle="tab" runat="server" href="#wizard3" role="tab" aria-controls="wizard3" aria-selected="false">
                                <div class="wizard-step-icon">3</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Expediente</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation" id="wizard4TabCard">
                            <a class="nav-link noclick" id="wizard4Tab" data-toggle="tab" runat="server" href="#wizard4" role="tab" aria-controls="wizard4" aria-selected="false">
                                <div class="wizard-step-icon">4</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Niveles de Cuidado</div>
                                    <div class="wizard-step-text-details"></div>


                                </div>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation" id="wizard5TabCard">
                            <a class="nav-link noclick" id="wizard5Tab" data-toggle="tab" runat="server" href="#wizard5" role="tab" aria-controls="wizard5" aria-selected="false">
                                <div class="wizard-step-icon">5</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Resumen</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation" id="wizard6TabCard">
                            <a class="nav-link noclick" id="wizard6Tab" data-toggle="tab" runat="server" href="#wizard6" role="tab" aria-controls="wizard6" aria-selected="false">
                                <div class="wizard-step-icon">6</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Confirmación</div>
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
                                    <h5 class="card-title mb-4">Seleccionar episodio el cual se desea transferir</h5>

                                    <div>

                                        <label>Ingresar IUP del participante:</label>


                                        <div class="input-group">
                                            <input type="number" class="form-control bg-light border-0 small" runat="server" maxlength="9" id="txtIUP" placeholder="IUP" aria-label="Search" aria-describedby="basic-addon2">
                                            <div class="input-group-append">
                                                <button class="btn btn-primary" id="btnSearchIUP" type="button">
                                                    <i class="fas fa-search fa-sm"></i>
                                                </button>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtIUP" ID="rvIUP1"
                                                    CssClass="text-danger" ToolTip="El IUP es requerido." ErrorMessage="*" />
                                            </div>
                                        </div>
                                    </div>


                                    <div>


                                        <div runat="server" id="divParticipantInfo1">
                                            <label>Participante</label>
                                            <div class="row small text-muted">
                                                <div class="col-sm-3 text-truncate"><em>IUP:</em></div>
                                                <div class="col" runat="server" id="lblIUPInfo1"></div>
                                            </div>
                                            <div class="row small text-muted">
                                                <div class="col-sm-3 text-truncate"><em>Nombre:</em></div>
                                                <div class="col" runat="server" id="lblNameInfo1"></div>
                                            </div>
                                            <div class="row small text-muted">
                                                <div class="col-sm-3 text-truncate"><em>Fecha de Nacimiento:</em></div>
                                                <div class="col" runat="server" id="lblBornDateInfo1"></div>
                                            </div>
                                            <div class="row small text-muted">
                                                <div class="col-sm-3 text-truncate"><em>Sexo:</em></div>
                                                <div class="col" runat="server" id="lblSexInfo1"></div>
                                            </div>
                                            <div class="row small text-muted">
                                                <div class="col-sm-3 text-truncate"><em>Seguro Social</em></div>
                                                <div class="col" runat="server" id="lblSSNInfo1"></div>
                                            </div>
                                            <div class="row small text-muted">
                                                <div class="col-sm-3 text-truncate"><em>Edad</em></div>
                                                <div class="col" id="lblAgeInfo1" runat="server"></div>
                                            </div>
                                            <div class="row small text-muted">
                                                <div class="col-sm-3 text-truncate"><em>Veterano</em></div>
                                                <div class="col" id="lblVeteranInfo1" runat="server"></div>
                                            </div>
                                            <div class="row small text-muted">
                                                <div class="col-sm-3 text-truncate"><em>Grupo Etnico</em></div>
                                                <div class="col" id="lblEtniaInfo1" runat="server"></div>
                                            </div>


                                        </div>

                                        <div runat="server" id="lblNotEpisode" class="alert alert-warning" role="alert">
                                            Este participante no contiene episodios por el momento
                                        </div>
                                        <div runat="server" id="divEpisodes">

                                            <label runat="server">Seleccione un episodio</label>
                                            <div class="table-responsive">

                                                <table class="table table-bordered defaultTable display" style="width: 100%" id="gvEpisodeList">
                                                    <thead>
                                                        <tr>
                                                            <td># Episodio</td>
                                                            <td>Programa</td>
                                                            <td>Fecha</td>
                                                            <td>Estatus</td>
                                                            <td></td>

                                                        </tr>
                                                    </thead>

                                                </table>
                                                <label runat="server" id="lblTotal"></label>

                                            </div>
                                        </div>
                                    </div>





                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Wizard tab pane item 2-->
                        <div class="tab-pane py-5 py-xl-10 fade" id="wizard2" role="tabpanel" aria-labelledby="wizard2Tab">
                            <div class="row justify-content-center">
                                <div class="col-xxl-6 col-xl-8">
                                    <h3 class="text-primary">Paso 2</h3>
                                    <h5 class="card-title mb-4">Seleccionar programa el cual se desea transferir el episodio</h5>

                                    <div>
                                        <label>Elige el programa al que se desea transferir el episodio:</label>
                                        <div class="input-group">
                                            <asp:DropDownList CssClass="form-control bg-light border-0 small" runat="server" ID="ddlPrograma" DataTextField="NB_Programa" DataValueField="PK_Programa" placeholder="Elige" aria-label="Search" aria-describedby="basic-addon2" />
                                        </div>

                                    </div>

                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">
                                        <a class="btn btn-light" data-toggle="tab" href="#wizard1" role="tab" aria-controls="wizard1" onclick="wizard2to1();" type="button">Anterior</a>
                                        <a class="btn btn-primary" data-toggle="tab" href="#wizard3" id="btnNextStep2" role="tab" aria-controls="wizard3" onclick="wizard2to3();" type="button">Siguiente</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Wizard tab pane item 3-->
                        <div class="tab-pane py-5 py-xl-10 fade" id="wizard3" role="tabpanel" aria-labelledby="wizard3Tab">
                            <div class="row justify-content-center">
                                <div class="col-xxl-6 col-xl-8">
                                    <h3 class="text-primary">Paso 3</h3>
                                    <h5 class="card-title mb-4">Actualizar expediente de participante al cual se le transferirá el episodio</h5>

                                    <div runat="server" id="lblExpedienteMsg" class="alert alert-warning" role="alert">
                                    </div>
                                    <label class="text">1) Favor de elegir un número de expediente:</label>
                                    <div id="rdExpedienteList"></div>

                                    <asp:TextBox runat="server" TextMode="Number" onBlur="txtExpedienteChange();" MaxLength="12" CssClass="form-control" ID="txtExpediente" />

                                    <div id="divDeleteExpediente">

                                        <hr class="my-4">

                                        <label class="text">2) ¿Desea eliminar el expediente?</label>
                                        <br />
                                        <input type="checkbox" id="chkDeleteExpediente" /><span><label id="lblDeleteExpediente" for="chkDeleteExpediente"></label></span>


                                    </div>


                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">
                                        <a class="btn btn-light" data-toggle="tab" href="#wizard2" role="tab" aria-controls="wizard2" onclick="wizard3to2();" type="button">Anterior</a>
                                        <a class="btn btn-primary" data-toggle="tab" href="#wizard4" role="tab" id="btnNextStep3" aria-controls="wizard4" onclick="wizard3to4();" type="button">Siguiente</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Wizard tab pane item 4-->

                        <div class="tab-pane py-5 py-xl-10 fade" id="wizard4" role="tabpanel" aria-labelledby="wizard3Tab">
                            <div class="row justify-content-center">
                                <div class="col-xxl-6 col-xl-8">
                                    <h3 class="text-primary">Paso 4</h3>
                                    <div id="divMsgNvlAs" class="alert alert-warning" role="alert">
                                    </div>
                                    <div id="divMsgNvlMh" class="alert alert-warning" role="alert">
                                    </div>
                                    <div>
                                        <label>Actualizar nivel de cuidado</label>
                                        <div class="table-responsive">
                                            <table class="table table-bordered">
                                                <thead>
                                                    <tr>
                                                        <td>Tipo</td>
                                                        <td>Original</td>
                                                        <td>Nuevo</td>
                                                    </tr>
                                                </thead>

                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <label>Abuso de Sustancia</label>

                                                        </td>
                                                        <td>
                                                            <label id="nvlASCuiadoOriginal"></label>

                                                        </td>

                                                        <td>
                                                            <div class="input-group">
                                                                <asp:DropDownList CssClass="form-control bg-light border-0 small" runat="server" ID="ddlNvlAS" DataTextField="NB_Programa" DataValueField="PK_Programa" placeholder="Elige" aria-label="Search" aria-describedby="basic-addon2" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Salud Mental</label>
                                                        </td>
                                                        <td>
                                                            <label id="nvlMHCuiadoOriginal"></label>
                                                        </td>

                                                        <td>
                                                            <div class="input-group">
                                                                <asp:DropDownList CssClass="form-control bg-light border-0 small" runat="server" ID="ddlNvlMH" DataTextField="NB_Programa" DataValueField="PK_Programa" placeholder="Elige" aria-label="Search" aria-describedby="basic-addon2" />
                                                            </div>
                                                        </td>
                                                    </tr>

                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                    <hr class="my-4">

                                    <div class="d-flex justify-content-between">
                                        <a class="btn btn-light" data-toggle="tab" href="#wizard3" role="tab" aria-controls="wizard3" onclick="wizard4to3();" type="button">Anterior</a>
                                        <a class="btn btn-primary" data-toggle="tab" href="#wizard5" role="tab" id="btnNextStep4" aria-controls="wizard5" onclick="wizard4to5();" type="button">Siguiente</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Wizard tab pane item 5-->
                        <div class="tab-pane py-5 py-xl-10 fade" id="wizard5" role="tabpanel" aria-labelledby="wizard5Tab">
                            <div class="row justify-content-center">
                                <div class="col-xxl-6 col-xl-8">
                                    <h3 class="text-primary">Paso 5</h3>
                                    <h5 class="card-title mb-4">Resumen de la transacción que desea realizar</h5>

                                    <label>Resumen</label>
                                    <%--  --%>

                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <td></td>
                                                <td>Original</td>
                                                <td>Nuevo</td>
                                            </tr>
                                        </thead>

                                        <tbody>
                                            <tr>
                                                <td>Episodio</td>
                                                <td colspan="2">
                                                    <div runat="server" id="lblEpisodeResume"></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Programa</td>
                                                <td>
                                                    <div runat="server" id="lblProgramaResume"></div>
                                                </td>
                                                <td>
                                                    <div runat="server" id="lblProgramaResume2"></div>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>Nvl. Abuso de Sustancias</td>
                                                <td>
                                                    <div runat="server" id="lblResumeOriginalNvlAs"></div>
                                                </td>
                                                <td>
                                                    <div runat="server" id="lblResumeNvlAs"></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Nvl. Salud Mental</td>
                                                <td>
                                                    <div runat="server" id="lblResumeOriginalNvlMh"></div>
                                                </td>
                                                <td>
                                                    <div runat="server" id="lblResumeNvlMh"></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td># Expediente</td>
                                                <td>
                                                    <div runat="server" id="lblExpedienteOriginalResume"></div>
                                                </td>
                                                <td>
                                                    <div runat="server" id="lblExpedienteResume"></div>
                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>



                                    <br />
                                    <input type="checkbox" id="chkConfirmation" /><span><label for="chkConfirmation">Para finalizar debe confirmar la transacción marcando el encasillado.</label></span>

                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">
                                        <a class="btn btn-light" data-toggle="tab" href="#wizard4" role="tab" aria-controls="wizard4" onclick="wizard5to4();" type="button">Anterior</a>
                                        <a class="btn btn-primary" data-toggle="tab" href="#wizard6" role="tab" id="btnNextStep5" aria-controls="wizard5" onclick="wizard5to6();" type="button">Someter</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane py-5 py-xl-10 fade" id="wizard6" role="tabpanel" aria-labelledby="wizard5Tab">
                            <div class="row justify-content-center">
                                <div class="col-xxl-6 col-xl-8">
                                    <h3 class="text-primary">Step 5</h3>
                                    <h5 class="card-title mb-4">Confirmación de transacción</h5>

                                    <p id="lblConfirmacion"></p>
                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">
                                        <a class="btn btn-light" id="btnBackStep6" data-toggle="tab" href="#wizard5" role="tab" aria-controls="wizard6" onclick="wizard6to5();" type="button">Anterior</a>
                                        <a class="btn btn-primary" id="btnNextStep6" href="<%=ResolveClientUrl("~/App/Mod_MonitoreoSEPS/TransferenciaEpisodiosProgramas")%>">Terminar</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>


    <input type="number" runat="server" id="lblEpisode" hidden disabled />
    <input type="number" runat="server" id="lblPrograma" hidden disabled />
    <input type="number" runat="server" id="lblPersona" hidden disabled />
    <input type="text" runat="server" id="lblNbPrograma" hidden disabled />
    <input type="number" runat="server" id="lblNvlAs" hidden disabled />
    <input type="number" runat="server" id="lblNvlMh" hidden disabled />
    <input type="text" runat="server" id="lblDENvlAs" hidden disabled />
    <input type="text" runat="server" id="lblDENvlMh" hidden disabled />
    <input type="text" runat="server" id="lblExpedienteOriginal" hidden disabled />


    <!-- Modal -->
    <div class="modal fade" id="modalModule" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">Transferencia de Episodios a Otro Programa</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <p>Este módulo se utiliza para transferir un episodio de una persona a otro programa. Cuando se dice transferir entiéndase es con respecto al sistema de SEPS por ejemplo transferir un episodio que se registró de forma errónea en Metadona Caguas a Metadona San Juan.</p>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Ok</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="<%=ResolveClientUrl("~/Scripts/modulos/monitoreoSEPS/TransferEpisodioPrograma.js?ver=2")%>"></script>

</asp:Content>
