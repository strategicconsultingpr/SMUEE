<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ExtraccionesTEDS.aspx.cs" Inherits="SMUEE.App.Mod_TEDS.ExtraccionesTEDS" %>

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
                                    Generación de Extracciones TEDS
                                </div>

                            </h3>
                        </div>
                        <%-- <div class="col-12 col-xl-auto mb-3">
                        <a class="btn btn-sm btn-light text-primary"  runat="server" href="~/Account/Register">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-user-plus me-1">
                                <path d="M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path><circle cx="8.5" cy="7" r="4"></circle><line x1="20" y1="8" x2="20" y2="14"></line><line x1="23" y1="11" x2="17" y2="11"></line></svg>
                            Registrar Nuevo Usuario
                        </a>
                    </div>--%>
                    </div>
                </div>
            </div>
        </header>

        <div class="container-xl px-4">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <div class="card text-center">
                        <div class="card-header">
                            <asp:Image ID="bannerASSMCA" ImageUrl="~/Content/img/TopBannerASSMCA.jpg" runat="server" CssClass="img-fluid" />
                            <asp:Image ID="logoSAMHSA" ImageUrl="~/Content/img/LogoSAMHSACorto.png" runat="server" CssClass="img-fluid" />
                        </div>
                        <div class="card-body">
                            <div class="card-title">GENERACIÓN DE EXTRACCIONES TEDS</div>
                            <p class="card-text">Luego de eleccionar el rango de fecha el sistema generará los cuatro archivos en Excel en formato TEDS que pueden ser al Sistema DSS de SAMHSA y luego validados. Deberá ver los siguientes archivos de transacciones de clientes: un archvo de admisión de salud mental (MH-AD) , un archivo de admisiones de sustancias (SA_AD), un archivo de altas/updates de salud mental (MH_DIS) y un archivo de altas/updates sustancias (SA_DIS)</p>
                            <hr />
                            <div class="row form-group">
                                <div class="col-sm-6 text-center">
                                    <asp:Label Text="Fecha Inicial" runat="server" />
                                </div>
                                <div class="col-sm-6 text-center">
                                    <asp:Label Text="Fecha Final" runat="server"/>
                                </div>
                            </div>
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
                                    <asp:Label Text="Al" runat="server" />
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
                            <asp:Button runat="server" Text="Generar Extracciones" OnClick="Generar_Click" OnClientClick="lodingOn()" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <asp:PlaceHolder runat="server" ID="divDescargar" Visible="false">
            <script>
                $('#divLoading').modal('hide');
            </script>
            <br>
        <div class="container-xl px-4">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <div class="card text-center">
                        <div class="card-header">
                            <strong>DESCARGAR ARCHIVOS TEDS</strong>
                            <asp:Label ID="fechaLabel" Text="" runat="server" />
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-ms-2">
                                    <strong>ADMISIONES:</strong>
                                </div>
                                <div class="col-sm-5">
                                    <asp:Button OnClick="File" CssClass="hyper-button" Text="Admisión Salud Mental" CommandArgument="TEDS_MH_AD.txt" runat="server" ID="MHAD"/>
                                </div>
                                <div class="col-sm-5">
                                    <asp:Button OnClick="File" CssClass="hyper-button" Text="Admisión Abuso de Sustancias" CommandArgument="TEDS_SA_AD.txt" runat="server" ID="SAAD"/>
                                </div>
                               
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-ms-2">
                                    <strong>ALTAS:</strong>
                                </div>
                                 <div class="col-sm-5">
                                    <asp:Button OnClick="File" CssClass="hyper-button" Text="Altas Salud Mental" CommandArgument="TEDS_MH_DIS.txt" runat="server" ID="MHDIS" />
                                </div>
                                <div class="col-sm-5">
                                    <asp:Button OnClick="File" CssClass="hyper-button" Text="Altas Abuso de Sustancias" CommandArgument="TEDS_SA_DIS.txt" runat="server" ID="SADIS"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </asp:PlaceHolder>


        <div class="modal fade" id="divLoading" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered modal-lg">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="text-center">
                            
                                <div class="spinner-border text-primary">
                            <span class="sr-only">Los archivos TEDS se estan generando...</span>
                                </div>
                            </div>
                        <div class="text-center text-primary">
                            <h3>Los archivos TEDS se estan generando...</h3>
                            </div>
                        
                    </div>
                </div>
            </div>
        </div>

    </main>
    <script type="text/javascript">
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }

        $('input[id*=MHAD]').click(function () {
            $(this).css("text-color", "green");
        })

        $(document).ready(function () {
           // $('#divLoading').modal('show');
        });

        function lodingOn() {
            $('#divLoading').modal('show');
        }
        function lodingOff() {
            $('#divLoading').modal('hide');
        }

        $(document).ready(function () {
            $('[id*=startDate]').datepicker({
                weekStart: 1,
                daysOfWeekHighlighted: "6,0",
                autoclose: true,
                todayHighlight: true,
            }).on('changeDate', function (selected) {
                var minDate = new Date(selected.date.valueOf());
                $('[id*=endDate]').datepicker('setStartDate', minDate);
            });

            $('[id*=endDate]').datepicker({
                weekStart: 1,
                daysOfWeekHighlighted: "6,0",
                autoclose: true,
                todayHighlight: true,
            }).on('changeDate', function (selected) {
                var maxDate = new Date(selected.date.valueOf());
                $('[id*=startDate]').datepicker('setEndDate', maxDate);
            });
        });

        

        $(document).ready(function () {
            var date = new Date();
            date.setFullYear(date.getFullYear() - 1);
            $('[id*=startDate]').datepicker("setDate", date);
        });

        $(document).ready(function () {
            $('[id*=endDate]').datepicker("setDate", new Date());
        });


    </script>
</asp:Content>
