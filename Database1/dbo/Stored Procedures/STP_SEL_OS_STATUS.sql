CREATE PROCEDURE [dbo].[STP_SEL_OS_STATUS] 
@sos_id int=null
AS 
  BEGIN 

      SELECT  * 
      FROM  dbo.tab_ordem_servico_status 
      where sos_deletado is  null
		and sos_id = case when @sos_id is null then sos_id else @sos_id end;

  END ;
