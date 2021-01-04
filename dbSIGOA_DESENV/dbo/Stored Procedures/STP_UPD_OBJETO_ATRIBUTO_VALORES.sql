CREATE procedure [dbo].[STP_UPD_OBJETO_ATRIBUTO_VALORES] 
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

-- pode ser que o obj_id nao seja o da super/meso/infra/encontros, entao tem procurar o correto
declare @obj_id_aba int =0;		
declare @obj_codigo_rodovia varchar(10) = '';
if (@clo_id_aux > 1)
  begin
	set @obj_codigo_rodovia = (select left(obj_codigo, charindex('-',obj_codigo)-1) from dbo.tab_objetos where obj_id = @obj_id);
	set @obj_id_Rodovia = (select obj_id from dbo.tab_objetos where obj_codigo = @obj_codigo_rodovia);
  end
else
  begin
	set @obj_codigo_rodovia = (select obj_codigo from dbo.tab_objetos where obj_id = @obj_id);
	set @obj_id_Rodovia = @obj_id;
  end

declare @codigo_TipoOAE varchar(50) ='';

if (@nome_aba is not null)
begin

	if (CHARINDEX('tblFicha2', @nome_aba) >= 1) or (CHARINDEX('tblFicha3', @nome_aba) >= 1) or (CHARINDEX('tblFicha4', @nome_aba) >= 1)
	begin
		set @obj_id_aba = @obj_id;
		set @obj_id_TipoObraDeArte = @obj_id;
	end
	else
	if (@nome_aba = 'DADOS_GERAIS') or (@nome_aba = 'ATRIBUTOS_FUNCIONAIS') or (@nome_aba = 'ATRIBUTOS_FIXOS') 
	begin
		if (@clo_id_aux > 3) -- entao tem que subir a hierarquia ate o tipo OAE
		begin
			declare @id int = @obj_id;
			declare @id2 int = -1;
			declare @idpai int = 100;

			while (@idpai >= 0)
			begin
					select  @clo_id_aux = clo_id, 
							@tip_id = tip_id, 
							@idpai = isnull(obj_pai,-1),
							@id2 = obj_id
					from dbo.tab_objetos 
					where obj_id = @id;

				  if (@clo_id_aux = 3)  -- significa que o objeto é TIPO OAE
					 begin
						   set @obj_id_TipoObraDeArte = @id2;
						   set @clo_id = 3;
						   set @idpai = -1;
					 end
				set @id = @idpai;
			end;
			set @obj_id_aba = @obj_id_TipoObraDeArte;

			-- atualiza o obj_id da 
		end
		else
		begin
			 if (@clo_id_aux = 2) -- se classe = 2 entao obj_id = OAE quilometragem, entao checa se existe o TIPO DE OAE
			 begin
				set  @codigo_TipoOAE = (select top 1 @codigoOAE + '-' + tip_codigo from dbo.tab_objeto_tipos where tip_id = @selidTipoOAE);

				set @obj_id_TipoObraDeArte = (select obj_id from dbo.tab_objetos where obj_codigo = @codigo_TipoOAE);
				if (@obj_id_TipoObraDeArte is null)
					EXECUTE @obj_id_TipoObraDeArte = dbo.STP_INS_OBJETO @codigo_TipoOAE, '', null, null, @usu_id, @ip



				set @obj_id = @obj_id_TipoObraDeArte;
				set @tip_id = @obj_id_TipoObraDeArte;
				set @clo_id = 3;
			 end

			 if (@clo_id_aux = 3)  -- significa que o objeto é TIPO OAE
			 begin
				-- obj_id = id do tipo da obra de arte
				set @clo_id = 3;
				select @obj_id_aba= obj_id, 
					   @tip_id = tip_id 
				  from dbo.tab_objetos 
				   where clo_id = 3  and obj_id = @obj_id;

				   set @obj_id_TipoObraDeArte = @obj_id;
		end

		     set @obj_id_aba = @obj_id_TipoObraDeArte;
	     end
    end
	else
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

		set @obj_id_aba = (select top 1 obj_id  from dbo.tab_objetos  where clo_id = @clo_id  and tip_id = @tip_id and obj_codigo like (@codigoOAE + '%'));
	--	set @obj_id_aba = (select top 1 obj_id  from dbo.tab_objetos  where clo_id = @clo_id  and tip_id = @tip_id and obj_codigo like (@obj_codigo_rodovia + '%'));
	--	set @obj_id_TipoObraDeArte = (select top 1 obj_id  from dbo.tab_objetos  where clo_id = 3  and obj_codigo like (@obj_codigo_rodovia + '%'));
	
		if @clo_id_aux = 3
			set @obj_id_TipoObraDeArte = @obj_id;
		else
			set @obj_id_TipoObraDeArte = (select top 1 obj_id  from dbo.tab_objetos  where tip_id = @selidTipoOAE and  obj_codigo like (@obj_codigo_rodovia + '%'));
    end
end


if (@nome_aba = 'HISTORICO_INTERVENCOES')
begin
	set @obj_id_TipoObraDeArte = @obj_id;
	set  @obj_id_aba = @obj_id;
	set @clo_id = @clo_id_aux;
	set @tip_id = -1;
end

-- se @ati_id null significa que é valor unico
if (@ati_id is null)
begin
		exec dbo.STP_UPD_OBJ_ATRIBUTO_VALOR    @obj_id_aba,
													@atr_id,
													@ati_id,
													@atv_valores,
													@uni_id,
													@usu_id,
													@ip
end
else
begin
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
								if (@atr_id_aux <> 'txt_historico_documento_1')
								begin
											-- verifica que já existe o item na tabela. Se sim, atualiza ou insere
											select @jatem = count(*) 
											from dbo.tab_objeto_atributos_valores
											where atv_deletado is null
												and obj_id = convert(bigint, @obj_id_aba)
												and atr_id = convert(int, @atr_id_aux)
												and isnull(ati_id,'') = case when @ehItem = 1 then @ati_id_aux else isnull(ati_id,'') end;

											if (@jatem =0) --and (@ati_checked = 1)
												begin
													-- insere somente se tiver valor valido
													if ((@ati_id_aux is null and ltrim(@atv_valor) <> '')
														 or (@ati_id_aux is not null and ltrim(@atv_valor) <> ''))
														 begin
																declare @atv_id int
																set @atv_id = (select isnull(max(atv_id),0) +1 from dbo.tab_objeto_atributos_valores);
			
																-- insere novo 
																--select @atv_id, @obj_id_aba, @atr_id_aux, @ati_id_aux, @atv_valor, @uni_id, 1, getdate(), @usu_id
																insert into dbo.tab_objeto_atributos_valores (atv_id, obj_id, atr_id, ati_id, atv_valor, uni_id, atv_ativo, atv_data_criacao, atv_criado_por )
																values (@atv_id, @obj_id_aba, @atr_id_aux, @ati_id_aux, @atv_valor, @uni_id, 1, getdate(), @usu_id);

																set @atv_valores_log = @atv_valores_log + 'Chave[atv_id]=[' + convert(varchar(8), @atv_id)  + '];';
																set @atv_valores_log = @atv_valores_log + 'Coluna[atv_valor] de [vazio] para [' + @atv_valor  + '];';
														end
												end
											else
												if (@jatem > 0 ) --and (@ati_checked = 0)
													begin
														-- pega os valores antigos
														select  @atv_id_old = atv_id,
																@atv_valor_old = atv_valor
														from dbo.tab_objeto_atributos_valores
														where atv_deletado is null
															and obj_id = convert(bigint, @obj_id_aba)
															and atr_id = convert(int, @atr_id_aux);

														
														-- atualiza os dados
														update dbo.tab_objeto_atributos_valores
														set 
															 ati_id = @ati_id_aux
															,atv_valor = @atv_valor
															,uni_id = @uni_id
															,atv_data_atualizacao = getdate()
															,atv_atualizado_por = @usu_id
														where atv_deletado is null
															and obj_id = convert(bigint, @obj_id_aba)
															and atr_id = convert(int, @atr_id_aux)
															and isnull(ati_id,'') = case when @ehItem = 1 then @ati_id_aux else isnull(ati_id,'') end;

														if (@atv_valor_old <> @atv_valor)
														begin
															set @atv_valores_log = @atv_valores_log + 'Chave[atv_id]=[' + convert(varchar(8000), @atv_id_old)  + '];';
															set @atv_valores_log = @atv_valores_log + 'Coluna[atv_valor] de [' + @atv_valor_old + '] para [' + @atv_valor  + '];';
														end
													end

									end


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

					if (@jatem_insp =0) 
						begin
						  if (@sos_id <> 11 or @sos_id <> 14) -- 11= executada / 14= encerrada
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
						end
					else
						if (@jatem_insp > 0 ) 
							begin
								if (@sos_id <> 11 or @sos_id <> 14) -- 11= executada / 14= encerrada
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

						-- =====================================================================================
						end
				    end; --if


					set @atv_valores = right(@atv_valores,len(@atv_valores) - @idx)     
					if len(@atv_valores) = 0 break;
				end -- end while

				-- atualiza o objeto na ordem de servico para o valor do Tipo OAE
				if (@ord_id is not null)
				begin
					   declare @ord_id_aux2 bigint = (select obj_id from dbo.tab_ordens_servico where ord_id = @ord_id);
					--select @obj_id_TipoObraDeArte
						if (@ord_id_aux2 <> @obj_id_TipoObraDeArte)
						begin
							update dbo.tab_ordens_servico
								set obj_id = @obj_id_TipoObraDeArte
							where ord_id = @ord_id;


							-- atualiza objeto da tabela inspecoes
							update dbo.tab_inspecoes
								set obj_id = @obj_id_TipoObraDeArte
							where ord_id = @ord_id;
						end;
				end

				-- insere log
				declare @tabela varchar(300) = 'tab_objeto_atributos_valores';
				declare @tra_transacao_id int = 6; -- tra_transacao_id= 6 ==> alteração
				declare @mod_modulo_id_log int = -102; -- atributo valor

				set @atv_valores_log = 'Módulo Ficha Inspeção Cadastral Aba ' + @nome_aba + '.' + @atv_valores_log ; 

				exec dbo.STP_INS_LOGSISTEMA @tra_transacao_id, @usu_id, @mod_modulo_id_log,	@atv_valores_log,	@ip	

				set nocount off;
end
