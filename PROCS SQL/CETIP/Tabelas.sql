USE SIG
GO

/****** Object:  Table [dbo].[CETIP_POTENCIAL_ANALITICO]    Script Date: 21/10/2015 18:25:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CETIP_POTENCIAL_ANALITICO](
	[CNPJ] [varchar](14) NOT NULL,
	[Categoria] [varchar](50) NOT NULL,
	[Segmento] [varchar](20) NOT NULL,
	[Consorcio] [varchar](1) NOT NULL,
	[TipoPessoa] [varchar](1) NOT NULL,
	[Quantidade] [int] NOT NULL,
	[Volume] [decimal](18, 2) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CETIP_POTENCIAL_SINTETICO]    Script Date: 21/10/2015 18:25:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CETIP_POTENCIAL_SINTETICO](
	[CNPJ] [varchar](14) NULL,
	[Classificacao] [varchar](1) NULL,
	[RazaoSocial] [varchar](150) NULL,
	[NomeFantasia] [varchar](55) NULL,
	[CNAE] [varchar](174) NULL,
	[Endereco] [varchar](64) NULL,
	[Complemento] [varchar](137) NULL,
	[Numero] [varchar](10) NULL,
	[Bairro] [varchar](50) NULL,
	[UF] [varchar](2) NULL,
	[Cidade] [varchar](32) NULL,
	[CEP] [varchar](8) NULL,
	[Quantidade] [int] NULL,
	[Volume] [decimal](18, 2) NULL,
	[MarketShareQuant] [int] NULL,
	[MarketShareValor] [decimal](18, 2) NULL,
	[MarketShareAcimaQuant] [int] NULL,
	[MarketShareAcimaValor] [decimal](18, 2) NULL,
	[MarketShareAbaixoQuant] [int] NULL,
	[MarketShareAbaxoValor] [decimal](18, 2) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CETIP_RANKING]    Script Date: 21/10/2015 18:25:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CETIP_RANKING](
	[MesReferencia] [varchar](6) NOT NULL,
	[CNPJ] [varchar](14) NOT NULL,
	[Classificacao] [char](1) NOT NULL,
	[RazaoSocial] [varchar](150) NOT NULL,
	[NomeFantasia] [varchar](55) NOT NULL,
	[CNAE] [varchar](174) NOT NULL,
	[Endereco] [varchar](64) NOT NULL,
	[Complemento] [varchar](137) NOT NULL,
	[Número] [varchar](10) NULL,
	[Bairro] [varchar](50) NULL,
	[UF] [varchar](2) NULL,
	[Cidade] [varchar](32) NULL,
	[CEP] [varchar](8) NULL,
	[QuantidadedeBancos] [int] NOT NULL,
	[RankingCliente] [int] NOT NULL,
 CONSTRAINT [PK_Cetip_Ranking] PRIMARY KEY CLUSTERED 
(
	[MesReferencia] ASC,
	[CNPJ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
