USE SIG
GO

/****** Object:  Table [dbo].[prot_ocorrencia_cart]    Script Date: 28/03/2016 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON

GO
DROP   TABLE [dbo].[PROT_CARGA_CONFIRMA]
CREATE TABLE [dbo].[PROT_CARGA_CONFIRMA](

    Codigo_Cliente	VARCHAR(20),
    Nome_Cliente		VARCHAR(200),
    Contrato		VARCHAR(20),
    Parcela				VARCHAR(3),
    Protocolo		VARCHAR(30),
    Dt_Recepcao		DATETIME,
    Cartorio			VARCHAR(200),
    Ibge			VARCHAR(200),
    Cidade				VARCHAR(200)

) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[prot_ocorrencia_de_para]    Script Date: 28/03/2016 18:25:43 ******/
DROP   TABLE [dbo].[PROT_CONFIRMA_HISTORICO]

CREATE TABLE [dbo].[PROT_CONFIRMA_HISTORICO](

    Data_Mov		DATETIME,
    Codigo_Cliente	VARCHAR(20),
    Nome_Cliente		VARCHAR(200),
    Contrato		VARCHAR(20),
    Parcela				VARCHAR(3),
    Protocolo		VARCHAR(30),
    Dt_Recepcao		DATETIME,
    Cartorio			VARCHAR(200),
    Ibge			VARCHAR(200),
    Cidade				VARCHAR(200)

) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

-- ************************************** REMESSAS
DROP table AUX_PROT_REMESSA 
DROP table PROT_REMESSA_HIST

create table AUX_PROT_REMESSA (
Data_Mov	datetime,
CLIENTE		varchar(200),
ENDERECO	varchar(200),	
COMPL_ENDERECO	varchar(200),	
BAIRRO		varchar(200),
CEP			varchar(20),
CIDADE		varchar(200),
UF			varchar(2),
CNPJ		varchar(20),
Contrato		varchar(20),
PARCELA			varchar(10),
Valor			FLOAT,
VALOR_PROTESTO	FLOAT,
Dt_EMISSAO		DATETIME,
Dt_VENCIMENTO	DATETIME,
ESPECIE			varchar(200))

-- DROP table PROT_REMESSA_HIST  CUIDADO
create table PROT_REMESSA_HIST (
Data_Mov	datetime,
CLIENTE		varchar(200),
ENDERECO	varchar(200),	
COMPL_ENDERECO	varchar(200),	
BAIRRO		varchar(200),
CEP			varchar(20),
CIDADE		varchar(200),
UF			varchar(2),
CNPJ		varchar(20),
Contrato		varchar(25),
PARCELA			varchar(10),
Valor			FLOAT,
VALOR_PROTESTO	FLOAT,
Dt_EMISSAO		DATETIME,
Dt_VENCIMENTO	DATETIME,
ESPECIE			varchar(200))

CREATE INDEX PROT_REMESSA_HIST_IDX1 ON PROT_REMESSA_HIST (CONTRATO, PARCELA)
CREATE INDEX PROT_REMESSA_HIST_IDX2 ON PROT_REMESSA_HIST (Data_Mov, CONTRATO, PARCELA)



-- ************************************** REMESSAS
use sig
DROP table PROT_RETORNO_HIST
create table PROT_RETORNO_HIST (
Data_Mov	datetime,		--
CODIGO_CLIENTE		varchar(20),
NOME_CLIENTE			varchar(200),
Contrato		varchar(20),
PARCELA			varchar(10),
PROTOCOLO		varchar(20),
Dt_RECEPCAO		DATETIME,
CARTORIO		varchar(200),
IBGE 			varchar(200),
CIDADE			varchar(200),
DATA_OCORRENCIA	DATETIME,
STATUS			varchar(20),
DESCR_STATUS	varchar(200),  --
VALOR_CUSTAS	FLOAT )

CREATE INDEX PROT_RETORNO_HIST_IDX2 ON PROT_RETORNO_HIST (Data_Mov, CONTRATO, PARCELA)
/*
Data_Mov	CODigo_CLIENTE	NOME_CLIENTE	CONTRATO	PARCELA	PROTOCOLO	DT_RECEPCAO	CARTORIO	IBGE	CIDADE	
Data_OCORRENCIA	STATUS	DESCR_STATUS	VALOR_CUSTAS
use sig
*/
DROP   table PROT_RETORNO
create table PROT_RETORNO (
CODigo_CLIENTE		varchar(20),
NOME_CLIENTE			varchar(200),
Contrato		varchar(25),
PARCELA			varchar(10),
PROTOCOLO		varchar(20),
Dt_RECEPCAO		DATETIME,
CARTORIO		varchar(200),
IBGE 			varchar(200),
CIDADE			varchar(200),
DATA_OCORRENCIA	DATETIME,
STATUS			varchar(20),
DESCR_STATUS	varchar(200),  --
VALOR_CUSTAS	FLOAT )
CREATE INDEX PROT_RETORNO_IDX1 ON PROT_RETORNO (CONTRATO, PARCELA)

-- SELECT * FROM PROT_RETORNO (NOLOCK)
-- SELECT * FROM PROT_RETORNO_HIST (NOLOCK)
/*
DELETE FROM PROT_RETORNO_HIST 
INSERT INTO PROT_RETORNO_HIST 
                    Select '20160404'
                    	   ,[CODIGO_CLIENTE] 
                          ,[NOME_CLIENTE] 
                          ,[CONTRATO] 
                          ,[PARCELA] 
                          ,[PROTOCOLO] 
                          ,[DT_RECEPCAO] 
                          ,[CARTORIO] 
                          ,[IBGE] 
                          ,[CIDADE]
                          ,[DATA_OCORRENCIA]
                          ,[STATUS] 
                          ,'',[VALOR_CUSTAS] 
                    From PROT_RETORNO 

SELECT * FROM prot_ocorrencia_cart

SELECT * FROM PROT_OCORRENCIA_DE_PARA
*/
DROP   table PROT_CAPTURA_HIST (

create table PROT_CAPTURA_HIST (
Data_Mov			datetime,
Data_OCOR			datetime,

CODIGO_PRODUTO		varchar(200),
CONTRATO			varchar(20),
PARCELA				varchar(10),
COD_OCORRENCIA		varchar(10),
SEQ_OCORRENCIA		varchar(10),
OBS_OCORRENCIA		varchar(200)  )


CREATE INDEX PROT_CAPTURA_HIST_IDX1 ON PROT_CAPTURA_HIST (CONTRATO, PARCELA)
CREATE INDEX PROT_CAPTURA_HIST_IDX2 ON PROT_CAPTURA_HIST (Data_Mov, CONTRATO, PARCELA)

-- SELECT * FROM PROT_OCORRENCIA_cart
-- SELECT * FROM PROT_OCORRENCIA_DE_PARA
truncate table PROT_OCORRENCIA_DE_PARA 

insert PROT_OCORRENCIA_DE_PARA select '1','Pago','778','PAGO'
insert PROT_OCORRENCIA_DE_PARA select '2','Protestado','779','PROTESTADO'
insert PROT_OCORRENCIA_DE_PARA select '3','Retirado','780','RETIRADO'
insert PROT_OCORRENCIA_DE_PARA select '4','Sustado','781','SUSTADO'
insert PROT_OCORRENCIA_DE_PARA select '5','Devolvido pelo Cartório por irregularidades - Sem custas. Qu','784','PROT.DEV.IRREG C/CUS'
insert PROT_OCORRENCIA_DE_PARA select '6','Devolvido pelo Cartório por irregularidades - Com custas. Li','783','PROT.DEV.IRREG S/CUS'
insert PROT_OCORRENCIA_DE_PARA select '7','Liquidação em Condicional - Utilizada para os títulos liquid','795','LIQUIDAÇAO EM CONDIC'
insert PROT_OCORRENCIA_DE_PARA select '8','Título Aceito - Utilizado para Letras de Cambio e Duplicatas','787','TITULO ACEITO'
insert PROT_OCORRENCIA_DE_PARA select '9','Edital, apenas nos Estados da Bahia e Rio de Janeiro','796','EDITAL EST. BAHIA/RJ'
insert PROT_OCORRENCIA_DE_PARA select 'A','Protesto do banco cancelado','786','PROTESTO DE BANCO CANCELADO'
insert PROT_OCORRENCIA_DE_PARA select 'B','Protesto já efetuado','787','PROTESTO JÁ EFETUADO'
insert PROT_OCORRENCIA_DE_PARA select 'C','Protesto por edital','788','PROTESTO POR EDITAL'
insert PROT_OCORRENCIA_DE_PARA select 'D','Retirada por edital','789','RETIRADA POR EDITAL'
insert PROT_OCORRENCIA_DE_PARA select 'E','Protesto de terceiro cancelado','790','PROTESTO DE TERECEIRO CANCELADO'
insert PROT_OCORRENCIA_DE_PARA select 'F','Desistencia do protesto por liquidação bancária','785','DESIST.DE PROTESTO POR LIQUIDAÇÃO.'
insert PROT_OCORRENCIA_DE_PARA select 'G','Sustado Definitivo','782','SUSTADO DEFINITIVO'
insert PROT_OCORRENCIA_DE_PARA select 'I','Emissão da 2a via do Instrumento do Protesto','791','EMISSÃO DA 2ºVIA DO INSTRUMENTO DO PROTESTO'
insert PROT_OCORRENCIA_DE_PARA select 'J','Cancelamento já efetuado anteriormente','792','Cancelamento já efetuado anteriormente'
insert PROT_OCORRENCIA_DE_PARA select 'X','Cancelamento não efetuado','793','Cancelamento não efetuado'

insert PROT_OCORRENCIA_DE_PARA select '5','','784','PROT.DEV.IRREG C/CUS'
insert PROT_OCORRENCIA_DE_PARA select '6','','783','PROT.DEV.IRREG S/CUS'

insert PROT_OCORRENCIA_DE_PARA select '7','','795','LIQUIDAÇAO EM CONDIC'
insert PROT_OCORRENCIA_DE_PARA select '8','','787','TITULO ACEITO'
insert PROT_OCORRENCIA_DE_PARA select '9','','796','EDITAL EST. BAHIA/RJ'
insert PROT_OCORRENCIA_DE_PARA select 'A','','786','PROTESTO DE BANCO CANCELADO'
insert PROT_OCORRENCIA_DE_PARA select 'B','','787','PROTESTO JÁ EFETUADO'
insert PROT_OCORRENCIA_DE_PARA select 'C','','788','PROTESTO POR EDITAL'
insert PROT_OCORRENCIA_DE_PARA select 'D','','789','RETIRADA POR EDITAL'
insert PROT_OCORRENCIA_DE_PARA select 'E','','790','PROTESTO DE TERECEIRO CANCELADO'
insert PROT_OCORRENCIA_DE_PARA select 'F','','785','DESIST.DE PROTESTO POR LIQUIDAÇÃO.'
insert PROT_OCORRENCIA_DE_PARA select 'I','','791','EMISSÃO DA 2ºVIA DO INSTRUMENTO DO PROTESTO'
insert PROT_OCORRENCIA_DE_PARA select 'J','','792','Cancelamento já efetuado anteriormente'
insert PROT_OCORRENCIA_DE_PARA select 'X','','793','Cancelamento não efetuado'

update PROT_OCORRENCIA_DE_PARA
set ocorrencia_cart =c.ocorrencia
  FROM PROT_OCORRENCIA_cart c (nolock)
where cod_cart=c.cod

delete from PROT_OCORRENCIA_DE_PARA where cod_cart in ('5','6')

/*
use gestorcob
select * from EOCORuse gestorcob
select * from tmoti
select * from dbo.TSOCOR
use gestorcob
select * from EOCOR


--select * from CDCSANTANAMicroCredito..TSOCOR
*/