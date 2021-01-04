CREATE procedure [dbo].[STP_DEL_PERFIL] 
@per_id int,
@usu_id int,
@ip nvarchar(30)


--with encryption
as

begin try
		BEGIN TRAN T1
					
					update dbo.tab_perfis
					set per_deletado = GETDATE(),
						per_data_atualizacao = GETDATE(),
						per_atualizado_por = @usu_id
					 where per_id = @per_id;


					--exclui os modulos associados da dbo.tab_modulos_perfis
					update dbo.tab_modulos_perfis
					set mfl_deletado = GETDATE(),
						mfl_data_atualizacao = GETDATE(),
						mfl_atualizado_por = @usu_id
					 where per_id = @per_id;	
					 
					--exclui os grupos associados 
					update dbo.tab_perfis_grupos
					set pfg_deletado = GETDATE(),
						pfg_data_atualizacao = GETDATE(),
						pfg_atualizado_por = @usu_id
					 where per_id = @per_id;	
					 
					 
					--exclui os usuarios associados 
					update dbo.tab_perfis_usuarios
					set pfu_deletado = GETDATE(),
						pfu_data_atualizacao = GETDATE(),
						pfu_atualizado_por = @usu_id
					 where per_id = @per_id;						 
					 

					 				
			-- ********* INSERÇÃO DE LOG **************************************

				declare @tabela varchar(300) = 'tab_perfis';
				declare @tra_id int = 5; -- 5= "exclusão"
				declare @mod_id_log int = 1060; -- 1060 = perfis

				-- checa se a tmp table existem e a exclui
				set nocount on;
				if OBJECT_ID('tempdb..#tmpTabela') is not null
					DROP TABLE #tmpTabela;

				-- insere dados na tabela #tmpTabela
				SELECT * into #tmpTabela 
				from dbo.tab_perfis
				where per_id= @per_id ;


				-- concatena os valores e retorna em varchar
				declare @log_texto varchar(MAX); 
				exec dbo.STP_CONCATENA_COLS_VALS  @tabela, @retorno = @log_texto output;

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
