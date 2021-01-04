CREATE PROCEDURE [dbo].[STP_SEL_OBJ_CLASSES] 
@clo_id int = null
AS 
  BEGIN 

      SELECT  * 
      FROM  dbo.tab_objeto_classes
	  where clo_deletado is null
		and  clo_id = case when @clo_id is not null then @clo_id else clo_id end
	  order by clo_id; 

  END ;
