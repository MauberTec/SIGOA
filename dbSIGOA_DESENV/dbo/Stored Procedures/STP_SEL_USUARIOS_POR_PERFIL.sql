CREATE procedure [dbo].[STP_SEL_USUARIOS_POR_PERFIL]
@per_id int = null
 as

		if @per_id < 0
		select
			-1 as usu_id	
			,'' as usu_usuario	
			,0 as usu_ativo	
			,-1 as per_id	
			,0 as usu_Associado	
			,'' as usu_nome
		else
           select * from 
                     (
                            select usu.usu_id, usu_usuario, case when  usu_ativo= 1 then 1 else 0 end as usu_ativo, per_id,  1 as usu_Associado,usu_nome
                          	from dbo.tab_usuarios usu
	                          inner join dbo.tab_perfis_usuarios pfu on pfu.usu_id= usu.usu_id
	                          and pfu_deletado is null
	                          and usu.usu_deletado is null
							  and usu_ativo=1
                      union        
                            select  usu_id, usu_usuario, 0 as usu_ativo, -1 as per_id, 0 as usu_Associado, usu_nome
                          	from dbo.tab_usuarios 
                            where usu_id not in (select usu_id 
															from dbo.tab_perfis_usuarios 
															where per_id = @per_id
																and pfu_deletado is null)
							and usu_deletado is null
							and usu_ativo=1
                      ) as tbl1
                      where per_id = @per_id  or per_id <0
                      order by usu_usuario asc;  


return
