CREATE PROCEDURE [dbo].[STP_SEL_PRECOS_UNITARIOS_DATAS_BASES] 
AS 
  BEGIN 

	select	CONVERT(varchar(20),tpu_data_base_der,103)  AS tpu_data_base_der
	from (
			select distinct  tpu_data_base_der
			from dbo.tab_tpu_precos_unitarios 
	) tb1
	order by 1 desc;

  END ;
