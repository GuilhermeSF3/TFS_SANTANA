use SIG

-- DROP TABLE	  TCDI
Create table  TCDI (
DT_DE	smalldatetime,
PRC_CDI	float,
PRC_DIA	float,
dias_uteis_mes FLOAT )

GO

CREATE INDEX TCDI_IDX1 ON TCDI (DT_DE)
GO

-----------------------
/*
select * from  TCDI (nolock)

TRUNCATE TABLE TCDI
-- INSERT TCDI select '2017-05-01',10.86,	0.0,	22
-- INSERT TCDI select '2017-06-01',10.86,	0.0,	22

 INSERT TCDI select '2017-06-01',9.81,	0.0,	22
INSERT TCDI select '2017-07-01',9.81,	0.0,	22

-- PLANILHA EXCEL
09/05/2017			 10,86 

INSERT TCDI select '2017-05-01',11.13,	0.0,	22
INSERT TCDI select '2018-01-01', 9.34,	0.0,	22
INSERT TCDI select '2019-01-01', 9.35,	0.0,	22
INSERT TCDI select '2020-01-01',10.0,	0.0,	22
INSERT TCDI select '2021-01-01',10.52,	0.0,	22
INSERT TCDI select '2022-01-01',10.81,	0.0,	22

DT_DE	PRC_CDI	PRC_DIA	dias_uteis_mes
2017-05-01 00:00:00	11,13	0	22
2018-01-01 00:00:00	9,34	0	22
2019-01-01 00:00:00	9,35	0	22
2020-01-01 00:00:00	10	0	22
2021-01-01 00:00:00	10,52	0	22
2022-01-01 00:00:00	10,81	0	22
campos	SCR_financeiro_base		parametros ( DataDE YYYYmmDD ,  DataATE YYYYmmDD)												
dt_emissao	cod_operacao	dt_vcto	dias	pre_cdi	cod_papel	prc_cdi_real	prc_cdi_interm	prc_cdi_final	serie_operacao	vlr_original	vlr_mes_ant	vlr_mes_atual	vlr_rend_mes	vlr_projetado	cliente
01/03/2017	125	01/03/2018	365	cdi	LCI12345	 115,00 	 116,00 	 118,00 	Cesar	 15.000,00 	 15.080,00 	 15.165,82 	 85,82 	 16.797,80 	Easynvest
*/
-----------------------
DROP   table  AUX_FIN_OPER

Create table  AUX_FIN_OPER (
DT_EMISSAO		smalldatetime,
Cod_Operacao	VARCHAR(20),
Dt_Vcto		smalldatetime,
dias		FLOAT,
pre_cdi		VARCHAR(20),
cod_papel	VARCHAR(20),
prc_cdi_real	FLOAT,
prc_cdi_interm	FLOAT,
prc_cdi_final	FLOAT,
serie_operacao	VARCHAR(20),
COD_GERENTE		VARCHAR(20),
vlr_original	FLOAT,
vlr_mes_ant		FLOAT,
vlr_mes_atual	FLOAT,
vlr_rend_mes	FLOAT,
vlr_projetado	FLOAT,
cliente			VARCHAR(200),
COD_cliente			VARCHAR(20),
PAPEL_CODIGO	VARCHAR(20)   -- fk INSERI 24/7/17 
 )

Create INDEX  AUX_FIN_OPER_IDX1 ON  AUX_FIN_OPER(DT_EMISSAO,Cod_Operacao)
Create INDEX  AUX_FIN_OPER_IDX2 ON  AUX_FIN_OPER(Cod_Operacao)
Create INDEX  AUX_FIN_OPER_IDX3 ON  AUX_FIN_OPER(PAPEL_CODIGO)
GO

--ALTER TABLE  AUX_FIN_OPER ADD  PAPEL_CODIGO	VARCHAR(20) 

--  ALTER TABLE  AUX_FIN_OPER DROP COLUMN PAPEL_CODIGO

-----------------------
--		USE SIG
DROP   table  FIN_RES_OPER

Create table  FIN_RES_OPER (
DT_REF			smalldatetime,
DT_EMISSAO		smalldatetime,
Cod_Operacao	VARCHAR(20),
Dt_Vcto			smalldatetime,
dias			FLOAT,
pre_cdi			VARCHAR(20),
cod_papel		VARCHAR(20),
prc_cdi_real	FLOAT,
prc_cdi_interm	FLOAT,
prc_cdi_final	FLOAT,
serie_operacao	VARCHAR(20),
COD_GERENTE		VARCHAR(20),
vlr_original	FLOAT,
vlr_mes_ant		FLOAT,
vlr_mes_atual	FLOAT,
vlr_rend_mes	FLOAT,
vlr_projetado	FLOAT,
cliente			VARCHAR(200),
COD_cliente		VARCHAR(20),
gerente			VARCHAR(200),
CANAL			VARCHAR(200),
ORIGEM			VARCHAR(200),
FUNDO			VARCHAR(200),
PAPEL_CODIGO	VARCHAR(20)
 )

Create INDEX  FIN_RES_OPER_IDX1 ON  FIN_RES_OPER(DT_REF,Cod_Operacao)
Create INDEX  FIN_RES_OPER_IDX2 ON  FIN_RES_OPER(DT_REF,DT_EMISSAO,Cod_Operacao)
GO





-----------------------
/*
SP_HELP TCDI
SP_HELP AUX_FIN_OPER
SP_HELP AUX_FIN_OPER_PROJ

campos	SCR_fin_operacao_proj		parametros ( dt_aplic, cliente_sel, nr_ativo)												
dt_proj	tx_cdi	factor1	factor2	intermed_factor	resultado	vlr_dia	atualiz_dia	dt_aplic	cliente	nr_ativo	dt_vcto	vlr_captado	corretor	tx	cdi
01/03/2017	125	01/03/2018	365	cdi	LCI12345	 115,00 	 116,00 	 118,00 	Cesar						
*/

DROP   table  AUX_FIN_OPER_PROJ 

Create table  AUX_FIN_OPER_PROJ (
--DT_EMISSAO		smalldatetime,
Cod_Operacao	VARCHAR(20),			-- FK
Dt_proj			smalldatetime,
tx_cdi			FLOAT,
factor1			FLOAT,
factor2			FLOAT,
intermed_factor	FLOAT,
resultado		FLOAT,
vlr_dia			FLOAT,
atualiz_dia		FLOAT  )

Create INDEX  AUX_FIN_OPER_PROJ_IDX1 ON  AUX_FIN_OPER_PROJ(Cod_Operacao,Dt_proj)
GO


/*
SP_HELP AUX_FIN_OPER_PROJ
Dt_aplic		DATETIME,
cliente			VARCHAR(200),
COD_cliente		VARCHAR(20),
nr_ativo		VARCHAR(20),
Dt_vcto			DATETIME,
vlr_captado		FLOAT,
corretor		VARCHAR(200),
tx				FLOAT,
cdi				FLOAT)
*/
