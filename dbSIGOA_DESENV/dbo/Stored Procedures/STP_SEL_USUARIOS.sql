--Select Employees 

CREATE PROCEDURE [dbo].[STP_SEL_USUARIOS] 
@usu_id int = null
AS 
  BEGIN 

  if @usu_id is null
      SELECT  * 
      FROM  dbo.tab_usuarios
      where usu_deletado is  null; 
	else
      SELECT  * 
      FROM  dbo.tab_usuarios
      where usu_deletado is  null
		and usu_id = @usu_id; 
  END ;
