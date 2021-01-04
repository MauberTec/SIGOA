CREATE procedure [dbo].[STP_SEL_PERFIS_POR_GRUPO]
@gru_id int  = null
 as

		if @gru_id < 0
			select -1 as per_id	
			, '' as per_descricao	
			,-1 as per_id	
			, 0 as per_Associado
		else
             select * from 
                     (
                            select per.per_id, per_descricao,  1 as per_Associado
                          	from dbo.tab_perfis per
	                          inner join dbo.tab_perfis_grupos pfg on pfg.per_id= per.per_id
	                          where gru_id = @gru_id 
								and pfg_deletado is null
								and per_deletado is null
								and per_ativo = 1
                      union        
                            select  per_id, per_descricao,   0 as perfilAssociado
                          	from tab_perfis 
                            where per_id not in (select per_id 
														from dbo.tab_perfis_grupos 
															where gru_id = @gru_id
																and pfg_deletado is null)
							and per_deletado is null
							and per_ativo = 1
                      ) as tbl1
                     -- where per_id = @per_id  or per_id < 0
                      order by per_descricao asc;

return
