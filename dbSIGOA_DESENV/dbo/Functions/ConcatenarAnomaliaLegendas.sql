CREATE function [dbo].[ConcatenarAnomaliaLegendas] ()
returns varchar(max)
as
begin

DECLARE @saida VARCHAR(max) 

		SELECT  @saida = COALESCE(@saida + '; ', '') + (leg_codigo + ':' + leg_descricao)
		FROM dbo.tab_anomalia_legendas
		where leg_id >=0
		order by leg_codigo;

 return isnull(@saida,'')

end
