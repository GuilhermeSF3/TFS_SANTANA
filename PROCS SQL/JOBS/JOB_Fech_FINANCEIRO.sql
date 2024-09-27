USE SIG

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- EXEC [JOB_Fech_Financeiro] 

-- PROC CALCULO  [JOB_Fech_Financeiro] ********************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'JOB_Fech_Financeiro' AND type = 'P')
   DROP PROCEDURE [dbo].[JOB_Fech_Financeiro]
GO


CREATE PROCEDURE [dbo].[JOB_Fech_Financeiro] --(@data_ref AS DATETIME) 
AS
BEGIN

-- RODA DE 1				-- ATEH O DIA 10 do mes
IF datepart(dd,GETDATE()) < 10
BEGIN


	DECLARE @M1  AS DATETIME	-- DT REF	

		-- MES  DE FECHAMENTO
		SET		@M1 = GETDATE()
		SET		@M1 = CONVERT(CHAR(4),DATEPART(YEAR,@M1))+
						RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M1))),2)+'01'
		SET		@M1 = DATEADD(DD,-1,@M1)

-- Select * from  TCDI (nolock) 
--select * from fin_res_oper (nolock) where dt_ref='20170930'

	-- VERIFICA SE TEM CDI CADASTRADO E SE JAH NAO FOI PROCESSADO FECHAMENTO
	IF	NOT EXISTS(SELECT * FROM FIN_RES_OPER (NOLOCK) WHERE DT_REF	= @M1) 
		AND EXISTS(Select * from  TCDI (nolock) WHERE DT_DE = @M1)
		-- DE ATE		REF   CDI 
		-- PROCESSA FECHAMENTO FINANCEIRO 40 MIN AUTOMAT
		-- quebra por ano
		exec FIN_RESUMO_CALC '2014-01-01','2014-12-31',@M1,0.0
		-- PROCESSA FECHAMENTO FINANCEIRO 40 MIN AUTOMAT
		exec FIN_RESUMO_CALC '2015-01-01','2015-12-31',@M1,0.0
		-- PROCESSA FECHAMENTO FINANCEIRO 40 MIN AUTOMAT
		exec FIN_RESUMO_CALC '2016-01-01','2016-12-31',@M1,0.0
		-- PROCESSA FECHAMENTO FINANCEIRO 40 MIN AUTOMAT
		exec FIN_RESUMO_CALC '2017-01-01',@M1,@M1,0.0
	END
END

GO
