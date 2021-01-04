CREATE procedure [dbo].[STP_SEL_Menus]
@usu_id int
as


IF OBJECT_ID('tempdb..#tmpGetMenus') IS NOT NULL
drop table #tmpGetMenus

	if (@usu_id >-1)
	begin
			select    (CONVERT(varchar(5), mpai.mod_ordem) + CONVERT(varchar(5), isnull(mpai.mod_pai_id,'') )) as novaOrdem
			, *
				into #tmpGetMenus
				from dbo.tab_modulos mpai 
				where 
				mpai.mod_ativo = 1 --and mfilho.ATIVO=1 
				and mpai.mod_deletado is null
				and mpai.mod_id in (	select  mfl.mod_id
									    from dbo.tab_modulos_perfis mfl
										  inner join dbo.tab_modulos md on md.mod_id = mfl.mod_id and md.mod_deletado is null
											where  mod_ativo= 1
												  and per_id in (  select per_id as pfl_code 
																	   from dbo.tab_perfis_usuarios pfu
																	        inner join dbo.tab_usuarios usu on usu.usu_id= pfu.usu_id and pfu_deletado is null
		                       											where pfu.usu_id = @usu_id 
																			  and usu_ativo=1
																   union  
																   select per_id as pfl_code 
																        from dbo.tab_perfis_grupos pfg
																        inner join dbo.tab_grupos gru on gru.gru_id= pfg.gru_id																
																		where gru.gru_ativo = 1
																		      and pfg.gru_id in ( select gpu.gru_id 
																									from dbo.tab_grupos_usuarios gpu
																									 inner join dbo.tab_usuarios usu on usu.usu_id= gpu.usu_id
																								 where gpu.usu_id = @usu_id 
																									    and usu_ativo = 1)
															) 
												 and ( mfl_leitura | mfl_escrita | mfl_excluir |  mfl_inserir = 1 )  
									);

			 select novaOrdem,
					mod_id as men_menu_id,
					mod_nome_modulo as men_item,
					mod_descricao as men_descricao,
					mod_ordem as men_ordem,
					mod_caminho as men_caminho,
					isnull(mod_pai_id,-1) as men_pai_id,
					mod_id,
					mod_ativo as men_ativo,
					isnull(mod_icone,'') as men_icone,
					mod_data_criacao,
					mod_criado_por,
					mod_data_atualizacao,
					mod_atualizado_por 
			 from #tmpGetMenus
			 where mod_ativo = 1 and mod_deletado is null
				   and (mod_pai_id  in ( select distinct mod_id from #tmpGetMenus)
					or mod_pai_id is null)
			 order by mod_ordem 
	end
	else
	begin
			select * from dbo.tab_menus
			where men_menu_id in (7);
	end

IF OBJECT_ID('tempdb..#tmpGetMenus') IS NOT NULL
drop table #tmpGetMenus
return
