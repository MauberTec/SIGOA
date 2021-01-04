
CREATE PROCEDURE [dbo].[STP_SEL_INSPECAO_ANOMALIAS] 
@ord_id int = null
AS 
BEGIN 


declare @ins_id int = (select ins_id from [dbo].[tab_inspecoes] where ord_id = @ord_id);

declare @obj_id int = (select obj_id from [dbo].[tab_inspecoes] where ins_id = @ins_id);
declare @obj_codigo_TipoOAE varchar(50) = (select obj_codigo from tab_objetos where obj_id = @obj_id);
declare @obj_ids varchar(5000) = '';

declare @obj_codigos varchar(max) = '';

		select  @obj_codigos = COALESCE(@obj_codigos + ';', '') + obj_codigo
		from dbo.tab_objetos obj
		inner join dbo.tab_inspecoes_anomalias ian on ian.obj_id = obj.obj_id and ian.ian_deletado is null and ian.ian_ativo = 1
		where  obj_ativo = 1
			and obj_deletado is null;


IF OBJECT_ID('tempdb..##temp_ian') IS NOT NULL
  DROP TABLE ##temp_ian;


-- cria uma tabela temporaria
	create table ##temp_ian(	rownum int null,
							obj_id int, 
							obj_pai int, 
							obj_codigo varchar(1000), 
							[level] int, 
							[path] varchar(1000));

-- insere a linha superestrutura
	insert into ##temp_ian (obj_id, obj_pai, obj_codigo, [level], [path])
	select obj_id, obj_pai, obj_codigo, 0 AS [level], obj_codigo  AS [path]
	 from tab_objetos 
	  where obj_deletado is null and obj_ativo = 1
		and tip_id = 11  and CHARINDEX(@obj_codigo_TipoOAE, obj_codigo)  > 0 ;

	-- faz um loop inserindo primeiro a Superestrutura, Mesoestrutura, Infraestrutura e filhos, nessa ordem
	declare @tip_ids varchar(5000) = ',14,15,16,12,13,14,22,23,24,'; 
	declare @idx int = charindex(',',@tip_ids);
	declare @eind int = 0;

	declare @tip_id int = (substring(@tip_ids,0, @idx) );

	while (@idx < len(@tip_ids))
	begin
		set  @eind = isnull(((charindex(',', @tip_ids, @idx + 1)) - @idx - 1), 0);
		set @tip_id = substring(@tip_ids, (@idx  + 1),  @eind);
		set @obj_id  = (select obj_id from tab_objetos where obj_deletado is null and obj_ativo = 1 and tip_id = @tip_id  and CHARINDEX(@obj_codigo_TipoOAE, obj_codigo)  > 0);

			-- processa 
		if @obj_id is not null
		begin
			if (@tip_id = 14)
			begin
				insert into ##temp_ian (obj_id, obj_pai, obj_codigo, [level], [path])
				SELECT obj_id, obj_pai, obj_codigo, 1 AS [level], obj_codigo  AS [path]
				 from tab_objetos 
				  where obj_deletado is null and obj_ativo = 1
					and tip_id = @tip_id  and CHARINDEX(@obj_codigo_TipoOAE, obj_codigo)  > 0 
					and CHARINDEX(obj_codigo, @obj_codigos)  > 0 ;
			end
			else
			begin
				WITH cte (obj_id, obj_pai, obj_codigo,  level, path) AS (
					SELECT obj.obj_id, obj.obj_pai, obj.obj_codigo,  
						   1 AS level
						   , CAST(obj_codigo AS VARCHAR(1000)) AS path
						from tab_objetos obj						
						where obj.obj_id = @obj_id 
						--and CHARINDEX(@obj_codigos, obj.obj_codigo)  > 0 
				union ALL 
					SELECT obj.obj_id, obj.obj_pai, obj.obj_codigo, cte.level + 1 AS [level], CAST((cte.[path] + '/' + obj.obj_codigo) AS VARCHAR(1000)) AS [path]
					from tab_objetos obj 
						INNER JOIN cte ON cte.obj_id = obj.obj_pai 
					where obj_deletado is null and obj_ativo = 1 
				) 

				insert into ##temp_ian (obj_id, obj_pai, obj_codigo, [level], [path])
				SELECT obj_id, obj_pai, obj_codigo, [level], [path]
				FROM cte
					where CHARINDEX(obj_codigo, @obj_codigos)  > 0 
				ORDER BY path ASC;
			end
		end;
		set @idx = isnull(charindex(',', @tip_ids, @idx + 1), 0);
	end

-- adiciona o numero da linha para manter a ordenacao
		UPDATE tb1
		set tb1.rownum = tb2.ROW_ID
		from ##temp_ian AS tb1
		inner join (SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) as ROW_ID, obj_id from ##temp_ian) as tb2 on tb1.obj_id = tb2.obj_id


-- faz o select
		SELECT tmp.*
			  ,clo.clo_id, clo.clo_nome
			  ,tip.tip_nome
			  ,obj.obj_descricao
		      ,ian.*
			  ,ins.ins_id
			  ,atp.atp_id, atp_codigo, atp_descricao
			  ,ale.ale_id, ale.ale_codigo, ale.ale_descricao
			  ,aca.aca_id, aca.aca_codigo, aca.aca_descricao
			  ,leg.leg_id, leg.leg_codigo, leg.leg_descricao
			  ,ian.rpt_id_sugerido, ire_sug.ire_descricao as ire_descricao_sugerido
			  ,ian.rpt_id_adotado, ire_adt.ire_descricao as ire_descricao_adotado
			  ,case when clo.clo_id >= 10 then dbo.ConcatenarAnomaliaAlertas() else '' end as cmbAnomaliaAlertas
			  ,case when clo.clo_id >= 10 then dbo.ConcatenarAnomaliaLegendas() else '' end as cmbAnomaliaLegendas
			  ,case when clo.clo_id >= 10 then dbo.ConcatenarAnomaliaTipos (leg.leg_id) else '' end as cmbAnomaliaLegendas

		  FROM ##temp_ian tmp 
			  inner join dbo.tab_objetos obj on obj.obj_id = tmp.obj_id --and obj.obj_deletado is null and obj.obj_ativo = 1
			  inner join dbo.tab_objeto_classes clo on clo.clo_id = obj.clo_id and clo.clo_deletado is null and clo.clo_ativo = 1
			  inner join dbo.tab_objeto_tipos tip on tip.tip_id = obj.tip_id and tip.tip_deletado is null and tip.tip_ativo = 1
		      left join dbo.tab_inspecoes_anomalias ian on ian.obj_id = tmp.obj_id and ian.ian_deletado is null and ian.ian_ativo = 1
			  left join dbo.tab_inspecoes ins on ins.ins_id = ian.ins_id and ins.ins_deletado is null and ins.ins_ativo = 1
			  left join dbo.tab_anomalia_tipos atp on atp.atp_id = ian.atp_id and atp.atp_deletado is null and atp.atp_ativo = 1
			  left join dbo.tab_anomalia_alertas ale on ale.ale_id = ian.ale_id and ale.ale_deletado is null and ale.ale_ativo = 1
			  left join dbo.tab_anomalia_causas aca on aca.aca_id = ian.aca_id and aca.aca_deletado is null and aca.aca_ativo = 1
			  left join dbo.tab_anomalia_legendas leg on leg.leg_id = ian.leg_id and leg.leg_deletado is null and leg.leg_ativo = 1

			  left join dbo.tab_inspecoes_reparos ire_sug on ire_sug.ire_id = ian.leg_id and ire_sug.ire_deletado is null and ire_sug.ire_ativo = 1
			  left join dbo.tab_inspecoes_reparos ire_adt on ire_adt.ire_id = ian.leg_id and ire_adt.ire_deletado is null and ire_adt.ire_ativo = 1

		    where ian.ins_id= @ins_id or ian.ins_id is null
		  order by rownum



END ;
