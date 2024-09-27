    
-- EXEC SCR_DCO_CONF_PAGTO '20170720','20170720'    
-- EXEC SCR_DCO_CONF_PAGTO '20180301','20180302'    
    
-- EXEC SCR_DCO_CONF_PAGTO '20171025','20171025'    
USE sig --[Santana]
GO
/****** Object:  StoredProcedure [dbo].[scr_graf_Rolag_Consolid]    Script Date: 05/01/2017 16:23:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROC CARGA FECHAMENTO  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_DCO_CONF_PAGTO_DET' AND type = 'P')
   DROP PROCEDURE [dbo].SCR_DCO_CONF_PAGTO_DET
GO    
	
CREATE Procedure [dbo].SCR_DCO_CONF_PAGTO_DET (                             
@DT_INI SMALLDATETIME,                    
@DT_FIM SMALLDATETIME,  --, @AGNT   INT SOH INTERNO             
@AGNT  INT,      
@PAGTO INT,      
@FAVOR INT        
 ) AS                        
                
 begin                
                
                
                  
/*                  
DROP   TABLE DCO_CONF_PGTO                
GO                
CREATE TABLE DCO_CONF_PGTO (                
A13CODORG int,                
A13DESCR varchar(20),                
OPNROPER varchar(15),                
OPDTBASE smalldatetime,                
OPCODCLI varchar(15),                
OPNOMEBNF varchar(200),                
                
ABRENAVAM varchar(11),                
DCFAVNOME varchar(200),                
DCVALOR float ,                
ABCHASSI varchar (20),                
                
ABUFLCPL varchar (10),                
EPDTLNC  DATETIME,                
EPHORALNC DATETIME)                
                
ALTER TABLE DCO_CONF_PGTO ADD O3CODORG VARCHAR(10) -- OPERADOR                
ALTER TABLE DCO_CONF_PGTO DROP COLUMN O3CODORG                 
                
ALTER TABLE DCO_CONF_PGTO ADD OPCODORG3 VARCHAR(10)  -- OPERADOR COD                
ALTER TABLE DCO_CONF_PGTO ADD OPERADOR  VARCHAR(200) -- OPERADOR NOM                
                
ALTER TABLE DCO_CONF_PGTO ADD NRRENAVAM VARCHAR(11) --NÚMERO RENAVAM (ABRENAVAM)                
                
ALTER TABLE DCO_CONF_PGTO ADD OPCODORG4 VARCHAR(10)  -- COD LOJA COD                
ALTER TABLE DCO_CONF_PGTO ADD LOJA  VARCHAR(200) -- LOJA NOM                
            
ALTER TABLE DCO_CONF_PGTO ADD CODPROD VARCHAR(7)             
          
ALTER TABLE DCO_CONF_PGTO DROP COLUMN CODPROD          
ALTER TABLE DCO_CONF_PGTO ADD CODPROD  VARCHAR(100)     

ALTER TABLE DCO_CONF_PGTO ADD CODMOV VARCHAR(5)        
                
*/         
TRUNCATE TABLE DCO_CONF_PGTO                  
      
IF @AGNT = '99'      
BEGIN                  
  INSERT INTO  DCO_CONF_PGTO                
  SELECT                
  O3CODORG,O3DESCR,                
  OPNROPER,                
  OPDTBASE,                
  OPCODCLI,                
  OPNOMEBNF,                 
  '' AS ABRENAVAM,                        
  B.DCFAVNOME, B.DCVALOR,                
                
  '' AS ABCHASSI,                
  '' AS ABUFLCPL,                
  ISNULL(EP,OPDTBASE),       
   ISNULL(EPHORALNC,'') , OPCODORG6, '','' AS NRRENAVAM,                 
   OPCODORG4, '',             
   OPCODPROD, B.DCCODMOV         
  FROM (SELECT * FROM CDCSANTANAMICROCREDITO..COPER AS OP1 (NOLOCK)                
    WHERE OPDTBASE BETWEEN  @DT_INI AND @DT_FIM) AS OP                
                
  INNER JOIN CDCSANTANAMICROCREDITO..TORG6 WITH (NOLOCK) ON OPCODORG6 = O6CODORG                
  INNER JOIN CDCSANTANAMICROCREDITO..TORG3 WITH (NOLOCK)ON O6CODORG3 = O3CODORG                
  LEFT  JOIN (SELECT  EPNRPROP,MAX(EPDTLNC +EPHORALNC) AS EP,MAX(EPHORALNC) AS EPHORALNC                
     FROM PROPWEBSMC..EPROP (NOLOCK)                 
     WHERE EPNRDECI = '22' -- PAGAMENTO DA REMESSA                
     GROUP BY  EPNRPROP ) AS P                
   ON EPNRPROP = OPNRPROP                
  LEFT  JOIN (select   DCNROPER, DCFAVNOME, DCVALOR, DCCODMOV                
   FROM  CDCSANTANAMICROCREDITO..CDOCS AS B1  (NOLOCK)                         
     ) AS B                
   ON   OP.OPNROPER=B.DCNROPER            
       
                    
  DELETE FROM  DCO_CONF_PGTO                   
   WHERE RIGHT(OPNROPER,1) IN ('A','B','C','D','E','F','G') -- REMOVE RENEGS                
                
                
  -- 30/10/17 AJUSTA NOME DO OPERADOR                
  UPDATE  DCO_CONF_PGTO                  
   SET  OPERADOR= B.O6DESCR    --  SELECT *                
   FROM CDCSANTANAMicroCredito..TORG6 B (NOLOCK)                 
   WHERE DCO_CONF_PGTO.OPCODORG3 = B.O6CODORG                
      
  -- 10/01/18 AJUSTA NOME DA LOJA           
  UPDATE  DCO_CONF_PGTO                  
   SET  LOJA= B.O4DESCR    --  SELECT *                
   FROM CDCSANTANAMicroCredito..TORG4 B (NOLOCK)                  WHERE DCO_CONF_PGTO.OPCODORG4 = B.O4CODORG                
                      
                
  -- ajusta os dados da Garantia                
  UPDATE  DCO_CONF_PGTO                  
   SET  ABRENAVAM= B.ABPLACA, -- USO O RENAVAM PARA A PLACA                
     ABCHASSI=B.ABCHASSI, ABUFLCPL=B.ABUFLCPL                
   FROM CDCSANTANAMicroCredito..CBFIN B (NOLOCK)                 
   WHERE B.ABNROPER=DCO_CONF_PGTO.OPNROPER                
    AND B.ABCNTRL = ( SELECT     MAX(ABCNTRL) FROM     CDCSANTANAMICROCREDITO..CBFIN B1 (NOLOCK)                
            WHERE B1.ABNROPER = B.ABNROPER ) --  AND B1.ABNRGAR IN ('01','1' ) )                
                
  -- ajusta os dados do CLIENTE NOME E NAO DO BENEFICIARIO                
  UPDATE  DCO_CONF_PGTO                  
   SET  OPNOMEBNF= B.CLNOMECLI                
   FROM CDCSANTANAMicroCredito..CCLIE B (NOLOCK)                 
   WHERE B.CLCODCLI=DCO_CONF_PGTO.OPCODCLI                
                
  -- insere número do renavam                
  UPDATE  DCO_CONF_PGTO                  
   SET  NRRENAVAM= B.ABRENAVAM                
   FROM CDCSANTANAMicroCredito..CBFIN B (NOLOCK)                 
   WHERE B.ABNROPER=DCO_CONF_PGTO.OPNROPER                
    AND B.ABCNTRL = ( SELECT     MAX(ABCNTRL) FROM     CDCSANTANAMICROCREDITO..CBFIN B1 (NOLOCK)                
            WHERE B1.ABNROPER = B.ABNROPER )                
        
  UPDATE DCO_CONF_PGTO              
  SET DCFAVNOME = 'ACESSÓRIOS'              
  FROM CDCSANTANAMICROCREDITO..CDOCS WHERE DCDTEMISS BETWEEN @DT_INI AND @DT_FIM AND DCCODMOV ='505'              
  AND DCNROPER = OPNROPER       
  AND DCO_CONF_PGTO.DCFAVNOME <> 'SCFI-PGFR' AND DCO_CONF_PGTO.DCVALOR <= 900.00 AND DCO_CONF_PGTO.CODMOV = '505'		
	
  		    
  --altera nome favorecido            
  UPDATE DCO_CONF_PGTO              
  SET DCFAVNOME = 'BOLETO'              
  FROM CDCSANTANAMICROCREDITO..CDOCS WHERE DCDTEMISS BETWEEN @DT_INI AND @DT_FIM AND DCCODMOV ='506' AND DCCODMOV <> '501'             
  AND DCNROPER = OPNROPER AND DCO_CONF_PGTO.DCFAVNOME = 'SCFI-PGFR'  AND DCO_CONF_PGTO.CODMOV <> '501' AND DCO_CONF_PGTO.CODMOV = '506'				       
  --AND DCO_CONF_PGTO.DCVALOR = (SELECT MAX(V.DCVALOR) FROM DCO_CONF_PGTO V WHERE V.OPNROPER = DCO_CONF_PGTO.OPNROPER AND DCO_CONF_PGTO.DCFAVNOME = 'SCFI-PGFR')                  
          
            
  UPDATE DCO_CONF_PGTO          
  SET CODPROD = CODPROD + ' - ' + TPDESCR          
  FROM PROPWEBSMC..TPROD (NOLOCK) INNER JOIN DCO_CONF_PGTO (NOLOCK)          
  ON TPCODPRD = CODPROD          
          
                  
  SELECT A13CODORG,A13DESCR,OPNROPER,OPDTBASE,OPCODCLI,OPNOMEBNF,ABRENAVAM,NRRENAVAM,DCFAVNOME,                
  DCVALOR,ABCHASSI,ABUFLCPL,EPDTLNC,                
  --CAST(EPHORALNC AS TIME) AS EPHORALNC                
  ltrim(right(convert(varchar(25), EPHORALNC, 120), 8))  AS EPHORALNC, OPCODORG3, OPERADOR                
  , OPCODORG4, LOJA, CODPROD                
  FROM DCO_CONF_PGTO (NOLOCK)      
  WHERE 1= 0+ CASE WHEN  @PAGTO='0' THEN 1       
   WHEN  @PAGTO = '1' AND A13CODORG = 149 THEN 1      
   WHEN  @PAGTO = '2' AND A13CODORG <> 149 THEN 1      
   END        
  AND 1= 0+ CASE WHEN @FAVOR='0' THEN 1       
   WHEN  @FAVOR = '1' AND DCFAVNOME NOT IN ('BOLETO', 'ACESSÓRIOS', 'SCFI-PGFR') THEN 1      
   WHEN  @FAVOR = '2' AND DCFAVNOME IN ('BOLETO') THEN 1      
   WHEN  @FAVOR = '3' AND DCFAVNOME IN ('SCFI-PGFR') THEN 1      
   WHEN  @FAVOR = '4' AND DCFAVNOME IN ('ACESSÓRIOS') THEN 1      
   END                     
  ORDER BY OPDTBASE, OPNROPER                
      
END      
      
IF @AGNT <> '99'      
BEGIN      
  INSERT INTO  DCO_CONF_PGTO                
  SELECT                
  O3CODORG,O3DESCR,                
  OPNROPER,                
  OPDTBASE,                
  OPCODCLI,                
  OPNOMEBNF,                 
  '' AS ABRENAVAM,                        
  'TOTAL' AS DCFAVNOME,       
  SUM(B.DCVALOR),               
  '' AS ABCHASSI,                
  '' AS ABUFLCPL,                
  ISNULL(EP,OPDTBASE),       
  ISNULL(EPHORALNC,'') , OPCODORG6, '','' AS NRRENAVAM,                 
  OPCODORG4, '',             
  OPCODPROD, B.DCCODMOV          
  FROM (SELECT * FROM CDCSANTANAMICROCREDITO..COPER AS OP1 (NOLOCK) WHERE OPDTBASE BETWEEN @DT_INI AND @DT_FIM) AS OP                
  INNER JOIN CDCSANTANAMICROCREDITO..TORG6 WITH (NOLOCK) ON OPCODORG6 = O6CODORG                
  INNER JOIN CDCSANTANAMICROCREDITO..TORG3 WITH (NOLOCK)ON O6CODORG3 = O3CODORG                
  LEFT  JOIN (SELECT  EPNRPROP,MAX(EPDTLNC +EPHORALNC) AS EP,MAX(EPHORALNC) AS EPHORALNC                
  FROM PROPWEBSMC..EPROP (NOLOCK)                 
  WHERE EPNRDECI = '22' -- PAGAMENTO DA REMESSA                
  GROUP BY  EPNRPROP ) AS P                
  ON EPNRPROP = OPNRPROP                
  LEFT  JOIN (SELECT   DCNROPER, DCFAVNOME, DCVALOR AS DCVALOR, DCCODMOV       
  FROM  CDCSANTANAMICROCREDITO..CDOCS AS B1  (NOLOCK)                         
  ) AS B                
  ON   OP.OPNROPER=B.DCNROPER          
  WHERE O3CODORG = @AGNT      
  GROUP BY O3CODORG, O3DESCR, OPNROPER,OPDTBASE, OPCODCLI, OPNOMEBNF, EP, EPHORALNC,OPCODORG6, OPCODORG4, OPCODPROD,B.DCCODMOV       
  ORDER BY OP.OPNROPER       
       
                    
  DELETE FROM  DCO_CONF_PGTO                   
   WHERE RIGHT(OPNROPER,1) IN ('A','B','C','D') -- REMOVE RENEGS                
                
                
  -- 30/10/17 AJUSTA NOME DO OPERADOR                
  UPDATE  DCO_CONF_PGTO                  
   SET  OPERADOR= B.O6DESCR    --  SELECT *                
   FROM CDCSANTANAMicroCredito..TORG6 B (NOLOCK)                 
   WHERE DCO_CONF_PGTO.OPCODORG3 = B.O6CODORG                
                
  -- 10/01/18 AJUSTA NOME DA LOJA           
  UPDATE  DCO_CONF_PGTO                  
   SET  LOJA= B.O4DESCR    --  SELECT *                
   FROM CDCSANTANAMicroCredito..TORG4 B (NOLOCK)                 
   WHERE DCO_CONF_PGTO.OPCODORG4 = B.O4CODORG                
                      
                
  -- ajusta os dados da Garantia                
  UPDATE  DCO_CONF_PGTO                  
   SET  ABRENAVAM= B.ABPLACA, -- USO O RENAVAM PARA A PLACA                
     ABCHASSI=B.ABCHASSI, ABUFLCPL=B.ABUFLCPL                
   FROM CDCSANTANAMicroCredito..CBFIN B (NOLOCK)                 
   WHERE B.ABNROPER=DCO_CONF_PGTO.OPNROPER                
    AND B.ABCNTRL = ( SELECT     MAX(ABCNTRL) FROM     CDCSANTANAMICROCREDITO..CBFIN B1 (NOLOCK)                
            WHERE B1.ABNROPER = B.ABNROPER ) --  AND B1.ABNRGAR IN ('01','1' ) )                
                
  -- ajusta os dados do CLIENTE NOME E NAO DO BENEFICIARIO                
  UPDATE  DCO_CONF_PGTO                  
   SET  OPNOMEBNF= B.CLNOMECLI                
   FROM CDCSANTANAMicroCredito..CCLIE B (NOLOCK)                 
   WHERE B.CLCODCLI=DCO_CONF_PGTO.OPCODCLI                
                
  -- insere número do renavam                
  UPDATE  DCO_CONF_PGTO                  
   SET  NRRENAVAM= B.ABRENAVAM                
   FROM CDCSANTANAMicroCredito..CBFIN B (NOLOCK)                 
   WHERE B.ABNROPER=DCO_CONF_PGTO.OPNROPER                
    AND B.ABCNTRL = ( SELECT     MAX(ABCNTRL) FROM     CDCSANTANAMICROCREDITO..CBFIN B1 (NOLOCK)                
            WHERE B1.ABNROPER = B.ABNROPER )        
                  
  SELECT A13CODORG,A13DESCR,OPNROPER,OPDTBASE,OPCODCLI,OPNOMEBNF,ABRENAVAM,NRRENAVAM,DCFAVNOME,                
  DCVALOR,ABCHASSI,ABUFLCPL,EPDTLNC,                
  --CAST(EPHORALNC AS TIME) AS EPHORALNC                
  ltrim(right(convert(varchar(25), EPHORALNC, 120), 8))  AS EPHORALNC, OPCODORG3, OPERADOR                
  , OPCODORG4, LOJA, CODPROD                
  FROM DCO_CONF_PGTO (NOLOCK)                          
END                
                
                
end 