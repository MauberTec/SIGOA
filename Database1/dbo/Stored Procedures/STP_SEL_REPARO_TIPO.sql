CREATE PROCEDURE [dbo].[STP_SEL_REPARO_TIPO] 
@rpt_id int=null
AS 
  BEGIN 

      SELECT  * 
      FROM  dbo.tab_reparo_tipos 
      where rpt_deletado is  null
		and rpt_id = case when @rpt_id is null then rpt_id else @rpt_id end;

  END ;
