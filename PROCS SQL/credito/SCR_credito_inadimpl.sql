Use sig

-- EXEC SCR_credito_inadimpl '2015-06-01', '2015-06-30','99',0,0
-- EXEC SCR_credito_inadimpl '2015-06-01', '2015-06-30','Alexandre de Almeida Medina',0,0

-- EXEC SCR_credito_inadimpl '2015-05-31', '2015-07-30','99',0,0
-- EXEC SCR_credito_inadimpl '2015-05-31', '2015-07-30','99','',''

-- EXEC SCR_credito_inadimpl '2015-06-01', '2015-06-20', '20150826','99'
-- EXEC SCR_credito_inadimpl '2015-01-01', '2015-03-20', '99'
-- EXEC SCR_credito_inadimpl '2015-07-01', '2015-07-31','2015-08-03', ''
-- EXEC SCR_credito_inadimpl '2015-06-01', '2015-06-30','2015-08-03', ''

-- EXEC SCR_credito_inadimpl '2015-05-01', '2015-05-31','2015-08-03', ''

-- SELECT *	FROM #CONTRATO
GO

-- PROC Credito INADIMPLENCIA **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_credito_inadimpl' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_credito_inadimpl]
GO

-- MENSAL
-- ***************************************
CREATE PROCEDURE [dbo].[SCR_credito_inadimpl](    
 @DATADE AS DATETIME,     
 @DATAATE AS DATETIME,    
-- @DATAATE_PGTO AS DATETIME,    
 @ANALISTA AS VARCHAR(200),    
 @ATRASO_DE AS INT,    
 @ATRASO_ATE AS INT)    
    
AS    
BEGIN    
    
SET NOCOUNT ON    
DECLARE @DATAATE_PGTO AS DATETIME    
SET @DATAATE_PGTO = GETDATE()    
    
SELECT DISTINCT    
 PROP.PPDTBASE dt_proposta,    
 EPROP.EPUSUARIO USUARIO,    
 REPLICATE(' ',200)    analista,     
 OPER.OPNROPER contrato,     
 PROP.PPNRPROP proposta,     
 OPER.OPDTBASE dt_contrato,    
 DESCR_TIPO  produto,    
 OPER.OPVLRFIN vlr_financiado,    
 OPER.OPCODCLI COD_CLI    
INTO     
 #CONTRATO    
    
FROM     
 PROPWEBSMC..CPROP PROP  (NOLOCK)     
--INNER JOIN PROPWEBSMC..CMOVP  (Nolock) ON  CMOVP.MPNRPROP = PROP.PPNRPROP AND MPSIT IN ('INT')     
INNER JOIN     
 CDCSANTANAMicroCredito..COPER (NOLOCK) OPER ON PROP.PPNRPROP = OPER.OPNRPROP     
LEFT JOIN     
 (select * from PROPWEBSMC..EPROP EPROP1 (NOLOCK) WHERE EPROP1.EPNRDECI IN ('120')  )as EPROP      
   ON EPROP.EPNRPROP = PROP.PPNRPROP     
  AND EPROP.EPNRSEQ = (SELECT MAX(P2.EPNRSEQ) FROM PROPWEBSMC..EPROP P2 (NOLOCK) WHERE P2.EPNRPROP = PROP.PPNRPROP AND P2.EPNRDECI  IN ('120') )     
 LEFT JOIN       
 TModa_tipo_prod M (NOLOCK)  ON OPER.OPCODOP = M.COD_MODA AND M.COD_PROD  in ('V','cg') LEFT JOIN     
 Ttipo_prod T (NOLOCK) ON T.COD_MODALIDADE = M.COD_MODALIDADE     
WHERE     
 PROP.PPDTBASE BETWEEN @DATADE AND @DATAATE     
    
UPDATE #CONTRATO    
SET  ANALISTA = USNOMEUSUC    
FROM ACESSOCORP..TUSU (NOLOCK)    
WHERE USNOMEUSU = USUARIO    
    
--Parcelas em Aberto    
-- select * from #CONTRATO    
SELECT    
 contrato,     
 max(dt_proposta)   AS DT_PROP,    
 max(analista) as analista,     
-- case when  analista='' then 'pb cad'    else (ISNULL(analista,'pb cad')) end AS analista,     
 MAX(C.CLCGC)  AS proposta,     
 MAX(dt_contrato)    AS dt_contrato,    
 ISNULL(MAX(produto),'')  AS produto,    
 MAX(vlr_financiado) AS vlr_financiado,      
 MAX(DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO)) dias_atraso,     
 MAX(PAVLRPR)  AS vlr_parcela ,    
 COUNT(PARC.PANROPER) qtd_parc_atraso,    
 MIN(PARC.PANRPARC)   ATRASO_ANTIGO
 --CASE WHEN PARC.PANRPARC = '1' THEN 'P1' WHEN PARC.PANRPARC = '2' THEN 'P2' WHEN PARC.PANRPARC = '3' THEN 'P3' ELSE '' END AS P123     
INTO #CONTRATOP123 FROM     
 #CONTRATO INNER JOIN CDCSANTANAMicroCredito..cparc PARC (NOLOCK) ON #CONTRATO.contrato = PARC.PANROPER      
 INNER JOIN CDCSANTANAMicroCredito..cCLIE C (NOLOCK)  ON #CONTRATO.COD_CLI = C.CLCODCLI    
WHERE     
produto is not null and   
 PARC.PADTVCTO <= @DATAATE_PGTO AND (PARC.PADTLIQ IS NULL OR PARC.PADTLIQ > @DATAATE_PGTO) AND     
 PARC.PACNTRL = (    
  SELECT     
   MAX(P1.PACNTRL)     
  FROM     
   CDCSANTANAMicroCredito..CPARC P1 (NOLOCK)    
  WHERE     
   P1.PANROPER= PARC.PANROPER AND P1.PANRPARC = PARC.PANRPARC)    
 AND 1=0+ CASE WHEN @ATRASO_DE+@ATRASO_ATE = 0 THEN 1    
     WHEN @ATRASO_DE+@ATRASO_ATE > 0 AND DATEDIFF(d,PARC.PADTVCTO,@DATAATE_PGTO) BETWEEN @ATRASO_DE AND @ATRASO_ATE THEN 1    
     ELSE 0 END    
    
 AND 1=0+ CASE WHEN @ANALISTA = '99'  THEN 1    
     WHEN @ANALISTA <>'99' AND  ANALISTA=@ANALISTA THEN 1    
     ELSE 0 END    
    
GROUP BY     
 contrato

 SELECT *,CASE WHEN atraso_antigo = '001' THEN 'P1' WHEN atraso_antigo = '002' THEN 'P2' WHEN atraso_antigo = '003' THEN 'P3' ELSE '' END AS P123   FROM #CONTRATOP123
END 