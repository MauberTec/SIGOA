CREATE procedure [dbo].[STP_SEL_MODULOS_POR_PERFIL]
@per_id int = null
as
--declare @Local_MFL_PERFIL int = 1
		if @per_id < 0
		select
				-1 as per_id
				,-1 as mod_id	
				,'' as mod_nome_modulo	
				,'' as mod_descricao	
				,0 as ativo	
				,0 as leitura	
				,0 as escrita	
				,0 as exclusao	
				,0 as insercao	
				,-1 as ModuloAssociado	
				,-1 as mod_pai_id	
				,-1 mod_ordem
		else
             select * from (
                        select per_id,  tab_modulos.mod_id, tab_modulos.mod_nome_modulo, tab_modulos.mod_descricao,
                                CASE when tab_modulos.mod_ativo =  1 then  1 else 0 end  as ativo,
                                CASE when tab_modulos_perfis.mfl_leitura = 1 then  1 else 0 end  as leitura,
                                CASE when tab_modulos_perfis.mfl_escrita = 1 then  1 else 0 end  as escrita,
                                CASE when tab_modulos_perfis.mfl_excluir = 1 then  1 else 0 end  as exclusao,
                                CASE when tab_modulos_perfis.mfl_inserir = 1 then  1 else 0 end  as insercao,
                                1 as ModuloAssociado,
                                isnull(mod_pai_id,-1) as mod_pai_id,
                                mod_ordem
                              from tab_modulos
                              inner join tab_modulos_perfis on tab_modulos.mod_id=tab_modulos_perfis.mod_id
							  where mod_ativo = 1
							   and mod_deletado is null
                         union
                          select @per_id,  tab_modulos.mod_id, tab_modulos.mod_nome_modulo, tab_modulos.mod_descricao,
                                CASE when tab_modulos.mod_ativo =1 then 1 else 0 end as ativo,
                                0 as leitura,
                                0 as escrita,
                                0 as exclusao,
                                0 as insercao,
                                0 as ModuloAssociado,
                                isnull(mod_pai_id,-1) as mod_pai_id,
                                mod_ordem
                              from tab_modulos
                              where  mod_id not in ( select DISTINCT mod_id from tab_modulos_perfis where per_id= @per_id )
									and  mod_ativo = 1
									and mod_deletado is null
                          ) as tbl1
                            where (per_id = @per_id  or per_id is null)
							and mod_id >=0							
                            order by mod_ordem asc; 
                            
                            return
