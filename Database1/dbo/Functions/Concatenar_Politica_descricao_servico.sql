
CREATE function [dbo].[Concatenar_Politica_descricao_servico] ( @ogv_id int)
returns varchar(max)
as
begin

DECLARE @saida VARCHAR(max) 


SELECT  @saida = COALESCE(@saida + ';', '') +  CONVERT(varchar(2), ogi_id_caracterizacao_situacao) + ':' + ocp_descricao_servico
  FROM dbo.tab_objeto_conserva_politica
  where ocp_ativo = 1  and ocp_deletado is null
  and   ogv_id = @ogv_id
  order by ogi_id_caracterizacao_situacao

--  select @saida


 return isnull(@saida,'')

end
