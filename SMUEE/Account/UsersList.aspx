<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="UsersList.aspx.cs" Inherits="SMUEE.Account.UsersList" %>

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
                                    <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"></path><circle cx="12" cy="7" r="4"></circle></svg> Lista de Usuarios</div>
                            
                        </h3>
                    </div>
                    <div class="col-12 col-xl-auto mb-3">
                        <%--<a class="btn btn-sm btn-light text-primary" href="user-management-groups-list.html">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-users me-1">
                                <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path><circle cx="9" cy="7" r="4"></circle><path d="M23 21v-2a4 4 0 0 0-3-3.87"></path><path d="M16 3.13a4 4 0 0 1 0 7.75"></path></svg>
                            Manejar Modulos
                        </a>--%>
                        <a class="btn btn-sm btn-light text-primary"  runat="server" href="~/Account/Register">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-user-plus me-1">
                                <path d="M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path><circle cx="8.5" cy="7" r="4"></circle><line x1="20" y1="8" x2="20" y2="14"></line><line x1="23" y1="11" x2="17" y2="11"></line></svg>
                            Registrar Nuevo Usuario
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </header>


    <div class="container-fluid px-4">
        <div class="card">
            <div class="card-body">
                <div class="table-responsive">

                    <asp:GridView runat="server" ID="gvUsersList" CssClass="table table-bordered usersListTable" Width="100%" AutoGenerateColumns="false" DataKeyNames="Usuario">
                        <Columns>
                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                            <asp:BoundField DataField="Email" HeaderText="CorreoElectrónico" />
                            <asp:BoundField DataField="Rol" HeaderText="Rol" />
                            <%--<asp:BoundField DataField="Modulos" HeaderText="Modulos" />--%>
                            <asp:TemplateField HeaderText="Módulos Accesibles">
                                <ItemTemplate>
                                    <div class="row">&nbsp <%# Eval("Modulos") %></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones"> 
                                <ItemTemplate>
                                    <asp:Label ID="Edit" runat="server" Enabled="true"></asp:Label>
                                    <a href='<%=ResolveClientUrl("~/Account/EditUser.aspx?pk_usuario=")%><%#Eval("PK_Usuario")%>' ><i class='fas fa-fw fa-edit'></i></a>
                                    <%--<asp:Label ID="Delete" runat="server" Enabled="true"></asp:Label>
                                    <a class="btn btn-datatable btn-icon btn-transparent-dark me-2"  href='~/Account/DeleteUser.aspx?pk_usuario=<%#Eval("PK_Usuario")%>'><i class='fas fa-fw fa-trash-alt'></i></a>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                        <%--<i class='fas fa-fw fa-trash-alt'></i>--%>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</main>
</asp:Content>
