CREATE PROCEDURE [dbo].[STP_SEL_ANOM_FLUXOSTATUS] 
@fst_id int=null
AS 
  BEGIN 

      SELECT  fos.*,
	  ast1.ast_codigo as ast_de_codigo, ast1.ast_descricao + ' (' + ast1.ast_codigo + ')' as ast_de_descricao,
	  ast2.ast_codigo as ast_para_codigo, ast2.ast_descricao + ' (' + ast2.ast_codigo + ')' as ast_para_descricao
      FROM  dbo.tab_anomalia_fluxos_status fos
		  inner join dbo.tab_anomalia_status ast1 on ast1.ast_id = fos.ast_id_de 
		  inner join dbo.tab_anomalia_status ast2 on ast2.ast_id = fos.ast_id_para
      where fst_deletado is  null
		and fst_id = case when @fst_id is null then fst_id else @fst_id end;

  END ;
