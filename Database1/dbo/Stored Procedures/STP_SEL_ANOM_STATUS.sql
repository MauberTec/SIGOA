create PROCEDURE [dbo].[STP_SEL_ANOM_STATUS] 
@ast_id int=null
AS 
  BEGIN 

      SELECT  * 
      FROM  dbo.tab_anomalia_status 
      where ast_deletado is  null
		and ast_id = case when @ast_id is null then ast_id else @ast_id end;

  END ;
