<%@ Page Language="VB" MasterPageFile="~/SantanaWeb.master" AutoEventWireup="true" CodeBehind="TermoDevolucao.aspx.vb" Inherits="Santana.Paginas.TI.TermoDevolucao" Title="Termo devolução" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Componentes" Namespace="Componentes" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Termo devolução</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
  



    <form id="form1" runat="server">
        <div class="container">
            <div class="card">
                <div class="card-header text-center">
                    <h4>Escolha o Presente para o Amigo Secreto</h4>
                </div>
                <div class="card-body">
                    <div class="mb-3">
                        <label for="txtUrlPresente" class="form-label">URL do Presente</label>
                        <input type="text" class="form-control" id="txtUrlPresente" runat="server" placeholder="Coloque a URL do presente aqui">
                    </div>
                    <button type="button" class="btn btn-submit btn-block" runat="server">
                        <i class="fas fa-gift"></i> Adicionar Presente
                    </button>
                </div>
            </div>

            <div class="card mt-4">
                <div class="card-header text-center">
                    <h4>Presentes dos Colaboradores</h4>
                </div>
                <div class="card-body">
                    <asp:GridView ID="gridPresentes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="UsuarioId" HeaderText="Colaborador" SortExpression="UsuarioId" />
                            <asp:BoundField DataField="UrlPresente" HeaderText="Presente" SortExpression="UrlPresente" />
                            <asp:BoundField DataField="DataCadastro" HeaderText="Data de Adição" SortExpression="DataCadastro" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>

  


        </ContentTemplate>

    </asp:UpdatePanel>






    <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
            <div class="overlay" />
            <div id="SpingLoad" class="overlayContent">
                <h2>Carregando</h2>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

  <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"></script>
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.min.js"></script>


  <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

  <script>

      function showSuccessAlert() {
          Swal.fire({
              icon: 'success',
              title: 'Presente Adicionado!',
              text: 'O presente foi adicionado com sucesso.',
              confirmButtonColor: '#007bff',
          });
      }
  </script>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet">


<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">


<link href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" rel="stylesheet">

                
  <style>
      body {
          background-color: #f8f9fa;
      }

      .container {
          margin-top: 50px;
      }

      .card {
          border-radius: 15px;
      }

      .card-header {
          background-color: #007bff;
          color: white;
          font-size: 18px;
      }

      .btn-submit {
          background-color: #007bff;
          color: white;
          border: none;
      }

      .btn-submit:hover {
          background-color: #0056b3;
      }

      .table th, .table td {
          vertical-align: middle;
      }
  </style>


</asp:Content>





