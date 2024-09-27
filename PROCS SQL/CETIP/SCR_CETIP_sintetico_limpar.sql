USE SIG
GO

-- EXEC [SCR_cetip_cruzamento] '20150701' , '20150731' ,''
-- EXEC [SCR_cetip_cruzamento] '20150601' , '20150615' ,''
-- PROC CARGA [SCR_CETIP_POTENCIAL_SINTETICO_GRAVAR]  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CETIP_POTENCIAL_SINTETICO_LIMPAR' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CETIP_POTENCIAL_SINTETICO_LIMPAR] 
GO

/****** Object:  StoredProcedure [dbo].[SCR_CETIP_POTENCIAL_SINTETICO_LIMPAR]    Script Date: 21/10/2015 18:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SCR_CETIP_POTENCIAL_SINTETICO_LIMPAR] (@MesReferencia varchar(6))  
AS  
BEGIN  
  
DELETE FROM CETIP_POTENCIAL_SINTETICO WHERE MesReferencia = @MesReferencia  
   
END  
  

GO
/****** Object:  StoredProcedure [dbo].[SCR_CETIP_POTENCIAL_SINTETICO_LIMPAR]    Script Date: 21/10/2015 18:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

