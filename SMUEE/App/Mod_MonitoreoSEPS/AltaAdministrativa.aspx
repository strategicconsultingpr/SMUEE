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
                                    Altas Administrativas
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
                                    <div class="wizard-step-text-name">Resumen</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation" id="wizard3TabCard">
                            <a class="nav-link noclick" id="wizard3Tab" data-toggle="tab" runat="server" href="#wizard3" role="tab" aria-controls="wizard3" aria-selected="false">
                                <div class="wizard-step-icon">3</div>
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
                                    <h5 class="card-title mb-4">Seleccionar episodio el cual desea crear un alta administrativa</h5>

                                    <div>

                                        <div>
                                            <label>Elige el programa:</label>
                                            <div class="input-group">
                                                <asp:DropDownList CssClass="form-control bg-light border-0 small" runat="server" ID="ddlPrograma" DataTextField="NB_Programa" DataValueField="PK_Programa" placeholder="Elige" aria-label="Search" aria-describedby="basic-addon2" />
                                            </div>

                                        </div>


                                        <div>
                                            <div runat="server" id="lblNotEpisode" class="alert alert-warning" role="alert">
                                                Este programas no contiene episodios para altas administrativas
                                            </div>
                                            <div runat="server" id="divEpisodes">

                                                <label runat="server">Episodios</label>
                                                <div class="table-responsive">

                                                    <table class="table table-bordered altasAdDataTable display" style="width: 100%" id="gvEpisodeList">
                                                        <thead>
                                                            <tr>
                                                                <td>Seleccionar</td>
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
                                    <label>Se creará un alta administrativa para los siguientes episodios:</label>


                                    <div id="divResume" style="height: 300px; overflow-y: scroll;">
                                      
                                    </div>

                                    <br />
                                    <input type="checkbox" id="chkConfirmation" /><span><label for="chkConfirmation">Para finalizar debe confirmar la transacción marcando el encasillado.</label></span>

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
                                    <h5 class="card-title mb-4">Confirmación de transacción</h5>

                                    <p id="lblConfirmacion"></p>

                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">
                                        <a class="btn btn-light" id="btnBackStep3" data-toggle="tab" href="#wizard2" role="tab" aria-controls="wizard2" onclick="wizard3to2();" type="button">Anterior</a>
                                        <a class="btn btn-primary" id="btnNextStep3" href="<%=ResolveClientUrl("~/App/Mod_MonitoreoSEPS/AltaAdministrativa")%>">Terminar</a>
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
                    <h5 class="modal-title" id="exampleModalLongTitle">Altas Administrativas
                    </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <p></p>
                    <p><b></b></p>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Ok</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/Scripts/modulos/monitoreoSEPS/AltaAdministrativa.js?ver=1")%>"></script>

</asp:Content>
