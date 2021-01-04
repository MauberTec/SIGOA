CREATE procedure [dbo].[STP_UPD_GRUPOS_VARIAVEIS_VALORES]
@ord_id int = -1,
@obj_id_tipoOAE int,
@Ponto1 varchar(10),
@Ponto2 varchar(10),
@Ponto3 varchar(10),
@OutrasInformacoes varchar(1000),
@listaConcatenada varchar(max),
@usu_id int,
@ip nvarchar(30)

as




		if (@ord_id >0)
		begin
		  execute  dbo.STP_UPD_INSPECAO_GRUPOS_VARIAVEIS_VALORES		@ord_id,
																	@Ponto1,
																	@Ponto2,
																	@Ponto3,
																	@OutrasInformacoes,
																	@listaConcatenada,
																	@usu_id,
			   													    @ip
			return 1;
		end;
		else
begin try

			BEGIN TRAN T1

	-- ********* SALVA PONTO1, PONTO2, PONTO3, OutrasInformacoes EM ATRIBUTOS **************************************
			--EXECUTE dbo.STP_UPD_OBJ_ATRIBUTO_VALOR @obj_id_tipoOAE, 132, null, @Ponto1, null, @usu_id, @ip
			--EXECUTE dbo.STP_UPD_OBJ_ATRIBUTO_VALOR @obj_id_tipoOAE, 133, null, @Ponto2, null, @usu_id, @ip
			--EXECUTE dbo.STP_UPD_OBJ_ATRIBUTO_VALOR @obj_id_tipoOAE, 134, null, @Ponto3, null, @usu_id, @ip
			--EXECUTE dbo.STP_UPD_OBJ_ATRIBUTO_VALOR @obj_id_tipoOAE, 170, null, @OutrasInformacoes, null, @usu_id, @ip
			declare @actionDate datetime
			set @actionDate = getdate()

			declare @atv_id int = 0;
			declare @atributo int= 132;
			declare @atributo_valor varchar(100)= '';

			declare @tem int = 1;
			declare @c int = 1;
			while @c <= 4 
			begin
				if @c = 1
				begin
					set @atributo = 132;
					set @atributo_valor = @Ponto1;
				end
				else
				  if @c = 2
					begin
						set @atributo = 133;
						set @atributo_valor = @Ponto2;
					end
					else
					  if @c = 3
						begin
							set @atributo = 134;
							set @atributo_valor = @Ponto3;
						end
						else
						  if @c = 4
							begin
								set @atributo = 170;
								set @atributo_valor = @OutrasInformacoes;
							end


				set @tem = (select count(*) from dbo.tab_objeto_atributos_valores where atv_deletado is null	and obj_id = @obj_id_tipoOAE	and atr_id = @atributo);
				if ( @tem = 0 )
				begin
					set @atv_id = (select isnull(max(atv_id),0) +1 from dbo.tab_objeto_atributos_valores);			
  					insert into dbo.tab_objeto_atributos_valores (atv_id, obj_id, atr_id, ati_id, atv_valor, uni_id, atv_ativo, atv_data_criacao, atv_criado_por )
					values (@atv_id, @obj_id_tipoOAE, @atributo, null, @atributo_valor, null, 1, getdate(), @usu_id);
				end
				else
				begin
					update dbo.tab_objeto_atributos_valores
					set 
						 ati_id = null
						,atv_valor = @atributo_valor
						,uni_id = null
						,atv_data_atualizacao = getdate()
						,atv_atualizado_por = @usu_id
					where atv_deletado is null
						and obj_id = @obj_id_tipoOAE
						and atr_id = @atributo;
				end;

				set @c = @c + 1;
			end;

	-- ************************************************************************************************************
	if rtrim(@listaConcatenada) <> ''
	begin
				declare @grupos_codigos varchar(max) =@listaConcatenada;

				declare @ogv_id int = 0
				declare @obj_id int = 37 
				declare @ogi_id_caracterizacao_situacao int = 0 
				declare @ati_id_condicao_inspecao  int = 0
				declare @ovv_observacoes_gerais varchar(255) = ''
	--	declare @tpu_id  int = 0
				declare @tpu_id  nvarchar(255) = '';
				declare @tpu_unidade  nvarchar(255) = '';
				declare @ovv_tpu_quantidade real = 0

				declare @grupo_codigo varchar(max) = '';
				declare @tr_idxIni int  = 1   ;  
				declare @tr_idxFim int  = 2   ;  
				declare @tr_delimiterIni varchar(20) = '<tr_linha>';
				declare @tr_delimiterFim varchar(20) = '</tr_linha>';

				declare @td_idxIni int  = 1   ;  
				declare @td_idxFim int  = 2   ;  
				declare @td_delimiter_quebra varchar(20) = '<quebra>';
				declare @posicao int = 1;
				declare @td_aux varchar(max) = '';

			-- ================ quebra em linhas =========================================
				while (@tr_idxIni != 0) 
				begin     
					set @tr_idxIni = charindex(@tr_delimiterIni, @grupos_codigos) + len(@tr_delimiterIni);
					set @tr_idxFim = charindex(@tr_delimiterFim, @grupos_codigos, @tr_idxIni+1);
					set @grupo_codigo = substring(@grupos_codigos, @tr_idxIni, @tr_idxFim - @tr_idxIni ) ; 

						-- ================ quebra em colunas =========================================
							set @grupo_codigo = @td_delimiter_quebra + @grupo_codigo + @td_delimiter_quebra;
							set @posicao = 1;
							while (@td_idxIni != 0) -- quebra em linhas
							begin     
								set @td_idxIni = charindex(@td_delimiter_quebra, @grupo_codigo) + len(@td_delimiter_quebra);
								set @td_idxFim = charindex(@td_delimiter_quebra, @grupo_codigo, @td_idxIni+1);
								set @td_aux = substring(@grupo_codigo, @td_idxIni, @td_idxFim - @td_idxIni ) ; 

								if @posicao = 1
									set @obj_id = @td_aux;
								else									
									if @posicao = 2
										set @ogv_id = @td_aux;
									else
										if @posicao = 3
											set @ogi_id_caracterizacao_situacao = @td_aux;
										else
											if @posicao = 4
												set @ati_id_condicao_inspecao = @td_aux;
											else
												if @posicao = 5
													set @ovv_observacoes_gerais = @td_aux;
												else
													if @posicao = 6
														set @tpu_id = @td_aux;
													else
														if @posicao = 7
															set @tpu_unidade = @td_aux;
														else
															if @posicao = 8
																set @ovv_tpu_quantidade = @td_aux;


							   set @grupo_codigo = right(@grupo_codigo, len(@grupo_codigo) - @td_idxFim +1)     
							   if len(@grupo_codigo) <= len(@td_delimiter_quebra)  break;
							   set @posicao = @posicao +1;
							end
							
								 
						 --  	select @ogv_id, @obj_id, @ogi_id_caracterizacao_situacao, @ati_id_condicao_inspecao,@ovv_observacoes_gerais,@tpu_id, @ovv_tpu_quantidade
							-- ======= SAIDA DOS DADOS ==================================
							declare @temQt int = 0;
							declare @oco_id int =0;

							  set @atv_id   = -1;
							  declare @ovv_id int = (select isnull(max(ovv_id),0)+1 from dbo.tab_objeto_grupo_objeto_variaveis_valores);
							  set @tem  = (select count(*) 
													from dbo.tab_objeto_grupo_objeto_variaveis_valores
													where ovv_ativo =1 and ovv_deletado is null
														and ogv_id = @ogv_id
														and obj_id = @obj_id
												  );
							 if (@tem = 0)
							 begin								
								-- salva o valor da Condicao de Inspecao (atributo do objeto GRUPO)
								set @atv_id  = isnull((select atv_id  from dbo.tab_objeto_atributos_valores
																where atv_ativo =1 and atv_deletado is null
																	and atr_id = 129
																	and obj_id = @obj_id),-1);
								if (@atv_id = -1 or @atv_id is null)
								begin
								    set @atv_id = (select isnull(max(atv_id),0)+1 from dbo.tab_objeto_atributos_valores);
								--	select @atv_id, @obj_id, 129, @ati_id_condicao_inspecao
									if convert(int, @ati_id_condicao_inspecao) > 0
										insert into dbo.tab_objeto_atributos_valores (atv_id, obj_id, atr_id, ati_id, atv_ativo,atv_data_criacao, atv_criado_por )
										values (@atv_id, @obj_id, 129, @ati_id_condicao_inspecao, 1, getdate(), @usu_id );	
									--else
									--	insert into dbo.tab_objeto_atributos_valores (atv_id, obj_id, atr_id, ati_id, atv_ativo,atv_data_criacao, atv_criado_por )
									--	values (@atv_id, @obj_id, 129, null, 1, getdate(), @usu_id );	

								end
								else
									begin
									if convert(int, @ati_id_condicao_inspecao) > 0
										update dbo.tab_objeto_atributos_valores 
										set ati_id = @ati_id_condicao_inspecao,
											atv_data_atualizacao = getdate(), 
											atv_atualizado_por = @usu_id
										where
											atv_id = @atv_id;
									end
									
							    -- salva o valor das variaveis
								if convert(int, @ogi_id_caracterizacao_situacao) > 0
							--		insert into dbo.tab_objeto_grupo_objeto_variaveis_valores (ovv_id, ogv_id,  obj_id , ogi_id_caracterizacao_situacao, ovv_observacoes_gerais, tpu_id,  ovv_tpu_quantidade, ovv_ativo, ovv_data_criacao, ovv_criado_por )
									insert into dbo.tab_objeto_grupo_objeto_variaveis_valores (ovv_id, ogv_id,  obj_id , ogi_id_caracterizacao_situacao, ovv_observacoes_gerais, ovv_descricao_servico,  ovv_unidade_servico, ovv_tpu_quantidade, ovv_ativo, ovv_data_criacao, ovv_criado_por )
									values (@ovv_id,@ogv_id, @obj_id, @ogi_id_caracterizacao_situacao, @ovv_observacoes_gerais, @tpu_id, @tpu_unidade, @ovv_tpu_quantidade, 1, getdate(),  @usu_id    );

						   -- 	-- salva o valor das quantidades na tabela [dbo].[tab_objeto_conserva]
								 -- set @temQt = (select count(*) 
									--					from dbo.tab_objeto_conserva
									--					where ovv_ativo =1 and ovv_deletado is null
									--						and ovv_id = @ovv_id
									--				     );	
								 -- if (@temQt = 0)
								 -- begin
								 --   set @oco_id  = (select isnull(max(oco_id),0)+1 from dbo.tab_objeto_conserva);
									--insert into dbo.tab_objeto_conserva (oco_id, ovv_id, oco_quantidade_conserva, ovv_ativo, ovv_data_criacao, ovv_criado_por )
									--values (@oco_id, @ovv_id, @ovv_tpu_quantidade, 1, GETDATE(), @usu_id );
								 -- end
								 -- else
								 -- begin
								 --   update dbo.tab_objeto_conserva 
									--	set oco_quantidade_conserva = @ovv_tpu_quantidade
									--		,ovv_atualizado_por = @usu_id
									--		,ovv_data_atualizacao = GETDATE()
									--where ovv_id = @ovv_id;
								 -- end
							 end
							 else
							 begin
								-- salva o valor da Condicao de Inspecao (atributo do objeto GRUPO)
								set @atv_id  =  (select isnull(atv_id, -1) from dbo.tab_objeto_atributos_valores
																where atv_ativo =1 and atv_deletado is null
																	and atr_id = 129
																	and obj_id = @obj_id);
								if (@atv_id = -1 or @atv_id is null)
								begin
								    set @atv_id =  isnull((select max(atv_id) from dbo.tab_objeto_atributos_valores),0)+1;
									
									if convert(int, @ati_id_condicao_inspecao) > 0
										insert into dbo.tab_objeto_atributos_valores (atv_id, obj_id, atr_id, ati_id, atv_ativo,atv_data_criacao, atv_criado_por )
										values (@atv_id, @obj_id, 129, @ati_id_condicao_inspecao, 1, getdate(), @usu_id );
								
								end
								else
									begin
									 if convert(int, @ati_id_condicao_inspecao) > 0
										update dbo.tab_objeto_atributos_valores 
										set ati_id = @ati_id_condicao_inspecao,
											atv_data_atualizacao = getdate(), 
											atv_atualizado_por = @usu_id
										where
											atv_id = @atv_id;
									end


							   -- salva o valor das variaveis
							   set @ovv_id = (select top 1 ovv_id 
													from dbo.tab_objeto_grupo_objeto_variaveis_valores
													where ovv_ativo =1 and ovv_deletado is null
														and ogv_id = @ogv_id
														and obj_id = @obj_id
												  );
							--	select 'ovv_id=',	@ovv_id			    

								if convert(int, @ogi_id_caracterizacao_situacao) > 0
									update  dbo.tab_objeto_grupo_objeto_variaveis_valores 
									set 
										ogi_id_caracterizacao_situacao = @ogi_id_caracterizacao_situacao, 
										ovv_observacoes_gerais = @ovv_observacoes_gerais, 
										--tpu_id = @tpu_id, 
										ovv_descricao_servico = @tpu_id,
										ovv_unidade_servico = @tpu_unidade,
										ovv_tpu_quantidade = @ovv_tpu_quantidade, 
										ovv_data_atualizacao = getdate(), 
										ovv_atualizado_por = @usu_id
									where ovv_id = @ovv_id; --ovv_id, ogv_id, obj_id,


						   -- 	-- salva o valor das quantidades na tabela [dbo].[tab_objeto_conserva]
								 -- set @temQt = (select count(*) 
									--					from dbo.tab_objeto_conserva
									--					where ovv_id = @ovv_id and ovv_ativo =1 and ovv_deletado is null );	

								 --if (@ovv_id >0)
								 --begin
								 -- if (@temQt = 0)
									--  begin
									--	set @oco_id  = (select isnull(max(oco_id),0)+1 from dbo.tab_objeto_conserva);
									--	select 'insert= ', @oco_id, @ovv_id, @ovv_tpu_quantidade
									--	insert into dbo.tab_objeto_conserva (oco_id, ovv_id, oco_quantidade_conserva, ovv_ativo, ovv_data_criacao, ovv_criado_por )
									--	values (@oco_id, @ovv_id, @ovv_tpu_quantidade, 1, GETDATE(), @usu_id );
									--	select 'ok'
									--  end
								 --  else
									--  begin
									--	--select 'update= ', @oco_id, @ovv_id, @ovv_tpu_quantidade
									--	update dbo.tab_objeto_conserva 
									--		set oco_quantidade_conserva = @ovv_tpu_quantidade
									--			,ovv_atualizado_por = @usu_id
									--			,ovv_data_atualizacao = GETDATE()
									--	where ovv_id = @ovv_id;
									--  end	
								 --end

							end

					-- ================ fim da quebra em colunas =========================================
				   set @grupos_codigos = right(@grupos_codigos, len(@grupos_codigos) - @tr_idxFim +1)     
				   if len(@grupos_codigos) <= len(@tr_delimiterFim)  break;
				end 
			-- ================ fim da quebra em linhas =========================================




			-- ********* INSERÇÃO DE LOG **************************************


			 -- ****************************************************************

		end
				set nocount off;

		COMMIT TRAN T1
		
		return 1;

end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
