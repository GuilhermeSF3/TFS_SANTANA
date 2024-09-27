USE sig --[Santana]
GO
/****** Object:  StoredProcedure [dbo].[scr_graf_Rolag_Consolid]    Script Date: 05/01/2017 16:23:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROC CARGA FECHAMENTO  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CRV_ATRASO_PROMOTORA_TTL' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CRV_ATRASO_PROMOTORA_TTL] 
GO   
CREATE PROCEDURE SCR_CRV_ATRASO_PROMOTORA_TTL(  
@DT_DE SMALLDATETIME,  
@DT_ATE SMALLDATETIME,  
@AGENTE VARCHAR(10)) AS  
  
BEGIN  
  
SELECT COUNT(*) AS TOTAL FROM CRV_PEND WHERE DIASATRASO > 60
  
END