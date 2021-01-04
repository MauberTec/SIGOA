--Select Employees 

CREATE PROCEDURE [dbo].[STP_SEL_PARAMETRO_ID] 
@par_id nvarchar(200)

AS 
  BEGIN 
	SELECT top 1 par_valor 
		 FROM  dbo.tab_parametros
			where par_id = @par_id; 


  END ;
