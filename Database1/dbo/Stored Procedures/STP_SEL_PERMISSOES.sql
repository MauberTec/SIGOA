CREATE procedure [dbo].[STP_SEL_PERMISSOES]
@usu_id int
as
		select mfl_leitura, mfl_escrita, mfl_excluir, mfl_inserir, mod_id, per_id
		from dbo.tab_modulos_perfis
		where 
		mfl_deletado is null and
		per_id in (  select per_id
									from dbo.tab_perfis_usuarios 
                       				where usu_id = @usu_id and pfu_deletado is null
					           union  
					           select per_id 
								from dbo.tab_perfis_grupos
					                where gru_id in ( select gru_id 
																from  dbo.tab_grupos_usuarios
																where usu_id = @usu_id  and gpu_deletado is null)
					    )     
				and (mfl_leitura | mfl_escrita | mfl_inserir | mfl_excluir = 1)           
return
