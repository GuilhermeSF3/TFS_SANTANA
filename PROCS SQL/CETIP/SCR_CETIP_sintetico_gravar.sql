USE SIG
GO

-- EXEC [SCR_cetip_cruzamento] '20150701' , '20150731' ,''
-- EXEC [SCR_cetip_cruzamento] '20150601' , '20150615' ,''
-- PROC CARGA [SCR_CETIP_POTENCIAL_SINTETICO_GRAVAR]  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CETIP_POTENCIAL_SINTETICO_GRAVAR' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CETIP_POTENCIAL_SINTETICO_GRAVAR] 
GO

/****** Object:  StoredProcedure [dbo].[SCR_CETIP_POTENCIAL_SINTETICO_GRAVAR]    Script Date: 21/10/2015 18:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SCR_CETIP_POTENCIAL_SINTETICO_GRAVAR] (  
 @MesReferencia varchar(6),  
 @CNPJ varchar(14),  
 @Classificacao varchar(1),  
 @RazaoSocial varchar(150),  
 @NomeFantasia varchar(55),  
 @CNAE varchar(174),  
 @Endereco varchar(64),  
 @Complemento varchar(137),  
 @Numero varchar(10),  
 @Bairro varchar(50),  
 @UF varchar(2),  
 @Cidade varchar(32),  
 @CEP varchar(8),  
 @Quantidade int,  
 @Volume decimal(18,2),  
 @MarketShareQuant int,  
 @MarketShareValor decimal(18,2),  
 @MarketShareAcimaQuant int,  
 @MarketShareAcimaValor decimal(18,2),  
 @MarketShareAbaixoQuant int,  
 @MarketShareAbaxoValor decimal(18,2)  
)  
AS  
BEGIN  
INSERT INTO CETIP_POTENCIAL_SINTETICO  
 (MesReferencia  
 ,CNPJ  
 ,Classificacao  
 ,RazaoSocial  
 ,NomeFantasia  
 ,CNAE  
 ,Endereco  
 ,Complemento  
 ,Numero  
 ,Bairro  
 ,UF  
 ,Cidade  
 ,CEP  
 ,Quantidade  
 ,Volume  
 ,MarketShareQuant  
 ,MarketShareValor  
 ,MarketShareAcimaQuant  
 ,MarketShareAcimaValor  
 ,MarketShareAbaixoQuant  
 ,MarketShareAbaxoValor)  
VALUES  
 (@MesReferencia,  
 @CNPJ,  
 @Classificacao,  
 @RazaoSocial,  
 @NomeFantasia,  
 @CNAE,  
 @Endereco,  
 @Complemento,  
 @Numero,  
 @Bairro,  
 @UF,  
 @Cidade,  
 @CEP,  
 @Quantidade,  
 @Volume,  
 @MarketShareQuant,  
 @MarketShareValor,  
 @MarketShareAcimaQuant,  
 @MarketShareAcimaValor,  
 @MarketShareAbaixoQuant,  
 @MarketShareAbaxoValor  
)  
END  
  

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
