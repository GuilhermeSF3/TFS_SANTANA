USE SIG
GO

-- PROC CARGA [SCR_CETIP_RANKING_DADOS]  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CETIP_RANKING_DADOS' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CETIP_RANKING_DADOS] 
GO

/****** Object:  StoredProcedure [dbo].[SCR_CETIP_RANKING_DADOS]    Script Date: 21/10/2015 18:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SCR_CETIP_RANKING_DADOS]   
AS   
BEGIN  
 SELECT   
  MesReferencia  
  ,CNPJ  
  ,Classificacao  
  ,RazaoSocial  
  ,NomeFantasia  
  ,CNAE  
  ,Endereco  
  ,Complemento  
  ,Número  
  ,Bairro  
  ,UF  
  ,Cidade  
  ,CEP  
  ,QuantidadedeBancos  
  ,RankingCliente  
 FROM  
   Cetip_Ranking (NOLOCK)  
 WHERE  
  MesReferencia = (SELECT MAX(MesReferencia) FROM Cetip_Ranking (NOLOCK) )  
END  
  