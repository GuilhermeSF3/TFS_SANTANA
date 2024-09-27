USE SIG --[Santana]
GO
/****** Object:  StoredProcedure [dbo].[SCR_fin_operacao]    Script Date: 29/05/2017 
SCR_fin_operacao		parametros (   dt_aplic, cliente_sel, nr_ativo)  -- localiza dados da operação

[SCR_fin_operacao] '20170302', '652', '4726'

exec [SCR_fin_operacao]  '2017-05-03','581', '5067'

exec [SCR_fin_operacao1] '2017-05-03','581', '5067'

03/03/2017 00:00

DT_EMISSAO	Cod_Operacao	Dt_Vcto	dias	pre_cdi	cod_papel	prc_cdi_real	serie_operacao	COD_GERENTE	vlr_original	cliente	COD_cliente
2017-03-03 00:00:00	4733	2020-02-17 00:00:00	1081	CDI	RDB-PFNL	114	RDB017001AO		100000	DANIEL PIOLI TORRES	653
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROC TELA operacao do IOPEN Captacao  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_fin_operacao' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_fin_operacao]
GO




CREATE PROCEDURE [dbo].[SCR_fin_operacao] (
	@DATA_OP  AS DATETIME,
	@COD_CLI  AS VARCHAR(20),
	@COD_OP   AS VARCHAR(20)) AS

--        	@PARAM1 as smalldatetime, 			@PARAM2 as varchar(10),			@PARAM3 as varchar(10)) AS


	BEGIN


-- LIMPA AUX ANTERIOR
TRUNCATE	TABLE AUX_FIN_OPER
-- sp_help AUX_FIN_OPER 
-- DECLARE @Codigo VARCHAR(20) SET @Codigo = '5076'

INSERT AUX_FIN_OPER (   DT_EMISSAO, Cod_Operacao, -- Dt_Vcto, dias, 
						pre_cdi,	cod_papel, prc_cdi_real, serie_operacao, 
						COD_GERENTE, vlr_original, cliente, COD_cliente , PAPEL_CODIGO )
SELECT	DATA, M.CODIGO,
		CASE WHEN  CHARINDEX('PRE',M.PAPEL_NOME,0)>0 THEN 'PRE' ELSE 'CDI' END ,  -- CDI-E PRE
		M.PAPEL_NOME, M.INDICE_PERCENTUAL, M.PAPEL_CODIGO,
		'', M.FINANCEIRO,'',CLIENTE_CODIGO
		, L.PAPEL_CODIGO
-- SELECT *
  From	  IOpen_Shop..Opn_Movimento	M (NoLock)
		, IOpen_Shop..Opn_Movimento_LASTRO L (NoLock)
		, IOpen_Shop..OPN_PAPEL		P (NoLock)			--  ALTEREI 5/7/17
--WHERE	M.DATA BETWEEN '20150101' AND '20170331'
WHERE	M.CODIGO =@Cod_OP		-- SOMENTE A OPERACAO SELECT
	AND M.OPERACAO_CODIGO IN (16)			-- EMISSAO 15 = DPGE  16= LC E RDB
	AND M.CODIGO=L.MOVIMENTO_CODIGO -- COLLATE DATABASE_DEFAULT  NUMERIC
	AND L.PAPEL_CODIGO=P.CODIGO -- COLLATE DATABASE_DEFAULT   NUMERIC  PAPEL_
-- 	AND P.DATA_BASE_VENCIMENTO > @DataREF		-- NAO VENCIDAS, NAO PAGAS
/*
  From	IOpen_Shop..Opn_Movimento (NoLock)
  Where Codigo=@Cod_OP AND OPERACAO_CODIGO=16			-- EMISSAO


-- PAPEL
UPDATE	AUX_FIN_OPER
SET		Dt_Vcto= DATA_VENCIMENTO , dias = DIAS_PAGAMENTO 
  from	IOpen_Shop..OPN_PAPEL (NoLock) 
  where PAPEL_CODIGO=SERIE_OPERACAO COLLATE DATABASE_DEFAULT
--  where PAPEL_CODIGO=cod_papel COLLATE DATABASE_DEFAULT
*/

DECLARE @CDI_CLI	FLOAT
SET		@CDI_CLI = (SELECT	INDICE_PERCENTUAL   From	IOpen_Shop..Opn_Movimento (NoLock)
					  Where Codigo=@Cod_OP AND OPERACAO_CODIGO=16		)	-- PRC
/*
-- PAPEL SEM CODIGO
-- DECLARE @DataREF  DATETIME SET @DataREF='20170531'
UPDATE	AUX_FIN_OPER
SET		AUX_FIN_OPER.PAPEL_CODIGO = (SELECT MAX(P.CODIGO)
									  from	IOpen_Shop..OPN_PAPEL P (NoLock) 
									  where P.DATA_EMISSAO = AUX_FIN_OPER.DT_EMISSAO 
										AND P.PAPEL_CODIGO=SERIE_OPERACAO COLLATE DATABASE_DEFAULT	)   -- TX CLIENTE		@CDI_CLI
WHERE	ISNULL(AUX_FIN_OPER.PAPEL_CODIGO,'')=''

-- QDO NAO LOCALIZA O PAPEL
-- DECLARE @DataREF  DATETIME SET @DataREF='20170531'
UPDATE	AUX_FIN_OPER
SET		AUX_FIN_OPER.PAPEL_CODIGO = (SELECT MAX(P.CODIGO)
									  from	IOpen_Shop..OPN_PAPEL P (NoLock) 
									  where P.DATA_EMISSAO = AUX_FIN_OPER.DT_EMISSAO 
								-- DATA_EMISSAO ='20170302'   AND INDICE_PERCENTUAL = 116.0
										AND P.INDICE_PERCENTUAL = AUX_FIN_OPER.prc_cdi_real
						) -- COLLATE DATABASE_DEFAULT	)   -- TX CLIENTE		@CDI_CLI
WHERE	ISNULL(AUX_FIN_OPER.PAPEL_CODIGO,'')=''
*/
	
-- DT_REF	DT_EMISSAO	Cod_Operacao	Dt_Vcto	dias	pre_cdi	cod_papel	prc_cdi_real	prc_cdi_interm	prc_cdi_final	serie_operacao	COD_GERENTE	vlr_original	vlr_mes_ant	vlr_mes_atual	vlr_rend_mes	vlr_projetado	cliente	COD_cliente	gerente	CANAL	ORIGEM	FUNDO	PAPEL_CODIGO
-- 2017-05-31 00:00:00	2017-03-01 00:00:00	4698	2018-03-01 00:00:00	365	CDI	LC-PJNL	110	NULL	NULL	LC0017005R3	NULL	15000	15297,48	15453,28	155,800000000001	15479,97	EASYNVEST TITULO COREETORA DE VALORES S.A.	117	NULL	NULL	NULL	NULL	3111

-- PAPEIS	VCTO E DIAS
UPDATE	AUX_FIN_OPER
SET		Dt_Vcto= DATA_VENCIMENTO , dias = DIAS_PAGAMENTO
      --, AUX_FIN_OPER.PAPEL_CODIGO = P.CODIGO
	FROM	IOpen_Shop..OPN_PAPEL P (NoLock) 
	WHERE	P.CODIGO = AUX_FIN_OPER.PAPEL_CODIGO
--  where LEFT(PAPEL_CODIGO,8)=LEFT(SERIE_OPERACAO,8) COLLATE DATABASE_DEFAULT
--  where P.PAPEL_CODIGO=SERIE_OPERACAO 

-- CLIENTE
UPDATE	AUX_FIN_OPER
SET		COD_GERENTE = GERENTE_CODIGO, 
		cliente		= NOME
  from	IOpen_Shop..OPN_CLIENTES (NoLock) 
  where Codigo=COD_cliente 

/*
-- GERENTE
-- SP_HELP AUX_FIN_OPER
UPDATE	AUX_FIN_OPER
SET		GERENTE =  NOME
  from	IOpen_Shop..OPN_GERENTE (NoLock) 
  where Codigo=COD_GERENTE 


				'21/08/2013' dt_vcto,
				12.13 vlr_original,
				'César' cod_gerente,
				'Pré' pre_cdi,
				1.04 prc_cdi_real,
				1.7714 tx_cliente,
				8.56 tx_final,
				'LCI12345' cod_papel
*/
SELECT	
--DT_EMISSAO,		Cod_Operacao,
		ISNULL(Dt_Vcto,'')	   AS Dt_Vcto,
		vlr_original,
		ISNULL(COD_GERENTE,'') AS COD_GERENTE,
		pre_cdi,
		cod_papel,
		prc_cdi_real,  
	0.0 as tx_cliente
		-- prc_cdi_interm, prc_cdi_final, 		serie_operacao,
		--vlr_mes_ant,vlr_mes_atual,vlr_rend_mes,vlr_projetado		dias,  		cliente,		COD_cliente
FROM	AUX_FIN_OPER (NOLOCK)
WHERE	COD_OPERACAO = @COD_OP

END
--use iopen_shop

/*
select * FROM	AUX_FIN_OPER (NOLOCK)
                txtDataVencimento.Text = row("dt_vcto")
                txtValorCaptado.Text = row("vlr_original")
                txtCorretor.Text = row("cod_gerente")
                txtTipoTaxa.Text = row("pre_cdi")
                txtTaxaIntermed.Text = row("prc_cdi_real")
                txtTaxaCliente.Text = row("prc_cdi_real")
                txtTaxaFinal.Text = row("prc_cdi_real")
                txtCodigoPapel.Text = row("cod_papel")

DT_EMISSAO	Cod_Operacao	Dt_Vcto	dias	pre_cdi	cod_papel	prc_cdi_real	serie_operacao	COD_GERENTE	vlr_original	cliente	COD_cliente
2017-03-03 00:00:00	4733	2020-02-17 00:00:00	1081	CDI	RDB-PFNL	114	RDB017001AO		100000	DANIEL PIOLI TORRES	653

-- FILTROS CLIENTES
DECLARE @DATA_OP DATETIME SET @DATA_OP ='20170420'
select CODIGO, Nome from iopen_shop..OPN_CLIENTES (nolock) where codigo in 
	(select DISTINCT cliente_codigo  from iopen_shop..opn_movimento (nolock)  where DATA= @DATA_OP)
			order by nome

-- FILTROS OPERACOES
DECLARE @DATA_OP DATETIME SET @DATA_OP ='20170420' DECLARE @CLI VARCHAR(20) SET @CLI='581'
select  CODIGO,CODIGO AS COD from iopen_shop..opn_movimento (nolock)  where DATA= @DATA_OP
AND cliente_codigo = @CLI


select * from iopen_shop..opn_operacao (nolock) where codigo=16
select * from iopen_shop..opn_movimento (nolock)  where codigo=5067

select * from iopen_shop..OPN_CLIENTES (nolock) order by nome

select * from iopen_shop..OPN_PAPEL (nolock)
select * from iopen_shop..OPN_GERENTE (nolock)


-- tipo de oper
50 tipos
CODIGO	DESCRICAO	TIPO_OPERACAO	MERCADO_CODIGO	SPB_CODIGO	SPB_CODIGO_TIPO_LEI	SPB_CODIGO_TIPO_COMP	SPB_TIPO_RET_COMP	SPB_TIPO_REP_IMP	SPB_TIPO_LIQ	DC	DEBITO_ESTOQUE	CREDITO_ESTOQUE	DEBITO_ESTOQUE_TER	CREDITO_ESTOQUE_TER	OPERACAO_VOLTA_CODIGO	PU_MEDIO	VISIVEL	MERCADO_CODIGO_CETIP
16	Emissão	16	0001	0001	NULL	NULL	NULL	NULL	NULL	C	NULL	14	NULL	NULL	NULL	0	1	0001


CODIGO	DESCRICAO
15	Resgate de Emissao
*/
