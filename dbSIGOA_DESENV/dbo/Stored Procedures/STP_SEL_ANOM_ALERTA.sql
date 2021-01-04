create PROCEDURE [dbo].[STP_SEL_ANOM_ALERTA] 
@ale_id int=null
AS 
  BEGIN 

      SELECT  * 
      FROM  dbo.tab_anomalia_alertas 
      where ale_deletado is  null
		and ale_id = case when @ale_id is null then ale_id else @ale_id end;

  END ;
