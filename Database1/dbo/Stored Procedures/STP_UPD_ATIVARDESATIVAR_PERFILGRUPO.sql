CREATE procedure [dbo].[STP_UPD_ATIVARDESATIVAR_PERFILGRUPO] 
@per_id int,
@gru_id int,
@usu_id int,
@ip nvarchar(30)

--with encryption
as
begin try
		BEGIN TRAN T1
					
					declare @jatem int = ( select COUNT(*)
                          					from dbo.tab_perfis_grupos
											where per_id = @per_id
												 and gru_id = @gru_id );
					
												 				
				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

					-- cria a #tmpTabela e limpa
					SELECT top 1 per_id, gru_id, pfg_deletado, pfg_data_criacao, pfl_criado_por, pfg_data_atualizacao, pfg_atualizado_por 
					into #tmpTabela 
					from dbo.tab_perfis_grupos;

					delete from #tmpTabela ;

					-- continua		
					if 	@jatem =0
					begin
					  -- insere na tabela tab_perfis_grupos
						insert into dbo.tab_perfis_grupos ( per_id, gru_id, pfg_data_criacao, pfl_criado_por)
						values (@per_id, @gru_id, getdate(), @usu_id);

						-- insere dados novos na tabela #tmpTabela 
						insert into #tmpTabela 
						SELECT per_id, gru_id, pfg_deletado, pfg_data_criacao, pfl_criado_por, pfg_data_atualizacao, pfg_atualizado_por 
                          	from dbo.tab_perfis_grupos
							where per_id = @per_id
									and gru_id = @gru_id;
					end
					else
					begin
					-- insere dados antigos na tabela #tmpTabela 
						insert into #tmpTabela 
						SELECT per_id, gru_id, pfg_deletado, pfg_data_criacao, pfl_criado_por, pfg_data_atualizacao, pfg_atualizado_por 
                          	from dbo.tab_perfis_grupos
							where per_id = @per_id
									and gru_id = @gru_id;						

						-- apaga da tabela tab_perfis_grupos
						delete from dbo.tab_perfis_grupos
						where per_id = @per_id
							 and gru_id = @gru_id ;						
					end;


			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_perfis_grupos';
				declare @tra_id int = 8; --8= "ativacão"
				declare @mod_id_log int = -1065; -- 1065 = tab_perfis_grupos

				if @jatem =1 
				   set @tra_id = 9; -- 9= desativacao / registro desativado

				-- checa se a tmp table existem e a exclui
				set nocount on;

				-- compara as linhas e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output, @tudo=1;

				-- exclui a temporaria
				DROP TABLE #tmpTabela;

				set nocount off;
				exec dbo.STP_INS_LOGSISTEMA @tra_id, @usu_id, @mod_id_log,	@log_texto,	@ip			

			 -- ****************************************************************

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
