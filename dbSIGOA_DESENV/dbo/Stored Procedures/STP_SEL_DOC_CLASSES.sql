CREATE PROCEDURE [dbo].[STP_SEL_DOC_CLASSES] 
@dcl_id int=null,
@tpd_id nvarchar(3) = null
AS 

  BEGIN 

	--select * from dbo.tab_documento_tipos
	--where tpd_deletado is null
	--and tpd_id = case when @tpd_id is null then tpd_id else @tpd_id end


  -- alteracao em 21/jul/2020 -> redirecionando temporariaamente para tabela [dbo].[tab_documento_tipos]
      SELECT  * 
      FROM  dbo.tab_documento_classes_projeto 
      where dcl_deletado is  null
		and dcl_id = case when @dcl_id is null then dcl_id else @dcl_id end
	  order by dcl_codigo;

  END ;
