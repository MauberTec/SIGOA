CREATE function [dbo].[ConcatenarMunicipios]() returns varchar(max)
as
begin

declare @saida varchar(max);

		select  @saida = COALESCE(@saida + ';', '') + RIGHT('000'+CAST(mun_id AS VARCHAR(3)),3)+ convert(varchar(150),mun_municipio)
		from dbo.tab_municipios
		order by mun_municipio;

 return isnull(@saida,'')

end
