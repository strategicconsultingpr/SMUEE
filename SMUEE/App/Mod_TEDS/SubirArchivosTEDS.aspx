<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SubirArchivosTEDS.aspx.cs" Inherits="SMUEE.App.Mod_TEDS.SubirArchivosTEDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        a.noclick {
            pointer-events: none;
        }

        table,
        th,
        td {
            border: 1px solid black;
            border-collapse: collapse;
        }
        /* setting the text-align property to center*/
        td {
            padding: 5px;
            text-align: center;
        }

        .center {
            display: block;
            margin-left: auto;
            margin-right: auto;
            width: 50%;
        }


        div.overflow {
            max-height: 250px;
            overflow-y: auto
        }


        .file-drop-area {
            position: relative;
            display: flex;
            align-items: center;
            max-width: 100%;
            padding: 25px;
            border: 1px dashed black;
            border-radius: 3px;
            transition: 0.2s;
        }

        .choose-file-button {
            flex-shrink: 0;
            border: 1px solid black;
            border-radius: 3px;
            padding: 8px 15px;
            margin-right: 10px;
            font-size: 12px;
            text-transform: uppercase;
        }

        .file-message {
            font-size: small;
            font-weight: 300;
            line-height: 1.4;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .file-input {
            position: absolute;
            left: 0;
            top: 0;
            height: 100%;
            width: 100%;
            cursor: pointer;
            opacity: 0;
        }

        .mt-100 {
            margin-top: 100px;
        }
    </style>






    <main>
        <header class="page-header page-header-compact page-header-light border-bottom bg-white mb-4">
            <div class="container-fluid px-4">
                <div class="page-header-content">
                    <div class="row align-items-center justify-content-between pt-3">
                        <div class="col-auto mb-3">
                            <h3 class="page-header-title">
                                <div class="page-header-icon">
                                    <i class="fa fa-download" aria-hidden="true"></i>
                                    Subir Archivos Aceptados por TEDS
                                </div>

                            </h3>
                        </div>
                    </div>
                </div>
            </div>
        </header>

        <!-- Main page content-->
        <div class="container-xl px-4 mt-n10">
            <div id="Step1Div" runat="server">
                <label class="Favor de elegir el tipo de archivo que desea subir">Tipo de archivo:</label>
                                <label>Total de Transacciones: </label> <span runat="server" id="Span1"></span>

                <asp:DropDownList ID="ddlTypeFile" CssClass="form-control" runat="server">
                    <asp:ListItem Value="1">Admisión</asp:ListItem>
                    <asp:ListItem Value="2">Altas</asp:ListItem>

                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ForeColor="Red" Text="*" ControlToValidate="btnfileInput"></asp:RequiredFieldValidator>


                <br />
                <br />

                <div class="file-drop-area">
                    <span class="choose-file-button">Elige un archivo</span>
                    <span class="file-message">o arrastra y suelte un archivo aquí</span>
                    <asp:FileUpload runat="server" CssClass="file-input" accept=".xls,.xlsx,.csv" ID="btnfileInput" />
                </div>

                <br />
                <asp:RequiredFieldValidator ID="rvFileInput" runat="server" Display="Dynamic" ForeColor="Red" Text="Debe elegir un archivo" ControlToValidate="btnfileInput"></asp:RequiredFieldValidator>

                <p></p>
                <asp:Button ID="btnSubmitFile" CssClass="btn btn-primary" Text="Subir Archivos" OnClientClick="sweetLoadingProcesando();" OnClick="btnSubmitFile_Click" runat="server" />

            </div>

            <div id="Step2Div" runat="server">   
                
                <label>Total de Transacciones: </label> <span runat="server" id="txtTotal"></span>
                <br />
                 <label>Aceptadas: </label><span runat="server" id="txtAcceptadas"></span>
                <br />
                 <label>No Encontradas: </label><span runat="server" id="txtNoEncontradas"></span>
                <br />


                
                <a id="btnOtherFile" class="btn btn-primary" href="<%=ResolveClientUrl("~/App/Mod_TEDS/SubirArchivosTEDS")%>" >Subir Otro Archivo</a>
                <asp:Button ID="btnNoEncontrados" CssClass="btn btn-warning" runat="server" OnClick="btnNoEncontrados2_Click" Text=" Descargar No encontrados" />

            </div>


        </div>
        
    </main>

      <!-- Modal -->
    <div class="modal fade" id="modalModule" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">Generación de Extracciones TEDS
                    </h5>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <div>
                        <img src="../../Images/samhsalogo.png" height="200" class="center" />
                    </div>
                    <p>
                        Luego de seleccionar el rango de fecha el sistema generará los cuatro archivos en Excel en formato TEDS que pueden ser al Sistema DSS de SAMHSA y luego validados.
                       <br />
                        <br />
                        Deberá ver los siguientes archivos de transacciones de clientes: un archvo de admisión de salud mental (MHAD) ,
                        un archivo de admisiones de sustancias (SAAD), un archivo de altas/updates de salud mental (MHDIS) y un archivo de altas/updates sustancias (SADIS)
                    </p>

                    <br />
                    <br />

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Ok</button>
                    </div>
                </div>
            </div>
        </div>
    </div>





    <script type="text/javascript" src="<%=ResolveClientUrl("~/Scripts/modulos/TEDS/SubirArchivosTEDS.js?ver=1")%>"></script>


</asp:Content>
