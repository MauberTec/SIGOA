CREATE PROCEDURE [dbo].[STP_SEL_INSPECAO_GRUPO_OBJETO_VARIAVEIS_VALORES] 
@ord_id int = 0
as 
begin 

declare @cabecalho1_cor varchar(7) = '#BFBFBF'; 
declare @cabecalho2_cor varchar(7) = '#D9D9D9'; 
declare @cabecalho3_cor varchar(7) = '#F2F2F2'; 

declare @ins_id int = (select distinct ins_id from tab_inspecoes where ord_id = @ord_id);
declare @obj_id int = (select distinct obj_id from tab_inspecoes where ins_id = @ins_id);
declare @obj_codigo_tipoOAE varchar(30) = (select distinct obj_codigo from tab_objetos where obj_id = @obj_id);

declare @tip_id int = 0 ;

 declare @caracterizacao_situacao_cmb varchar(max) = ''
 select @caracterizacao_situacao_cmb = COALESCE(@caracterizacao_situacao_cmb + ';', '') + convert(varchar(3), ogi_id) + ':' + ogi_item
  from  dbo.tab_objeto_grupo_objetos_variaveis_itens
    where ogv_id = 0

set @caracterizacao_situacao_cmb = substring(@caracterizacao_situacao_cmb,2, 10000); --remove o 1o ponto e virgula


declare @condicao_inspecao_cmb varchar(max) = ''
 select @condicao_inspecao_cmb = COALESCE(@condicao_inspecao_cmb + ';', '') + convert(varchar(4), ati_id) + ':' + ati_item
  from  dbo.tab_atributo_itens
    where ati_deletado is null and ati_ativo = 1	and atr_id = 129


set  @condicao_inspecao_cmb =  substring(@condicao_inspecao_cmb,2, 10000); --remove o 1o ponto e virgula

			select 
								ROW_NUMBER() OVER  ( 
										ORDER BY 
											case tip_pai
												when 11 then 1
												when 15 then 2
												when 16 then 3
												when 12 then 4
												when 13 then 5
												when 14 then 6
												when 22 then 6
												when 23 then 10
												when 24 then 11
											end, nome_grupo, variavel) AS nLinha,

					ROW_NUMBER() OVER  (PARTITION BY nome_grupo, nCabecalhoGrupo, tip_pai, variavel 
										ORDER BY 
											case tip_pai
												when 11 then 1
												when 15 then 2
												when 16 then 3
												when 12 then 4
												when 13 then 5
												when 14 then 6
												when 22 then 6
												when 23 then 10
												when 24 then 11
											end, tip_id_grupo) AS numero,

				case when (ROW_NUMBER() OVER  (PARTITION BY nome_grupo, nCabecalhoGrupo, tip_pai 
										ORDER BY 
											case tip_pai
												when 11 then 1
												when 15 then 2
												when 16 then 3
												when 12 then 4
												when 13 then 5
												when 14 then 6
												when 22 then 6
												when 23 then 10
												when 24 then 11
											end, tip_id_grupo) = 1)
											or (nCabecalhoGrupo > 0)
				then case when nCabecalhoGrupo = 0
						then nome_grupo
						else nome_pai
						end
				else '' 
				end
				AS nomeGrupo,

				(select count(*) from dbo.tab_objeto_grupo_objeto_variaveis where  
					ogv_deletado is null 
					and ogv_ativo = 1 
					and tip_id = tb1.tip_id_grupo
				) as mesclarLinhas,

				case when  tb1.ogv_id = 0 then 1 else 0 end as mesclarColunas,
				case when nCabecalhoGrupo = 1 
					then @cabecalho1_cor
					else
						case when nCabecalhoGrupo = 2
							then @cabecalho2_cor
							else @cabecalho3_cor
						end
					end as cabecalho_Cor,
				tb1.*,
				@caracterizacao_situacao_cmb as caracterizacao_situacao_cmb, 
				@condicao_inspecao_cmb as condicao_inspecao_cmb
			from (
					select 0 as obj_id, -1 as temFilhos,
						  tip_id as tip_pai, tip_nome as nome_pai,  		
						  case when tip_id  in (11,12,13,14) 
							   then 1 
							   else 
								   case when clo_id  = 8
								   then 3
								   else 2
								   end	
							   end as nCabecalhoGrupo,
						  0 as tip_id_grupo, '' as nome_grupo,
						  0 as ogv_id, '' as variavel,
						  0 as ogi_id_caracterizacao_situacao, '' as ogi_id_caracterizacao_situacao_item,
						  0 as ati_id_condicao_inspecao, '' as ati_id_condicao_inspecao_item,
						  '' as ovv_observacoes_gerais, 
						  0 as tpu_id, 
						  '' as tpu_descricao,
						  '' as  tpu_descricao_itens_cmb,
						  '' as uni_id, '' as uni_unidade,
						  0 as ovv_tpu_quantidade
					 from dbo.tab_objeto_tipos  
						where clo_id = 6 or clo_id = 7
						    --clo_id in (
								  --      select distinct clo_id
										--from tab_objetos  
										--where obj_ativo = 1 and obj_deletado is null 
										--and charindex(@obj_codigo_tipoOAE, obj_codigo) = 1
										--and clo_id >= 6
									--)
							or ( tip_id in (
								select distinct isnull(tp1.tip_pai,'') 
									from tab_objetos  obj1
									inner join tab_objeto_tipos tp1 on tp1.tip_id = obj1.tip_id and  tp1.tip_ativo = 1 and tp1.tip_deletado is null 
									where obj1.obj_ativo = 1 and obj1.obj_deletado is null 															
									and obj1.clo_id = 9 
									and charindex(@obj_codigo_tipoOAE, obj1.obj_codigo) = 1))
					union
						select 
							obj.obj_id, 
							(select case when count(*) > 0 then 1 else 0 end from tab_objetos where obj_ativo = 1 and obj_deletado is null and obj_pai = obj.obj_id) as TemFilhos,
							tippai.tip_id as tip_pai, tippai.tip_nome as nome_pai,  		
							0 as nCabecalhoGrupo,
							tip_obj.tip_id as tip_id_grupo, tip_obj.tip_nome as nome_grupo,
							ogv.ogv_id, ogv_nome as variavel,
 		
						    isnull(ivv.ogi_id_caracterizacao_situacao,'') as ogi_id_caracterizacao_situacao,
						    isnull(ogi1.ogi_item,'') as ogi_id_caracterizacao_situacao_item,	
							
						    isnull(atv.ati_id,0) as ati_id_condicao_inspecao,
						    isnull(ati.ati_item,'') as ati_id_condicao_inspecao_item,

						    isnull(ivv_observacoes_gerais,'') as ovv_observacoes_gerais, 
						    isnull(ivv.tpu_id,'') as tpu_id, 
					        isnull(ocp.ocp_descricao_servico,'') as tpu_descricao,
							dbo.Concatenar_Politica_descricao_servico (ogv.ogv_id) as  tpu_descricao_itens_cmb,

						    isnull(uni.uni_id,'') as uni_id, 
							isnull(ivv_unidade_servico,'') as uni_unidade,
						    isnull(ivv_tpu_quantidade,'') as ovv_tpu_quantidade
					from tab_objetos obj
						inner join tab_objeto_grupo_objeto_variaveis ogv on ogv.tip_id = obj.tip_id and ogv.ogv_deletado is null
						left join  tab_inspecoes_grupo_objeto_variaveis_valores ivv on ivv.ogv_id = ogv.ogv_id and ivv.obj_id=obj.obj_id and ivv.ivv_deletado is null AND  ivv.ins_id = @ins_id--or ivv.ogv_id is null 
	  				    inner join dbo.tab_objeto_tipos tip_obj on tip_obj.tip_id = obj.tip_id and tip_obj.tip_deletado is null and tip_obj.tip_ativo =1
						inner join dbo.tab_objeto_tipos tippai on tippai.tip_id = tip_obj.tip_pai and tippai.tip_deletado is null and tippai.tip_ativo =1

						left join dbo.tab_objeto_grupo_objetos_variaveis_itens ogi1 on ogi1.ogi_id = ivv.ogi_id_caracterizacao_situacao and ogi1.ogi_ativo = 1 and ogi1.ogi_deletado is null and ogi1.ogv_id=0
						left join dbo.tab_tpu_precos_unitarios tpu on tpu.tpu_id = ivv.tpu_id 
						left join dbo.tab_unidades_medida uni on uni.uni_id = tpu.uni_id

						left join dbo.tab_inspecao_atributos_valores atv on atv.obj_id = obj.obj_id and atv.atr_id = 129 AND atv.ins_id = @ins_id 
						left join dbo.tab_atributo_itens ati on ati.ati_id = atv.ati_id 

						 left join dbo.tab_objeto_conserva_politica ocp on ocp.ogi_id_caracterizacao_situacao = ivv.ogi_id_caracterizacao_situacao and ocp.ogv_id= ivv.ogv_id and ocp_deletado is null and ocp_ativo = 1

					where obj.obj_deletado is null
						and obj.clo_id = 9 -- classe de objetos
					    and  obj_ativo = 1 
					    and obj.obj_codigo like  @obj_codigo_tipoOAE + '%' 
						and  tip_obj.tip_pai in (
														  select distinct isnull(tp1.tip_pai,'') 
														  from tab_objetos  obj1
														    inner join tab_objeto_tipos tp1 on tp1.tip_id = obj1.tip_id and  tp1.tip_ativo = 1 and tp1.tip_deletado is null 
														  where obj1.obj_ativo = 1 and obj1.obj_deletado is null 															
															and obj1.clo_id = 9 
															and obj1.obj_codigo like  @obj_codigo_tipoOAE + '%' )

					) tb1
					  order by 
							   case tip_pai
								  when 11 then 1
								  when 15 then 2
								  when 16 then 3
								  when 12 then 4
								  when 13 then 5
								  when 14 then 6
								  when 22 then 6
								  when 20 then 7
								  when 21 then 8
								  when 26 then 9
								  when 23 then 10
								  when 29 then 11
								  when 30 then 12
								  when 31 then 13
							   else 30 --needed only is no IN clause above
							   end,
							   nome_grupo, variavel, ogv_id;

end ;
