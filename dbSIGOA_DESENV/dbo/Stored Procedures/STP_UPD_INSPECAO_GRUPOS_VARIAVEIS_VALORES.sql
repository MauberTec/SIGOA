CREATE procedure [dbo].[STP_UPD_INSPECAO_GRUPOS_VARIAVEIS_VALORES]
@ord_id int,
@Ponto1 varchar(10),
@Ponto2 varchar(10),
@Ponto3 varchar(10),
@OutrasInformacoes varchar(1000),
@listaConcatenada varchar(max),
@usu_id int,
@ip nvarchar(30)

as

begin try
		BEGIN TRAN T1

declare @ins_id int = (select ins_id from tab_inspecoes where ord_id = @ord_id);
declare @obj_id_tipoOAE int = (select obj_id from tab_inspecoes where ins_id = @ins_id);

	-- ********* SALVA PONTO1, PONTO2, PONTO3, OutrasInformacoes EM ATRIBUTOS **************************************
			--EXECUTE dbo.STP_UPD_OBJ_ATRIBUTO_VALOR @obj_id_tipoOAE, 132, null, @Ponto1, null, @usu_id, @ip
			--EXECUTE dbo.STP_UPD_OBJ_ATRIBUTO_VALOR @obj_id_tipoOAE, 133, null, @Ponto2, null, @usu_id, @ip
			--EXECUTE dbo.STP_UPD_OBJ_ATRIBUTO_VALOR @obj_id_tipoOAE, 134, null, @Ponto3, null, @usu_id, @ip
			--EXECUTE dbo.STP_UPD_OBJ_ATRIBUTO_VALOR @obj_id_tipoOAE, 170, null, @OutrasInformacoes, null, @usu_id, @ip
			declare @actionDate datetime
			set @actionDate = getdate()

			declare @iav_id int = 0;
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


				set @tem = (select count(*) from dbo.tab_inspecao_atributos_valores where iav_deletado is null	and obj_id = @obj_id_tipoOAE and ins_id=@ins_id	and atr_id = @atributo);
				if ( @tem = 0 )
				begin
					set @iav_id = (select isnull(max(iav_id),0) +1 from dbo.tab_inspecao_atributos_valores);			
  					insert into dbo.tab_inspecao_atributos_valores (iav_id, obj_id, ins_id, atr_id, ati_id, iav_valor, uni_id, iav_ativo, iav_data_criacao, iav_criado_por )
					values (@iav_id, @obj_id_tipoOAE, @ins_id, @atributo, null, @atributo_valor, null, 1, getdate(), @usu_id);
				end
				else
				begin
					update dbo.tab_inspecao_atributos_valores
					set 
						 ati_id = null
						,iav_valor = @atributo_valor
						,uni_id = null
						,iav_data_atualizacao = getdate()
						,iav_atualizado_por = @usu_id
					where iav_deletado is null
						and ins_id = @ins_id
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
				declare @ivv_observacoes_gerais varchar(255) = ''
	--	declare @tpu_id  int = 0
				declare @tpu_id  nvarchar(255) = '';
				declare @tpu_unidade  nvarchar(255) = '';
				declare @ivv_tpu_quantidade real = 0

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
													set @ivv_observacoes_gerais = @td_aux;
												else
													if @posicao = 6
														set @tpu_id = @td_aux;
													else
														if @posicao = 7
															set @tpu_unidade = @td_aux;
														else
															if @posicao = 8
																set @ivv_tpu_quantidade = @td_aux;


							   set @grupo_codigo = right(@grupo_codigo, len(@grupo_codigo) - @td_idxFim +1)     
							   if len(@grupo_codigo) <= len(@td_delimiter_quebra)  break;
							   set @posicao = @posicao +1;
							end
							
								 
						 --  	select @ogv_id, @obj_id, @ogi_id_caracterizacao_situacao, @ati_id_condicao_inspecao,@ivv_observacoes_gerais,@tpu_id, @ivv_tpu_quantidade
							-- ======= SAIDA DOS DADOS ==================================
							declare @temQt int = 0;
							declare @oco_id int =0;

							  set @iav_id   = -1;
							  declare @ivv_id int = (select isnull(max(ivv_id),0)+1 from dbo.tab_inspecoes_grupo_objeto_variaveis_valores);
							  set @tem  = (select count(*) 
													from dbo.tab_inspecoes_grupo_objeto_variaveis_valores
													where ivv_ativo =1 and ivv_deletado is null
													    and ins_id = @ins_id
														and ogv_id = @ogv_id
														and obj_id = @obj_id
												  );
							 if (@tem = 0)
							 begin								
								-- salva o valor da Condicao de Inspecao (atributo do objeto GRUPO)
								set @iav_id  = isnull((select iav_id  from dbo.tab_inspecao_atributos_valores
																where iav_ativo =1 and iav_deletado is null
																	and atr_id = 129
																	and ins_id = @ins_id
																	and obj_id = @obj_id),-1);
								if (@iav_id = -1 or @iav_id is null)
								begin
								    set @iav_id = (select isnull(max(iav_id),0)+1 from dbo.tab_inspecao_atributos_valores);
								--	select @atv_id, @obj_id, 129, @ati_id_condicao_inspecao
									if convert(int, @ati_id_condicao_inspecao) > 0
										insert into dbo.tab_inspecao_atributos_valores (iav_id, obj_id, ins_id, atr_id, ati_id, iav_valor, iav_ativo,iav_data_criacao, iav_criado_por )
										values (@iav_id, @obj_id, @ins_id, 129, @ati_id_condicao_inspecao, @ati_id_condicao_inspecao,1, getdate(), @usu_id );	
									--else
									--	insert into dbo.tab_inspecao_atributos_valores (atv_id, obj_id, atr_id, ati_id, atv_ativo,atv_data_criacao, atv_criado_por )
									--	values (@atv_id, @obj_id, 129, null, 1, getdate(), @usu_id );	

								end
								else
									begin
									  if convert(int, @ati_id_condicao_inspecao) > 0
										update dbo.tab_inspecao_atributos_valores 
										set ati_id = @ati_id_condicao_inspecao,
											iav_data_atualizacao = getdate(), 
											iav_atualizado_por = @usu_id
										where
											iav_id = @iav_id;
									end
									
							    -- salva o valor das variaveis
								if convert(int, @ogi_id_caracterizacao_situacao) > 0
							--		insert into dbo.tab_inspecoes_grupo_objeto_variaveis_valores (ivv_id, ogv_id,  obj_id , ogi_id_caracterizacao_situacao, ivv_observacoes_gerais, tpu_id,  ivv_tpu_quantidade, ivv_ativo, ivv_data_criacao, ivv_criado_por )
									insert into dbo.tab_inspecoes_grupo_objeto_variaveis_valores (ivv_id, ogv_id, ins_id,  obj_id , ogi_id_caracterizacao_situacao, ivv_observacoes_gerais, ivv_descricao_servico,  ivv_unidade_servico, ivv_tpu_quantidade, ivv_ativo, ivv_data_criacao, ivv_criado_por )
									values (@ivv_id,@ogv_id, @ins_id, @obj_id, @ogi_id_caracterizacao_situacao, @ivv_observacoes_gerais, @tpu_id, @tpu_unidade, @ivv_tpu_quantidade, 1, getdate(),  @usu_id    );

						   -- 	-- salva o valor das quantidades na tabela [dbo].[tab_objeto_conserva]
								 -- set @temQt = (select count(*) 
									--					from dbo.tab_objeto_conserva
									--					where ivv_ativo =1 and ivv_deletado is null
									--						and ivv_id = @ivv_id
									--				     );	
								 -- if (@temQt = 0)
								 -- begin
								 --   set @oco_id  = (select isnull(max(oco_id),0)+1 from dbo.tab_objeto_conserva);
									--insert into dbo.tab_objeto_conserva (oco_id, ivv_id, oco_quantidade_conserva, ivv_ativo, ivv_data_criacao, ivv_criado_por )
									--values (@oco_id, @ivv_id, @ivv_tpu_quantidade, 1, GETDATE(), @usu_id );
								 -- end
								 -- else
								 -- begin
								 --   update dbo.tab_objeto_conserva 
									--	set oco_quantidade_conserva = @ivv_tpu_quantidade
									--		,ivv_atualizado_por = @usu_id
									--		,ivv_data_atualizacao = GETDATE()
									--where ivv_id = @ivv_id;
								 -- end
							 end
							 else
							 begin
								-- salva o valor da Condicao de Inspecao (atributo do objeto GRUPO)
								set @iav_id  =  (select isnull(iav_id, -1) from dbo.tab_inspecao_atributos_valores
																where iav_ativo =1 and iav_deletado is null
																	and atr_id = 129
																	and ins_id = @ins_id
																	and obj_id = @obj_id);
								if (@iav_id = -1 or @iav_id is null)
								begin
								    set @iav_id =  isnull((select max(iav_id) from dbo.tab_inspecao_atributos_valores),0)+1;
									
									if convert(int, @ati_id_condicao_inspecao) > 0
										--insert into dbo.tab_inspecao_atributos_valores (iav_id, obj_id, ins_id, atr_id, ati_id, iav_ativo,iav_data_criacao, iav_criado_por )
										--values (@iav_id, @obj_id, @ins_id, 129, @ati_id_condicao_inspecao, 1, getdate(), @usu_id );
										insert into dbo.tab_inspecao_atributos_valores (iav_id, obj_id, ins_id, atr_id, ati_id, iav_valor, iav_ativo,iav_data_criacao, iav_criado_por )
										values (@iav_id, @obj_id, @ins_id, 129, @ati_id_condicao_inspecao,@ati_id_condicao_inspecao,  1, getdate(), @usu_id );

								end
								else
									begin
									 if convert(int, @ati_id_condicao_inspecao) > 0
										update dbo.tab_inspecao_atributos_valores 
										set ati_id = @ati_id_condicao_inspecao,
											iav_data_atualizacao = getdate(), 
											iav_atualizado_por = @usu_id
										where
											iav_id = @iav_id;
									end


							   -- salva o valor das variaveis
							   set @ivv_id = (select top 1 ivv_id 
													from dbo.tab_inspecoes_grupo_objeto_variaveis_valores
													where ivv_ativo =1 and ivv_deletado is null
														and ogv_id = @ogv_id
														and ins_id = @ins_id
														and obj_id = @obj_id
												  );
							--	select 'ivv_id=',	@ivv_id			    

								if convert(int, @ogi_id_caracterizacao_situacao) > 0
									update  dbo.tab_inspecoes_grupo_objeto_variaveis_valores 
									set 
										ogi_id_caracterizacao_situacao = @ogi_id_caracterizacao_situacao, 
										ivv_observacoes_gerais = @ivv_observacoes_gerais, 
										--tpu_id = @tpu_id, 
										ivv_descricao_servico = @tpu_id,
										ivv_unidade_servico = @tpu_unidade,
										ivv_tpu_quantidade = @ivv_tpu_quantidade, 
										ivv_data_atualizacao = getdate(), 
										ivv_atualizado_por = @usu_id
									where ivv_id = @ivv_id; --ivv_id, ogv_id, obj_id,


						   -- 	-- salva o valor das quantidades na tabela [dbo].[tab_objeto_conserva]
								 -- set @temQt = (select count(*) 
									--					from dbo.tab_objeto_conserva
									--					where ivv_id = @ivv_id and ivv_ativo =1 and ivv_deletado is null );	

								 --if (@ivv_id >0)
								 --begin
								 -- if (@temQt = 0)
									--  begin
									--	set @oco_id  = (select isnull(max(oco_id),0)+1 from dbo.tab_objeto_conserva);
									--	select 'insert= ', @oco_id, @ivv_id, @ivv_tpu_quantidade
									--	insert into dbo.tab_objeto_conserva (oco_id, ivv_id, oco_quantidade_conserva, ivv_ativo, ivv_data_criacao, ivv_criado_por )
									--	values (@oco_id, @ivv_id, @ivv_tpu_quantidade, 1, GETDATE(), @usu_id );
									--	select 'ok'
									--  end
								 --  else
									--  begin
									--	--select 'update= ', @oco_id, @ivv_id, @ivv_tpu_quantidade
									--	update dbo.tab_objeto_conserva 
									--		set oco_quantidade_conserva = @ivv_tpu_quantidade
									--			,ivv_atualizado_por = @usu_id
									--			,ivv_data_atualizacao = GETDATE()
									--	where ivv_id = @ivv_id;
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
