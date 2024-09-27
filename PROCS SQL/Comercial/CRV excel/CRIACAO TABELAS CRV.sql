USE SIG
GO

DROP   TABLE PEND151

CREATE TABLE PEND151
(OPCODPROD	varchar(6),
OPNROPER	varchar(15),
OPDTBASE	smalldatetime,
OPVLRFIN	float,
OPCODORG1	varchar(10),
O1DESCR		varchar(200),
OPCODORG2	varchar(10),
O2DESCR		varchar(200),
OPCODORG3	varchar(10),
O3DESCR		varchar(200),
OPCODORG4	varchar(10),
O4DESCR		varchar(200),
OPCODORG5	varchar(10),
O5DESCR		varchar(200),
OPCODORG6	varchar(10),
EPCODPEN	varchar(3),
EPCMPLTO	varchar(255),
OPCODCLI	varchar(15),
A13CODORG	int,
A13DESCR	varchar(200),
ABRENAVAM	varchar(11),
ABVLRTAB	float,
ABCHASSI	varchar(20),
ABCERTIF	varchar(10),
ABPLACA		varchar(7),
ABMODELO	varchar(80),
CLNOMECLI	varchar(200),
CLFONEFIS	varchar(20),
CLFONECOM	varchar(20),
CLCGC		varchar(18),
DIASATRASO	int,
EPDTAGEN	smalldatetime,
EPDTPEN		smalldatetime,
EPDTCAD		smalldatetime,
EPDTLNCBX	smalldatetime,
EPDTFIM		smalldatetime)

DROP   INDEX PEND151.PEND151_IDX1 
CREATE INDEX PEND151_IDX1			ON PEND151 (OPNROPER) 