<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Notificaciones.aspx.cs" Inherits="SMUEE.App.Notificaciones.Notificaciones" %>

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
                                    Notificaciones
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


        <div class="container-fluid px-4">

            <a class="btn btn-primary text-center float-right" href="ManejarNotificaciones.aspx">Registrar Notificación</a>
            <br />
            <br />
            <div class="card">

                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="gvNotificationList" CssClass="table table-bordered notificationsListTable" Width="100%" AutoGenerateColumns="false"  DataKeyNames="TITULO">
                            <Columns>
                                <asp:TemplateField HeaderText="Icono">
                                    <ItemTemplate>
                                        <div class='icon-circle <%#Eval("COLOR_ICONO")%>'><i class='<%#Eval("NB_ICONO")%>'></i></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TITULO" HeaderText="Título" />
                                <asp:BoundField DataField="DE_NOTIFICACIONES" HeaderText="Descripción" />
                                <asp:BoundField DataField="FE_ENVIADO" HeaderText="Fecha" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <a class="form-control btn btn-warning text-center" href='<%=ResolveClientUrl("~/App/Notificaciones/ManejarNotificaciones")%>?PkNotification=<%# Eval("PK_NOTIFICACIONES")%>'>Editar</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No existen notificaciones
                            </EmptyDataTemplate>
                        </asp:GridView>


                    </div>

                </div>

            </div>

        </div>

    </main>

</asp:Content>
