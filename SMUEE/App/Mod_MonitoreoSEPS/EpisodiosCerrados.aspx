<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EpisodiosCerrados.aspx.cs" Inherits="SMUEE.App.Mod_MonitoreoSEPS.EpisodiosCerrados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <main>
        <header class="page-header page-header-compact page-header-light border-bottom bg-white mb-4">
            <div class="container-fluid px-4">
                <div class="page-header-content">
                    <div class="row align-items-center justify-content-between pt-3">
                        <div class="col-auto mb-3">
                            <h3 class="page-header-title">
                                <div class="page-header-icon">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-user">
                                        <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"></path><circle cx="12" cy="7" r="4"></circle></svg>
                                     Reabir Episodios (sin alta registrada)
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
                            <a class="nav-link active" id="wizard1Tab" runat="server"  href="#wizard1" data-toggle="tab" role="tab" aria-controls="wizard1" aria-selected="true">
                                <div class="wizard-step-icon">1</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Seleccionar Episodio</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation" id="wizard2TabCard">
                            <a class="nav-link" id="wizard2Tab" data-toggle="tab" runat="server"  href="#wizard2" role="tab" aria-controls="wizard2" aria-selected="false">
                                <div class="wizard-step-icon">2</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Resumen</div>
                                    <div class="wizard-step-text-details"></div>
                                </div>
                            </a>
                        </li>
                        <li class="nav-item" role="presentation" id="wizard3TabCard">
                            <a class="nav-link" id="wizard3Tab" data-toggle="tab" runat="server" href="#wizard3" role="tab" aria-controls="wizard3" aria-selected="false">
                                <div class="wizard-step-icon">3</div>
                                <div class="wizard-step-text">
                                    <div class="wizard-step-text-name">Confirmación</div>
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
                                    <h5 class="card-title mb-4">Seleccionar episodio el cual desea reabir</h5>

                                    <div>

                                        <label>Ingresar IUP del participante:</label>


                                        <div class="input-group">
                                            <input type="text" class="form-control bg-light border-0 small" runat="server" maxlength="9" id="txtIUP" placeholder="IUP" aria-label="Search" aria-describedby="basic-addon2">
                                            <div class="input-group-append">
                                                <button class="btn btn-primary" id="btnSearchIUP" type="button">
                                                    <i class="fas fa-search fa-sm"></i>
                                                </button>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtIUP" ID="rvIUP1"
                                                    CssClass="text-danger" ToolTip="El IUP es requerido." ErrorMessage="*" />
                                            </div>
                                        </div>
                                    </div>

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


                                    <div>
                                        <div runat="server" id="lblNotEpisode" class="alert alert-warning" role="alert">
                                           Este participante no contiene episodios elegibles para reabrir
                                        </div>
                                        <div runat="server" id="divEpisodes">

                                            <label runat="server">Episodios</label>
                                            <div class="table-responsive">

                                                <table class="table table-bordered" id="gvEpisodeList">
                                                    <thead>
                                                        <tr>
                                                            <td># Episodio</td>
                                                            <td>Programa</td>
                                                            <td>Fecha</td>
                                                            <td>Estatus</td>
                                                            <td></td>
                                                        </tr>
                                                    </thead>

                                                    <tbody id="gvEpisodeListBody">
                                                    </tbody>
                                                </table>
                                                <label runat="server" id="lblTotal"></label>

                                            </div>
                                        </div>
                                    </div>





                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">

                                        <a class="btn btn-primary" data-toggle="tab" href="#wizard2" id="btnNextStep1" role="tab" aria-controls="wizard2" onclick="wizard1to2();"  type="button">Siguiente</a>

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
                                      <label>Resumen</label>
                                    <div class="row small text-muted">
                                        <div class="col-sm-3 text-truncate"><em>Participante:</em></div>
                                        <div class="col" runat="server" id="lblPersonaResume"></div>
                                    </div>
                                     <div class="row small text-muted">
                                        <div class="col-sm-3 text-truncate"><em>Episodio para reabrir:</em></div>
                                        <div class="col" runat="server" id="lblEpisodeResume"></div>
                                    </div>
                                    <div class="row small text-muted">
                                        <div class="col-sm-3 text-truncate"><em>Programa:</em></div>
                                        <div class="col" runat="server" id="lblProgramaResume"></div>
                                    </div>
                                    
                                    <br />
                                    <input type="checkbox" id="chkConfirmation" /><span><label for="chkConfirmation">Para finalizar debe confirmar la transacción marcando el encasillado.</label></span>

                                    <hr class="my-4">
                                                                        <div class="d-flex justify-content-between">
                                        <a class="btn btn-light" data-toggle="tab" href="#wizard1" role="tab" aria-controls="wizard1" onclick="wizard2to1();" type="button">Anterior</a>
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
                                        <a class="btn btn-primary" id="btnNextStep3" href="<%=ResolveClientUrl("~/App/Mod_MonitoreoSEPS/EpisodiosCerrados")%>" >Terminar</a>
                                    </div>
                          
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </main>

    <input type="number" runat="server" id="lblPersona" hidden  disabled />
     <input type="number" runat="server" id="lblEpisode" hidden disabled />
    <input type="number" runat="server" id="lblPrograma" hidden  disabled />
    <input type="text" runat="server" id="lblNbPrograma" hidden disabled />
    

    <script type="text/javascript" src="<%=ResolveClientUrl("~/Scripts/modulos/monitoreoSEPS/EpisodiosCerrados.js")%>"></script>

</asp:Content>
