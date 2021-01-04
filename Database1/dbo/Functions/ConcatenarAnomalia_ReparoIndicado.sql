
create function [dbo].[ConcatenarAnomalia_ReparoIndicado] ( @leg_codigo nvarchar(10),
														@atp_codigo nvarchar(10),
														@ale_codigo nvarchar(10),
														@aca_codigo nvarchar(10))
returns varchar(max)
as
begin

declare @leg_id int = (select top 1 leg_id from dbo.tab_anomalia_legendas where leg_codigo = @leg_codigo);
declare @atp_id int = (select top 1 atp_id from dbo.tab_anomalia_tipos where atp_codigo = @atp_codigo);
declare @ale_id int = (select top 1 ale_id from dbo.tab_anomalia_alertas where ale_codigo = @ale_codigo);
declare @aca_id int = (select top 1 aca_id from dbo.tab_anomalia_causas where aca_codigo = @aca_codigo);

DECLARE @saida_codigo VARCHAR(max) 

		select  @saida_codigo = COALESCE(@saida_codigo + '; ', '') + rpt_codigo 
		from dbo.tab_reparo_politica rpp
		  inner join dbo.tab_reparo_tipos rpt on rpt.rpt_id = rpp.rpt_id and rpt.rpt_deletado is null
		where rpp.rpp_deletado is null and rpp.rpp_ativo = 1
			and leg_id = @leg_id
			and atp_id = @atp_id
			and ale_id = @ale_id
			and aca_id = @aca_id
		order by rpt_codigo;

DECLARE @saida_desc VARCHAR(max) 
		select  @saida_desc = COALESCE(@saida_desc + '; ', '') + rpt_descricao 
		from dbo.tab_reparo_politica rpp
		  inner join dbo.tab_reparo_tipos rpt on rpt.rpt_id = rpp.rpt_id and rpt.rpt_deletado is null
		where rpp.rpp_deletado is null and rpp.rpp_ativo = 1
			and leg_id = @leg_id
			and atp_id = @atp_id
			and ale_id = @ale_id
			and aca_id = @aca_id
		order by rpt_codigo;

 return isnull(@saida_codigo,'')  + ':' +  isnull(@saida_desc,'')

end
