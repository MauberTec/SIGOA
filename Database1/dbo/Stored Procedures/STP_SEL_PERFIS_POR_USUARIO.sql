CREATE procedure [dbo].[STP_SEL_PERFIS_POR_USUARIO]
@usu_id int  = null
 as

		if @usu_id < 0
			select -1 as usu_id	
			, '' as per_descricao	
			,-1 as per_id	
			, 0 as per_Associado
		else
             select * from 
                     (
                            select per.per_id, per_descricao,  1 as per_Associado, @usu_id AS usu_id
                          	from dbo.tab_perfis per
	                          inner join dbo.tab_perfis_usuarios pfu on pfu.per_id= per.per_id
	                          where usu_id = @usu_id 
								and per_deletado is null
								and pfu_deletado is null
								and per_ativo = 1
                      union        
                            select  per_id, per_descricao,   0 as perfilAssociado, @usu_id AS usu_id
                          	from tab_perfis 
                            where per_id not in (select per_id 
														from dbo.tab_perfis_usuarios 
															where usu_id = @usu_id
																and pfu_deletado is null)
								and per_deletado is null
								and per_ativo = 1
                      ) as tbl1
                     where usu_id = @usu_id  or usu_id < 0
                      order by per_descricao asc;

return
