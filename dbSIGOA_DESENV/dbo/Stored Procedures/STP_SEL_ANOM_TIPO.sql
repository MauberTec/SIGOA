create PROCEDURE [dbo].[STP_SEL_ANOM_TIPO] 
@atp_id int=null
AS 
  BEGIN 

      SELECT  * 
      FROM  dbo.tab_anomalia_tipos 
      where atp_deletado is  null
		and atp_id = case when @atp_id is null then atp_id else @atp_id end;

  END ;
