USE SIG --[Santana]
GO
/****** Object:  StoredProcedure [dbo].[SCR_fin_operacao1]    Script Date: 29/05/2017 
SCR_fin_operacao1		parametros (  nr_ativo)  -- localiza dados da operação

exec [SCR_fin_operacao1] '5067'
exec [SCR_fin_operacao1] '4733'

exec [SCR_fin_operacao1] '4726'
******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROC TELA operacao do IOPEN Captacao  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_fin_operacao1' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_fin_operacao1]
GO




CREATE Procedure [dbo].[SCR_fin_operacao1] (
	@CODIGO  as varchar(20)) AS

	Begin


-- LIMPA AUX ANTERIOR
TRUNCATE	TABLE AUX_FIN_OPER
-- sp_help AUX_FIN_OPER 
-- DECLARE @Codigo VARCHAR(20) SET @Codigo = '5076'

INSERT AUX_FIN_OPER (   DT_EMISSAO, Cod_Operacao, -- Dt_Vcto, dias, 
						pre_cdi,	cod_papel, prc_cdi_real, serie_operacao, 
						COD_GERENTE, vlr_original, cliente, COD_cliente )
select	DATA, CODIGO,
		CASE WHEN  CHARINDEX('PRE',PAPEL_NOME,0)>0 THEN 'PRE' ELSE 'CDI' END ,  -- CDI-E PRE
		PAPEL_NOME, INDICE_PERCENTUAL, PAPEL_CODIGO,
		'', FINANCEIRO,'',CLIENTE_CODIGO
  From	IOpen_Shop..Opn_Movimento (NoLock)
  Where Codigo=@Codigo AND OPERACAO_CODIGO=16			-- EMISSAO


DECLARE @DataDe	DATETIME 
SET		@DataDe = (SELECT	DATA   From	IOpen_Shop..Opn_Movimento (NoLock)
					  Where Codigo=@Codigo AND OPERACAO_CODIGO=16		)	-- EMISSAO

DECLARE @CDI_CLI	FLOAT
SET		@CDI_CLI = (SELECT	INDICE_PERCENTUAL   From	IOpen_Shop..Opn_Movimento (NoLock)
					  Where Codigo=@Codigo AND OPERACAO_CODIGO=16		)	-- PRC
/*
-- PAPEL incompleto
UPDATE	AUX_FIN_OPER
SET		Dt_Vcto= DATA_VENCIMENTO , dias = DIAS_PAGAMENTO 
  from	IOpen_Shop..OPN_PAPEL (NoLock) 
  where PAPEL_CODIGO=SERIE_OPERACAO COLLATE DATABASE_DEFAULT
--  where PAPEL_CODIGO=cod_papel COLLATE DATABASE_DEFAULT
*/

-- PAPEL
UPDATE	AUX_FIN_OPER
SET		Dt_Vcto= DATA_VENCIMENTO , dias = DIAS_PAGAMENTO 
  from	IOpen_Shop..OPN_PAPEL (NoLock) 
  where LEFT(PAPEL_CODIGO,8)=LEFT(SERIE_OPERACAO,8) COLLATE DATABASE_DEFAULT
	AND Data_operacao=@DataDe		-- DATA OPER
	AND INDICE_PERCENTUAL=@CDI_CLI  -- TX CLIENTE
--  where PAPEL_CODIGO=cod_papel COLLATE DATABASE_DEFAULT
-- 'RDB017001A1'  EQUIV 		RDB0170002


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

				SELECT 
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
	--	DT_EMISSAO,		Cod_Operacao,
--		dias,
		Dt_Vcto,
		vlr_original,
		ISNULL(COD_GERENTE,'') AS COD_GERENTE,
		pre_cdi,
		COD_PAPEL,
		prc_cdi_real,  
		0.0 as  tx_cliente,
		0.0 as  tx_final

		-- prc_cdi_interm, prc_cdi_final, 		serie_operacao,
		--vlr_mes_ant,vlr_mes_atual,vlr_rend_mes,vlr_projetado  		cliente,		COD_cliente
FROM	AUX_FIN_OPER (NOLOCK)
WHERE	COD_OPERACAO = @CODIGO

END
--use iopen_shop

/*
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


-- PAPEL
UPDATE	AUX_FIN_OPER
SET		Dt_Vcto= DATA_VENCIMENTO , dias = DIAS_PAGAMENTO 
  from	IOpen_Shop..OPN_PAPEL (NoLock) 
  where PAPEL_CODIGO=SERIE_OPERACAO COLLATE DATABASE_DEFAULT
--  where PAPEL_CODIGO=cod_papel COLLATE DATABASE_DEFAULT

select *   from	IOpen_Shop..OPN_PAPEL (NoLock) 
	where papel_codigo='RDB017001A1'

'RDB017001'
'RDB017001A1'
*/
