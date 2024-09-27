USE sig --[Santana]
GO
/****** Object:  StoredProcedure [dbo].[scr_graf_Rolag_Consolid]    Script Date: 05/01/2017 16:23:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROC CARGA FECHAMENTO  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CRV_pend_PROMOTORA' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CRV_pend_PROMOTORA] 
GO    
CREATE Procedure [dbo].SCR_CRV_pend_PROMOTORA (      
 @DataContrato as datetime,      
@DT_REF SMALLDATETIME,              
@DT_INI SMALLDATETIME,          
@DT_FIM SMALLDATETIME,         
@AGNT   INT ) AS        
      
      
 begin      
    SELECT  COUNT(*) as CONTRATO       
  FROM CRV_PEND (NOLOCK)    
END 