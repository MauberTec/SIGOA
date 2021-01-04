--Select Employees 

CREATE PROCEDURE [dbo].[STP_SEL_MODULOS] 
@mod_id int = null
AS 
begin

	if (@mod_id is null )
		  SELECT  mod_id, mod_nome_modulo, mod_descricao, mod_ativo, mod_deletado, mod_ordem, mod_caminho, 
			  isnull(mod_pai_id,-1) as mod_pai_id,
			  mod_icone, mod_data_criacao, mod_criado_por, mod_data_atualizacao, mod_atualizado_por
		  FROM  dbo.tab_modulos 
		  where mod_deletado is null
		  and mod_id >=0
		  ORDER BY mod_ordem; 
	else
		  SELECT  mod_id, mod_nome_modulo, mod_descricao, mod_ativo, mod_deletado, mod_ordem, mod_caminho, 
			  isnull(mod_pai_id,-1) as mod_pai_id,
			  mod_icone, mod_data_criacao, mod_criado_por, mod_data_atualizacao, mod_atualizado_por
		  FROM  dbo.tab_modulos 
		  where mod_deletado is null
		  and mod_id= @mod_id
		  ORDER BY mod_ordem; 


end
