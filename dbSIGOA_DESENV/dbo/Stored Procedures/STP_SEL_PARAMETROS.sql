--Select Employees 

 CREATE PROCEDURE [dbo].[STP_SEL_PARAMETROS] 
 @par_id nvarchar(200) = ''
AS 
  BEGIN 

		SELECT  * 
		  FROM  dbo.tab_parametros 
		  where par_id like case when @par_id <> '' then @par_id else par_id end; 

  END ;
