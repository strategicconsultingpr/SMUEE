<%@ Page Title="Account Confirmation" Language="C#" MasterPageFile="~/NoLogin.Master" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs" Inherits="SMUEE.Account.Confirm" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">


    <!-- Begin Page Content -->
    <div class="container-fluid">

        <!-- 404 Error Text -->
        <div class="text-center">
        <div class="card o-hidden border-0 shadow-lg my-5">
          <div class="card-body p-0">
            <asp:PlaceHolder runat="server" ID="successPanel" ViewStateMode="Disabled" Visible="true">
                <div class="row error mx-auto" data-text="404">Nuevo Usuario</div>
                <p class="lead text-gray-800 mb-5">Cuenta confirmada</p>
                <p class="text-gray-500 mb-0">Gracias por confirmar su nueva cuenta.</p>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/Login">Acceder a mi cuenta</asp:HyperLink>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="errorPanel" ViewStateMode="Disabled" Visible="false">
                <div class="error mx-auto" data-text="404">ERROR</div>
                <p class="lead text-gray-800 mb-5">Página no encontrada o enlace expirado</p>
            </asp:PlaceHolder>
            </div>
            </div>
        </div>

    </div>
</asp:Content>
