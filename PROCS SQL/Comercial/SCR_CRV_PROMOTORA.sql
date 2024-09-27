USE sig --[Santana]
GO
/****** Object:  StoredProcedure [dbo].[scr_graf_Rolag_Consolid]    Script Date: 05/01/2017 16:23:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROC CARGA FECHAMENTO  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CRV_PROMOTORA' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CRV_PROMOTORA] 
GO  
CREATE Procedure [dbo].SCR_CRV_PROMOTORA (          
 @DataContrato as datetime,          
@DT_REF SMALLDATETIME,                  
@DT_INI SMALLDATETIME,              
@DT_FIM SMALLDATETIME,             
@AGNT   INT ) AS            
            
 begin             
            
TRUNCATE TABLE crv_PEND            
            
INSERT INTO  crv_PEND (OPCODPROD, OPNROPER, OPDTBASE, OPVLRFIN, OPCODORG1, O1DESCR,            
 OPCODORG2, O2DESCR, OPCODORG3, O3DESCR,  OPCODORG4, O4DESCR,            
 OPCODORG5, O5DESCR, OPCODORG6, EPCODPEN, EPCMPLTO, OPCODCLI,            
 A13CODORG, A13DESCR,           
 ABRENAVAM, ABVLRTAB, ABCHASSI, ABCERTIF,   ABPLACA, ABMODELO,           
 CLNOMECLI, CLFONEFIS, CLFONECOM, CLCGC,            
 DIASATRASO, EPDTAGEN, EPDTPEN, EPDTCAD, EPDTLNCBX, EPDTFIM)            
            
            
SELECT  distinct OPCODPROD,  O.OPNROPER, O.OPDTBASE, O.OPVLRFIN,O.OPCODORG1, '', O.OPCODORG2, '', O.OPCODORG6, '',            
  O.OPCODORG4, '', O.OPCODORG5, '', O.OPCODORG3, E.EPCODPEN, E.EPCMPLTO, O.OPCODCLI,            
  O.OPCODORG3,  replace( REPLACE( replace ('','PROMOTORA',''), 'PROMOT ',''), 'PROM',''),                
 '',0.0,'','','','',          
  C.CLNOMECLI, C.CLFONEFIS, C.CLFONECOM, C.CLCGC,                
  ISNULL((SELECT MAX(DATEDIFF(DD, P.PADTVCTO, @DT_REF))           
   FROM CDCSANTANAMicroCredito..CPARC P  (NOLOCK)          
   WHERE P.PANROPER = O.OPNROPER             
     AND ( P.PADTMOV IS NULL OR P.PADTMOV > @DT_REF) AND  P.PADTVAL <= @DT_REF AND  NOT (P.PACODMOV IN ('07', '08', '09', '13', '91', '94') AND             
     P.PADTLIQ IS NOT NULL)), 0) AS DIASATRASO,             
  E.EPDTAGEN, E.EPDTPEN, E.EPDTCAD, E.EPDTLNCBX, E.EPDTFIM             
            
FROM             
  CDCSANTANAMicroCredito..COPER O    (NOLOCK)          
  INNER JOIN CDCSANTANAMicroCredito..CCLIE C    (NOLOCK) ON O.OPCODCLI = C.CLCODCLI             
  INNER JOIN CDCSANTANAMicroCredito..EPEND E (NOLOCK) ON O.OPNROPER = E.EPNROPER             
  INNER JOIN CDCSANTANAMicroCredito..TPEND TP (NOLOCK) ON E.EPCODPEN = TP.PDCODPEN             
              
  LEFT JOIN CDCSANTANAMicroCredito..TORG3 T1O3 (NOLOCK) ON T1O3.O3CODORG = O.OPCODORG3         
WHERE E.EPCODPEN = '151'  -- ok em 1/9/17   --  '200'    pend crv antes 151 alterado em 28/8/17                 
and   ( O.OPDTLIQ IS NULL OR O.OPDTLIQ > @DT_REF)             
AND O.OPDTBASE <= @DT_FIM            
AND (E.EPDTFIM IS NULL OR E.EPDTFIM > @DT_REF )    
AND E.EPDTCAD <= @DT_REF            
AND E.EPDTCAD >= @DT_INI            
AND E.EPDTCAD <= @DT_FIM     
AND T1O3.O3ATIVA IN ('S','A')  
AND 1= 0+ CASE WHEN  @AGNT='99' THEN 1 -- TODOS OS AGENTES          
      WHEN  @AGNT<>'99' AND T1O3.O3CODORG = @AGNT  THEN 1          
      ELSE  0   END   
AND T1O3.O3DESCR NOT LIKE 'SHOP%'  	          
          
-- insere as operacoes da SERVICE CF CADASTRO de usuario complementar          
IF @AGNT<>'99'          
-- select * from usuario_svc          
INSERT INTO  crv_PEND (OPCODPROD, OPNROPER, OPDTBASE, OPVLRFIN, OPCODORG1, O1DESCR,            
 OPCODORG2, O2DESCR, OPCODORG3, O3DESCR,  OPCODORG4, O4DESCR,            
 OPCODORG5, O5DESCR, OPCODORG6, EPCODPEN, EPCMPLTO, OPCODCLI,            
 A13CODORG, A13DESCR,           
 ABRENAVAM, ABVLRTAB, ABCHASSI, ABCERTIF,   ABPLACA, ABMODELO,           
 CLNOMECLI, CLFONEFIS, CLFONECOM, CLCGC,            
 DIASATRASO, EPDTAGEN, EPDTPEN, EPDTCAD, EPDTLNCBX, EPDTFIM)            
            
            
SELECT  distinct OPCODPROD,  O.OPNROPER, O.OPDTBASE, O.OPVLRFIN,O.OPCODORG1, '', O.OPCODORG2, '', O.OPCODORG6, '',            
  O.OPCODORG4, '', O.OPCODORG5, '', O.OPCODORG3, E.EPCODPEN, E.EPCMPLTO, O.OPCODCLI,            
  O.OPCODORG3,  replace( REPLACE( replace ('','PROMOTORA',''), 'PROMOT ',''), 'PROM',''),           
-- B.ABRENAVAM, B.ABVLRTAB, B.ABCHASSI, B.ABCERTIF, B.ABPLACA, B.ABMODELO,           
 '',0.0,'','','','',          
  C.CLNOMECLI, C.CLFONEFIS, C.CLFONECOM, C.CLCGC,                
  ISNULL((SELECT MAX(DATEDIFF(DD, P.PADTVCTO, @DT_REF))           
   FROM CDCSANTANAMicroCredito..CPARC P  (NOLOCK)          
   WHERE P.PANROPER = O.OPNROPER             
     AND ( P.PADTMOV IS NULL OR P.PADTMOV > @DT_REF) AND  P.PADTVAL <= @DT_REF AND  NOT (P.PACODMOV IN ('07', '08', '09', '13', '91', '94') AND             
     P.PADTLIQ IS NOT NULL)), 0) AS DIASATRASO,             
  E.EPDTAGEN, E.EPDTPEN, E.EPDTCAD, E.EPDTLNCBX, E.EPDTFIM             
            
FROM             
  CDCSANTANAMicroCredito..COPER O    (NOLOCK)          
  INNER JOIN CDCSANTANAMicroCredito..CCLIE C    (NOLOCK) ON O.OPCODCLI = C.CLCODCLI             
  INNER JOIN CDCSANTANAMicroCredito..EPEND E (NOLOCK) ON O.OPNROPER = E.EPNROPER             
  INNER JOIN CDCSANTANAMicroCredito..TPEND TP (NOLOCK) ON E.EPCODPEN = TP.PDCODPEN              
  INNER  JOIN (select * from CDCSANTANAMicroCredito..TORG6 T3 (NOLOCK)          
    where convert(int,o6codorg) in (select distinct codOperador from usuario_svc S1 (NOLOCK)  where codAgente=@AGNT)          
       ) as T3-- SERVICE          
  ON  T3.O6CODORG =O.OPCODORG6   -- OPER      
          
  INNER JOIN CDCSANTANAMicroCredito..TORG3 T1O3 (NOLOCK)           
   ON  T1O3.O3CODORG = O.OPCODORG3  -- AG          
WHERE E.EPCODPEN = '151'  -- ok em 1/9/17   --  '200'    pend crv antes 151 alterado em 28/8/17          
 AND   ( O.OPDTLIQ IS NULL OR O.OPDTLIQ > @DT_REF)             
 AND O.OPDTBASE <= @DT_FIM            
 AND (E.EPDTFIM IS NULL OR E.EPDTFIM > @DT_REF )    
 AND E.EPDTCAD <= @DT_REF            
 AND E.EPDTCAD >= @DT_INI            
 AND E.EPDTCAD <= @DT_FIM    
 AND T1O3.O3ATIVA IN ('S','A')  
 AND T1O3.O3DESCR NOT LIKE 'SHOP%'  	                  
 -- FILTRO OK PELO JOIN          
            
            
-- ajusta os dados da Garantia 13/2/17          
UPDATE  crv_PEND            
 SET  ABRENAVAM= B.ABRENAVAM, ABVLRTAB=B.ABVLRTAB,           
   ABCHASSI=B.ABCHASSI, ABCERTIF=B.ABCERTIF,             
   ABPLACA=B.ABPLACA, ABMODELO=B.ABMODELO          
 FROM CDCSANTANAMicroCredito..CBFIN B (NOLOCK)           
 WHERE B.ABNROPER=crv_PEND.OPNROPER          
 AND   B.ABNRGAR ='01'   -- ALTEREI 13/2/17          
  AND       B.ABCNTRL = (SELECT  MAX(BX.ABCNTRL) FROM CDCSANTANAMicroCredito..CBFIN BX (NOLOCK)           
     WHERE BX.ABNROPER = crv_PEND.OPNROPER AND BX.ABNRGAR = '01') -- ALTEREI 13/2/17          
            
UPDATE  crv_PEND            
SET  O1DESCR = T1.O1DESCR     
FROM CDCSANTANAMicroCredito..TORG1 T1 (NOLOCK)         
WHERE T1.O1CODORG = OPCODORG1    
    
UPDATE  crv_PEND            
SET  O2DESCR = T2.O2DESCR     
FROM CDCSANTANAMicroCredito..TORG2 T2 (NOLOCK)         
WHERE T2.O2CODORG = OPCODORG2    
    
UPDATE  crv_PEND            
SET  A13DESCR = T3.O3DESCR     
FROM CDCSANTANAMicroCredito..TORG3 T3 (NOLOCK)         
WHERE T3.O3CODORG = OPCODORG6 --CODIGO AGENTE     
    
UPDATE  crv_PEND            
SET  O4DESCR = T4.O4DESCR     
FROM CDCSANTANAMicroCredito..TORG4 T4 (NOLOCK)         
WHERE T4.O4CODORG = OPCODORG4    
    
UPDATE  crv_PEND            
SET  O5DESCR = T5.O5DESCR     
FROM CDCSANTANAMicroCredito..TORG5 T5 (NOLOCK)         
WHERE T5.O5CODORG = OPCODORG5    
    
UPDATE  crv_PEND            
SET  O3DESCR = T6.O6DESCR     
FROM CDCSANTANAMicroCredito..TORG6 T6 (NOLOCK)         
WHERE T6.O6CODORG = OPCODORG3    
            
UPDATE CRV_PEND   
SET EPDTLNCBX = E.EPDTLNCBX  
FROM CDCSANTANAMICROCREDITO..EPEND E (NOLOCK)  
WHERE E.EPNROPER = OPNROPER  
            
--SELECT OPNROPER, OPDTBASE, CLNOMECLI, OPCODORG3, O3DESCR, OPCODORG4, O4DESCR, ABPLACA, ABRENAVAM, ABCHASSI, CASE WHEN DIASATRASO <0 then '0' else  DIASATRASO  END dias_atraso FROM PEND151          
SELECT OPCODORG6 AS A13CODORG, A13DESCR, OPNROPER, OPDTBASE, CLNOMECLI, OPCODORG3, O3DESCR, OPCODORG4, O4DESCR, ABPLACA, ABRENAVAM, ABCHASSI,           
  CASE WHEN DIASATRASO <0 then '0' else  DIASATRASO  END diasAtraso, EPDTLNCBX         
FROM crv_PEND (NOLOCK)          
  
             
          
          
          
end 