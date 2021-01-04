CREATE procedure [dbo].[STP_UPD_INSPECAO_ATRIBUTO_VALORES] 
@obj_id int,
@atr_id int = null,
@ati_id int = null,
@nome_aba varchar(200) = null,
@atv_valores varchar(max) = null,
@codigoOAE varchar(200) = null,
@selidTipoOAE int = null,
@uni_id int = null,
@ord_id int = null,
@usu_id int,
@ip nvarchar(30)

--with encryption
as

	declare @ins_id bigint = (select ins_id from dbo.tab_inspecoes where ins_deletado is null and ins_ativo =1 and ord_id= @ord_id);
	declare @sos_id bigint = (select sos_id from dbo.tab_ordens_servico where ord_id = @ord_id);
	declare @iav_valores_log varchar(max) = '';
	declare @iav_id_old bigint = 0;
	declare @iav_valor_old varchar(max) = 0;
	declare @iav_valor varchar(max) = '';
	declare @jatem_insp bigint = 0;


declare @clo_id int = null;
declare @tip_id int = null;

declare @obj_id_Rodovia int = -1

declare @obj_id_ObraDeArte int = -1
declare @obj_id_TipoObraDeArte int = -1
declare @clo_id_aux int = (select  clo_id from dbo.tab_objetos where obj_id = @obj_id);

declare @obj_id_aba int =0;		
declare @obj_codigo_rodovia varchar(10) = '';

declare @codigo_TipoOAE varchar(50) ='';
		set @obj_id_aba = @obj_id;
		set @obj_id_TipoObraDeArte = @obj_id;
				set @clo_id = 3;

if (@nome_aba is not null)
begin
		if (@nome_aba = 'SUPERESTRUTURA') 
		begin
			set @clo_id = 6;
			set @tip_id = 11;
		end
			else
				if (@nome_aba = 'MESOESTRUTURA') 
				begin
					set @clo_id = 6;
					set @tip_id = 12;
				end
				else
					if (@nome_aba = 'INFRAESTRUTURA') 
					begin
						set @clo_id = 6;
						set @tip_id = 13;
					end
					else
						if (@nome_aba = 'ENCONTROS') 
						begin
							set @clo_id = 6;
							set @tip_id = 14;
						end
end

if (@nome_aba = 'HISTORICO_INTERVENCOES')
	set @tip_id = -1;


-- se @ati_id significa que possui itens
 -- ********** separa @atv_valores em :  ********************************************
 -- ********************************     txt_atr_id_*:valor *************************
 -- ********************************     cmb_atr_id_*:ati_id   **********************
 -- ********************************     chk_atr_id_*_*(ati_id):checked:valor *******
 -- ********** e os insere/exclui da tabela *****************************************

	set nocount on;
				declare @delimiter varchar(1) = ';';
				declare @idx int  = 1   ;

				declare @atv_TMP varchar(2000)
				declare @ehItem int = 0;

				declare @atv_valor varchar(5000)
				declare @atr_id_aux varchar(50);
				declare @ati_id_aux varchar(5);
				declare @ati_checked varchar(1);

				declare @atv_id_old bigint = 0;
				declare @atv_valor_old varchar(max) = '';

				declare @atv_valores_log varchar(max)='';
				declare @jatem bigint=0;

				declare @atr_id_aux2 varchar(50);
				declare @atv_valor_aux2 varchar(5000)
				while (@idx != 0)
				begin     
					set @idx = charindex(@delimiter, @atv_valores);	
					if @idx!= 0 
					begin
						-- separa o pedaco
						select @atv_TMP = left(@atv_valores,@idx - 1) ; 

						-- quebra os valores
						if ((@nome_aba = 'tblFicha2_HISTORICO_INSPECOES') or (@nome_aba = 'tblFicha4_HISTORICO_INSPECOES'))
						begin
							set @atr_id_aux2 = substring(@atv_TMP, 1, charindex(':', @atv_TMP)-1);
							set @atv_valor_aux2 = substring(@atv_TMP, charindex(':', @atv_TMP)+1, 2000);

							--select @atv_TMP, @atr_id_aux, @atv_valor
							-- alteracao de dados somente para a linha 1
							if (@atr_id_aux2 = 'txt_historico_documento_1')
								  update dbo.tab_inspecoes
									set ins_documento = @atv_valor_aux2
								  where ord_id = @ord_id;
							  else
								if (@atr_id_aux2 = 'txt_historico_data_1')
									  update dbo.tab_inspecoes
										set ins_data = @atv_valor_aux2
									  where ord_id = @ord_id;
								else							
									if (@atr_id_aux2 = 'txt_historico_executantes_1')
										  update dbo.tab_inspecoes
											set ins_executantes = @atv_valor_aux2
										  where ord_id = @ord_id;
									else							
										if (@atr_id_aux2 = 'txt_historico_Pontuacao_Geral_OAE_1')
											  update dbo.tab_inspecoes
												set ins_pontuacaoOAE = @atv_valor_aux2
											  where ord_id = @ord_id;
										else
											if (@atr_id_aux2 = 'txt_historico_documento_2')
												  update dbo.tab_inspecoes
													set ins_documento_2 = @atv_valor_aux2
												  where ord_id = @ord_id;
											else
												if (@atr_id_aux2 = 'txt_historico_data_2')
													  update dbo.tab_inspecoes
														set ins_data_2 = @atv_valor_aux2
													  where ord_id = @ord_id;
												else							
													if (@atr_id_aux2 = 'txt_historico_executantes_2')
														  update dbo.tab_inspecoes
															set ins_executantes_2 = @atv_valor_aux2
														  where ord_id = @ord_id;
													else							
														if (@atr_id_aux2 = 'txt_historico_Pontuacao_Geral_OAE_2')
															  update dbo.tab_inspecoes
																set ins_pontuacaoOAE_2 = @atv_valor_aux2
															  where ord_id = @ord_id;
														else
															if (@atr_id_aux2 = 'txt_historico_documento_3')
																  update dbo.tab_inspecoes
																	set ins_documento_3 = @atv_valor_aux2
																  where ord_id = @ord_id;
															else
																if (@atr_id_aux2 = 'txt_historico_data_3')
																	  update dbo.tab_inspecoes
																		set ins_data_3 = @atv_valor_aux2
																	  where ord_id = @ord_id;
																else							
																	if (@atr_id_aux2 = 'txt_historico_executantes_3')
																		  update dbo.tab_inspecoes
																			set ins_executantes_3 = @atv_valor_aux2
																		  where ord_id = @ord_id;
																	else							
																		if (@atr_id_aux2 = 'txt_historico_Pontuacao_Geral_OAE_3')
																			  update dbo.tab_inspecoes
																				set ins_pontuacaoOAE_3 = @atv_valor_aux2
																			  where ord_id = @ord_id;


						end
						
						if left(@atv_TMP, 11) = 'txt_atr_id_'  
						begin						
							set @atr_id_aux = substring(@atv_TMP, 12, charindex(':', @atv_TMP)-12);
							set @atv_valor = substring(@atv_TMP, charindex(':', @atv_TMP)+1, 2000);
							set @ehItem = 0;
						end
						else
							if left(@atv_TMP, 11) = 'cmb_atr_id_' -- combo
							begin
								set @atr_id_aux = substring(@atv_TMP, 12, charindex(':', @atv_TMP)-12);
								set @atv_valor = substring(@atv_TMP, charindex(':', @atv_TMP)+1, 2000);
								set @ehItem = 0;
							end
							else
								if left(@atv_TMP, 11) = 'chk_atr_id_' -- checkbox
								begin
									set @atr_id_aux = substring(@atv_TMP, 12, charindex('_', @atv_TMP,12)-12);
									set @ati_id_aux = substring(@atv_TMP, charindex('_', @atv_TMP, 12)+1, charindex(':', @atv_TMP, 12)- charindex('_', @atv_TMP, 12) -1) ; 

									set @ati_checked = substring(@atv_TMP, charindex(':', @atv_TMP, len(@ati_id_aux))+1, 1) ;
									set @atv_valor = @ati_checked + ':' + substring(@atv_TMP, charindex(':', @atv_TMP, len(@ati_id_aux))+3, 2000) ;
									set @ehItem = 1;
								end

						if (@atr_id_aux = '157')
						begin
							set @atr_id_aux2 = substring(@atv_TMP, 1, charindex(':', @atv_TMP)-1);
							set @atv_valor_aux2 = substring(@atv_TMP, charindex(':', @atv_TMP)+1, 2000);

								update dbo.tab_inspecoes
								set ins_pontuacaoOAE = @atv_valor_aux2
								where ord_id = @ord_id;
						end

						-- se for Tipo OAE ou Descricao Tipo OAE (cmb_atr_id_98 ou txt_atr_id_105) entao atualiza tabela Objetos
							if (@atr_id_aux = '98') and (@nome_aba = 'DADOS_GERAIS') --  for Tipo OAE 
							begin
									declare @obj_codigoTipoOAE_old nvarchar(200) = (select top 1 obj_codigo from dbo.tab_objetos where  obj_deletado is null and obj_id= @obj_id_TipoObraDeArte );
									declare @tip_id_old int = (select top 1 tip_id from dbo.tab_objetos where  obj_deletado is null and obj_id= @obj_id_TipoObraDeArte );
									declare @tip_codigo_old varchar(10) = (select top 1 tip_codigo from dbo.tab_objeto_tipos where  tip_id= @tip_id_old );
									declare @tip_codigo_new varchar(10) = (select top 1 tip_codigo from dbo.tab_objeto_tipos where  tip_id= convert(int, @atv_valor) );
									declare @obj_codigoTipoOAE_new nvarchar(200) = replace(@obj_codigoTipoOAE_old, @tip_codigo_old,@tip_codigo_new); 

									declare @obj_DescricaoTipoOAE_old nvarchar(200) = (select top 1 obj_codigo from dbo.tab_objetos where  obj_deletado is null and obj_id= @obj_id_TipoObraDeArte );

									declare @tip_nome nvarchar(200) = dbo.fn_objeto_tipo_por_codigo (substring(rtrim(@tip_codigo_new),1,3) , 3, 'tip_nome', null);
									declare @obj_DescricaoTipoOAE_new nvarchar(200) = @tip_nome + ' ' + @obj_codigoTipoOAE_new;

									update dbo.tab_objetos
									set 
										tip_id = convert(int, @atv_valor),
										obj_codigo = @obj_codigoTipoOAE_new, 
										obj_descricao = @obj_DescricaoTipoOAE_new,
										obj_atualizado_por = @usu_id,
										obj_data_atualizacao = getdate() 
									where  obj_id= @obj_id_TipoObraDeArte ;

									-- log
								    set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(30), @obj_id_TipoObraDeArte)  + '];';
									set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[tip_id] de ['  +  convert(varchar(30),@tip_id_old)  + '] para [' + convert(varchar(30),@atv_valor)  + '];';
									set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[obj_codigo] de ['  + @obj_codigoTipoOAE_old  + '] para [' + @obj_codigoTipoOAE_new  + '];';
									set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[obj_descricao] de ['  + @obj_DescricaoTipoOAE_old  + '] para [' + @obj_DescricaoTipoOAE_new  + '];';

									-- expande para os filhos e cria log
									declare @obj_id_tmp bigint=0;
									declare @obj_codigo_tmp nvarchar(200) = '';
									declare @obj_descricao_tmp nvarchar(200) = '';
									declare @tip_id_tmp int=0;


									declare cursor_objs CURSOR FOR	select obj_id, obj_codigo, obj_descricao, tip_id
																		from dbo.tab_objetos 
																		where  
																		obj_deletado is null 
																		and obj_codigo like @obj_codigoTipoOAE_old + '%'
																		and obj_id <> @obj_id_TipoObraDeArte;

									open cursor_objs;
									fetch next from cursor_objs into @obj_id_tmp, @obj_codigo_tmp, @obj_descricao_tmp, @tip_id_tmp;

									while @@FETCH_STATUS = 0
										begin
									        set @tip_nome = (select tip_nome from dbo.tab_objeto_tipos where tip_deletado is null and tip_id= @tip_id_tmp);
										    set @obj_DescricaoTipoOAE_new = @tip_nome + ' ' +  replace(@obj_codigo_tmp,  @obj_codigoTipoOAE_old, @obj_codigoTipoOAE_new);

											update dbo.tab_objetos
											set 
												obj_codigo = replace(obj_codigo,  @obj_codigoTipoOAE_old, @obj_codigoTipoOAE_new),
												obj_descricao = @obj_DescricaoTipoOAE_new,
												obj_atualizado_por = @usu_id,
												obj_data_atualizacao = getdate() 
											where  obj_id= @obj_id_tmp ;
											
											-- log
											set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(30), @obj_id_tmp)  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[obj_codigo] de ['  + @obj_codigo_tmp  + '] para [' + replace(@obj_codigo_tmp,  @obj_codigoTipoOAE_old, @obj_codigoTipoOAE_new)  + '];';
											set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[obj_descricao] de ['  + @obj_descricao_tmp  + '] para [' + replace(@obj_descricao_tmp,  @obj_codigoTipoOAE_old, @obj_codigoTipoOAE_new)  + '];';

											fetch next from cursor_objs into @obj_id_tmp, @obj_codigo_tmp, @obj_descricao_tmp, @tip_id_tmp;
										end;
									close cursor_objs;
									DEALLOCATE cursor_objs;

							end
							else
							begin
								if  (@atr_id_aux = '105')  and (@nome_aba = 'DADOS_GERAIS')-- Descricao Tipo OAE 
								begin
								    declare @obj_descricao_old nvarchar(200) = (select top 1 obj_descricao from dbo.tab_objetos where  obj_deletado is null and obj_id = @obj_id_TipoObraDeArte );

									update dbo.tab_objetos
									set 
										obj_descricao = @atv_valor,
										obj_atualizado_por = @usu_id,
										obj_data_atualizacao = getdate() 
									where  obj_id= @obj_id_TipoObraDeArte ;

									-- log
								    set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(30), @obj_id_TipoObraDeArte)  + '];';
									set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[obj_descricao] de ['  + @obj_descricao_old  + '] para [' + @atv_valor  + '];';

								end
								else
									if  (@atr_id_aux = '107')  and (@nome_aba = 'DADOS_GERAIS')-- nome Rodovia 
									begin
										declare @obj_nomeRodovia_old nvarchar(200) = (select obj_descricao from dbo.tab_objetos where  obj_deletado is null and obj_id = @obj_id_Rodovia );

										update dbo.tab_objetos
										set 
											obj_descricao = @atv_valor,
											obj_atualizado_por = @usu_id,
											obj_data_atualizacao = getdate() 
										where  obj_id= @obj_id_Rodovia ;

										-- log
										set @atv_valores_log = @atv_valores_log + 'Chave[obj_id]=[' + convert(varchar(30), @obj_id_Rodovia)  + '];';
										set @atv_valores_log = @atv_valores_log + 'Coluna [tab_objetos].[obj_descricao] de ['  + @obj_nomeRodovia_old  + '] para [' + @atv_valor  + '];';

  									end
									else -- outros atributos
									if ((@atr_id_aux <> 'txt_historico_documento_1')and (@atr_id_aux <> 'txt_historico_documento_2') and (@atr_id_aux <> 'txt_historico_documento_3'))
									begin

										set @iav_valor = @atv_valor;

										-- verifica que já existe o item na tabela. Se sim, atualiza ou insere
										if @ehItem = 1
											set @jatem_insp = (select count(*) 
															from dbo.tab_inspecao_atributos_valores iav
															where iav_deletado is null
																and iav_ativo = 1
																and ins_id = @ins_id
																and atr_id = convert(int, @atr_id_aux)
																and ati_id = convert(int, @ati_id_aux));
										else
											set @jatem_insp = (select count(*) 
															from dbo.tab_inspecao_atributos_valores iav
															where iav_deletado is null
																and iav_ativo = 1
																and ins_id = @ins_id
																and atr_id = convert(int, @atr_id_aux));

										if (@jatem_insp =0)  -- if (@sos_id <> 11 or @sos_id <> 14) -- 11= executada / 14= encerrada
												begin
												-- insere somente se tiver valor valido
												if ((rtrim(isnull(@ati_id_aux,'')) = '') and ltrim(@iav_valor) <> '')
														or (rtrim(isnull(@ati_id_aux,'')) <> '' and ltrim(@iav_valor) <> '')
														begin
															declare @iav_id int
															set @iav_id = (select isnull(max(iav_id),0) +1 from dbo.tab_inspecao_atributos_valores);
			
															-- insere novo 
															insert into dbo.tab_inspecao_atributos_valores (iav_id, ins_id, obj_id, atr_id, ati_id, iav_valor, uni_id, iav_ativo, iav_data_criacao, iav_criado_por )
															values (@iav_id, @ins_id, @obj_id_TipoObraDeArte, convert(int,@atr_id_aux), convert(int,@ati_id_aux), @iav_valor, @uni_id, 1, getdate(), @usu_id);

															set @iav_valores_log = @iav_valores_log + 'Chave[iav_id]=[' + convert(varchar(30), @iav_id)  + '];';
															set @iav_valores_log = @iav_valores_log + 'Coluna[iav_valor] de [vazio] para [' + @iav_valor  + '];';
													end
												end
										else
											if (@jatem_insp > 0 )--	if (@sos_id <> 11 or @sos_id <> 14) -- 11= executada / 14= encerrada
													begin
														-- pega os valores antigos
														if @ehItem = 1
															select  @iav_id_old = iav_id,
																	@iav_valor_old = iav_valor
															from dbo.tab_inspecao_atributos_valores 
															where iav_deletado is null 
																and iav_ativo = 1
																and atr_id = convert(int, @atr_id_aux)
																and ati_id = convert(int,@ati_id_aux)
																and ins_id = @ins_id;
														else
															select  @iav_id_old = iav_id,
																	@iav_valor_old = iav_valor
															from dbo.tab_inspecao_atributos_valores 
															where iav_deletado is null 
																and iav_ativo = 1
																and atr_id = convert(int, @atr_id_aux)
																and ins_id = @ins_id;

														-- atualiza os dados
															update dbo.tab_inspecao_atributos_valores
															set
																	iav_valor = @iav_valor
																,uni_id = @uni_id
																,iav_data_atualizacao = getdate()
																,iav_atualizado_por = @usu_id
															where iav_deletado is null 
																and iav_ativo = 1
																and iav_id = @iav_id_old;

														if (@iav_valor_old <> @iav_valor)
														begin
															set @iav_valores_log = @iav_valores_log + 'Chave[iav_id]=[' + convert(varchar(30), @iav_id_old)  + '];';
															set @iav_valores_log = @iav_valores_log + 'Coluna[iav_valor] de [' + @iav_valor_old + '] para [' + @iav_valor  + '];';
														end
													end
								  end
							end

					end
					set @atv_valores = right(@atv_valores,len(@atv_valores) - @idx)     ;
					if len(@atv_valores) = 0 break;
				end -- end while


				-- insere log
				declare @tabela varchar(300) = 'tab_inspecao_atributos_valores';
				declare @tra_transacao_id int = 6; -- tra_transacao_id= 6 ==> alteração
				declare @mod_modulo_id_log int = -102; -- atributo valor

				set @atv_valores_log = 'Módulo Ficha Inspeção Cadastral Aba ' + @nome_aba + '.' + @atv_valores_log ; 

				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@atv_valores_log,	@ip	

				set nocount off;


