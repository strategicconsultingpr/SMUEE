<%@ Page Title="Register" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SMUEE.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%--<h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new account</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
    </div>--%>



    <div>
        <div class="container-xl px-4">
        <div class="row align-items-center justify-content-between pt-3">
            <div class="col-auto mb-3">
                <h1 class="h3 mb-2 text-gray-800"><i class="fas fa-fw fa-user-plus"></i>  Agregar Nuevo Usuario</h1>
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
        

        <hr>

            <div class="row justify-content-lg-center">
                            <!-- Donut Chart -->
                            <div class="col-xl-4 col-lg-7">
                            <div class="card shadow mb-4">
                                <!-- Card Header - Dropdown -->
                                <div class="card-header py-3">
                                  <h6 class="m-0 font-weight-bold text-primary">Foto de Perfil</h6>
                                </div>
                                <!-- Card Body -->
                                <div class="card-body text-center">
                                    <asp:Image ID="profileImg" ImageUrl="~" runat="server" CssClass="img-account-profile rounded-circle mb-2" />
                                          
                                      <p class="text-center mb-4">JPG o PNG no mayor de 5 MB</p>
                                      <div class="d-grid gap-2 d-md-flex justify-content-md-center">
                                          <asp:Button runat="server" Text="Agregar Nueva Imagen" CssClass="btn btn-primary" />
                                      </div>
                                  <hr>
                                   
                                </div>
                              </div>
                            </div>


                            <!-- Basic Card Example -->
                            <div class="col-xl-6 col-lg-5">
                              <div class="card shadow mb-4">
                                <div class="card-header py-3">
                                  <h6 class="m-0 font-weight-bold text-primary">Detalles de mi Perfil</h6>
                                </div>
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
                                          CssClass ="text-danger" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Rol de Usuario." />
                                      </div>
                                        <div class="form-group col-6">
                                            <label class="control-label">Modulos Accesbiles</label>
                                            <asp:CheckBoxList ID="chkModulos" runat="server" >
                                                <asp:ListItem Value="TEDS" Text="TEDS" />
                                                <asp:ListItem Value="MonitoreoSEPS" Text="Monitoreo SEPS" />
                                                <asp:ListItem Value="MantenimientoSEPS" Text="Mantenimiento SEPS" />
                                                <asp:ListItem Value="ReportesInformativos" Text="Reportes Informativos" />
                                            </asp:CheckBoxList>
                                      </div>
                                  </div>

                                    <asp:Button runat="server" Text="Agregar Usuario" OnClick="CreateUser_Click"  CssClass="btn btn-primary" />

                                </div>
                              </div>
                            </div>

                            </div>


        

    </div>




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
