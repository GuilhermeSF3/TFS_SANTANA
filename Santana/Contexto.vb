

Namespace Seguranca
    Public Class Contexto

        Private Const _keyUsuario As String = "55B194F2-0B22-44B2-B40A-BAEDAA1A9295"
        Private Const _keyReport As String = "97DD9E72-658C-4DFB-A6B5-68008B0B6961"
        Private Const _keyNavegacao As String = "E5C39879-27F5-4E8B-9B67-213B221413F7"
        Private Const _keyDadosTranferencia As String = "236CFFD9-F8D3-4F4C-9E4F-E3B52C89A17B"
        Private Const _keyDadosMenu As String = "E6D95818-9464-4D58-8465-A0AFCF9D1813"

        Private m_UsuarioLogado As UsuarioLogado
        Private m_Relatorio As ReportDataSource
        Private m_Navegacao As Navegacao
        Private m_DadosTransferencia As DadosTransferencia
        Private m_DadosMenu As Menus


        Public Sub New()

        End Sub


        Public Property UsuarioLogado() As UsuarioLogado
            Get
                If HttpContext.Current.Session IsNot Nothing Then
                    If HttpContext.Current.Session(_keyUsuario) IsNot Nothing Then
                        m_UsuarioLogado = DirectCast(HttpContext.Current.Session(_keyUsuario), UsuarioLogado)
                    Else
                        m_UsuarioLogado = New UsuarioLogado
                        HttpContext.Current.Session(_keyUsuario) = m_UsuarioLogado
                    End If
                End If
                Return m_UsuarioLogado
            End Get

            Protected Set(value As UsuarioLogado)
                m_UsuarioLogado = value

                If HttpContext.Current.Session IsNot Nothing Then
                    If m_UsuarioLogado IsNot Nothing Then
                        HttpContext.Current.Session(_keyUsuario) = m_UsuarioLogado
                    Else
                        HttpContext.Current.Session(_keyUsuario) = Nothing
                    End If
                End If
            End Set
        End Property


        'Public Sub UpdateUser()
        '    UsuarioLogado = m_UsuarioLogado
        'End Sub

        'Public Sub NewUserContext()
        '    m_UsuarioLogado = Nothing
        '    UpdateUser()
        '    m_UsuarioLogado = New UsuarioLogado
        'End Sub


        Public Property Relatorio() As ReportDataSource
            Get
                If HttpContext.Current.Session IsNot Nothing Then
                    If HttpContext.Current.Session(_keyReport) IsNot Nothing Then
                        m_Relatorio = DirectCast(HttpContext.Current.Session(_keyReport), ReportDataSource)
                    Else
                        m_Relatorio = New ReportDataSource
                        HttpContext.Current.Session(_keyReport) = m_Relatorio
                    End If
                End If
                Return m_Relatorio
            End Get

            Protected Set(value As ReportDataSource)
                m_Relatorio = value

                If HttpContext.Current.Session IsNot Nothing Then
                    If m_Relatorio IsNot Nothing Then
                        HttpContext.Current.Session(_keyReport) = m_Relatorio
                    Else
                        HttpContext.Current.Session(_keyReport) = Nothing
                    End If
                End If

            End Set
        End Property



        Public Property Navegacao() As Navegacao
            Get
                If HttpContext.Current.Session IsNot Nothing Then
                    If HttpContext.Current.Session(_keyNavegacao) IsNot Nothing Then
                        m_Navegacao = DirectCast(HttpContext.Current.Session(_keyNavegacao), Navegacao)
                    Else
                        m_Navegacao = New Navegacao
                        HttpContext.Current.Session(_keyNavegacao) = m_Navegacao
                    End If
                End If
                Return m_Navegacao
            End Get

            Protected Set(value As Navegacao)
                m_Navegacao = value

                If HttpContext.Current.Session IsNot Nothing Then
                    If m_Navegacao IsNot Nothing Then
                        HttpContext.Current.Session(_keyNavegacao) = m_Navegacao
                    Else
                        HttpContext.Current.Session(_keyNavegacao) = Nothing
                    End If
                End If

            End Set
        End Property




        Public Property DadosTransferencia() As DadosTransferencia
            Get
                If HttpContext.Current.Session IsNot Nothing Then
                    If HttpContext.Current.Session(_keyDadosTranferencia) IsNot Nothing Then
                        m_DadosTransferencia = DirectCast(HttpContext.Current.Session(_keyDadosTranferencia), DadosTransferencia)
                    Else
                        m_DadosTransferencia = New DadosTransferencia
                        HttpContext.Current.Session(_keyDadosTranferencia) = m_DadosTransferencia
                    End If
                End If
                Return m_DadosTransferencia
            End Get

            Protected Set(value As DadosTransferencia)
                m_DadosTransferencia = value

                If HttpContext.Current.Session IsNot Nothing Then
                    If m_DadosTransferencia IsNot Nothing Then
                        HttpContext.Current.Session(_keyDadosTranferencia) = m_DadosTransferencia
                    Else
                        HttpContext.Current.Session(_keyDadosTranferencia) = Nothing
                    End If
                End If

            End Set
        End Property






        Public Property DadosMenu() As Menus
            Get
                If HttpContext.Current.Session IsNot Nothing Then
                    If HttpContext.Current.Session(_keyDadosMenu) IsNot Nothing Then
                        m_DadosMenu = DirectCast(HttpContext.Current.Session(_keyDadosMenu), Menus)
                    Else
                        m_DadosMenu = New Menus
                        HttpContext.Current.Session(_keyDadosMenu) = m_DadosMenu
                    End If
                End If
                Return m_DadosMenu
            End Get

            Protected Set(value As Menus)
                m_DadosMenu = value

                If HttpContext.Current.Session IsNot Nothing Then
                    If m_DadosMenu IsNot Nothing Then
                        HttpContext.Current.Session(_keyDadosMenu) = m_DadosMenu
                    Else
                        HttpContext.Current.Session(_keyDadosMenu) = Nothing
                    End If
                End If

            End Set
        End Property

    End Class



    Public Class DadosTransferencia

        Public Overridable Property Classe() As String
            Get
                Return m_classe
            End Get
            Set(value As String)
                m_classe = value
            End Set
        End Property
        Private m_classe As String


        Public Overridable Property DataReferencia() As String
            Get
                Return m_dataReferencia
            End Get
            Set(value As String)
                m_dataReferencia = value
            End Set
        End Property
        Private m_dataReferencia As String

        Public Overridable Property CodAgente() As Integer
            Get
                Return m_codAgente
            End Get
            Set(value As Integer)
                m_codAgente = value
            End Set
        End Property
        Private m_codAgente As Integer
        Public Overridable Property Agente() As String
            Get
                Return m_agente
            End Get
            Set(value As String)
                m_agente = value
            End Set
        End Property
        Private m_agente As String

        Public Overridable Property CodCobradora() As Integer
            Get
                Return m_codCobradora
            End Get
            Set(value As Integer)
                m_codCobradora = value
            End Set
        End Property
        Private m_codCobradora As Integer

        Public Overridable Property Cobradora() As String 
            Get
                Return m_cobradora
            End Get
            Set(value As String)
                m_cobradora = value
            End Set
        End Property
        Private m_cobradora As String

        Public Overridable Property Linha() As Integer
            Get
                Return m_linha
            End Get
            Set(value As Integer)
                m_linha = value
            End Set
        End Property
        Private m_linha As Integer


        Public Overridable Property CodProduto() As String
            Get
                Return m_codProduto
            End Get
            Set(value As String)
                m_codProduto = value
            End Set
        End Property
        Private m_codProduto As String


        Public Overridable Property CodTipoProduto() As String
            Get
                Return m_codTipoProduto
            End Get
            Set(value As String)
                m_codTipoProduto = value
            End Set
        End Property
        Private m_codTipoProduto As String



        Public Overridable Property CodAnalista() As String
            Get
                Return m_codAnalista
            End Get
            Set(value As String)
                m_codAnalista = value
            End Set
        End Property
        Private m_codAnalista As String


        Public Overridable Property CodReanalise() As String
            Get
                Return m_codReanalise
            End Get
            Set(value As String)
                m_codReanalise = value
            End Set
        End Property
        Private m_codReanalise As String


        Public Overridable Property CodEmpresa() As String
            Get
                Return m_codEmpresa
            End Get
            Set(value As String)
                m_codEmpresa = value
            End Set
        End Property
        Private m_codEmpresa As String


    End Class


    Public Class Navegacao

        'Public Overridable Property TituloPaginaAtual() As String
        '    Get
        '        Return m_TituloPaginaAtual
        '    End Get
        '    Set(value As String)
        '        m_TituloPaginaAtual = value
        '    End Set
        'End Property
        'Private m_TituloPaginaAtual As String

        'Public Overridable Property LinkPaginaAtual() As String
        '    Get
        '        Return m_LinkPaginaAtual
        '    End Get
        '    Set(value As String)
        '        m_LinkPaginaAtual = value
        '    End Set
        'End Property
        'Private m_LinkPaginaAtual As String



        'Public Overridable Property TituloPaginaAnterior() As String
        '    Get
        '        Return m_TituloPaginaAnterior
        '    End Get
        '    Set(value As String)
        '        m_TituloPaginaAnterior = value
        '    End Set
        'End Property
        'Private m_TituloPaginaAnterior As String


        'Public Overridable Property LinkPaginaAnterior() As String
        '    Get
        '        Return m_LinkPaginaAnterior
        '    End Get
        '    Set(value As String)
        '        m_LinkPaginaAnterior = value
        '    End Set
        'End Property
        'Private m_LinkPaginaAnterior As String


        Public Overridable Property LinkPaginaDetalhe() As String
            Get
                Return m_LinkPaginaDetalhe
            End Get
            Set(value As String)
                m_LinkPaginaDetalhe = value
            End Set
        End Property
        Private m_LinkPaginaDetalhe As String


        Public Overridable Property LinkPaginaAnteriorRelatorio() As String
            Get
                Return m_LinkPaginaAnteriorRelatorio
            End Get
            Set(value As String)
                m_LinkPaginaAnteriorRelatorio = value
            End Set
        End Property
        Private m_LinkPaginaAnteriorRelatorio As String

    End Class


    Public Class UsuarioLogado


        Public Overridable Property Login() As String
            Get
                Return m_Login
            End Get
            Set(value As String)
                m_Login = value
            End Set
        End Property
        Private m_Login As String


        Public Overridable Property NomeUsuario() As String
            Get
                Return m_NomeUsuario
            End Get
            Set(value As String)
                m_NomeUsuario = value
            End Set
        End Property
        Private m_NomeUsuario As String


        Public Overridable Property Funcao() As String
            Get
                Return m_Funcao
            End Get
            Set(value As String)
                m_Funcao = value
            End Set
        End Property
        Private m_Funcao As String


        Public Overridable Property codGerente() As Integer
            Get
                Return m_codGerente
            End Get
            Set(value As Integer)
                m_codGerente = value
            End Set
        End Property
        Private m_codGerente As Integer


        Public Overridable Property CodFilial() As Integer
            Get
                Return m_CodFilial
            End Get
            Set(value As Integer)
                m_CodFilial = value
            End Set
        End Property
        Private m_CodFilial As Integer


        Public Overridable Property Cpf() As String
            Get
                Return m_Cpf
            End Get
            Set(value As String)
                m_Cpf = value
            End Set
        End Property
        Private m_Cpf As String


        Public Overridable Property EMail() As String
            Get
                Return m_EMail
            End Get
            Set(value As String)
                m_EMail = value
            End Set
        End Property
        Private m_EMail As String


        Public Overridable Property Ativo() As Integer
            Get
                Return m_Ativo
            End Get
            Set(value As Integer)
                m_Ativo = value
            End Set
        End Property
        Private m_Ativo As Integer


        Public Overridable Property NomeCompleto() As String
            Get
                Return m_NomeCompleto
            End Get
            Set(value As String)
                m_NomeCompleto = value
            End Set
        End Property
        Private m_NomeCompleto As String


        Public Overridable Property Perfil() As Integer
            Get
                Return m_Perfil
            End Get
            Set(value As Integer)
                m_Perfil = value
            End Set
        End Property
        Private m_Perfil As Integer


        Public Overridable Property Produto() As Integer
            Get
                Return m_Produto
            End Get
            Set(value As Integer)
                m_Produto = value
            End Set
        End Property
        Private m_Produto As Integer


        Public Overridable Property SingIn() As Boolean
            Get
                Return m_SingIn
            End Get
            Set(value As Boolean)
                m_SingIn = value
            End Set
        End Property
        Private m_SingIn As Boolean


    End Class


    Public Class ReportDataSource

        Public Sub New()
            m_reportDatas = New List(Of reportData)
            m_subReportFileNames = New List(Of subReport)
        End Sub

        Public Overridable Property reportDatas() As IList(Of reportData)
            Get
                Return m_reportDatas
            End Get
            Set(value As IList(Of reportData))
                m_reportDatas = value
            End Set
        End Property
        Private m_reportDatas As IList(Of reportData)



        Public Overridable Property reportFileName() As String
            Get
                Return m_reportFileName
            End Get
            Set(value As String)
                m_reportFileName = value
            End Set
        End Property
        Private m_reportFileName As String


        Public Overridable Property subReportFileNames() As IList(Of subReport)
            Get
                Return m_subReportFileNames
            End Get
            Set(value As IList(Of subReport))
                m_subReportFileNames = value
            End Set
        End Property
        Private m_subReportFileNames As IList(Of subReport)





    End Class


    Public Class reportData

        Public Sub New(ByVal reportDataItem As DataSet)
            m_reportData = reportDataItem
        End Sub

        Public Overridable Property reportDataItem() As DataSet
            Get
                Return m_reportData
            End Get
            Set(value As DataSet)
                m_reportData = value
            End Set
        End Property
        Private m_reportData As DataSet
    End Class


    Public Class subReport

        Public Sub New(ByVal subReportFileName As String)
            m_subReportFileName = subReportFileName
        End Sub

        Public Overridable Property subReportFileName() As String
            Get
                Return m_subReportFileName
            End Get
            Set(value As String)
                m_subReportFileName = value
            End Set
        End Property
        Private m_subReportFileName As String

    End Class
















    Public Class Menus

        Public Sub New()
            m_menus = New List(Of ItemMenu)
        End Sub

        Public Overridable Property ListMenu() As IList(Of ItemMenu)
            Get
                Return m_menus
            End Get
            Set(value As IList(Of ItemMenu))
                m_menus = value
            End Set
        End Property
        Private m_menus As IList(Of ItemMenu)

    End Class


    Public Class ItemMenu

        Public Sub New(ByVal menuId As String, ByVal perfil As List(Of Int32))
            m_menuId = menuId
            m_menuPerfil = perfil
        End Sub


        Public Overridable Property MenuId() As String
            Get
                Return m_menuId
            End Get
            Set(value As String)
                m_menuId = value
            End Set
        End Property
        Private m_menuId As String

     
        Public Overridable Property Perfil() As List(Of Int32)
            Get
                Return m_menuPerfil
            End Get
            Set(value As List(Of Int32))
                m_menuPerfil = value
            End Set
        End Property
        Private m_menuPerfil As List(Of Int32)

    End Class



End Namespace