<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AltaAdministrativa.aspx.cs" Inherits="SMUEE.App.Mod_MonitoreoSEPS.AltaAdministrativa" %>

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


        div.overflow {
            max-height: 300px;
            overflow-y: auto
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
                                    <i class="fa fa-wrench" aria-hidden="true"></i>
                                    Altas por Sistema
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
                                    <div class="wizard-step-text-name">Razón de Alta </div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>

                        <li class="nav-item" role="presentation" id="wizard3TabCard">
                            <a class="nav-link noclick" id="wizard3Tab" data-toggle="tab" runat="server" href="#wizard3" role="tab" aria-controls="wizard3" aria-selected="false">
                                <div class="wizard-step-icon">3</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Resumen</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>

                        <li class="nav-item" role="presentation" id="wizard4TabCard">
                            <a class="nav-link noclick" id="wizard4Tab" data-toggle="tab" runat="server" href="#wizard4" role="tab" aria-controls="wizard4" aria-selected="false">
                                <div class="wizard-step-icon">4</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Resume</div>
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
                                    <h5 class="card-title mb-4">Seleccionar programa y episodios el cual desea crear un alta por sistema</h5>
                                    <div>
                                        <div>
                                            <label>Elige el programa:</label>
                                            <div class="input-group">
                                                <asp:DropDownList CssClass="form-control bg-light border-0 small" runat="server" ID="ddlPrograma" DataTextField="NB_Programa" DataValueField="PK_Programa" placeholder="Elige" aria-label="Search" aria-describedby="basic-addon2" />
                                            </div>
                                        </div>
                                        <div>
                                            <div runat="server" id="lblNotEpisode" class="alert alert-warning" role="alert">
                                                Este programas no contiene episodios para altas por sistema
                                            </div>
                                            <div runat="server" id="divEpisodes">
                                                <br />
                                                <h5 runat="server">Episodios:</h5>
                                                <br />
                                               <p>Para seleccionar un episodio favor de pulsar en la fila de dicho episodio. Puede seleccionar más de uno.</p>

                                                <div class="table-responsive">
                                                    <table class="table table-bordered altasAdDataTable display" style="width: 100%" id="gvEpisodeList">
                                                        <thead>
                                                            <tr>
                                                                <td># Episodio</td>
                                                                <td>Nombre participante</td>
                                                                <td>Fecha Admision</td>
                                                                <td>Tipo de Último Perfil</td>
                                                                <td>Meses Sin Perfiles de Evaluación de Progreso</td>
                                                            </tr>
                                                        </thead>


                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <hr class="my-4">
                                        <div class="d-flex justify-content-between">

                                            <a class="btn btn-primary" data-toggle="tab" href="#wizard2" id="btnNextStep1" role="tab" aria-controls="wizard2" type="button">Siguiente</a>

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
                                    <h5 class="card-title mb-4">Razón de alta</h5>
                                    <label>Favor de elegir una razón de alta. Si elige una razón de alta que no sea un alta por sistema, favor de indicar la fecha de alta, si no la conoce el perfil se registrará con la fecha en la que se realizó la transacción.</label>
                                    <div class="table-responsive">
                                        <table class="table table-bordered altasDataTable display" style="width: 100%" id="gvSelectedEpisode">
                                            <thead>
                                                <tr>
                                                    <td># Episodio</td>
                                                    <td>Nombre participante</td>
                                                    <td>Razón de alta</td>
                                                    <td>Fecha Alta</td>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                    <br />

                                    <input type="checkbox" id="chkConfirmation" /><span><label for="chkConfirmation">Si la información suministrada es la correcto favor de rellenar el siguiente encasillado.</label></span>

                                    <br />
                                    <hr class="my-4">

                                    <div class="d-flex justify-content-between">
                                        <a class="btn btn-light" data-toggle="tab" href="#wizard1" role="tab" id="btnBackStep2" aria-controls="wizard1" onclick="wizard2to1();" type="button">Anterior</a>
                                        <a class="btn btn-primary" data-toggle="tab" href="#wizard3" id="btnNextStep2" role="tab" aria-controls="wizard3" onclick="wizard2to3();" type="button">Siguiente</a>
                                    </div>
                                </div>



                            </div>




                        </div>




                        <!-- Wizard tab pane item 4-->

                        <div class="tab-pane py-5 py-xl-10 fade" id="wizard3" role="tabpanel" aria-labelledby="wizard3Tab">
                            <div class="row justify-content-center">
                                <div class="col-xxl-6 col-xl-8">
                                    <h3 class="text-primary">Paso 3</h3>
                                    <h5 class="card-title mb-4">Resume</h5>

                                    <div id="divResume" class="overflow"></div>


                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">
                                        <a class="btn btn-light" id="btnBackStep3" data-toggle="tab" href="#wizard2" role="tab" aria-controls="wizard2" onclick="wizard3to2();" type="button">Anterior</a>
                                        <a class="btn btn-primary" data-toggle="tab" href="#wizard4" id="btnNextStep3" role="tab" aria-controls="wizard4" onclick="wizard3to4();" type="button">Siguiente</a>
                                    </div>

                                </div>
                            </div>
                        </div>


                        <div class="tab-pane py-5 py-xl-10 fade" id="wizard4" role="tabpanel" aria-labelledby="wizard4Tab">
                            <div class="row justify-content-center">
                                <div class="col-xxl-6 col-xl-8">
                                    <h3 class="text-primary">Paso 4</h3>
                                    <h5 class="card-title mb-4">Confirmación de transacción</h5>
                                    <div id="divResume2" class="overflow"></div>
                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">
                                        <a class="btn btn-primary" id="btnNextStep5" href="<%=ResolveClientUrl("~/App/Mod_MonitoreoSEPS/AltaAdministrativa")%>">Terminar</a>
                                    </div>

                                </div>
                            </div>
                        </div>


                    </div>
                </div>

            </div>
        </div>
    </main>

    <input type="number" runat="server" id="lblPersona" hidden disabled />
    <input type="number" runat="server" id="lblEpisode" hidden disabled />
    <input type="number" runat="server" id="lblPrograma" hidden disabled />
    <input type="text" runat="server" id="lblNbPrograma" hidden disabled />


    <!-- Modal -->
    <div class="modal fade" id="modalModule" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">Altas por Sistemas
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <p>Este módulo tiene como propósito generar altas a episodios en el sistema de SEPS y utiliza como referencia el SAEP para poder identificar episodios elegibles para crear un alta por sistema.</p>
                      
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Ok</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/Scripts/modulos/monitoreoSEPS/AltaAdministrativa.js?ver=1")%>"></script>

</asp:Content>
