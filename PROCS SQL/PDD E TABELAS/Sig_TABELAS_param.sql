-- homologacao  .190
-- tranfere pdd mensal

-- carrega tabela rating_historico
-- com os dados de BacenSantanaMICROcredito tabela EClas

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


use SIG
go

-- sp_help IP_peso
/*
sp_help USUARIO
-- USUARIO VC JA TEM
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

-- SENHA CRIPTOGRAFADA CF PROGRAMA USUARIO.ASPX
*/

sp_help IP_peso
DROP   TABLE IP_peso
CREATE TABLE IP_peso
(
DATA_REF	smalldatetime NOT NULL,
RATING	varchar	(3) NOT NULL,
FAIXA	varchar	(50),
PESO	float	,
CONSTRAINT IP_peso_IDX1	  PRIMARY KEY	(DATA_REF, RATING))


--CREATE INDEX IP_peso_IDX1 ON IP_peso  (DATA_REF,RATING)

-- *************************

-- sp_help  tcaptacao

DROP   TABLE tcaptacao
CREATE TABLE tcaptacao
(DT_DE			smalldatetime	NOT NULL,
PRC_CAPTACAO	float	,
CONSTRAINT TCAPTACAO_IDX1	  PRIMARY KEY	(DT_DE))

--CREATE INDEX TCAPTACAO_IDX1 ON tcaptacao  (DT_DE)

-- *************************
-- sp_help  tcOMISSAO

DROP   TABLE TCOMISSAO
CREATE TABLE TCOMISSAO
(DT_DE	smalldatetime NOT NULL,
 PRC_COMISSAO	float,
 CONSTRAINT TCOMISSAO_IDX1	  PRIMARY KEY	(DT_DE)	)

--CREATE INDEX TCOMISSAO_IDX1 ON TCOMISSAO  (DT_DE)

-- *************************
-- sp_help  tcUBO

DROP   TABLE tcUBO
CREATE TABLE tcUBO
(
 CUBO	int		NOT NULL,
 DESCR	varchar	(100)	,
 CONSTRAINT tcUBO_IDX1	  PRIMARY KEY	(CUBO)	)

--CREATE INDEX tcUBO_IDX1 ON tcUBO  (CUBO)

-- *************************

-- sp_help  tCOBRADORA
DROP   TABLE tCOBRADORA
CREATE TABLE tCOBRADORA (
	COCodProduto	int		NOT NULL,
	COCod			int		NOT NULL,
	CODescr	varchar(200),
	COAtivo	bit	,
	coTela	int	,
	CONSTRAINT TCOBRADORA_IN_01	  PRIMARY KEY	(COCodProduto, COCod)	)

--CREATE INDEX tCOBRADORA_IDX1 ON tCOBRADORA  (COCodProduto, COCod)

-- *************************


-- sp_help  FX_ANO

DROP   TABLE FX_ANO 
CREATE TABLE FX_ANO (
	DT_DE	smalldatetime	NOT NULL,
	DESCR	varchar	(30),
	DE	int	 NOT NULL,
	ATE	int	,
	ORDEM	int	,
	CONSTRAINT FX_ANO_IDX1	  PRIMARY KEY	(DT_DE, DE)	)

-- CREATE INDEX FX_ANO_IDX1 ON FX_ANO  (DT_DE, DE)

-- *************************

-- sp_help FX_ANO_safra

DROP   TABLE FX_ANO_safra
CREATE TABLE FX_ANO_safra (
	DT_DE	smalldatetime	NOT NULL,
	DESCR	varchar	(30),
	DE	int	 NOT NULL,
	ATE	int	,
	ORDEM	int		,
	CONSTRAINT FX_ANO_IDX1	  PRIMARY KEY	(DT_DE, DE)	)


--CREATE INDEX FX_ANO_safra_IDX1 ON FX_ANO_safra  (DT_DE, DE)

-- *************************

