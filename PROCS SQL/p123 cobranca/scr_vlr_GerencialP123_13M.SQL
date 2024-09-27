USE SIG
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROC CALCULO  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'scr_VLR_GERENCIALP123_13M' AND type = 'P')
   DROP PROCEDURE [dbo].[scr_VLR_GERENCIALP123_13M]
GO

-- [scr_VLR_GERENCIALP123_13M] '20160531'
-- [scr_VLR_GERENCIALP123_13M] '20160131'
-- [scr_VLR_GERENCIALP123_13M] '20160229'
CREATE Procedure [dbo].[scr_VLR_GERENCIALP123_13M] (	@data_ref as datetime) AS

			begin


/*
	--  14 MESES
	DECLARE @M1  AS DATETIME
	DECLARE @M2  AS DATETIME
	DECLARE @M3  AS DATETIME
	DECLARE @M4  AS DATETIME
	DECLARE @M5  AS DATETIME
	DECLARE @M6  AS DATETIME
	DECLARE @M7  AS DATETIME
	DECLARE @M8  AS DATETIME
	DECLARE @M9  AS DATETIME
	DECLARE @M10  AS DATETIME
	DECLARE @M11  AS DATETIME
	DECLARE @M12  AS DATETIME
	DECLARE @M13  AS DATETIME
	DECLARE @M14  AS DATETIME	-- DT REF
	DECLARE @M15  AS DATETIME	-- MEDIA

	--				declare @data_ref datetime set @data_ref = '20150228'
		-- MES  DE FECHAMENTO
		SET		@M14 = @data_ref

		-- MES ANTERIOR AO DO FECHAMENTO
		SET		@M13 = CONVERT(CHAR(4),DATEPART(YEAR,@M14))+
						RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M14))),2)+'01'
		SET		@M13 = DATEADD(DD,-1,@M13)
		--		SELECT @M1, @m2

		set		@M12 = CONVERT(CHAR(4),DATEPART(YEAR,@M13))+
					RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M13))),2)+'01'
		SET		@M12=DATEADD(DD,-1,@M12)
		--		SELECT @M1, @M2,@M3

		set  @M11 = CONVERT(CHAR(4),DATEPART(YEAR,@M12))+
					RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M12))),2)+'01'
		SET	 @M11=DATEADD(DD,-1,@M11)
		--		  SELECT @M4

	set  @M10 = CONVERT(CHAR(4),DATEPART(YEAR,@M11))+
				RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M11))),2)+'01'
	SET	 @M10 = DATEADD(DD,-1,@M10)
	--		 	SELECT @M5

	set  @M9 = CONVERT(CHAR(4),DATEPART(YEAR,@M10))+
				RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M10))),2)+'01'
	SET	 @M9 = DATEADD(DD,-1,@M9)

	set  @M8 = CONVERT(CHAR(4),DATEPART(YEAR,@M9))+
				RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M9))),2)+'01'
	SET	 @M8 = DATEADD(DD,-1,@M8)

	set  @M7 = CONVERT(CHAR(4),DATEPART(YEAR,@M8))+
				RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M8))),2)+'01'
	SET	 @M7 = DATEADD(DD,-1,@M7)

	set  @M6 = CONVERT(CHAR(4),DATEPART(YEAR,@M7))+
				RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M7))),2)+'01'
	SET	 @M6 = DATEADD(DD,-1,@M6)

	set		@M5 = CONVERT(CHAR(4),DATEPART(YEAR,@M6))+
				RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M6))),2)+'01'
	SET		@M5 = DATEADD(DD,-1,@M5)
	--			SELECT	@M10


	set		@M4 = CONVERT(CHAR(4),DATEPART(YEAR,@M5))+
				RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M5))),2)+'01'
	SET		@M4 = DATEADD(DD,-1,@M4)
	--			SELECT	@M11 

	set		@M3 = CONVERT(CHAR(4),DATEPART(YEAR,@M4))+
				RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M4))),2)+'01'
	SET		@M3 = DATEADD(DD,-1,@M3)

	set		@M2 = CONVERT(CHAR(4),DATEPART(YEAR,@M3))+
				RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M3))),2)+'01'
	SET		@M2 = DATEADD(DD,-1,@M2)

	set		@M1 = CONVERT(CHAR(4),DATEPART(YEAR,@M2))+
				RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MONTH,@M2))),2)+'01'
	SET		@M1 = DATEADD(DD,-1,@M1)
		-- 		SELECT @M1, @M2,@M3 , @M4, @M5,@M6,@M7,@m8,@M9,@M10,@M11,@M12,@M13,@M14


		--		VARIAVEIS PARA COMPOR OS 13 MESES 
		DECLARE @columns VARCHAR(200)
		SET		@columns = '['+CONVERT(VARCHAR(8),@M1,112)+']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M2 ,112)  + ']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M3 ,112)  + ']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M4 ,112)  + ']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M5 ,112)  + ']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M6 ,112)  + ']'SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M7 ,112)  + ']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M8 ,112)  + ']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M9 ,112)  + ']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M10 ,112)  + ']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M11 ,112)  + ']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M12 ,112)  + ']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M13 ,112)  + ']'
		SET     @columns = @columns + ',[' + CONVERT(VARCHAR(8),@M14 ,112)  + ']'


		--		SELECT  @columns

-- CALCULAR 14 MESES		
-- ************************************************** CARREGA AUXILIAR
 EXEC [PRC_P123GER_SCORE_CALC1M] @M1, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M2, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M3, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M4, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M5, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M6, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M7, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M8, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M9, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M10, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M11, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M12, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M13, @data_ref
 EXEC [PRC_P123GER_SCORE_CALC1M] @M14, @data_ref



-- RESUMO   14 MESES		
-- ************************************************** CARREGA AUXILIAR
EXEC [PRC_Score_13M_CALC]  @data_ref
*/

--SELECT	* FROM AUX_SCORE_13M (nolock) 
--WHERE DT_REF='20160531'


	SELECT			DESCR,
					ISNULL(m15,0.0) AS m15,
					ISNULL(m14,0.0) AS m14,
					ISNULL(m13,0.0) AS m13,
					ISNULL(m12,0.0) AS m12,
					ISNULL(m11,0.0) AS m11,
					ISNULL(m10,0.0) AS m10,
					ISNULL(m9,0.0) AS m9,
					ISNULL(m8,0.0) AS m8,
					ISNULL(m7,0.0) AS m7,
					ISNULL(m6,0.0) AS m6,
					ISNULL(m5,0.0) AS m5,
					ISNULL(m4,0.0) AS m4,
					ISNULL(m3,0.0) AS m3,
					ISNULL(m2,0.0) AS m2,
					ISNULL(m1,0.0) AS m1,
				
					ORDEM
	FROM SCORE_13M (NOLOCK) 
	WHERE DT_REF = @data_ref
	ORDER BY ORDEM



-- SELECT * FROM AUX_SCORE_13M (NOLOCK)
-- SELECT * FROM SCORE_13M (NOLOCK)

end