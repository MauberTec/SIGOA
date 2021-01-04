
CREATE PROCEDURE [dbo].[STP_SEL_INSPECAO_ANOMALIAS_VALORES] 
@ord_id int = null
AS 
BEGIN 


declare @ins_id int = (select ins_id from [dbo].[tab_inspecoes] where ord_id = @ord_id);
declare @obj_id int = (select obj_id from tab_ordens_servico where ord_id = @ord_id);


declare @obj_codigo_TipoOAE varchar(50) = (select obj_codigo from tab_objetos where obj_id = @obj_id);
declare @obj_ids varchar(5000) = '';

-- cria lista dos obj_ids procurados
    declare @lista_obj_ids varchar(max) = '';

	select  @lista_obj_ids = COALESCE(@lista_obj_ids + ',', '') +  convert(varchar(20),obj_id) 
		from dbo.tab_inspecoes_anomalias
	where ins_id = @ins_id
	and ian_deletado is null and ian_ativo = 1;

-- remove a 1a virgula
set @lista_obj_ids = SUBSTRING(@lista_obj_ids,2,10000000)


IF OBJECT_ID('tempdb..##temp2') IS NOT NULL
  DROP TABLE ##temp2;


-- cria uma tabela temporaria
	create table ##temp2(	rownum int null,
							obj_id int, 
							obj_pai int, 
							obj_codigo varchar(1000), 
							[level] int, 
							[path] varchar(1000),
							[ian_numero] int null,
							item varchar(100) null);

-- insere a linha superestrutura
	insert into ##temp2 (obj_id, obj_pai, obj_codigo, [level], [path])
	select obj_id, obj_pai, obj_codigo, 0 AS [level], obj_id  AS [path]
	 from tab_objetos 
	  where obj_deletado is null and obj_ativo = 1
		and tip_id = 11  and CHARINDEX(@obj_codigo_TipoOAE, obj_codigo)  > 0 ;

	-- faz um loop inserindo primeiro a Superestrutura, Mesoestrutura, Infraestrutura e filhos, nessa ordem
	declare @tip_ids varchar(5000) = ',15,16,12,13,14,22,23,24,'; 
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
				insert into ##temp2 (obj_id, obj_pai, obj_codigo, [level], [path])
				SELECT obj_id, obj_pai, obj_codigo, 0 AS [level],  convert(varchar(10), obj_id)  AS [path]
				 from tab_objetos 
				  where obj_deletado is null and obj_ativo = 1
					and tip_id = @tip_id  and CHARINDEX(@obj_codigo_TipoOAE, obj_codigo)  > 0 ;
			end
			else
			begin
				WITH cte (obj_id, obj_pai, obj_codigo,  [level], [path]) AS (
					SELECT obj_id, obj_pai, obj_codigo,  
						   0 AS level
						    ,CAST( convert(varchar(10), obj_pai) + ',' + convert(varchar(10), obj_pai)  AS VARCHAR(1000)) AS [path]
						from tab_objetos 
						where obj_id = @obj_id 
				union ALL 
					SELECT c.obj_id, c.obj_pai, c.obj_codigo, cte.level + 1 AS [level], 
					CAST(cte.[path] + ',' + convert(varchar(10), c.obj_pai) + ',' + convert(varchar(10), c.obj_id)  AS VARCHAR(1000)) AS [path]
					--CAST((cte.[path] + ',' + convert(varchar(10), c.obj_pai) + ',' +  convert(varchar(5),c.obj_id)) AS VARCHAR(1000)) AS [path]
					from tab_objetos c 
						INNER JOIN cte ON cte.obj_id = c.obj_pai 
					where obj_deletado is null and obj_ativo = 1 					
				) 

				insert into ##temp2 (obj_id, obj_pai, obj_codigo, [level], [path])
				SELECT obj_id, obj_pai, obj_codigo, [level], [path]
				FROM cte
				ORDER BY path ASC;
			end
		end;
		set @idx = isnull(charindex(',', @tip_ids, @idx + 1), 0);
	end


	-- cria lista dos itens que permanecerão
	select  @lista_obj_ids = COALESCE(@lista_obj_ids + ',', '') +  [path]
	  from ##temp2 AS tb1
	  inner join dbo.tab_objetos obj on obj.obj_id = tb1.obj_id 
	where  (tb1.obj_id in (select * from dbo.ConvertToTableInt(@lista_obj_ids)))
		and (clo_id >= 10 or clo_id<=6 or tip_id = 11  );

	-- remove os itens que nao interessam
	delete from ##temp2 
	where obj_id not in (select * from dbo.ConvertToTableInt(@lista_obj_ids));


-- adiciona o numero da linha para manter a ordenacao
		UPDATE tb1
		set tb1.rownum = tb2.ROW_ID
		from ##temp2 AS tb1
		inner join (SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) as ROW_ID, obj_id from ##temp2) as tb2 on tb1.obj_id = tb2.obj_id

-- ajusta a coluna level
declare @obj_codigo_mesoestrutura varchar(100) = (select tb1.obj_codigo from ##temp2  tb1 inner join dbo.tab_objetos obj on obj.obj_id = tb1.obj_id  where tip_id = 12);
declare @obj_codigo_infraestrutura varchar(100) = (select tb1.obj_codigo from ##temp2  tb1 inner join dbo.tab_objetos obj on obj.obj_id = tb1.obj_id  where tip_id = 13);


	update tb1
		set level = level + 1
	from ##temp2 AS tb1
	inner join dbo.tab_objetos obj on obj.obj_id = tb1.obj_id 
	where level > 0
		and tb1.obj_codigo not like @obj_codigo_mesoestrutura + '%'
		and tb1.obj_codigo not like @obj_codigo_infraestrutura + '%' ;

-- ajusta a subdivisao 2
	update tb1
	set tb1.level = 1
	from ##temp2 AS tb1
	inner join dbo.tab_objetos obj on obj.obj_id = tb1.obj_id 
	where 
	   obj.clo_id = 7;


-- varre as linhas fazendo os ajustes de numero Item, level, numero da anomalia
    declare @nivel1 int = 0;
    declare @nivel2 int  = 0;
    declare @nivel3 int  = 0;
    declare @nivel4 int  = 0;
    declare @nivel5 int  = 0;

    declare @iAnomalia int = 0;
    declare @item varchar(200) = '';
    declare @path varchar(2000) = '';

    declare @rownum int  = 0;
	declare @level int = 0;
	declare @clo_id int = 0;
	declare @obj_id_aux int = 0;


	declare cursor_1 CURSOR FOR 
		SELECT rownum, [level], [clo_id],[path], tmp.obj_id
		  FROM ##temp2 tmp 
			  inner join dbo.tab_objetos obj on obj.obj_id = tmp.obj_id --and obj.obj_deletado is null and obj.obj_ativo = 1
		 -- where isnull(ian.ins_id,'') = case when isnull(@ins_id,'') = '' then '' else @ins_id end
		  order by rownum;

		OPEN cursor_1;
		FETCH NEXT FROM cursor_1 INTO  @rownum, @level,  @clo_id, @path, @obj_id_aux;

		while @@FETCH_STATUS = 0
		begin
		        if @level = 0
			    begin 
					set @nivel1 = @nivel1 + 1; 
					set @nivel2 = 0; 
					set @nivel3 = 0; 
					set @nivel4 = 0; 
					set @nivel5 = 0; 
					set @item = CONVERT(varchar(3), @nivel1); 
				end
				else
					if @level = 1
					begin
						set @nivel2 = @nivel2 + 1; 
						set @nivel3 = 0; 
						set @nivel4 = 0; 
						set @nivel5 = 0; 
						set @item = CONVERT(varchar(3),@nivel1) + '.' + CONVERT(varchar(3),@nivel2); 
					end
					else 
						if @level = 2
						begin
							set @nivel3 = @nivel3 + 1;
							set @nivel4 = 0; 
							set @nivel5 = 0;  
							set @item = CONVERT(varchar(3),@nivel1) + '.' + CONVERT(varchar(3),@nivel2) + '.' + CONVERT(varchar(3),@nivel3); 
						end
						else 
							if @level = 3
							begin
								set @nivel4 = @nivel4 + 1; 
								set @nivel5 = 0; 
								set @item = CONVERT(varchar(3),@nivel1) + '.' + CONVERT(varchar(3),@nivel2) + '.' + CONVERT(varchar(3),@nivel3) + '.' + CONVERT(varchar(3),@nivel4); 
							end
							else 
								if @level = 4
								begin
									set @nivel5 = @nivel5 + 1; 
									set @item = CONVERT(varchar(3),@nivel1) + '.' + CONVERT(varchar(3),@nivel2) + '.' + CONVERT(varchar(3),@nivel3) + '.' + CONVERT(varchar(3),@nivel4) + '.' + CONVERT(varchar(3),@nivel5); 
								end;

                        if @clo_id >= 10
						begin
                           set @iAnomalia = @iAnomalia + 1;

						   if (@obj_id_aux in (select * from dbo.ConvertToTableInt(@lista_obj_ids)))
							 set @lista_obj_ids = @lista_obj_ids + ',' + @path;
						end

					update ##temp2
						set 
						item = @item,
						ian_numero = case when @clo_id >= 10 then @iAnomalia else 0 end
					where rownum = @rownum;

			FETCH NEXT FROM cursor_1 INTO  @rownum, @level,  @clo_id, @path, @obj_id_aux;
		end;

		close cursor_1;
		deallocate cursor_1;

	declare	@ins_anom_Responsavel nvarchar(255) = '';
	declare @ins_anom_data nvarchar(15) = '';
	declare @ins_anom_quadroA_1 nvarchar(3) = '';
	declare @ins_anom_quadroA_2 nvarchar(500) = '';

	select  @ins_anom_Responsavel = ins_anom_Responsavel,
			@ins_anom_data = ins_anom_data,
			@ins_anom_quadroA_1 = ins_anom_quadroA_1,
			@ins_anom_quadroA_2 = ins_anom_quadroA_2
	from [dbo].[tab_inspecoes]
	where ins_id = @ins_id;


	declare @lstReparoTipos varchar(max) = '';

		SELECT  @lstReparoTipos = COALESCE(@lstReparoTipos + '; ', '') + (rpt_codigo + ':' + rpt_descricao)
		FROM dbo.tab_reparo_tipos
		where rpt_id >=0
		order by rpt_id;


	if ((select COUNT(*) from ##temp2) = 0)
	   select    @obj_codigo_TipoOAE as obj_codigo_TipoOAE
	   			,@ins_anom_Responsavel as ins_anom_Responsavel
				,@ins_anom_data as ins_anom_data
				,@ins_anom_quadroA_1 as ins_anom_quadroA_1
				,@ins_anom_quadroA_2 as ins_anom_quadroA_2
				, 0 as  	rownum
				, 0 as  	obj_id
				, 0 as  	obj_pai
				, '' as  	obj_codigo
				, 0 as  	level
				, 0 as  	path
				, null as  	ian_numero
				, ''  as  	item
				, null as  	col_Localizacao
				, 0 as  	clo_id
				, ''  as  	clo_nome
				, 0 as  	tip_id
				, ''  as  	tip_nome
				, ''  as  	obj_descricao
				, 0 as  	ian_id
				, 0 as  	obj_id
				, 0 as  	ins_id
				, ''  as  	ian_numero
				, null as  	atp_id
				, null as  	ian_sigla
				, null as  	ale_id
				, null as  	ian_quantidade
				, null as  	ian_espacamento
				, null as  	ian_largura
				, null as  	ian_comprimento
				, null as  	ian_abertura_minima
				, null as  	ian_abertura_maxima
				, null as  	aca_id
				, null as  	ian_fotografia
				, null as  	ian_croqui
				, null as  	ian_desenho
				, null as  	ian_observacoes
				, null as  	leg_id
				, null as  	ian_ativo
				, null as  	ian_deletado
				, null as  	ian_data_criacao
				, null as  	ian_criado_por
				, null as  	ian_data_atualizacao
				, null as  	ian_atualizado_por
				, null as  	ire_id_sugerido
				, null as  	ire_id_adotado
				, null as  	ins_id
				, null as  	atp_codigo
				, null as  	atp_descricao
				, null as  	ale_codigo
				, null as  	ale_descricao
				, null as  	aca_codigo
				, null as  	aca_descricao
				, null as  	leg_codigo
				, null as  	leg_descricao
				, null as  	ire_id_sugerido
				, null as  	ire_id_adotado
				, null as  	lstLegendas
				, null as  	lstAlertas
				, null as  	lstCausas
				, null as  	lstTipos
				, null as  	lstReparoIndicado
				, null as  	lstReparoTipos

	else
-- faz o select
		SELECT tmp.*
			  ,@obj_codigo_TipoOAE as obj_codigo_TipoOAE
			  ,@ins_anom_Responsavel as ins_anom_Responsavel
			  ,@ins_anom_data as ins_anom_data
			  ,@ins_anom_quadroA_1 as ins_anom_quadroA_1
			  ,@ins_anom_quadroA_2 as ins_anom_quadroA_2
			  , case when clo.clo_id < 10 then tip.tip_nome else obj_descricao end as col_Localizacao
			  ,clo.clo_id, clo.clo_nome
			  ,tip.tip_id, tip.tip_nome
			  ,obj.obj_descricao
		      ,ian.*
			  ,ins.ins_id
			  ,atp_codigo, atp_descricao
			  ,ale.ale_codigo, ale.ale_descricao 
			  ,aca.aca_codigo, aca.aca_descricao
			  ,leg.leg_codigo, leg.leg_descricao
			  , rpt1.rpt_codigo as rpt_id_adotado_codigo
			  , rpt1.rpt_descricao as rpt_id_adotado_descricao
			  , rpt1.rpt_unidade as rpt_id_adotado_unidade
			  , rpt2.rpt_codigo as rpt_id_sugerido_codigo
			  , rpt2.rpt_descricao as rpt_id_sugerido_descricao
			  , rpt2.rpt_unidade as rpt_id_sugerido_unidade

			  ,dbo.ConcatenarAnomaliaLegendas() as lstLegendas
			  ,dbo.ConcatenarAnomaliaAlertas() as lstAlertas
			  ,dbo.ConcatenarAnomaliaCausas_by_Legenda (isnull(leg.leg_codigo,'')) as lstCausas
			  ,dbo.ConcatenarAnomaliaTipos_by_Legenda(isnull(leg.leg_codigo,'')) as lstTipos
			  ,dbo.ConcatenarAnomalia_ReparoIndicado ( isnull(leg.leg_codigo,''),
														isnull(atp_codigo,''),
														isnull(ale_codigo,''),
														isnull(aca_codigo,'')) as lstReparoIndicado
			  , @lstReparoTipos as lstReparoTipos
		  FROM ##temp2 tmp 
			  inner join dbo.tab_objetos obj on obj.obj_id = tmp.obj_id --and obj.obj_deletado is null and obj.obj_ativo = 1
			  inner join dbo.tab_objeto_classes clo on clo.clo_id = obj.clo_id and clo.clo_deletado is null and clo.clo_ativo = 1
			  inner join dbo.tab_objeto_tipos tip on tip.tip_id = obj.tip_id and tip.tip_deletado is null and tip.tip_ativo = 1
		      left join dbo.tab_inspecoes_anomalias ian on ian.obj_id = tmp.obj_id and ian.ian_deletado is null and ian.ian_ativo = 1
			  left join dbo.tab_inspecoes ins on ins.ins_id = ian.ins_id --and ins.ins_deletado is null and ins.ins_ativo = 1
			  left join dbo.tab_anomalia_tipos atp on atp.atp_id = ian.atp_id --and atp.atp_deletado is null and atp.atp_ativo = 1
			  left join dbo.tab_anomalia_alertas ale on ale.ale_id = ian.ale_id-- and ale.ale_deletado is null and ale.ale_ativo = 1
			  left join dbo.tab_anomalia_causas aca on aca.aca_id = ian.aca_id --and aca.aca_deletado is null and aca.aca_ativo = 1
			  left join dbo.tab_anomalia_legendas leg on leg.leg_id = ian.leg_id --and leg.leg_deletado is null and leg.leg_ativo = 1
			  left join dbo.tab_reparo_tipos rpt1 on rpt1.rpt_id = ian.rpt_id_adotado
			  left join dbo.tab_reparo_tipos rpt2 on rpt2.rpt_id = ian.rpt_id_sugerido

		 -- where 
		  		--isnull(ian.ins_id,'') = case when isnull(@ins_id,'') = '' then '' else @ins_id end
				--and 
			--	tmp.obj_id in (select * from dbo.ConvertToTableInt(@lista_obj_ids))
		  order by rownum



END ;
