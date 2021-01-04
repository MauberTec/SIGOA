CREATE function [dbo].[ConcatenarAtributoItens] ( @atr_id int, @Descricao int)
returns varchar(max)
as
begin

	DECLARE @saida VARCHAR(max) 

	if (@Descricao = 0)
		SELECT  @saida = COALESCE(@saida + '; ', '') + case when rtrim(ati_item) <>'' then convert(varchar(100),ati_item) else '-'  end
		FROM dbo.tab_atributo_itens
		where  atr_id= @atr_id
			and ati_ativo = 1
			and ati_deletado is null
		order by ati_id;
	else
			SELECT  @saida = COALESCE(@saida + '; ', '') + case when ati_id <>0 then convert(varchar(2),ati_id) else '-'  end
			FROM dbo.tab_atributo_itens
			where  atr_id= @atr_id
			and ati_ativo = 1
			and ati_deletado is null
			order by ati_id;


 return isnull(@saida,'')

end
