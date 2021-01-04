CREATE procedure [dbo].[STP_UPD_ATIVARDESATIVAR_MODULOPERFIL] 
@per_id int,
@mod_id int,
@mod_pai_id int,
@operacao nvarchar(1),
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
-- ********************* A ROTINA DE LOG DO SISTEMA ESTA EM FORMA DE TRIGGER ********************

				declare @novoEstado bit = 0;
				declare @nFilhos int = 0;
				
				if (@operacao = 'R' )				
				begin	
					update dbo.tab_modulos_perfis
					set mfl_leitura = ~ mfl_leitura,
						mfl_data_atualizacao = GETDATE(),
						mfl_atualizado_por = @usu_id,
						mfl_ip = @ip
					 where per_id = @per_id
					 and (mod_id = @mod_id or (mod_id = @mod_pai_id and mfl_leitura=0) );

					 -- se for modulo pai, expande para os filhos
					 if (@mod_pai_id < 0) 
					 begin
							set @novoEstado = (select mfl_leitura 
												from dbo.tab_modulos_perfis  
												where per_id = @per_id
													 and mod_id = @mod_id );			
													 	
							update dbo.tab_modulos_perfis
							set mfl_leitura = @novoEstado,
								mfl_data_atualizacao = GETDATE(),
								mfl_atualizado_por = @usu_id,
								mfl_ip = @ip
							 where per_id = @per_id
							 and mod_id in (select distinct mod_id 
													from  dbo.tab_modulos
													 where mod_pai_id = @mod_id);						
					 end
					 else 
					 begin
					 -- desativa o  modulo pai se não houver mais filhos ativos
						set @nFilhos = (select count(mfl_leitura) 
												from dbo.tab_modulos_perfis mfl
												inner join dbo.tab_modulos md on md.mod_id= mfl.mod_id and mod_deletado is null and mfl.mod_id >0
												where per_id = @per_id
													 and mod_pai_id = @mod_pai_id
													 and mfl_leitura = 1
										);
							if @nFilhos =0
								update dbo.tab_modulos_perfis
								set mfl_leitura = 0,
									mfl_data_atualizacao = GETDATE(),
									mfl_atualizado_por = @usu_id,
									mfl_ip = @ip
								 where per_id = @per_id
								 and mod_id = @mod_pai_id;
					 end
				end
			    else
					if (@operacao ='W' )
					begin
						update dbo.tab_modulos_perfis
						set mfl_escrita = ~mfl_escrita,
							mfl_data_atualizacao = GETDATE(),
							mfl_atualizado_por = @usu_id,
							mfl_ip = @ip
						 where per_id = @per_id
								 and (mod_id = @mod_id or (mod_id = @mod_pai_id and mfl_escrita=0) );
					 
						 if (@mod_pai_id < 0)
						 begin
								set @novoEstado = (select mfl_escrita 
												from dbo.tab_modulos_perfis  
												where per_id = @per_id
													 and mod_id = @mod_id );							 
								update dbo.tab_modulos_perfis
								set mfl_escrita = @novoEstado,
									mfl_data_atualizacao = GETDATE(),
									mfl_atualizado_por = @usu_id,
									mfl_ip = @ip
								 where per_id = @per_id
								 and mod_id in (select distinct mod_id 
														from  dbo.tab_modulos
														 where mod_pai_id = @mod_id);						
						 end	
						 else 
							 begin
							 -- desativa o  modulo pai se não houver mais filhos ativos
								set @nFilhos = (select count(mfl_escrita) 
														from dbo.tab_modulos_perfis mfl
														inner join dbo.tab_modulos md on md.mod_id= mfl.mod_id and mod_deletado is null and mfl.mod_id >0
														where per_id = @per_id
															 and mod_pai_id = @mod_pai_id
															 and mfl_escrita = 1
												);
									if @nFilhos =0
										update dbo.tab_modulos_perfis
										set mfl_escrita = 0,
											mfl_data_atualizacao = GETDATE(),
											mfl_atualizado_por = @usu_id,
											mfl_ip = @ip
										 where per_id = @per_id
										 and mod_id = @mod_pai_id;
							 end
					end						 
			    else
					if (@operacao ='X' )
					begin
						update dbo.tab_modulos_perfis
						set mfl_excluir = ~ mfl_excluir,
							mfl_data_atualizacao = GETDATE(),
							mfl_atualizado_por = @usu_id,
							mfl_ip = @ip
						 where per_id = @per_id
						 and (mod_id = @mod_id or (mod_id = @mod_pai_id and mfl_excluir=0) );

						 if (@mod_pai_id  < 0)
						 begin
								set @novoEstado = (select mfl_excluir 
												from dbo.tab_modulos_perfis  
												where per_id = @per_id
													 and mod_id = @mod_id );						 
						 
								update dbo.tab_modulos_perfis
								set mfl_excluir = @novoEstado,
									mfl_data_atualizacao = GETDATE(),
									mfl_atualizado_por = @usu_id,
									mfl_ip = @ip
								 where per_id = @per_id
								 and mod_id in (select distinct mod_id 
														from  dbo.tab_modulos
														 where mod_pai_id = @mod_id);						
						 end
						 else 
							 begin
							 -- desativa o  modulo pai se não houver mais filhos ativos
								set @nFilhos = (select count(mfl_excluir) 
														from dbo.tab_modulos_perfis mfl
														inner join dbo.tab_modulos md on md.mod_id= mfl.mod_id and mod_deletado is null and mfl.mod_id >0
														where per_id = @per_id
															 and mod_pai_id = @mod_pai_id
															 and mfl_excluir = 1
												);
									if @nFilhos =0
										update dbo.tab_modulos_perfis
										set mfl_excluir = 0,
											mfl_data_atualizacao = GETDATE(),
											mfl_atualizado_por = @usu_id,
											mfl_ip = @ip
										 where per_id = @per_id
										 and mod_id = @mod_pai_id;
							 end
					end
			    else
					if (@operacao ='I' )
					begin
						update dbo.tab_modulos_perfis
						set mfl_inserir = ~ mfl_inserir,
							mfl_data_atualizacao = GETDATE(),
							mfl_atualizado_por = @usu_id,
							mfl_ip = @ip
						 where per_id = @per_id
						  and (mod_id = @mod_id or (mod_id = @mod_pai_id and mfl_inserir=0) );
						 
						 if (@mod_pai_id  < 0)
						 begin
								set @novoEstado = (select mfl_inserir 
												from dbo.tab_modulos_perfis  
												where per_id = @per_id
													 and mod_id = @mod_id );						 
						 
								update dbo.tab_modulos_perfis
								set mfl_inserir = @novoEstado,
									mfl_data_atualizacao = GETDATE(),
									mfl_atualizado_por = @usu_id,
									mfl_ip = @ip
								 where per_id = @per_id
								 and mod_id in (select distinct mod_id 
														from  dbo.tab_modulos
														 where mod_pai_id = @mod_id);						
						 end	
						 else 
							 begin
							 -- desativa o  modulo pai se não houver mais filhos ativos
								set @nFilhos = (select count(mfl_inserir) 
														from dbo.tab_modulos_perfis mfl
														inner join dbo.tab_modulos md on md.mod_id= mfl.mod_id and mod_deletado is null and mfl.mod_id >0
														where per_id = @per_id
															 and mod_pai_id = @mod_pai_id
															 and mfl_inserir = 1
												);
									if @nFilhos =0
										update dbo.tab_modulos_perfis
										set mfl_inserir = 0,
											mfl_data_atualizacao = GETDATE(),
											mfl_atualizado_por = @usu_id,
											mfl_ip = @ip
										 where per_id = @per_id
										 and mod_id = @mod_pai_id;
							 end
					end
					
		COMMIT TRAN T1

end try
begin catch
		ROLLBACK TRAN T1
            PRINT 'The following error has occurred:  ' + ERROR_MESSAGE()

		--Raise an error
		 DECLARE @ErrorMessage varchar(255), @ErrorSeverity int, @ErrorState int 
		 SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE() 
		 RAISERROR(@ErrorMessage,@ErrorSeverity,@ErrorState) 
end catch
