create PROCEDURE [dbo].[STP_SEL_ANOM_CAUSA_BY_LEGENDA] 
@leg_codigo varchar(10) = ''
AS 
BEGIN 

	SELECT aca_codigo, aca_descricao
	  FROM [dbo].[tab_anomalia_causas] aca
	  inner join [dbo].[tab_anomalia_legendas] leg on leg.leg_id = aca.leg_id and leg.leg_deletado is null
	  where leg.leg_codigo = @leg_codigo
	  and aca_deletado is null
	  order by CONVERT(int, aca_codigo)


  END ;
