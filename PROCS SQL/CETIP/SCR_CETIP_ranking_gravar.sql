USE SIG
GO

-- PROC CARGA [SCR_CETIP_RANKING_GRAVAR]  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CETIP_RANKING_GRAVAR' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CETIP_RANKING_GRAVAR] 
GO

/****** Object:  StoredProcedure [dbo].[SCR_CETIP_RANKING_GRAVAR]    Script Date: 21/10/2015 18:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SCR_CETIP_RANKING_GRAVAR] (  
 @MesReferencia  varchar(6),  
 @CNPJ varchar(14),  
 @Classificacao char(1),  
 @RazaoSocial varchar(150),  
 @NomeFantasia varchar(55),  
 @CNAE varchar(174),  
 @Endereco varchar(64),  
 @Complemento varchar(137),  
 @Número varchar(10),  
 @Bairro varchar(50),  
 @UF varchar(2),  
 @Cidade varchar(32),  
 @CEP varchar(8),  
 @QuantidadedeBancos int,  
 @RankingCliente int)  
  
AS   
BEGIN  
 INSERT INTO Cetip_Ranking  
  (MesReferencia  
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
  ,RankingCliente)  
 VALUES  
  (@MesReferencia,  
  @CNPJ,  
  @Classificacao,  
  @RazaoSocial,  
  @NomeFantasia,  
  @CNAE,  
  @Endereco,  
  @Complemento,  
  @Número,  
  @Bairro,  
  @UF,  
  @Cidade,  
  @CEP,  
  @QuantidadedeBancos,  
  @RankingCliente)  
END  
  