CREATE function [dbo].[ConcatenarAnomaliaTipos] (@leg_id int)
returns varchar(max)
as
begin

DECLARE @saida VARCHAR(max)  = ''

	if (@leg_id > 0)
		SELECT  @saida = COALESCE(@saida + '; ', '') + (atp_id + ':' + atp_codigo + '-' + atp_descricao)
		FROM dbo.tab_anomalia_tipos
		where leg_id = @leg_id
		order by atp_codigo;

 return isnull(@saida,'')

end
