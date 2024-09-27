Use sig
GO

IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CRV_CATIVA_ANALI_AGENTE' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CRV_CATIVA_ANALI_AGENTE]
GO

CREATE PROCEDURE [dbo].SCR_CRV_CATIVA_ANALI_AGENTE (    
@AGNT   INT ) AS      
    
 begin    
    
    
SELECT (CASE WHEN O3ATIVA IN ('S','A') THEN 'S' ELSE 'N' END) AS ATIVO  from  CDCSANTANAMicroCredito..TORG3  (nolock)     
WHERE O3CODORG =@AGNT      
    
END 