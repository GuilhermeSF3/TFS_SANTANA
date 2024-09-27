-- homologacao  .190
-- tranfere pdd mensal

-- carrega tabela rating_historico
-- com os dados de BacenSantanaMICROcredito tabela EClas

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


use SIG
--use SF_relatorios
go

--DROP PROCEDURE [dbo].SF_tabelas


/*
sp_help rating_historico
DT_REF	varchar	no	20
NUM_OPE	varchar	no	50
DT_BASE	smalldatetime	no	4
NVL_RISCO	varchar	no	50
NOM_CLI	varchar	no	100
DT_VCTO1	smalldatetime	no	4
SLD_INSCRITO	float	no	8
VLR_PRINC	float	no	8
VL_JUROS	float	no	8
TX_RAP	float	no	8
RRF_JR	float	no	8
VLR_FV	float	no	8
PERC_PDD	float	no	8
VLR_PDD	nchar	no	20
Atraso	int	no	4
rola14	int	no	4
rola30	int	no	4
rola60	int	no	4
rola90	int	no	4
rola120	int	no	4
rola150	int	no	4
rola180	int	no	4
rola360	int	no	4
volta14	int	no	4
volta30	int	no	4
volta60	int	no	4
volta90	int	no	4
volta120	int	no	4
volta150	int	no	4
volta180	int	no	4
volta360	int	no	4
entrada14	int	no	4
entrada30	int	no	4
entrada60	int	no	4
entrada90	int	no	4
entrada120	int	no	4
entrada150	int	no	4
entrada180	int	no	4
entrada360	int	no	4
*/

CREATE TABLE rating_historico (
DT_REF		varchar(20),
NUM_OPE		varchar(50),
DT_BASE		smalldatetime,
NVL_RISCO	varchar(50),
NOM_CLI		varchar(100),
DT_VCTO1	smalldatetime,
SLD_INSCRITO	float,
VLR_PRINC	float,
VL_JUROS	float,
TX_RAP		float,
RRF_JR		float,
VLR_FV		float,
PERC_PDD	float,
VLR_PDD		nchar(20),
Atraso		int,
rola14		int,
rola30		int,
rola60		int,
rola90		int,
rola120		int,
rola150		int,
rola180		int,
rola360		int,
volta14		int,
volta30		int,
volta60		int,
volta90		int,
volta120	int,
volta150	int,
volta180	int,
volta360	int,
entrada14	int,
entrada30	int,
entrada60	int,
entrada90	int,
entrada120	int,
entrada150	int,
entrada180	int,
entrada360	int )


alter table RATING_HISTORICO
add  parc	int,
	 dtVencParc smalldatetime

alter table RATING_HISTORICO
add   AGENTE	VARCHAR(6),
	  COBRADORA VARCHAR(6),
	  DT_FECHA	SMALLDATETIME

-- VCTO DA 1A PARCELA
alter table RATING_HISTORICO
add   DT_VCTO1P	SMALLDATETIME

SP_HELP RATING_HISTORICO
ALTER	TABLE RATING_HISTORICO
		ALTER COLUMN VLR_PDD FLOAT

-- ADAPTA OS CAMPOS + COD CLIENTE
alter table RATING_HISTORICO
add  CODCLI	VARCHAR(15)

-- ADAPTA OS CAMPOS + COD PRODUTO = 1 LOJAS, 2= VEICULOS, 6=GIRO
alter table RATING_HISTORICO
add  CODPROD	SMALLINT

-- SP_HELP RATING_HISTORICO

-- SP_HELP RR_AUX_CONTRATO_2M
-- drop TABLE RR_AUX_CONTRATO_2M
CREATE TABLE RR_AUX_CONTRATO_2M (
DT_FECHA		SMALLDATETIME,
NUM_OPE			varchar(50),
AGENTE			VARCHAR(6),
COBRADORA		VARCHAR(6),
NVL_RISCO		varchar(50),
NVL_RISCO_ANT	varchar(50),
SLD_INSCRITO	float,
SLD_INSCRITO_ANT	float,
Atraso		int,
Atraso_ANT	int,

PARC		INT,
PARC_ANT	INT,

VENCTO		SMALLDATETIME,
VENCTO_ANT	SMALLDATETIME,

--INICIO		INT,
ENTRA		int,
RECUPERA	int,
ROLA		int	 )

alter table RR_AUX_CONTRATO_2M
add   CODPROD	SMALLINT


---------------------
-- drop TABLE RR_ROLLRATE
CREATE TABLE RR_ROLLRATE (
DT_FECHA		SMALLDATETIME,
AGENTE			VARCHAR(6),
COBRADORA		VARCHAR(6),
ORDEM_LINHA		int,
ORDEM_FAIXA		int,
FORMATO			VARCHAR(6),
DESCRICAO		VARCHAR(50),
QTDE			int,
PRC_QTDE		FLOAT,
SALDO			FLOAT,
PRC_SALDO		FLOAT,
FAIXA_DE		INT,
FAIXA_ATE		INT,
RATING			VARCHAR(6)
 )

---------------------
-- drop TABLE RR_ROLLRATE_RPT
CREATE TABLE RR_ROLLRATE_RPT (
	DT_FECHA		SMALLDATETIME,
	AGENTE			VARCHAR(6),
	COBRADORA		VARCHAR(6),
	ORDEM_LINHA		int,
	FORMATO			VARCHAR(6),
	DESCRICAO		VARCHAR(50),

	A_QTDE			FLOAT,
	A_SALDO			FLOAT,
	B_QTDE			FLOAT,
	B_SALDO			FLOAT,
	C_QTDE			FLOAT,
	C_SALDO			FLOAT,
	D_QTDE			FLOAT,
	D_SALDO			FLOAT,
	E_QTDE			FLOAT,
	E_SALDO			FLOAT,
	F_QTDE			FLOAT,
	F_SALDO			FLOAT,
	G_QTDE			FLOAT,
	G_SALDO			FLOAT,
	H_QTDE			FLOAT,
	H_SALDO			FLOAT,
	HH_QTDE			FLOAT,
	HH_SALDO		FLOAT,
	TTL_QTDE		FLOAT,
	TTL_SALDO		FLOAT
	 )

END

-- auxiliares
select * 
into pdd_sem_veic
FROM RATING_HISTORICO where 0=1

-- SP_HELP pdd_sem_veic
Column_name	Type	Computed	Length
DT_REF	varchar	no	20
NUM_OPE	varchar	no	50
DT_BASE	smalldatetime	no	4
NVL_RISCO	varchar	no	50
NOM_CLI	varchar	no	100
DT_VCTO1	smalldatetime	no	4
SLD_INSCRITO	float	no	8
VLR_PRINC	float	no	8
VL_JUROS	float	no	8
TX_RAP	float	no	8
RRF_JR	float	no	8
VLR_FV	float	no	8
PERC_PDD	float	no	8
VLR_PDD	float	no	8
Atraso	int	no	4
rola14	int	no	4
rola30	int	no	4
rola60	int	no	4
rola90	int	no	4
rola120	int	no	4
rola150	int	no	4
rola180	int	no	4
rola360	int	no	4
volta14	int	no	4
volta30	int	no	4
volta60	int	no	4
volta90	int	no	4
volta120	int	no	4
volta150	int	no	4
volta180	int	no	4
volta360	int	no	4
entrada14	int	no	4
entrada30	int	no	4
entrada60	int	no	4
entrada90	int	no	4
entrada120	int	no	4
entrada150	int	no	4
entrada180	int	no	4
entrada360	int	no	4
parc	int	no	4
dtVencParc	smalldatetime	no	4
AGENTE	varchar	no	6
COBRADORA	varchar	no	6
DT_FECHA	smalldatetime	no	4
DT_VCTO1P	smalldatetime	no	4
CODCLI	varchar	no	15
CODPROD	smallint	no	2


SELECT * INTO RATING_PROJETADO
	FROM RATING_HISTORICO WHERE 1=0

-- PRAZO DOBRADO
ALTER TABLE RATING_PROJETADO
ADD ADECORRER INT, PLANO INT

-- ADECORRER , PLANO , ATRASO_RENEG 
ALTER TABLE RATING_PROJETADO ADD ATRASO_RENEG INT
ALTER TABLE RATING_PROJETADO ADD OPDTVCTO SMALLDATETIME


-- SP_HELP SALDO_AUX 
-- IDENTICO AO COPER
Column_name	Type	Computed	Length
OPCODOP	varchar	no	3
OPNATOP	varchar	no	2
CLATIV	varchar	no	3
OPNROPER	varchar	no	15
CLNOMECLI	varchar	no	60
OPDTPRCAB	smalldatetime	no	4
OPCODPRD	varchar	no	3
OPCODNIV	varchar	no	2
OPVLRPR	float	no	8
OPVLRJR	float	no	8
OPRAP	float	no	8
OPMORA	float	no	8
OPRRFPR	float	no	8
OPRRFJR	float	no	8
OPRRFMR	float	no	8


-- SP_HELP  AUX_PARC
--
DROP TABLE  AUX_PARC
CREATE TABLE  AUX_PARC
(PANROPER	varchar(20),
PANRPARC	varchar(5),
PADTVCTO	SMALLDATETIME,
PAPMT		FLOAT,
PADTLIQ		smalldatetime)

--SP_HELP  AUX_RR_PARC_PROJETADA
DROP TABLE  AUX_RR_PARC_PROJETADA
CREATE TABLE  AUX_RR_PARC_PROJETADA
(PANROPER	varchar(15),
PANRPARC	varchar(3),
PADTVCTO	SMALLDATETIME,
MINPADTMOV	smalldatetime)


SELECT * INTO RR_PRJ_CONTRATO_2M  from RR_AUX_CONTRATO_2M  WHERE 1=0
DELETE FROM RR_PRJ_CONTRATO_2M

-- drop table RR_ROLLRATE_DIA
sp_help RR_ROLLRATE_DIA
Column_name	Type	Computed	Length
DT_FECHA	smalldatetime	no	4
AGENTE	varchar	no	6
COBRADORA	varchar	no	6
ORDEM_LINHA	int	no	4
ORDEM_FAIXA	int	no	4
FORMATO	varchar	no	6
DESCRICAO	varchar	no	50
QTDE	int	no	4
PRC_QTDE	float	no	8
SALDO	float	no	8
PRC_SALDO	float	no	8
FAIXA_DE	int	no	4
FAIXA_ATE	int	no	4
RATING	varchar	no	6

-- drop table RR_ROLLRATE_DIA_rpt

SELECT * INTO RR_ROLLRATE_DIA  from RR_ROLLRATE   WHERE 1=0

SELECT * INTO RR_ROLLRATE_DIA_RPT   from RR_ROLLRATE_RPT   WHERE 1=0

-- drop table RR_ROLLRATE_DIA_rpt
-- sp_help  RR_ROLLRATE_DIA_rpt

create TABLE RR_ROLLRATE_DIA_rpt (
DT_FECHA	smalldatetime,
AGENTE	varchar	(6),
COBRADORA	varchar(6),
ORDEM_LINHA	int	,
FORMATO	varchar	(6),
DESCRICAO	varchar	(50),
AA_QTDE		float	,
AA_SALDO	float	,
A_QTDE	float	,
A_SALDO	float	,
B_QTDE	float	,
B_SALDO	float	,
C_QTDE	float	,
C_SALDO	float	,
D_QTDE	float	,
D_SALDO	float	,
E_QTDE	float	,
E_SALDO	float	,
F_QTDE	float	,
F_SALDO	float	,
G_QTDE	float	,
G_SALDO	float	,
H_QTDE	float	,
H_SALDO	float	,
HH_QTDE	float	,
HH_SALDO	float,
TTL_QTDE	float,
TTL_SALDO	float)

--  123
-- RR_ROLLRATE_123
-- RR_123_CONTRATO_2M
-- RR_ROLLRATE_123_RPT
-- ***************************************************
-- anterior 123 serve para recebimento
-- select * into RC_123_rpt  from 	RR_ROLLRATE_123_rpt 
-- drop table RR_ROLLRATE_rec_rpt
DROP	TABLE RC_123_rpt
create	TABLE RC_123_rpt (
DT_FECHA	smalldatetime,
AGENTE		varchar	(6),
COBRADORA	varchar(6),
ORDEM_LINHA	int	,
FORMATO		varchar	(6),
DESCRICAO	varchar	(50),
P1_QTDE		float	,
P1_SALDO	float	,
P2_QTDE		float	,
P2_SALDO	float	,
P3_QTDE		float	,
P3_SALDO	float	,
TTL_QTDE	float,
TTL_SALDO	float,
PRC_TTL		float,
PRC_ACUM	float
)

--  123

SELECT * INTO RR_ROLLRATE_123 FROM RR_ROLLRATE WHERE 1=0
SELECT * INTO RR_123_CONTRATO_2M FROM RR_AUX_CONTRATO_2M WHERE 1=0
drop table RR_ROLLRATE_123_RPT
SELECT * INTO RR_ROLLRATE_123_RPT FROM RR_ROLLRATE_RPT WHERE 1=0


---------------------
-- copia das tabelas para o diario

SELECT * INTO RR_PRJ_CONTRATO_2M  from RR_AUX_CONTRATO_2M  WHERE 1=0
--DELETE FROM RR_PRJ_CONTRATO_2M

SELECT * INTO RR_ROLLRATE_DIA  from RR_ROLLRATE   WHERE 1=0

SELECT * INTO RR_ROLLRATE_DIA_RPT   from RR_ROLLRATE_RPT   WHERE 1=0


--  ***********************************
DROP TABLE USUARIO
 
CREATE TABLE USUARIO (
 Login     VARCHAR(20) NOT NULL,
 NomeUsuario    VARCHAR(20) NOT NULL,
 Funcao     CHAR(3) NOT NULL,
 CodGerente    CHAR(7) NULL,
 CodFilial    CHAR(5) NOT NULL,
 Cpf     CHAR(11) NULL,
 EMail     VARCHAR(50) NULL,
 Ativo     INT NOT NULL,
 NomeCompleto    VARCHAR(50) NOT NULL,
 senha	varchar(40),
	perfil	int,
 CONSTRAINT CK1_USUARIO CHECK (Funcao IN ('VIP', 'DIR', 'SUP', 'GEG', 'GER', 'ASS', 'ADM')),
 CONSTRAINT PK_USUARIO PRIMARY KEY (Login)
)
 
ALTER TABLE USUARIO 
ADD  	PRODUTO	INT	

UPDATE USUARIO
SET PRODUTO=0 WHERE PRODUTO IS NULL
 

sp_help USUARIO

INSERT INTO USUARIO values 
('teste','Teste','VIP','00002','01','00','teste@gmail.com',1,'Teste','zgv9FQWbaNZ2iIhNej0+jA==' ,0,2)
 
INSERT INTO USUARIO values 
('ri','Raquel Izumizawa','VIP','00002','01','00','raquel_mi@hotmail.com',1,'Raquel Izumizawa','zgv9FQWbaNZ2iIhNej0+jA==' ,0,2)

INSERT INTO USUARIO values 
('acesso','Acessos','GEG','00003','01','00','suporte@santanafinanceira.com.br',1,'Adm','zgv9FQWbaNZ2iIhNej0+jA==',0,0)

SELECT * FROM USUARIO

update USUARIO set nomecompleto='Junior' where login='junior'
 

--  ***********************************
--  ***********************************
drop   table TCOBRADORA
create table TCOBRADORA (
COCodProduto 	int,
COCod 		int,
CODescr 	varchar(200),
COAtivo 	bit  		)

alter table TCOBRADORA add COTela int

CREATE INDEX [TCOBRADORA_IN_01] ON TCOBRADORA ([COCodProduto],[COCod]) ON [PRIMARY]

--  ***********************************
--  ***********************************

--  ***********************************
DROP table TAGENTE
create table TAGENTE (
AGCodProduto 	int,
AGCod 		int,
AGDESCR		varchar(100),
AGAtivo 	bit  		)

CREATE INDEX [TAGENTE_IN_01] ON TAGENTE ([AGCodProduto],[AGCod]) ON [PRIMARY]

--  ***********************************

--  ***********************************


DROP table TAUX_CONTRATO
create table TAUX_CONTRATO (

	CONTRATO	VARCHAR(20)	,
	CPF_CNPJ	VARCHAR(20),
	NOME_CLIENTE	VARCHAR(200),
	AGENTE			VARCHAR(200),
	COBRADORA		VARCHAR(200),
	DATA_CONTRATO	SMALLDATETIME,
	PLANO			INT,
	PARCELA_ABERTO	INT,
	PRIMEIRO_VCTO	SMALLDATETIME,
	ATRASO			INT,
	VENCIMENTO		SMALLDATETIME,
	VLR_PARCELA		FLOAT,
	QTD_PARC_ATRASO	INT,
 	SALDO_INSCRITO	FLOAT,
 	VLR_FINANCIADO	FLOAT,
	MARCA			VARCHAR(200),
	MODELO			VARCHAR(200),
	ANO_FABRIC		VARCHAR(50),
	PARC_PAGAS		INT,
	PROFISSAO		VARCHAR(200),
	CARGO			VARCHAR(200)
)

-- SP_HELP TAUX_CONTRATO
ALTER table TAUX_CONTRATO ADD 
	COD_CLI			VARCHAR(50),
	COD_COB			VARCHAR(50),
-- ALTER table TAUX_CONTRATO ADD 
	COD_VEIC		VARCHAR(50),
	COD_OPERADOR VARCHAR(50)    ,
-- ALTER table TAUX_CONTRATO ADD 
    COD_AGENTE VARCHAR(50)

-- ALTER table TAUX_CONTRATO DROP COLUMN 	COD_AGE

sp_help Torg3_agente
select * from Torg3_agente
delete from Torg3_agente
insert Torg3_agente
select  distinct * from sf_relatorios2..Torg3_agentebkp

-- REPET select  distinct * from sf_relatorios2..Torg3_agente2

select  distinct * from sf_relatorios2..Torg3_agentebkp

select  * from sf_relatorios2..Torg3_agente
select  * into Torg3_agente from sf_relatorios2..Torg3_agente

select * from Torg3_agente where cod_agente=94

select * from Tagente
insert Tagente
select distinct 2,cod_agente, max(agente),1 from Torg3_agente group by cod_agente
delete from Torg3_agente
where agente='AGUINALDO'
ALBERTO RICELI

select * from Tcobradora
insert Tcobradora
select distinct 2,lccodacob,descr_cobradora,1 from LEVANTAMENTO
select * from GESTORCOB..TACOB
  INNER JOIN GESTORCOB..TACOB C ON C.ACCODACOB = E.LCCODACOB  


-- RE_ESTOQUE_RPT
-- ***************************************************
-- ESTOQUE FIXO NO MES
-- select * into RE_ESTOQUE_rpt  from 	RC_123_rpt WHERE 1=0
-- select * from 	RE_ESTOQUE_rpt
-- drop table RE_ESTOQUE_rpt
DROP	TABLE RE_ESTOQUE_rpt
create	TABLE RE_ESTOQUE_rpt (
DT_REF	SMALLDATETIME,
DT_FECHA	smalldatetime,
AGENTE		varchar	(6),
COBRADORA	varchar(6),
ORDEM_LINHA	int	,
FORMATO		varchar	(6),
DESCRICAO	varchar	(50),
EC_QTDE		float	,
EC_SALDO	float	,
EP_QTDE		float	,
EP_SALDO	float	,
RC_QTDE		float	,
RC_SALDO	float	,
PRCC_REC	float,
RP_QTDE		float,
RP_SALDO	float,
PRCP_REC	float
)

--  FIM RECUPERACAO ESTOQUE

-- SP_HELP AUX_PARC

-- SP_HELP  AUX_PARC
-- SP_HELP  RATING_HISTORICO
DROP TABLE  RATING_RECUPERA_PARC
CREATE TABLE  RATING_RECUPERA_PARC
(DT_REF	SMALLDATETIME,
DT_FECHA	SMALLDATETIME,
AGENTE		varchar(10),
COBRADORA	varchar(10),
NVL_RISCO	varchar(2),
SLD_INSCRITO	FLOAT,
PANROPER	varchar(20),
PANRPARC	varchar(5),
PADTVCTO	SMALLDATETIME,
PAPMT		FLOAT,
PADTLIQ		smalldatetime)

-- SP_HELP  RATING_HISTORICO
-- PARCELAS EM ABERTO NO FECHAMENTO
DROP TABLE  RATING_HISTORICO_PARC
CREATE TABLE  RATING_HISTORICO_PARC
(DT_REF	SMALLDATETIME,
DT_FECHA	SMALLDATETIME,
AGENTE		varchar(10),
COBRADORA	varchar(10),
NVL_RISCO	varchar(2),
SLD_INSCRITO	FLOAT,
PANROPER	varchar(20),
PANRPARC	varchar(5),
PADTVCTO	SMALLDATETIME,
PAPMT		FLOAT,
PADTLIQ		smalldatetime)

-- SP_HELP  PERFORM_SAFRA
-- PARCELAS ENVIADAS E CONTROLE DE PAGTO EM ASSESSORIA COBRANCA
DROP TABLE  PERFORM_SAFRA
CREATE TABLE  PERFORM_SAFRA
 (	dt_ENVIO	SMALLDATETIME,
	NROPER		VARCHAR(20), 
	NRPARC		VARCHAR(05), 
	VLR_ENVIO	FLOAT,
	CODACOB		VARCHAR(10),
	CODREGR		VARCHAR(10)	,	
	RECEBIDO	INT,
	DT_RECEB	SMALLDATETIME, 
	VLR_RECEB	FLOAT,
	dt_VCTO		SMALLDATETIME
)

CREATE TABLE PE_ESTOQUE_RPT (
		DT_FECHA	smalldatetime,
		AGENTE		varchar	(6),
		COBRADORA	varchar(6),
		ORDEM_LINHA	int	,
		FORMATO		varchar	(6),
		DESCRICAO	varchar	(50),
		AA_QTDE		float	,
		AA_SALDO	float	,
		A_QTDE		float	,
		A_SALDO	float	,
		B_QTDE		float	,
		B_SALDO	float	,
		PRCC_REC	float,
		RP_QTDE		float,
		RP_SALDO	float,
		PRCP_REC	float
	 )

DROP TABLE  PERFORM_ESTOQUE
CREATE TABLE  PERFORM_ESTOQUE
 (	dt_REF		SMALLDATETIME,
	dt_ENVIO	SMALLDATETIME,	
	NROPER		VARCHAR(20), 
	NRPARC		VARCHAR(05), 
	VLR_ENVIO	FLOAT,
	CODACOB		VARCHAR(10),
	CODREGR		VARCHAR(10)	,	
	RECEBIDO	INT,
	DT_RECEB	SMALLDATETIME, 
	VLR_RECEB	FLOAT,
	dt_VCTO		SMALLDATETIME
)

CREATE INDEX  PERFORM_ESTOQUE_IDX1 ON PERFORM_ESTOQUE
(DT_REF,dt_ENVIO,NROPER,NRPARC)

CREATE INDEX  PERFORM_SAFRA_IDX1 ON PERFORM_SAFRA
(dt_ENVIO,NROPER,NRPARC)

DROP INDEX IX_ProductVendor_VendorID ON Purchasing.ProductVendor

-- index
CREATE INDEX  RATING_HISTORICO_PARC_IDX1 ON RATING_HISTORICO_PARC
(DT_FECHA,PANROPER,PANRPARC)

CREATE INDEX  RATING_HISTORICO_PARC_IDX2 ON RATING_HISTORICO_PARC
(DT_FECHA,AGENTE,COBRADORA,NVL_RISCO,PANROPER,PANRPARC)

-- index
CREATE INDEX  RATING_HISTORICO_IDX1 ON RATING_HISTORICO
(DT_FECHA,NVL_RISCO,NUM_OPE)
CREATE INDEX  RATING_HISTORICO_IDX2 ON RATING_HISTORICO
(DT_FECHA,AGENTE,COBRADORA,NVL_RISCO,NUM_OPE)


CREATE INDEX  RR_AUX_CONTRATO_2M_IDX1 ON RR_AUX_CONTRATO_2M
(DT_FECHA,AGENTE,COBRADORA,NVL_RISCO,NUM_OPE)


-- SP_HELP Performance_carga
CREATE TABLE Performance_carga  (
		CONTRATO	varchar(15),
		PROPOSTA	varchar(9),
		AGENTE	varchar(27),
		CODPROMOTORA	varchar(6),
		PROMOTORA	varchar(20),
		LOJA	varchar(47),
		NOME_CLIENTE	varchar(60),
		CARTEIRA	varchar(2),
		MODALIDADE	varchar(3),
		DATA_CONTRATO	smalldatetime,
		PLANO	smallint,
		PARCELA	varchar(3),
		PRI_VCTO	smalldatetime,
		ATRASO	int,
		Rating	varchar(2),
		SituacaoBacen	varchar(2),
		VENCIMENTO	smalldatetime,
		VLR_PARCELA	float,
		QTD_PARC_ATRASO	int,
		VALOR_RISCO	float,
		VALOR_FINANCIADO	float,
		TAC	float,
		IOC	float,
		CPF_CNPJ	varchar(18),
		RG	varchar(16),
		ENDERECO	varchar(43),
		BAIRRO	varchar(25),
		CIDADE	varchar(40),
		CEP	varchar(8),
		UF	varchar(2),
		TELEFONE	varchar(20),
		CELULAR	varchar(15),
		FONE_COMERCIAL	varchar(20),
		LOCAL_TRABALHO	varchar(40),
		REFERENCIA1	varchar(20),
		REFERENCIA2	varchar(20),
		VALORBEM	float,
		MARCA	varchar(20),
		MODELO	varchar(80),
		ANO_FABRIC	varchar(4),
		ANO_MODELO	varchar(4),
		COR	varchar(15),
		PLACA	varchar(7),
		COBRADORA	varchar(24),
		SERASA	varchar(3),
		OCORRENCIA	varchar(258),
		REGRA	varchar(61),
		BLOQUEIO	smalldatetime,
		DESBLOQUEIO	smalldatetime,
		FAIXA	varchar(50),
		GRUPO	varchar(50),
		RENAVAM	varchar(50),
		CHASSI	varchar(50),
		QTDPAGAR	int,
		PROFISSAO	varchar(100),
		CARGO	varchar(100),
		COD_AGENTE	varchar(50),
		PARCELAS_PAGAS	int,
		FAIXA_RATING	varchar(50),
		SALDOINSC	float,
		DATA_REF	smalldatetime,
		DATA_PAGAMENTO	smalldatetime,
		RECEBIDO	varchar(1),
		VALORREC	float,
		CODCOBRADORA	int )


CREATE INDEX  Performance_carga_IDX1 ON Performance_carga
			 (	DATA_REF	,		CODCOBRADORA,  CONTRATO	)


-- SP_HELP Performance_carga_PARC
DROP TABLE Performance_carga_PARC  
CREATE TABLE Performance_carga_PARC  (
		CONTRATO	varchar(15),
		PARCELA	varchar(3),
		ATRASO	int,
		VENCIMENTO	smalldatetime,
		DATA_REF	smalldatetime,
		DATA_PAGAMENTO	smalldatetime,
		RECEBIDO	varchar(1),
		VALORREC	float,
		CODCOBRADORA	int ,
		COBRADORA   VARCHAR(200))


CREATE INDEX  Performance_carga_PARC_IDX1 ON Performance_carga_PARC
			 (	CONTRATO,PARCELA	)

DROP TABLE PERFORMANCE_HISTORICO

CREATE TABLE PERFORMANCE_HISTORICO
(
CONTRATO	varchar(15),
PROPOSTA	varchar(9),
AGENTE	varchar(27),
CODPROMOTORA	varchar(6),
PROMOTORA	varchar(20),
LOJA	varchar(47),
NOME_CLIENTE	varchar(60),
CARTEIRA	varchar(2),
MODALIDADE	varchar(3),
DATA_CONTRATO	smalldatetime,
PLANO	smallINT,
PARCELA	varchar(3),
PRI_VCTO	smalldatetime,
ATRASO	int	,
Rating	varchar(2),
SituacaoBacen	varchar(2),
VENCIMENTO	smalldatetime,
VLR_PARCELA	float,
QTD_PARC_ATRASO	int,
VALOR_RISCO	float,
VALOR_FINANCIADO	float,
TAC	float,
IOC	float,
CPF_CNPJ	varchar(18),
RG	varchar(16),
ENDERECO	varchar(43),
BAIRRO	varchar(25),
CIDADE	varchar(40),
CEP	varchar(8),
UF	varchar(2),
TELEFONE	varchar(20),
CELULAR	varchar(15),
FONE_COMERCIAL	varchar(20),
LOCAL_TRABALHO	varchar(40),
REFERENCIA1	varchar(20),
REFERENCIA2	varchar(20),
VALORBEM	float	,
MARCA	varchar(20),
MODELO	varchar(80),
ANO_FABRIC	varchar(4),
ANO_MODELO	varchar(4),
COR	varchar(15),
PLACA	varchar(7),
COBRADORA	varchar(24),
SERASA	varchar(3),
OCORRENCIA	varchar(258),
REGRA	varchar(61),
BLOQUEIO	smalldatetime,
DESBLOQUEIO	smalldatetime,
FAIXA	varchar(50),
GRUPO	varchar(50),
RENAVAM	varchar(50),
CHASSI	varchar(50),
QTDPAGAR	int	,
PROFISSAO	varchar(100),
CARGO	varchar(100),
COD_AGENTE	varchar(50),
PARCELAS_PAGAS	int	,
FAIXA_RATING	varchar(50),
SALDOINSC	float	,
DATA_REF	smalldatetime,
DATA_PAGAMENTO	smalldatetime,
RECEBIDO	varchar(1),
VALORREC	float	,
CODCOBRADORA	int	
)

CREATE INDEX  PERFORMANCE_HISTORICO_IDX1 ON PERFORMANCE_HISTORICO
			 (	CONTRATO,PARCELA	)

DROP INDEX RR_prj_CONTRATO_2M.RR_prj_CONTRATO_2M_IDX1
CREATE INDEX  RR_prj_CONTRATO_2M_IDX1 ON RR_prj_CONTRATO_2M
	(DT_FECHA , AGENTE, COBRADORA, NVL_RISCO_ANT , RECUPERA)

DROP INDEX RR_prj_CONTRATO_2M.RR_prj_CONTRATO_2M_IDX2
CREATE INDEX  RR_prj_CONTRATO_2M_IDX2 ON RR_prj_CONTRATO_2M
	(DT_FECHA , AGENTE, COBRADORA, NVL_RISCO, ENTRA )

DROP INDEX RR_prj_CONTRATO_2M.RR_prj_CONTRATO_2M_IDX3
CREATE INDEX  RR_prj_CONTRATO_2M_IDX3 ON RR_prj_CONTRATO_2M
	(DT_FECHA , AGENTE, COBRADORA, NVL_RISCO_ANT, ROLA )

DROP INDEX RR_prj_CONTRATO_2M.RR_prj_CONTRATO_2M_IDX4
CREATE INDEX  RR_prj_CONTRATO_2M_IDX4 ON RR_prj_CONTRATO_2M
	(DT_FECHA , AGENTE, COBRADORA, NVL_RISCO, RECUPERA )

-- *************************

SELECT * FROM IP_PESO 
DROP  TABLE IP_PESO 

CREATE TABLE IP_PESO (DATA_REF SMALLDATETIME, RATING VARCHAR(3), FAIXA VARCHAR(50), PESO FLOAT)
DELETE IP_PESO
INSERT IP_PESO SELECT '20140101','B','16 a 30' , 0.13 *100.0
INSERT IP_PESO SELECT '20140101','C','31 a 60' , 0.15 *100.0
INSERT IP_PESO SELECT '20140101','D','61 a 90' , 0.15 *100.0
INSERT IP_PESO SELECT '20140101','E','91 a 120' , 0.15 *100.0

INSERT IP_PESO SELECT '20140101','F','121 a 150' , 0.20 *100.0
INSERT IP_PESO SELECT '20140101','G','151 a 180' , 0.16 *100.0
INSERT IP_PESO SELECT '20140101','H','181 a 359' , 0.06 *100.0

-- *************************
DROP table aux_ip_ordem
create table aux_ip_ordem
(codcobradora varchar(20), ttl_ip float, ordem int identity(1,1) )

-- *************************
CREATE TABLE PERFORMANCE_rpt
( DATA_REF smalldatetime,
CODCOBRADORA  varchar(10),
 COBRADORA  varchar(100),
ORDEM INT,
ORDEM_LINHA	int	,
FORMATO	varchar(6),
DESCRICAO	varchar(50),
B_PRC	float,
C_PRC	float,
D_PRC	float,
E_PRC	float,
F_PRC	float,
G_PRC	float,
H_PRC	float,  -- ATE 360
TTL_IP	float,
DIF_IP	float)

CREATE INDEX PERFORMANCE_rpt_IDX1 ON PERFORMANCE_rpt (DATA_REF,CODCOBRADORA,ORDEM_LINHA )
CREATE INDEX PERFORMANCE_rpt_IDX2 ON PERFORMANCE_rpt (DATA_REF,ORDEM,ORDEM_LINHA )


-- *************************
DROP TABLE PERFORMANCE_CALC
CREATE TABLE PERFORMANCE_CALC  (
DATA_REF  SMALLDATETIME,
COD_COBRADORA	 VARCHAR(10) ,
COBRADORA	 VARCHAR(200) ,
RATING VARCHAR(2),
FAIXA VARCHAR(200) ,
QTDE INT ,
VLR_PARC FLOAT ,
VLR_REC  FLOAT ,
PRC_REC  FLOAT ,
PRC_REC_DIA  FLOAT ,
PRC_REC_REAL  FLOAT ,
META	FLOAT,
REALIZADO FLOAT,
DIF_DIA	FLOAT,
DIF_ACUM FLOAT
)

-- *************************
PERFORMANCE_HISTORICO	
-- SP_HELP Performance_carga
-- TRUNCATE TABLE Performance_carga                
DELETE FROM Performance_carga                
-- Sp_help Performance_carga
-- ALTER TABLE Performance_carga ADD CODLOJA VARCHAR(20)
-- ALTER TABLE Performance_carga ADD CODCLI VARCHAR(20)
-- ALTER TABLE Performance_carga ADD CODCLI VARCHAR(30)

-- *************************
-- DELETE FROM Performance_carga_PARC
CREATE TABLE Performance_carga_PARC  (
		CONTRATO	varchar(15),
		PARCELA	varchar(3),
		ATRASO	int,
		VENCIMENTO	smalldatetime,
		DATA_REF	smalldatetime,
		DATA_PAGAMENTO	smalldatetime,
		RECEBIDO	varchar(1),
		VALORREC	float,
		CODCOBRADORA	int )


CREATE INDEX  Performance_carga_PARC_IDX1 ON Performance_carga_PARC
			 (	CONTRATO,PARCELA	)
-- *************************
