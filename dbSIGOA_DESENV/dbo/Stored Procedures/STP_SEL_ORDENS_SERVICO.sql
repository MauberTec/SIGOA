CREATE PROCEDURE [dbo].[STP_SEL_ORDENS_SERVICO] 
@ord_id int = -1,
@filtroOrdemServico_codigo varchar(100)= null,
@filtroObj_codigo varchar(100)=null,
@filtroTiposOS int = null,
@filtroStatusOS int = null,
@filtroData varchar(50)= 'Inicio_Programada',
@filtroord_data_De varchar(15)= '01/01/2000',
@filtroord_data_Ate varchar(15)= '31/12/2100'


AS 
BEGIN 

--declare @ord_id int = 0
--declare @filtroOrdemServico_codigo varchar(100)= null
--declare @filtroObj_codigo varchar(100)=null
--declare @filtroTiposOS int = null
--declare @filtroStatusOS int = null
--declare @filtroData varchar(50)= 'Inicio_Execucao'
--declare @filtroord_data_De varchar(15)= '01/01/2000'
--declare @filtroord_data_Ate varchar(15)= '31/12/2100'


set nocount on
		declare @ord_id_orig int = @ord_id;
		declare @ord_pai int ;

		-- checa se as tmp table existe e a exclui
		IF OBJECT_ID('tempdb..#temp1') IS NOT NULL
			DROP TABLE #temp1;

			-- cria a temporaria 
			select 
					'000' as row_num,
					1000 as temFilhos,
					1 as nNivel,
					ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario
			into #temp1
			from dbo.tab_ordens_servico
				where ord_deletado is null
				--and ord_ativo = 1
				and (ord_pai < 0)
			order by ord_codigo;

			-- alter table row_num
			alter table #temp1 alter column row_num varchar(max);
			alter table #temp1 add row_expandida bit;

			-- limpa deixando somente a estrutura
			delete from #temp1;

if (@ord_id <> 0) -- objetoID = 0 ==> todos; ord_id > 0 ==> expande; objID < 0 ==> encolhe
begin
			-- acha os filhos e insere na temporaria
		if (@ord_id > 0)
			insert into #temp1 (row_num, temFilhos, nNivel, row_expandida, ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario)
			select 
					RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY ord_codigo))),3) as row_num,
					(select count(*) from dbo.tab_ordens_servico tb2 where ord_deletado is null  and ord_pai = tb1.ord_id)  as temFilhos,
					dbo.nivel_hierarquico_ordem_servico(ord_id) as nNivel,
					0 as row_expandida,
					ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario
			from dbo.tab_ordens_servico tb1
				where ord_deletado is null
				--and ord_ativo = 1
				and ord_pai = @ord_id
			order by ord_codigo;
		else
			set @ord_id = - @ord_id; --inverte o sinal

		-- acha os irmaos
		declare @ord_objeto_rownum varchar(max); 
		declare @ord_objeto_rownum_pai varchar(max); 

		set @ord_pai = (select isnull(ord_pai,-1) from  dbo.tab_ordens_servico where ord_id = @ord_id);
		select @ord_objeto_rownum = row_num
		from (
				select 
						RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY ord_codigo))),3) as row_num,
						(select count(*) from dbo.tab_ordens_servico tb2 where ord_deletado is null and ord_pai = tb3.ord_id)  as temFilhos,
						dbo.nivel_hierarquico_ordem_servico(tb3.ord_id) as nNivel,
						ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario
				from dbo.tab_ordens_servico tb3
					where ord_deletado is null
					--and ord_ativo = 1
					and (isnull(ord_pai,-1) = @ord_pai)
			) as tb1
		where ord_id = @ord_id;

		-- coloca o prefixo nos filhos
		update #temp1 set row_num = @ord_objeto_rownum + '.' + row_num;

		-- insere os irmaos
		insert into #temp1 (row_num, temFilhos, nNivel, row_expandida, ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario)
		select 
				RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY ord_codigo))),3) as row_num,
				(select count(*) from dbo.tab_ordens_servico tb2 where ord_deletado is null and ord_pai = tb1.ord_id)  as temFilhos,
				dbo.nivel_hierarquico_ordem_servico(ord_id) as nNivel,
				0 as row_expandida,
				ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario
		from dbo.tab_ordens_servico tb1
			where ord_deletado is null
			--and ord_ativo = 1
			and isnull(ord_pai,-1) = @ord_pai
		order by ord_codigo;

		-- coloca o flag row_expandida no pai
		if (@ord_id_orig > 0)
			update #temp1 set row_expandida = 1 where ord_id = @ord_id_orig;
		else
			update #temp1 set row_expandida = 0 where ord_id = -@ord_id_orig;


	-- ************** começa a subir na hierarquia *****************************
			declare @i int = 0;
	if @ord_pai >0
	begin
				while ((@i < 100) )
				begin
					-- acha o item pai e os tios:novo @ord_id  = ord_pai
						set @ord_id = (select ord_pai from dbo.tab_ordens_servico where ord_id = @ord_id);
						set @ord_pai = (select isnull(ord_pai,-1) from dbo.tab_ordens_servico where ord_id = @ord_id);

						-- procura a posicao do item pai
						select @ord_objeto_rownum = row_num
						from (
								select 
										RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY ord_codigo))),3) as row_num,
										(select count(*) from dbo.tab_ordens_servico tb2 where ord_deletado is null  and ord_pai = tb3.ord_id)  as temFilhos,
										dbo.nivel_hierarquico_ordem_servico(ord_id) as nNivel,
										tb3.ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario
								from dbo.tab_ordens_servico tb3
									where ord_deletado is null
									--and ord_ativo = 1
									and (isnull(ord_pai,-1) = @ord_pai)
							) as tb1
						where tb1.ord_id = @ord_id;

					-- coloca o prefixo nos filhos
					update #temp1 set row_num = @ord_objeto_rownum + '.' + row_num;

					-- insere os irmaos
					insert into #temp1 (row_num, temFilhos, nNivel, row_expandida, ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario)
					select 
							RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY ord_codigo))),3) as row_num,
							(select count(*) from dbo.tab_ordens_servico tb2 where ord_deletado is null  and ord_pai = tb1.ord_id)  as temFilhos,
							dbo.nivel_hierarquico_ordem_servico(ord_id) as nNivel,
							0 as row_expandida,
							ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario
					from dbo.tab_ordens_servico tb1
						where ord_deletado is null
						--and ord_ativo = 1
						and isnull(ord_pai,-1) = @ord_pai
					order by ord_codigo;


					-- acerta o flag row_expandida
					update #temp1 set row_expandida = 1 where ord_id = @ord_id;

					if ((@ord_pai is null) or (@ord_pai< 0))
					  set @i = 150;

				end; -- while
	end

	-- ********* remove os irmaos da raiz, deixando somente o nó do objeto selecionado
	declare @prefixo nvarchar(3) = (select top 1 substring(row_num,1,3) from #temp1 where ord_id = ABS( @ord_id_orig ) );
	delete from #temp1
	where row_num not like (@prefixo + '%') 

end
else
if (@ord_id = 0) -- tudo
begin
	--if (isnull(@filtroOrdemServico_codigo,'') <> '' 
	--	or isnull(@filtroObj_codigo,'') <> '' 
	--	or @filtroTiposOS is not null 
	--	or @filtroStatusOS is not null
	--	or isnull(@filtroord_data_De,'') <> ''
	--	or isnull(@filtroord_data_Ate,'') <> ''
	--	)
	--begin
			insert into #temp1 (row_num, temFilhos, nNivel, row_expandida, ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario)
			select 
					RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY ord_codigo))),3) as row_num,
					(select count(*) from dbo.tab_ordens_servico tb2 where ord_deletado is null  and ord_pai = tb1.ord_id)  as temFilhos,
					dbo.nivel_hierarquico_ordem_servico(tb1.ord_id)  as nNivel,
					1 as row_expandida,
					ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, tb1.obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario
			from dbo.tab_ordens_servico tb1
			left join dbo.tab_objetos obj on obj.obj_id = tb1.obj_id and obj.obj_deletado is null 
				where ord_deletado is null
				--and	ord_codigo like '%' + isnull(@filtroOrdemServico_codigo, '')  + '%'
				--and	isnull(obj_codigo,'') like '%' + isnull(@filtroObj_codigo, '') + '%'
				--and isnull(tos_id,'') = case when @filtroTiposOS is null  then isnull(tos_id,'') else @filtroTiposOS end
				--and isnull(sos_id,'') = case when @filtroStatusOS is null then isnull(sos_id,'') else @filtroStatusOS end

				--and	((ord_data_inicio_execucao >= case when @filtroord_data_De = '' then convert(datetime, '01/01/2000',103) else convert(datetime, @filtroord_data_De + ' 00:00:00', 103)  end)
				--      or (ord_data_inicio_execucao is null)
				--	  )
				--and	((ord_data_inicio_execucao <= case when @filtroord_data_Ate = '' then convert(datetime, '31/12/2099',103) else convert(datetime, @filtroord_data_Ate + ' 23:59:59', 103) end)
				--      or (ord_data_inicio_execucao is null)
				--	  )
			order by ord_codigo;

--	end
--	else
--	begin

--			insert into #temp1 (row_num, temFilhos, nNivel, row_expandida, ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario)
--			select 
--					RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY ord_codigo))),3) as row_num,
--					(select count(*) from dbo.tab_ordens_servico tb2 where ord_deletado is null  and ord_pai = tb1.ord_id)  as temFilhos,
--					1  as nNivel,
--					0 as row_expandida,
--					ord_id, ord_codigo, ord_descricao, ord_pai, ocl_id, tos_id, sos_id, obj_id, ord_ativo, ord_criticidade, con_id, ord_data_inicio_programada, ord_data_termino_programada, ord_data_inicio_execucao, ord_data_termino_execucao, ord_quantidade_estimada, uni_id_qt_estimada, ord_quantidade_executada, uni_id_qt_executada, ord_custo_estimado, ord_custo_final, ord_aberta_por, ord_data_abertura, ord_responsavel_der, ord_responsavel_fiscalizacao, con_id_fiscalizacao, ord_responsavel_execucao, con_id_execucao, ord_responsavel_suspensao, ord_data_suspensao, ord_responsavel_cancelamento, ord_data_cancelamento, ord_data_reinicio, con_id_orcamento, tpt_id, tpu_data_base_der, tpu_id, tpu_preco_unitario
--			from dbo.tab_ordens_servico tb1
--				where ord_deletado is null
--				--and ord_ativo = 1
--				and (ord_pai is null)
--			order by ord_codigo;
--   end
end


-- alter table row_num
alter table #temp1 add row_numero float;
update #temp1 set row_num =  case when LEN(row_num) > 3 then substring(row_num,1,3) + ',' + REPLACE( substring(row_num,4,LEN(row_num)-1), '.','') else  row_num end
update #temp1 set row_numero = convert(float, replace(row_num,',','.'))

update #temp1 set nNivel = nNivel -1;


-- checa se as tmp table existe e a exclui
IF OBJECT_ID('tempdb..#temp2') IS NOT NULL
	DROP TABLE #temp2;

select tmp.*, 
		ord1.ord_codigo as ord_codigo_pai, ord1.ord_descricao as ord_descricao_pai,
		ocl.ocl_codigo, ocl.ocl_descricao,
		tos.tos_codigo, tos.tos_descricao,
		sos.sos_codigo, sos.sos_descricao,
		obj.obj_codigo, obj.obj_descricao,
		tpu.tpu_codigo_der, tpu.tpu_descricao,
		con1.con_codigo, con1.con_descricao,
		con2.con_codigo as con_codigofiscalizacao, con2.con_descricao as con_descricaofiscalizacao,
		con3.con_codigo as con_codigoexecucao, con3.con_descricao as con_descricaoexecucao,
		con4.con_codigo as con_codigoorcamento, con4.con_descricao as con_descricaoorcamento,
		usu.usu_usuario as ord_aberta_por_usuario, usu.usu_nome as ord_aberta_por_nome
into  #temp2
from #temp1 tmp
		  left join dbo.tab_ordens_servico ord1 on ord1.ord_id = tmp.ord_pai and ord1.ord_deletado is null and ord1.ord_ativo = 1
		  left join dbo.tab_ordem_servico_classes ocl on ocl.ocl_id = tmp.ocl_id and ocl.ocl_deletado is null and ocl.ocl_ativo = 1
		  left join dbo.tab_ordem_servico_tipos tos on tos.tos_id = tmp.tos_id and tos.tos_deletado is null and tos.tos_ativo = 1
		  left join dbo.tab_ordem_servico_status sos on sos.sos_id = tmp.sos_id and sos.sos_deletado is null and sos.sos_ativo = 1
		  left join dbo.tab_objetos obj on obj.obj_id = tmp.obj_id and obj.obj_deletado is null and obj.obj_ativo = 1
		  left join dbo.tab_tpu_tipos_preco tpt on tpt.tpt_id = tmp.tpt_id and tpt.tpt_deletado is null and tpt.tpt_ativo = 1
		  left join dbo.tab_tpu_precos_unitarios tpu on tpu.tpu_data_base_der = tmp.tpu_data_base_der and tpu.tpt_id = tmp.tpt_id
														and tpu.tpu_deletado is null and tpu.tpu_ativo = 1
		  left join dbo.tab_unidades_medida uni3 on uni3.uni_id = tmp.uni_id_qt_estimada and uni3.uni_deletado is null and uni3.uni_ativo = 1
		  left join dbo.tab_unidades_medida uni4 on uni4.uni_id = tmp.uni_id_qt_executada and uni4.uni_deletado is null and uni4.uni_ativo = 1

		  left join dbo.tab_contratos con1 on con1.con_id = tmp.con_id and con1.con_deletado is null and con1.con_ativo = 1
		  left join dbo.tab_contratos con2 on con2.con_id = tmp.con_id_fiscalizacao and con2.con_deletado is null and con2.con_ativo = 1
		  left join dbo.tab_contratos con3 on con3.con_id = tmp.con_id_execucao and con3.con_deletado is null and con3.con_ativo = 1
		  left join dbo.tab_contratos con4 on con4.con_id = tmp.con_id_orcamento and con4.con_deletado is null and con4.con_ativo = 1

		  left join dbo.tab_usuarios usu on usu.usu_id = tmp.ord_aberta_por and usu.usu_deletado is null and usu.usu_ativo = 1

where 
		tmp.ord_codigo like '%' + isnull(@filtroOrdemServico_codigo, '') + '%'
	and	isnull(obj.obj_codigo,'%') like '%' + isnull(@filtroObj_codigo, '') + '%'
	and isnull(tmp.sos_id,-1) = case when @filtroStatusOS is null then isnull(tmp.sos_id,-1) else @filtroStatusOS end
	and isnull(tmp.tos_id,-1) = case when @filtroTiposOS is null then isnull(tmp.tos_id,-1) else @filtroTiposOS end
	
	--and isnull(tmp.ord_data_inicio_execucao, convert(date,'02/02/1900')) between  case when @filtroord_data_De is null then convert(date,'01/01/1900') else @filtroord_data_De end
	--									 and  case when @filtroord_data_Ate is null then convert(date,'31/12/2300') else @filtroord_data_Ate end
	-- order by row_num;


	-- cria a lista dos status possiveis 
	alter table #temp2 add lst_proximos_status varchar(max);
	update #temp2 set lst_proximos_status = dbo.ConcatenarStatusOS(sos_id);



	if (@filtroData = 'Abertura')
		select * 
		from  #temp2 tmp
		where  isnull(tmp.ord_data_abertura, convert(date,'02/02/1900')) between  case when @filtroord_data_De is null then convert(date,'01/01/1900') else @filtroord_data_De end
			   and  case when @filtroord_data_Ate is null then convert(date,'31/12/2300') else @filtroord_data_Ate end
		order by row_num;
	else
		if (@filtroData = 'Inicio_Programada')
			select * 
			from  #temp2 tmp
			where  isnull(tmp.ord_data_inicio_programada, convert(date,'02/02/1900')) between  case when @filtroord_data_De is null then convert(date,'01/01/1900') else @filtroord_data_De end
				   and  case when @filtroord_data_Ate is null then convert(date,'31/12/2300') else @filtroord_data_Ate end
			order by row_num;
		else
			if (@filtroData = 'Termino_Programada')
				select * 
				from  #temp2 tmp
				where  isnull(tmp.ord_data_termino_programada, convert(date,'02/02/1900')) between  case when @filtroord_data_De is null then convert(date,'01/01/1900') else @filtroord_data_De end
					   and  case when @filtroord_data_Ate is null then convert(date,'31/12/2300') else @filtroord_data_Ate end
				order by row_num;
			else
				if (@filtroData = 'Inicio_Execucao')
					select * 
					from  #temp2 tmp
					where  isnull(tmp.ord_data_inicio_execucao, convert(date,'02/02/1900')) between  case when @filtroord_data_De is null then convert(date,'01/01/1900') else @filtroord_data_De end
						   and  case when @filtroord_data_Ate is null then convert(date,'31/12/2300') else @filtroord_data_Ate end
					order by row_num;
				else
					if (@filtroData = 'Termino_Execucao')
						select * 
						from  #temp2 tmp
						where  isnull(tmp.ord_data_termino_execucao, convert(date,'02/02/1900')) between  case when @filtroord_data_De is null then convert(date,'01/01/1900') else @filtroord_data_De end
							   and  case when @filtroord_data_Ate is null then convert(date,'31/12/2300') else @filtroord_data_Ate end
						order by row_num;
					else
						if (@filtroData = 'Suspensao')
							select * 
							from  #temp2 tmp
							where  isnull(tmp.ord_data_suspensao, convert(date,'02/02/1900')) between  case when @filtroord_data_De is null then convert(date,'01/01/1900') else @filtroord_data_De end
								   and  case when @filtroord_data_Ate is null then convert(date,'31/12/2300') else @filtroord_data_Ate end
							order by row_num;
						else
							if (@filtroData = 'Cancelamento')
								select * 
								from  #temp2 tmp
								where  isnull(tmp.ord_data_cancelamento, convert(date,'02/02/1900')) between  case when @filtroord_data_De is null then convert(date,'01/01/1900') else @filtroord_data_De end
									   and  case when @filtroord_data_Ate is null then convert(date,'31/12/2300') else @filtroord_data_Ate end
								order by row_num;
							else
								if (@filtroData = 'Reinicio')
									select * 
									from  #temp2 tmp
									where  isnull(tmp.ord_data_reinicio, convert(date,'02/02/1900')) between  case when @filtroord_data_De is null then convert(date,'01/01/1900') else @filtroord_data_De end
										   and  case when @filtroord_data_Ate is null then convert(date,'31/12/2300') else @filtroord_data_Ate end
									order by row_num;

		-- checa se as tmp tables existem e as exclui
	IF OBJECT_ID('tempdb..#temp1') IS NOT NULL
		DROP TABLE #temp1;

	IF OBJECT_ID('tempdb..#temp2') IS NOT NULL
		DROP TABLE #temp2;


  END ;
