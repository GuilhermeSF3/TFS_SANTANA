USE sig
GO
/****** Object:  StoredProcedure [dbo].[SCR_credito_BASE]    Script Date: 02/05/2017  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- PROC CARGA FECHAMENTO  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_credito_BASE' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_credito_BASE]
GO


create Procedure [dbo].[SCR_credito_BASE] (                            
 @DATADE   AS DATETIME,                             
 @DATAATE  AS DATETIME,          
 @CPF VARCHAR(20),          
 @PAGTO INT)  AS                            
                            
BEGIN                            
                            
/*                            
SP_HELP AUX_CRED_BASE                            
ALTER TABLE AUX_CRED_BASE ADD MOTIVOS VARCHAR(500)                        
                        
ALTER TABLE AUX_CRED_BASE ADD COD_TABELA VARCHAR(10)                      
ALTER TABLE AUX_CRED_BASE ADD DESCR_TABELA VARCHAR(500)                   
ALTER TABLE AUX_CRED_BASE ADD MPDTSIT SMALLDATETIME                  
ALTER TABLE AUX_CRED_BASE ADD PLANO INT                 
ALTER TABLE AUX_CRED_BASE ADD VLRPARC FLOAT                
ALTER TABLE AUX_CRED_BASE ADD VLRENTR FLOAT              
ALTER TABLE AUX_CRED_BASE ADD VLRENDA FLOAT             
ALTER TABLE AUX_CRED_BASE ADD CONTRATO VARCHAR(20)            
ALTER TABLE AUX_CRED_BASE ADD APROV_AUT VARCHAR(20)          
ALTER TABLE AUX_CRED_BASE ADD COD_CRIVO VARCHAR(50)       
      
ALTER TABLE AUX_CRED_BASE ADD DT_ENT_MESA SMALLDATETIME      
ALTER TABLE AUX_CRED_BASE ADD HR_ENT_MESA VARCHAR(10)      
ALTER TABLE AUX_CRED_BASE ADD DT_SAI_MESA SMALLDATETIME      
ALTER TABLE AUX_CRED_BASE ADD HR_SAI_MESA VARCHAR(10)      
ALTER TABLE AUX_CRED_BASE ADD TEMPO_MESA BIGINT      
ALTER TABLE AUX_CRED_BASE ADD DT_ENT_ANALI SMALLDATETIME      
ALTER TABLE AUX_CRED_BASE ADD HR_ENT_ANALI VARCHAR(10)      
ALTER TABLE AUX_CRED_BASE ADD DT_SAI_ANALI SMALLDATETIME      
ALTER TABLE AUX_CRED_BASE ADD HR_SAI_ANALI VARCHAR(10)      
ALTER TABLE AUX_CRED_BASE ADD TEMPO_ANALI BIGINT 
ALTER TABLE AUX_CRED_BASE ADD REANALISE VARCHAR(10)
           
                      
DROP TABLE AUX_CRED_BASE                             
CREATE TABLE AUX_CRED_BASE (                            
ppnrprop varchar(9),                            
clcgc  varchar(18),                            
clnomecli varchar(60),                            
ppdtbase smalldatetime,                            
ppscoring varchar(20),                            
ppvlrfin float,                            
PPTETOFIN float,                            
mpsit  varchar(3),                     
tddescr  varchar(25),                                      
COD_LOJA varchar(20), --                            
O4NOME  varchar(40),                            
COD_AGENTE varchar(20), --                            
A13DESCR varchar(20),                            
ABMODELO varchar(80),                            
ABANOMOD varchar(4),                            
ABVLRTAB float,                            
--                            
COD_PRODUTO  varchar(20),                             
PRODUTO   varchar(200),                            
PERC_GARANTIA float,                            
ANALISTA  varchar(200),                            
OCUPACAO  varchar(100),                            
COD_OPERADOR varchar(20),                             
OPERADOR  varchar(200),                            
                            
ppCodCli varchar(30),                            
PPCODORG4 varchar(20),                            
PPCODORG3 varchar(20),                            
O3CODORGA13 varchar(20),                            
mpNrDeci varchar(20),                            
mpCodDeci varchar(20)                            
--,PPCODOP  varchar(20)                       
  )                            
                            
     SELECT * FROM AUX_CRED_BASE                       
                            
                            
*/                            
                            
TRUNCATE TABLE AUX_CRED_BASE                            
                            
-- PROPOSTAS                    
IF @CPF = '99' BEGIN                  
INSERT  AUX_CRED_BASE ( ppNrProp, ppDtBase,   ppScoring, ppVlrFin, PpTetoFIN,                             
       ppCodCli, PPCODORG4 , COD_LOJA, PPCODORG3, COD_OPERADOR, COD_PRODUTO,COD_AGENTE, COD_TABELA,PLANO,VLRPARC, VLRENTR, CONTRATO)                            
--PPCODOP  )                            
Select  ppNrProp,ppDtBase,ppScoring, ppVlrFin, PpTetoFIN,        
   ppCodCli, PPCODORG4 , PPCODORG4 , PPCODORG6  , PPCODORG6, PPCODPRD,PPCODORG3, --30/4/18                      
   PPCODTAB,PPQTDDPARC,PPVLRPARC, PPVLRENTR, PPNROPER           
--PPCODOP                            
 From propWebSmc..cProp (NOLOCK)                            
 Where ppDtBase BETWEEN @DATADE  AND @DATAATE             
 AND 1= 0+ CASE WHEN  @PAGTO='0' THEN 1               
   WHEN  @PAGTO = '1' AND PPCODORG3 = '000149' THEN 1              
   WHEN  @PAGTO = '2' AND PPCODORG3 <> '000149' THEN 1              
   END                
END                       
          
IF @CPF <> '99' BEGIN          
INSERT  AUX_CRED_BASE ( ppNrProp, ppDtBase,   ppScoring, ppVlrFin, PpTetoFIN,                             
       ppCodCli, PPCODORG4 , COD_LOJA, PPCODORG3, COD_OPERADOR, COD_PRODUTO,COD_AGENTE, COD_TABELA,PLANO,VLRPARC, VLRENTR, CONTRATO)                            
Select  ppNrProp,ppDtBase,ppScoring, ppVlrFin, PpTetoFIN,                    
   ppCodCli, PPCODORG4 , PPCODORG4 , PPCODORG6  , PPCODORG6, PPCODPRD,PPCODORG3,                    
   PPCODTAB,PPQTDDPARC,PPVLRPARC, PPVLRENTR, PPNROPER            
--PPCODOP                            
 From propWebSmc..cProp (NOLOCK)                         
 INNER JOIN CDCSANTANAMICROCREDITO..CCLIE (NOLOCK) ON PPCODCLI = CLCODCLI          
 Where REPLACE(REPLACE(CLCGC,'-',''),'.','') = REPLACE(REPLACE(@CPF,'-',''),'.','')          
END                            
          
-- CLIENTE                            
UPDATE  AUX_CRED_BASE                            
 SET  clCgc=C.clCgc,clNomeCli=C.clNomeCli, vlRenda = c.clrenda                            
 FROM propWebSmc..cClip C (NOLOCK)                            
 WHERE ppCodCli=C.clCodCli                            
                            
-- MOVIMENTO                            
UPDATE  AUX_CRED_BASE                            
 SET  mpSit = M.mpSit, mpNrDeci = M.mpNrDeci, mpCodDeci = M.mpCodDeci, mpDtSit = M.mpDtSit                            
 FROM propWebSmc..cMovP M (NOLOCK)                            
 WHERE ppNrProp = M.mpNrProp                            
                            
-- 4 LOJA                            
UPDATE  AUX_CRED_BASE                            
 SET  O4NOME=L.O4NOME                            
 FROM cdcSantanaMicroCredito..tOrg4 L(NOLOCK)                            
 WHERE PPCODORG4 =L.O4CODORG                            
                            
-- 3 OPERADOR, CODIGO AGENTE                            
UPDATE  AUX_CRED_BASE                            
 SET -- O3CODORGA13=O.O6CODORG3,       COD_AGENTE =O.O6CODORG3,                            
  OPERADOR = O.O6DESCR                            
 -- SELECT *                            
 FROM cdcSantanaMicroCredito..tOrg6 O (NOLOCK)                            
 WHERE COD_OPERADOR=O.O6CODORG  -- PPCODORG3=                            
                            
-- AGENTE     ORIGENS ALTERADO 30/4/18                        
UPDATE  AUX_CRED_BASE                            
 SET  O3CODORGA13=O.PPCODORG3,                            
   COD_AGENTE =O.PPCODORG3                         
 -- SELECT PPCODORG6,*                            
 FROM propWebSmc..CPROP O (NOLOCK)                            
 WHERE AUX_CRED_BASE.PPNRPROP=O.PPNRPROP                        
                      
UPDATE  AUX_CRED_BASE                            
 SET  A13DESCR = A.O3DESCR    -- SELECT *                       
 FROM cdcSantanaMicroCredito..tORG3 A (NOLOCK)                            
 WHERE AUX_CRED_BASE.COD_AGENTE = A.O3CODORG   -- ALTERADO 30/4/18                          
                        
--   SELECT * FROM AUX_CRED_BASE                        
-- DECISAO                            
UPDATE  AUX_CRED_BASE                            
 SET  tdDescr = D.tdDescr                            
 FROM propWebSmc..tDeci D (NOLOCK)                            
 WHERE mpNrDeci = D.tdNrDeci and mpcodDeci = d.tdcoddeci                             
                            
-- VEICULO                            
UPDATE  AUX_CRED_BASE                            
 SET  ABMODELO=B.ABMODELO,ABANOMOD=B.ABANOMOD,ABVLRTAB=B.ABVLRTAB                            
 FROM propWebSmc..cbfip B (NOLOCK)                            
 WHERE ppNrProp=B.ABNRPROP                            
  
--TETO  
UPDATE  AUX_CRED_BASE                            
 SET  PPTETOFIN = (B.ABVLRFIN/ABVLRVND)*100  
 FROM propWebSmc..cbfip B (NOLOCK)                            
 WHERE ppNrProp=B.ABNRPROP                            
 AND PPTETOFIN IS NULL  
                            
-- PRODUTO                            
UPDATE  AUX_CRED_BASE                            
 SET  PRODUTO = A.TPDESCR                            
 --  SELECT *                            
 FROM cdcSantanaMicroCredito..tPROD A (NOLOCK)                            
 WHERE COD_PRODUTO = A.TPCODPRD                            
                            
-- OCUPACAO                            
 UPDATE AUX_CRED_BASE                            
 SET  OCUPACAO =   CASE CLCODOCUP                             
          WHEN '01' THEN 'CLT'                             
          WHEN '02' THEN 'AUTONOMO'                             
          WHEN '03' THEN 'APOSENTADO'                             
          WHEN '04' THEN 'EMPRESARIO'                            
          WHEN '05' THEN 'PROFISSIONAL LIBERAL'                            
          ELSE 'S/ INFO'                             
        END                             
 FROM  PROPWEBSMC..cclip C (NOLOCK)                            
 WHERE PPCODCLI = CLCODCLI                             
                            
-- ANALISTA VARCHAR(100), AGENTE VARCHAR(100), OPERADOR VARCHAR(100), LOJA VARCHAR(100))                            
--  SELECT USNOMEUSUC, USNOMEUSU FROM ACESSOCORP..TUSU (NOLOCK) WHERE USATIVO='S'                            
--  select * from AUX_CRED_BASE                            
update  AUX_CRED_BASE                             
SET   ANALISTA = U.USNOMEUSUC       -- C.EPUSUARIO                            
FROM  PROPWEBSMC..EPROP C  (NOLOCK),                            
   ACESSOCORP..TUSU  U  (NOLOCK)                 
WHERE    AUX_CRED_BASE.PPNRPROP = C.EPNRPROP                            
 AND  USNOMEUSU = C.EPUSUARIO                            
 AND  1= 0+ CASE WHEN EPNRDECI=100 THEN 1   -- tempo q proposta fica parada                            
     WHEN EPNRDECI=120 THEN 1  -- tempo q os analistas ATUAM na proposta                            
     ELSE 0 END                             
            
UPDATE AUX_CRED_BASE SET APROV_AUT = 'SIM'            
FROM PROPWEBSMC..EPROP (NOLOCK)  INNER JOIN PROPWEBSMC..CPROP C (NOLOCK) ON EPNRPROP = C.PPNRPROP          
WHERE EPNRDECI = '35'            
AND PPCODPRD IN ('000204', '000304')          
AND EPNRPROP = AUX_CRED_BASE.PPNRPROP            
          
UPDATE AUX_CRED_BASE SET APROV_AUT = 'SIM/MESA'            
FROM PROPWEBSMC..EPROP (NOLOCK) WHERE EPNRDECI = '120' AND           
EPNRPROP IN (SELECT PPNRPROP FROM AUX_CRED_BASE (NOLOCK) WHERE APROV_AUT = 'SIM')          
AND EPNRPROP = AUX_CRED_BASE.PPNRPROP          
                            
UPDATE  AUX_CRED_BASE                            
 SET  DESCR_TABELA = A.TTDESCR                      
 FROM cdcSantanaMicroCredito..ttabe A (NOLOCK)                            
 WHERE COD_TABELA = A.TTCODTAB                      
                      
                    
--ATUALIZA SCORE PRODUTOS CP -- TIAGO 28/09/2019                    
UPDATE AUX_CRED_BASE SET PPSCORING = CRCONTEUDO                    
FROM  MONITOR..ECONSULT (NOLOCK)                     
INNER JOIN  MONITOR..ECONSLTR (NOLOCK) ON CSIDCONS = CRIDCONS                    
WHERE AUX_CRED_BASE.PPNRPROP = CSNRPROP                    
AND COD_PRODUTO IN ('000223','000224','000225','000231')                    
AND CRCAMPO = 'DISCRETA - RESULTADO DO SCORE'                    
                    
--ATUALIZA CÓDIGO CRIVO        
UPDATE AUX_CRED_BASE SET COD_CRIVO = CSNRCONSEXT        
FROM MONITOR..ECONSULT (NOLOCK)            
WHERE CSNRDECI='599'          
AND PPNRPROP = CSNREXTERNO    

--REANALISE
UPDATE AUX_CRED_BASE              
SET  REANALISE = CASE WHEN MPNUMRA IS NULL THEN 'NÃO' ELSE 'SIM' END              
FROM PROPWEBSMC..CMOVP E  (NOLOCK)              
WHERE MPNRPROP = PPNRPROP  
        
--TEMPO DE MESA      
UPDATE AUX_CRED_BASE SET         
DT_ENT_MESA = CONVERT(VARCHAR(4),DATEPART(YYYY,EPDTLNC))+          
RIGHT('00'+LTRIM(CONVERT(VARCHAR(2),DATEPART(MM,EPDTLNC))),2)+          
RIGHT('00'+LTRIM(CONVERT(VARCHAR(2),DATEPART(DD,EPDTLNC))),2),         
HR_ENT_MESA = EPHORALNC        
FROM PROPWEBSMC..EPROP E (NOLOCK)         
WHERE PPNRPROP = E.EPNRPROP        
AND EPNRSEQ = (SELECT  MAX(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1  (NOLOCK)          
       WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN ('1549','100'))        
        
UPDATE AUX_CRED_BASE SET         
DT_SAI_MESA = CONVERT(VARCHAR(4),DATEPART(YYYY,EPDTLNC))+          
RIGHT('00'+LTRIM(CONVERT(VARCHAR(2),DATEPART(MM,EPDTLNC))),2)+          
RIGHT('00'+LTRIM(CONVERT(VARCHAR(2),DATEPART(DD,EPDTLNC))),2),         
HR_SAI_MESA = EPHORALNC        
FROM PROPWEBSMC..EPROP E (NOLOCK)         
WHERE PPNRPROP = E.EPNRPROP        
AND EPNRSEQ = (SELECT  MAX(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1  (NOLOCK)          
       WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN ('120'))         
        
UPDATE AUX_CRED_BASE        
  SET TEMPO_MESA = (SIG.dbo.FN_TEMPO_ANALISE(DT_ENT_MESA, HR_ENT_MESA,DT_SAI_MESA, HR_SAI_MESA))       
      
--TEMPO_ANALI      
UPDATE AUX_CRED_BASE SET         
DT_ENT_ANALI = CONVERT(VARCHAR(4),DATEPART(YYYY,EPDTLNC))+          
RIGHT('00'+LTRIM(CONVERT(VARCHAR(2),DATEPART(MM,EPDTLNC))),2)+          
RIGHT('00'+LTRIM(CONVERT(VARCHAR(2),DATEPART(DD,EPDTLNC))),2),         
HR_ENT_ANALI = EPHORALNC        
FROM PROPWEBSMC..EPROP E (NOLOCK)         
WHERE PPNRPROP = E.EPNRPROP        
AND EPNRSEQ = (SELECT  MAX(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1  (NOLOCK)          
       WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN ('120'))        
        
UPDATE AUX_CRED_BASE SET         
DT_SAI_ANALI = CONVERT(VARCHAR(4),DATEPART(YYYY,EPDTLNC))+          
RIGHT('00'+LTRIM(CONVERT(VARCHAR(2),DATEPART(MM,EPDTLNC))),2)+          
RIGHT('00'+LTRIM(CONVERT(VARCHAR(2),DATEPART(DD,EPDTLNC))),2),         
HR_SAI_ANALI = EPHORALNC      
FROM PROPWEBSMC..EPROP E (NOLOCK)         
WHERE PPNRPROP = E.EPNRPROP         
AND EPNRSEQ = (SELECT  MAX(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1  (NOLOCK)          
       WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN ('2','91','96','18','79'))        
        
UPDATE AUX_CRED_BASE        
  SET TEMPO_ANALI = (SIG.dbo.FN_TEMPO_ANALISE(DT_ENT_ANALI, HR_ENT_ANALI,DT_SAI_ANALI, HR_SAI_ANALI))        
        
SELECT                            
 ppnrprop,                             
 ISNULL(clcgc,'') AS clcgc,                            
 ISNULL(clnomecli,'') AS clnomecli,                            
 ppdtbase,                            
 ISNULL(ppscoring,'') AS ppscoring,                            
 ppvlrfin,                            
 PPTETOFIN,                            
 mpsit,                            
 tddescr,                    
 mpdtsit,                  
 COD_LOJA,                            
 O4NOME,                            
 COD_AGENTE,                            
 A13DESCR,                                    
 ABMODELO,                            
 ABANOMOD,                            
 ABVLRTAB,                            
 COD_PRODUTO,                            
 ISNULL(PRODUTO,'') AS PRODUTO,                           
 ISNULL(PERC_GARANTIA,0.0) AS PERC_GARANTIA,                            
 ISNULL(ANALISTA,'') AS ANALISTA,                            
 ISNULL(OCUPACAO,'') AS OCUPACAO,                            
 COD_OPERADOR,                            
 OPERADOR,                      
 COD_TABELA,                      
 DESCR_TABELA,                
 PLANO,                
 VLRPARC,              
 VLRENTR,              
 vlRenda,            
 contrato,            
 APROV_AUT,        
 COD_CRIVO,    
 CAST(TEMPO_MESA/60 AS VARCHAR) + ':' + CAST(TEMPO_MESA - ((TEMPO_MESA / 60)*60) AS VARCHAR) AS TEMPO_MESA,     
 CAST(TEMPO_ANALI/60 AS VARCHAR) + ':' + CAST(TEMPO_ANALI - ((TEMPO_ANALI / 60)*60) AS VARCHAR) AS TEMPO_ANALI ,
 REANALISE   
FROM  AUX_CRED_BASE (NOLOCK)            
                           
END 