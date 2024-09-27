USE SIG
GO

/****** Object:  Table [dbo].[prot_ocorrencia_cart]    Script Date: 28/03/2016 *****
select * from [PROT_CARGA_CONFIRMA]
select * from [PROT_CONFIRMA_HISTORICO]
select * from AUX_PROT_REMESSA
select * from PROT_REMESSA_HIST

select * from PROT_RETORNO_HIST
select * from PROT_RETORNO
select * from PROT_CAPTURA_HIST
 SELECT * FROM PROT_OCORRENCIA_DE_PARA

select * from [prot_ocorrencia_cart]
SELECT * FROM [prot_contratos]

*/
insert [dbo].[prot_ocorrencia_cart] values ('1','Pago')

insert [dbo].[prot_ocorrencia_cart] values ('2','Protestado')
insert [dbo].[prot_ocorrencia_cart] values ('3','Retirado')
insert [dbo].[prot_ocorrencia_cart] values ('4','Sustado')
insert [dbo].[prot_ocorrencia_cart] values ('5','Devolvido pelo Cartório por irregularidades - Sem custas. Quando o cartório não cobrar custas quando da devolução do título por irregularidade.')
insert [dbo].[prot_ocorrencia_cart] values ('6','Devolvido pelo Cartório por irregularidades - Com custas. Liquidação em Condicional - Utilizada para os títulos liquidados em Cartório com cheque do próprio devedor - Esta ocorrencia é utilizada apenas para os cartórios que repassam ao banco o cheque do próprio devedor.')
insert [dbo].[prot_ocorrencia_cart] values ('7','Liquidação em Condicional - Utilizada para os títulos liquidados em Cartório com cheque do próprio devedor - Esta ocorrencia é utilizada apenas para os cartórios que repassam ao banco o cheque do próprio devedor.')
insert [dbo].[prot_ocorrencia_cart] values ('8','Título Aceito - Utilizado para Letras de Cambio e Duplicatas, para aceite do sacados.')
insert [dbo].[prot_ocorrencia_cart] values ('9','Edital, apenas nos Estados da Bahia e Rio de Janeiro')
insert [dbo].[prot_ocorrencia_cart] values ('A','Protesto do banco cancelado')
insert [dbo].[prot_ocorrencia_cart] values ('B','Protesto já efetuado')
insert [dbo].[prot_ocorrencia_cart] values ('C','Protesto por edital')
insert [dbo].[prot_ocorrencia_cart] values ('D','Retirada por edital')
insert [dbo].[prot_ocorrencia_cart] values ('E','Protesto de terceiro cancelado')
insert [dbo].[prot_ocorrencia_cart] values ('F','Desistencia do protesto por liquidação bancária')
insert [dbo].[prot_ocorrencia_cart] values ('G','Sustado Definitivo')
insert [dbo].[prot_ocorrencia_cart] values ('I','Emissão da 2a via do Instrumento do Protesto')
insert [dbo].[prot_ocorrencia_cart] values ('J','Cancelamento já efetuado anteriormente')
insert [dbo].[prot_ocorrencia_cart] values ('X','Cancelamento não efetuado')

/****** Object:  Table [dbo].[prot_ocorrencia_de_para]    Script Date: 28/03/2016 18:25:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prot_ocorrencia_de_para](

COD_CART		VARCHAR(20),
OCORRENCIA_CART	VARCHAR(300),
COD_FC			VARCHAR(20),
OCORRENCIA_FC	VARCHAR(300)

) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prot_contratos]    Script Date: 28/03/2016 18:25:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prot_contratos](
	
CONTRATO	VARCHAR(20),
OBS			VARCHAR(300)

 CONSTRAINT [PK_prot_contratos] PRIMARY KEY CLUSTERED 
(
	[CONTRATO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
