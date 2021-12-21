<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="SMUEE.Account.EditUser" %>

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
                                    Lista de Usuarios
                                </div>

                            </h3>
                        </div>
                        <div class="col-12 col-xl-auto mb-3">
                            <a class="btn btn-sm btn-light text-primary" href="<%=ResolveClientUrl("~/Account/UsersList")%>">
                                <svg class="fa-feather feather-arrow-left me-1" xmlns="http://www.we.org/2000/svg" width="24" height="24" viewbox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"
                                    stroke-linecap="round" stroke-linejoin="round">
                                <line x1="19" y1="12" x2="5" y2="12"></line>
                                <polyline points="12 19 5 12 12 5"></polyline>
                            </svg>
                                Volver a lista de Usuarios
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </header>


        <div class="container-xl px-4 mt-4">
            <div class="row">
                <div class="col-xl-4">
                    <div class="card mb-4 mb-xl-0">
                        <div class="card-header">Foto de Perfil</div>
                        <div class="card-body text-center">
                            <asp:Image ID="profileImg" ImageUrl="~" runat="server" CssClass="img-account-profile rounded-circle mb-2" />
                            <div class="small font-italic text-muted mb-4">JPG o PNG no mayor de 4 MB</div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:FileUpload runat="server" ID="imgUpload" ToolTip="Seleccione imagen de perfil" />
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button runat="server" OnClick="ChangeImg_Click" Text="Agregar Nueva Imagen" CssClass="btn btn-primary" />
                                </div>
                            </div>
                            <div class="row">
                                <asp:Label Text="" ID="lblChangeImg" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-8">
                    <div class="card mb-4">
                        <div class="card-header">Detalles del Perfil</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="form-group col-6">
                                    <label class="control-label">Correo Electrónico</label>
                                    <asp:TextBox runat="server" ID="Email" CssClass="form-control form-control-user" TextMode="Email" Placeholder="Ingrese el correo electrónico" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" CssClass="text-danger" ErrorMessage="El correo electrónico es requerido" />
                                </div>
                                <div class="form-group col-6">
                                    <label class="control-label">Teléfono</label>
                                    <asp:TextBox runat="server" ID="Telefono" CssClass="form-control form-control-user" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Telefono" CssClass="text-danger" ErrorMessage="Su telefono es requerido" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-6">
                                    <label class="control-label">Primer Nombre</label>
                                    <asp:TextBox runat="server" ID="NB_Primero" CssClass="form-control form-control-user" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NB_Primero" CssClass="text-danger" ErrorMessage="Su primer nombre es requerido" />
                                </div>
                                <div class="form-group col-6">
                                    <label class="control-label">Segundo Nombre</label>
                                    <asp:TextBox runat="server" ID="NB_Segundo" CssClass="form-control form-control-user" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="NB_Segundo" CssClass="text-danger" ErrorMessage="Su segundo nombre es requerido" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-6">
                                    <label class="control-label">Primer Apellido</label>
                                    <asp:TextBox runat="server" ID="AP_Primero" CssClass="form-control form-control-user" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="AP_Primero" CssClass="text-danger" ErrorMessage="Su primer apellido es requerido" />
                                </div>
                                <div class="form-group col-6">
                                    <label class="control-label">Segundo Apellido</label>
                                    <asp:TextBox runat="server" ID="AP_Segundo" CssClass="form-control form-control-user" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="AP_Segundo" CssClass="text-danger" ErrorMessage="Su segundo nombre es requerido" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-6">
                                    <label class="control-label">Rol de Usuario</label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRol"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRol" InitialValue="0"
                                        CssClass="text-danger" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Rol de Usuario." />
                                </div>
                                <div class="form-group col-6">
                                    <label class="control-label">Modulos Accesbiles</label>
                                    <asp:CheckBoxList ID="chkModulos" runat="server">
                                        <asp:ListItem Value="TEDS" Text="TEDS" />
                                        <asp:ListItem Value="MonitoreoSEPS" Text="Monitoreo SEPS" />
                                        <asp:ListItem Value="MantenimientoSEPS" Text="Mantenimiento SEPS" />
                                        <asp:ListItem Value="ReportesInformativos" Text="Reportes Informativos" />
                                    </asp:CheckBoxList>
                                </div>
                            </div>

                            <asp:Button runat="server" Text="Actualizar Usuario" OnClick="ProfileUpdate_Click" CssClass="btn btn-primary" />

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

        function sweetAlertRef(titulo, texto, icono, ref) {
            var baseUrl = "<%=ResolveClientUrl("~/")%>" + ref;

            swal({
                title: titulo,
                text: texto,
                icon: icono
            }).then((value) => { window.location.href = baseUrl; });
        }

        function sweetAlert(titulo, texto, icono) {
            swal({
                title: titulo,
                text: texto,
                icon: icono
            });
        }
    </script>
</asp:Content>
