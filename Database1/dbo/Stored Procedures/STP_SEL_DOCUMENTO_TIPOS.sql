CREATE PROCEDURE [dbo].[STP_SEL_DOCUMENTO_TIPOS] 
@tpd_id nvarchar(3) = null,
@tpd_subtipo int = null
AS 
  BEGIN 

      
	  select  tpd_id, tpd_subtipo, tpd_descricao, tpd_ativo
		  from dbo.tab_documento_tipos tpd
		  where tpd_deletado is null
			  and tpd_id = case when @tpd_id is null then tpd_id else @tpd_id end
			  and tpd_subtipo = case when @tpd_subtipo is null then tpd_subtipo else @tpd_subtipo end

	  union all
	  select dcl_codigo as tpd_id, 3 as tpd_subtipo, dcl_descricao as tpd_descricao, dcl_ativo as tpd_ativo
		  from dbo.tab_documento_classes_projeto dcl
		  where dcl_deletado is null
			  and dcl_codigo = case when @tpd_id is null then dcl_codigo else @tpd_id end
			  and 3 = case when @tpd_subtipo is null then 3 else @tpd_subtipo end

	  order by tpd_subtipo, tpd_id;



  END ;
