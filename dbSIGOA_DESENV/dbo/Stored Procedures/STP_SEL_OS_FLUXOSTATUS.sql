CREATE PROCEDURE [dbo].[STP_SEL_OS_FLUXOSTATUS] 
@fos_id int=null
AS 
  BEGIN 

      SELECT  fos.*,
	  sos1.sos_codigo as sos_de_codigo, sos1.sos_descricao + ' (' + sos1.sos_codigo + ')' as sos_de_descricao,
	  sos2.sos_codigo as sos_para_codigo, sos2.sos_descricao + ' (' + sos2.sos_codigo + ')' as sos_para_descricao
      FROM  dbo.tab_ordem_servico_fluxos_status fos
		  inner join dbo.tab_ordem_servico_status sos1 on sos1.sos_id = fos.sos_id_de 
		  inner join dbo.tab_ordem_servico_status sos2 on sos2.sos_id = fos.sos_id_para
      where fos_deletado is  null
		and fos_id = case when @fos_id is null then fos_id else @fos_id end;

  END ;
