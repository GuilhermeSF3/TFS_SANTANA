USE SIG
GO

/****** Object:  StoredProcedure [dbo].[SCR_CETIP_POTENCIAL_SINTETICO_DADOS]    Script Date: 21/10/2015 18:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- remover
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CETIP_POTENCIAL_SINTETICO_DADOS' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CETIP_POTENCIAL_SINTETICO_DADOS] 
GO

CREATE PROCEDURE [dbo].[SCR_CETIP_POTENCIAL_SINTETICO_DADOS]  
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
 ,MarketShareAbaxoValor  
FROM   
 Cetip_Potencial_Sintetico (NOLOCK)  
WHERE  
   MesReferencia = (SELECT MAX(MesReferencia) FROM Cetip_Potencial_Sintetico (NOLOCK) )  
          
END  
  
  