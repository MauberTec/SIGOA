
create function [dbo].[calcula_nota_ficha_rotineira] ( @obj_id int = -1, @ins_id int = -1, @qualparte int = 3)
returns varchar(max)
as
begin

declare @lista varchar (1000)= '';

	if (@qualparte = 1)
		set @lista = '99,100';
	else
		if (@qualparte = 2)
			set @lista = '135,136,137,138,139,140,141,142,143,144';
		else
			if (@qualparte = 3)
				set @lista = '99,100,135,136,137,138,139,140,141,142,143,144';

declare @retorno float = 0;

		if (@obj_id > 0)
			select @retorno = isnull(sum(convert(float, ati.ati_item)),0)
			from dbo.tab_atributos atr
				  left join dbo.tab_objeto_atributos_valores atv on atv.atr_id = atr.atr_id and atv.atv_deletado is null
				  inner join dbo.tab_atributo_itens ati on ati.atr_id = atv.atr_id and ati.ati_deletado is null
			where
			  atr.atr_deletado is null
			  and obj_id = @obj_id
			  and atr.atr_id in (select * from [dbo].[ConvertToTableInt] (@lista ))
			  and ati.ati_id = isnull(atv.atv_valor,0); 
		else
		  if (@ins_id > 0)
			  select  @retorno = isnull(sum(convert(float, ati.ati_item)),0)
			  from dbo.tab_atributos atr
				  left join dbo.tab_inspecao_atributos_valores iav on iav.atr_id = atr.atr_id and iav.iav_deletado is null
				  inner join dbo.tab_atributo_itens ati on ati.atr_id = iav.atr_id and ati.ati_deletado is null
			  where
				  atr.atr_deletado is null
				  and ins_id = @ins_id
				  and atr.atr_id in (select * from [dbo].[ConvertToTableInt] (@lista ))
				  and isnull(ati.ati_id,0) = isnull(iav.iav_valor,0);
			else
			  set @retorno = 0;

	  return @retorno ;

end
