CREATE function [dbo].[ConcatenarAnomaliaAlertas] ()
returns varchar(max)
as
begin

DECLARE @saida VARCHAR(max) 

		SELECT  @saida = COALESCE(@saida + '; ', '') + (ale_codigo + ':' + ale_descricao)
		FROM dbo.tab_anomalia_alertas
		where ale_id >=0
		order by ale_id;

 return isnull(@saida,'')

end
