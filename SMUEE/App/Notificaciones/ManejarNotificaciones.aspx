<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ManejarNotificaciones.aspx.cs" Inherits="SMUEE.App.Notificaciones.ManejarNotificaciones" %>

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
                                    Registro de Notificaciones
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
    </main>


    <div class="container-fluid px-4">

        <div class="card">
            <div class="card-body">
                <div class="form-row">
                    <div class="form-group col-md-6">
                        <label for="ddlIcon">Icono</label>
                        <asp:DropDownList runat="server" ID="ddlIcon" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="rvIcon" ControlToValidate="ddlIcon" Text="Icono es un campo requerido"></asp:RequiredFieldValidator>

                    </div>
                    <div class="form-group col-md-6">
                        <label for="txtTitle">Título</label>
                        <input type="text" class="form-control" id="txtTitle" runat="server" maxlength="100">
                        <asp:RequiredFieldValidator runat="server" ID="rvTitle" ControlToValidate="txtTitle" Text="Título es un campo requerido"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="form-group">
                    <label for="txtDescription">Descripción</label>
                    <input type="text" class="form-control" id="txtDescription" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ID="rvDescription" ControlToValidate="txtDescription" Text="Descripción es un campo requerido"></asp:RequiredFieldValidator>
                </div>


                <div class="form-group">
                    <label for="ddlIcon">Activo</label>
                    <asp:DropDownList runat="server" ID="ddlActive" CssClass="form-control">
                        <asp:ListItem Text="Sí" Value="1"></asp:ListItem>
                        <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="rvActive" ControlToValidate="ddlActive" Text="Activo es un campo requerido"></asp:RequiredFieldValidator>

                </div>


                <div class="form-group">

                    <div class="row">
                        <div class="col">
                            <label for="lstUser">Elegir Usuarios</label>
                        </div>
                        <div class="col">
                            <asp:Button ID="btnSelectAll" CssClass="btn btn-primary float-right" runat="server" Text="Seleccionar Todos" CausesValidation="false" OnClick="btnSelectAll_Click" />
                        </div>
                    </div>

                    <br />
                    <div style="overflow-y: scroll; height: 300px;" class="card">
                        <asp:CheckBoxList runat="server" CellSpacing="10" CellPadding="3" ID="lstUser">
                        </asp:CheckBoxList>

                    </div>
                </div>
                <hr />

                <asp:Button ID="btnAdd" CssClass="btn btn-primary" Text="Registrar" runat="server" OnClick="btnAdd_Click" />
                <asp:Button ID="btnModify" CssClass="btn btn-primary" Text="Modificar" runat="server" OnClick="btnModify_Click" />
                <asp:Button ID="btnDelete" CssClass="btn btn-danger" Text="Eliminar" runat="server" CausesValidation="false" OnClick="btnDelete_Click" />
                <a class="btn btn-info" href="<%=ResolveClientUrl("~/App/Notificaciones/Notificaciones")%>">Atras</a>


            </div>
        </div>
    </div>

    <script type="text/javascript">

    

        function sweetAlertRef(titulo, texto, icono, ref) {
            var baseUrl = "<%=ResolveClientUrl("~/")%>" + ref;

            swal({
                title: titulo,
                text: texto,
                icon: icono
            }).then((value) => { window.location.href = baseUrl; });
        }

        function sweetAlert(titulo, texto, icono) {
            var baseUrl = "<%=ResolveClientUrl("~/")%>" + ref;

            swal({
                title: titulo,
                text: texto,
                icon: icono
            }).then((value) => { window.location.href = baseUrl; });
        }
    </script>

</asp:Content>
