USE sig 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_PendFORMALIZ_TTL_PEN' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_PendFORMALIZ_TTL_PEN]
GO   
    
CREATE Procedure [dbo].SCR_PendFORMALIZ_TTL_PEN (                                        
         @DT_INI SMALLDATETIME,                            
         @DT_FIM SMALLDATETIME,                           
         @AGNT   INT ) AS                          
                        
 begin                        
                       
SELECT COUNT (*) AS TOTAL FROM DCO_PEND_FORMALIZ_ANALITICO
WHERE 1= 0+ CASE WHEN  @AGNT='99' THEN 1 
    WHEN  @AGNT<>'99' AND A13CODORG = @AGNT  THEN 1            
    ELSE  0   END  
AND SITUACAO = 'INT' AND MOTIVO <> ''
                      
END 