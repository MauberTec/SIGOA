--Select Employees 

create PROCEDURE [dbo].[STP_SEL_OBJETOS_ID] 
@obj_id int 

AS 
BEGIN 
			select 
					*
			from dbo.tab_objetos
				where (obj_id = @obj_id)
			order by obj_codigo;

  END ;
