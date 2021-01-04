CREATE function [dbo].[fn_objeto_tipo_por_codigo] (@tip_codigo nvarchar(10), @clo_id int, @qualSaida varchar(10), @tip_pai int = null)
returns varchar(200)
as
begin

	if @tip_pai is null
	 set @tip_pai = -1;

	DECLARE @saida varchar(255);

	if (@qualSaida = 'tip_id')
		set @saida = (select top 1 tip_id from dbo.tab_objeto_tipos where tip_codigo = @tip_codigo and clo_id= @clo_id and  tip_pai = case when @tip_pai is not null then @tip_pai else tip_pai end );
	else
		if (@qualSaida = 'tip_nome')
			set @saida = (select top 1 tip_nome from dbo.tab_objeto_tipos where tip_codigo = @tip_codigo and clo_id= @clo_id and tip_pai = case when @tip_pai is not null then @tip_pai else tip_pai end);
		else
		if (@qualSaida = 'tip_descricao')
			set @saida = (select top 1 tip_descricao from dbo.tab_objeto_tipos where tip_codigo = @tip_codigo and clo_id= @clo_id and tip_pai = case when @tip_pai is not null then @tip_pai else tip_pai end);


 return isnull(@saida,-1)

end
