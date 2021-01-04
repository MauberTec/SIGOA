CREATE procedure [dbo].[STP_SEL_USUARIOS_POR_GRUPO]
@gru_id int = null
 as

		if @gru_id < 0
		select
			-1 as usu_id	
			,'' as usu_usuario	
			,0 as usu_ativo	
			,-1 as gru_id	
			,0 as usu_Associado	
			,'' as usu_nome
		else
           select * from 
                     (
                            select usu.usu_id, usu_usuario, case when  usu_ativo= 1 then 1 else 0 end as usu_ativo, gru_id,  1 as usu_Associado,usu_nome
                          	from dbo.tab_usuarios usu
	                          inner join dbo.tab_grupos_usuarios gpu on gpu.usu_id= usu.usu_id
	                          and usu.usu_deletado is null
	                          and gpu_deletado is null
							  and usu_ativo=1
                      union        
                            select  usu_id, usu_usuario, 0 as usu_ativo, -1 as gru_id, 0 as usu_Associado, usu_nome
                          	from dbo.tab_usuarios 
                            where usu_id not in (select usu_id 
															from dbo.tab_grupos_usuarios 
																where gru_id = @gru_id
																and gpu_deletado is null)
							and usu_deletado is null
							and usu_ativo=1
                      ) as tbl1
          where gru_id = @gru_id  or gru_id <0
          order by usu_Associado desc, usu_usuario asc;  


return
