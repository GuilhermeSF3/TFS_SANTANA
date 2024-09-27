-- SCR_CETIP_GERARARQUIVO '2015-08-11' , '2015-08-17'

-- SCR_CETIP_GERARARQUIVO '2015-06-07' , '2015-06-12'
USE SIG
GO

-- EXEC [SCR_CETIP_GERARARQUIVO] '20150601' , '20150615'
-- PROC CARGA DRE_RESULTADO_ANALITICO  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_CETIP_GERARARQUIVO' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_CETIP_GERARARQUIVO] 
GO

CREATE Procedure [dbo].[SCR_CETIP_GERARARQUIVO] (@data_de as datetime,@data_ate as datetime)       
AS      
BEGIN      
      
SELECT DISTINCT       
      
 REPLACE(REPLACE(PROPWEBSMC.dbo.CCLIP.CLTPFJ, 'F', '1'), 'J', '2') AS TipoDocumento,       
  RIGHT('000000' +       
  CASE WHEN len(ltrim(rtrim(clcgc))) = 14 THEN       
    LTRIM(substring(clcgc, 1, 3) + substring(clcgc, 5, 3) + substring(clcgc, 9, 3))      
   ELSE LTRIM(RIGHT(substring(clcgc, 1, 2) + substring(clcgc, 4, 3) + substring(clcgc, 8, 3), 9))      
   END  -- DIGITOS      
     + RIGHT(LTRIM(RTRIM(PROPWEBSMC.dbo.CCLIP.CLCGC)), 2) ,14) AS Documento,       
      
  RIGHT('00000000000000' +       
  CASE WHEN len(ltrim(rtrim(O4CPFCGC))) = 14 THEN       
     LTRIM(substring(O4CPFCGC, 1, 3) + substring(O4CPFCGC, 5, 3) + substring(O4CPFCGC, 9, 3) )      
   ELSE LTRIM(RIGHT(substring(O4CPFCGC, 1, 2) + substring(O4CPFCGC, 4, 3) + substring(O4CPFCGC, 8, 3), 9))      
    END       
  + CASE WHEN len(ltrim(rtrim(O4CPFCGC))) = 14 THEN ''       
   ELSE substring(O4CPFCGC, 12, 4) END       
  + RIGHT(LTRIM(RTRIM(CDCSANTANAMicroCredito.dbo.torg4.O4CPFCGC)), 2) ,14) AS CNPJLoja,       
   
 RIGHT('00000000000'+ LTRIM(      
  CONVERT(VARCHAR(15),CONVERT(NUMERIC(15), ISNULL(PROPWEBSMC.dbo.CPROP.PPVLRFIN, 0) * 100)) ),11 ) AS ValorAprovado,       
 REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(19), CONVERT(DATETIME, PROPWEBSMC.dbo.CPROP.PPDTBASE, 112), 126), '-', ''), 'T', ''), ':', '') AS DataAprovacao      
      
FROM               
 PROPWEBSMC.dbo.CPROP INNER JOIN      
 PROPWEBSMC.dbo.CMOVP ON PROPWEBSMC.dbo.CPROP.PPNRPROP = PROPWEBSMC.dbo.CMOVP.MPNRPROP INNER JOIN      
 PROPWEBSMC.dbo.CBFIP ON PROPWEBSMC.dbo.CPROP.PPNRPROP = PROPWEBSMC.dbo.CBFIP.ABNRPROP INNER JOIN      
 PROPWEBSMC.dbo.CCLIP ON PROPWEBSMC.dbo.CPROP.PPCODCLI = PROPWEBSMC.dbo.CCLIP.CLCODCLI INNER JOIN      
 CDCSANTANAMicroCredito.dbo.torg6 AS T3 ON PROPWEBSMC.dbo.CPROP.PPCODORG6 = T3.O6CODORG INNER JOIN      
 CDCSANTANAMicroCredito.dbo.torg4 ON PROPWEBSMC.dbo.CPROP.PPCODORG4 = CDCSANTANAMicroCredito.dbo.torg4.O4CODORG INNER JOIN      
 CDCSANTANAMicroCredito.dbo.TORG3 AS T1O3 ON T3.O6CODORG3 = T1O3.O3CODORG      
WHERE      
 (PROPWEBSMC.dbo.CPROP.PPDTBASE >= @data_de) AND       
 (PROPWEBSMC.dbo.CPROP.PPDTBASE <= @data_ate) AND       
 (T1O3.O3CODORG <> '1')      
AND  MPSIT IN ('AND','APR','REP','INT')      
END