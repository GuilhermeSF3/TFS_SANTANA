use netfactor
declare @p1 int
set @p1=1
--exec sp_prepare @p1 output,N'',N'  

SELECT   
case when  (ingressoStruc.ingAceite = 1 ) 
then sum(ingValordeface) else 0 end  as totalAceite , 
case when  (ingressoStruc.ingDocumentoFisico = 1 ) 
then sum(ingValordeface) else 0 end  as totalDoc , 
-- canhoto
case when  (ingressoStruc.ingCanhoto NOT IN (''A'',''N''))  
then sum(ingValordeface) else 0 end  as totalCanhoto ,  
case when  (ingressoStruc.ingAr NOT IN (''A''))  
then sum(ingValordeface) else 0 end  as totalAr ,  
-- checagem
case when  (netTipoConfirmacao.tcoSituacao = 1 OR netTipoConfirmacao.tcoSituacao = 3 
		OR netTipoConfirmacao.tcoSituacao = 5)  
		then sum(ingValordeface) else 0 end  as totalConfirmacao , 
case when  (ingressoStruc.ingNotaFiscal NOT IN (''A''))    
  then sum(ingValordeface) else 0 end  as totalNota,  
case when  (ingressoStruc.ingExpMercadoria NOT IN (''A''))    
  then sum(ingValordeface) else 0 end  as totalExpMercadoria, 
 case when  (ingressoStruc.ingMinuta NOT IN (''A''))    
  then sum(ingValordeface) else 0 end  as totalMinuta,  
case when  (ingressoStruc.ingOrdemColeta NOT IN (''A'')) 
     then sum(ingValordeface) else 0 end  as totalOrdemColeta,  
case when  (ingressoStruc.ingEntMercadoria NOT IN (''A''))      
then sum(ingValordeface) else 0 end  as totalEntMercadoria  
FROM nfIngressos (NOLOCK) as ingressoStruc 
Inner Join nfIdentificadorGlobal idg (NOLOCK) on idg.idgCodigo = ingressoStruc.idgCodigo 
	AND idg.idgConsideraDocFaltante =  1  
LEFT OUTER JOIN netTipoConfirmacao  (NOLOCK) 
		ON CAST(netTipoConfirmacao.tco_id AS VARCHAR) = ingressoStruc.ingConfirmacaoTipo  WHERE 1 = 1  
AND (  (ingressoStruc.ingAceite =   1  ) OR (ingressoStruc.ingDocumentoFisico =   1   )
 OR (ingressoStruc.ingEntMercadoria NOT IN (''A'')  ) OR (ingressoStruc.ingExpMercadoria NOT IN (''A'')  ) 
 OR (ingressoStruc.ingNotaFiscal NOT IN (''A'')  ) 
 OR (ingressoStruc.ingCanhoto NOT IN (''A'',''N'') ) 
 OR (ingressoStruc.ingAr NOT IN (''A'') ) 
 OR (ingressoStruc.ingMinuta NOT IN (''A'') ) 
 OR (ingressoStruc.ingOrdemColeta NOT IN (''A'') )  
 OR(netTipoConfirmacao.tcoSituacao = 1 
 OR netTipoConfirmacao.tcoSituacao = 3 OR netTipoConfirmacao.tcoSituacao = 5))
   AND ingressoStruc.empCodigo = 2 
   AND ingressoStruc.cedCodigo =   84   -- 1 CEDENTE
   AND ingressoStruc.ingDataLiquidacao is null 
group by netTipoConfirmacao.tcoSituacao, ingressoStruc.ingAceite,
     ingressoStruc.ingDocumentoFisico, ingressoStruc.ingCanhoto , 
     ingressoStruc.ingAr, ingressoStruc.ingConfirmacaoTipo, 
     ingressoStruc.ingMinuta, ingressoStruc.ingOrdemColeta, 
     ingressoStruc.ingNotaFiscal, ingressoStruc.ingEntMercadoria,
     ingressoStruc.ingExpMercadoria 

-- ',1 select @p1