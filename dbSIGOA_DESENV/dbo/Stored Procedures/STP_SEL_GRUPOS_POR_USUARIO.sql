CREATE procedure [dbo].[STP_SEL_GRUPOS_POR_USUARIO]
@usu_id int  = null
 as
 

		if @usu_id < 0
			select 
			-1 as gru_id	
			, '' as gru_descricao	
			, 0 as gru_Associado
		else
             select * from 
                     (
                            select gru.gru_id, gru_descricao,  1 as gru_Associado
                          	from dbo.tab_grupos gru
	                          inner join dbo.tab_grupos_usuarios gpu on gpu.gru_id= gru.gru_id
	                          where usu_id = @usu_id 
									  and gru_deletado is null 
									  and gpu_deletado is null
									  and gru_ativo = 1
                      union        
                            select  gru_id, gru_descricao,   0 as gru_Associado
                          	from tab_grupos 
                            where gru_id not in (select gru_id 
														from  dbo.tab_grupos_usuarios
															where usu_id = @usu_id
															and gpu_deletado is null)
							and gru_deletado is null
							and gru_ativo = 1
                      ) as tbl1
                     -- where per_id = @per_id  or per_id < 0
                      order by gru_descricao asc;

return
