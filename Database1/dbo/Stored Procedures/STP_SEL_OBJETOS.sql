--Select Employees 

CREATE PROCEDURE [dbo].[STP_SEL_OBJETOS] 
@obj_id int = -1,
@filtro_obj_codigo varchar(100)= null,
@filtro_obj_descricao varchar(100)=null,
@filtro_clo_id int = null,
@filtro_tip_id int = null

AS 
BEGIN 
		declare @obj_id_orig int = @obj_id;

		declare @obj_pai int ;

		-- checa se as tmp table existe e a exclui
		IF OBJECT_ID('tempdb..#temp1') IS NOT NULL
			DROP TABLE #temp1;

			-- cria a temporaria 
			select 
					'000' as row_num,
					1000 as temFilhos,
					1 as nNivel,
					obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por
			into #temp1
			from dbo.tab_objetos
				where obj_deletado is null
				--and obj_ativo = 1
				and (obj_pai < 0)
			order by obj_codigo;

			-- alter table row_num
			alter table #temp1 alter column row_num varchar(max);
			alter table #temp1 add row_expandida bit;

			-- limpa deixando somente a estrutura
			delete from #temp1;

if (@obj_id <> 0) -- objetoID = 0 ==> todos; objId > 0 ==> expande; objID < 0 ==> encolhe
begin
			-- acha os filhos e insere na temporaria
		if (@obj_id > 0)
			insert into #temp1 (row_num, temFilhos, nNivel, row_expandida, obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por)
			select 
				--	RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY obj_codigo))),3) as row_num,
					RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY clo_id,tip_id))),3) as row_num,
					(select count(*) from dbo.tab_objetos tb2 where obj_deletado is null  and obj_pai = tb1.obj_id)  as temFilhos,
					dbo.nivel_hierarquico_objeto(obj_id) as nNivel,
					0 as row_expandida,
					obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por
			from dbo.tab_objetos tb1
				where obj_deletado is null
				--and obj_ativo = 1
				and obj_pai = @obj_id
			order by obj_codigo;
		else
			set @obj_id = - @obj_id; --inverte o sinal

		-- acha os irmaos
		declare @obj_objeto_rownum varchar(max); 
		declare @obj_objeto_rownum_pai varchar(max); 

		set @obj_pai = (select isnull(obj_pai,-1) from  dbo.tab_objetos where obj_id = @obj_id);
		select @obj_objeto_rownum = row_num
		from (
				select 
						--RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY obj_codigo))),3) as row_num,
						RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY clo_id,tip_id))),3) as row_num,
						(select count(*) from dbo.tab_objetos tb2 where obj_deletado is null and obj_pai = tb3.obj_id)  as temFilhos,
						dbo.nivel_hierarquico_objeto(tb3.obj_id) as nNivel,
						obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por
				from dbo.tab_objetos tb3
					where obj_deletado is null
					--and obj_ativo = 1
					and (isnull(obj_pai,-1) = @obj_pai)
			) as tb1
		where obj_id = @obj_id;

		-- coloca o prefixo nos filhos
		update #temp1 set row_num = @obj_objeto_rownum + '.' + row_num;

		-- insere os irmaos
		insert into #temp1 (row_num, temFilhos, nNivel, row_expandida, obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por)
		select 
				--RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY obj_codigo))),3) as row_num,
				RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY clo_id,tip_id))),3) as row_num,
				(select count(*) from dbo.tab_objetos tb2 where obj_deletado is null and obj_pai = tb1.obj_id)  as temFilhos,
				dbo.nivel_hierarquico_objeto(obj_id) as nNivel,
				0 as row_expandida,
				obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por
		from dbo.tab_objetos tb1
			where obj_deletado is null
			--and obj_ativo = 1
			and isnull(obj_pai,-1) = @obj_pai
		order by obj_codigo;

		-- coloca o flag row_expandida no pai
		if (@obj_id_orig > 0)
			update #temp1 set row_expandida = 1 where obj_id = @obj_id_orig;
		else
			update #temp1 set row_expandida = 0 where obj_id = -@obj_id_orig;


	-- ************** começa a subir na hierarquia *****************************
			declare @i int = 0;
	if @obj_pai >0
	begin
				while ((@i < 100) )
				begin
					-- acha o item pai e os tios:novo @obj_id  = obj_pai
						set @obj_id = (select obj_pai from dbo.tab_objetos where obj_id = @obj_id);
						set @obj_pai = (select isnull(obj_pai,-1) from dbo.tab_objetos where obj_id = @obj_id);

						-- procura a posicao do item pai
						select @obj_objeto_rownum = row_num
						from (
								select 
									--	RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY obj_codigo))),3) as row_num,
										RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY clo_id,tip_id))),3) as row_num,
										(select count(*) from dbo.tab_objetos tb2 where obj_deletado is null  and obj_pai = tb3.obj_id)  as temFilhos,
										dbo.nivel_hierarquico_objeto(obj_id) as nNivel,
										tb3.obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por
								from dbo.tab_objetos tb3
									where obj_deletado is null
									--and obj_ativo = 1
									and (isnull(obj_pai,-1) = @obj_pai)
							) as tb1
						where tb1.obj_id = @obj_id;

					-- coloca o prefixo nos filhos
					update #temp1 set row_num = @obj_objeto_rownum + '.' + row_num;

					-- insere os irmaos
					insert into #temp1 (row_num, temFilhos, nNivel, row_expandida, obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por)
					select 
						--	RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY obj_codigo))),3) as row_num,
							RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY clo_id,tip_id))),3) as row_num,
							(select count(*) from dbo.tab_objetos tb2 where obj_deletado is null  and obj_pai = tb1.obj_id)  as temFilhos,
							dbo.nivel_hierarquico_objeto(obj_id) as nNivel,
							0 as row_expandida,
							obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por
					from dbo.tab_objetos tb1
						where obj_deletado is null
						--and obj_ativo = 1
						and isnull(obj_pai,-1) = @obj_pai
					order by obj_codigo;


					-- acerta o flag row_expandida
					update #temp1 set row_expandida = 1 where obj_id = @obj_id;

					if ((@obj_pai is null) or (@obj_pai< 0))
					  set @i = 150;

				end; -- while
	end

	-- ********* remove os irmaos da raiz, deixando somente o nó do objeto selecionado
	declare @rownum_obj_orig nvarchar(20) = (select top 1 substring(row_num,1,3) from #temp1 where obj_id = abs(@obj_id_orig));
	delete from #temp1
	where  row_num not like (@rownum_obj_orig + '%') 





	/*
	-- **********  ADENDO PARA FILTRAR SOMENTE OS ITENS HIERARQUICOS VERTICAIS DO OBJETO *****************
	-- cria lista de obj_ids subindo a hierarquia
	declare @obj_pai_aux int = -1
	declare @obj_id_aux int = @obj_id
	declare @lista_obj_ids varchar(max) = convert(varchar(3), @obj_id_aux);

	while @obj_pai_aux <> ''
	begin
		set @obj_pai_aux = ( select isnull(obj_pai, '')
							from dbo.tab_objetos
							where obj_id = @obj_id_aux);

		set @lista_obj_ids = @lista_obj_ids + ',' +  convert(varchar(3), @obj_pai_aux);
		set @obj_id_aux = @obj_pai_aux;
		end

	delete from #temp1
	where (obj_id not IN (select * from  dbo.ConvertToTableInt(@lista_obj_ids)) or obj_id is null)

	-- **********  end ADENDO ***********************
*/

end
else
if (@obj_id = 0) -- tudo
begin
	if (isnull(@filtro_obj_codigo,'') <> '' or isnull(@filtro_obj_descricao,'') <> '' or @filtro_clo_id is not null or @filtro_tip_id is not null)
	begin

			IF OBJECT_ID('tempdb..#temp0') IS NOT NULL
				DROP TABLE #temp0;

			select clo_id,  tip_id, obj_codigo, obj_id, obj_pai
			into #temp0
			from dbo.tab_objetos 
				where obj_deletado is null
							--and (obj_pai is null)
							and	obj_codigo like '%' + @filtro_obj_codigo + '%'
							and	obj_descricao like '%' + isnull(@filtro_obj_descricao, '') + '%'
							and isnull(tip_id, '') = case when @filtro_tip_id is null then isnull(tip_id, '') else @filtro_tip_id end

							--and isnull(clo_id, '') = case when @filtro_clo_id is null then isnull(clo_id, '') else @filtro_clo_id end
							and isnull(clo_id, '') in ( case when @filtro_clo_id is null 
																	 then isnull(clo_id, '') 
																	 else 
																		case when @filtro_clo_id = -13 
																		then  2
																		else @filtro_clo_id
																		end
																	 end
														  ,case when @filtro_clo_id is null 
																	 then isnull(clo_id, '') 
																	 else 
																		case when @filtro_clo_id = -13 
																		then  3
																		else @filtro_clo_id
																		end
																	 end
														);

			if( isnull(@filtro_clo_id,0) <> -13)
				insert into #temp1 (row_num, temFilhos, nNivel, row_expandida, obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por)
				 select 
 					RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY tb1.obj_codigo))),3) as row_num,
									(select count(*) from dbo.tab_objetos tb2 where obj_deletado is null  and obj_pai = tb1.obj_id)  as temFilhos,
									dbo.nivel_hierarquico_objeto(tb1.obj_id)  as nNivel,
									0 as row_expandida,
									tb1.obj_id, tb1.clo_id, tb1.tip_id, tb1.obj_codigo, obj_descricao, tb1.obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por 
				 from #temp0 tb1
				 inner join ( select  min(clo_id) as clo_id, left(obj_codigo,6) as obj_codigo_parcial
								from #temp0
								group by left(obj_codigo,6)
							) tmp2 on tmp2.clo_id = tb1.clo_id and tmp2.obj_codigo_parcial = left(tb1.obj_codigo,6)
				 inner join dbo.tab_objetos obj on obj.obj_id = tb1.obj_id
				order by  clo_id, tip_id, obj_codigo ;
			else
				insert into #temp1 (row_num, temFilhos, nNivel, row_expandida, obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por)
				 select 
 					RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY tb1.obj_codigo))),3) as row_num,
									(select count(*) from dbo.tab_objetos tb2 where obj_deletado is null  and obj_pai = tb1.obj_id)  as temFilhos,
									dbo.nivel_hierarquico_objeto(tb1.obj_id)  as nNivel,
									0 as row_expandida,
									tb1.obj_id, tb1.clo_id, tb1.tip_id, tb1.obj_codigo, obj_descricao, tb1.obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por 
				 from #temp0 tb1
				 inner join dbo.tab_objetos obj on obj.obj_id = tb1.obj_id
				order by  clo_id, tip_id, obj_codigo ;

	end
	else
	begin

			insert into #temp1 (row_num, temFilhos, nNivel, row_expandida, obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por)
			select 
					RIGHT('000'+ CONVERT(VARCHAR,(ROW_NUMBER() OVER (ORDER BY obj_codigo))),3) as row_num,
					(select count(*) from dbo.tab_objetos tb2 where obj_deletado is null  and obj_pai = tb1.obj_id)  as temFilhos,
					1  as nNivel,
					0 as row_expandida,
					obj_id, clo_id, tip_id, obj_codigo, obj_descricao, obj_pai, obj_organizacao, obj_departamento, obj_status, obj_arquivo_kml, obj_ativo, obj_deletado, obj_data_criacao, obj_criado_por, obj_data_atualizacao, obj_atualizado_por
			from dbo.tab_objetos tb1
				where obj_deletado is null
				--and obj_ativo = 1
				and (obj_pai is null)
			order by obj_codigo;
   end
end



-- alter table row_num
alter table #temp1 add row_numero float;
update #temp1 set row_num =  case when LEN(row_num) > 3 then substring(row_num,1,3) + ',' + REPLACE( substring(row_num,4,LEN(row_num)-1), '.','') else  row_num end
update #temp1 set row_numero = convert(float, replace(row_num,',','.'))

update #temp1 set nNivel = nNivel -1;

-- acrescenta coluna podeDeletar
alter table #temp1 add obj_podeDeletar bit;
update #temp1 set obj_podeDeletar = 1; -- todos =  true

-- faz os ajustes

declare @nn int =0;
update #temp1 
set obj_podeDeletar = dbo.checa_pode_deletar_objeto(obj_id);


-- SELECT DE FINAL DE RETORNO
select tmp.*, 
		cls.clo_nome, cls.clo_descricao,
		tip.tip_nome, tip.tip_descricao
from #temp1 tmp
	inner join dbo.tab_objeto_classes cls on cls.clo_id = tmp.clo_id and cls.clo_ativo=1 and cls.clo_deletado is null
	left join dbo.tab_objeto_tipos tip on tip.tip_id = tmp.tip_id and tip.tip_ativo=1 and tip.tip_deletado is null

--where 
--		tmp.obj_codigo like '%' + isnull(@filtro_obj_codigo, '') + '%'
--	and	tmp.obj_descricao like '%' + isnull(@filtro_obj_descricao, '') + '%'
--	and tmp.clo_id = case when @filtro_clo_id is null then tmp.clo_id else @filtro_clo_id end
--	and tmp.tip_id = case when @filtro_tip_id is null then tmp.tip_id else @filtro_tip_id end

order by row_num;

		-- checa se as tmp tables existem e as exclui
	IF OBJECT_ID('tempdb..#temp1') IS NOT NULL
		DROP TABLE #temp1;
  END ;
