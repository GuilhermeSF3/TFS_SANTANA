USE SIG
GO

-- PROC CARGA [SCR_CETIP_RANKING_LIMPAR]  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CETIP_RANKING_LIMPAR' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CETIP_RANKING_LIMPAR] 
GO

/****** Object:  StoredProcedure [dbo].[SCR_CETIP_RANKING_LIMPAR]    Script Date: 21/10/2015 18:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SCR_CETIP_RANKING_LIMPAR]    Script Date: 21/10/2015 18:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SCR_CETIP_RANKING_LIMPAR] (@MesReferencia varchar(6))  
AS   
BEGIN  
 DELETE FROM Cetip_Ranking WHERE MesReferencia = @MesReferencia  
END  
  
GO
