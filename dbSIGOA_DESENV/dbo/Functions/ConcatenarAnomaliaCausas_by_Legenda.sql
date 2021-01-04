
CREATE function [dbo].[ConcatenarAnomaliaCausas_by_Legenda] (@leg_codigo varchar(10) = '')
returns varchar(max)
as
begin

DECLARE @saida VARCHAR(max) 


if @leg_codigo = '' 
		SELECT  @saida = COALESCE(@saida + '; ', '') + (aca_codigo + ':' + aca_descricao)
		from dbo.tab_anomalia_causas aca
	     inner join [dbo].[tab_anomalia_legendas] leg on leg.leg_id = aca.leg_id and leg.leg_deletado is null
	    where 
	       aca_deletado is null
	    order by  aca.leg_id, CONVERT(int, aca_codigo)
else
		SELECT  @saida = COALESCE(@saida + '; ', '') + (aca_codigo + ':' + aca_descricao)
		from dbo.tab_anomalia_causas aca
	     inner join [dbo].[tab_anomalia_legendas] leg on leg.leg_id = aca.leg_id and leg.leg_deletado is null
	    where 
		leg.leg_codigo =  @leg_codigo 
	      and aca_deletado is null
	    order by CONVERT(int, aca_codigo)

 return isnull(@saida,'')

end
