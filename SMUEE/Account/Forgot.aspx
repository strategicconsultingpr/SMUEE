<%@ Page Title="Forgot password" Language="C#" MasterPageFile="~/NoLogin.Master" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="SMUEE.Account.ForgotPassword" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <main>
        <div class="container-xl px-4">
            <div class="row justify-content-center">
                <div class="col-lg-5">

                    <!-- Basic forgot password form-->
                    <div class="card shadow-lg border-0 rounded-lg mt-5">
                        <div class="card-header justify-content-center">
                            <h3 class="fw-light my-4">Recuperar mi contraseña</h3>
                        </div>
                        <div class="card-body">
                            <asp:PlaceHolder ID="loginForm" runat="server">
                                <div class="small mb-3 text-muted">Ingrese su correo electrónico y le enviaremos un enlace para que recupere su contraseña.</div>
                                <!-- Forgot password form-->
                                <div>
                                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                        <p class="text-danger">
                                            <asp:Literal runat="server" ID="FailureText" />
                                        </p>
                                    </asp:PlaceHolder>
                                    <!-- Form Group (email address)-->
                                    <div class="mb-3">
                                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="small mb-1">Correo Electrónico</asp:Label>
                                        <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" Placeholder="Ingrese su correo electrónico" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                            CssClass="text-danger" ErrorMessage="Su correo electrónico es requerido." />
                                    </div>
                                    <!-- Form Group (submit options)-->
                                    <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
                                        <a class="small" href="<%=ResolveClientUrl("~/Account/Login")%>">Regresar a inicio</a>
                                        <asp:Button runat="server" OnClick="Forgot" Text="Enviar Enlance" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="DisplayEmail" Visible="false">
                                <p class="text-info">
                                    Favor verificar su correo electrónico para recuperar su contraseña.
                                </p>
                            </asp:PlaceHolder>
                        </div>
                        <div class="card-footer text-center">
                            <%--<div class="small"><a href="auth-register-basic.html">Need an account? Sign up!</a></div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
