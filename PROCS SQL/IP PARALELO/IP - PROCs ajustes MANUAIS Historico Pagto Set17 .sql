use sig
--  EXEC IP_consulta '20170630' ,'612259799' ,'7'
--  EXEC IP_consulta '20170731' ,'612259799' ,'7'
-- PROC consulta no IP  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'IP_consulta' AND type = 'P')
   DROP PROCEDURE [dbo].[IP_consulta]
GO


CREATE  PROCEDURE [dbo].[IP_consulta] (@DTREF  as datetime,@contr as varchar(20),@PARC as varchar(20) )
AS
BEGIN      
DECLARE @DTINI DATETIME
SET @DTINI = CONVERT(CHAR(4),DATEPART(YYYY,@DTREF))+
			RIGHT(RTRIM('00'+CONVERT(CHAR(2),DATEPART(MM,@DTREF))),2)+ '01'

-- select * from performance_historico (nolock) where data_ref between '20170801' and '20170831' and contrato='612192634' and parcela=29
-- , @parc as INT,@cobrad as varchar(10), @dtpg as datetime
IF @PARC IN ('','1')
	SELECT 	data_ref, contrato, 	parcela  , VLR_PARCELA,codcobradora,cobradora,
			data_pagamento , recebido , valorRec
	FROM    Performance_historico  (NOLOCK)
	WHERE   Data_ref BETWEEN @DTINI AND @DTREF AND contrato = RTRIM(@contr)
ELSE
	SELECT  data_ref, contrato, 	parcela  , VLR_PARCELA,codcobradora,cobradora,
			data_pagamento , recebido , valorRec
	FROM    Performance_Historico  (NOLOCK)
	WHERE   Data_ref BETWEEN @DTINI AND @DTREF AND contrato = RTRIM(@contr)
		AND Parcela = CONVERT(INT,RTRIM(@PARC))

END
GO


-- PROC pgto de parcela forcada no IP  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'IP_Pagto_manual' AND type = 'P')
   DROP PROCEDURE [dbo].[IP_Pagto_manual]
GO


CREATE  PROCEDURE [dbo].[IP_Pagto_manual] (@DTREF  as datetime,@contr as varchar(20), @parc as INT,@cobrad as varchar(10), @dtpg as datetime )
AS
BEGIN      

-- select * from performance_historico (nolock) where data_ref between '20170801' and '20170831' and contrato='612192634' and parcela=29

update  performance_historico set data_pagamento = @DTPG , recebido = '1' , valorRec=VLR_PARCELA
 where  data_ref = @DTREF and  contrato = @contr and  codcobradora=@cobrad 
	and parcela =CONVERT(INT,RTRIM(@PARC))

END
GO


-- PROC REABERTURA de parcela AJUSTE DA BAIXA no IP  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'IP_REABRE_manual' AND type = 'P')
   DROP PROCEDURE [dbo].[IP_REABRE_manual]
GO


CREATE  PROCEDURE [dbo].[IP_REABRE_manual] (@DTREF  as datetime,@contr as varchar(20), @parc as INT,@cobrad as varchar(10), @dtpg as datetime )
AS
BEGIN      

-- select * from performance_historico (nolock) where data_ref between '20170801' and '20170831' and contrato='612192634' and parcela=29
-- limpar da COBRADORA
--update  performance_historico set data_pagamento = NULL , recebido = '' , valorRec=0.0
-- where  data_ref = '20170807' and  contrato = ('616002120') and parcela in (6,7,8,9) and  codcobradora=699 -- rennov

update  performance_historico set data_pagamento = NULL , recebido = '' , valorRec=0.0
 where  data_ref = @DTREF and  contrato = @contr and  codcobradora=@cobrad -- icr
	and parcela =CONVERT(INT,RTRIM(@PARC))

END
GO


-- PROC REABERTURA de parcela AJUSTE DA BAIXA no IP  **********************************************************************************************************************************************************************************
IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'IP_COPIAR_manual' AND type = 'P')
   DROP PROCEDURE [dbo].[IP_COPIAR_manual]
GO


CREATE  PROCEDURE [dbo].[IP_COPIAR_manual] (@DTREF  as datetime,@contr as varchar(20), @parc as INT,@cobrad as varchar(10), @DTPARA as datetime, @PARAcobrad as varchar(10) )
AS
BEGIN      

/*
select * from performance_historico (nolock) where data_ref between '20170801' and '20170831' 
and contrato='612177411'
612177411	31 A 34  ; 	35  pgto 2017-08-28 00:00:00  reneg vcto 2017-08-25 00:00:00  3 d atraso
select * FROM	 	 CDCSANTANAMicrocredito..cPARC O  (NOLOCK) where panroper='612177411' and panrparc=35

CONTRATO	PROPOSTA	AGENTE	CODPROMOTORA	PROMOTORA	LOJA	NOME_CLIENTE	CARTEIRA	MODALIDADE	DATA_CONTRATO	PLANO	PARCELA	PRI_VCTO	ATRASO	Rating	SituacaoBacen	VENCIMENTO	VLR_PARCELA	QTD_PARC_ATRASO	VALOR_RISCO	VALOR_FINANCIADO	TAC	IOC	CPF_CNPJ	RG	ENDERECO	BAIRRO	CIDADE	CEP	UF	TELEFONE	CELULAR	FONE_COMERCIAL	LOCAL_TRABALHO	REFERENCIA1	REFERENCIA2	VALORBEM	MARCA	MODELO	ANO_FABRIC	ANO_MODELO	COR	PLACA	COBRADORA	SERASA	OCORRENCIA	REGRA	BLOQUEIO	DESBLOQUEIO	FAIXA	GRUPO	RENAVAM	CHASSI	QTDPAGAR	PROFISSAO	CARGO	COD_AGENTE	PARCELAS_PAGAS	FAIXA_RATING	SALDOINSC	DATA_REF	DATA_PAGAMENTO	RECEBIDO	VALORREC	CODCOBRADORA
612177411	941436230	NULL	001403	PESSOA - RENAN	006316-JOSE EDUARDO PESSOA DA SILVA ME	THIAGO FONTES GASPAR	61	612	2014-09-25 00:00:00	42	034	2014-10-25 00:00:00	98	E	E	2017-07-25 00:00:00	246,69	0	2595	3800	0	80,67	32497958874	44.252.730-5	R FREDERICO RODRIGUES DE GODOI, 81	JARDIM ZAIRA	MAUA	09321470	SP	2375-9144	94172-0061	4427-4037	GPA CIA BRAS DE DISTRIBUIÇÃO	4511-9444	4577-6619	8575	GM - Chevrolet	CORSA - WIND 1.0 MPF	1996	1996	PRATA	JTK7625	GRB - COBRAN€AS	NULL	NULL	8273-3¦ FASE 61 A 120 - GERAL	NULL	NULL	6- 91 A 120	NULL	652447520	9BGSC08ZTTC738315	NULL	AUX ADMINISTRATIVO	AUX ADMINISTRATIVO	NULL	NULL	E- 91 a 120	NULL	2017-08-01 00:00:00	2017-08-28 00:00:00	1	246,69	722

INSERT performance_historico	SELECT
'612177411','941436230',NULL,'001403','PESSOA - RENAN','006316-JOSE EDUARDO PESSOA DA SILVA ME','THIAGO FONTES GASPAR',61,612,'2014-09-25',42,'035','2014-10-25',98,'E','E','2017-08-25',246.69,0,2595,3800,0,80.67,'32497958874','44.252.730-5','R FREDERICO RODRIGUES DE GODOI, 81','JARDIM ZAIRA','MAUA','09321470','SP','2375-9144','94172-0061','4427-4037','GPA CIA BRAS DE DISTRIBUIÇÃO','4511-9444','4577-6619',8575,'GM - Chevrolet','CORSA - WIND 1.0 MPF',1996,1996,'PRATA','JTK7625','GRB - COBRAN€AS',NULL,NULL,'8273-3¦ FASE 61 A 120 - GERAL',NULL,NULL,'6- 91 A 120',NULL,'652447520','9BGSC08ZTTC738315',NULL,'AUX ADMINISTRATIVO','AUX ADMINISTRATIVO',NULL,NULL,'E- 91 a 120',NULL,'2017-08-01','2017-08-28',1,246.69,722
-- OK
*/

INSERT  performance_historico 
SELECT  CONTRATO,	PROPOSTA,		AGENTE,		CODPROMOTORA,	PROMOTORA,	
		LOJA,		NOME_CLIENTE,	CARTEIRA,	MODALIDADE,		DATA_CONTRATO,
		PLANO,		PARCELA,		PRI_VCTO,	ATRASO,			Rating,
		SituacaoBacen,	VENCIMENTO,	VLR_PARCELA,	QTD_PARC_ATRASO,	VALOR_RISCO,
		VALOR_FINANCIADO,	TAC,	IOC,		CPF_CNPJ,		RG,
		ENDERECO,	BAIRRO,			CIDADE,		CEP,			UF,			
		TELEFONE,	CELULAR,		FONE_COMERCIAL,	LOCAL_TRABALHO,	REFERENCIA1,	
		REFERENCIA2,VALORBEM,		MARCA,		MODELO,			ANO_FABRIC,
						-- ALTERA COBRADORA
		ANO_MODELO,		COR,		PLACA,		'',		SERASA,
		OCORRENCIA,		REGRA,		BLOQUEIO,	DESBLOQUEIO,	FAIXA,
		GRUPO,		RENAVAM,		CHASSI,		QTDPAGAR,		PROFISSAO,
		CARGO,		COD_AGENTE,		PARCELAS_PAGAS,	FAIXA_RATING,	SALDOINSC,
		-- ALTERADO DT REF E								COBRADORA
		@DTPARA AS DATA_REF,	DATA_PAGAMENTO,	RECEBIDO,	VALORREC,	@PARAcobrad AS CODCOBRADORA
--612242794	941543612	ARANDA	000240	ARANDA - EDU	005396-JOAO PIMENTEL DA SILVA SAO JOSE LAJE LT	NELSON NUNES	61	612	2016-03-29 00:00:00	36	016	2016-04-29 00:00:00	6	A	A	2017-07-29 00:00:00	289,22	0	4385,48	3450	0	135,26	10567018890	14.550.305-7	R NOVO ORIENTE DO PIAUI, 765	VILA SILVIA	SAO PAULO	03820310	SP		94692-4051	2066-0600	INOVA GESTÃO DE SERVIÇOS URBANOS S/A	2775-9398	2515-5929	6853	GM - Chevrolet	MONZA - SL/E SR 2.0	1991	1991	VERDE	BFD9193	RENNOV PA	NULL	NULL	16000-FASE 0 - 5 A 15	NULL	NULL	2- 6 A 15	NULL	433522500	9BGJK11YMMB023284	NULL	VARREDOR	VARREDOR	000002	NULL	A- 6 a 15	NULL	2017-09-01 00:00:00	NULL		0	710
FROM	PERFORMANCE_HISTORICO	(NOLOCK)
WHERE	DATA_REF = @DTREF and  contrato = @contr and  codcobradora=@cobrad -- icr
	AND parcela  = CONVERT(INT,@parc )

-- ATUALIZA O NOME DA COBRADORA NO REGISTRO NOVO ************
UPDATE		performance_historico
	SET		COBRADORA = 
		CASE	WHEN C.COBRADORA='UNIAO - JURIDICO' THEN 'UNIAO COBRAN€AS'
				WHEN C.COBRADORA='RENNOV JURIDICO'  THEN 'RENNOV - VEICULOS'
				WHEN C.COBRADORA='GRB JURIDICO'	    THEN 'GRB - COBRAN€AS'
				WHEN C.COBRADORA='ICR JURIDICO'	    THEN 'ICR COBRANÇAS'
				ELSE C.COBRADORA					END	
FROM	(select COCOD,CODESCR AS COBRADORA	from tcobradora (nolock) where CoTela = 1 )  AS C
	WHERE	DATA_REF = @DTPARA and  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc )


update  performance_historico set ATRASO= DATEDIFF(D, vencimento , data_ref )
	WHERE	DATA_REF = @DTPARA and  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc )

UPDATE  performance_historico  SET RATING = 'A', FAIXA_RATING = 'A- 6 a 15' 
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND  ATRASO  <= 15                             
  
UPDATE  performance_historico  SET RATING = 'B', FAIXA_RATING = 'B- 16 a 30' 
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND   ATRASO  >15 and ATRASO <= 30                
                
UPDATE  performance_historico  SET RATING = 'C' , FAIXA_RATING = 'C- 31 a 60'
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND  ATRASO > 30 and ATRASO  <= 60                
                
UPDATE  performance_historico  SET RATING = 'D', FAIXA_RATING = 'D- 61 a 90' 
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND  ATRASO > 60 and ATRASO <= 90                
                
UPDATE  performance_historico  SET RATING = 'E', FAIXA_RATING = 'E- 91 a 120' 
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND  ATRASO >90 and ATRASO <= 120                
              
UPDATE  performance_historico  SET RATING = 'F', FAIXA_RATING = 'F- 121 a 150' 
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND  ATRASO >120 and ATRASO<= 150                
                
UPDATE  performance_historico  SET RATING = 'G', FAIXA_RATING = 'G- 151 a 180' 
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND    ATRASO > 150 and ATRASO <= 180                
                 
UPDATE  performance_historico  SET RATING = 'H', FAIXA_RATING = 'H- 181 a 360' 
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND    ATRASO >180 and ATRASO  <= 360                
                
UPDATE  performance_historico  SET RATING = 'HH', FAIXA_RATING = 'H- > 360' 
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND    ATRASO  > 360                
                
-- FAIXA
UPDATE  performance_historico  SET FAIXA = '2- 6 A 15' 
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND  ATRASO  <= 15
      
UPDATE  performance_historico  SET FAIXA = '3- 16 A 30' 
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND  ATRASO >  15 AND ATRASO <= 30;          
      
UPDATE  performance_historico  SET FAIXA = '4- 31 A 60'  
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND   ATRASO >  30 AND ATRASO <= 60;         
      
UPDATE  performance_historico  SET FAIXA = '5- 61 A 90'  
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND   ATRASO >  60 AND ATRASO <= 90;          
      
UPDATE  performance_historico  SET FAIXA = '6- 91 A 120'  
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND   ATRASO >  90 AND ATRASO <= 120;          
      
UPDATE  performance_historico  SET FAIXA = '7- 121 A 150'  	
WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND   ATRASO >  120 AND ATRASO <= 150;          
      
UPDATE  performance_historico  SET FAIXA = '8- 151 A 180'  	
WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND   ATRASO >  150 AND ATRASO <= 180;          
      
UPDATE  performance_historico  SET FAIXA = '9- 181 A 360'  
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND   ATRASO >  180 AND ATRASO <= 360;          
      
UPDATE  performance_historico  SET FAIXA = '99- > 360'  
	WHERE	DATA_REF = @DTPARA  AND  contrato = @contr and  codcobradora=@PARAcobrad 
		AND Parcela  = CONVERT(INT,@parc)	AND   ATRASO >  360;           
          



END
GO
