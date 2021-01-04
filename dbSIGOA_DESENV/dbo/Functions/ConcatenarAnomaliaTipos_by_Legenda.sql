
CREATE function [dbo].[ConcatenarAnomaliaTipos_by_Legenda] (@leg_codigo varchar(10) = '')
returns varchar(max)
as
begin

DECLARE @saida VARCHAR(max) 

	  SELECT  @saida = COALESCE(@saida + '; ', '') + (convert(varchar(5),atp_codigo) + ':' + atp_descricao)
	  FROM [dbo].[tab_anomalia_tipos] atp
	  inner join [dbo].[tab_anomalia_legendas] leg on leg.leg_id = atp.leg_id and leg.leg_deletado is null
		where leg.leg_codigo = case when @leg_codigo = '' then leg.leg_codigo else @leg_codigo end
		and atp_deletado is null
	  order by atp_codigo

 return isnull(@saida,'')

end
