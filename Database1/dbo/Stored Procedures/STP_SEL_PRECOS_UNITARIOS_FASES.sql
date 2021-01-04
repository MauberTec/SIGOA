CREATE PROCEDURE [dbo].[STP_SEL_PRECOS_UNITARIOS_FASES] 
AS 
  BEGIN 

			select *
			from  dbo.tab_tpu_fases
			order by fas_id asc;

  END ;
