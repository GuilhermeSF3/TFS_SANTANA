USE sig 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--ALTER TABLE DCO_PEND_FORMALIZ_ANALITICO ADD DT_FORMALIZ SMALLDATETIME
--ALTER TABLE DCO_PEND_FORMALIZ_ANALITICO ADD DT_INTEG SMALLDATETIME
--ALTER TABLE DCO_PEND_FORMALIZ_ANALITICO ADD VLR_LIB FLOAT
--ALTER TABLE DCO_PEND_FORMALIZ_ANALITICO ADD SCORE INT

IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_PendFORMALIZ_analitico' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_PendFORMALIZ_analitico]
GO   
    
CREATE Procedure [dbo].SCR_PendFORMALIZ_analitico (                                        
         @DT_INI SMALLDATETIME,                            
         @DT_FIM SMALLDATETIME,                           
         @AGNT   INT ) AS                          
                        
 begin                        
                        
                        
-- DECLARE @DT_INI SMALLDATETIME DECLARE @DT_FIM SMALLDATETIME DECLARE @AGNT   INT  SET @DT_INI='20170801' SET @DT_FIM='20170801' SET @AGNT=99                        
-- PENDENCIA DE CONTRATO                          
/*TRUNCATE TABLE crv_PEND                           
                          
INSERT INTO  crv_PEND  (OPCODPROD, OPNROPER, OPDTBASE, OPVLRFIN, OPCODORG1, O1DESCR,                          
       OPCODORG2, O2DESCR, OPCODORG3, O3DESCR,  OPCODORG4, O4DESCR,                          
       OPCODORG5, O5DESCR, OPCODORG6,   -- opcodorg6 com no. Proposta                        
       EPCODPEN, EPCMPLTO, OPCODCLI,                          
       A13CODORG, A13DESCR,                         
       CLNOMECLI, CLFONEFIS, CLFONECOM, CLCGC,                          
       DIASATRASO)                        
     --, EPDTAGEN, EPDTPEN, EPDTCAD, EPDTLNCBX, EPDTFIM)                             
-- declare @DT_INI datetime declare @DT_FIM datetime set @DT_INI='20170801'  set @DT_FIM='20170831'                         
SELECT  distinct OPCODPRD,  OPNROPER, OPDTBASE, OPVLRFIN,                        
    --O.PPCODORG1, T1.O1DESCR, O.PPCODORG2, T2.O2DESCR,                         
     '','',  '','',                        
    OPCODORG6, T3.O6DESCR,                          
    OPCODORG4, T4.O4DESCR, OPCODORG5, T5.O5DESCR, -- O.PPCODORG6,                        
    OPNRPROP , -- -- opcodorg6 com no. Proposta                        
    --E.EPNRDECI                      
   '',E.epCmplto, -- E.epTPmotivo,                         
    OPCODCLI,                          
    T1O3.O3CODORG,  LTRIM(REPLACE(replace( REPLACE( replace (O3DESCR,'PROMOTORA',''), 'PROMOT ',''), 'PROM',''),'.','')),                         
    C.CLNOMECLI, C.CLFONEFIS, C.CLFONECOM, C.CLCGC,                              
    0 AS DIASATRASO                        
    -- E.EPDTAGEN, E.EPDTPEN, E.EPDTCAD, E.EPDTLNCBX, E.EPDTFIM                           
FROM                           
  CDCSANTANAMicroCredito..COPER O    (NOLOCK)                      
  INNER JOIN CDCSANTANAMicroCredito..CCLIE C    (NOLOCK) ON O.OPCODCLI = C.CLCODCLI                         
  INNER JOIN CDCSANTANAMicroCredito..EPEND E (NOLOCK) ON O.OPNROPER = E.EPNROPER                         
  INNER JOIN CDCSANTANAMicroCredito..TPEND TP (NOLOCK) ON E.EPCODPEN = TP.PDCODPEN                         
  INNER  JOIN CDCSANTANAMicroCredito..TORG1 T1 (NOLOCK) ON O.OPCODORG1 = T1.O1CODORG                         
  INNER  JOIN CDCSANTANAMicroCredito..TORG2 T2 (NOLOCK) ON O.OPCODORG2 = T2.O2CODORG                         
  INNER  JOIN CDCSANTANAMicroCredito..TORG4 T4 (NOLOCK) ON O.OPCODORG4 = T4.O4CODORG                         
  INNER  JOIN CDCSANTANAMicroCredito..TORG5 T5 (NOLOCK) ON O.OPCODORG5 = T5.O5CODORG                         
                      
 -- OPERADOR                      
  INNER  JOIN (select * from CDCSANTANAMicroCredito..TORG6 T3a (NOLOCK)                      
    where convert(int,o6codorg) in (select distinct codOperador from usuario_svc S1 (NOLOCK)  where codAgente=@AGNT)                      
       ) as T3-- SERVICE                      
  ON O.OPCODORG6 = T3.O6CODORG                         
           
  INNER JOIN CDCSANTANAMicroCredito..TORG3 T1O3 (NOLOCK)                       
   ON  T1O3.O3CODORG = T3.O6CODORG3                      
WHERE E.EPCODPEN = '007'  -- pendencia de CONTRATO                      
 AND ( O.OPDTLIQ IS NULL OR O.OPDTLIQ > @DT_FIM)              
 AND O.OPDTBASE >= @DT_INI                       
 AND O.OPDTBASE <= @DT_FIM    -- REMOVI  E.EPDTFIM > @DT_FIM                      
 -- EM 25/10/17 SOMENTE OS PENDENTES  **********   EPDTFIM PREENCHIDA NAO ESTAH MAIS PEND                      
 AND (E.EPDTFIM IS NULL )  AND E.EPDTCAD <= @DT_FIM               
 AND T1O3.O3CODORG <> '000149'  --18/10/2018 - TIAGO                    
                         
-- limpa pend formaliz                        
-- SELECT * FROM DCO_PEND_FORMALIZ_analitico (NOLOCK)                        
TRUNCATE   TABLE DCO_PEND_FORMALIZ_analitico                        
-- PENDENTES POR AGENTE                        
INSERT  DCO_PEND_FORMALIZ_analitico                       
   (A13CODORG ,A13DESCR,REGIAO,integrada,pend_AGENTE, OPERACAO,PROPOSTA,                      
   cod_OPERADOR, OPERADOR, COD_LOJA, LOJA, MOTIVO, CLCGC)                        
SELECT  DISTINCT                      
   A13CODORG, A13DESCR, O1DESCR, 0 ,1   , OPNROPER,OPCODORG6 --PROPOSTA                      
   , OPCODORG3, O3DESCR,  OPCODORG4, O4DESCR, EPCMPLTO, CLCGC                      
-- SELECT *                       
 FROM  crv_PEND (NOLOCK)                        
-- GROUP BY A13CODORG, A13DESCR, O1DESCR                        
                      
                             
-- SELECT * FROM crv_PEND (NOLOCK)                        
-- ***********************************************************  PEND INTERNA DCO                        
TRUNCATE TABLE crv_PEND                           
-- 107 PEND INT DCO                          
INSERT INTO  crv_PEND  (OPCODPROD, OPNROPER, OPDTBASE, OPVLRFIN, OPCODORG1, O1DESCR,                          
    OPCODORG2, O2DESCR, OPCODORG3, O3DESCR,  OPCODORG4, O4DESCR,                          
    OPCODORG5, O5DESCR, OPCODORG6, EPCODPEN, EPCMPLTO, OPCODCLI,                          
    A13CODORG, A13DESCR,                         
    CLNOMECLI, CLFONEFIS, CLFONECOM, CLCGC,                          
    DIASATRASO)                        
    --, EPDTAGEN, EPDTPEN, EPDTCAD, EPDTLNCBX, EPDTFIM)                          
    --   declare @DT_INI datetime declare @DT_FIM datetime set @DT_INI='20170801'  set @DT_FIM='20170831'                         
SELECT  distinct PPCODPRD,  O.PPNROPER, O.PPDTBASE, O.PPVLRFIN,                        
  --O.PPCODORG1, T1.O1DESCR, O.PPCODORG2, T2.O2DESCR,                         
  '','',  '','',                        
  O.PPCODORG6, T3.O6DESCR,                          
  O.PPCODORG4, T4.O4DESCR, O.PPCODORG5, T5.O5DESCR, --O.PPCODORG6,                        
  E.EPNRPROP,  -- COM PROP                        
  E.EPNRDECI,'', -- E.EPCMPLTO SEM MOTIVO NA PEND FORMALIZ                      
  O.PPCODCLI,                          
  T1O3.O3CODORG,  replace( REPLACE( replace (O3DESCR,'PROMOTORA',''), 'PROMOT ',''), 'PROM',''),                         
  C.CLNOMECLI, C.CLFONEFIS, C.CLFONECOM, C.CLCGC,                              
  0 AS DIASATRASO                        
  --E.EPDTAGEN, E.EPDTPEN, E.EPDTCAD, E.EPDTLNCBX, E.EPDTFIM                           
FROM                           
  (SELECT * FROM propWebSmc..CPROP E (NOLOCK) WHERE PPDTBASE BETWEEN @DT_INI AND @DT_FIM ) AS O                        
 INNER JOIN propWebSmc..EPROP E (NOLOCK) ON  E.EPNRPROP   =O.PPNRPROP                        
 LEFT  JOIN CDCSANTANAMicroCredito..CCLIE C    (NOLOCK) ON O.PPCODCLI = C.CLCODCLI                           
--  LEFT  JOIN CDCSANTANAMicroCredito..TORG1 T1 (NOLOCK) ON O.PPCODORG1 = T1.O1CODORG                           
--  LEFT  JOIN CDCSANTANAMicroCredito..TORG2 T2 (NOLOCK) ON O.PPCODORG2 = T2.O2CODORG                           
  LEFT  JOIN CDCSANTANAMicroCredito..TORG6 T3 (NOLOCK) ON O.PPCODORG6 = T3.O6CODORG                           
  LEFT  JOIN CDCSANTANAMicroCredito..TORG4 T4 (NOLOCK) ON O.PPCODORG4 = T4.O4CODORG                           
  LEFT  JOIN CDCSANTANAMicroCredito..TORG5 T5 (NOLOCK) ON O.PPCODORG5 = T5.O5CODORG                           
  LEFT  JOIN CDCSANTANAMicroCredito..TORG3 T1O3 (NOLOCK) ON T3.O6CODORG3 = T1O3.O3CODORG                          
WHERE  E.EPNRDECI IN('107','602','103')  --  Pend FORMALIZACAO                      
 AND   (E.EPNRSEQ > (SELECT MIN(E1.EPNRSEQ) FROM  propWebSmc..EPROP E1 (NOLOCK)                         
      WHERE E1.EPNRPROP = O.PPNRPROP  AND  E1.EPNRDECI IN('662','663','664')) ) -- FORMALIZACAO               
    AND T1O3.O3CODORG <> '000149'            --18/10/2018 - TIAGO              
 AND O.PPDTBASE >= @DT_INI            
 AND O.PPDTBASE <= @DT_FIM            
        
-- PENDENTES INTERNA 107                      
INSERT  DCO_PEND_FORMALIZ_analitico                       
   (A13CODORG ,A13DESCR,REGIAO,integrada,PEND_DCO, OPERACAO,PROPOSTA,                      
   cod_OPERADOR, OPERADOR, COD_LOJA, LOJA, CLCGC)                        
SELECT  DISTINCT                      
   A13CODORG, A13DESCR, O1DESCR, 0 ,1   , OPNROPER,OPCODORG6 --PROPOSTA                      
   , OPCODORG3, O3DESCR,  OPCODORG4, O4DESCR, CLCGC                     
-- SELECT *                       
 FROM  crv_PEND P (NOLOCK)                        
WHERE  P.OPNROPER  NOT IN ( SELECT P1.OPERACAO                       
         FROM DCO_PEND_FORMALIZ_analitico P1 (NOLOCK))                      
                      
-- ATUALIZA PEND FORMALIZACAO                      
-- SELECT * FROM DCO_PEND_FORMALIZ_analitico (NOLOCK)                      
UPDATE  DCO_PEND_FORMALIZ_analitico                       
SET   PEND_DCO=1, MOTIVO = RTRIM(MOTIVO) +' '+ EPCMPLTO                      
FROM CDCSANTANAMicroCredito..EPEND E (NOLOCK)      
WHERE E.EPNROPER IN (SELECT OPERACAO FROM DCO_PEND_FORMALIZ_analitico (NOLOCK))      
AND EPCMPLTO IS NOT NULL      
AND EPDTPEN = (SELECT MAX(EPDTPEN) FROM CDCSANTANAMicroCredito..EPEND E1 (NOLOCK) WHERE E1.EPNROPER = E.EPNROPER)      
AND DCO_PEND_FORMALIZ_analitico.OPERACAO = E.EPNROPER                      
-- SELECT * FROM   crv_PEND P (NOLOCK)                      
                       
                                      
                        
-- ***********************************************************  PEND CONTRATO                        
TRUNCATE TABLE crv_PEND                           
-- 007 PEND CONTRATO                           
INSERT INTO  crv_PEND  (OPCODPROD, OPNROPER, OPDTBASE, OPVLRFIN, OPCODORG1, O1DESCR,                          
       OPCODORG2, O2DESCR, OPCODORG3, O3DESCR,  OPCODORG4, O4DESCR,                          
       OPCODORG5, O5DESCR, OPCODORG6, EPCODPEN, EPCMPLTO, OPCODCLI,                          
       A13CODORG, A13DESCR,                         
      CLNOMECLI, CLFONEFIS, CLFONECOM, CLCGC,                          
      DIASATRASO)                        
   --, EPDTAGEN, EPDTPEN, EPDTCAD, EPDTLNCBX, EPDTFIM)                          
--   declare @DT_INI datetime declare @DT_FIM datetime set @DT_INI='20170801'  set @DT_FIM='20170831'                         
SELECT  distinct '' , --PPCODPRD,                         
  O.OPNROPER, O.OPDTBASE, O.OPVLRFIN,                        
      --O.PPCODORG1, T1.O1DESCR, O.PPCODORG2, T2.O2DESCR,                         
     '','',  '','',                        
     O.OPCODORG6, T3.O6DESCR,                          
  O.OPCODORG4, T4.O4DESCR, O.OPCODORG5, T5.O5DESCR, --O.PPCODORG6,                        
     O.OPNRPROP,  -- COM PROP                        
  E.EPCODPEN, E.EPCMPLTO,  --MOTIVO DA PEND DE CONTRATO                      
  O.OPCODCLI,                          
  T1O3.O3CODORG,  replace( REPLACE( replace (O3DESCR,'PROMOTORA',''), 'PROMOT ',''), 'PROM',''),                         
  C.CLNOMECLI, C.CLFONEFIS, C.CLFONECOM, C.CLCGC,                              
  0 AS DIASATRASO                        
  --E.EPDTAGEN, E.EPDTPEN, E.EPDTCAD, E.EPDTLNCBX, E.EPDTFIM                           
FROM                           
  ( SELECT  * FROM  CDCSANTANAMicroCredito..COPER O1    (NOLOCK)                        
  WHERE OPDTBASE BETWEEN  @DT_INI AND @DT_FIM ) AS O                        
 -- PENDENCIAS DE CONTRATO  --------------                        
  INNER JOIN CDCSANTANAMicroCredito..EPEND E  (NOLOCK) ON O.OPNROPER = E.EPNROPER                           
  INNER JOIN CDCSANTANAMicroCredito..TPEND TP (NOLOCK) ON E.EPCODPEN = TP.PDCODPEN                           
/*                        
  INNER JOIN CDCSANTANAMicroCredito..EPEND E (NOLOCK) ON O.OPNROPER = E.EPNROPER                           
  INNER JOIN CDCSANTANAMicroCredito..TPEND TP (NOLOCK) ON E.EPCODPEN = TP.PDCODPEN                           
*/                        
                        
  LEFT  JOIN CDCSANTANAMicroCredito..CCLIE C  (NOLOCK) ON C.CLCODCLI  = O.OPCODCLI                         
  LEFT  JOIN CDCSANTANAMicroCredito..TORG6 T3 (NOLOCK) ON T3.O6CODORG = O.OPCODORG6                         
  LEFT  JOIN CDCSANTANAMicroCredito..TORG4 T4 (NOLOCK) ON T4.O4CODORG = O.OPCODORG4                        
  LEFT  JOIN CDCSANTANAMicroCredito..TORG5 T5 (NOLOCK) ON T5.O5CODORG = O.OPCODORG5                
  LEFT  JOIN CDCSANTANAMicroCredito..TORG3 T1O3 (NOLOCK) ON  T1O3.O3CODORG = T3.O6CODORG3                        
WHERE  E.EPCODPEN = '007'  --    pend CONTRATOS   007                        
 -- INCLUI MAIS CONDICOES DEZ/17                       
 AND   ( O.OPDTLIQ IS NULL OR O.OPDTLIQ > @DT_FIM)                         
 AND O.OPDTBASE <= @DT_FIM    -- REMOVI  E.EPDTFIM > @DT_FIM                    
 AND O.OPDTBASE >= @DT_INI              
 -- EM 25/10/17 SOMENTE OS PENDENTES  **********   EPDTFIM PREENCHIDA NAO ESTAH MAIS PEND                      
 AND (E.EPDTFIM IS NULL OR E.EPDTFIM > @DT_FIM)  AND E.EPDTCAD <= @DT_FIM               
  AND T1O3.O3CODORG <> '000149'  --18/10/2018 - TIAGO                        
                       
-- PENDENTES CONTRATO                      
INSERT  DCO_PEND_FORMALIZ_analitico                       
   (A13CODORG ,A13DESCR,REGIAO,integrada,PEND_CONTRATO, OPERACAO,PROPOSTA,                      
   cod_OPERADOR, OPERADOR, COD_LOJA, LOJA,MOTIVO, CLCGC)                        
SELECT  DISTINCT                      
   A13CODORG, A13DESCR, O1DESCR, 0 ,1   , OPNROPER,OPCODORG6 --PROPOSTA                      
   , OPCODORG3, O3DESCR,  OPCODORG4, O4DESCR,EPCMPLTO,CLCGC                     
-- SELECT *                       
 FROM  crv_PEND P (NOLOCK)                        
WHERE  P.OPNROPER  NOT IN ( SELECT P1.OPERACAO                       
         FROM DCO_PEND_FORMALIZ_analitico P1 (NOLOCK))         
         
      
-- ATUALIZA OS AGENTES CF PEND                        
-- ATUALIZA PEND CONTRATO                      
-- SELECT * FROM DCO_PEND_FORMALIZ_analitico (NOLOCK)                      
UPDATE  DCO_PEND_FORMALIZ_analitico                       
SET   PEND_CONTRATO=1, MOTIVO=RTRIM(MOTIVO)+' ' +EPCMPLTO                      
FROM CDCSANTANAMicroCredito..EPEND E (NOLOCK)      
WHERE E.EPNROPER IN (SELECT OPERACAO FROM DCO_PEND_FORMALIZ_analitico (NOLOCK))      
AND EPCMPLTO IS NOT NULL      
AND EPDTPEN = (SELECT MAX(EPDTPEN) FROM CDCSANTANAMicroCredito..EPEND E1 (NOLOCK) WHERE E1.EPNROPER = E.EPNROPER)      
AND DCO_PEND_FORMALIZ_analitico.OPERACAO = E.EPNROPER                      
                        
-- ***********************************************************  PEND GRAVAMES                        
TRUNCATE TABLE DCO_PENDGravames                           
                          
INSERT INTO  DCO_PENDGravames  (OPCODPROD, OPNROPER, OPDTBASE, OPVLRFIN, OPCODORG1, O1DESCR,                          
 OPCODORG2, O2DESCR, OPCODORG3, O3DESCR,  OPCODORG4, O4DESCR,                          
 OPCODORG5, O5DESCR, OPCODORG6, EPCODPEN, EPCMPLTO, OPCODCLI,                          
 A13CODORG, A13DESCR,                     
 CLNOMECLI, CLFONEFIS, CLFONECOM, CLCGC,                          
 DIASATRASO, EPDTAGEN, EPDTPEN, EPDTCAD, EPDTLNCBX, EPDTFIM)                          
                        
                        
SELECT  distinct OPCODPROD,  O.OPNROPER, O.OPDTBASE, O.OPVLRFIN,O.OPCODORG1, T1.O1DESCR, O.OPCODORG2, T2.O2DESCR, O.OPCODORG6, T3.O6DESCR,                          
  O.OPCODORG4, T4.O4DESCR, O.OPCODORG5, T5.O5DESCR, O.OPCODORG6, E.EPCODPEN, E.EPCMPLTO, O.OPCODCLI,                    
  T1O3.O3CODORG,  replace( REPLACE( replace (O3DESCR,'PROMOTORA',''), 'PROMOT ',''), 'PROM',''),                         
  C.CLNOMECLI, C.CLFONEFIS, C.CLFONECOM, C.CLCGC,                              
  ISNULL((SELECT MAX(DATEDIFF(DD, P.PADTVCTO, @DT_FIM))                         
   FROM CDCSANTANAMicroCredito..CPARC P  (NOLOCK)                        
   WHERE P.PANROPER = O.OPNROPER                           
     AND ( P.PADTMOV IS NULL OR P.PADTMOV > @DT_FIM) AND  P.PADTVAL <= @DT_FIM AND  NOT (P.PACODMOV IN ('07', '08', '09', '13', '91', '94') AND                           
     P.PADTLIQ IS NOT NULL)), 0) AS DIASATRASO,                           
  E.EPDTAGEN, E.EPDTPEN, E.EPDTCAD, E.EPDTLNCBX, E.EPDTFIM                           
                          
FROM                           
    CDCSANTANAMicroCredito..COPER O    (NOLOCK)                        
  INNER JOIN CDCSANTANAMicroCredito..CCLIE C    (NOLOCK) ON O.OPCODCLI = C.CLCODCLI                           
  INNER JOIN CDCSANTANAMicroCredito..EPEND E (NOLOCK) ON O.OPNROPER = E.EPNROPER                           
  INNER JOIN CDCSANTANAMicroCredito..TPEND TP (NOLOCK) ON E.EPCODPEN = TP.PDCODPEN                           
                        
  LEFT  JOIN CDCSANTANAMicroCredito..TORG1 T1 (NOLOCK) ON O.OPCODORG1 = T1.O1CODORG                           
  LEFT  JOIN CDCSANTANAMicroCredito..TORG2 T2 (NOLOCK) ON O.OPCODORG2 = T2.O2CODORG                           
  LEFT  JOIN CDCSANTANAMicroCredito..TORG6 T3 (NOLOCK) ON O.OPCODORG6 = T3.O6CODORG                           
  LEFT  JOIN CDCSANTANAMicroCredito..TORG4 T4 (NOLOCK) ON O.OPCODORG4 = T4.O4CODORG                           
  LEFT  JOIN CDCSANTANAMicroCredito..TORG5 T5 (NOLOCK) ON O.OPCODORG5 = T5.O5CODORG                           
  LEFT JOIN CDCSANTANAMicroCredito..TORG3 T1O3 (NOLOCK) ON T3.O6CODORG3 = T1O3.O3CODORG                          
WHERE  E.EPCODPEN IN ( '200','407','507')  -- pend de gravames                        
and ( O.OPDTLIQ IS NULL OR O.OPDTLIQ > @DT_FIM)                           
AND O.OPDTBASE <= @DT_FIM   -- somente Pendentes OR E.EPDTFIM > @DT_FIM               
AND O.OPDTBASE >= @DT_INI                     
-- **************************                      
--AND (E.EPDTFIM IS NULL  )  -- ESTAVAM PENDENTES HISTORICO DIFERENTE DA PRODUCAO, SOH PENDENTES                      
AND E.EPDTCAD <= @DT_FIM                          
AND E.EPDTCAD >= @DT_INI                          
AND E.EPDTCAD <= @DT_FIM                          
AND 1= 0+ CASE WHEN  @AGNT='99' THEN 1 -- TODOS OS AGENTES                        
      WHEN  @AGNT<>'99' AND T1O3.O3CODORG = @AGNT  THEN 1                        
      ELSE  0   END                        
 AND T1O3.O3CODORG <> '000149'       --18/10/2018 - TIAGO              
/*                      
SELECT  distinct '' , --PPCODPRD,                         
  O.OPNROPER, O.OPDTBASE, O.OPVLRFIN,                        
      --O.PPCODORG1, T1.O1DESCR, O.PPCODORG2, T2.O2DESCR,                         
     '','',  '','',                        
     O.OPCODORG3, T3.O3DESCR,                          
  O.OPCODORG4, T4.O4DESCR, O.OPCODORG5, T5.O5DESCR, --O.PPCODORG6,                        
     O.OPNRPROP,  -- COM PROP                        
  E.EPCODPEN, E.EPCMPLTO, -- '', -- E.epTPmotivo,                        
  O.OPCODCLI,                          
  T1O3.A13CODORG,  replace( REPLACE( replace (A13DESCR,'PROMOTORA',''), 'PROMOT ',''), 'PROM',''),                         
  C.CLNOMECLI, C.CLFONEFIS, C.CLFONECOM, C.CLCGC,                              
  0 AS DIASATRASO                        
  --E.EPDTAGEN, E.EPDTPEN, E.EPDTCAD, E.EPDTLNCBX, E.EPDTFIM                           
FROM                           
  ( SELECT * FROM  CDCSANTANAMicroCredito..COPER O1    (NOLOCK)                        
WHERE OPDTBASE BETWEEN  @DT_INI AND @DT_FIM ) AS O                        
 -- PENDENCIAS DE CONTRATO  --------------                        
  INNER JOIN CDCSANTANAMicroCredito..EPEND E  (NOLOCK) ON O.OPNROPER = E.EPNROPER                           
  INNER JOIN CDCSANTANAMicroCredito..TPEND TP (NOLOCK) ON E.EPCODPEN = TP.PDCODPEN                           
  LEFT  JOIN CDCSANTANAMicroCredito..CCLIE C (NOLOCK) ON C.CLCODCLI  = O.OPCODCLI                     
  LEFT  JOIN CDCSANTANAMicroCredito..TORG3 T3 (NOLOCK) ON T3.O3CODORG = O.OPCODORG3                         
  LEFT  JOIN CDCSANTANAMicroCredito..TORG4 T4 (NOLOCK) ON T4.O4CODORG = O.OPCODORG4                        
  LEFT  JOIN CDCSANTANAMicroCredito..TORG5 T5 (NOLOCK) ON T5.O5CODORG = O.OPCODORG5                           
  LEFT  JOIN CDCSANTANAMicroCredito..TA1O3 T1O3 (NOLOCK) ON  T1O3.A13CODORG = T3.O3CODORGA13                        
WHERE  E.EPCODPEN  IN ( '200','407','507')  -- pend de gravames                        
 AND   ( O.OPDTLIQ IS NULL OR O.OPDTLIQ > @DT_FIM)                           
 AND O.OPDTBASE <= @DT_FIM   -- somente Pendentes OR E.EPDTFIM > @DT_FIM                         
 AND (E.EPDTFIM IS NULL  )  AND E.EPDTCAD <= @DT_FIM                          
 AND E.EPDTCAD >= @DT_INI                          
 AND E.EPDTCAD <= @DT_FIM                          
 -- FILTRO OK PELO JOIN                        
*/                        
-- REMOVE PEND GRAVAMES ********** SEM RENEG  em 20/10/17 ***********                        
-- SELECT * FROM  DCO_PENDContrato                           
DELETE  FROM  DCO_PENDGravames                           
 WHERE RIGHT(OPNROPER,1) IN ('A','B','C','D') -- REMOVE RENEGS                        
                        
-- ATUALIZA PEND GRAVAMES                      
                       
-- PENDENTES CONTRATO                      
INSERT  DCO_PEND_FORMALIZ_analitico                       
   (A13CODORG ,A13DESCR,REGIAO,integrada,PEND_GRAVAMES, OPERACAO,PROPOSTA,                      
   cod_OPERADOR, OPERADOR, COD_LOJA, LOJA,MOTIVO,CLCGC)                        
SELECT  DISTINCT                      
   A13CODORG, A13DESCR, O1DESCR, 0 ,1   , OPNROPER,'' --PROPOSTA                      
   , OPCODORG3, O3DESCR,  OPCODORG4, O4DESCR, EPCMPLTO,CLCGC                      
-- SELECT *                       
 FROM  DCO_PENDGravames P (NOLOCK)                        
WHERE  P.OPNROPER  NOT IN ( SELECT P1.OPERACAO                       
         FROM DCO_PEND_FORMALIZ_analitico P1 (NOLOCK))        
         
UPDATE  DCO_PEND_FORMALIZ_analitico             SET   PEND_GRAVAMES=1,                      
   MOTIVO = RTRIM(MOTIVO) +' ' + EPCMPLTO                      
FROM CDCSANTANAMicroCredito..EPEND E (NOLOCK)      
WHERE E.EPNROPER IN (SELECT OPERACAO FROM DCO_PEND_FORMALIZ_analitico (NOLOCK))      
AND EPCMPLTO IS NOT NULL      
AND EPDTPEN = (SELECT MAX(EPDTPEN) FROM CDCSANTANAMicroCredito..EPEND E1 (NOLOCK) WHERE E1.EPNROPER = E.EPNROPER)      
AND DCO_PEND_FORMALIZ_analitico.OPERACAO = E.EPNROPER                       
                        
-- PG CTA AG SINTETICO NA FORMALIZACAO ************************* NOV DEZ/17                        
TRUNCATE TABLE DCO_Prop_PG_Cta_AG                        
                        
INSERT INTO  DCO_Prop_PG_Cta_AG                      
SELECT  DISTINCT O3CODORG,O3DESCR,                      
   OPNROPER,                      
   OPDTBASE,                      
   OPCODCLI,                      
   OPNOMEBNF,                       
   '' AS ABRENAVAM,                      
                      
   --'' AS DCFAVNOME,  0.0 AS DCVALOR,                      
   B.DCFAVNOME, B.DCVALOR,                      
                      
   '' AS ABCHASSI,                      
   '' AS ABUFLCPL,                      
   ISNULL(EP,OPDTBASE), -- EPDTLNC +EPHORALNC                      
   --EPDTLNC +                      
    ISNULL(EPHORALNC,''),                       
    -- OPNRPROP varchar (20),  Cod_loja varchar (20),  Loja  varchar (200)                      
    OPNRPROP, OpCodORG4, '','',OPCODORG6,''                      
   --,OPCODOP,''                      
FROM (SELECT  * FROM  CDCSANTANAMICROCREDITO..COPER    AS OP1 (NOLOCK)                      
  WHERE OPDTBASE BETWEEN  @DT_INI AND @DT_FIM )  AS OP                      
INNER JOIN CDCSANTANAMICROCREDITO..TORG6 WITH (NOLOCK) ON OPCODORG6 = O6CODORG                      
INNER JOIN CDCSANTANAMICROCREDITO..TORG3 WITH (NOLOCK) ON O6CODORG3 = O3CODORG                      
LEFT  JOIN (SELECT  EPNRPROP,MAX(EPDTLNC +EPHORALNC)   AS EP, MAX(EPHORALNC) AS EPHORALNC                      
   FROM PROPWEBSMC..EPROP (NOLOCK)                       
   WHERE EPNRDECI IN ('662','663','664') -- PAGAMENTO DA REMESSA                    
   GROUP BY  EPNRPROP ) AS P                      
 ON EPNRPROP = OPNRPROP                      
LEFT  JOIN (select   DCNROPER, DCFAVNOME, DCVALOR                      
    FROM  CDCSANTANAMICROCREDITO..CDOCS AS B1  (NOLOCK)                       
) AS B                      
 ON   OP.OPNROPER=B.DCNROPER                 
 WHERE O3CODORG <> '000149'              
 AND OPDTBASE >= @DT_INI            
 AND OPDTBASE <= @DT_FIM                    
                      
DELETE FROM  DCO_Prop_PG_Cta_AG                         
 WHERE RIGHT(OPNROPER,1) IN ('A','B','C','D') -- REMOVE RENEGS                      
                      
-- ATUALIZA O NOME DA LOJA                      
-- Cod_loja varchar (20),  Loja  varchar (200)                      
UPDATE  DCO_Prop_PG_Cta_AG                        
 SET  LOJA = B.O4NOME                      
 --  SELECT *                      
 FROM CDCSANTANAMicroCredito..TORG4 B (NOLOCK)  --- LOJA                      
 WHERE B.O4CODORG=DCO_Prop_PG_Cta_AG.Cod_loja                      
                      
                      
-- ATUALIZA O NOME DO CLIENTE  -- PRECISO SEPARAR BNF E CLIENTE                      
-- Cod_loja varchar (20),  Loja  varchar (200)                      
UPDATE  DCO_Prop_PG_Cta_AG                        
 SET  OPNOMEBNF = B.CLNOMECLI                      
 --  SELECT *                      
 FROM CDCSANTANAMicroCredito..CCLIE B (NOLOCK)  --- LOJA                      
 WHERE B.CLCODCLI=DCO_Prop_PG_Cta_AG.OPCODCLI                       
                      
                      
-- DEZ 17 ***********                      
-- REMOVE O LOJA = BALCÃO  BALC BALCAO                       
-- SELECT * FROM DCO_Prop_PG_Cta_AG (NOLOCK)                      
-- DELETE  DCO_Prop_PG_Cta_AG   WHERE LEFT(LTRIM(LOJA),4) = 'BALC'  OR LEFT(LTRIM(LOJA),4) = 'BAL '                       
-- OR (NOME_CLI LIKE '%BALCÃO%' ) OR (NOME_CLI LIKE '%BALCAO%' )                      
                      
                      
-- REMOVE AGENTE CORPAL                      
DELETE  DCO_Prop_PG_Cta_AG                        
 WHERE A13DESCR LIKE '%CORPAL%'                      
                      
-- CLIENTE E FAVORECIDO = REMOVER                      
DELETE  DCO_Prop_PG_Cta_AG                        
 WHERE NOME_CLI = OPNOMEBNF                      
--  SELECT * FROM  DCO_Prop_PG_Cta_AG  (NOLOCK)  WHERE   CHARINDEX(LEFT(OPNOMEBNF,5),A13DESCR) >0 OR CHARINDEX(LEFT(OPNOMEBNF,4),A13DESCR) >0  -- NOME DO BENEF                      
                      
-- ajusta os dados do CLIENTE NOME E NAO DO BENEFICIARIO                      
UPDATE  DCO_Prop_PG_Cta_AG                        
 SET  NOME_CLI= B.CLNOMECLI                      
 FROM CDCSANTANAMicroCredito..CCLIE B (NOLOCK)                       
 WHERE B.CLCODCLI=DCO_Prop_PG_Cta_AG.OPCODCLI                      
                      
-- -- condicao Cliente e Favorecido <>   e Favorecido Prox do Agente.  SEM Balcao                      
-- REMOVE O LOJA = BALCÃO  BALC BALCAO                       
DELETE  DCO_Prop_PG_Cta_AG                        
 WHERE (NOME_CLI LIKE '%BALC%' ) OR (NOME_CLI LIKE '%BALCÃO%' ) OR (NOME_CLI LIKE '%BALCAO%' )                      
                      
                      
-- 30/10/17 AJUSTA NOME DO OPERADOR                      
UPDATE  DCO_Prop_PG_Cta_AG                        
 SET  OPERADOR= B.O6DESCR    --  SELECT *                      
 FROM CDCSANTANAMicroCredito..TORG6 B (NOLOCK)                       
 WHERE DCO_Prop_PG_Cta_AG.OPCODORG3 = B.O6CODORG                      
                      
-- ajusta os dados da Garantia                      
UPDATE  DCO_Prop_PG_Cta_AG                        
 SET  ABRENAVAM= B.ABRENAVAM,ABCHASSI=B.ABCHASSI, ABUFLCPL=B.ABUFLCPL                      
 FROM CDCSANTANAMicroCredito..CBFIN B (NOLOCK)                       
 WHERE B.ABNROPER=DCO_Prop_PG_Cta_AG.OPNROPER                      
  AND B.ABCNTRL = ( SELECT     MAX(ABCNTRL) FROM     CDCSANTANAMICROCREDITO..CBFIN B1 (NOLOCK)                      
                               WHERE B1.ABNROPER = B.ABNROPER ) --  AND B1.ABNRGAR IN ('01','1' ) )                      
                      
-- TRANSFERE DADOS                      
-- SELECT * FROM DCO_Prop_PG_Cta_AG (NOLOCK)                      
-- ATUALIZA PEND GRAVAMES             
-- SELECT * FROM DCO_PEND_FORMALIZ_analitico (NOLOCK)                      
UPDATE  DCO_PEND_FORMALIZ_analitico                       
SET   PG_CTA_AG = 1                      
FROM  DCO_Prop_PG_Cta_AG P (NOLOCK)                        
WHERE  DCO_PEND_FORMALIZ_analitico.OPERACAO = P.OPNROPER                      
-- SELECT * FROM   crv_PEND P (NOLOCK)                      
                       
-- PENDENTES CONTRATO                      
                   
INSERT  DCO_PEND_FORMALIZ_analitico                       
   (A13CODORG ,A13DESCR,--REGIAO,                      
integrada,PG_CTA_AG, OPERACAO,PROPOSTA)                      
  --,  cod_OPERADOR, OPERADOR, COD_LOJA, LOJA)                        
SELECT  DISTINCT                      
   A13CODORG, A13DESCR, --O1DESCR,                      
   0 ,1   , OPNROPER,'' --PROPOSTA                      
  -- , OPCODORG3, O3DESCR                      
--,  OPCODORG4, O4DESCR                      
-- SELECT *                       
 FROM  DCO_Prop_PG_Cta_AG P (NOLOCK)                        
WHERE  P.OPNROPER  NOT IN ( SELECT P1.OPERACAO                       
         FROM DCO_PEND_FORMALIZ_analitico P1 (NOLOCK))                      
                      
-- SUBSTITUICAO DE GARANTIA *******************                      
TRUNCATE TABLE crv_PEND                        
                        
INSERT INTO  crv_PEND (OPCODPROD, OPNROPER, OPDTBASE, OPVLRFIN, OPCODORG1, O1DESCR,                        
 OPCODORG2, O2DESCR, OPCODORG3, O3DESCR,  OPCODORG4, O4DESCR,                        
 OPCODORG5, O5DESCR, OPCODORG6, EPCODPEN, EPCMPLTO, OPCODCLI,                        
 A13CODORG, A13DESCR,                       
 ABRENAVAM, ABVLRTAB, ABCHASSI, ABCERTIF,   ABPLACA, ABMODELO,                       
 CLNOMECLI, CLFONEFIS, CLFONECOM, CLCGC,                        
 DIASATRASO )                      
                        
SELECT  distinct OPCODPROD,  O.OPNROPER, O.OPDTBASE, O.OPVLRFIN,O.OPCODORG1, T1.O1DESCR, O.OPCODORG2, T2.O2DESCR, O.OPCODORG6, T3.O6DESCR,                        
  O.OPCODORG4, T4.O4DESCR, O.OPCODORG5, T5.O5DESCR, O.OPCODORG6,                       
 --E.EPCODPEN, E.EPCMPLTO, O.OPCODCLI,                        
 O.OPCODPRD, '', O.OPCODCLI,                        
  T1O3.O3CODORG,  replace( REPLACE( replace (O3DESCR,'PROMOTORA',''), 'PROMOT ',''), 'PROM',''),                       
-- B.ABRENAVAM, B.ABVLRTAB, B.ABCHASSI, B.ABCERTIF, B.ABPLACA, B.ABMODELO,                       
 '',0.0,'','','','',                      
  C.CLNOMECLI, C.CLFONEFIS, C.CLFONECOM, C.CLCGC,                            
  ISNULL((SELECT MAX(DATEDIFF(DD, P.PADTVCTO, @DT_FIM))                       
   FROM CDCSANTANAMicroCredito..CPARC P  (NOLOCK)                      
   WHERE P.PANROPER = O.OPNROPER                         
     AND ( P.PADTMOV IS NULL OR P.PADTMOV > @DT_FIM) AND  P.PADTVAL <= @DT_FIM AND  NOT (P.PACODMOV IN ('07', '08', '09', '13', '91', '94') AND                         
     P.PADTLIQ IS NOT NULL)), 0) AS DIASATRASO                      
FROM                         
  (SELECT O1.*                       
 FROM (SELECT * FROM CDCSANTANAMicroCredito..EOCOR (NOLOCK)                      
     WHERE --EODTOCOR >='20180101'  -- AJUSTES 7/3/18                      
        EODTOCOR BETWEEN @DT_INI AND @DT_FIM                       
      AND  EOCODOCOR IN (305,306) ) AS OC -- SUBSTITUICAO DE GARANTIA                      
      --  WHERE O1.OPCODPRD IN (420,400,440) NULL EM 5/3/18                      
 JOIN  CDCSANTANAMicroCredito..COPER O1    (NOLOCK)                       
 ON EONROPER = OPNROPER -- AND EOCODOCOR IN (305,306) -- SUBSTITUICAO DE GARANTIA                      
  ) AS O                      
   INNER JOIN CDCSANTANAMicroCredito..CCLIE C    (NOLOCK) ON O.OPCODCLI = C.CLCODCLI                         
  LEFT  JOIN CDCSANTANAMicroCredito..TORG1 T1 (NOLOCK) ON O.OPCODORG1 = T1.O1CODORG                         
  LEFT  JOIN CDCSANTANAMicroCredito..TORG2 T2 (NOLOCK) ON O.OPCODORG2 = T2.O2CODORG                    
  LEFT  JOIN CDCSANTANAMicroCredito..TORG6 T3 (NOLOCK) ON O.OPCODORG6 = T3.O6CODORG                         
  LEFT  JOIN CDCSANTANAMicroCredito..TORG4 T4 (NOLOCK) ON O.OPCODORG4 = T4.O4CODORG                         
  LEFT  JOIN CDCSANTANAMicroCredito..TORG5 T5 (NOLOCK) ON O.OPCODORG5 = T5.O5CODORG                         
  LEFT JOIN CDCSANTANAMicroCredito..TORG3 T1O3 (NOLOCK) ON T3.O6CODORG3 = T1O3.O3CODORG                        
WHERE                      
  O.OPDTBASE BETWEEN  @DT_INI  AND @DT_FIM                        
AND    ( O.OPDTLIQ IS NULL OR O.OPDTLIQ > @DT_FIM)                         
-- ALTERADOS DESATIVADOS EM 13/9/17                      
AND 1= 0+ CASE WHEN  @AGNT=99  THEN 1 -- TODOS OS AGENTES                      
      WHEN  @AGNT=980 THEN 1 -- SOH OS DESATIVADOS         
      WHEN  @AGNT NOT IN (99,980) AND T1O3.O3CODORG = @AGNT  THEN 1                      
      ELSE  0   END                       
 AND T1O3.O3CODORG <> '000149'   --18/10/2018 - TIAGO              
  AND O.OPDTBASE >= @DT_INI            
 AND O.OPDTBASE <= @DT_FIM            
                        
                      
-- TRANSFERE DADOS                      
-- ATUALIZA PEND GRAVAMES                      
-- SELECT * FROM DCO_PEND_FORMALIZ_analitico (NOLOCK)                      
UPDATE  DCO_PEND_FORMALIZ_analitico                       
SET   SUBSTITUI_GARANTIA = 1                      
FROM  crv_PEND P (NOLOCK)                        
WHERE  DCO_PEND_FORMALIZ_analitico.OPERACAO = P.OPNROPER                      
-- SELECT * FROM   crv_PEND P (NOLOCK)                      
-- SUBSTIT GARANT                      
INSERT  DCO_PEND_FORMALIZ_analitico                       
   (A13CODORG ,A13DESCR,--REGIAO,                      
   integrada,PG_CTA_AG, OPERACAO,PROPOSTA,                      
   MOTIVO)                      
  --,  cod_OPERADOR, OPERADOR, COD_LOJA, LOJA)                        
SELECT  DISTINCT                      
   A13CODORG, A13DESCR, --O1DESCR,                      
   0 ,1   , OPNROPER,'',EPCMPLTO --PROPOSTA                      
-- SELECT *                      
 FROM  crv_PEND P (NOLOCK)                        
WHERE  P.OPNROPER  NOT IN ( SELECT P1.OPERACAO                       
         FROM DCO_PEND_FORMALIZ_analitico P1 (NOLOCK))                      
                      
                      
                      
                      
                      
--A13CODORG A13DESCR A13CODORGA23 A13DDDTEL A13TEL A13DTCAD                      
--1 PROMOTORA SHOPCRED NULL 11 98514-9702 2010-01-05 00:00:00                         
-- SELECT * FROM DCO_SUBST_GARANTIA                      
                      
                      
                      
-- PROPOSTA PRODUCAO *******************                      
UPDATE  DCO_PEND_FORMALIZ_ANALITICO                        
 SET  PROPOSTA= OP.OPNRPROP                      
FROM CDCSANTANAMICROCREDITO..COPER  OP (NOLOCK)                      
WHERE   DCO_PEND_FORMALIZ_ANALITICO.OPERACAO = OP.OPNROPER                      
                      
-- agentes PRODUCAO *******************                      
UPDATE  DCO_PEND_FORMALIZ_ANALITICO                        
 SET  A13CODORG= OP.O6CODORG3                      
FROM CDCSANTANAMICROCREDITO..TORG6  OP (NOLOCK)                      
WHERE   OP.O6CODORG = COD_OPERADOR                     
                   
UPDATE  DCO_PEND_FORMALIZ_ANALITICO                        
 SET  A13DESCR= OP.O3DESCR                      
FROM CDCSANTANAMICROCREDITO..TORG3  OP (NOLOCK)                      
WHERE   DCO_PEND_FORMALIZ_ANALITICO.A13CODORG = OP.O3CODORG                      
                        
-- AGENTE ATIVO S N                   
UPDATE DCO_PEND_FORMALIZ_ANALITICO                        
 SET AGATIVO = (SELECT CASE WHEN ISNULL(O3CODORG2,0)=1 THEN 'S' ELSE 'N' END                        
      FROM CDCSANTANAMicroCredito..TORG3 T1O3 (NOLOCK)  -- AGENTE                        
      WHERE DCO_PEND_FORMALIZ_ANALITICO.A13CODORG = T1O3.O3CODORG )                        
                       
-- BALCAO                      
UPDATE DCO_PEND_FORMALIZ_ANALITICO                        
 SET BALCAO = CASE WHEN LEFT(LOJA,4)='BALC' THEN 1 ELSE 0 END                      
                      
-- ANALISTA                  
--UPDATE DCO_PEND_FORMALIZ_ANALITICO                    
--SET EPUSUARIO = (SELECT E.EPUSUINI FROM PROPWEBSMC..EPROP E(NOLOCK)                    
--WHERE E.EPNRDECI IN ('114') AND PROPOSTA = E.EPNRPROP AND E.EPNRSEQ = (SELECT MAX(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1 WHERE E1.EPNRPROP = E.EPNRPROP AND E1.EPNRDECI IN ('114')))                    
  
UPDATE DCO_PEND_FORMALIZ_ANALITICO                    
SET EPUSUARIO = (SELECT E.EPUSUARIO FROM PROPWEBSMC..EPROP E(NOLOCK)                    
WHERE E.EPNRDECI IN ('600') AND PROPOSTA = E.EPNRPROP AND E.EPNRSEQ = (SELECT MAX(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1 WHERE E1.EPNRPROP = E.EPNRPROP AND E1.EPNRDECI IN ('600')))                    
WHERE EPUSUARIO IS NULL  
                  
--MOTIVO                  
UPDATE DCO_PEND_FORMALIZ_ANALITICO                  
SET MOTIVO = (SELECT MTDESCR FROM PROPWEBSMC..EPROP E INNER JOIN PROPWEBSMC..TMOTI (NOLOCK) ON REPLACE(SUBSTRING(EPTPMOTIVO, 1, CHARINDEX(';', EPTPMOTIVO)),';','') = MTCODMOT                          
INNER JOIN PROPWEBSMC..CMOVP (NOLOCK) ON MPNRPROP = E.EPNRPROP WHERE EPNRDECI IN('600') AND EPSITFIM IN ('PEN','REP') AND PROPOSTA = E.EPNRPROP                
AND E.EPDTHRFIM = (SELECT MAX(E1.EPDTHRFIM) FROM PROPWEBSMC..EPROP E1 WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN('600')  AND EPTPMOTIVO <> ''))                  
                  
--SITUACAO                  
UPDATE DCO_PEND_FORMALIZ_ANALITICO             
SET SITUACAO = (SELECT MPSIT FROM PROPWEBSMC..CMOVP M (NOLOCK)                   
WHERE M.MPNRPROP = PROPOSTA)        
  
--DATA FORMALIZAÇÃO            
UPDATE DCO_PEND_FORMALIZ_ANALITICO  
SET DT_FORMALIZ = (SELECT EPDTLNC FROM PROPWEBSMC..EPROP E (NOLOCK) WHERE E.EPNRDECI = '600' AND EPNRPROP = PROPOSTA  
AND EPNRSEQ = (SELECT MIN(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1 WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN('600')))  
  
--DATA INTEGRAÇÃO  
UPDATE DCO_PEND_FORMALIZ_ANALITICO  
SET DT_INTEG = (SELECT EPDTLNC FROM PROPWEBSMC..EPROP E (NOLOCK) WHERE E.EPNRDECI = '22' AND EPNRPROP = PROPOSTA  
AND EPNRSEQ = (SELECT MIN(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1 WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN('22')))  

DELETE FROM DCO_PEND_FORMALIZ_ANALITICO  WHERE PROPOSTA IN (SELECT PPNRPROP FROM PROPWEBSMC..CPROP (NOLOCK) WHERE PPCODPRD IN ('000300','000301','000233','000242'))
                      
--SELECT * FROM CDCSANTANAMICROCREDITO..TA1O3  ag (NOLOCK)                      
--SELECT * FROM CDCSANTANAMICROCREDITO..TORG3  OP (NOLOCK)                      
    */                    
-- ************** NOVO - TIAGO  - 03/06/2019*******************                        

TRUNCATE TABLE DCO_PEND_FORMALIZ_ANALITICO

INSERT INTO DCO_PEND_FORMALIZ_ANALITICO(
A13CODORG,
A13DESCR,                                       
CLCGC,                       
PROPOSTA,     
DT_FORMALIZ,  
MOTIVO,                      
COD_LOJA,                      
LOJA,                      
COD_OPERADOR,                       
OPERADOR,                      
EPUSUARIO,                  
SITUACAO,  
DT_INTEG,
VLR_LIB,
SCORE)  
SELECT DISTINCT PPCODORG3, O3DESCR, CLCODCLI, PPNRPROP, '', '', PPCODORG4, O4DESCR, PPCODORG6, O6DESCR, '', '', '', 
PPVLRFIN,PPSCORING FROM PROPWEBSMC..EPROP E (NOLOCK)   
INNER JOIN PROPWEBSMC..CPROP (NOLOCK) ON E.EPNRPROP = PPNRPROP   
INNER JOIN PROPWEBSMC..CMOVP (NOLOCK) ON PPNRPROP = MPNRPROP   
INNER JOIN CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ON PPCODORG3 = O3CODORG
INNER JOIN CDCSANTANAMICROCREDITO..TORG4 (NOLOCK) ON PPCODORG4 = O4CODORG
INNER JOIN CDCSANTANAMICROCREDITO..TORG6 (NOLOCK) ON PPCODORG6 = O6CODORG
LEFT  JOIN CDCSANTANAMICROCREDITO..COPER (NOLOCK) ON PPNRPROP = OPNRPROP
INNER JOIN CDCSANTANAMicroCredito..CCLIE (NOLOCK) ON PPCODCLI = CLCODCLI                         
WHERE EPDTLNC BETWEEN @DT_INI AND @DT_FIM AND PPCODPRD <> '000221'   
AND E.EPNRSEQ = (SELECT MAX(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1 WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN ('662','663'))  
AND PPCODOP IN (SELECT M.COD_MODA                    
FROM TTIPO_PROD T (NOLOCK), TMODA_TIPO_PROD M (NOLOCK)                      
WHERE M.COD_PROD = T.COD_PROD                     
AND T.COD_MODALIDADE = M.COD_MODALIDADE                     
AND T.COD_PROD IN('V','CP'))   
AND (ISNULL(OPESTORNO,'') <>'S') 
UNION 
SELECT DISTINCT PPCODORG3, O3DESCR, CLCODCLI, PPNRPROP, '', '', PPCODORG4, O4DESCR, PPCODORG6, O6DESCR, '', '', '',
PPVLRFIN,PPSCORING FROM PROPWEBSMC..EPROP E (NOLOCK)   
INNER JOIN PROPWEBSMC..CPROP (NOLOCK) ON E.EPNRPROP = PPNRPROP   
INNER JOIN PROPWEBSMC..CMOVP (NOLOCK) ON PPNRPROP = MPNRPROP   
INNER JOIN CDCSANTANAMICROCREDITO..TORG3 (NOLOCK) ON PPCODORG3 = O3CODORG
INNER JOIN CDCSANTANAMICROCREDITO..TORG4 (NOLOCK) ON PPCODORG4 = O4CODORG
INNER JOIN CDCSANTANAMICROCREDITO..TORG6 (NOLOCK) ON PPCODORG6 = O6CODORG
LEFT  JOIN CDCSANTANAMICROCREDITO..COPER (NOLOCK) ON PPNRPROP = OPNRPROP
INNER JOIN CDCSANTANAMicroCredito..CCLIE (NOLOCK) ON PPCODCLI = CLCODCLI                         
WHERE E.EPDTHRFIM BETWEEN @DT_INI + '00:00:00' AND @DT_FIM + '23:59:59'  AND PPCODPRD <> '000221'   
AND E.EPNRSEQ = (SELECT MAX(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1 WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN ('662','663'))  
AND PPCODOP IN (SELECT M.COD_MODA                    
FROM TTIPO_PROD T (NOLOCK), TMODA_TIPO_PROD M (NOLOCK)                      
WHERE M.COD_PROD = T.COD_PROD                     
AND T.COD_MODALIDADE = M.COD_MODALIDADE                     
AND T.COD_PROD IN('V','CP'))   
AND (ISNULL(OPESTORNO,'') <>'S') 

UPDATE DCO_PEND_FORMALIZ_ANALITICO  
SET DT_FORMALIZ = (SELECT EPDTLNC FROM PROPWEBSMC..EPROP E (NOLOCK) WHERE E.EPNRDECI IN('662','663','664') AND EPNRPROP = PROPOSTA  
AND EPNRSEQ = (SELECT MAX(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1 WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN('662','663','664')))  

UPDATE DCO_PEND_FORMALIZ_ANALITICO                  
SET MOTIVO = (SELECT MTDESCR FROM PROPWEBSMC..EPROP E INNER JOIN PROPWEBSMC..TMOTI (NOLOCK) ON REPLACE(SUBSTRING(EPTPMOTIVO, 1, CHARINDEX(';', EPTPMOTIVO)),';','') = MTCODMOT                          
INNER JOIN PROPWEBSMC..CMOVP (NOLOCK) ON MPNRPROP = E.EPNRPROP WHERE EPNRDECI IN('600') AND EPSITFIM IN ('PEN','REP') AND PROPOSTA = E.EPNRPROP                
AND E.EPDTHRFIM = (SELECT MAX(E1.EPDTHRFIM) FROM PROPWEBSMC..EPROP E1 WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN('600')  AND EPTPMOTIVO <> ''))                  

UPDATE DCO_PEND_FORMALIZ_ANALITICO                    
SET EPUSUARIO = (SELECT E.EPUSUARIO FROM PROPWEBSMC..EPROP E(NOLOCK)                    
WHERE E.EPNRDECI IN ('600') AND PROPOSTA = E.EPNRPROP AND E.EPNRSEQ = (SELECT MAX(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1 WHERE E1.EPNRPROP = E.EPNRPROP AND E1.EPNRDECI IN ('600')))                    
WHERE EPUSUARIO IS NULL  

UPDATE DCO_PEND_FORMALIZ_ANALITICO             
SET SITUACAO = (SELECT MPSIT FROM PROPWEBSMC..CMOVP M (NOLOCK)                   
WHERE M.MPNRPROP = PROPOSTA)      

UPDATE DCO_PEND_FORMALIZ_ANALITICO  
SET DT_INTEG = (SELECT EPDTLNC FROM PROPWEBSMC..EPROP E (NOLOCK) WHERE E.EPNRDECI = '22' AND EPNRPROP = PROPOSTA  
AND EPNRSEQ = (SELECT MIN(E1.EPNRSEQ) FROM PROPWEBSMC..EPROP E1 WHERE E1.EPNRPROP = E.EPNRPROP AND EPNRDECI IN('22')))  

SELECT  ISNULL(D.A13CODORG,'') AS A13CODORG,                       
ISNULL(AG.O3DESCR,'') AS A13DESCR,                       
ISNULL(AGATIVO,'')  AS AGATIVO,                       
ISNULL(CLCGC,'') AS CPF_CNPJ,                       
ISNULL(PROPOSTA,'') AS PROPOSTA,     
DT_FORMALIZ,  
ISNULL(MOTIVO,'')  AS MOTIVO,                      
ISNULL(COD_LOJA,'') AS COD_LOJA,                      
ISNULL(LOJA,'')  AS LOJA,                      
ISNULL(COD_OPERADOR,'') AS COD_OPERADOR,                       
ISNULL(OPERADOR,'')  AS OPERADOR,                      
ISNULL(PEND_AGENTE,'')  AS PEND_AGENTE,                      
ISNULL(PEND_DCO,'')  AS PEND_DCO,                      
ISNULL(PEND_CONTRATO,'') AS PEND_CONTRATO,                       
ISNULL(PEND_GRAVAMES,'') AS PEND_GRAVAMES,                      
ISNULL(PG_CTA_AG,'')   AS PG_CTA_AG,                       
ISNULL(BALCAO,'')   AS BALCAO,                      
ISNULL(SUBSTITUI_GARANTIA,'') AS SUBSTITUI_GARANTIA,                  
ISNULL(D.EPUSUARIO,'') AS USUARIO,                  
ISNULL(SITUACAO,'') AS SITUACAO,  
DT_INTEG,
VLR_LIB,
SCORE FROM DCO_PEND_FORMALIZ_ANALITICO D
LEFT join  CDCSANTANAMICROCREDITO..TORG3  ag (NOLOCK) on D.A13codorg = ag.O3codorg      
WHERE 1= 0+ CASE WHEN  @AGNT='99' THEN 1 -- TODAS OS AGENTES    
    WHEN  @AGNT<>'99' AND A13CODORG = @AGNT  THEN 1            
    ELSE  0   END  
ORDER BY PROPOSTA
                      
END 