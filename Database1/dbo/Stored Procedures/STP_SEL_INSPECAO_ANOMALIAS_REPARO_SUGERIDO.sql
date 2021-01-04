
CREATE PROCEDURE [dbo].[STP_SEL_INSPECAO_ANOMALIAS_REPARO_SUGERIDO] 
@leg_codigo varchar(10),
@atp_codigo varchar(10),
@ale_codigo varchar(10),
@aca_codigo varchar(10)

AS 
BEGIN 

		SELECT TOP 1 rpt.*
		  FROM dbo.tab_reparo_politica rpp
			  inner join dbo.tab_anomalia_alertas ale on ale.ale_id = rpp.ale_id and ale.ale_deletado is null
			  inner join dbo.tab_anomalia_causas aca on aca.aca_id = rpp.aca_id and aca.aca_deletado is null
			  inner join dbo.tab_anomalia_legendas leg on leg.leg_id = rpp.leg_id and leg.leg_deletado is null
			  inner join dbo.tab_anomalia_tipos atp on atp.atp_id = rpp.atp_id and atp.atp_deletado is null
			  inner join dbo.tab_reparo_tipos rpt on rpt.rpt_id = rpp.rpt_id and rpt.rpt_deletado is null
		  where rpp_deletado is null
			  and leg.leg_codigo = @leg_codigo
			  and atp.atp_codigo = @atp_codigo
			  and ale.ale_codigo = @ale_codigo
			  and aca.aca_codigo = @aca_codigo

END ;
