CREATE procedure [dbo].[STP_SEL_DOCUMENTO_OBJETOS]
@doc_id int,
@obj_codigo varchar(200)=null
as
 
  	if (@obj_codigo is null) -- retorna os Objetos associados
	begin
		if @doc_id < 0
			select -1 as obj_id	
			,'' as obj_codigo
			, '' as obj_descricao	
			,-1 as doc_id	
			, 0 as obj_Associado
		else
          select distinct obj.obj_id, obj_codigo, obj_descricao,  1 as obj_Associado
            from dbo.tab_objetos obj
	            inner join dbo.tab_documento_objeto dob on dob.obj_id= obj.obj_id
	            where doc_id = @doc_id 
					and dob_deletado is  null
					and obj_deletado is null
					and obj_ativo = 1
			 order by obj_descricao asc;
	end
	else -- retorna os Objetos NAO associados
	begin
	   --   SELECT  * 
		  --from  dbo.tab_objetos obj
		  --left join dbo.tab_documento_objeto dob on dob.obj_id = obj.obj_id and dob.dob_deletado is null
		  --where obj.obj_deletado is  null
				
				--and obj.obj_codigo like  '%' + @obj_codigo + '%'
				--and (dob.obj_id is null or dob.doc_id <> @doc_id); 

	      SELECT  * 
		  from  dbo.tab_objetos obj		  
		  where 
				obj.obj_codigo like  '%' + @obj_codigo + '%'
				and obj.obj_deletado is  null
				and obj_id 
						not in (select distinct obj_id 
								from dbo.tab_documento_objeto dob 
								where dob_deletado is null
								and doc_id = @doc_id);
		 -- order by ord_codigo;
		 
	end;


return
