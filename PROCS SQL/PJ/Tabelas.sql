USE SIG
GO

/****** Object:  Table [dbo].[CETIP_POTENCIAL_ANALITICO]    Script Date: 21/10/2015 18:25:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
-- drop TABLE [PJ_ASSINADIG]
CREATE TABLE [dbo].[PJ_ASSINADIG](
                                strDET1 [char](20) NOT NULL,
                                strDETfixo [char](24) NOT NULL,
                                strDET2 [char](356) NOT NULL,
								Ordem int

) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

create index PJ_ASSINADIG_idx1 on [PJ_ASSINADIG] (ordem)
-- SELECT * FROM  [PJ_ASSINADIG]
--1                                                             00000000             109                      016769/A    0104160000000118400        01N040316                 0000000000000000000                          0247344197000140KELVION INTERCAMBIADORES LTDA           RODOVIA SP-354 KM-43,5 S/N              SERRA DOS CR07803970FRANCO DA ROCHASP                                           000003

