USE sig --[Santana]
GO
/****** Object:  StoredProcedure [dbo].[scr_graf_Rolag_Consolid]    Script Date: 05/01/2017 16:23:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROC CARGA FECHAMENTO  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CRV_ttl_EQUIPE' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CRV_ttl_EQUIPE] 
GO  
CREATE Procedure [dbo].SCR_CRV_ttl_EQUIPE (          
 @DataContrato as datetime,          
@DT_REF SMALLDATETIME,                  
@DT_INI SMALLDATETIME,              
@DT_FIM SMALLDATETIME,             
@AGNT   INT,
@OPERADOR VARCHAR(10)) AS            
          
       
 begin          
          
     
SELECT  COUNT(*) as CONTRATO           
FROM CDCSANTANAMicroCredito..TORG3 T1O3 (NOLOCK),           
CDCSANTANAMicroCredito..COPER  (NOLOCK),           
CDCSANTANAMicroCredito..TORG6 T3 (NOLOCK)           
WHERE OPDTLIQ IS NULL AND T3.O6CODORG3 = T1O3.O3CODORG AND  OPCODORG6   = T3.O6CODORG AND OPDTLIQ IS NULL           
--AND OPDTBASE <='" + Mid(dt_CONT, 7, 4) & "-" & Mid(dt_CONT, 4, 2) & "-" & Mid(dt_CONT, 1, 2) & "'           
AND OPDTBASE  between @DT_INI AND @DT_FIM           
--   And T1O3.A13CODORG = @AGNT          
AND 1= 0+ CASE WHEN  @AGNT='99' THEN 1 -- TODOS OS AGENTES          
    WHEN  @AGNT<>'99' AND T1O3.O3CODORG = @AGNT  THEN 1          
    ELSE  0   END  
AND 1= 0+ CASE WHEN  @OPERADOR='99' THEN 1 -- TODOS OS AGENTES            
    WHEN  @OPERADOR<>'99' AND O6CODORG = @OPERADOR  THEN 1            
    ELSE  0   END    		          
and T1O3.o3ativa in ('S','A')    
AND T1O3.O3DESCR LIKE 'SHOP%' AND T1O3.O3DESCR <> 'SHOPCRED'
          
END 