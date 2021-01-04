create PROCEDURE [dbo].[STP_SEL_OS_TIPOS] 
@tos_id int = null
AS 
  BEGIN 

      SELECT  * 
      FROM  dbo.tab_ordem_servico_tipos 
      where tos_deletado is  null
		and tos_id = case when @tos_id is null then tos_id else @tos_id end;

  END ;
