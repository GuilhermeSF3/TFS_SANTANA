/*
SELECT A13CODORG,A13DESCR,* from  CDCSANTANAMicroCredito..TA1O3  (nolock) 

SELECT * from  CDCSANTANAMicroCredito..Torg3  (nolock) 
SELECT * from  acessoSantanaMicroCredito..TUSU (nolock)

SELECT * from  netfactor..login (nolock)
SELECT * from  netfactor..obusuario (nolock)
*/
use sig

select * from tbl_svc (nolock)

create table tbl_svc (
codigo	int primary key,
nome	varchar(50)		)

insert tbl_svc select 
A13CODORG	A13DESCR
146	BKF SOLUCOES
147	CORPAL
149	BKF AUTO
163	FINANZERO AUTO
164	FINANZERO AGENTES
165	CREDFIT
167	CRED MARTINELLI
168	EASYCREDITO APP

codigo	nome
146	Creditas
164	FINANZERO
165	CREDFIT
168	EASYCREDITO

insert tbl_svc select 146,'Creditas'
insert tbl_svc select 164,'FINANZERO'
insert tbl_svc select 165,'CREDFIT'
insert tbl_svc select 168,'EASYCREDITO'

select codigo,nome from tbl_svc (nolock)

select * from usuario_svc (nolock)

select codgerente,nomeUsuario from usuario (nolock) where perfil=8

CodAGENTE	CodAgSVC	CodOperador
119	164	1821

SELECT * FROM usuario_svc (NOLOCK) ORDER BY codAGENTE

