<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Entrada.aspx.cs" Inherits="SMUEE.Entrada" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <%--<header class="page-header page-header-dark bg-gradient-primary-to-secondary pb-2">
            <div class="container-xl px-4">
                <div class="page-header-content pt-4">
                    <div class="row align-items-center justify-content-between">
                        <div class="col-auto mt-4">
                            <h1 class="page-header-title">
                                Sistema Modular de la UEE
                            </h1>
                            <div class="page-header-subtitle">Sistema en el cual se encuentran los diferentes reportes y módulos accesibles por la UEE</div>
                        </div>
                    </div>
                </div>
            </div>
        </header>--%>


        <div class="container-xl px-4 mt-5">
                        <!-- Custom page header alternative example-->
                        <div class="d-flex justify-content-between align-items-sm-center flex-column flex-sm-row mb-4">
                            <div class="me-4 mb-3 mb-sm-0">
                                <h1 class="mb-0">Tablero Principal</h1>
                                <div class="small">
                                    <span class="fw-500 text-primary"><asp:Label ID="lblDia" Text="" runat="server" /></span>
                                    <asp:Label ID="lblFecha" Text="" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- Illustration dashboard card example-->
                        <div class="card card-waves mb-4 mt-5">
                            <div class="card-body p-5">
                                <div class="row align-items-center justify-content-between">
                                    <div class="col">
                                        <h2 class="text-primary">¡Bienvenido al Sistema Modular de la UEE!</h2>
                                        <p class="text-gray-700">En esta aplicación web se encuentran los diferentes módulos y reportes asociados a tareas realizadas por el equipo de la UEE</p>
                                        
                                    </div>
                                    <div class="col d-none d-lg-block mt-xxl-n4"><img class="img-fluid px-xl-4 mt-xxl-n5" src="<%=ResolveClientUrl("~/Content/img/undraw_team_collaboration_re_ow29.svg")%>"></div>
                                </div>
                            </div>
                        </div>
            </div>

    </main>

</asp:Content>
