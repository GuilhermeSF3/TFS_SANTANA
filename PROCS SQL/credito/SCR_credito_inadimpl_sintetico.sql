Use sig
-- EXEC SCR_credito_inadimpl_sint '2015-08-01', '2015-08-31','99',0,0
-- EXEC SCR_credito_inadimpl_sint '2015-08-01', '2015-08-31','99',60,100

-- EXEC SCR_credito_inadimpl_sint '2015-06-01', '2015-06-30','99',0,0
-- EXEC SCR_credito_inadimpl_sint '2015-06-01', '2015-06-30','Alexandre de Almeida Medina',0,0

-- EXEC SCR_credito_inadimpl_sint '2015-05-31', '2015-07-30','99',0,0
-- EXEC SCR_credito_inadimpl_sint '2015-05-31', '2015-07-30','99','',''

-- EXEC SCR_credito_inadimpl_sint '2015-06-01', '2015-06-20', '20150826','99'
-- EXEC SCR_credito_inadimpl_sint '2015-01-01', '2015-03-20', '99'
-- EXEC SCR_credito_inadimpl_sint '2015-07-01', '2015-07-31','2015-08-03', ''
-- EXEC SCR_credito_inadimpl_sint '2015-06-01', '2015-06-30','2015-08-03', ''


-- SELECT *	FROM #CONTRATO
GO

-- PROC Credito INADIMPLENCIA **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_credito_inadimpl_sint' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_credito_inadimpl_sint]
GO

-- MENSAL
-- ***************************************
CREATE PROCEDURE [dbo].[SCR_credito_inadimpl_sint](
	@DATADE AS DATETIME, 
	@DATAATE AS DATETIME,
	@ANALISTA AS VARCHAR(200),
	@ATRASO_DE	AS INT,
	@ATRASO_ATE	AS INT)

AS
BEGIN

SET NOCOUNT ON
-- DROP TABLE #CONTRATO
--   declare  	@DATADE AS DATETIME declare 	@DATAATE AS DATETIME set @DATADE='20150801' set @DATAATE='20150831' 

DECLARE	@DATAATE_PGTO AS DATETIME
SET @DATAATE_PGTO = GETDATE()


SELECT	DISTINCT
	PROP.PPDTBASE	dt_proposta,
	EPROP.EPUSUARIO USUARIO,
	REPLICATE(' ',200)				analista, 
	OPER.OPNROPER	contrato, 
	PROP.PPNRPROP	proposta, 
	OPER.OPDTBASE	dt_contrato,
	DESCR_TIPO		produto,
	OPER.OPVLRFIN	vlr_financiado,
	OPER.OPCODCLI	COD_CLI,
	0 AS INADIMPLENTE,
	0 AS ATRASO
INTO 
	#CONTRATO

FROM	
	PROPWEBSMC..CPROP PROP  (NOLOCK) 
INNER JOIN 
	CDCSANTANAMicroCredito..COPER (NOLOCK) OPER ON PROP.PPNRPROP = OPER.OPNRPROP 
LEFT JOIN 
	(select * from PROPWEBSMC..EPROP EPROP1 (NOLOCK) WHERE EPROP1.EPNRDECI IN ( '100', '120')  )as EPROP  
				ON EPROP.EPNRPROP = PROP.PPNRPROP 
			AND EPROP.EPNRSEQ = (SELECT		MAX(P2.EPNRSEQ) FROM PROPWEBSMC..EPROP P2 (NOLOCK) 
									WHERE	P2.EPNRPROP = PROP.PPNRPROP AND P2.EPNRDECI  IN ( '100', '120') ) 
 LEFT JOIN 

	TModa_tipo_prod M (NOLOCK)  ON OPER.OPCODOP =	M.COD_MODA	AND M.COD_PROD  in ( 'V','cg') LEFT JOIN 
	Ttipo_prod T (NOLOCK) ON T.COD_MODALIDADE = M.COD_MODALIDADE 
WHERE	
	PROP.PPDTBASE BETWEEN @DATADE AND @DATAATE 

-- ANALISTA DO CONTRATO
UPDATE	#CONTRATO
SET		ANALISTA = USNOMEUSUC
FROM	ACESSOCORP..TUSU (NOLOCK)
WHERE	USNOMEUSU = USUARIO

-- INADIMPLENTE 
UPDATE	#CONTRATO
SET		INADIMPLENTE=1,
		ATRASO = ATR
FROM 	#CONTRATO INNER JOIN 
		(SELECT		PANROPER,MAX(DATEDIFF(d,P.PADTVCTO,@DATAATE_PGTO)) AS ATR
			FROM	CDCSANTANAMicroCredito..cparc P (NOLOCK) 
			WHERE 
				-- VENCIDO E NAO PAGO
				P.PADTVCTO  <= @DATAATE_PGTO AND (P.PADTLIQ IS NULL OR P.PADTLIQ > @DATAATE_PGTO) AND 
				P.PACNTRL	= (SELECT  		MAX(P1.PACNTRL) 
									FROM 		CDCSANTANAMicroCredito..CPARC P1 (NOLOCK)
									WHERE		P1.PANROPER= P.PANROPER AND P1.PANRPARC = P.PANRPARC)
				AND DATEDIFF(d,P.PADTVCTO,@DATAATE_PGTO)<= 2 
			GROUP BY PANROPER
			 )  AS  PARC -- VENCIDO HA 2 DIAS 
		ON #CONTRATO.contrato = PARC.PANROPER  
/*
	-- ATRASO NA FX	
	AND 1=0+ CASE	WHEN @ATRASO_DE+@ATRASO_ATE = 0 THEN 1
					WHEN @ATRASO_DE+@ATRASO_ATE > 0 AND DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO) BETWEEN @ATRASO_DE AND @ATRASO_ATE THEN 1
					ELSE 0 END
*/
-- TTL DO CALCULO INADIMPLENTES 
-- select * from #inadimpl

delete aux_inadimpl
insert aux_inadimpl
SELECT
	ANALISTA AS ANALISTA, 
	sum(case when INADIMPLENTE=1 then  1 else 0 end) as atraso,
	sum(case when INADIMPLENTE=1 then  vlr_financiado ELSE 0.0 END) AS vlr_ATRASO,  

	-- ADIMPLENTE
	sum(case when INADIMPLENTE=0  then 1 else 0 end) as ADIMPLENTE,
	sum(case when INADIMPLENTE=0  then  vlr_financiado else 0.0 end) AS vlr_ADIMPL,  

	count(distinct contrato) as qtd,
	sum(vlr_financiado) AS vlr_FINANC,
	0.0  AS prc_ATRASO
--INTO #inadimpl
FROM 
	#CONTRATO
/*WHERE 
		1=0+ CASE	WHEN @ATRASO_DE+@ATRASO_ATE = 0 THEN 1
					WHEN @ATRASO_DE+@ATRASO_ATE > 0 AND INADIMPLENTE=1 THEN 1
					ELSE 0 END

	AND
		 1=0+ CASE	WHEN @ANALISTA = '99'  THEN 1
					WHEN @ANALISTA <>'99' AND  ANALISTA=@ANALISTA THEN 1
					ELSE 0 END  	*/
GROUP BY analista


-- LINHA DE TOTAL
INSERT aux_inadimpl
SELECT  
	'TOTAL' AS ANALISTA, 
	sum(atraso) as atraso,
	sum(vlr_ATRASO) AS vlr_ATRASO,  

	-- ADIMPLENTE
	sum(ADIMPLENTE) as ADIMPLENTE,
	sum(vlr_ADIMPL) AS vlr_ADIMPL,  

	SUM(qtd) as qtd,
	sum(vlr_FINANC) AS vlr_FINANC,
	0.0  AS prc_ATRASO
FROM	aux_inadimpl R (NOLOCK)



-- a partir dos contratos, ttl por analistas
-- DECLARE @DATAATE_PGTO DATETIME SET @DATAATE_PGTO = '20150930'
/*
create table aux_inadimpl (
analista	varchar(200),
atraso		int,
vlr_atraso	float,
adimplente	int,
vlr_adimpl	float,
qtd			int,
vlr_financ	float,
prc_atraso	float)


SELECT
	--contrato, 
	--max(dt_proposta)			AS DT_PROP,
	analista as analista, 
	sum(case when DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO)> 2   and PARC.PADTLIQ IS NULL then  1 else 0 end) as atraso,
--	MAX(C.CLCGC)		AS proposta, 
--	MAX(dt_contrato)    AS dt_contrato,
--	ISNULL(MAX(produto),'')  AS produto,
	sum(case when DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO)> 2   and PARC.PADTLIQ IS NULL then   vlr_financiado ELSE 0.0 END) AS vlr_ATRASO,  
--	MAX(DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO)) dias_atraso, 

	-- ADIMPLENTE
	sum(case when DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO)<= 2   and PARC.PADTLIQ IS NOT NULL then 1 else 0 end) as ADIMPLENTE,
	sum(case when DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO)<= 2   and PARC.PADTLIQ IS NOT NULL then  vlr_financiado else 0.0 end) AS vlr_ADIMPL,  
	count(distinct contrato) as qtd,
	sum(distinct vlr_financiado) AS vlr_FINANC,
	sum(case when DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO)> 2   and PARC.PADTLIQ IS NULL then  vlr_financiado ELSE 0.0 END)
		/sum(distinct vlr_financiado)  * 100.0  AS prc_ATRASO

into #inadimpl
FROM 
	#CONTRATO INNER JOIN CDCSANTANAMicroCredito..cparc PARC (NOLOCK) ON #CONTRATO.contrato = PARC.PANROPER  
	INNER JOIN CDCSANTANAMicroCredito..cCLIE C (NOLOCK)  ON #CONTRATO.COD_CLI = C.CLCODCLI
WHERE 
--	PARC.PADTVCTO <= @DATAATE_PGTO AND (PARC.PADTLIQ IS NULL OR PARC.PADTLIQ > @DATAATE_PGTO) AND 
	PARC.PACNTRL = (
		SELECT 
			MAX(P1.PACNTRL) 
		FROM 
			CDCSANTANAMicroCredito..CPARC P1 (NOLOCK)
		WHERE 
			P1.PANROPER= PARC.PANROPER AND P1.PANRPARC = PARC.PANRPARC)


	AND 1=0+ CASE	WHEN @ATRASO_DE+@ATRASO_ATE = 0 THEN 1
					WHEN @ATRASO_DE+@ATRASO_ATE > 0 AND DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO) BETWEEN @ATRASO_DE AND @ATRASO_ATE THEN 1
					ELSE 0 END

	AND 1=0+ CASE	WHEN @ANALISTA = '99'  THEN 1
					WHEN @ANALISTA <>'99' AND  ANALISTA=@ANALISTA THEN 1
					ELSE 0 END
GROUP BY analista
*/

-- select * from #inadimpl
update aux_inadimpl-- #inadimpl
	set PRC_ATRASO = VLR_ATRASO /VLR_FINANC * 100.0
/*
qtd = (SELECT COUNT(*) FROM	#CONTRATO WHERE #CONTRATO.ANALISTA =  #inadimpl.ANALISTA)	,
	vlr_FINANC = (SELECT SUM(vlr_financiado) FROM	#CONTRATO WHERE #CONTRATO.ANALISTA =  #inadimpl.ANALISTA) 

,
	sum(case when DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO)> 2   and PARC.PADTLIQ IS NULL then  vlr_financiado ELSE 0.0 END)
		/sum(distinct vlr_financiado)  * 100.0  AS prc_ATRASO

	FROM	#CONTRATO
	--, 	ACESSOCORP..TUSU (NOLOCK)	WHERE	USNOMEUSU = USUARIO
	GROUP BY ANALISTA
*/

IF @ANALISTA='99' 
	select * from aux_inadimpl

IF @ANALISTA<>'99' 
	select * from aux_inadimpl
	where	ANALISTA IN (@ANALISTA,'TOTAL')
--#inadimpl
--Parcelas em Aberto
-- select * from #CONTRATO
/*
SELECT
	contrato, 
	max(dt_proposta)			AS DT_PROP,
	max(analista) as analista, 
	MAX(C.CLCGC)		AS proposta, 
	MAX(dt_contrato)    AS dt_contrato,
	ISNULL(MAX(produto),'')  AS produto,
	MAX(vlr_financiado) AS vlr_financiado,  
	MAX(DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO)) dias_atraso, 
	MAX(PAVLRPR)		AS vlr_parcela ,
	COUNT(PARC.PANROPER) qtd_parc_atraso,
	MIN(PARC.PANRPARC)   ATRASO_ANTIGO 
FROM 
	#CONTRATO INNER JOIN CDCSANTANAMicroCredito..cparc PARC (NOLOCK) ON #CONTRATO.contrato = PARC.PANROPER  
	INNER JOIN CDCSANTANAMicroCredito..cCLIE C (NOLOCK)  ON #CONTRATO.COD_CLI = C.CLCODCLI
WHERE 
	PARC.PADTVCTO <= @DATAATE_PGTO AND (PARC.PADTLIQ IS NULL OR PARC.PADTLIQ > @DATAATE_PGTO) AND 
	PARC.PACNTRL = (
		SELECT 
			MAX(P1.PACNTRL) 
		FROM 
			CDCSANTANAMicroCredito..CPARC P1 (NOLOCK)
		WHERE 
			P1.PANROPER= PARC.PANROPER AND P1.PANRPARC = PARC.PANRPARC)
	AND 1=0+ CASE	WHEN @ATRASO_DE+@ATRASO_ATE = 0 THEN 1
					WHEN @ATRASO_DE+@ATRASO_ATE > 0 AND DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO) BETWEEN @ATRASO_DE AND @ATRASO_ATE THEN 1
					ELSE 0 END

	AND 1=0+ CASE	WHEN @ANALISTA = '99'  THEN 1
					WHEN @ANALISTA <>'99' AND  ANALISTA=@ANALISTA THEN 1
					ELSE 0 END

GROUP BY 
	contrato

select * from aux_inadimpl
*/
END
