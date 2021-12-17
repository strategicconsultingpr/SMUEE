<%@ Page Title="Password Changed" Language="C#" MasterPageFile="~/NoLogin.Master" AutoEventWireup="true" CodeBehind="ResetPasswordConfirmation.aspx.cs" Inherits="SMUEE.Account.ResetPasswordConfirmation" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <!-- Begin Page Content -->
    <div class="container-fluid">

        <!-- 404 Error Text -->
        <div class="text-center">
        <div class="card o-hidden border-0 shadow-lg my-5">
          <div class="card-body p-0">
              <div class="row error mx-auto" data-text="404">Actualización de Contraseña</div>
                <p class="lead text-gray-800 mb-5">Nueva Contraseña Registrada</p>
                <p class="text-gray-500 mb-0">Gracias por registrar su nueva contraseña.</p>
                <asp:HyperLink ID="login" runat="server" NavigateUrl="~/Account/Login">Acceder a mi cuenta</asp:HyperLink>
            </div>
            </div>
        </div>

    </div>
</asp:Content>
