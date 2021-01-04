CREATE procedure [dbo].[STP_SEL_DOCUMENTO_OSS]
@doc_id int,
@filtroOrdemServico_codigo varchar(100)= null,
@filtroObj_codigo varchar(100)=null,
@filtroTiposOS int = null
as
 -- retorna as OSs associadas
 	if ((@filtroOrdemServico_codigo is null)
		  and (@filtroObj_codigo is null) 
		    and (@filtroTiposOS is null))
	begin
		if @doc_id < 0
			select -1 as ord_id	
			,'' as ord_codigo
			, '' as ord_descricao	
			,-1 as doc_id	
			, 0 as ord_Associada
			, 1 as ord_ativo
		else
          select distinct ord.ord_id, ord_codigo, ord_descricao,  1 as ord_Associada, ord_ativo
            from dbo.tab_ordens_servico ord
	            inner join dbo.tab_documento_ordens_servicos dos on dos.ord_id= ord.ord_id
	            where doc_id = @doc_id 
					and dos_deletado is  null
					and ord_deletado is null
					and ord_ativo = 1
			 order by ord_descricao asc;
	end
	else  -- retorna as OSs NAO associadas, para preenchimento do combo de associacao
	begin
		  SELECT distinct ord.ord_id, ord_codigo, ord_descricao, ord_ativo 
		  from  dbo.tab_ordens_servico ord
		  inner join dbo.tab_objetos obj on obj.obj_id = ord.obj_id and obj.obj_deletado is null and obj.obj_ativo = 1
		  where 
				ord.ord_codigo like case when @filtroOrdemServico_codigo is null then '%' else @filtroOrdemServico_codigo + '%' end
				and obj.obj_codigo like case when @filtroObj_codigo is null then '%' else @filtroObj_codigo + '%' end
				and ord.tos_id = case when @filtroTiposOS is null then ord.tos_id else @filtroTiposOS end
				and ord.ord_deletado is  null
				and ord_id 
				not in (select distinct ord_id 
						from dbo.tab_documento_ordens_servicos dos 
						where dos_deletado is null
						and doc_id = @doc_id)
		  order by ord_codigo;
	end

return
