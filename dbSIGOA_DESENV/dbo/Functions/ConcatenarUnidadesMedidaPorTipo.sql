create function [dbo].[ConcatenarUnidadesMedidaPorTipo] ( @unt_id int, @Descricao int)
returns varchar(max)
as
begin

	DECLARE @saida VARCHAR(max) 

	if (@Descricao = 0)
		SELECT  @saida = COALESCE(@saida + ', ', '') + case when rtrim(uni_unidade) <>'' then convert(varchar,uni_unidade) else '-'  end
		FROM dbo.tab_unidades_medida
		where  unt_id= @unt_id
			and uni_ativo = 1
			and uni_deletado is null
		order by uni_unidade;
	else
		if (@Descricao = 1)
			SELECT  @saida = COALESCE(@saida + ', ', '') + case when rtrim(uni_descricao) <>'' then convert(varchar, uni_descricao) else '-'  end
			FROM dbo.tab_unidades_medida
			where  unt_id= @unt_id
			and uni_ativo = 1
			and uni_deletado is null
			order by uni_unidade;
		else
			SELECT  @saida = COALESCE(@saida + ', ', '') + case when uni_id <>0 then convert(varchar(2),uni_id) else '-'  end
			FROM dbo.tab_unidades_medida
			where  unt_id= @unt_id
			and uni_ativo = 1
			and uni_deletado is null
			order by uni_unidade;


 return isnull(@saida,'')

end
