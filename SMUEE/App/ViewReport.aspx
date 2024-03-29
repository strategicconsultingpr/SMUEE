﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="SMUEE.App.ViewReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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
                                    <asp:Label ID="lblReporte" Text="" runat="server" />
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
            <div class="card">
                <div class="card-body">
                    <div class="table-responsive">
                       <%-- <asp:ScriptManager ID="ScriptManagerReport" runat="server"></asp:ScriptManager>--%>
                        <rsweb:reportviewer id="rvSiteMapping" runat="server" showprintbutton="false" width="99.9%" height="100%" asyncrendering="true" zoommode="Percent" keepsessionalive="true" sizetoreportcontent="false" processingmode="Remote">
                        </rsweb:reportviewer>

                    </div>
                </div>
            </div>
        </div>

    </main>

</asp:Content>
