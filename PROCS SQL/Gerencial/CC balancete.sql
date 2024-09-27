
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.SPN_BAL_GER_CUSTO @p_NUME_PLAN_CUST NUMERIC(3),  
                                                 @p_CODX_MODE      NUMERIC(5),  
                                                 @p_DATA_INIC      DATETIME,  
                                                 @p_DATA_FINA      DATETIME,   
                                                 @p_CTA_GEREN_I    VARCHAR(18),  
                                                 @p_CTA_GEREN_F    VARCHAR(18),  
                                                 @p_CTA_CUSTO_I    VARCHAR(18),  
                                                 @p_CTA_CUSTO_F    VARCHAR(18),  
                                                 @p_MODELO         VARCHAR(12),  
                                                 @p_MOVI_SALD      VARCHAR(6),  
                                                 @p_USUARIO        VARCHAR(12)   
          AS  
              
            
          DECLARE @v_USUARIO         VARCHAR(12)   
          DECLARE @v_DATA_APUR         DATETIME   
          BEGIN                    
          SELECT @v_USUARIO = '#' + @p_USUARIO    
            
          IF @p_MOVI_SALD = 'SALD'  
             BEGIN  
             SELECT @v_DATA_APUR =  ( SELECT Max(AP.DATA_APUR_SALD)    
                                        FROM APURACAO_POR_FILIAL    AP,  
                                             TMP_EMPRESA_FILIAL_N   TM  
                                       WHERE AP.CODX_CONG      = TM.CONG_TMP  
                                         AND AP.CODX_EMPR      = TM.EMPR_TMP  
                                         AND AP.CODX_FILI      = TM.FILI_TMP  
                                         AND TM.USUA_TMP       = @p_USUARIO  
                                         AND AP.DATA_APUR_SALD < @p_DATA_INIC )  
             END  
          ELSE  
             BEGIN  
             SELECT @v_DATA_APUR = @p_DATA_INIC  
             END  
            
          DELETE FROM TMP_BALANCETE_GER  
           WHERE USUA_BAL = @p_USUARIO   
            
          DELETE FROM TMP_BALANCETE_GER  
           WHERE USUA_BAL = @v_USUARIO      
          -----------------------------------------------  
          IF @p_MODELO = 'RAZAO'                                                                                          
             INSERT INTO TMP_BALANCETE_GER                                                                                        
                 SELECT  @v_USUARIO,                                                          --  USUA_BAL    
                         LI.DATA_LOTE,                                                        --  DATA_REFE_BAL      
                         @p_CODX_MODE,                                                        --  CODX_MODE_BAL                  
                         CONVERT(CHAR(18),PG.NUME_CONT_PG),                                   --  NUME_CONT_PG_BAL               
                         PG.NUME_CONT_FMTD_PG,                                                --  NUME_CONT_FMTD_PG_BAL          
                         PG.MNEM_CONT_PG_N,                                                   --  MNEM_CONT_PG_BAL               
                         CONVERT(CHAR(1),PG.GRAU_CONT_PG),                                    --  GRAU_CONT_PG_BAL               
                         PG.TIPO_CONT_PG,                                                     --  TIPO_CONT_PG_BAL               
                         PG.NATU_CONT_PG,                                                     --  NATU_CONT_PG_BAL               
                         CONVERT(CHAR(1),PG.DIGI_VERI_CONT_PG),                               --  DIGI_VERI_CONT_PG_BAL      
                         PG.NOME_CONT_PG,                                                     --  NOME_CONT_PG_BAL               
                         CASE WHEN  LI.DATA_LOTE BETWEEN  @v_DATA_APUR AND @p_DATA_INIC       --  SALD_ANTE_PG_BAL     
                              THEN  SUM(RI.VALR_LANC_CENT_CUST)  ELSE 0 END,               
 0,                                                               --  MOVI_DEBI_PG_BAL          
                         0,                                                                   --  MOVI_CRED_PG_BAL               
                         SUM(RI.VALR_LANC_CENT_CUST),                                         -- SALD_ATUA_PG_BAL               
                         0,     --  PERC_POND_PG_BAL               
                         0,    --  SALD_POND_PG_BAL               
                         CONVERT(CHAR(18),PL.NUME_CONT),                                      --  NUME_CONT_COS_BAL              
                         PL.NUME_CONT_FMTD,                                                   --  NUME_CONT_FMTD_COS_BAL         
                         PL.NOME_CONT,                                                        --  NOME_CONT_COS_BAL              
                         CONVERT(CHAR(1),PL.GRAU_CONT),                                       --  GRAU_CONT_COS_BAL    
                         CASE WHEN  LI.DATA_LOTE BETWEEN @v_DATA_APUR AND @p_DATA_INIC        --  SALD_ANTE_COS_BAL     
                              THEN  LI.VALR_LANC  ELSE 0 END,            
                         0,                                                                   --  MOVI_DEBI_COS_BAL              
                         0,                                                                   --  MOVI_CRED_COS_BAL              
                         LI.VALR_LANC,                                                        --  SALD_ATUA_COS_BAL              
                         PC.NUME_CENT_CUST_FMTD,                                              --  NUME_CONT_FMTD_CUST_BAL        
                         PC.NOME_CENT_CUST,                                                   --  NOME_CONT_CUST_BAL             
                         CONVERT(CHAR(10),LI.DATA_LOTE,103),                                  --  DATA_LANC_CUST_BAL         
                         LI.NUME_LOTE,                                                        --  LOTE_LANC_CUST_BAL         
                         LI.NUME_LINH_LOTE,                                                   --  LINH_LANC_CUST_BAL         
                         SUBSTRING(HI.TEXT_LINH_HIST_LANC,1,100)                              --  HIST_LANC_CUST_BAL             
              FROM LANCAMENTO_INFORMADO LI,                                                    
                   HISTORICO_INFORMADO  HI,                                                                                                             
                   RATEIO_INFORMADO     RI,                                                                                     
                   PLANO_GERAL          PG,                                                                                     
                   PLANO_DE_CUSTO       PC,                                                                                     
                   PLANO_CONTABIL       PL,                                                                                     
                   RELACOES             RL,  
                   TMP_EMPRESA_FILIAL_N EM                                                                                      
            WHERE PG.CODX_MODE           = RL.CODX_MODE                                                                         
              AND PG.NUME_CONT_PG        = RL.NUME_CONT_PG                                                                      
              AND LI.CODX_CONG           = RL.CODX_CONG                                                                         
              AND LI.NUME_CONT           = RL.NUME_CONT                                                                         
              AND RI.NUME_PLAN_CUST      = PC.NUME_PLAN_CUST                                                                    
              AND RI.NUME_CENT_CUST      = PC.NUME_CENT_CUST     
              AND RI.CODX_CONG         = PC.CODX_CONG        
              AND PL.CODX_CONG           = RL.CODX_CONG                                                                         
              AND PL.NUME_CONT           = RL.NUME_CONT                        
              AND LI.CODX_CONG           = RI.CODX_CONG                                                                         
              AND LI.CODX_EMPR = RI.CODX_EMPR                                                                         
              AND LI.CODX_FILI           = RI.CODX_FILI                                                                         
              AND LI.NUME_LANC           = RI.NUME_LANC     
              AND LI.CODX_CONG           = HI.CODX_CONG                                                                         
              AND LI.CODX_EMPR           = HI.CODX_EMPR                                                                         
              AND LI.CODX_FILI           = HI.CODX_FILI                                                                         
              AND LI.NUME_LANC           = HI.NUME_LANC     
              AND LI.CODX_CONG           = EM.CONG_TMP  
              AND LI.CODX_EMPR           = EM.EMPR_TMP                                                                         
              AND LI.CODX_FILI           = EM.FILI_TMP     
              AND EM.USUA_TMP            = @p_USUARIO    
              AND RI.NUME_CENT_CUST     >= CONVERT(NUMERIC(18),@p_CTA_CUSTO_I)  
              AND RI.NUME_CENT_CUST     <= CONVERT(NUMERIC(18),@p_CTA_CUSTO_F)  
              AND PG.NUME_CONT_PG       >= CONVERT(NUMERIC(18),@p_CTA_GEREN_I)  
              AND PG.NUME_CONT_PG       <= CONVERT(NUMERIC(18),@p_CTA_GEREN_F)  
              AND LI.DATA_LOTE          >  @v_DATA_APUR                         -- Maior que a apuracao  
              AND LI.DATA_LOTE          <= @p_DATA_FINA  
              AND PG.CODX_MODE           = @p_CODX_MODE  
              AND PG.TIPO_CONT_PG        = 'F'  
              AND RI.NUME_PLAN_CUST      = @p_NUME_PLAN_CUST   
             GROUP BY PG.NUME_CONT_FMTD_PG,                
                      PG.NUME_CONT_PG,                     
                      PG.TIPO_CONT_PG,                     
                      PG.MNEM_CONT_PG_N,                     
                      PG.NATU_CONT_PG,                     
                      PG.GRAU_CONT_PG,                     
                      PG.NOME_CONT_PG ,                    
                      PG.DIGI_VERI_CONT_PG,                
                      PG.PERC_POND_PG,                     
                      RL.NUME_CONT_PG,                     
                      LI.SINA_LANC,                        
                      LI.CODX_CONG,                        
                      LI.CODX_EMPR,                        
                      LI.CODX_FILI,                        
                      PC.NUME_CENT_CUST_FMTD,              
                      PG.PERC_POND_PG,                     
                      PC.NOME_CENT_CUST,                   
                      PL.NUME_CONT,                        
                      PL.NUME_CONT_FMTD,                   
                      PL.NOME_CONT,                        
                      PL.GRAU_CONT,                        
                      LI.DATA_LOTE,  
                      LI.NUME_LOTE,  
                      LI.NUME_LINH_LOTE,  
                      LI.VALR_LANC,  
                      HI.TEXT_LINH_HIST_LANC  
          -----------------------------------------------  
          IF @p_MODELO = 'ANALITICO'                                                                                          
             INSERT INTO TMP_BALANCETE_GER                                                                                        
                 SELECT  @v_USUARIO,      --  USUA_BAL       
                         CASE WHEN  LI.DATA_LOTE BETWEEN @v_DATA_APUR AND @p_DATA_INIC             --  DATA_REFE_BAL      
                              THEN  @p_DATA_INIC   
          ELSE  @p_DATA_FINA END,                      
                         @p_CODX_MODE,                                                             --  CODX_MODE_BAL                       
                         CONVERT(CHAR(18),PG.NUME_CONT_PG),                                        --  NUME_CONT_PG_BAL                    
                         PG.NUME_CONT_FMTD_PG,                                                     --  NUME_CONT_FMTD_PG_BAL               
                         PG.MNEM_CONT_PG_N,                                                        --  MNEM_CONT_PG_BAL             
                         CONVERT(CHAR(1),PG.GRAU_CONT_PG),                                         --  GRAU_CONT_PG_BAL                    
                         PG.TIPO_CONT_PG,                                                          --  TIPO_CONT_PG_BAL                    
                         PG.NATU_CONT_PG,                                                          --  NATU_CONT_PG_BAL                    
                         CONVERT(CHAR(1),PG.DIGI_VERI_CONT_PG),               --  DIGI_VERI_CONT_PG_BAL          
                         PG.NOME_CONT_PG,                                                          --  NOME_CONT_PG_BAL                    
                         CASE WHEN  LI.DATA_LOTE BETWEEN @v_DATA_APUR AND @p_DATA_INIC             --  SALD_ANTE_PG_BAL     
                              THEN  SUM(RI.VALR_LANC_CENT_CUST)  ELSE 0 END,                                 
                         0,                                                                        --  MOVI_DEBI_PG_BAL                    
                         0,                                                                        --  MOVI_CRED_PG_BAL                    
                         SUM(RI.VALR_LANC_CENT_CUST),                                              --  SALD_ATUA_PG_BAL                    
                         0,                                                                        --  PERC_POND_PG_BAL                    
                         0,                                                                        --  SALD_POND_PG_BAL                    
                         CONVERT(CHAR(18),PL.NUME_CONT),                                           --  NUME_CONT_COS_BAL                   
                         PL.NUME_CONT_FMTD,                                                        --  NUME_CONT_FMTD_COS_BAL              
                         PL.NOME_CONT,                                                             --  NOME_CONT_COS_BAL                   
                         CONVERT(CHAR(1),PL.GRAU_CONT),                                            --  GRAU_CONT_COS_BAL     
                         CASE WHEN  LI.DATA_LOTE BETWEEN @v_DATA_APUR AND @p_DATA_INIC             --  SALD_ANTE_COS_BAL     
                              THEN  SUM(RI.VALR_LANC_CENT_CUST)  ELSE 0 END,                  
                         0,                                                                        --  MOVI_DEBI_COS_BAL                   
                         0,                                                                        --  MOVI_CRED_COS_BAL                   
                         SUM(RI.VALR_LANC_CENT_CUST),                                              --  SALD_ATUA_COS_BAL                   
                         PC.NUME_CENT_CUST_FMTD,                                                   --  NUME_CONT_FMTD_CUST_BAL             
                         PC.NOME_CENT_CUST,                                                        --  NOME_CONT_CUST_BAL                  
                         CONVERT(CHAR(10),@p_DATA_FINA,103),                                       --  DATA_LANC_CUST_BAL       
          0,                                                                        --  LOTE_LANC_CUST_BAL          
                   0,                                                   --  LINH_LANC_CUST_BAL          
          ' '                                                                       --  HIST_LANC_CUST_BAL                  
              FROM LANCAMENTO_INFORMADO LI,                                                         
                   RATEIO_INFORMADO     RI,                                                                                     
       PLANO_GERAL          PG,                                                                                     
                   PLANO_DE_CUSTO       PC,                                                                                     
                   PLANO_CONTABIL       PL,               
                   RELACOES             RL,  
                   TMP_EMPRESA_FILIAL_N EM                                                                                         
            WHERE PG.CODX_MODE           = RL.CODX_MODE                                                                         
         AND PG.NUME_CONT_PG        = RL.NUME_CONT_PG                                                                      
              AND LI.CODX_CONG           = RL.CODX_CONG                                                                         
              AND LI.NUME_CONT           = RL.NUME_CONT                                                              
              AND RI.NUME_PLAN_CUST      = PC.NUME_PLAN_CUST                                                                    
              AND RI.NUME_CENT_CUST      = PC.NUME_CENT_CUST                                                                    
              AND RI.CODX_CONG           = PC.CODX_CONG                                                                         
              AND PL.CODX_CONG           = RL.CODX_CONG                                                                         
              AND PL.NUME_CONT           = RL.NUME_CONT                        
              AND LI.CODX_CONG           = RI.CODX_CONG                                                                         
              AND LI.CODX_EMPR           = RI.CODX_EMPR                                                                         
              AND LI.CODX_FILI           = RI.CODX_FILI                                                                         
              AND LI.NUME_LANC           = RI.NUME_LANC        
              AND LI.CODX_CONG           = EM.CONG_TMP  
              AND LI.CODX_EMPR           = EM.EMPR_TMP                                                                         
              AND LI.CODX_FILI           = EM.FILI_TMP     
              AND EM.USUA_TMP            = @p_USUARIO                
              AND RI.NUME_CENT_CUST     >= CONVERT(NUMERIC(18),@p_CTA_CUSTO_I)  
              AND RI.NUME_CENT_CUST     <= CONVERT(NUMERIC(18),@p_CTA_CUSTO_F)  
              AND PG.NUME_CONT_PG       >= CONVERT(NUMERIC(18),@p_CTA_GEREN_I)  
              AND PG.NUME_CONT_PG       <= CONVERT(NUMERIC(18),@p_CTA_GEREN_F)  
              AND LI.DATA_LOTE          >  @v_DATA_APUR                         -- Maior que a apuracao  
              AND LI.DATA_LOTE          <= @p_DATA_FINA  
              AND PG.CODX_MODE           = @p_CODX_MODE  
              AND PG.TIPO_CONT_PG        = 'F'  
              AND RI.NUME_PLAN_CUST      = @p_NUME_PLAN_CUST   
             GROUP BY PG.NUME_CONT_FMTD_PG,                
                      PG.NUME_CONT_PG,                     
                      PG.TIPO_CONT_PG,                     
                      PG.MNEM_CONT_PG_N,                     
                      PG.NATU_CONT_PG,                     
                      PG.GRAU_CONT_PG,                     
                      PG.NOME_CONT_PG ,                    
         PG.DIGI_VERI_CONT_PG,                
                      PG.PERC_POND_PG,                     
                     RL.NUME_CONT_PG,                     
       LI.SINA_LANC,                        
                      LI.CODX_CONG,                        
                      LI.CODX_EMPR,                        
                      LI.CODX_FILI,                        
                      PC.NUME_CENT_CUST_FMTD,              
                      PG.PERC_POND_PG,                     
                      PC.NOME_CENT_CUST,                   
                      PL.NUME_CONT,                        
                      PL.NUME_CONT_FMTD,                   
                      PL.NOME_CONT,                        
                      PL.GRAU_CONT,       
                      LI.DATA_LOTE   
            
          -----------------------------------------------  
          IF @p_MODELO = 'SINTETICO'                                                                                          
             INSERT INTO TMP_BALANCETE_GER                                                                                        
                 SELECT  @v_USUARIO,                                                              --  USUA_BAL      
                         CASE WHEN  LI.DATA_LOTE BETWEEN @v_DATA_APUR AND @p_DATA_INIC               --  DATA_REFE_BAL         
                              THEN  @p_DATA_INIC   
                              ELSE  @p_DATA_FINA END,                     
                         @p_CODX_MODE,                                                                --  CODX_MODE_BAL                    
                         CONVERT(CHAR(18),PG.NUME_CONT_PG),                                           --  NUME_CONT_PG_BAL                 
                         PG.NUME_CONT_FMTD_PG,                                                        --  NUME_CONT_FMTD_PG_BAL            
                         PG.MNEM_CONT_PG_N,                                                           --  MNEM_CONT_PG_BAL                 
                         CONVERT(CHAR(1),PG.GRAU_CONT_PG),                                            --  GRAU_CONT_PG_BAL                 
                         PG.TIPO_CONT_PG,                                                             --  TIPO_CONT_PG_BAL                 
                         PG.NATU_CONT_PG,                                                             --  NATU_CONT_PG_BAL                 
                         CONVERT(CHAR(1),PG.DIGI_VERI_CONT_PG),                                       --  DIGI_VERI_CONT_PG_BAL       
                         PG.NOME_CONT_PG,                                                             --  NOME_CONT_PG_BAL     
                         CASE WHEN  LI.DATA_LOTE BETWEEN @v_DATA_APUR AND @p_DATA_INIC                --  SALD_ANTE_PG_BAL     
                              THEN  SUM(RI.VALR_LANC_CENT_CUST)  ELSE 0 END,     
                         0,                                                                           --  MOVI_DEBI_PG_BAL                 
                         0,                                                                           --  MOVI_CRED_PG_BAL                 
                         SUM(RI.VALR_LANC_CENT_CUST),                                                 --  SALD_ATUA_PG_BAL                 
                         0,                                                                           --  PERC_POND_PG_BAL                 
                         0,                                                                           --  SALD_POND_PG_BAL                 
                         0,                                                                           --  NUME_CONT_COS_BAL                
                         ' ',                                                                         --  NUME_CONT_FMTD_COS_BAL           
                         ' ',                                              --  NOME_CONT_COS_BAL                
                         ' ', --  GRAU_CONT_COS_BAL    
                         CASE WHEN  LI.DATA_LOTE BETWEEN @v_DATA_APUR AND @p_DATA_INIC                --  SALD_ANTE_COS_BAL     
                              THEN  SUM(LI.VALR_LANC)  ELSE 0 END,           
                         0,                                                                           --  MOVI_DEBI_COS_BAL                
                         0,                                                                           --  MOVI_CRED_COS_BAL                
                         SUM(LI.VALR_LANC),                                                           --  SALD_ATUA_COS_BAL                
                         PC.NUME_CENT_CUST_FMTD,                   --  NUME_CONT_FMTD_CUST_BAL          
                         PC.NOME_CENT_CUST,                                                           --  NOME_CONT_CUST_BAL               
                         CONVERT(CHAR(10),@p_DATA_FINA,103),                                          --  DATA_LANC_CUST_BAL         
                         0,                                                                           --  LOTE_LANC_CUST_BAL         
                       0,                                                                           --  LINH_LANC_CUST_BAL         
                         ' '                                                                          --  HIST_LANC_CUST_BAL               
              FROM LANCAMENTO_INFORMADO LI,                                                                                          
                   RATEIO_INFORMADO     RI,       
                   PLANO_GERAL          PG,                                                                                     
                   PLANO_DE_CUSTO       PC,                                                           
                   RELACOES             RL,  
                   TMP_EMPRESA_FILIAL_N EM                                                                                         
            WHERE PG.CODX_MODE           = RL.CODX_MODE                                      
              AND PG.NUME_CONT_PG        = RL.NUME_CONT_PG                                                                      
              AND LI.CODX_CONG           = RL.CODX_CONG                                                                         
              AND LI.NUME_CONT           = RL.NUME_CONT                                                                         
              AND RI.NUME_PLAN_CUST      = PC.NUME_PLAN_CUST                                                                    
              AND RI.NUME_CENT_CUST      = PC.NUME_CENT_CUST                                                                    
              AND RI.CODX_CONG           = PC.CODX_CONG     
              AND LI.CODX_CONG           = RI.CODX_CONG                                                                         
              AND LI.CODX_EMPR           = RI.CODX_EMPR                                                                         
              AND LI.CODX_FILI           = RI.CODX_FILI                                                                         
              AND LI.NUME_LANC           = RI.NUME_LANC        
              AND LI.CODX_CONG           = EM.CONG_TMP  
              AND LI.CODX_EMPR           = EM.EMPR_TMP                                                                         
              AND LI.CODX_FILI           = EM.FILI_TMP     
              AND EM.USUA_TMP            = @p_USUARIO                
              AND RI.NUME_CENT_CUST     >= CONVERT(NUMERIC(18),@p_CTA_CUSTO_I)  
              AND RI.NUME_CENT_CUST     <= CONVERT(NUMERIC(18),@p_CTA_CUSTO_F)  
              AND PG.NUME_CONT_PG       >= CONVERT(NUMERIC(18),@p_CTA_GEREN_I)  
    AND PG.NUME_CONT_PG       <= CONVERT(NUMERIC(18),@p_CTA_GEREN_F)  
              AND LI.DATA_LOTE          >  @v_DATA_APUR        -- Maior que a apuracao  
              AND LI.DATA_LOTE     <= @p_DATA_FINA  
              AND PG.CODX_MODE           = @p_CODX_MODE  
              AND PG.TIPO_CONT_PG        = 'F'  
              AND RI.NUME_PLAN_CUST      = @p_NUME_PLAN_CUST   
             GROUP BY PG.NUME_CONT_FMTD_PG,                
                      PG.NUME_CONT_PG,                     
                      PG.TIPO_CONT_PG,                     
                      PG.MNEM_CONT_PG_N,                     
                      PG.NATU_CONT_PG,                     
                      PG.GRAU_CONT_PG,                     
                      PG.NOME_CONT_PG ,                    
                      PG.DIGI_VERI_CONT_PG,                
                      PG.PERC_POND_PG,                     
                      RL.NUME_CONT_PG,                     
                      LI.SINA_LANC,                        
         LI.CODX_CONG,                        
                      LI.CODX_EMPR,                        
                      LI.CODX_FILI,                        
                      PC.NUME_CENT_CUST_FMTD,              
                      PG.PERC_POND_PG,                     
                      PC.NOME_CENT_CUST,    
                      LI.DATA_LOTE   
          -----------------------------------------------  
          IF @p_MODELO = 'AGLUTINADO'                                                                                          
             INSERT INTO TMP_BALANCETE_GER                                                                                        
                 SELECT  @v_USUARIO,                                                                 --  USUA_BAL    
                         CASE WHEN  LI.DATA_LOTE BETWEEN @v_DATA_APUR AND @p_DATA_INIC               --  DATA_REFE_BAL         
                              THEN  @p_DATA_INIC   
                              ELSE  @p_DATA_FINA END,    
                         @p_CODX_MODE,                                                               --  CODX_MODE_BAL                         
                         CONVERT(CHAR(18),PG.NUME_CONT_PG),                                          --  NUME_CONT_PG_BAL                           
                         PG.NUME_CONT_FMTD_PG,                                                       --  NUME_CONT_FMTD_PG_BAL                      
                         PG.MNEM_CONT_PG_N,                                                          --  MNEM_CONT_PG_BAL                           
                         CONVERT(CHAR(1),PG.GRAU_CONT_PG),    --  GRAU_CONT_PG_BAL                           
                         PG.TIPO_CONT_PG,                                                            --  TIPO_CONT_PG_BAL                           
                         PG.NATU_CONT_PG,                                                            --  NATU_CONT_PG_BAL                           
                         CONVERT(CHAR(1),PG.DIGI_VERI_CONT_PG),                                      --  DIGI_VERI_CONT_PG_BAL                 
                         PG.NOME_CONT_PG,                                                            --  NOME_CONT_PG_BAL    
                         CASE WHEN  LI.DATA_LOTE BETWEEN @v_DATA_APUR AND @p_DATA_INIC               --  SALD_ANTE_PG_BAL     
                              THEN  SUM(RI.VALR_LANC_CENT_CUST)  ELSE 0 END,     
                         0,                                                                          --  MOVI_DEBI_PG_BAL                     
                         0,                                                                          --  MOVI_CRED_PG_BAL                     
                         SUM(RI.VALR_LANC_CENT_CUST),                                                --  SALD_ATUA_PG_BAL                     
0,                                                                          --  PERC_POND_PG_BAL                     
                         0,                                                   --  SALD_POND_PG_BAL                     
                         0,   --  NUME_CONT_COS_BAL                    
                         ' ',                                                                        --  NUME_CONT_FMTD_COS_BAL               
                         ' ',                                                                        --  NOME_CONT_COS_BAL                    
                         ' ',                                                                        --  GRAU_CONT_COS_BAL                    
                         0,                                                                          --  SALD_ANTE_COS_BAL                    
                         0,                                                                          --  MOVI_DEBI_COS_BAL                    
                         0,              --  MOVI_CRED_COS_BAL                    
                         0,                                                                          --  SALD_ATUA_COS_BAL                    
                         ' ',                                                                        --  NUME_CONT_FMTD_CUST_BAL              
                         ' ',                                                                        --  NOME_CONT_CUST_BAL      
                         CONVERT(CHAR(10),@p_DATA_FINA,103),                                         --  DATA_LANC_CUST_BAL       
                         0,                                                                --  LOTE_LANC_CUST_BAL          
                         0,                                                                          --  LINH_LANC_CUST_BAL          
                         ' '                                                                         --  HIST_LANC_CUST_BAL                   
              FROM LANCAMENTO_INFORMADO LI,                                                          
                   RATEIO_INFORMADO     RI,                                                                                     
                   PLANO_GERAL          PG,                                                                                     
                   PLANO_DE_CUSTO       PC,                                                           
                   RELACOES             RL,  
                   TMP_EMPRESA_FILIAL_N EM                                                                                         
            WHERE PG.CODX_MODE           = RL.CODX_MODE                                                                         
              AND PG.NUME_CONT_PG        = RL.NUME_CONT_PG                                      
              AND LI.CODX_CONG           = RL.CODX_CONG                                                                         
              AND LI.NUME_CONT           = RL.NUME_CONT                                                                         
              AND RI.NUME_PLAN_CUST      = PC.NUME_PLAN_CUST                                                                    
              AND RI.NUME_CENT_CUST      = PC.NUME_CENT_CUST                                                                    
              AND RI.CODX_CONG           = PC.CODX_CONG     
              AND LI.CODX_CONG           = RI.CODX_CONG                                                                         
              AND LI.CODX_EMPR           = RI.CODX_EMPR                                                                         
              AND LI.CODX_FILI           = RI.CODX_FILI                                                                         
              AND LI.NUME_LANC           = RI.NUME_LANC        
              AND LI.CODX_CONG           = EM.CONG_TMP  
              AND LI.CODX_EMPR           = EM.EMPR_TMP                            
              AND LI.CODX_FILI           = EM.FILI_TMP     
              AND EM.USUA_TMP            = @p_USUARIO                
              AND RI.NUME_CENT_CUST     >= CONVERT(NUMERIC(18),@p_CTA_CUSTO_I)  
              AND RI.NUME_CENT_CUST     <= CONVERT(NUMERIC(18),@p_CTA_CUSTO_F)  
              AND PG.NUME_CONT_PG       >= CONVERT(NUMERIC(18),@p_CTA_GEREN_I)  
              AND PG.NUME_CONT_PG       <= CONVERT(NUMERIC(18),@p_CTA_GEREN_F)  
            AND LI.DATA_LOTE          >  @v_DATA_APUR                         -- Maior que a apuracao  
              AND LI.DATA_LOTE          <= @p_DATA_FINA  
              AND PG.CODX_MODE           = @p_CODX_MODE  
              AND PG.TIPO_CONT_PG        = 'F'  
              AND RI.NUME_PLAN_CUST      = @p_NUME_PLAN_CUST   
             GROUP BY PG.NUME_CONT_FMTD_PG,                
                      PG.NUME_CONT_PG,                     
                      PG.TIPO_CONT_PG,                     
                      PG.MNEM_CONT_PG_N,                     
                      PG.NATU_CONT_PG,                     
                      PG.GRAU_CONT_PG,                   
                      PG.NOME_CONT_PG ,                    
                      PG.DIGI_VERI_CONT_PG,                
                      PG.PERC_POND_PG,                     
                      RL.NUME_CONT_PG,                     
                      LI.SINA_LANC,                        
                      LI.CODX_CONG,                        
                      LI.CODX_EMPR,                        
                      LI.CODX_FILI,    
                      PG.PERC_POND_PG,      
                      LI.DATA_LOTE     
          ----------------------------------------------- Agrupa por data  
          IF @p_MODELO = 'RAZAO'       
             INSERT INTO TMP_BALANCETE_GER                            
                 SELECT  @p_USUARIO               ,    
                         DATA_REFE_BAL            ,      
                         CODX_MODE_BAL            ,     
                         NUME_CONT_PG_BAL         ,                
                         NUME_CONT_FMTD_PG_BAL    ,                
                         MNEM_CONT_PG_BAL         ,                
                         GRAU_CONT_PG_BAL         ,                
                         TIPO_CONT_PG_BAL         ,                
                         NATU_CONT_PG_BAL         ,                
                         DIGI_VERI_CONT_PG_BAL    ,                
                         NOME_CONT_PG_BAL         ,     
                         SUM(SALD_ANTE_PG_BAL)    ,                       
                         SUM(MOVI_DEBI_PG_BAL)    ,                   
                         SUM(MOVI_CRED_PG_BAL)    ,                  
                         SUM(SALD_ATUA_PG_BAL)    ,     
                         SUM(PERC_POND_PG_BAL)    ,                    
                         SUM(SALD_POND_PG_BAL)    ,                   
                         NUME_CONT_COS_BAL        ,                
                         NUME_CONT_FMTD_COS_BAL   ,                
                         NOME_CONT_COS_BAL        ,                
                         GRAU_CONT_COS_BAL        ,                
                         SUM(SALD_ANTE_COS_BAL)   ,               
                         SUM(MOVI_DEBI_COS_BAL)   ,               
                         SUM(MOVI_CRED_COS_BAL)   ,            
                         SUM(SALD_ATUA_COS_BAL)   ,              
                         NUME_CONT_FMTD_CUST_BAL  ,                
                         NOME_CONT_CUST_BAL       ,                
                         DATA_LANC_CUST_BAL       ,              
                         LOTE_LANC_CUST_BAL       ,                
                         LINH_LANC_CUST_BAL       ,         
                         HIST_LANC_CUST_BAL                        
                    FROM TMP_BALANCETE_GER                         
                   WHERE USUA_BAL = @v_USUARIO  
                GROUP BY DATA_REFE_BAL,  
                         CODX_MODE_BAL            ,                
            NUME_CONT_PG_BAL         ,                
                         NUME_CONT_FMTD_PG_BAL    ,          
                         MNEM_CONT_PG_BAL         ,                
                         GRAU_CONT_PG_BAL         ,                
                         TIPO_CONT_PG_BAL         ,                
                         NATU_CONT_PG_BAL         ,                
                         DIGI_VERI_CONT_PG_BAL    ,                
                    NOME_CONT_PG_BAL         ,     
                         NUME_CONT_COS_BAL        ,                
                         NUME_CONT_FMTD_COS_BAL   ,                
                         NOME_CONT_COS_BAL        ,                
                         GRAU_CONT_COS_BAL        ,    
                         NUME_CONT_FMTD_CUST_BAL  ,                
                         NOME_CONT_CUST_BAL       ,   
                         DATA_LANC_CUST_BAL       ,  
                         LOTE_LANC_CUST_BAL       ,                
                         LINH_LANC_CUST_BAL       ,                
                         HIST_LANC_CUST_BAL     
          ELSE       
             INSERT INTO TMP_BALANCETE_GER                            
                 SELECT  @p_USUARIO               ,    
                       @p_DATA_FINA             ,      
                         CODX_MODE_BAL            ,     
                         NUME_CONT_PG_BAL         ,                
                         NUME_CONT_FMTD_PG_BAL    ,                
                         MNEM_CONT_PG_BAL         ,                
                         GRAU_CONT_PG_BAL         ,                
                         TIPO_CONT_PG_BAL         ,                
                         NATU_CONT_PG_BAL         ,                
                         DIGI_VERI_CONT_PG_BAL    ,                
                         NOME_CONT_PG_BAL         ,     
                         SUM(SALD_ANTE_PG_BAL)    ,                       
                         SUM(MOVI_DEBI_PG_BAL)    ,                   
                         SUM(MOVI_CRED_PG_BAL)    ,                  
                         SUM(SALD_ATUA_PG_BAL)    ,   
                         SUM(PERC_POND_PG_BAL)    ,                    
                         SUM(SALD_POND_PG_BAL)    ,                   
                         NUME_CONT_COS_BAL        ,                
                         NUME_CONT_FMTD_COS_BAL   ,                
                         NOME_CONT_COS_BAL        ,                
                         GRAU_CONT_COS_BAL        ,                
                         SUM(SALD_ANTE_COS_BAL)   ,               
                         SUM(MOVI_DEBI_COS_BAL)   ,               
                         SUM(MOVI_CRED_COS_BAL)   ,               
                         SUM(SALD_ATUA_COS_BAL)   ,              
                         NUME_CONT_FMTD_CUST_BAL  ,                
                         NOME_CONT_CUST_BAL       ,                
                         DATA_LANC_CUST_BAL       ,              
                         LOTE_LANC_CUST_BAL       ,    
        
                         LINH_LANC_CUST_BAL       ,                
                         HIST_LANC_CUST_BAL                        
                    FROM TMP_BALANCETE_GER                         
                   WHERE USUA_BAL = @v_USUARIO  
                GROUP BY CODX_MODE_BAL            ,                
                         NUME_CONT_PG_BAL         ,                
                         NUME_CONT_FMTD_PG_BAL    ,                
                         MNEM_CONT_PG_BAL         ,                
                         GRAU_CONT_PG_BAL         ,                
                      TIPO_CONT_PG_BAL         ,                
                         NATU_CONT_PG_BAL         ,                
                         DIGI_VERI_CONT_PG_BAL    ,                
                         NOME_CONT_PG_BAL         ,     
                         NUME_CONT_COS_BAL        ,        
                    NUME_CONT_FMTD_COS_BAL   ,                
                         NOME_CONT_COS_BAL        ,                
                         GRAU_CONT_COS_BAL        ,    
                         NUME_CONT_FMTD_CUST_BAL  ,                
                         NOME_CONT_CUST_BAL       ,   
                         DATA_LANC_CUST_BAL       ,  
                         LOTE_LANC_CUST_BAL       ,                
                         LINH_LANC_CUST_BAL       ,                
                         HIST_LANC_CUST_BAL     
          -------------------------------------------------------------------------------------  
          IF (@p_MODELO = 'SINTETICO') or    
             (@p_MODELO = 'ANALITICO')   
             BEGIN  
             UPDATE TMP_BALANCETE_GER  
                SET MOVI_DEBI_PG_BAL =   
                       (SELECT SUM(SALD_ANTE_PG_BAL)   
                          FROM TMP_BALANCETE_GER  
                         WHERE USUA_BAL = @p_USUARIO  
                        GROUP BY  NUME_CONT_FMTD_PG_BAL)  
             WHERE USUA_BAL = @p_USUARIO  
             UPDATE TMP_BALANCETE_GER  
                SET MOVI_CRED_PG_BAL =   
                       (SELECT SUM(SALD_ATUA_PG_BAL)   
                          FROM TMP_BALANCETE_GER  
                         WHERE USUA_BAL = @p_USUARIO  
                        GROUP BY  NUME_CONT_FMTD_PG_BAL)  
             WHERE USUA_BAL = @p_USUARIO   
             END  
          ------------------------------------------------------------------------------------   
    --DELETE FROM TMP_BALANCETE_GER  
          -- WHERE USUA_BAL = @v_USUARIO      
             
          END