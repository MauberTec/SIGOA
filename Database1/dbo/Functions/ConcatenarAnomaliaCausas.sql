
CREATE function [dbo].[ConcatenarAnomaliaCausas] ()--(@leg_codigo varchar(10) = ''

returns varchar(max)
as
begin

DECLARE @saida VARCHAR(max) 

		SELECT  @saida = COALESCE(@saida + '; ', '') + (aca_codigo + ':' + aca_descricao)
		FROM dbo.tab_anomalia_causas
		where aca_id >=0
		order by aca_id;

 return isnull(@saida,'')

end
