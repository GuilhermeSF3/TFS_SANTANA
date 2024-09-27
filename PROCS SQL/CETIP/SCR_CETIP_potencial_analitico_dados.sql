USE SIG
GO

IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CETIP_POTENCIAL_ANALITICO_DADOS' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CETIP_POTENCIAL_ANALITICO_DADOS] 
GO

-- MENSAL
-- ***************************************
CREATE PROCEDURE [dbo].[SCR_CETIP_POTENCIAL_ANALITICO_DADOS]   
AS  
BEGIN  
  
SELECT   
 MesReferencia  
 ,CNPJ  
 ,Categoria  
 ,Segmento  
 ,Consorcio  
 ,TipoPessoa  
 ,Quantidade  
 ,Volume  
FROM   
 Cetip_Potencial_Analitico  (NOLOCK)  
Where  
 MesReferencia = (SELECT MAX(MesReferencia) FROM Cetip_Potencial_Analitico  (NOLOCK))  
   
END  
  
GO
