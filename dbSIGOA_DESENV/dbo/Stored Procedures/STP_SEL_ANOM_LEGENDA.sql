create PROCEDURE [dbo].[STP_SEL_ANOM_LEGENDA] 
@leg_id int=null
AS 
  BEGIN 

      SELECT  * 
      FROM  dbo.tab_anomalia_legendas 
      where leg_deletado is  null
		and leg_id = case when @leg_id is null then leg_id else @leg_id end;

  END ;
