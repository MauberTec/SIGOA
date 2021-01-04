CREATE function [dbo].[ConcatenarAtributoItens_Valores] ( 
@obj_id int,
@atr_id int,
@ord_id int = 0,
@Descricao int,
@ehTabInspecao int = 0)
returns varchar(max)
as
begin

if (@ehTabInspecao is null )
  set @ehTabInspecao = 0;

	DECLARE @saida VARCHAR(max) 
	declare @atv_valor varchar(max);

	if (@Descricao = 0)
	begin
	  if (@ehTabInspecao = 0)
			SELECT  @saida = COALESCE(@saida + '; ', '') + case when rtrim(ati_item) <>'' then convert(varchar,ati_item) else '-'  end
			FROM dbo.tab_atributo_itens ati
				inner join dbo.tab_objeto_atributos_valores atv on atv.ati_id = ati.ati_id and atv_deletado is null
			where 
				atv.atr_id= @atr_id
				and obj_id = @obj_id

				and ati_ativo = 1 
				and ati_deletado is null
			order by ati.ati_id ;
		else
			SELECT  @saida = COALESCE(@saida + '; ', '') + case when rtrim(ati_item) <>'' then convert(varchar,ati_item) else '-'  end
			FROM dbo.tab_atributo_itens ati
				inner join dbo.tab_inspecao_atributos_valores iav on iav.ati_id = ati.ati_id and iav_deletado is null and iav_ativo=1
				inner join dbo.tab_inspecoes ins on  ins.ins_id = iav.ins_id and ins.ins_deletado is null and ins_ativo=1
			where 
				iav.atr_id= @atr_id
				and ins.obj_id = @obj_id
				and ins.ord_id = @ord_id
				and ati_ativo = 1 
				and ati_deletado is null
			order by ati.ati_id ;
	end
	else
		if (@ehTabInspecao = 0)
		begin
			SELECT  @saida = COALESCE(@saida + '; ', '') + case when rtrim(ati.ati_id) <>'' then convert(varchar,ati.ati_id) else '-'  end
			FROM dbo.tab_atributo_itens ati
			inner join dbo.tab_objeto_atributos_valores atv on atv.ati_id = ati.ati_id and atv_deletado is null
			where 
				atv.atr_id= @atr_id
				and obj_id = @obj_id
				and ati_ativo = 1 
				and ati_deletado is null
			order by ati.ati_id ;

			SELECT @atv_valor = atv_valor
			FROM dbo.tab_atributo_itens ati
			inner join dbo.tab_objeto_atributos_valores atv on atv.ati_id = ati.ati_id and atv_deletado is null
			where 
				atv.atr_id= @atr_id
				and obj_id = @obj_id
				and ati_ativo = 1 
				and ati_deletado is null
				and CHARINDEX('___', ati_item) > 0
			order by ati.ati_id ;
	
			set @saida =  replace (@saida, '____', '['+  isnull(@atv_valor,'') + ']');
		end
		else
		begin
			SELECT  @saida = COALESCE(@saida + '; ', '') + case when rtrim(ati.ati_id) <>'' then convert(varchar,ati.ati_id) else '-'  end
			FROM dbo.tab_atributo_itens ati
				inner join dbo.tab_inspecao_atributos_valores iav on iav.ati_id = ati.ati_id and iav_deletado is null and iav_ativo=1
				inner join dbo.tab_inspecoes ins on  ins.ins_id = iav.ins_id and ins.ins_deletado is null and ins_ativo=1
			where 
				iav.atr_id= @atr_id
				and ins.obj_id = @obj_id
				and ins.ord_id = @ord_id
				and ati_ativo = 1 
				and ati_deletado is null
			order by ati.ati_id ;

			SELECT @atv_valor = iav_valor
			FROM dbo.tab_atributo_itens ati
				inner join dbo.tab_inspecao_atributos_valores iav on iav.ati_id = ati.ati_id and iav_deletado is null and iav_ativo=1
				inner join dbo.tab_inspecoes ins on  ins.ins_id = iav.ins_id and ins.ins_deletado is null and ins_ativo=1
			where 
				iav.atr_id= @atr_id
				and ins.obj_id = @obj_id
				and ati_ativo = 1 
				and ati_deletado is null
				and CHARINDEX('___', ati_item) > 0
			order by ati.ati_id ;
	
			set @saida =  replace (@saida, '____', '['+  isnull(@atv_valor,'') + ']');
		end

 return isnull(@saida,'')

end
