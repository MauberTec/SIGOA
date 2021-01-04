--Select Employees 

CREATE PROCEDURE [dbo].[STP_SEL_GRUPOS] 
@gru_id int = null
AS 
  BEGIN 

  if (@gru_id is null )
      SELECT  * 
      FROM  dbo.tab_grupos 
      where gru_deletado is  null;
	else
      SELECT  * 
      FROM  dbo.tab_grupos 
      where gru_deletado is  null
		and gru_id = @gru_id;

  END ;
