USE SIG
GO

-- PROC CARGA DRE_RESULTADO_ANALITICO  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CETIP_UPLOAD' AND type = 'P')
   DROP PROCEDURE [dbo].SCR_CETIP_UPLOAD 
GO


CREATE Procedure [dbo].[SCR_CETIP_UPLOAD] (  
   @data_ref as datetime,  
   @TipoDocumento  as varchar(1),  
            @Documento as varchar(14),  
            @CNPJLoja as numeric(14, 0),  
            @ValorAprovado as numeric(18,2),  
            @DataAprovacao as datetime,  
            @DataInclusao as datetime,  
            @DataContrato as datetime,  
            @Marca as varchar(50),  
            @AnoModelo as numeric(5, 0),  
            @AnoFabricacao as numeric(5, 0),  
            @PrazoOperacao as numeric(5, 0),  
            @CodigoFIPE as varchar(10),  
            @ValorFIPE as decimal(18, 2),  
            @ValorFaixaAprovado as numeric(5, 0),  
            @CNPJAgente as numeric(15,0),  
            @NomeAgente as varchar(50),  
            @PagoLoja as varchar(2)) AS  
  
 begin  
  INSERT  RT_CONVERSAO   
   (TIP_REGISTRO,  
   TIP_DOCUMENTO,  
   NUM_CPF,  
   CNPJ_LOJA,  
   VLR_APROV,  
   DT_APROV,  
   DT_INCLU,  
   DT_CONTRATO,  
   MARCA,  
   ANO_MODELO,  
   ANO_FABRIC,  
   OP_PRAZO,  
   COD_FIPE,  
   VLR_FIPE,  
   VLR_FAIXA,  
   CNPJ_AGENTE,  
   NOM_AGENTE,  
   PAGO_LOJA)  
  select   
   'D',  
   @TipoDocumento,  
            @Documento,  
            @CNPJLoja,  
            @ValorAprovado,  
            @DataAprovacao,  
            @DataInclusao,  
            @DataContrato,  
            @Marca,  
            @AnoModelo,  
            @AnoFabricacao,  
            @PrazoOperacao,  
            @CodigoFIPE,  
            @ValorFIPE,  
            @ValorFaixaAprovado,  
            @CNPJAgente,  
            @NomeAgente,  
            @PagoLoja  
WHERE   
      @DataContrato <> '1753-01-01 00:00:00.000'  
 end  