CREATE function [dbo].[ConcatenarUnidadesMedidaPorTipoIdUnidade] ( @unt_id int)
returns varchar(max)
as
begin

	DECLARE @saida VARCHAR(max) 

	select  @saida = COALESCE(@saida + ';', '') + RIGHT('000'+CAST(uni_id AS VARCHAR(2)),2)+ convert(varchar(100),uni_unidade)
		FROM dbo.tab_unidades_medida
		where  unt_id= @unt_id
			and uni_ativo = 1
			and uni_deletado is null
		order by uni_unidade;


 return isnull(@saida,'')

end
