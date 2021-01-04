CREATE function [dbo].[ConcatenarAtributoItemIdCodigo] ( @atr_id int)
returns varchar(max)
as
begin

	declare @saida VARCHAR(max) 


		select  @saida = COALESCE(@saida + ';', '') + RIGHT('000'+CAST(ati_id AS VARCHAR(3)),3)+ convert(varchar(1000),ati_item)
		from dbo.tab_atributo_itens
		where  atr_id= @atr_id
			and ati_ativo = 1
			and ati_deletado is null
		order by ati_id;



 return isnull(@saida,'')

end
