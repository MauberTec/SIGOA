CREATE procedure [dbo].[STP_UPD_INSPECAO_ANOMALIAS_VALORES]
	@ord_id int = 0,
	@ins_anom_Responsavel nvarchar(255) = '',
	@ins_anom_data nvarchar(15) = '',
	@ins_anom_quadroA_1 nvarchar(3) = '',
	@ins_anom_quadroA_2 nvarchar(500) = '',
	@listaConcatenada varchar(max) = '',
	@usu_id int = 2,
	@ip nvarchar(30) = '127.0.0.1'
AS

	declare @ins_id int = (select ins_id from [dbo].[tab_inspecoes] where ord_id = @ord_id);
	declare @obj_id_TipoOAE int = (select obj_id from tab_ordens_servico where ord_id = @ord_id);
	declare @ipt_id int = (select [tos_id] from tab_ordens_servico where ord_id = @ord_id);
	declare @obj_codigo_TipoOAE varchar(50) = (select obj_codigo from tab_objetos where obj_id = @obj_id_TipoOAE);
	declare @actionDate datetime = getdate();

	declare @ian_id int;
	declare @ian_numero int;
	declare @atp_id int; declare @atp_codigo nvarchar(10);
	declare @ian_sigla nvarchar(2);

	declare @ale_id int; declare @ale_codigo nvarchar(10);
	declare @ian_quantidade int;
	declare @ian_espacamento int;
	declare @ian_largura int;
	declare @ian_comprimento int;
	declare @ian_abertura_minima int;
	declare @ian_abertura_maxima int;
	declare @aca_id int; declare @aca_codigo nvarchar(10);
	declare @ian_fotografia varchar(255);
	declare @ian_croqui varchar(255);
	declare @ian_desenho nvarchar(255);
	declare @ian_observacoes nvarchar(255);
	declare @leg_id int; declare @leg_codigo nvarchar(10);
	declare @ian_ativo bit = 1
	declare @rpt_id_sugerido int= -1
	declare @rpt_id_adotado int = -1
	declare @ian_quantidade_adotada real= 0
	declare @ian_quantidade_sugerida real =0

begin try
	BEGIN TRAN T1

	-- ************  SALVA OS DADOS SOLTOS ************************************************************************************************

	-- checa se tem inspecao cadastrada
	declare @tem int = 0;
	set @tem = (select COUNT(*) from [dbo].[tab_inspecoes] where ins_id = @ins_id and ins_deletado is null);

	if @tem = 0
	begin
		set @ins_id = (select isnull(max(ins_id),0) +1 from dbo.[tab_inspecoes]);
		insert into [dbo].[tab_inspecoes] (ins_id, ipt_id, obj_id, ord_id, ins_ativo, ins_data_criacao, ins_criado_por, ins_anom_Responsavel, ins_anom_data, ins_anom_quadroA_1, ins_anom_quadroA_2 )
		values (@ins_id, @ipt_id, @obj_id_TipoOAE, @ord_id, 1, @actionDate, @usu_id, @ins_anom_Responsavel, @ins_anom_data, @ins_anom_quadroA_1, @ins_anom_quadroA_2);
	end
	else
	begin
		update [dbo].[tab_inspecoes] set
			ins_data_atualizacao = @actionDate, 
			ins_atualizado_por = @usu_id, 
			ins_anom_Responsavel = @ins_anom_Responsavel, 
			ins_anom_data = @ins_anom_data,
			ins_anom_quadroA_1 = @ins_anom_quadroA_1,
			ins_anom_quadroA_2 = @ins_anom_quadroA_2
		where ins_id = @ins_id;
	end


	-- ************  SALVA O TABELAO ************************************************************************************************
	if rtrim(@listaConcatenada) <> ''
	begin
				declare @grupos_codigos varchar(max) =@listaConcatenada;
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
						set @ian_id = 0;
						set @ian_numero = 0;
						set @atp_id = 0;
						set @ian_sigla = '';
						set @ale_id  = 0;
						set @ian_quantidade  = 0;
						set @ian_espacamento  = 0;
						set @ian_largura  = 0;
						set @ian_comprimento  = 0;
						set @ian_abertura_minima  = 0;
						set @ian_abertura_maxima  = 0;
						set @aca_id = 0;
						set @ian_fotografia  = '';
						set @ian_croqui  = '';
						set @ian_desenho = '';
						set @ian_observacoes  = '';
						set @leg_id = 0;
						set @ian_ativo = 1;
						set @rpt_id_sugerido = -1;
						set @rpt_id_adotado  = -1;
						set @ian_quantidade_sugerida = 0;
						set @ian_quantidade_adotada  = 0;

					set @tr_idxIni = charindex(@tr_delimiterIni, @grupos_codigos) + len(@tr_delimiterIni);
					set @tr_idxFim = charindex(@tr_delimiterFim, @grupos_codigos, @tr_idxIni+1);
					set @grupo_codigo = substring(@grupos_codigos, @tr_idxIni, @tr_idxFim - @tr_idxIni ) ; 
					set @grupo_codigo = @td_delimiter_quebra + @grupo_codigo; 

						-- ================ quebra em colunas =========================================
							set @posicao = 1;

							while (@td_idxIni != 0) -- quebra em linhas
							begin     
								set @td_idxIni = charindex(@td_delimiter_quebra, @grupo_codigo) + len(@td_delimiter_quebra);
								set @td_idxFim = charindex(@td_delimiter_quebra, @grupo_codigo, @td_idxIni+1);
								set @td_aux = substring(@grupo_codigo, @td_idxIni, @td_idxFim - @td_idxIni ) ; 

								--select @posicao, @td_aux, @grupo_codigo

								
								if @posicao = 1
									set @ian_id = CONVERT(int, @td_aux);
								else									
									if @posicao = 2
										set @ian_numero = CONVERT(int, @td_aux);
									else
										if @posicao = 3
											set @leg_codigo =  rtrim(ltrim(@td_aux));
										else
											if @posicao = 4
												set @atp_codigo =  rtrim(ltrim(@td_aux));
											else
												if @posicao = 5
													set @ale_codigo = rtrim(ltrim(@td_aux));
												else
													if @posicao = 6
														set @ian_quantidade = CONVERT(int, @td_aux);
													else
														if @posicao = 7
															set @ian_espacamento = CONVERT(int, @td_aux);
														else
															if @posicao = 8
																set @ian_largura = CONVERT(int, @td_aux);
															else
																if @posicao = 9
																	set @ian_comprimento = CONVERT(int, @td_aux);
																else
																	if @posicao = 10
																		set @ian_abertura_minima = CONVERT(int, @td_aux);
																	else
																		if @posicao = 11
																			set @ian_abertura_maxima = CONVERT(int, @td_aux);
																		else
																			if @posicao = 12
																				set @aca_codigo = rtrim(ltrim(@td_aux));
																			else
																				if @posicao = 13
																					set @ian_fotografia = rtrim(ltrim(@td_aux));
																				else
																					if @posicao = 14
																						set @ian_croqui = rtrim(ltrim(@td_aux));
																					else
																						if @posicao = 15
																							set @ian_desenho = rtrim(ltrim(@td_aux));
																						else
																							if @posicao = 16
																								set @ian_observacoes = @td_aux;                       
																							else
																								if @posicao = 17
																									set @rpt_id_sugerido = CONVERT(int, @td_aux);
																								else
																									if @posicao = 18
																										set @rpt_id_adotado =  CONVERT(int, @td_aux);   
																									else
																										if @posicao = 19
																										begin
																										  if (ISNUMERIC(@td_aux) = 1)
																											set @ian_quantidade_sugerida = convert(real,@td_aux) ; 
																										end
																										else
																											if @posicao = 20
																											begin
																											   if (ISNUMERIC(@td_aux) = 1)
																												  set @ian_quantidade_adotada = convert(real,@td_aux) ;    
																											end


                           
								if (@leg_codigo = '-1')
									set @leg_id = -1;
								else 								
								    set @leg_id = isnull((select top 1 leg_id from dbo.tab_anomalia_legendas where leg_codigo = @leg_codigo), -1);

								if (@atp_codigo = '-1')
									set @atp_id = -1;
								else 
									set @atp_id =  isnull((select top 1 atp_id from dbo.tab_anomalia_tipos where atp_codigo = @atp_codigo), -1);

								if (@ale_codigo = '-1')
									set @ale_id = -1;
								else 
									set @ale_id =  isnull((select top 1 ale_id from dbo.tab_anomalia_alertas where ale_codigo = @ale_codigo), -1);

								if (@aca_codigo = '-1')
									set @aca_id = -1;
								else 								
									set @aca_id = isnull((select top 1 aca_id from dbo.tab_anomalia_causas where aca_codigo = @aca_codigo),-1);
								
							   set @grupo_codigo = right(@grupo_codigo, len(@grupo_codigo) - @td_idxFim +1)     
							   if len(@grupo_codigo) <= len(@td_delimiter_quebra)  break;
							   set @posicao = @posicao +1;
							end
							
					-- ======= SAIDA DOS DADOS ==================================
						-- checa se tem
						set @tem = (select COUNT(*) from dbo.tab_inspecoes_anomalias where ian_id = @ian_id );
						if @tem = 0
						begin					
							insert into dbo.tab_inspecoes_anomalias(ian_id, obj_id, ins_id, ian_numero, atp_id, ale_id, ian_quantidade, ian_espacamento, ian_largura, ian_comprimento, ian_abertura_minima, ian_abertura_maxima, aca_id, ian_fotografia, ian_croqui, ian_desenho, ian_observacoes, leg_id, ian_sigla, ian_ativo, ian_data_criacao, ian_criado_por, rpt_id_sugerido, rpt_id_adotado, ian_quantidade_sugerida, ian_quantidade_adotada )
							values (@ian_id, @obj_id_TipoOAE, @ins_id, @ian_numero, @atp_id, @ale_id, @ian_quantidade, @ian_espacamento, @ian_largura, @ian_comprimento, @ian_abertura_minima, @ian_abertura_maxima, @aca_id, @ian_fotografia, @ian_croqui, @ian_desenho, @ian_observacoes, @leg_id, @leg_id, 1,  @actionDate, @usu_id, @rpt_id_sugerido, @rpt_id_adotado, @ian_quantidade_sugerida, @ian_quantidade_adotada  );
						end
						else
						begin
								update dbo.tab_inspecoes_anomalias 
								set
								   ian_numero = @ian_numero
								   ,atp_id = @atp_id
								   ,ian_sigla = @leg_id -- @ian_sigla
								   ,ale_id = @ale_id
								   ,ian_quantidade = @ian_quantidade
								   ,ian_espacamento = @ian_espacamento
								   ,ian_largura = @ian_largura
								   ,ian_comprimento = @ian_comprimento
								   ,ian_abertura_minima = @ian_abertura_minima
								   ,ian_abertura_maxima = @ian_abertura_maxima
								   ,aca_id = @aca_id
								   ,ian_fotografia = @ian_fotografia
								   ,ian_croqui = @ian_croqui
								   ,ian_desenho = @ian_desenho
								   ,ian_observacoes = @ian_observacoes
								   ,leg_id = @leg_id 
								   ,ian_ativo = @ian_ativo
								   ,ian_data_atualizacao = @actionDate
								   ,ian_atualizado_por = @usu_id
								   ,rpt_id_sugerido = @rpt_id_sugerido
								   ,rpt_id_adotado = @rpt_id_adotado
								   ,ian_quantidade_adotada = @ian_quantidade_adotada
								   ,ian_quantidade_sugerida = @ian_quantidade_sugerida

							 where ian_id = @ian_id;
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

