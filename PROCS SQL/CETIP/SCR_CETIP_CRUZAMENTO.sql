USE SIG
GO

IF EXISTS (SELECT name FROM sysobjects 
         WHERE name = 'SCR_cetip_cruzamento' AND type = 'P')
   DROP PROCEDURE [dbo].[SCR_cetip_cruzamento] 
GO

CREATE Procedure [dbo].[SCR_cetip_cruzamento]   
 (@DTREF  SMALLDATETIME, @DTATE  SMALLDATETIME,@AGENTE VARCHAR(20) ) AS    
  
BEGIN  
  
  
/*   
DROP   table cetip_cruza  
create table cetip_cruza (  
COD_LOJA varchar(10),   
CGC_LOJA varchar(20),  
NOME_LOJA varchar(100),    
COD_OPERADOR varchar(20),  
NOME_OPERADOR varchar(100),    
COD_AGENTE varchar(20),  
NOME_AGENTE varchar(100),     
NUMERO_PROPOSTA varchar(20),  
NUMERO_OPERACAO varchar(20),  
PLANO int,   
NOME_CLIENTE varchar(100),  
CPF_CLIENTE varchar(20),    
PRODUTO varchar(20),  
COD_TABELA varchar(20),  
DT_CADASTRO datetime,   
DT_BASE dateTIME,   
VL_FINANCIADO FLOAT,   
VL_LIBERADO  FLOAT,    
FINALIZADO  varchar(20),   
SITUACAO varchar(20),  
MODELO_PROPOSTA varchar(20),  
ANO_FAB_POPOSTA varchar(20),  
ANO_MOD_PROPOSTA varchar(20),  
CNPJ_LOJA_PAGA  varchar(20),  
VLR_APROV_PAGO FLOAT,   
MARCA_PAGO  varchar(100),  
ANO_MODELO_PAGO varchar(20),  
ANO_FABRICACAO_PAGO   varchar(20),  
PRAZO_PAGO           varchar(20),  
COD_FIPE varchar(20),  
VALOR_FIPE_PAGO FLOAT,  
CNPJ_AGENTE_FINANCEIRO  varchar(20),  
NOME_AGENTE_FINANCEIRO varchar(100),  
PAGO_LOJA_PROPOSTA   varchar(20)  
)  
  
*/  
  
DELETE cetip_cruza  
INSERT cetip_cruza  
select distinct  ppcodorg4 AS COD_LOJA,   
                 O4CPFCGC  AS CGC_LOJA,  
                 O4NOME    AS NOME_LOJA,    
                 ppCODORG6  AS COD_OPERADOR,   
     O6DESCR   AS NOME_OPERADOR,  
     ISNULL(PpCODORG3,'') AS COD_AGENTE,   
                 ISNULL(O3DESCR,'')  AS NOME_AGENTE,                   
                 ppnrprop  AS NUMERO_PROPOSTA,  
                 ISNULL(ppnroper,'')  AS NUMERO_OPERACAO,  
                 ISNULL(PPQTDDPARC,0) AS PLANO,   
                 ISNULL(ppnomecli,'') AS NOME_CLIENTE,   
                 ISNULL(ppcgc,'')     AS CPF_CLIENTE,                    
     PPCODPRD  AS PRODUTO,   
                 ISNULL(PPCODTAB,'')  AS COD_TABELA,   
                 ppdtcad   AS DT_CADASTRO,   
     ppdtbase  AS DT_BASE,   
                 ISNULL(PPVLROPCZ,0.0) AS VL_FINANCIADO,   
                 ISNULL(ppvlrliq1,0.0) AS VL_LIBERADO,                    
     pppropfim AS FINALIZADO,   
                 mpsit     AS SITUACAO,  
                   
     ISNULL(ABMODELO,'')  AS MODELO_PROPOSTA,  
                 ISNULL(ABANOFAB,'')  AS ANO_FAB_POPOSTA,  
                 ISNULL(ABANOMOD,'')  AS ANO_MOD_PROPOSTA  ,   
    '','','','','',  0,'',0.0,'','' , ''  
FROM  propwebsmc..cprop (NOLOCK)  
LEFT JOIN   propwebsmc..cmovp (NOLOCK)  
 ON  ppnrprop = mpnrprop  
LEFT JOIN   PROPWEBSMC..cclip  (NOLOCK)  
 ON  CLCODCLI = PPCODCLI   
LEFT JOIN   PROPWEBSMC..CBFIP  (NOLOCK)  
 ON  ABNRPROP = ppnrprop   
LEFT JOIN   CDCSANTANAMicroCredito..torg4 (NOLOCK)  
 ON  O4CODORG = ppcodorg4  
LEFT JOIN   CDCSANTANAMicroCredito..torg3 (NOLOCK)  
 ON  O3CODORG = PPCODORG3  
LEFT JOIN   CDCSANTANAMicroCredito..Torg6 (NOLOCK)  
 ON  O6CODORG = ppCODORG6  
             
WHERE   
  ppdtbase BETWEEN  @DTREF AND @DTATE   
AND 1= 0+ CASE WHEN @AGENTE='' THEN 1  
       WHEN @AGENTE<>'' AND  ppCODORG3 =@AGENTE THEN 1  
       ELSE 0 END  

UPDATE CETIP_CRUZA  
SET  CNPJ_LOJA_PAGA =  ISNULL(CONVERT(VARCHAR(20),CETIP.CNPJ_LOJA),'') ,  
  VLR_APROV_PAGO =  ISNULL(CETIP.VLR_APROV,0.0),   
        MARCA_PAGO    =  ISNULL(CETIP.MARCA,'')     ,  
        ANO_MODELO_PAGO=  ISNULL(CONVERT(VARCHAR(20),CETIP.ANO_MODELO),'') ,  
  ANO_FABRICACAO_PAGO =  ISNULL(CONVERT(VARCHAR(20),CETIP.ANO_FABRIC),''),  
                 PRAZO_PAGO =  ISNULL(CONVERT(VARCHAR(20),CETIP.OP_PRAZO),'')  ,                     
                 COD_FIPE = ISNULL(CETIP.COD_FIPE,'')    ,  
                 VALOR_FIPE_PAGO = ISNULL(CETIP.VLR_FIPE,0.0)    ,  
     CNPJ_AGENTE_FINANCEIRO   = ISNULL(CONVERT(VARCHAR(20),CETIP.CNPJ_AGENTE),'')   ,  
     NOME_AGENTE_FINANCEIRO   = ISNULL(CETIP.NOM_AGENTE,'')   ,  
                 PAGO_LOJA_PROPOSTA  =  ISNULL(CETIP.PAGO_LOJA ,'')     
FROM SIG..RT_CONVERSAO CETIP  (NOLOCK)   
WHERE 
    case when len(ltrim(rtrim(CETIP_CRUZA.CPF_CLIENTE))) = 14 then  

   '000'+right(substring(CETIP_CRUZA.CPF_CLIENTE,1,3)+substring(CETIP_CRUZA.CPF_CLIENTE,5,3)+substring(CETIP_CRUZA.CPF_CLIENTE,9,3), 9)  
    else  
   right(substring(CETIP_CRUZA.CPF_CLIENTE,1,2)+substring(CETIP_CRUZA.CPF_CLIENTE,4,3)+substring(CETIP_CRUZA.CPF_CLIENTE,8,3)+substring(CETIP_CRUZA.CPF_CLIENTE,12,4),12 )  
    end +  
    right('00'+ltrim(rtrim(CETIP_CRUZA.CPF_CLIENTE)),2) = CETIP.NUM_CPF   
 AND CETIP.DT_CONTRATO = ISNULL((SELECT MAX(DT_CONTRATO)  FROM SIG..RT_CONVERSAO C2  (NOLOCK)   
     WHERE CETIP.NUM_CPF = C2.NUM_CPF  
      AND C2.DT_CONTRATO>=@DTREF),'')  
  

UPDATE CETIP_CRUZA  
SET  CNPJ_LOJA_PAGA =  CGC_LOJA ,  
  VLR_APROV_PAGO =  VL_LIBERADO,   
  
        MARCA_PAGO    =  MODELO_PROPOSTA     ,  
        ANO_MODELO_PAGO=  ANO_MOD_PROPOSTA ,  
  ANO_FABRICACAO_PAGO =  ANO_FAB_POPOSTA,  
  
                 PRAZO_PAGO =  PLANO  ,                     
     CNPJ_AGENTE_FINANCEIRO   = 5503849000100 ,  
     NOME_AGENTE_FINANCEIRO   = 'SANTANA SA CRED FIN INV'  ,  
                 PAGO_LOJA_PROPOSTA  =  'S'     
WHERE SITUACAO = 'INT' AND CNPJ_AGENTE_FINANCEIRO =''  
  
  UPDATE  CETIP_CRUZA  
   SET  --COD_FIPE  
     VALOR_FIPE_PAGO = convert(float,VEIC.abvlrtab)  
   FROM  (SELECT  coper.opnroper, coper.OPQTDDPARC, coper.opvlrfin, cbfin.abmodelo,  
        cbfin.abanomod,cbfin.abvlrtab  
       from CdcSantanaMicroCredito..COper coper (NOLOCK)  
       inner join CdcSantanaMicroCredito..cbfin  (NOLOCK)  
       on coper.opnroper = cbfin.abnroper   
       ) AS VEIC  
   WHERE CETIP_CRUZA.NUMERO_OPERACAO  = OPNROPER  
   AND SITUACAO = 'INT' AND CNPJ_AGENTE_FINANCEIRO =''  
  
  
SELECT * FROM CETIP_CRUZA (NOLOCK)  
   
END  