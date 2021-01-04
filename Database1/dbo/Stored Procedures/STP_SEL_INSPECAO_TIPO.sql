create PROCEDURE [dbo].[STP_SEL_INSPECAO_TIPO] 
@ipt_id int=null
AS 
  BEGIN 

      SELECT  * 
      FROM  dbo.tab_inspecao_tipos 
      where ipt_deletado is  null
		and ipt_id = case when @ipt_id is null then ipt_id else @ipt_id end;

  END ;
