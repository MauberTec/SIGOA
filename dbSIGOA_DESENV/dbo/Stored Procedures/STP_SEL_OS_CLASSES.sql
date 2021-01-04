CREATE PROCEDURE [dbo].[STP_SEL_OS_CLASSES] 
@ocl_id int = null
AS 
  BEGIN 

      SELECT  * 
      FROM  dbo.tab_ordem_servico_classes 
      where ocl_deletado is  null
		and ocl_id = case when @ocl_id is null then ocl_id else @ocl_id end;

  END ;
