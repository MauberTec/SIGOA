CREATE procedure [dbo].[STP_SEL_GRUPOS_POR_PERFIL]
@per_id int  = null
 as
 

		if @per_id < 0
			select -1 as gru_id	
			, '' as gru_descricao	
			,-1 as per_id	
			, 0 as grupoAssociado
		else
             select * from 
                     (
                            select gru.gru_id, gru_descricao,  1 as grupoAssociado
                          	from dbo.tab_grupos gru
	                          inner join dbo.tab_perfis_grupos pfg on pfg.gru_id= gru.gru_id
	                          where per_id = @per_id 
									and  pfg_deletado is  null
									and gru_deletado is null
									and gru_ativo = 1
                      union        
                            select  gru_id, gru_descricao,   0 as grupoAssociado
                          	from tab_grupos 
                            where gru_id not in (select gru_id 
														from dbo.tab_perfis_grupos 
															where per_id = @per_id
																and pfg_deletado is null)															
								and gru_deletado is  null
								and gru_ativo = 1
                      ) as tbl1
                     -- where per_id = @per_id  or per_id < 0
                      order by gru_descricao asc;

return
