--Select Employees 

CREATE PROCEDURE [dbo].[STP_SEL_PERFIS] 
@per_id int = null
AS 
  BEGIN 
   if (@per_id is null )
      SELECT  * 
      FROM dbo.tab_perfis  
      where per_deletado is  null; 
	else
      SELECT  * 
      FROM dbo.tab_perfis  
      where per_deletado is  null
		and per_id = @per_id; 

  END ;
