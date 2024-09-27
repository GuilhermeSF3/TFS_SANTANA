
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
use SIG
go

-- EXEC SCR_MAPA_PDV  '20160201',1 , '0'
-- EXEC SCR_MAPA_PDV  '20160201',1 , 'DANIEL'

-- de ate veic
-- EXEC SCR_MAPA_PDV  '20150518',1 
-- EXEC SCR_MAPA_PDV  '20150524',1

-- EXEC SCR_MAPA_PDV  '20151031',1, 'DANIEL'
-- EXEC SCR_MAPA_PDV  '20151031',1, ' '

-- EXEC SCR_MAPA_PDV  '20151031',1, '2'

-- update usuario set codgerente = '0'

-- DELETE FROM CML_PDV  WHERE DT_FECHA= '20150524'


IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_MAPA_PDV' AND type = 'P')
   DROP PROCEDURE [dbo].SCR_MAPA_PDV
GO


CREATE Procedure [dbo].SCR_MAPA_PDV (@DT_INI as datetime, @PRODUTO as int , @AGENTE AS VARCHAR(200))
--, @rpt as int, @CART as int) 
AS
BEGIN
-- @DT3M as datetime, @data_ref as datetime,@veiculo as int
-- SELECT GETDATE()
-- SELECT  DATEDIFF(D,'20150524',GETDATE()) 

/*

ALTER TABLE USUARIO ALTER COLUMN CODGERENTE VARCHAR(20)
UPDATE USUARIO  SET CODGERENTE='EDNILSOM' WHERE LOGIN='ednilsom'

IF DATEDIFF(D,@DT_INI,GETDATE()) < 2 
BEGIN
	EXEC [PDV_CALCULO_LIMITES] @DT_INI,'D'
END

Acrescentar no relatório PDV PJ -(SIG) as seguintes informações:
1. saldo conta corrente PJ;
2. percental de checagem;
3. valor carteira CDC em PV;
4. limite para operar considerando o que segue:
-valor limite menos a soma da carteira desconto e carteira capital de giro (valor PV - CDC); 
se o capital de giro tiver garantia de veículo ou imóvel não contemplar na soma.


SELECT	*	FROM CML_PDV (NOLOCK)

alter table CML_PDV add saldoCC	float,
						Prc_checa float
alter table CML_PDV add vlr_carteira_PV	float, limite_op	float

alter table CML_PDV drop column saldoCC, Prc_checa , vlr_carteira_PV, limite_op


alter table CML_PDV add DUPLICATA	float,CHEQUE FLOAT, NP FLOAT

SP_HELP CML_PDV
*/

SELECT	DT_FECHA,
		COD_CEDN,
		NOM_CEDN,
		COD_OPERADOR,
		NOM_OPERADOR,
		CNPJ,
		TELEFONE,
		CONTATO,
		QTD_CANHOTO,
		PRC_CANHOTO,
		SLD_DEVEDOR_ATT,
		SLD_DEVEDOR_CDC,
		SLD_DEVEDOR_TTL,
		LIMITE,
		SLD_DISPONIVEL,
		COR_LINHA,
		ORDEM_LINHA,
		QTD_TITULO,
		QTD_CANHOTO_REC,
		ISNULL(saldoCC,0.0)		AS saldoCC,
		ISNULL(Prc_checa,0.0)	AS Prc_checa,
		ISNULL(vlr_carteira_PV,0.0) AS vlr_carteira_PV,
		ISNULL(limite_op,0.0)	AS limite_op,
		ISNULL(DUPLICATA,0.0)	AS DUPLICATA,
		ISNULL(CHEQUE,0.0)		AS CHEQUE,
		ISNULL(NP,0.0)			AS NP

--*
--		, 0.0 as saldoCC, 0.0 as Prc_checa, 0.0 as vlr_carteira_PV, 0.0 limite_op
	FROM	CML_PDV (NOLOCK) 
	WHERE	DT_FECHA= @DT_INI
		AND 1= 0 + CASE WHEN RTRIM(@AGENTE)='' or RTRIM(@AGENTE)='0' THEN 1
						WHEN NOT (RTRIM(@AGENTE)=''  or RTRIM(@AGENTE)='0'  ) AND isnumeric(@AGENTE)=0 AND
							 COD_OPERADOR IN (select OPER_COD from finsgrdbs..tb_oper (nolock) WHERE oper_abv_nom = @AGENTE)  THEN 1
						WHEN NOT (RTRIM(@AGENTE)=''  or RTRIM(@AGENTE)='0'  ) AND isnumeric(@AGENTE)=1  and
							 COD_OPERADOR = 
convert(int,(case when isnumeric(@AGENTE)=1 then @AGENTE else '0' end)) THEN 1
-- convert(int,@AGENTE)  THEN 1
						ELSE 0 END
/*

sp_help CML_PDV
SELECT DT_FECHA, DESCR_VEIC, DESCRICAO, AGENTE, FX1, FX2,
			 FX3,  FX4, FX5, TTL, ORDEM_LINHA
 FROM		CML_RPT (NOLOCK)
	WHERE	DT_FECHA=@DT_FIM AND RPT=2 
	ORDER BY AGENTE,ORDEM_LINHA
*/
-- SELECT * FROM  CML_RPT (NOLOCK) WHERE DT_FECHA='20150131' AND RPT=2 ORDER BY AGENTE,ORDEM_LINHA

END
GO