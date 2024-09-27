use sig
go

CREATE TABLE FX_PJ_MODALIDADE  (	
DT_DE	smalldatetime,
COD		int,
DESCR	varchar(200),
TX_DE	FLOAT,
TX_ATE	FLOAT,
PRC_COMISSAO	FLOAT,
ORDEM	int)

CREATE INDEX FX_PJ_MODALIDADE_IDX1 ON FX_PJ_MODALIDADE (DT_DE, COD)

CREATE TABLE FX_PJ_AJUDA  (	
DT_DE	smalldatetime,
DESCR	varchar(100),
PRAZO_DE	int,
PRAZO_ATE	int,
VALOR_DE	FLOAT,
VALOR_ATE	FLOAT,
VLR_AJUDA	FLOAT,
ORDEM	int)

CREATE INDEX FX_PJ_AJUDA_IDX1 ON FX_PJ_AJUDA (DT_DE, DESCR)

/*
select * from FX_PJ_AJUDA (NOLOCK) ORDER BY DT_DE,ORDEM 
sp_help  FX_PJ_AJUDA
*/


CREATE TABLE FX_PJ_CLIENTE_entrada  (	
cod	int,
DT_DE	smalldatetime )

CREATE INDEX FX_PJ_CLIENTE_entrada_IDX1 ON FX_PJ_CLIENTE_entrada (COD)

/*
select cod,'' AS nome, DT_DE from FX_PJ_CLIENTE_entrada (NOLOCK) ORDER BY COD
sp_help  FX_PJ_CLIENTE_entrada
*/


CREATE TABLE FX_PJ_CLIENTE_COMISSAO  (	
cod	int,
DT_DE	smalldatetime,
NOVA_MODALIDADE	int,
TX_DE	FLOAT,
TX_ATE	FLOAT,
PRC_COMISSAO	FLOAT)

CREATE INDEX FX_PJ_CLIENTE_COMISSAO_IDX1 ON FX_PJ_CLIENTE_COMISSAO (COD,DT_DE)

/*
select cod	,'' AS NOME, DT_DE	,NOVA_MODALIDADE, TX_DE	,TX_ATE	,PRC_COMISSAO from FX_PJ_CLIENTE_COMISSAO (NOLOCK) ORDER BY DT_DE,COD
sp_help  FX_PJ_CLIENTE_COMISSAO
*/

CREATE TABLE FX_PJ_CLIENTE_OPERADOR  (	
COD	INT,
DT_DE	smalldatetime,
DT_ATE	smalldatetime,
COD_OPERADOR	int)

CREATE INDEX FX_PJ_CLIENTE_OPERADOR_IDX1 ON FX_PJ_CLIENTE_OPERADOR (COD,DT_DE)

/*
select COD	,'' AS NOME,DT_DE	,DT_ATE	,COD_OPERADOR from FX_PJ_CLIENTE_OPERADOR (NOLOCK) ORDER BY DT_DE,COD
sp_help  FX_PJ_CLIENTE_OPERADOR
*/


CREATE TABLE FX_PJ_DESCONTO (
DT_DESCONTO	smalldatetime,
GRUPO_OPERADOR	varchar(100),
VLR_DESCONTO	FLOAT)

CREATE INDEX FX_PJ_DESCONTO_IDX1 ON FX_PJ_DESCONTO (DT_DESCONTO,GRUPO_OPERADOR)

/*
select * from FX_PJ_DESCONTO (NOLOCK) ORDER BY DT_DESCONTO DESC,GRUPO_Operador
sp_help  FX_PJ_meta
*/

CREATE TABLE FX_PJ_TAXA  (	
DT_DE	smalldatetime,
DESCR	varchar(100),
TX_DE	FLOAT,
TX_ATE	FLOAT,
PRC_COMISSAO	FLOAT,
ORDEM	int)

CREATE INDEX FX_PJ_TAXA_IDX1 ON FX_PJ_TAXA (DT_DE, DESCR)

/*
select * from FX_PJ_TAXA (NOLOCK) ORDER BY DT_DE,ORDEM 
sp_help  FX_PJ_TAXA
*/

CREATE TABLE FX_PJ_META  (	
DT_DE	smalldatetime,
DESCR	varchar(100),
vlr_prod	FLOAT,
META_DE	FLOAT,
META_ATE	FLOAT,
PRC_COMISSAO	FLOAT,
ORDEM	int)

CREATE INDEX FX_PJ_META_IDX1 ON FX_PJ_META (DT_DE, DESCR)

/*
select * from FX_PJ_META (NOLOCK) ORDER BY DT_DE,ORDEM 
sp_help  FX_PJ_META
*/


CREATE TABLE FX_PJ_TC  (	
DT_DE	smalldatetime,
DESCR	varchar(100),
META_DE	FLOAT,
META_ATE	FLOAT,
VLR_TC	FLOAT,
ORDEM	int)

CREATE INDEX FX_PJ_TC_IDX1 ON FX_PJ_TC (DT_DE, DESCR)

/*
select * from FX_PJ_TC (NOLOCK) ORDER BY DT_DE,ORDEM 
sp_help  FX_PJ_TC
*/

CREATE TABLE FX_PJ_META_VLR (	
DT_DE	smalldatetime,
OPERADOR	varchar(100),
VLR_TC	FLOAT)


CREATE INDEX FX_PJ_META_VLR_IDX1 ON FX_PJ_META_VLR (DT_DE, OPERADOR)

/*
select * from FX_PJ_meta_VLR (NOLOCK) ORDER BY DT_DE DESC,Operador
sp_help  FX_PJ_meta_VLR

select * from finsgrdbs..TB_OPER (nolock) order by 1
select oper_cod cod, OPER_ABV_NOM from finsgrdbs..TB_OPER (NOLOCK) order by 2

select * from finsgrdbs..tb_cednte (nolock) order by 1
select cednte_cedn,cednte_nome from finsgrdbs..tb_cednte (nolock) order by 1

select cednte_cedn as Code,(convert(varchar(10),cednte_cedn)+' '+cednte_nome ) as CODNOME  from finsgrdbs..tb_cednte (nolock) order by 2
 Code,CONCAT(Code ,' ',Name ) as CODNOME 
select oper_cod as cod,(convert(varchar(10),oper_cod)+' '+OPER_ABV_NOM ) as CODNOME from finsgrdbs..TB_OPER (nolock) order by OPER_ABV_NOM
*/


